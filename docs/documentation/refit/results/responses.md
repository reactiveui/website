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

**2. Configure generated JSON metadata.** Reuse the [JSON context and serializer setup](../serialization/json.md#set-up-a-reusable-serializer).
Register `Person`, the body type. Do not register `ApiResponse<Person>` as a JSON root.
Refit creates that wrapper locally; the server sends only the body's JSON.

**3. Guard the result and dispose the wrapper.** The [return-type example](return-types.md#keep-status-and-error-details)
shows the normal API call. The example below constructs the same kind of wrapper directly.
This is useful when writing a client adapter or a test.

```csharp
using HttpRequestMessage request = new(HttpMethod.Get, "https://people.example/person");
using HttpResponseMessage message = new(HttpStatusCode.OK) { RequestMessage = request };
using ApiResponse<Person> response = new(message, new(1, "Ada"), settings);
using ApiResponse<Person> explicitError = new(message, new(1, "Ada"), settings, error: null);
Console.WriteLine(response.IsReceived); // True
Console.WriteLine(response.StatusCode); // OK
Console.WriteLine(response.RequestMessage.RequestUri);
Console.WriteLine(response.Settings == settings); // True
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
A transport is the HTTP code that connects to the server and carries the bytes.
[The transport example below](#when-no-reply-arrives) shows that constructor.

## Status success and content success differ

A status from 200 through 299 is an HTTP success. It does not prove that the body matches your C# model.
The response can have status 200 and contain invalid JSON.
The runnable example supplies a 200 reply with the text `not JSON`:

```csharp
using ApiResponse<Person> malformed = await api.GetMalformedAsync(CancellationToken.None);
Console.WriteLine(malformed.IsReceived); // True
Console.WriteLine(malformed.IsSuccessStatusCode); // True
Console.WriteLine(malformed.IsSuccessful); // False
Console.WriteLine(malformed.HasContent); // False
_ = await malformed.EnsureSuccessStatusCodeAsync();
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
_ = await response.EnsureSuccessStatusCodeAsync();
_ = await response.EnsureSuccessfulAsync();
_ = await typed.EnsureSuccessStatusCodeAsync();
_ = await typed.EnsureSuccessfulAsync();
_ = await untyped.EnsureSuccessStatusCodeAsync();
_ = await untyped.EnsureSuccessfulAsync();
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
using HttpRequestMessage request = new(HttpMethod.Get, "https://people.example/person");
HttpRequestException cause = new("Connection unavailable.");
ApiRequestException fromCause = new(request, request.Method, settings, cause);
ApiRequestException withMessage = new("Could not contact people API.", request, request.Method, settings);
ApiRequestException withBoth = new("Could not contact people API.", request, request.Method, settings, cause);
using ApiResponse<Person> missing = new(request, response: null, content: null, settings, fromCause);
Console.WriteLine(missing.IsReceived); // False
Console.WriteLine(missing.StatusCode is null); // True
if (missing.HasRequestError(out ApiRequestException? sendError))
{
    Console.WriteLine(sendError.Message); // Connection unavailable.
}
```

**Implementation discrepancy:** the concrete guards check for a response before using the captured error.
With a null response, both concrete guards throw `InvalidOperationException` rather than the documented
`ApiRequestException`. The interface guards surface the captured transport error.
The example checks this difference:

```csharp
IApiResponse<Person> typed = missing;
try
{
    _ = await typed.EnsureSuccessfulAsync();
    throw new InvalidOperationException("The interface guard should throw.");
}
catch (ApiRequestException error)
{
    SampleCheck.Equal(fromCause, error);
}

try
{
    _ = await missing.EnsureSuccessfulAsync();
    throw new InvalidOperationException("The concrete guard should throw.");
}
catch (InvalidOperationException error)
{
    SampleCheck.Equal("The response is unavailable for this API response.", error.Message);
}
```

Use `HasRequestError` or an interface guard when handling this case.
Read [error bodies and problem details](errors.md) for the exception properties.
The [response examples](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Responses)
contain the executable reproduction.

## Ownership

`ApiResponse<T>.Dispose()` disposes the underlying HTTP response once.
Repeated calls are safe. It does not dispose the request separately.
Read any needed body or headers before disposing the wrapper.
Disposal does not change its status flags or erase the stored value.

The ownership example creates separate request and response streams. These checks confirm
that disposing the wrapper closes only the response stream:

```csharp
response.Dispose();
response.Dispose();
SampleCheck.Equal(false, responseStream.CanRead);
SampleCheck.Equal(true, requestStream.CanRead);
```
