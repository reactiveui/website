---
Order: 8
---
# Async utility and sharing

These operators run side effects, share one subscription between several subscribers, and choose where your callbacks
run.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Async;
using ReactiveUI.Primitives.Async.Signals;
```

## Side effects

### `Tap`

`Tap` runs your code on each value and passes the value on unchanged. It suits logging. Other overloads also run code
for resumable errors and for the end of the stream, with plain or async lambdas.

### `DoOnSubscribe`

`DoOnSubscribe` runs an action each time someone subscribes, before the source is subscribed. It can be async.

### `OnDispose`

`OnDispose` runs an action when the subscription is torn down. It can be async.

```csharp
List<int> values = await SignalAsync.Range(1, 2)
    .Tap(static x => Console.WriteLine($"saw {x}"))
    .DoOnSubscribe(static () => Console.WriteLine("subscribed"))
    .OnDispose(static () => Console.WriteLine("disposed"))
    .ToListAsync();

await Task.Delay(20);
```

Output:

```text
subscribed
saw 1
saw 2
disposed
```

## Sharing one subscription

A cold stream starts its work for each subscriber. To have several subscribers share one run of the work, turn it into a
**connectable** stream: a `ConnectableSignalAsync<T>`. It subscribes to the source only when you call `ConnectAsync`,
and passes every value to all its subscribers. Dispose the handle `ConnectAsync` hands back to disconnect.

### `Publish`

`Publish` shares values as they arrive. A subscriber that joins late misses the values sent before it joined. Give
`Publish` a starting value, and each subscriber gets the latest value at once.

```csharp
ISignalAsync<int> source = Signal.Create<int>();
ConnectableSignalAsync<int> shared = source.Publish();

await using IAsyncDisposable first = await shared.SubscribeAsync(static x => Console.WriteLine($"first {x}"));
await using IAsyncDisposable connection = await shared.ConnectAsync(CancellationToken.None);

await source.OnNextAsync(1, CancellationToken.None);

await using IAsyncDisposable second = await shared.SubscribeAsync(static x => Console.WriteLine($"second {x}"));

await source.OnNextAsync(2, CancellationToken.None);
```

Output:

```text
first 1
first 2
second 2
```

### `ReplayLatestPublish`

`ReplayLatestPublish` also sends the latest value to a subscriber that joins late.

```csharp
ISignalAsync<int> source = Signal.Create<int>();
ConnectableSignalAsync<int> shared = source.ReplayLatestPublish();

await using IAsyncDisposable connection = await shared.ConnectAsync(CancellationToken.None);
await source.OnNextAsync(1, CancellationToken.None);

await using IAsyncDisposable late = await shared.SubscribeAsync(static x => Console.WriteLine($"late {x}"));
```

Output:

```text
late 1
```

### `StatelessPublish` and `StatelessReplayLatestPublish`

The **stateless** forms keep nothing once the source ends. A subscriber that joins after the end starts fresh: it does
not get the completion, and `StatelessPublish(initialValue)` sends it the starting value again. The stateful forms send a
late subscriber the completion straight away.

```csharp
ISignalAsync<int> source = Signal.Create<int>();
ConnectableSignalAsync<int> shared = source.StatelessPublish(0);

await using IAsyncDisposable connection = await shared.ConnectAsync(CancellationToken.None);
await source.OnNextAsync(5, CancellationToken.None);
await source.OnCompletedAsync(Result.Success);

await using IAsyncDisposable late = await shared.SubscribeAsync(static x => Console.WriteLine($"late {x}"));
await Task.Delay(20);
```

Output:

```text
late 0
```

### `Multicast`

`Multicast` shares through an async signal you choose. The other forms each build one for you. See
[async signals](signals.md).

```csharp
ConnectableSignalAsync<int> shared = Signal.Create<int>().Multicast(Signal.CreateBehavior(-1));

await using IAsyncDisposable subscription = await shared.SubscribeAsync(static x => Console.WriteLine(x));
```

Output:

```text
-1
```

### `RefCount`

`RefCount` connects when the first subscriber arrives, and disconnects when the last one leaves. You never call
`ConnectAsync` yourself.

```csharp
var runs = 0;
IObservableAsync<long> ticks = SignalAsync.Defer(() => { runs++; return SignalAsync.Every(TimeSpan.FromMilliseconds(20)); })
                                          .Publish()
                                          .RefCount();

IAsyncDisposable a = await ticks.SubscribeAsync(static _ => { });
IAsyncDisposable b = await ticks.SubscribeAsync(static _ => { });
await Task.Delay(50);
await a.DisposeAsync();
await b.DisposeAsync();

Console.WriteLine($"timers started: {runs}");
```

Output:

```text
timers started: 1
```

### `ReplayLastOnSubscribe`

`ReplayLastOnSubscribe` shares the source in one call. Each new subscriber gets the latest value at once, or the
starting value you give if nothing has arrived yet.

```csharp
ISignalAsync<int> source = Signal.Create<int>();
IObservableAsync<int> latest = source.ReplayLastOnSubscribe(0);

