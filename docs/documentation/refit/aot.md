---
Order: 4
---
# AOT and generated clients

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/aot/aot.csproj).

If you are preparing a mobile app for Native AOT, its service calls need to be ready too.
Refit can generate the HTTP client during the build. You also need to prepare how the app
will read and write the JSON models used by those calls.

This page brings those two pieces together: a generated Refit client and a generated JSON
context. It walks through the setup, explains where it helps, and shows how to publish and run the example.

## Where this helps in real apps

A mobile app can create Refit clients without discovering their methods at runtime.
Trimming removes code the app does not appear to use, which can reduce deployment size.
Reflection-only access can need code that the trimmer cannot see, so generated JSON metadata
makes the request and reply models visible to the trimmer.
This supports .NET MAUI apps that use Native AOT on iOS and Mac Catalyst.
See Microsoft's [MAUI AOT guide](https://learn.microsoft.com/en-us/dotnet/maui/deployment/nativeaot?view=net-maui-10.0)
for the app requirements.

Services and command-line apps can use the same generated clients.
Native AOT can reduce startup time and memory use, but the result depends on the whole app.
Measure on your target device or deployment. See Microsoft's
[Native AOT overview](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/).

If your app explicitly requests HTTP/3, check its publish settings as well. .NET disables HTTP/3
support for trimmed and Native AOT apps unless `Http3Support` is enabled. Refit applies the
`RefitSettings.Version` and `VersionPolicy` values to each generated request; they select the
HTTP version policy but do not enable the platform's HTTP/3 support.

## Make one call ready for AOT

**1. Use a generated request.** Start with `IPeopleApi` from
[the first request](index.md#your-first-request). Resolve it with `RestService.ForGenerated<T>`.
This chooses the generated implementation and does not fall back to runtime request building.

**2. Generate the JSON readers and writers too.** Request generation and JSON generation are separate jobs.
Add your request and reply types to a `JsonSerializerContext`.
The attributes below ask .NET's JSON generator to produce code for `Person`.
This file imports `System.Text.Json.Serialization`.

```csharp
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(Person[]))]
[JsonSerializable(typeof(List<Person>))]
internal sealed partial class SampleJsonContext : JsonSerializerContext;
```

**3. Supply the context to Refit's serializer.** `TypeInfoResolver` tells the JSON serializer where
to get each type's generated readers and writers. Keep the options and settings for reuse.
This example imports `System.Text.Json` and declares the fields inside its sample class.

```csharp
private static readonly JsonSerializerOptions JsonOptions = new(SampleJsonContext.Default.Options) { TypeInfoResolver = SampleJsonContext.Default };

private static readonly RefitSettings Settings = new(new SystemTextJsonContentSerializer(JsonOptions));
```

`SampleJsonContext.Default.Person` is a `JsonTypeInfo<Person>`. It describes one model and lets
the typed `JsonSerializer` overloads work without looking up metadata through reflection:

```csharp
JsonTypeInfo<Person> personInfo = SampleJsonContext.Default.Person;
string json = JsonSerializer.Serialize(new(1, "Ada"), personInfo);
Person? restored = JsonSerializer.Deserialize(json, personInfo);
Console.WriteLine(restored?.Name); // Ada
```

Refit's `SystemTextJsonContentSerializer` does not take `JsonTypeInfo<T>` as a method argument.
It uses `JsonSerializerOptions.TypeInfoResolver`, so assigning the generated context to
`TypeInfoResolver` connects every registered `JsonTypeInfo<T>` to Refit's generic request and
reply overloads. Use the typed overloads above for JSON work outside Refit, and reuse the same
context in the Refit settings.

**4. Call the API with those settings.** The sample host supplies its shared HTTP client.
In your app, pass your own client.

```csharp
IPeopleApi api = RestService.ForGenerated<IPeopleApi>(host.Client, Settings);
Person person = await api.GetPersonAsync(1, CancellationToken.None);
Console.WriteLine(person.Name); // Ada
```

The complete [native example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Aot/NativeClient/NativeClient.csproj)
publishes a small console app. From the Refit checkout's `src` folder, publish and run it on Linux x64:

```bash
dotnet publish examples/Documentation/Aot/NativeClient/NativeClient.csproj -c Release -r linux-x64
./examples/Documentation/Aot/NativeClient/bin/Release/net10.0/linux-x64/publish/NativeClient
```

Choose a runtime identifier that matches your build host and target when using another platform.

## Why generated metadata matters

Generated requests and JSON metadata give the trimmer a direct route to the required code.
That keeps generated clients suitable for trimmed JIT apps as well as Native AOT apps.
Runtime-only lookups can need members that trimming would otherwise remove.

Register every JSON request and reply type in your context. Register list or array types too
when a method serializes or reads that whole container. Use metadata generation for reading replies.
The default generation mode in the example supports both reading and writing.

Fix `RF006` warnings before using generated-only clients. They identify a method that needs runtime
request building. If reflection is acceptable for your app, the `Refit.Reflection` package supplies that path.
It does not make an unsupported method safe for Native AOT.

Publish and run the real app as part of verification. An ordinary build is not enough to show that its
dependencies survive trimming. Check trimming and AOT diagnostics before deployment.

## Windows and Apple apps

Refit's shipping API uses portable .NET types. It does not add separate Windows or Apple request types.
The same interfaces can serve a Windows desktop app or a .NET MAUI app on Apple devices.
Your UI framework still controls where screen updates run.
See [Primitives UI platforms](../primitives/platforms.md) for choosing the thread that delivers a stream's values.

.NET MAUI supports Native AOT deployment on iOS and Mac Catalyst.
Its benefit depends on the app and device. The rest of the app must support trimming too.
Read Microsoft's [MAUI Native AOT guide](https://learn.microsoft.com/en-us/dotnet/maui/deployment/nativeaot?view=net-maui-10.0)
for the platform setup and limits.
Android and Windows use their own compilation choices. See [MAUI runtimes and compilation](https://learn.microsoft.com/en-us/dotnet/maui/deployment/runtimes-compilation?view=net-maui-10.0).

## Recent Refit releases

These notes separate Refit's request-generation work from .NET's native compilation step.

| Release | Change relevant to these pages |
| --- | --- |
| [14.0.0](https://github.com/reactiveui/refit/releases/tag/v14.0.0) | Completed the move to generated request building, separated the reflection package, and added SSE streaming. Some helpers and settings delegates use `ValueTask`. |
| [15.0.0](https://github.com/reactiveui/refit/releases/tag/v15.0.0) | Added keyed generated-client registration and improved generated-client lookup when registration runs late. |
| [15.2.0](https://github.com/reactiveui/refit/releases/tag/v15.2.0) | Added request-body compression and fixed analyzer loading in Visual Studio. |

The request generator is only one part of AOT readiness. It does not replace the JSON context or the
publish and run checks for your whole app.
