---
Order: 2
---
# Timing and threads

These helpers control *when* values go out and *which thread* runs your code. They add rate limits, batch values on
a pause, spot a stream that has gone quiet, and share one timer between many subscribers.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Concurrency;
using ReactiveUI.Primitives.Extensions;
using ReactiveUI.Primitives.Signals;
```

Most of them take a **sequencer**, the object that decides the clock a helper times with and the thread that runs
your code. Leave it out, and a helper uses `Sequencer.Default`, which times on a background thread. See
[sequencers and scheduling](../scheduling.md).

The examples mark the moment each value is sent with a comment, and show the output against the clock. To run them
without waiting for real time, pass a `VirtualClock`, as the [last section](#testing-with-a-virtual-clock) shows.

## Batching on a pause

### `BufferUntilIdle` and `BufferUntilInactive`

`BufferUntilIdle` collects values into a list. Once the stream has been quiet for the time you give, it sends the
list and starts a new one. When the stream completes, it sends the values it still holds. `BufferUntilInactive` is
the same helper under a second name.

Use it to save a burst of edits in one go.

```csharp
var edits = new Signal<int>();

edits.BufferUntilIdle(TimeSpan.FromMilliseconds(300))
     .Subscribe(static batch => Console.WriteLine(string.Join(", ", batch)));

edits.OnNext(1);   // at 0 ms
edits.OnNext(2);   // at 100 ms
edits.OnNext(3);   // at 200 ms
edits.OnNext(4);   // at 600 ms
```

Output:

| Time | Output |
|---|---|
| 500 ms | `1, 2, 3` |
| 900 ms | `4` |

## Limiting how often values go out

Each helper in this section limits a fast stream in a different way. Pick the one whose timeline matches what you
want.

| Helper | Sends |
|---|---|
| `ThrottleFirst` | The **first** value, then ignores the rest for a while. |
| `ThrottleOnScheduler` | The **newest** value once the stream has been quiet for a while. |
| `ThrottleDistinct` | The newest value after a quiet period, if it differs from the last one sent. |
| `DebounceImmediate` | The very first value at once, then the newest value after each quiet period. |
| `DebounceUntil` / `ThrottleUntilTrue` | A value that passes a test at once, others after a quiet period. |
| `Conflate` | The newest value, with at least a set gap between two values. |

### `ThrottleFirst`

`ThrottleFirst` sends a value, then ignores every value for the time you give. The first value after that window
goes out, and a new window starts. A double-clicked button runs its command once.

```csharp
var clicks = new Signal<int>();

clicks.ThrottleFirst(TimeSpan.FromMilliseconds(300))
      .Subscribe(static x => Console.WriteLine(x));

clicks.OnNext(1);   // at 0 ms
clicks.OnNext(2);   // at 100 ms
clicks.OnNext(3);   // at 350 ms
clicks.OnNext(4);   // at 400 ms
```

Output:

| Time | Output |
|---|---|
| 0 ms | `1` |
| 350 ms | `3` |

### `ThrottleOnScheduler`

`ThrottleOnScheduler` waits until the stream has been quiet for the time you give, then sends the newest value. It is
[`Calm`](../time.md) with a sequencer you must pass.

```csharp
var typing = new Signal<string>();

typing.ThrottleOnScheduler(TimeSpan.FromMilliseconds(300), Sequencer.Default)
      .Subscribe(static text => Console.WriteLine(text));

typing.OnNext("a");    // at 0 ms
typing.OnNext("ab");   // at 100 ms
```

Output:

| Time | Output |
|---|---|
| 400 ms | `ab` |

### `ThrottleDistinct`

`ThrottleDistinct` waits for a quiet period like `ThrottleOnScheduler`. It then sends the newest value only if it
differs from the last value it sent. The [helpers overview](index.md#your-first-helper) uses it to save a name.

```csharp
var search = new Signal<string>();

search.ThrottleDistinct(TimeSpan.FromMilliseconds(300))
      .Subscribe(static term => Console.WriteLine(term));

search.OnNext("cat");   // at 0 ms
search.OnNext("cat");   // at 500 ms
search.OnNext("dog");   // at 1000 ms
```

Output:

| Time | Output |
|---|---|
| 300 ms | `cat` |
| 1300 ms | `dog` |

The second `cat` went quiet too, but it matched the last value sent, so nothing went out.

### `DebounceImmediate`

`DebounceImmediate` sends the very first value at once. After that it waits for a quiet period before each value,
like `ThrottleOnScheduler`. A screen reacts to the first change with no delay, and calms down after.

```csharp
var slider = new Signal<int>();

