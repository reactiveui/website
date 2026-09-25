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

**1. Put `[Multipart]` on the HTTP method.** It selects `multipart/form-data` content.
The boundary is the text that separates the parts in the HTTP body.
The default is `----MyGreatBoundary`. `[Multipart("report-boundary")]` supplies your own.
The boundary must meet the HTTP content parser's rules; the attribute does not validate it.

**2. Choose a part wrapper.** `StreamPart` uses a stream you own.
`ByteArrayPart` uses bytes already in memory. `FileInfoPart` opens a local file when the content is created.
Each constructor takes `(value, fileName, contentType = null, name = null)`.
The file name is the name sent to the service; it need not be a local path.

**3. Declare the method.** `UploadAsync` sends a file and a title.
The `[Query]` parameter stays in the URL rather than becoming another body part.

```csharp
internal interface IMultipartApi
{
    [Multipart]
    [Post("/upload")]
    Task<HttpResponseMessage> UploadAsync([AliasAs("file")] StreamPart file, string title, [Query] string mode);
}
```

**4. Make the call.** Create the client with your `httpClient` and `settings`, then pass the parts.

```csharp
IMultipartApi api = RestService.ForGenerated<IMultipartApi>(httpClient, settings);

await using MemoryStream stream = new("Quarterly totals"u8.ToArray());
StreamPart file = new(stream, "report.txt", "text/plain");
using HttpResponseMessage reply = await api.UploadAsync(file, "Annual report", "preview");
// POST /upload?mode=preview
// part "file": file name "report.txt", text/plain, "Quarterly totals"
// part "title": "Annual report"
```

The generator builds multipart methods inline, so they need no runtime reflection.
Refit leaves `stream` open. The `await using` declaration disposes it.

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
Null or empty `ContentType` keeps the content's own type. `StreamPart`, `ByteArrayPart` and `FileInfoPart`
content has no `Content-Type` header of its own.
The method does not assign content disposition; that happens when the content is added to a multipart body.

```csharp
ByteArrayPart notes = new("Meeting notes"u8.ToArray(), "notes.txt", "text/plain", "attachment");
using HttpContent content = notes.ToContent();
string body = await content.ReadAsStringAsync(cancellationToken); // "Meeting notes"
// content.Headers.ContentType?.MediaType == "text/plain"
// content.Headers.ContentDisposition == null
```

## Stream ownership and repeated files

Disposing content made from `StreamPart` or a raw caller stream leaves that stream open.
Refit sends from its current position and does not rewind it.
You must position the stream before sending and dispose it when finished.
A retry must provide readable data again, for example by rewinding a seekable stream before the next call.

`FileInfoPart` and raw `FileInfo` open streams that Refit owns.
Disposing their content closes those streams, so the file is free again once the call ends.
Every `ToContent()` call creates new content; avoid sharing one content instance across sends.

A collection of part wrappers produces one part per entry. Entries without a `Name` override share the parameter's field name.
A null collection or null single parameter contributes no part. Do not place null file-wrapper entries inside a collection:
the generated loop does not skip them.

```csharp
[Multipart]
[Post("/files")]
Task<HttpResponseMessage> UploadFilesAsync(ByteArrayPart bytes, FileInfoPart file, IEnumerable<ByteArrayPart> attachments);
```

```csharp
FileInfo reportFile = new("report.txt");
ByteArrayPart summary = new("Quarterly totals"u8.ToArray(), "summary.txt", "text/plain");
FileInfoPart report = new(reportFile, "q3-report.txt", "text/plain", "document");
ByteArrayPart[] attachments = [new("chart"u8.ToArray(), "chart.png"), new("table"u8.ToArray(), "table.csv")];
using HttpResponseMessage reply = await api.UploadFilesAsync(summary, report, attachments);
// part "bytes": file name "summary.txt"
// part "document": file name "q3-report.txt" (Name overrides the parameter name "file")
// part "attachments": file name "chart.png"
// part "attachments": file name "table.csv"
```

You can also pass raw values without a wrapper. Each one takes its names from the table above.

```csharp
[Multipart]
[Post("/raw")]
Task<HttpResponseMessage> UploadRawAsync(HttpContent content, Stream raw, byte[] bytes, FileInfo file);
```

