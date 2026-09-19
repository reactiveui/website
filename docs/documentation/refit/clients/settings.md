---
Order: 2
---
# Settings

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/clients-settings/clients-settings.csproj).

Calls to the same service often need the same rules: how to write JSON, format a date,
add an access token, or handle a failed reply. `RefitSettings` keeps those choices in one
place so you can apply them across a client.

Set up these rules before creating the client. Start with the serializer your service needs,
then change the other settings when a request calls for them.

## Set the serializer and formatters

**1. Prepare generated metadata.** Use the context and serializer from
[client creation](creation.md#reuse-your-http-client).

**2. Choose the settings constructor.** The serializer is required in these constructor overloads.
A null formatter selects the default formatter.


```csharp
RefitSettings defaults = new() { ContentSerializer = JsonSettings.ContentSerializer };
RefitSettings serializerOnly = new(JsonSettings.ContentSerializer);
RefitSettings values = new(JsonSettings.ContentSerializer, new DefaultUrlParameterFormatter());
RefitSettings forms = new(JsonSettings.ContentSerializer, null, new DefaultFormUrlEncodedParameterFormatter());
RefitSettings keys = new(JsonSettings.ContentSerializer, null, null, new CamelCaseUrlParameterKeyFormatter());
```

**3. Pass the settings to client creation or registration.** Reuse the configured serializer.
The parameterless constructor creates a System.Text.Json serializer and default formatters.
Assign generated JSON metadata before using that serializer in an AOT app.
A null serializer passed to a constructor throws `ArgumentNullException`.

A production client commonly keeps one `RefitSettings` instance with its generated JSON context
and assigns an asynchronous `ExceptionFactory` when the service has a typed error envelope.
Refit awaits this factory. Keep its asynchronous work asynchronous: do not use `.Result` or
`.Wait()`, which can block a thread and can deadlock code that has a synchronization context.
The factory returns an exception for that envelope or `null` to suppress the HTTP error.

The reflection builder retains the serializer supplied when it is constructed.
Other settings may be read when a request is built.
Changing shared settings during calls does not provide a uniform reconfiguration contract.
Prepare a different settings instance when clients need different rules.

## Align naming rules with generated JSON

`CamelCase()`, `SnakeCase()` and `KebabCase()` return new settings.
They align the JSON naming policy and URL key formatter.
For AOT, replace their serializer with one whose generated context uses the same convention.
These contexts register the demonstration model, `ClientNamingInput(int PageSize)`.


```csharp
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(ClientNamingInput))]
internal sealed partial class ClientCamelJsonContext : JsonSerializerContext;
```


```csharp
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower)]
[JsonSerializable(typeof(ClientNamingInput))]
internal sealed partial class ClientSnakeJsonContext : JsonSerializerContext;
```


```csharp
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.KebabCaseLower)]
[JsonSerializable(typeof(ClientNamingInput))]
internal sealed partial class ClientKebabJsonContext : JsonSerializerContext;
```

The example checks all three shortcuts and writes the model with each generated context.


```csharp
RefitSettings camel = RefitSettings.CamelCase();
RefitSettings snake = RefitSettings.SnakeCase();
RefitSettings kebab = RefitSettings.KebabCase();
RefitSettings[] naming = [camel, snake, kebab];
System.Text.Json.Serialization.JsonSerializerContext[] contexts = [ClientCamelJsonContext.Default, ClientSnakeJsonContext.Default, ClientKebabJsonContext.Default];
for (int index = 0; index < naming.Length; index++)
{
    RefitSettings settings = naming[index];
    System.Text.Json.Serialization.JsonSerializerContext context = contexts[index];
    JsonSerializerOptions options = new(context.Options) { TypeInfoResolver = context };
    settings.ContentSerializer = new SystemTextJsonContentSerializer(options);
    Console.WriteLine(settings.UrlParameterKeyFormatter.Format(NamingKey));
    using HttpContent content = settings.ContentSerializer.ToHttpContent(new ClientNamingInput(NamingPageSize));
    Console.WriteLine(await content.ReadAsStringAsync());
}
```

The key/body names are `pageSize`, `page_size` and `page-size`.
Register each request, reply and container type your real API uses.
See [JSON contexts](../serialization/json.md) for combining and reusing registrations.
An explicit `AliasAs` name wins over a naming rule.
An explicit serializer property name can also control flattened query keys.
See [query formatters](../requests/query-formatters.md).

## URL resolution

`UrlResolution` defaults to `RefitLegacy`.
Legacy mode requires a leading slash and prepends the base address path.
For example, base `https://service.example/api/` and route `/people` produce `/api/people`.

`Rfc3986` uses standard URI resolution.
A leading slash replaces the base path: `/people` produces `https://service.example/people`.
With a base ending in `/api/`, `people` produces `/api/people`.
With a base ending in `/api`, `people` replaces the final path segment and produces `/people`.
Keep the trailing slash when the base path represents a folder to append to.

## Check buffering and unresolved routes

`Buffered` asks Refit to load the request body into memory before passing it to the HTTP handler.
An explicit `[Body(true)]` or `[Body(false)]` overrides the client setting for that argument.
The [policy example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Clients/SettingsPolicies.cs)
checks both client defaults and both attribute overrides. Its handler observes the content length
before reading any bytes:

```csharp
RefitSettings settings = new(serializer) { Buffered = buffer };
ISettingsPolicyApi api = RestService.ForGenerated<ISettingsPolicyApi>(client, settings);
SampleCheck.Equal(BufferingHandler.ReplyText, await api.InheritedAsync(new(1, "Ada")));
SampleCheck.Equal(buffer, handler.LengthBeforeRead.HasValue);
SampleCheck.Equal(BufferingHandler.ReplyText, await api.UnbufferedAsync(new(1, "Ada")));
SampleCheck.Equal(false, handler.LengthBeforeRead.HasValue);
SampleCheck.Equal(BufferingHandler.ReplyText, await api.BufferedAsync(new(1, "Ada")));
SampleCheck.Equal(true, handler.LengthBeforeRead > 0);
```

The same project declares `/policy/{tenant}` without a matching method argument.
With `AllowUnmatchedRouteParameters` false, building that request throws `ArgumentException`.
True retains the placeholder for code that will rewrite it later. It does not supply a tenant value.

```csharp
RefitSettings settings = new(serializer) { AllowUnmatchedRouteParameters = allow };
ISettingsPolicyApi api = RestService.ForGenerated<ISettingsPolicyApi>(client, settings);
```

Configure either setting before creating the client. The complete example reuses one local
HTTP client across the checks and does not contact a server.

## Settings reference

Each row describes one constructor, naming factory, or public property. Nullable constructor arguments use `null` to select the formatter default; they are not optional C# parameters.

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| [`RefitSettings`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Holds the serializer, URL/form formatters, request-building options, exception factories, and HTTP-version settings used by a Refit client. | None. | Mutable settings object. |
| [`RefitSettings()`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates a complete settings object with Refit's default serializer, formatters, and exception factories. | None. | New settings with the System.Text.Json serializer, default URL, form, and key formatters, plus default exception factories. |
| [`RefitSettings(IHttpContentSerializer contentSerializer)`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates settings that use the supplied content serializer and the other defaults. | [`IHttpContentSerializer`](../serialization/json.md) `contentSerializer`: serializer; must not be `null`. | New settings using the supplied serializer and default URL, form, and key formatters. |
| [`RefitSettings(IHttpContentSerializer contentSerializer, IUrlParameterFormatter? urlParameterFormatter)`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates settings with a supplied serializer and URL-value formatter. | [`IHttpContentSerializer`](../serialization/json.md) `contentSerializer`: required serializer; [`IUrlParameterFormatter`](../requests/query-formatters.md) `urlParameterFormatter`: formatter or `null` for the default. | New settings using the supplied choices and the default form and key formatters. |
| [`RefitSettings(IHttpContentSerializer contentSerializer, IUrlParameterFormatter? urlParameterFormatter, IFormUrlEncodedParameterFormatter? formUrlEncodedParameterFormatter)`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates settings with supplied serializer, URL-value, and form-value formatters. | [`IHttpContentSerializer`](../serialization/json.md) `contentSerializer`: required serializer; [`IUrlParameterFormatter`](../requests/query-formatters.md) `urlParameterFormatter`: formatter or `null`; [`IFormUrlEncodedParameterFormatter`](../requests/query-formatters.md) `formUrlEncodedParameterFormatter`: formatter or `null`. | New settings using the supplied choices and the default key formatter. |
| [`RefitSettings(IHttpContentSerializer contentSerializer, IUrlParameterFormatter? urlParameterFormatter, IFormUrlEncodedParameterFormatter? formUrlEncodedParameterFormatter, IUrlParameterKeyFormatter? urlParameterKeyFormatter)`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates settings with supplied serializer and all formatter choices. | [`IHttpContentSerializer`](../serialization/json.md) `contentSerializer`: required serializer; [`IUrlParameterFormatter`](../requests/query-formatters.md) `urlParameterFormatter`: formatter or `null`; [`IFormUrlEncodedParameterFormatter`](../requests/query-formatters.md) `formUrlEncodedParameterFormatter`: formatter or `null`; [`IUrlParameterKeyFormatter`](../requests/query-formatters.md) `urlParameterKeyFormatter`: formatter or `null`. | New settings; a `null` formatter selects its default. |
| [`RefitSettings.CamelCase()`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates settings that serialize JSON and format URL/form keys in camelCase. | None. | New [`RefitSettings`](settings.md) using camelCase JSON and URL/form keys. |
| [`RefitSettings.SnakeCase()`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates settings that serialize JSON and format URL/form keys in snake_case. | None. | New [`RefitSettings`](settings.md) using snake_case JSON and URL/form keys. |
| [`RefitSettings.KebabCase()`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Creates settings that serialize JSON and format URL/form keys in kebab-case. | None. | New [`RefitSettings`](settings.md) using kebab-case JSON and URL/form keys. |
| [`AuthorizationHeaderValueGetter`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Supplies a token for a declared `[Authorize]` header that has no token. Generated preparation uses it even with a supplied `HttpClient`; a settings-created handler also uses it for an explicit token. | [`Func<HttpRequestMessage, CancellationToken, ValueTask<string>>`](https://learn.microsoft.com/dotnet/api/system.func-3) or `null`. | Token getter; default `null`. An empty returned token removes the header. |
| [`HttpMessageHandlerFactory`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Supplies the primary handler when Refit creates the `HttpClient`. | [`Func<HttpMessageHandler>`](https://learn.microsoft.com/dotnet/api/system.func-1) or `null`. | Handler factory; default `null`. Refit ignores it when you supply an existing `HttpClient`. |
| [`ExceptionFactory`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Maps unsuccessful HTTP responses to exceptions. | [`Func<HttpResponseMessage, ValueTask<Exception?>>`](https://learn.microsoft.com/dotnet/api/system.func-2). | Exception factory; default creates Refit API exceptions. A `null` result suppresses the HTTP error. |
| [`DeserializationExceptionFactory`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Maps response-body deserialization failures to exceptions. | [`Func<HttpResponseMessage, Exception, ValueTask<Exception?>>`](https://learn.microsoft.com/dotnet/api/system.func-3) or `null`. | Deserialization exception factory; default `null`. A `null` result suppresses the error. |
| [`ContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Serializes request bodies and deserializes response bodies. | [`IHttpContentSerializer`](../serialization/json.md). | Body/reply serializer; default [`SystemTextJsonContentSerializer`](../serialization/json.md). |
| [`ReturnTypeAdapters`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Registers custom return wrappers for the opt-in reflection request builder, such as `IObservable<T>`. | Read-only [`IList<Type>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist-1) property. | Mutable adapter list; default empty. Reflection builds consult it; source-generated builds discover adapters at compile time. |
| [`UrlParameterKeyFormatter`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Formats parameter names used in route, query, and form data. | [`IUrlParameterKeyFormatter`](../requests/query-formatters.md). | URL/form key formatter; default [`DefaultUrlParameterKeyFormatter`](../requests/query-formatters.md). |
| [`HonorContentSerializerPropertyNamesInQuery`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Chooses whether flattened query names follow serializer property names. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | `true` makes flattened query keys honor serializer names; default `true`. `AliasAs` wins in either mode. |
| [`UrlParameterFormatter`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Formats parameter values inserted into URLs. | [`IUrlParameterFormatter`](../requests/query-formatters.md). | Path/query value formatter; default [`DefaultUrlParameterFormatter`](../requests/query-formatters.md). |
| [`UrlParameterFormatterMap`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Selects URL value formatters by exact runtime type before the general formatter. | Read-only [`IDictionary<Type, IUrlParameterFormatter>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2) property. | Mutable formatter map; default empty. Base classes and interfaces are not searched. |
| [`FormUrlEncodedParameterFormatter`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Formats values written into form-url-encoded request bodies. | [`IFormUrlEncodedParameterFormatter`](../requests/query-formatters.md). | Form value formatter; default [`DefaultFormUrlEncodedParameterFormatter`](../requests/query-formatters.md). |
| [`CollectionFormat`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Selects how collection values become repeated or joined URL parameters. | [`CollectionFormat`](../requests/queries.md). | Collection rendering mode; default `RefitParameterFormatter`. |
| [`Buffered`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Chooses whether request content is buffered before the HTTP send. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | Buffer request content before sending; default `false`. |
| [`CaptureRequestContent`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Captures request-body text so an [`ApiExceptionBase`](../results/responses.md) can expose it after a failed request. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | Retain request-body text in memory; default `false`. Avoid it for large or streamed uploads. |
| [`CaptureMethodArguments`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Stores boxed interface-call arguments in the request options for a handler to inspect. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | Retain an `object?[]` for the request lifetime; default `false`. |
| [`MaxExceptionContentLength`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Limits the response-body characters captured while building an API exception. | [`int?`](https://learn.microsoft.com/dotnet/api/system.nullable-1) characters. | Error-body capture limit; default `null` (unbounded). |
| [`ExceptionRedactor`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Scrubs sensitive data from an [`ApiExceptionBase`](../results/responses.md) before Refit returns it. | [`Action<ApiExceptionBase>`](https://learn.microsoft.com/dotnet/api/system.action-1) or `null`. | Exception scrubbing hook; default `null`. |
| [`AllowUnmatchedRouteParameters`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Allows route placeholders without matching method parameters. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | Leaves unmatched `{token}` text for later rewriting when `true`; default `false`. |
| [`ValidateHeaders`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Enables framework validation when Refit applies declared headers. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean). | Use framework header parsing; default `false`. Invalid values throw `FormatException` when a request is built. |
| [`UrlResolution`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Selects how relative request paths resolve against `HttpClient.BaseAddress`. | [`UrlResolutionMode`](../advanced/request-helpers.md). | Base-address resolution mode; default `RefitLegacy`. |
| [`RequestBodySerialization`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Selects how Refit creates JSON request-body content. | [`RequestBodySerializationMode`](../requests/bodies.md). | JSON body serialization mode; default `Default`. `Buffered` and `Streamed` require [`ISynchronousContentSerializer`](../serialization/json.md). |
| [`RequestCompression`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Selects the content encoding applied to every request body. | [`RequestCompression`](../requests/bodies.md). | Request-body coding; default `None`. A `[Body]` coding overrides this setting. |
| [`RequestCompressionLevel`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Sets the compression effort for compressed request bodies. | [`CompressionLevel`](https://learn.microsoft.com/dotnet/api/system.io.compression.compressionlevel). | Compression effort; default `Optimal`. |
| [`RequestCompressionOptions`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Provides per-coding compressor settings that override the compression level for that coding. | [`RequestCompressionOptions`](../requests/bodies.md) or `null` (.NET 9+). | Per-coding compressor settings; default `null`, which uses the compression level. |
| [`HttpRequestMessageOptions`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Copies these local values to every generated request's options on modern .NET, or properties on .NET Framework. | [`Dictionary<string, object>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.dictionary-2) or `null`; `init` only. | Local request values; default `null`. The dictionary remains mutable after initialization. |
| [`TransportExceptionFactory`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Maps exceptions thrown by `HttpClient.SendAsync` to the exception Refit surfaces. | [`Func<HttpRequestMessage, Exception, CancellationToken, Exception>`](https://learn.microsoft.com/dotnet/api/system.func-4). | Default preserves an `OperationCanceledException` when its token was cancelled; otherwise it wraps the failure in [`ApiRequestException`](../results/responses.md). |
| [`Version`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Sets the HTTP version requested on generated requests. | [`Version`](https://learn.microsoft.com/dotnet/api/system.version) (.NET 6+). | Requested HTTP version; default HTTP/1.1. |
| [`VersionPolicy`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) | Sets the policy used when negotiating the requested HTTP version. | [`HttpVersionPolicy`](https://learn.microsoft.com/dotnet/api/system.net.http.httpversionpolicy) (.NET 6+). | Version negotiation policy; default `RequestVersionOrLower`. |

The enum values used by these properties are:

| `Enum` | Value | Meaning |
| --- | --- | --- |
| [`CollectionFormat`](https://github.com/reactiveui/refit/blob/main/src/Refit/CollectionFormat.cs) | `RefitParameterFormatter` (0) | Use the configured value formatter. |
| `CollectionFormat` | `Csv` (1), `Ssv` (2), `Tsv` (3), `Pipes` (4) | Comma, space, tab, or pipe separated values. |
| `CollectionFormat` | `Multi` (5) | Repeat the parameter for each value. |
| `CollectionFormat` | `Indexed` (6) | Expand object elements with indexed keys. |
| [`RequestBodySerializationMode`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestBodySerializationMode.cs) | `Default` (0) | Normal asynchronous serialization. |
| `RequestBodySerializationMode` | `Buffered` (1) | Synchronous serialization into buffered content. |
| `RequestBodySerializationMode` | `Streamed` (2) | Synchronous serialization to the request stream. |
| [`RequestCompression`](https://github.com/reactiveui/refit/blob/main/src/Refit/RequestCompression.cs) | `Default` (0), `None` (1), `GZip` (2), `Brotli` (3), `Zstandard` (4) | Use settings, no coding, gzip, Brotli, or Zstandard. Brotli requires .NET 8; Zstandard requires .NET 11. |
| [`CompressionLevel`](https://learn.microsoft.com/dotnet/api/system.io.compression.compressionlevel) | `Optimal` (0), `Fastest` (1), `NoCompression` (2), `SmallestSize` (3) | Compression effort choices used by `RequestCompressionLevel`. |
| [`UrlResolutionMode`](https://github.com/reactiveui/refit/blob/main/src/Refit/UrlResolutionMode.cs) | `RefitLegacy` (0), `Rfc3986` (1) | Legacy base-path prepending or RFC 3986 URI resolution. |
| [`System.Net.Http.HttpVersionPolicy`](https://learn.microsoft.com/dotnet/api/system.net.http.httpversionpolicy) | `RequestVersionOrLower` (0), `RequestVersionOrHigher` (1), `RequestVersionExact` (2) | HTTP version negotiation choices. |

Source: [`RefitSettings.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/RefitSettings.cs) at Refit SHA `6f0507fa061f1844a8da6ea92e839b622dfc74ef`.

`ReturnTypeAdapters` and `UrlParameterFormatterMap` expose mutable collections through read-only properties.
Registering a formatter map entry makes generated calls use the formatter route instead of the built-in formatting branch.
`HttpRequestMessageOptions` is init-only, but its dictionary remains mutable.
Configure all three before calls begin.

[Request bodies](../requests/bodies.md) explains buffering, compression and body ownership.
[Headers](../requests/headers.md) explains validation and token declarations.
.NET Framework settings omit `Version`, `VersionPolicy` and compression options.
.NET 8 includes HTTP version settings but omits compression options.
