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

**2. Supply real header collections if needed.** The runnable example creates an `HttpResponseMessage`
and assigns its headers, request and version to the stub. The test owns those disposable messages.

**3. Read through `IApiResponse<T>`.** The interface's success contracts tell the compiler when content
or metadata is non-null. The concrete stub properties do not carry those narrowing attributes.
The [response-stub example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs)
assigns every property and asserts the guarded content.
Its `PersonJson` constant holds `{"id":1,"name":"Ada"}`.

| Property | What the test supplies |
| --- | --- |
| `Content` | Typed body or default. |
| `HasContent` | Whether the test promises non-null content. |
| `IsSuccessfulWithContent` | Whether success and non-null content are both promised. |
| `IsSuccessStatusCode` | Whether the supplied status is 200–299. |
| `IsSuccessful` | Whether status succeeds and no error occurred. |
| `IsReceived` | Whether a reply arrived. |
| `StatusCode`, `ReasonPhrase`, `Version` | Reply metadata, or null when absent. |
| `Headers`, `ContentHeaders` | Reply and body header collections, or null. |
| `RequestMessage` | Associated request, or null. |
| `Error` | `ApiExceptionBase` for a simulated failure, or null. |

True success/received flags need the metadata their interface contracts promise.
A true `HasContent` or `IsSuccessfulWithContent` needs non-null content.
The stub does not enforce these promises. Incorrect combinations can mislead your code and the compiler.

## Select an error kind

`HasRequestError(out ApiRequestException?)` checks whether `Error` is a request-phase exception.
`HasResponseError(out ApiException?)` checks for a response/body error, including `ValidationApiException`.
Each returns false and assigns null for the other kind or a missing error.
A true result supplies a non-null out value. These methods inspect `Error`, regardless of your status flags.

The [runnable example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs)
creates and checks both error kinds. Read [error construction](../results/errors.md) for their constructors.
`Dispose()` does nothing. It does not dispose any request, response or resource you assigned.
The test must dispose those resources itself.

## JSON metadata boundary

The stub does not serialize `T`, so creating it requires no JSON context.
If the code under test later serializes the model, register that model with `[JsonSerializable]`
on a `JsonSerializerContext` and configure a source-generated serializer there.
For an end-to-end `StubHttp` test, repeat [the full generated client setup](index.md#make-your-first-test).
A response stub alone does not test trimming or native AOT execution.

## Response and error excerpts

The success excerpt sets every property. `CheckResponse` reads through the interface.


```csharp
using HttpRequestMessage request = new(HttpMethod.Get, "https://people.example/people/1");
using HttpResponseMessage message = new(HttpStatusCode.OK) { RequestMessage = request, Content = new StringContent(PersonJson), };
StubApiResponse<TestingPerson> stub = new()
{
    Content = new(1, PersonName),
    HasContent = true,
    IsSuccessfulWithContent = true,
    Headers = message.Headers,
    ContentHeaders = message.Content.Headers,
    IsSuccessStatusCode = true,
    IsSuccessful = true,
    IsReceived = true,
    StatusCode = message.StatusCode,
    ReasonPhrase = message.ReasonPhrase,
    RequestMessage = request,
    Version = message.Version,
    Error = null,
};
CheckResponse(stub);

SampleCheck.Equal(false, stub.HasRequestError(out _));
SampleCheck.Equal(false, stub.HasResponseError(out _));
stub.Dispose();
```


```csharp
private static void CheckResponse(IApiResponse<TestingPerson> response)
{
    if (response.IsSuccessfulWithContent)
    {
        SampleCheck.Equal(PersonName, response.Content.Name);
    }
}
```

The error excerpt checks the out values for request and response failures.


```csharp
ApiRequestException sendError = new("Connection failed.", request, request.Method, CreateSettings());
using StubApiResponse<TestingPerson> failed = new() { Error = sendError };
SampleCheck.Equal(true, failed.HasRequestError(out ApiRequestException? captured));
SampleCheck.Equal(sendError, captured);
using HttpResponseMessage rejected = new(HttpStatusCode.BadRequest) { RequestMessage = request };
ApiException replyError = await ApiException.Create(request, request.Method, rejected, CreateSettings());
using StubApiResponse<TestingPerson> refused = new()
{
    Error = replyError,
    IsReceived = true,
    StatusCode = HttpStatusCode.BadRequest,
    Headers = rejected.Headers,
    ContentHeaders = rejected.Content.Headers,
    Version = rejected.Version,
    ReasonPhrase = rejected.ReasonPhrase,
    RequestMessage = request,
};
SampleCheck.Equal(true, refused.HasResponseError(out ApiException? capturedReply));
SampleCheck.Equal(replyError, capturedReply);
```
