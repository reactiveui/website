---
Order: 6
---
# Test a Refit client

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/testing-index/testing-index.csproj).

You should be able to test a service call even when the service is offline. `Refit.Testing`
lets your test choose the reply and inspect the request your client sends. You can check a
successful call, an error or missing data without relying on a running server.

The main helper, `StubHttp`, stands in for the HTTP handler. A handler is the object that `HttpClient`
passes each request to. `StubHttp` is a stub: a stand-in that answers with replies you set up and never
touches the network. Your real Refit client still builds the request, so the test can catch mistakes in
its route, headers and body.

## Pick your test framework

`Refit.Testing` works with any test framework. It has no test attributes or assert methods of its own.
The samples on these pages note each result in a comment. To see the same checks written as full tests,
pick the page for the framework your project uses:

- [xUnit](xunit.md)
- [NUnit](nunit.md)
- [MSTest](mstest.md)
- [TUnit](tunit.md)

Each page covers reading a reply, checking what your app sent, handling a 404, a slow or broken network,
and streaming data in and out.

## Make your first test

**1. Reference `Refit.Testing` and Refit.** Add both NuGet packages to your test project.
The runnable .NET 10 example, [`Testing.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs),
references their source projects instead. It needs no server and no internet connection when it runs.

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


**4. Add the settings members.** Put these two members in your test class.
`CreateSettings()` returns new settings each time.
Give each handler its own settings, because `StubHttp` changes the settings you pass it.

```csharp
private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions(TestingJsonContext.Default.Options) { TypeInfoResolver = TestingJsonContext.Default };

private static RefitSettings CreateSettings() => new RefitSettings(new SystemTextJsonContentSerializer(JsonOptions));
```

**5. Stub the calls and send them.** You want to send a real Refit request with no server, then see
what your client sent. Fill a `StubHttp` with entries. Each entry pairs a route with a reply.
A route is a rule that picks which requests the entry answers, here by HTTP method and path.
A reply is what the stub sends back. By default a route is one-shot: it answers one matching request,
then it is used up.

[//]: # "excerpt:Testing/Testing.cs#ShowClientAsync"

```csharp
using StubHttp http = new StubHttp
{
    {
        Route.Get("/people/{id}"),
        Reply.With(new TestingPerson(1, "Ada"))
    },
    {
        Route.Post("/people"),
        Reply.With(new TestingPerson(2, "Grace"), HttpStatusCode.Created)
    },
};
ITestingApi api = http.CreateGeneratedClient<ITestingApi>("https://api.example.com", CreateSettings());

TestingPerson person = await api.GetAsync(1); // person.Name == "Ada"
TestingPerson created = await api.CreateAsync(new TestingPerson(2, "Grace")); // created.Name == "Grace"
TestingPerson? sent = await http.LastRequestBodyAsync<TestingPerson>(); // sent?.Name == "Grace"
```

`Reply.With` turns a model into a JSON body. It uses the serializer from the settings you passed to
`CreateGeneratedClient`. The client calls go through the stub, so Refit builds real requests.

The stub also captures each request. To capture a request is to keep a copy of its body, so you can read
it after the call ends. `LastRequestBodyAsync<T>()` reads the latest captured body back as a model.
`RequestBodyAsync<T>(index)` reads the body at a zero-based position in `Requests`.

Finish each test with `http.VerifyAllCalled()`. It throws when a one-shot route received no request.
[Verification](verification.md) covers it in full. Watch out for two rules:

- A request that no route matches throws `InvalidOperationException`. The stub does not return an automatic 404.
- A one-shot route answers once. A second `GetAsync(1)` here would find no route. Add another entry, or make
  the route reusable as [Routes](routes.md) shows.

The page's code needs `using` directives for `System.Net`, `System.Text.Json`, `System.Text.Json.Serialization`,
`Refit` and `Refit.Testing`.

## Point settings at the stub

Some code takes a `RefitSettings` and builds its own client. You want that code to talk to the stub instead
of the real network. The [`Testing.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs)
sample shows the three ways to wire it up.

