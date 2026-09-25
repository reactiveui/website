---
Order: 7
---
# Request bodies

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/requests-bodies/requests-bodies.csproj).

Creating a person, saving a form or uploading data usually means sending more than a few
values in the URL. That data goes in the request body. Refit can turn your C# value into
the JSON, text or form data the service expects.

Start with a JSON body below. Later sections cover streams, JSON Lines, buffering and compression
when a service or a larger upload needs them.

## Send data in the body

**1. Register the JSON types.** Use generated metadata so the same client can work with AOT.
Metadata describes the JSON names and readers for a type. Refit's generated client and the JSON
generator do separate jobs: one builds HTTP requests, and the other writes and reads JSON.

```csharp
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(Person[]))]
[JsonSerializable(typeof(List<Person>))]
internal sealed partial class SampleJsonContext : JsonSerializerContext;
```

Hand the context to `RestService.ForGenerated` in step 4. To build the serializer and settings yourself instead,
see the [JSON guide](../serialization/json.md#build-the-options-yourself).

**2. Describe the body format.** Put `Body` on the one parameter that supplies the body.
The form input appears below.

```csharp
internal interface IBodyApi
{
    [Post("/body/json")]
    Task<Person> JsonAsync([Body] Person person);

    [Post("/body/text")]
    Task<Person> TextAsync([Body] string text);

    [Post("/body/quoted")]
    Task<Person> QuotedAsync([Body(BodySerializationMethod.Serialized)] string text);

    [Post("/body/stream")]
    Task<Person> StreamAsync([Body] Stream stream);

    [Post("/body/content")]
    Task<Person> ContentAsync([Body] HttpContent content);

    [Post("/body/form")]
    Task<Person> FormAsync([Body(BodySerializationMethod.UrlEncoded)] ContactForm form);

    [Post("/body/lines")]
    Task<Person> LinesAsync([Body(BodySerializationMethod.JsonLines)] IEnumerable<Person> people);

    [Post("/body/gzip")]
    Task<Person> GzipAsync([Body(
        BodySerializationMethod.Serialized,
        true,
        Compression = RequestCompression.GZip,
        CompressionLevel = CompressionLevel.Fastest)] Person person);
}
```

**3. Describe form fields.** `AliasAs` changes a field name. `Multi` repeats the key for each item.
`SerializeNull` sends an empty value instead of omitting a null property.

```csharp
internal sealed class ContactForm
{
    [AliasAs("name")]
    public string FullName { get; init; } = "Ada Lovelace";

    [Query(CollectionFormat.Multi)]
    public string[] Tags { get; init; } = ["math", "code"];

    [Query(SerializeNull = true)]
    public string? Note { get; init; }
}
```

**4. Send the requests.** `httpClient` is the shared HTTP client from the
[first-request example](../index.md). The client takes the generated JSON context above.

```csharp
IBodyApi api = RestService.ForGenerated<IBodyApi>(httpClient, SampleJsonContext.Default);
Person saved = await api.JsonAsync(new(1, "Ada"));
await api.TextAsync("hello");
await api.QuotedAsync("quoted");

await using MemoryStream stream = new("stream text"u8.ToArray());
await api.StreamAsync(stream);
Console.WriteLine(stream.CanRead); // True: the caller still owns the stream.

using StringContent content = new("content text");
await api.ContentAsync(content);
await api.FormAsync(new());
await api.LinesAsync([new(1, "Ada"), new(2, "Grace")]);
await api.GzipAsync(new(1, "Ada"));
Console.WriteLine(saved.Name); // Ada
```

The source example checks the actual bytes received by each local route.
It also decompresses the gzip body and checks the restored JSON.
JSON Lines puts a newline between the two items. Refit does not add a trailing newline.
See: the complete [body example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Bodies/Bodies.cs).

To pass settings that you built yourself, hand them to the same call. `JsonOptions` holds options
that use the context as their `TypeInfoResolver`, built as in
[pass settings instead of a context](../index.md#pass-settings-instead-of-a-context).
The examples below build their settings from the same options.

```csharp
RefitSettings settings = new(new SystemTextJsonContentSerializer(JsonOptions));
IBodyApi withSettings = RestService.ForGenerated<IBodyApi>(httpClient, settings);
Person savedWithSettings = await withSettings.JsonAsync(new(1, "Ada"));
```

## Write the body with explicit metadata

A method can take a `JsonTypeInfo<T>` parameter for the body. `T` is the type of the `[Body]` parameter.
The parameter holds the metadata for that type, such as a property of your JSON context.
Refit writes the body with it and does not send the parameter. A second parameter can supply the metadata
for the reply, as in this method:

```csharp
[Post("/orders")]
Task<Order> PlaceOrderAsync([Body] NewOrder order, JsonTypeInfo<NewOrder> newOrderInfo, JsonTypeInfo<Order> orderInfo, CancellationToken cancellationToken);
```

```csharp
NewOrder newOrder = new("Ada", [new("KB-1", 1, 49.5m)]);
Order placed = await api.PlaceOrderAsync(newOrder, OrdersJsonContext.Default.NewOrder, OrdersJsonContext.Default.Order, cancellationToken);
```

The metadata's own options apply to that call. The parameter works with the buffered and streamed request-body modes below.
It needs a JSON body: a form, JSON Lines or multipart body cannot take one, and the build fails with `RF014`.
See [pass metadata to a method](../serialization/json.md#pass-metadata-to-a-method) for the rules.

## Choose a serialization method

| `BodySerializationMethod` | Behavior |
| --- | --- |
| `Default = 0` | Passes [`HttpContent`](https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent) and streams through. Sends a string as plain text. Uses the configured serializer for other values. |
| `Serialized = 3` | Uses the configured serializer, including for strings. A JSON string includes quotes. |
| `UrlEncoded = 2` | Sends form key/value pairs. A dictionary or a generated property map supplies the fields. |
| `JsonLines = 4` | Sends an enumerable as one serialized value per line. Register the element types with the JSON context. |
| `Json = 1` | An obsolete name retained for compatibility. Use `Serialized` in new code. |

Supplied `HttpContent` and streams also bypass serialization in the form and JSON Lines helpers.
A form string is escaped as one whole string, so `name=Ada` is sent as `name%3DAda`.
Use a dictionary or model to send separate form fields. A single non-enumerable JSON Lines value
is wrapped as one item.
Form property names can come from `AliasAs` or the configured serializer's naming rules.
The key formatter applies when no explicit name exists.
See [query formatting](query-formatters.md) for the related naming and value format APIs.

## Buffering and serialization modes

`BodyAttribute` has four constructors: no arguments, `buffered`, `serializationMethod`, or both.
`SerializationMethod` defaults to `Default`. `Buffered` is null unless you supplied a bool.
Null follows `RefitSettings.Buffered`, whose default is false.
True makes Refit load the content into a buffer before sending. False skips that extra step.

```csharp
BodyAttribute inherited = new();
BodyAttribute buffered = new(true);
BodyAttribute serialized = new(BodySerializationMethod.Serialized);
BodyAttribute explicitPolicy = new(BodySerializationMethod.Serialized, false) { Compression = RequestCompression.Brotli, CompressionLevel = CompressionLevel.Fastest };
Console.WriteLine(inherited.Buffered is null); // True
Console.WriteLine(explicitPolicy.Buffered); // False
```


`RefitSettings.RequestBodySerialization` controls a different step: how the serializer creates JSON content.

| `RequestBodySerializationMode` | Behavior |
| --- | --- |
| `Default = 0` | Uses the serializer's usual content method. System.Text.Json uses its async metadata path. |
| `Buffered = 1` | Uses [`ISynchronousContentSerializer`](../serialization/json.md) to write a complete byte buffer. |
| `Streamed = 2` | Uses that interface to write into the outgoing stream without storing the whole body. |

Buffered content can provide its length before sending. Streamed content usually cannot.
Both synchronous modes can use generated fast-path writers when the JSON options allow them.
If your serializer lacks that capability, the generated and reflection builders fall back to
its normal `ToHttpContent` method. The source example verifies that fallback with a wrapper
that exposes only `IHttpContentSerializer` and retains generated JSON metadata.
See [serializer capabilities](../serialization/json.md#serializer-capabilities).
Choose `mode` from the three values above. The runnable example sends and checks the same JSON
through each mode, then repeats the requests with the content-only capability wrapper.

```csharp
RefitSettings settings = new(new SystemTextJsonContentSerializer(JsonOptions)) { RequestBodySerialization = mode };
IBodyApi api = RestService.ForGenerated<IBodyApi>(httpClient, settings);
Person result = await api.JsonAsync(new(1, "Ada"));
Console.WriteLine(result.Name); // Ada
```

`ContentOnlySerializer` below stands for any serializer that implements only `IHttpContentSerializer`.
The example's version forwards each call to the `SystemTextJsonContentSerializer` it wraps.

```csharp
ContentOnlySerializer limited = new(new SystemTextJsonContentSerializer(JsonOptions));
RefitSettings fallback = new(limited) { RequestBodySerialization = mode };
IBodyApi fallbackApi = RestService.ForGenerated<IBodyApi>(httpClient, fallback);
Person fallbackResult = await fallbackApi.JsonAsync(new(1, "Ada"));
```


## Compression and ownership

`BodyAttribute.Compression` overrides `RefitSettings.RequestCompression` for one method.
`Default` follows settings. `None` opts that body out of compression.
`CompressionLevel` applies when the attribute explicitly selects a coding.

Gzip works on every Refit target. Brotli needs .NET 8 or later.
Zstandard deliberately requires .NET 11; it is unavailable in these .NET 10 examples.
Unsupported codings throw `PlatformNotSupportedException` when Refit builds the request.

| `RequestCompression` | Result |
| --- | --- |
| `Default = 0` | The attribute takes coding and level from settings. Settings set to `Default` do not compress. |
| `None = 1` | No coding; an attribute can opt out of a settings-level coding. |
| `GZip = 2` | `Content-Encoding: gzip`. |
| `Brotli = 3` | `Content-Encoding: br` on .NET 8 and later. |
| `Zstandard = 4` | `Content-Encoding: zstd` on .NET 11 and later. |

These policies are declared on generated API methods. The local timeout constant is 25 milliseconds.
The example also checks whole-string form escaping and a single JSON Lines value.

```csharp
internal interface IBodyPolicyApi
{
    [Post("/body/buffered")]
    Task<Person> BufferedAsync([Body(true)] Person person);

    [Post("/body/none")]
    Task<Person> NoneAsync([Body(BodySerializationMethod.Serialized, false, Compression = RequestCompression.None)] Person person);

    [Post("/body/brotli")]
    Task<Person> BrotliAsync([Body(BodySerializationMethod.Serialized, Compression = RequestCompression.Brotli, CompressionLevel = CompressionLevel.Fastest)] Person person);

    [Post("/body/form-text")]
    Task<Person> FormTextAsync([Body(BodySerializationMethod.UrlEncoded)] string text);

    [Post("/body/one-line")]
    Task<Person> OneLineAsync([Body(BodySerializationMethod.JsonLines)] Person person);

    [Get("/child")]
    Task<Person> RootedAsync();

    [Get("child")]
    Task<Person> RelativeAsync();

    [Get("/body/timeout")]
    [Timeout(BodyPolicies.TimeoutMilliseconds)]
    Task<Person> TimeoutAsync();
}
```

`RequestCompressionOptions` exists on .NET 9 and later, with a public default constructor.
Its nullable `GZip` and `Brotli` properties accept `ZLibCompressionOptions` and
`BrotliCompressionOptions`. A nonnull options object for a coding overrides the compression level
for that coding. A null property leaves that coding using its resolved level.
Options remain settings-level choices even when the body attribute selects the coding.

```csharp
RefitSettings settings = new(new SystemTextJsonContentSerializer(JsonOptions))
{
    RequestCompression = RequestCompression.GZip,
    RequestCompressionLevel = CompressionLevel.Fastest,
    RequestCompressionOptions = new() { GZip = new(), Brotli = new() },
};
IBodyApi inherited = RestService.ForGenerated<IBodyApi>(httpClient, settings);
IBodyPolicyApi overrides = RestService.ForGenerated<IBodyPolicyApi>(httpClient, settings);
await inherited.JsonAsync(new(1, "Ada"));
await overrides.BrotliAsync(new(1, "Ada"));
await overrides.NoneAsync(new(1, "Ada"));
```

The local handler decompresses both gzip and Brotli and verifies the exact restored JSON.
It also verifies that the `None` attribute sends no content-coding header.

## Zstandard options on .NET 11

The .NET 11 build adds `RequestCompressionOptions.Zstandard`, accepting
`ZstandardCompressionOptions`. This separate generated-client project exercises all three
coding option properties and verifies the exact headers and decompressed bytes.
`AppendChecksum` configures the Zstandard frame, while a null options property selects the level-based path.

```csharp
RefitSettings settings = new(new SystemTextJsonContentSerializer(JsonOptions))
{
    RequestCompressionLevel = CompressionLevel.Fastest,
    RequestCompressionOptions = new() { GZip = new(), Brotli = new(), Zstandard = new() { AppendChecksum = true } },
};
ICompressionApi api = RestService.ForGenerated<ICompressionApi>(httpClient, settings);
```

Here `coding` selects `GZip`, `Brotli` or `Zstandard`.

```csharp
settings.RequestCompression = coding;
Person result = await api.PutAsync(new(1, "Ada"));
Console.WriteLine(result.Name); // Ada
```

Run the separate [.NET 11 project](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Bodies/Compression/Compression.csproj)
when using Zstandard; the main .NET 10 examples verify that requesting it fails.

## URI and per-call deadline policies

`RefitLegacy = 0` preserves the base-address path and requires a leading slash on the method path.
`Rfc3986 = 1` uses HttpClient's URI merge rules. With a base address ending in `/root/`, `/child`
resolves to `/child` in RFC mode, and `child` appends to become `/root/child`.
A base address without its final slash treats the last segment as a file to replace.
The local example checks the legacy prefix and both RFC forms.

```csharp
HttpClient httpClient = new() { BaseAddress = new Uri("https://people.example/root/") };

RefitSettings legacy = new(new SystemTextJsonContentSerializer(JsonOptions)) { UrlResolution = UrlResolutionMode.RefitLegacy };
IBodyPolicyApi legacyApi = RestService.ForGenerated<IBodyPolicyApi>(httpClient, legacy);
await legacyApi.RootedAsync(); // /root/child

RefitSettings rfc = new(new SystemTextJsonContentSerializer(JsonOptions)) { UrlResolution = UrlResolutionMode.Rfc3986 };
IBodyPolicyApi rfcApi = RestService.ForGenerated<IBodyPolicyApi>(httpClient, rfc);
```

`TimeoutAttribute(int milliseconds)` exposes its value through the read-only `Milliseconds` property.
A positive value applies a deadline to the effective cancellation token for the call.
Zero and negative values disable that per-call deadline. It composes with the caller's token,
HttpClient timeout and handler timeouts; the first cancellation takes effect.
The local handler waits for cancellation, so this example verifies the deadline without a live server.

```csharp
TimeoutAttribute timeout = new(TimeoutMilliseconds);
TimeoutAttribute disabled = new(0);
TimeoutAttribute negative = new(-1);
Console.WriteLine(timeout.Milliseconds);
```

```csharp
IBodyPolicyApi api = RestService.ForGenerated<IBodyPolicyApi>(client, settings);
try
{
    await api.TimeoutAsync();
}
catch (OperationCanceledException)
{
    Console.WriteLine("The per-call deadline canceled the request.");
    return;
}
```

A timeout surfaces as `OperationCanceledException` or its `TaskCanceledException` subclass.


Refit keeps a supplied stream open when it disposes the request. You own and dispose that stream.
A supplied `HttpContent` becomes request content and is disposed with the request.
Do not share the same content instance across concurrent calls.


## Obsolete JSON body method

`BodySerializationMethod.Json = 1` is an obsolete compatibility value. It follows the serializer
path, including for strings, while `Serialized = 3` supplies the current name without a compiler warning.
A compiler probe verifies the shipped `CS0618` diagnostic; the live examples use `Serialized`.
The probe names that diagnostic with the `ObsoleteWarning` constant.

```csharp
const string source = """
    internal static class LegacyBodyMode
    {
        internal static Refit.BodySerializationMethod Mode => Refit.BodySerializationMethod.Json;
    }
    """;
CSharpCompilation compilation = ToolingCompilation.Create(source);
bool warned = AnalyzerSample.Contains(compilation.GetDiagnostics(), ObsoleteWarning);
```

Body creation and coding rules are in [GeneratedRequestRunner.BodyContent.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/GeneratedRequestRunner.BodyContent.cs),
[GeneratedRequestRunner.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/GeneratedRequestRunner.cs)
and [RequestContentCoding.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestContentCoding.cs).
Per-call cancellation is in [RequestExecutionHelpers.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestExecutionHelpers.cs).

## API reference

| API | Description | Parameters or value | Returns and behavior |
| --- | --- | --- | --- |
| [`BodyAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Marks one interface-method parameter as the HTTP request body. | Applies to a parameter. | Refit uses the parameter value as `HttpContent`, stream content, plain text, or serialized content according to its type and `SerializationMethod`. |
| [`BodySerializationMethod`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodySerializationMethod.cs) | Selects how Refit turns a body value into HTTP content. | Enum values below. | Use with `BodyAttribute` to choose text, serialized, form, or JSON Lines content. |
| [`RequestBodySerializationMode`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBodySerializationMode.cs) | Selects how Refit writes serialized JSON request content. | Enum values below. | Configure through `RefitSettings.RequestBodySerialization`. |
| [`RequestCompression`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompression.cs) | Selects the content coding applied to a request body. | Enum values below. | Configure a default in `RefitSettings` or override it on `BodyAttribute`. |
| [`RequestCompressionOptions`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompressionOptions.cs) | Holds optional compressor-specific settings that replace the resolved compression level for each coding. | Available on .NET 9 and later. | Assign it to `RefitSettings.RequestCompressionOptions`. |
| [`TimeoutAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/TimeoutAttribute.cs) | Applies a per-call timeout to a Refit interface method. | Applies to a method. | A positive timeout cancels the request when it elapses. |
| [`BodySerializationMethod.Default = 0`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodySerializationMethod.cs) | Uses Refit's standard body rules. | `0` | Passes `HttpContent` and streams through, sends strings as plain text, and uses the configured serializer for other values. |
| [`BodySerializationMethod.Json = 1`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodySerializationMethod.cs) | Retains the former name for serialized content. | `1`; obsolete. | Uses the configured serializer, including for strings. Use `Serialized` in new code. |
| [`BodySerializationMethod.UrlEncoded = 2`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodySerializationMethod.cs) | Writes form URL-encoded content. | `2` | A dictionary or object's fields supply form keys and values. |
| [`BodySerializationMethod.Serialized = 3`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodySerializationMethod.cs) | Serializes every body value with the configured content serializer. | `3` | Strings use the serializer too, so a JSON string includes its quotes. |
| [`BodySerializationMethod.JsonLines = 4`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodySerializationMethod.cs) | Writes newline-delimited JSON. | `4` | Serializes each enumerable item with the configured serializer and writes one item per line. |
| [`RequestBodySerializationMode.Default = 0`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBodySerializationMode.cs) | Uses the serializer's asynchronous JSON-content path. | `0` | `System.Text.Json` uses its metadata-based path. |
| [`RequestBodySerializationMode.Buffered = 1`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBodySerializationMode.cs) | Serializes JSON into a complete byte buffer before sending. | `1`; requires [`ISynchronousContentSerializer`](../serialization/json.md). | Sends `ByteArrayContent` with `Content-Length`; suited to small and medium bodies. |
| [`RequestBodySerializationMode.Streamed = 2`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBodySerializationMode.cs) | Writes JSON through a `Utf8JsonWriter` to the request stream. | `2`; requires [`ISynchronousContentSerializer`](../serialization/json.md). | Bounds peak memory with pooled chunks and does not set `Content-Length`; suited to large uploads. |
| [`RequestCompression.Default = 0`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompression.cs) | Inherits the coding from `RefitSettings.RequestCompression`. | `0` | Uses the settings coding and level. |
| [`RequestCompression.None = 1`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompression.cs) | Disables compression for this body. | `1` | Sends no content coding even when settings choose one. |
| [`RequestCompression.GZip = 2`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompression.cs) | Compresses the body with gzip. | `2`; every Refit target. | Sends `Content-Encoding: gzip`. |
| [`RequestCompression.Brotli = 3`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompression.cs) | Compresses the body with Brotli. | `3`; .NET 8 and later. | Sends `Content-Encoding: br`. |
| [`RequestCompression.Zstandard = 4`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompression.cs) | Compresses the body with Zstandard. | `4`; .NET 11 and later. | Sends `Content-Encoding: zstd`. |
| [`BodyAttribute()`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Creates a body parameter attribute without overrides. | None. | Uses `SerializationMethod.Default` and leaves `Buffered` unset so settings decide. |
| [`BodyAttribute(bool buffered)`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Creates a body parameter attribute with an explicit buffering policy. | `buffered`: [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | Sets `Buffered`; serialization remains `Default`. |
| [`BodyAttribute(BodySerializationMethod serializationMethod, bool buffered)`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Creates a body parameter attribute with explicit serialization and buffering policies. | `serializationMethod`: `BodySerializationMethod`; `buffered`: [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | Sets both properties. |
| [`BodyAttribute(BodySerializationMethod serializationMethod)`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Creates a body parameter attribute with an explicit serialization method. | `serializationMethod`: `BodySerializationMethod`. | Sets `SerializationMethod` and leaves `Buffered` unset so settings decide. |
| [`RequestCompressionOptions()`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompressionOptions.cs) | Creates empty compressor-specific settings. | None; .NET 9 and later. | All coding option properties are `null`, so compression uses its resolved level. |
| [`TimeoutAttribute(int milliseconds)`](https://github.com/reactiveui/refit/blob/main/src/Refit/TimeoutAttribute.cs) | Creates a method timeout attribute. | `milliseconds`: [`int`](https://learn.microsoft.com/dotnet/api/system.int32). | A positive value applies the per-call deadline; zero or a negative value disables it. |
| [`BodyAttribute.Buffered`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Gets the per-body buffering override. | Read-only [`bool?`](https://learn.microsoft.com/dotnet/api/system.boolean). | `null` uses `RefitSettings.Buffered`; `true` buffers content before sending and `false` skips it. |
| [`BodyAttribute.SerializationMethod`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Gets the selected body serialization method. | Read-only `BodySerializationMethod`; default `Default`. | Determines how ordinary body values become HTTP content. |
| [`BodyAttribute.Compression`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Gets or sets a method-level request content coding. | Settable `RequestCompression`; default `Default`. | `Default` follows settings, while `None` opts this body out of a settings-level coding. |
| [`BodyAttribute.CompressionLevel`](https://github.com/reactiveui/refit/blob/main/src/Refit/BodyAttribute.cs) | Gets or sets the compression effort for an explicitly selected coding. | Settable [`CompressionLevel`](https://learn.microsoft.com/dotnet/api/system.io.compression.compressionlevel); default `Optimal`. | Refit reads it only when `Compression` names a coding; otherwise settings provide the level. |
| [`RequestCompressionOptions.GZip`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompressionOptions.cs) | Gets or sets gzip-specific compressor settings. | Settable [`ZLibCompressionOptions?`](https://learn.microsoft.com/dotnet/api/system.io.compression.zlibcompressionoptions). | A non-null value replaces the resolved level for gzip; `null` uses that level. |
| [`RequestCompressionOptions.Brotli`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompressionOptions.cs) | Gets or sets Brotli-specific compressor settings. | Settable [`BrotliCompressionOptions?`](https://learn.microsoft.com/dotnet/api/system.io.compression.brotlicompressionoptions). | A non-null value replaces the resolved level for Brotli; `null` uses that level. |
| [`RequestCompressionOptions.Zstandard`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompressionOptions.cs) | Gets or sets Zstandard-specific compressor settings. | Settable [`ZstandardCompressionOptions?`](https://learn.microsoft.com/dotnet/api/system.io.compression.zstandardcompressionoptions); .NET 11 and later. | A non-null value replaces the resolved level for Zstandard; `null` uses that level. |
| [`TimeoutAttribute.Milliseconds`](https://github.com/reactiveui/refit/blob/main/src/Refit/TimeoutAttribute.cs) | Gets the timeout supplied to `TimeoutAttribute`. | Read-only [`int`](https://learn.microsoft.com/dotnet/api/system.int32), in milliseconds. | The effective request deadline exists only when the value is positive. |
