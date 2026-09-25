---
Order: 5
---
# Migration Guide: Bindings on ReactiveUI.Binding

ReactiveUI now runs `WhenAnyValue`, `Bind`, `OneWayBind`, `BindCommand` and `ToProperty` on
[ReactiveUI.Binding](../binding/index.md). Most of your code keeps compiling as it is. This guide covers the calls
that need a change, and the places where the new engine behaves differently.

ReactiveUI.Binding writes the code for each binding while your project builds. A *source generator* is the part of
the compiler that writes it. An *analyzer* is the part that checks your calls and reports a warning when it cannot
write code for one. View location, view hosts, routing, activation and validation stay in ReactiveUI.

## Upgrade in five steps

1. **Update the ReactiveUI packages.** `ReactiveUI` brings `ReactiveUI.Binding` with it, and `ReactiveUI.Wpf`,
   `ReactiveUI.WinForms` and `ReactiveUI.Maui` bring the matching Binding platform package. You add no new
   package. Reference one ReactiveUI flavor, never both:

   | Your app uses | Reference | It brings |
   |---|---|---|
   | ReactiveUI.Primitives (the default) | `ReactiveUI` | `ReactiveUI.Binding` |
   | System.Reactive | `ReactiveUI.Reactive` | `ReactiveUI.Binding.Reactive` |

