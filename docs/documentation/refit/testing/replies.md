---
Order: 2
---
# Supply test replies

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/testing-replies/testing-replies.csproj).

To test how your app handles a service response, you need control over what comes back.
A reply is what a stub entry sends back when its [route](routes.md) matches. The `Reply` helpers let you
return a model, a status code, raw text, a stream or a reply built from the incoming request.

Use them to give a successful call realistic data, reproduce an error body, or see how your
code handles an empty response. This page starts with a typed model and builds from there.

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

You need replies of different shapes: JSON, an error body, plain text, raw bytes, a bare status, or a reply
that depends on the request. This [`Testing.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs)
sample sets up one route for each shape and sends a request to each.

[//]: # "excerpt:Testing/Testing.cs#ShowRepliesAsync"

```csharp
using StubHttp http = new StubHttp
{
    {
        Route.Get("/json"),
        Reply.Json("{\"id\":1,\"name\":\"Ada\"}")
    },
    {
        Route.Get("/rejected"),
        Reply.Json("{\"error\":\"invalid\"}", HttpStatusCode.BadRequest)
    },
    {
        Route.Get("/text"),
        Reply.Text("hello")
    },
    {
        Route.Get("/html"),
        Reply.Text("<p>hello</p>", "text/html")
    },
    {
        Route.Get("/content"),
        Reply.Content(new ByteArrayContent([1]))
    },
    {
        Route.Get("/status"),
        Reply.Status(HttpStatusCode.NoContent)
    },
    {
        Route.Get("/sync"),
        Reply.From(static request => new HttpResponseMessage(HttpStatusCode.Accepted) { Content = new StringContent(request.RequestUri!.AbsolutePath) })
    },
    {
        Route.Post("/async"),
        Reply.From(static async request => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(await request.Content!.ReadAsStringAsync()) })
    },
};
using HttpClient httpClient = new HttpClient(http, disposeHandler: false);

using HttpResponseMessage json = await httpClient.GetAsync(new Uri("https://api.example.com/json")); // json.StatusCode == OK
using HttpResponseMessage rejected = await httpClient.GetAsync(new Uri("https://api.example.com/rejected")); // rejected.StatusCode == BadRequest
using HttpResponseMessage text = await httpClient.GetAsync(new Uri("https://api.example.com/text")); // text body == "hello"
using HttpResponseMessage html = await httpClient.GetAsync(new Uri("https://api.example.com/html")); // html content type == "text/html"
using HttpResponseMessage content = await httpClient.GetAsync(new Uri("https://api.example.com/content")); // content.StatusCode == OK
using HttpResponseMessage status = await httpClient.GetAsync(new Uri("https://api.example.com/status")); // status.StatusCode == NoContent
using HttpResponseMessage sync = await httpClient.GetAsync(new Uri("https://api.example.com/sync")); // sync body == "/sync"
using HttpResponseMessage echoed = await httpClient.PostAsync(new Uri("https://api.example.com/async"), new StringContent("Ada")); // echoed body == "Ada"
```

The sample sends through a plain `HttpClient`, so it needs no model metadata. Each reply works the same way
behind a Refit client.

| Method | Body and status |
| --- | --- |
| `With<T>(body)` / `With<T>(body, status)` | Serialize the typed body with the adopted serializer. Status 200 or your supplied status. |
| `Json(body)` / `Json(body, status)` | UTF-8 text with `application/json`. Status 200 or your supplied status. JSON validity is not checked. |
| `Text(body)` / `Text(body, contentType)` | UTF-8 text with `text/plain` or your supplied media type. Status 200. |
| `Status(statusCode)` | The supplied status with no explicit body. |
| `Content(body)` | The exact [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) object, with status 200. |
| `From(responder)` | Your lambda returns the complete response. The sync and async overloads receive the request. A third overload also receives the send's `CancellationToken`. |
| `Stream(source)` | A streamed body that your test controls through a stream source. Status 200. |
| `JsonLines<T>(items)` / `ServerSentEvents<T>(items)` | A ready-made stream of the items, in JSON Lines or server-sent-events format. Status 200. |

The two `From` lambdas in the sample capture nothing, so they are marked `static`.
The async one reads the request body and sends it back.
The handler fills in `RequestMessage` when your response has none.

Use `Reply.From((request, cancellationToken) => ...)` when your lambda reads a streaming upload.
Pass the token to each read so that cancelling the call stops it. [Read an upload one line at a time](streaming.md#read-an-upload-one-line-at-a-time) shows it.

A stream source is a `StreamSource` object. Your test uses it to release items into a streamed reply one at a time.
[Receive a stream](streaming.md#receive-a-stream) covers `Reply.Stream`, `Reply.JsonLines` and `Reply.ServerSentEvents`.

## Response property precedence

You can also construct `new StubResponse` with init properties.
Its `Status` defaults to OK. Its optional properties are `Json`, `Text`, `ContentType`, `Content`,
`Responder`, `ResponderAsync` and `CancellableResponderAsync`. The factory methods select the usual shapes for you.

When one `StubResponse` sets several of these at once, you need to know which one wins.
This [`Testing.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs)
sample sets all of them but `CancellableResponderAsync`.

