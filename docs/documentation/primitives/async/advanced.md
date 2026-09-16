---
Order: 10
---
# Writing your own async operator

The async operators are built from public parts, so you can build your own the same way. This page covers those
parts, plus a few helpers for async disposal and errors that nothing else can deliver.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Async;
using ReactiveUI.Primitives.Async.Advanced;
using ReactiveUI.Primitives.Async.Disposables;
using ReactiveUI.Primitives.Async.Helpers;
using ReactiveUI.Primitives.Async.Signals;
```

Reach for these only when no operator does the job. Most apps never need them.

## An operator of your own

An operator has two halves. The **signal** is the `IObservableAsync<T>` you hand back. The **witness** is the
`IObserverAsync<T>` it subscribes to the source: it receives each value, changes it, and passes it on.

### `IWitnessAsync<T>` and `WitnessAsync`

A witness must cope with notifications that arrive after it is disposed or cancelled, and with a callback that throws.
`WitnessAsync` handles all of that. You implement `IWitnessAsync<T>`:

1. Hold a `WitnessAsyncState` as a **non-readonly field**, and return it by `ref` from `IWitnessState.Witness`.
   `WitnessAsyncState` is a struct: a `readonly` field or a copy would be a separate state, and notifications would be
   lost.
2. Forward `OnNextAsync`, `OnErrorResumeAsync`, `OnCompletedAsync` and `DisposeAsync` to the matching `WitnessAsync`
   method.
3. Write your logic in the three `Core` methods. `WitnessAsync` calls them only when it is safe.

### `WitnessSubscription`

`WitnessSubscription.SubscribeAsync` subscribes your witness to the source, links its teardown to the subscription,
and hands back the witness as the handle to dispose.

This operator doubles every value:

```csharp
Console.WriteLine(string.Join(", ", await new DoubleSignal(SignalAsync.Range(1, 3)).ToListAsync()));

public sealed class DoubleSignal(IObservableAsync<int> source) : IObservableAsync<int>
{
    public ValueTask<IAsyncDisposable> SubscribeAsync(IObserverAsync<int> witness, CancellationToken cancellationToken) =>
        WitnessSubscription.SubscribeAsync(source, new DoubleWitness(witness), witness, cancellationToken);
}

public sealed class DoubleWitness(IObserverAsync<int> downstream) : IWitnessAsync<int>
{
    private WitnessAsyncState _witness;

    ref WitnessAsyncState IWitnessState.Witness => ref _witness;

    public ValueTask OnNextAsync(int value, CancellationToken cancellationToken) =>
        WitnessAsync.OnNextAsync(this, value, cancellationToken);

    public ValueTask OnErrorResumeAsync(Exception error, CancellationToken cancellationToken) =>
        WitnessAsync.OnErrorResumeAsync(this, error, cancellationToken);

    public ValueTask OnCompletedAsync(Result result) => WitnessAsync.OnCompletedAsync(this, result);

    public ValueTask DisposeAsync() => WitnessAsync.DisposeStateAsync(this);

    ValueTask IWitnessAsync<int>.OnNextAsyncCore(int value, CancellationToken cancellationToken) =>
        downstream.OnNextAsync(value * 2, cancellationToken);

    ValueTask IWitnessAsync<int>.OnErrorResumeAsyncCore(Exception error, CancellationToken cancellationToken) =>
        downstream.OnErrorResumeAsync(error, cancellationToken);

    ValueTask IWitnessAsync<int>.OnCompletedAsyncCore(Result result) => downstream.OnCompletedAsync(result);
}
```

Output:

```text
2, 4, 6
```

When two threads call the same witness at the same time, `WitnessAsync` drops the second call and reports a
`ConcurrentWitnessCallsException` to the [global handler](#unhandledexceptionhandler). A call from the thread already
inside the witness runs.

### Ending the stream early

An operator such as `Take` stops the source before the source is done. Three helpers do that, and let you subscribe
the witness yourself instead of through `WitnessSubscription`:

- `LinkUpstreamCancellation(token)` cancels the witness when the `CancellationToken` passed to `SubscribeAsync`
  cancels.
- `AssignSourceSubscriptionAsync(subscription)` stores the source subscription in the witness state, so disposing the
  witness disposes the subscription too.
- `WitnessAsync.DisposeFromNotificationAsync` disposes the witness from inside one of its own callbacks.

This operator sends the first two values, completes, and stops the source:

```csharp
List<int> firstTwo = await new FirstTwoSignal(SignalAsync.Range(1, 10).OnDispose(static () => Console.WriteLine("source stopped")))
    .ToListAsync();

