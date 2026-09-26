---
Order: 6
---
# Custom converters

[Run the complete page example](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/examples/Documentation/Pages/custom-converters/custom-converters.csproj).

A binding joins two properties. Sometimes the two properties have different types, and the library has no built-in converter for that pair. A to-do app might show a `TodoPriority` as a colour name in a `Label`. A form might keep the tags of a task in an `IReadOnlyList<string>` and show them in an `Entry` as one line of text. A converter teaches the binding how to move a value between the two types.

The [converters page](converters.md) covers the built-in converters and the idea of a converter. This page covers the converters you write yourself. It shows how to write one and how to hand it to a binding. It shows how to register one so every binding finds it. It ends with moving converters over from a ReactiveUI app. Start with the walkthrough below. If you stop after it, you can write and use a converter.

The outputs assume the invariant culture, which the example project sets at start-up. Several converters format and parse with the current culture.

## Write a converter and use it

**1. Derive from `BindingTypeConverter`.** `BindingTypeConverter<TFrom, TTo>` is the base class for a converter between one pair of types. It supplies `FromType`, `ToType` and the boxed `TryConvertTyped` method. You write two members: `GetAffinityForObjects`, which scores the converter, and `TryConvert`, which converts one value. The converter below turns a `TodoPriority` into a colour name, the kind of text a `Label` can show.

```csharp
public sealed class TodoPriorityToColorConverter : BindingTypeConverter<TodoPriority, string>
{
    private const int ConvertorAffinity = 10;

    public override int GetAffinityForObjects() => ConvertorAffinity;

    public override bool TryConvert(TodoPriority from, object? conversionHint, [MaybeNullWhen(true)] out string? result)
    {
        result = from switch
        {
            TodoPriority.Low => "Gray",
            TodoPriority.Normal => "Orange",
            TodoPriority.High => "Red",
            _ => null,
        };

        return result is not null;
    }
}
```

`TryConvert` returns `true` and sets `result` when the conversion works. It returns `false` when it cannot convert the value. The switch gives `null` for a `TodoPriority` value it does not list, so the converter reports a failure instead of inventing a colour.

**2. Score it.** Affinity is a number that ranks the converters for one pair of types. A positive number means the converter applies. Zero or less excludes it. The highest number wins, and the converter registered first wins a tie. The built-in converters score 2 and the equality converter scores 1, so a converter that scores 10 outranks them all.

**3. Call it on its own.** A converter is an ordinary object. You can call `TryConvert` on it without a binding, which is the easiest way to test one. The loop below converts every priority and prints whether each conversion worked.

```csharp
TodoPriorityToColorConverter converter = new TodoPriorityToColorConverter();

foreach (TodoPriority priority in Enum.GetValues<TodoPriority>())
{
    bool success = converter.TryConvert(priority, null, out var colorName);
    Console.WriteLine($"{priority}: {success} {colorName}");
}
```

```text
Low: True Gray
Normal: True Orange
High: True Red
```

**4. Pass it to a binding.** `BindOneWay` takes the converter as its last argument. The binding writes the colour name to the label at once and again each time the priority changes. The lambdas name the source property and the target property, as they do on the [bindings page](bindings.md).

```csharp
TodoItem item = new() { Title = CarRegistrationTitle, Priority = TodoPriority.High };
Label badge = new();

using IDisposable binding = item.BindOneWay(badge, x => x.Priority, x => x.Text, new TodoPriorityToColorConverter());
Console.WriteLine(badge.Text);

item.Priority = TodoPriority.Low;
Console.WriteLine(badge.Text);
```

```text
Red
Gray
```

`using var` disposes the binding at the end of the method, which stops the label from following the item.

**5. Register it once.** Passing a converter to every binding gets repetitive. Register the converter with the shared converter service instead, and every binding between the two types finds it. `BindingConverters.Current` is that service. Its `TypedConverters` registry holds converters for one exact pair of types. `TryGetConverter` returns the best one for a pair. The excerpt registers the converter, fetches it back by its two types and converts two priorities. That shows the service can find the converter without a binding.

```csharp
BindingConverters.Current.TypedConverters.Register(new TodoPriorityToColorConverter());

IBindingTypeConverter? converter = BindingConverters.Current.TypedConverters.TryGetConverter(typeof(TodoPriority), typeof(string));

bool success = converter!.TryConvertTyped(TodoPriority.High, null, out var highColor);
Console.WriteLine(success);
Console.WriteLine(highColor);

success = converter.TryConvertTyped(TodoPriority.Low, null, out var lowColor);
Console.WriteLine(success);
Console.WriteLine(lowColor);
```

```text
True
Red
True
Gray
```

