---
Order: 4
---
# Converters

[Run the complete page example](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/converters/converters.csproj).

A binding often joins two properties of different types. A text box holds a `string`. The view model holds an `int`. A converter turns a value of one type into a value of another. The binding calls it each time a value crosses from one side to the other.

ReactiveUI.Binding ships converters for the common pairs: text to and from numbers, dates, times, durations, booleans, GUIDs and URIs. Each one also has a nullable form. You rarely call a converter yourself, because a binding finds the right one. The examples on this page call them directly, because that shows what each converter accepts and what it rejects. Writing your own converter belongs on [Custom converters](custom-converters.md).

Four terms carry the page.

- A **converter** is a class with a source type and a target type. `StringToIntegerTypeConverter` reads a `string` and produces an `int`.
- A **conversion hint** is an optional `object` that you pass with the value. Each converter decides what a hint means. Most ignore it.
- An **affinity** is an `int` that a converter reports for its type pair. When several converters serve the same pair, the highest affinity wins. Every built-in converter reports 2, except `EqualityTypeConverter`, which reports 1.
- The **converter service** is the table of converters that bindings look in. `BindingConverters.Current` holds it.

The examples set the current culture to the invariant culture, so their output is the same on every machine. In your own app the converters use the current culture. The output below shows the invariant form.

## Convert one value

**1. Choose the converter for the type pair.** Converter names follow one pattern: `StringToIntegerTypeConverter` converts from `string` to `int`, and `IntegerToStringTypeConverter` converts back. A converter holds no state. Create one with `new`.

**2. Call `TryConvert`.** Pass the value, the conversion hint and an `out` variable for the result. The call returns `true` when the conversion worked. It returns `false` when it did not, and the `out` variable then holds the default of the target type. A converter reports a bad value through the return value and does not throw. Pass `null` as the hint when you have none. The excerpt below converts one good text and one bad text, so you see both results.

```csharp
var converter = new StringToIntegerTypeConverter();

// Success case: valid issue number
var success = converter.TryConvert("42", conversionHint: null, out var issueNumber);
Console.WriteLine(success);
Console.WriteLine(issueNumber);

// Failure case: non-numeric input
var failure = converter.TryConvert(InvalidNumberText, conversionHint: null, out var failResult);
Console.WriteLine(failure);
Console.WriteLine(failResult);
```

The text `not-a-number` fails. The result is `0`, the default `int`.

```text
True
42
False
0
```

**3. Convert back for display.** A converter for the opposite direction turns the `int` into text for a label. Converting a number to text always succeeds. The excerpt formats an issue number and a zero, so you see the text a label would show.

```csharp
var converter = new IntegerToStringTypeConverter();

// Success case: display issue number
var success = converter.TryConvert(IssueNumber, conversionHint: null, out var issueText);
Console.WriteLine(success);
Console.WriteLine(issueText);

// Success case: no issues
_ = converter.TryConvert(0, conversionHint: null, out var noIssuesText);
Console.WriteLine(noIssuesText);
```

```text
True
42
0
```

**4. Let the binding find the converter.** When the two properties of a binding have different types, the binding converts through the converter service. It looks for the converter of that exact pair of types. You write no converter code. A binding can also take conversion functions in place of a converter object, one function for each direction of a two-way binding. [Bindings](bindings.md) shows the binding calls and both ways to convert.

## The converter contract

Every built-in converter implements the same members. The table names them and says where each one comes from.

| Member | Declared on | What it does |
| --- | --- | --- |
| `FromType` | `IBindingTypeConverter` | The source type. |
| `ToType` | `IBindingTypeConverter` | The target type. |
| `GetAffinityForObjects()` | `IBindingTypeConverter` | The priority among converters for the same pair. Zero or less excludes the converter. |
| `TryConvertTyped(object?, object?, out object?)` | `IBindingTypeConverter` | Converts a boxed value. |
| `TryConvert(TFrom?, object?, out TTo?)` | `IBindingTypeConverter<TFrom, TTo>` | Converts a value of the source type, with no boxing. |

`BindingTypeConverter<TFrom, TTo>` is the base class of every converter on this page except two. It supplies `FromType`, `ToType` and `TryConvertTyped`. `TryConvertTyped` returns `false` when the value is not a `TFrom`. It also returns `false` for `null` when `TFrom` is a value type that cannot hold null.

