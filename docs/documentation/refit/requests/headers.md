---
Order: 5
---
# Headers and authorization

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/requests-headers/requests-headers.csproj).

A service may need to know which app is calling, which reply format it should send, or which
access token to check. HTTP headers carry those details alongside the request's URL and body.

With Refit, you can declare headers that every call shares and pass changing values with each
method call. This page shows both approaches, including how to obtain an authorization token.

## Build a request with headers

**1. Put shared headers on the interface.** `Headers` takes strings such as `Accept: application/json`.
A method can change or remove a header from the interface.
Here, `Headers("X-Remove")` removes that header because the string has no value after a colon.

```csharp
[Headers("Accept: application/json", "X-App: Delivery", "X-Remove: old")]
internal interface IHeaderApi
{
    [Get("/headers")]
    [Headers("X-Remove")]
    Task<HttpRequestMessage> BuildAsync(
        [Header("X-App")] string app,
        [HeaderCollection] IDictionary<string, string> headers,
        [Authorize] string token,
        [Property("tenant")] string tenant,
        [Property] int page);

    [Get("/header-person")]
    [Headers("Authorization: Bearer")]
    Task<Person> ReadAsync(CancellationToken cancellationToken);
}
```

**2. Use parameters for values that change.** `Header` gives one parameter a header name.
`HeaderCollection` sends the entries in a dictionary as headers.
`Authorize` writes the `Authorization` header with the default `Bearer` scheme.

**3. Build the request and inspect the headers.** The sample's `api` is a generated `IHeaderApi`
using its shared HTTP client. `App` is `Driver` and `RegionHeader` is `X-Region`.
`SampleToken` is a random token for the local stub service.
In your app, get a real token from your authentication service.

```csharp
using HttpRequestMessage request = await api.BuildAsync(App, new Dictionary<string, string> { [RegionHeader] = "north" }, SampleToken, "customer-a", 1);
Console.WriteLine(string.Join(",", request.Headers.GetValues("X-App"))); // Driver
Console.WriteLine(string.Join(",", request.Headers.GetValues(RegionHeader))); // north
Console.WriteLine(request.Headers.Authorization); // Bearer followed by the sample token
Console.WriteLine(request.Headers.Contains("X-Remove")); // False
```

Run the complete [header example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Headers/Headers.cs)
to check overrides, removal, token lookup and validation.

## Get a token when sending

Declare an authorization scheme without a token, such as `Headers("Authorization: Bearer")`.
Set `RefitSettings.AuthorizationHeaderValueGetter` to the method that obtains the token.
The generated request calls it before sending. It passes the request and cancellation token to the getter.
The getter returns a `ValueTask<string>`.

```csharp
RefitSettings tokenSettings = new(host.Settings.ContentSerializer) { AuthorizationHeaderValueGetter = static (_, _) => ValueTask.FromResult(SampleToken) };
IHeaderApi securedApi = RestService.ForGenerated<IHeaderApi>(host.Client, tokenSettings);
Person person = await securedApi.ReadAsync(CancellationToken.None);
Console.WriteLine(person.Name); // Ada
```

This example uses a supplied `HttpClient` and still invokes the getter.
The getter fills an empty token on a declared scheme. It does not replace a token already on the request.
An empty result removes the authorization header.
Use `Authorize("Basic")` or another scheme name when your service expects that scheme.
The attribute supplies the scheme. Your app supplies a token in the format that scheme needs.

## Header order and validation

Per-call values can override shared headers. A method's headers override its interface's headers.
More specific declarations on an inherited interface take priority over base declarations.
A null dynamic header value removes that header. An empty string is an empty header value.

`RefitSettings.ValidateHeaders` defaults to false. Refit then adds values without .NET's header validation.
True uses .NET validation and can reject an invalid header value.

The [header example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Headers/Headers.cs)
tries the malformed `User-Agent` value `broken(` in both modes. False preserves it; true
throws `FormatException` while building the request, before anything is sent.
Refit strips carriage returns and line feeds from header names and values in both modes.
Content headers belong to the request content. A request without a body cannot carry a content header.

| Attribute or property | Use |
| --- | --- |
| `Headers(params string[] headers)` / `Headers` | Shared interface or method headers. |
| `Header(string header)` / `Header` | One header value from a method argument. |
| `HeaderCollection()` | A header dictionary from a method argument. |
| `Authorize(string scheme = "Bearer")` / `Scheme` | An authorization token from a method argument. |
| `RefitSettings.AuthorizationHeaderValueGetter` | Obtains a missing token before a declared authorized request is sent. |
| `RefitSettings.ValidateHeaders` | Chooses whether .NET validates header values. |

Use [request context](request-context.md) for local values a handler needs.
Those values are not sent as headers or query entries.

## Choose an authorization scheme explicitly

`Authorize()` defaults to Bearer. `Authorize("Basic")` uses Basic and expects your app to supply
the already encoded credentials. This interface also sets context through an interface property.


```csharp
internal interface IContextApi
{
    [Property("tenant")]
    string Tenant { get; set; }

    [Get("/context")]
    [Headers("X-App: Delivery")]
    Task<HttpRequestMessage> BuildAsync([Authorize("Basic")] string token, [Header("X-App")] string? app);

    [Post("/context")]
    Task SaveAsync([Body] Person person, [Property("tenant")] string tenant, CancellationToken cancellationToken);
}
```

Here `api` is a generated `IContextApi` with `Tenant = "client-tenant"`.
Passing null for `app` removes the static header. `TestToken` is randomly generated local test data;
`TenantKey` is the constant `"tenant"`.


```csharp
using HttpRequestMessage built = await api.BuildAsync(TestToken, null);
Console.WriteLine(built.Headers.Authorization); // Basic followed by the test token
Console.WriteLine(built.Headers.Contains("X-App")); // False
_ = built.Options.TryGetValue(new(TenantKey), out string? clientTenant);
Console.WriteLine(clientTenant); // client-tenant
```

`HeadersAttribute.Headers` returns the supplied array, rather than a copy; a null array becomes an empty array.
`HeaderAttribute.Header` and `AuthorizeAttribute.Scheme` store their constructor strings without validation.
