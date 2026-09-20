---
Order: 3
---
# Results

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/results-index/results-index.csproj).

After sending a request, your app needs to decide what to do with the reply. Sometimes you
only need a person or a list of records. Other calls need the status and headers, an explanation
of a failure, or a way to process items as they arrive.

The return type on your Refit method controls what your caller receives. Start with a task
returning the deserialized body for an ordinary request. Refit reports an unsuccessful HTTP
response, a transport failure, or a deserialization failure as an exception for that form.

Choose `ApiResponse<T>` or `IApiResponse<T>` when you want to inspect the status, headers,
body, and error on the returned value. Dispose an `ApiResponse<T>` after you finish with it.
Use `IObservable<T>` when the request result should be delivered through a subscription. It
represents one request result. Use `IAsyncEnumerable<T>` when one response contains a stream
of items that your code should read with `await foreach`. Use `PagedEnumerable<TPage, TItem>` when a
service returns a long list in pages and your code should read every item.

These pages help you choose a return form and handle its result.

| Page | What it covers |
| --- | --- |
| [Return types](return-types.md) | Awaiting one result, querying a reply, and checking a response wrapper. |
| [Streaming replies](streaming.md) | Reading several items with `await foreach`. |
| [Response details](responses.md) | Status, content, headers, transport failures and success guards. |
| [Error bodies](errors.md) | Typed error JSON, problem details and custom failure handling. |
| [Return adapters](adapters.md) | Custom wrappers and matching generic return types. |
| [Pagination](pagination.md) | Reading every item of a paged list with cursors, offsets, headers and links. |
