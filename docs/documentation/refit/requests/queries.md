---
Order: 2
---
# Query names, values and collections

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/requests-queries/requests-queries.csproj).

Search and list requests often need extra choices: a search term, a page number or a set of
filters. A query string carries those values after the `?` in a URL, such as `?page=1`.
Refit builds it from your method arguments.

You can keep simple arguments as they are, rename them for the service, or group related
filters in an object. This page shows how Refit handles each form, including collections and missing values.

## Build a search request

**1. Describe the query inputs.** `SearchAsync` keeps the fixed query `active=true` from its route.
`AliasAs` gives `name` the service name `q`. Refit appends `page` and leaves out a null `city`.
The other methods below show the query shapes used later on this page.

```csharp
internal interface IQueryApi
{
    [Get("/people?active=true")]
    Task<HttpRequestMessage> SearchAsync([AliasAs("q")] string name, int page, string? city);

    [Get("/people")]
    Task<HttpRequestMessage> FilterAsync([Query(".", "filter")] SearchFilter filter);

    [Get("/people")]
    Task<HttpRequestMessage> CollectionsAsync([Query(CollectionFormat.Multi)] int[] ids, [Query(CollectionFormat.Csv)] string[] tags, [Query(CollectionFormat.Indexed)] List<Person> people);

    [Get("/people")]
    Task<HttpRequestMessage> FlagsAsync([QueryName] string?[] flags, [AliasAs("q")] [Encoded] string encoded);

    [Get("/people")]
    [QueryUriFormat(UriFormat.Unescaped)]
    Task<HttpRequestMessage> UnescapedAsync(string q);

    [Get("/people")]
    Task<HttpRequestMessage> JsonNamesAsync([Query] JsonNamedValue value);
}
```

