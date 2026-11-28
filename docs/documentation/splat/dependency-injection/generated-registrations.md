---
Order: 2
---
# Generate service registrations

A receipt printer needs a shop name and a counter. Each printer is a new object, but printers at the same shop should share one sequence of receipt numbers. The Splat dependency injection source generator builds the factories for those objects during compilation.

Add `Splat` and `Splat.DependencyInjection.SourceGenerator` to the project that owns the registrations. Register the dependencies and call `SetupIOC()` once when that project starts.

```csharp
SplatRegistrations.RegisterConstant(new Shop("Market"));
SplatRegistrations.Register<ReceiptPrinter>();
SplatRegistrations.RegisterLazySingleton<ReceiptNumbers>();
SplatRegistrations.SetupIOC();

var printer = AppLocator.Current.GetService<ReceiptPrinter>();
```

## Choose what each caller receives

| Registration | What a caller receives |
| --- | --- |
| `Register<T>()` or `Register<TService, TImplementation>()` | A new instance on each lookup. |
| `RegisterLazySingleton<T>()` or `RegisterLazySingleton<TService, TImplementation>()` | The same instance after its first lookup in that resolver. Interface registrations also accept a `LazyThreadSafetyMode`. |
| `RegisterConstant(instance)` | The object you already created. This call registers it immediately in `AppLocator.CurrentMutable`. |

Each lifetime supports a string contract to distinguish registrations of the same service. Use the same name when resolving it. Constructor and property dependencies use their types' default registrations.

Use a string literal or a constant accessible from another type in the same assembly for a generated registration. The generator writes the contract expression into a separate class. A private constant on your startup class causes compiler error `CS0122`.

```csharp
SplatRegistrations.RegisterLazySingleton<IReceiptNumbers, ReceiptNumbers>(
    "online", LazyThreadSafetyMode.ExecutionAndPublication);
// After SetupIOC(), both callers use the online sales counter.
var counter = AppLocator.Current.GetService<IReceiptNumbers>("online");
```

## Choose a constructor or inject a property

With one accessible constructor, the generator supplies its parameters. If the type has several, mark the intended constructor with `[DependencyInjectionConstructor]`. A constructor can request `Lazy<T>` when `T` has a lazy singleton registration. The generated factory also sets properties marked with `[DependencyInjectionProperty]`. Give those properties an accessible setter. [Read the receipt printer](https://github.com/reactiveui/Splat.DI.SourceGenerator/blob/main/src/examples/ReceiptServices/ReceiptPrinter.cs).

## Use an independent resolver

Call `SetupIOC(resolver)` to install the generated factories in a resolver you own. Register its constants directly on that resolver before asking it for a service. This lets a second shop start its own counter.

```csharp
using var branch = new ModernDependencyResolver();
branch.RegisterConstant(new Shop("Branch"));
SplatRegistrations.SetupIOC(branch);
var branchPrinter = branch.GetService<ReceiptPrinter>();
```

The analyzer identifies ambiguous or inaccessible constructors and inaccessible property setters. The generator also reports circular dependencies, duplicate registrations for the same service and contract, and `Lazy<T>` dependencies without a matching lazy registration. [Run the receipt services example](https://github.com/reactiveui/Splat.DI.SourceGenerator/tree/main/src/examples).
