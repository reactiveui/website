---
Order: 14
---
# Give HTTP requests a priority

If a user opens a profile while the app is fetching thumbnails, send the profile
request through Fusillade's foreground handler. A handler sits between an `HttpClient`
and its transport. Fusillade uses a shared queue to give foreground reads priority.

Install `Fusillade` and import `Fusillade`. Create a client for each kind of work
and reuse it. The handlers belong to `NetCache`, so leave them alive when disposing a client.

```csharp
using var foreground = new HttpClient(NetCache.UserInitiated, disposeHandler: false);
string profile = await foreground.GetStringAsync(profileUri, cancellationToken);
```

| Handler | When to use it |
| --- | --- |
| `NetCache.UserInitiated` | The user is waiting for the result. |
| `NetCache.Background` | The app is refreshing data in the background. |
| `NetCache.Speculative` | The app may need the data soon and has a byte budget for prefetching. |
| `NetCache.Offline` | The app should read a previously saved response body. |

## Share an in-flight read

Two matching GET requests sent through the same `RateLimitedHttpMessageHandler` while the
first is still running share one download. Each caller gets its own response to dispose.
HEAD and OPTIONS also participate. Other methods go directly to the inner handler.
Cancelling one caller leaves the download running for the remaining callers.

```csharp
using var queue = new OperationQueue(maximumConcurrent: 2);
using var handler = new RateLimitedHttpMessageHandler(
    new HttpClientHandler(), Priority.UserInitiated, operationQueue: queue);
using var client = new HttpClient(handler);
Task<string> first = client.GetStringAsync(profileUri, cancellationToken);
Task<string> second = client.GetStringAsync(profileUri, cancellationToken);
string[] profiles = await Task.WhenAll(first, second);
```

Import `Punchclock` for `OperationQueue`. Each handler adds its `priority` offset to
its base priority. Choose `Priority.Explicit` to start from zero. The other bases are
`Speculative` (10), `Background` (20) and `UserInitiated` (100). Pass the same queue
to multiple handlers to share the concurrency limit. The built-in handlers use
`NetCache.OperationQueue`. [Queue priorities and concurrency](../punchclock/index.md).

## Limit speculative transfers

Set `maxBytesToRead` on a custom handler or call `NetCache.Speculative.ResetLimit(bytes)`
to give the shared handler a fresh budget. A completed response with a known content
length reduces the balance. The response that takes the balance below zero can finish.
Subsequent eligible reads are cancelled until you reset the limit. Calling `ResetLimit()`
removes the cap. The built-in speculative handler starts with 5 MiB.

To display the last budget your app set, derive from `LimitingHttpMessageHandler` and override `ResetLimit(long?)`. The parameterless `ResetLimit()` calls the override with `null`. The example forwards the limit to the transport and records it for the UI. [Read the prefetch budget handler](https://github.com/reactiveui/Fusillade/blob/main/src/examples/HttpCache/PrefetchBudgetHandler.cs).

## Read a saved body offline

Implement `IRequestCache.SaveAsync` to store the response bytes under the supplied key.
Implement `FetchAsync` to return those bytes on a hit or `null` on a miss. Assign the
cache to `NetCache.RequestCache` at startup. You can also pass the two callbacks directly
when constructing the handlers.

```csharp
using var handler = new OfflineHttpMessageHandler(cache.FetchAsync);
using var offline = new HttpClient(handler);
using HttpResponseMessage reply = await offline.GetAsync(profileUri, cancellationToken);
```

The offline handler returns HTTP 200 for a saved body and 503 for a miss. It needs
either a fetch callback or a registered cache. The key includes the request method,
URI and selected headers, including authorization. Use
`RateLimitedHttpMessageHandler.UniqueKeyForRequest(request)` for a matching lookup.

## Use your application's handler registrations

With Splat, import `Splat.Builder` and call `app.CreateFusilladeNetCache()` on your
built `IAppInstance`. This connects `NetCache` to that app's resolver. You can supply
your own handlers through the `NetCache` properties.

For System.Reactive applications, install `Fusillade.Reactive` and use the
`Fusillade.Reactive` and `Punchclock.Reactive` namespaces.
The local example shares a download, reads the cached bytes offline and resets a
prefetch budget with both packages. [Run the HTTP cache examples](https://github.com/reactiveui/Fusillade/tree/main/src/examples).
