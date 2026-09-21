---
Order: 3
---
# Dependency injection

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/clients-dependency-injection/clients-dependency-injection.csproj).

Several parts of your app may need the same API client. Registering it with dependency
injection lets those parts ask for the interface in their constructors. You can keep the
service address, request settings and HTTP handlers together in the app's setup code.

`HttpClientFactoryExtensions` is a static class that supplies these registration methods to `IServiceCollection` and `IHttpClientBuilder`.
Refit's integration uses `IHttpClientFactory` to manage the HTTP handlers behind those clients.
This page shows the initial registration, then how to give clients different names or keys.

Install `Refit.HttpClientFactory` beside Refit.
Use `AddRefitGeneratedClient<T>` with a JSON context for clients that support AOT.
This registration binds the interface to Refit's emitted implementation. Keep the reflection
registration in the separate section for applications that accept runtime metadata.
The context overloads exist on .NET 8 and later.

## Register and resolve a generated client

**1. Choose the context.** Use the [shared context](../serialization/json.md) that registers `Person`.
Refit reads and writes JSON with the context's own options, and it never falls back to reflection.

**2. Register the client.** Import `Microsoft.Extensions.DependencyInjection`.
`CreateTransport(expected)` is the sample's local handler factory.
In an app, keep the normal transport or configure the handler your app needs.

**3. Resolve the interface and call it.** This console example owns and disposes its service provider.
An application host owns the provider for a hosted app.


```csharp
ServiceCollection services = new();
_ = services.AddRefitGeneratedClient<IClientApi>(SampleJsonContext.Default)
    .ConfigureHttpClient(static client => client.BaseAddress = new(BaseUrl))
    .ConfigurePrimaryHttpMessageHandler(() => CreateTransport(expected));
await using ServiceProvider provider = services.BuildServiceProvider();
IClientApi api = provider.GetRequiredService<IClientApi>();
Person person = await api.ReadAsync();
Console.WriteLine(person.Name); // Ada
```

The API implementation is transient: each resolution can create a new implementation.
The factory manages the HTTP handler's lifetime.
Configure the returned builder to set the base address, handlers or your app's HTTP resilience pipeline.
An unavailable generated implementation fails when the client is resolved.

## Register with settings

