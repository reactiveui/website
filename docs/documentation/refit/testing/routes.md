---
Order: 1
---
# Match outgoing requests

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/testing-routes/testing-routes.csproj).

A test needs to know whether your app asked for the right thing. Checking only the returned
value can miss a wrong URL, a missing header or an incorrect request body.

Refit's route matchers let you describe the request you expect and choose its reply.
Start with a method and path, then add conditions for the details that matter to your test.

## Choose a method and path

**1. Add expectations.** `Route.Get`, `Post`, `Put`, `Delete`, `Patch` and `Head` select their HTTP methods.
`Route.Any` accepts any method. `Route.For` accepts an `HttpMethod`, such as OPTIONS.
Every factory accepts a path template. `Route.Fallback()` supplies a reusable catch-all.

**2. Send through `HttpClient`.** A raw client is useful for checking handler rules alone.
For a full Refit test, use the [generated client and JSON context](index.md#make-your-first-test).
Raw HTTP does not need JSON metadata unless you serialize or deserialize models.

**3. Check the status.** A fallback declared first cannot hide a one-shot expectation.


```csharp
using StubHttp http = new()
{
    {
        Route.Fallback(),
        Reply.Status(HttpStatusCode.NotFound)
    },
    {
        Route.Get("/get"),
        Reply.Status(HttpStatusCode.OK)
    },
    {
        Route.Post("/post"),
        Reply.Status(HttpStatusCode.Created)
    },
    {
        Route.Put("/put"),
        Reply.Status(HttpStatusCode.NoContent)
    },
    {
        Route.Delete("/delete"),
        Reply.Status(HttpStatusCode.NoContent)
    },
    {
        Route.Patch("/patch"),
        Reply.Status(HttpStatusCode.NoContent)
    },
    {
        Route.Head("/head"),
        Reply.Status(HttpStatusCode.OK)
    },
    {
        Route.For(HttpMethod.Options, "/options"),
        Reply.Status(HttpStatusCode.OK)
    },
    {
        Route.Any("/any"),
        Reply.Status(HttpStatusCode.Accepted)
    },
};
using HttpClient client = CreateClient(http);
using HttpResponseMessage get = await client.GetAsync(new Uri("https://people.example/get"));
SampleCheck.Equal(HttpStatusCode.OK, get.StatusCode);
using HttpResponseMessage missing = await client.GetAsync(new Uri("https://people.example/missing"));
SampleCheck.Equal(HttpStatusCode.NotFound, missing.StatusCode);
```

## Path and priority rules

`Template` is required. A leading `/` matches the request's absolute path.
An absolute template compares scheme, host and path. `"*"` accepts any path.
A complete `{name}` segment matches one nonempty segment. It does not expose a captured parameter.
An embedded placeholder, such as `person-{id}`, is literal text.
Literal segments compare case-sensitively. Segment counts and trailing slashes matter.
A query written inside a template is ignored. Set a query property to check it.

The handler tries one-shot expectations, then reusable routes, then fallbacks.
Within each group it uses declaration order. It does not rank routes by path detail.
A one-shot route is consumed after matching. Add identical routes in order to supply a sequence of replies.
`Reusable=true` allows repeated matches and removes the verification requirement.
`Fallback=true` gives the final priority and also removes the verification requirement.
A manually configured fallback must pass its other conditions. `Route.Fallback()` accepts everything.
An unmatched request throws `InvalidOperationException`; it does not return an automatic 404.

## Check queries, headers and bodies

The [constraint example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs)
sets every matching property and sends a form body that satisfies them.

| Property | Required request behavior |
| --- | --- |
| `Method` | Same HTTP method. Null accepts any method. |
| `Query` | Contains each decoded key/value pair. Extra pairs are allowed. |
| `ExactQuery` | Same decoded pairs and count as the supplied encoded query, ignoring order. Omit the leading `?`. |
| `ExactQueryParams` | Same decoded pairs and count as the supplied array, ignoring order. |
| `Headers` | Contains each name/value pair in request or content headers. Names use HTTP header lookup. Values compare exactly. Multiple values join with `", "`. |
| `Body` | Exact body text. Missing content counts as an empty string. |
| `FormData` | Contains each decoded form pair. Extra pairs are allowed. The media type is not checked. |
| `Where` | The synchronous predicate returns true. |
| `WhereAsync` | The asynchronous predicate returns true. It runs after `Where` passes. |

Query and form decoding converts `+` to a space and decodes percent escapes.
Bare keys have empty values. Empty entries between `&` characters are ignored.
Keys and values compare case-sensitively. All three query properties can be used together.
`ExactQuery` checks decoded content, despite its string argument. It does not check raw byte spelling or pair order.
Predicates run after the built-in checks and receive no cancellation token.
Body buffering makes content rereadable when buffering succeeds.

## Duplicate-query discrepancy

The implementation compares pair count and membership. It does not check how many times a pair occurs.
Expected behavior: two expected `a=1` pairs require two actual `a=1` pairs.
Actual behavior: `a=1&b=2` passes because the count is two and `a=1` is present.
The same discrepancy affects both exact-query properties.
The runnable example asserts the actual response, rather than hiding the defect:


```csharp
using StubHttp http = new()
{
    {
        new RouteMatcher { Template = "/query", ExactQueryParams = [("a", "1"), ("a", "1")] },
        Reply.Text(AcceptedReply)
    },
    {
        new RouteMatcher { Template = "/query", ExactQuery = "a=1&a=1" },
        Reply.Text(AcceptedReply)
    },
};
using HttpClient client = CreateClient(http);
using HttpResponseMessage pairs = await client.GetAsync(new Uri("https://people.example/query?a=1&b=2"));
using HttpResponseMessage encoded = await client.GetAsync(new Uri("https://people.example/query?a=1&b=2"));
SampleCheck.Equal(AcceptedReply, await pairs.Content.ReadAsStringAsync());
SampleCheck.Equal(AcceptedReply, await encoded.Content.ReadAsStringAsync());
```

Avoid duplicate exact-query expectations. If duplicates matter, parse and count pairs in `Where`.
Also send one-shot tests serially. Matching and consumption are separate steps.
Concurrent requests can both select the same one-shot route before either consumes it.
That is a race: the result depends on how requests overlap.

## Complete constraint and enumeration excerpts

The complete constraint test uses raw form data. No serializer metadata is needed for this body.


```csharp
RouteMatcher route = new()
{
    Method = HttpMethod.Post,
    Template = "/people/{id}",
    Query = [("mode", "short")],
    ExactQuery = "extra=1&mode=short",
    ExactQueryParams = [("mode", "short"), ("extra", "1")],
    Headers = [("X-Test", "yes"), ("Content-Type", "application/x-www-form-urlencoded")],
    Body = "name=Ada+Lovelace&extra=1",
    FormData = [("name", "Ada Lovelace")],
    Where = static request => request.RequestUri!.Host == "people.example",
    WhereAsync = static async request => (await request.Content!.ReadAsStringAsync()).Contains(PersonName, StringComparison.Ordinal),
    Reusable = true,
    Fallback = false,
};
using StubHttp http = new() { { route, Reply.Text("matched") } };
using HttpClient client = CreateClient(http);
using HttpRequestMessage request = new(HttpMethod.Post, "https://people.example/people/7?mode=short&extra=1")
{
    Content = new FormUrlEncodedContent([new("name", "Ada Lovelace"), new("extra", "1")]),
};
request.Headers.Add("X-Test", "yes");
using HttpResponseMessage response = await client.SendAsync(request);
SampleCheck.Equal("matched", await response.Content.ReadAsStringAsync());
Verify(http);
```

Both enumeration interfaces return snapshots of the configured routes.
The example checks each route and confirms the nine-entry table size.


```csharp
int genericCount = 0;
foreach (RouteMatcher route in (IEnumerable<RouteMatcher>)http)
{
    SampleCheck.Equal(true, route.Template.Length > 0);
    genericCount++;
}

SampleCheck.Equal(RouteCount, genericCount);
int count = 0;
foreach (object route in (IEnumerable)http)
{
    SampleCheck.Equal(true, route is RouteMatcher);
    count++;
}

SampleCheck.Equal(RouteCount, count);
```
