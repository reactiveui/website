---
Order: 5
---
# Serialization

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/serialization-index/serialization-index.csproj).

Your app works with C# objects, but a service sends and receives data in a format such as
JSON or XML. Refit uses a serializer to turn your objects into request bodies and turn reply
bodies back into values your app can use.

Refit selects one content serializer through `RefitSettings.ContentSerializer`. The default is
`SystemTextJsonContentSerializer`, which writes typed request bodies and reads typed replies.
Every serializer implements `IHttpContentSerializer`. A serializer can also implement
`ISynchronousContentSerializer` for buffered or streamed request bodies,
`IStreamingContentSerializer` for incremental `IAsyncEnumerable<T>` replies, or
`IJsonTypeInfoContentSerializer` for methods that take a `JsonTypeInfo<T>` parameter.
These capabilities are optional, so a custom serializer only needs to support the work its client requires.

Start with [System.Text.Json and generated metadata](json.md) for a new client. Generated JSON
metadata keeps the model contract available for trimming and Native AOT. Use the other pages
when the service or an existing app requires a different serializer or content format.

| Page | Use it for |
| --- | --- |
| [System.Text.Json](json.md) | The default JSON serializer, JSON contexts, your own serializer settings, metadata parameters and combining registrations. |
| [Newtonsoft.Json](newtonsoft-json.md) | The `Refit.Newtonsoft.Json` package, existing JSON converters and serialization rules. |
| [XML](xml.md) | The `Refit.Xml` package, XML models, namespaces and reader/writer configuration. |
| [Content serializers](content.md) | JSON Lines, streaming response formats, synchronous capabilities and custom serializers. |

The JSON and content pages include complete API tables with links to the implementation and
examples. The [request bodies](../requests/bodies.md) and [streaming replies](../results/streaming.md)
pages explain how those serializer capabilities fit into a Refit interface method.