Register a `RefitSettings` when you need more than JSON, or when you prefer to own the JSON options.
Pick the options way when you have `JsonSerializerOptions` to share, need full control,
or want one options object reused elsewhere in your app.
The short path needs no options object. The options way makes you assign the `TypeInfoResolver` yourself
and keep the options unchanged after first use.
[Client creation](creation.md#use-settings-instead-of-a-context) builds `JsonSettings` this way.

The settings holder is a singleton: its settings are shared by clients for that registration.


```csharp
ServiceCollection settingsServices = new();
_ = settingsServices.AddSingleton<ISettingsFor>(new SettingsFor<IClientApi>(JsonSettings));
_ = settingsServices.AddRefitGeneratedClient<IClientApi>(JsonSettings)
    .ConfigureHttpClient(static client => client.BaseAddress = new(BaseUrl))
    .ConfigurePrimaryHttpMessageHandler(() => CreateTransport(expected));
await using ServiceProvider settingsProvider = settingsServices.BuildServiceProvider();
IClientApi settingsApi = settingsProvider.GetRequiredService<IClientApi>();
Person fromSettings = await settingsApi.ReadAsync();
```

To keep your settings and add the context, pass both. That is the bridge.
See [obtain settings from services](#obtain-settings-from-services).

## Distinguish clients with a key

A key lets you register the same interface for different destinations or policies.
Resolve it with `GetRequiredKeyedService<T>` and the same key.
Refit rejects null keys.


```csharp
ServiceCollection keyedServices = new();
_ = keyedServices.AddKeyedRefitGeneratedClient<IClientApi>(ServiceKey, SampleJsonContext.Default)
    .ConfigureHttpClient(static client => client.BaseAddress = new(BaseUrl))
    .ConfigurePrimaryHttpMessageHandler(() => CreateTransport(expected));
await using ServiceProvider keyedProvider = keyedServices.BuildServiceProvider();
IClientApi regional = keyedProvider.GetRequiredKeyedService<IClientApi>(ServiceKey);
```

The keyed overloads with settings also take an HTTP client name.
This registration names the underlying HTTP client `regional-people`:

```csharp
ServiceCollection keyedSettingsServices = new();
_ = keyedSettingsServices.AddKeyedRefitGeneratedClient<IClientApi>(ServiceKey, JsonSettings, "regional-people")
    .ConfigureHttpClient(static client => client.BaseAddress = new(BaseUrl))
    .ConfigurePrimaryHttpMessageHandler(() => CreateTransport(expected));
await using ServiceProvider keyedSettingsProvider = keyedSettingsServices.BuildServiceProvider();
IClientApi regionalWithSettings = keyedSettingsProvider.GetRequiredKeyedService<IClientApi>(ServiceKey);
```

`regional-people` is the underlying named HTTP client.
Omitting a name lets Refit derive one from the interface and key.
A service key and an HTTP client name have different jobs: the key selects the DI service;
the name selects the factory's HTTP configuration.

## Obtain settings from services

The settings factory returns settings through the service provider.
Refit stores the result in a singleton holder.
The factory is not invoked for every HTTP request.
Do not capture a scoped token service in that singleton settings object.


```csharp
ServiceCollection factoryServices = new();
_ = factoryServices.AddSingleton(JsonSettings);
_ = factoryServices.AddRefitGeneratedClient<IClientApi>(static serviceProvider => serviceProvider.GetRequiredService<RefitSettings>(), "people");
```

`SettingsFor<T>` carries the nullable settings for one interface.
`ISettingsFor` exposes its read-only `Settings` property without the generic interface type.
Refit registers `SettingsFor<T>`, not `ISettingsFor`; register the interface yourself when your code needs it.
The constructor stores the supplied settings reference; it does not copy settings.


```csharp
SettingsFor<IClientApi> holder = settingsProvider.GetRequiredService<SettingsFor<IClientApi>>();
ISettingsFor untypedHolder = settingsProvider.GetRequiredService<ISettingsFor>();
```

Pass a context next to the settings factory to keep your settings and add the context.
Refit sets the context on the settings that the factory returns, so their naming policy and converters apply.
This registration returns snake_case settings and adds `SampleJsonContext`.
When the factory returns `null`, Refit uses the context's own options.

```csharp
ServiceCollection contextFactoryServices = new();
_ = contextFactoryServices.AddSingleton(RefitSettings.SnakeCase());
_ = contextFactoryServices.AddRefitGeneratedClient<IClientApi>(SampleJsonContext.Default, static serviceProvider => serviceProvider.GetRequiredService<RefitSettings>(), "snake-people")
    .ConfigureHttpClient(static client => client.BaseAddress = new(BaseUrl))
    .ConfigurePrimaryHttpMessageHandler(() => CreateTransport(expected));
```

## Resolve authorization per request

`AddAuthorizationHeaderValueProvider` adds a handler that creates a fresh DI scope for each request.
Use its service provider to resolve a token service with a scoped lifetime.
The short sample returns a local token without resolving another service.


```csharp
ServiceCollection tokenServices = new();
StubHttp tokenTransport = CreateTransport(expected);
_ = tokenServices.AddRefitGeneratedClient<IClientApi>(JsonSettings)
    .ConfigureHttpClient(static client => client.BaseAddress = new(BaseUrl))
    .ConfigurePrimaryHttpMessageHandler(() => tokenTransport)
    .AddAuthorizationHeaderValueProvider(static (_, _, _) => ValueTask.FromResult(HandlerToken));
await using ServiceProvider tokenProvider = tokenServices.BuildServiceProvider();
IClientApi secured = tokenProvider.GetRequiredService<IClientApi>();
```

The handler runs for every outgoing request.
It keeps a declared authorization scheme or uses `Bearer` when no scheme exists.
It replaces an existing token.
A null, empty or whitespace result removes the header.
It asynchronously disposes the token-service scope before passing the request to the next handler.
The delegate receives the request and cancellation token.

## Authorization getters and handlers

The [plain supplied-client example](../requests/headers.md#get-a-token-when-sending)
fills a declared scheme only when its token is missing.
`CreateHttpClient` and DI registration also install a handler when
`AuthorizationHeaderValueGetter` is configured.
That handler obtains a token whenever an authorization header exists, including a header with an explicit token.

The runnable reproduction uses a local transport that requires the handler token.


```csharp
using HttpClient client = RestService.CreateHttpClient(BaseUrl, settings);
IClientApi api = RestService.ForGenerated<IClientApi>(client, settings);
await api.AuthorizedAsync();
Console.WriteLine(calls); // 2
```

The missing-token call asks twice: generated preparation and the handler both invoke the getter.
The sample resets its invocation counter and passes a different explicit caller token.


```csharp
await api.ExplicitAsync(CallerToken);
Console.WriteLine(calls); // 1
```

The explicit-token call asks once, and the handler replaces the supplied token.
This differs from generated preparation's preserve-explicit-token rule.
Avoid combining the settings getter with another token provider.
Use a plain supplied client with generated preparation when you need that rule,
or use the scoped provider when a handler should own authorization for every request.

## Reflection registration and existing builders

`AddRefitClient` and `AddKeyedRefitClient` register the reflection request builder.
Install `Refit.Reflection` and use these only when reflection is acceptable.
Their generic overloads carry trimming warnings.
Runtime-`Type` overloads also require dynamic generic construction.
The [separate reflection executable](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Clients/Reflection)
compiles these calls without claiming Native AOT support.


```csharp
ServiceCollection services = new();
_ = services.AddRefitClient<IClientApi>(settings)
    .ConfigureHttpClient(static client => client.BaseAddress = new(BaseUrl));
_ = services.AddRefitClient(ClientInterface, static _ => null, "runtime-selected");
_ = services.AddKeyedRefitClient<IClientApi>(ServiceKey, settings);
```

The `IHttpClientBuilder` overloads attach registrations to an existing named HTTP configuration.
They preserve its name.


```csharp
IHttpClientBuilder http = services.AddHttpClient("existing")
    .ConfigureHttpClient(static client => client.BaseAddress = new(BaseUrl));
_ = http.AddRefitClient<IClientApi>(settings);
_ = http.AddKeyedRefitClient(ClientInterface, ServiceKey, settings);
```

There are no generated-registration overloads on `IHttpClientBuilder`.
Start from the service collection's generated registration and configure its returned builder.

## Registration overloads

Every registration method below returns an `IHttpClientBuilder`.
Generic methods require a class/interface type through `where T : class`.
`settings` can be null; a settings factory can be null or return null to select defaults.
Those defaults do not add your app's generated JSON metadata. With a context, a null factory result selects the context's own options.

| Declaration | Description | Parameters and defaults | Return/value type |
| --- | --- | --- | --- |
| `IServiceCollection.AddRefitClient(Type refitInterfaceType)` | Registers the reflection request builder for `refitInterfaceType` using the default settings and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | [`IServiceCollection`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection) receiver; [`Type`](https://learn.microsoft.com/dotnet/api/system.type) `refitInterfaceType`: Refit interface. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers a reflection-capable client with default settings. |
| `IServiceCollection.AddRefitClient(Type refitInterfaceType, RefitSettings? settings)` | Registers the reflection request builder for `refitInterfaceType` using the supplied fixed settings reference and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `refitInterfaceType`; [`RefitSettings`](settings.md) `settings`: nullable fixed settings. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers a reflection-capable client using those settings. |
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
| `IServiceCollection.AddRefitGeneratedClient<T>(JsonSerializerContext context)` | Registers the generated implementation of `T` on a JSON context's own options, with reflection-based JSON off, and Refit's derived HTTP client name. | `T : class`; [`JsonSerializerContext`](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonserializercontext) `context`: the generated context. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers generated-only `T`. A type the context does not list throws `NotSupportedException` when a call runs. .NET 8 and later. |
| `IServiceCollection.AddRefitGeneratedClient<T>(JsonSerializerContext context, bool allowReflectionFallback)` | Registers the generated implementation of `T` on the context's own options, optionally with a reflection fallback. | `T : class`; `context`; [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean) `allowReflectionFallback`: `true` lets a type the context does not list use reflection-based JSON. Not trim or Native AOT safe. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers generated-only `T`. .NET 8 and later. |
| `IServiceCollection.AddRefitGeneratedClient<T>(JsonSerializerContext context, Func<IServiceProvider, RefitSettings?>? settingsAction)` | Registers the generated implementation of `T`, keeps the settings that `settingsAction` returns and adds the context. | `T : class`; `context`; `settingsAction`: nullable provider settings factory. A `null` result uses the context's own options. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers generated-only `T`. Refit sets the composed serializer on the settings the factory returns. .NET 8 and later. |
| `IServiceCollection.AddRefitGeneratedClient<T>(JsonSerializerContext context, Func<IServiceProvider, RefitSettings?>? settingsAction, string? httpClientName)` | Registers the generated implementation of `T` as above under the supplied HTTP client name. | `T : class`; `context`; `settingsAction`; `httpClientName`: nullable underlying client name. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers generated-only `T` under that HTTP client name. .NET 8 and later. |
| `IServiceCollection.AddRefitGeneratedClient<T>(JsonSerializerContext context, Func<IServiceProvider, RefitSettings?>? settingsAction, string? httpClientName, bool allowReflectionFallback)` | Registers the generated implementation of `T` as above, optionally with a reflection fallback. | `T : class`; `context`; `settingsAction`; `httpClientName`; `allowReflectionFallback`: `true` lets a type no resolver lists use reflection-based JSON. Not trim or Native AOT safe. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers generated-only `T`. .NET 8 and later. |
| `IServiceCollection.AddKeyedRefitGeneratedClient<T>(object? serviceKey)` | Registers the generated implementation of `T` under the required non-null service key using the default settings and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`: non-null DI key. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed generated-only `T` with default settings. |
| `IServiceCollection.AddKeyedRefitGeneratedClient<T>(object? serviceKey, RefitSettings? settings)` | Registers the generated implementation of `T` under the required non-null service key using the supplied fixed settings reference and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`; `settings`: nullable fixed settings. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed generated-only `T` using those settings. |
| `IServiceCollection.AddKeyedRefitGeneratedClient<T>(object? serviceKey, RefitSettings? settings, string? httpClientName)` | Registers the generated implementation of `T` under the required non-null service key using the supplied fixed settings reference and the supplied HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`; `settings`; `httpClientName`. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed generated-only `T` with fixed settings under that HTTP client name. |
| `IServiceCollection.AddKeyedRefitGeneratedClient<T>(object? serviceKey, Func<IServiceProvider, RefitSettings?>? settingsAction)` | Registers the generated implementation of `T` under the required non-null service key using the settings returned by `settingsAction` through DI and Refit’s derived HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`; `settingsAction`: nullable provider settings factory. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed generated-only `T` with settings resolved from DI. |
| `IServiceCollection.AddKeyedRefitGeneratedClient<T>(object? serviceKey, Func<IServiceProvider, RefitSettings?>? settingsAction, string? httpClientName)` | Registers the generated implementation of `T` under the required non-null service key using the settings returned by `settingsAction` through DI and the supplied HTTP client name, then returns the builder for further HTTP configuration. | `T : class`; `serviceKey`; `settingsAction`; `httpClientName`. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed generated-only `T` with DI-provided settings under that HTTP client name. |
| `IServiceCollection.AddKeyedRefitGeneratedClient<T>(object? serviceKey, JsonSerializerContext context)` | Registers the generated implementation of `T` under the required non-null service key on a JSON context's own options, with reflection-based JSON off. | `T : class`; `serviceKey`: non-null DI key; `context`: the generated context. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed generated-only `T`. .NET 8 and later. |
| `IServiceCollection.AddKeyedRefitGeneratedClient<T>(object? serviceKey, JsonSerializerContext context, Func<IServiceProvider, RefitSettings?>? settingsAction)` | Registers the generated implementation of `T` under the key, keeps the settings that `settingsAction` returns and adds the context. | `T : class`; `serviceKey`; `context`; `settingsAction`: nullable provider settings factory. A `null` result uses the context's own options. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed generated-only `T`. .NET 8 and later. |
| `IServiceCollection.AddKeyedRefitGeneratedClient<T>(object? serviceKey, JsonSerializerContext context, Func<IServiceProvider, RefitSettings?>? settingsAction, string? httpClientName, bool allowReflectionFallback)` | Registers the generated implementation of `T` under the key as above, with an HTTP client name and an optional reflection fallback. | `T : class`; `serviceKey`; `context`; `settingsAction`; `httpClientName`; `allowReflectionFallback`: `true` lets a type no resolver lists use reflection-based JSON. Not trim or Native AOT safe. | [`IHttpClientBuilder`](https://learn.microsoft.com/dotnet/api/microsoft.extensions.dependencyinjection.ihttpclientbuilder): registers keyed generated-only `T`. .NET 8 and later. |
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
| `SettingsFor<T>(RefitSettings? settings)` | Constructs a `SettingsFor<T>` holder that keeps the supplied nullable settings reference for interface type `T`. | [`RefitSettings?`](settings.md) `settings`: settings reference or `null`; `T` identifies the interface. | New [`SettingsFor<T>`](dependency-injection.md) holder that stores that settings reference for one registered interface. |
| `SettingsFor<T>.Settings` | Exposes the nullable `RefitSettings` reference associated with the registered Refit interface. | None. | [`RefitSettings`](settings.md)`?`: the settings reference stored for `T`; it can be null to select defaults. |
| `ISettingsFor.Settings` | Exposes the nullable `RefitSettings` reference from a `SettingsFor<T>` instance without exposing its interface type. | None. | [`RefitSettings`](settings.md)`?`: the settings reference stored by that holder. |

| Type | Purpose |
| --- | --- |
| `Refit.HttpClientFactoryExtensions` | Static extension class for service-collection and existing-builder registration methods. |
| `Refit.ISettingsFor` | Interface that exposes a nullable settings reference without a closed Refit interface type. |
| `Refit.SettingsFor<T>` | Generic DI holder that associates a nullable settings reference with interface type `T`. |

Implementation: [`HttpClientFactoryExtensions.ServiceCollection.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit.HttpClientFactory/HttpClientFactoryExtensions.ServiceCollection.cs), [`HttpClientFactoryExtensions.JsonContext.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit.HttpClientFactory/HttpClientFactoryExtensions.JsonContext.cs), [`HttpClientFactoryExtensions.HttpClientBuilder.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit.HttpClientFactory/HttpClientFactoryExtensions.HttpClientBuilder.cs), [`HttpClientFactoryCore.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit.HttpClientFactory/HttpClientFactoryCore.cs), [`ScopedAuthorizationHeaderHandler.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit.HttpClientFactory/ScopedAuthorizationHeaderHandler.cs), [`SettingsFor{T}.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit.HttpClientFactory/SettingsFor%7BT%7D.cs), and [`ISettingsFor.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit.HttpClientFactory/ISettingsFor.cs) at Refit SHA `6f0507fa061f1844a8da6ea92e839b622dfc74ef`.

Settings factories have type `Func<IServiceProvider, RefitSettings?>`.
Names have type `string?`.
Keys have type `object?`, but must contain a non-null value.
There are no runtime-`Type` generated DI registrations.
