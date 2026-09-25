---
Order: 2
---
# Generated request helpers

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/advanced-request-helpers/advanced-request-helpers.csproj).

Behind a Refit interface call are several steps: filling in the URL, adding headers, writing
the body, sending the request and reading the reply. The source generator writes that code
for each interface method, and it calls `GeneratedRequestRunner` to carry out many of the steps.

You do not call these helpers for an ordinary service call. This page shows the code Refit
generates for common interface methods, so you can read it and debug it. Each section then
shows what a helper returns for a given input. If you call a helper yourself, you also choose
the formatting, cancellation and ownership rules that the generator normally chooses from
your interface.

The generated excerpts on this page are trimmed from the generator's output. They drop the
`global::` prefixes and the `refit` prefix on local names, and they show enum values by name
where the generator writes a cast. `settings` is the client's `RefitSettings`, and `Client` is
its `HttpClient`.

## Build a path

**You write** methods with route placeholders:

```csharp
public interface IProductsApi
{
    [Get("/products/{id}")]
    Task<Product> GetAsync(int id);

    [Get("/files/{**path}")]
    Task<Stream> GetFileAsync(string path);
}
```

**Refit generates** a `BuildRequestPath` call with the placeholder's position worked out at
compile time. For the integer `id`, it uses the generic overload when default formatting applies:

```csharp
useDefaultFormatting
    ? GeneratedRequestRunner.BuildRequestPath("/products/{id}", settings.AllowUnmatchedRouteParameters, (10, 14), id)
    : GeneratedRequestRunner.BuildRequestPath("/products/{id}", settings.AllowUnmatchedRouteParameters, [((10, 14), GeneratedRequestRunner.FormatUrlParameter(settings, id, GeneratedParameterAttributeProvider.Empty, typeof(int)))])
```

For the catch-all `{**path}`, it escapes the value with `RoundTripEscapePath` and marks it as
already encoded:

```csharp
GeneratedRequestRunner.BuildRequestPath(
    "/files/{**path}",
    settings.AllowUnmatchedRouteParameters,
    [((7, 15), GeneratedRequestRunner.RoundTripEscapePath(path?.ToString(), settings, GeneratedParameterAttributeProvider.Empty, typeof(string)), true)])
```

`BuildRequestPath` takes placeholder ranges with an inclusive start and an exclusive end.
Keep the ranges in order, do not let them overlap, and include the braces in each range.
The overload taking string values escapes each replacement. Its overload with a `PreEncoded`
flag appends flagged values verbatim. A null replacement for an optional `{name?}` removes
the `/` before it. A plain `{name}` with a null value leaves an empty segment.
The two-argument overload checks a template that has no replacements. Any unresolved
placeholder throws `ArgumentException` unless `allowUnmatchedParameter` is true.

```csharp
string byName = GeneratedRequestRunner.BuildRequestPath("/products/{name}", false, [((10, 16), "shoes/boots")]); // "/products/shoes%2Fboots"
string order = GeneratedRequestRunner.BuildRequestPath("/orders/{id}", false, (8, 12), 42, "D6"); // "/orders/000042"
string file = GeneratedRequestRunner.RoundTripEscapePath("reports/2026 q3.pdf", settings, GeneratedParameterAttributeProvider.Empty, typeof(string)); // "reports/2026%20q3.pdf"
```

The generic overload without a format appends an invariant formatted span without escaping
it, when it fits its buffer. The generator uses it only for unformatted integers, whose digits
and optional minus sign are safe in a URL. Do not use that overload for other
`ISpanFormattable` values. The generic overload with a format escapes the rendered value
and supports other span-formattable values.
The no-format generic overload is compiled under `NET6_0_OR_GREATER`, and its formatted
counterpart under `NET8_0_OR_GREATER`. Both are present on Refit's .NET 8 and later targets
and absent on its .NET Framework targets.

`RoundTripEscapePath` keeps the `/` separators of a catch-all route value, and formats and
escapes each section between them. Its result is already escaped, so insert it with
`PreEncoded = true`. Otherwise the percent signs are escaped a second time.

## Resolve the request URL

**Refit generates** a `BuildRelativeUri` call around every path. A `[QueryUriFormat]` method
adds a fourth argument with its `UriFormat`:

```csharp
var request = new HttpRequestMessage(
    HttpMethod.Get,
    GeneratedRequestRunner.BuildRelativeUri(Client, "/products", settings.UrlResolution));
```

`BuildRelativeUri` returns a relative `Uri`, not the final absolute request address.
With `UrlResolutionMode.RefitLegacy`, it requires a leading slash and prefixes the path of the
client's base address, without that path's trailing slash. A missing base address throws
`InvalidOperationException`. With `Rfc3986`, it leaves the relative path for `HttpClient`
to resolve. The overload taking `UriFormat` re-encodes the full path and query in legacy
mode. RFC mode ignores that argument.

```csharp
HttpClient httpClient = new() { BaseAddress = new Uri("https://api.example.com/v2/") };
Uri legacy = GeneratedRequestRunner.BuildRelativeUri(httpClient, "/products", UrlResolutionMode.RefitLegacy); // "/v2/products"
Uri rfc = GeneratedRequestRunner.BuildRelativeUri(httpClient, "products", UrlResolutionMode.Rfc3986); // "products"
```

