---
Order: 3
---
# Shared query formatters

[Run the complete page example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Pages/requests-query-formatters/requests-query-formatters.csproj).

Your C# property might be called `PageSize`, while the service expects `page_size`.
It might also require dates in a particular format. A shared formatter applies those rules
across your requests so you can keep the naming and formatting choices in one place.

Refit provides separate formatters for parameter names and values. Start with a built-in naming
rule or date format, then add your own formatter when the service needs something more specific.

## Set shared date rules

**1. Describe the inputs.** The sample builds a report request with one date argument and two object properties.

```csharp
internal interface IFormatterApi
{
    [Get("/reports")]
    Task<HttpRequestMessage> DatesAsync(DateTime day, DateFilter dates);

    [Get("/reports")]
    Task<HttpRequestMessage> ActiveAsync(bool active);
}
```

```csharp
internal sealed class DateFilter
{
    public DateTime Started { get; init; }

    [Query(Format = "yyyy")]
    public DateTime End { get; init; }
}
```

**2. Register formats on the default value formatter.** `AddFormat<DateTime>` supplies a rule for dates.
`AddFormat<DateFilter, DateTime>` supplies a more specific rule for dates inside `DateFilter`.
Pass that formatter through `RefitSettings` when creating the client.
The source file imports `System.Globalization`.
Its constants are `DateFormat = "yyyy-MM-dd"` and `DayText = "2026-09-17"`.

```csharp
DefaultUrlParameterFormatter values = new();
values.AddFormat<DateTime>("yyyy-MM");
values.AddFormat<DateFilter, DateTime>(DateFormat);
RefitSettings settings = new(host.Settings.ContentSerializer) { UrlParameterFormatter = values };
IFormatterApi api = RestService.ForGenerated<IFormatterApi>(host.Client, settings);
DateTime day = DateTime.ParseExact(DayText, DateFormat, CultureInfo.InvariantCulture);
using HttpRequestMessage dates = await api.DatesAsync(day, new() { Started = day, End = day });
Console.WriteLine(dates.RequestUri); // /reports?day=2026-09&Started=2026-09-17&End=2026
```

**3. Keep an attribute for a local exception.** `End` keeps its `Query(Format = "yyyy")` rule.
A property's explicit format wins over a container rule. A container rule wins over a general type rule.

Run the complete [formatter example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Queries/Formatters.cs)
to check the names and values. The [query example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Queries/Queries.cs)
also shows naming formatters applied to flattened query objects.

## Choose a key naming rule

The key formatter changes names taken from C# parameters and properties.
An explicit `AliasAs` name keeps the name you supplied.
Here are the four built-in key formatters. `Key` is the example's constant for `PageSize`.

```csharp
IUrlParameterKeyFormatter[] names =
[
    new DefaultUrlParameterKeyFormatter(),
    new CamelCaseUrlParameterKeyFormatter(),
    new SnakeCaseUrlParameterKeyFormatter(),
    new KebabCaseUrlParameterKeyFormatter(),
];
foreach (IUrlParameterKeyFormatter formatter in names)
{
    Console.WriteLine(formatter.Format(Key)); // PageSize, pageSize, page_size, page-size
}
```

| Key formatter | `Format("PageSize")` | Settings shortcut |
| --- | --- | --- |
| `DefaultUrlParameterKeyFormatter` | `PageSize` | The default settings. |
| `CamelCaseUrlParameterKeyFormatter` | `pageSize` | `RefitSettings.CamelCase()` |
| `SnakeCaseUrlParameterKeyFormatter` | `page_size` | `RefitSettings.SnakeCase()` |
| `KebabCaseUrlParameterKeyFormatter` | `page-size` | `RefitSettings.KebabCase()` |

Each has a parameterless constructor and a `Format(string key)` method.
You can implement `IUrlParameterKeyFormatter.Format` for your own naming rule.
Set `RefitSettings.UrlParameterKeyFormatter` to that formatter.

## Set a value rule for one type

`IUrlParameterFormatter.Format` receives the value, its attributes and the containing type.
It returns the text to put into a URL. Returning null leaves out the value.
This formatter writes booleans as `yes` or `no`.
Its source file imports `System.Reflection` for `ICustomAttributeProvider`.

```csharp
internal sealed class YesNoFormatter : IUrlParameterFormatter
{
    public string? Format(object? value, ICustomAttributeProvider attributeProvider, Type type) => value switch
    {
        true => "yes",
        false => "no",
        _ => null,
    };
}
```

Register it in `UrlParameterFormatterMap` to apply it to booleans.
Other types keep the client's normal value formatter.

