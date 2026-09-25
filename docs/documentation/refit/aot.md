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
The JSON context APIs are available on .NET 8 and later.

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
Refit takes advantage of System.Text.Json
[source generation](https://learn.microsoft.com/dotnet/standard/serialization/system-text-json/source-generation).
Add your request and reply types to a JSON context: a `partial` class that derives from `JsonSerializerContext`.
`SampleJsonContext` is only the name this example gives its class. Give yours any name.
The attributes below ask the System.Text.Json source generator to produce code for `Person`.
This file imports `System.Text.Json` and `System.Text.Json.Serialization`.

```csharp
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(Person[]))]
[JsonSerializable(typeof(List<Person>))]
internal sealed partial class SampleJsonContext : JsonSerializerContext;
```

Keep the `JsonSourceGenerationOptions(JsonSerializerDefaults.Web)` line. It is the line that goes wrong most often.
A context without it expects PascalCase property names and matches them by exact case.
A service that sends camelCase names then fills your models with default values, and nothing reports an error.
The web defaults read camelCase names and ignore case.
[JSON and generated metadata](serialization/json.md#add-the-web-defaults) shows the failure.

**3. Give the context to Refit and call the API.** `RestService.ForGenerated<T>` takes the context.
Refit reads and writes JSON with the context's own options, and it never falls back to reflection.
A type that you did not list on the context throws `NotSupportedException`.
`httpClient` is your app's shared `HttpClient`, with its `BaseAddress` set to the API root.

```csharp
IPeopleApi api = RestService.ForGenerated<IPeopleApi>(httpClient, SampleJsonContext.Default);
Person person = await api.GetPersonAsync(1, cancellationToken);
Console.WriteLine(person.Name); // Ada
```

The context is the only JSON setup the client needs.
Refit does not write `[JsonSerializable]` entries for you. You list each type on your context yourself.

**Or build the settings yourself.** Pick this form when you have `JsonSerializerOptions` to share,
need full control, or want one options object reused elsewhere in your app.
The short path needs no options object. This form makes you assign the `TypeInfoResolver` yourself and keep
the options unchanged after first use. A `TypeInfoResolver` tells the serializer where to find the metadata for each type.
Give it the context, wrap the options in a serializer, and pass the settings to `ForGenerated`.
Both forms are safe for Native AOT, and the native example runs both.

```csharp
private static readonly JsonSerializerOptions JsonOptions = new(SampleJsonContext.Default.Options) { TypeInfoResolver = SampleJsonContext.Default };
```

```csharp
RefitSettings settings = new(new SystemTextJsonContentSerializer(JsonOptions));
IPeopleApi withSettings = RestService.ForGenerated<IPeopleApi>(httpClient, settings);
Person fromSettings = await withSettings.GetPersonAsync(1, cancellationToken);
Console.WriteLine(fromSettings.Name); // Ada
```

If you have settings and want the context too, skip the hand-made resolver. `settings.UseJsonContext(context)` and
`ForGenerated(client, context, settings)` keep your settings and add the context.
Your naming policy and converters apply. See [choose whose settings apply](serialization/json.md#choose-whose-settings-apply).

A method can also take a `JsonTypeInfo<T>` parameter, which is the metadata for one type.
See [pass metadata to a method](serialization/json.md#pass-metadata-to-a-method).

**4. Publish and run.** The complete [native example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Aot/NativeClient/NativeClient.csproj)
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

Reflection-based JSON is off when you pass a context. Pass `allowReflectionFallback: true` to let a type
that the context does not list use reflection. That option is not trim or Native AOT safe.
Add the missing type to the context instead.

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
