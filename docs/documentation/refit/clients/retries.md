---
Order: 7
---
# HTTP-aware retries

When a service is busy, it may tell you when to try again. `HttpRetryHandler` reads that
response header and waits before sending a fresh request. Without a valid hint, it uses
exponential backoff: each wait grows until it reaches a configured limit.

The handler is opt-in and works with both generated Refit clients and ordinary `HttpClient`.
It lives in `Refit` on .NET 8 and later. It needs no interface attributes, generator changes
or resilience package.

## Make a retrying client

**1. Describe a read.** This method returns the HTTP response itself, so it needs no JSON
configuration. Dispose the response after reading it.

```csharp
internal interface IStatusApi
{
    [Get("/status")]
    Task<HttpResponseMessage> GetStatusAsync(CancellationToken cancellationToken);
}
```

**2. Choose the policy and transport.** `MaxRetries` counts extra sends after the first.
Choose the delay reader that matches your provider.

```csharp
var handler = new HttpRetryHandler(new HttpRetryOptions
{
    MaxRetries = 3,
    DelayProvider = RetryDelayProviders.Azure,
})
{
    InnerHandler = new HttpClientHandler(),
};
using var client = new HttpClient(handler)
{
    BaseAddress = new Uri("https://api.example"),
};
```

**3. Create and call the generated client.** The same cancellation token stops sends and
retry waits.

```csharp
var api = RestService.ForGenerated<IStatusApi>(client);
using var response = await api.GetStatusAsync(cancellationToken);
```

For dependency injection, add a fresh handler to the client registration:

```csharp
services.AddRefitGeneratedClient<IStatusApi>()
    .ConfigureHttpClient(static client => client.BaseAddress = new Uri("https://api.example"))
    .AddHttpMessageHandler(static () => new HttpRetryHandler(new HttpRetryOptions
    {
        DelayProvider = RetryDelayProviders.Azure,
    }));
```

You can also supply the complete chain through `RefitSettings.HttpMessageHandlerFactory`.
See [client creation](creation.md) and [dependency injection](dependency-injection.md).

## Choose which failures retry

By default, the handler retries HTTP 408, 429, 500, 502, 503 and 504, plus
`HttpRequestException`. HTTP 403 retries only when the delay reader finds a valid timing hint.
An ordinary authorization failure does not retry. Successful responses do not retry.

`ShouldRetry` replaces response classification. Transport failures have their own switch:

```csharp
var options = new HttpRetryOptions
{
    ShouldRetry = static response =>
        response.StatusCode is HttpStatusCode.TooManyRequests
            or HttpStatusCode.ServiceUnavailable,
    RetryHttpRequestExceptions = false,
};
```

GET, HEAD, OPTIONS and TRACE may retry by default. Set `RetryUnsafeMethods = true` only
when replay is safe for the endpoint. For a create operation, that usually means a
server-supported idempotency key: a key that lets the server recognize repeated sends of
the same operation. Cancellation and exceptions other than `HttpRequestException` never retry.

`MaxRetries = 0` bypasses both retries and body buffering.

## Read provider timing hints

Readers try these headers in priority order:

| Reader | Headers and units |
| --- | --- |
| `RetryAfter` (default) | `Retry-After`: integer seconds or an HTTP date. |
| `Azure` | `x-ms-retry-after-ms`, then `retry-after-ms`: milliseconds; then `Retry-After`. |
| `Aws` | `x-amz-retry-after`: milliseconds; then `Retry-After`. |
| `GitHub` | `Retry-After`; otherwise `x-ratelimit-reset` in Unix seconds on 403/429 when `x-ratelimit-remaining` is zero. |
| `Jira` | `Retry-After`; otherwise `X-RateLimit-Reset` as ISO 8601 on 429. |

Microsoft Graph and Slack use standard `Retry-After`, so the default reader covers them.
The AWS reader reads timing headers, not the AWS SDK's full retry policy.

Readers reject ambiguous multiple values, malformed numbers and overflow. An invalid
higher-priority header falls through to the next header. A past absolute timestamp means
the retry may start immediately.

For a custom header, choose its encoding:

```csharp
var options = new HttpRetryOptions
{
    DelayProvider = static (response, now, token) =>
        RetryDelayProviders.ReadHeader(
            response, "X-Retry-Milliseconds", RetryHeaderFormat.Milliseconds, now, token)
        ?? RetryDelayProviders.RetryAfter(response, now, token),
};
```

`RetryHeaderFormat` supports seconds, milliseconds, Unix seconds, standard `Retry-After`
and ISO 8601 dates. `DelayProvider = null` disables header hints.

## Backoff, jitter and time

The fallback starts at one second and doubles up to 30 seconds. Full jitter randomizes the
fallback wait between zero and that attempt's cap. Set `UseJitter = false` for a fixed wait.
`BaseDelay` and `MaxDelay` change the limits.

