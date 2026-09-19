---
Order: 3
---
# Dependency injection

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/clients-dependency-injection/clients-dependency-injection.csproj).

Several parts of your app may need the same API client. Registering it with dependency
injection lets those parts ask for the interface in their constructors. You can keep the
service address, request settings and HTTP handlers together in the app's setup code.

Refit's integration uses `IHttpClientFactory` to manage the HTTP handlers behind those clients.
This page shows the initial registration, then how to give clients different names or keys.

Install `Refit.HttpClientFactory` beside Refit.
Use `AddRefitGeneratedClient<T>` with generated JSON metadata for clients that support AOT.
This registration binds the interface to Refit's emitted implementation. Keep the reflection
registration in the separate section for applications that accept runtime metadata.

## Register and resolve a generated client

**1. Prepare settings.** Use the generated context and shared `JsonSettings` from
[client creation](creation.md#reuse-your-http-client).

**2. Register the client.** Import `Microsoft.Extensions.DependencyInjection`.
`CreateTransport(expected)` is the sample's local handler factory.
In an app, keep the normal transport or configure the handler your app needs.

**3. Resolve the interface and call it.** This console example owns and disposes its service provider.
An application host owns the provider for a hosted app.


```csharp
ServiceCollection services = new();
_ = services.AddSingleton<ISettingsFor>(new SettingsFor<IClientApi>(JsonSettings));
_ = services.AddRefitGeneratedClient<IClientApi>(JsonSettings)
    .ConfigureHttpClient(static client => client.BaseAddress = new(BaseUrl))
    .ConfigurePrimaryHttpMessageHandler(() => CreateTransport(expected));
await using ServiceProvider provider = services.BuildServiceProvider();
IClientApi api = provider.GetRequiredService<IClientApi>();
Person person = await api.ReadAsync();
```

The API implementation is transient: each resolution can create a new implementation.
The settings holder is a singleton: its settings are shared by clients for that registration.
The factory manages the HTTP handler's lifetime.
Configure the returned builder to set the base address, handlers or your app's HTTP resilience pipeline.
An unavailable generated implementation fails when the client is resolved.

## Distinguish clients with a key

A key lets you register the same interface for different destinations or policies.
Resolve it with `GetRequiredKeyedService<T>` and the same key.
Refit rejects null keys.


```csharp
ServiceCollection keyedServices = new();
_ = keyedServices.AddKeyedRefitGeneratedClient<IClientApi>(ServiceKey, JsonSettings, "regional-people")
    .ConfigureHttpClient(static client => client.BaseAddress = new(BaseUrl))
    .ConfigurePrimaryHttpMessageHandler(() => CreateTransport(expected));
await using ServiceProvider keyedProvider = keyedServices.BuildServiceProvider();
IClientApi regional = keyedProvider.GetRequiredKeyedService<IClientApi>(ServiceKey);
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
The constructor stores the supplied settings reference; it does not copy settings.


```csharp
SettingsFor<IClientApi> holder = provider.GetRequiredService<SettingsFor<IClientApi>>();
ISettingsFor untypedHolder = provider.GetRequiredService<ISettingsFor>();
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

Every method below returns `IHttpClientBuilder`.
Generic methods require a class/interface type through `where T : class`.
`settings` can be null; a settings factory can be null or return null to select defaults.
Those defaults do not add your app's generated JSON metadata.

| Receiver and method | Available arguments |
| --- | --- |
| `IServiceCollection.AddRefitGeneratedClient<T>` | None; settings; settings + name; settings factory; settings factory + name. |
| `IServiceCollection.AddKeyedRefitGeneratedClient<T>` | Key followed by the same five shapes. |
| `IServiceCollection.AddRefitClient<T>` | None; settings; settings + name; settings factory; settings factory + name. |
| `IServiceCollection.AddRefitClient(Type, ...)` | Type followed by the same five shapes. |
| `IServiceCollection.AddKeyedRefitClient<T>` | Key followed by the same five shapes. |
| `IServiceCollection.AddKeyedRefitClient(Type, ...)` | Type, then key, followed by the same five shapes. |
| `IHttpClientBuilder.AddRefitClient<T>` | None; settings; settings factory. |
| `IHttpClientBuilder.AddRefitClient(Type, ...)` | Type followed by those three shapes. |
| `IHttpClientBuilder.AddKeyedRefitClient<T>` | Key followed by those three shapes. |
| `IHttpClientBuilder.AddKeyedRefitClient(Type, ...)` | Type, then key, followed by those three shapes. |
| `IHttpClientBuilder.AddAuthorizationHeaderValueProvider` | One token delegate taking service provider, request and cancellation token and returning `ValueTask<string>`. |

Settings factories have type `Func<IServiceProvider, RefitSettings?>`.
Names have type `string?`.
Keys have type `object?`, but must contain a non-null value.
There are no runtime-`Type` generated DI registrations.