[//]: # "excerpt:Testing/Testing.cs#ShowSettingsWiringAsync"

```csharp
using StubHttp http = new StubHttp();
RefitSettings fresh = http.ToSettings(); // fresh.HttpMessageHandlerFactory!() == http
RefitSettings supplied = CreateSettings();
RefitSettings wired = http.ToSettings(supplied); // wired is the same instance as supplied

// Uses default settings, which replaces the serializer wired in above: the stub keeps one, and the last call wins.
ITestingApi api = http.CreateGeneratedClient<ITestingApi>("https://api.example.com");
```

- `ToSettings()` creates new settings. Their `HttpMessageHandlerFactory` returns the stub.
- `ToSettings(supplied)` changes your settings in place and returns the same instance.
- `CreateGeneratedClient<T>(hostUrl)` with no settings creates default settings and wires them the same way.

Each of these calls also makes the stub adopt the settings' serializer. The stub holds one serializer, and
the last call wins. In the sample, the last call uses default settings. The stub now writes typed replies and
reads captured bodies with the default serializer, not the one from `CreateSettings()`.
Wire one set of settings to each stub.

## Use a reflection client

Your interface may have no generated client, for example in a project without Refit's source generator.
`CreateClient<T>` builds the client at run time with reflection instead. Reflection means reading type
information while the app runs. The sample is in
[`TestingReflection.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/TestingReflection.cs).

[//]: # "excerpt:Testing/TestingReflection.cs#RunAsync"

```csharp
using StubHttp http = new StubHttp { { Route.Get("/people/1"), Reply.Json("{\"id\":1,\"name\":\"Ada\"}") } };
ITestingApi defaults = http.CreateClient<ITestingApi>("https://api.example.com");
TestingPerson first = await defaults.GetAsync(1); // first.Name == "Ada"

RefitSettings settings = new RefitSettings(new SystemTextJsonContentSerializer(TestingJsonContext.Default.Options));

// each route answers once, so add another for the second call
http.Add(Route.Get("/people/1"), Reply.Json("{\"id\":1,\"name\":\"Ada\"}"));
ITestingApi configured = http.CreateClient<ITestingApi>("https://api.example.com", settings);
TestingPerson second = await configured.GetAsync(1); // second == first
```

`CreateClient<T>(hostUrl)` uses default settings. `CreateClient<T>(hostUrl, settings)` keeps your serializer.
`Reply.Json` sends raw JSON text, so the reply itself needs no model metadata.
The route is one-shot, so the sample adds a second entry before the second call.

Both overloads call `RestService.For<T>`. On modern targets they carry a trimming warning. Trimming removes
code the build believes is unused, and reflection can need that code. The example's Native AOT host leaves
this sample out. Prefer `CreateGeneratedClient<T>` for new tests.

## Choose the test boundary

| Topic | Use it to |
| --- | --- |
| [Routes](routes.md) | Check paths, queries, headers and bodies. |
| [Replies](replies.md) | Supply typed JSON, text, bytes or a custom response. |
| [Verification](verification.md) | Inspect requests and check every expectation arrived. |
| [Network faults](faults.md) | Check delays, HTTP failures and connection failures. |
| [Streams, uploads and time](streaming.md) | Pace streamed items, drop or stall a body, read an upload yourself and control simulated time. |
| [Response stubs](response-stubs.md) | Test code that accepts [`IApiResponse<T>`](../results/responses.md) directly. |

Use handler tests for an app service that wraps a Refit interface.
Use a response stub when your code only consumes a reply wrapper.
For a ReactiveUI app, keep HTTP calls asynchronous and pass their results into your view model.
See [ReactiveUI commands](../../reactiveui/handbook/commands/index.md) for managing asynchronous work from the UI.

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
That path uses reflection and carries a trimming warning on modern targets.
[Use a reflection client](#use-a-reflection-client) shows both. Prefer the generated factories for new tests.
Creating a client does not make an HTTP request or prove that its JSON configuration is complete.

## API reference

| API | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`StubHttp`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Declarative `HttpMessageHandler` for Refit tests; stores route matchers and their replies, records requests, and supports one-shot, reusable and fallback routes. | None | Handler type implementing [`IEnumerable<RouteMatcher>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable-1). |
| [`StubHttp()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Starts a handler with no expected routes. | None | Creates an empty route table using the default JSON content serializer. |
| [`StubHttp(NetworkBehavior behavior)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Starts an empty handler and enables network-fault simulation. | `behavior`: [`NetworkBehavior`](faults.md) applied to each matched request | Creates an empty route table with the supplied behavior. |
| [`StubHttp.Requests`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Exposes requests received by the handler in arrival order. | Get-only [`IReadOnlyList<HttpRequestMessage>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1) | Returns a live read-only view, including unmatched and failed requests. |
| [`StubHttp.Behavior`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Enables, replaces or disables simulated network conditions. | Nullable [`NetworkBehavior`](faults.md), get/set; default `null` | Gets or sets behavior; `null` skips simulation. |
| [`StubHttp.RequestCapture`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Chooses whether request bodies are read before matching and recorded. | [`RequestCapture`](streaming.md#test-a-streaming-upload), get/set; default `RequestCapture.Full` | `None` and `Bounded(maxBytes)` leave the body for your reply code; `null` throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception). |
| [`StubHttp.TimeProvider`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Sets the clock for simulated delays and verification timeouts. | [`TimeProvider`](https://learn.microsoft.com/dotnet/api/system.timeprovider), get/set; default `TimeProvider.System` | A fake clock makes the test move time forward itself. See [simulated time](streaming.md#control-simulated-time). `null` throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception). |
| [`StubHttp.Add(RouteMatcher route, StubResponse response)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Adds a route and the reply returned when it matches; collection initializers call this method. | `route`: [`RouteMatcher`](routes.md); `response`: [`StubResponse`](replies.md) | Returns [`void`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void); rejects null arguments and tracks one-shot expectations. |
| [`StubHttp.ToSettings()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Creates settings that route a Refit client through this handler. | None | Returns new [`RefitSettings`](../clients/settings.md) whose handler factory returns this handler. |
| [`StubHttp.ToSettings(RefitSettings baseSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Reuses supplied settings and points them at this handler. | `baseSettings`: [`RefitSettings`](../clients/settings.md) to update | Returns the same settings after replacing its handler factory and adopting its serializer. |
| [`StubHttp.CreateClient<T>(string hostUrl)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Creates a reflection-based Refit client using default settings. | `hostUrl`: base address | Returns `T` from `RestService.For<T>`; carries runtime reflection/trimming requirements. |
| [`StubHttp.CreateClient<T>(string hostUrl, RefitSettings baseSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Creates a reflection-based client while retaining supplied serializer and URL settings. | `hostUrl`: base address; `baseSettings`: [`RefitSettings`](../clients/settings.md) to route through this handler | Returns `T` from `RestService.For<T>` after rewiring the supplied settings. |
| [`StubHttp.CreateGeneratedClient<T>(string hostUrl)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Creates a source-generated Refit client using default settings. | `hostUrl`: base address | Returns generated client `T`; throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) when no generated implementation is registered. |
| [`StubHttp.CreateGeneratedClient<T>(string hostUrl, RefitSettings baseSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Creates a source-generated client while retaining supplied settings. | `hostUrl`: base address; `baseSettings`: [`RefitSettings`](../clients/settings.md) to route through this handler | Returns generated client `T`; throws [`InvalidOperationException`](https://learn.microsoft.com/dotnet/api/system.invalidoperationexception) when no generated implementation is registered. |
| [`StubHttp.SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) *(protected override)* | Records, matches and consumes an incoming request, applies network behavior, then builds its configured reply. | `request`: [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage); `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken) | Returns [`Task<HttpResponseMessage>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1); throws when no route matches or cancellation is requested. |
