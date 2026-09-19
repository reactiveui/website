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
WirePerson? person = await serializer.FromHttpContentAsync<WirePerson>(content, CancellationToken.None);
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

| Member | Behavior and significant inputs |
| --- | --- |
| `NewtonsoftJsonContentSerializer()` | Resolves `JsonConvert.DefaultSettings` lazily, or creates settings. Forces inherited `TypeNameHandling` to `None`. |
| `NewtonsoftJsonContentSerializer(JsonSerializerSettings?)` | Retains supplied settings. Null selects the lazy default path. Explicit settings keep their configured `TypeNameHandling`. |
| `ToHttpContent<T>(T)` | Creates UTF-8 `application/json` content through `JsonConvert.SerializeObject`. |
| `FromHttpContentAsync<T>(HttpContent, CancellationToken)` | Buffers the content, then reads JSON through a stream reader. A null content argument returns default. Cancellation controls buffering and opening the stream. The subsequent JSON parsing is synchronous. |
| `DeserializeFromString<T>(string)` | Uses `JsonConvert.DeserializeObject` with the same settings. Invalid JSON can throw. |
| `GetFieldNameForProperty(PropertyInfo)` | Returns an explicit JSON property name; rejects null property metadata. |

The reader honors the HTTP content's charset. Missing charset uses UTF-8. An invalid charset
throws `InvalidOperationException`. The async reader closes the content stream after reading.
Do not expect to reuse that stream afterward.

Default settings disable type-name handling because untrusted JSON must not choose runtime
types through `$type` fields. Configure polymorphism deliberately when your service needs it.
Set serializer settings before sharing the serializer between requests.

## Run the example

The [complete optional serializer project](https://github.com/reactiveui/refit/tree/main/src/examples/Documentation/Serialization/Other)
targets .NET 10 and C# 14. It references local Refit source and checks both JSON and XML round trips.
Run from the Refit checkout's `src` folder:

```bash
dotnet run --project examples/Documentation/Serialization/Other/Other.csproj -c Release -p:LangVersion=14.0
```

This is a separate reflection-dependent executable. The native documentation runner uses
System.Text.Json with generated metadata instead.
