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

**2. Create the client and inspect a request.** The sample host's HTTP client has a base address
of `https://people.example`. `[PathPrefix("/v1")]` adds `/v1` before each route in `IRoutesApi`.

```csharp
IRoutesApi api = RestService.ForGenerated<IRoutesApi>(host.Client, host.Settings);
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
