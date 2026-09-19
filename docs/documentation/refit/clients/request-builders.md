---
Order: 4
---
# Request builders

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/clients-request-builders/clients-request-builders.csproj).

Sometimes a library needs to choose an API method while the app is running, then build and
send that call. Refit's request builders provide the lower-level pieces for that work.
They are useful for a custom client factory or an integration that works with method metadata.

For a client whose interface you can call directly, start with [client creation](creation.md).
This page covers the extra control available through `RequestBuilder`.

## Select an overloaded method

Install `Refit.Reflection` for these examples.
They use runtime metadata and runtime generic compilation, so they belong to the
[separate reflection executable](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Clients/Reflection).
Generated JSON metadata keeps JSON writing and reading explicit, but does not make reflection request building AOT compatible.

**1. Define the methods.** The two `BuildAsync` methods differ by parameter type.
`Task<HttpRequestMessage>` returns a request without sending it.
The generic method attaches a local property.


```csharp
internal interface IRequestLookupApi
{
    [Get("/lookup/{id}")]
    Task<HttpRequestMessage> BuildAsync(int id);

    [Get("/lookup/{id}")]
    Task<HttpRequestMessage> BuildAsync(string id);

    [Get("/lookup")]
    Task<HttpRequestMessage> GenericAsync<T>([Property("value")] T value);
}
```

**2. Create the builder with your settings.** `IRequestBuilder<T>` identifies the API interface.
Its `Settings` property returns the settings used by the builder.

**3. Select the numeric overload.** Pass parameter types in declaration order.
The delegate takes the HTTP client and an `object[]` of arguments.
Its result follows the method's declared return type, so this example casts it to `Task<HttpRequestMessage>`.


```csharp
IRequestBuilder<IRequestLookupApi> lookup = RequestBuilder.ForType<IRequestLookupApi>(host.Settings);
Func<HttpClient, object[], object?> invoke = lookup.BuildRestResultFuncForMethod(nameof(IRequestLookupApi.BuildAsync), [typeof(int)]);
using HttpRequestMessage request = await (Task<HttpRequestMessage>)invoke(host.Client, [1])!;
Console.WriteLine(request.RequestUri); // /lookup/1
```

Dispose a request returned without sending it.
For a normal `Task<Person>` method, invoking the delegate sends the request and returns that task.
An observable method returns the observable shape, not an immediate reply.
See [return types](../results/return-types.md).

## Close a generic method

The third argument supplies generic type arguments.
The parameter-types array describes the resulting closed method signature.
This call selects `GenericAsync<int>(int)` and checks its local request property.


```csharp
Func<HttpClient, object[], object?> generic = lookup.BuildRestResultFuncForMethod(nameof(IRequestLookupApi.GenericAsync), [typeof(int)], [typeof(int)]);
using HttpRequestMessage genericRequest = await (Task<HttpRequestMessage>)generic(host.Client, [1])!;
_ = genericRequest.Options.TryGetValue(new("value"), out int value);
```

The builder caches delegates by method name, parameter types and generic type arguments.
It clones arrays stored as cache keys.
The interface must be an interface type, including a closed generic interface.
Inherited HTTP methods participate in lookup.

| Public member | Contract |
| --- | --- |
| `RequestBuilder.ForType<T>()` | Creates a typed reflection builder with default settings. |
| `RequestBuilder.ForType<T>(RefitSettings?)` | Creates a typed reflection builder with supplied settings or defaults. |
| `RequestBuilder.ForType(Type)` | Creates an untyped reflection builder with defaults. |
| `RequestBuilder.ForType(Type, RefitSettings?)` | Creates an untyped reflection builder with supplied settings or defaults. |
| `IRequestBuilder.Settings` | Gets the settings used by the builder. |
| `BuildRestResultFuncForMethod(string, Type[]? = null, Type[]? = null)` | Returns `Func<HttpClient, object[], object?>` for the selected HTTP method. |
| `IRequestBuilder<T>` | Identifies the target API interface and inherits the untyped contract. |

A non-interface or null target throws `ArgumentException`.
A missing HTTP method or an overloaded name without parameter types throws `ArgumentException`.
An unsuitable parameter signature throws `InvalidOperationException`.
Generic arguments must satisfy the method's generic constraints.
An absent reflection package throws `NotSupportedException` with installation guidance.

## Register another generated implementation

Refit's generator uses public registration APIs to make implementations available to `ForGenerated`.
They are hidden from ordinary editor completion through `EditorBrowsable(Never)`.
A generator integration can supply factories directly.
Registering another factory for the same interface replaces that entry.

The [advanced sample implementation](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Clients/RegisteredClient.cs)
retains the client base address and settings, making each registration shape observable.
It is a small hand-written implementation used to demonstrate this infrastructure.
`RegisteredInterface` contains `typeof(IRegisteredClient)`.


```csharp
RestService.RegisterGeneratedFactory<IRegisteredClient>(static (http, builder) => new RegisteredClient(http, builder.Settings));
IRegisteredClient typed = RestService.ForGenerated<IRegisteredClient>(client, JsonSettings);

RestService.RegisterGeneratedFactory(RegisteredInterface, static (http, builder) => new RegisteredClient(http, builder.Settings));
IRegisteredClient selected = (IRegisteredClient)RestService.ForGenerated(RegisteredInterface, client, JsonSettings);

RestService.RegisterGeneratedSettingsFactory<IRegisteredClient>(static (http, settings) => new RegisteredClient(http, settings));
IRegisteredClient inline = RestService.ForGenerated<IRegisteredClient>(client, JsonSettings);
```

| Method | Factory contract |
| --- | --- |
| `RegisterGeneratedFactory<T>` | `Func<HttpClient, IRequestBuilder, T>`. |
| `RegisterGeneratedFactory` | Interface `Type` and `Func<HttpClient, IRequestBuilder, object>`. |
| `RegisterGeneratedSettingsFactory<T>` | `Func<HttpClient, RefitSettings, T>`. |

Registration rejects a null factory; the untyped overload also rejects a null interface type.
The settings-factory route constructs clients whose requests build inline.
The request-builder factory receives a generated-only builder when resolved through `ForGenerated`.
That builder exposes settings but throws `NotSupportedException` if the implementation asks it for a reflected method delegate.
Keep all requests generated when using these factories in an AOT app.