`TryConvertTyped` is the boxed form of `TryConvert`. It takes and returns `object`, so it works on a converter you fetched from a registry without knowing its types. The [registration section](#register-converters) below covers the registries in full.

## Choose how to implement the interface

Two shapes exist for a converter, and both register the same way.

The base class is the shorter one. `BindingTypeConverter<TFrom, TTo>` also implements `IBindingTypeConverter<TFrom, TTo>`, which adds a typed `TryConvert`. A generated binding calls the typed method when it can, so a value type is not boxed on the way through. Prefer the base class.

The interface is the longer one. `IBindingTypeConverter` has four members: `FromType`, `ToType`, `GetAffinityForObjects` and `TryConvertTyped`. Implement it directly when the converter must derive from another class. You write the type checks yourself. The class below does the same priority-to-colour job as the first converter, but it implements the interface directly.

```csharp
public sealed class PriorityColourConverter : IBindingTypeConverter
{
    private const int ColourAffinity = 20;

    public Type FromType => typeof(TodoPriority);

    public Type ToType => typeof(string);

    public int GetAffinityForObjects() => ColourAffinity;

    public bool TryConvertTyped(object? from, object? conversionHint, [NotNullWhen(true)] out object? result)
    {
        result = from switch
        {
            TodoPriority.Low => "Green",
            TodoPriority.Normal => "Amber",
            TodoPriority.High => "Crimson",
            _ => null,
        };

        return result is not null;
    }
}
```

The `from switch` matches only a `TodoPriority`. Any other value falls to `null`, and the method returns `false`. The next excerpt calls the converter directly. It prints the two types, the score and the result for a `TodoPriority`, then tries a `string` to show the failure.

```csharp
PriorityColourConverter converter = new PriorityColourConverter();

Console.WriteLine($"{converter.FromType.Name} -> {converter.ToType.Name}");
Console.WriteLine(converter.GetAffinityForObjects());
Console.WriteLine(converter.TryConvertTyped(TodoPriority.High, null, out var colour));
Console.WriteLine(colour);
Console.WriteLine(converter.TryConvertTyped("High", null, out var notAPriority));
Console.WriteLine(notAPriority is null);
```

```text
TodoPriority -> String
20
True
Crimson
False
True
```

A binding uses this converter as it uses the base-class one. This binding shows a priority as a colour name through it.

```csharp
TodoItem item = new() { Title = CarRegistrationTitle, Priority = TodoPriority.High };
Label badge = new();

using IDisposable binding = item.BindOneWay(badge, x => x.Priority, x => x.Text, new PriorityColourConverter());
Console.WriteLine(badge.Text);

item.Priority = TodoPriority.Low;
Console.WriteLine(badge.Text);
```

```text
Crimson
Green
```

### Call a converter through its base class or its interfaces

A converter can be held as its base class, as the untyped interface or as the typed interface. `CurrencyTextConverter` derives from `BindingTypeConverter<decimal, string>`. The first excerpt holds it as the base class and calls the typed `TryConvert`.

```csharp
BindingTypeConverter<decimal, string> converter = new CurrencyTextConverter(DollarSymbol);

Console.WriteLine($"{converter.FromType.Name} -> {converter.ToType.Name}");
Console.WriteLine(converter.GetAffinityForObjects());
Console.WriteLine(converter.TryConvert(TransferAmount, conversionHint: null, out var amountText));
Console.WriteLine(amountText);
```

```text
Decimal -> String
10
True
$250.50
```

A registry returns the untyped `IBindingTypeConverter`. The second excerpt gets the converter from a registry and casts it to `IBindingTypeConverter<TFrom, TTo>`, which has the typed `TryConvert`.

```csharp
ConverterService service = new();
service.TypedConverters.Register(new CurrencyTextConverter(DollarSymbol));

IBindingTypeConverter untyped = service.TypedConverters.TryGetConverter(typeof(decimal), typeof(string))!;
IBindingTypeConverter<decimal, string> typed = (IBindingTypeConverter<decimal, string>)untyped;

Console.WriteLine($"{untyped.FromType.Name} -> {untyped.ToType.Name}");
Console.WriteLine(typed.TryConvert(TransferAmount, conversionHint: null, out var amountText));
Console.WriteLine(amountText);
```

```text
Decimal -> String
True
$250.50
```

A converter takes constructor arguments like any class. Here the currency symbol comes from the constructor.

### Use the conversion hint

Every conversion method takes a `conversionHint`. The hint is an extra value the caller passes to the converter, such as a format string. A converter may ignore it. `PriorityMatchesHintConverter` reads it: it returns `true` when the priority equals the priority passed as the hint. The next section shows a binding that passes the hint. The class below compares the priority with the hint, so one converter can answer a different question for each binding.

```csharp
public sealed class PriorityMatchesHintConverter : BindingTypeConverter<TodoPriority, bool>
{
    private const int MatchAffinity = 10;

    public override int GetAffinityForObjects() => MatchAffinity;

    public override bool TryConvert(TodoPriority from, object? conversionHint, out bool result)
    {
        result = conversionHint is TodoPriority expected && from == expected;
        return true;
    }
}
```

## Pass a converter to a binding

A binding that gets a converter object uses it for that binding only. It ignores the registries. Five binding methods take a converter: `BindOneWay`, `BindTwoWay`, `OneWayBind`, `Bind` and `BindTo`. `BindOneWay` and `BindTwoWay` join two properties from the model side. `OneWayBind` and `Bind` join a view to its view model. `BindTo` writes the values of an observable to a property. The [bindings page](bindings.md) explains the differences.

When a converter returns `false`, the binding writes nothing for that value. The target keeps its last value.

### Pass a hint with the converter

`BindOneWay` takes an optional conversion hint after the converter. This binding ticks a check box while the priority equals the hint. Changing the priority to `Low` unticks the box.

```csharp
TodoItem item = new() { Title = CarRegistrationTitle, Priority = TodoPriority.High };
CheckBox highPriorityBox = new();

using IDisposable binding = item.BindOneWay(highPriorityBox, x => x.Priority, x => x.IsChecked, new PriorityMatchesHintConverter(), TodoPriority.High);
Console.WriteLine(highPriorityBox.IsChecked);

item.Priority = TodoPriority.Low;
Console.WriteLine(highPriorityBox.IsChecked);
```

```text
True
False
```

`BindTo` takes the hint before the converter. It writes the values of an observable, here the changes of the priority, to the check box.

```csharp
TodoItem item = new() { Title = CarRegistrationTitle, Priority = TodoPriority.High };
CheckBox highPriorityBox = new();

using (item.WhenChanged(x => x.Priority).BindTo(highPriorityBox, x => x.IsChecked, TodoPriority.High, new PriorityMatchesHintConverter()))
{
    Console.WriteLine(highPriorityBox.IsChecked);

    item.Priority = TodoPriority.Low;
    Console.WriteLine(highPriorityBox.IsChecked);
}
```

```text
True
False
```

`WhenChanged` is an observable that emits each value of a property. See [observing](observing.md).

### Pass one converter for each direction

A two-way binding moves values both ways, so it needs two conversions. `BindTwoWay` and `Bind` take two converters. The first converts from the source to the target, and the second converts back. The built-in `NullableDateOnlyToStringTypeConverter` and `StringToNullableDateOnlyTypeConverter` do the work here. You can pass your own the same way.

```csharp
InMemoryTodoStore store = InMemoryTodoStore.CreateSeeded();
TodoItem item = (await store.GetAsync(DentistTaskId))!;
Entry dueDateBox = new();

using IDisposable binding = item.BindTwoWay(
    dueDateBox,
    x => x.DueDate,
    x => x.Text,
    new NullableDateOnlyToStringTypeConverter(),
    new StringToNullableDateOnlyTypeConverter());
Console.WriteLine(dueDateBox.Text);

dueDateBox.Text = TypedDueDate;
Console.WriteLine(item.DueDate);
```

```text
04/02/2026
05/01/2026
```

The same shape works for a converter pair of your own. This binding keeps the tags of a task and a text box in step. `TagListToTextConverter` joins the tags with a comma. `TextToTagListConverter` splits the text at the commas, trims each tag and drops blank ones.

```csharp
TodoItem item = new() { Title = CarRegistrationTitle, Tags = ["car", "admin"] };
Entry tagsBox = new();

using IDisposable binding = item.BindTwoWay(tagsBox, x => x.Tags, x => x.Text, new TagListToTextConverter(), new TextToTagListConverter());
Console.WriteLine(tagsBox.Text);

tagsBox.Text = TypedTags;
Console.WriteLine(string.Join("|", item.Tags));
```

```text
car, admin
car|admin|urgent
```

Each half is also a plain converter. This excerpt calls both halves without a binding, so you can test each direction on its own.

```csharp
TagListToTextConverter toText = new TagListToTextConverter();
TextToTagListConverter toTags = new TextToTagListConverter();
TodoItem item = new() { Title = CarRegistrationTitle, Tags = ["car", "admin"] };

Console.WriteLine(toText.TryConvert(item.Tags, null, out var text));
Console.WriteLine(text);
Console.WriteLine(toTags.TryConvert(TypedTags, null, out var tags));
Console.WriteLine(string.Join("|", tags!));
```

```text
True
car, admin
True
car|admin|urgent
```

### Bind a view to a view model with converters

The view-first methods `OneWayBind` and `Bind` take the converters as well. The lambda for the view model reads from the view model. The lambda for the view reads from the view. `OneWayBind` shows the total balance as text through the built-in `DecimalToStringTypeConverter`.

```csharp
AccountsView view = new();
AccountsViewModel viewModel = new(new InMemoryBankingBackend());
view.ViewModel = viewModel;

using IReactiveBinding<AccountsView, string> binding = view.OneWayBind(viewModel, x => x.TotalBalance, v => v.TotalBalanceLabel.Text, new DecimalToStringTypeConverter());
await viewModel.LoadAccountsAsync();

Console.WriteLine(view.TotalBalanceLabel.Text);
```

```text
17680.75
```

`Bind` keeps the transfer amount and its text box in step. It takes a converter for each direction. The excerpt sets the amount in the view model and prints the text box, then types text in the box and prints the amount in the view model.

```csharp
TransferView view = new();
TransferViewModel viewModel = new(new InMemoryBankingBackend());
view.ViewModel = viewModel;

using IReactiveBinding<TransferView, BindingChange> binding = view.Bind(
    viewModel,
    x => x.Draft.Amount,
    v => v.AmountTextBox.Text,
    new DecimalToStringTypeConverter(),
    new StringToDecimalTypeConverter());

viewModel.Draft.Amount = TransferAmount;
Console.WriteLine(view.AmountTextBox.Text);

view.AmountTextBox.Text = TypedAmount;
Console.WriteLine(viewModel.Draft.Amount);
```

```text
250.50
99.95
```

The converter overloads of `BindOneWay`, `BindTwoWay`, `OneWayBind` and `Bind` keep the parameter names of the library's own methods. A call that names an argument, such as `conversionHint:`, resolves to them. The scheduler parameter is optional and comes last. When you leave it out, or pass `null`, the binding writes on the thread that owns the target. See [threading](threading.md).

### Build a list of views from a list of view models

A converter can produce more than a `string`. `AccountsToViewsConverter` converts a list of accounts into a list of views, one for each account. It asks a view locator for each view. A view locator finds the view class that belongs to a view model. See [views](views.md).

The converter takes the locator through its constructor. It derives from `BindingTypeConverter<IReadOnlyList<Account>, IEnumerable>` and scores 10. A `null` list fails the conversion, so the binding writes nothing. A view model with no matching view is left out of the result. The binding below maps `Account` to `AccountSummaryView` in a locator, then shows every account as a row.

```csharp
DefaultViewLocator locator = new();
locator.Map<Account, AccountSummaryView>();
AccountsView view = new();
AccountsViewModel viewModel = new(new InMemoryBankingBackend());
view.ViewModel = viewModel;

using IReactiveBinding<AccountsView, System.Collections.IEnumerable> binding = view.OneWayBind(viewModel, x => x.Accounts, v => v.AccountList.ItemsSource, new AccountsToViewsConverter(locator));
await viewModel.LoadAccountsAsync();

foreach (AccountSummaryView row in view.AccountList.ItemsSource.OfType<AccountSummaryView>())
{
    Console.WriteLine(row.ViewModel!.Name);
}
```

```text
Everyday Account
Savings Account
```

## Register converters

A registered converter serves every binding, so you do not pass it each time. The converter service `ConverterService` owns three registries. Each registry has its own job.

| Property | Interface it holds | What it holds |
| --- | --- | --- |
| `TypedConverters` | `IBindingTypeConverter` | Converters for one exact source type and target type, such as `int` to `string`. |
| `FallbackConverters` | `IBindingFallbackConverter` | Converters the library asks only when no typed converter matches. |
| `SetMethodConverters` | `ISetMethodBindingConverter` | Converters that change how a binding writes to its target. |

`BindingConverters.Current` is the service that bindings use. It starts empty. The builder's `WithCoreServices` registers the built-in converters in the builder's own service, and `BuildApp` replaces `Current` with that service. A `new ConverterService()` is also empty, so you can create as many as you like, for example one for each test.

Register converters once, when the application starts. Each registration copies the registry's contents so that lookups can run without a lock. Registering is the slow step, and reading is the fast one. Both are safe to call from any thread.

### Register and list

`Register` files a converter under the `FromType` and `ToType` it reports. The pair is read once, at registration. `GetAllConverters` returns a copy of everything the registry holds. Later registrations do not change that copy. The excerpt counts the converters before and after one registration, so you can see the registry change.

```csharp
ConverterService service = new();

Console.WriteLine(service.TypedConverters.GetAllConverters().Count());

service.TypedConverters.Register(new DemoIntToStringConverter(LowAffinityScore));

Console.WriteLine(service.TypedConverters.GetAllConverters().Count());
```

```text
0
1
```

### Resolve by affinity

Several converters can share one pair. `TryGetConverter` asks each one for its affinity and returns the highest positive one, or `null` when the pair has no usable converter. On a tie, the converter registered first wins. This example registers a converter that scores 1 and another that scores 100.

```csharp
ConverterService service = new();
service.TypedConverters.Register(new DemoIntToStringConverter(LowAffinityScore));
service.TypedConverters.Register(new DemoIntToStringConverter(HighAffinityScore));

IBindingTypeConverter? resolved = service.TypedConverters.TryGetConverter(typeof(int), typeof(string));

Console.WriteLine(resolved!.GetAffinityForObjects());
```

```text
100
```

### Override a built-in converter

To replace a built-in converter, register one for the same pair with a higher affinity. `DefaultConverterRegistration.RegisterDefaults` adds the built-in typed converters to a service. This example registers `CustomBoolToStringConverter`, which scores 100 and writes `AFFIRMATIVE` and `NEGATIVE`.

```csharp
ConverterService service = new();
DefaultConverterRegistration.RegisterDefaults(service);

IBindingTypeConverter? builtIn = service.TypedConverters.TryGetConverter(typeof(bool), typeof(string));
Console.WriteLine(builtIn!.GetType().Name);

service.TypedConverters.Register(new CustomBoolToStringConverter());

IBindingTypeConverter? resolved = service.TypedConverters.TryGetConverter(typeof(bool), typeof(string));
bool success = resolved!.TryConvertTyped(true, null, out var result);

Console.WriteLine(resolved.GetType().Name);
Console.WriteLine(success);
Console.WriteLine(result);
```

```text
BooleanToStringTypeConverter
CustomBoolToStringConverter
True
AFFIRMATIVE
```

Give a replacement an affinity above 2. A converter that scores 2 or less loses to the built-in one, and a tie goes to the converter registered first. `RegisterDefaults` adds every converter each time you call it, so call it once. It registers no fallback or set-method converters.

### Register in a service of your own

`ResolveConverter` looks a converter up by type pair in a service. It asks the typed registry first and returns a converter for that pair when one applies. The excerpt registers one converter in a service of your own and resolves it by type pair. Nothing else in the process sees this service.

```csharp
ConverterService service = new();

service.TypedConverters.Register(new PriorityColourConverter());

object? resolved = service.ResolveConverter(typeof(TodoPriority), typeof(string));

Console.WriteLine(resolved!.GetType().Name);
```

```text
PriorityColourConverter
```

### Use the registries without a service

The three registries, `BindingTypeConverterRegistry`, `BindingFallbackConverterRegistry` and `SetMethodBindingConverterRegistry`, are public classes. You can create them without a `ConverterService` and register a converter in each. The excerpt does that, then asks each registry for the converter it holds. Use this when you need a single registry and no service.

```csharp
BindingTypeConverterRegistry typed = new();
BindingFallbackConverterRegistry fallback = new();
SetMethodBindingConverterRegistry setMethod = new();

typed.Register(new PriorityColourConverter());
fallback.Register(new CustomFallbackConverter());
setMethod.Register(new DemoSetMethodConverter());

Console.WriteLine(typed.TryGetConverter(typeof(TodoPriority), typeof(string))!.GetType().Name);
Console.WriteLine(fallback.TryGetConverter(typeof(TodoPriority), typeof(string))!.GetType().Name);
Console.WriteLine(setMethod.TryGetConverter(typeof(string), typeof(string))!.GetType().Name);
```

```text
PriorityColourConverter
CustomFallbackConverter
DemoSetMethodConverter
```

### Register with the builder

The application builder registers converters at start-up. `WithConverter`, `WithFallbackConverter` and `WithSetMethodConverter` each take one converter. `BuildApp` makes the configured service the shared `BindingConverters.Current`. The [setup page](setup.md) covers the builder. The excerpt registers one converter of each kind, builds the app and then reads all three back from the shared service.

```csharp
IReactiveUIBindingBuilder builder = RxBindingBuilder.CreateReactiveUIBindingBuilder();
_ = builder
    .WithCoreServices()
    .WithConverter(new PriorityColourConverter())
    .WithFallbackConverter(new CustomFallbackConverter())
    .WithSetMethodConverter(new DemoSetMethodConverter())
    .BuildApp();

ConverterService service = BindingConverters.Current;

Console.WriteLine(service.TypedConverters.TryGetConverter(typeof(TodoPriority), typeof(string))!.GetType().Name);
Console.WriteLine(service.FallbackConverters.TryGetConverter(typeof(TodoPriority), typeof(string))!.GetType().Name);
Console.WriteLine(service.SetMethodConverters.TryGetConverter(typeof(string), typeof(string))!.GetType().Name);
```

```text
PriorityColourConverter
CustomFallbackConverter
DemoSetMethodConverter
```

## Cover many type pairs with a fallback converter

A typed converter matches one exact pair. A fallback converter covers a family of pairs with one rule. You implement `IBindingFallbackConverter`, which has two members. `GetAffinityForObjects` takes the two types and says whether the converter handles the pair. `TryConvert` does the conversion. The class below handles any conversion whose target is a `string`, whatever the source type is.

```csharp
public sealed class CustomFallbackConverter : IBindingFallbackConverter
{
    private const int FallbackAffinity = 1;

    public int GetAffinityForObjects(Type fromType, Type toType) =>
        toType == typeof(string) ? FallbackAffinity : 0;

    public bool TryConvert(Type fromType, object from, Type toType, object? conversionHint, out object? result)
    {
        if (toType != typeof(string))
        {
            result = null;
            return false;
        }

        result = from.ToString();
        return result is not null;
    }
}
```

This converter turns any value into text with `ToString()`. It scores 1 when the target is `string` and 0 for every other target. A fallback converter must return a value that is not `null` when it returns `true`. The library treats a `null` result as a failed conversion. The library never passes it a `null` source value.

The library registers no fallback converters of its own. Register yours in `FallbackConverters`, which has the same `Register`, `TryGetConverter` and `GetAllConverters` members as the typed registry. The excerpt registers the converter and counts what the fallback registry holds.

```csharp
ConverterService service = new();

service.FallbackConverters.Register(new CustomFallbackConverter());

Console.WriteLine(service.FallbackConverters.GetAllConverters().Count());
```

```text
1
```

The registry picks the fallback converter the same way: highest positive affinity, and the earliest registered wins a tie. Affinity only ranks fallback converters against each other. The next excerpt asks the converter for its score on two pairs and converts a priority.

```csharp
ConverterService service = new();
service.FallbackConverters.Register(new CustomFallbackConverter());

IBindingFallbackConverter? fallback = service.FallbackConverters.TryGetConverter(typeof(TodoPriority), typeof(string));

Console.WriteLine(fallback!.GetAffinityForObjects(typeof(TodoPriority), typeof(string)));
Console.WriteLine(fallback.GetAffinityForObjects(typeof(TodoPriority), typeof(int)));
Console.WriteLine(fallback.TryConvert(typeof(TodoPriority), TodoPriority.High, typeof(string), null, out var text));
Console.WriteLine(text);
```

```text
1
0
True
High
```

### Typed converters win over fallback converters

`ConverterService.ResolveConverter` asks the typed registry first. It asks the fallback registry only when no typed converter applies. A typed converter always wins, whatever the affinities are. It returns `null` when neither registry has a match. The return type is `object?`, because the result is either an `IBindingTypeConverter` or an `IBindingFallbackConverter`. The excerpt resolves three type pairs from one service. Each pair shows a different outcome.

```csharp
ConverterService service = new();
DefaultConverterRegistration.RegisterDefaults(service);
service.FallbackConverters.Register(new CustomFallbackConverter());

object? forInteger = service.ResolveConverter(typeof(int), typeof(string));
object? forPriority = service.ResolveConverter(typeof(TodoPriority), typeof(string));
object? forGuid = service.ResolveConverter(typeof(TodoPriority), typeof(Guid));

Console.WriteLine(forInteger!.GetType().Name);
Console.WriteLine(forPriority!.GetType().Name);
Console.WriteLine(forGuid is null);
```

```text
IntegerToStringTypeConverter
CustomFallbackConverter
True
```

The `int` to `string` pair has a built-in typed converter, so the fallback converter never sees it. `TodoPriority` to `string` has none, so the fallback converter answers. `TodoPriority` to `Guid` has no typed converter and the fallback converter scores 0 for it, so nothing answers.

## Replace how a binding writes

A set-method converter changes the write step of a binding. A binding normally assigns the new value to the target property. A set-method converter can do something else instead, such as fill a collection with the new items. Implement `ISetMethodBindingConverter`, which has two members.

- `GetAffinityForObjects` ranks the converter for the type of the value and the type of the target. Either type can be `null`.
- `PerformSet` writes the value. It takes the target, the new value and the index arguments for an indexer. It returns the result of the write. The class below is the smallest useful one. It scores 1 for every pair and returns the new value unchanged.

```csharp
public sealed class DemoSetMethodConverter : ISetMethodBindingConverter
{
    private const int SetMethodAffinity = 1;

    public int GetAffinityForObjects(Type? fromType, Type? toType) => SetMethodAffinity;

    public object? PerformSet(object? toTarget, object? newValue, object?[]? arguments) => newValue;
}
```

Register it in `SetMethodConverters`. `ResolveSetMethodConverter` on the service picks the best one for a pair of types, with the same rule as the other registries. Its registry has the same `Register`, `TryGetConverter` and `GetAllConverters` members. The excerpt registers the converter, resolves it for a pair of strings and calls `PerformSet` on it.

```csharp
ConverterService service = new();
service.SetMethodConverters.Register(new DemoSetMethodConverter());

ISetMethodBindingConverter? converter = service.ResolveSetMethodConverter(typeof(string), typeof(string));

Console.WriteLine(converter!.GetType().Name);
Console.WriteLine(converter.PerformSet(null, "Renew car registration", null));
```

```text
DemoSetMethodConverter
Renew car registration
```

`GetAllConverters` counts what the set-method registry holds, the same check the typed and fallback registries offer.

```csharp
ConverterService service = new();

service.SetMethodConverters.Register(new DemoSetMethodConverter());

Console.WriteLine(service.SetMethodConverters.GetAllConverters().Count());
```

```text
1
```

A set-method converter usually applies to one kind of target. Return 0 for the others. `LegacyTagListSetMethodConverter` fills a `List<string>` instead of replacing it, so it applies only when the target is one. The excerpt asks it for its score on three pairs. A generated collection write passes `null` for the index arguments. It casts the returned value to the target type and reports it as the new value.

```csharp
ConverterService service = new();
service.SetMethodConverters.Register(new LegacyTagListSetMethodConverter());

ISetMethodBindingConverter converter = service.ResolveSetMethodConverter(typeof(IReadOnlyList<string>), typeof(List<string>))!;

Console.WriteLine(converter.GetAffinityForObjects(typeof(IReadOnlyList<string>), typeof(List<string>)));
Console.WriteLine(converter.GetAffinityForObjects(typeof(string), typeof(string)));
Console.WriteLine(converter.GetAffinityForObjects(fromType: null, toType: null));
```

```text
5
0
0
```

The score is 5 for the tag list pair, 0 for two strings and 0 when both types are `null`. The migration section below shows this converter at work.

## Migrate converters from ReactiveUI

An app built on ReactiveUI registers its binding converters with a Splat resolver. Splat is the library ReactiveUI uses to register a service once and look it up by type later. Moving that app to ReactiveUI.Binding leaves you with converters you wrote and tested. You do not rewrite them.

ReactiveUI's reflection engine takes its converters from the Splat resolver. ReactiveUI.Binding takes them from a `ConverterService`, and generated bindings read `BindingConverters.Current`. A converter that sits only in the resolver is never asked. The migration helpers read the resolver once and copy what it holds into a service.

The resolver must hold registrations of this library's `IBindingTypeConverter`, `IBindingFallbackConverter` and `ISetMethodBindingConverter`. The helpers ask the resolver for those three service types. The example project builds a resolver that holds one converter of each kind, registered the way a ReactiveUI app registers them. The method below is that setup. The steps that follow read from the resolver it returns.

```csharp
public static ModernDependencyResolver Create()
{
    ModernDependencyResolver resolver = new();

    resolver.RegisterConstant<IBindingTypeConverter>(new LegacyPriorityToColorConverter());
    resolver.RegisterConstant<IBindingFallbackConverter>(new LegacyObjectToStringFallbackConverter());
    resolver.RegisterConstant<ISetMethodBindingConverter>(new LegacyTagListSetMethodConverter());

    return resolver;
}
```

**1. Read the converters.** `ConverterMigrationHelper.ExtractConverters` returns the typed, fallback and set-method converters that the resolver holds. It registers nothing. It skips a `null` registration and keeps the order the resolver returns. The result is an `ExtractedConverters` record with one list for each kind. The excerpt reads the resolver and prints how many converters of each kind it found and their class names.

```csharp
using Splat.ModernDependencyResolver legacyResolver = LegacyAppDependencyResolver.Create();

ExtractedConverters extracted = ConverterMigrationHelper.ExtractConverters(legacyResolver);

Console.WriteLine(extracted.TypedConverters.Count);
Console.WriteLine(extracted.FallbackConverters.Count);
Console.WriteLine(extracted.SetMethodConverters.Count);
Console.WriteLine(extracted.TypedConverters[0].GetType().Name);
Console.WriteLine(extracted.FallbackConverters[0].GetType().Name);
Console.WriteLine(extracted.SetMethodConverters[0].GetType().Name);
```

```text
1
1
1
LegacyPriorityToColorConverter
LegacyObjectToStringFallbackConverter
LegacyTagListSetMethodConverter
```

**2. Copy them into a service.** `ImportFrom` is an extension on `ConverterService`. It extracts the three kinds and registers each converter with the matching registry. Each converter keeps its behaviour: the typed one maps `TodoPriority.High` to `#D32F2F`, the fallback one shows an `IssueState` as its text and the set-method one fills the list. The excerpt imports into a new service, then uses one converter of each kind to prove the copy worked.

```csharp
using Splat.ModernDependencyResolver legacyResolver = LegacyAppDependencyResolver.Create();
ConverterService service = new();

service.ImportFrom(legacyResolver);

IBindingTypeConverter? typed = service.TypedConverters.TryGetConverter(typeof(TodoPriority), typeof(string));
Console.WriteLine(typed!.TryConvertTyped(TodoPriority.High, null, out var highColour));
Console.WriteLine(highColour);

IBindingFallbackConverter? fallback = service.FallbackConverters.TryGetConverter(typeof(IssueState), typeof(string));
Console.WriteLine(fallback!.TryConvert(typeof(IssueState), IssueState.Closed, typeof(string), null, out var stateText));
Console.WriteLine(stateText);

ISetMethodBindingConverter? setMethod = service.SetMethodConverters.TryGetConverter(typeof(IReadOnlyList<string>), typeof(List<string>));
List<string> tagList = ["draft"];
List<string> newTags = ["travel", "admin"];

_ = setMethod!.PerformSet(tagList, newTags, null);

Console.WriteLine(string.Join(", ", tagList));
```

```text
True
#D32F2F
True
Closed
travel, admin
```

The import is a one-time copy. A converter that you add to the resolver later does not reach the service. Import after the resolver is complete.

**3. Import into the shared service.** The next two examples import into `BindingConverters.Current`, which is the service that bindings use when you do not hand them another one. The first passes the migrated converter to `BindTo` as the converter override.

```csharp
using Splat.ModernDependencyResolver legacyResolver = LegacyAppDependencyResolver.Create();

BindingConverters.Current.ImportFrom(legacyResolver);

IBindingTypeConverter? migrated = BindingConverters.Current.TypedConverters.TryGetConverter(typeof(TodoPriority), typeof(string));
TodoItem item = new() { Title = "Renew passport", Priority = TodoPriority.High };
Label priorityBadge = new();

using (item.WhenChanged(x => x.Priority).BindTo(priorityBadge, x => x.Text, migrated!))
{
    Console.WriteLine(priorityBadge.Text);

    item.Priority = TodoPriority.Low;

    Console.WriteLine(priorityBadge.Text);
}
```

```text
#D32F2F
#9E9E9E
```

The second leaves the converter out. The binding looks in the same service and takes the converter with the highest affinity for the two types. Prefer this form. Import once at start-up, and every binding that needs a migrated converter finds it.

```csharp
using Splat.ModernDependencyResolver legacyResolver = LegacyAppDependencyResolver.Create();

BindingConverters.Current.ImportFrom(legacyResolver);

TodoItem item = new() { Title = "Book dentist", Priority = TodoPriority.Normal };
Label priorityBadge = new();

using (item.WhenChanged(x => x.Priority).BindTo(priorityBadge, x => x.Text))
{
    Console.WriteLine(priorityBadge.Text);

    item.Priority = TodoPriority.High;

    Console.WriteLine(priorityBadge.Text);
}
```

```text
#FB8C00
#D32F2F
```

The legacy converter scores 100, so it wins over any built-in converter for its pair.

**4. Work with `ExtractedConverters`.** The record is public, and you can build one yourself. It deconstructs into its three lists. The excerpt builds one from three single-item lists, takes it apart again and prints the class name of each converter.

```csharp
List<IBindingTypeConverter> typed = [new LegacyPriorityToColorConverter()];
List<IBindingFallbackConverter> fallback = [new LegacyObjectToStringFallbackConverter()];
List<ISetMethodBindingConverter> setMethod = [new LegacyTagListSetMethodConverter()];
ExtractedConverters extracted = new(typed, fallback, setMethod);

var (typedConverters, fallbackConverters, setMethodConverters) = extracted;

Console.WriteLine(typedConverters[0].GetType().Name);
Console.WriteLine(fallbackConverters[0].GetType().Name);
Console.WriteLine(setMethodConverters[0].GetType().Name);
```

```text
LegacyPriorityToColorConverter
LegacyObjectToStringFallbackConverter
LegacyTagListSetMethodConverter
```

It is a `record`, so two results are equal when they hold the same three lists, and `with` copies one. This excerpt compares a result with a copy and with a copy that has no fallback converters. It also checks `Equals`, `GetHashCode` and `ToString`.

```csharp
using Splat.ModernDependencyResolver legacyResolver = LegacyAppDependencyResolver.Create();
ExtractedConverters extracted = ConverterMigrationHelper.ExtractConverters(legacyResolver);
ExtractedConverters same = extracted with { };
ExtractedConverters withoutFallback = extracted with { FallbackConverters = [] };
object boxedSame = same;

Console.WriteLine(extracted == same);
Console.WriteLine(extracted != withoutFallback);
Console.WriteLine(extracted.Equals(same));
Console.WriteLine(extracted.Equals(boxedSame));
Console.WriteLine(extracted.GetHashCode() == same.GetHashCode());
Console.WriteLine(extracted.ToString().StartsWith(nameof(ExtractedConverters), StringComparison.Ordinal));
```

```text
True
True
True
True
True
True
```

### Where this differs from ReactiveUI

- **Where converters live.** ReactiveUI's reflection engine takes its converters from the Splat resolver. ReactiveUI.Binding takes them from a `ConverterService`, and `ImportFrom` copies them across.
- **How many services exist.** A Splat resolver is usually the one global locator. You can create any number of `ConverterService` objects, each with its own registries.
- **Late registrations.** The import copies what the resolver holds at that moment. A converter registered with the resolver after the import does not reach the service.

## Pair two conversions

A two-way binding needs a conversion for each direction. `TwoWayConverterPair<TSourceProp, TTargetProp>` is a `record` that holds the two conversions as delegates. `Forward` converts a source value to the target type. `Reverse` converts a target value back. The runtime binding methods in `ReactiveUI.Binding.Fallback` take a pair. The type is public but hidden from IntelliSense. `TwoWayConverters.Create` builds a pair without naming its type arguments. A pair deconstructs into `Forward` and `Reverse`. The excerpt pairs a priority-to-text conversion with a text-to-priority one, takes the pair apart and calls each half.

```csharp
Func<TodoPriority, string> toText = static priority => priority.ToString();
Func<string, TodoPriority> toPriority = Enum.Parse<TodoPriority>;
TwoWayConverterPair<TodoPriority, string> pair = new(toText, toPriority);

var (forward, reverse) = pair;

Console.WriteLine(forward(TodoPriority.High));
Console.WriteLine(reverse("Low"));
```

```text
High
Low
```

Two pairs are equal when they hold the same two delegates. A pair with a different delegate is not equal. The excerpt also checks `Equals`, `GetHashCode` and `ToString`.

```csharp
Func<TodoPriority, string> toText = static priority => priority.ToString();
Func<string, TodoPriority> toPriority = Enum.Parse<TodoPriority>;
TwoWayConverterPair<TodoPriority, string> pair = new(toText, toPriority);
TwoWayConverterPair<TodoPriority, string> same = new(toText, toPriority);
TwoWayConverterPair<TodoPriority, string> different = new(toText, static _ => TodoPriority.Low);
object boxedSame = same;

Console.WriteLine(pair == same);
Console.WriteLine(pair != different);
Console.WriteLine(pair.Equals(boxedSame));
Console.WriteLine(pair.GetHashCode() == same.GetHashCode());
Console.WriteLine(pair.ToString().StartsWith(nameof(TwoWayConverterPair<,>), StringComparison.Ordinal));
```

```text
True
True
True
True
True
```

## Next steps

- [Converters](converters.md) covers the built-in converters.
- [Bindings](bindings.md) covers the binding methods that take a converter.
- [Views](views.md) covers the view locator that `AccountsToViewsConverter` calls.
- [Setup](setup.md) covers the builder that registers converters at start-up.
- [Unsafe](unsafe.md) covers the reflection-based twins of the binding methods.

## API reference

The table lists the public members of the converter contracts, the registries, the builder methods and the migration helpers. The built-in converters are on the [converters page](converters.md). The generated [API reference](api.md) lists every signature.

| Member | What it does | Type and values | Notes |
| --- | --- | --- | --- |
| [`IBindingTypeConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/IBindingTypeConverter.cs) | Converts a boxed value from one type to another for the binding methods. | Interface that also implements `IEnableLogger`. | Register an implementation to teach a binding a type pair. |
| [`IBindingTypeConverter.FromType`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/IBindingTypeConverter.cs) | Gets the source type the converter accepts. | Read-only `Type`. | A registry reads it once, at registration. |
| [`IBindingTypeConverter.ToType`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/IBindingTypeConverter.cs) | Gets the target type the converter produces. | Read-only `Type`. | A registry reads it once, at registration. |
| [`IBindingTypeConverter.GetAffinityForObjects`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/IBindingTypeConverter.cs) | Scores the converter against others for the same type pair. | Returns `int`. | A positive value means the converter applies. Zero or less excludes it. The highest value wins, and the earliest registered converter wins a tie. |
| [`IBindingTypeConverter.TryConvertTyped`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/IBindingTypeConverter.cs) | Converts a boxed value. | Takes `object? from` and `object? conversionHint`. Returns `bool` and an `object?` result. | Returns `false` on failure, and the result is `null`. A `null` source is accepted only when the converter can convert `null`. |
| [`IBindingTypeConverter<TFrom, TTo>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/IBindingTypeConverter%7BTFrom,TTo%7D.cs) | Adds a typed conversion for one type pair. | Interface. `TFrom` is contravariant. | Generated bindings call the typed method when they can, so a value type is not boxed. |
| [`IBindingTypeConverter<TFrom, TTo>.TryConvert`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/IBindingTypeConverter%7BTFrom,TTo%7D.cs) | Converts a value without boxing. | Takes `TFrom? from` and `object? conversionHint`. Returns `bool` and a `TTo?` result. | The result can be `null` on success when the target type is nullable. |
| [`BindingTypeConverter<TFrom, TTo>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingTypeConverter.cs) | Base class for a converter between one type pair. | Abstract class with a protected constructor. | Supplies `FromType` as `typeof(TFrom)`, `ToType` as `typeof(TTo)` and `TryConvertTyped`. |
| [`BindingTypeConverter<TFrom, TTo>.GetAffinityForObjects`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingTypeConverter.cs) | Scores the converter. You override it. | Abstract. Returns `int`. | The built-in converters return 2 and the equality converter returns 1, so a larger value outranks them. |
| [`BindingTypeConverter<TFrom, TTo>.TryConvert`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingTypeConverter.cs) | Converts one value. You override it. | Abstract. Takes `TFrom? from` and `object? conversionHint`. Returns `bool` and a `TTo?` result. | Return `false` when the value cannot be converted. |
| [`BindingTypeConverter<TFrom, TTo>.TryConvertTyped`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/BindingTypeConverter.cs) | Converts a boxed value by casting it to `TFrom` and calling `TryConvert`. | Takes `object? from` and `object? conversionHint`. Returns `bool` and an `object?` result. | Returns `false` when the value is not a `TFrom`. A `null` source becomes `default` when `TFrom` can hold `null`, and fails when it cannot. The hint passes through unchanged. |
| [`IBindingFallbackConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/IBindingFallbackConverter.cs) | Converts type pairs that no typed converter covers. | Interface that also implements `IEnableLogger`. | The converter service asks fallback converters only when no typed converter applies. The library registers none. |
| [`IBindingFallbackConverter.GetAffinityForObjects`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/IBindingFallbackConverter.cs) | Scores the converter for a runtime type pair. | Takes `Type fromType` and `Type toType`. Returns `int`. | A positive value means the converter handles the pair. Zero or less means it does not. The score ranks fallback converters against each other only. |
| [`IBindingFallbackConverter.TryConvert`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/IBindingFallbackConverter.cs) | Converts a value to the target type. | Takes `Type fromType`, `object from`, `Type toType` and `object? conversionHint`. Returns `bool` and an `object?` result. | The library never passes `null` as the source. A `true` return with a `null` result counts as a failed conversion. |
| [`ISetMethodBindingConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/ISetMethodBindingConverter.cs) | Replaces how a binding writes a value to its target, for example to fill a collection. | Interface that also implements `IEnableLogger`. | Register it in the set-method registry. |
| [`ISetMethodBindingConverter.GetAffinityForObjects`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/ISetMethodBindingConverter.cs) | Scores the converter for writing one type to a target of another type. | Takes `Type? fromType` and `Type? toType`. Returns `int`. | Both types can be `null`. Zero or less excludes the converter. |
| [`ISetMethodBindingConverter.PerformSet`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/ISetMethodBindingConverter.cs) | Writes a value to the target. | Takes `object? toTarget`, `object? newValue` and `object?[]? arguments`. Returns `object?`. | `arguments` holds the index arguments of an indexer target. A generated collection write passes `null` and casts the returned value to the target type. |
| [`ConverterService`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/ConverterService.cs) | Owns the three registries and resolves the best converter for a type pair. | Sealed class with a public parameterless constructor. | A new service is empty. |
| [`ConverterService.TypedConverters`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/ConverterService.cs) | Gets the registry of typed converters. | Read-only `BindingTypeConverterRegistry`. | Each converter matches one exact source and target pair. |
| [`ConverterService.FallbackConverters`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/ConverterService.cs) | Gets the registry of fallback converters. | Read-only `BindingFallbackConverterRegistry`. | Consulted when no typed converter applies. |
| [`ConverterService.SetMethodConverters`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/ConverterService.cs) | Gets the registry of set-method converters. | Read-only `SetMethodBindingConverterRegistry`. | These converters replace the write step of a binding. |
| [`ConverterService.ResolveConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/ConverterService.cs) | Returns the best typed converter for the pair, or else the best fallback converter. | Takes `Type fromType` and `Type toType`. Returns `object?`. | A typed converter always wins, whatever the affinities. The result is an `IBindingTypeConverter`, an `IBindingFallbackConverter` or `null`. Throws `ArgumentNullException` when either type is `null`. |
| [`ConverterService.ResolveSetMethodConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/ConverterService.cs) | Returns the best set-method converter for the pair. | Takes `Type? fromType` and `Type? toType`. Returns `ISetMethodBindingConverter?`. | Both types can be `null`. Returns `null` when no converter has a positive affinity. |
| [`BindingConverters`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/BindingConverters.cs) | Holds the process-wide converter service that generated and runtime bindings use. | Static class. | The builder's `BuildApp` replaces the service. |
| [`BindingConverters.Current`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/BindingConverters.cs) | Gets the converter service in use. | Read-only `ConverterService`. | Starts empty. It holds the built-in converters only after the builder publishes its service. |
| [`BindingTypeConverterRegistry`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/BindingTypeConverterRegistry.cs) | Holds typed converters grouped by exact source and target type pair. | Sealed class with a public parameterless constructor. | Each registration copies the registry, so registering is slow and lookups are fast. Safe to call from any thread. |
| [`BindingTypeConverterRegistry.Register`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/BindingTypeConverterRegistry.cs) | Files a converter under its `FromType` and `ToType`. | Takes `IBindingTypeConverter converter`. Returns nothing. | Several converters can share a pair. Throws `ArgumentNullException` for `null`. |
| [`BindingTypeConverterRegistry.TryGetConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/BindingTypeConverterRegistry.cs) | Returns the converter with the highest positive affinity for the exact pair. | Takes `Type fromType` and `Type toType`. Returns `IBindingTypeConverter?`. | The earliest registered converter wins a tie. Returns `null` when none is registered or every one scores zero or less. Throws `ArgumentNullException` when either type is `null`. |
| [`BindingTypeConverterRegistry.GetAllConverters`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/BindingTypeConverterRegistry.cs) | Returns a copy of every registered converter. | Returns `IEnumerable<IBindingTypeConverter>`. | Empty when none are registered. A registration made after the call does not change the copy. The order across type pairs is not defined. |
| [`BindingFallbackConverterRegistry`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/BindingFallbackConverterRegistry.cs) | Holds fallback converters, which the registry asks about a type pair at lookup time. | Sealed class with a public parameterless constructor. | Same locking model as the typed registry. |
| [`BindingFallbackConverterRegistry.Register`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/BindingFallbackConverterRegistry.cs) | Adds a fallback converter. | Takes `IBindingFallbackConverter converter`. Returns nothing. | Throws `ArgumentNullException` for `null`. |
| [`BindingFallbackConverterRegistry.TryGetConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/BindingFallbackConverterRegistry.cs) | Asks each converter for its affinity and returns the highest positive one. | Takes `Type fromType` and `Type toType`. Returns `IBindingFallbackConverter?`. | The earliest registered converter wins a tie. Returns `null` when none scores above zero. Throws `ArgumentNullException` when either type is `null`. |
| [`BindingFallbackConverterRegistry.GetAllConverters`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/BindingFallbackConverterRegistry.cs) | Returns a copy of every registered fallback converter. | Returns `IEnumerable<IBindingFallbackConverter>`. | Registration order. Empty when none are registered. |
| [`SetMethodBindingConverterRegistry`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/SetMethodBindingConverterRegistry.cs) | Holds set-method converters, which the registry asks about a type pair at lookup time. | Sealed class with a public parameterless constructor. | Same locking model as the typed registry. |
| [`SetMethodBindingConverterRegistry.Register`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/SetMethodBindingConverterRegistry.cs) | Adds a set-method converter. | Takes `ISetMethodBindingConverter converter`. Returns nothing. | Throws `ArgumentNullException` for `null`. |
| [`SetMethodBindingConverterRegistry.TryGetConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/SetMethodBindingConverterRegistry.cs) | Asks each converter for its affinity and returns the highest positive one. | Takes `Type? fromType` and `Type? toType`. Returns `ISetMethodBindingConverter?`. | Both types can be `null`. The earliest registered converter wins a tie. Returns `null` when none scores above zero. |
| [`SetMethodBindingConverterRegistry.GetAllConverters`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/SetMethodBindingConverterRegistry.cs) | Returns a copy of every registered set-method converter. | Returns `IEnumerable<ISetMethodBindingConverter>`. | Registration order. Empty when none are registered. |
| [`DefaultConverterRegistration`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/DefaultConverterRegistration.cs) | Registers the built-in typed converters with a service. | Static class. | Registers no fallback or set-method converters. |
| [`DefaultConverterRegistration.RegisterDefaults`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/DefaultConverterRegistration.cs) | Adds the string, equality, boolean, `Guid`, `Uri`, numeric, and date and time converters, each with its nullable forms. | Takes `ConverterService service`. Returns nothing. | Each call adds every converter again. `DateOnly` and `TimeOnly` converters exist on .NET 8 and later. Throws `ArgumentNullException` for `null`. |
| [`IReactiveUIBindingBuilder.WithConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/IReactiveUIBindingBuilder.cs) | Registers a typed converter in the builder's service. | Takes `IBindingTypeConverter converter`. Returns `IReactiveUIBindingBuilder`. | `ReactiveUIBindingBuilder` implements it, and throws `ArgumentNullException` for `null`. [`BuilderMixins`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Mixins/BuilderMixins.cs) adds the same method on `IAppBuilder`, which throws `InvalidOperationException` when the app builder is not a binding builder. |
| [`IReactiveUIBindingBuilder.WithFallbackConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/IReactiveUIBindingBuilder.cs) | Registers a fallback converter in the builder's service. | Takes `IBindingFallbackConverter converter`. Returns `IReactiveUIBindingBuilder`. | Same null and receiver rules as `WithConverter`. |
| [`IReactiveUIBindingBuilder.WithSetMethodConverter`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Builder/IReactiveUIBindingBuilder.cs) | Registers a set-method converter in the builder's service. | Takes `ISetMethodBindingConverter converter`. Returns `IReactiveUIBindingBuilder`. | Same null and receiver rules as `WithConverter`. |
| [`ConverterMigrationHelper`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/ConverterMigrationHelper.cs) | Reads converters registered with a Splat dependency resolver. | Static class. | Registers nothing. |
| [`ConverterMigrationHelper.ExtractConverters`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/ConverterMigrationHelper.cs) | Collects the typed, fallback and set-method converters the resolver holds. | Takes `IReadonlyDependencyResolver resolver`. Returns `ExtractedConverters`. | Keeps the order the resolver returns and skips a `null` registration. Throws `ArgumentNullException` for a `null` resolver. |
| [`ConverterMigrationHelperMixins.ImportFrom`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/ConverterMigrationHelperMixins.cs) | Extension member on `ConverterService` that registers the resolver's converters with the matching registries. | Takes `IReadonlyDependencyResolver resolver`. Returns nothing. | A one-time copy. A converter added to the resolver after the call does not reach the service. Throws `ArgumentNullException` for a `null` service or resolver. |
| [`ExtractedConverters`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/ExtractedConverters.cs) | Groups the converters found in a resolver by the role each one fills. | Sealed record. The constructor takes the three lists in the order typed, fallback, set-method. | Two results are equal when they hold the same three lists. Deconstructs into the three lists. |
| [`ExtractedConverters.TypedConverters`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/ExtractedConverters.cs) | Holds the converters that move a value between two known types. | `IList<IBindingTypeConverter>`, init-only. | Empty when the resolver holds none. |
| [`ExtractedConverters.FallbackConverters`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/ExtractedConverters.cs) | Holds the converters asked when no typed converter matches. | `IList<IBindingFallbackConverter>`, init-only. | Empty when the resolver holds none. |
| [`ExtractedConverters.SetMethodConverters`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Bindings/Converters/ExtractedConverters.cs) | Holds the converters that write a value to a target. | `IList<ISetMethodBindingConverter>`, init-only. | Empty when the resolver holds none. |
| [`TwoWayConverterPair<TSourceProp, TTargetProp>`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/TwoWayConverterPair.cs) | Holds the two conversions a two-way binding needs. | Sealed record. The constructor takes `Forward` and `Reverse`. | Hidden from IntelliSense. Two pairs are equal when they hold the same two delegates. |
| [`TwoWayConverterPair.Forward`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/TwoWayConverterPair.cs) | Converts a source value to the target type. | `Func<TSourceProp, TTargetProp>`, init-only. | Read by deconstructing the pair as well. |
| [`TwoWayConverterPair.Reverse`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/TwoWayConverterPair.cs) | Converts a target value back to the source type. | `Func<TTargetProp, TSourceProp>`, init-only. | Read by deconstructing the pair as well. |
| [`TwoWayConverters`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/TwoWayConverters.cs) | Builds a `TwoWayConverterPair` without naming its type arguments. | Static class. | Hidden from IntelliSense. |
| [`TwoWayConverters.Create`](https://github.com/reactiveui/ReactiveUI.Binding.SourceGenerators/blob/main/src/ReactiveUI.Binding.Shared/Fallback/TwoWayConverters.cs) | Pairs a forward and a reverse conversion. | Takes `Func<TSourceProp, TTargetProp> forward` and `Func<TTargetProp, TSourceProp> reverse`. Returns `TwoWayConverterPair<TSourceProp, TTargetProp>`. | The type arguments come from the arguments. |
