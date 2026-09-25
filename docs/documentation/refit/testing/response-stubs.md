---
Order: 5
---
# Test code that accepts a response

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/testing-response-stubs/testing-response-stubs.csproj).

Some code only deals with the result of an API call. For example, a method might inspect
the status and decide which message to show. You can test that decision by giving it a
response with the exact data and flags you need.

`StubApiResponse<T>` lets you create that response directly. Use it for code that accepts
`IApiResponse<T>`, and use [handler tests](index.md) when the request itself is part of what you want to check.

## Supply a consistent state

The stub skips HTTP and JSON serialization. It cannot replace a concrete `ApiResponse<T>` return value.

**1. Choose the content and flags.** Every property is init-only and independent.
Setting `Content` does not set `HasContent`. Setting status 200 does not set `IsSuccessful`.

**2. Supply real header collections if needed.** `HttpResponseHeaders` has no public constructor.
Create an `HttpResponseMessage` and assign its headers and version to the stub. The test owns that disposable message.

**3. Read through `IApiResponse<T>`.** The interface's success contracts tell the compiler when content
or metadata is non-null. The concrete stub properties do not carry those narrowing attributes.
The [success example](#response-and-error-examples) passes a stub to the code under test and checks its result.

| Property | Type | Default and what the test supplies |
| --- | --- | --- |
| `Content` | `T?` | `default(T)`. Typed body for the scenario. |
| `HasContent` | [bool] | `false`. Whether the test promises non-null content. |
| `IsSuccessfulWithContent` | [bool] | `false`. Whether success and non-null content are both promised. |
| `IsSuccessStatusCode` | [bool] | `false`. Whether the supplied status is 200–299. |
| `IsSuccessful` | [bool] | `false`. Whether status succeeds and no error occurred. |
| `IsReceived` | [bool] | `false`. Whether a reply arrived. |
| `StatusCode` | [HttpStatusCode], nullable | `null`. Reply status for the scenario. |
| `ReasonPhrase` | [string], nullable | `null`. Reply reason phrase. |
| `Version` | [Version], nullable | `null`. HTTP version. |
| `Headers` | [HttpResponseHeaders], nullable | `null`. Reply header collection. |
| `ContentHeaders` | [HttpContentHeaders], nullable | `null`. Body header collection. |
| `RequestMessage` | [HttpRequestMessage], nullable | `null`. Associated request. |
| `Error` | [ApiExceptionBase], nullable | `null`. Exception for a simulated failure. |

True success/received flags need the metadata their interface contracts promise.
A true `HasContent` or `IsSuccessfulWithContent` needs non-null content.
The stub does not enforce these promises. Incorrect combinations can mislead your code and the compiler.

## Select an error kind

`HasRequestError(out ApiRequestException?)` checks whether `Error` is a request-phase exception.
`HasResponseError(out ApiException?)` checks for a response/body error, including `ValidationApiException`.
Each returns false and assigns null for the other kind or a missing error.
A true result supplies a non-null out value. These methods inspect `Error`, regardless of your status flags.

The [error examples](#response-and-error-examples) create and check both error kinds. Read [error construction](../results/errors.md) for their constructors.
`Dispose()` does nothing. It does not dispose any request, response or resource you assigned.
The test must dispose those resources itself.

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StubApiResponse<T>()` | Creates an independently configurable response wrapper for a test scenario. | None. `T` is the body type. | A stub with the defaults above. |
| `HasRequestError(out ApiRequestException? error)` | Tests whether this stub represents a transport failure before a response arrived. | [ApiRequestException] `error`: receives the request-phase error or `null`. | [bool]: `true` exactly when `Error` is an [`ApiRequestException`](../results/errors.md); the output is non-null on success. |
| `HasResponseError(out ApiException? error)` | Tests whether this stub represents an HTTP or body-reading response failure. | [ApiException] `error`: receives the response-phase error or `null`. | [bool]: `true` exactly when `Error` is an [`ApiException`](../results/errors.md), including [`ValidationApiException`](../results/errors.md); the output is non-null on success. |
| `Dispose()` | Satisfies the response-wrapper disposal contract without owning assigned resources. | None. | `void`; does not dispose any assigned resource. |

Source: [StubApiResponse.cs](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubApiResponse.cs).

[bool]: https://learn.microsoft.com/dotnet/api/system.boolean
[string]: https://learn.microsoft.com/dotnet/api/system.string
[Version]: https://learn.microsoft.com/dotnet/api/system.version
[HttpStatusCode]: https://learn.microsoft.com/dotnet/api/system.net.httpstatuscode
[HttpResponseHeaders]: https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpresponseheaders
[HttpContentHeaders]: https://learn.microsoft.com/dotnet/api/system.net.http.headers.httpcontentheaders
[HttpRequestMessage]: https://learn.microsoft.com/dotnet/api/system.net.http.httprequestmessage
[ApiExceptionBase]: ../results/errors.md
[ApiException]: ../results/errors.md
[ApiRequestException]: ../results/errors.md

## JSON metadata boundary

The stub does not serialize `T`, so creating it requires no JSON context.
If the code under test later serializes the model, register that model with `[JsonSerializable]`
on a `JsonSerializerContext` and configure a source-generated serializer there.
For an end-to-end `StubHttp` test, repeat [the full generated client setup](index.md#make-your-first-test).
A response stub alone does not test trimming or native AOT execution.

## Response and error examples

**A successful response.** Suppose your app has this method. It reads a response through `IApiResponse<T>`.


```csharp
static string Greet(IApiResponse<TestingPerson> response) =>
    response.IsSuccessfulWithContent ? $"Hello, {response.Content.Name}" : "The person could not be loaded.";
```

The test gives it a stub in the success state. The headers and version come from a real `HttpResponseMessage`.


```csharp
using HttpResponseMessage message = new(HttpStatusCode.OK);
using StubApiResponse<TestingPerson> response = new()
{
    Content = new TestingPerson(1, "Ada"),
    HasContent = true,
    IsSuccessfulWithContent = true,
    IsSuccessStatusCode = true,
    IsSuccessful = true,
    IsReceived = true,
    StatusCode = message.StatusCode,
    Headers = message.Headers,
    ContentHeaders = message.Content.Headers,
    Version = message.Version,
};

string greeting = Greet(response);

Assert.Equal("Hello, Ada", greeting);
```

**A request error.** An `ApiRequestException` means the call failed before a response arrived.


```csharp
using HttpRequestMessage request = new(HttpMethod.Get, "https://api.example.com/people/1");
ApiRequestException sendError = new("Connection failed.", request, request.Method, new RefitSettings());
using StubApiResponse<TestingPerson> response = new() { Error = sendError };

bool failedToSend = response.HasRequestError(out ApiRequestException? error);

Assert.True(failedToSend);
Assert.Same(sendError, error);
```

**A response error.** An `ApiException` means the server replied with a failure.


```csharp
using HttpRequestMessage request = new(HttpMethod.Get, "https://api.example.com/people/1");
using HttpResponseMessage rejected = new(HttpStatusCode.BadRequest) { RequestMessage = request };
ApiException replyError = await ApiException.Create(request, request.Method, rejected, new RefitSettings());
using StubApiResponse<TestingPerson> response = new()
{
    Error = replyError,
    IsReceived = true,
    StatusCode = rejected.StatusCode,
    Headers = rejected.Headers,
    Version = rejected.Version,
};

bool rejectedByServer = response.HasResponseError(out ApiException? error);

Assert.True(rejectedByServer);
Assert.NotNull(error);
Assert.Equal(HttpStatusCode.BadRequest, error.StatusCode);
```
