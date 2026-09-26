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

Fix every `RF006` warning before you use generated-only clients.
[Find methods that fall back to reflection](#find-methods-that-fall-back-to-reflection) explains the warning.

Publish and run the real app as part of verification. An ordinary build is not enough to show that its
dependencies survive trimming. Check trimming and AOT diagnostics before deployment.

## Find methods that fall back to reflection

Refit generates the request code for most methods when your project builds. A few method shapes stop that.
Refit then leaves the method to the reflection request builder. That builder lives in the `Refit.Reflection`
package and builds each request at runtime by reading your interface through reflection.
It is not safe for trimming or Native AOT.

A generated-only client never uses the reflection request builder. You get one from `RestService.ForGenerated`
or [`AddRefitGeneratedClient`](clients/dependency-injection.md). Calling a method that needs the builder then throws.
The `RF006` warning finds these methods while you build, so you can fix them before the app runs.

**1. Build and find the warning.** `RF006` sits on the part of the method that stops generation.
That is the parameter, the return type or the HTTP method attribute. When no single part is responsible,
the warning sits on the method name. Here, `filters` is responsible:

```csharp
[Get("/query")]
Task<string> Search(object filters);
```

**2. Read the three parts of the message.** For `Search` on an interface named `IApi`, the warning reads:

```text
Method IApi.Search falls back to the reflection request builder because query parameter 'filters' has type
'object', which cannot be flattened into query values at compile time. Add [QueryConverter(typeof(...))] naming
an IQueryConverter<object>, use a concrete type with readable properties, or set TreatAsString. If reflection is
acceptable, reference the Refit.Reflection package and create the client with RestService.For; the method still
fails under Native AOT and through generated-only registration (AddRefitGeneratedClient, RestService.ForGenerated).
```

- **The reason** names the parameter, return type or attribute, and says why Refit cannot generate the request.
  To flatten a query object means to turn each of its properties into its own `name=value` pair.
  Refit cannot see the properties of an `object`.
- **The fix** says what to change so Refit generates the request.
- **The advice** says whether `Refit.Reflection` can run the method instead.
  [How to read the advice](#how-to-read-the-advice) covers the three kinds.

**3. Change the declaration.** Follow the fix. Here, give `filters` a concrete type.
`Filter` is a plain class with a `string? Term` property, so Refit can flatten it:

```csharp
[Get("/query")]
Task<string> Search(Filter filters);
```

Build again. The warning is gone, and Refit generates the request.

### How to read the advice

The last sentence of each message is one of three kinds of advice.

| Advice | What it means | What to do |
| --- | --- | --- |
| Reference `Refit.Reflection` and create the client with `RestService.For` | Only the generator lacks support for this shape. The reflection request builder can run it. | Change the declaration for Native AOT. If you accept reflection, add the package and use `RestService.For`. The method still fails under Native AOT and through `AddRefitGeneratedClient` or `RestService.ForGenerated`. |
| `Refit.Reflection` rejects this declaration at runtime too | The declaration is invalid for both builders. | Change the declaration. |
| `Refit.Reflection` does not support this feature | The method uses a feature that only generated requests support. | Change the declaration. |

### Reasons

Each `RF006` warning carries a `Reason` property with a name from the table below.
When a parameter is responsible, the `ParameterOrdinal` property gives its position, starting at 0.
Build tools and editor extensions can read both properties.

The table groups the reasons by their advice.
"Reflection" means `Refit.Reflection` can run the method. "Invalid" means neither builder accepts it.
"Generated only" means only generated requests support it.

| Reason | What stops generation | What to change | Advice |
| --- | --- | --- | --- |
| `UnsupportedReturnType` | The return type is not one that generated requests produce, and no return adapter handles it. | Return `Task`, `Task<T>`, `ValueTask<T>`, `IObservable<T>`, `IAsyncEnumerable<T>` or `PagedEnumerable<TPage, TItem>`. Or declare an [`IReturnTypeAdapter<TReturn, TResult>`](results/adapters.md) for the type in the project. | Reflection |
| `UnreadableHttpMethod` | A custom HTTP method attribute does not return its verb as `new HttpMethod("VERB")` with a string literal. | Override `Method` with an expression that builds `HttpMethod` from a string literal. | Reflection |
| `GenericUrlEncodedBody` | A form-url-encoded body has a method type parameter as its type, so its properties are unknown. | Use a concrete body type, or send the body as JSON. | Reflection |
| `UnknownBodySerialization` | A body names a `BodySerializationMethod` value that is not defined. | Use a defined `BodySerializationMethod` member. | Reflection |
| `UnsupportedPathParameterType` | A value bound to `{name}` is `object`, an interface or a type parameter with no constraint. | Declare a simple type, a concrete class, struct or array, or constrain the type parameter to a class. | Reflection |
| `EncodedRoundTripNotString` | An `[Encoded]` parameter bound to `{**name}` is not a `string`. | Declare it as `string`, or remove `[Encoded]` so Refit escapes each segment. | Reflection |
| `UnsupportedQueryType` | A query parameter has a type that Refit cannot flatten when it builds. | Add `[QueryConverter(typeof(...))]` naming an `IQueryConverter<T>`, use a concrete type with readable properties, or set `TreatAsString`. | Reflection |
| `UnresolvedPathProperty` | A `{param.Prop}` placeholder does not name a public readable property of a simple type. | Name such a property, or constrain the type parameter to a class that declares it. | Reflection |
| `UnsupportedPathObjectQuery` | An object bound to the path has a property that no placeholder uses, and Refit cannot flatten it into the query. | Bind that property to a placeholder, or change it to a simple value or a collection of simple values. | Reflection |
| `UnsupportedMultipartPart` | A multipart parameter has a type Refit cannot turn into a part when it builds, such as `object` or an interface. | Use `StreamPart`, `ByteArrayPart`, `FileInfoPart`, `Stream`, `byte[]`, `FileInfo`, `string`, `HttpContent` or a concrete serializable type. | Reflection |
| `FormObjectMultipartPart` | A multipart parameter uses `[FormObject]`. Only the reflection request builder splits an object into one part per property. | Declare each form field as its own parameter, or remove `[FormObject]` to send the object as one serialized part. | Reflection |
| `UnsupportedPathTemplate` | The path contains a backslash, a line break or unbalanced braces. | Use `/` between segments and close every `{` placeholder within its segment. | Invalid |
| `UrlParameterType` | A `[Url]` parameter is not a `string` or `Uri`. | Declare it as `string` or `System.Uri`. | Invalid |
| `UrlParameterWithPath` | A `[Url]` parameter sits beside a path, a path parameter or a second `[Url]` parameter. | Give the HTTP method attribute an empty path and keep one `[Url]` parameter. It supplies the full absolute URL. | Invalid |
| `MultipleBodies` | The method has a second `[Body]` parameter. | Keep one `[Body]` parameter. Send the other values as query, header or path values. | Invalid |
| `SecondImplicitBody` | A second parameter with no attribute would also become the request body. | Mark the intended body with `[Body]`. Mark the other with `[Query]` or give it a `[QueryConverter]`. | Invalid |
| `MultipleCancellationTokens` | The method has a second `CancellationToken`. | Keep one `CancellationToken` parameter. | Invalid |
| `MultipleHeaderCollections` | The method has a second `[HeaderCollection]` parameter. | Keep one. | Invalid |
| `MultipleAuthorizeParameters` | The method has a second `[Authorize]` parameter. | Keep one. | Invalid |
| `MultipartWithBody` | A `[Multipart]` method has a `[Body]` parameter. | Remove `[Body]`. Every parameter of a multipart method becomes a part. | Invalid |
| `UnreadableQueryConverter` | A `[QueryConverter]` does not name a converter type. | Pass `typeof(...)` of an `IQueryConverter<T>` implementation. | Generated only |
| `InvalidPagedMethod` | The `[Paged]` setup is invalid. | Fix the [`RF013` error](results/pagination.md#build-time-checks) on the method. | Generated only |
| `InvalidJsonTypeInfoParameter` | A `JsonTypeInfo<T>` parameter cannot be used. | Fix the [`RF014` error](serialization/json.md#build-time-checks) on the method. | Generated only |

`RF006` and the generator share one classifier. The classifier is the code that decides whether Refit can
generate a request. When a new Refit release learns to generate a shape, its `RF006` warning goes away on its own.

`RF006` appears only when generated request building is on, which is the default.
With `RefitGeneratedRequestBuilding` set to `false`, every method uses the reflection request builder by design,
so Refit reports nothing.

### Fail the build on a fallback

A project that publishes with Native AOT, or that registers only generated clients, should not build with a fallback.
Turn `RF006` into an error there. The check is off by default, so a project that chooses `Refit.Reflection`
keeps building.

Set this property in the project file:

```xml
<PropertyGroup>
  <RefitRequireGeneratedRequests>true</RefitRequireGeneratedRequests>
</PropertyGroup>
```

Refit then adds `RF006` to `WarningsAsErrors` before the compiler runs.
To cover only some folders, set the severity in an `.editorconfig` file in that folder instead:

```ini
[*.cs]
dotnet_diagnostic.RF006.severity = error
```

## Which method shapes generate

The table shows common method shapes and what Refit does with each. It makes three separate claims:

- **Request generated** says whether Refit generates the request. "No" gives the `RF006` reason.
- **JSON metadata** lists the types your JSON context must describe for the method. "(parameter)" means the
  method's `JsonTypeInfo<T>` parameter supplies that metadata. "none" means the method reads and writes no JSON.
  A generated request does not make JSON trim-safe on its own. You still list these types on your context.
- **Native AOT smoke** names the method in `Refit.NativeAotSmoke` that covers the shape. Refit's build publishes
  that small program with Native AOT and runs it. A dash means no native run covers the shape.

The table is classifier-backed. Refit's own tests compile every example through the generator and the `RF006`
analyzer, and they fail when a row no longer matches.

In the examples, `Todo`, `Form` and `Record` are plain classes. `Filter` is a plain class with an
`IQueryConverter<Filter>` named `FilterConverter`. `T` is a type parameter of the method.

| Shape | Example | Request generated | JSON metadata | Native AOT smoke |
| --- | --- | --- | --- | --- |
| Path value, JSON reply | `[Get("/todos/{id}")] Task<Todo> Get(int id);` | Yes | reply `Todo` | — |
| JSON body | `[Post("/todos")] Task<Todo> Create([Body] Todo item);` | Yes | body `Todo`, reply `Todo` | `CreateTodoAsync` |
| JSON body with `JsonTypeInfo<T>` | `[Post("/todos")] Task<Todo> Create([Body] Todo item, JsonTypeInfo<Todo> info);` | Yes | body `Todo` (parameter), reply `Todo` (parameter) | `CreateDescribedTodoAsync` |
| Form-url-encoded body | `[Post("/forms")] Task<string> Submit([Body(BodySerializationMethod.UrlEncoded)] Form form);` | Yes | none | `SubmitFormAsync` |
| Query values and collections | `[Get("/search")] Task<string> Search(string q, int? page, [Query(CollectionFormat.Multi)] int[] ids);` | Yes | none | `SearchAsync` |
| `ApiResponse<T>` reply | `[Get("/status")] Task<ApiResponse<Todo>> Status();` | Yes | reply `Todo` | `GetStatusAsync` |
| Generic body and reply | `[Post("/echo")] Task<T> Echo<T>([Body] T item);` | Yes | body `T`, reply `T` | `EchoAsync` |
| Observable reply | `[Get("/legacy")] IObservable<HttpResponseMessage> Observe();` | Yes | none | — |
| JSON Lines upload | `[Post("/uploads")] Task Upload([Body(BodySerializationMethod.JsonLines)] IAsyncEnumerable<Record> records);` | Yes | JSON Lines `Record` | `UploadAsync` |
| Streamed reply | `[Get("/todos")] IAsyncEnumerable<Todo> List();` | Yes | reply `Todo` | — |
| Multipart stream part | `[Multipart][Post("/upload")] Task Upload(StreamPart file);` | Yes | none | — |
| Raw string body | `[Post("/notes")] Task Note([Body] string text);` | Yes | none | — |
| Query converter | `[Get("/filter")] Task<string> Find([QueryConverter(typeof(FilterConverter))] Filter filter);` | Yes | none | — |
| Query object of unknown shape | `[Get("/query")] Task<string> Search(object filters);` | No: `UnsupportedQueryType` | n/a | — |
| Multipart part of unknown shape | `[Multipart][Post("/upload")] Task Upload(object payload);` | No: `UnsupportedMultipartPart` | n/a | — |
| `[FormObject]` multipart part | `[Multipart][Post("/upload")] Task Upload([FormObject] Form form);` | No: `FormObjectMultipartPart` | n/a | — |
| Generic form-url-encoded body | `[Post("/form")] Task Post<T>([Body(BodySerializationMethod.UrlEncoded)] T form);` | No: `GenericUrlEncodedBody` | n/a | — |
| Path value of unknown shape | `[Get("/users/{id}")] Task<string> Get(object id);` | No: `UnsupportedPathParameterType` | n/a | — |
| Synchronous return | `[Get("/sync")] string Sync();` | No: `UnsupportedReturnType` | n/a | — |

[Refit diagnostics](diagnostics.md) lists every warning and error that Refit reports while you build.

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
