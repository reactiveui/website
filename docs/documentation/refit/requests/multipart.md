---
Order: 8
---
# Upload files with multipart requests

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/requests-multipart/requests-multipart.csproj).

An upload often includes a file and a few details about it, such as a title or description.
A multipart request sends them together as separate named parts. Refit lets you supply each
part as a method argument and choose the file names and content types the service expects.

The first example sends a file with some text. Later sections cover multiple files, streams
and custom parts, including which streams your app must keep open.

## Send a file and text together

The [complete local examples](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Multipart)
build and run on .NET 10 with C# 14. Their handler inspects the real generated requests without contacting a server.
`SampleCheck.Equal` throws when a value differs from the expected result.
The source files include imports, the handler and the runner.

**1. Put `[Multipart]` on the HTTP method.** It selects `multipart/form-data` content.
The boundary is the text that separates the parts in the HTTP body.
`[Multipart("sample-boundary")]` supplies a custom boundary.
The default is `----MyGreatBoundary`, available from `new MultipartAttribute().BoundaryText`.
The boundary must meet the HTTP content parser's rules; the attribute does not validate it.

**2. Choose a part wrapper.** `StreamPart` uses a stream you own.
`ByteArrayPart` uses bytes already in memory. `FileInfoPart` opens a local file when the content is created.
Each constructor takes `(value, fileName, contentType = null, name = null)`.
The file name is the name sent to the service; it need not be a local path.

**3. Make an asynchronous call.** `UploadAsync` below sends a file and title.
The `[Query]` parameter stays in the URL rather than becoming another body part.
The other methods show file collections, raw values, JSON model parts and a custom extension.


```csharp
internal interface IMultipartApi
{
    [Multipart("sample-boundary")]
    [Post("/upload")]
    Task<HttpResponseMessage> UploadAsync([AliasAs("file")] StreamPart file, string title, [Query] string mode);

    [Multipart]
    [Post("/files")]
    Task<HttpResponseMessage> UploadFilesAsync(ByteArrayPart bytes, FileInfoPart file, IEnumerable<ByteArrayPart> attachments);

    [Multipart]
    [Post("/raw")]
    Task<HttpResponseMessage> UploadRawAsync(HttpContent content, Stream raw, byte[] bytes, FileInfo file);

    [Multipart]
    [Post("/metadata")]
    Task<HttpResponseMessage> UploadMetadataAsync(UploadMetadata metadata, Guid token);

    [Multipart]
    [Post("/custom")]
    Task<HttpRequestMessage> BuildAsync([AliasAs("aliased")] MultipartItem item);
}
```

The runnable example creates one `HttpClient` with a local `MultipartHandler` and passes it to
`RestService.ForGenerated<IMultipartApi>(client, settings)`. The generator builds all five methods inline.
It needs no runtime reflection request builder.
The handler stores plain value snapshots because Refit disposes a sent request and its content.


```csharp
await using MemoryStream stream = new(StreamBytes);
StreamPart file = new(stream, "report.txt", TextMediaType, "chosen-field");
using HttpResponseMessage reply = await api.UploadAsync(file, ReportTitle, "preview");
SampleCheck.Equal(Boundary, handler.Boundary);
SampleCheck.Equal("?mode=preview", handler.Query);
SampleCheck.Equal("chosen-field", handler.Parts[0].Name);
SampleCheck.Equal("report.txt", handler.Parts[0].FileName);
SampleCheck.Equal(StreamText, handler.Parts[0].Body);
SampleCheck.Equal("title", handler.Parts[1].Name);
SampleCheck.Equal(ReportTitle, handler.Parts[1].Body);
SampleCheck.Equal(true, stream.CanRead);
```

The sample's constants provide `text/plain`, `stream text`, `byte text`, `bytes.txt`,
`Annual report`, `sample-boundary`, the greeting `hello` and the second attachment byte 2.
`file.Name` in later excerpts belongs to the temporary local file created by the complete runner.

## Field names, file names and content types

The form field name and transmitted file name are separate values.

| Input | Form field name | File name sent |
| --- | --- | --- |
| A part wrapper with `Name` set | Its `Name`, overriding `[AliasAs]` | Its nonempty `FileName` |
| A wrapper with `Name = null` | `[AliasAs]`, otherwise parameter name | Its nonempty `FileName` |
| A wrapper with empty `FileName` | The same field-name rules | The parameter's aliased or declared name |
| Raw `Stream` or `byte[]` | Aliased or declared parameter name | The same name |
| Raw `FileInfo` | Aliased or declared parameter name | `FileInfo.Name` |
| Raw `HttpContent` | Its existing content-disposition metadata | Its existing metadata |
| A string, formatted value or serialized model | Aliased or declared parameter name | None |

