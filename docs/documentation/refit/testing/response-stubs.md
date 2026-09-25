---
Order: 5
---
# Test code that accepts a response

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/testing-response-stubs/testing-response-stubs.csproj).

Some code only deals with the result of an API call. For example, a method might take an `IApiResponse<T>`,
check its status and decide which message to show. That method never sends an HTTP request.
You can test it by handing it a response with exactly the data and flags you need.

`StubApiResponse<T>` is that response. It implements `IApiResponse<T>`, and you set every property yourself.

## When to build a response by hand

Build a `StubApiResponse<T>` when both of these are true:

- The code under test receives an `IApiResponse<T>` as a parameter or from a mock.
- The code under test never calls HTTP itself.

Use a [`StubHttp` handler](index.md) instead when the request is part of what you want to check,
or when the code calls a Refit client itself. The stub skips HTTP and JSON entirely.
It cannot stand in where the code needs a concrete `ApiResponse<T>`, because it is a different class.

## Build a success and two failures

How do you build an `IApiResponse<T>` by hand for code that never calls through a stub HTTP client?

**1. Set the content and the flags.** Every property is init-only and separate from the others.
Setting `Content` does not set `HasContent`. Setting status 200 does not set `IsSuccessful`.

**2. Set `Error` for a failure.** An `ApiRequestException` means the call failed before any reply arrived.
An `ApiException` means the server replied with a failure.

**3. Pass the stub to the code under test.** Read it through `IApiResponse<T>`, as your app code does.

Source: [`Testing.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs).

[//]: # "excerpt:Testing/Testing.cs#ShowResponseStubsAsync"

```csharp
using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://api.example.com/people/1");
using HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK) { RequestMessage = request, Content = new StringContent("{\"id\":1,\"name\":\"Ada\"}") };
using StubApiResponse<TestingPerson> stub = new StubApiResponse<TestingPerson>
{
    Content = new TestingPerson(1, "Ada"),
    HasContent = true,
    StatusCode = message.StatusCode,
    Headers = message.Headers,
    ContentHeaders = message.Content.Headers,
    Version = message.Version,
    IsSuccessStatusCode = true,
    IsSuccessful = true,
    IsSuccessfulWithContent = true,
    IsReceived = true,
};
string? name = stub.Content?.Name; // "Ada"

ApiRequestException sendError = new ApiRequestException("Connection failed.", request, request.Method, CreateSettings());
using StubApiResponse<TestingPerson> failed = new StubApiResponse<TestingPerson> { Error = sendError };
bool hasRequestError = failed.HasRequestError(out ApiRequestException? captured); // true, captured == sendError

using HttpResponseMessage rejected = new HttpResponseMessage(HttpStatusCode.BadRequest) { RequestMessage = request };
ApiException replyError = await ApiException.Create(request, request.Method, rejected, CreateSettings());
using StubApiResponse<TestingPerson> refused = new StubApiResponse<TestingPerson>
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
bool hasResponseError = refused.HasResponseError(out ApiException? capturedReply); // true, capturedReply == replyError

