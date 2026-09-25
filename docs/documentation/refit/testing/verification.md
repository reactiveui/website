---
Order: 3
---
# Inspect requests and verify expectations

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/testing-verification/testing-verification.csproj).

A test can pass without ever making the API call you meant to check. Verification catches
that by confirming that the requests you expected actually reached the test handler.
You can also inspect the captured requests to see which values your app sent.

Use these checks alongside assertions on the returned result. Receiving a request proves
the call happened; your result assertions check what the app did with the reply.

## Wait for an expected request

**1. Create the complete table before sending.** Every ordinary route is a one-shot expectation.
Reusable and fallback routes do not have to be called.

**2. Start asynchronous verification when a request may arrive later.** `VerifyAllCalledAsync()`
waits for up to one second. `VerifyAllCalledAsync(timeout)` uses your `TimeSpan`.
Neither overload accepts a cancellation token. A zero timeout performs an immediate check.
The timeout runs on `StubHttp.TimeProvider`. Set a fake clock to fail the check without waiting.
See [simulated time](streaming.md#control-simulated-time).

**3. Send and await the call.** The [runnable verification example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs)
asserts that verification is pending before it sends, then awaits both the response and verification.
It also proves that an unmet expectation throws `InvalidOperationException`.
The error lists the missing methods and templates.

`VerifyAllCalled()` checks immediately and throws the same missing-route error.
It does not wait or block on a task. Call it after awaiting the requests under test.
A route is consumed before simulated delay, injected failure and responder execution.
Even a request that later fails can satisfy verification.
The protected `SendAsync(request, cancellationToken)` implements this behavior through `HttpClient` or `HttpMessageInvoker`.
It honors cancellation during matching body reads and simulated delay, and checks again after building a normal reply.
Initial request buffering has no token. Predicates and responders receive no token either.

## Inspect sent models

`Requests` is a live read-only list of request objects in arrival order.
It includes unmatched requests and failed sends. A client can dispose a recorded request.
Read stable details after the sends finish, and avoid enumerating the list while another request arrives.

`LastRequestBodyAsync<T>()` reads the latest buffered body using the adopted serializer.
It throws `InvalidOperationException` when there are no recorded requests.
`RequestBodyAsync<T>(index)` does the same for a zero-based request index.
A negative or unavailable index throws `ArgumentOutOfRangeException`.
The [first test](index.md#make-your-first-test) demonstrates both names.

These methods read what the [request-capture policy](streaming.md#test-a-streaming-upload) recorded.
The default policy, `RequestCapture.Full`, records every body. `RequestCapture.None` records nothing, so they return default.

Absent content returns default. A body that cannot be buffered also returns default.
Captured empty or malformed JSON is passed to the serializer and can throw.
Buffering decodes bytes as UTF-8 and stores the media type without its charset.
Keep typed inspection to UTF-8 JSON. It can corrupt bodies using another text encoding.
The complete example demonstrates the problem with UTF-16 JSON: the configured serializer reads
the original content successfully, but typed inspection of the captured body throws `JsonException`.
For a standalone typed test, repeat the JSON context registration for the request model and set
`TypeInfoResolver` before creating the generated client. Raw request inspection needs no JSON context.

## Verification API reference

These members belong to [`StubHttp`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs),
a declarative [`HttpMessageHandler`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmessagehandler)
that records requests and consumes one-shot route expectations. None accepts a cancellation token.
`T` is the model type to read using the adopted serializer.

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| [`VerifyAllCalled()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Checks immediately that every one-shot route has been consumed. | None. | `void`; throws [`InvalidOperationException`][invalid-operation] immediately if a one-shot expectation is missing. |
| [`VerifyAllCalledAsync()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Waits for one-shot routes using the handler's default one-second timeout. | None. | [Task]: completes when all expectations are consumed, or faults with the missing-route error after one second. |
| [`VerifyAllCalledAsync(TimeSpan timeout)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Waits for one-shot routes using a caller-selected timeout. | [TimeSpan] `timeout`: maximum wait; zero checks immediately. | [Task]: completes when expectations are consumed, or faults with the missing-route error after the timeout. See the completed-verification limitation below. |
| [`LastRequestBodyAsync<T>()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Deserializes the most recently captured request body as `T` with the adopted serializer. | None. | [`Task<T?>`][task-result]: latest captured body deserialized as `T`, or `default` for absent or unbufferable content. Throws [`InvalidOperationException`][invalid-operation] if there are no requests. |
| [`RequestBodyAsync<T>(int index)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) | Deserializes the captured body at a recorded request position with the adopted serializer. | [int] `index`: zero-based request position. | [`Task<T?>`][task-result]: selected captured body deserialized as `T`, or `default` for absent or unbufferable content. Throws [`ArgumentOutOfRangeException`][argument-out-of-range] for an invalid index. |

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

## Adding routes after completed verification

Expected behavior: adding a one-shot route makes asynchronous verification wait for that request.
Actual behavior: the handler keeps its completed signal. The next async verification faults immediately
when the added route is missing, even with a positive timeout.
A signal here is the internal task used to notify waiting verification that every expectation was consumed.

The runnable example first completes one route, adds another, and checks the actual failure.
It then sends the second request and checks synchronous verification succeeds.
This uses no timing-based sleeps. See [the executable reproduction](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs).
Build the full table before any verification completes, or create another handler for a separate scenario.

## Concurrent one-shot requests

A lock protects mutation of handler lists. Selection and consumption use separate steps.
Two overlapping requests can select the same unused one-shot route.
Verification may count it once while both requests receive its reply.
Send one-shot scenarios serially. Use reusable routes for repeated independent requests.
The complete example reproduces this without relying on timing: an asynchronous predicate waits
until both requests are matching, then lets both continue. Both receive the reply, and verification succeeds.
One expectation should accept one request, so this is an implementation discrepancy.

## Executable verification excerpts

This call belongs to a synchronous helper in the complete example. It runs after a client call finishes.
The asynchronous example uses it alongside both async overloads.


```csharp
http.VerifyAllCalled();
```


```csharp
using StubHttp http = new() { { Route.Get("/expected"), Reply.Text("received") } };
using HttpClient client = CreateClient(http);
Task waiting = http.VerifyAllCalledAsync(TimeSpan.FromSeconds(1));
SampleCheck.Equal(false, waiting.IsCompleted);
using HttpResponseMessage response = await client.GetAsync(new Uri("https://people.example/expected"));
await waiting;
await http.VerifyAllCalledAsync();
Verify(http);
SampleCheck.Equal(null, await http.LastRequestBodyAsync<TestingPerson>());
```

The additional-route reproduction asserts immediate completion and the missing second route.


```csharp
using StubHttp http = new() { { Route.Get("/first"), Reply.Status(HttpStatusCode.OK) } };
using HttpClient client = CreateClient(http);
using HttpResponseMessage first = await client.GetAsync(new Uri("https://people.example/first"));
await http.VerifyAllCalledAsync();
http.Add(Route.Get("/second"), Reply.Status(HttpStatusCode.OK));
Task verification = http.VerifyAllCalledAsync(TimeSpan.FromSeconds(1));
SampleCheck.Equal(true, verification.IsCompleted);
bool rejected = false;
try
{
    await verification;
}
catch (InvalidOperationException error)
{
    rejected = error.Message.Contains("/second", StringComparison.Ordinal);
}

SampleCheck.Equal(true, rejected);
using HttpResponseMessage second = await client.GetAsync(new Uri("https://people.example/second"));
Verify(http);
```
