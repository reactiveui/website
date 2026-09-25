---
Order: 7
---
# Test streams, uploads and time

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/testing-streaming/testing-streaming.csproj).

A streaming call hands your app each item as soon as it arrives. To test that, you need to control
when each item arrives. A real server cannot promise that, and a test that waits with `Task.Delay` is slow and can fail at random.

`Refit.Testing` gives the test that control. A `StreamSource` is a response body that sends a chunk only when your test
tells it to. A fake clock controls simulated delays and verification timeouts. Nothing waits for real time.

A *chunk* is one piece of the response body. Refit reads a JSON Lines body one line at a time and a
server-sent events body one event at a time. See [streaming results](../results/streaming.md).

## Test that the first item arrives early

**1. Declare a streaming method.** A method that returns `IAsyncEnumerable<T>` reads the body while it arrives.


```csharp
internal interface ITestingStreamingApi
{
    [Get("/people/live")]
    IAsyncEnumerable<TestingPerson> WatchAsync(CancellationToken cancellationToken);

    [Post("/people/import")]
    Task ImportAsync([Body(BodySerializationMethod.JsonLines)] IEnumerable<TestingPerson> people);
}
```

**2. Reply with a `StreamSource`.** `Reply.Stream(source)` sends the status and headers at once.
The body stays empty until the test releases something.

**3. Release one item and read it.** `Release(item)` serializes the item with the client's serializer and frames it for the
source's format. The client reads it right away.

**4. Check that the next read waits.** Start the next `MoveNextAsync()` before you release the second item.
The returned `ValueTask<bool>` is not complete, because no data is waiting. Release the item and await the read.


```csharp
StreamSource source = new(StreamingContentFormat.JsonLines);
using StubHttp http = new() { { Route.Get("/people/live"), Reply.Stream(source) } };
ITestingStreamingApi api = http.CreateGeneratedClient<ITestingStreamingApi>("https://api.example.com", CreateSettings());
await using IAsyncEnumerator<TestingPerson> people = api.WatchAsync(CancellationToken.None).GetAsyncEnumerator();

source.Release(new TestingPerson(1, "Ada"));
Assert.True(await people.MoveNextAsync());
Assert.Equal("Ada", people.Current.Name);

ValueTask<bool> next = people.MoveNextAsync();
Assert.False(next.IsCompleted); // Grace has not been released yet

source.Release(new TestingPerson(2, "Grace"));
Assert.True(await next);
Assert.Equal("Grace", people.Current.Name);

source.Complete();
Assert.False(await people.MoveNextAsync());
await source.Closed; // reaching the end disposed the response
```

