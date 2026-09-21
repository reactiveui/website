---
Order: 7
---
# Refit

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/index/index.csproj).

When your app needs data from a web service, you want the call to be easy to read and use.
Refit lets you describe that call on a C# interface. You specify the service's route and the
values it needs, then call the interface method to make the request.

Refit generates the implementation during the build. It fills in the URL, headers and body,
sends the request through `HttpClient`, and reads the reply into your C# model.

These examples use .NET 10 and C# 14 as their baseline. All C# 14 features are available.
Examples for another target framework say so explicitly.

You keep control of the HTTP client and can share settings for JSON, authorization and error handling.
The walkthrough below builds a first call, then the subject pages explain the choices in more detail.

Install the `Refit` NuGet package in the project that declares your interface.

```bash
dotnet add package Refit
```

## Your first request

**1. Choose a reply type.** This service returns a person's ID and name as JSON.
JSON is a text format for objects, lists and values.

```csharp
internal sealed record Person(int Id, string Name);
```

**2. Describe the request.** `[Get]` selects the HTTP `GET` method. A GET asks a service to read data.
The `{id}` part is a route placeholder. Refit replaces it with the method's `id` argument.
An interface like this is the contract between your app and the service.

```csharp
internal interface IPeopleApi
{
    [Get("/people/{id}")]
    Task<Person> GetPersonAsync(int id, CancellationToken cancellationToken);
}
```

**3. Register the JSON types.** The compiler generates a JSON context from these registrations.
It supplies model metadata without finding properties through reflection at runtime.
Use the same context when you publish a Native AOT app.
Add `using System.Text.Json;` and `using System.Text.Json.Serialization;` for the JSON types.
Keep the `JsonSerializerDefaults.Web` line. It makes the context read the camelCase names that most services send.
[JSON and generated metadata](serialization/json.md#add-the-web-defaults) explains what goes wrong without it.

```csharp
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(Person[]))]
[JsonSerializable(typeof(List<Person>))]
internal sealed partial class SampleJsonContext : JsonSerializerContext;
```

**4. Reuse one HTTP client.** Create the HTTP client when your app starts and reuse it for calls.
In the runnable samples, `host.Client` is that client. Its `BaseAddress` is `https://people.example`.
The sample host supplies local replies, so you can run the examples without a web server.

**5. Create the implementation and call it.** `RestService.ForGenerated<T>` uses the implementation
that Refit generated during the build. Pass the JSON context as the second argument.
This call asks for `https://people.example/people/1`.
In your own app, pass your HTTP client in place of `host.Client`. Add `using Refit;` to use Refit's types.

```csharp
IPeopleApi api = RestService.ForGenerated<IPeopleApi>(host.Client, SampleJsonContext.Default);
Person person = await api.GetPersonAsync(1, CancellationToken.None);
Console.WriteLine(person.Name); // Ada
```

Pass a cancellation token from the caller when a request should stop with a screen or a user action.
The example uses `CancellationToken.None` because its short local request has no caller to cancel it.

## Pass settings instead of a context

`RefitSettings` holds the serializer and the other choices for a client, such as headers and error handling.
You can build your own `JsonSerializerOptions`, wrap them in a serializer and pass the settings instead of the context.
Pick this form when you have options to share, need full control, or want one options object
elsewhere in your app. The short path above needs no options object. This form makes you assign the
`TypeInfoResolver` yourself and keep the options unchanged after first use.
The sample host exposes its settings as `host.Settings`.

```csharp
private static readonly JsonSerializerOptions JsonOptions = new(SampleJsonContext.Default.Options) { TypeInfoResolver = SampleJsonContext.Default };

private readonly RefitSettings _settings = new(new SystemTextJsonContentSerializer(JsonOptions));
```

```csharp
IPeopleApi withSettings = RestService.ForGenerated<IPeopleApi>(host.Client, host.Settings);
Person fromSettings = await withSettings.GetPersonAsync(1, CancellationToken.None);
```

In your own app, pass `_settings` in place of `host.Settings`.
[Create a client](clients/creation.md#use-settings-instead-of-a-context) covers the settings form in detail.

## Find a topic

| Page | What you can do |
| --- | --- |
| [API reference](api-reference.md) | Find types, overloads, parameters and return values across all topics on one page. |
| [Why use Refit?](why-refit.md) | Compare an interface contract with a raw HTTP implementation. |
| [Client creation and settings](clients/index.md) | Create generated clients, configure settings and register clients with dependency injection. |
| [Routes and HTTP methods](requests/routes.md) | Choose a method, fill a route, and inspect a request before sending it. |
| [Return types](results/return-types.md) | Choose `Task<T>`, `ValueTask<T>`, `IObservable<T>` or a response wrapper. |
| [Streaming replies](results/streaming.md) | Read a JSON array, JSON Lines or server-sent events with `IAsyncEnumerable<T>`. |
| [Pagination](results/pagination.md) | Read every item of a paged list, such as an S3, Azure or GitHub listing, with `await foreach`. |
| [JSON and generated metadata](serialization/json.md) | Register a JSON context, keep your serializer settings, pass metadata to a method and check missing registrations. |
| [AOT and generated clients](aot.md) | Generate request and JSON code for apps that compile ahead of time. |
| [Testing clients](testing/index.md) | Supply local replies, inspect requests and check which routes were called. |

Refit uses [ReactiveUI.Primitives](../primitives/index.md) inside its runtime.
Your API methods expose C# types such as `ValueTask<T>` and `IObservable<T>`.
You can await a reply or use [LINQ-style operators](results/return-types.md#querying-a-reply) to shape it.

## Run the examples

The [documentation examples](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation)
live in folders named for their topic. Each page links to its folder.
They reference the Refit source projects, including the generator that creates the client implementation.
Website blocks can show a focused subset of the complete example. Setup and runtime checks
stay in the source project when the page does not need them.

From the Refit checkout's `src` folder, run:

```bash
dotnet run --project examples/Documentation/Documentation.csproj -c Release
```

The program supplies local HTTP replies and checks the documented results.
Its `Common/SampleHost.cs` owns the HTTP client for the whole run.
Its `Program.cs` calls each topic's example and disposes the host when the run ends.
