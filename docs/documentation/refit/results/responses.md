---
Order: 3
---
# Response details and success checks

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/results-responses/results-responses.csproj).

Sometimes the reply's data is only part of what your app needs. You may also need its status,
a header from the service, or details about why the call failed. `ApiResponse<T>` keeps those
pieces together with the value Refit read from the body.

The wrapper helps you tell a successful call from a failed connection or an unreadable reply.
This page shows which checks to make before using the value and how to release the response afterward.

## Check a reply

**1. Declare a wrapped return type.** Use `Task<ApiResponse<Person>>` or `Task<IApiResponse<Person>>`
on your interface method. `Person` is the model from [your first request](../index.md#your-first-request).

**2. Configure generated JSON metadata.** Give the client a [JSON context](../serialization/json.md#register-a-context),
or the settings you build from one.
Register `Person`, the body type. Do not register `ApiResponse<Person>` as a JSON root.
Refit creates that wrapper locally; the server sends only the body's JSON.

**3. Guard the result and dispose the wrapper.** The [return-type example](return-types.md#keep-status-and-error-details)
shows the normal API call. The example below constructs the same kind of wrapper directly.
This is useful when writing a client adapter or a test.

```csharp
using HttpRequestMessage request = new(HttpMethod.Get, "https://api.example.com/people/1");
using HttpResponseMessage message = new(HttpStatusCode.OK) { RequestMessage = request };
using ApiResponse<Person> response = new(message, new(1, "Ada"), settings);
// response.IsReceived == true, response.StatusCode == HttpStatusCode.OK
if (response.IsSuccessfulWithContent)
{
    Console.WriteLine(response.Content.Name); // Ada
}
```

Both response-only constructors require a non-null `HttpResponseMessage` with `RequestMessage` set.
A missing response raises `ArgumentNullException`. A missing associated request raises `ArgumentException`.
The overload with `error` records the supplied `ApiExceptionBase`; it does not infer an error from the status.
The content argument is your deserialized value. The constructor does not parse JSON.

The five-argument constructor takes the request separately. It accepts a null response for a transport failure.
A transport failure happens before Refit receives an HTTP response.
[The transport example below](#when-no-reply-arrives) shows that constructor.

## Read the body with explicit metadata

A method that returns `ApiResponse<T>` can take a `JsonTypeInfo<T>` parameter. Refit reads the body with that metadata
and does not send the parameter. `T` is the body type, `Order` here, not `ApiResponse<Order>`.

```csharp
[Get("/orders/{id}")]
Task<ApiResponse<Order>> GetOrderResponseAsync(int id, JsonTypeInfo<Order> orderInfo, CancellationToken cancellationToken);
```

```csharp
using ApiResponse<Order> response = await api.GetOrderResponseAsync(5, OrdersJsonContext.Default.Order, cancellationToken);
// response.StatusCode == HttpStatusCode.OK, response.Content?.Customer == "Ada"
```

The wrapper carries the status and headers as usual. See [pass metadata to a method](../serialization/json.md#pass-metadata-to-a-method)
for the rules.

## Status success and content success differ

A status from 200 through 299 is an HTTP success. It does not prove that the body matches your C# model.
The response can have status 200 and contain invalid JSON.
Here the service replies 200 with the text `not JSON`:

```csharp
using ApiResponse<Person> malformed = await api.GetMalformedAsync(cancellationToken);
// malformed.IsReceived == true, malformed.IsSuccessStatusCode == true
// malformed.IsSuccessful == false, malformed.HasContent == false
await malformed.EnsureSuccessStatusCodeAsync(); // passes: it checks only the status
if (malformed.HasResponseError(out ApiException? readError))
{
    Console.WriteLine(readError.InnerException?.GetType().Name); // JsonException
}
```

`EnsureSuccessStatusCodeAsync` checks only the status. `EnsureSuccessfulAsync` also checks `Error`.
Each returns the same response on success. Await each returned `ValueTask` once.
Use the full-success check for model replies. Use the status-only check when your code handles body errors itself.

| Property | Meaning |
| --- | --- |
| `IsReceived` | A response message exists. False means no response arrived. |
| `IsSuccessStatusCode` | A response exists and its status is 200–299. |
| `IsSuccessful` | The status succeeds and `Error` is null. |
| `HasContent` | `Content` is non-null. For a value type, its default value is also non-null. |
| `IsSuccessfulWithContent` | Both `IsSuccessful` and `HasContent` are true. |
| `Content` | The deserialized reply value, or default when unavailable. |
| `Error` | A captured `ApiExceptionBase`, or null. An unsuccessful manually constructed wrapper may have no error. |
| `Settings` | The settings supplied to the concrete wrapper. It keeps the same instance. |
| `RequestMessage` | The request associated with this reply. The interface permits null; the concrete wrapper returns its constructor argument. |
| `StatusCode`, `ReasonPhrase`, `Version` | The response status, reason text and HTTP version. Null when no response exists. |
| `Headers` | The response headers. Null when no response exists. |
| `ContentHeaders` | The body headers, such as its media type. Null when unavailable. |

Success alone does not promise a body. A 204 reply, for example, can succeed without content.
The flags do not validate an app rule such as “the name must not be empty”. Apply that rule yourself.

## Guards through either interface

`IApiResponse` carries status, headers and errors. `IApiResponse<out T>` adds typed content and its presence flags.
The `out T` lets a wrapper for a derived model be read through an interface for its base model.
`ApiResponseExtensions` supplies both guards for both interfaces:

```csharp
IApiResponse<Person> typed = response;
IApiResponse untyped = response;
await response.EnsureSuccessStatusCodeAsync();
await response.EnsureSuccessfulAsync();
await typed.EnsureSuccessStatusCodeAsync();
await typed.EnsureSuccessfulAsync();
await untyped.EnsureSuccessStatusCodeAsync();
await untyped.EnsureSuccessfulAsync();
```

The concrete guards return `ValueTask<ApiResponse<T>>`.
The generic interface guards return `ValueTask<IApiResponse<T>>`.
The non-generic interface guards return `ValueTask<IApiResponse>`.
Interface guards reject a null receiver with `ArgumentNullException`.

On failure, an interface guard throws the captured error.
If no error was recorded, it throws `InvalidOperationException`.
It does not create an HTTP error or dispose the wrapper. Keep the `using` declaration around the call.
The concrete guard creates an `ApiException` when a received unsuccessful reply has no captured error.
It disposes the response before throwing that error.

## When no reply arrives

`HasRequestError(out ApiRequestException?)` checks for a failure before a response arrives.
`HasResponseError(out ApiException?)` checks for a received-response or body-reading error.
Both return false and assign null when that error kind is absent.
`ValidationApiException` also counts as a response error.

The three `ApiRequestException` constructors let you supply a cause, a message, or both.
The cause-only overload uses the cause's message and rejects a null cause.
All retain the request, method and settings you supply.

```csharp
using HttpRequestMessage request = new(HttpMethod.Get, "https://api.example.com/people/1");
HttpRequestException cause = new("Connection unavailable.");
ApiRequestException fromCause = new(request, request.Method, settings, cause);
ApiRequestException withMessage = new("Could not contact the people service.", request, request.Method, settings);
ApiRequestException withBoth = new("Could not contact the people service.", request, request.Method, settings, cause);
using ApiResponse<Person> missing = new(request, response: null, content: null, settings, fromCause);
// missing.IsReceived == false, missing.StatusCode == null
if (missing.HasRequestError(out ApiRequestException? sendError))
{
    Console.WriteLine(sendError.Message); // Connection unavailable.
}
```

**Implementation discrepancy:** the concrete guards check for a response before using the captured error.
With a null response, both concrete guards throw `InvalidOperationException` rather than the documented
`ApiRequestException`. The interface guards surface the captured transport error:

```csharp
IApiResponse<Person> typed = missing;
try
{
    await typed.EnsureSuccessfulAsync();
}
catch (ApiRequestException error)
{
    Console.WriteLine(error.Message); // Connection unavailable. (the captured fromCause)
}

try
{
    await missing.EnsureSuccessfulAsync();
}
catch (InvalidOperationException error)
{
    Console.WriteLine(error.Message); // The response is unavailable for this API response.
}
```

Use `HasRequestError` or an interface guard when handling this case.
Read [error bodies and problem details](errors.md) for the exception properties.

## Ownership

`ApiResponse<T>.Dispose()` disposes the underlying HTTP response once.
Repeated calls are safe. It does not dispose the request separately.
Read any needed body or headers before disposing the wrapper.
Disposal does not change its status flags or erase the stored value.

Disposing the wrapper closes the response body, but the request stays yours to dispose:

```csharp
MemoryStream requestStream = new();
MemoryStream responseStream = new();
using HttpRequestMessage request = new(HttpMethod.Post, "https://api.example.com/people") { Content = new StreamContent(requestStream) };
HttpResponseMessage message = new(HttpStatusCode.OK) { RequestMessage = request, Content = new StreamContent(responseStream) };
ApiResponse<Person> response = new(message, new(1, "Ada"), settings);

response.Dispose();
response.Dispose(); // safe: the second call does nothing
// responseStream.CanRead == false: the wrapper disposed the response
// requestStream.CanRead == true: the using declaration disposes the request later
```

## Response API reference

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| [`ApiRequestException`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiRequestException.cs) | Represents a failure while Refit sends a request before a response arrives. | None. | An [`ApiExceptionBase`](errors.md) that retains request context and may wrap the sending exception. |
| [`ApiResponse<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs) | Wraps a deserialized body, HTTP metadata, settings, and a captured error. | `T`: the body type. | A sealed [`IApiResponse<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse%7BT%7D.cs) that disposes the received response. |
| [`IApiResponse`](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse.cs) | Defines status, metadata, error, and disposal members for a Refit response. | None. | The base response contract. |
| [`IApiResponse<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse%7BT%7D.cs) | Adds a covariant deserialized body and body-presence checks to `IApiResponse`. | `out T`: the body type read by callers. | The typed response contract. |
| [`ApiResponseExtensions`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponseExtensions.cs) | Provides success guards for generic and non-generic response interfaces. | None. | A static extension class. |
| [`ApiResponse<T>(HttpRequestMessage request, HttpResponseMessage? response, T? content, RefitSettings settings, ApiExceptionBase? error = null)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs) | Creates a response wrapper that keeps the request, optional HTTP response, deserialized content, settings, and captured error together. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `request`; [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage)`?` `response`; `T?` `content`; [`RefitSettings`](../clients/settings.md) `settings`; [`ApiExceptionBase`](errors.md)`?` `error = null`. | New response wrapper. `response` may be null for a transport failure; `error` defaults to null. |
| [`ApiResponse<T>(HttpResponseMessage response, T? content, RefitSettings settings)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs) | Creates a wrapper for a received response with no captured error. | [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) `response`; `T?` `content`; [`RefitSettings`](../clients/settings.md) `settings`. | A new wrapper. `response` and `response.RequestMessage` must be non-null. |
| [`ApiResponse<T>(HttpResponseMessage response, T? content, RefitSettings settings, ApiExceptionBase? error)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs) | Creates a wrapper for a received response and a supplied captured error. | [`HttpResponseMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage) `response`; `T?` `content`; [`RefitSettings`](../clients/settings.md) `settings`; [`ApiExceptionBase`](errors.md)`?` `error`. | A new wrapper. `response` and `response.RequestMessage` must be non-null. |
| [`ApiResponse<T>.Dispose()`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs) | Disposes the received HTTP response once. | None. | [`void`](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/void); it does not dispose `RequestMessage`. |
| [`ApiResponse<T>.EnsureSuccessStatusCodeAsync()`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs) | Guards only the HTTP status. | None. | [`ValueTask<ApiResponse<T>>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1); returns this for 2xx. Otherwise it disposes and throws `Error` or a created `ApiException`; with no response it throws `InvalidOperationException`. |
| [`ApiResponse<T>.EnsureSuccessfulAsync()`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs) | Guards the HTTP status and captured error. | None. | [`ValueTask<ApiResponse<T>>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1); returns this when `IsSuccessful` is true. Failure behavior matches the status guard. |
| `ApiResponse<T>.HasRequestError(out ApiRequestException? error)` | Checks for a captured transport error and returns it through the out parameter. | `out` [`ApiRequestException`](errors.md)`?` `error`. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean); true when `Error` is a request error. |
| `ApiResponse<T>.HasResponseError(out ApiException? error)` | Checks for a captured response error and returns it through the out parameter. | `out` [`ApiException`](errors.md)`?` `error`. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean); true when `Error` is a response error. |
| `ApiResponse<T>.Content` | Stores the deserialized response body, or the default value when no body was read. | None. | `T?`: the stored response body, or `default(T)` when no value was read. For a non-nullable value type, this can be a value such as `0`. |
| `ApiResponse<T>.ContentHeaders` | Exposes headers belonging to the received response body. | None. | [`HttpContentHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpcontentheaders)`?`: content headers, or null when no response content exists. |
| `ApiResponse<T>.Error` | Stores the captured transport, HTTP-status, or deserialization error. | None. | [`ApiExceptionBase`](errors.md)`?`: the captured transport, HTTP, or deserialization error. |
| `ApiResponse<T>.HasContent` | Reports whether the deserialized Content is non-null. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean); `Content` is non-null. |
| `ApiResponse<T>.IsSuccessfulWithContent` | Reports whether the response succeeded without an error and has non-null Content. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when the status is successful, no error was captured, and `Content` is non-null. |
| `ApiResponse<T>.Headers` | Exposes headers from the received HTTP response. | None. | [`HttpResponseHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders)`?`: the received response headers, or null when no response arrived. |
| `ApiResponse<T>.IsReceived` | Reports whether an HTTP response message was received. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when an HTTP response arrived. |
| `ApiResponse<T>.IsSuccessStatusCode` | Reports whether the received status code is in the 2xx range. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when a received response has a 2xx status. |
| `ApiResponse<T>.IsSuccessful` | Reports whether the status is 2xx and no error was captured; it does not require content. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when the status is 2xx and no error was captured. It does not promise body content. |
| `ApiResponse<T>.ReasonPhrase` | Exposes the reason phrase returned with the HTTP status. | None. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?`: the server reason phrase, or null when none is available. |
| `ApiResponse<T>.RequestMessage` | Exposes the request associated with the response wrapper. | None. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage): the request associated with this wrapper. |
| `ApiResponse<T>.Settings` | Exposes the RefitSettings used to process the response. | None. | [`RefitSettings`](../clients/settings.md): the settings instance supplied to the constructor. |
| `ApiResponse<T>.StatusCode` | Exposes the received HTTP status code, or null when no response arrived. | None. | [`HttpStatusCode`](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode)`?`: the received status, or null when no response arrived. |
| `ApiResponse<T>.Version` | Exposes the HTTP version used by the received response. | None. | [`Version`](https://learn.microsoft.com/dotnet/api/system.version)`?`: the received HTTP version, or null when no response arrived. |
| `IApiResponse.HasRequestError(out ApiRequestException? error)` | Checks for a captured transport error and returns it through the out parameter. | `out` [`ApiRequestException`](errors.md)`?` `error`. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true and assigns the transport error when the request failed before a response; otherwise false and null. |
| `IApiResponse.HasResponseError(out ApiException? error)` | Checks for a captured response error and returns it through the out parameter. | `out` [`ApiException`](errors.md)`?` `error`. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true and assigns the response or body-reading error; otherwise false and null. |
| `IApiResponse.Headers` | Exposes headers from the received HTTP response. | None. | [`HttpResponseHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders)`?`: the received response headers, or null when no response arrived. |
| `IApiResponse.ContentHeaders` | Exposes headers belonging to the received response body. | None. | [`HttpContentHeaders`](https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpcontentheaders)`?`: headers for the received response body, or null when they are unavailable. |
| `IApiResponse.IsReceived` | Reports whether an HTTP response message was received. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when an HTTP response arrived. |
| `IApiResponse.IsSuccessStatusCode` | Reports whether the received status code is in the 2xx range. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when a received response has a 2xx status. |
| `IApiResponse.IsSuccessful` | Reports whether the status is 2xx and no error was captured; it does not require content. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when the status is 2xx and no error was captured. It does not promise body content. |
| `IApiResponse.StatusCode` | Exposes the received HTTP status code, or null when no response arrived. | None. | [`HttpStatusCode`](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode)`?`: the received status, or null when no response arrived. |
| `IApiResponse.ReasonPhrase` | Exposes the reason phrase returned with the HTTP status. | None. | [`string`](https://learn.microsoft.com/dotnet/api/system.string)`?`: the server reason phrase, or null when none is available. |
| `IApiResponse.RequestMessage` | Exposes the request associated with the response wrapper. | None. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage)`?`: the request that led to the response, or null when unavailable. |
| `IApiResponse.Version` | Exposes the HTTP version used by the received response. | None. | [`Version`](https://learn.microsoft.com/dotnet/api/system.version)`?`: the received HTTP version, or null when no response arrived. |
| `IApiResponse.Error` | Stores the captured transport, HTTP-status, or deserialization error. | None. | [`ApiExceptionBase`](errors.md)`?`: a captured transport, HTTP, or deserialization error. An unsuccessful response can have no captured error. |
| `IApiResponse<T>.Content` | Stores the deserialized response body, or the default value when no body was read. | None. | `T?`: the stored response body, or `default(T)` when no value was read. For a non-nullable value type, this can be a value such as `0`. |
| `IApiResponse<T>.HasContent` | Reports whether the deserialized Content is non-null. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when `Content` is non-null. |
| `IApiResponse<T>.IsSuccessfulWithContent` | Reports whether the response succeeded without an error and has non-null Content. | None. | [`bool`](https://learn.microsoft.com/dotnet/api/system.boolean): true when `IsSuccessful` is true and `Content` is non-null. |
| [`ApiRequestException(HttpRequestMessage message, HttpMethod httpMethod, RefitSettings refitSettings, Exception innerException)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiRequestException.cs) | Creates a transport exception with the request, HTTP method, settings, and supplied message or inner cause. | [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`RefitSettings`](../clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception) `innerException`. | New transport exception using the non-null cause's message. |
| [`ApiRequestException(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, RefitSettings refitSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiRequestException.cs) | Creates a transport exception with the request, HTTP method, settings, and supplied message or inner cause. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`RefitSettings`](../clients/settings.md) `refitSettings`. | New transport exception with the supplied message. |
| [`ApiRequestException(string exceptionMessage, HttpRequestMessage message, HttpMethod httpMethod, RefitSettings refitSettings, Exception? innerException)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiRequestException.cs) | Creates a transport exception with the request, HTTP method, settings, and supplied message or inner cause. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `exceptionMessage`; [`HttpRequestMessage`](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage) `message`; [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) `httpMethod`; [`RefitSettings`](../clients/settings.md) `refitSettings`; [`Exception`](https://learn.microsoft.com/dotnet/api/system.exception)`?` `innerException`. | New transport exception with the supplied message and optional cause. |
| [`ApiResponseExtensions.EnsureSuccessStatusCodeAsync<T>(IApiResponse<T> response)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponseExtensions.cs) | Guards only the HTTP status of a typed response. | [`IApiResponse<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse%7BT%7D.cs) `response`. | [`ValueTask<IApiResponse<T>>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1); returns the same response for 2xx. It rejects null, and otherwise throws `Error` or `InvalidOperationException` without disposing. |
| [`ApiResponseExtensions.EnsureSuccessfulAsync<T>(IApiResponse<T> response)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponseExtensions.cs) | Guards the HTTP status and captured error of a typed response. | [`IApiResponse<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse%7BT%7D.cs) `response`. | [`ValueTask<IApiResponse<T>>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1); returns the same response when `IsSuccessful` is true. Failure behavior matches the status guard. |
| [`ApiResponseExtensions.EnsureSuccessStatusCodeAsync(IApiResponse response)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponseExtensions.cs) | Guards only the HTTP status of a non-generic response. | [`IApiResponse`](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse.cs) `response`. | [`ValueTask<IApiResponse>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1); returns the same response for 2xx. It rejects null, and otherwise throws `Error` or `InvalidOperationException` without disposing. |
| [`ApiResponseExtensions.EnsureSuccessfulAsync(IApiResponse response)`](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponseExtensions.cs) | Guards the HTTP status and captured error of a non-generic response. | [`IApiResponse`](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse.cs) `response`. | [`ValueTask<IApiResponse>`](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask-1); returns the same response when `IsSuccessful` is true. Failure behavior matches the status guard. |

Types: [HttpRequestMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage), [HttpResponseMessage](https://learn.microsoft.com/dotnet/api/system.net.http.httpresponsemessage), [HttpMethod](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod), [HttpStatusCode](https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode), [Version](https://learn.microsoft.com/dotnet/api/system.version), and [ValueTask](https://learn.microsoft.com/dotnet/api/system.threading.tasks.valuetask).

Production source: [ApiResponse{T}.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponse%7BT%7D.cs), [IApiResponse.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse.cs), [IApiResponse{T}.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/IApiResponse%7BT%7D.cs), [ApiResponseExtensions.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiResponseExtensions.cs), and [ApiRequestException.cs](https://github.com/reactiveui/refit/blob/main/src/Refit/ApiRequestException.cs).
