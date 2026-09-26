# Command names

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/guidelines/guidelines.csproj).

A [command](../../handbook/commands/index.md) property and the method it wraps look alike: both are members of the
view model, and both can carry a verb such as `Save` or `Search`. Give the command property a `Command` suffix, so
a reader can tell which is which without opening the constructor, and give the method it wraps an `Impl` suffix.

## Name the command and its method

**1. Suffix the command property with `Command`, and its method with `Impl`.** `SaveImplAsync` does the work;
`saveCommand` is the property a view binds to.

```csharp
Enrollment enrollment = new("Robotics Club") { IsEnabled = true };
IObservable<bool> canSave = enrollment.WhenAnyValue(x => x.IsEnabled);

using ReactiveCommand<RxVoid, RxVoid> saveCommand = ReactiveCommand.CreateFromTask(SaveImplAsync, canSave);
using ReactiveCommand<string, RxVoid> searchCommand = ReactiveCommand.CreateFromTask<string, RxVoid>(SearchImplAsync);

_ = await saveCommand.Execute();
_ = await searchCommand.Execute("Robotics");

enrollment.IsEnabled = false;
Console.WriteLine(await saveCommand.CanExecute.FirstAsync());
```

```text
Saved
Searched for Robotics
False
```

**2. Read `CanExecute` after the condition changes.** `saveCommand` takes `canSave`, a stream of
`enrollment.IsEnabled`, as its `canExecute` argument. Setting `IsEnabled` to `false` after the command already ran
makes `CanExecute` read `false` on the next check. `searchCommand` takes a `string` parameter and needs no
`canExecute` argument to run.

Name the property itself after the action it performs, with `Command` as the suffix: `SaveCommand`, `DeleteCommand`,
`RefreshCommand`, `SearchCommand`. Avoid a property with no suffix, such as `Save`, and avoid a name that describes
the mechanism instead of the action, such as `PerformSave`.

## Naming with source generators

`ReactiveUI.SourceGenerators`, a separate package, derives the command name for you: a method named `Synchronize`
decorated with `[ReactiveCommand]` generates a property named `SynchronizeCommand`. The `Impl` convention above
still applies to whatever method the generated command calls. [Reduce boilerplate code](../../handbook/view-models/boilerplate-code.md)
covers the attribute.

## At a glance

| Convention | Example | Why |
| --- | --- | --- |
| `Command` suffix on the property | `SaveCommand`, `SearchCommand` | Tells a command apart from a plain method or property at a glance |
| Verb-based property name | `SaveCommand`, not `PerformSaveCommand` | Names the action, not the mechanism |
| `Impl` suffix on the wrapped method | `SaveImplAsync` behind `SaveCommand` | Separates the command from the work it runs |
