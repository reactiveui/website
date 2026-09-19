---
Order: 8
---
# Advanced Refit APIs

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/advanced-index/advanced-index.csproj).

Most apps can describe an API on an interface and let Refit handle the request. If you are
building a client generator, an editor tool or a custom integration, you may need access
to the pieces underneath that interface.

These pages explain those pieces: helpers that build and send requests, objects that hold
method details, and compiler tools that generate and check client code. Start here when
you need to extend how Refit works or integrate it into your own tooling.

Read [generated request helpers](request-helpers.md) for paths, headers, options, bodies,
form descriptors, and sending. [Query building](query-builder.md) covers ordered escaping,
collections, generated attribute providers, and configured formatting.
[Method metadata and client names](method-metadata.md) covers reflected metadata objects
and the names used by HTTP client factory registration.

Read [compiler tooling](tooling.md) for source generators, analyzer messages, code fixes, identifier
helpers, and the public index and range types inside the tooling assemblies.
The [runnable tooling examples](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Tooling)
use .NET 10 and C# 14.

For application code, start with [your first request](../index.md).
Read the [AOT guide](../aot.md) before publishing a trimmed or native app.