using ApiResponse<TestingPerson> real = new ApiResponse<TestingPerson>(message, new TestingPerson(1, "Ada"), CreateSettings());
string? realName = real.Content?.Name; // the same code reads a stub and a real ApiResponse: "Ada"
```

The sample builds three stubs and one real response.

- `stub` is a success. It sets the content, the status and the success flags. `stub.Content?.Name` is `"Ada"`.
- `failed` carries only an `ApiRequestException`. `HasRequestError` returns `true` and hands back that same exception.
- `refused` carries an `ApiException` from a `400 Bad Request` reply, with the reply's metadata copied across.
  `HasResponseError` returns `true` and hands back that exception.
- `real` is a real `ApiResponse<T>` built from `message`. The same read works on it and on the stub.

The real `ApiResponse<T>` constructor throws `ArgumentException` when the `HttpResponseMessage` has no
`RequestMessage`. That is why the sample sets `RequestMessage = request` on `message`.
A stub has no such check.

### What the success stub leaves unset

The success stub copies `Headers`, `ContentHeaders` and `Version` from `message`, because its success flags promise
them. `HttpResponseHeaders` has no public constructor, so copying from an `HttpResponseMessage` is the way to supply
real header collections. The test owns that message and disposes it.

These properties keep their defaults:

| Property | Type | Value in the success stub | What it means for you |
| --- | --- | --- | --- |
| `ReasonPhrase` | [string], nullable | `null` | The text after the status code, such as `OK`. |
| `RequestMessage` | [HttpRequestMessage], nullable | `null` | The request that led to the reply. |
| `Error` | [ApiExceptionBase], nullable | `null` | No failure. |

### Keep the flags consistent

The `IApiResponse<T>` interface tells the compiler what each flag promises.
A `true` `HasContent` or `IsSuccessfulWithContent` promises non-null `Content`.
A `true` `IsSuccessful`, `IsSuccessStatusCode` or `IsReceived` promises non-null `Headers`, `StatusCode` and `Version`.
The compiler trusts those promises when your code reads the stub through the interface.
The stub does not check them. If a stub sets `IsSuccessful` but leaves `Headers` `null`, code that reads `Headers`
after checking `IsSuccessful` compiles without a warning and then throws `NullReferenceException`.
Set every value the flags promise, as the success stub in the sample does.

The concrete stub properties carry no such promises. Read the stub through `IApiResponse<T>` to get them.

## Tell the error kinds apart

`HasRequestError(out ApiRequestException?)` checks whether `Error` is a request-phase exception.
`HasResponseError(out ApiException?)` checks for a reply error, including `ValidationApiException`.
Each returns `false` and sets its out value to `null` for the other kind or for no error.
A `true` result gives a non-null out value. Both methods look only at `Error`. They ignore your status flags.
See [error construction](../results/errors.md) for the exception constructors.

`Dispose()` does nothing. It does not dispose any request, reply or other resource you assigned.
The test disposes those itself.

## API reference

| Property | Type | Default |
| --- | --- | --- |
| `Content` | `T?` | `default(T)` |
| `HasContent` | [bool] | `false` |
| `IsSuccessfulWithContent` | [bool] | `false` |
| `IsSuccessStatusCode` | [bool] | `false` |
| `IsSuccessful` | [bool] | `false` |
| `IsReceived` | [bool] | `false` |
| `StatusCode` | [HttpStatusCode], nullable | `null` |
| `ReasonPhrase` | [string], nullable | `null` |
| `Version` | [Version], nullable | `null` |
| `Headers` | [HttpResponseHeaders], nullable | `null` |
| `ContentHeaders` | [HttpContentHeaders], nullable | `null` |
| `RequestMessage` | [HttpRequestMessage], nullable | `null` |
| `Error` | [ApiExceptionBase], nullable | `null` |

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `StubApiResponse<T>()` | Creates a response whose properties you set yourself. | None. `T` is the content type. | A stub with the defaults above. |
| `HasRequestError(out ApiRequestException? error)` | Tests whether the stub represents a failure before any reply arrived. | [ApiRequestException] `error`: receives the request-phase error or `null`. | [bool]: `true` exactly when `Error` is an [`ApiRequestException`](../results/errors.md); the out value is non-null then. |
| `HasResponseError(out ApiException? error)` | Tests whether the stub represents a reply failure. | [ApiException] `error`: receives the reply error or `null`. | [bool]: `true` exactly when `Error` is an [`ApiException`](../results/errors.md), including [`ValidationApiException`](../results/errors.md); the out value is non-null then. |
| `Dispose()` | Meets the `IDisposable` contract of `IApiResponse<T>`. | None. | `void`; disposes nothing you assigned. |

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

## JSON metadata

The stub does not serialize `T`, so creating one needs no JSON context.
If the code under test serializes the model later, register that model with `[JsonSerializable]`
on a `JsonSerializerContext` and configure a source-generated serializer there.
For an end-to-end `StubHttp` test, repeat [the full generated client setup](index.md#make-your-first-test).
A response stub alone does not test trimming or native AOT.