[//]: # "excerpt:Testing/Testing.cs#ShowReplyPropertiesAsync"

```csharp
using StringContent explicitContent = new StringContent("explicit");
StubResponse properties = new StubResponse
{
    Status = HttpStatusCode.Created,
    Json = "{}",
    Text = "text",
    ContentType = "text/plain",
    Content = explicitContent,
    Responder = static _ => new HttpResponseMessage(HttpStatusCode.Accepted),
    ResponderAsync = static _ => Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent)),
};
using StubHttp http = new StubHttp { { Route.Get("/precedence"), properties } };
using HttpClient httpClient = new HttpClient(http, disposeHandler: false);
using HttpResponseMessage precedence = await httpClient.GetAsync(new Uri("https://api.example.com/precedence")); // precedence.StatusCode == NoContent, ResponderAsync wins
```

The reply has status 204 No Content, so `ResponderAsync` wins.
`CancellableResponderAsync` takes precedence over `ResponderAsync`, which takes precedence over `Responder`.
Any responder controls the entire reply, including status. Without a responder, the body uses this order:
explicit `Content`, then the typed or streamed body from `With`, `Stream`, `JsonLines` or `ServerSentEvents`,
then `Json`, then `Text`. `ContentType` applies only to `Text`.
Set one body property per reply, so the test reads plainly.

## Dispose content and allocate repeated replies

Dispose every received `HttpResponseMessage` or Refit response wrapper.
`Reply.Content` keeps the same content object. A reusable route can return that disposed object on a later call.
For repeated requests, prefer `Reply.From` and allocate a fresh message and content inside its lambda.
Typed, JSON and text replies allocate fresh content per response.
A custom responder must also allocate a fresh response each time unless you carefully manage its ownership.

The serializer is stored on the handler, rather than on each route.
A later `ToSettings` call can change how existing typed replies are written.
Keep one serializer configuration per handler.

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
| [`Reply.Stream(StreamSource source)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a streamed reply whose body your test releases through a stream source. | `source`: [`StreamSource`](streaming.md#receive-a-stream) that supplies the body | Returns [`StubResponse`](replies.md) with status [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode) and the source's media type; a null `source` throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception). |
| [`Reply.JsonLines<T>(IEnumerable<T> items)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a finished JSON Lines stream from a fixed set of items. | `items`: [`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) to write, one per line | Returns [`StubResponse`](replies.md) that writes every item with the handler serializer, then ends the stream; a null `items` throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception). |
| [`Reply.ServerSentEvents<T>(IEnumerable<T> items)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Reply.cs) | Creates a finished server-sent-events stream from a fixed set of items. | `items`: [`IEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) to write, one event each | Returns [`StubResponse`](replies.md) with `text/event-stream` content that writes every item, then ends the stream; a null `items` throws [`ArgumentNullException`](https://learn.microsoft.com/dotnet/api/system.argumentnullexception). |
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
