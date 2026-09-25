---
Order: 3
---
# Generated query builder

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/advanced-query-builder/advanced-query-builder.csproj).

Building a query string means handling small details the same way every time: escaping spaces,
leaving out missing values and joining collections in the format the service expects.
`GeneratedQueryStringBuilder` is the helper Refit's generated clients use for that work.

You do not call it for an ordinary service call. You declare query parameters on the interface,
and the source generator writes the builder calls for you. This page shows that generated code,
so you can read it and debug it. You can also call the builder yourself when you write a
[query converter](../requests/query-converters.md) or other client infrastructure.

## What Refit generates for a query

**You write** a method whose parameters become query values:

```csharp
public interface IProductsApi
{
    [Get("/products")]
    Task<List<Product>> SearchAsync(
        string? term,
        [Query(Format = "D3")] int page,
        [Query(CollectionFormat.Multi)] string[] tags,
        CancellationToken cancellationToken);
}
```

**Refit generates** code that builds the query with `GeneratedQueryStringBuilder`. This excerpt is
trimmed from the generator's output. It drops the `global::` prefixes and the `refit` prefix on
local names, and it shows enum values by name where the generator writes a cast such as
`(CollectionFormat)5`.

```csharp
var useDefaultFormatting = GeneratedRequestRunner.UsesDefaultUrlParameterFormatting(settings);
var query = new GeneratedQueryStringBuilder("/products", false);
if (term != null)
{
    query.AddPreEscapedKey("term", useDefaultFormatting ? term : GeneratedRequestRunner.FormatUrlParameter(settings, term, GeneratedParameterAttributeProvider.Empty, typeof(string)), false);
}
if (useDefaultFormatting)
{
    query.AddFormattedPreEscapedKey("page", page, "D3", false);
}
else
{
    query.AddPreEscapedKey("page", GeneratedRequestRunner.FormatUrlParameter(settings, page, pageAttributeProvider, typeof(int)), false);
}
if (tags != null)
{
    query.BeginCollection("tags", CollectionFormat.Multi, false);
    foreach (var tag in tags)
    {
        query.AddCollectionValue(useDefaultFormatting ? tag : GeneratedRequestRunner.FormatUrlParameter(settings, tag, tagsAttributeProvider, typeof(string[])));
    }
    query.EndCollection();
}
var request = new HttpRequestMessage(
    HttpMethod.Get,
    GeneratedRequestRunner.BuildRelativeUri(Client, query.Build(), settings.UrlResolution));
```

The generator knows each key at compile time, so it escapes the key once and calls the
`PreEscapedKey` methods. When you have not customized URL formatting, it formats values inline.
Otherwise it sends each value through your formatter. The later sections explain each call.

A call to that method sends this query:

```csharp
List<Product> products = await api.SearchAsync("blue shoes", 12, ["sale", "new"], cancellationToken);
// GET /products?term=blue%20shoes&page=012&tags=sale&tags=new
```

## Append pairs and flags

The builder is a `ref struct`: keep it local, pass it by `ref` when another method updates it,
and finish it before an `await`. A copy can share its pooled buffers with the original.
These calls append the values you supply. They do not discover an object's properties.

The constructor taking only `relativePath` detects an existing `?`. The constructor with
`hasQuery` trusts your answer; generated code supplies a value known at compile time.
An existing template query stays ahead of the new parameters. Calls keep their order.

`Add` escapes the key and value with URI data escaping. A null value omits the pair.
An empty string emits `key=`. `preEncoded: true` appends both parts verbatim.
`AddPreEscapedKey` always appends its key verbatim. It escapes only its value, unless
`preEncoded` is true. Supply an escaped key to it, such as `sort%20by`, not a key with spaces.
`AddFlag` appends a key without `=`. A null flag is omitted.

`AddFormatted<T>` and `AddFormattedPreEscapedKey<T>` accept `ISpanFormattable` values.
They format with the invariant culture, apply the format you supply, and escape the result.
These methods avoid an intermediate formatted string. Their keys follow the same
rules as `Add` and `AddPreEscapedKey`.

