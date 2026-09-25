---
Order: 4
---
# Error bodies and problem details

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/results-errors/results-errors.csproj).

A failed request can tell your app more than "something went wrong." A service might explain
why a name was rejected or return validation messages you can show beside a form field.
Refit keeps that reply in `ApiException` so your code can read and act on it.

This page shows how to read a typed error body, handle problem details and choose how failures
reach your callers. Register error models alongside successful reply models in your JSON context.

## Read an error body

**1. Declare the server's error model.** This example's server sends a code and a message on status 400.

```csharp
internal sealed record Failure(string Code, string Message);
```

**2. Register success and error shapes.** `ProblemDetails` covers the standard validation format used below.
The [first-request model](../index.md#your-first-request) supplies `Person`.
Include every error model your app deserializes. An AOT build needs these registrations too.

```csharp
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(Failure))]
[JsonSerializable(typeof(ProblemDetails))]
internal sealed partial class ErrorJsonContext : JsonSerializerContext;
```

**3. Reuse options and settings.** Keep one options object in a field, and build `settings` from it.
They keep JSON metadata available without a reflection resolver.

```csharp
private static readonly JsonSerializerOptions Options = new(ErrorJsonContext.Default.Options) { TypeInfoResolver = ErrorJsonContext.Default };
```

```csharp
RefitSettings settings = new(new SystemTextJsonContentSerializer(Options));
```

**4. Declare the calls.** Create the client with
`RestService.ForGenerated<IErrorsApi>(httpClient, settings)`.
Give your `httpClient` the server's base address. The runnable example uses a local message handler.

```csharp
internal interface IErrorsApi
{
    [Get("/failures/rejected")]
    Task<Person> GetRejectedAsync(CancellationToken cancellationToken);

    [Get("/failures/rejected")]
    Task<ApiResponse<Person>> GetRejectedResponseAsync(CancellationToken cancellationToken);

    [Get("/failures/malformed")]
    Task<ApiResponse<Person>> GetMalformedAsync(CancellationToken cancellationToken);

    [Get("/failures/problem")]
    Task<Person> GetProblemAsync(CancellationToken cancellationToken);
}
```

**5. Read the rejected reply.** A `Task<Person>` call throws the default response error.
A wrapped call records that error in `Error` instead.

```csharp
try
{
    await api.GetRejectedAsync(cancellationToken);
}
catch (ApiException error)
{
    Console.WriteLine(error.StatusCode); // BadRequest
    Failure? failure = await error.GetContentAsAsync<Failure>();
    Console.WriteLine(failure?.Code); // name_taken
}
```

`GetContentAsAsync<T>()` reads the buffered text through the configured `IHttpContentSerializer`.
It returns default when `HasContent` is false. Invalid JSON or missing type metadata can throw.
`GetContentAs<T>()` uses `ISynchronousContentDeserializer` and returns default for absent content.
It throws `NotSupportedException` if the configured serializer lacks that capability.
System.Text.Json supports both paths.

Use the synchronous method when your caller must remain synchronous. This helper reads the buffered text:

```csharp
Failure? synchronous = error.GetContentAs<Failure>();
bool read = error.TryGetContentAs(out Failure? attempted);
Console.WriteLine(attempted?.Code); // name_taken
```

`TryGetContentAs<T>(out T?)` returns false for absent content, an unsupported synchronous serializer,
a deserialization error, or a null result. It assigns default on failure.
This method can be used in a C# exception filter, where `await` is unavailable.
It also turns missing JSON metadata into false. Register the model and test that path;
do not treat false as proof that the server sent no error body.

See [the complete error examples](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Errors).

## Shared request details

`ApiExceptionBase` is the abstract base of `ApiRequestException` and `ApiException`.
These properties apply to either kind:

| Property | Meaning |
| --- | --- |
| `HttpMethod` | The method supplied for the failed call. |
| `RequestMessage` | The request, including its headers and local options. |
| `Uri` | `RequestMessage.RequestUri`, which can be null. |
| `RefitSettings` | The settings retained for the call and later error-body reading. |
| `RequestContent` | Captured request-body text when enabled; you can replace it to remove private data. |
| `HasRequestContent` | The captured text is neither null nor empty. Whitespace counts as present. |

Its three protected constructors support a cause, a message, or both, plus request, method and settings.
The cause-only overload takes its message from the cause and rejects a null cause.
They provide shared state for exception subclasses. Use the concrete [request exception constructors](responses.md#when-no-reply-arrives)
for a failure before a reply.

`ApiException` adds `StatusCode`, `ReasonPhrase`, response `Headers`, and nullable `ContentHeaders`.
Its settable `Content` contains raw response text. `HasContent` excludes null, empty and whitespace-only text.
`ContentHeaders` has a protected setter for subclasses.
The four protected constructors take request, method, raw content, status, reason, headers and settings.
The overloads independently add a custom exception message and an inner cause.
Without a custom message, the message includes the numeric status and reason phrase.

Avoid logging entire requests or exceptions through a property-walking serializer.
Headers and captured bodies can contain tokens or personal data.
`RefitSettings.ExceptionRedactor` lets your app remove those values before an `ApiException` leaves its factory.
The callback changes the same exception instance. It must handle any validation subtype it receives.

## Define a contextual exception

Derive from `ApiExceptionBase` when your app needs another kind of request error.
Pass the actual request, HTTP method and settings to its protected constructor.
These private sample subclasses demonstrate every constructor path.
The complete source retains the request and reply until the checks finish.

```csharp
private sealed class ContextError : ApiExceptionBase
{
    public ContextError(HttpRequestMessage request, RefitSettings settings, Exception cause)
        : base(request, HttpMethod.Get, settings, cause)
    {
    }

    public ContextError(string message, HttpRequestMessage request, RefitSettings settings)
        : base(message, request, HttpMethod.Get, settings)
    {
    }

    public ContextError(string message, HttpRequestMessage request, RefitSettings settings, Exception cause)
        : base(message, request, HttpMethod.Get, settings, cause)
    {
    }
}
```

Derive from `ApiException` when you also have reply context. `ReplyError` takes a reply
and passes its status, reason and headers to the base constructor. Null content means
the exception does not retain a response-body string.

```csharp
private sealed class ReplyError : ApiException
{
    public ReplyError(HttpRequestMessage request, HttpResponseMessage reply, RefitSettings settings)
        : base(request, HttpMethod.Get, null, reply.StatusCode, reply.ReasonPhrase, reply.Headers, settings)
    {
    }

    public ReplyError(HttpRequestMessage request, HttpResponseMessage reply, RefitSettings settings, Exception cause)
        : base(request, HttpMethod.Get, null, reply.StatusCode, reply.ReasonPhrase, reply.Headers, settings, cause)
    {
    }

    public ReplyError(string message, HttpRequestMessage request, HttpResponseMessage reply, RefitSettings settings)
        : base(message, request, HttpMethod.Get, null, reply.StatusCode, reply.ReasonPhrase, reply.Headers, settings)
    {
    }

    public ReplyError(string message, HttpRequestMessage request, HttpResponseMessage reply, RefitSettings settings, Exception cause)
        : base(message, request, HttpMethod.Get, null, reply.StatusCode, reply.ReasonPhrase, reply.Headers, settings, cause)
    {
    }
}
```

Here, `request` and `reply` are the actual messages, and `settings` supplies generated
JSON metadata as shown above. `ReplyFailure` is the text `"App reply failure."`.

```csharp
ContextError fromCause = new(request, settings, cause);
ContextError fromMessage = new("App failure.", request, settings);
ContextError fromBoth = new("App failure.", request, settings, cause);
ReplyError fromReply = new(request, reply, settings);
ReplyError replyWithCause = new(request, reply, settings, cause);
ReplyError customReply = new(ReplyFailure, request, reply, settings);
ReplyError customWithCause = new(ReplyFailure, request, reply, settings, cause);
Console.WriteLine(fromCause.Message); // Transport failed.
Console.WriteLine(customWithCause.StatusCode); // BadRequest
```

The [complete constructor sample](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Errors/ExceptionConstructors.cs)
checks the default message, retained inner causes and shared HTTP context.

## Create an error from an HTTP reply

Use `ApiException.Create` in an HTTP integration that already has the request and response.
The ordinary overloads require an unsuccessful status. Their optional inner exception retains a cause.
They return a `Task<ApiException>`; await it before reading the captured content.

```csharp
using HttpRequestMessage request = new(HttpMethod.Get, "https://people.example/person");
using HttpResponseMessage response = new(HttpStatusCode.BadRequest) { RequestMessage = request, Content = new StringContent("""{"code":"name_taken"}""") };
ApiException error = await ApiException.Create(request, request.Method, response, settings);
Console.WriteLine(error.HasContent); // True
Console.WriteLine(error.Uri);
DefaultApiExceptionFactory factory = new(settings);
using HttpResponseMessage success = new(HttpStatusCode.OK) { RequestMessage = request };
Exception? noError = await factory.CreateAsync(success);
Console.WriteLine(noError is null); // True
```

`DefaultApiExceptionFactory(RefitSettings)` retains your settings.
`CreateAsync(HttpResponseMessage)` returns null for a successful response.
For an unsuccessful response it requires the response's `RequestMessage` and delegates to `ApiException.Create`.
A missing request raises `InvalidOperationException`. Set it in your custom message handler.

The custom-message overloads of `ApiException.Create` accept even a successful status.
Refit uses that distinction to represent unreadable JSON on a 200 reply.
An optional inner exception preserves the reading error:

```csharp
using HttpResponseMessage unreadable = new(HttpStatusCode.OK) { RequestMessage = request, Content = new StringContent("not JSON") };
JsonException cause = new("Invalid person JSON.");
ApiException readError = await ApiException.Create("Could not read the person.", request, request.Method, unreadable, settings, cause);
```

All four factory overloads reject a null response.
The two without a custom message reject success with `ArgumentException`.
The two with a custom message do not check success.
Factories capture content headers and read the body, then call `ExceptionRedactor` if configured.
The body-reading path catches failures to avoid replacing the HTTP error.
If reading or problem-details conversion fails, the factory can return a plain `ApiException` instead.

`MaxExceptionContentLength` sets a character limit, not a byte limit, for the captured response body.
Null means unbounded. Zero or a negative value produces empty captured content.
A truncated JSON document may not deserialize. Set a useful limit and handle an unreadable body.
`CaptureRequestContent` controls request-body capture separately and defaults to false.
It buffers the request content before sending and can increase memory use.

## Standard validation replies

A reply with media type `application/problem+json` describes a problem in a standard JSON shape.
Refit's factory attempts to return a `ValidationApiException`.
Its `Content` hides the base string property and exposes `ProblemDetails?`.
Cast to `ApiException` when you need the raw body text.

**AOT gap:** registering `ProblemDetails` in a generated context does not make this automatic conversion work.
Its init-only `Extensions` property becomes a generated constructor parameter.
System.Text.Json rejects binding an extension-data property to that parameter with `InvalidOperationException`.
Refit's factory catches it, and the API call throws a plain `ApiException`.

Catch `ApiException` and use the public synchronous parser as a reflection-free fallback.
This excerpt belongs in a synchronous helper that receives the caught error:

```csharp
ValidationApiException validation = error as ValidationApiException ?? ValidationApiException.Create(error);
ProblemDetails? problem = validation.Content;
Console.WriteLine(problem?.Title); // Invalid name
Console.WriteLine(problem?.Errors["name"][0]); // Name is required.
Console.WriteLine(error.Content); // Raw JSON
```

| `ProblemDetails` property | Meaning |
| --- | --- |
| `Type` | A URI identifying the kind of problem; defaults to `about:blank`. |
| `Title` | A short label for that kind of problem. |
| `Status` | The status in the JSON document. It does not replace the actual HTTP status. |
| `Detail` | Text about this occurrence. |
| `Instance` | A URI identifying this occurrence. |
| `Errors` | A mutable dictionary from field name to an array of validation messages. Empty by default. |
| `Extensions` | A mutable dictionary for other JSON properties. Empty by default. |

The dictionaries have init setters: supply them when constructing the model, then modify their entries if needed.
The five scalar properties have ordinary setters.
Extension value types depend on the serializer. The synchronous parser used here reads simple values directly.
Register concrete runtime types too if you write extension values back to JSON in an AOT app.

`ValidationApiException.Create(ApiException)` converts an existing non-empty body synchronously.
It parses the standard fields itself rather than using your configured serializer.
It compares field names without regard to case, accepts string or array validation messages,
and reads simple extension values as booleans, numbers, strings or dates.
Other values remain `JsonElement` values. Null/blank content raises `ArgumentException`; invalid JSON raises `JsonException`.

The automatic async factory path uses your configured serializer instead.
Missing or unusable metadata prevents conversion to the validation subtype.
The original `ApiException` can remain, because the factory catches that conversion failure.

```csharp
error.Content = """{"title":"Invalid name","status":400,"errors":{"name":["Name is required."]},"trace":"sample"}""";
ValidationApiException validation = ValidationApiException.Create(error);
ValidationApiException withMessage = new("Invalid input.");
ValidationApiException withCause = new("Invalid input.", cause);
ProblemDetails local = new()
{
    Type = "about:blank",
    Title = "Invalid name",
    Status = (int)HttpStatusCode.BadRequest,
    Detail = "Choose a name.",
    Instance = "/people",
    Errors = new() { ["name"] = ["Name is required."] },
    Extensions = new Dictionary<string, object> { ["trace"] = "sample" },
};
```

The two message constructors create a validation exception without parsed `ProblemDetails`.
They take a message and optionally a non-null inner exception.
They supply synthetic GET request context and status 500; they do not represent a received server reply.
Prefer the factory when you have a real HTTP error to convert.

## Customize failure handling

### Limit and redact captured data

`MaxExceptionContentLength` limits the response text retained in `ApiException.Content`.
Null reads the whole body, zero retains an empty string, and a positive value keeps that many
characters at most. The [error-policy example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Errors/ErrorPolicies.cs)
checks all three choices. Here, `limit` is null, zero, or five:

```csharp
RefitSettings settings = new() { MaxExceptionContentLength = limit };
InvalidOperationException cause = new("The service rejected the person.");
ApiException error = await ApiException.Create(request, request.Method, response, settings, cause);
```

The factory retains the request method, response headers, content headers and reason phrase.
The limit applies to the response body text. It does not limit headers or captured request data.

Use `ExceptionRedactor` to remove details before an HTTP response exception reaches your caller.
`RequestContent` is the captured request text; `Content` is the captured reply text.
Removing them does not remove headers from the retained request:

```csharp
RefitSettings settings = new()
{
    ExceptionRedactor = static error =>
    {
        error.RequestContent = null;
        error.RequestMessage.Headers.Authorization = null;
        if (error is ApiException replyError)
        {
            replyError.Content = null;
        }
    },
};
```

**Transport discrepancy:** the current transport-error path skips `ExceptionRedactor`.
The intended contract is to redact the retained exception before it propagates.
The executable example shows that both the default and a custom transport factory retain
the request's authorization header without calling the hook.
Until that path is fixed, invoke the hook in your transport factory:

```csharp
settings.TransportExceptionFactory = (request, cause, token) =>
{
    if (cause is OperationCanceledException && token.IsCancellationRequested)
    {
        return cause;
    }

    ApiRequestException error = new(request, request.Method, settings, cause);
    settings.ExceptionRedactor?.Invoke(error);
    return error;
};
```

This keeps caller cancellation unchanged and redacts the wrapped send failure.
Configure the factory before creating the client.

### Choose exception factories

`RefitSettings.ExceptionFactory` receives a response and returns `ValueTask<Exception?>`.
Null lets Refit continue reading it, even for a non-success status.
`DeserializationExceptionFactory` receives the response and reading error.
Null from that callback suppresses the reading error and yields the default result.
Decide explicitly whether your app can use that default.

`TransportExceptionFactory` receives the request, send error and cancellation token.
The default passes through caller cancellation when the supplied token is canceled.
It wraps other send failures in `ApiRequestException`.
A wrapper call records an `ApiExceptionBase` transport result; other exception types are thrown directly.
Caller cancellation can therefore throw even when the method returns `ApiResponse<T>`.

When a service defines a typed error body, have `ExceptionFactory` deserialize that body with the
same configured serializer and return your service-specific exception. Keep a raw-content fallback
for gateway or malformed replies, and do not throw from the factory while it is reading the body.

**Implementation discrepancy:** received-response wrappers retain only `ApiException` results from
the response and deserialization factories. A custom exception of another type is discarded.
The same custom exception is thrown for a plain result call.
This example reproduces the lost error:

```csharp
RefitSettings customSettings = new(new SystemTextJsonContentSerializer(Options))
{
    ExceptionFactory = static _ => ValueTask.FromResult<Exception?>(new InvalidOperationException("App-specific failure.")),
};
IErrorsApi custom = RestService.ForGenerated<IErrorsApi>(httpClient, customSettings);
using ApiResponse<Person> wrapper = await custom.GetRejectedResponseAsync(cancellationToken);
Console.WriteLine(wrapper.Error is null); // True: the custom exception is lost.
```

Use `ApiException` for a custom wrapper error, or handle the status yourself.
On a success status with a custom reading error of another type, `IsSuccessful` can be true despite failed reading.
`EnsureSuccessfulAsync` can also return without throwing in that case.
The intended behavior is to retain the custom failure and report unsuccessful deserialization.
The [error examples](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Errors/Errors.cs)
check both the HTTP-error and deserialization-error cases.

## Error API reference

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| `ApiExceptionBase` | Abstract base class for Refit exceptions that retain the failed request, its HTTP method, and the settings used for the call. | None. | Base for request-send and response exceptions. |
| `ApiExceptionBase(HttpRequestMessage message, HttpMethod httpMethod, RefitSettings refitSettings, Exception innerException)` | Initializes an error with the request context and a required underlying exception. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`RefitSettings`](../clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception) `innerException`. | Protected base constructor using the non-null cause's message. |
| `ApiExceptionBase(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, RefitSettings refitSettings)` | Initializes an error with a caller-supplied message and request context. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`RefitSettings`](../clients/settings.md) `refitSettings`. | Protected base constructor with the supplied message. |
| `ApiExceptionBase(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, RefitSettings refitSettings, Exception? innerException)` | Initializes an error with a caller-supplied message, request context, and optional cause. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`RefitSettings`](../clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception)`?` `innerException`. | Protected base constructor with an optional cause. |
| `ApiExceptionBase.HttpMethod` | Identifies the HTTP method that Refit used for the failed request. | None. | [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod): the method used by the failed call. |
| `ApiExceptionBase.Uri` | Exposes the request URI when the retained request has one. | None. | [`Uri`](https://learn.microsoft.com/dotnet/api/system.uri)`?`: `RequestMessage.RequestUri`. |
| `ApiExceptionBase.RequestMessage` | Gives access to the live request, including headers and request options. | None. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage): the request, including its headers and local options. |
| `ApiExceptionBase.RequestContent` | Holds request-body text captured before sending when `CaptureRequestContent` is enabled. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` value. | Captured request-body text. You can replace it to remove private data. |
| `ApiExceptionBase.HasRequestContent` | Lets you test whether captured request text is available without checking the property yourself. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when captured request text is not null or empty. Whitespace counts as present. |
| `ApiExceptionBase.RefitSettings` | Gets the settings that governed the failed call. | None. | [`RefitSettings`](../clients/settings.md): settings retained for the call and later error-body reading. |
| `ApiException` | Represents an error received after the server sent an HTTP response. | None. | Exception with response status, headers, and buffered body text. |
| `ApiException(HttpRequestMessage message, HttpMethod httpMethod, string? content, HttpStatusCode statusCode, string? reasonPhrase, HttpResponseHeaders headers, RefitSettings refitSettings)` | Initializes a response exception with Refit's status-and-reason message. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `content`; [`HttpStatusCode`](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode) `statusCode`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `reasonPhrase`; [`HttpResponseHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders) `headers`; [`RefitSettings`](../clients/settings.md) `refitSettings`. | Protected HTTP-response constructor. |
| `ApiException(HttpRequestMessage message, HttpMethod httpMethod, string? content, HttpStatusCode statusCode, string? reasonPhrase, HttpResponseHeaders headers, RefitSettings refitSettings, Exception? innerException)` | Initializes a response exception with Refit's status-and-reason message and an optional underlying cause. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `content`; [`HttpStatusCode`](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode) `statusCode`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `reasonPhrase`; [`HttpResponseHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders) `headers`; [`RefitSettings`](../clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception)`?` `innerException`. | Protected HTTP-response constructor with an optional cause. |
| `ApiException(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, string? content, HttpStatusCode statusCode, string? reasonPhrase, HttpResponseHeaders headers, RefitSettings refitSettings)` | Initializes a response exception with an app-defined message. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `content`; [`HttpStatusCode`](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode) `statusCode`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `reasonPhrase`; [`HttpResponseHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders) `headers`; [`RefitSettings`](../clients/settings.md) `refitSettings`. | Protected HTTP-response constructor with the supplied message. |
| `ApiException(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, string? content, HttpStatusCode statusCode, string? reasonPhrase, HttpResponseHeaders headers, RefitSettings refitSettings, Exception? innerException)` | Initializes a response exception with an app-defined message and an optional underlying cause. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `content`; [`HttpStatusCode`](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode) `statusCode`; [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` `reasonPhrase`; [`HttpResponseHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders) `headers`; [`RefitSettings`](../clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception)`?` `innerException`. | Protected HTTP-response constructor with the supplied message and optional cause. |
| `ApiException.Create(HttpRequestMessage message, HttpMethod httpMethod, HttpResponseMessage response, RefitSettings refitSettings)` | Builds an ApiException asynchronously from the failed HTTP response and request metadata. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) `response`; [`RefitSettings`](../clients/settings.md) `refitSettings`. | [`Task<ApiException>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1) that captures the unsuccessful response. |
| `ApiException.Create(HttpRequestMessage message, HttpMethod httpMethod, HttpResponseMessage response, RefitSettings refitSettings, Exception? innerException)` | Builds an ApiException asynchronously from the failed HTTP response and request metadata. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) `response`; [`RefitSettings`](../clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception)`?` `innerException`. | [`Task<ApiException>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1) that captures the response and optional cause. |
| `ApiException.Create(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, HttpResponseMessage response, RefitSettings refitSettings)` | Builds an ApiException asynchronously from the failed HTTP response and request metadata. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) `response`; [`RefitSettings`](../clients/settings.md) `refitSettings`. | [`Task<ApiException>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1) with the supplied message. |
| `ApiException.Create(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, HttpResponseMessage response, RefitSettings refitSettings, Exception? innerException)` | Builds an ApiException asynchronously from the failed HTTP response and request metadata. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) `response`; [`RefitSettings`](../clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception)`?` `innerException`. | [`Task<ApiException>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1) with the supplied message and optional cause. |
| `ApiException.GetContentAsAsync<T>()` | Deserializes buffered response text through the configured asynchronous content serializer. | None; `T` is the requested error-body type. | [`Task<T?>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task-1); asynchronous deserialization. |
| `ApiException.GetContentAs<T>()` | Deserializes buffered response text through the configured synchronous content serializer. | None; `T` is the requested error-body type. | `T?`; synchronous deserialization or [`NotSupportedException`](https://learn.microsoft.com/dotnet/api/system.notsupportedexception). |
| `ApiException.TryGetContentAs<T>(out T? content)` | Tries synchronous error-body deserialization without letting parsing or serializer-support failures escape. | `out T?` `content`. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean); false for absent, unsupported, or invalid content. |
| `ApiException.StatusCode` | Identifies the HTTP status sent by the server. | None. | [`HttpStatusCode`](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode): the received HTTP response status. |
| `ApiException.ReasonPhrase` | Preserves the optional reason phrase sent with the HTTP status. | None. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?`: the server reason phrase, if supplied. |
| `ApiException.Headers` | Provides the response headers retained from the failed response. | None. | [`HttpResponseHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders): the received response headers. |
| `ApiException.ContentHeaders` | Provides headers belonging to the buffered response body. | None; protected setter. | [`HttpContentHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpcontentheaders)`?`: headers for the captured response body. |
| `ApiException.Content` | Holds the raw buffered response body and lets a redactor replace or clear it. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` value. | Captured raw response text. You can replace it to remove private data. |
| `ApiException.HasContent` | Tests whether `Content` contains non-whitespace response text. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when `Content` is not null, empty, or whitespace. |
| `DefaultApiExceptionFactory` | Supplies Refit's default conversion from an unsuccessful HTTP response to an `ApiException`. | None. | Response-to-exception factory. |
| `DefaultApiExceptionFactory(RefitSettings refitSettings)` | Creates the exception factory that turns unsuccessful responses into ApiException instances using the supplied settings. | [`RefitSettings`](../clients/settings.md) `refitSettings`: settings used to create exceptions. | New factory. |
| `DefaultApiExceptionFactory.CreateAsync(HttpResponseMessage responseMessage)` | Returns no exception for a successful response, or creates an `ApiException` from an unsuccessful response's retained request. | [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) `responseMessage`. | [`ValueTask<Exception?>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1); null for a successful response. |
| `ProblemDetails` | Models a standard HTTP problem document, including validation errors and extension fields. | None. | Data object used by `ValidationApiException`. |
| `ProblemDetails()` | Initializes a problem document with empty `Errors` and `Extensions`, and `Type` set to `about:blank`. | None. | New problem-details object with empty Errors/Extensions and Type "about:blank". |
| `ProblemDetails.Errors` | Maps each invalid field name to its validation messages. | None; `init` only. | [`Dictionary<string, string[]>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.dictionary-2); default empty. |
| `ProblemDetails.Extensions` | Stores JSON properties that are not standard problem-details fields. | None; `init` only. | [`IDictionary<string, object>`](https://learn.microsoft.com/dotnet/api/system.collections.generic.idictionary-2); default empty. |
| `ProblemDetails.Type` | Identifies the kind of problem, usually with a URI. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` value. | A URI that identifies the problem kind; default `about:blank`. |
| `ProblemDetails.Title` | Gives a short human-readable label for the problem kind. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` value. | A short label for the problem kind; default null. |
| `ProblemDetails.Status` | Carries the status recorded in the JSON problem document. | [`int`](https://learn.microsoft.com/dotnet/api/system.int32) value. | The status value carried in the problem document; default 0. It does not replace the actual HTTP status. |
| `ProblemDetails.Detail` | Explains this particular problem occurrence. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` value. | Text about this problem occurrence; default null. |
| `ProblemDetails.Instance` | Identifies this particular problem occurrence, usually with a URI. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?` value. | A URI that identifies this problem occurrence; default null. |
| `ValidationApiException` | Represents an API error whose body has been parsed as standard problem details. | None. | `ApiException` subtype with typed validation content. |
| `ValidationApiException(string message)` | Creates a validation exception for app code that has no received problem response to convert. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `message`. | New validation exception with synthetic HTTP context. |
| `ValidationApiException(string message, Exception innerException)` | Creates a validation exception with an app-defined message and a required cause. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `message`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception) `innerException`: non-null cause. | New validation exception with cause. |
| `ValidationApiException.Create(ApiException exception)` | Parses the non-blank raw body of an existing API exception as standard problem details. | [`ApiException`](errors.md) `exception`: error to convert; it must contain non-whitespace content. | ValidationApiException with ProblemDetails content. |
| `ValidationApiException.Content` | Exposes the parsed problem document while hiding `ApiException.Content` on a validation exception. | None; private setter. | [`ProblemDetails`](errors.md)`?`: the parsed validation body, or null when this exception was created only with a message. |

Types: [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage), [HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage), [HttpMethod](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod), [HttpStatusCode](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode), [HttpResponseHeaders](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders), [HttpContentHeaders](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpcontentheaders), [Task](https://learn.microsoft.com/dotnet/api/system.threading.tasks.task), [ValueTask](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask), and [Dictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.dictionary-2).

Production source: [ApiExceptionBase.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiExceptionBase.cs), [ApiException.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiException.cs), [ApiRequestException.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiRequestException.cs), [DefaultApiExceptionFactory.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultApiExceptionFactory.cs), [ProblemDetails.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/ProblemDetails.cs), and [ValidationApiException.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/ValidationApiException.cs).
