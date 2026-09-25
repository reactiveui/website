---
Order: 3
---
# Inspect requests and verify expectations

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/testing-verification/testing-verification.csproj).

A test can pass without ever making the API call you meant to check. Verification catches
that. It confirms that every request you expected actually reached the test handler.
You can also inspect the captured requests to see which values your app sent.

Use these checks alongside checks on the returned result. A received request proves
the call happened. Your result checks prove what the app did with the reply.

Each route you add to a `StubHttp` handler is a one-shot expectation unless you mark it reusable or fallback.
A one-shot route answers one request and is then consumed. Verification looks only at one-shot routes.

## Wait for an expected request

How do you wait for a route to be called, and fail loudly when one never is?

**1. Build the complete route table before sending.** Reusable and fallback routes do not have to be called.

**2. Start asynchronous verification when a request may arrive later.** `VerifyAllCalledAsync()`
waits for up to one second. `VerifyAllCalledAsync(timeout)` waits for up to your `TimeSpan`.
A zero timeout checks at once. Neither overload accepts a cancellation token.

**3. Send the request and await both tasks.** The verification task completes once every one-shot route is consumed.
It fails with `InvalidOperationException` when the timeout passes first.
The error message lists each missing method and template.

The sample below starts verification before the send. It then shows a second handler whose route nobody calls.
Source: [`Testing.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs).

[//]: # "excerpt:Testing/Testing.cs#ShowVerificationAsync"

```csharp
using StubHttp http = new StubHttp { { Route.Get("/expected"), Reply.Text("received") } };
using HttpClient httpClient = new HttpClient(http, disposeHandler: false);
Task waiting = http.VerifyAllCalledAsync(TimeSpan.FromSeconds(1));
bool notYetCalled = waiting.IsCompleted; // false: nothing has called /expected yet
using HttpResponseMessage response = await httpClient.GetAsync(new Uri("https://api.example.com/expected"));
await waiting; // now completes, because the route above was called

using StubHttp unmet = new StubHttp { { Route.Get("/unmet"), Reply.Status(HttpStatusCode.OK) } };
Task unmetVerification = unmet.VerifyAllCalledAsync(TimeSpan.Zero);
try
{
    await unmetVerification; // throws: nothing ever called /unmet
}
catch (InvalidOperationException error)
{
    Console.WriteLine(error.Message); // "1 expected request(s) were not made:\n  - GET /unmet"
}
```

`waiting.IsCompleted` is `false` before the send, because nothing has called `/expected` yet.
`await waiting` completes after the call. On the second handler, the zero timeout checks at once and throws.

`VerifyAllCalled()` checks immediately and throws the same error. It does not wait and does not block on a task.
Call it after you await the requests under test.

The timeout runs on `StubHttp.TimeProvider`. Give the handler a fake clock, and the verification fails when you
move the clock past the timeout. The test does not wait in real time.
See [control simulated time](streaming.md#control-simulated-time) for that sample.

### When a route counts as called

The handler consumes a route as soon as the request matches it. That happens before any simulated delay,
injected failure or reply code runs. So a request that later fails can still satisfy verification.

The protected `SendAsync(request, cancellationToken)` runs this logic when you send through `HttpClient` or `HttpMessageInvoker`.
It honors cancellation while matching reads a body and during a simulated delay.
It checks the token again after it builds a normal reply.
The first read of the request body takes no token, and neither do match predicates.
Reply code receives the token only through the [`Reply.From` overload](replies.md#json-text-and-custom-content) that takes one.

## Inspect sent models

`Requests` is a live read-only list of the request objects, in the order they arrived.
It includes unmatched requests and failed sends. A client can dispose a recorded request.
Read the details you need after the sends finish. Do not enumerate the list while another request is arriving.

`LastRequestBodyAsync<T>()` reads the latest captured body with the handler's serializer.
It throws `InvalidOperationException` when the handler has recorded no requests.
`RequestBodyAsync<T>(index)` does the same for a zero-based request index.
A negative or unavailable index throws `ArgumentOutOfRangeException`.
The [first test](index.md#make-your-first-test) shows both methods.

Both methods read what the [request-capture policy](streaming.md#test-a-streaming-upload) recorded.
The default policy, `RequestCapture.Full`, records every body. `RequestCapture.None` records nothing, so both methods
return `default`. Under `RequestCapture.Bounded`, they throw `InvalidOperationException` for a body that was cut off or not read to the end.

A request without a body returns `default`. So does a body that could not be buffered.
Captured empty or malformed JSON goes to the serializer, which can throw.

### Captured bodies are read back as UTF-8

Why does `LastRequestBodyAsync` throw for a body that was not sent as UTF-8?
The handler re-reads every captured body as UTF-8 text before it deserializes it.
A body sent in another encoding, such as UTF-16, turns into bytes the serializer cannot parse.
Source: [`Testing.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs).