Valid server hints replace fallback backoff. Jitter never shortens a server hint. A negative
custom interval or a hint above `MaxDelay` stops retries and returns the response. Increase
`MaxDelay` if you want to wait through a longer provider rate-limit window.

`TimeProvider` supplies both the clock for dates and the timers for waits.
`HttpClient.Timeout` bounds the whole logical call, including those waits.

Options are copied when the handler is created. Custom callbacks must be thread-safe
because calls can share a handler.

## Request replay and ownership

Each send gets a fresh request with the original headers, HTTP version, options and content
headers. Bodies are serialized once into a bounded buffer. The default limit is one MiB,
set through `MaxRequestContentBufferSize`. Oversized bodies fail before the first send.
Do not use retries for unbounded streaming uploads.

The handler disposes failed responses and cloned requests. The caller owns the original
request and final response. The handler does not read or buffer response content.

A failed JSON read happens after the HTTP handler has returned. Retrying at this layer
does not cover it. A Microsoft Graph batch can report throttled operations inside an HTTP
200 response; handle those operations in application code rather than replaying the batch.
A GitHub secondary limit without timing headers cannot be distinguished from an ordinary
403 by a header-only reader.

## Test retries without a network

`Refit.Testing` provides `RetryReply.After` for seconds or dates and `RetryReply.Header` for
provider-specific or deliberately invalid hints. Each match creates a fresh response.
Queue one-shot routes with the same matcher to supply a failure followed by success:

```csharp
using var stub = new StubHttp();
stub.Add(Route.Get("/status"), RetryReply.After(TimeSpan.Zero));
stub.Add(Route.Get("/status"), Reply.Status(HttpStatusCode.OK));
using var client = new HttpClient(stub.CreateRetryHandler())
{
    BaseAddress = new Uri("https://api.test"),
};
var api = RestService.ForGenerated<IStatusApi>(client);
using var response = await api.GetStatusAsync(cancellationToken);
await stub.VerifyAllCalledAsync();
```

`StubHttp.CreateRetryHandler()` uses the stub's `TimeProvider`. The overload taking options
uses their clock instead. To test nonzero waits, give the stub a `FakeTimeProvider` from
`Microsoft.Extensions.TimeProvider.Testing` and advance it after a retry timer is registered.
For asynchronous responders and multiple retries, observe timer registration rather than
guessing when to advance. The [retry timing tests](https://github.com/reactiveui/refit/blob/main/src/tests/Refit.Testing.Tests/HttpRetryHandlerTests.Time.cs)
show that pattern.

See [testing](../testing/index.md) for route matching and verification, and
[testing streams and time](../testing/streaming.md) for the stub clock.

## Compose other resilience features

A *resilience strategy* changes how a call handles failures or load. The handler boundary
also fits timeouts, concurrency limits and circuit breakers. They are not built into
`HttpRetryHandler`.

- Put an attempt timeout or concurrency limit inside retries so it applies to each send.
- Put a total-call timeout outside retries so it covers sends and waits together.
- Put a circuit breaker inside retries to count attempts, or outside to count logical failures.
  A circuit breaker stops calls temporarily after repeated failures.
- Partition rate limits and breakers by provider, tenant or endpoint so one busy service
  does not pause unrelated requests.

Hedging sends parallel attempts and needs stricter replay safeguards. A typed fallback
chooses replacement application data, so it belongs above the HTTP layer.

Both `Microsoft.Extensions.Resilience` and `Microsoft.Extensions.Http.Resilience` are
[built on Polly](https://learn.microsoft.com/dotnet/core/resilience/).
They remain optional. See [Microsoft resilience integration](resilience.md).
Do not stack retry strategies without calculating their combined attempts.
Three retries in each of two layers can produce sixteen network sends.

## Provider references

- [HTTP Retry-After, RFC 9110 section 10.2.3](https://www.rfc-editor.org/rfc/rfc9110.html#section-10.2.3)
- [Azure Cosmos DB response headers](https://learn.microsoft.com/rest/api/cosmos-db/common-cosmosdb-rest-response-headers)
- [Azure OpenAI quota guidance](https://learn.microsoft.com/azure/ai-services/openai/how-to/quota)
- [Microsoft Graph throttling](https://learn.microsoft.com/graph/throttling)
- [AWS SDK retry behavior](https://docs.aws.amazon.com/sdkref/latest/guide/feature-retry-behavior.html)
- [GitHub REST rate limits](https://docs.github.com/en/rest/using-the-rest-api/rate-limits-for-the-rest-api)
- [Slack Web API rate limits](https://docs.slack.dev/apis/web-api/rate-limits/)
- [Jira Cloud rate limiting](https://developer.atlassian.com/cloud/jira/platform/rate-limiting/)