2. **Build, and read the warnings.** Every warning that starts with `RXUIBIND` names a call that gets no generated
   code. Such a call throws when it runs. The [Unsafe calls](#calls-the-generator-cannot-read) section below and
   the [analyzer messages](../binding/setup.md#read-the-analyzer-messages) list the fixes.

3. **Fix the calls the generator cannot read.** Make private controls and nested types `internal`, or switch the
   call to its `Unsafe` twin.

4. **Check your own `IReactiveObject` types.** A class that implements `IReactiveObject` by hand needs one change
   to raise `PropertyChanged`. Classes that derive from `ReactiveObject` need nothing. See
   [Hand-written IReactiveObject types](#hand-written-ireactiveobject-types).

5. **Run your tests.** Read [Behavior that changed](#behavior-that-changed) for the differences a test can catch,
   such as the thread a binding writes on.

## Namespaces

The binding methods and their types now live in the `ReactiveUI.Binding` namespace (`ReactiveUI.Binding.Reactive`
for the System.Reactive flavor). The ReactiveUI package adds that namespace to your project's global usings. With
`<ImplicitUsings>enable</ImplicitUsings>`, your calls compile with no new `using`. Without implicit usings, add
this line once:

```csharp
global using ReactiveUI.Binding;
```

These types moved to `ReactiveUI.Binding` and keep their names: `IViewFor`, `IViewFor<T>`, `IActivatableView`,
`IReactiveBinding<TView, TValue>`, `BindingDirection`, `TriggerUpdate`, `IObservedChange<TSender, TValue>`,
`ICreatesObservableForProperty`, `IBindingTypeConverter` and the standard converters, `Interaction<TInput, TOutput>`,
`ObservableAsPropertyHelper<T>`, `ViewContractAttribute` and `SingleInstanceViewAttribute`.

ReactiveUI.Binding also has a view locator of its own, with the same names as ReactiveUI's: `IViewLocator`,
`ViewLocator` and `DefaultViewLocator`. In your code those names still mean ReactiveUI's locator, which the view
hosts use. Write `ReactiveUI.Binding.ViewLocator` when you want the other one.

## Calls the generator cannot read

The generator writes code for a call when each property is picked by an inline lambda over a path it can read. A
*path* is the chain of members in the lambda, such as `x => x.Customer.Name`. A path can go through properties and
through instance fields, such as controls named in XAML:

```csharp
this.Bind(ViewModel, vm => vm.Name, v => v.NameBox.Text);
```

Every binding method has an `Unsafe` twin with the same parameters, such as `BindUnsafe` and `WhenAnyValueUnsafe`.
An `Unsafe` twin reads the path with reflection while your app runs. It works for any path, but trimming can
remove the members it looks for, so it carries `[RequiresUnreferencedCode]`. Prefer the generated call, and
use the twin only where the generator cannot help.

| Your call | What to do |
|---|---|
| The path goes through an indexer or a method, such as `x => x.Items[0].Length` | Call the `Unsafe` twin |
| The call sits in a generic method, and its types come from type parameters | Call the `Unsafe` twin |
| A type in the call is a private or protected nested class | Make the class `internal`, or call the `Unsafe` twin |
| A control field is private | Make it `internal`, as below |
| The path is an `Expression` you build at run time | Call `WhenAnyDynamic`, which always uses reflection |
| The binding needs `signalViewUpdate` or `TriggerUpdate` | Call `BindUnsafe`, the only overload that has them |

WinForms designer fields and MAUI `x:Name` fields are private by default. Make a MAUI field `internal` in XAML:

```xml
<Entry x:Name="NameBox" x:FieldModifier="internal" />
```

A field raises no change notification, so the binding reads it once. The rest of the path is watched as usual.

[Unsafe twins and the runtime fallback](../binding/unsafe.md) shows every twin with an example.

### Triggered bindings

`signalViewUpdate` and `TriggerUpdate` exist only on `BindUnsafe`, and only with conversion lambdas. The signal
now comes after the two converters. Here `saveClicked` is a stream that fires when the user clicks Save, so the
amount is written to the view model only then. The type of its values does not matter:

```csharp
view.BindUnsafe(
    viewModel,
    static vm => vm.Amount,
    static v => v.AmountBox.Text,
    static amount => amount.ToString(CultureInfo.CurrentCulture),
    static text => decimal.TryParse(text, CultureInfo.CurrentCulture, out var value) ? value : 0M,
    saveClicked,
    TriggerUpdate.ViewToViewModel);
```

A conversion lambda always returns a value, and the binding writes it. ReactiveUI used to skip the write when a
converter refused a value, such as `"fa0"` typed into a number box. To keep that, return the value the other side
already holds. The binding sees an equal value and writes nothing.

## ToProperty and ObservableAsPropertyHelper

`ToProperty` is generated. Name the property with a lambda, `x => x.Total`, or with `nameof(Total)`. Any other
form, such as a computed string, gets no generated code and throws when it runs. On a `ReactiveObject`, change
notifications and `SuppressChangeNotifications` work as before.

`ObservableAsPropertyHelper<T>` keeps its members: `Value`, `IsSubscribed`, `ThrownExceptions` and `Default()`.
One thing changed. When the source stream fails and nothing subscribes to `ThrownExceptions`, the helper throws the
error on the thread that produced it. It no longer goes to `RxState.DefaultExceptionHandler`. Subscribe to
`ThrownExceptions` if you relied on the handler.

[Properties backed by observables](../binding/properties.md) covers `ToProperty` in full.

## Views follow their view model

`this.Bind(ViewModel, ...)`, `OneWayBind`, `BindCommand` and `BindInteraction` accept a nullable view model, so
they compile when your view's `ViewModel` property is nullable. The binding follows `view.ViewModel`:

- A binding made before the view has a view model waits for one.
- Setting a new view model moves the binding to it.
- While a parent on the path is `null`, such as `x => x.Customer!.Name` with no `Customer`, the view keeps its
  value. Nothing is written until the path is whole again.

A view that implements only the non-generic `IViewFor` binds the view model you pass in.

## Hand-written IReactiveObject types

The generated code watches `PropertyChanged`. A class that implements `IReactiveObject` itself raises that event
only after it calls `SubscribePropertyChangedEvents`. Call it from the event's `add` accessor:

```csharp
private PropertyChangedEventHandler? _propertyChanged;

public event PropertyChangedEventHandler? PropertyChanged
{
    add
    {
        this.SubscribePropertyChangedEvents();
        _propertyChanged += value;
    }

    remove => _propertyChanged -= value;
}

void IReactiveObject.RaisePropertyChanged(PropertyChangedEventArgs args) => _propertyChanged?.Invoke(this, args);
```

Do the same with `SubscribePropertyChangingEvents` for `PropertyChanging`. ReactiveUI's own view hosts and
platform base classes already do this.

## Interactions

`Interaction<TInput, TOutput>.Handle` returns `Task<TOutput>`, not `IObservable<TOutput>`. Await it:

```csharp
var confirmed = await ViewModel.ConfirmDelete.Handle(item);
```

## Behavior that changed

### Every binding writes on the view's thread

Each binding moves its writes onto the thread that owns the view, on every platform. A value set on another
thread reaches the view one turn of the message loop later. A test that sets a value and checks the view at once
may need to let the UI thread run first.

A binding to a WPF, WinForms or MAUI object needs that platform's Binding package to move its writes. The
ReactiveUI platform packages bring it. Without it, warning RXUIBIND017 reports the call.

### A burst of changes writes once

A binding writes only the latest value. When a property changes several times before the binding can write, the
values in between are skipped, and the binding's change stream skips them too.

### Converters are picked by the declared types

A binding picks its converter from the types of the two properties as they are declared. ReactiveUI's builder
registers the standard converters. The old fallback through `TypeDescriptor` is gone, so register a converter for
any other pair. When nothing is registered and the value already has the target type, it passes through
unchanged. For example, a `string` held in an `object`-typed `SelectedItem` binds to a `string` property.

### Missing change notifications are found at build time

ReactiveUI logged a warning when your app ran into a property with no change notification. The analyzer now
reports it as RXUIBIND010 when you build. The binding reads such a property once, as before.

### Custom observation providers compete by score

A custom `ICreatesObservableForProperty` handles a property only when its score is higher than the generated code's.
The generated code wins a tie. Call `ObservationAffinityChecker.Refresh()` after you change registrations.

## Removed types

| Removed | Use instead |
|---|---|
| ReactiveUI's `WhenAny*`, `ObservableForProperty`, `Bind`, `OneWayBind`, `BindTo`, `BindCommand`, `InvokeCommand` and `BindInteraction` extension methods | The same names from `ReactiveUI.Binding`, or their `Unsafe` twins |
| `PropertyBinderImplementation`, `CommandBinderImplementation`, `InteractionBinderImplementation` | The extension methods |
| `Reflection`, `ExpressionMixins`, `ReflectionMixins`, `ExpressionRewriter` | `ReactiveUI.Binding.Expressions` |
| ReactiveUI's observation providers for `INotifyPropertyChanged`, KVO, UIKit, AppKit, Android, WinForms, WPF and WinUI | Nothing. The generated code covers them |
| ReactiveUI's platform command binders | Nothing. The generated code covers them |
| The Apple `NSDate` converters | Nothing. The generated code converts `NSDate` |
| `ComponentModelFallbackConverter` | A converter you register for the pair |

## Build requirements

- **Compiler:** Roslyn 4.8 or newer, which ships with Visual Studio 2022 17.8 and the .NET 8 SDK. An older
  compiler stops the build with RXUIBIND100.
- **Interceptors:** with Roslyn 4.13 or newer and C# 11 or later, the generator replaces each call in place. With an
  older compiler or language version it writes overloads instead, and your code does not change either way.
  [Choose how calls reach generated code](../binding/setup.md#choose-how-calls-reach-generated-code) explains the
  difference.
- **Trimming and NativeAOT:** generated code is safe to trim. The `Unsafe` twins and `WhenAnyDynamic` are not, and a
  `PublishAot` build reports every place that calls them.

## Where to go next

- [ReactiveUI.Binding overview](../binding/index.md)
- [Bindings](../binding/bindings.md)
- [Analyzer messages](../binding/setup.md#read-the-analyzer-messages)
- [Threading](../binding/threading.md)
