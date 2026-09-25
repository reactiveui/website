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

**2. Give Refit the JSON context.** The [shared context](../serialization/json.md) registers `Person`.
Refit reads and writes JSON with the context's own options, and it never falls back to reflection.
This is the short path. It needs no options or settings object.

**3. Create the implementation.** Pass `httpClient`, your app's shared `HttpClient` with its `BaseAddress` set.
Keep the client alive for the calls that use it.

This supplied-client shape keeps the transport, handlers and fixed authentication headers in
`HttpClient`, while `ForGenerated<T>` supplies only the typed Refit implementation. It is the
generated path to use when trimming or Native AOT matters.


```csharp
IClientApi api = RestService.ForGenerated<IClientApi>(httpClient, SampleJsonContext.Default);
Person person = await api.ReadAsync();
Console.WriteLine(person.Name); // Ada
```

The [runnable source](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Clients/Clients.cs)
checks the returned person. Dispose your shared HTTP client when its owner shuts down.
Creating a Refit implementation around a supplied client does not create another HTTP client.
The context overloads exist on .NET 8 and later.

## Use settings instead of a context

`RefitSettings` holds the serializer and every other choice for a client: headers, formatters and error handling.
Pass settings when you need more than JSON, or when you prefer to own the JSON options.
Pick the options way when you have `JsonSerializerOptions` to share, need full control,
or want one options object reused elsewhere in your app.
The short path needs no options object. The options way makes you assign the `TypeInfoResolver` yourself
and keep the options unchanged after first use.
Add `using System.Text.Json;` for `JsonSerializerOptions`.


```csharp
private static readonly JsonSerializerOptions JsonOptions = new(SampleJsonContext.Default.Options) { TypeInfoResolver = SampleJsonContext.Default };
```

`ForGenerated<T>` takes settings built from those options in place of the context:

```csharp
RefitSettings settings = new(new SystemTextJsonContentSerializer(JsonOptions));
IClientApi withSettings = RestService.ForGenerated<IClientApi>(httpClient, settings);
```

`RefitSettings.ForJsonContext(context)` is a shortcut that builds settings from a context.
They use the context's own options, like the short path.

```csharp
RefitSettings shortcut = RefitSettings.ForJsonContext(SampleJsonContext.Default);
IClientApi withShortcut = RestService.ForGenerated<IClientApi>(httpClient, shortcut);
```

When you have settings and want the context too, `ForGenerated(client, context, settings)` is the bridge.
It keeps your settings and adds the context, so you do not assign a `TypeInfoResolver` by hand.
It sets the result as `settings.ContentSerializer`. Your naming policy and converters apply.
See [choose whose settings apply](../serialization/json.md#choose-whose-settings-apply).
This example keeps a `Buffered` setting next to the context.

```csharp
IClientApi bridged = RestService.ForGenerated<IClientApi>(httpClient, SampleJsonContext.Default, new RefitSettings { Buffered = true });
```

## Select an interface through Type

Use the non-generic overload when your app selects a known interface through a `Type` value.
It returns `object`, so the caller casts the implementation.
Its settings parameter is required, and it has no context overload.
Pass `settings` from above, or settings built with `RefitSettings.ForJsonContext`.


```csharp
Type apiType = typeof(IClientApi);
object implementation = RestService.ForGenerated(apiType, httpClient, settings);
IClientApi selected = (IClientApi)implementation;
```

This lookup can resolve a registered generated interface without runtime generic construction.
Loading unknown interface types by name is a separate reflection concern.
Keep each interface and its generated implementation available in the published app.

## Own a client created from a URL

`CreateHttpClient` gives you a client you can explicitly dispose.
It sets `BaseAddress`, uses `HttpMessageHandlerFactory` when supplied, and installs the settings token handler.


```csharp
using HttpClient client = RestService.CreateHttpClient("https://people.example", settings);
IClientApi api = RestService.ForGenerated<IClientApi>(client, settings);
Person person = await api.ReadAsync();
```

The string overloads of `ForGenerated`, with or without a context, create their own HTTP client.
For an interface that inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable),
the generated implementation disposes that client. Use that interface only when you want it to own
the transport.


