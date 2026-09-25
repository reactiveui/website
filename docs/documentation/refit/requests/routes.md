---
Order: 1
---
# Routes and HTTP methods

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/requests-routes/requests-routes.csproj).

When your app asks for a person or saves a change, the service needs to know which operation
to perform and which resource to use. An HTTP method, such as GET or POST, identifies the
operation. A route supplies the path, such as `/people/1`.

In Refit, you describe both on your interface method. Refit fills in changing parts of the
path from the arguments you pass, so the call can stay as simple as `GetAsync(id)`.

## Build your first request

**1. Describe the routes.** These examples return `Task<HttpRequestMessage>`.
That return type asks Refit to build the request and hand it to you without sending it.
`Person` is the reply and body type from [the first request](../index.md#your-first-request).

```csharp
[PathPrefix("/v1")]
internal interface IRoutesApi
{
    [Get("/people/{id}")]
    Task<HttpRequestMessage> GetAsync(int id);

    [Post("/people")]
    Task<HttpRequestMessage> PostAsync([Body] Person person);

    [Put("/people/{id}")]
    Task<HttpRequestMessage> PutAsync(int id, [Body] Person person);

    [Patch("/people/{id}")]
    Task<HttpRequestMessage> PatchAsync(int id, [Body] Person person);

    [Delete("/people/{id}")]
    Task<HttpRequestMessage> DeleteAsync(int id);

    [Head("/people/{id}")]
    Task<HttpRequestMessage> HeadAsync(int id);

    [Options("/people")]
    Task<HttpRequestMessage> OptionsAsync();

    [Get("/people/{id?}")]
    Task<HttpRequestMessage> OptionalAsync(int? id);

    [Get("/files/{**path}")]
    Task<HttpRequestMessage> FileAsync(string path);
}
```

**2. Create the client and inspect a request.** In this example `httpClient` has a base address
of `https://people.example`. `[PathPrefix("/v1")]` adds `/v1` before each route in `IRoutesApi`.

```csharp
IRoutesApi api = RestService.ForGenerated<IRoutesApi>(httpClient, settings);
using HttpRequestMessage request = await api.GetAsync(1);
Console.WriteLine(request.Method); // GET
Console.WriteLine(request.RequestUri); // /v1/people/1
```

**3. Dispose the request.** The `using` declaration above releases the request and its body.
If you send it yourself, you also own the reply and must dispose it when you finish reading it.
The built request keeps a relative URL. `HttpClient` combines it with `BaseAddress` when it sends it.

The complete [request-building example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Routes/Routes.cs)
checks placeholders, prefixes and complete URLs. The [attribute example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Routes/RouteAttributes.cs)
checks every built-in HTTP method attribute and the custom-attribute extension point.

## Pick an HTTP method

Choose the method that the service expects. These are common uses, but the service defines its contract.

| Attribute | HTTP method | Common use |
| --- | --- | --- |
| `[Get(path)]` | GET | Read a resource. |
| `[Post(path)]` | POST | Submit data or create a resource. |
| `[Put(path)]` | PUT | Replace a resource. |
| `[Patch(path)]` | PATCH | Change part of a resource. |
| `[Delete(path)]` | DELETE | Remove a resource. |
| `[Head(path)]` | HEAD | Read reply headers without a reply body. |
| `[Options(path)]` | OPTIONS | Ask what operations a service accepts. |

Each attribute's `Method` property holds its HTTP method. Its inherited `Path` property holds the route.
These attributes derive from `HttpMethodAttribute`.

The interface above declares all seven methods. Build the other six in the same way as GET:

```csharp
using HttpRequestMessage post = await api.PostAsync(new(1, "Ada"));
using HttpRequestMessage put = await api.PutAsync(1, new(1, "Ada"));
using HttpRequestMessage patch = await api.PatchAsync(1, new(1, "Ada"));
using HttpRequestMessage delete = await api.DeleteAsync(1);
using HttpRequestMessage head = await api.HeadAsync(1);
using HttpRequestMessage options = await api.OptionsAsync();
```

For a normal call, return a [reply type](../results/return-types.md) such as `Task<Person>` instead.
Refit then builds and sends the request for you.

## Fill a placeholder

The route `/people/{id}` takes the value of `id` from the method argument.
Refit matches placeholder names without regard to case. It escapes argument values so a space
or a slash inside a value does not become a different URL part.
Escaping replaces a reserved character with text such as `%20` for a space.

An optional placeholder ends in `?`, such as `{id?}`. Passing `null` leaves out that segment.
The `{**path}` form keeps slashes inside a path argument. It still escapes the text between the slashes.

```csharp
using HttpRequestMessage allPeople = await api.OptionalAsync(null);
using HttpRequestMessage onePerson = await api.OptionalAsync(1);
using HttpRequestMessage file = await api.FileAsync("reports/annual report.pdf");
Console.WriteLine(allPeople.RequestUri); // /v1/people
Console.WriteLine(onePerson.RequestUri); // /v1/people/1
Console.WriteLine(file.RequestUri); // /v1/files/reports/annual%20report.pdf
```

## Supply a complete URL

`[Url]` takes a complete, absolute URL from a `string` or `Uri` argument.
An absolute URL includes the scheme and host, such as `https://cdn.example/manual.pdf`.
Use an empty method path because the argument already supplies the URL.

```csharp
internal interface IAbsoluteApi
{
    [Get("")]
    Task<HttpRequestMessage> DownloadAsync([Url] string absoluteUrl);

    [Get("/")]
    Task<HttpRequestMessage> DownloadUriAsync([Url] Uri absoluteUrl);
}
```

```csharp
using HttpRequestMessage download = await downloads.DownloadAsync("https://cdn.example/manual.pdf");
Console.WriteLine(download.RequestUri); // https://cdn.example/manual.pdf
```

The call uses the supplied host instead of `BaseAddress`. An invalid or relative URL throws
`ArgumentException` while Refit builds the request. Choose URLs your app trusts when the client adds
authorization headers, since those headers can travel to the supplied host.

## Shared prefixes

`PathPrefixAttribute.Prefix` holds the prefix supplied to its constructor.
Use `[PathPrefix]` on the interface to avoid repeating a common route part in every method.
The client interface's prefix also applies to methods that it inherits.
Pass the service root as `BaseAddress` when you use the prefix to supply its API path.

The implementation removes trailing slashes from the prefix and leading slashes from the method path
before joining them. Keep the prefix's leading slash, as in `/v1`; it does not insert a missing leading slash.
A blank prefix or a prefix made only of slashes adds nothing. A derived interface's prefix is used for
its inherited methods rather than concatenating the base and derived prefixes.

The absolute URL argument can also be a `Uri`. An empty method path or `/` permits the argument to
replace the route. Additional query arguments can still be appended.


```csharp
using HttpRequestMessage uriDownload = await downloads.DownloadUriAsync(new("https://cdn.example/manual.pdf"));
Console.WriteLine(uriDownload.RequestUri); // https://cdn.example/manual.pdf
```

## Read and extend method metadata

Each verb constructor stores its path in `HttpMethodAttribute.Path`. It does not validate the route
at construction time. The seven built-in `Method` overrides return the corresponding HTTP methods.
The base class has a protected constructor and protected path setter for subclasses.
This small attribute demonstrates that extension point directly; it is not used on a generated interface.
`Path` below is the sample's constant for `"/people"`.


```csharp
[AttributeUsage(AttributeTargets.Method)]
internal sealed class SearchAttribute(string path) : HttpMethodAttribute(path)
{
    public override HttpMethod Method => new("SEARCH");

    internal void ChangePath(string path) => Path = path;
}
```


```csharp
HttpMethodAttribute[] verbs =
[
    new GetAttribute(Path), new PostAttribute(Path),
    new PutAttribute(Path), new PatchAttribute(Path),
    new DeleteAttribute(Path), new HeadAttribute(Path),
    new OptionsAttribute(Path),
];
foreach (HttpMethodAttribute verb in verbs)
{
    Console.WriteLine($"{verb.Method} {verb.Path}");
}

SearchAttribute search = new("/old-search");
search.ChangePath("/search");
Console.WriteLine($"{search.Method} {search.Path}"); // SEARCH /search
```

## Route attribute reference

| Member | Description | Parameters | Returns or value |
| --- | --- | --- | --- |
| [`HttpMethodAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/HttpMethodAttribute.cs) | Base attribute for declaring the HTTP method and route template used by a Refit interface method. | None. Abstract class. | Attribute type inherited by Refit's built-in HTTP method attributes. |
| [`HttpMethodAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/HttpMethodAttribute.cs) | Stores the route template for an HTTP operation. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes the base attribute with the supplied path. |
| [`HttpMethodAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/HttpMethodAttribute.cs) | Identifies the HTTP verb represented by the attribute. | None. Abstract getter. | [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) for the operation. |
| [`HttpMethodAttribute.Path`](https://github.com/reactiveui/refit/blob/main/src/Refit/HttpMethodAttribute.cs) | Holds the route template Refit combines with method parameters. | None publicly; protected setter for derived attributes. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) route template supplied to the constructor or changed by a subclass. |
| [`DeleteAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/DeleteAttribute.cs) | Attribute that declares a DELETE request on an interface method. | None. Sealed attribute for methods. | Attribute type inherited from `HttpMethodAttribute`. |
| [`DeleteAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/DeleteAttribute.cs) | Declares a DELETE route with the supplied template. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes a DELETE route attribute. |
| [`DeleteAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/DeleteAttribute.cs) | Supplies the HTTP method for a DELETE route. | None. | [`HttpMethod.Delete`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod.delete). |
| [`GetAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/GetAttribute.cs) | Attribute that declares a GET request on an interface method. | None. Sealed attribute for methods. | Attribute type inherited from `HttpMethodAttribute`. |
| [`GetAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/GetAttribute.cs) | Declares a GET route with the supplied template. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes a GET route attribute. |
| [`GetAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/GetAttribute.cs) | Supplies the HTTP method for a GET route. | None. | [`HttpMethod.Get`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod.get). |
| [`HeadAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeadAttribute.cs) | Attribute that declares a HEAD request on an interface method. | None. Sealed attribute for methods. | Attribute type inherited from `HttpMethodAttribute`. |
| [`HeadAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeadAttribute.cs) | Declares a HEAD route with the supplied template. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes a HEAD route attribute. |
| [`HeadAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeadAttribute.cs) | Supplies the HTTP method for a HEAD route. | None. | [`HttpMethod.Head`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod.head). |
| [`OptionsAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/OptionsAttribute.cs) | Attribute that declares an OPTIONS request on an interface method. | None. Sealed attribute for methods. | Attribute type inherited from `HttpMethodAttribute`. |
| [`OptionsAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/OptionsAttribute.cs) | Declares an OPTIONS route with the supplied template. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes an OPTIONS route attribute. |
| [`OptionsAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/OptionsAttribute.cs) | Supplies the HTTP method for an OPTIONS route. | None. | [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) whose method name is `OPTIONS`. |
| [`PatchAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/PatchAttribute.cs) | Attribute that declares a PATCH request on an interface method. | None. Sealed attribute for methods. | Attribute type inherited from `HttpMethodAttribute`. |
| [`PatchAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/PatchAttribute.cs) | Declares a PATCH route with the supplied template. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes a PATCH route attribute. |
| [`PatchAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/PatchAttribute.cs) | Supplies a custom [`HttpMethod`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod) whose name is `PATCH`. | None. | HTTP method named `PATCH`. |
| [`PathPrefixAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/PathPrefixAttribute.cs) | Attribute that prepends a shared route prefix to methods on an interface. | None. Sealed attribute for interfaces. | Interface attribute carrying a shared route prefix. |
| [`PathPrefixAttribute(string prefix)`](https://github.com/reactiveui/refit/blob/main/src/Refit/PathPrefixAttribute.cs) | Stores the prefix Refit applies to the interface's method routes. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `prefix`: shared route prefix. | Initializes an interface route-prefix attribute. |
| [`PathPrefixAttribute.Prefix`](https://github.com/reactiveui/refit/blob/main/src/Refit/PathPrefixAttribute.cs) | Exposes the prefix supplied to the constructor. | None. Read-only. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) route prefix. |
| [`PostAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/PostAttribute.cs) | Attribute that declares a POST request on an interface method. | None. Sealed attribute for methods. | Attribute type inherited from `HttpMethodAttribute`. |
| [`PostAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/PostAttribute.cs) | Declares a POST route with the supplied template. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes a POST route attribute. |
| [`PostAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/PostAttribute.cs) | Supplies the HTTP method for a POST route. | None. | [`HttpMethod.Post`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod.post). |
| [`PutAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/PutAttribute.cs) | Attribute that declares a PUT request on an interface method. | None. Sealed attribute for methods. | Attribute type inherited from `HttpMethodAttribute`. |
| [`PutAttribute(string path)`](https://github.com/reactiveui/refit/blob/main/src/Refit/PutAttribute.cs) | Declares a PUT route with the supplied template. | [`string`](https://learn.microsoft.com/dotnet/api/system.string) `path`: route template. | Initializes a PUT route attribute. |
| [`PutAttribute.Method`](https://github.com/reactiveui/refit/blob/main/src/Refit/PutAttribute.cs) | Supplies the HTTP method for a PUT route. | None. | [`HttpMethod.Put`](https://learn.microsoft.com/dotnet/api/system.net.http.httpmethod.put). |
| [`UrlAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/UrlAttribute.cs) | Attribute that marks a parameter as the complete absolute request URL. | None. Sealed attribute for parameters. | Parameter marker consumed while Refit builds the request. |
| [`UrlAttribute()`](https://github.com/reactiveui/refit/blob/main/src/Refit/UrlAttribute.cs) | Marks a method parameter as the absolute URL used for the request. | None. | Initializes a URL parameter marker. |

Production source: [`HttpMethodAttribute.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/HttpMethodAttribute.cs), [`GetAttribute.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/GetAttribute.cs), [`PostAttribute.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/PostAttribute.cs), [`PutAttribute.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/PutAttribute.cs), [`PatchAttribute.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/PatchAttribute.cs), [`DeleteAttribute.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/DeleteAttribute.cs), [`HeadAttribute.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/HeadAttribute.cs), [`OptionsAttribute.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/OptionsAttribute.cs), [`PathPrefixAttribute.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/PathPrefixAttribute.cs), and [`UrlAttribute.cs`](https://github.com/reactiveui/refit/blob/main/src/Refit/UrlAttribute.cs).