An empty wrapper `Name` does not trigger fallback; only null does. Use null when you want the parameter's name.
Empty names can be rejected while the multipart body is built.
For a raw `HttpContent`, Refit calls `Add(content)` and preserves its headers.
Set its content disposition yourself when the service expects a named field.

`MultipartItem.FileName`, `ContentType` and `Name` are read-only metadata.
`StreamPart.Value`, `ByteArrayPart.Value` and `FileInfoPart.Value` expose the original supplied object.
Byte arrays are not copied. Null values or a null file name throw `ArgumentNullException`.
Constructing `FileInfoPart` does not open the file; `ToContent()` does.

`ToContent()` creates HTTP content and applies a nonempty `ContentType` as its media type.
Use a media type such as `application/pdf`, without a `charset` parameter.
An invalid media type can throw `FormatException` during content creation.
Null or empty `ContentType` preserves the content's existing type.
The method does not assign content disposition; that happens when the content is added to a multipart body.


```csharp
await using MemoryStream stream = new(StreamBytes);
StreamPart streaming = new(stream, "source.txt");
using (HttpContent content = streaming.ToContent())
{
    SampleCheck.Equal(StreamText, await content.ReadAsStringAsync());
    SampleCheck.Equal(null, content.Headers.ContentDisposition);
}

SampleCheck.Equal(true, stream.CanRead);
SampleCheck.Equal(stream.Length, stream.Position);
SampleCheck.Equal(stream, streaming.Value);
SampleCheck.Equal("source.txt", streaming.FileName);
SampleCheck.Equal(null, streaming.Name);
SampleCheck.Equal(null, streaming.ContentType);
byte[] value = ByteBytes;
ByteArrayPart bytes = new(value, BytesFileName, TextMediaType, "attachment");
SampleCheck.Equal(value, bytes.Value);
SampleCheck.Equal("attachment", bytes.Name);
SampleCheck.Equal(TextMediaType, bytes.ContentType);
using HttpContent byteContent = bytes.ToContent();
SampleCheck.Equal(ByteText, await byteContent.ReadAsStringAsync());
SampleCheck.Equal(TextMediaType, byteContent.Headers.ContentType?.MediaType);
FileInfoPart disk = new(file, "public-name.txt");
SampleCheck.Equal(file, disk.Value);
using (HttpContent diskContent = disk.ToContent())
{
    SampleCheck.Equal("disk text", await diskContent.ReadAsStringAsync());
    SampleCheck.Equal(null, diskContent.Headers.ContentType);
}
```

## Stream ownership and repeated files

Disposing content made from `StreamPart` or a raw caller stream leaves that stream open.
Refit sends from its current position and does not rewind it.
You must position the stream before sending and dispose it when finished.
A retry must provide readable data again, for example by rewinding a seekable stream before the next call.

`FileInfoPart` and raw `FileInfo` open streams that Refit owns.
Disposing their content closes those streams. The direct-conversion test also reopens the file exclusively
after disposing its content. Every `ToContent()` call creates new content; avoid sharing one content instance across sends.

A collection of part wrappers produces one part per entry. Entries without a `Name` override share the parameter's field name.
A null collection or null single parameter contributes no part. Do not place null file-wrapper entries inside a collection:
the generated loop does not skip them.


```csharp
ByteArrayPart bytes = new(ByteBytes, BytesFileName, TextMediaType);
FileInfoPart disk = new(file, "download-name.txt", TextMediaType, "document");
using HttpResponseMessage reply = await api.UploadFilesAsync(bytes, disk, [new([1], "one.bin"), new([SecondByte], "two.bin")]);
SampleCheck.Equal(new MultipartAttribute().BoundaryText, handler.Boundary);
SampleCheck.Equal("bytes", handler.Parts[0].Name);
SampleCheck.Equal(BytesFileName, handler.Parts[0].FileName);
SampleCheck.Equal("document", handler.Parts[1].Name);
SampleCheck.Equal("download-name.txt", handler.Parts[1].FileName);
SampleCheck.Equal("attachments", handler.Parts[2].Name);
SampleCheck.Equal("attachments", handler.Parts[3].Name);
SampleCheck.Equal(Boundary, new MultipartAttribute(Boundary).BoundaryText);
```


