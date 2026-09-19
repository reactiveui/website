---
Order: 8
---
# Advanced Refit APIs

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/advanced-index/advanced-index.csproj).

Most apps can describe an API on an interface and let Refit handle the request. A custom
integration may need access to the pieces underneath that interface.

These pages explain those pieces: helpers that build and send requests and objects that hold
method details. Start here when you need to inspect or build client infrastructure around
request construction.

Read [generated request helpers](request-helpers.md) for paths, headers, options, bodies,
form descriptors, and sending. [Query building](query-builder.md) covers ordered escaping,
collections, generated attribute providers, and configured formatting.
[Method metadata and client names](method-metadata.md) covers reflected metadata objects
and the names used by HTTP client factory registration. Each page includes an overview followed
by an exhaustive reference table for the public members in that area.

For application code, start with [your first request](../index.md).
Read the [AOT guide](../aot.md) before publishing a trimmed or native app.
