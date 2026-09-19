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
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(Person[]))]
[JsonSerializable(typeof(List<Person>))]
internal sealed partial class SampleJsonContext : JsonSerializerContext;
```

Create one serializer and reuse its settings. The [JSON guide](../serialization/json.md) shows the complete setup.

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

**4. Send the requests.** `host.Client` is the shared HTTP client from the
[first-request example](../index.md). `Settings` uses the generated JSON context above.

```csharp
const int graceId = 2;
IBodyApi api = RestService.ForGenerated<IBodyApi>(host.Client, Settings);
Person saved = await api.JsonAsync(new(1, "Ada"));
await api.TextAsync("hello");
await api.QuotedAsync("quoted");

await using MemoryStream stream = new("stream text"u8.ToArray());
await api.StreamAsync(stream);
Console.WriteLine(stream.CanRead); // True: the caller still owns the stream.

using StringContent content = new("content text");
await api.ContentAsync(content);
await api.FormAsync(new());
await api.LinesAsync([new(1, "Ada"), new(graceId, "Grace")]);
await api.GzipAsync(new(1, "Ada"));
Console.WriteLine(saved.Name); // Ada
```

The source example checks the actual bytes received by each local route.
It also decompresses the gzip body and checks the restored JSON.
JSON Lines puts a newline between the two items. Refit does not add a trailing newline.
See: [the runnable body examples](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Bodies).

## Choose a serialization method

| `BodySerializationMethod` | Behavior |
| --- | --- |
| `Default = 0` | Passes `HttpContent` and streams through. Sends a string as plain text. Uses the configured serializer for other values. |
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
| `Buffered = 1` | Uses `ISynchronousContentSerializer` to write a complete byte buffer. |
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
RefitSettings settings = new(host.Settings.ContentSerializer) { RequestBodySerialization = mode };
IBodyApi api = RestService.ForGenerated<IBodyApi>(host.Client, settings);
Person result = await api.JsonAsync(new(1, "Ada"));
Console.WriteLine(result.Name); // Ada
```

```csharp
ContentOnlySerializer limited = new((SystemTextJsonContentSerializer)host.Settings.ContentSerializer);
RefitSettings fallback = new(limited) { RequestBodySerialization = mode };
IBodyApi fallbackApi = RestService.ForGenerated<IBodyApi>(host.Client, fallback);
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
RefitSettings settings = new(host.Settings.ContentSerializer)
{
    RequestCompression = RequestCompression.GZip,
    RequestCompressionLevel = CompressionLevel.Fastest,
    RequestCompressionOptions = new() { GZip = new(), Brotli = new() },
};
IBodyApi inherited = RestService.ForGenerated<IBodyApi>(host.Client, settings);
IBodyPolicyApi overrides = RestService.ForGenerated<IBodyPolicyApi>(host.Client, settings);
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
RefitSettings settings = new(host.Settings.ContentSerializer)
{
    RequestCompressionLevel = CompressionLevel.Fastest,
    RequestCompressionOptions = new() { GZip = new(), Brotli = new(), Zstandard = new() { AppendChecksum = true } },
};
ICompressionApi api = RestService.ForGenerated<ICompressionApi>(host.Client, settings);
```

Here `coding` selects `GZip`, `Brotli` or `Zstandard`.

```csharp
settings.RequestCompression = coding;
Person result = await api.PutAsync(new(1, "Ada"));
Console.WriteLine(result.Name); // Ada
```

Run the [separate .NET 11 project](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Bodies/Compression)
when using Zstandard; the main .NET 10 examples verify that requesting it fails.

## URI and per-call deadline policies

`RefitLegacy = 0` preserves the base-address path and requires a leading slash on the method path.
`Rfc3986 = 1` uses HttpClient's URI merge rules. With a base address ending in `/root/`, `/child`
resolves to `/child` in RFC mode, and `child` appends to become `/root/child`.
A base address without its final slash treats the last segment as a file to replace.
The local example checks the legacy prefix and both RFC forms.

```csharp
RefitSettings legacy = new(host.Settings.ContentSerializer) { UrlResolution = UrlResolutionMode.RefitLegacy };
IBodyPolicyApi legacyApi = RestService.ForGenerated<IBodyPolicyApi>(host.Client, legacy);
await legacyApi.RootedAsync(); // /root/child

RefitSettings rfc = new(host.Settings.ContentSerializer) { UrlResolution = UrlResolutionMode.Rfc3986 };
IBodyPolicyApi rfcApi = RestService.ForGenerated<IBodyPolicyApi>(host.Client, rfc);
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
