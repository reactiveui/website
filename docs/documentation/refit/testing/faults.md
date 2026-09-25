---
Order: 4
---
# Simulate network faults

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/testing-faults/testing-faults.csproj).

A service will not always reply quickly or successfully. Your tests can simulate a slow
response, a broken connection or a server error to check how the app reacts.

`NetworkBehavior` adds those conditions to the test handler. You choose which failures to
produce; your app's own code decides whether to retry, show an error or stop waiting.

## Configure a predictable failure

**1. Set delay and probabilities explicitly.** The defaults include delay and a small failure chance.
Set `FailurePercent=0` when you only want HTTP errors.
Probabilities are fractions: 0 means never and 1 means always.

**2. Attach the behavior.** Give it to `new StubHttp(behavior)` or assign `http.Behavior`.
Assign null to disable simulation. The delay runs on `http.TimeProvider`. Assign a fake clock so the test
moves time forward itself instead of waiting. See [simulated time](streaming.md#control-simulated-time).

**3. Send through a client and check the result.** The [fault examples](#fault-examples)
check a 503 response and then a thrown `HttpRequestException` with the configured message.
Raw `HttpClient` receives the simulated exception directly.
Refit can wrap a connection failure as `ApiRequestException` according to its error settings.
See [error handling](../results/errors.md).

## Defaults and calculation methods

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `NetworkBehavior()` | Creates deterministic fault simulation with the standard seed and defaults. | None. | A behavior with random seed `0` and the defaults below. |
| `NetworkBehavior(int seed)` | Creates fault simulation whose random sequence starts from your chosen seed. | [int] `seed`: random sequence seed. | A behavior with the supplied seed and the defaults below. The same ordered calls repeat within a runtime. |
| `NextDelay()` | Draws the delay that the next simulated request would use. | None. | [TimeSpan]: next varied delay. The multiplier is clamped at zero. |
| `NextIsFailure()` | Draws whether the next simulation produces a connection failure. | None. | [bool]: next trial against `FailurePercent`. |
| `NextIsError()` | Draws whether the next simulation produces an HTTP error response. | None. | [bool]: next trial against `ErrorPercent`. |
| `CreateFailure()` | Builds the configured connection exception without throwing it. | None. | [Exception]: result of `FailureFactory()`. Creates the exception without throwing it. |
| `CreateErrorResponse()` | Builds a disposable HTTP error reply from the configured status code. | None. | [HttpResponseMessage]: fresh response with the configured status and an empty text body. The caller must dispose it. |

| Property | Type | Default and behavior |
| --- | --- | --- |
| `Delay` | [TimeSpan] | Two seconds. Base delay for simulation. |
| `Variance` | [double] | `0.4`. Fraction above and below the delay. Zero fixes the delay. |
| `FailurePercent` | [double] | `0.03`. Connection-failure probability. |
| `ErrorPercent` | [double] | `0`. HTTP-error probability when no connection failure occurs. |
| `ErrorStatusCode` | [HttpStatusCode] | `InternalServerError` (`500`). Injected response status. |
| `FailureFactory` | [`Func<Exception>`][failure-factory] | Creates an [HttpRequestException] with message `Refit.Testing simulated network failure.` |
| `StubHttp.Behavior` | [NetworkBehavior][network-behavior], nullable | Constructor-supplied behavior, or `null` to disable simulation. See [handler construction](index.md). |

Source: [NetworkBehavior.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/NetworkBehavior.cs)
and [StubHttp.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs).

[int]: https://learn.microsoft.com/dotnet/api/system.int32
[bool]: https://learn.microsoft.com/dotnet/api/system.boolean
[double]: https://learn.microsoft.com/dotnet/api/system.double
[TimeSpan]: https://learn.microsoft.com/dotnet/api/system.timespan
[Exception]: https://learn.microsoft.com/dotnet/api/system.exception
[HttpResponseMessage]: https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage
[HttpStatusCode]: https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode
[HttpRequestException]: https://learn.microsoft.com/dotnet/api/system.net.http.httprequestexception
[failure-factory]: https://learn.microsoft.com/dotnet/api/system.func-1
[network-behavior]: https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/NetworkBehavior.cs

All properties are settable. Values are not validated.
Use nonnegative delays, sensible variance and probabilities from zero through one.
Random draws are locked, but concurrent call ordering can change which request gets each draw.
Standalone calculation methods consume the same random sequence as handler simulation.

The handler first matches and consumes the route, then delays, then tries a connection failure,
then tries an HTTP error. A connection failure prevents the error trial from running.
Neither fault invokes your normal responder. Unmatched requests do not receive simulation.
Retries need enough one-shot routes for each attempt, or a reusable route.
Verification can succeed before the delay or failure finishes. Await the send separately.

## Typed replies and AOT

For a standalone generated Refit test, register every request/reply model on a `JsonSerializerContext`
with `[JsonSerializable]`. Set its context as `TypeInfoResolver` in the options passed to
`SystemTextJsonContentSerializer`. Pass those settings to `CreateGeneratedClient<T>`.
The [complete setup](index.md#make-your-first-test) applies even when a fault sometimes replaces the typed reply.
Fault injection does not supply missing JSON metadata or validate native AOT compatibility.

## Fault examples

These tests send through a raw `HttpClient` built on the handler. Each request still needs a matching route.

**An HTTP error.** `ErrorPercent = 1` replaces every reply with the error status.


```csharp
NetworkBehavior behavior = new()
{
    Delay = TimeSpan.Zero,
    FailurePercent = 0,
    ErrorPercent = 1,
    ErrorStatusCode = HttpStatusCode.ServiceUnavailable,
};
using StubHttp http = new(behavior)
{
    { Route.Get("/people/1"), Reply.Json("""{"id":1,"name":"Ada"}""") },
};
using HttpClient httpClient = new(http, disposeHandler: false);

using HttpResponseMessage response = await httpClient.GetAsync(new Uri("https://api.example.com/people/1"));

Assert.Equal(HttpStatusCode.ServiceUnavailable, response.StatusCode);
http.VerifyAllCalled();
```

**A connection failure.** `FailurePercent = 1` makes every request throw the exception from `FailureFactory`.


```csharp
NetworkBehavior behavior = new()
{
    Delay = TimeSpan.Zero,
    FailurePercent = 1,
    FailureFactory = static () => new HttpRequestException("Connection reset."),
};
using StubHttp http = new(behavior)
{
    { Route.Get("/people/1"), Reply.Json("""{"id":1,"name":"Ada"}""") },
};
using HttpClient httpClient = new(http, disposeHandler: false);

HttpRequestException error = await Assert.ThrowsAsync<HttpRequestException>(
    () => httpClient.GetAsync(new Uri("https://api.example.com/people/1")));

Assert.Equal("Connection reset.", error.Message);
```

**Draw values without a request.** The calculation methods use the same random sequence as the handler.
A fixed seed makes that sequence repeat.


```csharp
NetworkBehavior behavior = new(seed: 7)
{
    Delay = TimeSpan.Zero,
    Variance = 0,
    FailurePercent = 0,
    ErrorPercent = 1,
    ErrorStatusCode = HttpStatusCode.ServiceUnavailable,
    FailureFactory = static () => new HttpRequestException("Connection reset."),
};

Assert.Equal(TimeSpan.Zero, behavior.NextDelay());
Assert.False(behavior.NextIsFailure());
Assert.True(behavior.NextIsError());
Assert.Equal("Connection reset.", behavior.CreateFailure().Message);
using HttpResponseMessage error = behavior.CreateErrorResponse();
Assert.Equal(HttpStatusCode.ServiceUnavailable, error.StatusCode);
```