```csharp
using StringContent note = new("Reviewed by finance");
note.Headers.ContentDisposition = new("form-data") { Name = "note" };
await using MemoryStream raw = new("Quarterly totals"u8.ToArray());
using HttpResponseMessage reply = await api.UploadRawAsync(note, raw, "Chart data"u8.ToArray(), reportFile);
// part "note": no file name, the content exactly as you built it
// part "raw": file name "raw"
// part "bytes": file name "bytes"
// part "file": file name "report.txt" (reportFile.Name)
```

## Send a model as one JSON part

A concrete sealed model without `[FormObject]` is serialized as one part, named after its parameter.
Strings become UTF-8 `text/plain` parts. Guid and date/time values use the form formatter and plain text.
Numbers, booleans, enums and other serialized models use the content serializer.

For standalone use, repeat the generated JSON setup below.
`JsonSerializerContext` holds metadata describing how to read and write a model.
`[JsonSerializable]` registers a type. `RefitSettings.ForJsonContext` builds settings that read the metadata.
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
RefitSettings settings = RefitSettings.ForJsonContext(MultipartJsonContext.Default);
```

```csharp
[Multipart]
[Post("/metadata")]
Task<HttpResponseMessage> UploadMetadataAsync(UploadMetadata metadata, Guid token);
```

```csharp
Guid token = Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301");
using HttpResponseMessage reply = await api.UploadMetadataAsync(new("Annual report"), token);
// part "metadata": application/json, {"title":"Annual report"}
// part "token": text/plain, 3f2504e0-4f89-11d3-9a0c-0305e82c3301
```

This works with generated request code and runs in a Native AOT app.
It covers the declared static parameter types. An `object`, interface or open generic parameter can need
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

Declare the parameter as `MultipartItem`, or as your own type.

```csharp
[Multipart]
[Post("/notes")]
Task<HttpResponseMessage> UploadNoteAsync([AliasAs("attachment")] MultipartItem note);
```

The first part below keeps the `text/plain` type that `StringContent` sets. The second overrides the field name
and media type. Its empty file name falls back to the parameter's alias.

```csharp
TextPart plain = new("Reviewed by finance", "notes.txt");
using HttpContent content = plain.ToContent(); // content.Headers.ContentType?.MediaType == "text/plain"

TextPart named = new("Reviewed by finance", string.Empty, "text/markdown", "note");
using HttpResponseMessage reply = await api.UploadNoteAsync(named);
// part "note": file name "attachment", text/markdown, "Reviewed by finance"
```

## Flatten a form object: reflection-only path

`[FormObject]` writes a complex object's public properties as separate text parts.
It does not turn file-valued properties into file attachments; pass files as separate parameters.
Aliases take precedence, then serializer field names, then the URL key formatter.
Values use `FormUrlEncodedParameterFormatter`. Collection formats, nested `parent.child` names,
depth limits and reference-cycle guards follow form-body flattening.
Null fields are omitted unless their query configuration requests null serialization.
An emitted null value becomes empty text; unnamed or whitespace-only fields are skipped.

The generator does not build `[FormObject]` methods. With the default generated request mode,
such a method produces analyzer warning `RF006`, and Refit sends it to the reflection request builder.
That builder is in the `Refit.Reflection` package. Create the client with `RestService.For`.
Reflection flattening reads properties at runtime, so it is not trim or Native AOT safe.
A generated JSON context does not change that.
The separate [JIT-only project](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Multipart/Legacy/Legacy.csproj)
runs this example. It sets `RefitGeneratedRequestBuilding=false`, which selects reflection request construction for every method.

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
IFormUploadApi api = RestService.For<IFormUploadApi>(httpClient, settings);
using HttpResponseMessage reply = await api.UploadAsync(new FormFields(), new("Mix flour and water"u8.ToArray(), "recipe.txt"));
// part "caption": "Annual report"
// part "Tags": "math"
// part "Tags": "code"
// part "Note": "" (SerializeNull sends the null as empty text)
// part "recipe": file name "recipe.txt"
```

## Obsolete attachment naming

`AttachmentNameAttribute(name)` stores its supplied string in read-only `Name`.
On a supported parameter, the legacy builder uses it as the file-name override; it does not replace the field's parameter name.
Wrapper metadata still supplies a nonempty wrapper file name and its explicit `Name`.
Although the attribute can target properties, multipart attachment routing reads parameter attributes.