```csharp
using StringContent content = new("custom body");
content.Headers.ContentDisposition = new("form-data") { Name = "custom-field" };
await using MemoryStream stream = new("raw stream"u8.ToArray());
using HttpResponseMessage reply = await api.UploadRawAsync(content, stream, "raw bytes"u8.ToArray(), file);
SampleCheck.Equal("custom-field", handler.Parts[0].Name);
SampleCheck.Equal("raw", handler.Parts[1].FileName);
SampleCheck.Equal("bytes", handler.Parts[2].FileName);
SampleCheck.Equal(file.Name, handler.Parts[3].FileName);
SampleCheck.Equal(true, stream.CanRead);
```

## Send a model as one JSON part

A concrete sealed model without `[FormObject]` is serialized as one part, named after its parameter.
Strings become UTF-8 `text/plain` parts. Guid and date/time values use the form formatter and plain text.
Numbers, booleans, enums and other serialized models use the content serializer.

For standalone use, repeat the generated JSON setup below.
`JsonSerializerContext` holds metadata describing how to read and write a model.
`[JsonSerializable]` registers a type, and `TypeInfoResolver` finds its metadata at runtime.
Register collection and closed generic model types separately when you add them.
Raw file bytes and text do not need JSON metadata.


```csharp
internal sealed record UploadMetadata(string Title);
```


```csharp
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(UploadMetadata))]
internal sealed partial class MultipartJsonContext : JsonSerializerContext;
```


```csharp
private static readonly JsonSerializerOptions Options = new(MultipartJsonContext.Default.Options) { TypeInfoResolver = MultipartJsonContext.Default };

private static readonly RefitSettings Settings = new(new SystemTextJsonContentSerializer(Options));
```


```csharp
using HttpResponseMessage reply = await api.UploadMetadataAsync(new(ReportTitle), Guid.Empty);
SampleCheck.Equal("metadata", handler.Parts[0].Name);
SampleCheck.Equal("application/json", handler.Parts[0].MediaType);
SampleCheck.Equal("{\"title\":\"Annual report\"}", handler.Parts[0].Body);
SampleCheck.Equal(Guid.Empty.ToString(), handler.Parts[1].Body);
SampleCheck.Equal(TextMediaType, handler.Parts[1].MediaType);
```

The main example uses generated request code and JSON metadata,
and its native executable runs the same local assertions.
This result covers the declared static parameter types. An `object`, interface or open generic parameter can need
the reflection request builder instead. Read [AOT setup](../aot.md) and [JSON configuration](../serialization/json.md).

## Extend `MultipartItem`

Derive from `MultipartItem` when the built-in wrappers do not supply the content you need.
Its protected constructors accept `(fileName, contentType)` or `(fileName, contentType, name)`.
Override protected `CreateContent()` to return fresh content. Call inherited public `ToContent()`
to create that content and apply the configured media type.
The two-argument constructor leaves `Name` null.


```csharp
internal sealed class TextPart : MultipartItem
{
    private readonly string _text;

    internal TextPart(string text, string fileName)
        : base(fileName, null) => _text = text;

    internal TextPart(string text, string fileName, string? contentType, string? name)
        : base(fileName, contentType, name) => _text = text;

    protected override HttpContent CreateContent() => new StringContent(_text);
}
```

The extension test runs both constructors. The second overrides the field name and media type,
while its empty file name falls back to the `aliased` parameter name.


```csharp
TextPart simple = new(Greeting, "hello.txt");
using HttpContent standalone = simple.ToContent();
SampleCheck.Equal(TextMediaType, standalone.Headers.ContentType?.MediaType);
TextPart named = new(Greeting, string.Empty, "application/x-sample", "chosen");
using HttpRequestMessage request = await api.BuildAsync(named);
using IEnumerator<HttpContent> parts = ((MultipartFormDataContent)request.Content!).GetEnumerator();
SampleCheck.Equal(true, parts.MoveNext());
HttpContent part = parts.Current;
SampleCheck.Equal(false, parts.MoveNext());
SampleCheck.Equal("chosen", part.Headers.ContentDisposition?.Name?.Trim('"'));
SampleCheck.Equal("aliased", part.Headers.ContentDisposition?.FileName?.Trim('"'));
SampleCheck.Equal("application/x-sample", part.Headers.ContentType?.MediaType);
SampleCheck.Equal(Greeting, await part.ReadAsStringAsync());
```

## Flatten a form object: reflection-only path

`[FormObject]` writes a complex object's public properties as separate text parts.
It does not turn file-valued properties into file attachments; pass files as separate parameters.
Aliases take precedence, then serializer field names, then the URL key formatter.
Values use `FormUrlEncodedParameterFormatter`. Collection formats, nested `parent.child` names,
depth limits and reference-cycle guards follow form-body flattening.
Null fields are omitted unless their query configuration requests null serialization.
An emitted null value becomes empty text; unnamed or whitespace-only fields are skipped.

