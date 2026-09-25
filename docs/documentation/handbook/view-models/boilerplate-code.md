---
Order: 2
---
# Source Generators and Fody, the easy way to create properties in ReactiveUI

## Legacy Note
If you are tired of writing boilerplate code for property change notifications, you can try one of the following: 
- [PropertyChanged.Fody](https://github.com/Fody/PropertyChanged) or 
- [ReactiveUI.Fody](https://www.nuget.org/packages/ReactiveUI.Fody).

These two libraries are both based on [Fody](https://github.com/Fody) - an extensible tool for weaving .NET assemblies, and they'll 
inject `INotifyPropertyChanged` code into decorated properties at compile time for you. 

## Recommend for new projects
- [ReactiveUI.SourceGenerators](https://www.nuget.org/packages/ReactiveUI.SourceGenerators/)

This library is a Source Generator that generates properties and commands for you. It is a new way to generate properties and commands for ReactiveUI taking decorated fields and methods and generating the properties and ReactiveCommands for you.

We recommend using [ReactiveUI.SourceGenerators](https://www.nuget.org/packages/ReactiveUI.SourceGenerators/) package that also handles `ReactiveCommands`.
For `ObservableAsPropertyHelper<T>` properties, use the `[ObservableAsProperty]` attribute from [ReactiveUI.Binding](../../binding/properties.md#declare-the-property-with-an-attribute).

# The manual way to create properties in ReactiveUI

## Read-write properties
Typically properties are declared like this:

```cs
private string _name;
public string Name 
{
    get => _name;
    set => this.RaiseAndSetIfChanged(ref _name, value);
}
```

## ObservableAsPropertyHelper properties

Similarly, to declare output properties, the code looks like this:

```cs
ObservableAsPropertyHelper<string> _firstName;
public string FirstName => _firstName.Value;
```

Then the helper is initialized with a call to `ToProperty`:

```cs
// firstNameObservable is IObservable<string>
_firstName = firstNameObservable
  .ToProperty(this, x => x.FirstName);
```

# Using ReactiveUI.SourceGenerators

With [ReactiveUI.SourceGenerators](https://www.nuget.org/packages/ReactiveUI.SourceGenerators/).

- **Minimum Requirements**:
  - **C# Version**: 12.0
  - **Visual Studio Version**: 17.8.0
  - **ReactiveUI Version**: 19.5.31+

These Source Generators were designed to work in full with ReactiveUI V19.5.31 and newer supporting all features, currently:
- `[Reactive]` With field and access modifiers, partial property support (C# 13 Visual Studio Version 17.12.0)
- `[ReactiveCommand]`
- `[ReactiveCommand(CanExecute = nameof(IObservableBoolName))]` with CanExecute
- `[ReactiveCommand(OutputScheduler = "RxSchedulers.MainThreadScheduler")]` using a ReactiveUI Scheduler
- `[ReactiveCommand(OutputScheduler = nameof(_isheduler))]` using a Scheduler defined in the class
- `[ReactiveCommand][property: AttributeToAddToCommand]` with Attribute passthrough
- `[IViewFor(nameof(ViewModelName))]`
- `[RoutedControlHost("YourNameSpace.CustomControl")]`
- `[ViewModelControlHost("YourNameSpace.CustomControl")]`

Versions older than V19.5.31 to this:
- All functions fully supported, except for `[ReactiveCommand]` all supported except Cancellation Token asnyc methods.

The Source Generators are not a direct replacement for [ReactiveUI.Fody](https://www.nuget.org/packages/ReactiveUI.Fody/), but they can be used together.
You can continue to use ReactiveUI.Fody and migrate to ReactiveUI.SourceGenerators at your own pace.

As fody operates at the IL level, it can be used to generate properties that directly replace the code you specified in the Property templates for `[Reactive]` and `[ObservableAsProperty]` properties.
Source Generators add to your code instead of replacing it, so you we use fields and methods to generate the properties and commands.

The `[Reactive]` Attribute is applied to fields, and the Source Generator will generate the properties for you.
`[Reactive]` will generate a property with a backing field and the RaiseAndSetIfChanged method. You can provide initializers for the field.

The `[ReactiveCommand]` Attribute is applied to methods, and the Source Generator will generate a ReactiveCommand property for you.
The method can be one of the following
- a void method, 
- a method with a return value, 
- a method with a return value and a parameter,  
- a method with a return value of Task.
- a method with a return value of Task and a parameter,
- a method with a return value of Task and a CancellationToken, 
- a method with a return value of Task and a parameter and a CancellationToken,
- a method with a return value of Task of T.
- a method with a return value of Task of T and a parameter,
- a method with a return value of Task of T and a CancellationToken, 
- a method with a return value of Task of T and a parameter and a CancellationToken,
- a method with a return value of IObservable,
- a method with a return value of IObservable and a parameter.


## Usage Reactive property `[Reactive]`

### Usage Reactive property with field
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass : ReactiveObject
{
    [Reactive]
    private string _myProperty;
}
```

### Usage Reactive property with set Access Modifier
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass : ReactiveObject
{
    [Reactive(SetModifier = AccessModifier.Protected)]
    private string _myProperty;
}
```

### Usage Reactive property with property Attribute pass through
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass : ReactiveObject
{
    [Reactive]
    [property: JsonIgnore]
    private string _myProperty;
}
```

### Usage Reactive property with initial value
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass : ReactiveObject
{
    [Reactive]
    private string _myProperty = "Default Value";
}
```

### Usage Reactive property from partial property

Partial properties are supported in C# 13 and Visual Studio 17.12.0 and later.
Both the getter and setter must be empty, and the `[Reactive]` attribute must be placed on the property.
Override and Virtual properties are supported.
Set Access Modifier is also supported on partial properties.

```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass : ReactiveObject
{
    [Reactive]
    public partial string MyProperty { get; set; }
}
```


## Read-only properties with `[ObservableAsProperty]`

ReactiveUI.SourceGenerators does not generate `[ObservableAsProperty]`. ReactiveUI.Binding provides it, and the
ReactiveUI package brings ReactiveUI.Binding with it.

Declare the property as `partial` and get-only, and mark it `[ObservableAsProperty]`. The generator writes the property
body and a field named `_{name}Helper`. Assign that field in the constructor with `ToProperty`. Partial properties need
C# 13 or later.

```csharp
using ReactiveUI;
using ReactiveUI.Primitives.Signals;

public partial class MyReactiveClass : ReactiveObject
{
    public MyReactiveClass()
    {
        _myPropertyHelper = MyPropertyObservable()
            .ToProperty(this, static x => x.MyProperty, initialValue: "Default Value");
    }

    [ObservableAsProperty]
    public partial string MyProperty { get; }

    private static IObservable<string> MyPropertyObservable() => Signal.Emit("Test Value");
}
```

`MyProperty` returns `"Default Value"` until the stream produces its first value. Below C# 13, write the
`ObservableAsPropertyHelper<T>` field and the property yourself, as in
[The manual way to create properties](#the-manual-way-to-create-properties-in-reactiveui).
[Properties backed by observables](../../binding/properties.md) covers both forms. To move code that used the
earlier ReactiveUI.SourceGenerators attribute, see the
[ReactiveUI.Binding migration guide](../../upgrading/reactiveui-binding-migration.md#reactiveuisourcegenerators).

## Usage ReactiveCommand `[ReactiveCommand]`

Note: `InitializeCommands();` has been removed from the latest version of the Source Generators. This is now handled by the Source Generator.

### Usage ReactiveCommand without parameter
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    [ReactiveCommand]
    private void Execute() { }
}
```

### Usage ReactiveCommand with parameter
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    [ReactiveCommand]
    private void Execute(string parameter) { }
}
```

### Usage ReactiveCommand with parameter and return value
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    [ReactiveCommand]
    private string Execute(string parameter) => parameter;
}
```

### Usage ReactiveCommand with parameter and async return value
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    [ReactiveCommand]
    private async Task<string> Execute(string parameter) => await Task.FromResult(parameter);
}
```

### Usage ReactiveCommand with IObservable return value
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    [ReactiveCommand]
    private IObservable<string> Execute(string parameter) => Signal.Emit(parameter);
}
```

### Usage ReactiveCommand with CancellationToken
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    [ReactiveCommand]
    private async Task Execute(CancellationToken token) => await Task.Delay(1000, token);
}
```

### Usage ReactiveCommand with CancellationToken and parameter
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    [ReactiveCommand]
    private async Task<string> Execute(string parameter, CancellationToken token)
    {
        await Task.Delay(1000, token);
        return parameter;
    }
}
```

### Usage ReactiveCommand with CanExecute
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    private IObservable<bool> _canExecute;

    [Reactive]
    private string _myProperty1;

    [Reactive]
    private string _myProperty2;

    public MyReactiveClass()
    {
        _canExecute = this.WhenAnyValue(x => x.MyProperty1, x => x.MyProperty2, (x, y) => !string.IsNullOrEmpty(x) && !string.IsNullOrEmpty(y));
    }

    [ReactiveCommand(CanExecute = nameof(_canExecute))]
    private void Search() { }
}
```

### Usage ReactiveCommand with property Attribute pass through
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    private IObservable<bool> _canExecute;

    [Reactive]
    private string _myProperty1;

    [Reactive]
    private string _myProperty2;

    public MyReactiveClass()
    {
        _canExecute = this.WhenAnyValue(x => x.MyProperty1, x => x.MyProperty2, (x, y) => !string.IsNullOrEmpty(x) && !string.IsNullOrEmpty(y));
    }

    [ReactiveCommand(CanExecute = nameof(_canExecute))]
    [property: JsonIgnore]
    private void Search() { }
}
```

## Usage IViewFor `[IViewFor(nameof(ViewModelName))]`

### IViewFor usage

IVIewFor is used to link a View to a ViewModel, this is used to link the ViewModel to the View in a way that ReactiveUI can use it to bind the ViewModel to the View.
The ViewModel is passed as a string to the IViewFor Attribute.
The class must inherit from a UI Control from any of the following platforms and namespaces:
- Maui (Microsoft.Maui)
- WinUI (Microsoft.UI.Xaml)
- WPF (System.Windows or System.Windows.Controls)
- WinForms (System.Windows.Forms)
- Avalonia (Avalonia)
- Uno (Windows.UI.Xaml).

```csharp
using ReactiveUI.SourceGenerators;

[IViewFor(nameof(MyReactiveClass))]
public partial class MyReactiveControl : UserControl, IViewFor<MyReactiveClass>
{
    public MyReactiveControl()
    {
        InitializeComponent();
        ViewModel = new MyReactiveClass();
    }
}
```

`[IViewFor]` generates the `ViewModel` property and the `IViewFor<T>` implementation. It does not register the view.
ReactiveUI.Binding's generator adds each class that declares `IViewFor<T>` to the [generated view lookup](../../binding/views.md#the-generated-view-dispatch).
That generator cannot see the interface `[IViewFor]` adds, so list `IViewFor<T>` on the class yourself, as above.

## Platform specific Attributes

### WinForms

#### RoutedControlHost
```csharp
using ReactiveUI.SourceGenerators.WinForms;

[RoutedControlHost("YourNameSpace.CustomControl")]
public partial class MyCustomRoutedControlHost;
```

#### ViewModelControlHost
```csharp
using ReactiveUI.SourceGenerators.WinForms;

[ViewModelControlHost("YourNameSpace.CustomControl")]
public partial class MyCustomViewModelControlHost;
```

# Using ReactiveUI.Fody - Legacy

### Strongly discouraged for new projects

With [ReactiveUI.Fody](https://www.nuget.org/packages/ReactiveUI.Fody/), you don't have to write boilerplate code for getters and setters of read-write properties — the package will do it automagically for you at compile time.
All you have to do is annotate the property with the `[Reactive]` attribute, as shown below.

## ReactiveUI.Fody - Read-write properties

```cs
[Reactive]
public string Name { get; set; }
```

> **Note** `ReactiveUI.Fody` currently doesn't support inline auto property initializers in generic types. It works fine with non-generic types. But if you are working on a generic type, don't attempt to write code like `public string Name { get; set; } = "Name";`, this won't work as you might expect and will likely throw a very weird exception. To workaround this limitation, move your property initialization code to the constructor of your view model class. We know about this limitation and [have a tracking issue for this](https://github.com/reactiveui/ReactiveUI/issues/2416).

## ReactiveUI.Fody - ObservableAsPropertyHelper properties

With ReactiveUI.Fody, you can simply declare a read-only property using the `[ObservableAsProperty]` attribute, using either option of the two options shown below. One option is to annotate the getter of the property:

```cs
public string FirstName { [ObservableAsProperty] get; }
```

Another option is to annotate the property as a whole:

```cs
[ObservableAsProperty]
public string FirstName { get; }
```
    
The field will be generated and the property implemented at compile time. Because there is no field for you to pass to `.ToProperty`, you should use the `.ToPropertyEx` extension method provided by this library:

```cs
// firstNameObservable is IObservable<string>
firstNameObservable.ToPropertyEx(this, x => x.FirstName);
```

This extension will assign the auto-generated field for you rather than relying on the `out` parameter.

> **Note** The generated getter for property of type `T` annotated with the `[ObservableAsProperty]` attribute will return `default(T)` in case if the property isn't yet initialized via a call to `ToPropertyEx`. To be more specific, the generated getter code looks somewhat like `T PropertyName => oaph?.Value ?? default(T);`, where `oaph` is a field of type `ObservableAsProperty<T>` which is generated by the compiler.
