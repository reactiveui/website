---
Order: 7
---
# Test streams, uploads and time

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/testing-streaming/testing-streaming.csproj).

A streaming call hands your app each item as soon as it arrives. To test that, you need to control when each item
arrives. A real server cannot promise that. A test that waits with `Task.Delay` is slow and can fail at random.

`Refit.Testing` gives the test that control. `StubHttp` is a *stub*: a fake `HttpMessageHandler` that answers your
client instead of a real server. You give it *routes*. A route says which requests it matches, and its *reply* says what
to send back. A route is *one-shot* by default: it answers one request and is then used up.

This page covers three things a real server makes hard to test:

- **Receiving a stream.** A *stream source* (`StreamSource`) is a response body that sends a chunk only when your test
  tells it to.
- **Sending a stream.** The *capture* policy (`RequestCapture`) decides whether `StubHttp` reads an upload before your
  reply code does.
- **Time.** A fake clock controls simulated delays and verification timeouts. Nothing waits for real time.

A *chunk* is one piece of the response body. Refit reads a JSON Lines body one line at a time and a server-sent events
body one event at a time. See [streaming results](../results/streaming.md).

The samples on this page call this interface. `WatchAsync` reads a stream. `ImportAsync` and `ImportLiveAsync` send one.

```csharp
/// <summary>Streaming calls exercised by the controllable test responses.</summary>
internal interface ITestingStreamingApi
{
    /// <summary>Reads people one at a time as the response body arrives.</summary>
    /// <param name="cancellationToken">The token that cancels the request and the body read.</param>
    /// <returns>The people parsed from the streamed body.</returns>
    [Get("/people/live")]
    IAsyncEnumerable<TestingPerson> WatchAsync(CancellationToken cancellationToken);

    /// <summary>Uploads people as JSON Lines, one serialized person per line.</summary>
    /// <param name="people">The people to upload; enumerated while the body is written.</param>
    /// <returns>A task that completes when the upload has been answered.</returns>
    [Post("/people/import")]
    Task ImportAsync([Body(BodySerializationMethod.JsonLines)] IEnumerable<TestingPerson> people);

    /// <summary>Uploads people as JSON Lines from an asynchronous producer, one serialized person per line as it arrives.</summary>
    /// <param name="people">The people to upload; enumerated once while the body is written.</param>
    /// <param name="cancellationToken">The token that flows to the producer and to every write.</param>
    /// <returns>A task that completes when the upload has been answered.</returns>
    [Post("/people/import-live")]
    Task ImportLiveAsync([Body(BodySerializationMethod.JsonLines)] IAsyncEnumerable<TestingPerson> people, CancellationToken cancellationToken);
}
```

Source: [`ITestingStreamingApi.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/ITestingStreamingApi.cs).
`CreateSettings()` in the samples is the helper from the [testing overview](index.md#make-your-first-test). It registers
the JSON metadata.

## Receive a stream

The samples in this section are in
[`TestingStreaming.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/TestingStreaming.cs).

### Prove the first item arrives early

The problem: how do you prove a streamed reply hands you the first item before the second one has even arrived?

1. **Create a stream source.** `new StreamSource(StreamingContentFormat.JsonLines)` makes an empty JSON Lines body.
2. **Reply with it.** `Reply.Stream(source)` sends status 200 and the headers at once. The body stays empty until the
   test releases something.
3. **Release one item and read it.** `Release(item)` sends one chunk. The client's serializer turns the item into JSON
   when the client reads it. The read completes right away.
4. **Check that the next read waits.** Start the next `MoveNextAsync()` before you release the second item. The
   returned `ValueTask<bool>` is not complete, because no data is waiting. Release the item, then await the read.
5. **End the body.** `Complete()` ends the body. The next read returns `false`, Refit disposes the response, and the
   `Closed` task completes.