```csharp
GeneratedQueryStringBuilder query = new("/search?sort=price");
query.Add("q", "blue shoes", preEncoded: false);
query.AddFlag("inStock", preEncoded: false);
query.AddFormatted("page", 2, "D3", preEncoded: false);
string url = query.Build(); // "/search?sort=price&q=blue%20shoes&inStock&page=002"
```

`Build` returns the original path when you appended nothing. It releases the pooled
buffers, so make it the last call on the builder.

## Append a collection

Call `BeginCollection`, then `AddCollectionValue` or `AddCollectionValueFormatted<T>` for each
element, then `EndCollection`. Do not overlap or nest collections. The source checks this call
order with debug assertions only. A release build does not validate it.

| Format | Result |
| --- | --- |
| `Multi` | One pair per non-null element; an empty collection emits nothing. |
| `Csv` or `RefitParameterFormatter` | One comma-joined value. |
| `Ssv` | One value joined with spaces. |
| `Tsv` | One value joined with tabs. |
| `Pipes` | One value joined with vertical bars. |
| `Indexed` | This low-level helper joins with commas. Generated query-object code performs [indexed expansion](../requests/queries.md) separately. |

For joined formats, a null element contributes an empty position, and an empty collection
still emits `key=`. `EndCollection` escapes the whole joined value, delimiters included,
unless `preEncoded` is true. The formatted element overload uses the invariant culture with
no format string. End each collection before calling `Build`.

```csharp
GeneratedQueryStringBuilder query = new("/products", hasQuery: false);
query.BeginCollection("tag", CollectionFormat.Multi, preEncoded: false);
query.AddCollectionValue("sale");
query.AddCollectionValue("new arrivals");
query.EndCollection();
query.BeginCollection("ids", CollectionFormat.Csv, preEncoded: false);
query.AddCollectionValueFormatted(3);
query.AddCollectionValueFormatted(7);
query.EndCollection();
string url = query.Build(); // "/products?tag=sale&tag=new%20arrivals&ids=3%2C7"
```

For a `[Query(CollectionFormat.Csv)] int[] ids` parameter, generated code calls
`AddCollectionValueFormatted` for each element when default formatting applies.

## Supply attributes and configured formatting

A custom URL formatter can read the attributes on a parameter, such as `[Query(Format = "D3")]`.
Generated code supplies those attributes through an attribute provider, without inspecting
the parameter with reflection. For the `page` parameter above, **Refit generates** one cached field:

```csharp
private static readonly ICustomAttributeProvider pageAttributeProvider =
    new GeneratedSingleTypeParameterAttributeProvider(typeof(QueryAttribute), new object[] { new QueryAttribute() { Format = "D3" } });
```

`GeneratedSingleTypeParameterAttributeProvider` takes one exact attribute type and its `object[]`.
When a parameter has several attribute types, such as `[Query]` and `[AliasAs]`, generated code
uses `GeneratedParameterAttributeProvider` instead. It takes a `Dictionary<Type, object[]>`.
Its static `Empty` field supplies no attributes. Both implement `ICustomAttributeProvider`.

For both types, `GetCustomAttributes(bool)` returns every attribute.
`GetCustomAttributes(Type, bool)` returns only the array for that exact type, or an empty
array. `IsDefined(Type, bool)` checks the configured type or dictionary key, even if its
array is empty. They do not search base attribute types, and they ignore `inherit`.
Null types are invalid. The multi-type provider caches its flattened array on first access.
The providers keep the dictionary and arrays you supply, and return shared arrays.
Do not change their contents after construction, including the flattened result.

An empty array for a configured `QueryAttribute` causes a failure today.
The providers report the type as defined, and the default URL formatter then reads
the first entry. Calling `FormatUrlParameter` with that provider throws
`IndexOutOfRangeException`, rather than treating the attribute as absent.
The complete sample reproduces this failure. Omit the dictionary key or
use `GeneratedParameterAttributeProvider.Empty` when there is no query attribute.

`GeneratedRequestRunner.FormatUrlParameter` first looks for a formatter registered for the
value's exact runtime type. Otherwise it uses the settings' URL formatter. It passes your
attribute provider and declared type to that formatter:

