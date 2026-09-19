---
Order: 5
---
# Serialization

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/serialization-index/serialization-index.csproj).

Your app works with C# objects, but a service sends and receives data in a format such as
JSON or XML. Refit uses a serializer to turn your objects into request bodies and turn reply
bodies back into values your app can use.

Start with [System.Text.Json and generated metadata](json.md) for a new client. The other
pages cover existing Newtonsoft.Json rules, XML services and custom content handling.

| Page | Use it for |
| --- | --- |
| [System.Text.Json](json.md) | Generated metadata, context reuse and combining registrations. |
| [Newtonsoft.Json](newtonsoft-json.md) | Existing JSON converters and serialization rules. |
| [XML](xml.md) | XML models, namespaces and reader/writer configuration. |
| [Content serializers](content.md) | Custom serializers, streaming formats and synchronous capabilities. |
