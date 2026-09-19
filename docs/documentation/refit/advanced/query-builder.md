---
Order: 3
---
# Generated query builder

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/advanced-query-builder/advanced-query-builder.csproj).

Building a query string means handling small details consistently: escaping spaces, leaving
out missing values and joining collections in the format the service expects.
`GeneratedQueryStringBuilder` is the helper Refit's generated clients use for that work.

You can use it directly when writing client infrastructure that already knows the parameter
names and values. For an ordinary service call, the interface's query attributes let Refit
choose these steps for you.

## Append pairs and flags

The [complete .NET 10 / C# 14 query sample](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/RuntimeHelpers/QuerySample.cs)
checks the examples against the Refit source project. It uses the `Refit` namespace.

The builder is a `ref struct`: keep it local, pass it by `ref` when another helper updates it,
and finish it before an `await`. A copy can duplicate ownership of its pooled buffers.
These calls append supplied values; they do not discover an object's properties.

The constructor taking only `relativePath` detects an existing `?`. The constructor with
`hasQuery` trusts your answer; generated code supplies a value known at compile time.
An existing template query stays ahead of the new parameters. Calls preserve their order.

`Add` escapes the key and value with URI data escaping. A null value omits the pair;
an empty string emits `key=`. `preEncoded: true` appends both parts verbatim.
`AddPreEscapedKey` always appends its key verbatim and escapes only its value, unless
`preEncoded` is true. Supply an escaped key, rather than an ordinary key containing spaces.
`AddFlag` appends a key without `=`. A null flag is omitted.

`AddFormatted<T>` and `AddFormattedPreEscapedKey<T>` accept `ISpanFormattable` values.
They format with the invariant culture, apply the supplied format, and escape the rendered
value. These methods avoid an intermediate formatted string. Their keys follow the same
rules as `Add` and `AddPreEscapedKey` respectively.

```csharp
GeneratedQueryStringBuilder builder = new("/search?fixed=1");
builder.Add("term", "a b", preEncoded: false);
builder.Add("missing", null, preEncoded: false);
builder.AddPreEscapedKey("already%20escaped", "x/y", preEncoded: false);
builder.Add("raw", "a%2Fb", preEncoded: true);
builder.AddFlag("verbose", preEncoded: false);
builder.AddFlag(null, preEncoded: false);
builder.AddFormatted("page", SampleValues.Count, "D3", preEncoded: false);
builder.AddFormattedPreEscapedKey("row%20count", SampleValues.Rows, null, preEncoded: false);
string path = builder.Build();
```

The result is `/search?fixed=1&term=a%20b&already%20escaped=x%2Fy&raw=a%2Fb&verbose&page=012&row%20count=25`.
`Build` returns the original path when no parameter was appended. It releases the pooled
buffers; treat it as the last operation on this builder.

## Append a collection

Call `BeginCollection`, then `AddCollectionValue` or `AddCollectionValueFormatted<T>`,
then `EndCollection`. Collections must not overlap or nest. The source checks this call
order with debug assertions; it does not provide a release-mode validation layer.

| Format | Result |
| --- | --- |
| `Multi` | One pair per non-null element; an empty collection emits nothing. |
| `Csv` or `RefitParameterFormatter` | One comma-joined value. |
| `Ssv` | One value joined with spaces. |
| `Tsv` | One value joined with tabs. |
| `Pipes` | One value joined with vertical bars. |
| `Indexed` | This low-level helper joins with commas. Generated query-object code performs [indexed expansion](../requests/queries.md) separately. |

For joined formats, a null element contributes an empty position. An empty collection
still emits `key=`. `EndCollection` escapes the complete joined value, including delimiters,
unless `preEncoded` is true. The formatted element overload uses invariant culture with
no format string. End each collection before calling `Build`.

```csharp
const string searchPath = "/search";
GeneratedQueryStringBuilder collections = new(searchPath, hasQuery: false);
collections.BeginCollection("tag", CollectionFormat.Multi, preEncoded: false);
collections.AddCollectionValue("a b");
collections.AddCollectionValue(null);
collections.AddCollectionValueFormatted(SampleValues.Element);
collections.EndCollection();
collections.BeginCollection("ids", CollectionFormat.Csv, preEncoded: false);
collections.AddCollectionValueFormatted(1);
collections.AddCollectionValue(null);
collections.AddCollectionValue("3");
collections.EndCollection();
collections.BeginCollection("empty", CollectionFormat.Pipes, preEncoded: false);
collections.EndCollection();
string collectionPath = collections.Build();
```

This produces `/search?tag=a%20b&tag=7&ids=1%2C%2C3&empty=`.

## Supply attributes and configured formatting

Generated code supplies attribute providers to formatters without inspecting a parameter
with reflection. `GeneratedSingleTypeParameterAttributeProvider` takes one exact attribute
type and its `object[]`. `GeneratedParameterAttributeProvider` takes a
`Dictionary<Type, object[]>` for several types. Its static `Empty` field supplies no attributes.
Both implement `ICustomAttributeProvider`.

For both types, `GetCustomAttributes(bool)` returns every attribute;
`GetCustomAttributes(Type, bool)` returns only the array for that exact type, or an empty
array. `IsDefined(Type, bool)` checks the configured type or dictionary key, even if its
array is empty. Base attribute types are not searched and `inherit` is ignored.
Null types are invalid. The multi-type provider caches its flattened array on first access.
The providers retain the supplied dictionary and arrays, and return shared arrays.
Treat their contents as immutable after construction, including the flattened result.

An empty array for a configured `QueryAttribute` is a current compatibility problem:
the providers report the type as defined, and the default URL formatter then indexes
the first entry. Calling `FormatUrlParameter` with that provider throws
`IndexOutOfRangeException`, rather than treating it as an absent formatting attribute.
The complete sample reproduces and asserts this failure. Omit the dictionary key or
use `GeneratedParameterAttributeProvider.Empty` when there is no query attribute.

```csharp
QueryAttribute query = new() { Format = "D3" };
object[] queryAttributes = [query];
GeneratedSingleTypeParameterAttributeProvider single = new(typeof(QueryAttribute), queryAttributes);
GeneratedParameterAttributeProvider multiple = new(new Dictionary<Type, object[]> { [typeof(QueryAttribute)] = queryAttributes, [typeof(AliasAsAttribute)] = [new AliasAsAttribute("page")], });
string? formatted = GeneratedRequestRunner.FormatUrlParameter(new(), SampleValues.Count, single, typeof(int));
object[] all = multiple.GetCustomAttributes(inherit: true);
object[] exact = multiple.GetCustomAttributes(typeof(QueryAttribute), inherit: false);
bool hasQuery = single.IsDefined(typeof(QueryAttribute), inherit: true);
```

Here `formatted` is `012`. `GeneratedRequestRunner.FormatUrlParameter` first chooses a
formatter registered for the value's exact runtime type, then falls back to the settings'
URL formatter. It passes your attribute provider and declared type to that formatter.
`FormatInvariant<T>` instead calls an `IFormattable` value directly with invariant culture;
it does not consult a formatter or map.

`BuildQueryKey` uses an explicit alias if supplied. Otherwise it applies the configured
key formatter to the CLR name. It then prepends `prefixSegment` verbatim; include the
delimiter in that segment. The three guards `UsesDefaultUrlParameterFormatting`,
`UsesDefaultFormUrlEncodedParameterFormatting`, and `UsesDefaultUrlParameterKeyFormatting`
tell generated code whether its inline formatting matches the settings. The URL guard also
requires an empty formatter map and a pristine default formatter. The other guards require
the exact built-in formatter type, so a derived formatter disables them.

`AddFormattedCollectionProperty` updates a builder by `ref` for a flattened query property.
It formats each element with `ElementProviderType`, then formats each result or joined
result with `JoinedProvider` and `JoinedType`. This preserves customized formatter behavior.
A null collection appends nothing. `Multi` keeps elements separate; other formats join them
before the second pass. The generated fast path uses the collection methods directly when
the default formatter makes that extra pass unnecessary.

The runnable sample checks these guards and the two-pass helper. `SampleValues.Items`
contains `1` and `2`; `Check.Require` fails if the demonstrated result differs.

```csharp
Check.Require(GeneratedRequestRunner.UsesDefaultUrlParameterFormatting(settings), "Pristine URL formatting permits inlining.");
Check.Require(GeneratedRequestRunner.UsesDefaultFormUrlEncodedParameterFormatting(settings), "Pristine form formatting permits inlining.");
Check.Require(GeneratedRequestRunner.UsesDefaultUrlParameterKeyFormatting(settings), "Pristine key formatting permits inlining.");
Check.Require(GeneratedRequestRunner.FormatInvariant(SampleValues.Count, "D3") == "012", "Invariant formatting applies the requested format.");
Check.Require(GeneratedRequestRunner.BuildQueryKey(settings, "Name", "alias", "filter.") == "filter.alias", "Explicit aliases bypass key formatting.");
const string unchangedPath = "/items";
GeneratedQueryStringBuilder query = new(unchangedPath);
GeneratedRequestRunner.AddFormattedCollectionProperty(
    ref query,
    settings,
    SampleValues.Items,
    "ids",
    CollectionFormat.Csv,
    false,
    (typeof(int[]), GeneratedParameterAttributeProvider.Empty, typeof(int[])));
Check.Require(query.Build() == "/items?ids=1%2C2", "Collection properties use two formatting passes.");
```

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
| --- | --- | --- |
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
| --- | --- | --- |
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
| --- | --- | --- |
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
