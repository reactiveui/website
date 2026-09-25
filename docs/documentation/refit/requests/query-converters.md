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
public sealed record SearchChoice(string Name, int Limit);
```

**2. Implement `IQueryConverter<T>.Flatten`.** Add the entries to the supplied builder.
Pass `false` for `preEncoded` to let it escape ordinary names and values.
Use `keyPrefix` so a `Query` prefix on the argument still works.

```csharp
public sealed class SearchChoiceConverter : IQueryConverter<SearchChoice>
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
The [query builder](../advanced/query-builder.md) page describes every `builder` method.

**3. Name the converter on the argument.** The converter must implement `IQueryConverter<T>`
for the argument's declared type. Its public parameterless constructor must be available to the generated code.

```csharp
public interface IPeopleApi
{
    [Get("/people")]
    Task<List<Person>> SearchAsync([QueryConverter(typeof(SearchChoiceConverter))] SearchChoice choice, CancellationToken cancellationToken);

    [Get("/people")]
    Task<List<Person>> FindAsync([Query(".", "person")] [QueryConverter(typeof(SystemTextJsonQueryConverter<Person>))] Person person, CancellationToken cancellationToken);
}
```

For `SearchAsync`, Refit generates a cached converter and one `Flatten` call. This excerpt is
trimmed from the generator's output:

```csharp
private static readonly SearchChoiceConverter queryConverter = new SearchChoiceConverter();

// Inside the generated SearchAsync:
var query = new GeneratedQueryStringBuilder("/people", false);
if (choice != null)
{
    queryConverter.Flatten(choice, "", ref query, settings);
}
```

The generated code keeps one converter instance per converter type, so keep per-call data in
the method. A null argument skips the converter. For `FindAsync`, Refit passes `"person."`
as `keyPrefix`: the prefix from `[Query]` plus its delimiter.

**4. Call the generated method.**

```csharp
IPeopleApi api = RestService.ForGenerated<IPeopleApi>(httpClient, settings);
List<Person> people = await api.SearchAsync(new SearchChoice("Ada Lovelace", 20), cancellationToken);
// GET /people?q=Ada%20Lovelace&limit=20
```

## Reuse generated JSON metadata

`SystemTextJsonQueryConverter<T>` gets property names and getters from the client's JSON serializer.
It uses JSON names, including the naming policy and `JsonPropertyName` attributes.
For a value whose runtime type differs from its declared type, it uses that runtime type's metadata.

