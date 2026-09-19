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

`XmlContentSerializer` uses .NET's `XmlSerializer`, including runtime reflection and code generation.
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
WirePerson? person = await serializer.FromHttpContentAsync<WirePerson>(content, CancellationToken.None);
Console.WriteLine(person?.Name); // Ada
```

## Buffered parsing and field names

`DeserializeFromString<T>` reads an already buffered XML string synchronously.
`GetFieldNameForProperty` returns an explicit `[XmlElement]` name, then an `[XmlAttribute]`
name when no element attribute exists. It returns null without either attribute.

```csharp
WirePerson? person = serializer.DeserializeFromString<WirePerson>("<person><Id>1</Id><display_name>Ada</display_name></person>");
string? name = serializer.GetFieldNameForProperty(typeof(WirePerson).GetProperty(nameof(WirePerson.Name))!);
Console.WriteLine(person?.Name); // Ada
Console.WriteLine(name); // display_name
```

## Settings reference

Configure settings before you use a serializer. It caches an `XmlSerializer` for each type.
Changing model overrides or namespaces later does not rebuild an existing cache entry.

| `XmlContentSerializerSettings` member | Default and purpose |
| --- | --- |
| Constructor | Creates reader/writer settings, an empty default namespace mapping and empty attribute overrides. |
| `XmlDefaultNamespace` | Null. Supplies the default namespace when constructing a serializer for deserialization. |
| `XmlReaderWriterSettings` | New settings wrapper. Controls XML readers and writers; keep it non-null. |
| `XmlNamespaces` | One empty-prefix/empty-namespace mapping. Supplies namespace declarations when writing. |
| `XmlAttributeOverrides` | Empty overrides. Changes a model's XML mapping when its cached serializer is first created. |

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

| `XmlReaderWriterSettings` member | Behavior |
| --- | --- |
| Four constructors | Accept no settings, a non-null reader, a non-null writer, or both. Missing components get defaults. |
| `ReaderSettings` | Gets or replaces the reader object. A null replacement throws. Reading the property applies Refit's required settings. |
| `WriterSettings` | Gets or replaces the writer object. A null replacement throws. Reading the property applies Refit's required settings. |
| `AllowDtdProcessing` | False by default; obsolete security opt-out. Leave it false for service responses. |

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

| `XmlContentSerializer` member | Behavior |
| --- | --- |
| Two constructors | Use defaults or supplied non-null XML settings. |
| `ToHttpContent<T>(T)` | Rejects null. Writes the item's runtime type into a memory buffer and returns byte content. |
| `FromHttpContentAsync<T>(HttpContent, CancellationToken)` | Reads the body string with cancellation, then parses it synchronously as `T`. Invalid or incompatible XML can throw. |
| `DeserializeFromString<T>(string)` | Parses a buffered XML string as `T` with the configured reader. |
| `GetFieldNameForProperty(PropertyInfo)` | Returns an explicit element/attribute name; rejects null property metadata. |

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
