---
NoTitle: true
IsBlog: true
Title: ReactiveUI Source Generators 
Tags: Article
Author: Chris Pulman
Published: 2025-01-28
---

# Leveraging `ReactiveUI.SourceGenerators` with C# 12 and Visual Studio 17.8.0

Author: Chris Pulman  
Published: January 28, 2025  

---

With the release of C# 12 and Visual Studio 17.8.0, writing reactive applications has become more efficient thanks to `ReactiveUI.SourceGenerators`. This library streamlines the development process by reducing boilerplate code when working with properties, commands, and reactive bindings in ReactiveUI. By using `[Reactive]`, `[ObservableAsProperty]`, and `[ReactiveCommand]` attributes, you can write clean and maintainable ViewModel code.

In this article, we'll dive into the details of these attributes, showcase examples, and discuss why `ReactiveUI.SourceGenerators` is a significant improvement over previous approaches like `ReactiveUI.Fody`.

---

## Introduction to `ReactiveUI.SourceGenerators`

`ReactiveUI.SourceGenerators` simplifies the development of reactive applications by generating common code patterns at compile-time. This removes the need to manually implement features such as property change notifications or reactive command wiring.

While previous solutions like `ReactiveUI.Fody` used IL weaving to achieve similar goals, source generators offer a more modern and robust approach. Here’s why:

### Why `ReactiveUI.SourceGenerators` Is Better Than `ReactiveUI.Fody`:
1. **Compile-Time Safety**: 
   - Source generators operate during the compilation process, allowing you to see generated code directly. This provides better transparency and immediate feedback when things go wrong.
   - With Fody, the weaving occurs post-compilation, making debugging and troubleshooting more complex.
   
2. **No External Tooling**: 
   - Source generators are natively supported by the C# compiler. Fody requires additional tooling and configuration to function, which can complicate build pipelines.
   
3. **IDE Integration**: 
   - Generated code from source generators is accessible in modern IDEs like Visual Studio 17.8.0. You can inspect the generated code, improving both understanding and maintainability.
   - Fody-generated code is invisible, leading to a "black-box" experience.
   
4. **Future-Proof**: 
   - Source generators are the recommended approach for modern C# development and will continue to receive support and improvements. Fody, while useful, is considered more of a legacy solution for projects requiring IL weaving.

---

## Prerequisites

Before exploring the examples, ensure your environment meets the following requirements:

1. **Visual Studio 17.8.0** or later.
2. **.NET 7** or higher.
3. Install the required NuGet packages:
   - `ReactiveUI`
   - `ReactiveUI.SourceGenerators`

Install these packages using the NuGet Package Manager or the command line:

```bash
dotnet add package ReactiveUI
dotnet add package ReactiveUI.SourceGenerators
```

## Using the `[Reactive]` Attribute
The `[Reactive]` attribute enables automatic property change notifications for private fields in your ReactiveObject-derived classes.
It eliminates the need to manually define backing fields and call RaisePropertyChanged.

### Example
```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public partial class MainViewModel : ReactiveObject
{
    [Reactive]
    private string _firstName;

    [Reactive]
    private string _lastName;

    public string FullName => $"{FirstName} {LastName}";
}
```

### Explanation

The `[Reactive]` attribute generates properties for the annotated private fields and wires them to notify changes via `INotifyPropertyChanged`.
In the example above, `_firstName` and `_lastName` are private fields, but they behave like reactive properties, notifying bindings or subscribers when their values change.

## Using the `[ObservableAsProperty]` Attribute

The `[ObservableAsProperty]` attribute simplifies the creation of read-only properties backed by observables. 
This attribute eliminates the need to manually use ObservableAsPropertyHelper.

### Example
```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public partial class MainViewModel : ReactiveObject
{
    [ObservableAsProperty]
    private string _status;

    [Reactive]
    private string _firstName;

    [Reactive]
    private string _lastName;

    public MainViewModel()
    {
        var statusObservable = this.WhenAnyValue(x => x.FirstName, x => x.LastName,
            (firstName, lastName) => $"{firstName} {lastName}");

        statusObservable.ToProperty(this, nameof(StatusProperty));
    }
}
```

