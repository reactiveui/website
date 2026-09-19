---
Order: 1
---
# Create a client

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/clients-creation/clients-creation.csproj).

Once you have described a service on an interface, you need an object your app can call.
Refit creates that client for you. Give it the HTTP client and settings you want to use,
then call the interface methods to make requests.

Start with `RestService.ForGenerated<T>`. It uses the implementation Refit generated for
your interface and works with the generated JSON setup shown below.

## Reuse your HTTP client

`ForGenerated<T>` does not switch an unsupported method to reflected request building.

**1. Describe the API.** The complete sample also declares methods that demonstrate authorization.
`Person` is the model from [your first request](../index.md#your-first-request).


```csharp
internal interface IClientApi
{
    [Get("/clients/person")]
    Task<Person> ReadAsync();

    [Get("/clients/authorized")]
    [Headers("Authorization: Bearer")]
    Task<Person> AuthorizedAsync();

    [Get("/clients/explicit")]
    Task<Person> ExplicitAsync([Authorize] string token);
}
```

**2. Reuse JSON metadata.** The [shared context](../serialization/json.md) registers `Person`.
The source file imports `System.Text.Json`. Its settings live in the sample class.


```csharp
private static readonly JsonSerializerOptions JsonOptions = new(SampleJsonContext.Default.Options) { TypeInfoResolver = SampleJsonContext.Default };

private static readonly RefitSettings JsonSettings = new(new SystemTextJsonContentSerializer(JsonOptions));
```

**3. Create the implementation.** `host.Client` is the shared client with the local sample transport.
Pass your app's client with its `BaseAddress` set when you use this in your app.
Keep the client alive for the calls that use it.

This supplied-client shape keeps the transport, handlers and fixed authentication headers in
`HttpClient`, while `ForGenerated<T>` supplies only the typed Refit implementation. It is the
generated path to use when trimming or Native AOT matters.


```csharp
IClientApi api = RestService.ForGenerated<IClientApi>(host.Client, JsonSettings);
Person person = await api.ReadAsync();
Console.WriteLine(person.Name); // Ada
```

The [runnable source](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Clients/Clients.cs)
checks the returned person. Dispose your shared HTTP client when its owner shuts down.
Creating a Refit implementation around a supplied client does not create another HTTP client.

## Select an interface through Type

Use the non-generic overload when your app selects a known interface through a `Type` value.
It returns `object`, so the caller casts the implementation.
Its settings parameter is required.
The sample's `ClientInterface` contains `typeof(IClientApi)`.


```csharp
object implementation = RestService.ForGenerated(ClientInterface, host.Client, JsonSettings);
IClientApi selected = (IClientApi)implementation;
```

This lookup can resolve a registered generated interface without runtime generic construction.
Loading unknown interface types by name is a separate reflection concern.
Keep each interface and its generated implementation available in the published app.

## Own a client created from a URL

`CreateHttpClient` gives you a client you can explicitly dispose.
It sets `BaseAddress`, uses `HttpMessageHandlerFactory` when supplied, and installs the settings token handler.
The example's `settings` use generated JSON metadata and a local handler factory.


```csharp
using HttpClient client = RestService.CreateHttpClient(BaseUrl, settings);
IClientApi api = RestService.ForGenerated<IClientApi>(client, settings);
Person person = await api.ReadAsync();
```

The string overloads of `ForGenerated` create their own HTTP client.
For an interface that inherits `IDisposable`, Refit generates disposal that disposes that client.
Use that interface only when you want it to own the transport.
`OwnedClientInterface` contains `typeof(IOwnedClientApi)` in this example.


```csharp
internal interface IOwnedClientApi : IClientApi, IDisposable;
```


```csharp
using IOwnedClientApi owned = RestService.ForGenerated<IOwnedClientApi>(BaseUrl, settings);
using IOwnedClientApi defaultOwned = RestService.ForGenerated<IOwnedClientApi>(BaseUrl);
using IOwnedClientApi selectedOwned = (IOwnedClientApi)RestService.ForGenerated(OwnedClientInterface, BaseUrl, settings);
```

Disposing such an implementation also disposes a supplied HTTP client.
Use an ordinary interface for clients sharing a transport owned elsewhere.
Prefer an explicitly owned `HttpClient` when the API interface does not expose disposal.

`CreateHttpClient` rejects a null, empty or whitespace URL.
An invalid base address fails URI validation.
Its settings parameter can be null.
Legacy URL resolution trims trailing slashes; RFC resolution preserves them.
See [URL resolution settings](settings.md#url-resolution).

## Choose the reflection path when needed

`RestService.For` supports interfaces whose methods need runtime request building.
Install `Refit.Reflection` for that fallback.
This path uses reflection to inspect metadata and can require runtime generic compilation.
It is unsuitable as a Native AOT fallback.


```csharp
IClientApi reflected = RestService.For<IClientApi>(host.Client, host.Settings);
IRequestBuilder<IClientApi> builder = RequestBuilder.ForType<IClientApi>(host.Settings);
IClientApi suppliedBuilder = RestService.For(host.Client, builder);
object runtimeSelected = RestService.For(ClientInterface, host.Client, builder);
```

The settings overload first tries a fully inline generated implementation.
If that is unavailable, it creates a reflected request builder.
The supplied-builder overload uses the builder you provide.
All `For` overloads carry trimming warnings because their fallback needs runtime metadata.
See [request builders](request-builders.md) for selecting reflected methods.

## Creation overloads

| Method | Accepted arguments after the interface selection |
| --- | --- |
| `ForGenerated<T>` | `HttpClient`; `HttpClient, RefitSettings`; `string`; `string, RefitSettings`. |
| `ForGenerated` | `Type, HttpClient, RefitSettings`; `Type, string, RefitSettings`. |
| `For<T>` | `HttpClient`; `HttpClient, RefitSettings?`; `HttpClient, IRequestBuilder<T>`; `string`; `string, RefitSettings?`. |
| `For` | `Type, HttpClient`; `Type, HttpClient, RefitSettings?`; `Type, HttpClient, IRequestBuilder`; `Type, string`; `Type, string, RefitSettings?`. |
| `CreateHttpClient` | `string, RefitSettings?`; returns the HTTP client for the caller to own. |

Generic overloads without settings use defaults.
Those defaults do not register your JSON models for AOT.
Supply generated serializer metadata for calls that serialize or read models.
Generated overloads that take settings reject null settings and null clients.
An unavailable generated implementation throws `InvalidOperationException`.
An unsupported generated method throws instead of using reflected fallback.
Fix the generator's `RF006` diagnostic before using that method through generated-only creation.

On .NET Framework, generated registration differs because module initializers are unavailable.
Refit can resolve the emitted implementation type by name and construct it through reflection.
The generated-only request builder still rejects reflected method fallback.
The modern .NET examples avoid that type-name construction path.
