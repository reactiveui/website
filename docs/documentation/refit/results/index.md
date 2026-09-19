---
Order: 3
---
# Results

After sending a request, your app needs to decide what to do with the reply. Sometimes you
only need a person or a list of records. Other calls need the status and headers, an explanation
of a failure, or a way to process items as they arrive.

The return type on your Refit method controls what your caller receives. These pages help
you choose a form and handle its result.

| Page | What it covers |
| --- | --- |
| [Return types](return-types.md) | Awaiting one result, querying a reply, and checking a response wrapper. |
| [Streaming replies](streaming.md) | Reading several items with `await foreach`. |
| [Response details](responses.md) | Status, content, headers, transport failures and success guards. |
| [Error bodies](errors.md) | Typed error JSON, problem details and custom failure handling. |
| [Return adapters](adapters.md) | Custom wrappers and matching generic return types. |