### Explanation

The `[ObservableAsProperty]` attribute converts the observable statusObservable into a read-only property.
The generated property _status automatically updates whenever the observable emits a new value.
This attribute is ideal for computed properties or projections that rely on reactive updates, as it reduces boilerplate and keeps your code declarative.

The `[ObservableAsProperty]` attribute is a powerful tool for creating reactive properties that automatically update based on observable sources. 
By leveraging this attribute, you can simplify your ViewModel code and improve the readability and maintainability of your reactive applications. 
By decorating a `IObservable<T>` property or method with the `[ObservableAsProperty]` attribute, you can automatically update the property whenever the source observable emits a new value.
To use the `[ObservableAsProperty]` attribute in this way, you need to provide the source observable and the property name to bind to the call `InitializeOAPH();` method.
This method is generated by the source generator and initializes the property with the latest value from the source observable.

## Using the `[ReactiveCommand]` Attribute
The `[ReactiveCommand]` attribute simplifies the creation of ReactiveCommand instances.
These commands encapsulate business logic and can handle both synchronous and asynchronous operations.

### Example
```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.Reactive;

public partial class MainViewModel : ReactiveObject
{
    [ReactiveCommand]
    private void Save()
    {
        Console.WriteLine("Save executed!");
    }

    [ReactiveCommand]
    private IObservable<Unit> Load()
    {
        Console.WriteLine("Loading data...");
        return Observable.Return(Unit.Default);
    }
}
```

### Explanation

- Save Command: The Save method is automatically wrapped in a ReactiveCommand that can be bound to UI elements.
- Load Command: This example demonstrates how to create a command for asynchronous operations.

The generator handles all the plumbing for creating ReactiveCommand.Create or ReactiveCommand.CreateFromTask, saving significant time and effort.

### Benefits of ReactiveUI.SourceGenerators

Using ReactiveUI.SourceGenerators over traditional manual coding or Fody-based solutions provides several advantages:

- Clarity and Transparency: Generated code is visible and debuggable in IDEs, ensuring developers always understand how their application behaves.
- Reduced Boilerplate: Attributes like `[Reactive]`, `[ObservableAsProperty]`, and `[ReactiveCommand]` drastically reduce repetitive code.
- Type Safety: The source generator operates at compile-time, ensuring type safety and better integration with modern C# features.
- Improved Debugging: Since the generated code is part of the compilation pipeline, you can step into it with your debugger, unlike Fody, where changes are applied after compilation.

### Full Example: A Complete ViewModel

Below is a comprehensive example that demonstrates all three attributes in action:

```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.Reactive;
using System.Reactive.Linq;

public partial class MainViewModel : ReactiveObject
{
    [Reactive]
    private string _firstName;

    [Reactive]
    private string _lastName;

    [ObservableAsProperty]
    private string _fullName;

    [ReactiveCommand]
    private void Save() => Console.WriteLine("Saved!");

    [ReactiveCommand]
    private IObservable<Unit> Load()
    {
        Console.WriteLine("Loading...");
        return Observable.Return(Unit.Default);
    }

    public MainViewModel()
    {
        var fullNameObservable = this.WhenAnyValue(
            x => x.FirstName, x => x.LastName,
            (firstName, lastName) => $"{firstName} {lastName}");

        fullNameObservable.ToProperty(this, nameof(FullNameProperty));
    }
}
```

### Conclusion

ReactiveUI.SourceGenerators is a game-changer for developers building reactive applications. By leveraging one of the `[Reactive]` `[ObservableAsProperty]` `[ReactiveCommand]` attributes, you can write concise, readable, and maintainable code while eliminating boilerplate.

This modern approach offers compile-time safety, seamless IDE integration, and future-proof tooling, making it a superior alternative to ReactiveUI.Fody.

Upgrade your projects to use ReactiveUI.SourceGenerators today and experience the benefits of a clean and efficient reactive development workflow.

For more details, visit the [ReactiveUI.SourceGenerators](https://github.com/reactiveui/ReactiveUI.SourceGenerators) GitHub repository.
