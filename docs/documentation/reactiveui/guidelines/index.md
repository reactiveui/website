---
Order: 3
---
# Guidelines

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/guidelines/guidelines.csproj).

A ReactiveUI app is built from ordinary C# and a handful of library types: `ReactiveObject`, `ReactiveCommand`, and
streams you subscribe to. Nothing stops you from using them the wrong way, so this section collects the habits that
keep an app maintainable, testable and correct. Each guideline links to the [handbook](../handbook/index.md) page
that explains the type or operator behind it, and to the framework and platform pages that go deeper.

## Start the app with RxAppBuilder

Configuring dependency injection, schedulers and platform services one call at a time is easy to get wrong: a
service registered too late, a scheduler set on the wrong thread. `RxAppBuilder.CreateReactiveUIBuilder()` gathers
all of it into one fluent chain instead.

**1. Build an instance.** The example below builds an isolated instance, so it does not touch the schedulers your
app's own `RxAppBuilder` call already set up for this process.

```csharp
ReactiveUIBuilder builder = RxAppBuilder.CreateReactiveUIBuilder();
builder.WithMainThreadScheduler(Sequencer.Immediate, setRxApp: false);
builder.WithTaskPoolScheduler(TaskPoolSequencer.Default, setRxApp: false);
builder.WithCoreServices();
IReactiveUIInstance instance = builder.BuildApp();

Console.WriteLine(instance.MainThreadScheduler?.GetType().Name);
```

```text
ImmediateSequencer
```

A real app also calls `WithViewsFromAssembly`, `WithRegistration` to register its own services, and the platform
extension for its UI framework, such as `WithWpf` or `WithMaui`. [RxAppBuilder](../handbook/rxappbuilder.md) walks
through every one of those calls.

## Read schedulers and the exception handler through RxSchedulers and RxState

The static `RxApp` class does not exist. Read the two schedulers `RxAppBuilder` configured, and the exception
handler it installed, through `RxSchedulers` and `RxState` instead.

```csharp
Console.WriteLine(RxSchedulers.MainThreadScheduler.GetType().Name);
Console.WriteLine(RxSchedulers.TaskpoolScheduler.GetType().Name);
Console.WriteLine(RxState.DefaultExceptionHandler is not null);
```

```text
ImmediateSequencer
TaskPoolSequencer
True
```

`RxSchedulers.MainThreadScheduler` and `TaskpoolScheduler` are `ISequencer` values: a sequencer decides when and
where work runs. [Scheduling](../handbook/scheduling.md) covers them and `RxState.DefaultExceptionHandler`, the
observer that receives an exception a subscription would otherwise drop silently.

## Remove boilerplate with source generators