```csharp
settings.UrlParameterFormatterMap[typeof(bool)] = new YesNoFormatter();
using HttpRequestMessage active = await api.ActiveAsync(true);
Console.WriteLine(active.RequestUri); // /reports?active=yes
```

The type map takes priority over `RefitSettings.UrlParameterFormatter`.
Refit checks for a registered formatter before using the default one.

## Honor JSON property names

`HonorContentSerializerPropertyNamesInQuery` defaults to true. Set it to false when query
keys should follow CLR names and your key formatter instead of explicit JSON names.
The [query example](https://github.com/reactiveui/refit/blob/main/src/examples/Documentation/Queries/Queries.cs)
uses a model whose `Value` property has `[JsonPropertyName("wire-name")]` and checks both settings:

```csharp
foreach (bool honor in new[] { true, false })
{
    RefitSettings settings = new(host.Settings.ContentSerializer) { HonorContentSerializerPropertyNamesInQuery = honor, UrlParameterKeyFormatter = new CamelCaseUrlParameterKeyFormatter() };
    IQueryApi api = RestService.ForGenerated<IQueryApi>(host.Client, settings);
    using HttpRequestMessage request = await api.JsonNamesAsync(new() { Value = 1 });
    SampleCheck.Equal(honor ? "/people?wire-name=1" : "/people?value=1", request.RequestUri?.OriginalString);
}
```

This setting affects query names, not JSON body names. An explicit `AliasAs` still takes priority.

## The default value formatter

`DefaultUrlParameterFormatter` uses invariant culture for values such as numbers and dates.
It keeps URL values consistent across device language settings.
For enums, it uses an `EnumMember` value when the enum supplies one.
Otherwise it formats the enum value normally.

`Format(value, attributeProvider, type)` is also public for custom code that needs the same rules.
Supply the attributes and container type so it can apply property and container-specific formats.
`AddFormat` adds a registration. Adding the same registration twice throws an exception.
Both the general-type and container-specific overloads reject duplicates with `ArgumentException`.

Query value rules also apply to path arguments.
Form bodies have a separate `IFormUrlEncodedParameterFormatter` so a form can use a different rule.

Call the built-in formatters directly when building your own URL or form fields. Here `typeof(DateTime)`
supplies an attribute provider with no `Query` attribute. The third URL argument is the containing type,
so the second call selects the `DateFilter` registration. The form formatter takes the format directly.


```csharp
string? general = values.Format(day, typeof(DateTime), typeof(DateTime));
string? contained = values.Format(day, typeof(DateTime), typeof(DateFilter));
DefaultFormUrlEncodedParameterFormatter formValues = new();
string? formDate = formValues.Format(day, DateFormat);
string? omitted = formValues.Format(null, null);
Console.WriteLine(general); // 2026-09
Console.WriteLine(contained); // 2026-09-17
Console.WriteLine(formDate); // 2026-09-17
Console.WriteLine(omitted is null); // True
```

The URL formatter checks that the attribute provider is non-null before checking the value.
A null value then returns null. Registrations match the value's exact runtime type and the exact container type.
Blank attribute formats allow the registered rules to apply. Strings keep their text regardless of a format.
The form formatter also uses invariant culture and `EnumMember`, but has no `AddFormat` registrations.

## API reference

| API | Description | Parameters | Returns and behavior |
| --- | --- | --- | --- |
| [`IUrlParameterFormatter.Format(object? value, ICustomAttributeProvider attributeProvider, Type type)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IUrlParameterFormatter.cs) | Defines how an implementation converts a URL parameter value. | `value`: [`object`](https://learn.microsoft.com/en-us/dotnet/api/system.object); `attributeProvider`: [`ICustomAttributeProvider`](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.icustomattributeprovider); `type`: containing [`Type`](https://learn.microsoft.com/en-us/dotnet/api/system.type) | Returns [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), or `null` to omit the value. |
| [`IFormUrlEncodedParameterFormatter.Format(object? value, string? formatString)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IFormUrlEncodedParameterFormatter.cs) | Defines how an implementation converts a form-url-encoded field value. | `value`: [`object`](https://learn.microsoft.com/en-us/dotnet/api/system.object); `formatString`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) format, which may be `null` | Returns [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string), or `null` to omit the field. |
| [`IUrlParameterKeyFormatter.Format(string key)`](https://github.com/reactiveui/refit/blob/main/src/Refit/IUrlParameterKeyFormatter.cs) | Defines how an implementation converts a URL parameter name into its wire key. | `key`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key | Returns the formatted [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key. |
| [`DefaultUrlParameterKeyFormatter()`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultUrlParameterKeyFormatter.cs) | Creates the default key formatter. | None | Creates a formatter whose `Format` method returns each key unchanged. |
| [`DefaultUrlParameterKeyFormatter.Format(string key)`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultUrlParameterKeyFormatter.cs) | Applies the identity naming rule to a URL parameter key. | `key`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key | Returns the same key. |
| [`CamelCaseUrlParameterKeyFormatter()`](https://github.com/reactiveui/refit/blob/main/src/Refit/CamelCaseUrlParameterKeyFormatter.cs) | Creates a key formatter that converts leading uppercase letters to camelCase. | None | Creates a camelCase key formatter. |
| [`CamelCaseUrlParameterKeyFormatter.Format(string key)`](https://github.com/reactiveui/refit/blob/main/src/Refit/CamelCaseUrlParameterKeyFormatter.cs) | Converts the leading uppercase run of a key to camelCase and leaves keys that do not start with uppercase unchanged. | `key`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key | Returns the camelCase [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key. |
| [`SnakeCaseUrlParameterKeyFormatter()`](https://github.com/reactiveui/refit/blob/main/src/Refit/SnakeCaseUrlParameterKeyFormatter.cs) | Creates a key formatter that separates words with underscores. | None | Creates a snake_case key formatter. |
| [`SnakeCaseUrlParameterKeyFormatter.Format(string key)`](https://github.com/reactiveui/refit/blob/main/src/Refit/SnakeCaseUrlParameterKeyFormatter.cs) | Converts a key to snake_case. | `key`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key | Returns the snake_case [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key. |
| [`KebabCaseUrlParameterKeyFormatter()`](https://github.com/reactiveui/refit/blob/main/src/Refit/KebabCaseUrlParameterKeyFormatter.cs) | Creates a key formatter that separates words with hyphens. | None | Creates a kebab-case key formatter. |
| [`KebabCaseUrlParameterKeyFormatter.Format(string key)`](https://github.com/reactiveui/refit/blob/main/src/Refit/KebabCaseUrlParameterKeyFormatter.cs) | Converts a key to kebab-case. | `key`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key | Returns the kebab-case [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) key. |
| [`DefaultFormUrlEncodedParameterFormatter()`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultFormUrlEncodedParameterFormatter.cs) | Creates the default form-url-encoded value formatter. | None | Creates an invariant-culture formatter that uses `EnumMember` values when available. |
| [`DefaultFormUrlEncodedParameterFormatter.Format(object? value, string? formatString)`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultFormUrlEncodedParameterFormatter.cs) | Formats a form value with an optional format string. | `value`: [`object`](https://learn.microsoft.com/en-us/dotnet/api/system.object); `formatString`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) format, which may be `null` | Returns invariant-culture [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) text, uses an `EnumMember` value when available, and returns `null` for a `null` value. |
| [`DefaultUrlParameterFormatter()`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultUrlParameterFormatter.cs) | Creates the default URL value formatter. | None | Creates an invariant-culture formatter with no registered formats. |
| [`DefaultUrlParameterFormatter.AddFormat<TParameter>(string format)`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultUrlParameterFormatter.cs) | Registers a format for values whose runtime type is exactly `TParameter`. | `format`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) format; `TParameter`: value type | Returns [`void`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void); adding the same type twice throws [`ArgumentException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception). A non-blank query attribute format takes precedence. |
| [`DefaultUrlParameterFormatter.AddFormat<TContainer, TParameter>(string format)`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultUrlParameterFormatter.cs) | Registers a format for an exact `TParameter` value inside an exact `TContainer` type. | `format`: [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) format; `TContainer`: containing type; `TParameter`: value type | Returns [`void`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/void); duplicate container/type registrations throw [`ArgumentException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception). A non-blank query attribute format takes precedence. |
| [`DefaultUrlParameterFormatter.Format(object? value, ICustomAttributeProvider attributeProvider, Type type)`](https://github.com/reactiveui/refit/blob/main/src/Refit/DefaultUrlParameterFormatter.cs) | Formats a URL value using a query attribute format, a container-specific registration, or a general type registration. | `value`: [`object`](https://learn.microsoft.com/en-us/dotnet/api/system.object); `attributeProvider`: [`ICustomAttributeProvider`](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.icustomattributeprovider); `type`: containing [`Type`](https://learn.microsoft.com/en-us/dotnet/api/system.type) | Returns invariant-culture [`string`](https://learn.microsoft.com/en-us/dotnet/api/system.string) text, uses an `EnumMember` value when available, and returns `null` for a `null` value. Throws [`ArgumentNullException`](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception) when `attributeProvider` is `null`. |
