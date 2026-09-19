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

## API reference

| API | Description | Parameters or value | Returns and behavior |
| --- | --- | --- | --- |
| [`Route`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Provides static factories for common request matchers. | Static class; do not create an instance. | Each factory returns a configured [`RouteMatcher`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs). |
| [`Route.Any(string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a path regardless of its HTTP method. | `template`: a relative or absolute path template; a complete `{name}` segment matches one path segment. | Returns a [`RouteMatcher`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) with no method restriction. |
| [`Route.Get(string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a `GET` request for a path. | `template`: the relative or absolute path template to match. | Returns a matcher whose method is `GET`. |
| [`Route.Post(string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a `POST` request for a path. | `template`: the relative or absolute path template to match. | Returns a matcher whose method is `POST`. |
| [`Route.Put(string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a `PUT` request for a path. | `template`: the relative or absolute path template to match. | Returns a matcher whose method is `PUT`. |
| [`Route.Delete(string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a `DELETE` request for a path. | `template`: the relative or absolute path template to match. | Returns a matcher whose method is `DELETE`. |
| [`Route.Patch(string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a `PATCH` request for a path. | `template`: the relative or absolute path template to match. | Returns a matcher whose method is `PATCH`. |
| [`Route.Head(string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a `HEAD` request for a path. | `template`: the relative or absolute path template to match. | Returns a matcher whose method is `HEAD`. |
| [`Route.For(HttpMethod method, string template)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Matches a path for an HTTP method that has no convenience factory, such as `OPTIONS`. | `method`: the [`HttpMethod`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpmethod) to require; `template`: the relative or absolute path template to match. | Returns a matcher for the supplied method and template. |
| [`Route.Fallback()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/Route.cs) | Creates a catch-all route tried after every one-shot and reusable route. | None. | Returns a matcher with `Template` set to `"*"` and `Fallback` set to `true`; it may match repeatedly. |
| [`RouteMatcher`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Describes the request that a route table entry accepts. | Set `Template` and any init-only conditions in an object initializer. | A configured matcher is paired with a `Reply` in [`StubHttp`](index.md#make-your-first-test). |
| [`RouteMatcher()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Creates a matcher for custom conditions. | None. | Returns a matcher with optional conditions unset. Set its required `Template` before it is added to a route table. |
| [`RouteMatcher.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Restricts a matcher to one HTTP method. | Init-only [`HttpMethod?`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpmethod); `null` is the default. | A non-null value must equal the request method. `null` accepts every method. |
| [`RouteMatcher.Template`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Supplies the path pattern every matcher needs. | Required init-only [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string): a relative or absolute path, or `"*"` for every path. | The handler matches this template against the request URI. |
| [`RouteMatcher.Query`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Requires selected decoded query pairs. | Init-only nullable array of `(string Key, string Value)` pairs to find. | Every supplied pair must occur; the request may contain other pairs. |
| [`RouteMatcher.ExactQuery`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Requires the complete decoded query from encoded text. | Init-only nullable [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) without a leading `?`. | Requires the same decoded pair count and members, ignoring order. |
| [`RouteMatcher.ExactQueryParams`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Requires the complete decoded query from named pairs. | Init-only nullable array of `(string Key, string Value)` pairs. | Requires the same pair count and members, ignoring order. |
| [`RouteMatcher.Headers`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Requires selected request or content headers. | Init-only nullable array of `(string Name, string Value)` pairs. | Every supplied header name and value must occur. |
| [`RouteMatcher.Body`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Requires an exact text request body. | Init-only nullable [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) containing the expected body. | The request body must equal the value. Missing content is an empty string. |
| [`RouteMatcher.FormData`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Requires selected decoded form fields. | Init-only nullable array of `(string Key, string Value)` pairs to find in the body. | Every supplied form pair must occur; extra pairs and the media type are ignored. |
| [`RouteMatcher.Where`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Adds a synchronous check for details the built-in properties do not cover. | Init-only nullable [`Func<HttpRequestMessage, bool>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-2); its [`HttpRequestMessage`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httprequestmessage) argument is the request being matched. | The route matches only when the predicate returns `true`. |
| [`RouteMatcher.WhereAsync`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Adds an asynchronous check, such as one that reads the request body. | Init-only nullable [`Func<HttpRequestMessage, Task<bool>>`](https://learn.microsoft.com/en-us/dotnet/api/system.func-2); use [`Task`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task) to return the result. | The route matches only when the task completes with `true`, after `Where` passes. |
| [`RouteMatcher.Reusable`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Makes a route available for repeated background behavior. | Init-only [`bool`](https://learn.microsoft.com/en-us/dotnet/api/system.boolean); default `false`. | `true` allows repeated matches and excludes the route from `VerifyAllCalled`. |
| [`RouteMatcher.Fallback`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) | Makes a route the final match attempt. | Init-only [`bool`](https://learn.microsoft.com/en-us/dotnet/api/system.boolean); default `false`. | `true` gives the route fallback priority, allows repeated matches, and excludes it from `VerifyAllCalled`. |
| [`StubHttp.GetEnumerator()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) *(explicit `IEnumerable<RouteMatcher>`)* | Lets you enumerate configured matchers as [`RouteMatcher`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/RouteMatcher.cs) values. | None; cast `StubHttp` to [`IEnumerable<RouteMatcher>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) to call it. | Returns an [`IEnumerator<RouteMatcher>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerator-1) over a snapshot of the route table. |
| [`StubHttp.GetEnumerator()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Testing/StubHttp.cs) *(explicit `IEnumerable`)* | Lets non-generic code enumerate the configured matchers. | None; cast `StubHttp` to [`IEnumerable`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable) to call it. | Returns a non-generic [`IEnumerator`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerator) over the same route snapshot. |
