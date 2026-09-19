---
Order: 2
---
# Requests

To call a web service, your app needs to describe what it wants and supply the data the service
expects. With Refit, you put that description on an interface method. Its attributes tell Refit
where the call goes and how to turn the arguments into a request.

These pages walk through the pieces of a request, from a simple URL to search values,
authorization headers and file uploads.

Start with [routes and HTTP methods](routes.md) to describe a request in an interface.
You can also ask Refit to build a request without sending it. This helps you inspect its URL and headers.

[Query names, values and collections](queries.md) shows how to send search inputs in the URL.
[Query converters](query-converters.md) show how to encode a value with a custom JSON converter.
[Query formatters](query-formatters.md) show how to format query names, values and form fields.

Use [headers](headers.md) for request metadata and [request context](request-context.md) for local options.
[Request bodies](bodies.md) covers JSON, text, streams, forms, JSON Lines and compression.
[Multipart uploads](multipart.md) covers files, streams and named form parts.
