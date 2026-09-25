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
Here the GET and DELETE expectations answer their requests. The fallback answers the request that matches neither.
`disposeHandler: false` leaves the handler for its own `using` to dispose.


```csharp
using StubHttp http = new()
{
    { Route.Fallback(), Reply.Status(HttpStatusCode.NotFound) },
    { Route.Get("/people/{id}"), Reply.Json("""{"id":1,"name":"Ada"}""") },
    { Route.Delete("/people/{id}"), Reply.Status(HttpStatusCode.NoContent) },
};
using HttpClient httpClient = new(http, disposeHandler: false);

using HttpResponseMessage found = await httpClient.GetAsync(new Uri("https://api.example.com/people/1"));
using HttpResponseMessage deleted = await httpClient.DeleteAsync(new Uri("https://api.example.com/people/1"));
using HttpResponseMessage missing = await httpClient.GetAsync(new Uri("https://api.example.com/orders/1"));

Assert.Equal(HttpStatusCode.OK, found.StatusCode);
Assert.Equal(HttpStatusCode.NoContent, deleted.StatusCode);
Assert.Equal(HttpStatusCode.NotFound, missing.StatusCode);
http.VerifyAllCalled();
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

The [constraint example](#check-every-condition) sets every matching property and sends a form body that satisfies them.

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
Expected behavior: two expected `tag=books` pairs require two actual `tag=books` pairs.
Actual behavior: `tag=books&page=2` passes because the count is two and `tag=books` is present.
The same discrepancy affects `ExactQuery`. This test shows the actual behavior:


```csharp
using StubHttp http = new()
{
    {
        new RouteMatcher { Template = "/search", ExactQueryParams = [("tag", "books"), ("tag", "books")] },
        Reply.Text("matched")
    },
};
using HttpClient httpClient = new(http, disposeHandler: false);

using HttpResponseMessage response = await httpClient.GetAsync(new Uri("https://api.example.com/search?tag=books&page=2"));

Assert.Equal("matched", await response.Content.ReadAsStringAsync()); // matched, with one tag=books instead of two
```

Avoid duplicate exact-query expectations. If duplicates matter, parse and count pairs in `Where`.
Also send one-shot tests serially. Matching and consumption are separate steps.
Concurrent requests can both select the same one-shot route before either consumes it.
That is a race: the result depends on how requests overlap.

## Check every condition

This test sets every matching condition and sends a form body that satisfies them all.
The body is raw form data, so no serializer metadata is needed.


```csharp
RouteMatcher route = new()
{
    Method = HttpMethod.Post,
    Template = "/people/{id}",
    Query = [("notify", "true")],
    ExactQuery = "notify=true&source=web",
    ExactQueryParams = [("source", "web"), ("notify", "true")],
    Headers = [("X-Api-Key", "test-key"), ("Content-Type", "application/x-www-form-urlencoded")],
    Body = "name=Ada+Lovelace&city=London",
    FormData = [("name", "Ada Lovelace")],
    Where = static request => request.RequestUri!.Host == "api.example.com",
    WhereAsync = static async request => (await request.Content!.ReadAsStringAsync()).Contains("London", StringComparison.Ordinal),
};
using StubHttp http = new() { { route, Reply.Text("updated") } };
using HttpClient httpClient = new(http, disposeHandler: false);
using HttpRequestMessage request = new(HttpMethod.Post, "https://api.example.com/people/7?notify=true&source=web")
{
    Content = new FormUrlEncodedContent([new("name", "Ada Lovelace"), new("city", "London")]),
};
request.Headers.Add("X-Api-Key", "test-key");

using HttpResponseMessage response = await httpClient.SendAsync(request);

Assert.Equal("updated", await response.Content.ReadAsStringAsync());
http.VerifyAllCalled();
```

## List the configured routes

`StubHttp` implements `IEnumerable<RouteMatcher>`, so LINQ works on it.
Both enumeration interfaces return a snapshot of the configured routes.


```csharp
using StubHttp http = new()
{
    { Route.Get("/people/{id}"), Reply.Status(HttpStatusCode.OK) },
    { Route.Post("/people"), Reply.Status(HttpStatusCode.Created) },
};

string[] templates = http.Select(static route => route.Template).ToArray();

Assert.Equal(new[] { "/people/{id}", "/people" }, templates);
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