[//]: # "excerpt:Testing/TestingStreaming.cs#ShowEarlyItemsAsync"

```csharp
StreamSource source = new StreamSource(StreamingContentFormat.JsonLines);
using StubHttp http = new StubHttp { { Route.Get("/people/live"), Reply.Stream(source) } };
ITestingStreamingApi api = http.CreateGeneratedClient<ITestingStreamingApi>("https://api.example.com", CreateSettings());
await using IAsyncEnumerator<TestingPerson> people = api.WatchAsync(CancellationToken.None).GetAsyncEnumerator();

source.Release(new TestingPerson(1, "Ada"));
bool gotFirst = await people.MoveNextAsync();
string firstName = people.Current.Name; // "Ada"

ValueTask<bool> second = people.MoveNextAsync();
bool secondReadyEarly = second.IsCompleted; // false: Grace has not been released yet

source.Release(new TestingPerson(2, "Grace"));
bool gotSecond = await second;
string secondName = people.Current.Name; // "Grace"

source.Complete();
bool gotThird = await people.MoveNextAsync(); // false: the source is complete
await source.Closed; // reaching the end disposed the response
```

This test proves your app gets each item as it arrives, not only after the whole body. Use it for any code that shows
live data. Await `source.Closed` at the end to prove your code released the response.

### Cancel a waiting read

The problem: what happens to a stalled stream read when you cancel it?

Cancel the token while a read waits. The read throws `OperationCanceledException`. Refit then disposes the response, so
`Closed` completes.

[//]: # "excerpt:Testing/TestingStreaming.cs#ShowCancellationAsync"

```csharp
StreamSource source = new StreamSource();
using StubHttp http = new StubHttp { { Route.Get("/people/live"), Reply.Stream(source) } };
ITestingStreamingApi api = http.CreateGeneratedClient<ITestingStreamingApi>("https://api.example.com", CreateSettings());
using CancellationTokenSource cancellation = new CancellationTokenSource();
await using IAsyncEnumerator<TestingPerson> people = api.WatchAsync(cancellation.Token).GetAsyncEnumerator();

source.Release(new TestingPerson(1, "Ada"));
bool gotFirst = await people.MoveNextAsync();
Task<bool> stalled = people.MoveNextAsync().AsTask();
bool stalledEarly = stalled.IsCompleted; // false: no second person has arrived

await cancellation.CancelAsync();
try
{
    await stalled; // throws OperationCanceledException
}
catch (OperationCanceledException error)
{
    Console.WriteLine(error.Message); // "A task was canceled."
}

await source.Closed; // the client disposed the response when the read was cancelled
```

Use this to check that your app stops cleanly when the user leaves a screen or a timeout fires. Await `Closed` to
prove the response did not stay open after the cancellation.

### Drop the connection

The problem: how do you simulate a connection dropping mid-stream after some items already arrived?

Release the chunks first, then call `Disconnect()`. The client reads those chunks. Its next read throws
`HttpIOException` with `HttpRequestError.ResponseEnded`. A real dropped connection gives the same error on .NET 8 and
later. On .NET Framework the read throws `IOException`.

[//]: # "excerpt:Testing/TestingStreaming.cs#ShowDisconnectAsync"

```csharp
StreamSource source = new StreamSource();
source.Release(new TestingPerson(1, "Ada"));
source.Disconnect();
using StubHttp http = new StubHttp { { Route.Get("/people/live"), Reply.Stream(source) } };
ITestingStreamingApi api = http.CreateGeneratedClient<ITestingStreamingApi>("https://api.example.com", CreateSettings());

List<string> names = [];
try
{
    await foreach (TestingPerson person in api.WatchAsync(CancellationToken.None))
    {
        names.Add(person.Name); // "Ada" arrives before the drop
    }
}
catch (HttpIOException error)
{
    Console.WriteLine(error.Message); // "Refit.Testing simulated disconnect. (ResponseEnded)"
}
```

Your app keeps the items that arrived before the drop. Check that it keeps them and reports the error. Use
`Fail(exception)` instead of `Disconnect()` when you want the read to throw an exception of your own.

### Stall the body

The problem: how do you prove headers arrived while the body is still stalled?

Do not release anything. The status and content type arrive, but a body read waits. It ends only when you release
data, cancel the read or dispose the response. This sample uses a plain `HttpClient` with
`HttpCompletionOption.ResponseHeadersRead`, so it can read the headers before the body.

[//]: # "excerpt:Testing/TestingStreaming.cs#ShowStalledBodyAsync"

```csharp
StreamSource source = new StreamSource(StreamingContentFormat.ServerSentEvents);
using StubHttp http = new StubHttp { { Route.Get("/events"), Reply.Stream(source) } };
using HttpClient httpClient = new HttpClient(http, disposeHandler: false);
using CancellationTokenSource cancellation = new CancellationTokenSource();

HttpResponseMessage response = await httpClient.GetAsync(new Uri("https://api.example.com/events"), HttpCompletionOption.ResponseHeadersRead);
string? contentType = response.Content.Headers.ContentType?.MediaType; // "text/event-stream": headers arrived
Stream body = await response.Content.ReadAsStreamAsync();
Task<int> read = body.ReadAsync(new byte[64], cancellation.Token).AsTask();
bool readCompletedEarly = read.IsCompleted; // false: the body itself has not arrived

await cancellation.CancelAsync();
try
{
    await read; // never reached: the read is cancelled first
}
catch (OperationCanceledException error)
{
    Console.WriteLine(error.Message); // "A task was canceled."
}

bool closedBeforeDispose = source.IsClosed; // false: cancelling the read did not close the body
response.Dispose();
bool closedAfterDispose = source.IsClosed; // true: disposing the response closed the body
```

Cancelling the read does not close the body. `IsClosed` stays `false` until the code disposes the response. Refit's
generated client disposes the response for you, as the cancellation sample shows. Code that calls `HttpClient`
directly must dispose the response itself.

Use a stalled body to check your app's own timeout. Cancel the token yourself instead of waiting for a real timer.

### Reply with a ready-made stream

The problem: how do you reply with a ready-made JSON Lines or server-sent events stream instead of controlling it by
hand?

When the test does not need to pace the items, use `Reply.JsonLines(items)` or `Reply.ServerSentEvents(items)`. Each
sends one chunk per item and then ends the body.

[//]: # "excerpt:Testing/TestingStreaming.cs#ShowFixedStreamsAsync"

```csharp
TestingPerson[] people = [new TestingPerson(1, "Ada"), new TestingPerson(2, "Grace")];
using StubHttp http = new StubHttp
{
    {
        Route.Get("/people/live"),
        Reply.JsonLines(people)
    },
    {
        Route.Get("/people/live"),
        Reply.ServerSentEvents(people)
    },
};
ITestingStreamingApi api = http.CreateGeneratedClient<ITestingStreamingApi>("https://api.example.com", CreateSettings());

List<string> jsonLinesNames = [];
await foreach (TestingPerson person in api.WatchAsync(CancellationToken.None))
{
    jsonLinesNames.Add(person.Name);
}

List<string> serverSentEventNames = [];
await foreach (TestingPerson person in api.WatchAsync(CancellationToken.None))
{
    serverSentEventNames.Add(person.Name); // Ada, Grace: same people, delivered as server-sent events this time
}
```

The two routes match the same path. Both are one-shot, so the first call gets the JSON Lines reply and the second call
gets the server-sent events reply. Refit reads the same two people from both.

Each response gets a fresh copy of the items, so these replies also work on a reusable route. A `StreamSource` feeds only
one response. A second response from the same source throws `InvalidOperationException`.

## Test a streaming upload

A streaming upload sends records while your app still produces them. The client side is in
[upload many records as JSON Lines](../requests/bodies.md#upload-many-records-as-json-lines). This section tests what
the server side sees.

The samples in this section are in
[`TestingStreaming.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/TestingStreaming.cs).

### Choose a capture policy

By default `StubHttp` reads the whole request body before it matches a route. That lets route body matchers and
`LastRequestBodyAsync<T>()` work. It also means an upload is read in full before your reply code runs. A real server
does not do that.

Set `StubHttp.RequestCapture` to one of three policies:

| Policy | What it does | Typed inspection afterwards |
| --- | --- | --- |
| `RequestCapture.Full` (default) | Reads and buffers every body before matching. | Yes. |
| `RequestCapture.None` | Never reads or records the body. Your reply code reads it. | No. `LastRequestBodyAsync<T>()` returns `default`. |
| `RequestCapture.Bounded(maxBytes)` | Leaves the body for your reply code. It records at most `maxBytes` per request while that code reads. | Yes, when the reply code read the whole body and it fit in `maxBytes`. |

A route that matches on `Body` or `FormData` needs `Full`, because matching would read the body. Such a route throws
`InvalidOperationException` under the other policies. `Bounded` throws `ArgumentOutOfRangeException` for a negative
limit.

### Let the reply code read the upload

The problem: how do you let your reply code read an upload stream directly, instead of one `StubHttp` already buffered?

This sample counts how many people the upload's `IEnumerable<T>` produced before the reply code started. It runs the
upload twice: once with `RequestCapture.None` and once with `RequestCapture.Full`.

[//]: # "excerpt:Testing/TestingStreaming.cs#ShowUploadAsync"

```csharp
int pulled = 0;
IEnumerable<TestingPerson> UploadPeople()
{
    pulled++;
    yield return new TestingPerson(1, "Ada");
    pulled++;
    yield return new TestingPerson(2, "Grace");
}

int pulledBeforeReply = -1;
using StubHttp http = new StubHttp
{
    {
        Route.Post("/people/import"),
        Reply.From(async request =>
        {
            pulledBeforeReply = pulled;
            await request.Content!.ReadAsStringAsync(); // read the whole upload
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        })
    },
};
http.RequestCapture = RequestCapture.None; // stops StubHttp from reading the body before the reply above does
ITestingStreamingApi api = http.CreateGeneratedClient<ITestingStreamingApi>("https://api.example.com", CreateSettings());

await api.ImportAsync(UploadPeople());
int pulledWithNoCapture = pulledBeforeReply; // 0: the handler left the whole upload for the reply code
int totalPulled = pulled; // 2
TestingPerson? recorded = await http.LastRequestBodyAsync<TestingPerson>(); // null: RequestCapture.None recorded nothing

pulled = 0; // the second half repeats the upload with RequestCapture.Full
http.RequestCapture = RequestCapture.Full; // buffers the whole upload before any reply code runs
http.Add(Route.Post("/people/import"), Reply.From(request =>
{
    pulledBeforeReply = pulled;
    return new HttpResponseMessage(HttpStatusCode.Accepted);
}));
await api.ImportAsync(UploadPeople());
int pulledWithFullCapture = pulledBeforeReply; // 2: everything was pulled before the reply ran
```

With `None` the count is zero when the reply code starts. The reply code pulls both people as it reads. `None` records
nothing, so `LastRequestBodyAsync<T>()` returns `null`. With `Full` the count is two, because `StubHttp` read the whole
upload first.

Use `None` when your test checks that the server can start before the upload ends. Keep `Full` when you want to inspect
the body afterwards.

### Read an upload one line at a time

The problem: how do you read an uploaded JSON Lines body, one line at a time, from inside a reply?

`Reply.From((request, cancellationToken) => ...)` passes your reply code the send's `CancellationToken`. Pass that token
to every read, the way a network handler does. Cancelling the call then reaches the content your app is writing.

[//]: # "excerpt:Testing/TestingStreaming.cs#ShowLiveUploadAsync"

```csharp
static async IAsyncEnumerable<TestingPerson> UploadPeopleAsync()
{
    yield return new TestingPerson(1, "Ada");
    yield return new TestingPerson(2, "Grace");
}

List<string> lines = [];
using StubHttp http = new StubHttp
{
    {
        Route.Post("/people/import-live"),
        Reply.From(async (request, cancellationToken) =>
        {
            await using Stream body = await request.Content!.ReadAsStreamAsync(cancellationToken);
            using StreamReader reader = new StreamReader(body);
            string? line;
            while ((line = await reader.ReadLineAsync(cancellationToken)) is not null)
            {
                lines.Add(line);
            }

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        })
    },
};

// RequestCapture.None stops StubHttp from reading the body itself, so the responder above is the
// first (and only) code to read the uploaded stream.
http.RequestCapture = RequestCapture.None;
ITestingStreamingApi api = http.CreateGeneratedClient<ITestingStreamingApi>("https://api.example.com", CreateSettings());

await api.ImportLiveAsync(UploadPeopleAsync(), CancellationToken.None); // lines now has one JSON object per uploaded person
```

`RequestCapture.None` makes the reply code the only code that reads the body. After the call, `lines` holds one JSON
object for each uploaded person. Read the body with a `StreamReader`, not `ReadAsStringAsync`, when you want to act on
each line as it arrives.

### Cap the recorded bytes

The problem: how do you cap how many bytes of a request body `StubHttp` captures for inspection?

`RequestCapture.Bounded(maxBytes)` records the bytes while your reply code reads them. It keeps at most `maxBytes` for
each request.

[//]: # "excerpt:Testing/TestingStreaming.cs#ShowBoundedCaptureAsync"

```csharp
// Reading the body is what fills a bounded capture, so the reply echoes the uploaded person back.
StubResponse echo = Reply.From(static async request =>
{
    string json = await request.Content!.ReadAsStringAsync();
    return new HttpResponseMessage(HttpStatusCode.Created) { Content = new StringContent(json, Encoding.UTF8, "application/json") };
});

// Each route answers once, and this sample sends two requests.
using StubHttp http = new StubHttp
{
    { Route.Post("/people"), echo },
    { Route.Post("/people"), echo },
};
ITestingApi api = http.CreateGeneratedClient<ITestingApi>("https://api.example.com", CreateSettings());

http.RequestCapture = RequestCapture.Bounded(1024); // roomy enough for one serialized person
await api.CreateAsync(new TestingPerson(2, "Grace"));
TestingPerson? recorded = await http.LastRequestBodyAsync<TestingPerson>(); // recorded?.Name == "Grace"

http.RequestCapture = RequestCapture.Bounded(4); // far too small for one serialized person
await api.CreateAsync(new TestingPerson(2, "Grace"));
try
{
    await http.LastRequestBodyAsync<TestingPerson>(); // throws: the capture was truncated
}
catch (InvalidOperationException error)
{
    Console.WriteLine(error.Message); // "The request body exceeded the capture limit of 4 bytes; raise RequestCapture.Bounded or use RequestCapture.Full."
}
```

The sample calls `CreateAsync` from `ITestingApi`, the interface on the [testing overview](index.md). Both routes share
one `echo` reply, and each call uses one of the two one-shot routes. With a 1024-byte limit, `LastRequestBodyAsync<T>()` returns
the person. With a 4-byte limit, it throws `InvalidOperationException`, because the body did not fit.

`LastRequestBodyAsync<T>()` also throws `InvalidOperationException` when the reply code did not read the body to the
end. Under `Bounded`, make sure your reply code reads the whole body before you inspect it.

## Control simulated time

[Network faults](faults.md) can add a delay to each request. The seed passed to `NetworkBehavior` decides which delay
and which fault each request gets. It does not decide when the delay ends. Set `StubHttp.TimeProvider` to a fake clock to
control that.

`FakeTimeProvider` comes from the `Microsoft.Extensions.TimeProvider.Testing` package. Its `Advance` method moves time
forward. Any delay that has run out then completes. The clock belongs to one handler. It changes nothing in Refit
itself, and nothing outside the test.

The samples in this section are in
[`TestingStreaming.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/TestingStreaming.cs).

### Delay a reply

The problem: how do you make simulated network delay respond to a fake clock instead of real time?

[//]: # "excerpt:Testing/TestingStreaming.cs#ShowSimulatedDelayAsync"

```csharp
FakeTimeProvider clock = new FakeTimeProvider();
NetworkBehavior behavior = new NetworkBehavior(7) // 7 is the random seed, so the random choices repeat on every run
{
    Delay = TimeSpan.FromSeconds(2),
    Variance = 0,
    FailurePercent = 0,
};
using StubHttp http = new StubHttp(behavior) { { Route.Get("/slow"), Reply.Text("done") } };
http.TimeProvider = clock;
using HttpClient httpClient = new HttpClient(http, disposeHandler: false);

Task<HttpResponseMessage> pending = httpClient.GetAsync(new Uri("https://api.example.com/slow"));
clock.Advance(TimeSpan.FromSeconds(1));
bool stillWaiting = pending.IsCompleted; // false: one simulated second is still outstanding

clock.Advance(TimeSpan.FromSeconds(1));
using HttpResponseMessage response = await pending; // arrives once the fake clock has advanced the full 2-second delay
string body = await response.Content.ReadAsStringAsync(); // "done"
```

The behavior adds a two-second delay. After one simulated second the request still waits. After the second one the
reply arrives. The test takes no real time.

Use this to test a loading indicator or your own timeout. Set `Variance` and `FailurePercent` to zero when you want the
exact delay and no faults.

### Time out verification

The problem: does the verification timeout also follow a fake clock instead of real time?

It does. `VerifyAllCalledAsync(timeout)` waits on `StubHttp.TimeProvider`.

[//]: # "excerpt:Testing/TestingStreaming.cs#ShowVerificationTimeoutAsync"

```csharp
FakeTimeProvider clock = new FakeTimeProvider();
using StubHttp http = new StubHttp { { Route.Get("/expected"), Reply.Status(HttpStatusCode.OK) } };
http.TimeProvider = clock;

Task verification = http.VerifyAllCalledAsync(TimeSpan.FromSeconds(5));
bool doneEarly = verification.IsCompleted; // false: nothing has called /expected yet

clock.Advance(TimeSpan.FromSeconds(5));
try
{
    await verification; // throws once the simulated clock reaches the 5-second timeout
}
catch (InvalidOperationException error)
{
    Console.WriteLine(error.Message); // "1 expected request(s) were not made:\n  - GET /expected"
}
```

Nothing calls `/expected`, so the verification waits. When you advance the clock to the five-second timeout, it throws
`InvalidOperationException`. The message lists the one-shot routes that got no request. See
[verification](verification.md).

## Full tests in your framework

The samples above show the calls without a test framework. For complete streaming tests with attributes and asserts,
see the `StreamingTests` section for [xUnit](xunit.md#stream-data-in-and-out),
[NUnit](nunit.md#stream-data-in-and-out), [MSTest](mstest.md#stream-data-in-and-out) or
[TUnit](tunit.md#stream-data-in-and-out).

## API reference

| API | Description | Returns and behavior |
| --- | --- | --- |
| `new StreamSource()` | Creates a JSON Lines body (`application/x-ndjson`). | A source with no chunks. |
| `new StreamSource(StreamingContentFormat format)` | Creates a body for `JsonLines`, `ServerSentEvents` (`text/event-stream`) or `JsonArray` (`application/json`). | Throws `ArgumentOutOfRangeException` for another value. |
| `Release<T>(T item)` | Sends one item, framed for the format and serialized by the client's serializer when the client reads it. | Throws `InvalidOperationException` after the body ended. |
| `ReleaseText(string text)`, `ReleaseBytes(byte[] bytes)` | Sends raw UTF-8 text or bytes without framing. | Throws `ArgumentNullException` for `null`. |
| `Complete()` | Ends the body. A JSON array gets its closing bracket first. | Later reads return the end of the body. |
| `Disconnect()` | Drops the connection after the chunks already released. | The next read throws `HttpIOException` (`IOException` on .NET Framework). |
| `Fail(Exception error)` | Ends the body with your exception. | The next read throws `error`. |
| `ReleasedChunks`, `ReadChunks` | Count released chunks (including the end) and chunks the client has started to read. | `int`. |
| `Closed`, `IsClosed` | Report that the client disposed the response or its body stream. | `Task` and `bool`. |
| `Reply.Stream(StreamSource source)` | Replies with status 200 and the source as the body. | A source feeds one response. A second response throws `InvalidOperationException`. |
| `Reply.JsonLines<T>(IEnumerable<T> items)`, `Reply.ServerSentEvents<T>(IEnumerable<T> items)` | Reply with one chunk per item and then end the body. | A fresh body for every response. |
| `Reply.From(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> responder)` | Builds the reply from the request and the send's `CancellationToken`. | Pass the token to every body read so cancelling the call reaches the upload. |
| `StubHttp.RequestCapture` | Chooses `Full`, `None` or `Bounded(maxBytes)`. | Defaults to `Full`. `Bounded` throws `ArgumentOutOfRangeException` for a negative limit. |
| `StubHttp.TimeProvider` | The clock for simulated delays and verification timeouts. | Defaults to `TimeProvider.System`. Throws `ArgumentNullException` for `null`. |

Source: [StreamSource.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StreamSource.cs),
[RequestCapture.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RequestCapture.cs),
[Reply.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) and
[StubHttp.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs).