[//]: # "excerpt:Testing/Testing.cs#ShowCapturedEncodingAsync"

```csharp
RefitSettings settings = CreateSettings();
using StubHttp http = new StubHttp { { Route.Post("/encoded"), Reply.Status(HttpStatusCode.OK) } };
settings = http.ToSettings(settings);
using StringContent original = new StringContent("{\"id\":1,\"name\":\"Ada\"}", Encoding.Unicode, "application/json");
TestingPerson? expected = await settings.ContentSerializer.FromHttpContentAsync<TestingPerson>(original); // expected?.Name == "Ada"
using HttpClient httpClient = new HttpClient(http, disposeHandler: false);
using HttpResponseMessage reply = await httpClient.PostAsync(new Uri("https://api.example.com/encoded"), original); // reply.StatusCode == OK

Task<TestingPerson?> corruptedRead = http.LastRequestBodyAsync<TestingPerson>();
try
{
    await corruptedRead; // throws: StubHttp always re-reads captured bytes as UTF-8
}
catch (JsonException error)
{
    Console.WriteLine(error.Message); // "'0x00' is an invalid start of a property name. Expected a '\"'. Path: $ | LineNumber: 0 | BytePositionInLine: 1."
}
```

The serializer reads the original UTF-16 content and gets `"Ada"`. The handler still replies `OK` to the post.
Only the typed read of the captured body fails, with a `JsonException`.
This is the current behaviour. Keep typed inspection to UTF-8 JSON. For a body in another encoding,
read the raw content from `Requests` instead.

For a standalone typed test, register the request model on your JSON context and set
`TypeInfoResolver` before you create the generated client. Raw request inspection needs no JSON context.

## Verification API reference

These members belong to [`StubHttp`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs).
It is an [`HttpMessageHandler`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmessagehandler)
that records requests and consumes one-shot routes. None of these members accepts a cancellation token.
`T` is the model type to read with the handler's serializer.

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| [`VerifyAllCalled()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Checks immediately that every one-shot route has been consumed. | None. | `void`; throws [`InvalidOperationException`][invalid-operation] immediately if a one-shot route is missing. |
| [`VerifyAllCalledAsync()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Waits for one-shot routes using the handler's default one-second timeout. | None. | [Task]: completes when all one-shot routes are consumed, or faults with the missing-route error after one second. |
| [`VerifyAllCalledAsync(TimeSpan timeout)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Waits for one-shot routes using a timeout you choose. | [TimeSpan] `timeout`: maximum wait; zero checks immediately. | [Task]: completes when all one-shot routes are consumed, or faults with the missing-route error after the timeout. See [routes added after verification](#routes-added-after-verification). |
| [`LastRequestBodyAsync<T>()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Deserializes the most recently captured request body as `T` with the handler's serializer. | None. | [`Task<T?>`][task-result]: latest captured body as `T`, or `default` for absent or unbufferable content. Throws [`InvalidOperationException`][invalid-operation] if there are no requests. |
| [`RequestBodyAsync<T>(int index)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Deserializes the captured body at a recorded request position with the handler's serializer. | [int] `index`: zero-based request position. | [`Task<T?>`][task-result]: selected captured body as `T`, or `default` for absent or unbufferable content. Throws [`ArgumentOutOfRangeException`][argument-out-of-range] for an invalid index. |

| Property | Type | Value |
| --- | --- | --- |
| [`Requests`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | [`IReadOnlyList<HttpRequestMessage>`][requests] | Get-only live list of recorded [HttpRequestMessage] objects in arrival order, including unmatched requests and failed sends. |

Source: [StubHttp.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs).

[Task]: https://learn.microsoft.com/dotnet/api/system.threading.tasks.task
[task-result]: https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1
[TimeSpan]: https://learn.microsoft.com/dotnet/api/system.timespan
[int]: https://learn.microsoft.com/dotnet/api/system.int32
[requests]: https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist-1
[HttpRequestMessage]: https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage
[invalid-operation]: https://learn.microsoft.com/dotnet/api/system.invalidoperationexception
[argument-out-of-range]: https://learn.microsoft.com/dotnet/api/system.argumentoutofrangeexception

## Routes added after verification

What happens if you pass verification, then add another expected route?
You might expect the next `VerifyAllCalledAsync` call to wait for the new route. It does not.
Once asynchronous verification has passed, the handler remembers that success.
The next asynchronous verification fails at once when the new route is missing, even with a positive timeout.
Source: [`Testing.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs).

[//]: # "excerpt:Testing/Testing.cs#ShowAdditionalRouteAsync"

```csharp
using StubHttp http = new StubHttp { { Route.Get("/first"), Reply.Status(HttpStatusCode.OK) } };
using HttpClient httpClient = new HttpClient(http, disposeHandler: false);
using HttpResponseMessage first = await httpClient.GetAsync(new Uri("https://api.example.com/first"));
await http.VerifyAllCalledAsync(); // passes: /first was called

http.Add(Route.Get("/second"), Reply.Status(HttpStatusCode.OK));
Task verification = http.VerifyAllCalledAsync(TimeSpan.FromSeconds(1));
bool immediatelyDone = verification.IsCompleted; // true: a route added after the previous success already fails the wait
try
{
    await verification;
}
catch (InvalidOperationException error)
{
    Console.WriteLine(error.Message); // "1 expected request(s) were not made:\n  - GET /second"
}

using HttpResponseMessage second = await httpClient.GetAsync(new Uri("https://api.example.com/second"));
```

`immediatelyDone` is `true` before the one-second timeout has passed. The error names `GET /second`.
The route itself still works: the last line sends to `/second` and gets its reply.

This is the current behaviour. Build the full route table before any verification passes.
For a separate scenario, create another handler.

## Two requests on one one-shot route

If two requests match a one-shot route at the same instant, which reply do they get?
Both of them get it. The handler picks a matching route and consumes it in two separate steps.
Two overlapping requests can both pick the same unused one-shot route before either consumes it.
Source: [`Testing.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs).

[//]: # "excerpt:Testing/Testing.cs#ShowOneShotRaceAsync"

```csharp
int arrivals = 0;
using SemaphoreSlim bothMatching = new SemaphoreSlim(0, 2); // holds both requests inside matching until both arrive, so they match at the same instant
RouteMatcher route = new RouteMatcher
{
    Template = "/one-shot",
    WhereAsync = async request =>
    {
        if (Interlocked.Increment(ref arrivals) == 2) // the second arrival releases both waiting requests together
        {
            bothMatching.Release(2);
        }

        await bothMatching.WaitAsync();
        return true;
    },
};
using StubHttp http = new StubHttp { { route, Reply.Text("accepted") } };
using HttpClient httpClient = new HttpClient(http, disposeHandler: false);
Uri address = new Uri("https://api.example.com/one-shot");
Task<HttpResponseMessage> first = httpClient.GetAsync(address);
Task<HttpResponseMessage> second = httpClient.GetAsync(address);
using HttpResponseMessage firstReply = await first; // both requests share the one route's single reply
using HttpResponseMessage secondReply = await second;
```

The sample makes the overlap certain rather than relying on timing. Its `WhereAsync` predicate holds each request
until both have arrived, then lets both continue. A `SemaphoreSlim` does the holding. A semaphore is a counter that
blocks a waiter until another piece of code releases it. Both requests then receive the `"accepted"` reply.
Verification counts the route once and passes.

This is the current behaviour. One one-shot route can answer more than one request.
Send one-shot scenarios one at a time. Use a reusable route when independent requests may overlap.
