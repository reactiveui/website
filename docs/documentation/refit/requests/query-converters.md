---
Order: 4
---
# Query converters

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/requests-query-converters/requests-query-converters.csproj).

A service's search parameters may not match the object your app works with. For example,
your app might hold one search object while the service expects separate `q` and `limit`
values in the URL. A query converter lets you write that mapping once.

Refit calls your converter while building the request. You choose which values to include
and what to call them, while keeping the interface method convenient for your callers.

## Write a service-specific mapping

**1. Choose the input type.** This example sends a search name and a result limit.

```csharp
internal sealed record SearchChoice(string Name, int Limit);
```

**2. Implement `IQueryConverter<T>.Flatten`.** Add the entries to the supplied builder.
Pass `false` for `preEncoded` to let it escape ordinary names and values.
Use `keyPrefix` so a `Query` prefix on the argument still works.

```csharp
internal sealed class SearchChoiceConverter : IQueryConverter<SearchChoice>
{
    public void Flatten(SearchChoice value, string keyPrefix, ref GeneratedQueryStringBuilder builder, RefitSettings settings)
    {
        builder.Add($"{keyPrefix}q", value.Name, false);
        builder.Add($"{keyPrefix}limit", GeneratedRequestRunner.FormatInvariant(value.Limit, null), false);
    }
}
```

`GeneratedRequestRunner.FormatInvariant` renders the limit without relying on the device's language settings.
The converter writes two entries. It does not send a request.
The client shares its converter instance across calls, so keep per-call data in the method.

**3. Name the converter on the argument.** The converter must implement `IQueryConverter<T>`
for the argument's declared type. Its parameterless constructor must be accessible to the generated code.

```csharp
internal interface IConverterApi
{
    [Get("/people")]
    Task<HttpRequestMessage> SearchAsync([QueryConverter(typeof(SearchChoiceConverter))] SearchChoice choice);

    [Get("/people")]
    Task<HttpRequestMessage> PersonAsync([Query(".", "person")] [QueryConverter(typeof(SystemTextJsonQueryConverter<Person>))] Person person);
}
```

**4. Call the generated method.** `SearchLimit` is the sample's constant for `20`.

```csharp
IConverterApi api = RestService.ForGenerated<IConverterApi>(host.Client, host.Settings);
using HttpRequestMessage custom = await api.SearchAsync(new("Ada Lovelace", SearchLimit));
Console.WriteLine(custom.RequestUri); // /people?q=Ada%20Lovelace&limit=20
```

Run the complete [converter example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Queries/Converters.cs)
to check both approaches on this page.

## Reuse generated JSON metadata

`SystemTextJsonQueryConverter<T>` gets property names and getters from the client's JSON serializer.
It uses JSON names, including the naming policy and `JsonPropertyName` attributes.
For a value whose runtime type differs from its declared type, it uses that runtime type's metadata.

Register all those types in a JSON context. See [the AOT setup](../aot.md#make-one-call-ready-for-aot).
The sample's `SampleJsonContext` includes `Person` and uses camel-case JSON names.
These shared fields supply that metadata to the converter.
The file imports `System.Text.Json`.

```csharp
private static readonly JsonSerializerOptions Options = new(SampleJsonContext.Default.Options) { TypeInfoResolver = SampleJsonContext.Default };

private static readonly RefitSettings Settings = new(new SystemTextJsonContentSerializer(Options));
```

The `PersonAsync` declaration above selects the JSON converter and a `person.` prefix.
The resulting query uses `person.id` and `person.name`.

```csharp
IConverterApi jsonApi = RestService.ForGenerated<IConverterApi>(host.Client, Settings);
using HttpRequestMessage json = await jsonApi.PersonAsync(new(1, "Ada"));
Console.WriteLine(json.RequestUri); // /people?person.id=1&person.name=Ada
```

The converter's `Flatten` method skips null properties.
It expands nested objects under dotted names and uses `RefitSettings.CollectionFormat` for collections.
It uses `UrlParameterFormatter` to render values.
It needs `SystemTextJsonContentSerializer` as the client's content serializer.
With another serializer, it throws `NotSupportedException`.

## Generation requirements

`QueryConverterAttribute.ConverterType` holds the converter type you supplied.
The attribute works with generated request building.
If another part of the method prevents request generation, Refit reports `RF007`.
Fix the method so it can generate, or use a different argument shape.

The JSON converter reads the configured JSON metadata at runtime.
Supply a generated context to keep that metadata path ready for Native AOT.

## Call a converter yourself

The parameterless JSON converter also implements the public `IQueryConverter<T>.Flatten` contract.
The method adds entries to a builder; `Build` returns the finished path and releases its rented buffers.
This synchronous helper uses the same generated-metadata `Settings` shown above.
Its constants `QueryPath` and `JsonPrefix` hold `/people` and `person.`.


```csharp
SystemTextJsonQueryConverter<Person> converter = new();
GeneratedQueryStringBuilder builder = new(QueryPath);
converter.Flatten(new(1, "Ada"), JsonPrefix, ref builder, Settings);
string path = builder.Build();
Console.WriteLine(path); // /people?person.id=1&person.name=Ada
```

For this converter, JSON metadata chooses the property names. `AliasAs` and the URL key formatter
do not rename them. Nested names use a dot, and null properties are always omitted, even if a property
has `Query(SerializeNull = true)`. Collection elements are formatted as values rather than expanded as
indexed objects. It calls `UrlParameterFormatter` directly and does not consult `UrlParameterFormatterMap`
or a property's `Query(Format = ...)`. Nesting stops at depth 32. A null root value adds no entries.

The complete example also checks a nested person, a repeated integer collection and an omitted
null object. It registers `JsonQueryEnvelope` in `QueryJsonContext` and uses that generated metadata:

```csharp
SystemTextJsonContentSerializer serializer = new(NestedOptions);
RefitSettings settings = new(serializer) { CollectionFormat = CollectionFormat.Multi };
SystemTextJsonQueryConverter<JsonQueryEnvelope> converter = new();
JsonQueryEnvelope value = new(new(1, "Ada"), [1, SecondCode], null);
GeneratedQueryStringBuilder builder = new(QueryPath);
converter.Flatten(value, "filter.", ref builder, settings);
SampleCheck.Equal("/people?filter.person.id=1&filter.person.name=Ada&filter.codes=1&filter.codes=2", builder.Build());
```

Here, `SecondCode` is `2` and `QueryPath` is `/people`. The converter requires a
`SystemTextJsonContentSerializer`; the example also verifies rejection of an incompatible serializer.