slider.DebounceImmediate(TimeSpan.FromMilliseconds(300))
      .Subscribe(static x => Console.WriteLine(x));

slider.OnNext(1);   // at 0 ms
slider.OnNext(2);   // at 100 ms
slider.OnNext(3);   // at 200 ms
slider.OnNext(4);   // at 1000 ms
slider.OnNext(5);   // at 1100 ms
```

Output:

| Time | Output |
|---|---|
| 0 ms | `1` |
| 500 ms | `3` |
| 1400 ms | `5` |

Only the first value of the whole stream goes out at once. `4` arrived after a quiet gap, but it still waited.

### `DebounceUntil` and `ThrottleUntilTrue`

These wait for a quiet period, like `ThrottleOnScheduler`, except that a value that passes your test goes out at
once. A newer value replaces one still waiting. `ThrottleUntilTrue` does the same job and always times on
`Sequencer.Default`. `DebounceUntil` also takes a sequencer.

```csharp
var progress = new Signal<int>();

progress.DebounceUntil(TimeSpan.FromMilliseconds(300), static percent => percent == 100)
        .Subscribe(static p => Console.WriteLine(p));

progress.OnNext(40);    // at 0 ms
progress.OnNext(100);   // at 100 ms
progress.OnNext(10);    // at 150 ms, a new job starts
```

Output:

| Time | Output |
|---|---|
| 100 ms | `100` |
| 450 ms | `10` |

`100` passed the test, so it went out straight away. `40` was still waiting when `100` arrived, so it was dropped.

### `Conflate`

`Conflate` keeps at least a set gap between two values. When values arrive faster than that, it holds only the
newest and sends it once the gap has passed. A value that arrives after a long quiet gap goes out at once. You must
pass a sequencer.

```csharp
var prices = new Signal<int>();

prices.Conflate(TimeSpan.FromMilliseconds(300), Sequencer.Default)
      .Subscribe(static p => Console.WriteLine(p));

prices.OnNext(1);   // at 0 ms
prices.OnNext(2);   // at 100 ms
prices.OnNext(3);   // at 200 ms
prices.OnNext(4);   // at 700 ms
prices.OnNext(5);   // at 750 ms
```

Output:

| Time | Output |
|---|---|
| 300 ms | `3` |
| 700 ms | `4` |
| 1000 ms | `5` |

The first value is also held for the full gap.

### `SampleLatest`

`SampleLatest` sends the newest value each time a second stream, the trigger, sends anything. If the source has not
sent a value yet, a trigger does nothing. A trigger with no new value in between sends the same value again.

```csharp
var position = new Signal<int>();
var frame = new Signal<object>();

position.SampleLatest(frame)
        .Subscribe(static p => Console.WriteLine(p));

frame.OnNext("tick");      // nothing to send yet
position.OnNext(1);
position.OnNext(2);
frame.OnNext("tick");
frame.OnNext("tick");
```

Output:

```text
2
2
```

[`Probe`](../time.md) samples on a timer instead of a trigger stream.

## Spotting a quiet stream

### `DetectStale`

`DetectStale` wraps each value as an update. If the stream sends nothing for the time you give, it sends one
**stale** marker. The next value is an update again. Each item is a `Stale<T>`: check `IsStale`, and read `Update`
only when `IsStale` is `false`.

A price feed that stops updating can grey out its display.

```csharp
var feed = new Signal<decimal>();

feed.DetectStale(TimeSpan.FromMilliseconds(300), Sequencer.Default)
    .Subscribe(static item => Console.WriteLine(item.IsStale ? "stale" : $"price {item.Update}"));

feed.OnNext(10m);   // at 0 ms
feed.OnNext(11m);   // at 200 ms
feed.OnNext(12m);   // at 1000 ms
```

Output:

| Time | Output |
|---|---|
| 0 ms | `price 10` |
| 200 ms | `price 11` |
| 500 ms | `stale` |
| 1000 ms | `price 12` |

### `Heartbeat`

`Heartbeat` wraps each value as an update, and sends a **heartbeat** every period while the source is quiet. Each
item is a `Heartbeat<T>`: check `IsHeartbeat`, and read `Update` when it is `false`. A connection that must show it
is still alive, even with no data to send, is the usual fit.

```csharp
var messages = new Signal<string>();

