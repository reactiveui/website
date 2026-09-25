---
Order: 4
---
# Simulate network faults

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/testing-faults/testing-faults.csproj).

A service will not always reply quickly or successfully. Your tests can simulate a slow
response, a broken connection or a server error, and check how the app reacts.

`NetworkBehavior` adds those conditions to a `StubHttp` handler. You choose which faults it
produces. Your app's own code decides whether to retry, show an error or stop waiting.

## Simulate delay, failures and error replies

How do you simulate network delay, random failures and error status codes for one handler?

**1. Create a `NetworkBehavior` and set every value you rely on.** The defaults add a delay and a small
chance of failure, so set them explicitly. See [defaults](#defaults-probabilities-and-the-seed).

**2. Attach it to the handler.** Pass it to `new StubHttp(behavior)`, or assign `http.Behavior`.
Assign `null` to turn simulation off.

**3. Send the request and check the result.** Each request still needs a matching route.
A simulated fault replaces the reply that route would have sent.

The sample below first turns every reply into a `503 Service Unavailable`. It then raises `FailurePercent` to 1,
so the next request throws a connection failure instead.
Source: [`Testing.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs).

[//]: # "excerpt:Testing/Testing.cs#ShowFaultsAsync"

```csharp
NetworkBehavior behavior = new NetworkBehavior(7) // 7 is the random seed, so the random choices repeat on every run
{
    Delay = TimeSpan.Zero,
    Variance = 0,
    FailurePercent = 0,
    ErrorPercent = 1,
    ErrorStatusCode = HttpStatusCode.ServiceUnavailable,
    FailureFactory = static () => new HttpRequestException("Test connection failure."),
};
using StubHttp http = new StubHttp(behavior) { { Route.Get("/fault"), Reply.Text("normal") } };
using HttpClient httpClient = new HttpClient(http, disposeHandler: false);
using HttpResponseMessage error = await httpClient.GetAsync(new Uri("https://api.example.com/fault")); // error.StatusCode == ServiceUnavailable, from ErrorPercent = 1

behavior.FailurePercent = 1;
http.Add(Route.Get("/failure"), Reply.Text("normal"));
Task<HttpResponseMessage> failureCall = httpClient.GetAsync(new Uri("https://api.example.com/failure"));
try
{
    using HttpResponseMessage response = await failureCall; // throws, because FailurePercent is now 1
}
catch (HttpRequestException cause)
{
    Console.WriteLine(cause.Message); // "Test connection failure."
}
```

The first request gets `503` because `ErrorPercent` is 1. The normal `"normal"` reply never runs.
The second request throws the `HttpRequestException` that `FailureFactory` builds.
The sample changes `FailurePercent` on the live behavior. The handler reads its values on every request,
so the change applies to the next send.

### A connection failure through a Refit client

The sample sends through a plain `HttpClient`, so it sees the `HttpRequestException` itself.
A generated Refit client wraps a transport failure. A transport failure is an exception thrown while sending,
before any reply arrives.

- The client throws `ApiRequestException`.
- Its `Message` is the original exception's message.
- Its `InnerException` is the original `HttpRequestException`.
- A method that returns `ApiResponse<T>` does not throw. The response carries the `ApiRequestException` in `Error`.

`RefitSettings.TransportExceptionFactory` controls this wrapping. By default it wraps every exception except
an `OperationCanceledException` from a cancelled token. See [error handling](../results/errors.md).
For a complete Refit-client test in your framework, see `BrokenNetwork_ThrowsAConnectionError` for
[xUnit](xunit.md#brokennetwork_throwsaconnectionerror), [NUnit](nunit.md#brokennetwork_throwsaconnectionerror),
[MSTest](mstest.md#brokennetwork_throwsaconnectionerror) or [TUnit](tunit.md#brokennetwork_throwsaconnectionerror).

### Delay without waiting

The delay runs on `http.TimeProvider`. Assign a fake clock, and the test moves time forward itself instead of waiting.
See [control simulated time](streaming.md#control-simulated-time).

## Defaults, probabilities and the seed

A new `NetworkBehavior` starts with these values.

| Property | Type | Default | Meaning |
| --- | --- | --- | --- |
| `Delay` | [TimeSpan] | Two seconds | Base delay before each reply. |
| `Variance` | [double] | `0.4` | How far the delay can move above and below `Delay`, as a fraction of it. `0.4` means 60% to 140% of `Delay`. Zero fixes the delay. |
| `FailurePercent` | [double] | `0.03` | Chance that a request throws a connection failure. |
| `ErrorPercent` | [double] | `0` | Chance that a request gets an error reply instead of its route's reply. |
| `ErrorStatusCode` | [HttpStatusCode] | `InternalServerError` (`500`) | Status of the error reply. |
| `FailureFactory` | [`Func<Exception>`][failure-factory] | Builds an [HttpRequestException] with the message `Refit.Testing simulated network failure.` | Creates the exception for a connection failure. |

`FailurePercent` and `ErrorPercent` are probabilities from 0 to 1, despite their names.
0 means never, 1 means always, and `0.03` means 3% of requests.
Set `FailurePercent = 0` when you want only error replies.

The seed fixes the random sequence. `new NetworkBehavior()` uses seed 0. `new NetworkBehavior(seed)` uses your `int`.
The same seed gives the same sequence of delays and faults, in the same order, within one runtime.
That makes a test with a probability between 0 and 1 repeatable.

All properties are settable. The handler does not check their values.
Use a delay of zero or more, a variance from 0 to 1, and probabilities from 0 to 1.

### What happens to each request

1. The handler matches the request and consumes its one-shot route.
2. It waits for the delay.
3. It draws a connection failure against `FailurePercent`. A failure throws, and the steps stop here.
4. It draws an error reply against `ErrorPercent`. An error returns the error reply.
5. Otherwise the route's own reply runs.

Neither fault runs your route's reply code. Unmatched requests get no simulation.
A retry needs a one-shot route for each attempt, or a reusable route.
Verification can pass before the delay or failure finishes, because step 1 consumes the route.
Await the send separately. See [verification](verification.md#when-a-route-counts-as-called).

The random draws are thread-safe. When requests run at the same time, their order decides which request gets which draw.

## Check a behavior without a request

What do the low-level members return once you set the delay, failure and error values?
`NetworkBehavior` exposes the steps the handler uses. You can call them directly.
Source: [`Testing.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs).

[//]: # "excerpt:Testing/Testing.cs#ShowNetworkBehaviorMembersAsync"

```csharp
NetworkBehavior behavior = new NetworkBehavior(7) // 7 is the random seed, so the random choices repeat on every run
{
    Delay = TimeSpan.Zero,
    Variance = 0,
    FailurePercent = 0,
    ErrorPercent = 1,
    ErrorStatusCode = HttpStatusCode.ServiceUnavailable,
    FailureFactory = static () => new HttpRequestException("Test connection failure."),
};
TimeSpan delay = behavior.NextDelay(); // TimeSpan.Zero
bool isFailure = behavior.NextIsFailure(); // false, FailurePercent is 0
bool isError = behavior.NextIsError(); // true, ErrorPercent is 1
Exception failure = behavior.CreateFailure(); // failure.Message == "Test connection failure."
using HttpResponseMessage errorResponse = behavior.CreateErrorResponse(); // errorResponse.StatusCode == ServiceUnavailable
```

With `Delay` zero and `Variance` zero, `NextDelay()` returns `TimeSpan.Zero`. `NextIsFailure()` is `false` because
`FailurePercent` is 0. `NextIsError()` is `true` because `ErrorPercent` is 1.
`CreateFailure()` builds the exception without throwing it. `CreateErrorResponse()` builds a new reply,
which you must dispose.

These methods draw from the same random sequence as the handler. A direct call moves the sequence on,
so it changes which delay or fault the next request gets.

| Member | Description | Returns |
| --- | --- | --- |
| `NetworkBehavior()` | Creates a behavior with seed `0` and the defaults above. | A new behavior. |
| `NetworkBehavior(int seed)` | Creates a behavior whose random sequence starts from your seed. | A new behavior. |
| `NextDelay()` | Draws the delay for the next request. | [TimeSpan]: the varied delay, never below zero. |
| `NextIsFailure()` | Draws whether the next request fails. | [bool]: `true` when the draw falls under `FailurePercent`. |
| `NextIsError()` | Draws whether the next request gets an error reply. | [bool]: `true` when the draw falls under `ErrorPercent`. |
| `CreateFailure()` | Builds the connection-failure exception. | [Exception]: the result of `FailureFactory()`, not thrown. |
| `CreateErrorResponse()` | Builds an error reply. | [HttpResponseMessage]: a new reply with `ErrorStatusCode` and an empty text body. The caller disposes it. |
| `StubHttp.Behavior` | Gets or sets the handler's behavior. | [NetworkBehavior][network-behavior], or `null` for no simulation. |

Source: [NetworkBehavior.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/NetworkBehavior.cs)
and [StubHttp.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs).

[bool]: https://learn.microsoft.com/dotnet/api/system.boolean
[double]: https://learn.microsoft.com/dotnet/api/system.double
[TimeSpan]: https://learn.microsoft.com/dotnet/api/system.timespan
[Exception]: https://learn.microsoft.com/dotnet/api/system.exception
[HttpResponseMessage]: https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage
[HttpStatusCode]: https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode
[HttpRequestException]: https://learn.microsoft.com/dotnet/api/system.net.http.httprequestexception
[failure-factory]: https://learn.microsoft.com/dotnet/api/system.func-1
[network-behavior]: https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/NetworkBehavior.cs

## Typed replies and AOT

A fault sometimes replaces the typed reply, but the test still needs full JSON setup for the times it does not.
For a generated Refit client, register every request and reply model on a `JsonSerializerContext`
with `[JsonSerializable]`. Set that context as `TypeInfoResolver` in the options you pass to
`SystemTextJsonContentSerializer`. Pass those settings to `CreateGeneratedClient<T>`.
See [the complete setup](index.md#make-your-first-test).
Fault simulation does not supply missing JSON metadata, and it does not check native AOT compatibility.