Console.WriteLine(string.Join(", ", firstTwo));

public sealed class FirstTwoSignal(IObservableAsync<int> source) : IObservableAsync<int>
{
    public async ValueTask<IAsyncDisposable> SubscribeAsync(IObserverAsync<int> witness, CancellationToken cancellationToken)
    {
        var firstTwo = new FirstTwoWitness(witness);
        firstTwo.LinkUpstreamCancellation(cancellationToken);
        await firstTwo.AssignSourceSubscriptionAsync(await source.SubscribeAsync(firstTwo, cancellationToken));
        return firstTwo;
    }
}

public sealed class FirstTwoWitness(IObserverAsync<int> downstream) : IWitnessAsync<int>
{
    private WitnessAsyncState _witness;
    private int _count;

    ref WitnessAsyncState IWitnessState.Witness => ref _witness;

    public ValueTask OnNextAsync(int value, CancellationToken cancellationToken) =>
        WitnessAsync.OnNextAsync(this, value, cancellationToken);

    public ValueTask OnErrorResumeAsync(Exception error, CancellationToken cancellationToken) =>
        WitnessAsync.OnErrorResumeAsync(this, error, cancellationToken);

    public ValueTask OnCompletedAsync(Result result) => WitnessAsync.OnCompletedAsync(this, result);

    public ValueTask DisposeAsync() => WitnessAsync.DisposeStateAsync(this);

    async ValueTask IWitnessAsync<int>.OnNextAsyncCore(int value, CancellationToken cancellationToken)
    {
        await downstream.OnNextAsync(value, cancellationToken);

        if (++_count == 2)
        {
            await downstream.OnCompletedAsync(Result.Success);
            await WitnessAsync.DisposeFromNotificationAsync(this);
        }
    }

    ValueTask IWitnessAsync<int>.OnErrorResumeAsyncCore(Exception error, CancellationToken cancellationToken) =>
        downstream.OnErrorResumeAsync(error, cancellationToken);

    ValueTask IWitnessAsync<int>.OnCompletedAsyncCore(Result result) => downstream.OnCompletedAsync(result);
}
```

Output:

```text
source stopped
1, 2
```

The source would have sent ten values. It stopped after two.

## Combining the latest values yourself

[`SyncLatest`](combination.md) sends a result built from the latest value of each source, once every source has sent
one. It runs on a **coordinator**: an `ISyncLatestCoordinator<TResult>` that holds one **slot** per source, and builds
the result when asked. The built-in coordinators take up to ten sources of different types. Write your own to combine
any number of sources.

A coordinator has three parts:

- `Lifecycle`, a `SyncLatestLifecycle<TResult>`. It holds the subscriptions, sends results on, and completes the stream
  when every source has completed.
- `SubscribeAtAsync(index, token)`, which subscribes the source for one slot. `SubscribeToSlotAsync` does the work:
  it records each value with your lambda, and asks the coordinator for a result after each one.
- `EmitLatestAsync`, which builds the result and passes it to `Lifecycle.EmitDownstreamAsync`. Return without sending
  while any slot is still empty.

`SubscribeSourcesAsync` subscribes every slot in order. If it throws, dispose the coordinator, so the sources already
subscribed stop.

This operator sends the sum of the latest value from any number of `int` streams:

```csharp
var morning = Signal.CreateBehavior(2);
var afternoon = Signal.CreateBehavior(5);
var evening = Signal.CreateBehavior(1);

await using (await new SumLatestSignal([morning, afternoon, evening])
                 .SubscribeAsync(static total => Console.WriteLine($"total: {total}")))
{
    await afternoon.OnNextAsync(7, CancellationToken.None);
}

public sealed class SumLatestSignal(IObservableAsync<int>[] sources) : IObservableAsync<int>
{
    public async ValueTask<IAsyncDisposable> SubscribeAsync(IObserverAsync<int> witness, CancellationToken cancellationToken)
    {
        var coordinator = new SumLatestCoordinator(witness, sources);
        coordinator.Lifecycle.LinkExternalCancellation(cancellationToken);

        try
        {
            await coordinator.SubscribeSourcesAsync(cancellationToken);
        }
        catch
        {
            await coordinator.DisposeAsync();
            throw;
        }

        return coordinator;
    }
}

public sealed class SumLatestCoordinator(IObserverAsync<int> witness, IObservableAsync<int>[] sources) : ISyncLatestCoordinator<int>
{
    private readonly Optional<int>[] _latest = new Optional<int>[sources.Length];