```csharp
GeneratedSingleTypeParameterAttributeProvider pageAttributes = new(typeof(QueryAttribute), [new QueryAttribute { Format = "D3" }]);
string? page = GeneratedRequestRunner.FormatUrlParameter(new RefitSettings(), 12, pageAttributes, typeof(int)); // "012"
```

## Flatten a query object

A query object is a class whose properties each become a query value.

**You write** the object and a method that takes it with a key prefix:

```csharp
public sealed class ProductFilter
{
    public string? Category { get; set; }

    [Query(CollectionFormat.Csv)]
    public int[]? Ids { get; set; }
}

public interface IProductsApi
{
    [Get("/products")]
    Task<List<Product>> FilterAsync([Query(".", "filter")] ProductFilter filter);
}
```

**Refit generates** one block per property. This excerpt is trimmed the same way as the first one:

```csharp
if (filter != null)
{
    var category = filter.Category;
    if (category != null)
    {
        query.Add(GeneratedRequestRunner.BuildQueryKey(settings, "Category", null, "filter."), useDefaultFormatting ? category : GeneratedRequestRunner.FormatUrlParameter(settings, category, filterAttributeProvider, typeof(ProductFilter)), false);
    }

    var ids = filter.Ids;
    var idsKey = GeneratedRequestRunner.BuildQueryKey(settings, "Ids", null, "filter.");
    if (ids != null)
    {
        if (useDefaultFormatting)
        {
            query.BeginCollection(idsKey, CollectionFormat.Csv, false);
            foreach (var id in ids)
            {
                query.AddCollectionValue(GeneratedRequestRunner.FormatInvariant(id, null));
            }
            query.EndCollection();
        }
        else
        {
            GeneratedRequestRunner.AddFormattedCollectionProperty(ref query, settings, ids, idsKey, CollectionFormat.Csv, false, (typeof(int[]), filterAttributeProvider, typeof(ProductFilter)));
        }
    }
}
```

With default settings, `FilterAsync(new ProductFilter { Category = "shoes", Ids = [3, 7] })`
sends `GET /products?filter.Category=shoes&filter.Ids=3%2C7`.

`BuildQueryKey` uses an explicit alias if one is supplied. Otherwise it applies the configured
key formatter to the property name. It then prepends `prefixSegment` verbatim, so include the
delimiter in that segment:

```csharp
RefitSettings settings = new() { UrlParameterKeyFormatter = new CamelCaseUrlParameterKeyFormatter() };
string key = GeneratedRequestRunner.BuildQueryKey(settings, "Category", null, "filter."); // "filter.category"
string alias = GeneratedRequestRunner.BuildQueryKey(settings, "Category", "cat", "filter."); // "filter.cat"
```

`FormatInvariant<T>` calls an `IFormattable` value directly with the invariant culture.
It does not consult a formatter or the formatter map. For example,
`GeneratedRequestRunner.FormatInvariant(19.99m, null)` returns `"19.99"` even when the current
culture writes `19,99`.

The three guards `UsesDefaultUrlParameterFormatting`, `UsesDefaultFormUrlEncodedParameterFormatting`
and `UsesDefaultUrlParameterKeyFormatting` tell generated code whether its inline formatting
matches the settings. Each returns `true` for a new `RefitSettings`. The URL guard also
requires an empty formatter map and an unchanged default formatter. The other guards require
the exact built-in formatter type, so a derived formatter turns them off.

`AddFormattedCollectionProperty` updates a builder by `ref` for a collection property.
It formats each element with `ElementProviderType`. It then formats each result, or the joined
result, with `JoinedProvider` and `JoinedType`. These two passes keep a custom formatter's
behavior. A null collection appends nothing. `Multi` keeps elements separate; other formats
join them before the second pass. Generated code calls this method only when you have
customized URL formatting. Otherwise it uses the collection methods directly, as shown above.

The three span-format methods are compiled under `NET6_0_OR_GREATER`. Refit's shipped
.NET 8, 9, 10, and 11 surfaces include them; its .NET Framework surfaces omit them.
The [request helpers](request-helpers.md) describe path formatting and body construction.

## Query builder API reference

All arguments in this table are required. A nullable argument accepts `null` but cannot be omitted.
The methods returning `void` update this builder in place. The formatted methods require
`T : ISpanFormattable` and are available on modern .NET, as described above.

