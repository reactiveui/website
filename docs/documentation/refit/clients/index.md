---
Order: 2
---
# Clients and settings

After defining your API interface, you need to connect it to the service your app will use.
These pages show how to create that client, share request settings and make the client
available to the rest of your app. They also explain who should dispose the HTTP client.

| Page | What you can do |
| --- | --- |
| [Create a client](creation.md) | Reuse an HTTP client, create one from a URL, or select an interface through `Type`. |
| [Settings](settings.md) | Set naming, serialization, request options and failure handling. |
| [Dependency injection](dependency-injection.md) | Register generated clients, named transports and keyed clients. |
| [Request builders](request-builders.md) | Select reflected methods or integrate another client generator. |

The [client examples](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Clients)
use .NET 10, generated JSON metadata and local replies. `Clients.RunAsync` exercises generated clients.
The separate [reflection executable](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Clients/Reflection)
exercises APIs that need runtime metadata and runtime compilation.

From the Refit checkout's `src` folder, run the reflection executable with:

```bash
dotnet run --project examples/Documentation/Clients/Reflection/Reflection.csproj -c Release
```

Use the [AOT guide](../aot.md) to publish generated examples as native code.
Reflection examples use just-in-time compilation and do not demonstrate Native AOT compatibility.
