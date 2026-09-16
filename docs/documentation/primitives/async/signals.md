---
Order: 9
---
# Async signals

An async **signal** is a stream you send values into yourself, with `OnNextAsync`, `OnErrorResumeAsync` and
`OnCompletedAsync`. Every subscriber gets what you send. Each send returns a `ValueTask` that completes once the
subscribers have handled the value.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Async;
using ReactiveUI.Primitives.Async.Signals;
```

The factories are on `Signal` in `ReactiveUI.Primitives.Async.Signals`. Each hands back an `ISignalAsync<T>`.

| Factory | A new subscriber gets |
|---|---|
| `Signal.Create<T>()` | Only values sent after it subscribes. |
| `Signal.CreateBehavior(startValue)` | The latest value at once, or the start value if nothing has been sent. |
| `Signal.CreateReplayLatest<T>()` | The latest value at once, if one has been sent. |

## The three kinds

### `Signal.Create`

```csharp
ISignalAsync<string> chat = Signal.Create<string>();

await chat.OnNextAsync("sent before anyone listened", CancellationToken.None);

await using IAsyncDisposable subscription = await chat.SubscribeAsync(static m => Console.WriteLine(m));
await chat.OnNextAsync("hello", CancellationToken.None);
```

Output:

```text
hello
```

### `Signal.CreateBehavior`

```csharp
ISignalAsync<string> status = Signal.CreateBehavior("idle");

await status.OnNextAsync("busy", CancellationToken.None);

await using IAsyncDisposable subscription = await status.SubscribeAsync(static s => Console.WriteLine(s));
```

Output:

```text
busy
```

### `Signal.CreateReplayLatest`

```csharp
ISignalAsync<int> score = Signal.CreateReplayLatest<int>();

await using IAsyncDisposable before = await score.SubscribeAsync(static s => Console.WriteLine($"before: {s}"));
await score.OnNextAsync(7, CancellationToken.None);
await using IAsyncDisposable after = await score.SubscribeAsync(static s => Console.WriteLine($"after: {s}"));
```

Output:

```text
before: 7
after: 7
```

## Options

Each factory also takes an options record: `SignalCreationOptions`, `BehaviorSignalCreationOptions` or
`ReplayLatestSignalCreationOptions`. Each has a `Default`, and two settings.

### `PublishingOption`

`PublishingOption.Serial`, the default, runs subscribers one after another for each value. `PublishingOption.Concurrent`
runs them all at the same time, and the send completes when they have all finished. Choose `Concurrent` when subscribers
do slow, independent work, such as writing to two different services.

```csharp
ISignalAsync<int> serial = Signal.Create<int>();
ISignalAsync<int> concurrent = Signal.Create<int>(new SignalCreationOptions
{
    IsStateless = false,
    PublishingOption = PublishingOption.Concurrent,
});

foreach (var (name, signal) in new[] { ("serial", serial), ("concurrent", concurrent) })
{
    await using IAsyncDisposable a = await signal.SubscribeAsync(static async (_, ct) => await Task.Delay(200, ct));
    await using IAsyncDisposable b = await signal.SubscribeAsync(static async (_, ct) => await Task.Delay(200, ct));

    var clock = System.Diagnostics.Stopwatch.StartNew();
    await signal.OnNextAsync(1, CancellationToken.None);
    Console.WriteLine($"{name}: under 300 ms: {clock.ElapsedMilliseconds < 300}");
}
```

Output:

```text
serial: under 300 ms: False
concurrent: under 300 ms: True
```

### `IsStateless`

A signal normally ends for good when you complete it: a new subscriber gets the completion at once. A **stateless**
signal forgets that it ended. A new subscriber starts fresh, and later sends reach it. A stateless behavior signal also
starts again from its start value.

```csharp
ISignalAsync<int> counter = Signal.CreateBehavior(0, new BehaviorSignalCreationOptions
{
    IsStateless = true,
    PublishingOption = PublishingOption.Serial,
});

await counter.OnNextAsync(5, CancellationToken.None);
await counter.OnCompletedAsync(Result.Success);

