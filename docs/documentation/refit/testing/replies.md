---
Order: 2
---
# Supply test replies

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/testing-replies/testing-replies.csproj).

To test how your app handles a service response, you need control over what comes back.
The `Reply` helpers let you return a model, a status code, raw text or a reply built from
the incoming request.

Use them to give a successful call realistic data, reproduce an error body, or see how your
code handles an empty response. The examples start with a typed model and build from there.

## Return a typed model

**1. Generate JSON metadata for the body type.** The [complete setup](index.md#make-your-first-test)
registers `TestingPerson` with `[JsonSerializable(typeof(TestingPerson))]` on the sample's context class, `TestingJsonContext`.
Use `new SystemTextJsonContentSerializer` with options whose `TypeInfoResolver` is that context.
Repeat that setup when copying a standalone typed-reply test.
Register any returned collection or generic types separately. A missing registration can fail while writing the reply.

**2. Pass those settings to `CreateGeneratedClient`.** `StubHttp` adopts their serializer.
The generated factory avoids reflection fallback, making it the suitable client path for AOT.

**3. Supply the model with `Reply.With(body)`.** It returns status 200.
`Reply.With(body, status)` chooses another status. Both serialize when the handler builds the reply.
The [first test](index.md#make-your-first-test) runs both overloads and checks the typed request body.

## JSON, text and custom content

| Method | Body and status |
| --- | --- |
| `With<T>(body)` / `With<T>(body, status)` | Serialize the typed body with the adopted serializer. Status 200 or your supplied status. |
| `Json(body)` / `Json(body, status)` | UTF-8 text with `application/json`. Status 200 or your supplied status. JSON validity is not checked. |
| `Text(body)` / `Text(body, contentType)` | UTF-8 text with `text/plain` or your supplied media type. Status 200. |
| `Status(statusCode)` | The supplied status with no explicit body. |
| `Content(body)` | The exact [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) object, with status 200. |
| `From(responder)` | Your lambda returns the complete response. The sync and async overloads receive the request. A third overload also receives the send's `CancellationToken`. |

Raw HTTP examples need no model metadata. They create replies directly.
See the [reply examples](#reply-examples) for an error body and a reply built from the request.
Use `Reply.From(async (request, cancellationToken) => ...)` when your responder reads a streaming upload.
Pass the token to the read so that cancelling the call stops it.
The handler fills in `RequestMessage` when your response has none.

## Response property precedence

You can also construct `new StubResponse` with init properties.
Its `Status` defaults to OK. Its optional properties are `Json`, `Text`, `ContentType`, `Content`,
`Responder`, `ResponderAsync` and `CancellableResponderAsync`. The factory methods select the usual shapes for you.

`CancellableResponderAsync` takes precedence over `ResponderAsync`, which takes precedence over `Responder`.
Any responder controls the entire reply, including status. Without a responder, content uses this order:
explicit `Content`, typed body from `With`, `Json`, then `Text`. `ContentType` applies only to `Text`.
The [precedence example](#reply-examples) configures conflicting properties and checks that the async responder wins.

## Dispose content and allocate repeated replies

Dispose every received `HttpResponseMessage` or Refit response wrapper.
`Reply.Content` keeps the same content object. A reusable route can return that disposed object on a later call.
For repeated requests, prefer `Reply.From` and allocate a fresh message and content inside its lambda.
Typed, JSON and text replies allocate fresh content per response.
A custom responder must also allocate a fresh response each time unless you carefully manage its ownership.

The serializer is stored on the handler, rather than on each route.
A later `ToSettings` call can change how existing typed replies are written.
Keep one serializer configuration per handler.

## Reply examples

These tests send through a raw `HttpClient` built on the handler.

**An error body.** `Reply.Json(body, status)` returns raw JSON with the status you choose.


```csharp
using StubHttp http = new()
{
    { Route.Get("/people/99"), Reply.Json("""{"error":"not found"}""", HttpStatusCode.NotFound) },
};
using HttpClient httpClient = new(http, disposeHandler: false);

using HttpResponseMessage response = await httpClient.GetAsync(new Uri("https://api.example.com/people/99"));

Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
Assert.Equal("""{"error":"not found"}""", await response.Content.ReadAsStringAsync());
```

**A reply built from the request.** This async responder reads the request body and sends it back.
It creates a fresh response and content on every call.


```csharp
using StubHttp http = new()
{
    {
        Route.Post("/people"),
        Reply.From(static async request =>
        {
            string json = await request.Content!.ReadAsStringAsync();
            return new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };
        })
    },
};
using HttpClient httpClient = new(http, disposeHandler: false);
using StringContent body = new("""{"id":2,"name":"Grace"}""", Encoding.UTF8, "application/json");

using HttpResponseMessage response = await httpClient.PostAsync(new Uri("https://api.example.com/people"), body);

Assert.Equal(HttpStatusCode.Created, response.StatusCode);
Assert.Equal("""{"id":2,"name":"Grace"}""", await response.Content.ReadAsStringAsync());
```

**Property precedence.** This reply sets every property. `ResponderAsync` wins over `Responder` and every body.


```csharp
using StringContent content = new("explicit");
StubResponse reply = new()
{
    Status = HttpStatusCode.Created,
    Json = "{}",
    Text = "text",
    ContentType = "text/plain",
    Content = content,
    Responder = static _ => new HttpResponseMessage(HttpStatusCode.Accepted),
    ResponderAsync = static _ => Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent)),
};
using StubHttp http = new() { { Route.Get("/people/1"), reply } };
using HttpClient httpClient = new(http, disposeHandler: false);

using HttpResponseMessage response = await httpClient.GetAsync(new Uri("https://api.example.com/people/1"));

Assert.Equal(HttpStatusCode.NoContent, response.StatusCode); // ResponderAsync wins
```

## API reference

| API | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`Reply.With<T>(T body)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a typed successful reply using the handler's serializer. | `body`: generic type `T` | Returns [`StubResponse`](replies.md) that serializes the body with the handler serializer and uses [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode). |
| [`Reply.With<T>(T body, HttpStatusCode status)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a typed reply while choosing a non-default status. | `body`: generic type `T`; `status`: [`HttpStatusCode`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode) | Returns [`StubResponse`](replies.md) with serialized content and the supplied status. |
| [`Reply.Json(string body)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a successful reply from raw JSON text. | `body`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) JSON text | Returns [`StubResponse`](replies.md) with UTF-8 `application/json` content and status [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode). |
| [`Reply.Json(string body, HttpStatusCode status)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a raw JSON reply with a caller-selected status. | `body`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) JSON text; `status`: [`HttpStatusCode`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode) | Returns JSON [`StubResponse`](replies.md) with the supplied status. |
| [`Reply.Text(string body)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a plain-text successful reply. | `body`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) text | Returns [`StubResponse`](replies.md) with UTF-8 `text/plain` content and status [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode). |
| [`Reply.Text(string body, string contentType)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates text content with a custom media type. | `body`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) text; `contentType`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) media type | Returns UTF-8 text [`StubResponse`](replies.md) with the supplied media type and status [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode). |
| [`Reply.Status(HttpStatusCode statusCode)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a bodyless reply for a chosen status code. | `statusCode`: [`HttpStatusCode`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode) response status | Returns a [`StubResponse`](replies.md). |
| [`Reply.Content(HttpContent body)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Reuses an explicit HTTP content instance as a reply body. | `body`: [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) exact content instance | Returns [`StubResponse`](replies.md) using that content and status [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode). |
| [`Reply.From(Func<HttpRequestMessage, HttpResponseMessage> responder)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Uses a synchronous request-aware factory to build the whole reply. | `responder`: [`Func<HttpRequestMessage, HttpResponseMessage>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-2) request-to-response function | Returns [`StubResponse`](replies.md) whose responder supplies the complete [`HttpResponseMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage). |
| [`Reply.From(Func<HttpRequestMessage, Task<HttpResponseMessage>> responder)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Uses an asynchronous request-aware factory to build the whole reply. | `responder`: [`Func<HttpRequestMessage, Task<HttpResponseMessage>>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-2), with [`Task<TResult>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1) result | Returns [`StubResponse`](replies.md) whose async responder supplies the complete [`HttpResponseMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage). |
| [`Reply.From(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> responder)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Uses an asynchronous factory that also receives the send's cancellation token. | `responder`: [`Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-3) | Returns [`StubResponse`](replies.md) whose cancellable responder supplies the complete [`HttpResponseMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage). |
| [`StubResponse()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Creates an empty response description that you can configure with init properties. | None | Creates a [`StubResponse`](replies.md) with [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode). |
| [`StubResponse.Status`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Chooses the status for a property-based reply. | [`HttpStatusCode`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode), init-only; default [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode) | Sets the response status unless a responder supplies the complete response. |
| [`StubResponse.Json`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies the raw JSON alternative to a typed or explicit body. | Nullable [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), init-only; default `null` | Supplies raw JSON text. |
| [`StubResponse.Text`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies a raw text alternative to a typed or explicit body. | Nullable [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), init-only; default `null` | Supplies raw text. |
| [`StubResponse.ContentType`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Chooses the media type used when `Text` supplies the body. | Nullable [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), init-only; default `null` | Sets media type for `Text`; it does not alter JSON or explicit content. |
| [`StubResponse.Content`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies an exact content object in preference to text and JSON. | Nullable [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent), init-only; default `null` | Supplies exact content and takes precedence over JSON/text bodies. |
| [`StubResponse.Responder`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies the whole response through a synchronous callback. | Nullable [`Func<HttpRequestMessage, HttpResponseMessage>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-2), init-only; default `null` | Supplies a complete [`HttpResponseMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage) synchronously. |
| [`StubResponse.ResponderAsync`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies the whole response through an asynchronous callback. | Nullable [`Func<HttpRequestMessage, Task<HttpResponseMessage>>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-2), with [`Task<TResult>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1) result; init-only; default `null` | Supplies a complete [`HttpResponseMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage) asynchronously and takes precedence over `Responder`. |
| [`StubResponse.CancellableResponderAsync`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies the whole response through an asynchronous callback that also receives the send's cancellation token. | Nullable [`Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-3); init-only; default `null` | Supplies a complete [`HttpResponseMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage) asynchronously and takes precedence over `ResponderAsync` and `Responder`. |
