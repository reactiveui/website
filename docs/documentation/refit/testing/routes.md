---
Order: 1
---
# Match outgoing requests

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/testing-routes/testing-routes.csproj).

A test needs to know whether your app asked for the right thing. Checking only the returned
value can miss a wrong URL, a missing header or an incorrect request body.

A route is the rule that decides which requests a stub entry answers. A `RouteMatcher` holds that rule.
Start with a method and path, then add conditions for the details that matter to your test.
Each entry pairs a route with a [reply](replies.md), as the [first test](index.md#make-your-first-test) shows.

## Choose a method and path

You want to match a request by its HTTP method and path. You also want a safe answer for any request
you did not plan for. The [`Testing.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs)
sample sets up one route for each method and a fallback route.

[//]: # "excerpt:Testing/Testing.cs#ShowRoutesAsync"

```csharp
using StubHttp http = new StubHttp
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
        Route.Any("/any"),
        Reply.Status(HttpStatusCode.Accepted)
    },
};
using HttpClient httpClient = new HttpClient(http, disposeHandler: false);
using HttpResponseMessage get = await httpClient.GetAsync(new Uri("https://api.example.com/get")); // get.StatusCode == OK
using HttpResponseMessage post = await httpClient.PostAsync(new Uri("https://api.example.com/post"), null); // post.StatusCode == Created
using HttpResponseMessage delete = await httpClient.DeleteAsync(new Uri("https://api.example.com/any")); // delete.StatusCode == Accepted, Route.Any matches every method
using HttpResponseMessage missing = await httpClient.GetAsync(new Uri("https://api.example.com/missing")); // missing.StatusCode == NotFound, from Route.Fallback()
```

- `Route.Get`, `Post`, `Put`, `Delete`, `Patch` and `Head` each match one HTTP method.
- `Route.For` takes an `HttpMethod`, such as `HttpMethod.Options`, for a method with no factory of its own.
- `Route.Any` matches the path with any method.
- `Route.Fallback()` matches every request. A fallback route is tried only after every other route fails to match.

The fallback comes first in the list, yet the `/get` request still reaches its own route.
Only `/missing`, which no other route matches, gets the fallback's 404.
Without a fallback, an unmatched request throws `InvalidOperationException`.

The sample sends through a plain `HttpClient` built on the stub. That checks the stub's rules alone, and it
needs no JSON metadata. `disposeHandler: false` leaves the stub for its own `using` to dispose.
For a full Refit test, use the [generated client and JSON context](index.md#make-your-first-test).

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

You may need a route to match only when a request carries a certain header, form field or other detail.
Build a `RouteMatcher` yourself and set the conditions you need. The request must pass every condition you set.
This [`Testing.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs)
sample sets `Headers`, `FormData` and `Where`.