messages.Heartbeat(TimeSpan.FromMilliseconds(300), Sequencer.Default)
        .Subscribe(static item => Console.WriteLine(item.IsHeartbeat ? "heartbeat" : item.Update));
```

With no messages, the output is:

| Time | Output |
|---|---|
| 300 ms | `heartbeat` |
| 600 ms | `heartbeat` |
| 900 ms | `heartbeat` |

## Timers and scheduled values

### `SyncTimer`

`SyncTimer` is an extension on `TimeSpan`. It builds a timer that sends the current `DateTime` every period. Every
call with the same period and sequencer returns the **same** timer. A hundred rows on screen that each show a clock
share one timer, instead of running a hundred.

```csharp
IObservable<DateTime> first = TimeSpan.FromSeconds(1).SyncTimer();
IObservable<DateTime> second = TimeSpan.FromSeconds(1).SyncTimer();

Console.WriteLine(ReferenceEquals(first, second));

first.Subscribe(static now => Console.WriteLine($"row 1: {now:T}"));
second.Subscribe(static now => Console.WriteLine($"row 2: {now:T}"));
```

Output:

```text
True
```

Then both rows update together, once a second, starting straight away.

### `Schedule` on a value

`Schedule` on a plain value builds a stream that sends that value later, through a sequencer, then completes. Give it
a `TimeSpan` to wait, or a `DateTimeOffset` to send at a set time. You can also pass an `Action<T>` to run on the value
as it goes out, or a `Func<T, T>` to change it.

```csharp
42.Schedule(TimeSpan.FromMilliseconds(200), Sequencer.Default)
  .Subscribe(static x => Console.WriteLine(x));   // 42, at 200 ms

"ready".Schedule(TimeSpan.FromMilliseconds(100), Sequencer.Default, static s => Console.WriteLine($"about to send {s}"))
       .Subscribe(static s => Console.WriteLine(s));

3.Schedule(Sequencer.Default, static x => x * 2)
 .Subscribe(static x => Console.WriteLine(x));    // 6
```

### `Schedule` on a stream

The same overloads exist on `IObservable<T>`. They send each value of the stream through the sequencer, after the
wait if you give one.

```csharp
IObservable<int> numbers = Signal.Range(1, 2);

numbers.Schedule(TimeSpan.FromMilliseconds(200), Sequencer.Default)
       .Subscribe(static x => Console.WriteLine(x));   // 1 and 2, at 200 ms
```

Call these on a variable typed as `IObservable<T>`, as above. On a variable typed as a concrete signal such as
`Signal<int>`, C# picks the value overload and schedules the signal object itself.

### `While`

`While` is an extension on a `Func<bool>` condition. It runs an action for as long as the condition returns `true`,
and sends `RxVoid` after each run. When the condition returns `false`, it completes. Pass a sequencer to run each
turn through it.

```csharp
var count = 0;
Func<bool> notDone = () => count < 3;

notDone.While(() => count++)
       .Subscribe(_ => Console.WriteLine($"ran {count}"), static () => Console.WriteLine("completed"));
```

Output:

```text
ran 1
ran 2
ran 3
completed
```

The lambdas here read and change `count`, so they cannot be `static`.

### `Start`

`Start` runs a method through a sequencer and sends its result as a stream. The `Action` form sends `RxVoid` when the
action finishes. The `Func<TResult>` form, `ReactiveExtensions.Start`, sends the value the method returns.

Pass `null` as the sequencer to run the method straight away on the current thread, as below. Pass
`Sequencer.Default` to run it on a background thread instead.

```csharp
Action backup = static () => Console.WriteLine("backing up");

backup.Start(null)
      .Subscribe(static _ => Console.WriteLine("backup finished"));

ReactiveExtensions.Start(static () => 6 * 7, null)
                  .Subscribe(static answer => Console.WriteLine(answer));
```

Output:

```text
backing up
backup finished
42
```

[`Signal.Start`](../creation-factories.md) does the same job as a factory.

### `Using`

`Using` is an extension on any `IDisposable`. It runs your lambda with the object, sends the result, completes, and
then disposes the object. The `Action<T>` form sends `RxVoid`. The `Func<T, TResult>` form sends what your lambda
returns.

```csharp
using var file = new MemoryStream([1, 2, 3]);

file.Using(static stream => stream.Length)
    .Subscribe(static length => Console.WriteLine($"{length} bytes"));
