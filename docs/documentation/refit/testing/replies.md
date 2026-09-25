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

The example's `PersonJson` constant holds `{"id":1,"name":"Ada"}`.

| Method | Body and status |
| --- | --- |
| `With<T>(body)` / `With<T>(body, status)` | Serialize the typed body with the adopted serializer. Status 200 or your supplied status. |
| `Json(body)` / `Json(body, status)` | UTF-8 text with `application/json`. Status 200 or your supplied status. JSON validity is not checked. |
| `Text(body)` / `Text(body, contentType)` | UTF-8 text with `text/plain` or your supplied media type. Status 200. |
| `Status(statusCode)` | The supplied status with no explicit body. |
| `Content(body)` | The exact [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) object, with status 200. |
| `From(responder)` | Your lambda returns the complete response. Both sync and async overloads receive the request. |

Raw HTTP examples need no model metadata. They create replies directly.
The [runnable reply example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs)
sends each factory's route and checks its status or content.
Its asynchronous responder reads the request body before returning a response.
Responders receive no cancellation token. Pass a token from your test through a captured lambda if it needs one.
The handler fills in `RequestMessage` when your response has none.

## Response property precedence

You can also construct `new StubResponse` with init properties.
Its `Status` defaults to OK. Its optional properties are `Json`, `Text`, `ContentType`, `Content`,
`Responder` and `ResponderAsync`. The factory methods select the usual shapes for you.

`ResponderAsync` takes precedence over `Responder`. Either responder controls the entire reply,
including status. Without a responder, content uses this order: explicit `Content`, typed body from `With`,
`Json`, then `Text`. `ContentType` applies only to `Text`.
The runnable example configures conflicting properties and checks that the async responder wins.

## Dispose content and allocate repeated replies

Dispose every received `HttpResponseMessage` or Refit response wrapper.
`Reply.Content` keeps the same content object. A reusable route can return that disposed object on a later call.
For repeated requests, prefer `Reply.From` and allocate a fresh message and content inside its lambda.
Typed, JSON and text replies allocate fresh content per response.
A custom responder must also allocate a fresh response each time unless you carefully manage its ownership.

The serializer is stored on the handler, rather than on each route.
A later `ToSettings` call can change how existing typed replies are written.
Keep one serializer configuration per handler.

## Factory and property excerpts

The factory excerpt chooses each body kind and shows both responder overloads.
The complete sample sends the remaining routes after this excerpt.


```csharp
using StubHttp http = new()
{
    {
        Route.Get("/json"),
        Reply.Json(PersonJson)
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
        Reply.From(static request => new(HttpStatusCode.Accepted) { Content = new StringContent(request.RequestUri!.AbsolutePath) })
    },
    {
        Route.Post("/async"),
        Reply.From(static async request => new(HttpStatusCode.OK) { Content = new StringContent(await request.Content!.ReadAsStringAsync()) })
    },
};
using HttpClient client = CreateClient(http);
using HttpResponseMessage html = await client.GetAsync(new Uri("https://people.example/html"));
SampleCheck.Equal("text/html", html.Content.Headers.ContentType?.MediaType);
using HttpResponseMessage echoed = await client.PostAsync(new Uri("https://people.example/async"), new StringContent(PersonName));
SampleCheck.Equal(PersonName, await echoed.Content.ReadAsStringAsync());
```

This property excerpt proves that `ResponderAsync` wins when both responders and other bodies are configured.


```csharp
StubResponse properties = new()
{
    Status = HttpStatusCode.Created,
    Json = "{}",
    Text = "text",
    ContentType = "text/plain",
    Content = new StringContent("explicit"),
    Responder = static _ => new(HttpStatusCode.Accepted),
    ResponderAsync = static _ => Task.FromResult(new HttpResponseMessage(HttpStatusCode.NoContent)),
};
SampleCheck.Equal(HttpStatusCode.Created, properties.Status);
SampleCheck.Equal(true, properties.ResponderAsync is not null);
using StubHttp http = new() { { Route.Get("/precedence"), properties } };
using HttpClient client = CreateClient(http);
using HttpResponseMessage precedence = await client.GetAsync(new Uri("https://people.example/precedence"));
SampleCheck.Equal(HttpStatusCode.NoContent, precedence.StatusCode);
properties.Content.Dispose();
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
| [`StubResponse()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Creates an empty response description that you can configure with init properties. | None | Creates a [`StubResponse`](replies.md) with [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode). |
| [`StubResponse.Status`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Chooses the status for a property-based reply. | [`HttpStatusCode`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode), init-only; default [`HttpStatusCode.OK`](https://learn.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode) | Sets the response status unless a responder supplies the complete response. |
| [`StubResponse.Json`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies the raw JSON alternative to a typed or explicit body. | Nullable [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), init-only; default `null` | Supplies raw JSON text. |
| [`StubResponse.Text`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies a raw text alternative to a typed or explicit body. | Nullable [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), init-only; default `null` | Supplies raw text. |
| [`StubResponse.ContentType`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Chooses the media type used when `Text` supplies the body. | Nullable [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), init-only; default `null` | Sets media type for `Text`; it does not alter JSON or explicit content. |
| [`StubResponse.Content`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies an exact content object in preference to text and JSON. | Nullable [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent), init-only; default `null` | Supplies exact content and takes precedence over JSON/text bodies. |
| [`StubResponse.Responder`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies the whole response through a synchronous callback. | Nullable [`Func<HttpRequestMessage, HttpResponseMessage>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-2), init-only; default `null` | Supplies a complete [`HttpResponseMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage) synchronously. |
| [`StubResponse.ResponderAsync`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubResponse.cs) | Supplies the whole response through an asynchronous callback. | Nullable [`Func<HttpRequestMessage, Task<HttpResponseMessage>>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-2), with [`Task<TResult>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1) result; init-only; default `null` | Supplies a complete [`HttpResponseMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpresponsemessage) asynchronously and takes precedence over `Responder`. |
