---
Order: 1
---
# Return types

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/results-return-types/results-return-types.csproj).

The same service call can be useful in different ways. A screen might await the returned
person, while another part of the app needs the status and headers too. Refit lets you choose
what the caller receives by changing the return type on the interface method.

Start with `Task<T>` for a single reply. This page also shows response wrappers and
`IObservable<T>`, which lets you subscribe to the result and combine it with other values.
The observable sections explain how that works without assuming you already know reactive programming.

## Read one reply

**1. Pick the return type.** All the person methods below read the same resource.
`Person` is the type from [the first request](../index.md#your-first-request).

```csharp
internal interface IReturnTypesApi
{
    [Get("/person")]
    Task<Person> GetTaskAsync(CancellationToken cancellationToken);

    [Get("/person")]
    ValueTask<Person> GetValueTaskAsync(CancellationToken cancellationToken);

    [Get("/person")]
    IObservable<Person> GetPerson(CancellationToken cancellationToken);

    [Get("/person")]
    Task<ApiResponse<Person>> GetResponseAsync(CancellationToken cancellationToken);

    [Get("/ping")]
    Task PingAsync(CancellationToken cancellationToken);
}
```

**2. Create the client.** The runnable example passes the sample host's shared HTTP client to
`RestService.ForGenerated<IReturnTypesApi>`. The client has a base address of `https://people.example`.
Use your app's HTTP client in the same way.

**3. Await the result.** The calling code stays much the same for `Task<T>` and `ValueTask<T>`.
Use `Task` for a call that has no result value.

```csharp
Person fromTask = await api.GetTaskAsync(cancellationToken);
Person fromValueTask = await api.GetValueTaskAsync(cancellationToken);
Console.WriteLine(fromTask.Name); // Ada
Console.WriteLine(fromValueTask.Name); // Ada
await api.PingAsync(cancellationToken);
```

As a general rule, await a `ValueTask<T>` once. If several callers need the same operation,
convert it to a `Task<T>` with `AsTask()` and share that task.
Refit backs its `ValueTask<T>` result with a `Task<T>`, so repeated awaits work for its returned values.
A `ValueTask<T>` return type does not make the network reply faster. The generated client wraps the
asynchronous send operation in that return type.

The [return-type examples](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/ReturnTypes)
exercise each shape against local replies. They also check that two subscriptions send two requests.

## Querying a reply

An `IObservable<T>` is a stream that pushes values to code you attach with `Subscribe`.
For a Refit request, it delivers one reply value and then completes, meaning no more values
will arrive. A failed call sends an error instead.

A Refit stream is **cold**. This means each subscription starts its own request.
Creating the stream or adding operators does not send the request.
An operator is a method that builds a new stream from another stream.

`Where` passes through values that match your test. `Select` changes each value with a lambda.
This is like LINQ for values that arrive later, such as replies or events.
The operators below filter the reply and pick its name. They do not filter requests before sending them.

Add `using ReactiveUI.Primitives;` to use these operators and the lambda overloads of `Subscribe`.
Refit's public method still returns the C# `IObservable<Person>` type.

```csharp
IObservable<string> names = api.GetPerson(cancellationToken)
    .Where(static person => person.Id > 0)
    .Select(static person => person.Name);

TaskCompletionSource finished = new(TaskCreationOptions.RunContinuationsAsynchronously);
using IDisposable subscription = names.Subscribe(
    Console.WriteLine,
    error => finished.TrySetException(error),
    () => finished.TrySetResult());
await finished.Task;
```

`Subscribe` attaches code for a value, an error and completion. It returns an `IDisposable`.
The `using` declaration keeps this subscription alive until the example finishes and then disposes it.
Disposing a live subscription cancels its request.
The `TaskCompletionSource` lets this console example wait for completion or failure before it exits.

Refit does not move these callbacks to your UI thread. The request may complete on a worker thread.
Dispatch any UI update to your framework's UI dispatcher.

Refit uses [Primitives](../../primitives/index.md) inside the runtime to build its streams.
You do not need a Primitives-specific interface to declare a Refit method.
For more operators, read [filtering](../../primitives/filtering.md),
[transformation](../../primitives/transformation.md), and
[subscription best practices](../../primitives/best-practices.md).

## Keep status and error details

`ApiResponse<T>` holds the result, HTTP status, headers and any captured error.
Use a response wrapper when your app needs those details as well as the reply value.
Dispose the wrapper after reading it.

```csharp
using ApiResponse<Person> response = await api.GetResponseAsync(cancellationToken);
await response.EnsureSuccessfulAsync();
if (response.HasContent)
{
    Console.WriteLine(response.Content.Name); // Ada
}
```

`EnsureSuccessfulAsync` throws the captured error if the call did not fully succeed.
It returns a `ValueTask` that you await. `HasContent` tells you whether `Content` is non-null.
An HTTP success status alone does not prove that Refit could read the reply into your C# type.

If a send fails before an HTTP reply exists, the concrete guards throw `InvalidOperationException`.
The interface guards throw the retained send failure. See the
[documented transport discrepancy](responses.md#when-no-reply-arrives) before choosing a guard.

The [complete return-type example](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/ReturnTypes)
runs each shape against local replies, including the two observable subscriptions.

## Choose a shape

| Return type | What happens |
| --- | --- |
| [`Task`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task) | Sends the request and completes without a result value. See [reading one reply](#read-one-reply). |
| [`Task<T>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1) | Sends the request and gives you one result to await. See [reading one reply](#read-one-reply). |
| [`ValueTask<T>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1) | Sends the request and gives you one task-backed result to await. See [reading one reply](#read-one-reply). |
| [`IObservable<T>`](https://learn.microsoft.com/dotnet/api/system.iobservable-1) | Sends a fresh request per subscription and pushes one result. See [querying a reply](#querying-a-reply). |
| [`Task<ApiResponse<T>>`](responses.md) | Gives you a result wrapper with status, headers and a captured error. See [keeping status and error details](#keep-status-and-error-details). |
| [`Task<IApiResponse<T>>`](responses.md) | Gives you the typed response wrapper through its interface. See [response details](responses.md). |
| [`Task<IApiResponse>`](responses.md) | Gives you response details without a typed reply body; the wrapper owns live response content. See [response details](responses.md). |
| [`Task<HttpRequestMessage>`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) | Builds a request and returns it without sending it; the caller owns and must dispose it. |
| [`Task<HttpResponseMessage>`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) | Returns the live HTTP response; the caller owns and must dispose it. |
| [`Task<HttpContent>`](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent) | Returns the live response content; the caller owns and must dispose it. |
| [`Task<Stream>`](https://learn.microsoft.com/dotnet/api/system.io.stream) | Returns the live response body stream; the caller owns and must dispose it. |
| [`Task<ApiResponse<HttpResponseMessage>>`](responses.md) | Wraps the live HTTP response; the caller owns and must dispose it. |
| [`Task<ApiResponse<HttpContent>>`](responses.md) | Wraps the live response content; the caller owns and must dispose it. |
| [`Task<ApiResponse<Stream>>`](responses.md) | Wraps the live response body stream; the caller owns and must dispose it. |
| [`IAsyncEnumerable<T>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.iasyncenumerable-1) | Reads items from one [streaming reply](streaming.md); the enumeration owns the live response until it ends. |

For buffered raw reply content, `Task<string>` reads text. The `HttpResponseMessage`, `HttpContent` and `Stream`
return types, including their `ApiResponse<T>` and `IApiResponse<T>` wrappers, expose live response resources instead;
dispose the returned value when you finish with it. The non-generic `IApiResponse` also owns live response content.
The `IAsyncEnumerable<T>` streaming shape likewise keeps the response live while it is enumerated.
A raw `HttpResponseMessage` lets you check the status yourself.

Read [response details](responses.md) for all wrapper properties and guards.
Read [error bodies](errors.md) to configure generated JSON metadata for failures and validation replies.