**You write** a `[Url]` parameter when the caller supplies the whole address:

```csharp
[Get("")]
Task<Stream> DownloadAsync([Url] string url);
```

**Refit generates** a check, then uses the address as it is:

```csharp
var absoluteUrl = GeneratedRequestRunner.RequireAbsoluteUrl(url);
var request = new HttpRequestMessage(HttpMethod.Get, new Uri(absoluteUrl, UriKind.Absolute));
```

`RequireAbsoluteUrl` accepts a `string` or a `Uri`, and returns its absolute URL text.
A `Uri` contributes its `OriginalString`. It rejects null, empty and relative values with
`ArgumentException`. For example, `RequireAbsoluteUrl("https://cdn.example.com/images/42.png")`
returns the same text.

The check tests for `UriKind.Absolute`. It does not require an HTTP or HTTPS scheme.
On Linux, `RequireAbsoluteUrl("/items")` returns `"/items"`, which .NET accepts as a file URI.
A generated `[Url]` request can then reach HTTP dispatch with an unsupported scheme, instead of
failing this helper's argument check. The complete sample shows this limitation. Always pass
an explicit HTTP or HTTPS URL.

[Query building and formatting](query-builder.md) covers `BuildQueryKey`, `FormatInvariant`,
`FormatUrlParameter`, the three default-formatter guards, and `AddFormattedCollectionProperty`.

## Set headers and request options

**You write** header, property and timeout attributes:

```csharp
public interface IProductsApi
{
    [Post("/orders")]
    Task CreateAsync([Body] Order order, [Header("X-Request-Id")] string requestId, CancellationToken cancellationToken);

    [Get("/products")]
    [Timeout(5000)]
    Task<List<Product>> ListAsync([HeaderCollection] IDictionary<string, string> headers, [Property("TraceId")] int traceId);
}
```

**Refit generates** one helper call for each. Every method also gets the
`AddConfiguredRequestOptions` call. These lines come from both methods:

```csharp
GeneratedRequestRunner.SetHeader(request, "X-Request-Id", requestId?.ToString(), settings.ValidateHeaders);
GeneratedRequestRunner.AddHeaderCollection(request, headers, settings.ValidateHeaders);
GeneratedRequestRunner.AddConfiguredRequestOptions(request, settings, typeof(IProductsApi));
GeneratedRequestRunner.AddRequestProperty<int>(request, "TraceId", traceId);
GeneratedRequestRunner.SetRequestTimeout(request, 5000);
```

`SetHeader` removes an earlier request or content header with the same name, then adds
the new value. Null removes a header without adding one. On a method that accepts a body,
it can create empty content so a content header has a place to live. It strips CR and LF
from the name and value. With `validateHeaders: true`, a malformed value can throw
`FormatException`. Otherwise it adds the value with the headers' `TryAddWithoutValidation` method.
`AddHeaderCollection` applies the same rules to each dictionary entry. A null dictionary
does nothing. Later values replace earlier values with the same key.

```csharp
using HttpRequestMessage request = new(HttpMethod.Get, "/products");
GeneratedRequestRunner.SetHeader(request, "X-Api-Version", "1", validateHeaders: true);
GeneratedRequestRunner.SetHeader(request, "X-Api-Version", "2", validateHeaders: true);
// request.Headers.GetValues("X-Api-Version") returns only "2"
```

`AddConfiguredRequestOptions` applies the settings' request options and the interface type.
On .NET 8 and later it also applies the configured HTTP version and version policy.
`AddRequestProperty<TValue>` sets a typed `HttpRequestMessage.Options` value on those
targets. On .NET Framework it uses the request's `Properties` dictionary.
`SetRequestTimeout` stores the per-call milliseconds for the sending helpers. A positive
value applies a timeout in addition to cancellation. Zero or a negative value turns this
timeout off. Storing the option does not start a timer or send a request.

## Create body content

**You write** a `[Body]` parameter. For the `CreateAsync` method above, **Refit generates**:

```csharp
request.Content = GeneratedRequestRunner.CreateBodyContent<Order>(
    settings,
    order,
    BodySerializationMethod.Default,
    !settings.Buffered);
request.Content = GeneratedRequestRunner.CompressBodyContent(
    request.Content,
    settings,
    RequestCompression.Default,
    CompressionLevel.Optimal);
```

`CreateBodyContent<TBody>` returns an existing `HttpContent` unchanged and wraps a
`Stream` with `CreateStreamContent`. With `BodySerializationMethod.Default`, a string is
sent as raw text. Other values, or the `Serialized` mode, use the configured content
serializer. For ordinary values, this method supports `Default` and `Serialized`
(and the obsolete `Json` value, kept for compatibility). Other modes throw
`ArgumentOutOfRangeException`. Generated code calls the separate URL-encoded or JSON Lines
helpers for those modes. `streamBody: true` writes serialized content through a streaming
wrapper, unless the settings choose synchronous serialization, which already creates a buffer.

```csharp
using HttpContent note = GeneratedRequestRunner.CreateBodyContent(settings, "Leave at the front desk", BodySerializationMethod.Default, streamBody: false);
// await note.ReadAsStringAsync() returns "Leave at the front desk", sent as text/plain
```