**2. Create the generated client.** Use the shared client setup from [the first request](../index.md#your-first-request).
The example creates `api` with `RestService.ForGenerated<IQueryApi>(host.Client)`.

**3. Inspect the query text.** This return type builds a request without sending it.
Refit escapes the space in the name as `%20`.

```csharp
using HttpRequestMessage search = await api.SearchAsync("Ada Lovelace", 1, null);
Console.WriteLine(search.RequestUri); // /people?active=true&q=Ada%20Lovelace&page=1
```

Run the complete [query example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Queries/Queries.cs)
to check the scalar, object, collection, flag and URI-format URLs. The [query-options example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Queries/QueryOptions.cs)
checks constructor choices, value formats and null handling.

## Group values in an object

Refit can expand an object's public readable properties into query entries.
This is called flattening. `Query(".", "filter")` adds `filter.` before each property name.
The first argument chooses the text that joins the prefix and property name.
The second argument chooses the prefix.

```csharp
internal sealed class SearchFilter
{
    [AliasAs("name")]
    public string Term { get; init; } = "Ada Lovelace";

    public int PageSize { get; init; } = 20;

    [Query(Format = "0.00")]
    public decimal Price { get; init; } = 5;

    [Query(SerializeNull = true)]
    public string? Note { get; init; }
}
```

`AliasAs` keeps the explicit name `name`. `SnakeCase` changes `PageSize` to `page_size`.
`Query(Format = "0.00")` gives the price two decimal places.
`SerializeNull = true` sends the null note as `note=`. Without it, Refit leaves out a null property.

```csharp
IQueryApi snakeApi = RestService.ForGenerated<IQueryApi>(host.Client, RefitSettings.SnakeCase());
using HttpRequestMessage grouped = await snakeApi.FilterAsync(new());
Console.WriteLine(grouped.RequestUri); // /people?filter.name=Ada%20Lovelace&filter.page_size=20&filter.price=5.00&filter.note=
```

Nested objects add another property name at each level. A dictionary with supported key and value types
can also supply query entries. The generator follows the declared types when it builds these requests.
For a value whose shape is known only at runtime, use a [query converter](query-converters.md).

## Send a collection

Choose the collection format your service accepts. `Multi` repeats the key once per value.
`Csv` joins values with commas. `Indexed` expands a collection of objects under keys such as `people[0].Id`.
It applies to objects with public readable properties. A collection of simple values uses joined values instead.

```csharp
using HttpRequestMessage collection = await api.CollectionsAsync([1, Second], ["math", "code"], [new(1, "Ada"), new(Second, "Grace")]);
Console.WriteLine(collection.RequestUri); // /people?ids=1&ids=2&tags=math%2Ccode&people[0].Id=1&people[0].Name=Ada&people[1].Id=2&people[1].Name=Grace
```

`Second` is the example's constant for ID `2`.

| `CollectionFormat` | Shape for two string values |
| --- | --- |
| `Csv` | `tags=math%2Ccode` |
| `Ssv` | `tags=math%20code` |
| `Tsv` | `tags=math%09code` |
| `Pipes` | `tags=math%7Ccode` |
| `Multi` | `tags=math&tags=code` |
| `Indexed` | Object properties such as `people[0].Id=1&people[1].Id=2`. |
| `RefitParameterFormatter` | Uses the configured formatter. The default query formatter joins values with commas. |

An explicit `Query(CollectionFormat.Multi)` overrides `RefitSettings.CollectionFormat`.
When the attribute does not select a format, the setting supplies it.
An empty joined collection sends `key=`. An empty `Multi` collection sends no entries.
`Multi` skips null elements. Joined formats keep an empty place for each null element.

For a service whose wire names differ from your C# names, put `AliasAs` on the query properties
and choose `CollectionFormat.Multi` for repeated filters. This keeps the request model readable
while producing the exact keys and repeated values the service expects.

## Send flags and already escaped text

`QueryName` uses the argument's value as a query name with no `=value`.
A collection produces one flag for each non-null element.

`Encoded` tells Refit that the argument is already URL-escaped.
Use it when you already have valid escaped text. Keep the default escaping for ordinary input.
It applies to path arguments, query values and query flags.

```csharp
using HttpRequestMessage flagged = await api.FlagsAsync(["preview", null, "include notes"], "Ada%20Lovelace");
Console.WriteLine(flagged.RequestUri); // /people?preview&include%20notes&q=Ada%20Lovelace
```

`QueryUriFormat` chooses how .NET renders the final path and query.
For example, `UriFormat.Unescaped` keeps a space in the built request's original URI text.
It affects the whole path and query, including values you marked `Encoded`.
Use it only when you need that final URI format.

```csharp
using HttpRequestMessage unescaped = await api.UnescapedAsync("Ada Lovelace");
Console.WriteLine(unescaped.RequestUri?.OriginalString); // /people?q=Ada Lovelace
```

## Query attribute choices

| Attribute or property | Use |
| --- | --- |
| `AliasAs(name)` / `Name` | Sets an explicit parameter or property name. |
| `Query()` | Keeps the default delimiter and the configured collection format. |
| `Query(delimiter)` / `Delimiter` | Chooses the text between nested names. The default is `.`. |
| `Query(delimiter, prefix)` / `Prefix` | Adds a name before flattened properties. |
| `Query(delimiter, prefix, format)` / `Format` | Also supplies a value format string. |
| `Query(collectionFormat)` / `CollectionFormat` | Selects a collection format for this argument. |
| `Query.IsCollectionFormatSpecified` | Tells custom code whether the attribute explicitly chose a collection format. |
| `Query.TreatAsString` | Uses the object's `ToString()` result instead of flattening its properties. |
| `Query.SerializeNull` | Sends a null property as an empty value. |
| `QueryName()` | Sends valueless flags. |
| `Encoded()` | Keeps caller-escaped text. |
| `QueryUriFormat(uriFormat)` / `UriFormat` | Sets the final path and query rendering mode. |

For shared naming and value rules, see [query formatters](query-formatters.md).

## Compare constructor and format choices

The five constructors store the delimiter, prefix, value format or explicit collection format.
The default attribute's collection-format property reads `RefitParameterFormatter`, but
`IsCollectionFormatSpecified` is false, so the client's setting still decides the format.
Setting `CollectionFormat`, including through its constructor, makes that flag true.


```csharp
QueryAttribute defaults = new();
QueryAttribute delimiter = new("-");
QueryAttribute prefixed = new("-", Prefix);
QueryAttribute formatted = new("-", Prefix, "yyyy-MM");
QueryAttribute repeated = new(CollectionFormat.Multi) { SerializeNull = true, TreatAsString = true };
Console.WriteLine(defaults.Delimiter); // .
Console.WriteLine(defaults.Prefix is null); // True
Console.WriteLine(defaults.IsCollectionFormatSpecified); // False
Console.WriteLine(delimiter.Delimiter); // -
Console.WriteLine(prefixed.Prefix); // filter
Console.WriteLine(formatted.Format); // yyyy-MM
Console.WriteLine(repeated.CollectionFormat); // Multi
Console.WriteLine(repeated.IsCollectionFormatSpecified); // True
```

The three-argument `Query` formats a scalar value such as `amount` below; its prefix and delimiter
do not rename that scalar key. On the object argument,
it changes the names but its format is not applied to the object's properties. `Started` therefore
keeps its default invariant date text; `End` uses its own `Query(Format = "yyyy")` attribute.
Put formats on properties, or use a container rule from [query formatters](query-formatters.md).


```csharp
internal interface IQueryOptionsApi
{
    [Get("/reports")]
    Task<HttpRequestMessage> DatesAsync([Query("-", "filter", "yyyy-MM")] DateFilter dates);

    [Get("/reports")]
    Task<HttpRequestMessage> AmountAsync([Query("-", "filter", "0.00")] decimal amount);

    [Get("/reports")]
    Task<HttpRequestMessage> TagsAsync(string?[] tags);

    [Get("/reports")]
    Task<HttpRequestMessage> TextAsync([Query(TreatAsString = true)] SearchText phrase, [Query(Format = "")] SearchText other);
}
```


```csharp
IQueryOptionsApi api = RestService.ForGenerated<IQueryOptionsApi>(host.Client, host.Settings);
DateTime day = DateTime.ParseExact("2026-09-17", "yyyy-MM-dd", CultureInfo.InvariantCulture);
using HttpRequestMessage dates = await api.DatesAsync(new() { Started = day, End = day });
Console.WriteLine(dates.RequestUri); // /reports?filter-Started=09%2F17%2F2026%2000%3A00%3A00&filter-End=2026
using HttpRequestMessage amount = await api.AmountAsync(1);
Console.WriteLine(amount.RequestUri); // /reports?amount=1.00
```

`TreatAsString = true` calls the object's `ToString()` before value formatting.
An explicitly empty `Format = ""` selects the same behavior in generated requests.
A whitespace format does not select that shortcut. Define `ToString` to return the service's text:


```csharp
internal sealed record SearchText(string Value)
{
    public override string ToString() => Value;
}
```


```csharp
using HttpRequestMessage text = await api.TextAsync(new("Ada Lovelace"), new("Grace Hopper"));
Console.WriteLine(text.RequestUri); // /reports?phrase=Ada%20Lovelace&other=Grace%20Hopper
```

This loop builds each scalar collection format with a null middle element. Joined formats retain
its empty place. `Indexed` uses comma-separated values for this scalar collection; the object collection
earlier on this page demonstrates indexed property names. `results` stores the paths for the full sample's assertions.


```csharp
CollectionFormat[] formats =
[
    CollectionFormat.RefitParameterFormatter, CollectionFormat.Csv,
    CollectionFormat.Ssv, CollectionFormat.Tsv, CollectionFormat.Pipes,
    CollectionFormat.Multi, CollectionFormat.Indexed,
];
List<(CollectionFormat Format, string? Path)> results = [];
foreach (CollectionFormat format in formats)
{
    RefitSettings settings = new(host.Settings.ContentSerializer) { CollectionFormat = format };
    IQueryOptionsApi api = RestService.ForGenerated<IQueryOptionsApi>(host.Client, settings);
    using HttpRequestMessage request = await api.TagsAsync(["math", null, "code"]);
    Console.WriteLine($"{format}: {request.RequestUri}");
    results.Add((format, request.RequestUri?.OriginalString));
}
```

Attribute constructors store their supplied strings without validating them. Use a deliberate delimiter
and prefix; a null prefix means no prefix. Leaving out a value format means the normal formatter rules apply.
