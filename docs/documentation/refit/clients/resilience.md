---
Order: 5
---
# Resilience

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/clients-resilience/clients-resilience.csproj).

A service can fail for a moment. It restarts, it is busy, or a network link drops. Sending the same request again a
moment later often works. This is a *retry*. A *resilience handler* is an HTTP handler that makes those retries for you
and stops a call that takes too long.

Refit has no retry feature of its own. A generated client sends through an ordinary `HttpClient`, so you add
Microsoft's resilience handler to the client's registration. This page shows how to retry reads safely, what a retry
does not cover, and how to get a fresh token for each retry.

Install `Microsoft.Extensions.Http.Resilience` beside `Refit.HttpClientFactory`. Both work with Native AOT.

## Retry reads safely

The problem: a read fails because the service is briefly unavailable. You want the call to try again, but you never
want a create or an update to run twice.

**1. Declare the interface.** Nothing on the interface mentions retries. A `CancellationToken` parameter lets the
caller stop the call, including a retry that is waiting.

```csharp
internal interface IResilientPeopleApi
{
    [Get("/people/{id}")]
    Task<Person> GetPersonAsync(int id, CancellationToken cancellationToken);

    [Post("/people")]
    Task<Person> CreatePersonAsync([Body] Person person, CancellationToken cancellationToken);
}
```

**2. Choose the retry options.** The standard handler retries every HTTP method by default. A POST that timed out may
still have created the person, and sending it again could create a second one. `DisableForUnsafeHttpMethods` keeps
retries to methods that are safe to repeat, such as GET.

```csharp
internal static void ConfigureRetries(HttpStandardResilienceOptions options)
{
    options.Retry.MaxRetryAttempts = 3;
    options.Retry.Delay = TimeSpan.FromMilliseconds(100);
    options.Retry.DisableForUnsafeHttpMethods(); // POST, PUT, PATCH, DELETE and CONNECT are sent once
    options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(5); // limits each try
    options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(20); // limits every try and wait together
}
```

**3. Add the handler to the registration.** Call `AddStandardResilienceHandler` on the builder that
`AddRefitGeneratedClient<T>` returns.

```csharp
services.AddRefitGeneratedClient<IResilientPeopleApi>(SampleJsonContext.Default)
    .ConfigureHttpClient(static client => client.BaseAddress = new Uri("https://people.example"))
    .AddStandardResilienceHandler(ConfigureRetries);
```

**4. Call the client.** The first try gets `503 Service Unavailable`. The handler waits, tries again and gets the
person. Your code sees one successful call.

```csharp
Person person = await api.GetPersonAsync(1, CancellationToken.None); // the first try gets 503; the retry gets Ada
Console.WriteLine(person.Name); // Ada
Console.WriteLine(http.Requests.Count); // 2
```

The sample answers with a local `StubHttp` handler from `Refit.Testing`, so it shows both tries without a server.

## What a retry does not cover

The resilience handler sees HTTP responses. It does not see what Refit does with a response afterwards.

| Situation | What happens |
| --- | --- |
| A POST gets `503` | With `DisableForUnsafeHttpMethods`, the handler sends it once. Refit throws `ApiException` with status `ServiceUnavailable`. |
| A GET gets `200` with a body Refit cannot read | The handler sees a success and stops. Refit then fails to read the body and throws `ApiException`. A retry would not help, because the service sent a bad body. |
| The caller cancels | The call ends with `OperationCanceledException`, even during a wait between tries. |

```csharp
try
{
    _ = await api.CreatePersonAsync(new Person(2, "Grace"), CancellationToken.None);
}
catch (ApiException error)
{
    status = error.StatusCode; // ServiceUnavailable: the POST was sent once and not retried
}
```

## Choose the timeouts

The standard handler has two timeouts:

- **Attempt timeout** limits one try. A try that takes longer is cancelled and can be retried.
- **Total request timeout** limits the whole call: every try and every wait between them.

`HttpClient.Timeout` still applies around everything, and it defaults to 100 seconds. Keep the total request timeout
shorter than it, or the client stops the call before the handler finishes retrying. For one slow method, Refit's
`[Timeout]` attribute sets a deadline for that method's calls. See
[per-call deadline policies](../requests/bodies.md#uri-and-per-call-deadline-policies).

## Get a fresh token for each retry

The problem: an access token can expire between the first try and the retry. You want every try to carry a current
token.

`AddAuthorizationHeaderValueProvider` adds a handler that asks your delegate for a token before each request. See
[dependency injection](dependency-injection.md#resolve-authorization-per-request) for how it creates a scope.

Handlers run in the order you add them. The first one you add is the outer handler: it wraps everything added after
it. Add the resilience handler first and the token provider second. Every retry then passes through the token provider
again.

```csharp
IHttpClientBuilder builder = services.AddRefitGeneratedClient<IResilientPeopleApi>(SampleJsonContext.Default)
    .ConfigureHttpClient(static client => client.BaseAddress = new Uri("https://people.example"));
builder.AddStandardResilienceHandler(ConfigureRetries); // added first: the outer handler, which repeats what follows
builder.AddAuthorizationHeaderValueProvider((_, _, _) =>
    ValueTask.FromResult($"token-{Interlocked.Increment(ref tokensIssued)}")); // added second: runs again for every try
```

Keep the builder in a variable. `AddStandardResilienceHandler` returns a resilience pipeline builder, not the
`IHttpClientBuilder`, so you cannot chain the token provider after it.

```csharp
Console.WriteLine(string.Join(", ", tokensSent)); // token-1, token-2: the retry asked for a new token
```

If you add the two handlers in the other order, the token is fetched once and every retry sends `token-1`.

## Streamed uploads

A retry sends the same request content again. A [JSON Lines upload](../requests/bodies.md#upload-many-records-as-json-lines)
from an `IAsyncEnumerable<T>` can be sent only once, so a second try throws `InvalidOperationException` instead of
sending the records again. `DisableForUnsafeHttpMethods` already keeps POST and PUT uploads to one try. To retry an
upload, call the method again with a new sequence, as
[retry a failed upload](../requests/bodies.md#retry-a-failed-upload) shows.

Each page of a [paged method](../results/pagination.md) is a separate GET request. The handler retries one page without
fetching the earlier pages again.

## API reference

| API | Package | Description |
| --- | --- | --- |
| `IHttpClientBuilder.AddStandardResilienceHandler(Action<HttpStandardResilienceOptions>)` | `Microsoft.Extensions.Http.Resilience` | Adds rate limiting, a total timeout, retries, a circuit breaker and an attempt timeout, in that order. |
| `HttpRetryStrategyOptions.DisableForUnsafeHttpMethods()` | `Microsoft.Extensions.Http.Resilience` | Stops retries for POST, PUT, PATCH, DELETE and CONNECT. |
| `IHttpClientBuilder.AddAuthorizationHeaderValueProvider(...)` | `Refit.HttpClientFactory` | Sets the `Authorization` header from a delegate before each request that passes through it. |

Microsoft's [HTTP resilience guide](https://learn.microsoft.com/dotnet/core/resilience/http-resilience) describes every
option of the standard handler.

Source: [`Resilience.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Resilience/Resilience.cs)
and [`IResilientPeopleApi.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Resilience/IResilientPeopleApi.cs).