    public SyncLatestLifecycle<int> Lifecycle { get; } = new(witness, sources.Length);

    public ValueTask<IAsyncDisposable> SubscribeAtAsync(int index, CancellationToken cancellationToken) =>
        sources[index].SubscribeToSlotAsync(this, index, value => _latest[index] = Optional.Some(value), cancellationToken);

    public ValueTask EmitLatestAsync()
    {
        var total = 0;
        foreach (var slot in _latest)
        {
            if (!slot.TryGetValue(out var value))
            {
                return default;
            }

            total += value;
        }

        return Lifecycle.EmitDownstreamAsync(total);
    }

    public ValueTask DisposeAsync() => Lifecycle.DisposeAsync();
}
```

Output:

```text
total: 8
total: 10
```

`SubscribeToSlotAsync` subscribes the witness that `SyncLatestSlot.CreateWitness` builds. Call `CreateWitness` yourself
when you need to hold the witness before you subscribe it. A slot index outside the coordinator's sources throws
`ArgumentOutOfRangeException`.

### Other building blocks

| Type | What it is for |
|---|---|
| `TaskSignalSubscription<T>` and `TaskSignalSubscription.StartNew` | Runs an async job that sends to a witness, and cancels the job when disposed. The type behind `CreateAsBackgroundJob`. |
| `TaskResultCompletionSource<T>` | Completes the `ValueTask<T>` of a terminal operator such as `FirstAsync`, and disposes the subscription as it does. |
| `Result` | How a stream ended. `IsSuccess`, `IsFailure`, `Exception`, `Result.Success`, `Result.Failure(exception)`, and `TryThrow()`. |

`TaskSignalSubscription.StartNew` in action:

```csharp
TaskSignalSubscription<int> job = TaskSignalSubscription.StartNew<int>(
    static async (witness, cancellationToken) =>
    {
        await witness.OnNextAsync(1, cancellationToken);
        await witness.OnCompletedAsync(Result.Success);
    },
    Signal.Create<int>().AsObserverAsync());

await Task.Delay(50);
await job.DisposeAsync();
Console.WriteLine("job finished and disposed");
```

Output:

```text
job finished and disposed
```

## Async disposal

### `DisposableAsync.Create`

`DisposableAsync.Create` builds an `IAsyncDisposable` from a method. An overload takes a state object, so the method can
be `static`.

### `MultipleDisposableAsync`

`MultipleDisposableAsync` is the async form of [`MultipleDisposable`](../disposables.md#multipledisposable): a group
of `IAsyncDisposable` values, disposed together in the order you added them. `AddAsync`, `Remove` and `Clear` return a
`ValueTask`, because removing an item disposes it. An item added after the group is disposed is disposed at once.

```csharp
IAsyncDisposable Named(string name) =>
    DisposableAsync.Create(() => { Console.WriteLine($"{name} closed"); return ValueTask.CompletedTask; });

var group = new MultipleDisposableAsync(Named("socket"), Named("file"));
await group.AddAsync(Named("timer"));
Console.WriteLine($"holding {group.Count}");

await group.DisposeAsync();
await group.AddAsync(Named("late"));
```

Output:

```text
holding 3
socket closed
file closed
timer closed
late closed
```

The lambda in `Named` uses `name`, so it cannot be `static`.

### `SingleAssignmentDisposableAsync`

`SingleAssignmentDisposableAsync` holds one `IAsyncDisposable`, set once with `SetDisposableAsync`. A second set throws
`InvalidOperationException`.

### `SingleReplaceableDisposableAsync`

`SingleReplaceableDisposableAsync` holds the latest `IAsyncDisposable`. Each `SetDisposableAsync` disposes the value it
replaces.

```csharp
IAsyncDisposable Search(string term) =>
    DisposableAsync.Create(() => { Console.WriteLine($"search for {term} stopped"); return ValueTask.CompletedTask; });

var current = new SingleReplaceableDisposableAsync();
await current.SetDisposableAsync(Search("r"));
await current.SetDisposableAsync(Search("rx"));
await current.DisposeAsync();

var once = new SingleAssignmentDisposableAsync();
await once.SetDisposableAsync(Search("report"));

