---
Order: 4
---
# Content writers and stream readers

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/serialization-content/serialization-content.csproj).

A service may exchange data a little at a time, such as one JSON record on each line.
Refit's content writers and stream readers help you work with those formats when you need
more control over the HTTP body.

This page shows how to write JSON Lines and read streams of values. It also explains the
interfaces you can implement for a custom serializer. For ordinary interface methods,
start with [request bodies](../requests/bodies.md).

The [complete content example](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Content)
contains the declarations and setup used by the excerpts on this page.

## Write JSON Lines

Pass an `IEnumerable` and an `IHttpContentSerializer`. Each element becomes one serialized value.
The writer adds one LF byte between elements, with no final newline. An empty sequence writes no bytes.
The media type is `JsonLinesContent.JsonLinesMediaType`, which is `application/x-ndjson`.


```csharp
using JsonLinesContent content = new(new[] { person, person }, serializer);
string body = await content.ReadAsStringAsync();
Console.WriteLine(JsonLinesContent.JsonLinesMediaType);
Console.WriteLine(body);
```

Use compact JSON. The content copies each serializer's bytes unchanged, so an indented serializer
can put extra newlines inside a value. It does not strip a serializer's BOM or normalize its output.
The source-generated serializer used here produces compact UTF-8 without a BOM.
Each temporary element content is disposed after it is copied. The supplied sequence and serializer
are retained by reference; this class does not dispose the sequence or close the destination stream.

The constructor rejects a null sequence or serializer. `SerializeToStreamAsync(Stream, TransportContext?)`
is a protected override invoked by `HttpContent`; the context is unused. `TryComputeLength(out long)`
returns `false` and sets the length to `-1`, so the writer cannot advertise a known size in advance.
The inherited HTTP APIs may buffer the content when explicitly asked to do so.

## Read a stream directly

`IStreamingContentSerializer` is an optional capability, separate from `IHttpContentSerializer`.
`SystemTextJsonContentSerializer` supports both. Call
`DeserializeStreamAsync<T>(Stream, StreamingContentFormat, CancellationToken = default)`
and consume its result with `await foreach`. `T` is the element type, and elements can be null.


```csharp
await using MemoryStream stream = new(Encoding.UTF8.GetBytes(body));
await foreach (Person? item in streaming.DeserializeStreamAsync<Person>(stream, format, CancellationToken.None))
{
    Console.WriteLine(item!.Name);
}
```

| Format | Body framing |
| --- | --- |
| `JsonArray = 0` | One top-level JSON array; each array element is yielded. |
| `JsonLines = 1` | JSON values separated by whitespace on .NET 9 and later. |
| `ServerSentEvents = 2` | An SSE stream; each event's `data` payload is deserialized as JSON. |

On .NET 9 and later, the JSON Lines reader uses System.Text.Json's top-level-values reader.
Two values separated by a space work even without a line break. The .NET 10 example also checks
a leading UTF-8 BOM for array, JSON Lines and SSE inputs. On .NET 8 and .NET Framework,
a manual reader splits at LF, ignores blank lines, trims trailing CR, and accepts a final line without LF.
That older path needs a complete value on each nonblank line and does not explicitly strip a BOM.
SSE uses the SSE parser and exposes the deserialized payload, without its event name or ID.
The System.Text.Json implementation treats an unrecognized enum value as `JsonArray`.

The direct call leaves the supplied stream open after enumeration; the caller owns and disposes it.
Dispose the enumerator when stopping early, and pass a cancellation token for cancellable reads.
Refit's [streaming reply](../results/streaming.md) path also owns an HTTP response, so its cleanup
contract includes that response and its body stream.
Malformed JSON and missing generated metadata fail during enumeration.

## Infer values stored as object

`ObjectToInferredTypesConverter` is a `JsonConverter<object>` with a public default constructor,
`Read(ref Utf8JsonReader, Type, JsonSerializerOptions)` and
`Write(Utf8JsonWriter, object, JsonSerializerOptions)`.
Its read method selects a CLR type from the current token; the supplied `Type` does not change that choice.


```csharp
ObjectToInferredTypesConverter converter = new();
Utf8JsonReader reader = new("1"u8);
_ = reader.Read();
object? inferred = converter.Read(ref reader, typeof(object), Options);
Console.WriteLine(inferred!.GetType().Name); // Int64
```

| JSON token | Result |
| --- | --- |
| `true` or `false` | `bool` |
| Number representable as Int64 | `long` |
| Other number | `double` |
| String parseable as DateTime | `DateTime` |
| Other string | `string` |
| Object, array or a directly read null token | A detached `JsonElement` |

When registered as a converter, System.Text.Json normally handles null reference values itself.
The direct null-token result above describes calling `Read` explicitly.
Numbers are inferred into `long` or `double`; this is not a decimal-preserving conversion.
Objects and arrays remain JSON elements rather than becoming dictionaries or lists.

`Write` serializes the value's runtime type. A bare `new object()` becomes `{}` to prevent recursive
converter calls. Supply metadata for every runtime type that your app may write; knowing only
`object` does not describe the properties of a model. The standalone example checks the scalar,
object and array read branches, a model write and the bare-object write.


```csharp
[JsonSerializable(typeof(object))]
[JsonSerializable(typeof(bool))]
[JsonSerializable(typeof(long))]
[JsonSerializable(typeof(double))]
[JsonSerializable(typeof(DateTime))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(JsonElement))]
[JsonSerializable(typeof(Person))]
internal sealed partial class ContentJsonContext : JsonSerializerContext;
```


```csharp
using MemoryStream buffer = new();
using (Utf8JsonWriter writer = new(buffer))
{
    converter.Write(writer, person, Options);
}

Console.WriteLine(Encoding.UTF8.GetString(buffer.ToArray()));
```

On .NET 8 and later, the write path uses `options.GetTypeInfo(runtimeType)`. With generated metadata
this avoids runtime serializer discovery. Older framework targets call the runtime-Type serializer
and use reflection. The default Refit System.Text.Json serializer already includes this converter.

## Replace the obsolete serializer

`JsonContentSerializer` is retained in the public API for compatibility, with a public default constructor.
It is marked `Obsolete(..., error: true)`: using it causes a compiler error.
Its `ToHttpContent<T>(T)`, `FromHttpContentAsync<T>(HttpContent, CancellationToken = default)` and
`GetFieldNameForProperty(PropertyInfo)` all throw `NotSupportedException`, including when called
through another route. It is not a working alias for another serializer.

Use [SystemTextJsonContentSerializer](json.md) for generated JSON metadata, or
[NewtonsoftJsonContentSerializer](newtonsoft-json.md) from `Refit.Newtonsoft.Json` when migrating
code that depends on Newtonsoft.Json behavior. The complete content example excludes the obsolete
class so that its build remains free of warnings and errors. A separate compiler probe verifies
that direct use is rejected with `CS0619`; it does not execute the obsolete serializer.


```csharp
const string source = """
    internal static class LegacySerializer
    {
        internal static object Create() => new Refit.JsonContentSerializer();
    }
    """;
CSharpCompilation compilation = ToolingCompilation.Create(source);
bool rejected = AnalyzerSample.Contains(compilation.GetDiagnostics(), "CS0619");
```

The implementation is in [JsonLinesContent.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/JsonLinesContent.cs),
[SystemTextJsonContentSerializer.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs),
[SystemTextJsonStreamingDeserializer.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonStreamingDeserializer.cs),
[ObjectToInferredTypesConverter.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/ObjectToInferredTypesConverter.cs)
and [JsonContentSerializer.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/JsonContentSerializer.cs).
