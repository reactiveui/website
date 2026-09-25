---
Order: 3
---
# XML content

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/serialization-xml/serialization-xml.csproj).

Some services expect XML requests and return XML replies. The `Refit.Xml` package lets you
work with those services through C# models, while Refit handles writing and reading the XML.

The model's XML attributes control names such as the root element and each field. This page
starts with a simple person model, then covers namespaces and settings for services with stricter XML requirements.

## Configure an XML request and reply

[`XmlContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializer.cs) uses .NET's [`XmlSerializer`](https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer), including runtime reflection and code generation.
These examples do not establish trimming or Native AOT compatibility.

**1. Use a model the XML serializer can construct.** The example has a public class with
public setters and a parameterless constructor. `[XmlRoot]` names its root element.
`[XmlElement]` names a property element. Its JSON attribute is also used by the companion
[Newtonsoft.Json example](newtonsoft-json.md).
The attributes require `System.Xml.Serialization` and `Newtonsoft.Json` imports.

```csharp
[XmlRoot("person")]
public sealed class WirePerson
{
    public int Id { get; set; }

    [JsonProperty("display_name")]
    [XmlElement("display_name")]
    public string Name { get; set; } = string.Empty;
}
```

**2. Supply the XML settings to Refit.** `XmlContentSerializer()` creates defaults.
Its other constructor accepts a non-null `XmlContentSerializerSettings` instance.
The reader/writer settings have four constructors: defaults, reader only, writer only,
or both. Each retains the settings objects you supply.

```csharp
XmlReaderWriterSettings defaults = new();
XmlReaderSettings reader = new() { IgnoreComments = true };
XmlWriterSettings writer = new() { Encoding = Encoding.UTF8, Indent = true };
XmlReaderWriterSettings withReader = new(reader);
XmlReaderWriterSettings withWriter = new(writer);
XmlReaderWriterSettings both = new(reader, writer);
both.ReaderSettings = reader;
both.WriterSettings = writer;
XmlContentSerializerSettings xmlSettings = new()
{
    XmlDefaultNamespace = null,
    XmlReaderWriterSettings = both,
    XmlNamespaces = new([new(string.Empty, string.Empty)]),
    XmlAttributeOverrides = new(),
};
XmlContentSerializer serializer = new(xmlSettings);
RefitSettings settings = new(serializer);
XmlContentSerializer defaultSerializer = new();
```

Add `using System.Xml;` and `using System.Text;` for the settings and encoding.
Pass `settings` to a [client-creation method](../clients/creation.md).

**3. Read the reply.** Refit's serializer writes the request content and reads the response
content through these methods. The content declares `application/xml` and its writer's charset.

```csharp
using HttpContent content = serializer.ToHttpContent(new WirePerson { Id = 1, Name = "Ada" });
Console.WriteLine(content.Headers.ContentType?.MediaType); // application/xml
WirePerson? person = await serializer.FromHttpContentAsync<WirePerson>(content, cancellationToken);
Console.WriteLine(person?.Name); // Ada
```

## Buffered parsing and field names

`DeserializeFromString<T>` reads an already buffered XML string synchronously.
`GetFieldNameForProperty` returns the `ElementName` from `[XmlElement]`, then the
`AttributeName` from `[XmlAttribute]` when no element attribute exists. It returns `null`
without either attribute.

```csharp
WirePerson? person = serializer.DeserializeFromString<WirePerson>("<person><Id>1</Id><display_name>Ada</display_name></person>");
string? name = serializer.GetFieldNameForProperty(typeof(WirePerson).GetProperty(nameof(WirePerson.Name))!);
Console.WriteLine(person?.Name); // Ada
Console.WriteLine(name); // display_name
```

## Settings reference

Configure settings before you use a serializer. It caches an `XmlSerializer` for each type.
Changing model overrides or namespaces later does not rebuild an existing cache entry.

| `XmlContentSerializerSettings` member | Description | Default and purpose |
| --- | --- | --- |
| [`XmlContentSerializerSettings()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializerSettings.cs) | Creates settings for XML request and response serialization. | `XmlDefaultNamespace` is `null`; reader/writer settings are new; namespaces contain one empty-prefix/empty-namespace mapping; attribute overrides are empty. |
| [`XmlDefaultNamespace`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializerSettings.cs) | [`string?`](https://learn.microsoft.com/en-us/dotnet/api/system.string); the default XML namespace passed when constructing a serializer for deserialization. | `null` means no default namespace. The value is used when the type's serializer is first cached for reading. |
| [`XmlReaderWriterSettings`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializerSettings.cs) | [`XmlReaderWriterSettings`](xml.md); the paired reader and writer configuration. | Defaults to a new instance. Accessing its reader or writer applies asynchronous operation and safe DTD settings. |
| [`XmlNamespaces`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializerSettings.cs) | [`XmlSerializerNamespaces`](https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializernamespaces); namespace prefixes and URIs supplied to `XmlSerializer.Serialize`. | Defaults to one empty-prefix/empty-namespace mapping. |
| [`XmlAttributeOverrides`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializerSettings.cs) | [`XmlAttributeOverrides`](https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlattributeoverrides); alternate XML mappings for model types. | Defaults to an empty collection. Overrides are read when a type's cached `XmlSerializer` is created. |

The write path creates its cached serializer from the item's runtime type and overrides.
The read path uses the requested `T`, overrides and default namespace. Both use the same
type-keyed cache. The first path used for a type determines that entry's construction settings.

**Implementation discrepancy:** writing a type first can prevent a later read from using
`XmlDefaultNamespace`. A read should honor its configured namespace regardless of earlier writes.
The [namespace example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Serialization/Other/OtherSerializers.cs)
reproduces both call orders. Reading first succeeds; writing first makes the later read throw.

Use a separate serializer for reading when you depend on `XmlDefaultNamespace`.
Here, `serviceNamespace` is `"urn:people"`:

```csharp
XmlContentSerializer namespaceReader = new(new() { XmlDefaultNamespace = serviceNamespace });
const string namespacedXml = "<person xmlns=\"urn:people\"><Id>1</Id><display_name>Ada</display_name></person>";
WirePerson? person = namespaceReader.DeserializeFromString<WirePerson>(namespacedXml);
```

When the same model needs one namespace for both reading and writing, set an explicit root
mapping in `XmlAttributeOverrides`. The complete example verifies that round trip too.

| `XmlReaderWriterSettings` member | Description | Behavior |
| --- | --- | --- |
| [`XmlReaderWriterSettings()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlReaderWriterSettings.cs) | Creates paired XML reader and writer settings. | Both settings are new defaults. |
| [`XmlReaderWriterSettings(XmlReaderSettings readerSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlReaderWriterSettings.cs) | Takes reader settings and creates the writer settings. | Retains `readerSettings`; the writer settings are new defaults. A null argument throws `ArgumentNullException`. |
| [`XmlReaderWriterSettings(XmlWriterSettings writerSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlReaderWriterSettings.cs) | Takes writer settings and creates the reader settings. | Retains `writerSettings`; the reader settings are new defaults. A null argument throws `ArgumentNullException`. |
| [`XmlReaderWriterSettings(XmlReaderSettings readerSettings, XmlWriterSettings writerSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlReaderWriterSettings.cs) | Takes both caller-supplied settings. | Retains both objects. Either null argument throws `ArgumentNullException`. |
| [`ReaderSettings`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlReaderWriterSettings.cs) | [`XmlReaderSettings`](https://learn.microsoft.com/en-us/dotnet/api/system.xml.xmlreadersettings); gets or replaces the reader settings. | Assignment rejects `null`. Getting the value sets `Async = true`; unless `AllowDtdProcessing` is enabled, it also sets `DtdProcessing.Prohibit` and clears `XmlResolver`. |
| [`WriterSettings`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlReaderWriterSettings.cs) | [`XmlWriterSettings`](https://learn.microsoft.com/en-us/dotnet/api/system.xml.xmlwritersettings); gets or replaces the writer settings. | Assignment rejects `null`. Getting the value sets `Async = true`. |
| [`AllowDtdProcessing`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlReaderWriterSettings.cs) | [`bool`](https://learn.microsoft.com/en-us/dotnet/api/system.boolean); compatibility opt-out from Refit's DTD hardening. | Defaults to `false` and is obsolete. Setting it to `true` leaves caller-configured DTD processing and resolver settings in place. |

Each reader/writer property access sets both objects' `Async` properties to true.
By default it also sets `DtdProcessing.Prohibit` and clears the XML resolver. A DTD can
instruct a parser to load external data or expand entities. Those instructions are unsafe
in an untrusted response. The obsolete opt-out preserves caller-supplied DTD settings;
its compiler warning is intentional. These examples do not enable it.

The [compiler-host example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Tooling/CompatibilitySample.cs)
checks that reading and writing the opt-out both produce `CS0618`. It compiles this input to
inspect diagnostics; it does not execute the opt-out or parse a DTD:

```csharp
const string source = """
    internal static class XmlPolicy
    {
        internal static bool Configure(Refit.XmlReaderWriterSettings settings)
        {
            settings.AllowDtdProcessing = true;
            return settings.AllowDtdProcessing;
        }
    }
    """;
CSharpCompilation compilation = ToolingCompilation.Create(source);
ToolingCompilation.RequireNoErrors(compilation);
```

## Method reference

| `XmlContentSerializer` member | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`XmlContentSerializer()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializer.cs) | Creates an XML content serializer with default settings. | None | Uses a new [`XmlContentSerializerSettings`](xml.md). |
| [`XmlContentSerializer(XmlContentSerializerSettings settings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializer.cs) | Creates an XML content serializer with caller-supplied settings. | `settings`: non-null [`XmlContentSerializerSettings`](xml.md) | Stores the settings; `null` throws [`ArgumentNullException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception). |
| [`ToHttpContent<T>(T item)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializer.cs) | Serializes a value for an XML HTTP request. | `item`: value to serialize | Returns [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) with media type `application/xml` and the configured writer charset. `null` throws [`ArgumentNullException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception). The runtime type selects the cached `XmlSerializer`. |
| [`FromHttpContentAsync<T>(HttpContent content, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializer.cs) | Reads and deserializes an XML HTTP response. | `content`: [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) to read; `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken), default `default` | Returns [`Task<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1). It buffers the content as a string, then parses it synchronously with the serializer for `T`; cancellation applies while reading the content. |
| [`DeserializeFromString<T>(string content)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializer.cs) | Deserializes buffered XML text. | `content`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) containing XML | Returns `T?` parsed with the configured reader, default namespace, and attribute overrides. |
| [`GetFieldNameForProperty(PropertyInfo propertyInfo)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Xml/XmlContentSerializer.cs) | Finds the XML field name declared on a property. | `propertyInfo`: [`PropertyInfo`](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) to inspect | Returns the `ElementName` from an [`XmlElementAttribute`](https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlelementattribute), otherwise the `AttributeName` from an [`XmlAttributeAttribute`](https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlattributeattribute), otherwise `null`. A null property throws [`ArgumentNullException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception). |

These APIs do not stream a sequence of models through `IAsyncEnumerable<T>`.
The configured XML serializer also does not implement Refit's synchronous request-writing
interface. Use the default request serialization mode for it.

## Run the example

The complete [optional serializer project](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Serialization/Other/Other.csproj)
targets .NET 10 and C# 14. Run from the Refit checkout's `src` folder:

```bash
dotnet run --project examples/Documentation/Serialization/Other/Other.csproj -c Release -p:LangVersion=14.0
```

The separate executable checks XML settings and both parsing methods. The native
documentation runner uses generated System.Text.Json metadata.