[//]: # "excerpt:Testing/Testing.cs#ShowMatchersAsync"

```csharp
RouteMatcher route = new RouteMatcher
{
    Method = HttpMethod.Post,
    Template = "/people/{id}",
    Headers = [("X-Test", "yes")],
    FormData = [("name", "Ada Lovelace")],
    Where = static request => request.RequestUri!.Host == "api.example.com",
};
using StubHttp http = new StubHttp { { route, Reply.Text("matched") } };
using HttpClient httpClient = new HttpClient(http, disposeHandler: false);
using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.example.com/people/7")
{
    Content = new FormUrlEncodedContent(
    [
        new KeyValuePair<string, string>("name", "Ada Lovelace"),
    ]),
};
request.Headers.Add("X-Test", "yes");
using HttpResponseMessage response = await httpClient.SendAsync(request); // response body == "matched"
```

`Headers` looks for `X-Test: yes`. `FormData` looks for the `name` field in the form body.
`Where` runs your lambda on the request, here to check the host. The lambda captures nothing, so it is
marked `static`. The route's reply is `"matched"` only because all three checks pass, along with the method
and path.

These are all the properties you can set on a `RouteMatcher`:

| Property | Required request behavior |
| --- | --- |
| `Template` | Required. The path pattern, described in [Path and priority rules](#path-and-priority-rules). |
| `Method` | Same HTTP method. Null accepts any method. |
| `Query` | Contains each decoded key/value pair. Extra pairs are allowed. |
| `ExactQuery` | Same decoded pairs and count as the supplied encoded query, ignoring order. Omit the leading `?`. |
| `ExactQueryParams` | Same decoded pairs and count as the supplied array, ignoring order. |
| `Headers` | Contains each name/value pair in request or content headers. Names use HTTP header lookup. Values compare exactly. Multiple values join with `", "`. |
| `Body` | Exact body text. Missing content counts as an empty string. |
| `FormData` | Contains each decoded form pair. Extra pairs are allowed. The media type is not checked. |
| `Where` | The synchronous predicate returns true. |
| `WhereAsync` | The asynchronous predicate returns true. It runs after `Where` passes. |
| `Reusable` | Not a condition. `true` lets the route answer many requests and removes it from verification. |
| `Fallback` | Not a condition. `true` makes the route a fallback, tried last. It can answer many requests and is not verified. |

The stub checks the method, the path, the query properties and `Headers` first. Then it checks `Body` and
`FormData`, then `Where`, then `WhereAsync`. It stops at the first check that fails.

Query and form decoding converts `+` to a space and decodes percent escapes.
Bare keys have empty values. Empty entries between `&` characters are ignored.
Keys and values compare case-sensitively. All three query properties can be used together.
`ExactQuery` checks decoded content, despite its string argument. It does not check raw byte spelling or pair order.
Predicates run after the built-in checks and receive no cancellation token.
Body buffering makes content rereadable when buffering succeeds.

`Body` and `FormData` read the request body, so they need the stub's default `RequestCapture.Full` setting.
With any other [`RequestCapture`](streaming.md#test-a-streaming-upload) setting, a route that sets either one
throws `InvalidOperationException` when the stub tries to match it.

## Duplicate-query discrepancy

You might expect an exact-query route for `a=1&a=1` to need two `a=1` pairs. It does not.
The stub compares the pair count and checks that each expected pair is present.
It does not count how many times a pair occurs.
This [`Testing.cs`](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Testing/Testing.cs)
sample shows the current behavior for both exact-query properties.

[//]: # "excerpt:Testing/Testing.cs#ShowDuplicateQueryAsync"

```csharp
using StubHttp http = new StubHttp
{
    {
        new RouteMatcher { Template = "/query", ExactQueryParams = [("a", "1"), ("a", "1")] },
        Reply.Text("matched ExactQueryParams")
    },
    {
        new RouteMatcher { Template = "/query", ExactQuery = "a=1&a=1" },
        Reply.Text("matched ExactQuery")
    },
};
using HttpClient httpClient = new HttpClient(http, disposeHandler: false);
using HttpResponseMessage pairs = await httpClient.GetAsync(new Uri("https://api.example.com/query?a=1&b=2")); // matched ExactQueryParams, even though "a" is not duplicated

// Each route answers once, so this second request can only match the ExactQuery route, and it accepts it too.
using HttpResponseMessage encoded = await httpClient.GetAsync(new Uri("https://api.example.com/query?a=1&b=2")); // matched ExactQuery: "a=1&a=1" also accepts a query with a single a=1
```

The request `a=1&b=2` has two pairs, and `a=1` is one of them. That satisfies the `ExactQueryParams` route,
which answers the first request. The second request can only reach the `ExactQuery` route, and it passes there too.

Avoid duplicate pairs in exact-query routes. When the number of repeats matters, parse and count the pairs in `Where`.

Send requests to a one-shot route one at a time. Two requests that arrive together can both match it before
the stub marks it used. [Two requests on one one-shot route](verification.md#two-requests-on-one-one-shot-route)
shows the result.

## List the configured routes

`StubHttp` implements `IEnumerable<RouteMatcher>`, so LINQ works on it.
For example, `http.Select(static route => route.Template)` lists each route's template.
Both enumeration interfaces return a snapshot of the configured routes.

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
