---
Order: 2
---
# Clients and settings

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/clients-index/clients-index.csproj).

After defining your API interface, you need to connect it to the service your app will use.
These pages show how to create that client, apply request settings and make the client
available to the rest of your app. Generated clients are the normal path. The creation and
request-builder pages explain when runtime reflection is needed and how it affects trimming
and Native AOT. They also explain who should dispose the HTTP client.

| Page | What you can do |
| --- | --- |
| [Create a client](creation.md) | Create a generated client over a shared or new `HttpClient`, select an interface through `Type`, or use the reflection path when needed. |
| [Settings](settings.md) | Set naming, serialization, request options and failure handling. |
| [Dependency injection](dependency-injection.md) | Register generated clients, named transports and keyed clients. |
| [Request builders](request-builders.md) | Select reflected methods by name and parameter types, or supply a request builder to client creation. |

The [client example project](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/clients-index/clients-index.csproj)
uses .NET 10, generated JSON metadata and local replies. `Clients.RunAsync` exercises generated clients.
The separate [reflection project](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Clients/Reflection/Reflection.csproj)
exercises APIs that need runtime metadata and runtime compilation.

From the Refit checkout's `src` folder, run the reflection executable with:

```bash
dotnet run --project examples/Documentation/Clients/Reflection/Reflection.csproj -c Release
```

Use the [AOT guide](../aot.md) to publish generated examples as native code.
Reflection examples use just-in-time compilation and do not demonstrate Native AOT compatibility.
