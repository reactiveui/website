---
Order: 1
---
# Why use Refit?

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/why-refit/why-refit.csproj).

As an app grows, so does the code around its service calls. You need to build URLs, set
headers, send data and read replies. Much of that work follows the same pattern.

Refit lets you describe each call on an interface and generates the HTTP implementation.
The interface becomes a compact place to see what the service offers and what each call needs.
You can spend more of your client code on how the app uses the result.

This page compares that approach with a handwritten HTTP call and explains the choices
you still control, including the HTTP client and JSON settings.

That matters beyond the amount of code you write. Runtime lookup and compilation use CPU time and memory.
Those costs add up across server instances and matter when a mobile app starts.
The [AOT page](aot.md#where-this-helps-in-real-apps) shows how this helps containers, ARM servers and phones.

## Let Refit write the HTTP glue

**1. Describe the call you need.** The interface from [the first request](index.md#your-first-request)
says to send a GET to `/people/{id}` and return a `Person`.
An endpoint is a service operation at a particular URL.
The attribute describes the endpoint. The method describes what your app passes in and gets back.

```csharp
internal interface IPeopleApi
{
    [Get("/people/{id}")]
    Task<Person> GetPersonAsync(int id, CancellationToken cancellationToken);
}
```

**2. Create the client and call the method.** Follow the [first request](index.md#your-first-request)
to create the generated client with your shared `HttpClient`.
Refit fills `{id}`, builds and sends the request, reads the reply and handles cleanup.
You do not have to write that method body.

**3. See the work the declaration replaces.** Here is a handwritten method for the same request.
It builds the route, creates the request, sends it, checks the status and reads JSON.
It also disposes the request and reply.
The JSON context comes from the [AOT page](aot.md).
The file imports `System.Globalization` and `System.Net.Http.Json`.

```csharp
internal static async Task<Person?> GetPersonAsync(HttpClient client, int id, CancellationToken cancellationToken)
{
    string path = string.Create(CultureInfo.InvariantCulture, $"/people/{id}");
    using HttpRequestMessage request = new(HttpMethod.Get, path);
    using HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
    _ = response.EnsureSuccessStatusCode();
    return await response.Content.ReadFromJsonAsync(SampleJsonContext.Default.Person, cancellationToken);
}
```

That is manageable for one request. A service may also let you search, create, edit and delete people.
Each call needs its own route, argument mapping and reply handling.
You can build shared helpers for a handwritten client. With Refit, the generator writes the glue
and the compiler checks and compiles it for you.

The runnable [comparison](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Rationale/Rationale.cs)
uses the same shared `HttpClient` and local JSON reply for both methods.
It checks that both return the same person.

## Less glue to maintain

| Job | With Refit | With a raw implementation |
| --- | --- | --- |
| Add an endpoint | Declare a method with its route and argument roles. | Implement the request and reply handling, using your own helpers where needed. |
| Change a route or query name | Update the attribute on the method or parameter. | Update the code that builds the URL. |
| Send a body | Mark the body parameter. Refit uses the configured serializer to turn it into request data. | Connect the body writer to each request. |
| Choose how to receive a reply | Declare a supported return type. | Implement the reading and lifetime rules for that type. |

C# checks your method arguments and return types at build time. Refit's generator also checks
whether it can build the request from your declared types. This does not prove that the server follows
the same contract. You still need to test against its behavior.

Refit keeps `HttpClient` available for configuration. You can set a base address or timeout and supply
message handlers. A message handler is a step that receives a request before the transport sends it.
Use handlers for shared request behavior, such as logging or getting an authorization token.

Choose a raw implementation when your request needs a sending or reading process that the interface
cannot describe. You can use it alongside Refit clients and share the same HTTP setup.