Register all those types in a JSON context. See [the AOT setup](../aot.md#make-one-call-ready-for-aot).
The example's context class, `SampleJsonContext`, lists `Person` and uses camel-case JSON names.
Build the client's settings from that context:

```csharp
JsonSerializerOptions options = new(SampleJsonContext.Default.Options) { TypeInfoResolver = SampleJsonContext.Default };
RefitSettings settings = new(new SystemTextJsonContentSerializer(options));
```

The `FindAsync` declaration above selects the JSON converter and a `person.` prefix.
The query uses `person.id` and `person.name`:

```csharp
IPeopleApi api = RestService.ForGenerated<IPeopleApi>(httpClient, settings);
List<Person> people = await api.FindAsync(new Person(1, "Ada"), cancellationToken);
// GET /people?person.id=1&person.name=Ada
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
You can call it to see the query it writes. `Flatten` adds entries to a builder.
`Build` returns the finished path and releases the builder's rented buffers.
This call uses the `settings` built from `SampleJsonContext` above:

```csharp
SystemTextJsonQueryConverter<Person> converter = new();
GeneratedQueryStringBuilder query = new("/people");
converter.Flatten(new Person(1, "Ada Lovelace"), "person.", ref query, settings);
string url = query.Build(); // "/people?person.id=1&person.name=Ada%20Lovelace"
```

For this converter, JSON metadata chooses the property names. `AliasAs` and the URL key formatter
do not rename them. Nested names use a dot, and null properties are always omitted, even if a property
has `Query(SerializeNull = true)`. Collection elements are formatted as values rather than expanded as
indexed objects. It calls `UrlParameterFormatter` directly and does not consult `UrlParameterFormatterMap`
or a property's `Query(Format = ...)`. Nesting stops at depth 32. A null root value adds no entries.

A nested object and a collection flatten like this. `PeopleFilter` must be registered on the
JSON context that `options` comes from, and `CollectionFormat.Multi` repeats the key for each
code. The null `Manager` adds nothing:

```csharp
public sealed record PeopleFilter(Person Person, int[] Codes, Person? Manager);
```

```csharp
RefitSettings settings = new(new SystemTextJsonContentSerializer(options)) { CollectionFormat = CollectionFormat.Multi };
SystemTextJsonQueryConverter<PeopleFilter> converter = new();
GeneratedQueryStringBuilder query = new("/people");
converter.Flatten(new PeopleFilter(new Person(1, "Ada"), [10, 20], null), "filter.", ref query, settings);
string url = query.Build(); // "/people?filter.person.id=1&filter.person.name=Ada&filter.codes=10&filter.codes=20"
```

## API reference

| API | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`IQueryConverter<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IQueryConverter.cs) | Defines a source-generated converter that writes one parameter's query pairs into a [`GeneratedQueryStringBuilder`](../advanced/query-builder.md). | `T`: the declared parameter type handled by the converter. | Interface implemented by a custom query converter; generated request code caches one instance per converter type. |
| [`IQueryConverter<T>.Flatten(T value, string keyPrefix, ref GeneratedQueryStringBuilder builder, RefitSettings settings)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IQueryConverter.cs) | Writes the non-null query pairs for `value` into `builder`, prefixing each key with `keyPrefix`. | `value`: the declared query value; `keyPrefix`: the prefix from [`QueryAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/QueryAttribute.cs), or an empty string; `builder`: the mutable query builder; `settings`: the active [`RefitSettings`](../clients/settings.md). No parameter has a default. | [`void`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void); appends pairs in place. The converter is used by generated requests and is not used by the reflection request builder. |
| [`QueryConverterAttribute`](https://github.com/reactiveui/refit/blob/main/src/Refit/QueryConverterAttribute.cs) | Marks a query parameter for flattening by a specified [`IQueryConverter<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IQueryConverter.cs) implementation. | None. Apply it to a method parameter. | Attribute consumed by source-generated request code; the converter type must have a public parameterless constructor and match the parameter's declared type. |
| [`QueryConverterAttribute(Type converterType)`](https://github.com/reactiveui/refit/blob/main/src/Refit/QueryConverterAttribute.cs) | Selects the converter type that generated request code instantiates for the annotated parameter. | `converterType`: the [`Type`](https://learn.microsoft.com/en-us/dotnet/api/system.type) implementing [`IQueryConverter<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IQueryConverter.cs). No default. | [`void`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void); stores `converterType` in [`ConverterType`](https://github.com/reactiveui/refit/blob/main/src/Refit/QueryConverterAttribute.cs). |
| [`QueryConverterAttribute.ConverterType`](https://github.com/reactiveui/refit/blob/main/src/Refit/QueryConverterAttribute.cs) | Identifies the converter implementation selected for the annotated parameter. | None; read-only [`Type`](https://learn.microsoft.com/en-us/dotnet/api/system.type) property. | [`Type`](https://learn.microsoft.com/en-us/dotnet/api/system.type); returns the exact type passed to the constructor. |
| [`SystemTextJsonQueryConverter<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonQueryConverter.cs) | Provides a JSON-metadata-based [`IQueryConverter<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/IQueryConverter.cs) for nested, polymorphic, and otherwise runtime-shaped query values. | `T`: the declared parameter type. | Converter type; reads property names and getters from [`SystemTextJsonContentSerializer`](../serialization/json.md) metadata. |
| [`SystemTextJsonQueryConverter<T>()`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonQueryConverter.cs) | Creates a JSON metadata query converter for the declared type `T`. | None. | Creates [`SystemTextJsonQueryConverter<T>`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonQueryConverter.cs); it does not capture a value or serializer. |
| [`SystemTextJsonQueryConverter<T>.Flatten(T value, string keyPrefix, ref GeneratedQueryStringBuilder builder, RefitSettings settings)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SystemTextJsonQueryConverter.cs) | Walks the runtime value's JSON metadata and appends scalar, nested-object, and collection values to `builder`. | `value`: the root query value; `keyPrefix`: the prefix for its JSON property names; `builder`: the mutable query builder; `settings`: the active settings, including [`CollectionFormat`](../clients/settings.md) and [`UrlParameterFormatter`](../clients/settings.md). No parameter has a default. | [`void`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void); omits null properties, uses dotted keys for nested objects, formats collection elements according to settings, and stops nested traversal at depth 32. Throws [`NotSupportedException`](https://learn.microsoft.com/en-us/dotnet/api/system.notsupportedexception) unless `settings.ContentSerializer` is a [`SystemTextJsonContentSerializer`](../serialization/json.md). |