await using IAsyncDisposable early = await latest.SubscribeAsync(static x => Console.WriteLine($"early {x}"));
await source.OnNextAsync(7, CancellationToken.None);
await using IAsyncDisposable late = await latest.SubscribeAsync(static x => Console.WriteLine($"late {x}"));
```

Output:

```text
early 0
early 7
late 7
```

## Choosing where callbacks run

An **async context** says where code continues after an `await`: on a `SynchronizationContext`, such as a UI thread,
on a `TaskScheduler`, or through a [sequencer](../scheduling.md). `AsyncContext` holds one of these.
`AsyncContext.From(...)` builds one, `AsyncContext.GetCurrent()` reads the one you are running in, and
`AsyncContext.Default` is the thread pool.

### `WitnessOn`

`WitnessOn` runs your callbacks through a context: an `AsyncContext`, a `SynchronizationContext`, a `TaskScheduler`,
or a sequencer. In an app, pass the UI's context so a subscriber can update the screen.

```csharp
var uiContext = SynchronizationContext.Current;   // captured on the UI thread in an app

IObservableAsync<int> onUi = uiContext is null
    ? SignalAsync.Range(1, 2)
    : SignalAsync.Range(1, 2).WitnessOn(uiContext);

Console.WriteLine(string.Join(", ", await onUi.ToListAsync()));
```

Output:

```text
1, 2
```

Pass `forceYielding: true` to always hop, even when the code is running in that context.

### `ObserveOnSafe` and `ObserveOnIf`

`ObserveOnSafe` is `WitnessOn` that hands back the stream unchanged when the context is `null`, so the `if` above is not
needed. `ObserveOnIf` switches context only when a `bool` is `true`.

```csharp
IObservableAsync<int> maybeOnUi = SignalAsync.Range(1, 2).ObserveOnSafe(SynchronizationContext.Current is null ? null : AsyncContext.From(SynchronizationContext.Current));
Console.WriteLine(string.Join(", ", await maybeOnUi.ToListAsync()));

var runInBackground = false;
IObservableAsync<int> maybeInBackground = SignalAsync.Range(3, 2).ObserveOnIf(runInBackground, TaskScheduler.Default);
Console.WriteLine(string.Join(", ", await maybeInBackground.ToListAsync()));
```

Output:

```text
1, 2
3, 4
```

### `Wrap`

`Wrap` is an extension on a witness you write yourself. It hands back a witness that receives one notification at a
time and nothing after it is disposed. If your `OnNextAsync` throws, the wrapper passes the exception to your own
`OnErrorResumeAsync` instead of throwing it at the sender.

```csharp
IObserverAsync<int> safe = new FragileWitness().Wrap();

await safe.OnNextAsync(1, CancellationToken.None);
await safe.OnNextAsync(2, CancellationToken.None);
Console.WriteLine("the sender carried on");

public sealed class FragileWitness : IObserverAsync<int>
{
    public ValueTask OnNextAsync(int value, CancellationToken cancellationToken)
    {
        if (value == 2)
        {
            throw new InvalidOperationException("cannot handle 2");
        }

        Console.WriteLine($"handled {value}");
        return ValueTask.CompletedTask;
    }

    public ValueTask OnErrorResumeAsync(Exception error, CancellationToken cancellationToken)
    {
        Console.WriteLine($"reported: {error.Message}");
        return ValueTask.CompletedTask;
    }

    public ValueTask OnCompletedAsync(Result result) => ValueTask.CompletedTask;

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
```

Output:

```text
handled 1
reported: cannot handle 2
the sender carried on
```

### `Yield`

`Yield` yields before each value, so a producer that sends values very fast cannot keep the thread from other work.

### `IsSameAsCurrentAsyncContext`

`IsSameAsCurrentAsyncContext` tells you whether an `AsyncContext` is the one your code is running in now.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.Range(1, 3).Yield().ToListAsync()));
Console.WriteLine(AsyncContext.GetCurrent().IsSameAsCurrentAsyncContext());
```

Output:

```text
1, 2, 3
True
```

## Every operator on this page at a glance

| Operator | Second name | What it does |
|---|---|---|
| `Tap` | `Do` | Runs code on each value. |
| `DoOnSubscribe` | — | Runs code on each subscribe. |
| `OnDispose` | — | Runs code when the subscription is torn down. |
| `Publish` | — | Shares values as they arrive, optionally with a starting value. |
| `ReplayLatestPublish` | — | Shares, and sends the latest value to late subscribers. |
| `StatelessPublish` / `StatelessReplayLatestPublish` | — | The same, keeping nothing once the source ends. |
| `Multicast` | — | Shares through a signal you choose. |
| `RefCount` | — | Connects on the first subscriber and disconnects after the last. |
| `ReplayLastOnSubscribe` | — | Shares, sending the latest or a starting value to each subscriber. |
| `WitnessOn` | — | Runs callbacks through a context. |
| `ObserveOnSafe` | — | `WitnessOn`, or nothing for a `null` context. |
| `ObserveOnIf` | — | `WitnessOn` when a `bool` is `true`. |
| `Wrap` | — | Makes a witness of your own safe to call. |
| `Yield` | — | Yields before each value. |
| `IsSameAsCurrentAsyncContext` | — | Whether a context is the current one. |
| `ToAsyncSignal` on a stream | — | Hands back the same stream. |