```

Output:

```text
3 bytes
```

The object is disposed after your subscriber has seen the value and the completion.

## Choosing a thread

### `ObserveOnSafe`

`ObserveOnSafe` runs your callbacks through a sequencer, like [`WitnessOn`](../utility.md). When the sequencer is
`null`, it hands back the stream unchanged. A class that takes an optional sequencer can call it without an `if`.

```csharp
public sealed class PriceView(IObservable<decimal> prices, ISequencer? uiSequencer)
{
    public IDisposable Start() =>
        prices.ObserveOnSafe(uiSequencer)
              .Subscribe(static price => Console.WriteLine(price));
}
```

### `ObserveOnIf`

`ObserveOnIf` chooses a sequencer from a condition. With a `bool`, it decides once. With an `IObservable<bool>`, it
switches each time the condition changes. Give it one sequencer to use while the condition is `true`, and it runs
callbacks straight away otherwise. Or give it two, one for each case.

```csharp
var updates = new Signal<int>();
var isVisible = new Signal<bool>();

updates.ObserveOnIf(isVisible, Sequencer.Default)
       .Subscribe(static x => Console.WriteLine(x));
```

While `isVisible` is `true`, each value goes through `Sequencer.Default`. While it is `false`, or before it has sent
anything, each value runs straight away on the sending thread.

### `ScheduleSafe`

`ScheduleSafe` runs an action through a sequencer that may be `null`. See [sequencers](../scheduling.md).

## Testing with a virtual clock

Every helper on this page that takes a sequencer can take a `VirtualClock`. A `VirtualClock` only moves when you tell
it to, so a test runs in no time and gives the same result every run. See
[`VirtualClock`](../scheduling.md).

```csharp
var clock = new VirtualClock();
var start = clock.Now;
var feed = new Signal<decimal>();

feed.DetectStale(TimeSpan.FromMilliseconds(300), clock)
    .Subscribe(item => Console.WriteLine(
        $"{(clock.Now - start).TotalMilliseconds} ms: {(item.IsStale ? "stale" : item.Update.ToString())}"));

feed.OnNext(10m);
clock.AdvanceBy(TimeSpan.FromMilliseconds(1000));
```

Output:

```text
0 ms: 10
300 ms: stale
```

## Every helper on this page at a glance

| Helper | Second name | What it does |
|---|---|---|
| `BufferUntilIdle` | `BufferUntilInactive` | Sends a list of values each time the stream goes quiet. |
| `ThrottleFirst` | — | Sends a value, then ignores values for a while. |
| `ThrottleOnScheduler` | — | Sends the newest value after a quiet period. |
| `ThrottleDistinct` | — | The same, skipping a value equal to the last one sent. |
| `DebounceImmediate` | — | The first value at once, then the newest after each quiet period. |
| `DebounceUntil` | `ThrottleUntilTrue` | A value that passes a test at once, others after a quiet period. |
| `Conflate` | — | The newest value, at least a set gap apart. |
| `SampleLatest` | — | The newest value each time a trigger stream sends. |
| `DetectStale` | — | Marks the stream stale after a quiet period. |
| `Heartbeat` | — | Sends a heartbeat every period while the source is quiet. |
| `SyncTimer` | — | A timer shared by every caller with the same period. |
| `Schedule` | — | Sends a value, or each value of a stream, later through a sequencer. |
| `While` | — | Runs an action while a condition holds. |
| `Start` | — | Runs a method through a sequencer and sends its result. |
| `Using` | — | Runs a lambda with a disposable object, then disposes it. |
| `ObserveOnSafe` | — | `WitnessOn`, or nothing when the sequencer is `null`. |
| `ObserveOnIf` | — | Chooses a sequencer from a condition. |

`DebounceUntil` and `ThrottleUntilTrue` differ only in that `DebounceUntil` also takes a sequencer.

## The types behind these helpers

`Stale<T>` and `Heartbeat<T>` are record structs in `ReactiveUI.Primitives.Extensions`, with the interfaces
`IStale<T>` and `IHeartbeat<T>`. `SampleLatestObservable<T>` is a public class in
`ReactiveUI.Primitives.Extensions.Operators`. The other timing helpers build internal types, so call the helper.

`TimerSinkState<T>` is a public building block for an operator of your own that runs a timer. It holds the timer in a
`SwapDisposable` and queues values, errors and completion so that no lock is held while your observer runs.
