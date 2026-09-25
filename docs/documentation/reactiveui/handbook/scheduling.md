---
Order: 17
---
# Scheduling in ReactiveUI

A view model does work in the background, but the screen can only change on the UI thread. A **sequencer** decides
which thread, and when, a piece of work runs. ReactiveUI gives your app two, so all your code agrees on where work
goes:

- **`RxSchedulers.MainThreadScheduler`** runs work on the UI thread. On WPF it posts to the `Dispatcher`.
- **`RxSchedulers.TaskpoolScheduler`** runs work on the thread pool, as `Task.Run` does.

Both are `ISequencer` values from [ReactiveUI.Primitives](../../primitives/scheduling.md). Use them instead of creating
threads or picking sequencers yourself. Code that goes through them can be tested without waiting.

```csharp
using ReactiveUI;
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Concurrency;
using ReactiveUI.Primitives.Signals;
```

## Moving a stream to the UI thread

Put `WitnessOn(RxSchedulers.MainThreadScheduler)` before the callback that touches the screen:

```csharp
this.WhenAnyValue(x => x.MyImportantProperty)
    .WitnessOn(RxSchedulers.MainThreadScheduler)
    .Subscribe(x => StatusText = x);
```

`WitnessOn` hands each value to the sequencer, and your callback runs where the sequencer runs it. See
[UI platforms](../../primitives/platforms.md) for the sequencer behind `MainThreadScheduler` on each platform.

## Commands and property helpers

A `ReactiveCommand` sends its results through its `outputScheduler`, which is `RxSchedulers.MainThreadScheduler` when
you give none. An `ObservableAsPropertyHelper` raises `PropertyChanged` through its `scheduler`. With none, it raises
it on the thread the value arrived on, so a value from a background thread raises `PropertyChanged` there. Pass
`RxSchedulers.MainThreadScheduler` when the source may send from another thread:

```csharp
public class MyVm : ReactiveObject
{
    private readonly ObservableAsPropertyHelper<bool> _isRunning;

    public MyVm()
    {
        MyCommand = ReactiveCommand.Create<RxVoid, string>(_ => "done", outputScheduler: RxSchedulers.MainThreadScheduler);
        _isRunning = MyCommand.IsExecuting.ToProperty(this, nameof(IsRunning), scheduler: RxSchedulers.MainThreadScheduler);
    }

    public ReactiveCommand<RxVoid, string> MyCommand { get; }

    public bool IsRunning => _isRunning.Value;
}
```

`RxVoid` is a value that carries no data. Use it for a command that takes no parameter.

## Replacing threads and dispatcher calls

Work started with `new Thread()`, `Task.Run` or `Dispatcher.BeginInvoke` runs where ReactiveUI cannot see it, so a
test cannot control it. Send the work through the two sequencers instead.

This code starts its own work:

```csharp
var result = await Task.Run(() =>
{
    int number = ThisCalculationTakesALongTime();
    return number;
});

Dispatcher.BeginInvoke(new Action(() => DoAThing()));
```

This code sends the same work through the sequencers:

```csharp
var result = await Signal.Start(() =>
{
    int number = ThisCalculationTakesALongTime();
    return number;
}, RxSchedulers.TaskpoolScheduler);

RxSchedulers.MainThreadScheduler.Schedule(() => DoAThing());
```

`Signal.Start` runs the lambda on the sequencer you give, and sends its result. You can `await` the stream for that
result. See [creation factories](../../primitives/creation-factories.md).

If you write a shared component, take an `ISequencer` as an optional constructor parameter, so the app or a test can
choose.

## Testing

In a test run, `MainThreadScheduler` runs work straight away, because there is no UI thread. `TaskpoolScheduler` is
left unchanged.

To control time, use `With` from `ReactiveUI.Testing` with a `VirtualClock`: a sequencer whose time moves only when
you move it. Inside the block, both `RxSchedulers.MainThreadScheduler` and `RxSchedulers.TaskpoolScheduler` are the
clock. They go back to what they were when the block ends.

```csharp
using ReactiveUI.Testing;

new VirtualClock().With(clock =>
{
    RxSchedulers.MainThreadScheduler.Schedule(TimeSpan.FromSeconds(5), () => Console.WriteLine("five virtual seconds"));
    clock.AdvanceBy(TimeSpan.FromSeconds(5));
});
```

Output, with no real wait:

```text
five virtual seconds
```

See [testing with a virtual clock](../../primitives/scheduling.md#testing-with-a-virtual-clock) and [testing](testing.md).
