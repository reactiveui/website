---
Order: 1
---
# JSON and generated metadata

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/serialization-json/serialization-json.csproj).

Most JSON services exchange objects that your app wants to use as C# models. Refit uses
System.Text.Json by default to write those models into requests and read them from replies.

A generated JSON context tells the serializer which models your app uses and how to handle
them. Setting one up gives your client a clear, reusable configuration and supports trimming
and Native AOT. This page starts with one model, then covers larger sets of types and faster writing.

## Set up a reusable serializer

**1. Declare the models.** This example uses the same `Person` as the first-request walkthrough.

```csharp
internal sealed record Person(int Id, string Name);
```

**2. Register the JSON shapes.** Derive a partial class from `JsonSerializerContext`.
Add `JsonSerializable` for the root types you send and receive.
Register a collection or closed generic shape when that is the request or reply type.

```csharp
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(Person[]))]
[JsonSerializable(typeof(List<Person>))]
internal sealed partial class SampleJsonContext : JsonSerializerContext;
```

Here `Person`, `Person[]` and `List<Person>` are separate JSON roots.
Model properties also contribute their declared types. An `object` property needs each possible runtime type
registered too. The compiler generates the context when you build.
[Microsoft's source-generation guide](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation)
describes these registrations.

**3. Reuse the context, options and serializer.** Copy the context's options so its naming rules stay aligned.
Set `TypeInfoResolver` to that same context.
An `IJsonTypeInfoResolver` supplies JSON metadata for a requested type.

```csharp
private static readonly JsonSerializerOptions Options = new(SampleJsonContext.Default.Options) { TypeInfoResolver = SampleJsonContext.Default, };

private static readonly SystemTextJsonContentSerializer Serializer = new(Options);
```

Give this serializer to `new RefitSettings(Serializer)` and use those settings with your generated client.
Keep the options unchanged after the serializer starts using them.

**4. Write and read content.** These methods form the `IHttpContentSerializer` contract.
`FromHttpContentAsync<T>` also accepts a cancellation token and can return null for JSON null.

```csharp
SystemTextJsonContentSerializer serializer = Serializer;
using HttpContent content = serializer.ToHttpContent(new Person(1, "Ada"));
Person? person = await serializer.FromHttpContentAsync<Person>(content);
Console.WriteLine(person?.Name); // Ada
```

The example owns the content it creates directly and disposes it.
When Refit creates content for an API call, the request owns it.
The serializer can return null for JSON null. Malformed JSON raises a `JsonException`.
If you declare a body as an interface or abstract type, the serializer normally uses its runtime type.
Register that concrete model too. JSON polymorphism settings can keep the declared type's contract.
See: [the runnable serializer examples](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Serialization).

## Typed JSON metadata

`JsonTypeInfo<T>` holds metadata for one JSON type. The context's `Person` property returns
`JsonTypeInfo<Person>`. BCL `JsonSerializer` overloads accept it directly:

```csharp
JsonTypeInfo<Person> personInfo = SampleJsonContext.Default.Person;
string json = JsonSerializer.Serialize(new(1, "Ada"), personInfo);
Person? restored = JsonSerializer.Deserialize(json, personInfo);
Console.WriteLine(restored?.Name); // Ada
```

Refit's serializer uses metadata through `JsonSerializerOptions.TypeInfoResolver`.
Its public methods remain generic: the `T` in `ToHttpContent<T>` or
`FromHttpContentAsync<T>` selects the requested `JsonTypeInfo<T>` from that resolver.
You do not pass `JsonTypeInfo<T>` as a separate Refit argument. Use the typed BCL overloads when
you serialize JSON yourself. Use the resolver setup above when Refit serializes request bodies or
reads responses.

## Serializer capabilities

The built-in serializer implements `IHttpContentSerializer` and three optional capabilities.
Other serializers can implement the capabilities that they support.

| Interface | Description |
| --- | --- |
| [`IHttpContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/IHttpContentSerializer.cs) | Defines the required request-body writer, response-body reader and reflected property-name hook. |
| [`ISynchronousContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/ISynchronousContentSerializer.cs) | Adds synchronous buffered and streamed request-body writers. Refit uses them for `Buffered` and `Streamed` request-body modes. |
| [`ISynchronousContentDeserializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/ISynchronousContentDeserializer.cs) | Adds a reader for an error body that Refit has already buffered as a [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string). |
| [`IStreamingContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/IStreamingContentSerializer.cs) | Adds an incremental response reader for Refit interface methods that return [`IAsyncEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1). |

The [complete serialization example](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Serialization)
exercises each capability with generated metadata and local content. Use the interface that matches
the work your serializer needs to perform; `SystemTextJsonContentSerializer` implements all four.

| API | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`SystemTextJsonContentSerializer()`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates a serializer with Refit's default JSON configuration. | None | Creates and retains a new [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) from `GetDefaultJsonSerializerOptions()`. |
| [`SystemTextJsonContentSerializer(JsonSerializerOptions jsonSerializerOptions)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates a serializer with the supplied JSON configuration. | `jsonSerializerOptions`: [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) that controls JSON conversion and metadata lookup. | Retains and uses the supplied [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) instance. |
| [`SerializerOptions`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Gets the configuration used by this serializer. | None | Returns the same [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) instance passed to the constructor or created by the default constructor. |
| [`GetDefaultJsonSerializerOptions()`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates Refit's general-purpose JSON configuration. | None | Returns a fresh mutable [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) with camel-case names, case-insensitive matching, string-number reading, and Refit's object and enum converters. |
| [`GetFastPathJsonSerializerOptions()`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates options that can use System.Text.Json's source-generated serialization fast path after you assign generated metadata. | None | Returns a fresh mutable [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions) with camel-case names and case-insensitive matching, without Refit converters or custom number handling. |
| [`ToHttpContent<T>(T item)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Serializes a request value through Refit's normal asynchronous JSON-content path. | `item`: `T`, the request value to serialize. | Returns JSON [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent). It uses configured generated metadata when available; an interface or abstract `T` without polymorphism configuration uses the non-null value's runtime type. |
| [`ToHttpContentSynchronous<T>(T item)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Serializes a request value immediately into a buffered JSON body. | `item`: `T`, the request value to serialize. | Returns UTF-8 JSON [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) with a `ByteArrayContent` body and `application/json; charset=utf-8` content type. |
| [`ToStreamingHttpContent<T>(T item)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Creates a request body that serializes a value when the HTTP request sends it. | `item`: `T`, the request value to serialize. | Returns [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) that writes UTF-8 JSON to the request stream with `application/json; charset=utf-8` content type. |
| [`FromHttpContentAsync<T>(HttpContent content, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Reads a JSON HTTP body as a value. | `content`: [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent), the response body; `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken) that cancels the read. Default: `default`. | Returns [`Task<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1) for the deserialized value. |
| [`DeserializeFromString<T>(string content)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Reads an already buffered JSON string. | `content`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), the JSON text. | Returns `T?`, the deserialized value. Invalid JSON throws [`JsonException`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonexception). |
| [`DeserializeStreamAsync<T>(Stream stream, StreamingContentFormat format, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Reads one JSON value at a time from a framed response stream. | `stream`: [`Stream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream), the response body; `format`: [`StreamingContentFormat`](content.md), its JSON array, JSON Lines, or SSE framing; `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken) that cancels enumeration. Default: `default`. | Returns [`IAsyncEnumerable<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1) that yields values as they arrive. See [streaming replies](../results/streaming.md). |
| [`GetFieldNameForProperty(PropertyInfo propertyInfo)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonContentSerializer.cs) | Finds a property's explicit JSON field name for reflected integrations. | `propertyInfo`: [`PropertyInfo`](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo), the property to inspect. | Returns the [`JsonPropertyNameAttribute`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.serialization.jsonpropertynameattribute) name, or `null` when the property has no such attribute. |

```csharp
SystemTextJsonContentSerializer synchronous = Serializer;
using HttpContent buffered = synchronous.ToHttpContentSynchronous(new Person(1, "Ada"));
using HttpContent streamed = synchronous.ToStreamingHttpContent(new Person(1, "Ada"));
Person? fromText = Serializer.DeserializeFromString<Person>(await buffered.ReadAsStringAsync());
Person? fromStream = await Serializer.FromHttpContentAsync<Person>(streamed);
Console.WriteLine(fromText?.Name); // Ada
Console.WriteLine(fromStream?.Name); // Ada
```

`GetFieldNameForProperty` accepts `PropertyInfo`. It supports reflected integrations and honors
`JsonPropertyName`. It returns null when the property has no such attribute.
It does not apply the options' naming policy here. Generated request maps avoid that reflection step.
The example below supplies a known property carrying an explicit name and a property without that attribute.
The same hook is available through `IHttpContentSerializer`; a custom implementation can return a name or null.

```csharp
internal sealed class JsonNamedValue
{
    [JsonPropertyName("wire-name")]
    public int Value { get; init; }
}
```

```csharp
JsonNamedValue model = new();
System.Reflection.PropertyInfo explicitName = model.GetType().GetProperty(nameof(JsonNamedValue.Value))!;
System.Reflection.PropertyInfo policyOnly = typeof(Person).GetProperty(nameof(Person.Name))!;
string? name = Serializer.GetFieldNameForProperty(explicitName);
string? absent = Serializer.GetFieldNameForProperty(policyOnly);
Console.WriteLine(name); // wire-name
Console.WriteLine(absent is null); // True
```

`DeserializeStreamAsync<T>(Stream, StreamingContentFormat, CancellationToken = default)` accepts
array, JSON Lines or SSE framing. Here `streaming` is the serializer's `IStreamingContentSerializer`
capability, and `body` and `format` select one of those inputs. The caller owns this direct input stream.

```csharp
await using MemoryStream stream = new(Encoding.UTF8.GetBytes(body));
await foreach (Person? item in streaming.DeserializeStreamAsync<Person>(stream, format, CancellationToken.None))
{
    Console.WriteLine(item!.Name);
}
```

For framework-specific newline and BOM behavior, see [content readers](content.md).
For stream framing and cancellation, see [streaming replies](../results/streaming.md).

## Defaults and fast-path writers

`SystemTextJsonContentSerializer()` creates a new set of default options.
The options overload uses your instance. `SerializerOptions` exposes that instance.
`GetDefaultJsonSerializerOptions()` returns fresh mutable options with camel-case names,
case-insensitive property matching, reading numbers from strings and Refit's default converters.
On .NET 10 it also rejects duplicate JSON properties.
The default options have no generated resolver. Configure it before the first JSON operation if this
serializer will run in a native app. The sample exercises the default constructor and both options APIs,
and checks that each call to `GetDefaultJsonSerializerOptions` returns an independent instance.

```csharp
SystemTextJsonContentSerializer defaults = new();
JsonSerializerOptions defaultOptions = defaults.SerializerOptions;
defaultOptions.TypeInfoResolver = SampleJsonContext.Default;
JsonSerializerOptions separateDefaults = SystemTextJsonContentSerializer.GetDefaultJsonSerializerOptions();
using HttpContent content = defaults.ToHttpContent(new Person(1, "Ada"));
Person? person = await defaults.FromHttpContentAsync<Person>(content, CancellationToken.None);
Console.WriteLine(person!.Name); // Ada
```


`GetFastPathJsonSerializerOptions()` returns fresh options without Refit's default converters.
Set its resolver before use.
A fast-path writer is generated code that writes a registered model directly.
It helps serialization; reading still needs metadata.

```csharp
JsonSerializerOptions fastOptions = SystemTextJsonContentSerializer.GetFastPathJsonSerializerOptions();
fastOptions.TypeInfoResolver = SampleJsonContext.Default;
SystemTextJsonContentSerializer fastSerializer = new(fastOptions);
using HttpContent fastContent = fastSerializer.ToHttpContentSynchronous(new Person(1, "Ada"));
Person? fromFastContent = await fastSerializer.FromHttpContentAsync<Person>(fastContent);
Console.WriteLine(fromFastContent?.Name); // Ada
```

Use the context's default generation mode when you both write requests and read replies.
That mode provides metadata and writers. A serialization-only context cannot supply all reply-reading metadata.
Refit's `Buffered` and `Streamed` modes use synchronous JSON writing and can use these generated writers.
Its `Default` mode uses async JSON content. .NET can use the fast path for repeated small async payloads,
but that depends on runtime size checks. Do not assume every async request takes that path.

Check these conditions before choosing the fast path:

| Condition | What to do |
| --- | --- |
| Generated writer exists | Use `Default` or `Serialization` generation mode. Keep metadata too when you read replies. |
| No custom converters | Avoid entries in `JsonSerializerOptions.Converters` and [`JsonConverter`](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonconverter) attributes on the model or its members. |
| Compatible options | Keep naming, ignored-member and null-handling options aligned with the generated context. |
| Supported features | Avoid custom encoders, dictionary key policies and reference handling for this path. |
| Supported number writing | Avoid number handling that changes JSON output, such as `WriteAsString`. `AllowReadingFromString` alone does not block writing. |

Unsupported settings can make System.Text.Json use generated metadata instead of its fast writer.
That fallback can still work with AOT. It needs the metadata to be present.
Read [Microsoft Learn's supported options and attributes](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation-modes#serialization-optimization-fast-path-mode)
for the full conditions, fallback behavior and performance guidance.

## Missing metadata and trimming

Trimming removes code that appears unused. Reflection can need a member the trimmer cannot see.
Generated metadata makes the required model contract visible to the build.
Register all request and reply roots, collection shapes, closed generic wrappers and possible runtime types.
In a trimmed or Native AOT app, do not depend on System.Text.Json's reflection fallback. Missing generated
metadata causes JSON operations to fail (the unregistered `Page<Person>` example throws `NotSupportedException`);
register the type in a context instead.

## Combine contexts from separate modules

Keep each module's registrations in its own context. This second context owns a closed generic reply type.

```csharp
internal sealed record Page<T>(T[] Items);
```

```csharp
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(Page<Person>))]
internal sealed partial class PageJsonContext : JsonSerializerContext;
```

Combine the contexts once and reuse the resulting serializer. Both contexts use camel-case names.
The resolver checks them in order and uses the first that supplies the requested type.

```csharp
IJsonTypeInfoResolver resolver = JsonTypeInfoResolver.Combine(SampleJsonContext.Default, PageJsonContext.Default);
JsonSerializerOptions combinedOptions = new(SampleJsonContext.Default.Options) { TypeInfoResolver = resolver };
SystemTextJsonContentSerializer combinedSerializer = new(combinedOptions);
using HttpContent pageContent = combinedSerializer.ToHttpContent(new Page<Person>([new(1, "Ada")]));
Page<Person>? page = await combinedSerializer.FromHttpContentAsync<Page<Person>>(pageContent);
Console.WriteLine(page?.Items[0].Name); // Ada
```

See: [Microsoft Learn on combining source generators](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation#combine-source-generators).

## Check missing registrations

An unregistered type fails instead of being discovered through your generated context.
This check uses the first context alone. It has no registration for `Page<Person>`.

```csharp
try
{
    // Options resolves only SampleJsonContext's roots. Page<Person> belongs to PageJsonContext.
    using HttpContent missingContent = Serializer.ToHttpContent(new Page<Person>([]));
    throw new InvalidOperationException("Missing JSON metadata should fail.");
}
catch (NotSupportedException)
{
    Console.WriteLine("Register Page<Person> or combine the generated contexts.");
}
```

Do not add a reflection resolver to hide missing registrations in an AOT app.
Fix the context and check your [native publish](../aot.md).
An ordinary successful build does not prove that the native app has the required metadata.
