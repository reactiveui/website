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
registers `TestingPerson` with `[JsonSerializable(typeof(TestingPerson))]` on `TestingJsonContext`.
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
| `Content(body)` | The exact `HttpContent` object, with status 200. |
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
