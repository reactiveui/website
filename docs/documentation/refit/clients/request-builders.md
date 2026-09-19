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
Source-generated clients can build requests inline, but these APIs inspect interface and method metadata at runtime.
They carry both trimming and dynamic-code requirements, so keep them out of Native AOT paths.

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

| Declaration | Description | Parameters and defaults | Return/value | Source |
| --- | --- | --- | --- | --- |
| `RequestBuilder.ForType<T>(RefitSettings? settings)` | Resolves the optional reflection factory and creates a strongly typed builder for `T`; `null` settings are passed through to the factory. | [`RefitSettings`](settings.md) `settings`: settings for request construction, or `null`. `T` is the Refit API interface. | [`IRequestBuilder<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder%7BT%7D.cs) for `T`. | [RequestBuilder.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBuilder.cs) |
| `RequestBuilder.ForType<T>()` | Resolves the optional reflection factory and creates a strongly typed builder for `T` with `null` settings. | `T` is the Refit API interface. | [`IRequestBuilder<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder%7BT%7D.cs) for `T`. | [RequestBuilder.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBuilder.cs) |
| `RequestBuilder.ForType(Type refitInterfaceType, RefitSettings? settings)` | Resolves the optional reflection factory and creates a builder for the supplied Refit interface type. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: Refit interface, including a closed generic interface. [`RefitSettings`](settings.md) `settings`: settings for request construction, or `null`. | [`IRequestBuilder`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder.cs) for `refitInterfaceType`. | [RequestBuilder.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBuilder.cs) |
| `RequestBuilder.ForType(Type refitInterfaceType)` | Calls the settings overload with `null` and creates a builder for the supplied Refit interface type. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: Refit interface, including a closed generic interface. | [`IRequestBuilder`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder.cs) for `refitInterfaceType`. | [RequestBuilder.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBuilder.cs) |
| `IRequestBuilder.Settings` | Exposes the [`RefitSettings`](settings.md) instance used by this builder. | None. | [`RefitSettings`](settings.md) used by the builder. | [IRequestBuilder.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder.cs) |
| `IRequestBuilder.BuildRestResultFuncForMethod(string methodName, Type[]? parameterTypes = null, Type[]? genericArgumentTypes = null)` | Resolves and caches a delegate for a reflected interface method. The delegate builds the request and follows the method's declared return shape when invoked. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `methodName`: interface method name. [`Type[]`](https://learn.microsoft.com/dotnet/api/system.type) `parameterTypes`: declaration-order parameter types, default `null`; required to select among overloads. [`Type[]`](https://learn.microsoft.com/dotnet/api/system.type) `genericArgumentTypes`: types used to close a generic method, default `null`. | [`Func<HttpClient, object[], object?>`](https://learn.microsoft.com/dotnet/api/system.func-3), taking an [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) and argument array and returning the method's declared result. | [IRequestBuilder.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder.cs) |
| `RestService.RegisterGeneratedFactory(Type refitInterfaceType, Func<HttpClient, IRequestBuilder, object> factory)` | Stores a source-generated factory under an interface [`Type`](https://learn.microsoft.com/dotnet/api/system.type); a later registration for the same type replaces it. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: interface key. [`Func<HttpClient, IRequestBuilder, object>`](https://learn.microsoft.com/dotnet/api/system.func-3) `factory`: receives the client and a generated-only [`IRequestBuilder`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder.cs). | `void`; stores the factory. Null type or factory throws `ArgumentNullException`. | [RestService.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/RestService.cs) |
| `RestService.RegisterGeneratedFactory<T>(Func<HttpClient, IRequestBuilder, T> factory)` | Stores a typed source-generated factory under `typeof(T)`. | [`Func<HttpClient, IRequestBuilder, T>`](https://learn.microsoft.com/dotnet/api/system.func-3) `factory`: receives the client and generated-only builder and returns `T`. `T` is the Refit interface. | `void`; stores the typed factory. A null factory throws `ArgumentNullException`. | [RestService.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/RestService.cs) |
| `RestService.RegisterGeneratedSettingsFactory<T>(Func<HttpClient, RefitSettings, T> factory)` | Stores a typed source-generated factory that receives settings directly, so generated clients can build requests inline without reflection. | [`Func<HttpClient, RefitSettings, T>`](https://learn.microsoft.com/dotnet/api/system.func-3) `factory`: receives the client and settings and returns `T`. `T` is the Refit interface. | `void`; stores the settings factory. A null factory throws `ArgumentNullException`. | [RestService.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/RestService.cs) |
| `IRequestBuilder` | Defines the settings property and dynamic method-delegate operation used by request builders. | None. | Interface implemented by reflection and generated-only builders. | [IRequestBuilder.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder.cs) |
| `IRequestBuilder<T>` | Carries the target API interface type `T` while inheriting the untyped builder contract. | `T` is the Refit API interface. | [`IRequestBuilder`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder.cs). | [`IRequestBuilder{T}.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder%7BT%7D.cs) |
| `RequestBuilder` | Provides static entry points that resolve the optional reflection request-builder factory. | None. | Static class. | [RequestBuilder.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBuilder.cs) |

Production implementations: [`RequestBuilder.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBuilder.cs), [`IRequestBuilder.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder.cs), [`IRequestBuilder{T}.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder%7BT%7D.cs), and [`RestService.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/RestService.cs).

A non-interface target is rejected by the reflection factory; a null target throws `ArgumentNullException`.
A missing HTTP method or an overloaded name without parameter types throws `ArgumentException`.
When no reflected method matches the supplied signature, the builder throws `InvalidOperationException`;
invalid Refit parameter declarations also throw `ArgumentException`.
Generic arguments must satisfy the method's generic constraints.
An absent reflection package throws `NotSupportedException` with installation guidance.

## Register another generated implementation

Refit's generator uses public registration APIs to make implementations available to `ForGenerated`.
They are hidden from ordinary editor completion through `EditorBrowsable(Never)`.
A generator integration can supply factories directly.
Registering another factory through the same registration method for the same interface replaces that entry.
The settings-factory registry is separate from the request-builder-factory registry and takes precedence when resolving a generated client.

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

Registration rejects a null factory; the untyped overload also rejects a null interface type.
The settings-factory route constructs clients whose requests build inline.
The request-builder factory receives a generated-only builder when resolved through `ForGenerated`.
That builder exposes settings but throws `NotSupportedException` if the implementation asks it for a reflected method delegate.
Keep all requests generated when using these factories in an AOT app.
