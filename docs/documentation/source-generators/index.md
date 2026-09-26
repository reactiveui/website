---
Order: 4
---
# ReactiveUI.SourceGenerators

A view shows data that changes, so it needs to know when a value changes. Written by hand, each view model property
needs a backing field and a setter that raises a **change notification**, the `PropertyChanged` event a bound view
listens for. Each command needs a property that wraps a method. That code is the same every time.

[ReactiveUI.SourceGenerators](https://github.com/reactiveui/ReactiveUI.SourceGenerators) writes it for you. A
**source generator** is a compiler add-on that writes C# code while your project builds. You mark a property or a
method with an attribute, and the generator writes the rest of the member.

ReactiveUI 24.4 and later bring ReactiveUI.SourceGenerators 4.0.0 with them. There is nothing extra to install.

## Write your first view model

**1. Install the ReactiveUI package for your UI framework.** A class library that holds only view models installs
`ReactiveUI`. The [installation guide](../reactiveui/getting-started/installation/index.md) lists the package for each
platform.

```bash
dotnet add package ReactiveUI
```

**2. Declare the view model as a `partial` class and mark its members.** Add `using ReactiveUI.SourceGenerators;` to
each file that uses the attributes. Declare each property as `partial` with an empty `get` and `set`.

```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public partial class LoginViewModel : ReactiveObject
{
    private readonly IObservable<bool> _canLogIn;

    public LoginViewModel()
    {
        _canLogIn = this.WhenAnyValue(
            static x => x.UserName,
            static x => x.Password,
            static (userName, password) => userName.Length > 0 && password.Length > 0);

        _isValidHelper = _canLogIn.ToProperty(this, static x => x.IsValid);
    }

    [Reactive]
    public partial string UserName { get; set; } = string.Empty;

    [Reactive]
    public partial string Password { get; set; } = string.Empty;

    [ObservableAsProperty]
    public partial bool IsValid { get; }

    [ReactiveCommand(CanExecute = nameof(_canLogIn))]
    private async Task<bool> LogInAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        return Password == "secret";
    }
}
```

`WhenAnyValue` returns a **stream**, an `IObservable<T>` that emits a new value each time either property changes.
[Observing](../binding/observing.md) covers it.

**3. Build.** The generators write the rest of the class:

- The bodies of `UserName` and `Password`. Setting either one raises the change notification, but only when the value
  changes.
- The body of `IsValid` and its `_isValidHelper` field. `ToProperty` keeps `IsValid` equal to the latest value of
  `_canLogIn`. `[ObservableAsProperty]` comes from [ReactiveUI.Binding](../binding/properties.md), which ReactiveUI
  also brings.
- A `LogInCommand` property. It holds a **command**, an `ICommand` that also reports each result as a stream. The
  command runs `LogInAsync`, and it is enabled only while `_canLogIn` emits `true`. The generator drops the `Async`
  suffix from the name.

**4. Use the generated members.** They are ordinary properties, so your code and your tests read and set them
directly.

```csharp
var login = new LoginViewModel();
Console.WriteLine(login.IsValid);
login.UserName = "ada";
login.Password = "secret";
Console.WriteLine(login.IsValid);
```

```text
False
True
```

Bind the properties and the command to your view with `Bind` and `BindCommand`.
[Data binding](../reactiveui/handbook/data-binding/index.md) shows how.

## What comes with ReactiveUI

`ReactiveUI.Core` references ReactiveUI.SourceGenerators and passes its generators and analyzers on. `ReactiveUI`,
`ReactiveUI.Reactive` and every platform package build on `ReactiveUI.Core`, so any project that references one of
them gets:

- **Five attributes:** `[Reactive]`, `[ReactiveCommand]`, `[ReactiveCollection]`, `[BindableDerivedList]` and
  `[IReactiveObject]`. They live in the `ReactiveUI.SourceGenerators` namespace. ReactiveUI adds no global `using` for
  it, so add `using ReactiveUI.SourceGenerators;` to each file that uses them.
- **Code for the package you reference.** A `ReactiveUI` project gets `ReactiveUI.ReactiveCommand<RxVoid, T>`. A
  `ReactiveUI.Reactive` project gets `ReactiveUI.Reactive.ReactiveCommand<Unit, T>`, with the `Unit` type from
  System.Reactive. The attributes and the code you write are the same for both.
- **Analyzers.** An **analyzer** checks your code as you type and reports problems as warnings or errors.
  [Analyzer messages](#analyzer-messages) lists them.

`[ObservableAsProperty]` and view registration come from [ReactiveUI.Binding](../binding/index.md), which ReactiveUI
also brings. [Properties backed by observables](../binding/properties.md) covers `[ObservableAsProperty]`, and
[Views](../binding/views.md) covers how views are registered.

### Remove your own package reference

A project that references a ReactiveUI.SourceGenerators version older than 4.0.0 fails to restore with error NU1605,
because ReactiveUI needs 4.0.0. Remove that `PackageReference`, or set it to 4.0.0 or later.

```xml
<!-- Remove this line, or set Version to 4.0.0 or later -->
<PackageReference Include="ReactiveUI.SourceGenerators" Version="3.2.0" PrivateAssets="all" />
```

To move code written against the 3.x `[ObservableAsProperty]` or `[IViewFor]` attributes, follow the
[ReactiveUI.Binding migration guide](../reactiveui/upgrading/reactiveui-binding-migration.md#reactiveuisourcegenerators).

### Requirements

| Feature | Needs |
|---|---|
| The attributes on fields and methods | C# 12, and Visual Studio 2022 17.8 or the .NET 8 SDK |
| `[Reactive]` on a partial property | C# 13, and Visual Studio 2022 17.14 or the .NET 9 SDK 9.0.300 |
| A partial property with an initial value | C# 14, the default for .NET 10, and Visual Studio 2026 or the .NET 10 SDK |

C# 14 is the default language version when a project targets .NET 10. A project that targets .NET Framework sets
`<LangVersion>` to 12 or later and adds a polyfill package such as
[PolySharp](https://github.com/Sergio0694/PolySharp) or [Polyfill](https://github.com/SimonCropp/Polyfill).

## Properties with `[Reactive]`

`[Reactive]` writes a read-write property that raises a change notification when its value changes. The class must
derive from `ReactiveObject`, or carry [`[IReactiveObject]`](#classes-that-cannot-derive-from-reactiveobject).

### On a partial property

Mark a `partial` property with an empty `get` and `set`. Write the access modifiers you want on the declaration, such
as `protected set`, `virtual` or `required`. Attributes on the property stay on it.

```csharp
using System.Text.Json.Serialization;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public partial class ProfileViewModel : ReactiveObject
{
    [Reactive]
    public partial string DisplayName { get; set; } = "Guest";

    [Reactive]
    public partial int Visits { get; protected set; }

    [Reactive]
    [JsonPropertyName("email_address")]
    public partial string Email { get; set; } = string.Empty;
}
```

`DisplayName` starts as `"Guest"`. An initial value on a partial property needs C# 14.

Use the partial property form for every property you pass to `WhenAnyValue`. The next section explains why.

### On a field

`[Reactive]` also works on a private field. The generator names the property after the field: it drops a leading `_`
or `m_` and capitalizes the first letter, so `_theme` becomes `Theme`. The field initializer sets the starting value.

```csharp
using System.Text.Json.Serialization;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public partial class SettingsViewModel : ReactiveObject
{
    [Reactive]
    private string _theme = "Light";

    [Reactive(SetModifier = AccessModifier.Protected)]
    private int _version;

    [Reactive]
    [property: JsonPropertyName("font_size")]
    private int _fontSize = 12;
}
```

A field has no place for modifiers, so the attribute takes them. `SetModifier` sets the setter's access. A
`[property: ...]` attribute list moves an attribute from the field to the generated property.

> [!WARNING]
> `WhenAnyValue` throws at run time on a property generated from a `[Reactive]` field. ReactiveUI.Binding writes the
> code behind each `WhenAnyValue` call, and its generator cannot see a property another generator writes from a field.
> Declare any property you observe as a `[Reactive]` partial property instead. This is a known ReactiveUI.Binding gap,
> reported upstream.

### Options

| Option | What it does | On a partial property |
|---|---|---|
| `SetModifier = AccessModifier.Protected` | Sets the access of the generated setter. `AccessModifier` also has `Internal`, `Private`, `InternalProtected`, `PrivateProtected` and `Init`. | Write the access on the `set` instead. |
| `Inheritance = InheritanceModifier.Virtual` | Makes the property `virtual`. `InheritanceModifier` also has `Override` and `New`. | Write the modifier on the declaration instead. |
| `UseRequired = true` | Makes the property `required`. | Write `required` on the declaration instead. |
| `[Reactive(nameof(Other))]` | Also raises a change notification for each property you name. | Works the same. |

The last option suits a read-only property that you compute from others. Here, setting `FirstName` or `LastName` also
tells the view that `FullName` changed:

```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public partial class PersonViewModel : ReactiveObject
{
    [Reactive(nameof(FullName))]
    public partial string FirstName { get; set; } = string.Empty;

    [Reactive(nameof(FullName))]
    public partial string LastName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";
}
```

## Commands with `[ReactiveCommand]`

`[ReactiveCommand]` on a method writes a property that holds a `ReactiveCommand` for it. The property's name is the
method's name followed by `Command`. The generator drops a leading `_` or `m_`, and the `Async` suffix of a method that
returns a `Task`. The generator creates the command the first time you read the property, then keeps it.

```csharp
using ReactiveUI;
using ReactiveUI.Primitives.Signals;
using ReactiveUI.SourceGenerators;

public partial class DocumentViewModel : ReactiveObject
{
    [ReactiveCommand]
    private void Save() => Console.WriteLine("Saved");

    [ReactiveCommand]
    private static string Greet(string name) => $"Hello, {name}";

    [ReactiveCommand]
    private static async Task<int> LoadAsync(int id, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);
        return id;
    }

    [ReactiveCommand]
    private static IObservable<string> Status() => Signal.Emit("Ready");
}
```

This class gets `SaveCommand`, `GreetCommand`, `LoadCommand` and `StatusCommand`. A method that uses no instance data
can be `static`.

The method's signature decides the command's type. `TParam` is the parameter's type and `TResult` is what the method
returns.

| Method | Generated property type |
|---|---|
| `void M()` | `ReactiveCommand<RxVoid, RxVoid>` |
| `void M(TParam)` | `ReactiveCommand<TParam, RxVoid>` |
| `TResult M()` or `TResult M(TParam)` | `ReactiveCommand<RxVoid, TResult>` or `ReactiveCommand<TParam, TResult>` |
| `Task M()` or `Task M(TParam)` | `ReactiveCommand<RxVoid, RxVoid>` or `ReactiveCommand<TParam, RxVoid>` |
| `Task<TResult> M()` or `Task<TResult> M(TParam)` | `ReactiveCommand<RxVoid, TResult>` or `ReactiveCommand<TParam, TResult>` |
| `IObservable<TResult> M()` or `IObservable<TResult> M(TParam)` | `ReactiveCommand<RxVoid, TResult>` or `ReactiveCommand<TParam, TResult>` |

`RxVoid` is a type that carries no data. It stands in for "no parameter" and "no result". A `Task` method can also take
a `CancellationToken` as its last parameter. The command cancels that token when its execution is canceled.
[Canceling](../reactiveui/handbook/commands/canceling.md) covers how. Give the method at most one parameter besides
the `CancellationToken`. The generator writes no command for a method with more, and reports nothing.

Return a `Task` from an asynchronous method, never `async void`. The command waits for the `Task` and reports its
errors. It cannot see an `async void` method finish or fail.

### Enable a command with `CanExecute`

`CanExecute` names a member that says when the command can run. It must be an `IObservable<bool>` field, property or
method with no parameters. The command is enabled while that stream's latest value is `true`.

```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public partial class SearchViewModel : ReactiveObject
{
    private readonly IObservable<bool> _canSearch;

    public SearchViewModel() =>
        _canSearch = this.WhenAnyValue(
            static x => x.Query,
            static x => x.IsBusy,
            static (query, isBusy) => query.Length > 2 && !isBusy);

    [Reactive]
    public partial string Query { get; set; } = string.Empty;

    [Reactive]
    public partial bool IsBusy { get; set; }

    [ReactiveCommand(CanExecute = nameof(_canSearch))]
    private void Search() => Console.WriteLine($"Searching for {Query}");
}
```

Assign the `CanExecute` member in the constructor. The command reads it when it is created, the first time something
reads `SearchCommand`.

### Run a command in the background and pick its scheduler

A **scheduler** decides which thread runs a piece of work. In a `ReactiveUI` project it is an `ISequencer`, from
[ReactiveUI.Primitives](../primitives/index.md). By default a command runs a synchronous method on the thread that
calls `Execute`. It delivers results on `RxSchedulers.MainThreadScheduler`, so a bound view can use them directly.

```csharp
using System.Text.Json.Serialization;
using ReactiveUI;
using ReactiveUI.Primitives.Concurrency;
using ReactiveUI.SourceGenerators;

public partial class ReportViewModel : ReactiveObject
{
    private readonly ISequencer _resultSequencer;

    public ReportViewModel(ISequencer resultSequencer) => _resultSequencer = resultSequencer;

    [ReactiveCommand(RunInBackground = true)]
    private static int Square(int value) => value * value;

    [ReactiveCommand(BackgroundScheduler = "global::ReactiveUI.RxSchedulers.TaskpoolScheduler")]
    private static int Cube(int value) => value * value * value;

    [ReactiveCommand(RunInBackground = true)]
    private static async Task<string> ExportAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);
        return "report.csv";
    }

    [ReactiveCommand(OutputScheduler = nameof(_resultSequencer))]
    private static int Count() => 42;

    [ReactiveCommand(AccessModifier = PropertyAccessModifier.Internal)]
    private static void Reset() => Console.WriteLine("Reset");

    [ReactiveCommand]
    [property: JsonIgnore]
    private static void Refresh() => Console.WriteLine("Refreshed");
}
```

| Option | What it does |
|---|---|
| `RunInBackground = true` | Runs a synchronous method on ReactiveUI's background scheduler. Starts a `Task` method with `Task.Run`, passing the parameter and the `CancellationToken` through. A method that returns `IObservable<T>` is unaffected. |
| `BackgroundScheduler = "..."` | Picks the scheduler a synchronous method runs on. It turns on `RunInBackground`. A `Task` method always starts on the thread pool. |
| `OutputScheduler = "..."` | Picks the scheduler the command delivers its results on. |
| `AccessModifier = PropertyAccessModifier.Internal` | Sets the access of the generated property. It is `public` by default. |
| `[property: ...]` | Moves an attribute from the method to the generated property, such as `[property: JsonIgnore]`. |

`BackgroundScheduler` and `OutputScheduler` take one of two things:

- **The name of a scheduler member of the class**, a field, property or method with no parameters. Use `nameof`, as
  `Count` does above. A test can then pass in a scheduler it controls.
- **A built-in scheduler, written in full with `global::`**: `"global::ReactiveUI.RxSchedulers.MainThreadScheduler"`
  or `"global::ReactiveUI.RxSchedulers.TaskpoolScheduler"`. In a `ReactiveUI.Reactive` project, write
  `ReactiveUI.Reactive.RxSchedulers` instead of `ReactiveUI.RxSchedulers`.

The generator ignores any other text, and reports nothing. Check the generated code if a command does not use the
scheduler you named.

Keep slow or blocking work off the UI thread with `RunInBackground`.
[Scheduling](../reactiveui/handbook/scheduling.md) explains how ReactiveUI's schedulers work.

## Collections

### `[ReactiveCollection]`

`[ReactiveCollection]` on an `ObservableCollection<T>` field writes a property that raises a change notification when
you set it, and again each time the collection adds, removes or replaces an item. Set the collection through the
property, not the field. The property starts watching only the collection you assign through it.

```csharp
using System.Collections.ObjectModel;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public partial class TagsViewModel : ReactiveObject
{
    [ReactiveCollection]
    private ObservableCollection<string>? _tags;

    public TagsViewModel() => Tags = [];
}
```

After this, `Tags!.Add("maths")` raises a change notification for `Tags`.

### `[BindableDerivedList]`

`[BindableDerivedList]` on a `ReadOnlyObservableCollection<T>` field writes a read-only property that returns the
field. Use it for a list that other code fills, such as a filtered view of a larger collection. `AccessModifier` sets
the property's access.

```csharp
using System.Collections.ObjectModel;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public partial class InboxViewModel : ReactiveObject
{
    [BindableDerivedList]
    private readonly ReadOnlyObservableCollection<string> _unread;

    public InboxViewModel(ObservableCollection<string> unread) => _unread = new(unread);
}
```

The class gets an `Unread` property. The field must be a `ReadOnlyObservableCollection<T>`, or the generator reports
RXUISG0019.

## Classes that cannot derive from `ReactiveObject`

A class that already has a base class cannot also derive from `ReactiveObject`. Mark it `[IReactiveObject]`, and the
generator writes the `IReactiveObject` implementation into it. `[Reactive]` then works on its properties as usual.

```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public abstract class ModelBase
{
    public DateTimeOffset Created { get; } = DateTimeOffset.Now;
}

[IReactiveObject]
public partial class ContactViewModel : ModelBase
{
    [Reactive]
    public partial string Email { get; set; } = string.Empty;
}
```

## Other source generators in the same project

Each source generator sees only the code you wrote, not the code other generators write. The `System.Text.Json`
source generator cannot see a property that `[Reactive]` writes. To serialize a view model with a
`JsonSerializerContext`, declare the view model in one project and the context in a second project that references
it.

## Windows Forms control hosts

ReactiveUI.SourceGenerators also ships `[RoutedControlHost]` and `[ViewModelControlHost]` for Windows Forms, in the
`ReactiveUI.SourceGenerators.WinForms` namespace. They need ReactiveUI.SourceGenerators 4.0.1 or later, which
ReactiveUI brings.

## Analyzer messages

| ID | Severity | When you see it |
|---|---|---|
| RXUISG0009 | Error | The property a `[Reactive]` field would generate has the same name as the field. |
| RXUISG0010 | Error | A `[property: ...]` attribute on a `[Reactive]` field names a type the compiler cannot find. |
| RXUISG0011 | Error | A `[property: ...]` attribute on a `[Reactive]` field has an argument that is not valid. |
| RXUISG0012 | Error | An attribute any generator moves to a generated member names a type the compiler cannot find. |
| RXUISG0013 | Error | An attribute any generator moves to a generated member has an argument that is not valid. |
| RXUISG0015 | Error | A `[Reactive]` field's name or type would clash with other generated members. |
| RXUISG0016 | Info | A public auto-property in a ReactiveUI class can become a `[Reactive]` member. A code fix turns it into a `[Reactive]` field. Make it a partial property instead if you pass it to `WhenAnyValue`. |
| RXUISG0018 | Error | A `[Reactive]` member sits in a class that neither derives from `ReactiveObject` nor carries `[IReactiveObject]`. |
| RXUISG0019 | Error | A `[BindableDerivedList]` field is not a `ReadOnlyObservableCollection<T>`. |
| RXUISG0020 | Warning | A `[Reactive]` property, or the class that holds it, is not `partial`. A code fix makes both `partial`. |

Most of these errors name a missing `using` or a typo. Fix the attribute and build again.

## At a glance

| Attribute or option | What it does |
|---|---|
| `[Reactive]` | Writes a read-write property that raises a change notification, from a partial property or a field. |
| `[Reactive(nameof(Other))]` | Also raises a change notification for each named property. |
| `SetModifier`, `Inheritance`, `UseRequired` | Set the setter's access, the inheritance modifier, and `required` on a property generated from a field. |
| `[ReactiveCommand]` | Writes a `ReactiveCommand` property that runs the method. |
| `CanExecute` | Names an `IObservable<bool>` that enables and disables the command. |
| `RunInBackground`, `BackgroundScheduler` | Run the command's method off the calling thread. |
| `OutputScheduler` | Picks the scheduler the command delivers its results on. |
| `AccessModifier` | Sets the access of a generated command or list property. |
| `[property: ...]` | Moves an attribute from a field or method to the generated property. |
| `[ReactiveCollection]` | Writes a property that raises a change notification when an `ObservableCollection<T>` changes. |
| `[BindableDerivedList]` | Writes a read-only property for a `ReadOnlyObservableCollection<T>` field. |
| `[IReactiveObject]` | Writes the `IReactiveObject` implementation for a class that cannot derive from `ReactiveObject`. |