The current generator deliberately sends `[FormObject]` methods to the reflection request builder.
The [separate JIT-only example](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Multipart/Legacy)
references `Refit.Reflection` and calls `RestService.For` explicitly.
It is excluded from the main project and native publication.
That project's `RefitGeneratedRequestBuilding=false` selects reflection request construction for every method deliberately.
Diagnostic severities remain unchanged. The compatibility probe below checks the `RF006` warning that the same method
produces with the default generated request mode.
Adding a generated JSON context does not make reflective property flattening AOT-compatible.
The flattened model below is not JSON-serialized; the generated serializer settings cover the separate JSON model example.


```csharp
internal sealed class FormFields
{
    [AliasAs("caption")]
    public string Title { get; init; } = "Annual report";

    [Query(CollectionFormat.Multi)]
    public string[] Tags { get; init; } = ["math", "code"];

    [Query(SerializeNull = true)]
    public string? Note { get; init; }
}
```


```csharp
internal interface IFormUploadApi
{
    [Multipart]
    [Post("/form")]
    Task<HttpResponseMessage> UploadAsync([FormObject] FormFields fields, ByteArrayPart recipe);
}
```


```csharp
using MultipartHandler handler = new();
using HttpClient client = CreateClient(handler);
RefitSettings settings = new(new SystemTextJsonContentSerializer(MultipartJsonContext.Default.Options));
IFormUploadApi api = RestService.For<IFormUploadApi>(client, settings);
using HttpResponseMessage reply = await api.UploadAsync(new(), new("recipe"u8.ToArray(), "recipe.txt"));
SampleCheck.Equal("caption", handler.Parts[0].Name);
SampleCheck.Equal("Annual report", handler.Parts[0].Body);
SampleCheck.Equal("Tags", handler.Parts[1].Name);
SampleCheck.Equal("math", handler.Parts[1].Body);
SampleCheck.Equal("Tags", handler.Parts[2].Name);
SampleCheck.Equal("code", handler.Parts[2].Body);
SampleCheck.Equal("Note", handler.Parts[3].Name);
SampleCheck.Equal(string.Empty, handler.Parts[3].Body);
SampleCheck.Equal("recipe", handler.Parts[4].Name);
SampleCheck.Equal("recipe.txt", handler.Parts[4].FileName);
SampleCheck.Equal(true, new FormObjectAttribute() is Attribute);
```

The [compiling compatibility harness](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Tooling/CompatibilitySample.cs)
uses Roslyn, the C# compiler APIs, to analyze source input and assert its expected diagnostic.
The harness itself builds without warnings and verifies the default-mode limitation:


```csharp
const string source = """
    internal sealed class FormFields
    {
        public string Name { get; init; } = "Ada";
    }
    internal interface IFormUpload
    {
        [Refit.Multipart]
        [Refit.Post("/form")]
        System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> UploadAsync([Refit.FormObject] FormFields fields);
    }
    """;
CSharpCompilation compilation = ToolingCompilation.Create(source);
ImmutableArray<Diagnostic> diagnostics = await AnalyzerSample.DiagnoseAsync(compilation);
bool requiresReflection = AnalyzerSample.Contains(diagnostics, "RF006");
```

## Obsolete attachment naming

`AttachmentNameAttribute(name)` stores its supplied string in read-only `Name`.
On a supported parameter, the legacy builder uses it as the file-name override; it does not replace the field's parameter name.
Wrapper metadata still supplies a nonempty wrapper file name and its explicit `Name`.
Although the attribute can target properties, multipart attachment routing reads parameter attributes.

The type is obsolete and produces compiler warning `CS0618` when used directly.
The compatibility probe compiles that old usage as input and asserts the warning; it does not claim the obsolete API is warning-free.
Use `StreamPart`, `ByteArrayPart`, `FileInfoPart` or a `MultipartItem` extension to choose names in new code.
The wrapper examples above verify those replacements.


```csharp
const string source = """
    internal interface ILegacyUpload
    {
        [Refit.Multipart]
        [Refit.Post("/form")]
        System.Threading.Tasks.Task UploadAsync([Refit.AttachmentName("sent.bin")] byte[] attachment);
    }
    """;
CSharpCompilation compilation = ToolingCompilation.Create(source);
bool warned = AnalyzerSample.Contains(compilation.GetDiagnostics(), ObsoleteWarning);
```

`ObsoleteWarning` is the compiler probe's constant for `CS0618`.