| Type | Purpose |
| --- | --- |
| `GeneratedQueryStringBuilder` | A stack-only builder that appends an escaped query string to a relative request path without reflection. |
| `GeneratedParameterAttributeProvider` | Supplies attributes from a dictionary when a generated parameter has more than one attribute type. |
| `GeneratedSingleTypeParameterAttributeProvider` | Supplies one type's attributes without allocating a dictionary or flattening arrays. |

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GeneratedQueryStringBuilder(string relativePath)` | Starts query construction and detects an existing query marker. | [string] `relativePath`: path with escaped dynamic segments and any template query. | A builder that detects whether the path contains `?`. |
| `GeneratedQueryStringBuilder(string relativePath, bool hasQuery)` | Starts query construction using caller-known query state. | [string] `relativePath`: escaped path; [bool] `hasQuery`: whether it contains `?`. | A builder that trusts the supplied query state. |
| `Add(string name, string? value, bool preEncoded)` | Appends one ordinary query pair. | [string] `name`: key; [string] `value`: value or `null`; [bool] `preEncoded`: whether both parts are encoded. | `void`; appends a pair, or omits it for `null`. Empty values produce `key=`. |
| `AddPreEscapedKey(string name, string? value, bool preEncoded)` | Appends a pair whose key has already been escaped. | [string] `name`: escaped key; [string] `value`: value or `null`; [bool] `preEncoded`: whether the value is encoded. | `void`; appends the key verbatim and escapes the value unless `preEncoded` is true. Null omits the pair. |
| `AddFormatted<T>(string name, T value, string? format, bool preEncoded)` | Formats a value invariantly before appending an ordinary pair. | [string] `name`: key; [ISpanFormattable] `value`: value to format; [string] `format`: format or `null`; [bool] `preEncoded`: whether the key and formatted value are encoded. | `void`; formats with invariant culture and appends the pair. |
| `AddFormattedPreEscapedKey<T>(string name, T value, string? format, bool preEncoded)` | Formats a value for a key that has already been escaped. | [string] `name`: escaped key; [ISpanFormattable] `value`: value to format; [string] `format`: format or `null`; [bool] `preEncoded`: whether the formatted value is encoded. | `void`; appends the key verbatim and formats the value with invariant culture. |
| `AddFlag(string? name, bool preEncoded)` | Appends a valueless query flag. | [string] `name`: flag text or `null`; [bool] `preEncoded`: whether it is encoded. | `void`; appends a key without `=`, or omits a null flag. |
| `BeginCollection(string name, CollectionFormat collectionFormat, bool preEncoded)` | Opens a collection whose values will be appended next. | [string] `name`: key; [CollectionFormat] `collectionFormat`: join/repeat rule; [bool] `preEncoded`: whether the key and values are encoded. | `void`; opens a collection. Finish the preceding collection first. |
| `AddCollectionValue(string? value)` | Adds one raw value to the open collection. | [string] `value`: next value, or `null`. | `void`; adds a value to the open collection. Null is omitted for `Multi` and adds an empty position for joined formats. |
| `AddCollectionValueFormatted<T>(T value)` | Formats and adds one value to the open collection. | [ISpanFormattable] `value`: next value to format. | `void`; formats with invariant culture and no format string, then adds it to the open collection. |
| `EndCollection()` | Closes the open collection and writes its joined value when needed. | None. | `void`; finishes the open collection and writes any joined value. |
| `Build()` | Finalizes the path and releases builder storage. | None. | [string]: the completed relative path and query. Releases pooled storage. Treat this as the final operation. |

## Attribute provider API reference

Both providers implement [ICustomAttributeProvider]. They retain supplied arrays rather than
copying them. `inherit` is a required [bool] argument that these implementations ignore.
The `Type` lookups match an exact type rather than its base types.

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GeneratedParameterAttributeProvider(Dictionary<Type, object[]> attributes)` | Creates an attribute provider for parameters with several attribute types. | [`Dictionary<Type, object[]>`][attribute-map] `attributes`: each [Type] and its array of attribute objects. | A provider for several attribute types. |
| `GeneratedParameterAttributeProvider.GetCustomAttributes(bool inherit)` | Returns every configured attribute as one shared array. | [bool] `inherit`: ignored. | [object[]][object]: cached array of all configured attributes. Treat the returned array as read-only. |
| `GeneratedParameterAttributeProvider.GetCustomAttributes(Type attributeType, bool inherit)` | Returns attributes for one exact configured type. | [Type] `attributeType`: exact type to find; [bool] `inherit`: ignored. | [object[]][object]: the stored array, or an empty array when the key is absent. |
| `GeneratedParameterAttributeProvider.IsDefined(Type attributeType, bool inherit)` | Checks whether an exact attribute type has an entry. | [Type] `attributeType`: exact type to find; [bool] `inherit`: ignored. | [bool]: whether the dictionary contains the key, even if its array is empty. |
| `GeneratedSingleTypeParameterAttributeProvider(Type type, object[] attributes)` | Creates an attribute provider optimized for one attribute type. | [Type] `type`: shared attribute type; [object[]][object] `attributes`: attribute objects of that type. | A provider for one attribute type. |
| `GeneratedSingleTypeParameterAttributeProvider.GetCustomAttributes(bool inherit)` | Returns the provider's configured attribute array. | [bool] `inherit`: ignored. | [object[]][object]: the supplied array. Treat it as read-only. |
| `GeneratedSingleTypeParameterAttributeProvider.GetCustomAttributes(Type attributeType, bool inherit)` | Returns attributes only when the requested type matches. | [Type] `attributeType`: exact type to find; [bool] `inherit`: ignored. | [object[]][object]: the supplied array for the configured type, otherwise an empty array. |
| `GeneratedSingleTypeParameterAttributeProvider.IsDefined(Type attributeType, bool inherit)` | Checks whether the requested type matches the configured type. | [Type] `attributeType`: exact type to find; [bool] `inherit`: ignored. | [bool]: whether the type equals the configured type, even if its array is empty. |