`ReactiveUI.SourceGenerators` comes with ReactiveUI. It writes a property or a command for you from a decorated
property, field or method: `[Reactive]` for a settable property and `[ReactiveCommand]` for a command.
`[ObservableAsProperty]`, for a read-only property backed by a stream, comes from ReactiveUI.Binding, which ReactiveUI
also brings. [Reduce boilerplate code](../handbook/view-models/boilerplate-code.md) shows
every attribute, and [Declare the property with an attribute](../../binding/properties.md#declare-the-property-with-an-attribute)
covers `[ObservableAsProperty]`, which comes from `ReactiveUI.Binding`.

## Turn events into streams

A view raises events instead of exposing streams: a button click, a text box's `TextChanged`. Subscribing to the
event directly means unsubscribing it yourself, usually from an override such as `OnClosed`, and a forgotten
unsubscribe leaks the view. `ReactiveUI.Primitives.ObservableEvents`, a source generator, writes a stream for every
public event a type raises. Call `Events()` on the object, then read the event as a property on the result.
[Events](../handbook/events.md) is the full walkthrough, and the [WPF](platform/wpf-overview.md) and
[Windows Forms](platform/windows-forms.md) pages show it wired into a real control.

## Dispose every subscription

A subscription you never dispose keeps running for as long as the object it watches is alive, even after the
screen that created it is gone. [Dispose your subscriptions](framework/dispose-your-subscriptions.md) covers
`WhenActivated` and `DisposeWith`, and the one case that needs neither.

## Bind to a command instead of wiring a click handler

Calling a view model's method straight from a click handler leaves nothing to disable the control once the action
no longer applies. [Commands](framework/commands.md) covers binding to a `ReactiveCommand` with `BindCommand`
instead, and [Command names](framework/command-names.md) covers naming the command and the method behind it.

## Let an async command do its own work

Starting a task from a synchronous command's `Subscribe` hides that work from `IsExecuting` and `ThrownExceptions`.
Nothing then stops a second click from starting the work again while the first run is still going.
[Asynchronous commands](framework/asynchronous-commands.md) shows `ReactiveCommand.CreateFromTask` doing the same
work in a way both properties can see.

## Handle a command's failures through ThrownExceptions

A command's `ThrownExceptions` stream is where a subscriber that only watches, such as the screen, learns about a
failure. The `await` that starts the command still sees the exception; `ThrownExceptions` is for everyone else.

**1. Subscribe to `ThrownExceptions` before the command runs.** The subscriber below turns a failure into a message
a screen could show.

```csharp
using ReactiveCommand<RxVoid, RxVoid> save = ReactiveCommand.CreateFromTask(
    static () => throw new InvalidOperationException("No connection"));
string errorMessage = string.Empty;
using IDisposable subscription = save.ThrownExceptions.Subscribe(_ => errorMessage = "Unable to save. Please try again.");

try
{
    await save.Execute();
}
catch (InvalidOperationException)
{
    // The awaiting caller also sees the failure; ThrownExceptions is for a subscriber that only watches, such as the screen.
}

Console.WriteLine(errorMessage);
```

```text
Unable to save. Please try again.
```

## Await async work instead of blocking on it

Reading `.Result` or calling `.Wait()` on a `Task` blocks the calling thread until the task finishes. On a UI
thread that freezes the screen. On a thread-pool thread it can starve the pool of other work. `await` keeps the
thread free while the work runs.

```csharp
int grade = await FetchGradeAsync();
Console.WriteLine(grade);
```

```text
91
```

## Use descriptive names in a WhenAny selector

`x` and `y` compile as selector parameters, but they make the reader open the lambda to know which property is
which. [Use descriptive variables with WhenAny](framework/use-descriptive-variables-with-whenany.md) shows named
parameters next to the same rule written with `x` and `y`.

## Put this on the left of WhenAny

`this.WhenAny(...)` ties a pipeline's lifetime to the view model that owns it. Reading a dependency's property with
`dependency.WhenAny(...)` instead ties the pipeline to how long that dependency lives, which can outlast the view
model. [Put this on the left of WhenAny](framework/use-this-on-left-of-whenany.md) covers it, including the one
case that still needs `DisposeWith`.

## Read a derived value from an ObservableAsPropertyHelper, not a setter

A plain settable property can be written to from anywhere, including by mistake. An `ObservableAsPropertyHelper`
property has no setter, so its pipeline is the only place its value can come from.
[Prefer ObservableAsPropertyHelper over properties](framework/prefer-oaph-over-properties.md) shows both.

## Marshal to the UI thread at the boundary, not after every step

Adding `WitnessOn` after every operator in a pipeline works, but it schedules far more than it needs to. One
`WitnessOn(RxSchedulers.MainThreadScheduler)`, placed where the result reaches a bound property, is enough.
[UI thread and schedulers](framework/ui-thread-and-schedulers.md) shows both forms, and an async command that
marshals its result on its own.

## Share a chain that more than one subscriber reads

Subscribing to the same `WhenAnyValue` chain more than once repeats its work for every subscriber.
[Sharing](../../primitives/sharing.md) covers `ShareLatest` and the other operators that share one subscription
among many.

## Reach for DynamicData for reactive collections

An `ObservableCollection<T>` has no stream of its own additions and removals. [DynamicData](https://github.com/reactivemarbles/DynamicData)
adds a `SourceCache<T, TKey>` and operators that turn changes into a bound, sorted, filtered list.
For the simpler case, a stream of adds and removes from an `ObservableCollection<T>`, ReactiveUI's own
[Collections](../handbook/collections.md) helpers need no extra package.

## Validate input with ReactiveUI.Validation

`ReactiveUI.Validation`, a separate package, adds `ReactiveValidationObject` and `ValidationRule` for turning a
property's rules into a stream of validation state a command's `CanExecute` can use. See
[Validation](../../validation.md).

## Test with ReactiveUI.Testing

`ReactiveUI.Testing` gives a test a `VirtualClock`, a sequencer that moves only when the test tells it to. A test
that uses a time operator then runs at once, and the same way every time. [Testing](../handbook/testing.md) covers
it.

## Keep a view model platform-agnostic, focused, and built through its constructor

A view model that references a `Window` or another platform type cannot run on a different platform, or in a test,
without one. Depend on an interface such as `IDialogService` instead, and let the platform project supply the
implementation.

A view model with one clear job is easier to read, test and reuse than one with several. Give each view model a
narrow set of commands and properties, and split one that grows past that into smaller view models.

Take dependencies through the constructor rather than reaching for a service locator inside it.
`RxAppBuilder.WithRegistration` is where those dependencies get registered; [Registration](../handbook/registration.md)
covers it, and `AppLocator.Current.GetService<T>()` remains the escape hatch for code that cannot take a
constructor parameter.

## Continue reading

The `framework/` pages cover the command, property and subscription guidelines above in more detail, with a
passing and a failing example for each. The `platform/` pages cover the pattern each UI framework layers on top:
`ReactiveWindow<T>`, `ReactiveContentPage<T>`, `ReactiveComponentBase<T>` and their equivalents. The `debugging/`
pages cover diagnosing a problem once you have one. [Upgrading](../upgrading/index.md) covers moving code written
for an older ReactiveUI release onto the APIs this section describes.
