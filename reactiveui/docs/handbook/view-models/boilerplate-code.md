---
NoTitle: true
---
# Source Generators and Fody, the easy way to create properties in ReactiveUI

If you are tired of writing boilerplate code for property change notifications, you can try one of the following: 
- [PropertyChanged.Fody](https://github.com/Fody/PropertyChanged) or 
- [ReactiveUI.Fody](https://www.nuget.org/packages/ReactiveUI.Fody).

These two libraries are both based on [Fody](https://github.com/Fody) - an extensible tool for weaving .NET assemblies, and they'll 
inject `INotifyPropertyChanged` code into decorated properties at compile time for you. 

- [ReactiveUI.SourceGenerators](https://www.nuget.org/packages/ReactiveUI.SourceGenerators/)

This library is a Source Generator that generates properties and commands for you. It is a new way to generate properties and commands for ReactiveUI taking decorated fields and methods and generating the properties and ReactiveCommands for you.

We recommend using [ReactiveUI.SourceGenerators](https://www.nuget.org/packages/ReactiveUI.SourceGenerators/) package that also handles `ObservableAsProperyHelper` properties and `ReactiveCommands`.

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

These Source Generators were designed to work in full with ReactiveUI V19.5.31 and newer supporting all features, currently:
- [Reactive]
- [ObservableAsProperty]
- [ObservableAsProperty(PropertyName = "ReadOnlyPropertyName")]
- [ReactiveCommand]
- [ReactiveCommand(CanExecute = nameof(IObservableBoolName))] with CanExecute
- [ReactiveCommand][property: AttribueToAddToCommand] with Attribute passthrough
- [IViewFor(nameof(ViewModelName))]
- [RoutedControlHost("YourNameSpace.CustomControl")] for WinForms
- [ViewModelControlHost("YourNameSpace.CustomControl")] for WinForms

Versions older than V19.5.31 to this:
- All functions fully supported, except for `[ReactiveCommand]` all supported except Cancellation Token asnyc methods.

The Source Generators are not a direct replacement for [ReactiveUI.Fody](https://www.nuget.org/packages/ReactiveUI.Fody/), but they can be used together.
You can continue to use ReactiveUI.Fody and migrate to ReactiveUI.SourceGenerators at your own pace.

As fody operates at the IL level, it can be used to generate properties that directly replace the code you specified in the Property templates for `[Reactive]` and `[ObservableAsProperty]` properties.
Source Generators add to your code instead of replacing it, so you we use fields and methods to generate the properties and commands.

The `[Reactive]` and `[ObservableAsProperty]` Attributes are applied to fields, and the Source Generator will generate the properties for you.
`[Reactive]` will generate a property with a backing field and the RaiseAndSetIfChanged method. You can provide initialisers for the field.
`[ObservableAsProperty]` will generate a property with a ObservableAsPropertyHelper backing field. Any initialisers will be passed through as a default value.

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
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass : ReactiveObject
{
    [Reactive]
    private string _myProperty;
}
```

## Usage ObservableAsPropertyHelper `[ObservableAsProperty]`

### Usage ObservableAsPropertyHelper with Field
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass : ReactiveObject
{
    [ObservableAsProperty]
    private string _myProperty = "Default Value";

    public MyReactiveClass()
    {
        _myPrpertyHelper = MyPropertyObservable()
            .ToProperty(this, x => x.MyProperty);
    }

    IObservable<string> MyPropertyObservable() => Observable.Return("Test Value");
}
```

### Usage ObservableAsPropertyHelper with Observable Property
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass : ReactiveObject
{    
    public MyReactiveClass()
    { 
        // Initialize generated _myObservablePropertyHelper
        // for the generated MyObservableProperty
        InitializeOAPH();
    }

    [ObservableAsProperty]
    IObservable<string> MyObservable => Observable.Return("Test Value");
}
```

### Usage ObservableAsPropertyHelper with Observable Property and specific PropertyName
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass : ReactiveObject
{    
    public MyReactiveClass()
    { 
        // Initialize generated _testValuePropertyHelper
        // for the generated TestValueProperty
        InitializeOAPH();
    }

    [ObservableAsProperty(PropertyName = TestValueProperty)]
    IObservable<string> MyObservable => Observable.Return("Test Value");
}
```

### Usage ObservableAsPropertyHelper with Observable Method

NOTE: This does not support methods with parameters
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass : ReactiveObject
{    
    public MyReactiveClass()
    { 
        // Initialize generated _myObservablePropertyHelper
        // for the generated MyObservableProperty
        InitializeOAPH();
    }

    [ObservableAsProperty]
    IObservable<string> MyObservable() => Observable.Return("Test Value");
}
```

### Usage ObservableAsPropertyHelper with Observable Method and specific PropertyName
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass : ReactiveObject
{    
    public MyReactiveClass()
    { 
        // Initialize generated _testValuePropertyHelper
        // for the generated TestValueProperty
        InitializeOAPH();
    }

    [ObservableAsProperty(PropertyName = TestValueProperty)]
    IObservable<string> MyObservable() => Observable.Return("Test Value");
}
```

## Usage ReactiveCommand `[ReactiveCommand]`

### Usage ReactiveCommand without parameter
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    public MyReactiveClass()
    {
        InitializeCommands();
    }

    [ReactiveCommand]
    private void Execute() { }
}
```

### Usage ReactiveCommand with parameter
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    public MyReactiveClass()
    {
        InitializeCommands();
    }

    [ReactiveCommand]
    private void Execute(string parameter) { }
}
```

### Usage ReactiveCommand with parameter and return value
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    public MyReactiveClass()
    {
        InitializeCommands();
    }

    [ReactiveCommand]
    private string Execute(string parameter) => parameter;
}
```

### Usage ReactiveCommand with parameter and async return value
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    public MyReactiveClass()
    {
        InitializeCommands();
    }

    [ReactiveCommand]
    private async Task<string> Execute(string parameter) => await Task.FromResult(parameter);
}
```

### Usage ReactiveCommand with IObservable return value
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    public MyReactiveClass()
    {
        InitializeCommands();
    }

    [ReactiveCommand]
    private IObservable<string> Execute(string parameter) => Observable.Return(parameter);
}
```

### Usage ReactiveCommand with CancellationToken
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    public MyReactiveClass()
    {
        InitializeCommands();
    }

    [ReactiveCommand]
    private async Task Execute(CancellationToken token) => await Task.Delay(1000, token);
}
```

### Usage ReactiveCommand with CancellationToken and parameter
```csharp
using ReactiveUI.SourceGenerators;

public partial class MyReactiveClass
{
    public MyReactiveClass()
    {
        InitializeCommands();
    }

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
        InitializeCommands();
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
        InitializeCommands();
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
public partial class MyReactiveControl : UserControl
{
    public MyReactiveControl()
    {
        InitializeComponent();
        MyReactiveClass = new MyReactiveClass();
    }
}
```

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

# Using ReactiveUI.Fody

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
