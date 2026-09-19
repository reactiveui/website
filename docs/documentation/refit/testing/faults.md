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
Assign null to disable simulation.

**3. Send through a client and check the result.** The [runnable fault example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs)
checks a 503 response and then a thrown `HttpRequestException` with the configured message.
Raw `HttpClient` receives the simulated exception directly.
Refit can wrap a connection failure as `ApiRequestException` according to its error settings.
See [error handling](../results/errors.md).

## Defaults and calculation methods

| Member | Behavior |
| --- | --- |
| `NetworkBehavior()` | Seed 0. |
| `NetworkBehavior(seed)` | Seeded random sequence. The same calls in the same order repeat within a runtime. |
| `Delay` | Base delay. Default two seconds. |
| `Variance` | Fraction above and below the delay. Default 0.4 gives about 60%–140% of the base. Zero fixes the delay. |
| `FailurePercent` | Connection-failure probability. Default 0.03. |
| `ErrorPercent` | HTTP-error probability when no connection failure occurs. Default zero. |
| `ErrorStatusCode` | Injected reply status. Default 500. |
| `FailureFactory` | Exception-producing lambda. Default creates `HttpRequestException`. |
| `NextDelay()` | Draw the next varied delay. The calculated multiplier is clamped at zero. |
| `NextIsFailure()` / `NextIsError()` | Draw a probability trial. |
| `CreateFailure()` | Invoke your exception factory. |
| `CreateErrorResponse()` | Create a fresh response with configured status and an empty text body. Dispose it. |

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

## Calculation and injection excerpts

The calculation excerpt exercises the seeded constructor and every standalone method.
`SimulationSeed` is 7 and `DefaultDelaySeconds` is 2 in the complete source.


```csharp
NetworkBehavior defaults = new();
NetworkBehavior behavior = new(SimulationSeed)
{
    Delay = TimeSpan.Zero,
    Variance = 0,
    FailurePercent = 0,
    ErrorPercent = 1,
    ErrorStatusCode = HttpStatusCode.ServiceUnavailable,
    FailureFactory = static () => new HttpRequestException(FailureMessage),
};
SampleCheck.Equal(TimeSpan.FromSeconds(DefaultDelaySeconds), defaults.Delay);
SampleCheck.Equal(TimeSpan.Zero, behavior.NextDelay());
SampleCheck.Equal(false, behavior.NextIsFailure());
SampleCheck.Equal(true, behavior.NextIsError());
SampleCheck.Equal(FailureMessage, behavior.CreateFailure().Message);
using HttpResponseMessage standalone = behavior.CreateErrorResponse();
SampleCheck.Equal(HttpStatusCode.ServiceUnavailable, standalone.StatusCode);
```

The injection excerpt checks both fault kinds through a raw client.
Each attempt still needs a matching route.


```csharp
using StubHttp http = new(behavior) { { Route.Get("/fault"), Reply.Text("normal") } };
using HttpClient client = CreateClient(http);
using HttpResponseMessage error = await client.GetAsync(new Uri("https://people.example/fault"));
SampleCheck.Equal(HttpStatusCode.ServiceUnavailable, error.StatusCode);
Verify(http);
behavior.FailurePercent = 1;
http.Add(Route.Get("/failure"), Reply.Text("normal"));
bool failed = false;
try
{
    using HttpResponseMessage response = await client.GetAsync(new Uri("https://people.example/failure"));
}
catch (HttpRequestException cause)
{
    failed = cause.Message == FailureMessage;
}

SampleCheck.Equal(true, failed);
http.Behavior = null;
```