await using IAsyncDisposable subscription = await counter.SubscribeAsync(static x => Console.WriteLine(x));
await counter.OnNextAsync(1, CancellationToken.None);
```

Output:

```text
0
1
```

### The types behind the options

The options choose one of eight public classes. You can also construct them directly.

| | Serial | Concurrent |
|---|---|---|
| Plain | `SerialSignalAsync<T>` | `ConcurrentSignalAsync<T>` |
| Replay latest | `SerialReplayLatestSignalAsync<T>` | `ConcurrentReplayLatestSignalAsync<T>` |
| Plain, stateless | `SerialStatelessSignalAsync<T>` | `ConcurrentStatelessSignalAsync<T>` |
| Replay latest, stateless | `SerialStatelessReplayLatestSignalAsync<T>` | `ConcurrentStatelessReplayLatestSignalAsync<T>` |

`CreateBehavior` builds a replay-latest type that starts with your value.

## Handing out one side of a signal

### `Values`

`Values` is the read side of a signal, as an `IObservableAsync<T>`. Hand it to code that should subscribe but never
send.

### `AsObserverAsync`

`AsObserverAsync` is the write side, as an `IObserverAsync<T>`. Hand it to code that should send but never subscribe.

### `MapValues`

`MapValues` builds a signal whose read side runs through operators you choose. Sending into it sends into the original.

```csharp
ISignalAsync<int> readings = Signal.Create<int>();
ISignalAsync<int> positive = readings.MapValues(static values => values.Where(static x => x > 0));

await using IAsyncDisposable subscription = await positive.SubscribeAsync(static x => Console.WriteLine($"reading {x}"));

IObserverAsync<int> sensor = readings.AsObserverAsync();
await sensor.OnNextAsync(-1, CancellationToken.None);
await sensor.OnNextAsync(5, CancellationToken.None);
```

Output:

```text
reading 5
```

## Sending to many witnesses yourself

If you write a signal of your own, the `Concurrent` class sends one notification to an array of witnesses at the same
time. A **witness** is the object that receives a stream's values: here an `IObserverAsync<T>`. It has
`ForwardOnNextConcurrently`, `ForwardOnErrorResumeConcurrently` and `ForwardOnCompletedConcurrently`, each taking an
`ImmutableArray<IObserverAsync<T>>`. The `ValueTask` completes when every witness has finished.

```csharp
using System.Collections.Immutable;

ISignalAsync<int> left = Signal.Create<int>();
ISignalAsync<int> right = Signal.Create<int>();

await using IAsyncDisposable l = await left.SubscribeAsync(static x => Console.WriteLine($"left got {x}"));
await using IAsyncDisposable r = await right.SubscribeAsync(static x => Console.WriteLine($"right got {x}"));

ImmutableArray<IObserverAsync<int>> witnesses = [left.AsObserverAsync(), right.AsObserverAsync()];
await Concurrent.ForwardOnNextConcurrently(witnesses, 4, CancellationToken.None);
```

Output, in either order:

```text
left got 4
right got 4
```

The other two send a resumable error and the end of the stream the same way:

```csharp
using System.Collections.Immutable;

ISignalAsync<int> first = Signal.Create<int>();
ISignalAsync<int> second = Signal.Create<int>();

await using IAsyncDisposable f = await first.SubscribeAsync(
    static (x, _) => default,
    static (error, _) => { Console.WriteLine($"first saw {error.Message}"); return default; },
    static result => { Console.WriteLine($"first ended: {result.IsSuccess}"); return default; });
await using IAsyncDisposable s = await second.SubscribeAsync(
    static (x, _) => default,
    static (error, _) => { Console.WriteLine($"second saw {error.Message}"); return default; },
    static result => { Console.WriteLine($"second ended: {result.IsSuccess}"); return default; });

ImmutableArray<IObserverAsync<int>> both = [first.AsObserverAsync(), second.AsObserverAsync()];
await Concurrent.ForwardOnErrorResumeConcurrently(both, new TimeoutException("slow"), CancellationToken.None);
await Concurrent.ForwardOnCompletedConcurrently(both, Result.Success);
```

Output, with the two witnesses in either order for each notification:

```text
first saw slow
second saw slow
first ended: True
second ended: True
```

## At a glance

| Member | What it does |
|---|---|
| `Signal.Create` | A signal that sends only new values. |
| `Signal.CreateBehavior` | A signal with a start value that replays the latest. |
| `Signal.CreateReplayLatest` | A signal that replays the latest value once one is sent. |
| `SignalCreationOptions` and its two relatives | Choose `PublishingOption` and `IsStateless`. |
| `Values` | The read side of a signal. |
| `AsObserverAsync` | The write side of a signal. |
| `MapValues` | A signal whose read side runs through operators. |
| `Concurrent.Forward...Concurrently` | Sends to an array of witnesses at the same time. |
