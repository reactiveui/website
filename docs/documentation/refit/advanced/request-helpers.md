---
Order: 2
---
# Generated request helpers

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/advanced-request-helpers/advanced-request-helpers.csproj).

Behind a Refit interface call are several steps: filling in the URL, adding headers, writing
the body, sending the request and reading the reply. The generated client uses
`GeneratedRequestRunner` to carry out many of those steps.

These helpers are useful when you are building your own client infrastructure and need that
control. Calling them directly also means choosing the formatting, cancellation and ownership
rules that the generator normally chooses from your interface.

## Build a path

The [complete .NET 10 / C# 14 sample](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/RuntimeHelpers)
references local Refit source and runs against a local HTTP handler. It uses generated JSON
metadata and direct form getters. It also includes a separate reflected metadata example;
the combined sample project does not claim Native AOT support.

`BuildRequestPath` takes placeholder ranges with an inclusive start and exclusive end.
Generated code computes these positions at compile time. Keep the ranges ordered and
non-overlapping, and include the braces in each range. The string-value span overload
escapes each replacement. Its overload with a `PreEncoded` flag appends flagged values
verbatim. A null replacement for an optional `{name?}` removes its preceding `/`;
a plain `{name}` with a null value leaves an empty segment.
The parameterless overload checks a template without replacements. Any unresolved
placeholder throws `ArgumentException` unless `allowUnmatchedParameter` is true.

The generic overload without a format appends an invariant formatted span without escaping
when it fits its buffer. The generator uses it only for unformatted integers, whose digits
and optional minus sign are safe in a URL. Do not use that fast path for arbitrary
`ISpanFormattable` values. The generic overload with a format escapes the rendered value
and supports other span-formattable values.

`RoundTripEscapePath` preserves the `/` separators of a catch-all route value, formatting
and escaping its sections. Its result is already escaped; insert it with `PreEncoded = true`
to avoid escaping the percent signs again. `RequireAbsoluteUrl` accepts a string or `Uri`,
returns its absolute URL text, and rejects null, empty, or relative values with
`ArgumentException`. A `Uri` contributes its `OriginalString`.

The current validation checks `UriKind.Absolute`, rather than requiring an HTTP or HTTPS
scheme. On Linux, `RequireAbsoluteUrl("/items")` returns `"/items"`, which .NET accepts
as a file URI. A generated `[Url]` request can consequently reach HTTP dispatch with an
unsupported scheme instead of failing this helper's argument check. The complete sample
asserts this limitation; supply an explicit HTTP or HTTPS URL.

```csharp
const string template = "/items/{id}";
const string itemsPath = "/items";
(int StartIdx, int EndIdx) range = (SampleValues.PlaceholderStart, SampleValues.PlaceholderEnd);
string escaped = GeneratedRequestRunner.BuildRequestPath(template, false, [(range, "a/b")]);
string encoded = GeneratedRequestRunner.BuildRequestPath(template, false, [(range, "a%2Fb", true)]);
string integer = GeneratedRequestRunner.BuildRequestPath(template, false, range, SampleValues.Identifier);
string formatted = GeneratedRequestRunner.BuildRequestPath(template, false, range, SampleValues.Identifier, "D3");
string unchanged = GeneratedRequestRunner.BuildRequestPath(itemsPath, false);
string catchAll = GeneratedRequestRunner.RoundTripEscapePath("a b/c", settings, GeneratedParameterAttributeProvider.Empty, typeof(string));
string absolute = GeneratedRequestRunner.RequireAbsoluteUrl(new Uri(AbsoluteUrl));
```

Here `settings` is a `RefitSettings` and `AbsoluteUrl` is `"https://example.test/items"`.
Both string span calls produce `/items/a%2Fb`.
The integer calls produce `/items/42` and `/items/042`; the catch-all fragment is `a%20b/c`.
The no-format generic overload is compiled under `NET6_0_OR_GREATER`, and its formatted
counterpart under `NET8_0_OR_GREATER`. Both are present on Refit's .NET 8 and later targets
and absent on its .NET Framework targets.

`BuildRelativeUri` returns a relative `Uri`, not the final absolute request address.
With `UrlResolutionMode.RefitLegacy`, it requires a leading slash and prefixes the client's
base-address path, trimming that base path's trailing slash. A missing base address throws
`InvalidOperationException`. With `Rfc3986`, it leaves the relative path for `HttpClient`
to resolve. The overload taking `UriFormat` re-encodes the full path and query in legacy
mode; RFC mode ignores that argument. [Query building and formatting](query-builder.md)
covers `BuildQueryKey`, `FormatInvariant`, `FormatUrlParameter`, the three default-formatter
guards, and `AddFormattedCollectionProperty`.

The sample uses a shared `Client` whose base address is `https://example.test/api/`.
With `itemsPath = "/items"`, legacy resolution yields `/api/items`.
`ItemSegment` is `"items"`.

```csharp
Uri legacy = GeneratedRequestRunner.BuildRelativeUri(Client, itemsPath, UrlResolutionMode.RefitLegacy);
Uri rfc = GeneratedRequestRunner.BuildRelativeUri(Client, ItemSegment, UrlResolutionMode.Rfc3986, UriFormat.Unescaped);
Uri unescaped = GeneratedRequestRunner.BuildRelativeUri(Client, "/items?q=a%20b", UrlResolutionMode.RefitLegacy, UriFormat.Unescaped);
```

## Set headers and request options

`SetHeader` removes an earlier request or content header with the same name, then adds
the new value. Null removes a header without adding one. On a method that accepts a body,
it can create empty content so a content header has a place to live. It strips CR and LF
from the supplied name and value. With `validateHeaders: true`, malformed values can throw
`FormatException`; otherwise it uses the headers' `TryAddWithoutValidation` path.
`AddHeaderCollection` applies the same rules to each dictionary entry; a null dictionary
does nothing. Later values replace earlier values by key.

`AddConfiguredRequestOptions` applies the settings' request options and interface type.
On .NET 8 and later it also applies the configured HTTP version and version policy.
`AddRequestProperty<TValue>` sets a typed `HttpRequestMessage.Options` value on those
targets; .NET Framework uses the request's `Properties` dictionary.
`SetRequestTimeout` stores the per-call milliseconds for the sending helpers. A positive
value applies a timeout in addition to cancellation. Zero or a negative value disables
this timeout; storing the option does not start a timer or send a request.

```csharp
const string modeHeader = "X-Mode";
const string removedHeader = "X-Remove";
using HttpRequestMessage request = new(HttpMethod.Post, itemsPath);
GeneratedRequestRunner.SetHeader(request, modeHeader, "old", validateHeaders: true);
GeneratedRequestRunner.AddHeaderCollection(request, new Dictionary<string, string> { [modeHeader] = "new" }, validateHeaders: true);
GeneratedRequestRunner.SetHeader(request, removedHeader, "remove me", validateHeaders: false);
GeneratedRequestRunner.SetHeader(request, removedHeader, null, validateHeaders: false);
GeneratedRequestRunner.AddHeaderCollection(request, null, validateHeaders: true);
GeneratedRequestRunner.AddConfiguredRequestOptions(request, settings, typeof(IHelperApi));
GeneratedRequestRunner.AddRequestProperty(request, "TraceId", SampleValues.Identifier);
GeneratedRequestRunner.SetRequestTimeout(request, SampleValues.TimeoutMilliseconds);
```

## Create body content

`CreateBodyContent<TBody>` returns an existing `HttpContent` unchanged and wraps a
`Stream` with `CreateStreamContent`. With `BodySerializationMethod.Default`, a string is
sent as raw text. Other values, or the `Serialized` mode, use the configured content
serializer. For ordinary values, this method supports `Default` and `Serialized`
(and the retained obsolete `Json` value). Other modes throw `ArgumentOutOfRangeException`;
generated code calls the separate URL-encoded or JSON Lines helpers for those modes.
`streamBody: true` writes serialized content through a streaming wrapper unless the settings
choose synchronous serialization, which already creates a buffer.

`CreateJsonLinesBodyContent<TBody>` serializes each element of an enumerable as a JSON
value, with a newline between values and no trailing newline. A string is treated as a single value, rather than an enumerable
of characters. Existing content and streams pass through as with other body helpers.
`SerializeMultipartPart<T>` serializes one part through the configured serializer; it does
not create a multipart container. A serializer failure is wrapped in `ArgumentException`
with the field name and original exception.

`CreateStreamContent` leaves the caller's stream open when the content is disposed.
The caller still owns and must dispose that stream. Existing content returned unchanged
does not acquire this special stream protection.

`CompressBodyContent` resolves `RequestCompression.Default` from the settings.
Explicit `None` returns the same content. An explicit coding also selects the supplied
compression level, while the default uses the settings' level. Compressor options in the
settings can override level-based construction. The returned compression content owns its
inner content; dispose the returned wrapper.

```csharp
using HttpContent raw = GeneratedRequestRunner.CreateBodyContent(settings, "plain text", BodySerializationMethod.Default, streamBody: false);
using HttpContent json = GeneratedRequestRunner.CreateBodyContent(settings, SampleValues.Count, BodySerializationMethod.Serialized, streamBody: true);
using HttpContent lines = GeneratedRequestRunner.CreateJsonLinesBodyContent(settings, SampleValues.Items);
using HttpContent multipartPart = GeneratedRequestRunner.SerializeMultipartPart(settings, SampleValues.Count, nameof(count));
```

The compression example wraps its input content:

```csharp
const string compressionText = "compress me";
using HttpContent gzip = GeneratedRequestRunner.CompressBodyContent(new StringContent(compressionText), settings, RequestCompression.GZip, CompressionLevel.Fastest);
```

Add `using System.IO.Compression;`. The full sample's `HelperJsonContext` supplies
generated JSON metadata for the integer values. The body checks expect `12` for JSON and
`1\n2` for JSON Lines. GZip is available on all Refit targets, Brotli on .NET 8 and later,
and Zstandard on .NET 11 and later. Requesting an unavailable coding throws
`PlatformNotSupportedException`. The .NET 10 sample checks this exception for Zstandard;
it does not verify successful .NET 11 Zstandard compression or its options.

## Supply form descriptors

`CreateUrlEncodedBodyContent<TBody>(settings, body)` flattens form values using the
declared body's public properties or dictionary entries. A string is escaped as one entire
value, so `a=b` becomes `a%3Db`; it is not parsed as an already encoded form.
Existing content and streams pass through. Ordinary object flattening in this overload
uses reflected metadata.

The overload taking `FormField<TBody>[]` can use direct getters instead. That descriptor
path applies only to a non-null object that is not a dictionary and a configured
`SystemTextJsonContentSerializer`. Other serializer types can need their property-name
hook and fall back to reflected flattening. Nested complex form values can also require
runtime property traversal. Direct getters illustrate how generated code avoids discovery
for known simple fields; the descriptors alone are not a guarantee for every body shape.

`FormField<TBody>` stores the `Getter`, `ClrName`, `ExplicitName`, `PrefixSegment`,
`Format`, `CollectionFormat`, and `SerializeNull` supplied to its constructor. These are
read-only properties. `ResolveFieldName` uses `ExplicitName` when present; otherwise it
formats `ClrName` with the supplied key formatter, then prepends the prefix verbatim.
An explicit collection format overrides the settings' default. `SerializeNull` emits an
empty field for a null value; false omits it. The getter reads the field value directly.

The sample's `FormBody` has `Count = 12` and a null `Note`.

```csharp
FormBody body = new();
const string formPrefix = "form.";
FormField<FormBody> count = new(static value => value.Count, nameof(FormBody.Count), nameof(count), formPrefix, "D3", null, false);
FormField<FormBody> note = new(static value => value.Note, nameof(FormBody.Note), "note", null, null, CollectionFormat.Csv, true);
FormField<FormBody>[] fields = [count, note];
using HttpContent form = GeneratedRequestRunner.CreateUrlEncodedBodyContent(settings, body, fields);
string formText = await form.ReadAsStringAsync();
string? fieldName = count.ResolveFieldName(settings.UrlParameterKeyFormatter);
```

The result is `form.count=012&note=`. `CanUnrollForm` reports whether the body is a plain
non-null object: it excludes strings, streams, existing content, and dictionaries.
It does not check serializer compatibility or send a request.

## Send a built request

All four dispatch entry points require the client's `BaseAddress`, even when a request
has an absolute URI. They apply the configured authorization getter, exception handling,
and positive per-call timeout. The task entry points dispose the request after dispatch.
The flags are infrastructure contracts; select them to match the return type.

`SendVoidAsync` sends a request with no returned body and disposes the response.
The default exception factory throws on an HTTP error. `SendAsync<T, TBody>` can deserialize
`T`, return raw response/content/stream results, or construct an API response wrapper.
`isApiResponse: true` requires a supported wrapper type for `T`; `TBody` is its body type.
`bufferBody` controls buffering of request content before sending, not response content.

Use `shouldDisposeResponse: true` for a fully consumed value. Use false when returning
a wrapper, `HttpResponseMessage`, `HttpContent`, or response stream whose caller needs
the response to stay open. The caller must dispose the returned owner. For a plain-result
HTTP error, the pipeline can transfer the response to the thrown exception instead.

```csharp
const string itemsPath = "/items";
await GeneratedRequestRunner.SendVoidAsync(client, new(HttpMethod.Get, "/ping"), settings, bufferBody: false, CancellationToken.None);
int result = await GeneratedRequestRunner.SendAsync<int, int>(
    client,
    new(HttpMethod.Get, itemsPath),
    settings,
    isApiResponse: false,
    shouldDisposeResponse: true,
    bufferBody: false,
    CancellationToken.None);
using ApiResponse<int>? wrapped = await GeneratedRequestRunner.SendAsync<ApiResponse<int>, int>(
    client,
    new(HttpMethod.Get, itemsPath),
    settings,
    isApiResponse: true,
    shouldDisposeResponse: false,
    bufferBody: false,
    CancellationToken.None);
```

`SendObservable<T, TBody>` returns a cold observable: each subscription starts a new
request. Its factory must create a fresh message, because each request is disposed after
use. The method token and subscription token are linked when both can cancel.
The same result and ownership flags apply as for `SendAsync`.

Here `ToTask` from `ReactiveUI.Primitives` subscribes for a result and awaits it.
`handler` is the full sample's local HTTP handler. Both subscriptions send a request.

```csharp
IObservable<int> observable = GeneratedRequestRunner.SendObservable<int, int>(
    client,
    static () => new HttpRequestMessage(HttpMethod.Get, itemsPath),
    settings,
    isApiResponse: false,
    shouldDisposeResponse: true,
    bufferBody: false,
    CancellationToken.None);
int first = await observable.ToTask();
int second = await observable.ToTask();
```

`StreamAsync<T>` sends on enumeration and requires an `IStreamingContentSerializer`.
The built-in System.Text.Json serializer supports it. Response media type selects JSON
array, JSON Lines, or server-sent event framing. The sequence disposes the request, response,
and body stream when enumeration finishes or is disposed. It links the method token and
consumer token when both can cancel. A positive request timeout also applies while reading.
Unlike the observable factory, this call captures one request: do not reuse the sequence
for a second enumeration with a disposed request.

```csharp
HttpRequestMessage streamRequest = new(HttpMethod.Get, "/stream");
GeneratedRequestRunner.SetRequestTimeout(streamRequest, SampleValues.TimeoutMilliseconds);
int sum = 0;
await foreach (int item in GeneratedRequestRunner.StreamAsync<int>(client, streamRequest, settings, CancellationToken.None).WithCancellation(CancellationToken.None))
{
    sum += item;
}
```

The local handler returns `[1,2]`, so the sum is `3`. The complete sample also checks the
individual values and order, cold observable dispatch, raw response/content/stream results,
request options, and decompressed GZip and Brotli payloads.
[Method metadata](method-metadata.md) describes the reflected information objects.

Source: [path/header helpers](https://github.com/reactiveui/refit/blob/main/src/Refit/GeneratedRequestRunner.cs),
[body helpers](https://github.com/reactiveui/refit/blob/main/src/Refit/GeneratedRequestRunner.BodyContent.cs),
[dispatch entry points](https://github.com/reactiveui/refit/blob/main/src/Refit/GeneratedRequestRunner.Sending.cs),
[shared execution](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestExecutionHelpers.cs),
[streaming execution](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestExecutionHelpers.Streaming.cs),
[form descriptors](https://github.com/reactiveui/refit/blob/main/src/Refit/FormField.cs),
and [form flattening](https://github.com/reactiveui/refit/blob/main/src/Refit/FormValueMultimap.cs).
