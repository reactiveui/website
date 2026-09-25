---
Order: 5
---
# Migration Guide: Bindings on ReactiveUI.Binding

ReactiveUI now runs `WhenAnyValue`, `Bind`, `OneWayBind`, `BindCommand` and `ToProperty` on
[ReactiveUI.Binding](../../binding/index.md). Most of your code keeps compiling as it is. This guide covers the calls
that need a change, and the places where the new engine behaves differently.

ReactiveUI.Binding writes the code for each binding while your project builds. A *source generator* is the part of
the compiler that writes it. An *analyzer* is the part that checks your calls and reports a warning when it cannot
write code for one. View location moves to ReactiveUI.Binding too, so views are found by generated code. View hosts,
routing, activation and validation stay in ReactiveUI.

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
   the [analyzer messages](../../binding/setup.md#read-the-analyzer-messages) list the fixes.

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

These types moved to `ReactiveUI.Binding` and keep their names. A file that names one of them needs the namespace:

| Area | Types |
|---|---|
| Views | `IViewFor`, `IViewFor<T>`, `IActivatableView` |
| Bindings | `IReactiveBinding<TView, TValue>`, `ReactiveBinding<TView, TValue>`, `BindingDirection`, `TriggerUpdate` |
| Observed changes | `IObservedChange<TSender, TValue>`, `ObservedChange<TSender, TValue>` |
| Extension points | `ICreatesObservableForProperty`, `ICreatesCommandBinding`, `IPropertyBindingHook`, `BindingAffinity` |
| Converters | `IBindingTypeConverter`, `IBindingTypeConverter<TFrom, TTo>`, `IBindingFallbackConverter`, `ISetMethodBindingConverter`, the standard converters and their registries |
| Interactions | `Interaction<TInput, TOutput>`, `IInteraction<TInput, TOutput>`, `IInteractionContext<TInput, TOutput>`, `IOutputContext<TInput, TOutput>`, `UnhandledInteractionException<TInput, TOutput>` |
| Properties | `ObservableAsPropertyHelper<T>` and `ToProperty` |
| View registration | `ViewContractAttribute`, `SingleInstanceViewAttribute`, `ExcludeFromViewRegistrationAttribute` |
| View location | `IViewLocator`, `DefaultViewLocator`, `ViewLocator`, `ViewMappingBuilder`, `ViewLocatorNotFoundException` |

`ObservableForProperty` lives in `ReactiveUI.Binding.ObservableForProperty`. The ReactiveUI package imports that
namespace too. It reads its path with reflection, so it is not safe to trim. For changes only, without the current
value, `WhenAnyValue(x => x.Name).Skip(1)` is generated at compile time instead.

A view's `WhenActivated(block)` finds the view's `ViewModel` with reflection, so it is not safe to trim either. Pass
the view's own `WhenAnyValue(x => x.ViewModel)` as the second argument, and it activates the view model without
reflection:

```csharp
this.WhenActivated(
    disposables => disposables(this.Bind(ViewModel, x => x.Name, v => v.NameBox.Text)),
    this.WhenAnyValue(x => x.ViewModel));
```

## View location

The view hosts (`ViewModelViewHost` and `RoutedViewHost` on every platform) now use ReactiveUI.Binding's view locator.
Its source generator finds each `IViewFor<T>` in your app at compile time, so a view is found without registering it
and without reflection. A view you register in the service locator still wins over the generated lookup.

| Before | Now |
|---|---|
| `ViewLocator.Current` | `ViewLocator.GetCurrent()` |
| `IViewLocator.ResolveView<T>(string? contract)` and `ResolveView<T>()` | The same, as extension methods on `IViewLocator` |
| `IViewLocator.ResolveView(object? viewModel, string? contract)` | The same. It reads the view model's type while your app runs, so it is not safe to trim; `ResolveView(viewModel, contract)` with a typed view model is |
| `new ViewMappingBuilder(locator)` | `locator.CreateMappingBuilder()` |
| `DefaultViewLocator.Map<TViewModel, TView>()` chained | `Map` returns nothing; chain on `CreateMappingBuilder()` instead |

A custom `IViewLocator` implements two methods:

```csharp
public IViewFor? ResolveView<TViewModel>(TViewModel viewModel, string? contract)
    where TViewModel : class;

[RequiresDynamicCode("Resolves a view from the view model's runtime type.")]
public IViewFor? ResolveView(object? viewModel, string? contract);
```

The view without a contract answers only a request without a contract. A request for a contract that has no view
finds nothing, and the host's `ContractFallbackByPass` decides whether it then asks for the view without a contract.

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
A `readonly` field can sit along a path, but not at the end of a path that a binding writes to.

[Unsafe twins and the runtime fallback](../../binding/unsafe.md) shows every twin with an example.

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

No overload takes `IBindingTypeConverter` objects and a signal together. Write lambdas that call your
converter instead.

The last argument picks the direction the signal drives:

- **`TriggerUpdate.ViewToViewModel`**, the default: the signal replaces the view's own change notifications. Editing
  the view writes to the view model only when the signal fires.
- **`TriggerUpdate.ViewModelToView`**: after the first value from the view model, the signal decides when the view
  model writes to the view. The view's own changes still write to the view model straight away.

A conversion lambda always returns a value, and the binding writes it. ReactiveUI used to skip the write when a
converter refused a value, such as `"fa0"` typed into a number box. To keep that, return the value the other side
already holds. The binding sees an equal value and writes nothing.

## ToProperty and ObservableAsPropertyHelper

`ToProperty` is generated. Name the property with a lambda, `x => x.Total`, or with `nameof(Total)`. Any other
form, such as an indexer or a computed string, gets no generated code, and the analyzer reports RXUIBIND013. That
call throws when it runs. On a `ReactiveObject`, the generated code raises `PropertyChanging` and
`PropertyChanged` through ReactiveUI's own notification state, so `SuppressChangeNotifications` and
`DelayChangeNotifications` work as before.

`ObservableAsPropertyHelper<T>` keeps its members: `Value`, `IsSubscribed`, `ThrownExceptions` and `Default()`.
One thing changed. When the source stream fails and nothing subscribes to `ThrownExceptions`, the helper throws the
error on the thread that produced it. It no longer goes to `RxState.DefaultExceptionHandler`. Subscribe to
`ThrownExceptions` if you relied on the handler.

[Properties backed by observables](../../binding/properties.md) covers `ToProperty` in full.

## Views follow their view model

`this.Bind(ViewModel, ...)`, `OneWayBind`, `BindCommand` and `BindInteraction` accept a nullable view model, so
they compile when your view's `ViewModel` property is nullable. The binding follows `view.ViewModel`:

- A binding made before the view has a view model waits for one.
- Setting a new view model moves the binding to it.
- While a parent on the path is `null`, such as `x => x.Customer!.Name` with no `Customer`, the view keeps its
  value. Nothing is written until the path is whole again.

The view model you pass in decides the types of the binding. The binding itself reads `view.ViewModel`. A view that
implements only the non-generic `IViewFor` binds the view model you pass in. This works the same way for generated
calls and `Unsafe` twins.

## Hand-written IReactiveObject types

The generated code watches `PropertyChanged`. A class that implements `IReactiveObject` itself raises that event
only after it calls `SubscribePropertyChangedEvents`. ReactiveUI's old observation read the `Changed` stream
instead, so it never needed the call. Call it from the event's `add` accessor:

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

A handler receives an `IInteractionContext<TInput, TOutput>` from `ReactiveUI.Binding`. Register your handlers
against that type.

## Behavior that changed

### Every binding writes on the view's thread

Each binding moves its writes onto the thread that owns the view, on every platform. ReactiveUI used to do this
only for WPF two-way `Bind` and for command swaps. A value set on another thread reaches the view one turn of the
message loop later. A test that sets a value and checks the view at once may need to let the UI thread run first.
Set `BindingSchedulers.MainThread` when you want every binding to write on a sequencer you choose. A *sequencer*
is the object that decides which thread runs a piece of work.

A binding to a WPF, WinForms or MAUI object needs that platform's Binding package to move its writes. The
ReactiveUI platform packages bring it. Without it, warning RXUIBIND017 reports the call.

### A burst of changes writes once

A binding writes only the latest value. When a property changes several times before the binding can write, the
values in between are skipped, and the binding's change stream skips them too.

### Observations are delivered one at a time

A property observation delivers each change on the thread that raised it, one change at a time. When two threads
change the property together, the second thread waits up to 20 milliseconds. After that it hands its change to
the thread that is already delivering, and moves on. After-change observations then skip to the latest value.
Before-change observations, such as `WhenChanging`, keep every change in order. Your subscriber runs without a lock
held, so it can take its own locks safely.

### Converters are picked by the declared types

A binding picks its converter from the types of the two properties as they are declared. ReactiveUI's builder
registers the standard converters. The old fallback through `TypeDescriptor` is gone, so register a converter for
any other pair. When nothing is registered and the value already has the target type, it passes through
unchanged. For example, a `string` held in an `object`-typed `SelectedItem` binds to a `string` property.

### Missing change notifications are found at build time

ReactiveUI logged a warning when your app ran into a property with no change notification. The analyzer now
reports it as RXUIBIND010 when you build. The binding reads such a property once, as before. If your project treats
warnings as errors and a test model raises no notification on purpose, make the model raise
`PropertyChanged` rather than turning the warning off.

### Custom observation providers compete by score

A custom `ICreatesObservableForProperty` handles a property only when its score is higher than the generated code's.
The generated code wins a tie. Call `ReactiveUI.Binding.Fallback.ObservationAffinityChecker.Refresh()` after you
change registrations. A custom provider still has to handle trimming for itself.

## Removed types

| Removed | Use instead |
|---|---|
| ReactiveUI's `WhenAny*`, `ObservableForProperty`, `Bind`, `OneWayBind`, `BindTo`, `BindCommand`, `InvokeCommand` and `BindInteraction` extension methods | The same names from `ReactiveUI.Binding`, or their `Unsafe` twins |
| `PropertyBinderImplementation`, `CommandBinderImplementation`, `InteractionBinderImplementation` and their interfaces | The extension methods |
| `Reflection`: `Rewrite`, `ExpressionToPropertyNames`, `GetValueFetcherForProperty`, `GetValueFetcherOrThrow`, `GetValueSetterForProperty`, `GetValueSetterOrThrow`, `TryGetValueForPropertyChain`, `TryGetAllValuesForPropertyChain` and `TrySetValueToPropertyChain` | The same methods on `ReactiveUI.Binding.Expressions.Reflection` |
| `Reflection.ReallyFindType`, `GetEventArgsTypeForEvent`, `ThrowIfMethodsNotOverloaded` and `ViewModelWhenAnyValue` | Nothing. They served ReactiveUI's own engine |
| `ExpressionMixins`, `ReflectionMixins`, `ExpressionRewriter` | `ReactiveUI.Binding.Expressions.ExpressionMixins` |
| `INPCObservableForProperty`, `POCOObservableForProperty`, `IROObservableForProperty` and the providers for KVO, UIKit, AppKit, Android widgets, WinForms, WPF and WinUI | Nothing. The generated code covers them. A custom provider still registers as `ICreatesObservableForProperty` |
| `CreatesCommandBindingViaEvent`, `CreatesCommandBindingViaCommandParameter` and the WinForms, Android, UIKit and AppKit command binders | Nothing. The generated code covers them |
| The Apple `NSDate` converters | Nothing. The generated code converts `NSDate` |
| `ComponentModelFallbackConverter` | A converter you register for the pair |
| ReactiveUI's `IViewLocator`, `DefaultViewLocator`, `ViewLocator`, `ViewMappingBuilder` and `ViewLocatorNotFoundException` | The same names from `ReactiveUI.Binding`. See [View location](#view-location) |

## ReactiveUI.SourceGenerators

ReactiveUI.SourceGenerators no longer writes the two things ReactiveUI.Binding now provides: `[ObservableAsProperty]`
and view registration. `[Reactive]`, `[ReactiveCommand]`, `[IViewFor]` and the other attributes work as before.

| Removed from ReactiveUI.SourceGenerators | Use instead |
|---|---|
| `[ObservableAsProperty]` on a field, a method or an `IObservable<T>` property, and `InitializeOAPH()` | ReactiveUI.Binding's `[ObservableAsProperty]` on a `partial` property, assigned with `ToProperty` |
| The `ReadOnly`, `UseProtected`, `PropertyName` and `InitialValue` options of `[ObservableAsProperty]` | Declare the property with the name and access you want. Pass `initialValue:` to `ToProperty` |
| `RegisterViewsForViewModelsSourceGenerated()`, `SplatRegistrationType`, and the `RegistrationType` and `ViewModelRegistrationType` options of `[IViewFor]` | The generated view lookup. List `IViewFor<T>` on the view's declaration |
| RXUISG0014, RXUISG0017 and the RXUISPR0002 suppression | Nothing. They only applied to `[ObservableAsProperty]` |

RXUISG0012 and RXUISG0013 remain. They now report an attribute that any member forwards to its generated member
with a `[property:]` or `[field:]` target, when the attribute's type or arguments are not valid.

### Move a property to ReactiveUI.Binding's attribute

Declare the property yourself, as `partial` and get-only, and mark it `[ObservableAsProperty]`. The generator writes
the property body and a field named `_{name}Helper`. You assign that field with `ToProperty`, as before. Partial
properties need C# 13 or later.

Before:

```csharp
[ObservableAsProperty]
private string _fullName = string.Empty;

public PersonViewModel()
{
    _fullNameHelper = this.WhenAnyValue(x => x.FirstName, x => x.LastName, (first, last) => $"{first} {last}")
        .ToProperty(this, x => x.FullName);
}
```

After:

```csharp
public PersonViewModel()
{
    _fullNameHelper = this.WhenAnyValue(static x => x.FirstName, static x => x.LastName, static (first, last) => $"{first} {last}")
        .ToProperty(this, static x => x.FullName, initialValue: string.Empty);
}

[ObservableAsProperty]
public partial string FullName { get; }
```

The field initializer becomes the `initialValue:` argument. A method or an `IObservable<T>` property that carried the
attribute becomes the stream you pass to `ToProperty`.

Below C# 13, write the helper yourself: a `readonly ObservableAsPropertyHelper<string> _fullNameHelper` field and
`public string FullName => _fullNameHelper.Value;`. [Properties backed by observables](../../binding/properties.md) shows
both forms.

### Register views without the Splat options

ReactiveUI.Binding's generator adds every class whose declaration implements `IViewFor<T>` to the generated view
lookup. One source generator cannot see the code another one writes. So the lookup cannot see the interface that
`[IViewFor]` adds. List the interface on the class yourself. The members `[IViewFor]` generates still implement it.

Before:

```csharp
[IViewFor<LoginViewModel>(RegistrationType = SplatRegistrationType.PerRequest)]
public partial class LoginView : UserControl
{
}

AppLocator.CurrentMutable.RegisterViewsForViewModelsSourceGenerated();
```

After:

```csharp
[IViewFor<LoginViewModel>]
public partial class LoginView : UserControl, IViewFor<LoginViewModel>
{
}
```

Remove the call to `RegisterViewsForViewModelsSourceGenerated()`. `ViewModelRegistrationType` registered the view
model as well. Register it yourself if you resolved it from the service locator:

```csharp
AppLocator.CurrentMutable.RegisterLazySingleton(static () => new LoginViewModel());
```

A view registered in the service locator still wins over the generated lookup. So you can keep registering a view by
hand, for example one whose constructor takes arguments. [Views](../../binding/views.md#register-a-view-that-needs-arguments)
shows how.

## Build requirements

- **Compiler:** Roslyn 4.8 or newer, which ships with Visual Studio 2022 17.8 and the .NET 8 SDK. An older
  compiler stops the build with RXUIBIND100.
- **Interceptors:** with Roslyn 4.13 or newer and C# 11 or later, the generator replaces each call in place. With an
  older compiler or language version it writes overloads instead, and your code does not change either way.
  [Choose how calls reach generated code](../../binding/setup.md#choose-how-calls-reach-generated-code) explains the
  difference. The generated code compiles as C# 7.3 or later, and each project's generated code lives in a
  namespace of its own.
- **Trimming and NativeAOT:** generated code is safe to trim. The `Unsafe` twins and `WhenAnyDynamic` are not, and a
  `PublishAot` build reports every place that calls them.

## Where to go next

- [ReactiveUI.Binding overview](../../binding/index.md)
- [Bindings](../../binding/bindings.md)
- [Analyzer messages](../../binding/setup.md#read-the-analyzer-messages)
- [Threading](../../binding/threading.md)