try
{
    await once.SetDisposableAsync(Search("again"));
}
catch (InvalidOperationException error)
{
    Console.WriteLine(error.Message);
}
```

Output:

```text
search for r stopped
search for rx stopped
Disposable is already assigned.
```

### `DisposableAsyncSlot`

`DisposableAsyncSlot` manages a field that holds an `IAsyncDisposable?`. Pass the field by `ref`.

| Method | What it does |
|---|---|
| `AssignAsync(ref slot, value)` | Stores the value. If the slot is disposed, disposes the value at once. |
| `SwapAsync(ref slot, value)` | Stores the value and disposes the one it replaces. |
| `DisposeAsync(ref slot)` | Disposes what the slot holds and marks it disposed. |
| `IsDisposed(slot)` | Whether the slot is disposed. |

```csharp
IAsyncDisposable? current = null;

await DisposableAsyncSlot.AssignAsync(ref current, DisposableAsync.Create(static () => { Console.WriteLine("first closed"); return ValueTask.CompletedTask; }));
await DisposableAsyncSlot.SwapAsync(ref current, DisposableAsync.Create(static () => { Console.WriteLine("second closed"); return ValueTask.CompletedTask; }));
await DisposableAsyncSlot.DisposeAsync(ref current);
Console.WriteLine($"disposed: {DisposableAsyncSlot.IsDisposed(current)}");
```

Output:

```text
first closed
second closed
disposed: True
```

### `ToDisposableAsync`

`ToDisposableAsync` wraps a plain `IDisposable` as an `IAsyncDisposable`, so it fits wherever a subscription handle is
expected.

```csharp
IAsyncDisposable handle = new MemoryStream().ToDisposableAsync();
await handle.DisposeAsync();
Console.WriteLine("stream disposed");
```

Output:

```text
stream disposed
```

For the sync disposables, see [disposables](../disposables.md).

## Errors nothing else can deliver

### `UnhandledExceptionHandler`

Some exceptions have no subscriber to go to: a failure in background work, or a resumable error that
`CatchAndIgnoreErrorResume` took away. They go to one process-wide handler. `UnhandledExceptionHandler.Register` sets
it. Register a handler at startup that logs, so these exceptions are never silent.

### `FireAndForgetHelper.Run`

`FireAndForgetHelper.Run` starts async work you do not await. An exception from the work goes to the unhandled
exception handler instead of being lost.

```csharp
UnhandledExceptionHandler.Register(static error => Console.WriteLine($"unhandled: {error.Message}"));

FireAndForgetHelper.Run(static () => throw new InvalidOperationException("background save failed"));
await Task.Delay(50);
```

Output:

```text
unhandled: background save failed
```

## `Optional<T>` in async code

`TryGetValue` reads an [`Optional<T>`](../extensions/state-and-testing.md#optionalt) with the try pattern.

```csharp
Optional<int> found = Optional.Some(3);

if (found.TryGetValue(out var value))
{
    Console.WriteLine(value);
}
```

Output:

```text
3
```

## At a glance

| Member | What it does |
|---|---|
| `IWitnessAsync<T>`, `WitnessAsyncState`, `IWitnessState` | The contract for a witness of your own. |
| `WitnessAsync.OnNextAsync` / `OnErrorResumeAsync` / `OnCompletedAsync` / `DisposeStateAsync` / `DisposeFromNotificationAsync` | Safe forwarding for a witness. |
| `WitnessAsyncExtensions.AssignSourceSubscriptionAsync` / `LinkUpstreamCancellation` | Links a witness to its source and to outer cancellation. |
| `ISyncLatestCoordinator<TResult>`, `SyncLatestLifecycle<TResult>` | A coordinator that combines the latest value of each source. |
| `SubscribeSourcesAsync` | Subscribes every slot of a coordinator. |
| `SubscribeToSlotAsync` | Subscribes one source to a coordinator's slot. |
| `SyncLatestSlot.CreateWitness` | Builds the witness for one slot. |
| `WitnessSubscription.SubscribeAsync` | Subscribes a witness and hands it back as the handle. |
| `TaskSignalSubscription.StartNew` | Runs a job that sends to a witness. |
| `TaskResultCompletionSource<T>` | Completes a terminal operator's result. |
| `DisposableAsync.Create` | An `IAsyncDisposable` from a method. |
| `DisposableAsyncSlot` | A field holding an `IAsyncDisposable`. |
| `ToDisposableAsync` | An `IDisposable` as an `IAsyncDisposable`. |
| `UnhandledExceptionHandler.Register` | Sets the process-wide handler. |
| `FireAndForgetHelper.Run` | Starts async work, sending its exception to the handler. |
| `TryGetValue` | Reads an `Optional<T>`. |