`CreateJsonLinesBodyContent<TBody>` serializes each element of an enumerable as a JSON
value, with a newline between values and no trailing newline. It treats a string as a single
value, not as a sequence of characters. Existing content and streams pass through, as with
the other body helpers. For example, `CreateJsonLinesBodyContent(settings, new[] { 3, 7 })`
writes `3`, a newline, then `7`.

For a body declared as `IEnumerable<T>`, generated code calls `CreateTypedJsonLinesBodyContent<T>` instead.
It writes the same bytes. For a body declared as `IAsyncEnumerable<T>`, generated code calls
`CreateAsyncJsonLinesBodyContent<T>`, which writes each element as the producer yields it.
[Upload many records](../requests/bodies.md#upload-many-records-as-json-lines) shows both from the caller's side.

**You write** a `[Multipart]` method:

```csharp
[Multipart]
[Post("/upload")]
Task UploadAsync([AliasAs("file")] Stream file, Product metadata);
```

**Refit generates** one part per argument:

```csharp
var multipart = new MultipartFormDataContent("----MyGreatBoundary");
if (file != null)
{
    multipart.Add(GeneratedRequestRunner.CreateStreamContent(file), "file", "file");
}
if (metadata != null)
{
    multipart.Add(GeneratedRequestRunner.SerializeMultipartPart(settings, metadata, "metadata"), "metadata");
}
request.Content = multipart;
```

`SerializeMultipartPart<T>` serializes one part through the configured serializer. It does
not create a multipart container. It wraps a serializer failure in `ArgumentException`, with
the field name and the original exception.

`CreateStreamContent` leaves the caller's stream open when the content is disposed.
You still own that stream and must dispose it. Existing content that a helper returns
unchanged does not get this stream protection.

`CompressBodyContent` resolves `RequestCompression.Default` from the settings.
Explicit `None` returns the same content. An explicit coding also uses the compression
level you pass, while `Default` uses the settings' level. Compressor options in the
settings can override level-based construction. The returned compression content owns its
inner content, so dispose the returned wrapper.

```csharp
using HttpContent gzip = GeneratedRequestRunner.CompressBodyContent(new StringContent("""{"orderId":42}"""), settings, RequestCompression.GZip, CompressionLevel.Fastest);
// gzip.Headers.ContentEncoding contains "gzip"
```

Add `using System.IO.Compression;` for `CompressionLevel`. GZip is available on all Refit
targets, Brotli on .NET 8 and later, and Zstandard on .NET 11 and later. Requesting an
unavailable coding throws `PlatformNotSupportedException`. The .NET 10 sample checks this
exception for Zstandard. It does not verify successful .NET 11 Zstandard compression or its options.

## Supply form descriptors

**You write** a URL-encoded body. When every property is a simple value, **Refit generates**
code that reads each property directly and builds a `FormUrlEncodedContent`. That code calls
`CanUnrollForm`, `BuildQueryKey` and `FormatInvariant`, and it falls back to
`CreateUrlEncodedBodyContent(settings, form)` when the serializer is not
`SystemTextJsonContentSerializer`. When a property is a collection, Refit generates an array
of `FormField<TBody>` descriptors instead:

```csharp
public sealed class TagForm
{
    public string? Name { get; set; }

    [Query(CollectionFormat.Csv)]
    public string[]? Tags { get; set; }
}

[Post("/tags")]
Task SaveTagsAsync([Body(BodySerializationMethod.UrlEncoded)] TagForm form);
```

```csharp
private static readonly FormField<TagForm>[] formFields = new FormField<TagForm>[]
{
    new FormField<TagForm>(static body => (object?)body.Name, "Name", null, null, null, null, false),
    new FormField<TagForm>(static body => (object?)body.Tags, "Tags", null, null, null, CollectionFormat.Csv, false),
};

request.Content = GeneratedRequestRunner.CreateUrlEncodedBodyContent<TagForm>(settings, form, formFields);
```

`CreateUrlEncodedBodyContent<TBody>(settings, body)` flattens form values from the
declared body's public properties or dictionary entries. It escapes a string as one whole
value, so `a=b` becomes `a%3Db`. It does not parse the string as an already encoded form.
Existing content and streams pass through. This overload uses reflection to flatten an
ordinary object.

The overload taking `FormField<TBody>[]` can use the descriptors' getters instead. That path
applies only to a non-null object that is not a dictionary, with a
`SystemTextJsonContentSerializer` configured. Other serializer types can need their
property-name hook and fall back to reflection. Nested complex form values can also require
runtime property discovery. The descriptors avoid discovery for known simple fields. They do
not guarantee it for every body shape.

`FormField<TBody>` stores the `Getter`, `ClrName`, `ExplicitName`, `PrefixSegment`,
`Format`, `CollectionFormat` and `SerializeNull` passed to its constructor, as read-only
properties. `ResolveFieldName` uses `ExplicitName` when it is set. Otherwise it
formats `ClrName` with the key formatter you pass. It then prepends the prefix verbatim.
An explicit collection format overrides the settings' default. `SerializeNull: true` emits an
empty field for a null value; `false` omits it.

```csharp
TagForm form = new() { Name = "Summer sale", Tags = ["shoes", "hats"] };
FormField<TagForm>[] fields =
[
    new(static value => value.Name, nameof(TagForm.Name), "name", null, null, null, false),
    new(static value => value.Tags, nameof(TagForm.Tags), null, null, null, CollectionFormat.Csv, false),
];
using HttpContent content = GeneratedRequestRunner.CreateUrlEncodedBodyContent(settings, form, fields);
string body = await content.ReadAsStringAsync(); // "name=Summer+sale&Tags=shoes%2Chats"
```

`CanUnrollForm` reports whether the body is a plain non-null object. It returns `false` for
strings, streams, existing content and dictionaries. It does not check the serializer or send
a request.

## Send a built request

**Refit generates** one dispatch call at the end of each method. The call depends on the
return type:

| You write | Refit generates |
| --- | --- |
| `Task CreateAsync(..., CancellationToken cancellationToken)` | `SendVoidAsync(Client, request, settings, settings.Buffered, cancellationToken)` |
| `Task<List<Product>> ListAsync(...)` | `SendAsync<List<Product>, List<Product>>(Client, request, settings, false, true, false, CancellationToken.None)` |
| `Task<ApiResponse<Order>> SubmitAsync([Body(BodySerializationMethod.UrlEncoded)] OrderForm form)` | `SendAsync<ApiResponse<Order>, Order>(Client, request, settings, true, true, settings.Buffered, CancellationToken.None)` |
| `IObservable<List<Product>> WatchAsync()` | `SendObservable<List<Product>, List<Product>>(Client, BuildRequest, settings, false, true, false, CancellationToken.None)` |
| `IAsyncEnumerable<Product> StreamAsync(CancellationToken cancellationToken)` | `StreamAsync<Product>(Client, request, settings, cancellationToken)` |

The generated code passes the method's `CancellationToken` when it has one, and
`CancellationToken.None` when it does not. It passes `settings.Buffered` as `bufferBody` when
the method sends a serialized or form body.

All four dispatch methods require the client's `BaseAddress`, even when a request
has an absolute URI. They apply the configured authorization getter, exception handling
and positive per-call timeout. The task methods dispose the request after sending it.
Choose the flags to match the return type, as the generator does.

`SendVoidAsync` sends a request with no returned body and disposes the response.
The default exception factory throws on an HTTP error. `SendAsync<T, TBody>` can deserialize
`T`, return the raw response, content or stream, or build an API response wrapper.
`isApiResponse: true` requires a supported wrapper type for `T`, and `TBody` is its body type.
`bufferBody` controls buffering of the request content before sending, not the response content.

`shouldDisposeResponse` decides who disposes the response. Generated code passes `true` for
every result except `HttpResponseMessage`, `HttpContent` and `Stream`, including an
`ApiResponse<T>` whose body it has already read. For those three results it passes `false`,
because the caller reads from the live response and must dispose it. For a plain result
with an HTTP error, the pipeline hands the response to the thrown exception instead.

```csharp
List<Product>? products = await GeneratedRequestRunner.SendAsync<List<Product>, List<Product>>(
    httpClient,
    new HttpRequestMessage(HttpMethod.Get, "/products"),
    settings,
    isApiResponse: false,
    shouldDisposeResponse: true,
    bufferBody: false,
    cancellationToken);
```

`SendObservable<T, TBody>` returns a cold observable: each subscription starts a new
request. For an `IObservable<T>` method, generated code passes a local function that
builds a fresh request, because each request is disposed after use. The method token and
subscription token are linked when both can cancel. The same result and ownership flags
apply as for `SendAsync`.

`StreamAsync<T>` sends when you start enumerating and requires an `IStreamingContentSerializer`.
The built-in System.Text.Json serializer supports it. The response media type selects JSON
array, JSON Lines or server-sent event framing. The sequence disposes the request, response
and body stream when enumeration finishes or is disposed. It links the method token and
consumer token when both can cancel. A positive request timeout also applies while reading.
Unlike the observable, this call captures one request. Do not enumerate the sequence a
second time, because its request is already disposed.

The complete sample also checks the streamed values and their order, cold observable
dispatch, raw response, content and stream results, request options, and decompressed
GZip and Brotli payloads.
[Method metadata](method-metadata.md) describes the reflected information objects.

## Path and formatting overloads

All methods below are static members of `GeneratedRequestRunner`. Arguments are required
unless the signature shows a default. `settings` is the client's [RefitSettings].
A range is a [value tuple][tuple] of two [int] positions: inclusive start and exclusive end.

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `BuildRequestPath(string relativePathTemplate, bool allowUnmatchedParameter)` | Validates a parameterless route template before using it as a request path. | [string] `relativePathTemplate`: route; [bool] `allowUnmatchedParameter`: whether unresolved placeholders are allowed. | [string]: unchanged template, or throws for unresolved placeholders when the flag is false. |
| `BuildRequestPath(string relativePathTemplate, bool allowUnmatchedParameter, ReadOnlySpan<((int StartIdx, int EndIdx) Range, string? Value)> uriParams)` | Replaces several path placeholders using default escaping. | [string] template and [bool] unmatched flag; [ReadOnlySpan][span] `uriParams`: ordered placeholder ranges and replacement strings. | [string]: path with escaped replacements and optional null segments removed. |
| `BuildRequestPath(string relativePathTemplate, bool allowUnmatchedParameter, ReadOnlySpan<((int StartIdx, int EndIdx) Range, string? Value, bool PreEncoded)> uriParams)` | Replaces several placeholders while allowing selected values to bypass escaping. | [string] template and [bool] unmatched flag; [ReadOnlySpan][span] `uriParams`: ordered ranges, values, and per-value encoding flags. | [string]: path with replacements escaped unless their `PreEncoded` flag is true. |
| `BuildRequestPath<T>(string relativePathTemplate, bool allowUnmatchedParameter, (int StartIdx, int EndIdx) range, T value)` | Replaces one placeholder with an invariant unformatted numeric value. | [string] template; [bool] unmatched flag; [tuple] `range`: one placeholder; `value`: an [ISpanFormattable]. Requires `T : ISpanFormattable`. | [string]: path with an invariant formatted value. Use this overload only for unformatted integers, as explained above. |
| `BuildRequestPath<T>(string relativePathTemplate, bool allowUnmatchedParameter, (int StartIdx, int EndIdx) range, T value, string? format)` | Replaces one placeholder with an invariant value using a format string. | [string] template; [bool] unmatched flag; [tuple] `range`: placeholder; [ISpanFormattable] `value`; [string] `format`: format or `null`. Requires `T : ISpanFormattable`. | [string]: path with an escaped invariant formatted replacement. |
| `BuildRelativeUri(HttpClient client, string relativePath, UrlResolutionMode urlResolution)` | Combines a route with the client base path under the selected resolution rule. | [HttpClient] `client`: supplies the base path; [string] `relativePath`: route; [UrlResolutionMode] `urlResolution`: resolution rule. | [Uri]: relative URI for [`HttpClient`](https://learn.microsoft.com/dotnet/api/system.net.http.httpclient) to resolve. |
| `BuildRelativeUri(HttpClient client, string relativePath, UrlResolutionMode urlResolution, UriFormat queryUriFormat)` | Builds a relative URI and applies the legacy query rendering mode when relevant. | [HttpClient] `client`; [string] `relativePath`; [UrlResolutionMode] `urlResolution`; [UriFormat] `queryUriFormat`: legacy path/query escaping rule. | [Uri]: relative URI. RFC resolution ignores `queryUriFormat`. |
| `RequireAbsoluteUrl(object? url)` | Rejects a URL value that is absent or not absolute. | [object] `url`: a [string] or [Uri] with an absolute address. | [string]: original URL text. Throws `ArgumentException` if it cannot be parsed as absolute. This does not enforce HTTP/HTTPS. |
| `RoundTripEscapePath(string? value, RefitSettings settings, ICustomAttributeProvider attributeProvider, Type type)` | Formats and escapes a catch-all path without escaping its separators. | [string] `value`: catch-all path or `null`; [RefitSettings] `settings`; [ICustomAttributeProvider] `attributeProvider`: formatting attributes; [Type] `type`: declared value type. | [string]: formatted and escaped path sections with `/` separators retained. |
| `FormatUrlParameter(RefitSettings settings, object? value, ICustomAttributeProvider attributeProvider, Type type)` | Formats one value through the registered or default URL formatter. | [RefitSettings] `settings`; [object] `value`: value or `null`; [ICustomAttributeProvider] `attributeProvider`: attributes; [Type] `type`: declared type. | [string], nullable: result from the selected URL formatter. |
| `FormatInvariant<T>(T value, string? format)` | Renders an `IFormattable` using invariant culture without URL escaping. | `value`: an [IFormattable]; [string] `format`: format or `null`. Requires `T : IFormattable`. | [string]: invariant formatted value without URL escaping. |
| `BuildQueryKey(RefitSettings settings, string clrName, string? explicitName, string? prefixSegment)` | Builds the final query key from an alias or formatted CLR name and optional prefix. | [RefitSettings] `settings`; [string] `clrName`: declared name; [string] `explicitName`: alias or `null`; [string] `prefixSegment`: prefix including delimiter, or `null`. | [string]: explicit or formatted name with the prefix prepended. |
| `UsesDefaultUrlParameterFormatting(RefitSettings settings)` | Checks whether URL values can use the built-in formatter fast path. | [RefitSettings] `settings`: formatters to inspect. | [bool]: whether inline URL formatting matches the pristine default formatter and the formatter map is empty. |
| `UsesDefaultFormUrlEncodedParameterFormatting(RefitSettings settings)` | Checks whether form values use the exact built-in formatter type. | [RefitSettings] `settings`: formatter to inspect. | [bool]: whether the form formatter has the exact built-in default type. |
| `UsesDefaultUrlParameterKeyFormatting(RefitSettings settings)` | Checks whether query keys use the exact built-in key formatter type. | [RefitSettings] `settings`: formatter to inspect. | [bool]: whether the key formatter has the exact built-in default type. |
| `AddFormattedCollectionProperty(ref GeneratedQueryStringBuilder builder, RefitSettings settings, IEnumerable? values, string key, CollectionFormat collectionFormat, bool preEncoded, (Type ElementProviderType, ICustomAttributeProvider JoinedProvider, Type JoinedType) formatting)` | Formats and appends a collection-valued query property using the configured collection rule. | [GeneratedQueryStringBuilder] `builder`: updated by reference; [RefitSettings] `settings`; [IEnumerable] `values`: collection or `null`; [string] `key`; [CollectionFormat] `collectionFormat`; [bool] `preEncoded`; [tuple] `formatting`: element [Type], joined-value [ICustomAttributeProvider], and joined [Type]. | `void`; appends values using the two formatting passes described in [query building](query-builder.md). Null appends nothing. |

## Header and option overloads

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `SetHeader(HttpRequestMessage request, string name, string? value, bool validateHeaders)` | Replaces one request header and optionally validates its syntax. | [HttpRequestMessage] `request`; [string] `name`: header name; [string] `value`: replacement or `null`; [bool] `validateHeaders`: whether to validate header syntax. | `void`; replaces the header, or removes it for `null`. |
| `AddHeaderCollection(HttpRequestMessage request, IDictionary<string, string>? headers, bool validateHeaders)` | Applies a collection of header replacements to the request. | [HttpRequestMessage] `request`; [`IDictionary<string, string>`][dictionary] `headers`: replacements or `null`; [bool] `validateHeaders`: whether to validate syntax. | `void`; applies `SetHeader` to each entry. Null does nothing. |
| `AddConfiguredRequestOptions(HttpRequestMessage request, RefitSettings settings, Type interfaceType)` | Copies configured request options and HTTP version settings onto a request. | [HttpRequestMessage] `request`; [RefitSettings] `settings`: options and version rules; [Type] `interfaceType`: Refit interface. | `void`; stores request options and interface type, plus HTTP version settings on modern .NET. |
| `AddRequestProperty<TValue>(HttpRequestMessage request, string key, TValue value)` | Stores one typed request option for later request execution. | [HttpRequestMessage] `request`; [string] `key`: option key; `value`: option value. | `void`; sets a typed option, or a dictionary entry on .NET Framework. |
| `SetRequestJsonTypeInfo<T>(HttpRequestMessage request, JsonTypeInfo<T>? typeInfo)` | Stores the metadata that describes the response body, so the reply is read with it. Generated code calls it for a method that has a `JsonTypeInfo<T>` parameter. | [HttpRequestMessage] `request`; [`JsonTypeInfo<T>`](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo-1) `typeInfo`: metadata for the reply body, or `null` to store nothing. .NET 8 and later. | `void`; stores the metadata as a request option. |
| `SetRequestTimeout(HttpRequestMessage request, int timeoutMilliseconds)` | Records the per-request timeout for the send helper to apply. | [HttpRequestMessage] `request`; [int] `timeoutMilliseconds`: timeout in milliseconds. | `void`; stores a timeout for dispatch to apply. |

## Body helper overloads

`TBody` is the declared body type. The URL-encoded overloads require its public properties
to survive trimming when they use reflection. Read [AOT guidance](../aot.md) before using them in a native app.

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `CreateBodyContent<TBody>(RefitSettings settings, TBody body, BodySerializationMethod serializationMethod, bool streamBody)` | Serializes a request body according to the selected body mode, preserving supplied content and streams. | [RefitSettings] `settings`; `body`: value to send; [BodySerializationMethod] `serializationMethod`; [bool] `streamBody`: whether serialized content streams. | [HttpContent]: existing content, protected stream content, raw text, or serialized body as described above. |
| `CreateBodyContent<TBody>(RefitSettings settings, TBody body, JsonTypeInfo<TBody>? typeInfo, BodySerializationMethod serializationMethod, bool streamBody)` | Serializes a request body with the metadata a method parameter supplies. It follows the same body rules as the overload above. | [RefitSettings] `settings`; `body`: value to send; [`JsonTypeInfo<TBody>`](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo-1) `typeInfo`: metadata for `TBody`, or `null` to use the serializer's own lookup; [BodySerializationMethod] `serializationMethod`; [bool] `streamBody`. | [HttpContent]: as above, with the body written through `IJsonTypeInfoContentSerializer` when `typeInfo` is set. Throws `InvalidOperationException` when the serializer does not implement it. .NET 8 and later. |
| `CreateJsonLinesBodyContent<TBody>(RefitSettings settings, TBody body)` | Creates newline-delimited JSON content from one value or an enumerable body. | [RefitSettings] `settings`; `body`: one value or a sequence of values. | [HttpContent]: JSON Lines content, or existing content/stream handling. |
| `CreateTypedJsonLinesBodyContent<TElement>(RefitSettings settings, IEnumerable<TElement>? body)` | Generator-facing. Creates JSON Lines content for a body declared as `IEnumerable<TElement>`. | [RefitSettings] `settings`; `body`: the sequence, or `null`. | [HttpContent]: writes each element as `TElement` when that gives the same JSON as `CreateJsonLinesBodyContent`, which is when `TElement` has no derived types and the serializer is the built-in System.Text.Json one. Otherwise it returns exactly what `CreateJsonLinesBodyContent` returns. |
| `CreateAsyncJsonLinesBodyContent<TElement>(RefitSettings settings, IAsyncEnumerable<TElement>? body)` | Generator-facing. Creates JSON Lines content for a body declared as `IAsyncEnumerable<TElement>`. | [RefitSettings] `settings`; `body`: the asynchronous sequence, or `null`. | [HttpContent]: single-use content that writes each element as `TElement` as the producer yields it, ending every line with a line feed. A `null` body is written as one `null` line. |
| `CreateStreamContent(Stream stream)` | Wraps a caller-owned stream without taking ownership of that stream. | [Stream] `stream`: caller-owned body stream. | [HttpContent]: wrapper that leaves the stream open when disposed. |
| `CreateUrlEncodedBodyContent<TBody>(RefitSettings settings, TBody body)` | Converts a body to URL-encoded form content, with special handling for existing content, streams and strings. | [RefitSettings] `settings`; `body`: form object, dictionary, string, content or stream. | [HttpContent]: URL-encoded form or existing content/stream handling. Object flattening uses reflection. |
| `CreateUrlEncodedBodyContent<TBody>(RefitSettings settings, TBody body, FormField<TBody>[] fields)` | Converts a body to URL-encoded form content using generated field descriptors when supported. | [RefitSettings] `settings`; `body`: form value; `fields`: [form descriptors](#form-field-reference) with direct getters. | [HttpContent]: form content using eligible descriptors, otherwise the reflection path described above. |
| `CanUnrollForm(object? body)` | Checks whether a body can use the generated property-by-property form path. | [object] `body`: candidate form value, or `null`. | [bool]: `true` for non-null values other than strings, streams, HTTP content and dictionaries. |
| `SerializeMultipartPart<T>(RefitSettings settings, T value, string fieldName)` | Serializes one multipart value with the configured content serializer. | [RefitSettings] `settings`; `value`: one part; [string] `fieldName`: name used in an error. | [HttpContent]: serialized part. Serializer failures become `ArgumentException`. |
| `CompressBodyContent(HttpContent content, RefitSettings settings, RequestCompression compression, CompressionLevel level)` | Applies the resolved request compression setting to HTTP content. | [HttpContent] `content`: input; [RefitSettings] `settings`: defaults/options; [RequestCompression] `compression`: coding; [CompressionLevel] `level`: effort for explicit coding. | [HttpContent]: owning compression wrapper, or the same content when no coding applies. |

## Dispatch overloads

`T` is the caller's result type. `TBody` is the body type inside an API response wrapper.
For each dispatch, supply an [HttpClient] with `BaseAddress` set and the client's [RefitSettings].
The shared flags have these meanings:

| Parameter | Type | Value |
| --- | --- | --- |
| `isApiResponse` | [bool] | `true` when `T` is a supported [response wrapper](../results/responses.md). |
| `shouldDisposeResponse` | [bool] | `true` for a fully consumed result, including an `ApiResponse<T>`. Use `false` when returning `HttpResponseMessage`, `HttpContent` or `Stream`. |
| `bufferBody` | [bool] | Whether to buffer request content before sending. |

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `SendVoidAsync(HttpClient client, HttpRequestMessage request, RefitSettings settings, bool bufferBody, CancellationToken cancellationToken)` | Sends a request whose successful result has no response body. | [HttpClient] `client`; [HttpRequestMessage] `request`: message to send; [RefitSettings] `settings`; [bool] `bufferBody`: flag above; [CancellationToken] `cancellationToken`: request cancellation. | [Task]: completion without a result. Disposes the request and response. |
| `SendAsync<T, TBody>(HttpClient client, HttpRequestMessage request, RefitSettings settings, bool isApiResponse, bool shouldDisposeResponse, bool bufferBody, CancellationToken cancellationToken)` | Sends a request and processes its response as a deserialized value or API response wrapper. | [HttpClient] `client`; [HttpRequestMessage] `request`; [RefitSettings] `settings`; three [bool] flags above; [CancellationToken] `cancellationToken`: request cancellation. | [`Task<T?>`][task-result]: deserialized, raw, or wrapped result. Disposes the request. Response ownership follows the flag. |
| `SendObservable<T, TBody>(HttpClient client, Func<HttpRequestMessage> requestFactory, RefitSettings settings, bool isApiResponse, bool shouldDisposeResponse, bool bufferBody, CancellationToken methodCancellationToken)` | Creates a cold observable that builds and sends a fresh request for each subscription. | [HttpClient] `client`; [`Func<HttpRequestMessage>`][factory] `requestFactory`: creates a fresh message per subscription; [RefitSettings] `settings`; three [bool] flags above; [CancellationToken] `methodCancellationToken`: caller cancellation. | [`IObservable<T?>`][observable]: sends one request per subscription and delivers its result or error. See [observable replies](../results/return-types.md#querying-a-reply). |
| `StreamAsync<T>(HttpClient client, HttpRequestMessage request, RefitSettings settings, CancellationToken methodCancellationToken, CancellationToken cancellationToken = default)` | Sends a request and exposes the response body as an asynchronous stream. | [HttpClient] `client`; [HttpRequestMessage] `request`: one message; [RefitSettings] `settings`; [CancellationToken] `methodCancellationToken`: caller token; [CancellationToken] `cancellationToken`: enumeration token, default non-cancelable. | [`IAsyncEnumerable<T?>`][async-enumerable]: one streaming response. Enumeration/disposal releases its request, response and stream. |

## Form field reference

`FormField<TBody>` stores a getter and formatting rules for one field. Its properties are
read-only and retain the constructor arguments. None of the arguments has a default.

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `FormField(Func<TBody, object?> getter, string clrName, string? explicitName, string? prefixSegment, string? format, CollectionFormat? collectionFormat, bool serializeNull)` | Creates a descriptor that reads and formats one URL-encoded form field. | [`Func<TBody, object?>`][getter] `getter`: reads a field; [string] `clrName`: declared name; nullable [string] arguments: explicit name, prefix with delimiter and value format; nullable [CollectionFormat] `collectionFormat`: override or settings default; [bool] `serializeNull`: whether null emits an empty field. | A [`FormField<TBody>`](https://github.com/reactiveui/refit/blob/main/src/Refit/FormField.cs) descriptor. |
| `ResolveFieldName(IUrlParameterKeyFormatter urlParameterKeyFormatter)` | Resolves the final form key from the explicit name or configured key formatter. | [IUrlParameterKeyFormatter] `urlParameterKeyFormatter`: formats `ClrName` when no explicit name is set. | [string], nullable: resolved name with the prefix prepended. |

| Property | Type | Value |
| --- | --- | --- |
| `Getter` | [`Func<TBody, object?>`][getter] | Reads the field value from a body instance. |
| `ClrName` | [string] | Declared property name. |
| `ExplicitName` | [string], nullable | Alias or serializer name; `null` uses the key formatter. |
| `PrefixSegment` | [string], nullable | Prefix including delimiter; `null` adds none. |
| `Format` | [string], nullable | Value format; `null` uses default formatting. |
| `CollectionFormat` | [CollectionFormat], nullable | Explicit collection rule; `null` uses settings. |
| `SerializeNull` | [bool] | `true` emits an empty field for null; `false` omits it. |

| `UrlResolutionMode` value | Numeric value | Meaning |
| --- | --- | --- |
| `RefitLegacy` | `0` | Prefix the base-address path and require a leading slash. |
| `Rfc3986` | `1` | Use standard URI resolution. See [URL settings](../clients/settings.md#url-resolution). |

[string]: https://learn.microsoft.com/dotnet/api/system.string
[bool]: https://learn.microsoft.com/dotnet/api/system.boolean
[int]: https://learn.microsoft.com/dotnet/api/system.int32
[object]: https://learn.microsoft.com/dotnet/api/system.object
[Type]: https://learn.microsoft.com/dotnet/api/system.type
[tuple]: https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/value-tuples
[span]: https://learn.microsoft.com/dotnet/api/system.readonlyspan-1
[ISpanFormattable]: https://learn.microsoft.com/dotnet/api/system.ispanformattable
[IFormattable]: https://learn.microsoft.com/dotnet/api/system.iformattable
[ICustomAttributeProvider]: https://learn.microsoft.com/dotnet/api/system.reflection.icustomattributeprovider
[IEnumerable]: https://learn.microsoft.com/dotnet/api/system.collections.ienumerable
[dictionary]: https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2
[Uri]: https://learn.microsoft.com/dotnet/api/system.uri
[UriFormat]: https://learn.microsoft.com/dotnet/api/system.uriformat
[HttpClient]: https://learn.microsoft.com/dotnet/api/system.net.http.httpclient
[HttpRequestMessage]: https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage
[HttpContent]: https://learn.microsoft.com/dotnet/api/system.net.http.httpcontent
[Stream]: https://learn.microsoft.com/dotnet/api/system.io.stream
[CompressionLevel]: https://learn.microsoft.com/dotnet/api/system.io.compression.compressionlevel
[CancellationToken]: https://learn.microsoft.com/dotnet/api/system.threading.cancellationtoken
[Task]: https://learn.microsoft.com/dotnet/api/system.threading.tasks.task
[task-result]: https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1
[factory]: https://learn.microsoft.com/dotnet/api/system.func-1
[getter]: https://learn.microsoft.com/dotnet/api/system.func-2
[observable]: https://learn.microsoft.com/dotnet/api/system.iobservable-1
[async-enumerable]: https://learn.microsoft.com/dotnet/api/system.collections.generic.iasyncenumerable-1
[RefitSettings]: ../clients/settings.md
[UrlResolutionMode]: ../clients/settings.md#url-resolution
[GeneratedQueryStringBuilder]: query-builder.md
[CollectionFormat]: ../requests/queries.md
[BodySerializationMethod]: ../requests/bodies.md
[RequestCompression]: ../requests/bodies.md
[IUrlParameterKeyFormatter]: ../requests/query-formatters.md

Source: [path/header helpers](https://github.com/reactiveui/refit/blob/main/src/Refit/GeneratedRequestRunner.cs),
[body helpers](https://github.com/reactiveui/refit/blob/main/src/Refit/GeneratedRequestRunner.BodyContent.cs),
[dispatch entry points](https://github.com/reactiveui/refit/blob/main/src/Refit/GeneratedRequestRunner.Sending.cs),
[shared execution](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestExecutionHelpers.cs),
[streaming execution](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestExecutionHelpers.Streaming.cs),
[form descriptors](https://github.com/reactiveui/refit/blob/main/src/Refit/FormField.cs),
and [form flattening](https://github.com/reactiveui/refit/blob/main/src/Refit/FormValueMultimap.cs).