```csharp
internal interface IOwnedClientApi : IClientApi, IDisposable;
```


```csharp
const string baseUrl = "https://people.example";
using IOwnedClientApi owned = RestService.ForGenerated<IOwnedClientApi>(baseUrl, settings);
using IOwnedClientApi contextOwned = RestService.ForGenerated<IOwnedClientApi>(baseUrl, SampleJsonContext.Default);
using IOwnedClientApi defaultOwned = RestService.ForGenerated<IOwnedClientApi>(baseUrl);
using IOwnedClientApi selectedOwned = (IOwnedClientApi)RestService.ForGenerated(typeof(IOwnedClientApi), baseUrl, settings);
```

Disposing such an implementation also disposes a supplied HTTP client.
Use an ordinary interface for clients that share a transport owned elsewhere.
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
IClientApi reflected = RestService.For<IClientApi>(httpClient, settings);
IRequestBuilder<IClientApi> builder = RequestBuilder.ForType<IClientApi>(settings);
IClientApi suppliedBuilder = RestService.For(httpClient, builder);
object runtimeSelected = RestService.For(typeof(IClientApi), httpClient, builder);
```

The settings overload first tries a fully inline generated implementation.
If that is unavailable, it creates a reflected request builder.
The supplied-builder overload uses the builder you provide.
All `For` overloads carry trimming warnings because their fallback needs runtime metadata.
See [request builders](request-builders.md) for selecting reflected methods.

## Creation overloads

Each row describes one public overload. The `ForGenerated` overloads use only source-generated implementations. The `For` overloads can create a reflected request builder when an inline generated implementation is unavailable.

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `RestService` | Static factory class for creating Refit interface implementations. | None. | — |
| `CreateHttpClient(string hostUrl, RefitSettings? settings)` | Creates an HTTP client, chooses the configured handler chain, and sets its base address. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address; [`RefitSettings`](settings.md) `settings`: nullable settings for handlers and URL resolution. | [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) with the configured base address; the caller owns it. |
| `ForGenerated<T>(HttpClient client)` | Resolves the registered generated implementation for `T` with default settings and never builds reflected requests. | [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: non-null client used by the implementation. | `T`: generated implementation; if `T` inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing it also disposes `client`. Throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) when no generated implementation is available on modern .NET. |
| `ForGenerated<T>(HttpClient client, RefitSettings settings)` | Resolves the registered generated implementation for `T` with the supplied settings and never builds reflected requests. | [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: non-null client; [`RefitSettings`](settings.md) `settings`: non-null serializer and request settings. | `T`: generated implementation; if `T` inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing it also disposes `client`. Throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) when no generated implementation is available on modern .NET. |
| `ForGenerated<T>(HttpClient client, JsonSerializerContext context)` | Resolves the registered generated implementation for `T` and reads and writes JSON with the context's own options. It never builds reflected requests and never uses reflection-based JSON. | [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: non-null client; [`JsonSerializerContext`](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonserializercontext) `context`: the generated context. | `T`: generated implementation. Throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) when no generated implementation is available. A request or reply type that the context does not list throws [`NotSupportedException`](https://learn.microsoft.com/dotnet/api/system.notsupportedexception). .NET 8 and later. |
| `ForGenerated<T>(HttpClient client, JsonSerializerContext context, bool allowReflectionFallback)` | Resolves the generated implementation for `T` with the context's own options, optionally with a reflection fallback. | `client`; `context`; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `allowReflectionFallback`: `true` lets a type the context does not list use reflection-based JSON. Reflection is not trim or Native AOT safe. | `T`: generated implementation. .NET 8 and later. |
| `ForGenerated<T>(HttpClient client, JsonSerializerContext context, RefitSettings settings)` | Resolves the generated implementation for `T`, keeps the settings' serializer options and adds the context. | `client`; `context`; [`RefitSettings`](settings.md) `settings`: non-null settings whose serializer is a `SystemTextJsonContentSerializer`. | `T`: generated implementation. Sets `settings.ContentSerializer` to a serializer built on a copy of its options plus the context. Throws `InvalidOperationException` when the settings use another serializer. .NET 8 and later. |
| `ForGenerated<T>(HttpClient client, JsonSerializerContext context, RefitSettings settings, bool allowReflectionFallback)` | Resolves the generated implementation for `T`, keeps the settings' serializer options and adds the context, optionally with a reflection fallback. | `client`; `context`; `settings`; `allowReflectionFallback`: `true` lets a type no resolver lists use reflection-based JSON. Not trim or Native AOT safe. | `T`: generated implementation. .NET 8 and later. |
| `ForGenerated<T>(string hostUrl, JsonSerializerContext context)` | Creates an HTTP client, then resolves the generated implementation for `T` on the context's own options with reflection-based JSON off. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address; `context`. | `T`: generated implementation; if `T` inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing it also disposes the created client. .NET 8 and later. |
| `ForGenerated<T>(string hostUrl, JsonSerializerContext context, bool allowReflectionFallback)` | Creates an HTTP client, then resolves the generated implementation for `T` on the context's own options, optionally with a reflection fallback. | `hostUrl`; `context`; `allowReflectionFallback`. | `T`: generated implementation. .NET 8 and later. |
| `ForGenerated<T>(string hostUrl, JsonSerializerContext context, RefitSettings settings)` | Creates an HTTP client with the supplied settings, keeps their serializer options and adds the context. | `hostUrl`; `context`; `settings`: non-null settings whose serializer is a `SystemTextJsonContentSerializer`. | `T`: generated implementation. Sets `settings.ContentSerializer` to the composed serializer. .NET 8 and later. |
| `ForGenerated<T>(string hostUrl, JsonSerializerContext context, RefitSettings settings, bool allowReflectionFallback)` | Creates an HTTP client with the supplied settings, keeps their serializer options and adds the context, optionally with a reflection fallback. | `hostUrl`; `context`; `settings`; `allowReflectionFallback`. | `T`: generated implementation. .NET 8 and later. |
| `ForGenerated<T>(string hostUrl)` | Creates an HTTP client with default settings, then resolves the generated implementation for `T`. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address for the created client. | `T`: generated implementation; if `T` inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing it also disposes the created client. |
| `ForGenerated<T>(string hostUrl, RefitSettings settings)` | Creates an HTTP client with the supplied settings, then resolves the generated implementation for `T`. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address; [`RefitSettings`](settings.md) `settings`: non-null serializer and request settings. | `T`: generated implementation; if `T` inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing it also disposes the created client. |
| `ForGenerated(Type refitInterfaceType, HttpClient client, RefitSettings settings)` | Resolves a generated implementation for the runtime interface type over the supplied client. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: non-null Refit interface; [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: non-null transport; [`RefitSettings`](settings.md) `settings`: non-null settings. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) implementing the interface. For a source-generated disposable interface, disposing the cast implementation also disposes `client`. Throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) when no generated implementation is available on modern .NET. |
| `ForGenerated(Type refitInterfaceType, string hostUrl, RefitSettings settings)` | Creates an HTTP client with the supplied settings, then resolves the generated implementation for the runtime interface type. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: non-null Refit interface; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address; [`RefitSettings`](settings.md) `settings`: non-null settings. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) implementing the interface. For a source-generated disposable interface, disposing the cast implementation also disposes the created client. |
| `For<T>(HttpClient client)` | Creates `T` over a shared client with default settings, using an inline generated implementation when registered and otherwise a reflected request builder. | [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: transport used for requests. | `T`: implementation for `T`; reflection can build requests when no inline generated implementation is registered. A source-generated `T` that inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) disposes `client` when disposed. |
| `For<T>(HttpClient client, RefitSettings? settings)` | Creates `T` over a shared client, preferring an inline generated implementation and otherwise creating a reflected request builder. | [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: transport; [`RefitSettings`](settings.md) `settings`: nullable settings, where `null` selects defaults. | `T`: implementation for `T`; the reflected path uses the supplied settings. A source-generated `T` that inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) disposes `client` when disposed. |
| `For<T>(HttpClient client, IRequestBuilder<T> builder)` | Creates `T` over a shared client with the request builder you supply. | [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: transport; [`IRequestBuilder<T>`](request-builders.md) `builder`: request builder for `T`. | `T`: implementation using the supplied request builder. A source-generated `T` that inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) disposes `client` when disposed. |
| `For<T>(string hostUrl)` | Creates an HTTP client with default settings, then creates `T`, using generated inline requests when available and reflection otherwise. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address for the created client. | `T`: implementation for `T`; if `T` inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing it also disposes the created client. |
| `For<T>(string hostUrl, RefitSettings? settings)` | Creates an HTTP client with the selected settings, then creates `T`, using generated inline requests when available and reflection otherwise. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address; [`RefitSettings`](settings.md) `settings`: nullable settings, where `null` selects defaults. | `T`: implementation for `T`; if `T` inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing it also disposes the created client. |
| `For(Type refitInterfaceType, HttpClient client)` | Creates the runtime-selected interface over a shared client with default settings, using generated inline requests when available and reflection otherwise. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: interface to implement; [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: transport. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) implementing `refitInterfaceType`, using default settings. A source-generated disposable interface disposes `client` when its cast implementation is disposed. |
| `For(Type refitInterfaceType, HttpClient client, RefitSettings? settings)` | Creates the runtime-selected interface over a shared client, preferring generated inline requests and otherwise creating a reflected request builder. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: interface to implement; [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: transport; [`RefitSettings`](settings.md) `settings`: nullable settings, where `null` selects defaults. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) implementing `refitInterfaceType`; the reflected path uses the selected settings. A source-generated disposable interface disposes `client` when its cast implementation is disposed. |
| `For(Type refitInterfaceType, HttpClient client, IRequestBuilder builder)` | Creates the runtime-selected interface with the non-generic request builder you supply. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: interface to implement; [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: transport; [`IRequestBuilder`](request-builders.md) `builder`: request builder to use. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) implementing `refitInterfaceType`, using the supplied builder. A source-generated disposable interface disposes `client` when its cast implementation is disposed. |
| `For(Type refitInterfaceType, string hostUrl)` | Creates an HTTP client with default settings, then creates the runtime-selected interface, using generated inline requests when available and reflection otherwise. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: interface to implement; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address for the created client. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) implementing `refitInterfaceType`, using default settings; if that interface inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing the cast implementation also disposes the created client. |
| `For(Type refitInterfaceType, string hostUrl, RefitSettings? settings)` | Creates an HTTP client with the selected settings, then creates the runtime-selected interface, using generated inline requests when available and reflection otherwise. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: interface to implement; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address; [`RefitSettings`](settings.md) `settings`: nullable settings, where `null` selects defaults. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) implementing `refitInterfaceType`; if that interface inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing the cast implementation also disposes the created client. |

Production implementations: [`RestService.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/RestService.cs), [`RequestBuilder.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBuilder.cs), and [`IRequestBuilder.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder.cs).

Generic overloads without settings or a context use defaults.
Those defaults do not register your JSON models for AOT.
Pass a context, or settings that hold generated serializer metadata, for calls that serialize or read models.
Generated overloads that take settings reject null settings and null clients.
An unavailable generated implementation throws `InvalidOperationException`.
An unsupported generated method throws instead of using reflected fallback.
Fix the generator's `RF006` diagnostic before using that method through generated-only creation.

On .NET Framework, generated registration differs because module initializers are unavailable.
Refit can resolve the emitted implementation type by name and construct it through reflection.
The generated-only request builder still rejects reflected method fallback.
The modern .NET examples avoid that type-name construction path.
