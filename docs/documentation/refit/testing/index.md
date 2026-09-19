---
Order: 6
---
# Test a Refit client

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/testing-index/testing-index.csproj).

You should be able to test a service call even when the service is offline. `Refit.Testing`
lets your test choose the reply and inspect the request your client sends. You can check a
successful call, an error or missing data without relying on a running server.

The main helper, `StubHttp`, stands in for the HTTP handler. Your real Refit client still
builds the request, so the test can catch mistakes in its route, headers and body.

## Make your first test

**1. Reference `Refit.Testing` and Refit.** The complete [runnable .NET 10 example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs)
use the packages' source projects. An app can reference the corresponding NuGet packages.
The sample needs no server and no internet connection when it runs.

**2. Declare a model and interface.** The model is the C# shape of the reply body.


```csharp
internal sealed record TestingPerson(int Id, string Name);
```


```csharp
internal interface ITestingApi
{
    [Get("/people/{id}")]
    Task<TestingPerson> GetAsync(int id);

    [Post("/people")]
    Task<TestingPerson> CreateAsync([Body] TestingPerson person);
}
```

**3. Generate JSON metadata.** Metadata describes how to read and write each model.
A `JsonSerializerContext` holds that generated information. `[JsonSerializable]` registers a type.
This context registers `TestingPerson` for both the outgoing body and typed reply.
Register collection or generic body types separately when you add them.


```csharp
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(TestingPerson))]
internal sealed partial class TestingJsonContext : JsonSerializerContext;
```

Set `TypeInfoResolver` to the context. A resolver finds a type's metadata.
Keep this setup when you copy the standalone example. It avoids reflection-based JSON serialization.
Native AOT needs this generated metadata for the request and reply models.
Read [JSON configuration](../serialization/json.md) and [AOT](../aot.md) for registration limits.


```csharp
private static readonly JsonSerializerOptions JsonOptions = new(TestingJsonContext.Default.Options) { TypeInfoResolver = TestingJsonContext.Default };
```


```csharp
private static RefitSettings CreateSettings() => new(new SystemTextJsonContentSerializer(JsonOptions));
```

**4. Choose replies and make real client calls.** A route selects a request by its method and path.
A one-shot expectation must receive one matching request. Each entry below is such an expectation.
`SampleCheck.Equal` throws when a result differs. Your tests can use their assertion library instead.


```csharp
using StubHttp http = new()
{
    {
        Route.Get("/people/{id}"),
        Reply.With(new TestingPerson(1, PersonName))
    },
    {
        Route.Post("/people"),
        Reply.With(new TestingPerson(CreatedPersonId, CreatedName), HttpStatusCode.Created)
    },
};
ITestingApi api = http.CreateGeneratedClient<ITestingApi>(BaseUrl, CreateSettings());
TestingPerson person = await api.GetAsync(1);
SampleCheck.Equal(PersonName, person.Name);
_ = await api.CreateAsync(new(CreatedPersonId, CreatedName));
TestingPerson? sent = await http.LastRequestBodyAsync<TestingPerson>();
SampleCheck.Equal(CreatedName, sent?.Name);
SampleCheck.Equal(sent, await http.RequestBodyAsync<TestingPerson>(1));
SampleCheck.Equal(HttpMethod.Post, http.Requests[1].Method);
Verify(http);
```

The fences are excerpts of the complete example. Its files include imports, assertions and a runner.
Its constants name Ada, Grace, the created person's ID 2 and the base URL `https://people.example`.
Use `System.Net`, `System.Text.Json`, `System.Text.Json.Serialization`, `Refit` and `Refit.Testing`
when assembling the standalone code in your app.

## Choose the test boundary

| Topic | Use it to |
| --- | --- |
| [Routes](routes.md) | Check paths, queries, headers and bodies. |
| [Replies](replies.md) | Supply typed JSON, text, bytes or a custom response. |
| [Verification](verification.md) | Inspect requests and check every expectation arrived. |
| [Network faults](faults.md) | Check delays, HTTP failures and connection failures. |
| [Response stubs](response-stubs.md) | Test code that accepts `IApiResponse<T>` directly. |

Use handler tests for an app service that wraps a Refit interface.
Use a response stub when your code only consumes a reply wrapper.
For a ReactiveUI app, keep HTTP calls asynchronous and pass their results into your view model.
See [ReactiveUI commands](../../handbook/commands/index.md) for managing asynchronous work from the UI.

## Settings and client factories

`new StubHttp()` creates an empty table. `new StubHttp(behavior)` also enables network simulation.
`Add(route, reply)` appends an entry and rejects null arguments. Collection initializers call `Add`.
The handler implements `IEnumerable<RouteMatcher>` and `IEnumerable`; both enumerate a snapshot of its routes.

`ToSettings()` creates default settings. `ToSettings(baseSettings)` changes and returns your supplied instance.
It replaces `HttpMessageHandlerFactory` with a lambda returning this handler.
It also adopts `ContentSerializer` for typed replies and request inspection.
Other settings keep their values. Do not share one handler between clients with different serializers.
A later settings call changes the serializer for that handler, including captured-body inspection.

`CreateGeneratedClient<T>(hostUrl)` uses default settings.
`CreateGeneratedClient<T>(hostUrl, baseSettings)` keeps your serializer and other settings.
Both require a registered generated implementation of the Refit interface.
Refit's source generator creates and registers that implementation during your build.
They throw `InvalidOperationException` when none is registered.
Use the settings overload and generated JSON context in trimmed or AOT apps.

`CreateClient<T>(hostUrl)` and `CreateClient<T>(hostUrl, baseSettings)` use `RestService.For<T>`.
That path permits runtime reflection and carries a trimming warning on modern targets.
Trimming removes code the build believes is unused. Prefer the generated factories for new tests.
Creating a client does not make an HTTP request or prove that its JSON configuration is complete.


```csharp
using StubHttp wiring = new();
RefitSettings fresh = wiring.ToSettings();
SampleCheck.Equal(wiring, fresh.HttpMessageHandlerFactory!());
RefitSettings supplied = CreateSettings();
SampleCheck.Equal(supplied, wiring.ToSettings(supplied));
ITestingApi generated = wiring.CreateGeneratedClient<ITestingApi>(BaseUrl);
SampleCheck.Equal(true, generated is not null);
SampleCheck.Equal(wiring, supplied.HttpMessageHandlerFactory!());
```

The legacy factories run in the JIT version of the example. The native host excludes them.
Their settings overload has generated JSON metadata but the client factory itself can fall back to reflection.
The complete example calls both clients through local routes and checks that they return the same person.
Use generated factories for native execution.


```csharp
using StubHttp http = new();
http.Add(Route.Get("/people/1"), Reply.Json("{\"id\":1,\"name\":\"Ada\"}"));
ITestingApi defaults = http.CreateClient<ITestingApi>("https://people.example");
TestingPerson first = await defaults!.GetAsync(1);
RefitSettings settings = new(new SystemTextJsonContentSerializer(TestingJsonContext.Default.Options));
http.Add(Route.Get("/people/1"), Reply.Json("{\"id\":1,\"name\":\"Ada\"}"));
ITestingApi configured = http.CreateClient<ITestingApi>("https://people.example", settings);
TestingPerson second = await configured!.GetAsync(1);
```
