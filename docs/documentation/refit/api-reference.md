---
Order: 9
---
# Refit API reference

Find Refit types, overloads, parameters, and return values in one place. The tables are grouped
by category and topic. Follow a topic link for its walkthrough, examples, and detailed behavior.
Use your browser's find command to look up a type or method name.

- [Clients and settings](#clients-and-settings)
- [Requests](#requests)
- [Results](#results)
- [Serialization](#serialization)
- [Testing](#testing)
- [Advanced APIs](#advanced-apis)

## Clients and settings

### Create a client

[Full description and examples](clients/creation.md).

Types: `Refit.RestService`.

#### Creation overloads

[Full description and examples](clients/creation.md).

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `RestService` | Static factory class for creating Refit interface implementations. | None. | — |
| `CreateHttpClient(string hostUrl, RefitSettings? settings)` | Creates an HTTP client, chooses the configured handler chain, and sets its base address. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address; [`RefitSettings`](clients/settings.md) `settings`: nullable settings for handlers and URL resolution. | [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) with the configured base address; the caller owns it. |
| `ForGenerated<T>(HttpClient client)` | Resolves the registered generated implementation for `T` with default settings and never builds reflected requests. | [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: non-null client used by the implementation. | `T`: generated implementation; if `T` inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing it also disposes `client`. Throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) when no generated implementation is available on modern .NET. |
| `ForGenerated<T>(HttpClient client, RefitSettings settings)` | Resolves the registered generated implementation for `T` with the supplied settings and never builds reflected requests. | [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: non-null client; [`RefitSettings`](clients/settings.md) `settings`: non-null serializer and request settings. | `T`: generated implementation; if `T` inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing it also disposes `client`. Throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) when no generated implementation is available on modern .NET. |
| `ForGenerated<T>(string hostUrl)` | Creates an HTTP client with default settings, then resolves the generated implementation for `T`. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address for the created client. | `T`: generated implementation; if `T` inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing it also disposes the created client. |
| `ForGenerated<T>(string hostUrl, RefitSettings settings)` | Creates an HTTP client with the supplied settings, then resolves the generated implementation for `T`. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address; [`RefitSettings`](clients/settings.md) `settings`: non-null serializer and request settings. | `T`: generated implementation; if `T` inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing it also disposes the created client. |
| `ForGenerated(Type refitInterfaceType, HttpClient client, RefitSettings settings)` | Resolves a generated implementation for the runtime interface type over the supplied client. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: non-null Refit interface; [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: non-null transport; [`RefitSettings`](clients/settings.md) `settings`: non-null settings. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) implementing the interface. For a source-generated disposable interface, disposing the cast implementation also disposes `client`. Throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) when no generated implementation is available on modern .NET. |
| `ForGenerated(Type refitInterfaceType, string hostUrl, RefitSettings settings)` | Creates an HTTP client with the supplied settings, then resolves the generated implementation for the runtime interface type. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: non-null Refit interface; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address; [`RefitSettings`](clients/settings.md) `settings`: non-null settings. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) implementing the interface. For a source-generated disposable interface, disposing the cast implementation also disposes the created client. |
| `For<T>(HttpClient client)` | Creates `T` over a shared client with default settings, using an inline generated implementation when registered and otherwise a reflected request builder. | [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: transport used for requests. | `T`: implementation for `T`; reflection can build requests when no inline generated implementation is registered. A source-generated `T` that inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) disposes `client` when disposed. |
| `For<T>(HttpClient client, RefitSettings? settings)` | Creates `T` over a shared client, preferring an inline generated implementation and otherwise creating a reflected request builder. | [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: transport; [`RefitSettings`](clients/settings.md) `settings`: nullable settings, where `null` selects defaults. | `T`: implementation for `T`; the reflected path uses the supplied settings. A source-generated `T` that inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) disposes `client` when disposed. |
| `For<T>(HttpClient client, IRequestBuilder<T> builder)` | Creates `T` over a shared client with the request builder you supply. | [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: transport; [`IRequestBuilder<T>`](clients/request-builders.md) `builder`: request builder for `T`. | `T`: implementation using the supplied request builder. A source-generated `T` that inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable) disposes `client` when disposed. |
| `For<T>(string hostUrl)` | Creates an HTTP client with default settings, then creates `T`, using generated inline requests when available and reflection otherwise. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address for the created client. | `T`: implementation for `T`; if `T` inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing it also disposes the created client. |
| `For<T>(string hostUrl, RefitSettings? settings)` | Creates an HTTP client with the selected settings, then creates `T`, using generated inline requests when available and reflection otherwise. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address; [`RefitSettings`](clients/settings.md) `settings`: nullable settings, where `null` selects defaults. | `T`: implementation for `T`; if `T` inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing it also disposes the created client. |
| `For(Type refitInterfaceType, HttpClient client)` | Creates the runtime-selected interface over a shared client with default settings, using generated inline requests when available and reflection otherwise. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: interface to implement; [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: transport. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) implementing `refitInterfaceType`, using default settings. A source-generated disposable interface disposes `client` when its cast implementation is disposed. |
| `For(Type refitInterfaceType, HttpClient client, RefitSettings? settings)` | Creates the runtime-selected interface over a shared client, preferring generated inline requests and otherwise creating a reflected request builder. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: interface to implement; [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: transport; [`RefitSettings`](clients/settings.md) `settings`: nullable settings, where `null` selects defaults. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) implementing `refitInterfaceType`; the reflected path uses the selected settings. A source-generated disposable interface disposes `client` when its cast implementation is disposed. |
| `For(Type refitInterfaceType, HttpClient client, IRequestBuilder builder)` | Creates the runtime-selected interface with the non-generic request builder you supply. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: interface to implement; [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: transport; [`IRequestBuilder`](clients/request-builders.md) `builder`: request builder to use. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) implementing `refitInterfaceType`, using the supplied builder. A source-generated disposable interface disposes `client` when its cast implementation is disposed. |
| `For(Type refitInterfaceType, string hostUrl)` | Creates an HTTP client with default settings, then creates the runtime-selected interface, using generated inline requests when available and reflection otherwise. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: interface to implement; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address for the created client. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) implementing `refitInterfaceType`, using default settings; if that interface inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing the cast implementation also disposes the created client. |
| `For(Type refitInterfaceType, string hostUrl, RefitSettings? settings)` | Creates an HTTP client with the selected settings, then creates the runtime-selected interface, using generated inline requests when available and reflection otherwise. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: interface to implement; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `hostUrl`: non-null, non-whitespace base address; [`RefitSettings`](clients/settings.md) `settings`: nullable settings, where `null` selects defaults. | [`object`](https://learn.microsoft.com/dotnet/api/system.object) implementing `refitInterfaceType`; if that interface inherits [`IDisposable`](https://learn.microsoft.com/dotnet/api/system.idisposable), disposing the cast implementation also disposes the created client. |

### Dependency injection

[Full description and examples](clients/dependency-injection.md).

Types: `Refit.HttpClientFactoryExtensions`, `Refit.ISettingsFor`, `Refit.SettingsFor<T>`.

#### Registration overloads

[Full description and examples](clients/dependency-injection.md).

| Declaration | Description | Parameters and defaults | Return/value type |
| --- | --- | --- | --- |
| `IServiceCollection.AddRefitClient(Type refitInterfaceType)` | Registers the reflection request builder for `refitInterfaceType` using the default settings and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | [`IServiceCollection`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection) receiver; [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: Refit interface. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers a reflection-capable client with default settings. |
| `IServiceCollection.AddRefitClient(Type refitInterfaceType, RefitSettings? settings)` | Registers the reflection request builder for `refitInterfaceType` using the supplied fixed settings reference and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; [`RefitSettings`](clients/settings.md) `settings`: nullable fixed settings. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers a reflection-capable client using those settings. |
| `IServiceCollection.AddRefitClient(Type refitInterfaceType, RefitSettings? settings, string? httpClientName)` | Registers the reflection request builder for `refitInterfaceType` using the supplied fixed settings reference and the supplied HTTP client name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; `settings`; [`string`](https://learn.microsoft.com/dotnet/api/system.string) `httpClientName`: nullable underlying client name. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers a reflection-capable client with fixed settings under that HTTP client name. |
| `IServiceCollection.AddRefitClient(Type refitInterfaceType, Func<IServiceProvider, RefitSettings?>? settingsAction)` | Registers the reflection request builder for `refitInterfaceType` using the settings returned by `settingsAction` through DI and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; [`Func<IServiceProvider, RefitSettings?>`](https://learn.microsoft.com/dotnet/api/system.func-2) `settingsAction`: nullable provider settings factory. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers a reflection-capable client whose settings come from DI. |
| `IServiceCollection.AddRefitClient(Type refitInterfaceType, Func<IServiceProvider, RefitSettings?>? settingsAction, string? httpClientName)` | Registers the reflection request builder for `refitInterfaceType` using the settings returned by `settingsAction` through DI and the supplied HTTP client name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; `settingsAction`; `httpClientName`. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers a reflection-capable client with DI-provided settings under that HTTP client name. |
| `IServiceCollection.AddRefitClient<T>()` | Registers the reflection request builder for `T` using the default settings and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | None; `T : class` is the Refit interface. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers reflection-capable `T` with default settings. |
| `IServiceCollection.AddRefitClient<T>(RefitSettings? settings)` | Registers the reflection request builder for `T` using the supplied fixed settings reference and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `settings`: nullable fixed settings. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers reflection-capable `T` using those settings. |
| `IServiceCollection.AddRefitClient<T>(RefitSettings? settings, string? httpClientName)` | Registers the reflection request builder for `T` using the supplied fixed settings reference and the supplied HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `settings`; `httpClientName`. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers reflection-capable `T` with fixed settings under that HTTP client name. |
| `IServiceCollection.AddRefitClient<T>(Func<IServiceProvider, RefitSettings?>? settingsAction)` | Registers the reflection request builder for `T` using the settings returned by `settingsAction` through DI and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `settingsAction`: nullable provider settings factory. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers reflection-capable `T` with settings resolved from DI. |
| `IServiceCollection.AddRefitClient<T>(Func<IServiceProvider, RefitSettings?>? settingsAction, string? httpClientName)` | Registers the reflection request builder for `T` using the settings returned by `settingsAction` through DI and the supplied HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `settingsAction`; `httpClientName`. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers reflection-capable `T` with DI-provided settings under that HTTP client name. |
| `IServiceCollection.AddKeyedRefitClient(Type refitInterfaceType, object? serviceKey)` | Registers the reflection request builder for `refitInterfaceType` under the required non-null service key using the default settings and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; [`object`](https://learn.microsoft.com/dotnet/api/system.object) `serviceKey`: non-null DI key. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers a keyed reflection-capable client with default settings. |
| `IServiceCollection.AddKeyedRefitClient(Type refitInterfaceType, object? serviceKey, RefitSettings? settings)` | Registers the reflection request builder for `refitInterfaceType` under the required non-null service key using the supplied fixed settings reference and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; `serviceKey`; `settings`: nullable fixed settings. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers a keyed reflection-capable client using those settings. |
| `IServiceCollection.AddKeyedRefitClient(Type refitInterfaceType, object? serviceKey, RefitSettings? settings, string? httpClientName)` | Registers the reflection request builder for `refitInterfaceType` under the required non-null service key using the supplied fixed settings reference and the supplied HTTP client name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; `serviceKey`; `settings`; `httpClientName`. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers a keyed reflection-capable client with fixed settings under that HTTP client name. |
| `IServiceCollection.AddKeyedRefitClient(Type refitInterfaceType, object? serviceKey, Func<IServiceProvider, RefitSettings?>? settingsAction)` | Registers the reflection request builder for `refitInterfaceType` under the required non-null service key using the settings returned by `settingsAction` through DI and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; `serviceKey`; `settingsAction`: nullable provider settings factory. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers a keyed reflection-capable client with settings resolved from DI. |
| `IServiceCollection.AddKeyedRefitClient(Type refitInterfaceType, object? serviceKey, Func<IServiceProvider, RefitSettings?>? settingsAction, string? httpClientName)` | Registers the reflection request builder for `refitInterfaceType` under the required non-null service key using the settings returned by `settingsAction` through DI and the supplied HTTP client name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; `serviceKey`; `settingsAction`; `httpClientName`. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers a keyed reflection-capable client with DI-provided settings under that HTTP client name. |
| `IServiceCollection.AddKeyedRefitClient<T>(object? serviceKey)` | Registers the reflection request builder for `T` under the required non-null service key using the default settings and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`: non-null DI key. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed reflection-capable `T` with default settings. |
| `IServiceCollection.AddKeyedRefitClient<T>(object? serviceKey, RefitSettings? settings)` | Registers the reflection request builder for `T` under the required non-null service key using the supplied fixed settings reference and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`; `settings`: nullable fixed settings. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed reflection-capable `T` using those settings. |
| `IServiceCollection.AddKeyedRefitClient<T>(object? serviceKey, RefitSettings? settings, string? httpClientName)` | Registers the reflection request builder for `T` under the required non-null service key using the supplied fixed settings reference and the supplied HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`; `settings`; `httpClientName`. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed reflection-capable `T` with fixed settings under that HTTP client name. |
| `IServiceCollection.AddKeyedRefitClient<T>(object? serviceKey, Func<IServiceProvider, RefitSettings?>? settingsAction)` | Registers the reflection request builder for `T` under the required non-null service key using the settings returned by `settingsAction` through DI and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`; `settingsAction`: nullable provider settings factory. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed reflection-capable `T` with settings resolved from DI. |
| `IServiceCollection.AddKeyedRefitClient<T>(object? serviceKey, Func<IServiceProvider, RefitSettings?>? settingsAction, string? httpClientName)` | Registers the reflection request builder for `T` under the required non-null service key using the settings returned by `settingsAction` through DI and the supplied HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`; `settingsAction`; `httpClientName`. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed reflection-capable `T` with DI-provided settings under that HTTP client name. |
| `IServiceCollection.AddRefitGeneratedClient<T>()` | Registers the generated implementation of `T` using the default settings and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | None; `T : class` is the generated Refit interface. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers the generated-only implementation of `T` with default settings. |
| `IServiceCollection.AddRefitGeneratedClient<T>(RefitSettings? settings)` | Registers the generated implementation of `T` using the supplied fixed settings reference and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `settings`: nullable fixed settings. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers generated-only `T` using those settings. |
| `IServiceCollection.AddRefitGeneratedClient<T>(RefitSettings? settings, string? httpClientName)` | Registers the generated implementation of `T` using the supplied fixed settings reference and the supplied HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `settings`; `httpClientName`. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers generated-only `T` with fixed settings under that HTTP client name. |
| `IServiceCollection.AddRefitGeneratedClient<T>(Func<IServiceProvider, RefitSettings?>? settingsAction)` | Registers the generated implementation of `T` using the settings returned by `settingsAction` through DI and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `settingsAction`: nullable provider settings factory. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers generated-only `T` with settings resolved from DI. |
| `IServiceCollection.AddRefitGeneratedClient<T>(Func<IServiceProvider, RefitSettings?>? settingsAction, string? httpClientName)` | Registers the generated implementation of `T` using the settings returned by `settingsAction` through DI and the supplied HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `settingsAction`; `httpClientName`. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers generated-only `T` with DI-provided settings under that HTTP client name. |
| `IServiceCollection.AddKeyedRefitGeneratedClient<T>(object? serviceKey)` | Registers the generated implementation of `T` under the required non-null service key using the default settings and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`: non-null DI key. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed generated-only `T` with default settings. |
| `IServiceCollection.AddKeyedRefitGeneratedClient<T>(object? serviceKey, RefitSettings? settings)` | Registers the generated implementation of `T` under the required non-null service key using the supplied fixed settings reference and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`; `settings`: nullable fixed settings. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed generated-only `T` using those settings. |
| `IServiceCollection.AddKeyedRefitGeneratedClient<T>(object? serviceKey, RefitSettings? settings, string? httpClientName)` | Registers the generated implementation of `T` under the required non-null service key using the supplied fixed settings reference and the supplied HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`; `settings`; `httpClientName`. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed generated-only `T` with fixed settings under that HTTP client name. |
| `IServiceCollection.AddKeyedRefitGeneratedClient<T>(object? serviceKey, Func<IServiceProvider, RefitSettings?>? settingsAction)` | Registers the generated implementation of `T` under the required non-null service key using the settings returned by `settingsAction` through DI and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`; `settingsAction`: nullable provider settings factory. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed generated-only `T` with settings resolved from DI. |
| `IServiceCollection.AddKeyedRefitGeneratedClient<T>(object? serviceKey, Func<IServiceProvider, RefitSettings?>? settingsAction, string? httpClientName)` | Registers the generated implementation of `T` under the required non-null service key using the settings returned by `settingsAction` through DI and the supplied HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`; `settingsAction`; `httpClientName`. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed generated-only `T` with DI-provided settings under that HTTP client name. |
| `IHttpClientBuilder.AddRefitClient(Type refitInterfaceType)` | Registers the reflection request builder for `refitInterfaceType` using the default settings and the existing builder name, then returns the builder for further HTTP configuration. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder) receiver; `refitInterfaceType`; preserves the builder name. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): adds a reflection-capable client to the existing named builder with default settings. |
| `IHttpClientBuilder.AddRefitClient(Type refitInterfaceType, RefitSettings? settings)` | Registers the reflection request builder for `refitInterfaceType` using the supplied fixed settings reference and the existing builder name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; `settings`: nullable fixed settings. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): adds a reflection-capable client to the existing builder using those settings. |
| `IHttpClientBuilder.AddRefitClient(Type refitInterfaceType, Func<IServiceProvider, RefitSettings?>? settingsAction)` | Registers the reflection request builder for `refitInterfaceType` using the settings returned by `settingsAction` through DI and the existing builder name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; `settingsAction`: nullable provider settings factory. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): adds a reflection-capable client to the existing builder with DI-provided settings. |
| `IHttpClientBuilder.AddRefitClient<T>()` | Registers the reflection request builder for `T` using the default settings and the existing builder name, then returns the builder for further HTTP configuration. | None; `T : class` is the Refit interface. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): adds reflection-capable `T` to the existing named builder with default settings. |
| `IHttpClientBuilder.AddRefitClient<T>(RefitSettings? settings)` | Registers the reflection request builder for `T` using the supplied fixed settings reference and the existing builder name, then returns the builder for further HTTP configuration. | `T : class`; `settings`: nullable fixed settings. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): adds reflection-capable `T` to the existing builder using those settings. |
| `IHttpClientBuilder.AddRefitClient<T>(Func<IServiceProvider, RefitSettings?>? settingsAction)` | Registers the reflection request builder for `T` using the settings returned by `settingsAction` through DI and the existing builder name, then returns the builder for further HTTP configuration. | `T : class`; `settingsAction`: nullable provider settings factory. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): adds reflection-capable `T` to the existing builder with DI-provided settings. |
| `IHttpClientBuilder.AddKeyedRefitClient(Type refitInterfaceType, object? serviceKey)` | Registers the reflection request builder for `refitInterfaceType` under the required non-null service key using the default settings and the existing builder name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; `serviceKey`: non-null DI key. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): adds a keyed reflection-capable client to the existing named builder with default settings. |
| `IHttpClientBuilder.AddKeyedRefitClient(Type refitInterfaceType, object? serviceKey, RefitSettings? settings)` | Registers the reflection request builder for `refitInterfaceType` under the required non-null service key using the supplied fixed settings reference and the existing builder name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; `serviceKey`; `settings`: nullable fixed settings. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): adds a keyed reflection-capable client to the existing builder using those settings. |
| `IHttpClientBuilder.AddKeyedRefitClient(Type refitInterfaceType, object? serviceKey, Func<IServiceProvider, RefitSettings?>? settingsAction)` | Registers the reflection request builder for `refitInterfaceType` under the required non-null service key using the settings returned by `settingsAction` through DI and the existing builder name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; `serviceKey`; `settingsAction`: nullable provider settings factory. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): adds a keyed reflection-capable client to the existing builder with DI-provided settings. |
| `IHttpClientBuilder.AddKeyedRefitClient<T>(object? serviceKey)` | Registers the reflection request builder for `T` under the required non-null service key using the default settings and the existing builder name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`: non-null DI key. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): adds keyed reflection-capable `T` to the existing named builder with default settings. |
| `IHttpClientBuilder.AddKeyedRefitClient<T>(object? serviceKey, RefitSettings? settings)` | Registers the reflection request builder for `T` under the required non-null service key using the supplied fixed settings reference and the existing builder name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`; `settings`: nullable fixed settings. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): adds keyed reflection-capable `T` to the existing builder using those settings. |
| `IHttpClientBuilder.AddKeyedRefitClient<T>(object? serviceKey, Func<IServiceProvider, RefitSettings?>? settingsAction)` | Registers the reflection request builder for `T` under the required non-null service key using the settings returned by `settingsAction` through DI and the existing builder name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`; `settingsAction`: nullable provider settings factory. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): adds keyed reflection-capable `T` to the existing builder with DI-provided settings. |
| `IHttpClientBuilder.AddAuthorizationHeaderValueProvider(Func<IServiceProvider, HttpRequestMessage, CancellationToken, ValueTask<string>> getToken)` | Adds a handler to this builder that creates a fresh DI scope for each request and calls `getToken` with that scope, request and cancellation token. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder) receiver; [`Func<IServiceProvider, HttpRequestMessage, CancellationToken, ValueTask<string>>`](https://learn.microsoft.com/dotnet/api/system.func-4) `getToken`: per-request token callback. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): attaches a handler that resolves the authorization token in a fresh DI scope for each request. |
| `SettingsFor<T>(RefitSettings? settings)` | Constructs a `SettingsFor<T>` holder that keeps the supplied nullable settings reference for interface type `T`. | [`RefitSettings?`](clients/settings.md) `settings`: settings reference or `null`; `T` identifies the interface. | New [`SettingsFor<T>`](clients/dependency-injection.md) holder that stores that settings reference for one registered interface. |
| `SettingsFor<T>.Settings` | Exposes the nullable `RefitSettings` reference associated with the registered Refit interface. | None. | [`RefitSettings`](clients/settings.md)`?`: the settings reference stored for `T`; it can be null to select defaults. |
| `ISettingsFor.Settings` | Exposes the nullable `RefitSettings` reference from a `SettingsFor<T>` instance without exposing its interface type. | None. | [`RefitSettings`](clients/settings.md)`?`: the settings reference stored by that holder. |

| Type | Purpose |
| --- | --- |
| `Refit.HttpClientFactoryExtensions` | Static extension class for service-collection and existing-builder registration methods. |
| `Refit.ISettingsFor` | Interface that exposes a nullable settings reference without a closed Refit interface type. |
| `Refit.SettingsFor<T>` | Generic DI holder that associates a nullable settings reference with interface type `T`. |

### Settings

[Full description and examples](clients/settings.md).

Types: `Refit.RefitSettings`.

#### Settings reference

[Full description and examples](clients/settings.md).

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| [`RefitSettings`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Holds the serializer, URL/form formatters, request-building options, exception factories, and HTTP-version settings used by a Refit client. | None. | Mutable settings object. |
| [`RefitSettings()`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates a complete settings object with Refit's default serializer, formatters, and exception factories. | None. | New settings with the System.Text.Json serializer, default URL, form, and key formatters, plus default exception factories. |
| [`RefitSettings(IHttpContentSerializer contentSerializer)`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates settings that use the supplied content serializer and the other defaults. | [`IHttpContentSerializer`](serialization/json.md) `contentSerializer`: serializer; must not be `null`. | New settings using the supplied serializer and default URL, form, and key formatters. |
| [`RefitSettings(IHttpContentSerializer contentSerializer, IUrlParameterFormatter? urlParameterFormatter)`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates settings with a supplied serializer and URL-value formatter. | [`IHttpContentSerializer`](serialization/json.md) `contentSerializer`: required serializer; [`IUrlParameterFormatter`](requests/query-formatters.md) `urlParameterFormatter`: formatter or `null` for the default. | New settings using the supplied choices and the default form and key formatters. |
| [`RefitSettings(IHttpContentSerializer contentSerializer, IUrlParameterFormatter? urlParameterFormatter, IFormUrlEncodedParameterFormatter? formUrlEncodedParameterFormatter)`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates settings with supplied serializer, URL-value, and form-value formatters. | [`IHttpContentSerializer`](serialization/json.md) `contentSerializer`: required serializer; [`IUrlParameterFormatter`](requests/query-formatters.md) `urlParameterFormatter`: formatter or `null`; [`IFormUrlEncodedParameterFormatter`](requests/query-formatters.md) `formUrlEncodedParameterFormatter`: formatter or `null`. | New settings using the supplied choices and the default key formatter. |
| [`RefitSettings(IHttpContentSerializer contentSerializer, IUrlParameterFormatter? urlParameterFormatter, IFormUrlEncodedParameterFormatter? formUrlEncodedParameterFormatter, IUrlParameterKeyFormatter? urlParameterKeyFormatter)`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates settings with supplied serializer and all formatter choices. | [`IHttpContentSerializer`](serialization/json.md) `contentSerializer`: required serializer; [`IUrlParameterFormatter`](requests/query-formatters.md) `urlParameterFormatter`: formatter or `null`; [`IFormUrlEncodedParameterFormatter`](requests/query-formatters.md) `formUrlEncodedParameterFormatter`: formatter or `null`; [`IUrlParameterKeyFormatter`](requests/query-formatters.md) `urlParameterKeyFormatter`: formatter or `null`. | New settings; a `null` formatter selects its default. |
| [`RefitSettings.CamelCase()`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates settings that serialize JSON and format URL/form keys in camelCase. | None. | New [`RefitSettings`](clients/settings.md) using camelCase JSON and URL/form keys. |
| [`RefitSettings.SnakeCase()`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates settings that serialize JSON and format URL/form keys in snake_case. | None. | New [`RefitSettings`](clients/settings.md) using snake_case JSON and URL/form keys. |
| [`RefitSettings.KebabCase()`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates settings that serialize JSON and format URL/form keys in kebab-case. | None. | New [`RefitSettings`](clients/settings.md) using kebab-case JSON and URL/form keys. |
| [`AuthorizationHeaderValueGetter`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Supplies a token for a declared `[Authorize]` header that has no token. Generated preparation uses it even with a supplied `HttpClient`; a settings-created handler also uses it for an explicit token. | [`Func<HttpRequestMessage, CancellationToken, ValueTask<string>>`](https://learn.microsoft.com/dotnet/api/system.func-3) or `null`. | Token getter; default `null`. An empty returned token removes the header. |
| [`HttpMessageHandlerFactory`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Supplies the primary handler when Refit creates the `HttpClient`. | [`Func<HttpMessageHandler>`](https://learn.microsoft.com/dotnet/api/system.func-1) or `null`. | Handler factory; default `null`. Refit ignores it when you supply an existing `HttpClient`. |
| [`ExceptionFactory`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Maps unsuccessful HTTP responses to exceptions. | [`Func<HttpResponseMessage, ValueTask<Exception?>>`](https://learn.microsoft.com/dotnet/api/system.func-2). | Exception factory; default creates Refit API exceptions. A `null` result suppresses the HTTP error. |
| [`DeserializationExceptionFactory`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Maps response-body deserialization failures to exceptions. | [`Func<HttpResponseMessage, Exception, ValueTask<Exception?>>`](https://learn.microsoft.com/dotnet/api/system.func-3) or `null`. | Deserialization exception factory; default `null`. A `null` result suppresses the error. |
| [`ContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Serializes request bodies and deserializes response bodies. | [`IHttpContentSerializer`](serialization/json.md). | Body/reply serializer; default [`SystemTextJsonContentSerializer`](serialization/json.md). |
| [`ReturnTypeAdapters`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Registers custom return wrappers for the opt-in reflection request builder, such as `IObservable<T>`. | Read-only [`IList<Type>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1) property. | Mutable adapter list; default empty. Reflection builds consult it; source-generated builds discover adapters at compile time. |
| [`UrlParameterKeyFormatter`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Formats parameter names used in route, query, and form data. | [`IUrlParameterKeyFormatter`](requests/query-formatters.md). | URL/form key formatter; default [`DefaultUrlParameterKeyFormatter`](requests/query-formatters.md). |
| [`HonorContentSerializerPropertyNamesInQuery`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Chooses whether flattened query names follow serializer property names. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | `true` makes flattened query keys honor serializer names; default `true`. `AliasAs` wins in either mode. |
| [`UrlParameterFormatter`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Formats parameter values inserted into URLs. | [`IUrlParameterFormatter`](requests/query-formatters.md). | Path/query value formatter; default [`DefaultUrlParameterFormatter`](requests/query-formatters.md). |
| [`UrlParameterFormatterMap`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Selects URL value formatters by exact runtime type before the general formatter. | Read-only [`IDictionary<Type, IUrlParameterFormatter>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2) property. | Mutable formatter map; default empty. Base classes and interfaces are not searched. |
| [`FormUrlEncodedParameterFormatter`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Formats values written into form-url-encoded request bodies. | [`IFormUrlEncodedParameterFormatter`](requests/query-formatters.md). | Form value formatter; default [`DefaultFormUrlEncodedParameterFormatter`](requests/query-formatters.md). |
| [`CollectionFormat`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Selects how collection values become repeated or joined URL parameters. | [`CollectionFormat`](requests/queries.md). | Collection rendering mode; default `RefitParameterFormatter`. |
| [`Buffered`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Chooses whether request content is buffered before the HTTP send. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | Buffer request content before sending; default `false`. |
| [`CaptureRequestContent`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Captures request-body text so an [`ApiExceptionBase`](results/responses.md) can expose it after a failed request. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | Retain request-body text in memory; default `false`. Avoid it for large or streamed uploads. |
| [`CaptureMethodArguments`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Stores boxed interface-call arguments in the request options for a handler to inspect. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | Retain an `object?[]` for the request lifetime; default `false`. |
| [`MaxExceptionContentLength`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Limits the response-body characters captured while building an API exception. | [`int?`](https://learn.microsoft.com/dotnet/api/system.nullable-1) characters. | Error-body capture limit; default `null` (unbounded). |
| [`ExceptionRedactor`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Scrubs sensitive data from an [`ApiExceptionBase`](results/responses.md) before Refit returns it. | [`Action<ApiExceptionBase>`](https://learn.microsoft.com/dotnet/api/system.action-1) or `null`. | Exception scrubbing hook; default `null`. |
| [`AllowUnmatchedRouteParameters`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Allows route placeholders without matching method parameters. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | Leaves unmatched `{token}` text for later rewriting when `true`; default `false`. |
| [`ValidateHeaders`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Enables framework validation when Refit applies declared headers. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | Use framework header parsing; default `false`. Invalid values throw `FormatException` when a request is built. |
| [`UrlResolution`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Selects how relative request paths resolve against `HttpClient.BaseAddress`. | [`UrlResolutionMode`](advanced/request-helpers.md). | Base-address resolution mode; default `RefitLegacy`. |
| [`RequestBodySerialization`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Selects how Refit creates JSON request-body content. | [`RequestBodySerializationMode`](requests/bodies.md). | JSON body serialization mode; default `Default`. `Buffered` and `Streamed` require [`ISynchronousContentSerializer`](serialization/json.md). |
| [`RequestCompression`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Selects the content encoding applied to every request body. | [`RequestCompression`](requests/bodies.md). | Request-body coding; default `None`. A `[Body]` coding overrides this setting. |
| [`RequestCompressionLevel`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Sets the compression effort for compressed request bodies. | [`CompressionLevel`](https://learn.microsoft.com/dotnet/api/system.io.compression.compressionlevel). | Compression effort; default `Optimal`. |
| [`RequestCompressionOptions`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Provides per-coding compressor settings that override the compression level for that coding. | [`RequestCompressionOptions`](requests/bodies.md) or `null` (.NET 9+). | Per-coding compressor settings; default `null`, which uses the compression level. |
| [`HttpRequestMessageOptions`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Copies these local values to every generated request's options on modern .NET, or properties on .NET Framework. | [`Dictionary<string, object>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.dictionary-2) or `null`; `init` only. | Local request values; default `null`. The dictionary remains mutable after initialization. |
| [`TransportExceptionFactory`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Maps exceptions thrown by `HttpClient.SendAsync` to the exception Refit surfaces. | [`Func<HttpRequestMessage, Exception, CancellationToken, Exception>`](https://learn.microsoft.com/dotnet/api/system.func-4). | Default preserves an `OperationCanceledException` when its token was cancelled; otherwise it wraps the failure in [`ApiRequestException`](results/responses.md). |
| [`Version`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Sets the HTTP version requested on generated requests. | [`Version`](https://learn.microsoft.com/dotnet/api/system.version) (.NET 6+). | Requested HTTP version; default HTTP/1.1. |
| [`VersionPolicy`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Sets the policy used when negotiating the requested HTTP version. | [`HttpVersionPolicy`](https://learn.microsoft.com/dotnet/api/system.net.http.httpversionpolicy) (.NET 6+). | Version negotiation policy; default `RequestVersionOrLower`. |

| `Enum` | Value | Meaning |
| --- | --- | --- |
| [`CollectionFormat`](https://github.com/reactiveui/refit/blob/main/src/Refit/CollectionFormat.cs) | `RefitParameterFormatter` (0) | Use the configured value formatter. |
| `CollectionFormat` | `Csv` (1), `Ssv` (2), `Tsv` (3), `Pipes` (4) | Comma, space, tab, or pipe separated values. |
| `CollectionFormat` | `Multi` (5) | Repeat the parameter for each value. |
| `CollectionFormat` | `Indexed` (6) | Expand object elements with indexed keys. |
| [`RequestBodySerializationMode`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBodySerializationMode.cs) | `Default` (0) | Normal asynchronous serialization. |
| `RequestBodySerializationMode` | `Buffered` (1) | Synchronous serialization into buffered content. |
| `RequestBodySerializationMode` | `Streamed` (2) | Synchronous serialization to the request stream. |
| [`RequestCompression`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompression.cs) | `Default` (0), `None` (1), `GZip` (2), `Brotli` (3), `Zstandard` (4) | Use settings, no coding, gzip, Brotli, or Zstandard. Brotli requires .NET 8; Zstandard requires .NET 11. |
| [`CompressionLevel`](https://learn.microsoft.com/dotnet/api/system.io.compression.compressionlevel) | `Optimal` (0), `Fastest` (1), `NoCompression` (2), `SmallestSize` (3) | Compression effort choices used by `RequestCompressionLevel`. |
| [`UrlResolutionMode`](https://github.com/reactiveui/refit/blob/main/src/Refit/UrlResolutionMode.cs) | `RefitLegacy` (0), `Rfc3986` (1) | Legacy base-path prepending or RFC 3986 URI resolution. |
| [`System.Net.Http.HttpVersionPolicy`](https://learn.microsoft.com/dotnet/api/system.net.http.httpversionpolicy) | `RequestVersionOrLower` (0), `RequestVersionOrHigher` (1), `RequestVersionExact` (2) | HTTP version negotiation choices. |

### Request builders

[Full description and examples](clients/request-builders.md).

Types: `Refit.IRequestBuilder`, `Refit.IRequestBuilder<T>`, `Refit.RequestBuilder`.

#### Close a generic method

[Full description and examples](clients/request-builders.md).

| Declaration | Description | Parameters and defaults | Return/value |
| --- | --- | --- | --- |
| `RequestBuilder.ForType<T>(RefitSettings? settings)` | Resolves the optional reflection factory and creates a strongly typed builder for `T`; `null` settings are passed through to the factory. | [`RefitSettings`](clients/settings.md) `settings`: settings for request construction, or `null`. `T` is the Refit API interface. | [`IRequestBuilder<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder%7BT%7D.cs) for `T`. |
| `RequestBuilder.ForType<T>()` | Resolves the optional reflection factory and creates a strongly typed builder for `T` with `null` settings. | `T` is the Refit API interface. | [`IRequestBuilder<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder%7BT%7D.cs) for `T`. |
| `RequestBuilder.ForType(Type refitInterfaceType, RefitSettings? settings)` | Resolves the optional reflection factory and creates a builder for the supplied Refit interface type. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: Refit interface, including a closed generic interface. [`RefitSettings`](clients/settings.md) `settings`: settings for request construction, or `null`. | [`IRequestBuilder`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder.cs) for `refitInterfaceType`. |
| `RequestBuilder.ForType(Type refitInterfaceType)` | Calls the settings overload with `null` and creates a builder for the supplied Refit interface type. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: Refit interface, including a closed generic interface. | [`IRequestBuilder`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder.cs) for `refitInterfaceType`. |
| `IRequestBuilder.Settings` | Exposes the [`RefitSettings`](clients/settings.md) instance used by this builder. | None. | [`RefitSettings`](clients/settings.md) used by the builder. |
| `IRequestBuilder.BuildRestResultFuncForMethod(string methodName, Type[]? parameterTypes = null, Type[]? genericArgumentTypes = null)` | Resolves and caches a delegate for a reflected interface method. The delegate builds the request and follows the method's declared return shape when invoked. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `methodName`: interface method name. [`Type[]`](https://learn.microsoft.com/dotnet/api/system.type) `parameterTypes`: declaration-order parameter types, default `null`; required to select among overloads. [`Type[]`](https://learn.microsoft.com/dotnet/api/system.type) `genericArgumentTypes`: types used to close a generic method, default `null`. | [`Func<HttpClient, object[], object?>`](https://learn.microsoft.com/dotnet/api/system.func-3), taking an [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) and argument array and returning the method's declared result. |
| `RestService.RegisterGeneratedFactory(Type refitInterfaceType, Func<HttpClient, IRequestBuilder, object> factory)` | Stores a source-generated factory under an interface [`Type`](https://learn.microsoft.com/dotnet/api/system.type); a later registration for the same type replaces it. | [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: interface key. [`Func<HttpClient, IRequestBuilder, object>`](https://learn.microsoft.com/dotnet/api/system.func-3) `factory`: receives the client and a generated-only [`IRequestBuilder`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder.cs). | `void`; stores the factory. Null type or factory throws `ArgumentNullException`. |
| `RestService.RegisterGeneratedFactory<T>(Func<HttpClient, IRequestBuilder, T> factory)` | Stores a typed source-generated factory under `typeof(T)`. | [`Func<HttpClient, IRequestBuilder, T>`](https://learn.microsoft.com/dotnet/api/system.func-3) `factory`: receives the client and generated-only builder and returns `T`. `T` is the Refit interface. | `void`; stores the typed factory. A null factory throws `ArgumentNullException`. |
| `RestService.RegisterGeneratedSettingsFactory<T>(Func<HttpClient, RefitSettings, T> factory)` | Stores a typed source-generated factory that receives settings directly, so generated clients can build requests inline without reflection. | [`Func<HttpClient, RefitSettings, T>`](https://learn.microsoft.com/dotnet/api/system.func-3) `factory`: receives the client and settings and returns `T`. `T` is the Refit interface. | `void`; stores the settings factory. A null factory throws `ArgumentNullException`. |
| `IRequestBuilder` | Defines the settings property and dynamic method-delegate operation used by request builders. | None. | Interface implemented by reflection and generated-only builders. |
| `IRequestBuilder<T>` | Carries the target API interface type `T` while inheriting the untyped builder contract. | `T` is the Refit API interface. | [`IRequestBuilder`](https://github.com/reactiveui/refit/blob/main/src/Refit/IRequestBuilder.cs). |
| `RequestBuilder` | Provides static entry points that resolve the optional reflection request-builder factory. | None. | Static class. |


## Requests

### Routes and HTTP methods

[Full description and examples](requests/routes.md).

Types: `Refit.DeleteAttribute`, `Refit.GetAttribute`, `Refit.HeadAttribute`, `Refit.HttpMethodAttribute`, `Refit.OptionsAttribute`, `Refit.PatchAttribute`, `Refit.PathPrefixAttribute`, `Refit.PostAttribute`, `Refit.PutAttribute`, `Refit.UrlAttribute`.

#### Pick an HTTP method

[Full description and examples](requests/routes.md).

| Attribute | HTTP method | Common use |
| --- | --- | --- |
| `[Get(path)]` | GET | Read a resource. |
| `[Post(path)]` | POST | Submit data or create a resource. |
| `[Put(path)]` | PUT | Replace a resource. |
| `[Patch(path)]` | PATCH | Change part of a resource. |
| `[Delete(path)]` | DELETE | Remove a resource. |
| `[Head(path)]` | HEAD | Read reply headers without a reply body. |
| `[Options(path)]` | OPTIONS | Ask what operations a service accepts. |

#### Route attribute reference

[Full description and examples](requests/routes.md).

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| [`HttpMethodAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/HttpMethodAttribute.cs) | Base attribute for declaring the HTTP method and route template used by a Refit interface method. | None. Abstract class. | Attribute type inherited by Refit's built-in HTTP method attributes. |
| [`HttpMethodAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/HttpMethodAttribute.cs) | Stores the route template for an HTTP operation. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes the base attribute with the supplied path. |
| [`HttpMethodAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/HttpMethodAttribute.cs) | Identifies the HTTP verb represented by the attribute. | None. Abstract getter. | [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) for the operation. |
| [`HttpMethodAttribute.Path`](https://github.com/reactiveui/refit/blob/main/src/Refit/HttpMethodAttribute.cs) | Holds the route template Refit combines with method parameters. | None publicly; protected setter for derived attributes. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) route template supplied to the constructor or changed by a subclass. |
| [`DeleteAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/DeleteAttribute.cs) | Attribute that declares a DELETE request on an interface method. | None. Sealed attribute for methods. | Attribute type inherited from `HttpMethodAttribute`. |
| [`DeleteAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/DeleteAttribute.cs) | Declares a DELETE route with the supplied template. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes a DELETE route attribute. |
| [`DeleteAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/DeleteAttribute.cs) | Supplies the HTTP method for a DELETE route. | None. | [`HttpMethod.Delete`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod.delete). |
| [`GetAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/GetAttribute.cs) | Attribute that declares a GET request on an interface method. | None. Sealed attribute for methods. | Attribute type inherited from `HttpMethodAttribute`. |
| [`GetAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/GetAttribute.cs) | Declares a GET route with the supplied template. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes a GET route attribute. |
| [`GetAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/GetAttribute.cs) | Supplies the HTTP method for a GET route. | None. | [`HttpMethod.Get`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod.get). |
| [`HeadAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeadAttribute.cs) | Attribute that declares a HEAD request on an interface method. | None. Sealed attribute for methods. | Attribute type inherited from `HttpMethodAttribute`. |
| [`HeadAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeadAttribute.cs) | Declares a HEAD route with the supplied template. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes a HEAD route attribute. |
| [`HeadAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeadAttribute.cs) | Supplies the HTTP method for a HEAD route. | None. | [`HttpMethod.Head`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod.head). |
| [`OptionsAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/OptionsAttribute.cs) | Attribute that declares an OPTIONS request on an interface method. | None. Sealed attribute for methods. | Attribute type inherited from `HttpMethodAttribute`. |
| [`OptionsAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/OptionsAttribute.cs) | Declares an OPTIONS route with the supplied template. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes an OPTIONS route attribute. |
| [`OptionsAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/OptionsAttribute.cs) | Supplies the HTTP method for an OPTIONS route. | None. | [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) whose method name is `OPTIONS`. |
| [`PatchAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/PatchAttribute.cs) | Attribute that declares a PATCH request on an interface method. | None. Sealed attribute for methods. | Attribute type inherited from `HttpMethodAttribute`. |
| [`PatchAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/PatchAttribute.cs) | Declares a PATCH route with the supplied template. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes a PATCH route attribute. |
| [`PatchAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/PatchAttribute.cs) | Supplies a custom [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) whose name is `PATCH`. | None. | HTTP method named `PATCH`. |
| [`PathPrefixAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/PathPrefixAttribute.cs) | Attribute that prepends a shared route prefix to methods on an interface. | None. Sealed attribute for interfaces. | Interface attribute carrying a shared route prefix. |
| [`PathPrefixAttribute(string prefix)`](https://github.com/reactiveui/refit/blob/main/src/Refit/PathPrefixAttribute.cs) | Stores the prefix Refit applies to the interface's method routes. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `prefix`: shared route prefix. | Initializes an interface route-prefix attribute. |
| [`PathPrefixAttribute.Prefix`](https://github.com/reactiveui/refit/blob/main/src/Refit/PathPrefixAttribute.cs) | Exposes the prefix supplied to the constructor. | None. Read-only. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) route prefix. |
| [`PostAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/PostAttribute.cs) | Attribute that declares a POST request on an interface method. | None. Sealed attribute for methods. | Attribute type inherited from `HttpMethodAttribute`. |
| [`PostAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/PostAttribute.cs) | Declares a POST route with the supplied template. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes a POST route attribute. |
| [`PostAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/PostAttribute.cs) | Supplies the HTTP method for a POST route. | None. | [`HttpMethod.Post`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod.post). |
| [`PutAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/PutAttribute.cs) | Attribute that declares a PUT request on an interface method. | None. Sealed attribute for methods. | Attribute type inherited from `HttpMethodAttribute`. |
| [`PutAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/PutAttribute.cs) | Declares a PUT route with the supplied template. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes a PUT route attribute. |
| [`PutAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/PutAttribute.cs) | Supplies the HTTP method for a PUT route. | None. | [`HttpMethod.Put`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod.put). |
| [`UrlAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/UrlAttribute.cs) | Attribute that marks a parameter as the complete absolute request URL. | None. Sealed attribute for parameters. | Parameter marker consumed while Refit builds the request. |
| [`UrlAttribute()`](https://github.com/reactiveui/refit/blob/main/src/Refit/UrlAttribute.cs) | Marks a method parameter as the absolute URL used for the request. | None. | Initializes a URL parameter marker. |

### Query names, values and collections

[Full description and examples](requests/queries.md).

Types: `Refit.AliasAsAttribute`, `Refit.CollectionFormat`, `Refit.EncodedAttribute`, `Refit.QueryAttribute`, `Refit.QueryNameAttribute`, `Refit.QueryUriFormatAttribute`.

#### Send a collection

[Full description and examples](requests/queries.md).

| `CollectionFormat` | Shape for two string values |
| --- | --- |
| `Csv` | `tags=math%2Ccode` |
| `Ssv` | `tags=math%20code` |
| `Tsv` | `tags=math%09code` |
| `Pipes` | `tags=math%7Ccode` |
| `Multi` | `tags=math&tags=code` |
| `Indexed` | Object properties such as `people[0].Id=1&people[1].Id=2`. |
| `RefitParameterFormatter` | Uses the configured formatter. The default query formatter joins values with commas. |

#### Query attribute choices

[Full description and examples](requests/queries.md).

| Attribute or property | Use |
| --- | --- |
| `AliasAs(name)` / `Name` | Sets an explicit parameter or property name. |
| `Query()` | Keeps the default delimiter and the configured collection format. |
| `Query(delimiter)` / `Delimiter` | Chooses the text between nested names. The default is `.`. |
| `Query(delimiter, prefix)` / `Prefix` | Adds a name before flattened properties. |
| `Query(delimiter, prefix, format)` / `Format` | Also supplies a value format string. |
| `Query(collectionFormat)` / `CollectionFormat` | Selects a collection format for this argument. |
| `Query.IsCollectionFormatSpecified` | Tells custom code whether the attribute explicitly chose a collection format. |
| `Query.TreatAsString` | Uses the object's `ToString()` result instead of flattening its properties. |
| `Query.SerializeNull` | Sends a null property as an empty value. |
| `QueryName()` | Sends valueless flags. |
| `Encoded()` | Keeps caller-escaped text. |
| `QueryUriFormat(uriFormat)` / `UriFormat` | Sets the final path and query rendering mode. |

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| `CollectionFormat` | Selects how a collection becomes query or form text. | None. | [enum](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/enum) with the values listed above. |
| `CollectionFormat.RefitParameterFormatter` | Delegates collection rendering to the configured URL or form formatter. | None. | [int](https://learn.microsoft.com/dotnet/api/system.int32) value `0`; the default enum value. |
| `CollectionFormat.Csv` | Joins values with a comma. | None. | [int](https://learn.microsoft.com/dotnet/api/system.int32) value `1`. |
| `CollectionFormat.Ssv` | Joins values with a space. | None. | [int](https://learn.microsoft.com/dotnet/api/system.int32) value `2`. |
| `CollectionFormat.Tsv` | Joins values with a tab. | None. | [int](https://learn.microsoft.com/dotnet/api/system.int32) value `3`. |
| `CollectionFormat.Pipes` | Joins values with a pipe character. | None. | [int](https://learn.microsoft.com/dotnet/api/system.int32) value `4`. |
| `CollectionFormat.Multi` | Emits one key-value pair for each collection value. | None. | [int](https://learn.microsoft.com/dotnet/api/system.int32) value `5`. |
| `CollectionFormat.Indexed` | Expands each object element under an indexed key such as `items[0].Name`. | None. | [int](https://learn.microsoft.com/dotnet/api/system.int32) value `6`; scalar elements use comma-separated values. |
| `AliasAsAttribute` | An attribute that replaces a query parameter or property name with a service-specific name. | Applied to a parameter or property. | Sealed [Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) type. |
| `AliasAsAttribute(string name)` | Marks a parameter or property with the exact name Refit sends on the wire. | [string](https://learn.microsoft.com/dotnet/api/system.string) `name`: wire name. | An attribute whose [Name](https://learn.microsoft.com/dotnet/api/system.string) replaces the CLR name. |
| `AliasAsAttribute.Name` | Returns the alias supplied to the constructor. | None. Read-only. | [string](https://learn.microsoft.com/dotnet/api/system.string) wire name. |
| `EncodedAttribute` | An attribute that tells generated request building to preserve a caller-encoded parameter. | Applied to a parameter. | Sealed [Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) type. |
| `EncodedAttribute()` | Marks a parameter value as URL-encoded text that Refit appends verbatim. | None. | Attribute for path segments, query values, and `QueryName` flags. |
| `QueryAttribute` | An attribute that controls query or form field names, scalar formats, and collection formats. | Applied to a parameter or property. | Sealed [Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) type. |
| `QueryAttribute()` | Uses `.` as the nested-name delimiter and leaves the collection format to client settings. | None. | Attribute with no prefix or value format. |
| `QueryAttribute(CollectionFormat collectionFormat)` | Selects a collection format for this parameter or property. | [CollectionFormat](https://github.com/reactiveui/refit/blob/main/src/Refit/CollectionFormat.cs) `collectionFormat`: explicit collection mode. | Attribute for which `IsCollectionFormatSpecified` is `true`. |
| `QueryAttribute(string delimiter)` | Changes the separator between names when Refit flattens a complex value. | [string](https://learn.microsoft.com/dotnet/api/system.string) `delimiter`: nested-name separator. | Attribute with the supplied delimiter. |
| `QueryAttribute(string delimiter, string prefix)` | Changes flattened names to `prefix + delimiter + propertyName`. | [string](https://learn.microsoft.com/dotnet/api/system.string) `delimiter`: nested-name separator; [string](https://learn.microsoft.com/dotnet/api/system.string) `prefix`: name before flattened properties. | Attribute with the supplied delimiter and prefix. |
| `QueryAttribute(string delimiter, string prefix, string format)` | Also stores a value format for a scalar query value. It does not apply that format to flattened properties. | [string](https://learn.microsoft.com/dotnet/api/system.string) `delimiter`: nested-name separator; [string](https://learn.microsoft.com/dotnet/api/system.string) `prefix`: name before flattened properties; [string](https://learn.microsoft.com/dotnet/api/system.string) `format`: value format string. | Attribute with the supplied delimiter, prefix, and format. |
| `QueryAttribute.CollectionFormat` | Gets the selected format, or sets an explicit format that overrides client settings. | None. | [CollectionFormat](https://github.com/reactiveui/refit/blob/main/src/Refit/CollectionFormat.cs); reads as `RefitParameterFormatter` until set, while `IsCollectionFormatSpecified` distinguishes that unset state. |
| `QueryAttribute.Delimiter` | Returns the separator that joins the prefix and flattened property name. | None. Read-only. | [string](https://learn.microsoft.com/dotnet/api/system.string), default `"."`. |
| `QueryAttribute.Format` | Gets or sets the format string for a scalar query value. | None. | [string](https://learn.microsoft.com/dotnet/api/system.string) or `null`; default `null`. |
| `QueryAttribute.IsCollectionFormatSpecified` | Reports whether code assigned `CollectionFormat`, including through the collection-format constructor. | None. Read-only. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean), default `false`. |
| `QueryAttribute.Prefix` | Returns the name prepended to each flattened property. | None. Read-only. | [string](https://learn.microsoft.com/dotnet/api/system.string) or `null`; default `null`. |
| `QueryAttribute.SerializeNull` | Controls whether a null property is written as an empty value instead of omitted. | None. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean), default `false`. |
| `QueryAttribute.TreatAsString` | Controls whether Refit uses an object's `ToString()` result instead of flattening its properties. | None. | [bool](https://learn.microsoft.com/en-us/dotnet/api/system.boolean), default `false`. |
| `QueryNameAttribute` | An attribute that creates a presence-style query flag from a parameter value. | Applied to a parameter. | Sealed [Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) type. |
| `QueryNameAttribute()` | Marks a parameter whose formatted value becomes a bare query flag without `=value`. | None. | Attribute that omits null values and renders collection elements as separate flags. |
| `QueryUriFormatAttribute` | An attribute that controls how .NET renders a method's final request URI. | Applied to a method. | Sealed [Attribute](https://learn.microsoft.com/dotnet/api/system.attribute) type. |
| `QueryUriFormatAttribute(UriFormat uriFormat)` | Sets the .NET URI rendering mode for the method's complete path and query. | [UriFormat](https://learn.microsoft.com/dotnet/api/system.uriformat) `uriFormat`: final URI rendering mode. | Attribute applied to a method. |
| `QueryUriFormatAttribute.UriFormat` | Returns the URI rendering mode supplied to the constructor. | None. Read-only. | [UriFormat](https://learn.microsoft.com/dotnet/api/system.uriformat). |

### Shared query formatters

[Full description and examples](requests/query-formatters.md).

Types: `Refit.CamelCaseUrlParameterKeyFormatter`, `Refit.DefaultFormUrlEncodedParameterFormatter`, `Refit.DefaultUrlParameterFormatter`, `Refit.DefaultUrlParameterKeyFormatter`, `Refit.IFormUrlEncodedParameterFormatter`, `Refit.IUrlParameterFormatter`, `Refit.IUrlParameterKeyFormatter`, `Refit.KebabCaseUrlParameterKeyFormatter`, `Refit.SnakeCaseUrlParameterKeyFormatter`.

#### Choose a key naming rule

[Full description and examples](requests/query-formatters.md).

| Key formatter | `Format("PageSize")` | Settings shortcut |
| --- | --- | --- |
| `DefaultUrlParameterKeyFormatter` | `PageSize` | The default settings. |
| `CamelCaseUrlParameterKeyFormatter` | `pageSize` | `RefitSettings.CamelCase()` |
| `SnakeCaseUrlParameterKeyFormatter` | `page_size` | `RefitSettings.SnakeCase()` |
| `KebabCaseUrlParameterKeyFormatter` | `page-size` | `RefitSettings.KebabCase()` |

#### API reference

[Full description and examples](requests/query-formatters.md).

| API | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`IUrlParameterFormatter.Format(object? value, ICustomAttributeProvider attributeProvider, Type type)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IUrlParameterFormatter.cs) | Defines how an implementation converts a URL parameter value. | `value`: [`object`](https://learn.microsoft.com/en-us/dotnet/api/system.object); `attributeProvider`: [`ICustomAttributeProvider`](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.icustomattributeprovider); `type`: containing [`Type`](https://learn.microsoft.com/en-us/dotnet/api/system.type) | Returns [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), or `null` to omit the value. |
| [`IFormUrlEncodedParameterFormatter.Format(object? value, string? formatString)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IFormUrlEncodedParameterFormatter.cs) | Defines how an implementation converts a form-url-encoded field value. | `value`: [`object`](https://learn.microsoft.com/en-us/dotnet/api/system.object); `formatString`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) format, which may be `null` | Returns [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), or `null` to omit the field. |
| [`IUrlParameterKeyFormatter.Format(string key)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IUrlParameterKeyFormatter.cs) | Defines how an implementation converts a URL parameter name into its wire key. | `key`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key | Returns the formatted [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key. |
| [`DefaultUrlParameterKeyFormatter()`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultUrlParameterKeyFormatter.cs) | Creates the default key formatter. | None | Creates a formatter whose `Format` method returns each key unchanged. |
| [`DefaultUrlParameterKeyFormatter.Format(string key)`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultUrlParameterKeyFormatter.cs) | Applies the identity naming rule to a URL parameter key. | `key`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key | Returns the same key. |
| [`CamelCaseUrlParameterKeyFormatter()`](https://github.com/reactiveui/refit/blob/main/src/Refit/CamelCaseUrlParameterKeyFormatter.cs) | Creates a key formatter that converts leading uppercase letters to camelCase. | None | Creates a camelCase key formatter. |
| [`CamelCaseUrlParameterKeyFormatter.Format(string key)`](https://github.com/reactiveui/refit/blob/main/src/Refit/CamelCaseUrlParameterKeyFormatter.cs) | Converts the leading uppercase run of a key to camelCase and leaves keys that do not start with uppercase unchanged. | `key`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key | Returns the camelCase [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key. |
| [`SnakeCaseUrlParameterKeyFormatter()`](https://github.com/reactiveui/refit/blob/main/src/Refit/SnakeCaseUrlParameterKeyFormatter.cs) | Creates a key formatter that separates words with underscores. | None | Creates a snake_case key formatter. |
| [`SnakeCaseUrlParameterKeyFormatter.Format(string key)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SnakeCaseUrlParameterKeyFormatter.cs) | Converts a key to snake_case. | `key`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key | Returns the snake_case [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key. |
| [`KebabCaseUrlParameterKeyFormatter()`](https://github.com/reactiveui/refit/blob/main/src/Refit/KebabCaseUrlParameterKeyFormatter.cs) | Creates a key formatter that separates words with hyphens. | None | Creates a kebab-case key formatter. |
| [`KebabCaseUrlParameterKeyFormatter.Format(string key)`](https://github.com/reactiveui/refit/blob/main/src/Refit/KebabCaseUrlParameterKeyFormatter.cs) | Converts a key to kebab-case. | `key`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key | Returns the kebab-case [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key. |
| [`DefaultFormUrlEncodedParameterFormatter()`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultFormUrlEncodedParameterFormatter.cs) | Creates the default form-url-encoded value formatter. | None | Creates an invariant-culture formatter that uses `EnumMember` values when available. |
| [`DefaultFormUrlEncodedParameterFormatter.Format(object? value, string? formatString)`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultFormUrlEncodedParameterFormatter.cs) | Formats a form value with an optional format string. | `value`: [`object`](https://learn.microsoft.com/en-us/dotnet/api/system.object); `formatString`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) format, which may be `null` | Returns invariant-culture [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) text, uses an `EnumMember` value when available, and returns `null` for a `null` value. |
| [`DefaultUrlParameterFormatter()`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultUrlParameterFormatter.cs) | Creates the default URL value formatter. | None | Creates an invariant-culture formatter with no registered formats. |
| [`DefaultUrlParameterFormatter.AddFormat<TParameter>(string format)`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultUrlParameterFormatter.cs) | Registers a format for values whose runtime type is exactly `TParameter`. | `format`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) format; `TParameter`: value type | Returns [`void`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void); adding the same type twice throws [`ArgumentException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception). A non-blank query attribute format takes precedence. |
| [`DefaultUrlParameterFormatter.AddFormat<TContainer, TParameter>(string format)`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultUrlParameterFormatter.cs) | Registers a format for an exact `TParameter` value inside an exact `TContainer` type. | `format`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) format; `TContainer`: containing type; `TParameter`: value type | Returns [`void`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void); duplicate container/type registrations throw [`ArgumentException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception). A non-blank query attribute format takes precedence. |
| [`DefaultUrlParameterFormatter.Format(object? value, ICustomAttributeProvider attributeProvider, Type type)`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultUrlParameterFormatter.cs) | Formats a URL value using a query attribute format, a container-specific registration, or a general type registration. | `value`: [`object`](https://learn.microsoft.com/en-us/dotnet/api/system.object); `attributeProvider`: [`ICustomAttributeProvider`](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.icustomattributeprovider); `type`: containing [`Type`](https://learn.microsoft.com/en-us/dotnet/api/system.type) | Returns invariant-culture [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) text, uses an `EnumMember` value when available, and returns `null` for a `null` value. Throws [`ArgumentNullException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception) when `attributeProvider` is `null`. |

### Query converters

[Full description and examples](requests/query-converters.md).

Types: `Refit.IQueryConverter<T>`, `Refit.QueryConverterAttribute`, `Refit.SystemTextJsonQueryConverter<T>`.

#### API reference

[Full description and examples](requests/query-converters.md).

| API | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`IQueryConverter<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IQueryConverter.cs) | Defines a source-generated converter that writes one parameter's query pairs into a [`GeneratedQueryStringBuilder`](advanced/query-builder.md). | `T`: the declared parameter type handled by the converter. | Interface implemented by a custom query converter; generated request code caches one instance per converter type. |
| [`IQueryConverter<T>.Flatten(T value, string keyPrefix, ref GeneratedQueryStringBuilder builder, RefitSettings settings)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IQueryConverter.cs) | Writes the non-null query pairs for `value` into `builder`, prefixing each key with `keyPrefix`. | `value`: the declared query value; `keyPrefix`: the prefix from [`QueryAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/QueryAttribute.cs), or an empty string; `builder`: the mutable query builder; `settings`: the active [`RefitSettings`](clients/settings.md). No parameter has a default. | [`void`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void); appends pairs in place. The converter is used by generated requests and is not used by the reflection request builder. |
| [`QueryConverterAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/QueryConverterAttribute.cs) | Marks a query parameter for flattening by a specified [`IQueryConverter<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IQueryConverter.cs) implementation. | None. Apply it to a method parameter. | Attribute consumed by source-generated request code; the converter type must have a public parameterless constructor and match the parameter's declared type. |
| [`QueryConverterAttribute(Type converterType)`](https://github.com/reactiveui/refit/blob/main/src/Refit/QueryConverterAttribute.cs) | Selects the converter type that generated request code instantiates for the annotated parameter. | `converterType`: the [`Type`](https://learn.microsoft.com/en-us/dotnet/api/system.type) implementing [`IQueryConverter<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IQueryConverter.cs). No default. | [`void`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void); stores `converterType` in [`ConverterType`](https://github.com/reactiveui/refit/blob/main/src/Refit/QueryConverterAttribute.cs). |
| [`QueryConverterAttribute.ConverterType`](https://github.com/reactiveui/refit/blob/main/src/Refit/QueryConverterAttribute.cs) | Identifies the converter implementation selected for the annotated parameter. | None; read-only [`Type`](https://learn.microsoft.com/en-us/dotnet/api/system.type) property. | [`Type`](https://learn.microsoft.com/en-us/dotnet/api/system.type); returns the exact type passed to the constructor. |
| [`SystemTextJsonQueryConverter<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonQueryConverter.cs) | Provides a JSON-metadata-based [`IQueryConverter<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IQueryConverter.cs) for nested, polymorphic, and otherwise runtime-shaped query values. | `T`: the declared parameter type. | Converter type; reads property names and getters from [`SystemTextJsonContentSerializer`](serialization/json.md) metadata. |
| [`SystemTextJsonQueryConverter<T>()`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonQueryConverter.cs) | Creates a JSON metadata query converter for the declared type `T`. | None. | Creates [`SystemTextJsonQueryConverter<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonQueryConverter.cs); it does not capture a value or serializer. |
| [`SystemTextJsonQueryConverter<T>.Flatten(T value, string keyPrefix, ref GeneratedQueryStringBuilder builder, RefitSettings settings)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonQueryConverter.cs) | Walks the runtime value's JSON metadata and appends scalar, nested-object, and collection values to `builder`. | `value`: the root query value; `keyPrefix`: the prefix for its JSON property names; `builder`: the mutable query builder; `settings`: the active settings, including [`CollectionFormat`](clients/settings.md) and [`UrlParameterFormatter`](clients/settings.md). No parameter has a default. | [`void`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void); omits null properties, uses dotted keys for nested objects, formats collection elements according to settings, and stops nested traversal at depth 32. Throws [`NotSupportedException`](https://learn.microsoft.com/en-us/dotnet/api/system.notsupportedexception) unless `settings.ContentSerializer` is a [`SystemTextJsonContentSerializer`](serialization/json.md). |

### Headers and authorization

[Full description and examples](requests/headers.md).

Types: `Refit.AuthorizeAttribute`, `Refit.HeaderAttribute`, `Refit.HeaderCollectionAttribute`, `Refit.HeadersAttribute`.

#### Header order and validation

[Full description and examples](requests/headers.md).

| Attribute or property | Use |
| --- | --- |
| [`Headers(params string[] headers)`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeadersAttribute.cs) / [`Headers`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeadersAttribute.cs) | Shared interface or method headers. |
| [`Header(string header)`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeaderAttribute.cs) / [`Header`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeaderAttribute.cs) | One header value from a method argument. |
| [`HeaderCollection()`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeaderCollectionAttribute.cs) | A header dictionary from a method argument. |
| [`Authorize(string scheme = "Bearer")`](https://github.com/reactiveui/refit/blob/main/src/Refit/AuthorizeAttribute.cs) / [`Scheme`](https://github.com/reactiveui/refit/blob/main/src/Refit/AuthorizeAttribute.cs) | An authorization token from a method argument. |
| `RefitSettings.AuthorizationHeaderValueGetter` | Obtains a missing token before a declared authorized request is sent. |
| `RefitSettings.ValidateHeaders` | Chooses whether .NET validates header values. |

#### Header attribute reference

[Full description and examples](requests/headers.md).

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| [`AuthorizeAttribute(string scheme = "Bearer")`](https://github.com/reactiveui/refit/blob/main/src/Refit/AuthorizeAttribute.cs) | Declares that a method parameter supplies the token for an authorization header. | [string](https://learn.microsoft.com/dotnet/api/system.string) `scheme`: authorization scheme; default `"Bearer"`. | Creates an attribute that applies the scheme to a token parameter. |
| [`AuthorizeAttribute.Scheme`](https://github.com/reactiveui/refit/blob/main/src/Refit/AuthorizeAttribute.cs) | Gets the authorization scheme that Refit places before the token, such as `Bearer` or `Basic`. | None. Read-only. | [string](https://learn.microsoft.com/dotnet/api/system.string) scheme supplied to the constructor. |
| [`HeaderAttribute(string header)`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeaderAttribute.cs) | Maps one method argument to a named request header. | [string](https://learn.microsoft.com/dotnet/api/system.string) `header`: header declaration. | Creates an attribute that maps one method argument to the named request header. |
| [`HeaderAttribute.Header`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeaderAttribute.cs) | Gets the HTTP header name that receives the method argument value. | None. Read-only. | [string](https://learn.microsoft.com/dotnet/api/system.string) header declaration. |
| [`HeaderCollectionAttribute()`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeaderCollectionAttribute.cs) | Marks an argument whose dictionary supplies multiple request headers. | None. | Marker attribute for a header dictionary parameter. |
| [`HeadersAttribute(params string[] headers)`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeadersAttribute.cs) | Declares fixed headers that Refit adds to an interface or method request. | `params` [string[]](https://learn.microsoft.com/dotnet/api/system.string) `headers`: declarations; null becomes an empty array. | Creates shared interface or method headers from the supplied declarations. |
| [`HeadersAttribute.Headers`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeadersAttribute.cs) | Gets the header declarations Refit applies to interface or method requests. | None. Read-only. | [string[]](https://learn.microsoft.com/dotnet/api/system.string) declarations supplied to the constructor. |

### Local request context

[Full description and examples](requests/request-context.md).

Types: `Refit.HttpRequestMessageOptions`, `Refit.PropertyAttribute`.

#### Refit's option keys

[Full description and examples](requests/request-context.md).

| Key property | Value and use |
| --- | --- |
| `InterfaceType` | The top-level interface type for the call. |
| `MethodName` | The declared method name, such as `BuildAsync`. |
| `RelativePathTemplate` | The unfilled route, such as `/people/{id}`. Use this stable name for request metrics. |
| `RestMethodInfo` | Reflected method details when the request-building path supplies them. Generated requests avoid this reflection. |
| `MethodArguments` | The argument array when `CaptureMethodArguments` is true. |
| `RequestContent` | The captured body text when `CaptureRequestContent` is true. |

#### Request context reference

[Full description and examples](requests/request-context.md).

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| [`PropertyAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/PropertyAttribute.cs) | Marks an interface property or method parameter whose value Refit copies to the request's local options or properties. | None. | Attribute type. |
| `PropertyAttribute()` | Uses the marked property or parameter name as the request option key. | None. | A `PropertyAttribute` instance. The request value is stored under the inferred name. |
| `PropertyAttribute(string key)` | Uses an explicit request option key instead of the marked property or parameter name. | [string](https://learn.microsoft.com/dotnet/api/system.string) `key`: key stored in `Key`. | A `PropertyAttribute` instance. |
| `PropertyAttribute.Key` | Gets the explicit key selected for the marked property or parameter. | None. Read-only. | Nullable [string](https://learn.microsoft.com/dotnet/api/system.string): the supplied key, or `null` when Refit infers the name. |
| [`HttpRequestMessageOptions`](https://github.com/reactiveui/refit/blob/main/src/Refit/HttpRequestMessageOptions.cs) | Provides the string keys that Refit uses for built-in request metadata and optional captured values. | None. Static class. | Static class. Its members return keys for [`HttpRequestMessage.Options`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage.options) or the older `Properties` dictionary. |
| `HttpRequestMessageOptions.InterfaceType` | Identifies the option that stores the top-level Refit interface type used for the request. | None. Static read-only property. | [string](https://learn.microsoft.com/dotnet/api/system.string) `"Refit.InterfaceType"`. The value stored under this key is a [`Type`](https://learn.microsoft.com/dotnet/api/system.type). |
| `HttpRequestMessageOptions.RestMethodInfo` | Identifies the option that stores reflected method details when the reflection request builder supplies them. | None. Static read-only property. | [string](https://learn.microsoft.com/dotnet/api/system.string) `"Refit.RestMethodInfo"`. |
| `HttpRequestMessageOptions.MethodName` | Identifies the option that stores the declared Refit interface method name. | None. Static read-only property. | [string](https://learn.microsoft.com/dotnet/api/system.string) `"Refit.MethodName"`. |
| `HttpRequestMessageOptions.RelativePathTemplate` | Identifies the option that stores the unfilled route template for logging, metrics, and tracing. | None. Static read-only property. | [string](https://learn.microsoft.com/dotnet/api/system.string) `"Refit.RelativePathTemplate"`. |
| `HttpRequestMessageOptions.RequestContent` | Identifies the option that stores a captured request body string when `CaptureRequestContent` is enabled. | None. Static read-only property. | [string](https://learn.microsoft.com/dotnet/api/system.string) `"Refit.RequestContent"`. |
| `HttpRequestMessageOptions.MethodArguments` | Identifies the option that stores declared method arguments when `CaptureMethodArguments` is enabled. | None. Static read-only property. | [string](https://learn.microsoft.com/dotnet/api/system.string) `"Refit.MethodArguments"`. The value stored under this key is an `object?[]`. |

### Request bodies

[Full description and examples](requests/bodies.md).

Types: `Refit.BodyAttribute`, `Refit.BodySerializationMethod`, `Refit.RequestBodySerializationMode`, `Refit.RequestCompression`, `Refit.RequestCompressionOptions`, `Refit.TimeoutAttribute`.

#### Choose a serialization method

[Full description and examples](requests/bodies.md).

| `BodySerializationMethod` | Behavior |
| --- | --- |
| `Default = 0` | Passes [`HttpContent`](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent) and streams through. Sends a string as plain text. Uses the configured serializer for other values. |
| `Serialized = 3` | Uses the configured serializer, including for strings. A JSON string includes quotes. |
| `UrlEncoded = 2` | Sends form key/value pairs. A dictionary or a generated property map supplies the fields. |
| `JsonLines = 4` | Sends an enumerable as one serialized value per line. Register the element types with the JSON context. |
| `Json = 1` | An obsolete name retained for compatibility. Use `Serialized` in new code. |

#### Buffering and serialization modes

[Full description and examples](requests/bodies.md).

| `RequestBodySerializationMode` | Behavior |
| --- | --- |
| `Default = 0` | Uses the serializer's usual content method. System.Text.Json uses its async metadata path. |
| `Buffered = 1` | Uses [`ISynchronousContentSerializer`](serialization/json.md) to write a complete byte buffer. |
| `Streamed = 2` | Uses that interface to write into the outgoing stream without storing the whole body. |

#### Compression and ownership

[Full description and examples](requests/bodies.md).

| `RequestCompression` | Result |
| --- | --- |
| `Default = 0` | The attribute takes coding and level from settings. Settings set to `Default` do not compress. |
| `None = 1` | No coding; an attribute can opt out of a settings-level coding. |
| `GZip = 2` | `Content-Encoding: gzip`. |
| `Brotli = 3` | `Content-Encoding: br` on .NET 8 and later. |
| `Zstandard = 4` | `Content-Encoding: zstd` on .NET 11 and later. |

#### API reference

[Full description and examples](requests/bodies.md).

| API | Description | Parameters or value | Returns and behavior |
| --- | --- | --- | --- |
| [`BodyAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Marks one interface-method parameter as the HTTP request body. | Applies to a parameter. | Refit uses the parameter value as `HttpContent`, stream content, plain text, or serialized content according to its type and `SerializationMethod`. |
| [`BodySerializationMethod`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodySerializationMethod.cs) | Selects how Refit turns a body value into HTTP content. | Enum values below. | Use with `BodyAttribute` to choose text, serialized, form, or JSON Lines content. |
| [`RequestBodySerializationMode`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBodySerializationMode.cs) | Selects how Refit writes serialized JSON request content. | Enum values below. | Configure through `RefitSettings.RequestBodySerialization`. |
| [`RequestCompression`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompression.cs) | Selects the content coding applied to a request body. | Enum values below. | Configure a default in `RefitSettings` or override it on `BodyAttribute`. |
| [`RequestCompressionOptions`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompressionOptions.cs) | Holds optional compressor-specific settings that replace the resolved compression level for each coding. | Available on .NET 9 and later. | Assign it to `RefitSettings.RequestCompressionOptions`. |
| [`TimeoutAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/TimeoutAttribute.cs) | Applies a per-call timeout to a Refit interface method. | Applies to a method. | A positive timeout cancels the request when it elapses. |
| [`BodySerializationMethod.Default = 0`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodySerializationMethod.cs) | Uses Refit's standard body rules. | `0` | Passes `HttpContent` and streams through, sends strings as plain text, and uses the configured serializer for other values. |
| [`BodySerializationMethod.Json = 1`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodySerializationMethod.cs) | Retains the former name for serialized content. | `1`; obsolete. | Uses the configured serializer, including for strings. Use `Serialized` in new code. |
| [`BodySerializationMethod.UrlEncoded = 2`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodySerializationMethod.cs) | Writes form URL-encoded content. | `2` | A dictionary or object's fields supply form keys and values. |
| [`BodySerializationMethod.Serialized = 3`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodySerializationMethod.cs) | Serializes every body value with the configured content serializer. | `3` | Strings use the serializer too, so a JSON string includes its quotes. |
| [`BodySerializationMethod.JsonLines = 4`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodySerializationMethod.cs) | Writes newline-delimited JSON. | `4` | Serializes each enumerable item with the configured serializer and writes one item per line. |
| [`RequestBodySerializationMode.Default = 0`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBodySerializationMode.cs) | Uses the serializer's asynchronous JSON-content path. | `0` | `System.Text.Json` uses its metadata-based path. |
| [`RequestBodySerializationMode.Buffered = 1`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBodySerializationMode.cs) | Serializes JSON into a complete byte buffer before sending. | `1`; requires [`ISynchronousContentSerializer`](serialization/json.md). | Sends `ByteArrayContent` with `Content-Length`; suited to small and medium bodies. |
| [`RequestBodySerializationMode.Streamed = 2`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBodySerializationMode.cs) | Writes JSON through a `Utf8JsonWriter` to the request stream. | `2`; requires [`ISynchronousContentSerializer`](serialization/json.md). | Bounds peak memory with pooled chunks and does not set `Content-Length`; suited to large uploads. |
| [`RequestCompression.Default = 0`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompression.cs) | Inherits the coding from `RefitSettings.RequestCompression`. | `0` | Uses the settings coding and level. |
| [`RequestCompression.None = 1`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompression.cs) | Disables compression for this body. | `1` | Sends no content coding even when settings choose one. |
| [`RequestCompression.GZip = 2`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompression.cs) | Compresses the body with gzip. | `2`; every Refit target. | Sends `Content-Encoding: gzip`. |
| [`RequestCompression.Brotli = 3`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompression.cs) | Compresses the body with Brotli. | `3`; .NET 8 and later. | Sends `Content-Encoding: br`. |
| [`RequestCompression.Zstandard = 4`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompression.cs) | Compresses the body with Zstandard. | `4`; .NET 11 and later. | Sends `Content-Encoding: zstd`. |
| [`BodyAttribute()`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Creates a body parameter attribute without overrides. | None. | Uses `SerializationMethod.Default` and leaves `Buffered` unset so settings decide. |
| [`BodyAttribute(bool buffered)`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Creates a body parameter attribute with an explicit buffering policy. | `buffered`: [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | Sets `Buffered`; serialization remains `Default`. |
| [`BodyAttribute(BodySerializationMethod serializationMethod, bool buffered)`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Creates a body parameter attribute with explicit serialization and buffering policies. | `serializationMethod`: `BodySerializationMethod`; `buffered`: [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | Sets both properties. |
| [`BodyAttribute(BodySerializationMethod serializationMethod)`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Creates a body parameter attribute with an explicit serialization method. | `serializationMethod`: `BodySerializationMethod`. | Sets `SerializationMethod` and leaves `Buffered` unset so settings decide. |
| [`RequestCompressionOptions()`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompressionOptions.cs) | Creates empty compressor-specific settings. | None; .NET 9 and later. | All coding option properties are `null`, so compression uses its resolved level. |
| [`TimeoutAttribute(int milliseconds)`](https://github.com/reactiveui/refit/blob/main/src/Refit/TimeoutAttribute.cs) | Creates a method timeout attribute. | `milliseconds`: [`int`](https://learn.microsoft.com/dotnet/api/system.int32). | A positive value applies the per-call deadline; zero or a negative value disables it. |
| [`BodyAttribute.Buffered`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Gets the per-body buffering override. | Read-only [`bool?`](https://learn.microsoft.com/dotnet/api/system.boolean). | `null` uses `RefitSettings.Buffered`; `true` buffers content before sending and `false` skips it. |
| [`BodyAttribute.SerializationMethod`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Gets the selected body serialization method. | Read-only `BodySerializationMethod`; default `Default`. | Determines how ordinary body values become HTTP content. |
| [`BodyAttribute.Compression`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Gets or sets a method-level request content coding. | Settable `RequestCompression`; default `Default`. | `Default` follows settings, while `None` opts this body out of a settings-level coding. |
| [`BodyAttribute.CompressionLevel`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Gets or sets the compression effort for an explicitly selected coding. | Settable [`CompressionLevel`](https://learn.microsoft.com/dotnet/api/system.io.compression.compressionlevel); default `Optimal`. | Refit reads it only when `Compression` names a coding; otherwise settings provide the level. |
| [`RequestCompressionOptions.GZip`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompressionOptions.cs) | Gets or sets gzip-specific compressor settings. | Settable [`ZLibCompressionOptions?`](https://learn.microsoft.com/dotnet/api/system.io.compression.zlibcompressionoptions). | A non-null value replaces the resolved level for gzip; `null` uses that level. |
| [`RequestCompressionOptions.Brotli`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompressionOptions.cs) | Gets or sets Brotli-specific compressor settings. | Settable [`BrotliCompressionOptions?`](https://learn.microsoft.com/dotnet/api/system.io.compression.brotlicompressionoptions). | A non-null value replaces the resolved level for Brotli; `null` uses that level. |
| [`RequestCompressionOptions.Zstandard`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompressionOptions.cs) | Gets or sets Zstandard-specific compressor settings. | Settable [`ZstandardCompressionOptions?`](https://learn.microsoft.com/dotnet/api/system.io.compression.zstandardcompressionoptions); .NET 11 and later. | A non-null value replaces the resolved level for Zstandard; `null` uses that level. |
| [`TimeoutAttribute.Milliseconds`](https://github.com/reactiveui/refit/blob/main/src/Refit/TimeoutAttribute.cs) | Gets the timeout supplied to `TimeoutAttribute`. | Read-only [`int`](https://learn.microsoft.com/dotnet/api/system.int32), in milliseconds. | The effective request deadline exists only when the value is positive. |

### Upload files with multipart requests

[Full description and examples](requests/multipart.md).

Types: `Refit.AttachmentNameAttribute`, `Refit.ByteArrayPart`, `Refit.FileInfoPart`, `Refit.FormObjectAttribute`, `Refit.MultipartAttribute`, `Refit.MultipartItem`, `Refit.StreamPart`.

#### Field names, file names and content types

[Full description and examples](requests/multipart.md).

| Input | Form field name | File name sent |
| --- | --- | --- |
| A part wrapper with `Name` set | Its `Name`, overriding `[AliasAs]` | Its nonempty `FileName` |
| A wrapper with `Name = null` | `[AliasAs]`, otherwise parameter name | Its nonempty `FileName` |
| A wrapper with empty `FileName` | The same field-name rules | The parameter's aliased or declared name |
| Raw `Stream` or `byte[]` | Aliased or declared parameter name | The same name |
| Raw `FileInfo` | Aliased or declared parameter name | `FileInfo.Name` |
| Raw `HttpContent` | Its existing content-disposition metadata | Its existing metadata |
| A string, formatted value or serialized model | Aliased or declared parameter name | None |

#### API reference

[Full description and examples](requests/multipart.md).

| API | Description | Parameters or value | Returns and behavior |
| --- | --- | --- | --- |
| [`AttachmentNameAttribute(string name)`](https://github.com/reactiveui/refit/blob/main/src/Refit/AttachmentNameAttribute.cs) *(obsolete)* | Stores the legacy attachment file-name override. Use a part wrapper for new code. | `name`: [`string`](https://learn.microsoft.com/dotnet/api/system.string) to expose through `Name` | Creates the obsolete attribute; using it produces compiler warning `CS0618`. |
| [`AttachmentNameAttribute.Name`](https://github.com/reactiveui/refit/blob/main/src/Refit/AttachmentNameAttribute.cs) *(obsolete)* | Gets the legacy file-name override. | Read-only [`string`](https://learn.microsoft.com/dotnet/api/system.string) | Returns the constructor's `name`. |
| [`ByteArrayPart(byte[] value, string fileName, string? contentType = null, string? name = null)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ByteArrayPart.cs) | Creates a multipart item backed by a byte array. | `value`: [`byte[]`](https://learn.microsoft.com/dotnet/api/system.byte); `fileName`: [`string`](https://learn.microsoft.com/dotnet/api/system.string); `contentType`: optional media type, default `null`; `name`: optional form field name, default `null` | Stores the same byte array reference. Throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception) when `value` is `null`. |
| [`ByteArrayPart.Value`](https://github.com/reactiveui/refit/blob/main/src/Refit/ByteArrayPart.cs) | Gets the bytes supplied to the constructor. | Read-only [`byte[]`](https://learn.microsoft.com/dotnet/api/system.byte) | Returns the original array. |
| [`ByteArrayPart.CreateContent()`](https://github.com/reactiveui/refit/blob/main/src/Refit/ByteArrayPart.cs) *(protected override)* | Builds content for the byte-array part. | None | Returns [`ByteArrayContent`](https://learn.microsoft.com/dotnet/api/system.net.http.bytearraycontent) over `Value`. |
| [`FileInfoPart(FileInfo value, string fileName, string? contentType = null, string? name = null)`](https://github.com/reactiveui/refit/blob/main/src/Refit/FileInfoPart.cs) | Creates a multipart item backed by a local file. | `value`: [`FileInfo`](https://learn.microsoft.com/dotnet/api/system.io.fileinfo); `fileName`: [`string`](https://learn.microsoft.com/dotnet/api/system.string); `contentType`: optional media type, default `null`; `name`: optional form field name, default `null` | Stores the file information. Throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception) when `value` is `null`; it opens the file only when content is created. |
| [`FileInfoPart.Value`](https://github.com/reactiveui/refit/blob/main/src/Refit/FileInfoPart.cs) | Gets the source file information. | Read-only [`FileInfo`](https://learn.microsoft.com/dotnet/api/system.io.fileinfo) | Returns the original `FileInfo`. |
| [`FileInfoPart.CreateContent()`](https://github.com/reactiveui/refit/blob/main/src/Refit/FileInfoPart.cs) *(protected override)* | Opens the source file and builds content for the part. | None | Returns [`StreamContent`](https://learn.microsoft.com/dotnet/api/system.net.http.streamcontent) over a newly opened read stream. |
| [`FormObjectAttribute()`](https://github.com/reactiveui/refit/blob/main/src/Refit/FormObjectAttribute.cs) | Marks a complex multipart parameter for property flattening. | None | Causes each public property to become a text part on the reflection request-builder path. |
| [`MultipartAttribute(string boundaryText = "----MyGreatBoundary")`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartAttribute.cs) | Marks an HTTP method as multipart and chooses its boundary. | `boundaryText`: [`string`](https://learn.microsoft.com/dotnet/api/system.string), default `"----MyGreatBoundary"` | Stores the boundary used to separate parts. |
| [`MultipartAttribute.BoundaryText`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartAttribute.cs) | Gets the boundary configured for the method. | Read-only [`string`](https://learn.microsoft.com/dotnet/api/system.string) | Returns the supplied boundary text. |
| [`MultipartItem(string fileName, string? contentType)`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) *(protected)* | Initializes a custom multipart item without an explicit form field name. | `fileName`: [`string`](https://learn.microsoft.com/dotnet/api/system.string); `contentType`: optional media type | Stores the file name and content type, with `Name` set to `null`. Throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception) for a null file name. |
| [`MultipartItem(string fileName, string? contentType, string? name)`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) *(protected)* | Initializes a custom multipart item with optional form field metadata. | `fileName`: [`string`](https://learn.microsoft.com/dotnet/api/system.string); `contentType`: optional media type; `name`: optional form field name | Stores all three values. A null file name throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception). |
| [`MultipartItem.Name`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) | Gets the explicit form field name for the item. | Read-only [`string?`](https://learn.microsoft.com/dotnet/api/system.string) | Returns `null` when the constructor did not receive a name. |
| [`MultipartItem.ContentType`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) | Gets the optional media type for the item content. | Read-only [`string?`](https://learn.microsoft.com/dotnet/api/system.string) | Returns the configured content type, or `null`. |
| [`MultipartItem.FileName`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) | Gets the file name sent in the multipart disposition. | Read-only [`string`](https://learn.microsoft.com/dotnet/api/system.string) | Returns the required file name. |
| [`MultipartItem.ToContent()`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) | Creates this item's content and applies its nonempty `ContentType`. | None | Returns [`HttpContent`](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent). The caller disposes the returned content. |
| [`MultipartItem.CreateContent()`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) *(protected abstract)* | Defines how a derived item creates fresh underlying content. | None | Returns [`HttpContent`](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent); `ToContent()` applies the configured media type afterward. |
| [`StreamPart(Stream value, string fileName, string? contentType = null, string? name = null)`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamPart.cs) | Creates a multipart item backed by a caller-owned stream. | `value`: [`Stream`](https://learn.microsoft.com/dotnet/api/system.io.stream); `fileName`: [`string`](https://learn.microsoft.com/dotnet/api/system.string); `contentType`: optional media type, default `null`; `name`: optional form field name, default `null` | Stores the stream without copying it. Throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception) when `value` is `null`; disposing its content leaves the caller's stream open. |
| [`StreamPart.Value`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamPart.cs) | Gets the caller-owned stream. | Read-only [`Stream`](https://learn.microsoft.com/dotnet/api/system.io.stream) | Returns the original stream. |
| [`StreamPart.CreateContent()`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamPart.cs) *(protected override)* | Wraps the stream without taking ownership of it. | None | Returns [`HttpContent`](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent) that reads from `Value`. |
| [`AttachmentNameAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/AttachmentNameAttribute.cs) *(obsolete)* | Legacy attribute for naming an attachment. | None | Attribute type; prefer the part wrapper types. |
| [`ByteArrayPart`](https://github.com/reactiveui/refit/blob/main/src/Refit/ByteArrayPart.cs) | Represents byte-array content with multipart metadata. | None | Multipart item type derived from `MultipartItem`. |
| [`FileInfoPart`](https://github.com/reactiveui/refit/blob/main/src/Refit/FileInfoPart.cs) | Represents file content with multipart metadata. | None | Multipart item type derived from `MultipartItem`. |
| [`FormObjectAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/FormObjectAttribute.cs) | Marks a complex parameter for multipart property flattening. | None | Parameter attribute type. |
| [`MultipartAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartAttribute.cs) | Marks a method whose body contains named multipart parts. | None | Method attribute type. |
| [`MultipartItem`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) | Base class for parts that carry a file name and optional content metadata. | None | Abstract type for custom multipart items. |
| [`StreamPart`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamPart.cs) | Represents caller-owned stream content with multipart metadata. | None | Multipart item type derived from `MultipartItem`. |


## Results

### Return types

[Full description and examples](results/return-types.md).

#### Choose a shape

[Full description and examples](results/return-types.md).

| Return type | What happens |
| --- | --- |
| [`Task`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task) | Sends the request and completes without a result value. See [reading one reply](results/return-types.md#read-one-reply). |
| [`Task<T>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1) | Sends the request and gives you one result to await. See [reading one reply](results/return-types.md#read-one-reply). |
| [`ValueTask<T>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1) | Sends the request and gives you one task-backed result to await. See [reading one reply](results/return-types.md#read-one-reply). |
| [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) | Sends a fresh request per subscription and pushes one result. See [querying a reply](results/return-types.md#querying-a-reply). |
| [`Task<ApiResponse<T>>`](results/responses.md) | Gives you a result wrapper with status, headers and a captured error. See [keeping status and error details](results/return-types.md#keep-status-and-error-details). |
| [`Task<IApiResponse<T>>`](results/responses.md) | Gives you the typed response wrapper through its interface. See [response details](results/responses.md). |
| [`Task<IApiResponse>`](results/responses.md) | Gives you response details without a typed reply body; the wrapper owns live response content. See [response details](results/responses.md). |
| [`PagedEnumerable<TPage, TItem>`](results/pagination.md) | Sends one request per page, lazily, and yields the items of every page. Needs the source generator and a `[Paged]` attribute. See [pagination](results/pagination.md). |
| [`Task<HttpRequestMessage>`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) | Builds a request and returns it without sending it; the caller owns and must dispose it. |
| [`Task<HttpResponseMessage>`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) | Returns the live HTTP response; the caller owns and must dispose it. |
| [`Task<HttpContent>`](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent) | Returns the live response content; the caller owns and must dispose it. |
| [`Task<Stream>`](https://learn.microsoft.com/dotnet/api/system.io.stream) | Returns the live response body stream; the caller owns and must dispose it. |
| [`Task<ApiResponse<HttpResponseMessage>>`](results/responses.md) | Wraps the live HTTP response; the caller owns and must dispose it. |
| [`Task<ApiResponse<HttpContent>>`](results/responses.md) | Wraps the live response content; the caller owns and must dispose it. |
| [`Task<ApiResponse<Stream>>`](results/responses.md) | Wraps the live response body stream; the caller owns and must dispose it. |
| [`IAsyncEnumerable<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.iasyncenumerable-1) | Reads items from one [streaming reply](results/streaming.md); the enumeration owns the live response until it ends. |

### Streaming replies

[Full description and examples](results/streaming.md).

#### Reply formats

[Full description and examples](results/streaming.md).

| Format | Content type | Body shape |
| --- | --- | --- |
| `JsonArray` | `application/json`, or another type not listed below | A JSON array such as `[{"id":1,"name":"Ada"}]`. |
| `JsonLines` | `application/jsonl`, `application/x-ndjson`, or `application/x-jsonlines` | Each line holds a separate JSON value. |
| `ServerSentEvents` | `text/event-stream` | Each event's `data:` field holds a JSON value. |

### Response details and success checks

[Full description and examples](results/responses.md).

Types: `Refit.ApiRequestException`, `Refit.ApiResponseExtensions`, `Refit.ApiResponse<T>`, `Refit.IApiResponse`, `Refit.IApiResponse<T>`.

#### Status success and content success differ

[Full description and examples](results/responses.md).

| Property | Meaning |
| --- | --- |
| `IsReceived` | A response message exists. False means no response arrived. |
| `IsSuccessStatusCode` | A response exists and its status is 200–299. |
| `IsSuccessful` | The status succeeds and `Error` is null. |
| `HasContent` | `Content` is non-null. For a value type, its default value is also non-null. |
| `IsSuccessfulWithContent` | Both `IsSuccessful` and `HasContent` are true. |
| `Content` | The deserialized reply value, or default when unavailable. |
| `Error` | A captured `ApiExceptionBase`, or null. An unsuccessful manually constructed wrapper may have no error. |
| `Settings` | The settings supplied to the concrete wrapper. It keeps the same instance. |
| `RequestMessage` | The request associated with this reply. The interface permits null; the concrete wrapper returns its constructor argument. |
| `StatusCode`, `ReasonPhrase`, `Version` | The response status, reason text and HTTP version. Null when no response exists. |
| `Headers` | The response headers. Null when no response exists. |
| `ContentHeaders` | The body headers, such as its media type. Null when unavailable. |

#### Response API reference

[Full description and examples](results/responses.md).

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| [`ApiRequestException`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiRequestException.cs) | Represents a failure while Refit sends a request before a response arrives. | None. | An [`ApiExceptionBase`](results/errors.md) that retains request context and may wrap the sending exception. |
| [`ApiResponse<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs) | Wraps a deserialized body, HTTP metadata, settings, and a captured error. | `T`: the body type. | A sealed [`IApiResponse<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse%7BT%7D.cs) that disposes the received response. |
| [`IApiResponse`](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse.cs) | Defines status, metadata, error, and disposal members for a Refit response. | None. | The base response contract. |
| [`IApiResponse<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse%7BT%7D.cs) | Adds a covariant deserialized body and body-presence checks to `IApiResponse`. | `out T`: the body type read by callers. | The typed response contract. |
| [`ApiResponseExtensions`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponseExtensions.cs) | Provides success guards for generic and non-generic response interfaces. | None. | A static extension class. |
| [`ApiResponse<T>(HttpRequestMessage request, HttpResponseMessage? response, T? content, RefitSettings settings, ApiExceptionBase? error = null)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs) | Creates a response wrapper that keeps the request, optional HTTP response, deserialized content, settings, and captured error together. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `request`; [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)`?` `response`; `T?` `content`; [`RefitSettings`](clients/settings.md) `settings`; [`ApiExceptionBase`](results/errors.md)`?` `error = null`. | New response wrapper. `response` may be null for a transport failure; `error` defaults to null. |
| [`ApiResponse<T>(HttpResponseMessage response, T? content, RefitSettings settings)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs) | Creates a wrapper for a received response with no captured error. | [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) `response`; `T?` `content`; [`RefitSettings`](clients/settings.md) `settings`. | A new wrapper. `response` and `response.RequestMessage` must be non-null. |
| [`ApiResponse<T>(HttpResponseMessage response, T? content, RefitSettings settings, ApiExceptionBase? error)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs) | Creates a wrapper for a received response and a supplied captured error. | [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) `response`; `T?` `content`; [`RefitSettings`](clients/settings.md) `settings`; [`ApiExceptionBase`](results/errors.md)`?` `error`. | A new wrapper. `response` and `response.RequestMessage` must be non-null. |
| [`ApiResponse<T>.Dispose()`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs) | Disposes the received HTTP response once. | None. | [`void`](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/void); it does not dispose `RequestMessage`. |
| [`ApiResponse<T>.EnsureSuccessStatusCodeAsync()`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs) | Guards only the HTTP status. | None. | [`ValueTask<ApiResponse<T>>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1); returns this for 2xx. Otherwise it disposes and throws `Error` or a created `ApiException`; with no response it throws `InvalidOperationException`. |
| [`ApiResponse<T>.EnsureSuccessfulAsync()`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs) | Guards the HTTP status and captured error. | None. | [`ValueTask<ApiResponse<T>>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1); returns this when `IsSuccessful` is true. Failure behavior matches the status guard. |
| `ApiResponse<T>.HasRequestError(out ApiRequestException? error)` | Checks for a captured transport error and returns it through the out parameter. | `out` [`ApiRequestException`](results/errors.md)`?` `error`. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean); true when `Error` is a request error. |
| `ApiResponse<T>.HasResponseError(out ApiException? error)` | Checks for a captured response error and returns it through the out parameter. | `out` [`ApiException`](results/errors.md)`?` `error`. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean); true when `Error` is a response error. |
| `ApiResponse<T>.Content` | Stores the deserialized response body, or the default value when no body was read. | None. | `T?`: the stored response body, or `default(T)` when no value was read. For a non-nullable value type, this can be a value such as `0`. |
| `ApiResponse<T>.ContentHeaders` | Exposes headers belonging to the received response body. | None. | [`HttpContentHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpcontentheaders)`?`: content headers, or null when no response content exists. |
| `ApiResponse<T>.Error` | Stores the captured transport, HTTP-status, or deserialization error. | None. | [`ApiExceptionBase`](results/errors.md)`?`: the captured transport, HTTP, or deserialization error. |
| `ApiResponse<T>.HasContent` | Reports whether the deserialized Content is non-null. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean); `Content` is non-null. |
| `ApiResponse<T>.IsSuccessfulWithContent` | Reports whether the response succeeded without an error and has non-null Content. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when the status is successful, no error was captured, and `Content` is non-null. |
| `ApiResponse<T>.Headers` | Exposes headers from the received HTTP response. | None. | [`HttpResponseHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders)`?`: the received response headers, or null when no response arrived. |
| `ApiResponse<T>.IsReceived` | Reports whether an HTTP response message was received. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when an HTTP response arrived. |
| `ApiResponse<T>.IsSuccessStatusCode` | Reports whether the received status code is in the 2xx range. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when a received response has a 2xx status. |
| `ApiResponse<T>.IsSuccessful` | Reports whether the status is 2xx and no error was captured; it does not require content. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when the status is 2xx and no error was captured. It does not promise body content. |
| `ApiResponse<T>.ReasonPhrase` | Exposes the reason phrase returned with the HTTP status. | None. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?`: the server reason phrase, or null when none is available. |
| `ApiResponse<T>.RequestMessage` | Exposes the request associated with the response wrapper. | None. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage): the request associated with this wrapper. |
| `ApiResponse<T>.Settings` | Exposes the RefitSettings used to process the response. | None. | [`RefitSettings`](clients/settings.md): the settings instance supplied to the constructor. |
| `ApiResponse<T>.StatusCode` | Exposes the received HTTP status code, or null when no response arrived. | None. | [`HttpStatusCode`](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode)`?`: the received status, or null when no response arrived. |
| `ApiResponse<T>.Version` | Exposes the HTTP version used by the received response. | None. | [`Version`](https://learn.microsoft.com/dotnet/api/system.version)`?`: the received HTTP version, or null when no response arrived. |
| `IApiResponse.HasRequestError(out ApiRequestException? error)` | Checks for a captured transport error and returns it through the out parameter. | `out` [`ApiRequestException`](results/errors.md)`?` `error`. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true and assigns the transport error when the request failed before a response; otherwise false and null. |
| `IApiResponse.HasResponseError(out ApiException? error)` | Checks for a captured response error and returns it through the out parameter. | `out` [`ApiException`](results/errors.md)`?` `error`. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true and assigns the response or body-reading error; otherwise false and null. |
| `IApiResponse.Headers` | Exposes headers from the received HTTP response. | None. | [`HttpResponseHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders)`?`: the received response headers, or null when no response arrived. |
| `IApiResponse.ContentHeaders` | Exposes headers belonging to the received response body. | None. | [`HttpContentHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpcontentheaders)`?`: headers for the received response body, or null when they are unavailable. |
| `IApiResponse.IsReceived` | Reports whether an HTTP response message was received. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when an HTTP response arrived. |
| `IApiResponse.IsSuccessStatusCode` | Reports whether the received status code is in the 2xx range. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when a received response has a 2xx status. |
| `IApiResponse.IsSuccessful` | Reports whether the status is 2xx and no error was captured; it does not require content. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when the status is 2xx and no error was captured. It does not promise body content. |
| `IApiResponse.StatusCode` | Exposes the received HTTP status code, or null when no response arrived. | None. | [`HttpStatusCode`](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode)`?`: the received status, or null when no response arrived. |
| `IApiResponse.ReasonPhrase` | Exposes the reason phrase returned with the HTTP status. | None. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?`: the server reason phrase, or null when none is available. |
| `IApiResponse.RequestMessage` | Exposes the request associated with the response wrapper. | None. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage)`?`: the request that led to the response, or null when unavailable. |
| `IApiResponse.Version` | Exposes the HTTP version used by the received response. | None. | [`Version`](https://learn.microsoft.com/dotnet/api/system.version)`?`: the received HTTP version, or null when no response arrived. |
| `IApiResponse.Error` | Stores the captured transport, HTTP-status, or deserialization error. | None. | [`ApiExceptionBase`](results/errors.md)`?`: a captured transport, HTTP, or deserialization error. An unsuccessful response can have no captured error. |
| `IApiResponse<T>.Content` | Stores the deserialized response body, or the default value when no body was read. | None. | `T?`: the stored response body, or `default(T)` when no value was read. For a non-nullable value type, this can be a value such as `0`. |
| `IApiResponse<T>.HasContent` | Reports whether the deserialized Content is non-null. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when `Content` is non-null. |
| `IApiResponse<T>.IsSuccessfulWithContent` | Reports whether the response succeeded without an error and has non-null Content. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when `IsSuccessful` is true and `Content` is non-null. |
| [`ApiRequestException(HttpRequestMessage message, HttpMethod httpMethod, RefitSettings refitSettings, Exception innerException)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiRequestException.cs) | Creates a transport exception with the request, HTTP method, settings, and supplied message or inner cause. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`RefitSettings`](clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception) `innerException`. | New transport exception using the non-null cause's message. |
| [`ApiRequestException(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, RefitSettings refitSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiRequestException.cs) | Creates a transport exception with the request, HTTP method, settings, and supplied message or inner cause. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`RefitSettings`](clients/settings.md) `refitSettings`. | New transport exception with the supplied message. |
| [`ApiRequestException(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, RefitSettings refitSettings, Exception? innerException)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiRequestException.cs) | Creates a transport exception with the request, HTTP method, settings, and supplied message or inner cause. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`RefitSettings`](clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception)`?` `innerException`. | New transport exception with the supplied message and optional cause. |
| [`ApiResponseExtensions.EnsureSuccessStatusCodeAsync<T>(IApiResponse<T> response)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponseExtensions.cs) | Guards only the HTTP status of a typed response. | [`IApiResponse<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse%7BT%7D.cs) `response`. | [`ValueTask<IApiResponse<T>>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1); returns the same response for 2xx. It rejects null, and otherwise throws `Error` or `InvalidOperationException` without disposing. |
| [`ApiResponseExtensions.EnsureSuccessfulAsync<T>(IApiResponse<T> response)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponseExtensions.cs) | Guards the HTTP status and captured error of a typed response. | [`IApiResponse<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse%7BT%7D.cs) `response`. | [`ValueTask<IApiResponse<T>>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1); returns the same response when `IsSuccessful` is true. Failure behavior matches the status guard. |
| [`ApiResponseExtensions.EnsureSuccessStatusCodeAsync(IApiResponse response)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponseExtensions.cs) | Guards only the HTTP status of a non-generic response. | [`IApiResponse`](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse.cs) `response`. | [`ValueTask<IApiResponse>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1); returns the same response for 2xx. It rejects null, and otherwise throws `Error` or `InvalidOperationException` without disposing. |
| [`ApiResponseExtensions.EnsureSuccessfulAsync(IApiResponse response)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponseExtensions.cs) | Guards the HTTP status and captured error of a non-generic response. | [`IApiResponse`](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse.cs) `response`. | [`ValueTask<IApiResponse>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1); returns the same response when `IsSuccessful` is true. Failure behavior matches the status guard. |

### Error bodies and problem details

[Full description and examples](results/errors.md).

Types: `Refit.ApiException`, `Refit.ApiExceptionBase`, `Refit.DefaultApiExceptionFactory`, `Refit.ProblemDetails`, `Refit.ValidationApiException`.

#### Shared request details

[Full description and examples](results/errors.md).

| Property | Meaning |
| --- | --- |
| `HttpMethod` | The method supplied for the failed call. |
| `RequestMessage` | The request, including its headers and local options. |
| `Uri` | `RequestMessage.RequestUri`, which can be null. |
| `RefitSettings` | The settings retained for the call and later error-body reading. |
| `RequestContent` | Captured request-body text when enabled; you can replace it to remove private data. |
| `HasRequestContent` | The captured text is neither null nor empty. Whitespace counts as present. |

#### Standard validation replies

[Full description and examples](results/errors.md).

| `ProblemDetails` property | Meaning |
| --- | --- |
| `Type` | A URI identifying the kind of problem; defaults to `about:blank`. |
| `Title` | A short label for that kind of problem. |
| `Status` | The status in the JSON document. It does not replace the actual HTTP status. |
| `Detail` | Text about this occurrence. |
| `Instance` | A URI identifying this occurrence. |
| `Errors` | A mutable dictionary from field name to an array of validation messages. Empty by default. |
| `Extensions` | A mutable dictionary for other JSON properties. Empty by default. |

#### Error API reference

[Full description and examples](results/errors.md).

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| `ApiExceptionBase` | Abstract base class for Refit exceptions that retain the failed request, its HTTP method, and the settings used for the call. | None. | Base for request-send and response exceptions. |
| `ApiExceptionBase(HttpRequestMessage message, HttpMethod httpMethod, RefitSettings refitSettings, Exception innerException)` | Initializes an error with the request context and a required underlying exception. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`RefitSettings`](clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception) `innerException`. | Protected base constructor using the non-null cause's message. |
| `ApiExceptionBase(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, RefitSettings refitSettings)` | Initializes an error with a caller-supplied message and request context. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`RefitSettings`](clients/settings.md) `refitSettings`. | Protected base constructor with the supplied message. |
| `ApiExceptionBase(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, RefitSettings refitSettings, Exception? innerException)` | Initializes an error with a caller-supplied message, request context, and optional cause. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`RefitSettings`](clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception)`?` `innerException`. | Protected base constructor with an optional cause. |
| `ApiExceptionBase.HttpMethod` | Identifies the HTTP method that Refit used for the failed request. | None. | [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod): the method used by the failed call. |
| `ApiExceptionBase.Uri` | Exposes the request URI when the retained request has one. | None. | [`Uri`](https://learn.microsoft.com/dotnet/api/system.uri)`?`: `RequestMessage.RequestUri`. |
| `ApiExceptionBase.RequestMessage` | Gives access to the live request, including headers and request options. | None. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage): the request, including its headers and local options. |
| `ApiExceptionBase.RequestContent` | Holds request-body text captured before sending when `CaptureRequestContent` is enabled. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` value. | Captured request-body text. You can replace it to remove private data. |
| `ApiExceptionBase.HasRequestContent` | Lets you test whether captured request text is available without checking the property yourself. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when captured request text is not null or empty. Whitespace counts as present. |
| `ApiExceptionBase.RefitSettings` | Gets the settings that governed the failed call. | None. | [`RefitSettings`](clients/settings.md): settings retained for the call and later error-body reading. |
| `ApiException` | Represents an error received after the server sent an HTTP response. | None. | Exception with response status, headers, and buffered body text. |
| `ApiException(HttpRequestMessage message, HttpMethod httpMethod, string? content, HttpStatusCode statusCode, string? reasonPhrase, HttpResponseHeaders headers, RefitSettings refitSettings)` | Initializes a response exception with Refit's status-and-reason message. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `content`; [`HttpStatusCode`](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode) `statusCode`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `reasonPhrase`; [`HttpResponseHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders) `headers`; [`RefitSettings`](clients/settings.md) `refitSettings`. | Protected HTTP-response constructor. |
| `ApiException(HttpRequestMessage message, HttpMethod httpMethod, string? content, HttpStatusCode statusCode, string? reasonPhrase, HttpResponseHeaders headers, RefitSettings refitSettings, Exception? innerException)` | Initializes a response exception with Refit's status-and-reason message and an optional underlying cause. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `content`; [`HttpStatusCode`](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode) `statusCode`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `reasonPhrase`; [`HttpResponseHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders) `headers`; [`RefitSettings`](clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception)`?` `innerException`. | Protected HTTP-response constructor with an optional cause. |
| `ApiException(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, string? content, HttpStatusCode statusCode, string? reasonPhrase, HttpResponseHeaders headers, RefitSettings refitSettings)` | Initializes a response exception with an app-defined message. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `content`; [`HttpStatusCode`](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode) `statusCode`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `reasonPhrase`; [`HttpResponseHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders) `headers`; [`RefitSettings`](clients/settings.md) `refitSettings`. | Protected HTTP-response constructor with the supplied message. |
| `ApiException(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, string? content, HttpStatusCode statusCode, string? reasonPhrase, HttpResponseHeaders headers, RefitSettings refitSettings, Exception? innerException)` | Initializes a response exception with an app-defined message and an optional underlying cause. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `content`; [`HttpStatusCode`](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode) `statusCode`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `reasonPhrase`; [`HttpResponseHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders) `headers`; [`RefitSettings`](clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception)`?` `innerException`. | Protected HTTP-response constructor with the supplied message and optional cause. |
| `ApiException.Create(HttpRequestMessage message, HttpMethod httpMethod, HttpResponseMessage response, RefitSettings refitSettings)` | Builds an ApiException asynchronously from the failed HTTP response and request metadata. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) `response`; [`RefitSettings`](clients/settings.md) `refitSettings`. | [`Task<ApiException>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1) that captures the unsuccessful response. |
| `ApiException.Create(HttpRequestMessage message, HttpMethod httpMethod, HttpResponseMessage response, RefitSettings refitSettings, Exception? innerException)` | Builds an ApiException asynchronously from the failed HTTP response and request metadata. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) `response`; [`RefitSettings`](clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception)`?` `innerException`. | [`Task<ApiException>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1) that captures the response and optional cause. |
| `ApiException.Create(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, HttpResponseMessage response, RefitSettings refitSettings)` | Builds an ApiException asynchronously from the failed HTTP response and request metadata. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) `response`; [`RefitSettings`](clients/settings.md) `refitSettings`. | [`Task<ApiException>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1) with the supplied message. |
| `ApiException.Create(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, HttpResponseMessage response, RefitSettings refitSettings, Exception? innerException)` | Builds an ApiException asynchronously from the failed HTTP response and request metadata. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) `response`; [`RefitSettings`](clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception)`?` `innerException`. | [`Task<ApiException>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1) with the supplied message and optional cause. |
| `ApiException.GetContentAsAsync<T>()` | Deserializes buffered response text through the configured asynchronous content serializer. | None; `T` is the requested error-body type. | [`Task<T?>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1); asynchronous deserialization. |
| `ApiException.GetContentAs<T>()` | Deserializes buffered response text through the configured synchronous content serializer. | None; `T` is the requested error-body type. | `T?`; synchronous deserialization or [`NotSupportedException`](https://learn.microsoft.com/dotnet/api/system.notsupportedexception). |
| `ApiException.TryGetContentAs<T>(out T? content)` | Tries synchronous error-body deserialization without letting parsing or serializer-support failures escape. | `out T?` `content`. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean); false for absent, unsupported, or invalid content. |
| `ApiException.StatusCode` | Identifies the HTTP status sent by the server. | None. | [`HttpStatusCode`](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode): the received HTTP response status. |
| `ApiException.ReasonPhrase` | Preserves the optional reason phrase sent with the HTTP status. | None. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?`: the server reason phrase, if supplied. |
| `ApiException.Headers` | Provides the response headers retained from the failed response. | None. | [`HttpResponseHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders): the received response headers. |
| `ApiException.ContentHeaders` | Provides headers belonging to the buffered response body. | None; protected setter. | [`HttpContentHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpcontentheaders)`?`: headers for the captured response body. |
| `ApiException.Content` | Holds the raw buffered response body and lets a redactor replace or clear it. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` value. | Captured raw response text. You can replace it to remove private data. |
| `ApiException.HasContent` | Tests whether `Content` contains non-whitespace response text. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when `Content` is not null, empty, or whitespace. |
| `DefaultApiExceptionFactory` | Supplies Refit's default conversion from an unsuccessful HTTP response to an `ApiException`. | None. | Response-to-exception factory. |
| `DefaultApiExceptionFactory(RefitSettings refitSettings)` | Creates the exception factory that turns unsuccessful responses into ApiException instances using the supplied settings. | [`RefitSettings`](clients/settings.md) `refitSettings`: settings used to create exceptions. | New factory. |
| `DefaultApiExceptionFactory.CreateAsync(HttpResponseMessage responseMessage)` | Returns no exception for a successful response, or creates an `ApiException` from an unsuccessful response's retained request. | [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) `responseMessage`. | [`ValueTask<Exception?>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1); null for a successful response. |
| `ProblemDetails` | Models a standard HTTP problem document, including validation errors and extension fields. | None. | Data object used by `ValidationApiException`. |
| `ProblemDetails()` | Initializes a problem document with empty `Errors` and `Extensions`, and `Type` set to `about:blank`. | None. | New problem-details object with empty Errors/Extensions and Type "about:blank". |
| `ProblemDetails.Errors` | Maps each invalid field name to its validation messages. | None; `init` only. | [`Dictionary<string, string[]>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.dictionary-2); default empty. |
| `ProblemDetails.Extensions` | Stores JSON properties that are not standard problem-details fields. | None; `init` only. | [`IDictionary<string, object>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2); default empty. |
| `ProblemDetails.Type` | Identifies the kind of problem, usually with a URI. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` value. | A URI that identifies the problem kind; default `about:blank`. |
| `ProblemDetails.Title` | Gives a short human-readable label for the problem kind. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` value. | A short label for the problem kind; default null. |
| `ProblemDetails.Status` | Carries the status recorded in the JSON problem document. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) value. | The status value carried in the problem document; default 0. It does not replace the actual HTTP status. |
| `ProblemDetails.Detail` | Explains this particular problem occurrence. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` value. | Text about this problem occurrence; default null. |
| `ProblemDetails.Instance` | Identifies this particular problem occurrence, usually with a URI. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` value. | A URI that identifies this problem occurrence; default null. |
| `ValidationApiException` | Represents an API error whose body has been parsed as standard problem details. | None. | `ApiException` subtype with typed validation content. |
| `ValidationApiException(string message)` | Creates a validation exception for app code that has no received problem response to convert. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `message`. | New validation exception with synthetic HTTP context. |
| `ValidationApiException(string message, Exception innerException)` | Creates a validation exception with an app-defined message and a required cause. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `message`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception) `innerException`: non-null cause. | New validation exception with cause. |
| `ValidationApiException.Create(ApiException exception)` | Parses the non-blank raw body of an existing API exception as standard problem details. | [`ApiException`](results/errors.md) `exception`: error to convert; it must contain non-whitespace content. | ValidationApiException with ProblemDetails content. |
| `ValidationApiException.Content` | Exposes the parsed problem document while hiding `ApiException.Content` on a validation exception. | None; private setter. | [`ProblemDetails`](results/errors.md)`?`: the parsed validation body, or null when this exception was created only with a message. |

### Custom return adapters

[Full description and examples](results/adapters.md).

Types: `Refit.IReturnTypeAdapter<TReturn, TResult>`.

#### Adapter reference

[Full description and examples](results/adapters.md).

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| [`TReturn IReturnTypeAdapter<TReturn, TResult>.Adapt(Func<CancellationToken, Task<TResult>> invoke)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IReturnTypeAdapter.cs) | Converts the deferred HTTP operation into the custom return shape. | [`Func<CancellationToken, Task<TResult>>`](https://learn.microsoft.com/dotnet/api/system.func-2) `invoke`: deferred HTTP invocation. | `TReturn`: the wrapper value surfaced by the interface method. |
| [`RefitSettings.ReturnTypeAdapters`](clients/settings.md) | Exposes the adapter types that the opt-in reflection request builder uses to create custom return shapes. | None. Read-only [`IList<Type>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1) property; add a closed adapter type or supported open generic definition. Each entry is a [`Type`](https://learn.microsoft.com/dotnet/api/system.type). | Mutable adapter registry, initialized empty. Reflection builds consult it; source generation discovers adapters at compile time. |
| [`IReturnTypeAdapter<TReturn, TResult>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IReturnTypeAdapter.cs) | Defines the contract for converting a deferred HTTP call into the return type exposed by a Refit interface method. | `TReturn`: surfaced wrapper type. `TResult`: deserialized response body type. | Implement [`Adapt`](https://github.com/reactiveui/refit/blob/main/src/Refit/IReturnTypeAdapter.cs) to return the wrapper. |

### Pagination

[Full description and examples](results/pagination.md).

Types: `Refit.PagedEnumerable<TPage, TItem>`, `Refit.PagedEnumerable`, `Refit.PagedAttribute`, `Refit.PageTokenAttribute`, `Refit.PageContinuation`, `Refit.PageContinuation<TToken>` and `Refit.NextLinkOriginPolicy`.

#### Declare a paged method

[Full description and examples](results/pagination.md#read-every-item).

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| [`PagedAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/PagedAttribute.cs) | Marks a method that returns `PagedEnumerable<TPage, TItem>`, so the source generator emits its paging loop. | None. | Method attribute type. |
| [`PagedAttribute.Items`](https://github.com/reactiveui/refit/blob/main/src/Refit/PagedAttribute.cs) | Names the member of the page that holds the items. | Nullable [`string`](https://learn.microsoft.com/dotnet/api/system.string), dotted for nested members. When omitted, the page must have exactly one sequence of the item type. | The member the generator reads; a misspelled name is a build error. |
| [`PagedAttribute.Next`](https://github.com/reactiveui/refit/blob/main/src/Refit/PagedAttribute.cs) | Names the member of the page that holds the next token, offset or link. | Nullable [`string`](https://learn.microsoft.com/dotnet/api/system.string), dotted for nested members. The member must be nullable. | `null` or an empty string ends the sequence. |
| [`PagedAttribute.NextHeader`](https://github.com/reactiveui/refit/blob/main/src/Refit/PagedAttribute.cs) | Names the response header that holds the next token or link. | Nullable [`string`](https://learn.microsoft.com/dotnet/api/system.string). The page type must be `ApiResponse<T>` or `IApiResponse`. | `Link` is read for its `rel="next"` entry; any other header is read as the whole value. |
| [`PagedAttribute.Total`](https://github.com/reactiveui/refit/blob/main/src/Refit/PagedAttribute.cs) | Names the member of the page that holds the total item count. | Nullable [`string`](https://learn.microsoft.com/dotnet/api/system.string). The token parameter must be an [`int`](https://learn.microsoft.com/dotnet/api/system.int32). | The sequence ends when the offset reaches the total or a page is empty. |
| [`PagedAttribute.Origins`](https://github.com/reactiveui/refit/blob/main/src/Refit/PagedAttribute.cs) | Lists the origins a next link may point at. | Nullable [`string[]`](https://learn.microsoft.com/dotnet/api/system.string) of absolute `http` or `https` addresses. | A link to any other origin is refused and never requested. |
| [`PagedAttribute.SameOrigin`](https://github.com/reactiveui/refit/blob/main/src/Refit/PagedAttribute.cs) | Restricts next links to the origin of the client's `BaseAddress`. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean), default `false`. | Exactly one of `Origins`, `SameOrigin` and `AnyOrigin` is required for a method that follows links. |
| [`PagedAttribute.AnyOrigin`](https://github.com/reactiveui/refit/blob/main/src/Refit/PagedAttribute.cs) | Lets next links point at any `http` or `https` origin. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean), default `false`. | The client's credentials go to whatever origin the server names. |
| [`PageTokenAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/PageTokenAttribute.cs) | Marks the parameter that carries the continuation. | None. | Parameter attribute type. The argument is the token of the first page. |

#### Read the sequence

[Full description and examples](results/pagination.md#what-the-sequence-does-for-you).

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| [`PagedEnumerable<TPage, TItem>`](https://github.com/reactiveui/refit/blob/main/src/Refit/PagedEnumerable%7BTPage%2CTItem%7D.cs) | A lazy sequence of items that also exposes the pages. Every enumeration starts again from the first page. | `TPage`: the page type, your DTO or an `ApiResponse<T>` of it. `TItem`: the item type. | Implements [`IAsyncEnumerable<TItem>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.iasyncenumerable-1). |
| `AsPages()` | Exposes the pages as the service returned them. | None. | [`IAsyncEnumerable<TPage>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.iasyncenumerable-1); each page is disposed when you move past it. |
| `WithMaxPages(int pages)` | Bounds the pages fetched. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) `pages`: positive limit. | A new `PagedEnumerable<TPage, TItem>`. Throws [`ArgumentOutOfRangeException`](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception) unless `pages` is positive. |
| `WithPrefetch()` | Requests the next page while you read the current one. | None. | A new `PagedEnumerable<TPage, TItem>` that reads at most one page ahead. |
| `ToObservable()` | Exposes the items as a cold observable. | None. | [`IObservable<TItem>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1); each subscription starts from the first page, and disposing it cancels the request in flight. |
| `ToPageObservable()` | Exposes the pages as a cold observable. | None. | [`IObservable<TPage>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1); a page is disposed after `OnNext` returns. |

#### Write the loop yourself

[Full description and examples](results/pagination.md#write-the-loop-yourself).

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| [`PagedEnumerable.Create<TPage, TItem, TToken>(fetch, items, next)`](https://github.com/reactiveui/refit/blob/main/src/Refit/PagedEnumerable.cs) | Wraps a method that fetches one page for a token. The first page is requested with `default(TToken)`. | `fetch`: [`Func<TToken, CancellationToken, Task<TPage>>`](https://learn.microsoft.com/dotnet/api/system.func-3). `items`: [`Func<TPage, IEnumerable<TItem>?>`](https://learn.microsoft.com/dotnet/api/system.func-2). `next`: `Func<TPage, TToken, PageContinuation<TToken>>`. | A `PagedEnumerable<TPage, TItem>` that requests nothing until it is enumerated. Throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception) for a null delegate. |
| `PagedEnumerable.Create<TPage, TItem, TToken>(first, fetch, items, next)` | The same, starting from a token you supply. | `first`: the token of the first page; the delegates are as above. | A `PagedEnumerable<TPage, TItem>`. |
| `PagedEnumerable.FromCursor<TPage, TItem>(fetch, items, nextCursor)` | Wraps an API whose next page is named by a string cursor. | `fetch`: [`Func<string?, CancellationToken, Task<TPage>>`](https://learn.microsoft.com/dotnet/api/system.func-3). `nextCursor`: [`Func<TPage, string?>`](https://learn.microsoft.com/dotnet/api/system.func-2). | A sequence that ends on a `null` or empty cursor. |
| `PagedEnumerable.FromOffset<TPage, TItem>(fetch, items, nextOffset)` | Wraps an API addressed by the index of the first item, starting at zero. | `fetch`: [`Func<int, CancellationToken, Task<TPage>>`](https://learn.microsoft.com/dotnet/api/system.func-3). `nextOffset`: [`Func<TPage, int, int?>`](https://learn.microsoft.com/dotnet/api/system.func-3). | A sequence that ends when `nextOffset` returns `null`. |
| `PagedEnumerable.FromLinks<TPage, TItem>(originPolicy, fetch, items, nextLink)` | Wraps an API whose reply names the next page with a link. | `originPolicy`: [`NextLinkOriginPolicy`](https://github.com/reactiveui/refit/blob/main/src/Refit/NextLinkOriginPolicy.cs). `fetch`: [`Func<Uri?, CancellationToken, Task<TPage>>`](https://learn.microsoft.com/dotnet/api/system.func-3), called with `null` for the first page. `nextLink`: [`Func<TPage, Uri?>`](https://learn.microsoft.com/dotnet/api/system.func-2). | A sequence that follows only the links the policy allows. A refused link throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) without being requested. |
| [`PageContinuation.To<TToken>(TToken next)`](https://github.com/reactiveui/refit/blob/main/src/Refit/PageContinuation.cs) | Creates a continuation to the next page. | `next`: the token for the next fetch. | A continuation, or the end of the sequence when `next` is `null` or an empty string. |
| [`PageContinuation<TToken>`](https://github.com/reactiveui/refit/blob/main/src/Refit/PageContinuation%7BTToken%7D.cs) | Says whether another page follows and which token requests it. | None. | `HasNext`, `Token` and `End`. |
| [`NextLinkOriginPolicy.Allow(params Uri[] origins)`](https://github.com/reactiveui/refit/blob/main/src/Refit/NextLinkOriginPolicy.cs) | Creates a policy that follows only links whose scheme, host and port match a listed origin. | [`Uri[]`](https://learn.microsoft.com/dotnet/api/system.uri) `origins`: at least one absolute `http` or `https` address. | A policy. Throws [`ArgumentException`](https://learn.microsoft.com/dotnet/api/system.argumentexception) for an empty list or a relative address. |
| `NextLinkOriginPolicy.Unrestricted` | A policy that follows a link to any `http` or `https` origin. | None. | The shared unrestricted policy. |
| `NextLinkOriginPolicy.IsAllowed(Uri link)` | Tests whether a link may be followed. | [`Uri`](https://learn.microsoft.com/dotnet/api/system.uri) `link`: non-null. | `true` for an absolute `http` or `https` link without user information on an allowed origin. |
| [`IApiResponse.GetLink(string relation)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponseExtensions.cs) | Reads the target of the first `Link` response header entry with a relation. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `relation`: such as `next`, compared without regard to case. | [`Uri?`](https://learn.microsoft.com/dotnet/api/system.uri): the target, which may be relative, or `null` when there is none. |

## Serialization

### JSON and generated metadata

[Full description and examples](serialization/json.md).

Types: `Refit.IHttpContentSerializer`, `Refit.ISynchronousContentDeserializer`, `Refit.ISynchronousContentSerializer`, `Refit.SystemTextJsonContentSerializer`.

#### Serializer capabilities

[Full description and examples](serialization/json.md).

| Interface | Description |
| --- | --- |
| [`IHttpContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/IHttpContentSerializer.cs) | Defines the required request-body writer, response-body reader and reflected property-name hook. |
| [`ISynchronousContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/ISynchronousContentSerializer.cs) | Adds synchronous buffered and streamed request-body writers. Refit uses them for `Buffered` and `Streamed` request-body modes. |
| [`ISynchronousContentDeserializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/ISynchronousContentDeserializer.cs) | Adds a reader for an error body that Refit has already buffered as a [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string). |
| [`IStreamingContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/IStreamingContentSerializer.cs) | Adds an incremental response reader for Refit interface methods that return [`IAsyncEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1). |

| API | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`SystemTextJsonContentSerializer()`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates a serializer with Refit's default JSON configuration. | None | Creates and retains a new [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) from `GetDefaultJsonSerializerOptions()`. |
| [`SystemTextJsonContentSerializer(JsonSerializerOptions jsonSerializerOptions)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates a serializer with the supplied JSON configuration. | `jsonSerializerOptions`: [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) that controls JSON conversion and metadata lookup. | Retains and uses the supplied [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) instance. |
| [`SerializerOptions`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Gets the configuration used by this serializer. | None | Returns the same [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) instance passed to the constructor or created by the default constructor. |
| [`GetDefaultJsonSerializerOptions()`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates Refit's general-purpose JSON configuration. | None | Returns a fresh mutable [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) with camel-case names, case-insensitive matching, string-number reading, and Refit's object and enum converters. |
| [`GetFastPathJsonSerializerOptions()`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates options that can use System.Text.Json's source-generated serialization fast path after you assign generated metadata. | None | Returns a fresh mutable [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) with camel-case names and case-insensitive matching, without Refit converters or custom number handling. |
| [`ToHttpContent<T>(T item)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Serializes a request value through Refit's normal asynchronous JSON-content path. | `item`: `T`, the request value to serialize. | Returns JSON [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent). It uses configured generated metadata when available; an interface or abstract `T` without polymorphism configuration uses the non-null value's runtime type. |
| [`ToHttpContentSynchronous<T>(T item)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Serializes a request value immediately into a buffered JSON body. | `item`: `T`, the request value to serialize. | Returns UTF-8 JSON [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) with a `ByteArrayContent` body and `application/json; charset=utf-8` content type. |
| [`ToStreamingHttpContent<T>(T item)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates a request body that serializes a value when the HTTP request sends it. | `item`: `T`, the request value to serialize. | Returns [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) that writes UTF-8 JSON to the request stream with `application/json; charset=utf-8` content type. |
| [`FromHttpContentAsync<T>(HttpContent content, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Reads a JSON HTTP body as a value. | `content`: [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent), the response body; `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken) that cancels the read. Default: `default`. | Returns [`Task<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1) for the deserialized value. |
| [`DeserializeFromString<T>(string content)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Reads an already buffered JSON string. | `content`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), the JSON text. | Returns `T?`, the deserialized value. Invalid JSON throws [`JsonException`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonexception). |
| [`DeserializeStreamAsync<T>(Stream stream, StreamingContentFormat format, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Reads one JSON value at a time from a framed response stream. | `stream`: [`Stream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream), the response body; `format`: [`StreamingContentFormat`](serialization/content.md), its JSON array, JSON Lines, or SSE framing; `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken) that cancels enumeration. Default: `default`. | Returns [`IAsyncEnumerable<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1) that yields values as they arrive. See [streaming replies](results/streaming.md). |
| [`GetFieldNameForProperty(PropertyInfo propertyInfo)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Finds a property's explicit JSON field name for reflected integrations. | `propertyInfo`: [`PropertyInfo`](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo), the property to inspect. | Returns the [`JsonPropertyNameAttribute`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute) name, or `null` when the property has no such attribute. |

#### Defaults and fast-path writers

[Full description and examples](serialization/json.md).

| Condition | What to do |
| --- | --- |
| Generated writer exists | Use `Default` or `Serialization` generation mode. Keep metadata too when you read replies. |
| No custom converters | Avoid entries in `JsonSerializerOptions.Converters` and [`JsonConverter`](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter) attributes on the model or its members. |
| Compatible options | Keep naming, ignored-member and null-handling options aligned with the generated context. |
| Supported features | Avoid custom encoders, dictionary key policies and reference handling for this path. |
| Supported number writing | Avoid number handling that changes JSON output, such as `WriteAsString`. `AllowReadingFromString` alone does not block writing. |

### Newtonsoft.Json content

[Full description and examples](serialization/newtonsoft-json.md).

Types: `Refit.NewtonsoftJsonContentSerializer`.

#### Method reference

[Full description and examples](serialization/newtonsoft-json.md).

| API | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`NewtonsoftJsonContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) | Implements Refit's [`IHttpContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/IHttpContentSerializer.cs) with Newtonsoft.Json. It also implements [`ISynchronousContentDeserializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/ISynchronousContentDeserializer.cs). | No public properties. | Creates JSON request content, reads JSON response content, exposes buffered string deserialization, and maps explicit JSON property names for Refit. |
| [`NewtonsoftJsonContentSerializer()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) | Creates a serializer with lazily resolved default settings. | None. | Returns [`NewtonsoftJsonContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs). The default path invokes [`JsonConvert.DefaultSettings`](https://www.newtonsoft.com/json/help/html/P_Newtonsoft_Json_JsonConvert_DefaultSettings.htm), creates [`JsonSerializerSettings`](https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_JsonSerializerSettings.htm) when needed, and forces [`TypeNameHandling.None`](https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_TypeNameHandling.htm). |
| [`NewtonsoftJsonContentSerializer(JsonSerializerSettings? jsonSerializerSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) | Creates a serializer with caller-supplied Newtonsoft.Json settings. | `jsonSerializerSettings`: nullable [`JsonSerializerSettings`](https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_JsonSerializerSettings.htm); `null` selects the default-settings path. | Returns [`NewtonsoftJsonContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) and retains a non-null settings object as supplied. |
| [`ToHttpContent<T>(T item)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) | Serializes a value to JSON request content. | `item`: value of generic type `T` to serialize. | Returns [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) containing UTF-8 JSON with media type `application/json`. |
| [`FromHttpContentAsync<T>(HttpContent content, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) | Buffers and deserializes HTTP response content asynchronously. | `content`: [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) to read; `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken), default [`CancellationToken.None`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.none). | Returns [`Task<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1). A null content value returns `default`; otherwise the method reads the content using its charset or UTF-8, deserializes it, and disposes the read stream. |
| [`DeserializeFromString<T>(string content)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) | Deserializes an already buffered JSON string synchronously. | `content`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) containing JSON. | Returns nullable generic `T?` from [`JsonConvert.DeserializeObject<T>`](https://www.newtonsoft.com/json/help/html/M_Newtonsoft_Json_JsonConvert_DeserializeObject__1.htm). Newtonsoft.Json exceptions can propagate for invalid JSON. |
| [`GetFieldNameForProperty(PropertyInfo propertyInfo)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) | Finds the JSON field name that an object property declares explicitly. | `propertyInfo`: [`PropertyInfo`](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) to inspect. | Returns the [`JsonPropertyAttribute.PropertyName`](https://www.newtonsoft.com/json/help/html/P_Newtonsoft_Json_JsonPropertyAttribute_PropertyName.htm), or `null` when the property has no `JsonPropertyAttribute`. Throws [`ArgumentNullException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception) when `propertyInfo` is null. |

### XML content

[Full description and examples](serialization/xml.md).

Types: `Refit.XmlContentSerializer`, `Refit.XmlContentSerializerSettings`, `Refit.XmlReaderWriterSettings`.

#### Settings reference

[Full description and examples](serialization/xml.md).

| `XmlContentSerializerSettings` member | Description | Default and purpose |
| --- | --- | --- |
| [`XmlContentSerializerSettings()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializerSettings.cs) | Creates settings for XML request and response serialization. | `XmlDefaultNamespace` is `null`; reader/writer settings are new; namespaces contain one empty-prefix/empty-namespace mapping; attribute overrides are empty. |
| [`XmlDefaultNamespace`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializerSettings.cs) | [`string?`](https://learn.microsoft.com/en-us/dotnet/api/system.string); the default XML namespace passed when constructing a serializer for deserialization. | `null` means no default namespace. The value is used when the type's serializer is first cached for reading. |
| [`XmlReaderWriterSettings`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializerSettings.cs) | [`XmlReaderWriterSettings`](serialization/xml.md); the paired reader and writer configuration. | Defaults to a new instance. Accessing its reader or writer applies asynchronous operation and safe DTD settings. |
| [`XmlNamespaces`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializerSettings.cs) | [`XmlSerializerNamespaces`](https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializernamespaces); namespace prefixes and URIs supplied to `XmlSerializer.Serialize`. | Defaults to one empty-prefix/empty-namespace mapping. |
| [`XmlAttributeOverrides`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializerSettings.cs) | [`XmlAttributeOverrides`](https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlattributeoverrides); alternate XML mappings for model types. | Defaults to an empty collection. Overrides are read when a type's cached `XmlSerializer` is created. |

| `XmlReaderWriterSettings` member | Description | Behavior |
| --- | --- | --- |
| [`XmlReaderWriterSettings()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlReaderWriterSettings.cs) | Creates paired XML reader and writer settings. | Both settings are new defaults. |
| [`XmlReaderWriterSettings(XmlReaderSettings readerSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlReaderWriterSettings.cs) | Takes reader settings and creates the writer settings. | Retains `readerSettings`; the writer settings are new defaults. A null argument throws `ArgumentNullException`. |
| [`XmlReaderWriterSettings(XmlWriterSettings writerSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlReaderWriterSettings.cs) | Takes writer settings and creates the reader settings. | Retains `writerSettings`; the reader settings are new defaults. A null argument throws `ArgumentNullException`. |
| [`XmlReaderWriterSettings(XmlReaderSettings readerSettings, XmlWriterSettings writerSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlReaderWriterSettings.cs) | Takes both caller-supplied settings. | Retains both objects. Either null argument throws `ArgumentNullException`. |
| [`ReaderSettings`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlReaderWriterSettings.cs) | [`XmlReaderSettings`](https://learn.microsoft.com/en-us/dotnet/api/system.xml.xmlreadersettings); gets or replaces the reader settings. | Assignment rejects `null`. Getting the value sets `Async = true`; unless `AllowDtdProcessing` is enabled, it also sets `DtdProcessing.Prohibit` and clears `XmlResolver`. |
| [`WriterSettings`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlReaderWriterSettings.cs) | [`XmlWriterSettings`](https://learn.microsoft.com/en-us/dotnet/api/system.xml.xmlwritersettings); gets or replaces the writer settings. | Assignment rejects `null`. Getting the value sets `Async = true`. |
| [`AllowDtdProcessing`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlReaderWriterSettings.cs) | [`bool`](https://learn.microsoft.com/en-us/dotnet/api/system.boolean); compatibility opt-out from Refit's DTD hardening. | Defaults to `false` and is obsolete. Setting it to `true` leaves caller-configured DTD processing and resolver settings in place. |

#### Method reference

[Full description and examples](serialization/xml.md).

| `XmlContentSerializer` member | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`XmlContentSerializer()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializer.cs) | Creates an XML content serializer with default settings. | None | Uses a new [`XmlContentSerializerSettings`](serialization/xml.md). |
| [`XmlContentSerializer(XmlContentSerializerSettings settings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializer.cs) | Creates an XML content serializer with caller-supplied settings. | `settings`: non-null [`XmlContentSerializerSettings`](serialization/xml.md) | Stores the settings; `null` throws [`ArgumentNullException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception). |
| [`ToHttpContent<T>(T item)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializer.cs) | Serializes a value for an XML HTTP request. | `item`: value to serialize | Returns [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) with media type `application/xml` and the configured writer charset. `null` throws [`ArgumentNullException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception). The runtime type selects the cached `XmlSerializer`. |
| [`FromHttpContentAsync<T>(HttpContent content, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializer.cs) | Reads and deserializes an XML HTTP response. | `content`: [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) to read; `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken), default `default` | Returns [`Task<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1). It buffers the content as a string, then parses it synchronously with the serializer for `T`; cancellation applies while reading the content. |
| [`DeserializeFromString<T>(string content)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializer.cs) | Deserializes buffered XML text. | `content`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) containing XML | Returns `T?` parsed with the configured reader, default namespace, and attribute overrides. |
| [`GetFieldNameForProperty(PropertyInfo propertyInfo)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializer.cs) | Finds the XML field name declared on a property. | `propertyInfo`: [`PropertyInfo`](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) to inspect | Returns the `ElementName` from an [`XmlElementAttribute`](https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlelementattribute), otherwise the `AttributeName` from an [`XmlAttributeAttribute`](https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlattributeattribute), otherwise `null`. A null property throws [`ArgumentNullException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception). |

### Content writers and stream readers

[Full description and examples](serialization/content.md).

Types: `Refit.IStreamingContentSerializer`, `Refit.JsonContentSerializer`, `Refit.JsonLinesContent`, `Refit.ObjectToInferredTypesConverter`, `Refit.StreamingContentFormat`.

#### Read a stream directly

[Full description and examples](serialization/content.md).

| Format | Body framing |
| --- | --- |
| [`JsonArray = 0`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamingContentFormat.cs) | One top-level JSON array; each array element is yielded. |
| [`JsonLines = 1`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamingContentFormat.cs) | JSON values separated by whitespace on .NET 9 and later. |
| [`ServerSentEvents = 2`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamingContentFormat.cs) | An SSE stream; each event's `data` payload is deserialized as JSON. |

#### Infer values stored as object

[Full description and examples](serialization/content.md).

| JSON token | Result |
| --- | --- |
| `true` or `false` | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) |
| Number representable as Int64 | [`long`](https://learn.microsoft.com/dotnet/api/system.int64) |
| Other number | [`double`](https://learn.microsoft.com/dotnet/api/system.double) |
| String parseable as DateTime | `DateTime` |
| Other string | [`string`](https://learn.microsoft.com/dotnet/api/system.string) |
| Object, array or a directly read null token | A detached [`JsonElement`](https://learn.microsoft.com/dotnet/api/system.text.json.jsonelement) |

#### API reference

[Full description and examples](serialization/content.md).

| API | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`StreamingContentFormat`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamingContentFormat.cs) | Names the framing used by a streaming content serializer. | None | An enum with `JsonArray`, `JsonLines`, and `ServerSentEvents` values. |
| [`StreamingContentFormat.JsonArray = 0`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamingContentFormat.cs) | Selects one top-level JSON array. | None | Each array element is yielded as one `T` value. |
| [`StreamingContentFormat.JsonLines = 1`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamingContentFormat.cs) | Selects newline-delimited JSON values. | None | Each JSON value is yielded as one `T` value. |
| [`StreamingContentFormat.ServerSentEvents = 2`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamingContentFormat.cs) | Selects server-sent events. | None | Each event's `data` field is deserialized and yielded as one `T` value. |
| [`IStreamingContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/IStreamingContentSerializer.cs) | Defines the optional capability to deserialize response bodies incrementally. | None | Implement this interface when a serializer can produce an [`IAsyncEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1) without buffering the complete body. |
| [`JsonContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/JsonContentSerializer.cs) *(obsolete)* | Names the obsolete JSON serializer retained for binary compatibility. | None | A public class implementing [`IHttpContentSerializer`](serialization/json.md); its compiler error prevents direct use. |
| [`JsonLinesContent`](https://github.com/reactiveui/refit/blob/main/src/Refit/JsonLinesContent.cs) | Represents an HTTP body that writes one serialized value per JSON Lines record. | None | A sealed [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) implementation. |
| [`ObjectToInferredTypesConverter`](https://github.com/reactiveui/refit/blob/main/src/Refit/ObjectToInferredTypesConverter.cs) | Infers CLR values when System.Text.Json deserializes a value declared as `object`. | None | A [`JsonConverter<object>`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonconverter-1). |
| [`JsonLinesContent(IEnumerable items, IHttpContentSerializer serializer)`](https://github.com/reactiveui/refit/blob/main/src/Refit/JsonLinesContent.cs) | Creates HTTP content that serializes each item as one JSON Lines record. | `items`: [`IEnumerable`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable) values to write; `serializer`: [`IHttpContentSerializer`](serialization/json.md) for each value | Creates [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent). Throws [`ArgumentNullException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception) for either argument. |
| [`JsonLinesContent.JsonLinesMediaType`](https://github.com/reactiveui/refit/blob/main/src/Refit/JsonLinesContent.cs) | Identifies the media type emitted by JSON Lines content. | None | Returns [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) `application/x-ndjson`. |
| [`JsonLinesContent.SerializeToStreamAsync(Stream stream, TransportContext? context)`](https://github.com/reactiveui/refit/blob/main/src/Refit/JsonLinesContent.cs) *(protected override)* | Serializes each item to the destination stream as one newline-delimited JSON record. | `stream`: [`Stream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream) destination; `context`: unused [`TransportContext`](https://learn.microsoft.com/en-us/dotnet/api/system.net.transportcontext) | Returns [`Task`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task) and writes each serialized value with LF separators and no trailing LF. |
| [`JsonLinesContent.TryComputeLength(out long length)`](https://github.com/reactiveui/refit/blob/main/src/Refit/JsonLinesContent.cs) *(protected override)* | Reports whether the JSON Lines content has a known byte length. | `length`: [`long`](https://learn.microsoft.com/en-us/dotnet/api/system.int64) receives `-1` | Returns [`bool`](https://learn.microsoft.com/en-us/dotnet/api/system.boolean) `false`; the content has no advertised length. |
| [`IStreamingContentSerializer.DeserializeStreamAsync<T>(Stream stream, StreamingContentFormat format, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IStreamingContentSerializer.cs) | Reads a response stream according to its framing format and yields deserialized values as they arrive. | `stream`: [`Stream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream) source; `format`: [`StreamingContentFormat`](serialization/content.md) framing; `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken) used to cancel enumeration | Returns [`IAsyncEnumerable<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1). Malformed data or missing metadata fails during enumeration. |
| [`JsonContentSerializer()`](https://github.com/reactiveui/refit/blob/main/src/Refit/JsonContentSerializer.cs) *(obsolete)* | Represents the obsolete JSON serializer compatibility type. | None | Constructs the compatibility type, but direct use is a compiler error because the type is obsolete with `error: true`. |
| [`JsonContentSerializer.ToHttpContent<T>(T item)`](https://github.com/reactiveui/refit/blob/main/src/Refit/JsonContentSerializer.cs) *(obsolete)* | Attempts to serialize `item` into HTTP content. | `item`: generic value to serialize | Returns [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) in the signature, but always throws [`NotSupportedException`](https://learn.microsoft.com/en-us/dotnet/api/system.notsupportedexception). |
| [`JsonContentSerializer.FromHttpContentAsync<T>(HttpContent content, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit/JsonContentSerializer.cs) *(obsolete)* | Attempts to deserialize `content` as `T`. | `content`: [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) source; `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken) cancellation | Returns [`Task<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1) in the signature, but always throws [`NotSupportedException`](https://learn.microsoft.com/en-us/dotnet/api/system.notsupportedexception). |
| [`JsonContentSerializer.GetFieldNameForProperty(PropertyInfo propertyInfo)`](https://github.com/reactiveui/refit/blob/main/src/Refit/JsonContentSerializer.cs) *(obsolete)* | Attempts to calculate a serialized field name for a reflected property. | `propertyInfo`: [`PropertyInfo`](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) to inspect | Returns [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) in the signature, but always throws [`NotSupportedException`](https://learn.microsoft.com/en-us/dotnet/api/system.notsupportedexception). |
| [`ObjectToInferredTypesConverter()`](https://github.com/reactiveui/refit/blob/main/src/Refit/ObjectToInferredTypesConverter.cs) | Creates the converter used to infer CLR values when deserializing `object`. | None | Creates a [`JsonConverter<object>`](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter-1). |
| [`ObjectToInferredTypesConverter.Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ObjectToInferredTypesConverter.cs) | Reads one JSON token and converts it to the appropriate CLR value. | [`Utf8JsonReader`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.utf8jsonreader), [`Type`](https://learn.microsoft.com/en-us/dotnet/api/system.type), [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) | Returns nullable [`object`](https://learn.microsoft.com/en-us/dotnet/api/system.object), inferring scalar CLR types and retaining objects/arrays as [`JsonElement`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonelement). |
| [`ObjectToInferredTypesConverter.Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ObjectToInferredTypesConverter.cs) | Writes the supplied value as JSON using its runtime type. | [`Utf8JsonWriter`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.utf8jsonwriter), [`object`](https://learn.microsoft.com/en-us/dotnet/api/system.object), [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) | Returns [`void`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void), writes the runtime type, with a bare object represented as `{}`. |
| [`SystemTextJsonContentSerializer.DeserializeStreamAsync<T>(Stream stream, StreamingContentFormat format, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Selects the JSON array, JSON Lines or server-sent-events reader for a response stream. | `stream`: [`Stream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream) source; `format`: [`StreamingContentFormat`](serialization/content.md) framing; `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken) used to cancel reads | Returns [`IAsyncEnumerable<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1) using the selected framing. An unrecognized format uses the JSON-array reader. |


## Testing

### Test a Refit client

[Full description and examples](testing/index.md).

Types: `Refit.Testing.StubHttp`.

#### API reference

[Full description and examples](testing/index.md).

| API | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`StubHttp`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Declarative `HttpMessageHandler` for Refit tests; stores route matchers and their replies, records requests, and supports one-shot, reusable and fallback routes. | None | Handler type implementing [`IEnumerable<RouteMatcher>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1). |
| [`StubHttp()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Starts a handler with no expected routes. | None | Creates an empty route table using the default JSON content serializer. |
| [`StubHttp(NetworkBehavior behavior)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Starts an empty handler and enables network-fault simulation. | `behavior`: [`NetworkBehavior`](testing/faults.md) applied to each matched request | Creates an empty route table with the supplied behavior. |
| [`StubHttp.Requests`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Exposes requests received by the handler in arrival order. | Get-only [`IReadOnlyList<HttpRequestMessage>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1) | Returns a live read-only view, including unmatched and failed requests. |
| [`StubHttp.Behavior`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Enables, replaces or disables simulated network conditions. | Nullable [`NetworkBehavior`](testing/faults.md), get/set; default `null` | Gets or sets behavior; `null` skips simulation. |
| [`StubHttp.Add(RouteMatcher route, StubResponse response)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Adds a route and the reply returned when it matches; collection initializers call this method. | `route`: [`RouteMatcher`](testing/routes.md); `response`: [`StubResponse`](testing/replies.md) | Returns [`void`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void); rejects null arguments and tracks one-shot expectations. |
| [`StubHttp.ToSettings()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Creates settings that route a Refit client through this handler. | None | Returns new [`RefitSettings`](clients/settings.md) whose handler factory returns this handler. |
| [`StubHttp.ToSettings(RefitSettings baseSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Reuses supplied settings and points them at this handler. | `baseSettings`: [`RefitSettings`](clients/settings.md) to update | Returns the same settings after replacing its handler factory and adopting its serializer. |
| [`StubHttp.CreateClient<T>(string hostUrl)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Creates a reflection-based Refit client using default settings. | `hostUrl`: base address | Returns `T` from `RestService.For<T>`; carries runtime reflection/trimming requirements. |
| [`StubHttp.CreateClient<T>(string hostUrl, RefitSettings baseSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Creates a reflection-based client while retaining supplied serializer and URL settings. | `hostUrl`: base address; `baseSettings`: [`RefitSettings`](clients/settings.md) to route through this handler | Returns `T` from `RestService.For<T>` after rewiring the supplied settings. |
| [`StubHttp.CreateGeneratedClient<T>(string hostUrl)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Creates a source-generated Refit client using default settings. | `hostUrl`: base address | Returns generated client `T`; throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) when no generated implementation is registered. |
| [`StubHttp.CreateGeneratedClient<T>(string hostUrl, RefitSettings baseSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Creates a source-generated client while retaining supplied settings. | `hostUrl`: base address; `baseSettings`: [`RefitSettings`](clients/settings.md) to route through this handler | Returns generated client `T`; throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) when no generated implementation is registered. |
| [`StubHttp.SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) *(protected override)* | Records, matches and consumes an incoming request, applies network behavior, then builds its configured reply. | `request`: [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage); `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken) | Returns [`Task<HttpResponseMessage>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1); throws when no route matches or cancellation is requested. |

### Match outgoing requests

[Full description and examples](testing/routes.md).

Types: `Refit.Testing.Route`, `Refit.Testing.RouteMatcher`.

#### Check queries, headers and bodies

[Full description and examples](testing/routes.md).

| Property | Required request behavior |
| --- | --- |
| `Method` | Same HTTP method. Null accepts any method. |
| `Query` | Contains each decoded key/value pair. Extra pairs are allowed. |
| `ExactQuery` | Same decoded pairs and count as the supplied encoded query, ignoring order. Omit the leading `?`. |
| `ExactQueryParams` | Same decoded pairs and count as the supplied array, ignoring order. |
| `Headers` | Contains each name/value pair in request or content headers. Names use HTTP header lookup. Values compare exactly. Multiple values join with `", "`. |
| `Body` | Exact body text. Missing content counts as an empty string. |
| `FormData` | Contains each decoded form pair. Extra pairs are allowed. The media type is not checked. |
| `Where` | The synchronous predicate returns true. |
| `WhereAsync` | The asynchronous predicate returns true. It runs after `Where` passes. |

#### API reference

[Full description and examples](testing/routes.md).

| API | Description | Parameters or value | Returns and behavior |
| --- | --- | --- | --- |
| [`Route`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Provides static factories for common request matchers. | Static class; do not create an instance. | Each factory returns a configured [`RouteMatcher`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs). |
| [`Route.Any(string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a path regardless of its HTTP method. | `template`: a relative or absolute path template; a complete `{name}` segment matches one path segment. | Returns a [`RouteMatcher`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) with no method restriction. |
| [`Route.Get(string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a `GET` request for a path. | `template`: the relative or absolute path template to match. | Returns a matcher whose method is `GET`. |
| [`Route.Post(string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a `POST` request for a path. | `template`: the relative or absolute path template to match. | Returns a matcher whose method is `POST`. |
| [`Route.Put(string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a `PUT` request for a path. | `template`: the relative or absolute path template to match. | Returns a matcher whose method is `PUT`. |
| [`Route.Delete(string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a `DELETE` request for a path. | `template`: the relative or absolute path template to match. | Returns a matcher whose method is `DELETE`. |
| [`Route.Patch(string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a `PATCH` request for a path. | `template`: the relative or absolute path template to match. | Returns a matcher whose method is `PATCH`. |
| [`Route.Head(string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a `HEAD` request for a path. | `template`: the relative or absolute path template to match. | Returns a matcher whose method is `HEAD`. |
| [`Route.For(HttpMethod method, string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a path for an HTTP method that has no convenience factory, such as `OPTIONS`. | `method`: the [`HttpMethod`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpmethod) to require; `template`: the relative or absolute path template to match. | Returns a matcher for the supplied method and template. |
| [`Route.Fallback()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Creates a catch-all route tried after every one-shot and reusable route. | None. | Returns a matcher with `Template` set to `"*"` and `Fallback` set to `true`; it may match repeatedly. |
| [`RouteMatcher`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Describes the request that a route table entry accepts. | Set `Template` and any init-only conditions in an object initializer. | A configured matcher is paired with a `Reply` in [`StubHttp`](testing/index.md#make-your-first-test). |
| [`RouteMatcher()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Creates a matcher for custom conditions. | None. | Returns a matcher with optional conditions unset. Set its required `Template` before it is added to a route table. |
| [`RouteMatcher.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Restricts a matcher to one HTTP method. | Init-only [`HttpMethod?`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpmethod); `null` is the default. | A non-null value must equal the request method. `null` accepts every method. |
| [`RouteMatcher.Template`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Supplies the path pattern every matcher needs. | Required init-only [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string): a relative or absolute path, or `"*"` for every path. | The handler matches this template against the request URI. |
| [`RouteMatcher.Query`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Requires selected decoded query pairs. | Init-only nullable array of `(string Key, string Value)` pairs to find. | Every supplied pair must occur; the request may contain other pairs. |
| [`RouteMatcher.ExactQuery`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Requires the complete decoded query from encoded text. | Init-only nullable [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) without a leading `?`. | Requires the same decoded pair count and members, ignoring order. |
| [`RouteMatcher.ExactQueryParams`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Requires the complete decoded query from named pairs. | Init-only nullable array of `(string Key, string Value)` pairs. | Requires the same pair count and members, ignoring order. |
| [`RouteMatcher.Headers`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Requires selected request or content headers. | Init-only nullable array of `(string Name, string Value)` pairs. | Every supplied header name and value must occur. |
| [`RouteMatcher.Body`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Requires an exact text request body. | Init-only nullable [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) containing the expected body. | The request body must equal the value. Missing content is an empty string. |
| [`RouteMatcher.FormData`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Requires selected decoded form fields. | Init-only nullable array of `(string Key, string Value)` pairs to find in the body. | Every supplied form pair must occur; extra pairs and the media type are ignored. |
| [`RouteMatcher.Where`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Adds a synchronous check for details the built-in properties do not cover. | Init-only nullable [`Func<HttpRequestMessage, bool>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-2); its [`HttpRequestMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httprequestmessage) argument is the request being matched. | The route matches only when the predicate returns `true`. |
| [`RouteMatcher.WhereAsync`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Adds an asynchronous check, such as one that reads the request body. | Init-only nullable [`Func<HttpRequestMessage, Task<bool>>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-2); use [`Task`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task) to return the result. | The route matches only when the task completes with `true`, after `Where` passes. |
| [`RouteMatcher.Reusable`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Makes a route available for repeated background behavior. | Init-only [`bool`](https://learn.microsoft.com/en-us/dotnet/api/system.boolean); default `false`. | `true` allows repeated matches and excludes the route from `VerifyAllCalled`. |
| [`RouteMatcher.Fallback`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Makes a route the final match attempt. | Init-only [`bool`](https://learn.microsoft.com/en-us/dotnet/api/system.boolean); default `false`. | `true` gives the route fallback priority, allows repeated matches, and excludes it from `VerifyAllCalled`. |
| [`StubHttp.GetEnumerator()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) *(explicit `IEnumerable<RouteMatcher>`)* | Lets you enumerate configured matchers as [`RouteMatcher`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) values. | None; cast `StubHttp` to [`IEnumerable<RouteMatcher>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) to call it. | Returns an [`IEnumerator<RouteMatcher>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerator-1) over a snapshot of the route table. |
| [`StubHttp.GetEnumerator()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) *(explicit `IEnumerable`)* | Lets non-generic code enumerate the configured matchers. | None; cast `StubHttp` to [`IEnumerable`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable) to call it. | Returns a non-generic [`IEnumerator`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerator) over the same route snapshot. |

### Supply test replies

[Full description and examples](testing/replies.md).

Types: `Refit.Testing.Reply`, `Refit.Testing.StubResponse`.

#### JSON, text and custom content

[Full description and examples](testing/replies.md).

| Method | Body and status |
| --- | --- |
| `With<T>(body)` / `With<T>(body, status)` | Serialize the typed body with the adopted serializer. Status 200 or your supplied status. |
| `Json(body)` / `Json(body, status)` | UTF-8 text with `application/json`. Status 200 or your supplied status. JSON validity is not checked. |
| `Text(body)` / `Text(body, contentType)` | UTF-8 text with `text/plain` or your supplied media type. Status 200. |
| `Status(statusCode)` | The supplied status with no explicit body. |
| `Content(body)` | The exact [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) object, with status 200. |
| `From(responder)` | Your lambda returns the complete response. Both sync and async overloads receive the request. |

#### API reference

[Full description and examples](testing/replies.md).

| API | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`Reply.With<T>(T body)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a typed successful reply using the handler's serializer. | `body`: generic type `T` | Returns [`StubResponse`](testing/replies.md) that serializes the body with the handler serializer and uses [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode). |
| [`Reply.With<T>(T body, HttpStatusCode status)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a typed reply while choosing a non-default status. | `body`: generic type `T`; `status`: [`HttpStatusCode`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode) | Returns [`StubResponse`](testing/replies.md) with serialized content and the supplied status. |
| [`Reply.Json(string body)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a successful reply from raw JSON text. | `body`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) JSON text | Returns [`StubResponse`](testing/replies.md) with UTF-8 `application/json` content and status [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode). |
| [`Reply.Json(string body, HttpStatusCode status)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a raw JSON reply with a caller-selected status. | `body`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) JSON text; `status`: [`HttpStatusCode`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode) | Returns JSON [`StubResponse`](testing/replies.md) with the supplied status. |
| [`Reply.Text(string body)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a plain-text successful reply. | `body`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) text | Returns [`StubResponse`](testing/replies.md) with UTF-8 `text/plain` content and status [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode). |
| [`Reply.Text(string body, string contentType)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates text content with a custom media type. | `body`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) text; `contentType`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) media type | Returns UTF-8 text [`StubResponse`](testing/replies.md) with the supplied media type and status [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode). |
| [`Reply.Status(HttpStatusCode statusCode)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a bodyless reply for a chosen status code. | `statusCode`: [`HttpStatusCode`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode) response status | Returns a [`StubResponse`](testing/replies.md). |
| [`Reply.Content(HttpContent body)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Reuses an explicit HTTP content instance as a reply body. | `body`: [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) exact content instance | Returns [`StubResponse`](testing/replies.md) using that content and status [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode). |
| [`Reply.From(Func<HttpRequestMessage, HttpResponseMessage> responder)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Uses a synchronous request-aware factory to build the whole reply. | `responder`: [`Func<HttpRequestMessage, HttpResponseMessage>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-2) request-to-response function | Returns [`StubResponse`](testing/replies.md) whose responder supplies the complete [`HttpResponseMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage). |
| [`Reply.From(Func<HttpRequestMessage, Task<HttpResponseMessage>> responder)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Uses an asynchronous request-aware factory to build the whole reply. | `responder`: [`Func<HttpRequestMessage, Task<HttpResponseMessage>>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-2), with [`Task<TResult>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1) result | Returns [`StubResponse`](testing/replies.md) whose async responder supplies the complete [`HttpResponseMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage). |
| [`StubResponse()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Creates an empty response description that you can configure with init properties. | None | Creates a [`StubResponse`](testing/replies.md) with [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode). |
| [`StubResponse.Status`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Chooses the status for a property-based reply. | [`HttpStatusCode`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode), init-only; default [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode) | Sets the response status unless a responder supplies the complete response. |
| [`StubResponse.Json`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies the raw JSON alternative to a typed or explicit body. | Nullable [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), init-only; default `null` | Supplies raw JSON text. |
| [`StubResponse.Text`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies a raw text alternative to a typed or explicit body. | Nullable [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), init-only; default `null` | Supplies raw text. |
| [`StubResponse.ContentType`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Chooses the media type used when `Text` supplies the body. | Nullable [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), init-only; default `null` | Sets media type for `Text`; it does not alter JSON or explicit content. |
| [`StubResponse.Content`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies an exact content object in preference to text and JSON. | Nullable [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent), init-only; default `null` | Supplies exact content and takes precedence over JSON/text bodies. |
| [`StubResponse.Responder`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies the whole response through a synchronous callback. | Nullable [`Func<HttpRequestMessage, HttpResponseMessage>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-2), init-only; default `null` | Supplies a complete [`HttpResponseMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage) synchronously. |
| [`StubResponse.ResponderAsync`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies the whole response through an asynchronous callback. | Nullable [`Func<HttpRequestMessage, Task<HttpResponseMessage>>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-2), with [`Task<TResult>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1) result; init-only; default `null` | Supplies a complete [`HttpResponseMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage) asynchronously and takes precedence over `Responder`. |

### Inspect requests and verify expectations

[Full description and examples](testing/verification.md).

#### Verification API reference

[Full description and examples](testing/verification.md).

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| [`VerifyAllCalled()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Checks immediately that every one-shot route has been consumed. | None. | `void`; throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) immediately if a one-shot expectation is missing. |
| [`VerifyAllCalledAsync()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Waits for one-shot routes using the handler's default one-second timeout. | None. | [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task): completes when all expectations are consumed, or faults with the missing-route error after one second. |
| [`VerifyAllCalledAsync(TimeSpan timeout)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Waits for one-shot routes using a caller-selected timeout. | [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan) `timeout`: maximum wait; zero checks immediately. | [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task): completes when expectations are consumed, or faults with the missing-route error after the timeout. See the completed-verification limitation below. |
| [`LastRequestBodyAsync<T>()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Deserializes the most recently captured request body as `T` with the adopted serializer. | None. | [`Task<T?>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1): latest captured body deserialized as `T`, or `default` for absent or unbufferable content. Throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) if there are no requests. |
| [`RequestBodyAsync<T>(int index)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Deserializes the captured body at a recorded request position with the adopted serializer. | [int](https://learn.microsoft.com/dotnet/api/system.int32) `index`: zero-based request position. | [`Task<T?>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1): selected captured body deserialized as `T`, or `default` for absent or unbufferable content. Throws [`ArgumentOutOfRangeException`](https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception) for an invalid index. |

| Property | Type | Value |
| --- | --- | --- |
| [`Requests`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | [`IReadOnlyList<HttpRequestMessage>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1) | Get-only live list of recorded [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) objects in arrival order, including unmatched requests and failed sends. |

### Simulate network faults

[Full description and examples](testing/faults.md).

Types: `Refit.Testing.NetworkBehavior`.

#### Defaults and calculation methods

[Full description and examples](testing/faults.md).

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NetworkBehavior()` | Creates deterministic fault simulation with the standard seed and defaults. | None. | A behavior with random seed `0` and the defaults below. |
| `NetworkBehavior(int seed)` | Creates fault simulation whose random sequence starts from your chosen seed. | [int](https://learn.microsoft.com/dotnet/api/system.int32) `seed`: random sequence seed. | A behavior with the supplied seed and the defaults below. The same ordered calls repeat within a runtime. |
| `NextDelay()` | Draws the delay that the next simulated request would use. | None. | [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan): next varied delay. The multiplier is clamped at zero. |
| `NextIsFailure()` | Draws whether the next simulation produces a connection failure. | None. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean): next trial against `FailurePercent`. |
| `NextIsError()` | Draws whether the next simulation produces an HTTP error response. | None. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean): next trial against `ErrorPercent`. |
| `CreateFailure()` | Builds the configured connection exception without throwing it. | None. | [Exception](https://learn.microsoft.com/dotnet/api/system.exception): result of `FailureFactory()`. Creates the exception without throwing it. |
| `CreateErrorResponse()` | Builds a disposable HTTP error reply from the configured status code. | None. | [HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage): fresh response with the configured status and an empty text body. The caller must dispose it. |

| Property | Type | Default and behavior |
| --- | --- | --- |
| `Delay` | [TimeSpan](https://learn.microsoft.com/dotnet/api/system.timespan) | Two seconds. Base delay for simulation. |
| `Variance` | [double](https://learn.microsoft.com/dotnet/api/system.double) | `0.4`. Fraction above and below the delay. Zero fixes the delay. |
| `FailurePercent` | [double](https://learn.microsoft.com/dotnet/api/system.double) | `0.03`. Connection-failure probability. |
| `ErrorPercent` | [double](https://learn.microsoft.com/dotnet/api/system.double) | `0`. HTTP-error probability when no connection failure occurs. |
| `ErrorStatusCode` | [HttpStatusCode](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode) | `InternalServerError` (`500`). Injected response status. |
| `FailureFactory` | [`Func<Exception>`](https://learn.microsoft.com/dotnet/api/system.func-1) | Creates an [HttpRequestException](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestexception) with message `Refit.Testing simulated network failure.` |
| `StubHttp.Behavior` | [NetworkBehavior](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/NetworkBehavior.cs), nullable | Constructor-supplied behavior, or `null` to disable simulation. See [handler construction](testing/index.md). |

### Test code that accepts a response

[Full description and examples](testing/response-stubs.md).

Types: `Refit.Testing.StubApiResponse<T>`.

#### Supply a consistent state

[Full description and examples](testing/response-stubs.md).

| Property | Type | Default and what the test supplies |
| --- | --- | --- |
| `Content` | `T?` | `default(T)`. Typed body for the scenario. |
| `HasContent` | [bool](https://learn.microsoft.com/dotnet/api/system.boolean) | `false`. Whether the test promises non-null content. |
| `IsSuccessfulWithContent` | [bool](https://learn.microsoft.com/dotnet/api/system.boolean) | `false`. Whether success and non-null content are both promised. |
| `IsSuccessStatusCode` | [bool](https://learn.microsoft.com/dotnet/api/system.boolean) | `false`. Whether the supplied status is 200–299. |
| `IsSuccessful` | [bool](https://learn.microsoft.com/dotnet/api/system.boolean) | `false`. Whether status succeeds and no error occurred. |
| `IsReceived` | [bool](https://learn.microsoft.com/dotnet/api/system.boolean) | `false`. Whether a reply arrived. |
| `StatusCode` | [HttpStatusCode](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode), nullable | `null`. Reply status for the scenario. |
| `ReasonPhrase` | [string](https://learn.microsoft.com/dotnet/api/system.string), nullable | `null`. Reply reason phrase. |
| `Version` | [Version](https://learn.microsoft.com/dotnet/api/system.version), nullable | `null`. HTTP version. |
| `Headers` | [HttpResponseHeaders](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders), nullable | `null`. Reply header collection. |
| `ContentHeaders` | [HttpContentHeaders](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpcontentheaders), nullable | `null`. Body header collection. |
| `RequestMessage` | [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage), nullable | `null`. Associated request. |
| `Error` | [ApiExceptionBase](results/errors.md), nullable | `null`. Exception for a simulated failure. |

#### Select an error kind

[Full description and examples](testing/response-stubs.md).

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StubApiResponse<T>()` | Creates an independently configurable response wrapper for a test scenario. | None. `T` is the body type. | A stub with the defaults above. |
| `HasRequestError(out ApiRequestException? error)` | Tests whether this stub represents a transport failure before a response arrived. | [ApiRequestException](results/errors.md) `error`: receives the request-phase error or `null`. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean): `true` exactly when `Error` is an [`ApiRequestException`](results/errors.md); the output is non-null on success. |
| `HasResponseError(out ApiException? error)` | Tests whether this stub represents an HTTP or body-reading response failure. | [ApiException](results/errors.md) `error`: receives the response-phase error or `null`. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean): `true` exactly when `Error` is an [`ApiException`](results/errors.md), including [`ValidationApiException`](results/errors.md); the output is non-null on success. |
| `Dispose()` | Satisfies the response-wrapper disposal contract without owning assigned resources. | None. | `void`; does not dispose any assigned resource. |


## Advanced APIs

### Generated request helpers

[Full description and examples](advanced/request-helpers.md).

Types: `Refit.FormField<TBody>`, `Refit.GeneratedRequestRunner`, `Refit.UrlResolutionMode`.

#### Path and formatting overloads

[Full description and examples](advanced/request-helpers.md).

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BuildRequestPath(string relativePathTemplate, bool allowUnmatchedParameter)` | Validates a parameterless route template before using it as a request path. | [string](https://learn.microsoft.com/dotnet/api/system.string) `relativePathTemplate`: route; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `allowUnmatchedParameter`: whether unresolved placeholders are allowed. | [string](https://learn.microsoft.com/dotnet/api/system.string): unchanged template, or throws for unresolved placeholders when the flag is false. |
| `BuildRequestPath(string relativePathTemplate, bool allowUnmatchedParameter, ReadOnlySpan<((int StartIdx, int EndIdx) Range, string? Value)> uriParams)` | Replaces several path placeholders using default escaping. | [string](https://learn.microsoft.com/dotnet/api/system.string) template and [bool](https://learn.microsoft.com/dotnet/api/system.boolean) unmatched flag; [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1) `uriParams`: ordered placeholder ranges and replacement strings. | [string](https://learn.microsoft.com/dotnet/api/system.string): path with escaped replacements and optional null segments removed. |
| `BuildRequestPath(string relativePathTemplate, bool allowUnmatchedParameter, ReadOnlySpan<((int StartIdx, int EndIdx) Range, string? Value, bool PreEncoded)> uriParams)` | Replaces several placeholders while allowing selected values to bypass escaping. | [string](https://learn.microsoft.com/dotnet/api/system.string) template and [bool](https://learn.microsoft.com/dotnet/api/system.boolean) unmatched flag; [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan-1) `uriParams`: ordered ranges, values, and per-value encoding flags. | [string](https://learn.microsoft.com/dotnet/api/system.string): path with replacements escaped unless their `PreEncoded` flag is true. |
| `BuildRequestPath<T>(string relativePathTemplate, bool allowUnmatchedParameter, (int StartIdx, int EndIdx) range, T value)` | Replaces one placeholder with an invariant unformatted numeric value. | [string](https://learn.microsoft.com/dotnet/api/system.string) template; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) unmatched flag; [tuple](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/value-tuples) `range`: one placeholder; `value`: an [ISpanFormattable](https://learn.microsoft.com/dotnet/api/system.ispanformattable). Requires `T : ISpanFormattable`. | [string](https://learn.microsoft.com/dotnet/api/system.string): path with an invariant formatted value. Use this overload only for unformatted integers, as explained above. |
| `BuildRequestPath<T>(string relativePathTemplate, bool allowUnmatchedParameter, (int StartIdx, int EndIdx) range, T value, string? format)` | Replaces one placeholder with an invariant value using a format string. | [string](https://learn.microsoft.com/dotnet/api/system.string) template; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) unmatched flag; [tuple](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/value-tuples) `range`: placeholder; [ISpanFormattable](https://learn.microsoft.com/dotnet/api/system.ispanformattable) `value`; [string](https://learn.microsoft.com/dotnet/api/system.string) `format`: format or `null`. Requires `T : ISpanFormattable`. | [string](https://learn.microsoft.com/dotnet/api/system.string): path with an escaped invariant formatted replacement. |
| `BuildRelativeUri(HttpClient client, string relativePath, UrlResolutionMode urlResolution)` | Combines a route with the client base path under the selected resolution rule. | [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`: supplies the base path; [string](https://learn.microsoft.com/dotnet/api/system.string) `relativePath`: route; [UrlResolutionMode](clients/settings.md#url-resolution) `urlResolution`: resolution rule. | [Uri](https://learn.microsoft.com/dotnet/api/system.uri): relative URI for [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) to resolve. |
| `BuildRelativeUri(HttpClient client, string relativePath, UrlResolutionMode urlResolution, UriFormat queryUriFormat)` | Builds a relative URI and applies the legacy query rendering mode when relevant. | [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`; [string](https://learn.microsoft.com/dotnet/api/system.string) `relativePath`; [UrlResolutionMode](clients/settings.md#url-resolution) `urlResolution`; [UriFormat](https://learn.microsoft.com/dotnet/api/system.uriformat) `queryUriFormat`: legacy path/query escaping rule. | [Uri](https://learn.microsoft.com/dotnet/api/system.uri): relative URI. RFC resolution ignores `queryUriFormat`. |
| `RequireAbsoluteUrl(object? url)` | Rejects a URL value that is absent or not absolute. | [object](https://learn.microsoft.com/dotnet/api/system.object) `url`: a [string](https://learn.microsoft.com/dotnet/api/system.string) or [Uri](https://learn.microsoft.com/dotnet/api/system.uri) with an absolute address. | [string](https://learn.microsoft.com/dotnet/api/system.string): original URL text. Throws `ArgumentException` if it cannot be parsed as absolute. This does not enforce HTTP/HTTPS. |
| `RoundTripEscapePath(string? value, RefitSettings settings, ICustomAttributeProvider attributeProvider, Type type)` | Formats and escapes a catch-all path without escaping its separators. | [string](https://learn.microsoft.com/dotnet/api/system.string) `value`: catch-all path or `null`; [RefitSettings](clients/settings.md) `settings`; [ICustomAttributeProvider](https://learn.microsoft.com/dotnet/api/system.reflection.icustomattributeprovider) `attributeProvider`: formatting attributes; [Type](https://learn.microsoft.com/dotnet/api/system.type) `type`: declared value type. | [string](https://learn.microsoft.com/dotnet/api/system.string): formatted and escaped path sections with `/` separators retained. |
| `FormatUrlParameter(RefitSettings settings, object? value, ICustomAttributeProvider attributeProvider, Type type)` | Formats one value through the registered or default URL formatter. | [RefitSettings](clients/settings.md) `settings`; [object](https://learn.microsoft.com/dotnet/api/system.object) `value`: value or `null`; [ICustomAttributeProvider](https://learn.microsoft.com/dotnet/api/system.reflection.icustomattributeprovider) `attributeProvider`: attributes; [Type](https://learn.microsoft.com/dotnet/api/system.type) `type`: declared type. | [string](https://learn.microsoft.com/dotnet/api/system.string), nullable: result from the selected URL formatter. |
| `FormatInvariant<T>(T value, string? format)` | Renders an `IFormattable` using invariant culture without URL escaping. | `value`: an [IFormattable](https://learn.microsoft.com/dotnet/api/system.iformattable); [string](https://learn.microsoft.com/dotnet/api/system.string) `format`: format or `null`. Requires `T : IFormattable`. | [string](https://learn.microsoft.com/dotnet/api/system.string): invariant formatted value without URL escaping. |
| `BuildQueryKey(RefitSettings settings, string clrName, string? explicitName, string? prefixSegment)` | Builds the final query key from an alias or formatted CLR name and optional prefix. | [RefitSettings](clients/settings.md) `settings`; [string](https://learn.microsoft.com/dotnet/api/system.string) `clrName`: declared name; [string](https://learn.microsoft.com/dotnet/api/system.string) `explicitName`: alias or `null`; [string](https://learn.microsoft.com/dotnet/api/system.string) `prefixSegment`: prefix including delimiter, or `null`. | [string](https://learn.microsoft.com/dotnet/api/system.string): explicit or formatted name with the prefix prepended. |
| `UsesDefaultUrlParameterFormatting(RefitSettings settings)` | Checks whether URL values can use the built-in formatter fast path. | [RefitSettings](clients/settings.md) `settings`: formatters to inspect. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean): whether inline URL formatting matches the pristine default formatter and the formatter map is empty. |
| `UsesDefaultFormUrlEncodedParameterFormatting(RefitSettings settings)` | Checks whether form values use the exact built-in formatter type. | [RefitSettings](clients/settings.md) `settings`: formatter to inspect. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean): whether the form formatter has the exact built-in default type. |
| `UsesDefaultUrlParameterKeyFormatting(RefitSettings settings)` | Checks whether query keys use the exact built-in key formatter type. | [RefitSettings](clients/settings.md) `settings`: formatter to inspect. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean): whether the key formatter has the exact built-in default type. |
| `AddFormattedCollectionProperty(ref GeneratedQueryStringBuilder builder, RefitSettings settings, IEnumerable? values, string key, CollectionFormat collectionFormat, bool preEncoded, (Type ElementProviderType, ICustomAttributeProvider JoinedProvider, Type JoinedType) formatting)` | Formats and appends a collection-valued query property using the configured collection rule. | [GeneratedQueryStringBuilder](advanced/query-builder.md) `builder`: updated by reference; [RefitSettings](clients/settings.md) `settings`; [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.ienumerable) `values`: collection or `null`; [string](https://learn.microsoft.com/dotnet/api/system.string) `key`; [CollectionFormat](requests/queries.md) `collectionFormat`; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `preEncoded`; [tuple](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/value-tuples) `formatting`: element [Type](https://learn.microsoft.com/dotnet/api/system.type), joined-value [ICustomAttributeProvider](https://learn.microsoft.com/dotnet/api/system.reflection.icustomattributeprovider), and joined [Type](https://learn.microsoft.com/dotnet/api/system.type). | `void`; appends values using the two formatting passes described in [query building](advanced/query-builder.md). Null appends nothing. |

#### Header and option overloads

[Full description and examples](advanced/request-helpers.md).

| Overload | Description | Parameters | Returns |
| --- | --- | --- |
| `SetHeader(HttpRequestMessage request, string name, string? value, bool validateHeaders)` | Replaces one request header and optionally validates its syntax. | [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `request`; [string](https://learn.microsoft.com/dotnet/api/system.string) `name`: header name; [string](https://learn.microsoft.com/dotnet/api/system.string) `value`: replacement or `null`; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `validateHeaders`: whether to validate header syntax. | `void`; replaces the header, or removes it for `null`. |
| `AddHeaderCollection(HttpRequestMessage request, IDictionary<string, string>? headers, bool validateHeaders)` | Applies a collection of header replacements to the request. | [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `request`; [`IDictionary<string, string>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2) `headers`: replacements or `null`; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `validateHeaders`: whether to validate syntax. | `void`; applies `SetHeader` to each entry. Null does nothing. |
| `AddConfiguredRequestOptions(HttpRequestMessage request, RefitSettings settings, Type interfaceType)` | Copies configured request options and HTTP version settings onto a request. | [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `request`; [RefitSettings](clients/settings.md) `settings`: options and version rules; [Type](https://learn.microsoft.com/dotnet/api/system.type) `interfaceType`: Refit interface. | `void`; stores request options and interface type, plus HTTP version settings on modern .NET. |
| `AddRequestProperty<TValue>(HttpRequestMessage request, string key, TValue value)` | Stores one typed request option for later request execution. | [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `request`; [string](https://learn.microsoft.com/dotnet/api/system.string) `key`: option key; `value`: option value. | `void`; sets a typed option, or a dictionary entry on .NET Framework. |
| `SetRequestTimeout(HttpRequestMessage request, int timeoutMilliseconds)` | Records the per-request timeout for the send helper to apply. | [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `request`; [int](https://learn.microsoft.com/dotnet/api/system.int32) `timeoutMilliseconds`: timeout in milliseconds. | `void`; stores a timeout for dispatch to apply. |

#### Body helper overloads

[Full description and examples](advanced/request-helpers.md).

| Overload | Description | Parameters | Returns |
| --- | --- | --- |
| `CreateBodyContent<TBody>(RefitSettings settings, TBody body, BodySerializationMethod serializationMethod, bool streamBody)` | Serializes a request body according to the selected body mode, preserving supplied content and streams. | [RefitSettings](clients/settings.md) `settings`; `body`: value to send; [BodySerializationMethod](requests/bodies.md) `serializationMethod`; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `streamBody`: whether serialized content streams. | [HttpContent](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent): existing content, protected stream content, raw text, or serialized body as described above. |
| `CreateJsonLinesBodyContent<TBody>(RefitSettings settings, TBody body)` | Creates newline-delimited JSON content from one value or an enumerable body. | [RefitSettings](clients/settings.md) `settings`; `body`: one value or a sequence of values. | [HttpContent](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent): JSON Lines content, or existing content/stream handling. |
| `CreateStreamContent(Stream stream)` | Wraps a caller-owned stream without taking ownership of that stream. | [Stream](https://learn.microsoft.com/dotnet/api/system.io.stream) `stream`: caller-owned body stream. | [HttpContent](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent): wrapper that leaves the stream open when disposed. |
| `CreateUrlEncodedBodyContent<TBody>(RefitSettings settings, TBody body)` | Converts a body to URL-encoded form content, with special handling for existing content, streams and strings. | [RefitSettings](clients/settings.md) `settings`; `body`: form object, dictionary, string, content or stream. | [HttpContent](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent): URL-encoded form or existing content/stream handling. Object flattening uses reflection. |
| `CreateUrlEncodedBodyContent<TBody>(RefitSettings settings, TBody body, FormField<TBody>[] fields)` | Converts a body to URL-encoded form content using generated field descriptors when supported. | [RefitSettings](clients/settings.md) `settings`; `body`: form value; `fields`: [form descriptors](advanced/request-helpers.md#form-field-reference) with direct getters. | [HttpContent](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent): form content using eligible descriptors, otherwise the reflection path described above. |
| `CanUnrollForm(object? body)` | Checks whether a body can use the generated property-by-property form path. | [object](https://learn.microsoft.com/dotnet/api/system.object) `body`: candidate form value, or `null`. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean): `true` for non-null values other than strings, streams, HTTP content and dictionaries. |
| `SerializeMultipartPart<T>(RefitSettings settings, T value, string fieldName)` | Serializes one multipart value with the configured content serializer. | [RefitSettings](clients/settings.md) `settings`; `value`: one part; [string](https://learn.microsoft.com/dotnet/api/system.string) `fieldName`: name used in an error. | [HttpContent](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent): serialized part. Serializer failures become `ArgumentException`. |
| `CompressBodyContent(HttpContent content, RefitSettings settings, RequestCompression compression, CompressionLevel level)` | Applies the resolved request compression setting to HTTP content. | [HttpContent](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent) `content`: input; [RefitSettings](clients/settings.md) `settings`: defaults/options; [RequestCompression](requests/bodies.md) `compression`: coding; [CompressionLevel](https://learn.microsoft.com/dotnet/api/system.io.compression.compressionlevel) `level`: effort for explicit coding. | [HttpContent](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent): owning compression wrapper, or the same content when no coding applies. |

#### Dispatch overloads

[Full description and examples](advanced/request-helpers.md).

| Parameter | Type | Value |
| --- | --- | --- |
| `isApiResponse` | [bool](https://learn.microsoft.com/dotnet/api/system.boolean) | `true` when `T` is a supported [response wrapper](results/responses.md). |
| `shouldDisposeResponse` | [bool](https://learn.microsoft.com/dotnet/api/system.boolean) | `true` for a fully consumed result. Use `false` when returning a live response owner. |
| `bufferBody` | [bool](https://learn.microsoft.com/dotnet/api/system.boolean) | Whether to buffer request content before sending. |

| Overload | Description | Parameters | Returns |
| --- | --- | --- |
| `SendVoidAsync(HttpClient client, HttpRequestMessage request, RefitSettings settings, bool bufferBody, CancellationToken cancellationToken)` | Sends a request whose successful result has no response body. | [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`; [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `request`: message to send; [RefitSettings](clients/settings.md) `settings`; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `bufferBody`: flag above; [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken) `cancellationToken`: request cancellation. | [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task): completion without a result. Disposes the request and response. |
| `SendAsync<T, TBody>(HttpClient client, HttpRequestMessage request, RefitSettings settings, bool isApiResponse, bool shouldDisposeResponse, bool bufferBody, CancellationToken cancellationToken)` | Sends a request and processes its response as a deserialized value or API response wrapper. | [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`; [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `request`; [RefitSettings](clients/settings.md) `settings`; three [bool](https://learn.microsoft.com/dotnet/api/system.boolean) flags above; [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken) `cancellationToken`: request cancellation. | [`Task<T?>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1): deserialized, raw, or wrapped result. Disposes the request. Response ownership follows the flag. |
| `SendObservable<T, TBody>(HttpClient client, Func<HttpRequestMessage> requestFactory, RefitSettings settings, bool isApiResponse, bool shouldDisposeResponse, bool bufferBody, CancellationToken methodCancellationToken)` | Creates a cold observable that builds and sends a fresh request for each subscription. | [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`; [`Func<HttpRequestMessage>`](https://learn.microsoft.com/dotnet/api/system.func-1) `requestFactory`: creates a fresh message per subscription; [RefitSettings](clients/settings.md) `settings`; three [bool](https://learn.microsoft.com/dotnet/api/system.boolean) flags above; [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken) `methodCancellationToken`: caller cancellation. | [`IObservable<T?>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1): sends one request per subscription and delivers its result or error. See [observable replies](results/return-types.md#querying-a-reply). |
| `StreamAsync<T>(HttpClient client, HttpRequestMessage request, RefitSettings settings, CancellationToken methodCancellationToken, CancellationToken cancellationToken = default)` | Sends a request and exposes the response body as an asynchronous stream. | [HttpClient](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) `client`; [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `request`: one message; [RefitSettings](clients/settings.md) `settings`; [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken) `methodCancellationToken`: caller token; [CancellationToken](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken) `cancellationToken`: enumeration token, default non-cancelable. | [`IAsyncEnumerable<T?>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.iasyncenumerable-1): one streaming response. Enumeration/disposal releases its request, response and stream. |

#### Form field reference

[Full description and examples](advanced/request-helpers.md).

| Overload | Description | Parameters | Returns |
| --- | --- | --- |
| `FormField(Func<TBody, object?> getter, string clrName, string? explicitName, string? prefixSegment, string? format, CollectionFormat? collectionFormat, bool serializeNull)` | Creates a descriptor that reads and formats one URL-encoded form field. | [`Func<TBody, object?>`](https://learn.microsoft.com/dotnet/api/system.func-2) `getter`: reads a field; [string](https://learn.microsoft.com/dotnet/api/system.string) `clrName`: declared name; nullable [string](https://learn.microsoft.com/dotnet/api/system.string) arguments: explicit name, prefix with delimiter and value format; nullable [CollectionFormat](requests/queries.md) `collectionFormat`: override or settings default; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `serializeNull`: whether null emits an empty field. | A [`FormField<TBody>`](https://github.com/reactiveui/refit/blob/main/src/Refit/FormField.cs) descriptor. |
| `ResolveFieldName(IUrlParameterKeyFormatter urlParameterKeyFormatter)` | Resolves the final form key from the explicit name or configured key formatter. | [IUrlParameterKeyFormatter](requests/query-formatters.md) `urlParameterKeyFormatter`: formats `ClrName` when no explicit name is set. | [string](https://learn.microsoft.com/dotnet/api/system.string), nullable: resolved name with the prefix prepended. |

| Property | Type | Value |
| --- | --- | --- |
| `Getter` | [`Func<TBody, object?>`](https://learn.microsoft.com/dotnet/api/system.func-2) | Reads the field value from a body instance. |
| `ClrName` | [string](https://learn.microsoft.com/dotnet/api/system.string) | Declared property name. |
| `ExplicitName` | [string](https://learn.microsoft.com/dotnet/api/system.string), nullable | Alias or serializer name; `null` uses the key formatter. |
| `PrefixSegment` | [string](https://learn.microsoft.com/dotnet/api/system.string), nullable | Prefix including delimiter; `null` adds none. |
| `Format` | [string](https://learn.microsoft.com/dotnet/api/system.string), nullable | Value format; `null` uses default formatting. |
| `CollectionFormat` | [CollectionFormat](requests/queries.md), nullable | Explicit collection rule; `null` uses settings. |
| `SerializeNull` | [bool](https://learn.microsoft.com/dotnet/api/system.boolean) | `true` emits an empty field for null; `false` omits it. |

| `UrlResolutionMode` value | Numeric value | Meaning |
| --- | --- | --- |
| `RefitLegacy` | `0` | Prefix the base-address path and require a leading slash. |
| `Rfc3986` | `1` | Use standard URI resolution. See [URL settings](clients/settings.md#url-resolution). |

### Generated query builder

[Full description and examples](advanced/query-builder.md).

Types: `Refit.GeneratedParameterAttributeProvider`, `Refit.GeneratedQueryStringBuilder`, `Refit.GeneratedSingleTypeParameterAttributeProvider`.

#### Append a collection

[Full description and examples](advanced/query-builder.md).

| Format | Result |
| --- | --- |
| `Multi` | One pair per non-null element; an empty collection emits nothing. |
| `Csv` or `RefitParameterFormatter` | One comma-joined value. |
| `Ssv` | One value joined with spaces. |
| `Tsv` | One value joined with tabs. |
| `Pipes` | One value joined with vertical bars. |
| `Indexed` | This low-level helper joins with commas. Generated query-object code performs [indexed expansion](requests/queries.md) separately. |

#### Query builder API reference

[Full description and examples](advanced/query-builder.md).

| Type | Purpose |
| --- | --- |
| `GeneratedQueryStringBuilder` | A stack-only builder that appends an escaped query string to a relative request path without reflection. |
| `GeneratedParameterAttributeProvider` | Supplies attributes from a dictionary when a generated parameter has more than one attribute type. |
| `GeneratedSingleTypeParameterAttributeProvider` | Supplies one type's attributes without allocating a dictionary or flattening arrays. |

| Overload | Description | Parameters | Returns |
| --- | --- | --- |
| `GeneratedQueryStringBuilder(string relativePath)` | Starts query construction and detects an existing query marker. | [string](https://learn.microsoft.com/dotnet/api/system.string) `relativePath`: path with escaped dynamic segments and any template query. | A builder that detects whether the path contains `?`. |
| `GeneratedQueryStringBuilder(string relativePath, bool hasQuery)` | Starts query construction using caller-known query state. | [string](https://learn.microsoft.com/dotnet/api/system.string) `relativePath`: escaped path; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `hasQuery`: whether it contains `?`. | A builder that trusts the supplied query state. |
| `Add(string name, string? value, bool preEncoded)` | Appends one ordinary query pair. | [string](https://learn.microsoft.com/dotnet/api/system.string) `name`: key; [string](https://learn.microsoft.com/dotnet/api/system.string) `value`: value or `null`; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `preEncoded`: whether both parts are encoded. | `void`; appends a pair, or omits it for `null`. Empty values produce `key=`. |
| `AddPreEscapedKey(string name, string? value, bool preEncoded)` | Appends a pair whose key has already been escaped. | [string](https://learn.microsoft.com/dotnet/api/system.string) `name`: escaped key; [string](https://learn.microsoft.com/dotnet/api/system.string) `value`: value or `null`; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `preEncoded`: whether the value is encoded. | `void`; appends the key verbatim and escapes the value unless `preEncoded` is true. Null omits the pair. |
| `AddFormatted<T>(string name, T value, string? format, bool preEncoded)` | Formats a value invariantly before appending an ordinary pair. | [string](https://learn.microsoft.com/dotnet/api/system.string) `name`: key; [ISpanFormattable](https://learn.microsoft.com/dotnet/api/system.ispanformattable) `value`: value to format; [string](https://learn.microsoft.com/dotnet/api/system.string) `format`: format or `null`; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `preEncoded`: whether the key and formatted value are encoded. | `void`; formats with invariant culture and appends the pair. |
| `AddFormattedPreEscapedKey<T>(string name, T value, string? format, bool preEncoded)` | Formats a value for a key that has already been escaped. | [string](https://learn.microsoft.com/dotnet/api/system.string) `name`: escaped key; [ISpanFormattable](https://learn.microsoft.com/dotnet/api/system.ispanformattable) `value`: value to format; [string](https://learn.microsoft.com/dotnet/api/system.string) `format`: format or `null`; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `preEncoded`: whether the formatted value is encoded. | `void`; appends the key verbatim and formats the value with invariant culture. |
| `AddFlag(string? name, bool preEncoded)` | Appends a valueless query flag. | [string](https://learn.microsoft.com/dotnet/api/system.string) `name`: flag text or `null`; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `preEncoded`: whether it is encoded. | `void`; appends a key without `=`, or omits a null flag. |
| `BeginCollection(string name, CollectionFormat collectionFormat, bool preEncoded)` | Opens a collection whose values will be appended next. | [string](https://learn.microsoft.com/dotnet/api/system.string) `name`: key; [CollectionFormat](requests/queries.md) `collectionFormat`: join/repeat rule; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `preEncoded`: whether the key and values are encoded. | `void`; opens a collection. Finish the preceding collection first. |
| `AddCollectionValue(string? value)` | Adds one raw value to the open collection. | [string](https://learn.microsoft.com/dotnet/api/system.string) `value`: next value, or `null`. | `void`; adds a value to the open collection. Null is omitted for `Multi` and adds an empty position for joined formats. |
| `AddCollectionValueFormatted<T>(T value)` | Formats and adds one value to the open collection. | [ISpanFormattable](https://learn.microsoft.com/dotnet/api/system.ispanformattable) `value`: next value to format. | `void`; formats with invariant culture and no format string, then adds it to the open collection. |
| `EndCollection()` | Closes the open collection and writes its joined value when needed. | None. | `void`; finishes the open collection and writes any joined value. |
| `Build()` | Finalizes the path and releases builder storage. | None. | [string](https://learn.microsoft.com/dotnet/api/system.string): the completed relative path and query. Releases pooled storage. Treat this as the final operation. |

#### Attribute provider API reference

[Full description and examples](advanced/query-builder.md).

| Overload | Description | Parameters | Returns |
| --- | --- | --- |
| `GeneratedParameterAttributeProvider(Dictionary<Type, object[]> attributes)` | Creates an attribute provider for parameters with several attribute types. | [`Dictionary<Type, object[]>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.dictionary-2) `attributes`: each [Type](https://learn.microsoft.com/dotnet/api/system.type) and its array of attribute objects. | A provider for several attribute types. |
| `GeneratedParameterAttributeProvider.GetCustomAttributes(bool inherit)` | Returns every configured attribute as one shared array. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `inherit`: ignored. | [object[]](https://learn.microsoft.com/dotnet/api/system.object): cached array of all configured attributes. Treat the returned array as read-only. |
| `GeneratedParameterAttributeProvider.GetCustomAttributes(Type attributeType, bool inherit)` | Returns attributes for one exact configured type. | [Type](https://learn.microsoft.com/dotnet/api/system.type) `attributeType`: exact type to find; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `inherit`: ignored. | [object[]](https://learn.microsoft.com/dotnet/api/system.object): the stored array, or an empty array when the key is absent. |
| `GeneratedParameterAttributeProvider.IsDefined(Type attributeType, bool inherit)` | Checks whether an exact attribute type has an entry. | [Type](https://learn.microsoft.com/dotnet/api/system.type) `attributeType`: exact type to find; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `inherit`: ignored. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean): whether the dictionary contains the key, even if its array is empty. |
| `GeneratedSingleTypeParameterAttributeProvider(Type type, object[] attributes)` | Creates an attribute provider optimized for one attribute type. | [Type](https://learn.microsoft.com/dotnet/api/system.type) `type`: shared attribute type; [object[]](https://learn.microsoft.com/dotnet/api/system.object) `attributes`: attribute objects of that type. | A provider for one attribute type. |
| `GeneratedSingleTypeParameterAttributeProvider.GetCustomAttributes(bool inherit)` | Returns the provider's configured attribute array. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `inherit`: ignored. | [object[]](https://learn.microsoft.com/dotnet/api/system.object): the supplied array. Treat it as read-only. |
| `GeneratedSingleTypeParameterAttributeProvider.GetCustomAttributes(Type attributeType, bool inherit)` | Returns attributes only when the requested type matches. | [Type](https://learn.microsoft.com/dotnet/api/system.type) `attributeType`: exact type to find; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `inherit`: ignored. | [object[]](https://learn.microsoft.com/dotnet/api/system.object): the supplied array for the configured type, otherwise an empty array. |
| `GeneratedSingleTypeParameterAttributeProvider.IsDefined(Type attributeType, bool inherit)` | Checks whether the requested type matches the configured type. | [Type](https://learn.microsoft.com/dotnet/api/system.type) `attributeType`: exact type to find; [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `inherit`: ignored. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean): whether the type equals the configured type, even if its array is empty. |

| Field | Description | Type | Value |
| --- | --- | --- | --- |
| `GeneratedParameterAttributeProvider.Empty` | Reuses one provider for parameters that declare no attributes. | [`GeneratedParameterAttributeProvider`](advanced/query-builder.md) | Shared static readonly provider with no attributes. |

| Overload | Description | Parameters | Returns |
| --- | --- | --- |
| `GeneratedRequestRunner.FormatUrlParameter(RefitSettings settings, object? value, ICustomAttributeProvider attributeProvider, Type type)` | Formats one query value through the configured URL formatter. | [RefitSettings](clients/settings.md) `settings`: formatter configuration; [object](https://learn.microsoft.com/dotnet/api/system.object) `value`: value or `null`; [ICustomAttributeProvider](https://learn.microsoft.com/dotnet/api/system.reflection.icustomattributeprovider) `attributeProvider`: attributes for formatting; [Type](https://learn.microsoft.com/dotnet/api/system.type) `type`: declared value type. | [string](https://learn.microsoft.com/dotnet/api/system.string), nullable: result from the selected URL formatter. |

### Method metadata and client names

[Full description and examples](advanced/method-metadata.md).

Types: `Refit.ParameterType`, `Refit.RestMethodInfo`, `Refit.RestMethodParameterInfo`, `Refit.RestMethodParameterProperty`, `Refit.UniqueName`.

#### Method record reference

[Full description and examples](advanced/method-metadata.md).

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `RestMethodInfo(string Name, Type HostingType, MethodInfo MethodInfo, string RelativePath, Type ReturnType)` | Packages the reflected details that identify one Refit method. | [string](https://learn.microsoft.com/dotnet/api/system.string) `Name`: method name; [Type](https://learn.microsoft.com/dotnet/api/system.type) `HostingType`: declaring interface; [MethodInfo](https://learn.microsoft.com/dotnet/api/system.reflection.methodinfo) [`MethodInfo`](https://learn.microsoft.com/dotnet/api/system.reflection.methodinfo): reflected method; [string](https://learn.microsoft.com/dotnet/api/system.string) `RelativePath`: route template; [Type](https://learn.microsoft.com/dotnet/api/system.type) `ReturnType`: declared result type. | A [`RestMethodInfo`](advanced/method-metadata.md) containing the supplied metadata. |
| `Deconstruct(out string Name, out Type HostingType, out MethodInfo MethodInfo, out string RelativePath, out Type ReturnType)` | Splits the record into its positional values for deconstruction syntax. | The five `out` arguments receive the corresponding properties below, in constructor order. | `void`; copies the stored values to the arguments. |
| `Equals(RestMethodInfo? other)` | Compares this record with another method record. | `other`: another method record, or `null`. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean): `true` when all five properties are equal; `false` for `null`. |
| `Equals(object? obj)` | Compares this record with an arbitrary object of the same record type. | [object](https://learn.microsoft.com/dotnet/api/system.object) `obj`: any object, or `null`. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean): `true` only for a [`RestMethodInfo`](advanced/method-metadata.md) with equal properties. |
| `operator ==(RestMethodInfo? left, RestMethodInfo? right)` | Tests two records for value equality. | `left`, `right`: records to compare. Both may be `null`. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean): `true` for equal records or two nulls. |
| `operator !=(RestMethodInfo? left, RestMethodInfo? right)` | Tests two records for unequal values. | `left`, `right`: records to compare. Both may be `null`. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean): the opposite of `==`. |
| `GetHashCode()` | Produces a hash for use in hash-based collections. | None. | [int](https://learn.microsoft.com/dotnet/api/system.int32): a hash based on the stored values. Equal records have equal hashes. |
| `ToString()` | Renders the record and its values for diagnostics. | None. | [string](https://learn.microsoft.com/dotnet/api/system.string): the record name and its property names and values. |
| `<Clone>$()` (compiler member used by `with`) | Makes the shallow copy used by a C# `with` expression. | None. Use a [with expression](https://learn.microsoft.com/dotnet/csharp/language-reference/operators/with-expression) in C# rather than calling this metadata name. | A shallow [`RestMethodInfo`](advanced/method-metadata.md) copy. The reflected objects are shared with the original. |

| Property | Type | Value and access |
| --- | --- | --- |
| `Name` | [string](https://learn.microsoft.com/dotnet/api/system.string) | Method name supplied to the constructor; `get; init;`. |
| `HostingType` | [Type](https://learn.microsoft.com/dotnet/api/system.type) | Declaring interface supplied to the constructor; `get; init;`. |
| `MethodInfo` | [MethodInfo](https://learn.microsoft.com/dotnet/api/system.reflection.methodinfo) | Reflected method supplied to the constructor; `get; init;`. |
| `RelativePath` | [string](https://learn.microsoft.com/dotnet/api/system.string) | Route template supplied to the constructor; `get; init;`. |
| `ReturnType` | [Type](https://learn.microsoft.com/dotnet/api/system.type) | Declared result type supplied to the constructor; `get; init;`. |

#### Parameter metadata reference

[Full description and examples](advanced/method-metadata.md).

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `RestMethodParameterInfo(string name, ParameterInfo parameterInfo)` | Describes a route parameter by its binding name. | [string](https://learn.microsoft.com/dotnet/api/system.string) `name`: route parameter name; [ParameterInfo](https://learn.microsoft.com/dotnet/api/system.reflection.parameterinfo) `parameterInfo`: reflected parameter. | A named parameter description with `IsObjectPropertyParameter = false`. |
| `RestMethodParameterInfo(bool isObjectPropertyParameter, ParameterInfo parameterInfo)` | Describes a parameter whose properties supply route values. | [bool](https://learn.microsoft.com/dotnet/api/system.boolean) `isObjectPropertyParameter`: whether the binding reads object properties; [ParameterInfo](https://learn.microsoft.com/dotnet/api/system.reflection.parameterinfo) `parameterInfo`: reflected parameter. | A parameter description with the supplied flag and `Name = null`. |
| `RestMethodParameterProperty(string name, PropertyInfo propertyInfo)` | Describes one direct property used in route binding. | [string](https://learn.microsoft.com/dotnet/api/system.string) `name`: route binding name; [PropertyInfo](https://learn.microsoft.com/dotnet/api/system.reflection.propertyinfo) `propertyInfo`: property to read. | A property description with a one-element navigation chain. |
| `RestMethodParameterProperty(string name, IReadOnlyList<PropertyInfo> propertyChain)` | Describes a nested property walk used in route binding. | [string](https://learn.microsoft.com/dotnet/api/system.string) `name`: route binding name; [`IReadOnlyList<PropertyInfo>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1) `propertyChain`: non-empty chain of [PropertyInfo](https://learn.microsoft.com/dotnet/api/system.reflection.propertyinfo) objects in navigation order. | A property description that retains the list and uses its final element as [`PropertyInfo`](https://learn.microsoft.com/dotnet/api/system.reflection.propertyinfo). |

| Property | Type | Value and access |
| --- | --- | --- |
| `RestMethodParameterInfo.Name` | [string](https://learn.microsoft.com/dotnet/api/system.string), nullable | Name supplied to the named constructor, or `null` for the flag constructor; `get; set;`. |
| `RestMethodParameterInfo.ParameterInfo` | [ParameterInfo](https://learn.microsoft.com/dotnet/api/system.reflection.parameterinfo) | Reflected parameter supplied to either constructor; `get; set;`. |
| `RestMethodParameterInfo.IsObjectPropertyParameter` | [bool](https://learn.microsoft.com/dotnet/api/system.boolean) | Whether the binding reads object properties; defaults to `false` in the named constructor; `get; set;`. |
| `RestMethodParameterInfo.ParameterProperties` | [`List<RestMethodParameterProperty>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.list-1) | Starts empty. The list can be replaced during initialization and its contents can be changed later; `get; init;`. |
| `RestMethodParameterInfo.Type` | [`ParameterType`](advanced/method-metadata.md) | Starts as `Normal`; `get; set;`. See the values below. |
| `RestMethodParameterProperty.Name` | [string](https://learn.microsoft.com/dotnet/api/system.string) | Binding name supplied to either constructor; `get; set;`. |
| `RestMethodParameterProperty.PropertyInfo` | [PropertyInfo](https://learn.microsoft.com/dotnet/api/system.reflection.propertyinfo) | Final property to read; `get; set;`. Assigning it does not change `PropertyChain`. |
| `RestMethodParameterProperty.PropertyChain` | [`IReadOnlyList<PropertyInfo>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1) | Ordered navigation chain; `get; set;`. Assigning it does not change [`PropertyInfo`](https://learn.microsoft.com/dotnet/api/system.reflection.propertyinfo). |

| `ParameterType` value | Numeric value | Meaning |
| --- | --- | --- |
| `Normal` | `0` | Ordinary route value escaping. |
| `RoundTripping` | `1` | Catch-all path handling that retains `/` separators. |

#### Client name overloads

[Full description and examples](advanced/method-metadata.md).

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `UniqueName.ForType<T>()` | Reconstructs the generated implementation name for interface `T`. | None. `T` selects the interface. | [string](https://learn.microsoft.com/dotnet/api/system.string): generated implementation name, including assembly identity. |
| `UniqueName.ForType<T>(object? serviceKey)` | Adds a service-key suffix when naming interface `T`. | [object](https://learn.microsoft.com/dotnet/api/system.object) `serviceKey`: key used for registration, or `null`. | [string](https://learn.microsoft.com/dotnet/api/system.string): generated name with a service-key suffix, unless the key is `null` or an empty string. |
| `UniqueName.ForType(Type refitInterfaceType)` | Reconstructs a generated implementation name from a runtime interface type. | [Type](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: interface to name. | [string](https://learn.microsoft.com/dotnet/api/system.string): the same name as the generic overload for that interface. |
| `UniqueName.ForType(Type refitInterfaceType, object? serviceKey)` | Reconstructs a runtime interface name with an optional service-key suffix. | [Type](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: interface to name; [object](https://learn.microsoft.com/dotnet/api/system.object) `serviceKey`: registration key, or `null`. | [string](https://learn.microsoft.com/dotnet/api/system.string): name with the same service-key rules as the generic overload. |