`Complete()` ends the body. The loop finishes, Refit disposes the response, and `Closed` completes.
`CreateSettings()` is the helper from the [testing overview](index.md#make-your-first-test). It registers the JSON metadata.

## Cancellation, disconnects and stalled bodies

**Cancellation closes the response.** Cancel the token while a read waits. The read throws
`OperationCanceledException`. Refit then disposes the response, so `Closed` completes.


```csharp
StreamSource source = new();
using StubHttp http = new() { { Route.Get("/people/live"), Reply.Stream(source) } };
ITestingStreamingApi api = http.CreateGeneratedClient<ITestingStreamingApi>("https://api.example.com", CreateSettings());
using CancellationTokenSource cancellation = new();
await using IAsyncEnumerator<TestingPerson> people = api.WatchAsync(cancellation.Token).GetAsyncEnumerator();
source.Release(new TestingPerson(1, "Ada"));
Assert.True(await people.MoveNextAsync());

ValueTask<bool> waiting = people.MoveNextAsync();
await cancellation.CancelAsync();

await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await waiting);
await source.Closed; // the client disposed the response when the read was cancelled
```

**A dropped connection fails after the chunks it already sent.** Release the chunks first, then call `Disconnect()`.
The client reads those chunks. Its next read throws `HttpIOException` with `HttpRequestError.ResponseEnded`, the same
error a real dropped connection gives on .NET 8 and later. On .NET Framework it is an `IOException`.
Use `Fail(exception)` to throw your own exception instead.


```csharp
StreamSource source = new();
source.Release(new TestingPerson(1, "Ada"));
source.Disconnect();
using StubHttp http = new() { { Route.Get("/people/live"), Reply.Stream(source) } };
ITestingStreamingApi api = http.CreateGeneratedClient<ITestingStreamingApi>("https://api.example.com", CreateSettings());
List<string> names = [];

HttpIOException error = await Assert.ThrowsAsync<HttpIOException>(async () =>
{
    await foreach (TestingPerson person in api.WatchAsync(CancellationToken.None))
    {
        names.Add(person.Name);
    }
});

Assert.Equal("Ada", Assert.Single(names));
Assert.Equal(HttpRequestError.ResponseEnded, error.HttpRequestError);
```

**A stalled body sends headers and then nothing.** Do not release anything. The status and content type arrive,
but a body read waits until you release data, cancel the read or dispose the response.


```csharp
StreamSource source = new(StreamingContentFormat.ServerSentEvents);
using StubHttp http = new() { { Route.Get("/events"), Reply.Stream(source) } };
using HttpClient httpClient = new(http, disposeHandler: false);
using CancellationTokenSource cancellation = new();

using HttpResponseMessage response = await httpClient.GetAsync(new Uri("https://api.example.com/events"), HttpCompletionOption.ResponseHeadersRead);
Stream body = await response.Content.ReadAsStreamAsync();
ValueTask<int> read = body.ReadAsync(new byte[64], cancellation.Token);

Assert.Equal("text/event-stream", response.Content.Headers.ContentType?.MediaType);
Assert.False(read.IsCompleted); // the headers arrived; the body has not

await cancellation.CancelAsync();
await Assert.ThrowsAnyAsync<OperationCanceledException>(async () => await read);
```

Use a stalled body to check your app's own timeout. Cancel the token yourself instead of waiting for a real timer.

## Fixed streams

When the test does not need to pace the items, `Reply.JsonLines(items)` and `Reply.ServerSentEvents(items)` send one
chunk per item and then end the body. Each response gets its own copy, so these replies also work on a reusable route.
`Reply.ServerSentEvents(people)` works the same way.


```csharp
TestingPerson[] people = [new(1, "Ada"), new(2, "Grace")];
using StubHttp http = new() { { Route.Get("/people/live"), Reply.JsonLines(people) } };
ITestingStreamingApi api = http.CreateGeneratedClient<ITestingStreamingApi>("https://api.example.com", CreateSettings());

List<string> names = [];
await foreach (TestingPerson person in api.WatchAsync(CancellationToken.None))
{
    names.Add(person.Name);
}

Assert.Equal(new[] { "Ada", "Grace" }, names);
```

## Test a streaming upload

By default `StubHttp` reads the whole request body before it matches a route. That lets route body matchers and
`LastRequestBodyAsync<T>()` work. It also means an upload is read in full before your reply code runs, which a real server
would not do.

Set `RequestCapture` to change that:

| Policy | Reads the body before matching | Typed inspection afterwards |
| --- | --- | --- |
| `RequestCapture.Full` (default) | Yes | Yes |
| `RequestCapture.None` | No | No. `LastRequestBodyAsync<T>()` returns `default`. |
| `RequestCapture.Bounded(maxBytes)` | No | Yes, when the reply code read the whole body and it fit in `maxBytes`. |

With `None` or `Bounded`, your reply code reads the body at its own pace. This example counts how many people the
upload's `IEnumerable<T>` produced before the reply code started. With `None` the count is zero.
With the default policy it is two, because the handler had already read everything.


```csharp
int produced = 0;
IEnumerable<TestingPerson> Upload()
{
    produced++;
    yield return new TestingPerson(1, "Ada");
    produced++;
    yield return new TestingPerson(2, "Grace");
}

int producedBeforeReply = -1;
using StubHttp http = new()
{
    {
        Route.Post("/people/import"),
        Reply.From(async request =>
        {
            producedBeforeReply = produced;
            _ = await request.Content!.ReadAsStringAsync();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
        })
    },
};
http.RequestCapture = RequestCapture.None;
ITestingStreamingApi api = http.CreateGeneratedClient<ITestingStreamingApi>("https://api.example.com", CreateSettings());

await api.ImportAsync(Upload());

Assert.Equal(0, producedBeforeReply); // the handler left the upload for the reply code
Assert.Equal(2, produced);
```

`Bounded(maxBytes)` records the bytes as your reply code reads them. It keeps at most `maxBytes`.
`LastRequestBodyAsync<T>()` throws `InvalidOperationException` when the body was larger than the limit, or when the reply
code did not read it to the end. A route that matches on `Body` or `FormData` needs `Full`, because matching would read
the body. Such a route throws `InvalidOperationException` under the other policies.

## Control simulated time

[Network faults](faults.md) can add a delay to each request. `NetworkBehavior` seeds decide which delay and which fault
each request gets. They do not decide when the delay ends. Set `StubHttp.TimeProvider` to a fake clock to control that.
`FakeTimeProvider` comes from the `Microsoft.Extensions.TimeProvider.Testing` package. Its `Advance` method moves time
forward, and any delay that has run out then completes.


```csharp
FakeTimeProvider clock = new();
NetworkBehavior behavior = new() { Delay = TimeSpan.FromSeconds(2), Variance = 0, FailurePercent = 0 };
using StubHttp http = new(behavior)
{
    { Route.Get("/people/1"), Reply.Json("""{"id":1,"name":"Ada"}""") },
};
http.TimeProvider = clock;
using HttpClient httpClient = new(http, disposeHandler: false);

Task<HttpResponseMessage> pending = httpClient.GetAsync(new Uri("https://api.example.com/people/1"));
clock.Advance(TimeSpan.FromSeconds(1));
Assert.False(pending.IsCompleted); // one simulated second is still outstanding

clock.Advance(TimeSpan.FromSeconds(1));
using HttpResponseMessage response = await pending;
Assert.Equal(HttpStatusCode.OK, response.StatusCode);
```

The same clock sets when `VerifyAllCalledAsync(timeout)` gives up. The verification fails only after you advance the clock
past the timeout. See [verification](verification.md).

The clock belongs to one handler. It changes nothing in Refit itself, and nothing outside the test.

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
| `StubHttp.RequestCapture` | Chooses `Full`, `None` or `Bounded(maxBytes)`. | Defaults to `Full`. `Bounded` throws `ArgumentOutOfRangeException` for a negative limit. |
| `StubHttp.TimeProvider` | The clock for simulated delays and verification timeouts. | Defaults to `TimeProvider.System`. Throws `ArgumentNullException` for `null`. |

Source: [StreamSource.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StreamSource.cs),
[RequestCapture.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RequestCapture.cs),
[Reply.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) and
[StubHttp.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs).
