---
Order: 30
---
# Best practices

Most streams work the first time. The problems show up later: a subscription that never stops, a screen that
freezes, a test that takes seconds. The habits on this page stop those problems before they start.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Concurrency;
using ReactiveUI.Primitives.Disposables;
using ReactiveUI.Primitives.Signals;
```

## Mark lambdas static

A lambda that uses a local variable from around it **captures** that variable. The compiler then creates a
closure object to hold the variable, and a new delegate, each time that line runs. A lambda that captures
nothing needs neither, and the compiler reuses one delegate.

Mark a lambda `static` when it should capture nothing. The compiler then rejects any capture, so a later edit
cannot add one by accident.

```csharp
IObservable<int> tens = source.Select(static x => x * 10);
```

When the lambda needs a value from outside, use the `With` form of the operator. It passes your value in as
the first parameter, so the lambda can still be `static`.

```csharp
var factor = 10;

// captures factor
IObservable<int> captured = source.Select(x => x * factor);

// captures nothing
IObservable<int> scaled = source.MapWith(factor, static (f, x) => x * f);
```

Both send `10 20 30` for `1, 2, 3`. The `With` forms are `MapWith`, `KeepWith`, `TapWith` and
`Signal.CreateWithState`.

This matters most where a chain is built often, such as once per list item. For a chain you build once at
start-up, write whichever reads better.

## Dispose every subscription

A subscription keeps running until the stream completes or you dispose it. A stream like a `Signal<T>` may
never complete. Its subscription then keeps your callback, and everything the callback uses, alive for as long
as the signal lives.

Give each class that subscribes a `MultipleDisposable`, add every subscription to it with `DisposeWith`, and
dispose it once.

```csharp
public sealed class PriceTicker : IDisposable
{
    private readonly MultipleDisposable _subscriptions = new();

    public PriceTicker(IObservable<decimal> prices)
    {
        prices.Subscribe(
                  static p => Console.WriteLine($"price {p}"),
                  static error => Console.WriteLine($"prices failed: {error.Message}"))
              .DisposeWith(_subscriptions);
    }

    public void Dispose() => _subscriptions.Dispose();
}
```

After `Dispose`, the ticker has left the signal, and later prices do not reach it. In a ReactiveUI view,
`WhenActivated` gives you this group and disposes it for you. See [when activated](../handbook/when-activated.md).

## Always pass an error callback

If you subscribe without an error callback, an error is **thrown** at the code that sent it. That is rarely the
place you want to handle it. Pass an error callback whenever the stream can fail. Any stream that touches a
network, a file or user input can fail. The [utility page](utility.md#subscribing) shows what happens without
one.

To recover from the error inside the chain instead, see [error handling](error-handling.md).

## Move to the UI thread last

`WitnessOn` hands values to the next step through a sequencer. Put `WitnessOn` just before `Subscribe`, so the
filtering and shaping runs on the thread the values arrived on, and only the screen update runs on the UI
thread.

```csharp
readings.Where(static r => r > 0)
        .Select(static r => $"{r} C")
        .WitnessOn(uiSequencer)
        .Subscribe(text => label.Text = text);
```

Each step after `WitnessOn` runs on the UI thread. Heavy work placed there makes the screen stutter.

## Never block on a stream

A call that blocks stops its thread until the stream finishes. On the UI thread, the app freezes. On a
thread-pool thread, the pool has one fewer thread for other work. When enough threads block at once, work
queues up and everything slows down. That is called **thread-pool starvation**.

Use `await` instead. It gives the thread back while it waits.

```csharp
// blocks the thread until the stream completes
List<int> blocked = Signal.Range(5, 3).ToEnumerable().ToList();

// frees the thread while it waits
int first = await Signal.Range(5, 3).FirstAsync();
```

Pass a `CancellationToken` to the task operators, so a stream that never sends cannot leave your code waiting
for ever. See [getting a result as a task](aggregation.md#getting-a-result-as-a-task).

## Prefer Serialize to Synchronize

When several threads push into one stream, your callback must handle one value at a time. `Serialize` does
that without holding a lock while your callback runs. `Synchronize` holds a lock the whole time, and can
**deadlock**: two threads each wait for the other to let go, and neither ever does.

Reach for `Synchronize` only when several streams must share one gate. See
[handling values one at a time](utility.md#handling-values-one-at-a-time).

## Share expensive work

A cold stream runs its work once per subscriber. Two parts of the screen subscribing to one web request means
two requests. Share the stream instead.

```csharp
ConnectableSignal<string> shared = profile.ReplayLive(1);
shared.Connect();

shared.Subscribe(name => header.Text = name);
shared.Subscribe(name => menu.Text = name);
```

The work runs once, and both subscribers get the result. `ReplayLive(1)` also hands the result to a subscriber
that arrives after it was sent. See [sharing one subscription](sharing.md).

## React only to real changes

A state's `Changed` stream reports every value from its source, even one that leaves the state the same. Add
`Unique` when you only want real changes, so the screen does not redraw for nothing.

```csharp
ReadOnlyState<int> tens = readings.ToReadOnlyState(0, static r => r / 10);

tens.Changed.Unique().Subscribe(t => Console.WriteLine($"changed to {t}"));

// readings sends 21, 25, 31
// prints: changed to 0, changed to 2, changed to 3
```

## Test time with a virtual clock

Code that waits, such as `Calm` or `Expire`, is slow and unreliable to test with real time. The time
operators take a sequencer. Pass a `VirtualClock` in your tests, and move time forward yourself.

```csharp
var clock = new VirtualClock();
var typed = new Signal<string>();
var searched = new List<string>();

typed.Calm(TimeSpan.FromMilliseconds(300), clock).Subscribe(searched.Add);

typed.OnNext("r");
typed.OnNext("rx");

clock.AdvanceBy(TimeSpan.FromMilliseconds(299));   // searched is empty
clock.AdvanceBy(TimeSpan.FromMilliseconds(1));     // searched holds "rx"
```

The test runs instantly and gives the same answer every time. The [time page](time.md) has more on
`VirtualClock`.

## Keep side effects in Subscribe

`Select`, `Where` and the other shaping operators should only work out values. Saving a file or updating the
screen inside them is easy to miss when you read the chain later, and it runs again for every subscriber. Put
that work in `Subscribe`. Use `Tap` for logging and debugging only.

## Every practice at a glance

| Do | Instead of | Why |
|---|---|---|
| Mark lambdas `static`, and use a `With` operator for outside values | Capturing locals | No closure, and no capture added by accident. |
| Add every subscription to a `MultipleDisposable` | Dropping the `IDisposable` | Stops callbacks leaking for the life of the source. |
| Pass an error callback | Subscribing to values only | The error is handled where you expect. |
| Put `WitnessOn` just before `Subscribe` | Putting it early in the chain | Only the screen update runs on the UI thread. |
| `await` a task operator | `ToEnumerable` | Frees the thread while waiting. |
| `Serialize` | `Synchronize` | No lock held while your callback runs. |
| `ReplayLive(1)` or another sharing operator | Subscribing to a cold stream twice | The work runs once. |
| `Changed.Unique()` | `Changed` on its own | Reacts only to real changes. |
| Pass a `VirtualClock` in tests | Waiting on real time | Fast, and the same every time. |
| Side effects in `Subscribe` | Side effects in `Select` or `Where` | Easy to find when you read the chain. |