`StringConverter` and `EqualityTypeConverter` implement `IBindingTypeConverter` directly. They have no typed `TryConvert`. You call `TryConvertTyped` on them, as [String and equality](#string-and-equality) shows.

## What success and failure mean

Each group of converters treats a bad value or a `null` the same way.

- **Text to a value type**, such as `StringToIntegerTypeConverter`. `null` and text that does not parse return `false`.
- **Text to a nullable type**, such as `StringToNullableIntegerTypeConverter`. `null` and an empty string succeed with a `null` result. Text that does not parse returns `false`.
- **A value type to text.** The conversion succeeds.
- **A nullable type to text.** The conversion succeeds. A `null` value gives a `null` string.
- **A value type to its nullable form.** The conversion succeeds.
- **A nullable type to its value type.** A `null` value returns `false`.

A generated binding treats `false` as "no value to write". It writes nothing for that value, and the target keeps the value it has. The binding stays connected, so the next value is converted as usual. It logs nothing and throws nothing. The same happens when no converter serves the pair of types.

One case differs. Suppose you passed no converter of your own, and the source type can be assigned to the target type. Then a value that a registered converter declines is assigned as it is. A `null` `string` bound to a `string` property is one example: `StringConverter` declines it, and the target receives `null`. A converter that throws is a different case, which [Bindings](bindings.md#when-a-binding-fails) explains.

A numeric converter to text also takes a hint. An `int` hint sets the minimum digit count for a whole number, or the number of decimal places for `float`, `double` and `decimal`. A `string` hint is a .NET format string. A malformed format string throws `FormatException`.

## Numbers

Seven numeric types have a converter in each direction. The parse converters use the `TryParse` method of their type under the current culture. A value outside the range of the target type fails.

| Type | Text to number | Number to text |
| --- | --- | --- |
| `byte` | `StringToByteTypeConverter` | `ByteToStringTypeConverter` |
| `short` | `StringToShortTypeConverter` | `ShortToStringTypeConverter` |
| `int` | `StringToIntegerTypeConverter` | `IntegerToStringTypeConverter` |
| `long` | `StringToLongTypeConverter` | `LongToStringTypeConverter` |
| `float` | `StringToSingleTypeConverter` | `SingleToStringTypeConverter` |
| `double` | `StringToDoubleTypeConverter` | `DoubleToStringTypeConverter` |
| `decimal` | `StringToDecimalTypeConverter` | `DecimalToStringTypeConverter` |

The range check matters for the small types. A port number is a `short`, so `99999` does not fit. `TryConvert` returns `false` and does not wrap around. The excerpt parses one valid port and one that is too large.

```csharp
var converter = new StringToShortTypeConverter();

// Success case: valid port number
var success = converter.TryConvert("8080", conversionHint: null, out var portNumber);
Console.WriteLine(success);
Console.WriteLine(portNumber);

// Failure case: port out of valid range
var overflow = converter.TryConvert("99999", conversionHint: null, out _);
Console.WriteLine(overflow);
```

```text
True
8080
False
```

All seven parse converters work the same way. This excerpt shows the first three. Each one also reports its affinity.

```csharp
var toByte = new StringToByteTypeConverter();
_ = toByte.TryConvert("255", conversionHint: null, out var colorChannel);
Console.WriteLine($"{colorChannel} affinity {toByte.GetAffinityForObjects()}");

var toShort = new StringToShortTypeConverter();
_ = toShort.TryConvert("8080", conversionHint: null, out var portNumber);
Console.WriteLine($"{portNumber} affinity {toShort.GetAffinityForObjects()}");

var toInteger = new StringToIntegerTypeConverter();
_ = toInteger.TryConvert("42", conversionHint: null, out var issueNumber);
Console.WriteLine($"{issueNumber} affinity {toInteger.GetAffinityForObjects()}");
```

```text
255 affinity 2
8080 affinity 2
42 affinity 2
```

The formatting converters follow the same pattern in the other direction. This excerpt turns three numbers into text and prints each affinity, so you can confirm that they all rank the same.

```csharp
var fromByte = new ByteToStringTypeConverter();
_ = fromByte.TryConvert(ColorChannelValue, conversionHint: null, out var colorChannel);
Console.WriteLine($"{colorChannel} affinity {fromByte.GetAffinityForObjects()}");

var fromShort = new ShortToStringTypeConverter();
_ = fromShort.TryConvert(PortNumber, conversionHint: null, out var portNumber);
Console.WriteLine($"{portNumber} affinity {fromShort.GetAffinityForObjects()}");

var fromInteger = new IntegerToStringTypeConverter();
_ = fromInteger.TryConvert(IssueNumber, conversionHint: null, out var issueNumber);
Console.WriteLine($"{issueNumber} affinity {fromInteger.GetAffinityForObjects()}");
```

```text
255 affinity 2
8080 affinity 2
42 affinity 2
```

The [API reference](api.md#numbers) lists every numeric converter.

## Dates, times and durations

Five types have converters: `DateTime`, `DateTimeOffset`, `DateOnly`, `TimeOnly` and `TimeSpan`. The parse converters use the `TryParse` method of their type. The format converters produce the general or short format of the current culture. `TimeSpanToStringTypeConverter` always uses the invariant `c` format.

| Type | Text to value | Value to text |
| --- | --- | --- |
| `DateTime` | `StringToDateTimeTypeConverter` | `DateTimeToStringTypeConverter` |
| `DateTimeOffset` | `StringToDateTimeOffsetTypeConverter` | `DateTimeOffsetToStringTypeConverter` |
| `DateOnly` | `StringToDateOnlyTypeConverter` | `DateOnlyToStringTypeConverter` |
| `TimeOnly` | `StringToTimeOnlyTypeConverter` | `TimeOnlyToStringTypeConverter` |
| `TimeSpan` | `StringToTimeSpanTypeConverter` | `TimeSpanToStringTypeConverter` |

The `DateOnly` and `TimeOnly` converters exist on .NET 8 and later. The .NET Framework targets do not have them.

A user types a due date and time into a task app. This excerpt parses one valid text and one invalid text, so you see how a bad date fails.

```csharp
var converter = new StringToDateTimeTypeConverter();

// Success case: valid date-time
var success = converter.TryConvert("2025-12-25T10:30:00", conversionHint: null, out var dueDateTime);
Console.WriteLine(success);
Console.WriteLine(dueDateTime);

// Failure case: invalid date format
var failure = converter.TryConvert("not-a-date", conversionHint: null, out _);
Console.WriteLine(failure);
```

```text
True
12/25/2025 10:30:00
False
```

A time of day fails when it is out of range. `25:00:00` is not a time. The excerpt parses a valid start time and then that bad one.

```csharp
var converter = new StringToTimeOnlyTypeConverter();

// Success case: valid time
var success = converter.TryConvert("14:30:00", conversionHint: null, out var startTime);
Console.WriteLine(success);
Console.WriteLine(startTime);

// Failure case: invalid time format
var failure = converter.TryConvert("25:00:00", conversionHint: null, out _);
Console.WriteLine(failure);
```

```text
True
14:30
False
```

A duration formats as `hours:minutes:seconds` whatever the culture. The excerpt formats a project estimate of one and a half hours for display.

```csharp
var converter = new TimeSpanToStringTypeConverter();

var success = converter.TryConvert(ProjectDuration, conversionHint: null, out var formatted);
Console.WriteLine(success);
Console.WriteLine(formatted);
```

```text
True
01:30:00
```

A `DateTimeOffset` keeps its offset from UTC in the text. This excerpt also shows the four converters that serve one type: text to value, value to text, and the two nullable forms that [Nullable values](#nullable-values) explains.

```csharp
var toOffset = new StringToDateTimeOffsetTypeConverter();
_ = toOffset.TryConvert(ModifiedTimeText, conversionHint: null, out var modifiedTime);
Console.WriteLine($"{modifiedTime} affinity {toOffset.GetAffinityForObjects()}");

var fromOffset = new DateTimeOffsetToStringTypeConverter();
_ = fromOffset.TryConvert(FileLastModifiedTime, conversionHint: null, out var modifiedTimeText);
Console.WriteLine($"{modifiedTimeText} affinity {fromOffset.GetAffinityForObjects()}");

var toOptionalOffset = new StringToNullableDateTimeOffsetTypeConverter();
_ = toOptionalOffset.TryConvert(ModifiedTimeText, conversionHint: null, out var optionalModifiedTime);
Console.WriteLine($"{optionalModifiedTime} affinity {toOptionalOffset.GetAffinityForObjects()}");

var fromOptionalOffset = new NullableDateTimeOffsetToStringTypeConverter();
_ = fromOptionalOffset.TryConvert((DateTimeOffset?)FileLastModifiedTime, conversionHint: null, out var optionalModifiedTimeText);
Console.WriteLine($"{optionalModifiedTimeText} affinity {fromOptionalOffset.GetAffinityForObjects()}");
```

```text
09/21/2025 10:30:00 +00:00 affinity 2
09/21/2025 10:30:00 +00:00 affinity 2
09/21/2025 10:30:00 +00:00 affinity 2
09/21/2025 10:30:00 +00:00 affinity 2
```

The [API reference](api.md#dates-and-times) lists every date and time converter.

## Booleans, GUIDs and URIs

| Type | Text to value | Value to text |
| --- | --- | --- |
| `bool` | `StringToBooleanTypeConverter` | `BooleanToStringTypeConverter` |
| `Guid` | `StringToGuidTypeConverter` | `GuidToStringTypeConverter` |
| `Uri` | `StringToUriTypeConverter` | `UriToStringTypeConverter` |

`StringToBooleanTypeConverter` accepts `true` and `false` in any letter case. Any other text fails. The excerpt parses `True` and then `maybe`, so you see one success and one failure.

```csharp
var converter = new StringToBooleanTypeConverter();

// Success case: feature enabled
var success = converter.TryConvert("True", conversionHint: null, out var isEnabled);
Console.WriteLine(success);
Console.WriteLine(isEnabled);

// Failure case: invalid toggle input
var failure = converter.TryConvert("maybe", conversionHint: null, out _);
Console.WriteLine(failure);
```

```text
True
True
False
```

`StringToGuidTypeConverter` reads a GUID in a standard text form. `GuidToStringTypeConverter` writes the hyphenated `D` format. The excerpt parses a valid GUID and then text that is not one.

```csharp
var converter = new StringToGuidTypeConverter();

// Success case: valid GUID format
var success = converter.TryConvert("550e8400-e29b-41d4-a716-446655440000", conversionHint: null, out var correlationId);
Console.WriteLine(success);
Console.WriteLine(correlationId);

// Failure case: invalid GUID format
var failure = converter.TryConvert("not-a-guid", conversionHint: null, out _);
Console.WriteLine(failure);
```

```text
True
550e8400-e29b-41d4-a716-446655440000
False
```

`StringToUriTypeConverter` accepts relative and absolute URIs. It fails for `null` and for text that cannot form a URI. `UriToStringTypeConverter` writes the URI back as text. The excerpt formats a repository URL for display in a link.

```csharp
var converter = new UriToStringTypeConverter();

var success = converter.TryConvert(ReactiveUiRepositoryUrl, conversionHint: null, out var urlText);
Console.WriteLine(success);
Console.WriteLine(urlText);
```

```text
True
https://github.com/reactiveui/ReactiveUI
```

`bool` and `Guid` also have nullable converters. This excerpt runs all four for a `bool` in one method, so you see the whole set side by side.

```csharp
var toBoolean = new StringToBooleanTypeConverter();
_ = toBoolean.TryConvert("True", conversionHint: null, out var isEnabled);
Console.WriteLine($"{isEnabled} affinity {toBoolean.GetAffinityForObjects()}");

var fromBoolean = new BooleanToStringTypeConverter();
_ = fromBoolean.TryConvert(true, conversionHint: null, out var enabledText);
Console.WriteLine($"{enabledText} affinity {fromBoolean.GetAffinityForObjects()}");

var toOptionalBoolean = new StringToNullableBooleanTypeConverter();
_ = toOptionalBoolean.TryConvert("False", conversionHint: null, out var isOptionalEnabled);
Console.WriteLine($"{isOptionalEnabled} affinity {toOptionalBoolean.GetAffinityForObjects()}");

var fromOptionalBoolean = new NullableBooleanToStringTypeConverter();
_ = fromOptionalBoolean.TryConvert((bool?)false, conversionHint: null, out var optionalEnabledText);
Console.WriteLine($"{optionalEnabledText} affinity {fromOptionalBoolean.GetAffinityForObjects()}");
```

```text
True affinity 2
True affinity 2
False affinity 2
False affinity 2
```

The [API reference](api.md#booleans-guids-and-uris) lists these converters.

## Nullable values

A view model often holds an optional value, such as a due date that is not set yet. A text box shows an empty string for it. Four kinds of converter cover the nullable forms.

- **Text to nullable.** An empty or `null` string succeeds with a `null` result. Text that does not parse still fails.
- **Nullable to text.** A `null` value succeeds with a `null` string. Any other value formats as its plain form does.
- **Wrap.** A value becomes its nullable form. This always succeeds.
- **Unwrap.** A nullable value becomes its plain form. A `null` fails, because a plain type has no way to hold it.

Empty text is a valid answer to "is there a value?", so the text to nullable converters succeed on it. The excerpt parses a number, an empty string and bad text.

```csharp
var converter = new StringToNullableIntegerTypeConverter();

// Success case: valid issue number
var success = converter.TryConvert("42", conversionHint: null, out var issueNumber);
Console.WriteLine(success);
Console.WriteLine(issueNumber);

// Success case: empty string returns null
var empty = converter.TryConvert(string.Empty, conversionHint: null, out var noIssue);
Console.WriteLine(empty);
Console.WriteLine(noIssue is null);

// Failure case: invalid text
var failure = converter.TryConvert(InvalidNumberText, conversionHint: null, out _);
Console.WriteLine(failure);
```

```text
True
42
True
True
False
```

Pass a nullable value to the nullable to text converters. Cast a plain value to its nullable type first, as `(byte?)` does here. The excerpt formats an optional color channel for display.

```csharp
var converter = new NullableByteToStringTypeConverter();

var success = converter.TryConvert((byte?)ColorChannelValue, conversionHint: null, out var channelText);
Console.WriteLine(success);
Console.WriteLine(channelText);
```

```text
True
255
```

Wrap converters serve the numeric types. Each reports its type pair and its affinity. The excerpt wraps four whole-number types and prints each result, so you see the target change from the plain type to its `Nullable` form.

```csharp
var wrapByte = new ByteToNullableByteTypeConverter();
_ = wrapByte.TryConvertTyped(ColorChannelValue, conversionHint: null, out var colorChannel);
Console.WriteLine($"{wrapByte.FromType} -> {wrapByte.ToType}: {colorChannel} (affinity {wrapByte.GetAffinityForObjects()})");

var wrapShort = new ShortToNullableShortTypeConverter();
_ = wrapShort.TryConvertTyped(PortNumber, conversionHint: null, out var portNumber);
Console.WriteLine($"{wrapShort.FromType} -> {wrapShort.ToType}: {portNumber} (affinity {wrapShort.GetAffinityForObjects()})");

var wrapInteger = new IntegerToNullableIntegerTypeConverter();
_ = wrapInteger.TryConvertTyped(IssueNumber, conversionHint: null, out var issueNumber);
Console.WriteLine($"{wrapInteger.FromType} -> {wrapInteger.ToType}: {issueNumber} (affinity {wrapInteger.GetAffinityForObjects()})");

var wrapLong = new LongToNullableLongTypeConverter();
_ = wrapLong.TryConvertTyped(FileSize, conversionHint: null, out var fileSize);
Console.WriteLine($"{wrapLong.FromType} -> {wrapLong.ToType}: {fileSize} (affinity {wrapLong.GetAffinityForObjects()})");
```

```text
System.Byte -> System.Nullable`1[System.Byte]: 255 (affinity 2)
System.Int16 -> System.Nullable`1[System.Int16]: 8080 (affinity 2)
System.Int32 -> System.Nullable`1[System.Int32]: 42 (affinity 2)
System.Int64 -> System.Nullable`1[System.Int64]: 524288 (affinity 2)
```

Unwrap converters go the other way. The fractional types show the pattern for `float`, `double` and `decimal`. The excerpt unwraps one value of each type and prints the pair, so you see the `Nullable` source become the plain target.

```csharp
var unwrapSingle = new NullableSingleToSingleTypeConverter();
_ = unwrapSingle.TryConvertTyped((float?)Pi, conversionHint: null, out var pi);
Console.WriteLine($"{unwrapSingle.FromType} -> {unwrapSingle.ToType}: {pi} (affinity {unwrapSingle.GetAffinityForObjects()})");

var unwrapDouble = new NullableDoubleToDoubleTypeConverter();
_ = unwrapDouble.TryConvertTyped((double?)EulersNumber, conversionHint: null, out var eulersNumber);
Console.WriteLine($"{unwrapDouble.FromType} -> {unwrapDouble.ToType}: {eulersNumber} (affinity {unwrapDouble.GetAffinityForObjects()})");

var unwrapDecimal = new NullableDecimalToDecimalTypeConverter();
_ = unwrapDecimal.TryConvertTyped((decimal?)TransferAmount, conversionHint: null, out var transferAmount);
Console.WriteLine($"{unwrapDecimal.FromType} -> {unwrapDecimal.ToType}: {transferAmount} (affinity {unwrapDecimal.GetAffinityForObjects()})");
```

```text
System.Nullable`1[System.Single] -> System.Single: 3.14 (affinity 2)
System.Nullable`1[System.Double] -> System.Double: 2.71828 (affinity 2)
System.Nullable`1[System.Decimal] -> System.Decimal: 250.50 (affinity 2)
```

These two tables name every nullable converter. The numeric types have all four kinds.

| Type | Text to nullable | Nullable to text | Wrap | Unwrap |
| --- | --- | --- | --- | --- |
| `byte` | `StringToNullableByteTypeConverter` | `NullableByteToStringTypeConverter` | `ByteToNullableByteTypeConverter` | `NullableByteToByteTypeConverter` |
| `short` | `StringToNullableShortTypeConverter` | `NullableShortToStringTypeConverter` | `ShortToNullableShortTypeConverter` | `NullableShortToShortTypeConverter` |
| `int` | `StringToNullableIntegerTypeConverter` | `NullableIntegerToStringTypeConverter` | `IntegerToNullableIntegerTypeConverter` | `NullableIntegerToIntegerTypeConverter` |
| `long` | `StringToNullableLongTypeConverter` | `NullableLongToStringTypeConverter` | `LongToNullableLongTypeConverter` | `NullableLongToLongTypeConverter` |
| `float` | `StringToNullableSingleTypeConverter` | `NullableSingleToStringTypeConverter` | `SingleToNullableSingleTypeConverter` | `NullableSingleToSingleTypeConverter` |
| `double` | `StringToNullableDoubleTypeConverter` | `NullableDoubleToStringTypeConverter` | `DoubleToNullableDoubleTypeConverter` | `NullableDoubleToDoubleTypeConverter` |
| `decimal` | `StringToNullableDecimalTypeConverter` | `NullableDecimalToStringTypeConverter` | `DecimalToNullableDecimalTypeConverter` | `NullableDecimalToDecimalTypeConverter` |

The other types have the two text converters.

| Type | Text to nullable | Nullable to text |
| --- | --- | --- |
| `bool` | `StringToNullableBooleanTypeConverter` | `NullableBooleanToStringTypeConverter` |
| `Guid` | `StringToNullableGuidTypeConverter` | `NullableGuidToStringTypeConverter` |
| `DateTime` | `StringToNullableDateTimeTypeConverter` | `NullableDateTimeToStringTypeConverter` |
| `DateTimeOffset` | `StringToNullableDateTimeOffsetTypeConverter` | `NullableDateTimeOffsetToStringTypeConverter` |
| `DateOnly` | `StringToNullableDateOnlyTypeConverter` | `NullableDateOnlyToStringTypeConverter` |
| `TimeOnly` | `StringToNullableTimeOnlyTypeConverter` | `NullableTimeOnlyToStringTypeConverter` |
| `TimeSpan` | `StringToNullableTimeSpanTypeConverter` | `NullableTimeSpanToStringTypeConverter` |

`Uri` is a reference type and has no nullable converter. The [API reference](api.md#nullable-values) lists them all.

## String and equality

Two converters do not follow the `From`-`To` pattern.

`StringConverter` passes a `string` through unchanged. It serves a binding between two `string` properties. A `null` or a value that is not a `string` fails. The excerpt passes a task title through and prints the two types the converter serves.

```csharp
var converter = new StringConverter();
var success = converter.TryConvertTyped("Renew car registration", null, out var result);

Console.WriteLine(success);
Console.WriteLine(result);
Console.WriteLine(converter.FromType);
Console.WriteLine(converter.ToType);
```

```text
True
Renew car registration
System.String
System.String
```

It reports the same affinity as the other built-in converters. The excerpt prints that number.

```csharp
var converter = new StringConverter();

Console.WriteLine(converter.GetAffinityForObjects());
```

```text
2
```

`EqualityTypeConverter` turns any value into a `bool`. The result says whether the value equals the conversion hint. Use it to show whether a value matches a choice, such as whether an item's priority is `High`. The conversion always succeeds, and two `null` values are equal. `TodoItem` and `TodoPriority` come from the shared example app. The excerpt compares one item's priority with `High` and then with `Low`.

```csharp
var converter = new EqualityTypeConverter();
TodoItem item = new() { Title = "Renew car registration", Priority = TodoPriority.High };

var success = converter.TryConvertTyped(item.Priority, TodoPriority.High, out var isHigh);

Console.WriteLine(success);
Console.WriteLine(isHigh);

success = converter.TryConvertTyped(item.Priority, TodoPriority.Low, out var isLow);

Console.WriteLine(success);
Console.WriteLine(isLow);
```

```text
True
True
True
False
```

The converter serves the type pair `object` to `bool`. Its affinity is 1, so a converter for that pair with a higher affinity replaces it. The excerpt prints the pair and the affinity.

```csharp
var converter = new EqualityTypeConverter();

Console.WriteLine($"{converter.FromType} -> {converter.ToType}");
Console.WriteLine(converter.GetAffinityForObjects());
```

```text
System.Object -> System.Boolean
1
```

The [API reference](api.md#string-and-equality-converters) lists both.

## Find a converter in the converter service

`BindingConverters.Current` is the converter service that bindings use. It holds three registries. `TypedConverters` matches a converter to an exact source and target type pair. `FallbackConverters` and `SetMethodConverters` serve special cases, and [Custom converters](custom-converters.md) covers them.

The service is empty until you build the app. `BuildApp` replaces it with the service that the builder configured, and `WithCoreServices` puts every built-in converter in that service. [Setup](setup.md) covers the builder. The excerpt reads the service before the build, builds the app, and reads it again, so you see the built-in converters appear.

```csharp
Console.WriteLine(BindingConverters.Current.TypedConverters.GetAllConverters().Any());

IReactiveUIBindingBuilder builder = RxBindingBuilder.CreateReactiveUIBindingBuilder();
_ = builder.WithCoreServices().BuildApp();

var converter = BindingConverters.Current.TypedConverters.TryGetConverter(typeof(int), typeof(string));

Console.WriteLine(BindingConverters.Current.TypedConverters.GetAllConverters().Any());
Console.WriteLine(converter!.GetType().Name);
```

```text
False
True
IntegerToStringTypeConverter
```

`GetAllConverters` returns a copy of every registered converter. `TryGetConverter` takes the source and target types. It returns the converter with the highest affinity for that exact pair. On a tie, the converter registered first wins. It returns `null` when no converter serves the pair, or when every converter for the pair reports an affinity of zero or less. The excerpt looks up four type pairs and checks that the last one has no converter.

```csharp
var registry = BindingConverters.Current.TypedConverters;

Console.WriteLine(registry.TryGetConverter(typeof(bool), typeof(string))!.GetType().Name);
Console.WriteLine(registry.TryGetConverter(typeof(string), typeof(Guid))!.GetType().Name);
Console.WriteLine(registry.TryGetConverter(typeof(DateOnly?), typeof(string))!.GetType().Name);
Console.WriteLine(registry.TryGetConverter(typeof(TimeSpan), typeof(Guid)) is null);
```

```text
BooleanToStringTypeConverter
StringToGuidTypeConverter
NullableDateOnlyToStringTypeConverter
True
```

There is no built-in converter from `TimeSpan` to `Guid`, so the last line prints `True`. The match is exact. A converter for `int` to `string` does not serve `short` to `string`.

Lookups take no lock. Each registration copies the table, so register converters when the app starts and not on a hot path. Affinity is one scale for converters and for the other mechanisms a binding chooses between, which [Mechanisms](mechanisms.md) covers.

## Show and hide with a Visibility

A view shows or hides a control with a `Visibility` value, and a view model holds a `bool`. The MAUI package ships two converters between them. They live in the `ReactiveUI.Binding.Maui` namespace of the `ReactiveUI.Binding.Maui` package. The WPF package declares a pair with the same names for the WPF `Visibility`, listed under [Platforms](api.md#platforms).

`BooleanToVisibilityTypeConverter` turns `true` into `Visible` and `false` into `Collapsed`. Pass a `BooleanToVisibilityHints` value as the conversion hint to change that. Any other hint means `None`.

| Hint | Effect |
| --- | --- |
| `None` | `true` is `Visible`. `false` is `Collapsed`. |
| `Inverse` | Swaps the mapping. `true` is `Collapsed`. `false` is `Visible`. |
| `UseHidden` | Uses `Hidden` in place of `Collapsed`. The WinUI build ignores it, because WinUI has no `Hidden`. |

The hints are flags, so `Inverse | UseHidden` applies both. The conversion always succeeds. The excerpt shows a finished to-do item's mark with no hint, with `UseHidden` and with `Inverse`, so you see each mapping.

```csharp
var converter = new BooleanToVisibilityTypeConverter();

_ = converter.TryConvert(true, conversionHint: null, out var doneMark);
Console.WriteLine(doneMark);

_ = converter.TryConvert(false, BooleanToVisibilityHints.UseHidden, out var hiddenMark);
Console.WriteLine(hiddenMark);

_ = converter.TryConvert(true, BooleanToVisibilityHints.Inverse, out var openMark);
Console.WriteLine(openMark);
```

```text
Visible
Hidden
Collapsed
```

`VisibilityToBooleanTypeConverter` goes back. Only `Visible` gives `true`. `Hidden` and `Collapsed` give `false`. The `Inverse` hint flips the result. The excerpt reads a `Visible` value and then a `Collapsed` value with `Inverse`.

```csharp
var converter = new VisibilityToBooleanTypeConverter();

_ = converter.TryConvert(Microsoft.Maui.Visibility.Visible, conversionHint: null, out var isDone);
Console.WriteLine(isDone);

_ = converter.TryConvert(Microsoft.Maui.Visibility.Collapsed, BooleanToVisibilityHints.Inverse, out var isOpen);
Console.WriteLine(isOpen);
```

```text
True
True
```

`WithMaui()` on the builder registers both converters with the dependency resolver. It does not add them to `TypedConverters`. Create them with `new`, as the example does, and pass the instance where a binding takes a converter. [Custom converters](custom-converters.md) shows how to register a converter in the service.

## Where to go next

- [Custom converters](custom-converters.md) writes a converter, registers it, and replaces a built-in one by giving it a higher affinity.
- [Bindings](bindings.md) shows the binding calls that use these converters.
- [Mechanisms](mechanisms.md) covers affinity for observing properties and binding commands.
- [Setup](setup.md) builds the app and fills the converter service.
- The [API reference](api.md#converters) lists every converter, the registries and the contracts.

## API reference

Each row is one built-in converter or one member of this page's topic. Every converter is a `sealed` class, and it sits in the `ReactiveUI.Binding` namespace unless its row says otherwise. Each one reports an affinity of 2, except `EqualityTypeConverter`, which reports 1. A converter that does not name a hint ignores it.

| Member | What it does | Type and values | Notes |
| --- | --- | --- | --- |
| [`StringConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringConverter.cs) | Passes a `string` through unchanged. | `string` to `string`. | A `null` or a value that is not a `string` fails. Implements `IBindingTypeConverter` only, so call `TryConvertTyped`. |
| [`EqualityTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/EqualityTypeConverter.cs) | Says whether a value equals the conversion hint. | `object` to `bool`. | Always succeeds, and two `null` values are equal. Affinity 1. Implements `IBindingTypeConverter` only. |
| [`StringToByteTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToByteTypeConverter.cs) | Parses text into a `byte`. | `string` to `byte`. | `null`, unparseable text and a value above 255 return `false`. Uses the current culture. |
| [`StringToShortTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToShortTypeConverter.cs) | Parses text into a `short`. | `string` to `short`. | `null`, unparseable text and an out-of-range value return `false`. Uses the current culture. |
| [`StringToIntegerTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToIntegerTypeConverter.cs) | Parses text into an `int`. | `string` to `int`. | `null`, unparseable text and an out-of-range value return `false`. Uses the current culture. |
| [`StringToLongTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToLongTypeConverter.cs) | Parses text into a `long`. | `string` to `long`. | `null`, unparseable text and an out-of-range value return `false`. Uses the current culture. |
| [`StringToSingleTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToSingleTypeConverter.cs) | Parses text into a `float`. | `string` to `float`. | `null` and unparseable text return `false`. Uses the current culture. |
| [`StringToDoubleTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToDoubleTypeConverter.cs) | Parses text into a `double`. | `string` to `double`. | `null` and unparseable text return `false`. Uses the current culture. |
| [`StringToDecimalTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToDecimalTypeConverter.cs) | Parses text into a `decimal`. | `string` to `decimal`. | `null` and unparseable text return `false`. Uses the current culture. |
| [`ByteToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/ByteToStringTypeConverter.cs) | Formats a `byte` as text. | `byte` to `string`. | Always succeeds. Hint: an `int` is the minimum digit count, and a `string` is a format string. A malformed format throws `FormatException`. |
| [`ShortToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/ShortToStringTypeConverter.cs) | Formats a `short` as text. | `short` to `string`. | Always succeeds. Hint: an `int` is the minimum digit count, and a `string` is a format string. A malformed format throws `FormatException`. |
| [`IntegerToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/IntegerToStringTypeConverter.cs) | Formats an `int` as text. | `int` to `string`. | Always succeeds. Hint: an `int` is the minimum digit count, and a `string` is a format string. A malformed format throws `FormatException`. |
| [`LongToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/LongToStringTypeConverter.cs) | Formats a `long` as text. | `long` to `string`. | Always succeeds. Hint: an `int` is the minimum digit count, and a `string` is a format string. A malformed format throws `FormatException`. |
| [`SingleToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/SingleToStringTypeConverter.cs) | Formats a `float` as text. | `float` to `string`. | Always succeeds. Hint: an `int` is the number of decimal places, and a `string` is a format string. A malformed format throws `FormatException`. |
| [`DoubleToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/DoubleToStringTypeConverter.cs) | Formats a `double` as text. | `double` to `string`. | Always succeeds. Hint: an `int` is the number of decimal places, and a `string` is a format string. A malformed format throws `FormatException`. |
| [`DecimalToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/DecimalToStringTypeConverter.cs) | Formats a `decimal` as text. | `decimal` to `string`. | Always succeeds. Hint: an `int` is the number of decimal places, and a `string` is a format string. A malformed format throws `FormatException`. |
| [`StringToDateTimeTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToDateTimeTypeConverter.cs) | Parses text into a `DateTime`. | `string` to `DateTime`. | `null` and unparseable text return `false`. Uses the current culture. |
| [`DateTimeToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/DateTimeToStringTypeConverter.cs) | Formats a `DateTime` as text. | `DateTime` to `string`. | Always succeeds. Uses the general date and time format of the current culture. |
| [`StringToDateTimeOffsetTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToDateTimeOffsetTypeConverter.cs) | Parses text into a `DateTimeOffset`. | `string` to `DateTimeOffset`. | `null` and unparseable text return `false`. Uses the current culture. |
| [`DateTimeOffsetToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/DateTimeOffsetToStringTypeConverter.cs) | Formats a `DateTimeOffset` as text. | `DateTimeOffset` to `string`. | Always succeeds. Uses the general format of the current culture and includes the offset. |
| [`StringToDateOnlyTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToDateOnlyTypeConverter.cs) | Parses text into a `DateOnly`. | `string` to `DateOnly`; .NET 8 and later. | `null` and unparseable text return `false`. Uses the current culture. |
| [`DateOnlyToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/DateOnlyToStringTypeConverter.cs) | Formats a `DateOnly` as text. | `DateOnly` to `string`; .NET 8 and later. | Always succeeds. Uses the short date format of the current culture. |
| [`StringToTimeOnlyTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToTimeOnlyTypeConverter.cs) | Parses text into a `TimeOnly`. | `string` to `TimeOnly`; .NET 8 and later. | `null` and unparseable text return `false`. Uses the current culture. |
| [`TimeOnlyToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/TimeOnlyToStringTypeConverter.cs) | Formats a `TimeOnly` as text. | `TimeOnly` to `string`; .NET 8 and later. | Always succeeds. Uses the short time format of the current culture. |
| [`StringToTimeSpanTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToTimeSpanTypeConverter.cs) | Parses text into a `TimeSpan`. | `string` to `TimeSpan`. | `null` and unparseable text return `false`. Uses the current culture. |
| [`TimeSpanToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/TimeSpanToStringTypeConverter.cs) | Formats a `TimeSpan` as text. | `TimeSpan` to `string`. | Always succeeds. Uses the invariant `c` format. |
| [`StringToBooleanTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToBooleanTypeConverter.cs) | Parses text into a `bool`. | `string` to `bool`. | Accepts `true` and `false` in any letter case. `null` and other text return `false`. |
| [`BooleanToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/BooleanToStringTypeConverter.cs) | Formats a `bool` as text. | `bool` to `string`. | Always succeeds. Writes `True` or `False`. |
| [`StringToGuidTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToGuidTypeConverter.cs) | Parses text into a `Guid`. | `string` to `Guid`. | `null` and unparseable text return `false`. |
| [`GuidToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/GuidToStringTypeConverter.cs) | Formats a `Guid` as text. | `Guid` to `string`. | Always succeeds. Writes the hyphenated `D` format. |
| [`StringToUriTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToUriTypeConverter.cs) | Creates a `Uri` from text. | `string` to `Uri`. | Accepts relative and absolute URIs. `null` and text that cannot form a URI return `false`. |
| [`UriToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/UriToStringTypeConverter.cs) | Writes a `Uri` as text with `ToString`. | `Uri` to `string`. | A `null` value returns `false`. |
| [`StringToNullableByteTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToNullableByteTypeConverter.cs) | Parses optional text into a `byte?`. | `string` to `byte?`. | `null` or empty text gives a `null` result. Unparseable or out-of-range text returns `false`. Uses the current culture. |
| [`StringToNullableShortTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToNullableShortTypeConverter.cs) | Parses optional text into a `short?`. | `string` to `short?`. | `null` or empty text gives a `null` result. Unparseable or out-of-range text returns `false`. Uses the current culture. |
| [`StringToNullableIntegerTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToNullableIntegerTypeConverter.cs) | Parses optional text into an `int?`. | `string` to `int?`. | `null` or empty text gives a `null` result. Unparseable or out-of-range text returns `false`. Uses the current culture. |
| [`StringToNullableLongTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToNullableLongTypeConverter.cs) | Parses optional text into a `long?`. | `string` to `long?`. | `null` or empty text gives a `null` result. Unparseable or out-of-range text returns `false`. Uses the current culture. |
| [`StringToNullableSingleTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToNullableSingleTypeConverter.cs) | Parses optional text into a `float?`. | `string` to `float?`. | `null` or empty text gives a `null` result. Unparseable text returns `false`. Uses the current culture. |
| [`StringToNullableDoubleTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToNullableDoubleTypeConverter.cs) | Parses optional text into a `double?`. | `string` to `double?`. | `null` or empty text gives a `null` result. Unparseable text returns `false`. Uses the current culture. |
| [`StringToNullableDecimalTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToNullableDecimalTypeConverter.cs) | Parses optional text into a `decimal?`. | `string` to `decimal?`. | `null` or empty text gives a `null` result. Unparseable text returns `false`. Uses the current culture. |
| [`NullableByteToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableByteToStringTypeConverter.cs) | Formats a `byte?` as text. | `byte?` to `string`. | Always succeeds. A `null` value gives a `null` string. Hint: an `int` is the minimum digit count, and a `string` is a format string. |
| [`NullableShortToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableShortToStringTypeConverter.cs) | Formats a `short?` as text. | `short?` to `string`. | Always succeeds. A `null` value gives a `null` string. Hint: an `int` is the minimum digit count, and a `string` is a format string. |
| [`NullableIntegerToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableIntegerToStringTypeConverter.cs) | Formats an `int?` as text. | `int?` to `string`. | Always succeeds. A `null` value gives a `null` string. Hint: an `int` is the minimum digit count, and a `string` is a format string. |
| [`NullableLongToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableLongToStringTypeConverter.cs) | Formats a `long?` as text. | `long?` to `string`. | Always succeeds. A `null` value gives a `null` string. Hint: an `int` is the minimum digit count, and a `string` is a format string. |
| [`NullableSingleToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableSingleToStringTypeConverter.cs) | Formats a `float?` as text. | `float?` to `string`. | Always succeeds. A `null` value gives a `null` string. Hint: an `int` is the number of decimal places, and a `string` is a format string. |
| [`NullableDoubleToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableDoubleToStringTypeConverter.cs) | Formats a `double?` as text. | `double?` to `string`. | Always succeeds. A `null` value gives a `null` string. Hint: an `int` is the number of decimal places, and a `string` is a format string. |
| [`NullableDecimalToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableDecimalToStringTypeConverter.cs) | Formats a `decimal?` as text. | `decimal?` to `string`. | Always succeeds. A `null` value gives a `null` string. Hint: an `int` is the number of decimal places, and a `string` is a format string. |
| [`ByteToNullableByteTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/ByteToNullableByteTypeConverter.cs) | Wraps a `byte` in its nullable form. | `byte` to `byte?`. | Always succeeds. |
| [`ShortToNullableShortTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/ShortToNullableShortTypeConverter.cs) | Wraps a `short` in its nullable form. | `short` to `short?`. | Always succeeds. |
| [`IntegerToNullableIntegerTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/IntegerToNullableIntegerTypeConverter.cs) | Wraps an `int` in its nullable form. | `int` to `int?`. | Always succeeds. |
| [`LongToNullableLongTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/LongToNullableLongTypeConverter.cs) | Wraps a `long` in its nullable form. | `long` to `long?`. | Always succeeds. |
| [`SingleToNullableSingleTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/SingleToNullableSingleTypeConverter.cs) | Wraps a `float` in its nullable form. | `float` to `float?`. | Always succeeds. |
| [`DoubleToNullableDoubleTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/DoubleToNullableDoubleTypeConverter.cs) | Wraps a `double` in its nullable form. | `double` to `double?`. | Always succeeds. |
| [`DecimalToNullableDecimalTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/DecimalToNullableDecimalTypeConverter.cs) | Wraps a `decimal` in its nullable form. | `decimal` to `decimal?`. | Always succeeds. |
| [`NullableByteToByteTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableByteToByteTypeConverter.cs) | Unwraps a `byte?`. | `byte?` to `byte`. | A `null` value returns `false`. |
| [`NullableShortToShortTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableShortToShortTypeConverter.cs) | Unwraps a `short?`. | `short?` to `short`. | A `null` value returns `false`. |
| [`NullableIntegerToIntegerTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableIntegerToIntegerTypeConverter.cs) | Unwraps an `int?`. | `int?` to `int`. | A `null` value returns `false`. |
| [`NullableLongToLongTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableLongToLongTypeConverter.cs) | Unwraps a `long?`. | `long?` to `long`. | A `null` value returns `false`. |
| [`NullableSingleToSingleTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableSingleToSingleTypeConverter.cs) | Unwraps a `float?`. | `float?` to `float`. | A `null` value returns `false`. |
| [`NullableDoubleToDoubleTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableDoubleToDoubleTypeConverter.cs) | Unwraps a `double?`. | `double?` to `double`. | A `null` value returns `false`. |
| [`NullableDecimalToDecimalTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableDecimalToDecimalTypeConverter.cs) | Unwraps a `decimal?`. | `decimal?` to `decimal`. | A `null` value returns `false`. |
| [`StringToNullableBooleanTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToNullableBooleanTypeConverter.cs) | Parses optional text into a `bool?`. | `string` to `bool?`. | `null` or empty text gives a `null` result. Text other than `true` or `false` returns `false`. |
| [`NullableBooleanToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableBooleanToStringTypeConverter.cs) | Formats a `bool?` as text. | `bool?` to `string`. | Always succeeds. Writes `True` or `False`, and a `null` value gives a `null` string. |
| [`StringToNullableGuidTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToNullableGuidTypeConverter.cs) | Parses optional text into a `Guid?`. | `string` to `Guid?`. | `null` or empty text gives a `null` result. Unparseable text returns `false`. |
| [`NullableGuidToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableGuidToStringTypeConverter.cs) | Formats a `Guid?` as text. | `Guid?` to `string`. | Always succeeds. Writes the hyphenated `D` format, and a `null` value gives a `null` string. |
| [`StringToNullableDateTimeTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToNullableDateTimeTypeConverter.cs) | Parses optional text into a `DateTime?`. | `string` to `DateTime?`. | `null` or empty text gives a `null` result. Unparseable text returns `false`. Uses the current culture. |
| [`NullableDateTimeToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableDateTimeToStringTypeConverter.cs) | Formats a `DateTime?` as text. | `DateTime?` to `string`. | Always succeeds. Uses the general format of the current culture, and a `null` value gives a `null` string. |
| [`StringToNullableDateTimeOffsetTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToNullableDateTimeOffsetTypeConverter.cs) | Parses optional text into a `DateTimeOffset?`. | `string` to `DateTimeOffset?`. | `null` or empty text gives a `null` result. Unparseable text returns `false`. Uses the current culture. |
| [`NullableDateTimeOffsetToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableDateTimeOffsetToStringTypeConverter.cs) | Formats a `DateTimeOffset?` as text. | `DateTimeOffset?` to `string`. | Always succeeds. Includes the offset, and a `null` value gives a `null` string. |
| [`StringToNullableDateOnlyTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToNullableDateOnlyTypeConverter.cs) | Parses optional text into a `DateOnly?`. | `string` to `DateOnly?`; .NET 8 and later. | `null` or empty text gives a `null` result. Unparseable text returns `false`. Uses the current culture. |
| [`NullableDateOnlyToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableDateOnlyToStringTypeConverter.cs) | Formats a `DateOnly?` as text. | `DateOnly?` to `string`; .NET 8 and later. | Always succeeds. Uses the short date format of the current culture, and a `null` value gives a `null` string. |
| [`StringToNullableTimeOnlyTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToNullableTimeOnlyTypeConverter.cs) | Parses optional text into a `TimeOnly?`. | `string` to `TimeOnly?`; .NET 8 and later. | `null` or empty text gives a `null` result. Unparseable text returns `false`. Uses the current culture. |
| [`NullableTimeOnlyToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableTimeOnlyToStringTypeConverter.cs) | Formats a `TimeOnly?` as text. | `TimeOnly?` to `string`; .NET 8 and later. | Always succeeds. Uses the short time format of the current culture, and a `null` value gives a `null` string. |
| [`StringToNullableTimeSpanTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/StringToNullableTimeSpanTypeConverter.cs) | Parses optional text into a `TimeSpan?`. | `string` to `TimeSpan?`. | `null` or empty text gives a `null` result. Unparseable text returns `false`. Uses the current culture. |
| [`NullableTimeSpanToStringTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converter/NullableTimeSpanToStringTypeConverter.cs) | Formats a `TimeSpan?` as text. | `TimeSpan?` to `string`. | Always succeeds. Uses the invariant `c` format, and a `null` value gives a `null` string. |
| [`BooleanToVisibilityTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Maui.Shared/BooleanToVisibilityTypeConverter.cs) | Turns a `bool` into a `Visibility`. | `bool` to MAUI `Visibility`. Namespace `ReactiveUI.Binding.Maui`. | Always succeeds. `true` is `Visible`, and `false` is `Collapsed`. Hint: a `BooleanToVisibilityHints` value. Any other hint means `None`. |
| [`VisibilityToBooleanTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Maui.Shared/VisibilityToBooleanTypeConverter.cs) | Turns a `Visibility` into a `bool`. | MAUI `Visibility` to `bool`. Namespace `ReactiveUI.Binding.Maui`. | Always succeeds. Only `Visible` gives `true`. Hint: `Inverse` flips the result. Any other hint means `None`. |
| [`BooleanToVisibilityHints`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Maui.Shared/BooleanToVisibilityHints.cs) | Chooses how a `bool` maps to a `Visibility`. | Flags enum: `None = 0`, `Inverse = 2`, `UseHidden = 4`. Namespace `ReactiveUI.Binding.Maui`. | Pass a value as the conversion hint. `UseHidden` is ignored on WinUI. |
| [`BindingConverters`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/BindingConverters.cs) | Holds the converter service that bindings convert through. | Static class. `Current` is a `ConverterService`. | `Current` starts empty. `BuildApp` replaces it with the configured service, which holds the built-in converters. |
| [`ConverterService`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/ConverterService.cs) | Owns the typed, fallback and set-method registries. | Properties `TypedConverters`, `FallbackConverters` and `SetMethodConverters`. Method `ResolveConverter(Type, Type)`. | `ResolveConverter` returns the typed converter for the pair first, then a fallback converter, and `null` when neither exists. A new service holds no converters. |
| [`BindingTypeConverterRegistry`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/BindingTypeConverterRegistry.cs) | Holds typed converters grouped by exact type pair. | Methods `Register`, `TryGetConverter(Type, Type)` and `GetAllConverters()`. | `TryGetConverter` returns the highest positive affinity, the earliest registered converter on a tie, and `null` when none applies. Lookups take no lock. |
| [`DefaultConverterRegistration`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/DefaultConverterRegistration.cs) | Registers every built-in converter with a `ConverterService`. | Static method `RegisterDefaults(ConverterService)`. | Registers no fallback or set-method converters. Calling it twice adds every converter twice. Registers the `DateOnly` and `TimeOnly` converters on .NET 8 and later only. |
