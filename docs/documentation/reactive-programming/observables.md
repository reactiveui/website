# Streams in ReactiveUI

A ReactiveUI app mostly works with streams it did not build by hand: a property that changes, a command that runs, a
task that finishes. This page shows where those streams come from, what they send, and how to clean them up. For
streams in general, see the [Primitives overview](../primitives/index.md).

The examples use these namespaces:

```csharp
using ReactiveUI;
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;
```

## Your first stream from a property

A `ReactiveObject` raises `PropertyChanged` whenever a property set with `RaiseAndSetIfChanged` changes.
`WhenAnyValue` turns that into a stream of the property's values.

```csharp
public sealed class PersonViewModel : ReactiveObject
{
    private string _firstName = "";
    private string _lastName = "";

    public string FirstName { get => _firstName; set => this.RaiseAndSetIfChanged(ref _firstName, value); }

    public string LastName { get => _lastName; set => this.RaiseAndSetIfChanged(ref _lastName, value); }
}
```

**1. Ask for the properties you care about.** Give `WhenAnyValue` one lambda per property, and a last lambda that
combines their values.

**2. Subscribe.** Your callback runs with the current value straight away, then again after every change.

```csharp
var person = new PersonViewModel();

person.WhenAnyValue(x => x.FirstName, x => x.LastName, (first, last) => $"{first} {last}")
      .Subscribe(fullName => Console.WriteLine(fullName));

person.FirstName = "Ada";
person.LastName = "Lovelace";
```

Output, where the first line is a single space:

```text
 
Ada 
Ada Lovelace
```

## Other places streams come from

| Source | Stream |
|---|---|
| A property | `WhenAnyValue`. See [WhenAny](../handbook/when-any.md). |
| A command | `ReactiveCommand` is a stream of its results, and `IsExecuting` and `ThrownExceptions` are streams too. See [commands](../handbook/commands/index.md). |
| An event | `Events()` from the [observable events generator](../handbook/observable-events/index.md), or [`Signal.FromEvent`](../primitives/creation-factories.md#from-an-event). |
| A task | [`Signal.FromAsync`](../primitives/creation-factories.md). |
| A value you push yourself | [`Signal<T>`](../primitives/signals.md) and the other signals. |
| A rule or a timer | [Creation factories](../primitives/creation-factories.md) such as `Signal.Range`, `Signal.Every` and `Signal.Create`. |

`Signal.FromAsync` runs a task when you subscribe, and hands it a `CancellationToken` that cancels when you dispose:

```csharp
Signal.FromAsync(token => LoadGreetingAsync(token))
      .Subscribe(greeting => Console.WriteLine(greeting));

static async Task<string> LoadGreetingAsync(CancellationToken token)
{
    await Task.Delay(10, token);
    return "hello from a task";
}
```

Output:

```text
hello from a task
```

## Values, completion and failure

`Subscribe` takes up to three lambdas: one for each value, one for a failure, and one for completion. A stream that
fails or completes sends nothing after that.

```csharp
Signal.Range(1, 2).Subscribe(
    value => Console.WriteLine($"value: {value}"),
    error => Console.WriteLine($"failed: {error.Message}"),
    () => Console.WriteLine("completed"));

Signal.Fail<int>(new InvalidOperationException("no network")).Subscribe(
    value => Console.WriteLine($"value: {value}"),
    error => Console.WriteLine($"failed: {error.Message}"));
```

Output:

```text
value: 1
value: 2
completed
failed: no network
```

Always handle failure for a stream that can fail. With no failure lambda, the exception is thrown on whatever thread
sent it. For a command, subscribe to `ThrownExceptions` instead. See [error handling](../primitives/error-handling.md).

## Cold and hot streams

A **cold** stream starts again for each subscriber. `Signal.Range` is cold, so each subscriber gets every value:

```csharp
IObservable<int> cold = Signal.Range(1, 2);

cold.Subscribe(x => Console.WriteLine($"first: {x}"));
cold.Subscribe(x => Console.WriteLine($"second: {x}"));
```

Output:

```text
first: 1
first: 2
second: 1
second: 2
```

A **hot** stream runs whether or not anyone listens. A subscriber only sees what happens after it subscribes.
Property changes, events and a `Signal<T>` are hot:

```csharp
var hot = new Signal<int>();

hot.Subscribe(x => Console.WriteLine($"early: {x}"));
hot.OnNext(1);

hot.Subscribe(x => Console.WriteLine($"late: {x}"));
hot.OnNext(2);
```

Output:

```text
early: 1
early: 2
late: 2
```

To let several subscribers share one run of a cold stream, see [sharing one subscription](../primitives/sharing.md).

## Cleaning up

Every subscription holds on to its callback until you dispose it. A view model that subscribes to a longer-lived
object, and never disposes, is never freed.

In a view model, implement `IActivatableViewModel` and subscribe inside `WhenActivated`. Add each subscription to the
`MultipleDisposable` it hands you with `DisposeWith`. ReactiveUI disposes them all when the view deactivates.

```csharp
public sealed class DashboardViewModel : ReactiveObject, IActivatableViewModel
{
    public DashboardViewModel()
    {
        this.WhenActivated(disposables =>
        {
            Refreshes.Subscribe(_ => Console.WriteLine("refreshing"))
                     .DisposeWith(disposables);
        });
    }

    public ViewModelActivator Activator { get; } = new();

    public Signal<RxVoid> Refreshes { get; } = new();
}
```

```csharp
var dashboard = new DashboardViewModel();

dashboard.Activator.Activate();
dashboard.Refreshes.OnNext(RxVoid.Default);

dashboard.Activator.Deactivate();
dashboard.Refreshes.OnNext(RxVoid.Default);
```

Output:

```text
refreshing
```

The second value arrives after deactivation, so nothing prints. `RxVoid` is a value that carries no data, for a stream
where only the fact that something happened matters. See [WhenActivated](../handbook/when-activated.md) and
[disposables](../primitives/disposables.md).

## Threads

Work can move off the UI thread, but the screen can only change on it. Run slow work on
`RxSchedulers.TaskpoolScheduler`, then use `WitnessOn(RxSchedulers.MainThreadScheduler)` before the callback that
touches the screen:

```csharp
Signal.Start(() => 6 * 7, RxSchedulers.TaskpoolScheduler)
      .WitnessOn(RxSchedulers.MainThreadScheduler)
      .Subscribe(result => Console.WriteLine($"result: {result}"));
```

Output:

```text
result: 42
```

See [scheduling](../handbook/scheduling.md) and [UI platforms](../primitives/platforms.md).

## Testing

Streams that wait, such as a search that waits for typing to stop, are slow to test in real time. Pass a
`VirtualClock` to the operators that wait, and move time forward yourself. See
[testing with a virtual clock](../primitives/scheduling.md#testing-with-a-virtual-clock) and
[testing](../handbook/testing.md).

## Related topics

- [Operators in ReactiveUI](operators.md)
- [Why Primitives](../primitives/why-primitives.md)
- [Best practices](../primitives/best-practices.md)