| Field | Description | Type | Value |
| --- | --- | --- | --- |
| `GeneratedParameterAttributeProvider.Empty` | Reuses one provider for parameters that declare no attributes. | [`GeneratedParameterAttributeProvider`](query-builder.md) | Shared static readonly provider with no attributes. |

The configured formatting methods discussed above belong to `GeneratedRequestRunner`.
Their overloads are listed in the [request helper reference](request-helpers.md).
The URL formatter entry point accepts either attribute provider:

| Overload | Description | Parameters | Returns |
| --- | --- | --- | --- |
| `GeneratedRequestRunner.FormatUrlParameter(RefitSettings settings, object? value, ICustomAttributeProvider attributeProvider, Type type)` | Formats one query value through the configured URL formatter. | [RefitSettings](../clients/settings.md) `settings`: formatter configuration; [object] `value`: value or `null`; [ICustomAttributeProvider] `attributeProvider`: attributes for formatting; [Type] `type`: declared value type. | [string], nullable: result from the selected URL formatter. |

[string]: https://learn.microsoft.com/dotnet/api/system.string
[bool]: https://learn.microsoft.com/dotnet/api/system.boolean
[object]: https://learn.microsoft.com/dotnet/api/system.object
[Type]: https://learn.microsoft.com/dotnet/api/system.type
[ISpanFormattable]: https://learn.microsoft.com/dotnet/api/system.ispanformattable
[ICustomAttributeProvider]: https://learn.microsoft.com/dotnet/api/system.reflection.icustomattributeprovider
[attribute-map]: https://learn.microsoft.com/dotnet/api/system.collections.generic.dictionary-2
[CollectionFormat]: ../requests/queries.md

Source: [query buffer](https://github.com/reactiveui/refit/blob/main/src/Refit/GeneratedQueryStringBuilder.cs),
[multi-type provider](https://github.com/reactiveui/refit/blob/main/src/Refit/GeneratedParameterAttributeProvider.cs),
[single-type provider](https://github.com/reactiveui/refit/blob/main/src/Refit/GeneratedSingleTypeParameterAttributeProvider.cs),
and [formatting helpers](https://github.com/reactiveui/refit/blob/main/src/Refit/GeneratedRequestRunner.cs).