The type is obsolete. Using it produces compiler warning `CS0618`:

```csharp
[Multipart]
[Post("/form")]
Task UploadAsync([AttachmentName("sent.bin")] byte[] attachment); // warning CS0618
```

Use `StreamPart`, `ByteArrayPart`, `FileInfoPart` or a `MultipartItem` extension to choose names in new code.

## API reference

The table covers the public types, constructors, methods and properties used by multipart requests. The `CreateContent` methods are protected implementation points for derived types. The constructors for `MultipartItem` are also protected, so derive from it when you need a custom part.

| API | Description | Parameters or value | Returns and behavior |
| --- | --- | --- | --- |
| [`AttachmentNameAttribute(string name)`](https://github.com/reactiveui/refit/blob/main/src/Refit/AttachmentNameAttribute.cs) *(obsolete)* | Stores the legacy attachment file-name override. Use a part wrapper for new code. | `name`: [`string`](https://learn.microsoft.com/dotnet/api/system.string) to expose through `Name` | Creates the obsolete attribute; using it produces compiler warning `CS0618`. |
| [`AttachmentNameAttribute.Name`](https://github.com/reactiveui/refit/blob/main/src/Refit/AttachmentNameAttribute.cs) *(obsolete)* | Gets the legacy file-name override. | Read-only [`string`](https://learn.microsoft.com/dotnet/api/system.string) | Returns the constructor's `name`. |
| [`ByteArrayPart(byte[] value, string fileName, string? contentType = null, string? name = null)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ByteArrayPart.cs) | Creates a multipart item backed by a byte array. | `value`: [`byte[]`](https://learn.microsoft.com/dotnet/api/system.byte); `fileName`: [`string`](https://learn.microsoft.com/dotnet/api/system.string); `contentType`: optional media type, default `null`; `name`: optional form field name, default `null` | Stores the same byte array reference. Throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception) when `value` is `null`. |
| [`ByteArrayPart.Value`](https://github.com/reactiveui/refit/blob/main/src/Refit/ByteArrayPart.cs) | Gets the bytes supplied to the constructor. | Read-only [`byte[]`](https://learn.microsoft.com/dotnet/api/system.byte) | Returns the original array. |
| [`ByteArrayPart.CreateContent()`](https://github.com/reactiveui/refit/blob/main/src/Refit/ByteArrayPart.cs) *(protected override)* | Builds content for the byte-array part. | None | Returns [`ByteArrayContent`](https://learn.microsoft.com/dotnet/api/system.net.http.bytearraycontent) over `Value`. |
| [`FileInfoPart(FileInfo value, string fileName, string? contentType = null, string? name = null)`](https://github.com/reactiveui/refit/blob/main/src/Refit/FileInfoPart.cs) | Creates a multipart item backed by a local file. | `value`: [`FileInfo`](https://learn.microsoft.com/dotnet/api/system.io.fileinfo); `fileName`: [`string`](https://learn.microsoft.com/dotnet/api/system.string); `contentType`: optional media type, default `null`; `name`: optional form field name, default `null` | Stores the file information. Throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception) when `value` is `null`; it opens the file only when content is created. |
| [`FileInfoPart.Value`](https://github.com/reactiveui/refit/blob/main/src/Refit/FileInfoPart.cs) | Gets the source file information. | Read-only [`FileInfo`](https://learn.microsoft.com/dotnet/api/system.io.fileinfo) | Returns the original `FileInfo`. |
| [`FileInfoPart.CreateContent()`](https://github.com/reactiveui/refit/blob/main/src/Refit/FileInfoPart.cs) *(protected override)* | Opens the source file and builds content for the part. | None | Returns [`StreamContent`](https://learn.microsoft.com/dotnet/api/system.net.http.streamcontent) over a newly opened read stream. |
| [`FormObjectAttribute()`](https://github.com/reactiveui/refit/blob/main/src/Refit/FormObjectAttribute.cs) | Marks a complex multipart parameter for property flattening. | None | Causes each public property to become a text part on the reflection request-builder path. |
| [`MultipartAttribute(string boundaryText = "----MyGreatBoundary")`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartAttribute.cs) | Marks an HTTP method as multipart and chooses its boundary. | `boundaryText`: [`string`](https://learn.microsoft.com/dotnet/api/system.string), default `"----MyGreatBoundary"` | Stores the boundary used to separate parts. |
| [`MultipartAttribute.BoundaryText`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartAttribute.cs) | Gets the boundary configured for the method. | Read-only [`string`](https://learn.microsoft.com/dotnet/api/system.string) | Returns the supplied boundary text. |
| [`MultipartItem(string fileName, string? contentType)`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) *(protected)* | Initializes a custom multipart item without an explicit form field name. | `fileName`: [`string`](https://learn.microsoft.com/dotnet/api/system.string); `contentType`: optional media type | Stores the file name and content type, with `Name` set to `null`. Throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception) for a null file name. |
| [`MultipartItem(string fileName, string? contentType, string? name)`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) *(protected)* | Initializes a custom multipart item with optional form field metadata. | `fileName`: [`string`](https://learn.microsoft.com/dotnet/api/system.string); `contentType`: optional media type; `name`: optional form field name | Stores all three values. A null file name throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception). |
| [`MultipartItem.Name`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) | Gets the explicit form field name for the item. | Read-only [`string?`](https://learn.microsoft.com/dotnet/api/system.string) | Returns `null` when the constructor did not receive a name. |
| [`MultipartItem.ContentType`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) | Gets the optional media type for the item content. | Read-only [`string?`](https://learn.microsoft.com/dotnet/api/system.string) | Returns the configured content type, or `null`. |
| [`MultipartItem.FileName`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) | Gets the file name sent in the multipart disposition. | Read-only [`string`](https://learn.microsoft.com/dotnet/api/system.string) | Returns the required file name. |
| [`MultipartItem.ToContent()`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) | Creates this item's content and applies its nonempty `ContentType`. | None | Returns [`HttpContent`](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent). The caller disposes the returned content. |
| [`MultipartItem.CreateContent()`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) *(protected abstract)* | Defines how a derived item creates fresh underlying content. | None | Returns [`HttpContent`](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent); `ToContent()` applies the configured media type afterward. |
| [`StreamPart(Stream value, string fileName, string? contentType = null, string? name = null)`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamPart.cs) | Creates a multipart item backed by a caller-owned stream. | `value`: [`Stream`](https://learn.microsoft.com/dotnet/api/system.io.stream); `fileName`: [`string`](https://learn.microsoft.com/dotnet/api/system.string); `contentType`: optional media type, default `null`; `name`: optional form field name, default `null` | Stores the stream without copying it. Throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception) when `value` is `null`; disposing its content leaves the caller's stream open. |
| [`StreamPart.Value`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamPart.cs) | Gets the caller-owned stream. | Read-only [`Stream`](https://learn.microsoft.com/dotnet/api/system.io.stream) | Returns the original stream. |
| [`StreamPart.CreateContent()`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamPart.cs) *(protected override)* | Wraps the stream without taking ownership of it. | None | Returns [`HttpContent`](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent) that reads from `Value`. |
| [`AttachmentNameAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/AttachmentNameAttribute.cs) *(obsolete)* | Legacy attribute for naming an attachment. | None | Attribute type; prefer the part wrapper types. |
| [`ByteArrayPart`](https://github.com/reactiveui/refit/blob/main/src/Refit/ByteArrayPart.cs) | Represents byte-array content with multipart metadata. | None | Multipart item type derived from `MultipartItem`. |
| [`FileInfoPart`](https://github.com/reactiveui/refit/blob/main/src/Refit/FileInfoPart.cs) | Represents file content with multipart metadata. | None | Multipart item type derived from `MultipartItem`. |
| [`FormObjectAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/FormObjectAttribute.cs) | Marks a complex parameter for multipart property flattening. | None | Parameter attribute type. |
| [`MultipartAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartAttribute.cs) | Marks a method whose body contains named multipart parts. | None | Method attribute type. |
| [`MultipartItem`](https://github.com/reactiveui/refit/blob/main/src/Refit/MultipartItem.cs) | Base class for parts that carry a file name and optional content metadata. | None | Abstract type for custom multipart items. |
| [`StreamPart`](https://github.com/reactiveui/refit/blob/main/src/Refit/StreamPart.cs) | Represents caller-owned stream content with multipart metadata. | None | Multipart item type derived from `MultipartItem`. |
