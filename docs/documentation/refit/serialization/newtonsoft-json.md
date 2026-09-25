---
Order: 2
---
# Newtonsoft.Json content

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/serialization-newtonsoft-json/serialization-newtonsoft-json.csproj).

An existing app may already have Newtonsoft.Json converters or settings that define how its
models appear in JSON. Refit can use those same rules for API requests and replies through
the `Refit.Newtonsoft.Json` package.

This page shows how to pass your settings to Refit and check the resulting JSON.
For a new client, start with [System.Text.Json and generated metadata](json.md).

## Configure one serializer

`NewtonsoftJsonContentSerializer` uses runtime reflection. These examples do not establish Native AOT compatibility.

**1. Choose a public model.** The runnable example uses `WirePerson`, with `Id` and `Name`
properties. `[JsonProperty("display_name")]` maps `Name` to the service's JSON field.
Add `using Newtonsoft.Json;` for that attribute and the settings below.

**2. Give Refit the serializer.** The parameterless constructor uses default settings.
The other constructor accepts `JsonSerializerSettings`, or null for those defaults.
Reuse the configured serializer and `RefitSettings` for your client.

```csharp
NewtonsoftJsonContentSerializer defaults = new();
JsonSerializerSettings jsonSettings = new() { NullValueHandling = NullValueHandling.Ignore, TypeNameHandling = TypeNameHandling.None };
NewtonsoftJsonContentSerializer serializer = new(jsonSettings);
RefitSettings settings = new(serializer);
```

Pass `settings` to a [client-creation method](../clients/creation.md).
Request generation and JSON serialization are separate jobs. A generated request client
does not replace Newtonsoft.Json's reflected model handling.

**3. Read a reply.** This direct round trip shows the serializer methods that Refit calls.
The returned `HttpContent` belongs to the caller. Dispose it when you finish.

```csharp
using HttpContent content = serializer.ToHttpContent(new WirePerson { Id = 1, Name = "Ada" });
Console.WriteLine(content.Headers.ContentType?.MediaType); // application/json
WirePerson? person = await serializer.FromHttpContentAsync<WirePerson>(content, cancellationToken);
Console.WriteLine(person?.Name); // Ada
```

## Buffered error bodies and field names

`DeserializeFromString<T>` reads buffered JSON synchronously. Refit can use it when you
[read an exception's content](../results/errors.md#read-an-error-body).
`GetFieldNameForProperty` returns an explicit `JsonPropertyAttribute.PropertyName`.
It returns null when the property has no such name. It does not apply a contract resolver's
general naming rule to query keys.

```csharp
WirePerson? person = serializer.DeserializeFromString<WirePerson>("""{"Id":1,"display_name":"Ada"}""");
string? name = serializer.GetFieldNameForProperty(typeof(WirePerson).GetProperty(nameof(WirePerson.Name))!);
Console.WriteLine(person?.Name); // Ada
Console.WriteLine(name); // display_name
```

## Method reference

| API | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`NewtonsoftJsonContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) | Implements Refit's [`IHttpContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/IHttpContentSerializer.cs) with Newtonsoft.Json. It also implements [`ISynchronousContentDeserializer`](https://github.com/reactiveui/refit/blob/main/src/Refit/ISynchronousContentDeserializer.cs). | No public properties. | Creates JSON request content, reads JSON response content, exposes buffered string deserialization, and maps explicit JSON property names for Refit. |
| [`NewtonsoftJsonContentSerializer()`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) | Creates a serializer with lazily resolved default settings. | None. | Returns [`NewtonsoftJsonContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs). The default path invokes [`JsonConvert.DefaultSettings`](https://www.newtonsoft.com/json/help/html/P_Newtonsoft_Json_JsonConvert_DefaultSettings.htm), creates [`JsonSerializerSettings`](https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_JsonSerializerSettings.htm) when needed, and forces [`TypeNameHandling.None`](https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_TypeNameHandling.htm). |
| [`NewtonsoftJsonContentSerializer(JsonSerializerSettings? jsonSerializerSettings)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) | Creates a serializer with caller-supplied Newtonsoft.Json settings. | `jsonSerializerSettings`: nullable [`JsonSerializerSettings`](https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_JsonSerializerSettings.htm); `null` selects the default-settings path. | Returns [`NewtonsoftJsonContentSerializer`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) and retains a non-null settings object as supplied. |
| [`ToHttpContent<T>(T item)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) | Serializes a value to JSON request content. | `item`: value of generic type `T` to serialize. | Returns [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) containing UTF-8 JSON with media type `application/json`. |
| [`FromHttpContentAsync<T>(HttpContent content, CancellationToken cancellationToken = default)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) | Buffers and deserializes HTTP response content asynchronously. | `content`: [`HttpContent`](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent) to read; `cancellationToken`: [`CancellationToken`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken), default [`CancellationToken.None`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.none). | Returns [`Task<T?>`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1). A null content value returns `default`; otherwise the method reads the content using its charset or UTF-8, deserializes it, and disposes the read stream. |
| [`DeserializeFromString<T>(string content)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) | Deserializes an already buffered JSON string synchronously. | `content`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) containing JSON. | Returns nullable generic `T?` from [`JsonConvert.DeserializeObject<T>`](https://www.newtonsoft.com/json/help/html/M_Newtonsoft_Json_JsonConvert_DeserializeObject__1.htm). Newtonsoft.Json exceptions can propagate for invalid JSON. |
| [`GetFieldNameForProperty(PropertyInfo propertyInfo)`](https://github.com/reactiveui/refit/blob/main/src/Refit.Newtonsoft.Json/NewtonsoftJsonContentSerializer.cs) | Finds the JSON field name that an object property declares explicitly. | `propertyInfo`: [`PropertyInfo`](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo) to inspect. | Returns the [`JsonPropertyAttribute.PropertyName`](https://www.newtonsoft.com/json/help/html/P_Newtonsoft_Json_JsonPropertyAttribute_PropertyName.htm), or `null` when the property has no `JsonPropertyAttribute`. Throws [`ArgumentNullException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception) when `propertyInfo` is null. |

The reader honors the HTTP content's charset. Missing charset uses UTF-8. An invalid charset
throws `InvalidOperationException`. The async reader closes the content stream after reading.
Do not expect to reuse that stream afterward.

Default settings disable type-name handling because untrusted JSON must not choose runtime
types through `$type` fields. Configure polymorphism deliberately when your service needs it.
Set serializer settings before sharing the serializer between requests.

## Run the example

The complete [optional serializer project](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Serialization/Other/Other.csproj)
targets .NET 10 and C# 14. It references local Refit source and checks both JSON and XML round trips.
Run from the Refit checkout's `src` folder:

```bash
dotnet run --project examples/Documentation/Serialization/Other/Other.csproj -c Release -p:LangVersion=14.0
```

This is a separate reflection-dependent executable. The native documentation runner uses
System.Text.Json with generated metadata instead.
