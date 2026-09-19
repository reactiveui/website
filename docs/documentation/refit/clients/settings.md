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
The factory returns an exception for that envelope or `null` when Refit should continue its normal
failure handling.

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

The table lists all properties on the .NET 10 baseline.
Related pages explain the behavior and show request examples.

| Property | Default | What it controls |
| --- | --- | --- |
| `ContentSerializer` | System.Text.Json serializer | Body writing and reply reading. Prefer [generated JSON metadata](../serialization/json.md). |
| `AuthorizationHeaderValueGetter` | Null | Obtains a token for a declared authorization header. The [installed handler](dependency-injection.md#authorization-getters-and-handlers) has additional behavior. |
| `HttpMessageHandlerFactory` | Null | Creates the inner transport when Refit or its factory integration constructs the client. A supplied plain `HttpClient` keeps its own pipeline. |
| `ExceptionFactory` | Default API exception factory | Returns `ValueTask<Exception?>` for a reply. Null suppresses that HTTP error exception. See [error handling](../results/errors.md). |
| `DeserializationExceptionFactory` | Null | Translates a reply-reading exception; a null result suppresses it. See [error handling](../results/errors.md). |
| `TransportExceptionFactory` | Default request exception factory | Translates send failures. Caller cancellation passes through; other failures become `ApiRequestException`. |
| `ExceptionRedactor` | Null | Scrubs exception request/reply details before propagation. |
| `MaxExceptionContentLength` | Null | Caps error-body capture in characters. Null permits an unbounded read. |
| `CaptureRequestContent` | False | Reads the request body into memory before sending and retains its text for exception context. Avoid large uploads and private data in logs. |
| `CaptureMethodArguments` | False | Retains a boxed argument array in declaration order, including cancellation tokens. See [request context](../requests/request-context.md). |
| `HttpRequestMessageOptions` | Null | Init-only dictionary of local request options. Its values are copied to each request; they are not transmitted. |
| `UrlParameterKeyFormatter` | Default key formatter | Changes URL and form keys where no explicit name wins. |
| `HonorContentSerializerPropertyNamesInQuery` | True | Honors explicit serializer field names for flattened query properties. `AliasAs` wins; false uses aliases and the key formatter. |
| `UrlParameterFormatter` | Default value formatter | Formats path and query values. |
| `UrlParameterFormatterMap` | Empty dictionary | Overrides URL value formatting by exact runtime type. It does not walk base types or interfaces, or affect headers/body values. |
| `FormUrlEncodedParameterFormatter` | Default form formatter | Formats form values and property-specific formats used by query object flattening. |
| `CollectionFormat` | `RefitParameterFormatter` | Selects default collection rendering. See [query collections](../requests/queries.md). |
| `Buffered` | False | Loads request content into a buffer before sending unless the body attribute overrides it. |
| `RequestBodySerialization` | `Default` | Chooses ordinary, buffered or streamed JSON writing. The latter two require `ISynchronousContentSerializer`. |
| `RequestCompression` | `None` | Selects a request-body coding unless the body attribute overrides it. The server must accept that coding. |
| `RequestCompressionLevel` | `Optimal` | Selects compression effort for settings-selected coding. An explicit body coding uses the body's level. |
| `RequestCompressionOptions` | Null | Sets per-coding options that override the level for that coding. .NET 9 or later. |
| `AllowUnmatchedRouteParameters` | False | Allows unresolved route placeholders for later handler rewriting. Otherwise building the method/request fails. |
| `ValidateHeaders` | False | Enables framework header parsing. Rejected values can throw `FormatException`; both modes strip CR/LF. |
| `UrlResolution` | `RefitLegacy` | Selects legacy base-path prepending or RFC URI resolution. |
| `ReturnTypeAdapters` | Empty list | Registers return adapters for the reflection builder. The source generator discovers adapters at compile time. |
| `Version` | HTTP/1.1 | Sets the outgoing request's HTTP version. |
| `VersionPolicy` | `RequestVersionOrLower` | Allows the requested version or a lower version when the transport negotiates. |

`ReturnTypeAdapters` and `UrlParameterFormatterMap` expose mutable collections through read-only properties.
Registering a formatter map entry makes generated calls use the formatter route instead of the built-in formatting branch.
`HttpRequestMessageOptions` is init-only, but its dictionary remains mutable.
Configure all three before calls begin.

[Request bodies](../requests/bodies.md) explains buffering, compression and body ownership.
[Headers](../requests/headers.md) explains validation and token declarations.
.NET Framework settings omit `Version`, `VersionPolicy` and compression options.
.NET 8 includes HTTP version settings but omits compression options.
