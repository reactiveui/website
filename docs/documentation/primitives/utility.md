---
Order: 8
---
# Utility

These operators do not change what a stream sends. They change how you listen to it: how you subscribe, which
thread runs your code, how you peek at values on the way past, and how you clean up afterwards.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;
```

## Your first look inside a chain

You have built a chain of operators, and the result is not what you expected. You want to see what goes
through each step, without changing anything.

**1. Build the chain.** This one keeps the odd numbers and doubles them.

```csharp
IObservable<int> doubledOdds = Signal.Range(1, 5)
    .Where(x => x % 2 == 1)
    .Select(x => x * 2);
```

**2. Add a `Tap` wherever you want to look.** `Tap` runs your code for each value, then passes the value on
unchanged. It is like holding a window open onto the chain.

```csharp
IObservable<int> doubledOdds = Signal.Range(1, 5)
    .Tap(x => Console.WriteLine($"in:   {x}"))
    .Where(x => x % 2 == 1)
    .Tap(x => Console.WriteLine($"kept: {x}"))
    .Select(x => x * 2);
```

**3. Subscribe.**

```csharp
doubledOdds.Subscribe(x => Console.WriteLine($"out:  {x}"));
```

Output:

```text
in:   1
kept: 1
out:  2
in:   2
in:   3
kept: 3
out:  6
in:   4
in:   5
kept: 5
out:  10
```

Each value travels the whole chain before the next one starts. `2` and `4` went in, but never got past `Where`,
so no `kept` line follows them. When you have found the problem, delete the `Tap` calls. The chain works the
same without them.

## Subscribing

### `Subscribe`

`Subscribe` starts a stream and runs your code on what it sends. It takes up to three callbacks: one for each
value, one for an error, and one for completion. It hands back an `IDisposable`. Dispose it to stop listening.

```csharp
IDisposable subscription = Signal.Range(1, 3).Subscribe(
    x => Console.WriteLine($"value {x}"),
    error => Console.WriteLine($"error {error.Message}"),
    () => Console.WriteLine("completed"));
```

Output:

```text
value 1
value 2
value 3
completed
```

You can leave out the callbacks you do not need. `Subscribe()` with none at all starts the stream just for its
side effects.

> [!WARNING]
> If you leave out the error callback, an error is not quietly ignored. It is **thrown** at the code that sent
> it.
>
> ```csharp
> var live = new Signal<int>();
> live.Subscribe(x => Console.WriteLine(x));
>
> live.OnError(new InvalidOperationException("boom"));   // throws InvalidOperationException here
> ```
>
> The same happens when your value callback throws. The exception comes out of the `OnNext` call that sent the
> value. Pass an error callback whenever a stream might fail.

### `SubscribePrimitives`

`SubscribePrimitives` does exactly what `Subscribe` does, with the same callbacks. It exists for one reason.
If your project also references System.Reactive, both libraries give streams a `Subscribe` method, and C#
cannot tell which one you mean. Calling `SubscribePrimitives` picks this library's version.

```csharp
IDisposable subscription = Signal.Range(1, 3).SubscribePrimitives(
    x => Console.WriteLine($"value {x}"),
    () => Console.WriteLine("completed"));
```

### `SubscribeSafe`

`SubscribeSafe` protects the code that sends values from mistakes in your callbacks. If your value callback
throws, the exception goes to your error callback instead of back to the sender. Your subscription then ends.

```csharp
var readings = new Signal<int>();

readings.SubscribeSafe(
    x =>
    {
        if (x < 0)
        {
            throw new InvalidOperationException("reading below zero");
        }

        Console.WriteLine(x);
    },
    error => Console.WriteLine($"stopped: {error.Message}"));

readings.OnNext(5);
readings.OnNext(-1);
readings.OnNext(7);
Console.WriteLine("producer carried on");
```

Output:

```text
5
stopped: reading below zero
producer carried on
```

The callback threw on `-1`. The error went to your error callback, and `readings.OnNext(-1)` returned normally.
`7` was not printed, because the subscription had ended.

`SubscribeSafe` also takes an `IObserver<T>`, or just an error callback. `SubscribeSafePrimitives` is the
name-clash-free version, like `SubscribePrimitives`.

> [!NOTE]
> `LinqExtensions` also has 14 static `SubscribeSafe` methods that end in a `params byte[]` or `params bool[]`
> argument. That last argument is only there to help C# choose between overloads for nullable types. It is
> never read, so leave it out. Call `source.SubscribeSafe(...)` as normal.

## Choosing where your code runs

A UI app has one UI thread, the only one allowed to change what is on screen. A **sequencer** decides which thread runs a piece of work,
and when. The [time page](time.md) shows a sequencer called `VirtualClock`, which only runs work when you tell
it to. The examples below use it so you can see exactly when things happen.

### `WitnessOn`

`WitnessOn` hands each value to your callback through a sequencer. The value is sent straight away, but your
callback runs when the sequencer gets to it.

```csharp
using ReactiveUI.Primitives.Concurrency;

var clock = new VirtualClock();
var live = new Signal<int>();

live.WitnessOn(clock).Subscribe(x => Console.WriteLine($"got {x}"));

live.OnNext(1);
Console.WriteLine("sent 1");

clock.AdvanceBy(TimeSpan.FromTicks(1));
```

Output:

```text
sent 1
got 1
```

`1` was sent, but your callback did not run until the clock moved. In an app, you pass the UI thread's
sequencer, so a value that arrives on a background thread is handled on the UI thread. Values keep their
order.

### `SubscribeOn`

`SubscribeOn` moves the **subscribing** onto a sequencer, rather than the values. Use it when starting a
stream does slow work, such as opening a connection, and you do not want that work on the thread that called
`Subscribe`.

```csharp
var clock = new VirtualClock();

IObservable<int> numbers = Signal.Lazy(() =>
{
    Console.WriteLine("subscribing");
    return Signal.Range(1, 2);
});

numbers.SubscribeOn(clock).Subscribe(x => Console.WriteLine($"got {x}"));
Console.WriteLine("called Subscribe");

clock.AdvanceBy(TimeSpan.FromTicks(1));
```

Output:

```text
called Subscribe
subscribing
got 1
got 2
```

`Subscribe` returned at once. The stream did not start until the clock moved.

### `WitnessOn` against `SubscribeOn`

| Operator | Moves onto the sequencer | Use it for |
|---|---|---|
| `WitnessOn` | Your callback for each value. | Updating the screen from a background stream. |
| `SubscribeOn` | The work of starting the stream. | Keeping slow start-up work off the calling thread. |

## Handling values one at a time

Several threads can push values into the same stream at once. A plain `Signal<T>` lets those calls overlap,
so your callback can be running twice at the same moment. Code that is not written for that can go wrong in
ways that are hard to find.

### `Synchronize`

`Synchronize` makes sure your callback handles one value at a time. It takes a `lock` around each
callback, so other threads wait their turn.

```csharp
var live = new Signal<int>();
var busy = 0;
var overlaps = 0;

live.Synchronize().Subscribe(x =>
{
    if (Interlocked.Increment(ref busy) > 1)
    {
        Interlocked.Increment(ref overlaps);
    }

    Thread.SpinWait(1000);
    Interlocked.Decrement(ref busy);
});

Parallel.For(0, 4, _ =>
{
    for (var i = 0; i < 200; i++)
    {
        live.OnNext(i);
    }
});

Console.WriteLine($"overlaps: {overlaps}");   // overlaps: 0
```

Four threads sent 800 values between them, and the callback never ran twice at once. Run the same code without
`Synchronize` and you see hundreds of overlaps, a different number each time.

Pass your own gate to `Synchronize` to share it between several streams. Then their callbacks take turns with
each other as well. Any `object` works as a gate on every version of .NET. On .NET 9 and later you can also
pass a `Lock`, the lock type .NET 9 added.

### `Serialize`

`Serialize` also hands your callback one value at a time, and in the order the values were sent. The difference
is that it does not hold a lock **while your callback runs**.

That matters when your callback waits on another thread. Say your callback asks the UI thread to update the
screen, and waits for it. If the UI thread is itself trying to send a value into the same stream, a lock can
leave each thread waiting for the other for ever. That is a **deadlock**, and your app freezes. `Serialize`
avoids it. A value that arrives while another is being handled joins a queue, instead of making its thread
wait.

```csharp
live.Serialize().Subscribe(x => Console.WriteLine(x));
```

Swap `Synchronize` for `Serialize` in the example above, and the result is the same: `overlaps: 0`.

### `Synchronize` against `Serialize`

| Operator | One value at a time | Holds a lock while your callback runs | Can deadlock if your callback waits on another thread |
|---|---|---|---|
| `Synchronize` | Yes | Yes | Yes |
| `Serialize` | Yes | No | No |

Prefer `Serialize`. Reach for `Synchronize` when you need to share one gate between several streams.

## Watching values go past

### `Tap`

`Tap` runs your code for each value and passes the value on unchanged. The walkthrough at the top of this page
shows it. Give it three callbacks to see errors and completion too.

```csharp
Signal.Range(1, 2)
      .Tap(x => Console.WriteLine($"saw {x}"),
           error => Console.WriteLine($"saw error {error.Message}"),
           () => Console.WriteLine("saw completion"))
      .Subscribe(x => Console.WriteLine($"got {x}"), () => Console.WriteLine("completed"));
```

Output:

```text
saw 1
got 1
saw 2
got 2
saw completion
completed
```

`Tap` sees each notification just before the next step does. Use it for logging, counting, or debugging. Avoid
putting important work in a `Tap`, because it is easy to overlook when reading the chain later.

### `TapWith`

`TapWith` passes a state value into the callback as its first argument. That lets you mark the lambda `static`,
so it captures nothing and allocates no closure. See [mark lambdas `static`](best-practices.md#mark-lambdas-static).

```csharp
var log = new List<int>();

Signal.Range(1, 3)
      .TapWith(log, static (list, x) => list.Add(x))
      .Subscribe();

Console.WriteLine(string.Join(", ", log));   // 1, 2, 3
```

## Passing a stream on

### `AsObservable`

`AsObservable` hands you a **read-only view** of a stream. The view passes every value on, but it is a different
object. Code that receives the view cannot cast it back to the signal behind it, so it cannot push values in.

Use it when a class owns a signal and wants to let other code listen without letting that code send.

```csharp
var signal = new Signal<int>();
signal.Subscribe(x => Console.WriteLine($"received {x}"));

IObservable<int> readOnly = signal.AsObservable();
readOnly.Subscribe(x => Console.WriteLine($"view subscriber got {x}"));

Console.WriteLine(readOnly is Signal<int>);       // False
Console.WriteLine(readOnly is IObserver<int>);    // False

signal.OnNext(3);
```

Output:

```text
False
False
received 3
view subscriber got 3
```

The view is not a `Signal<int>`, and it is not an `IObserver<int>`, so there is nothing to push values into.
Calling `AsObservable` on a view hands back that same view rather than wrapping it again.

### `ToSignal` on a stream

`ToSignal` on a stream checks it is not `null`, then hands back the same stream. It is the do-nothing end of
the `ToSignal` family. The forms on a collection and a task, which do build a stream, are on the
[creation factories page](creation-factories.md).

## Cleaning up subscriptions

Every subscription is an `IDisposable`. A subscription you never dispose keeps running, and keeps your
callbacks and everything they use alive in memory. These helpers make disposal easier to get right.

### `DisposeWith` a group

`DisposeWith` with a `MultipleDisposable` adds your subscription to that group, and hands the subscription back.
That lets you add it on the same line that creates it. Disposing the group disposes everything in it.

```csharp
using ReactiveUI.Primitives.Disposables;

var subscriptions = new MultipleDisposable();

Signal.Range(1, 3)
      .Subscribe(x => Console.WriteLine(x))
      .DisposeWith(subscriptions);

subscriptions.Dispose();   // disposes every subscription in the group
```

### `DisposeWith` on its own

`DisposeWith` with no argument wraps a disposable in a `SingleDisposable`. However many times you dispose the
wrapper, it disposes what is inside only once.

```csharp
var cleanup = new ActionDisposable(() => Console.WriteLine("closed"));

SingleDisposable once = cleanup.DisposeWith();
once.Dispose();
once.Dispose();   // prints closed only once
```

Give it an action, and it runs your action just **before** it disposes what is inside:

```csharp
SingleDisposable wrapped = new ActionDisposable(() => Console.WriteLine("closed"))
    .DisposeWith(() => Console.WriteLine("about to close"));

wrapped.Dispose();
```

Output:

```text
about to close
closed
```

## Pushing a collection into an observer

### `FastForEach`

`FastForEach` sends every item of a collection to an observer, in order. It reads arrays and lists by position,
which is quicker than looping over them.

```csharp
using ReactiveUI.Primitives.Extensions;

var live = new Signal<int>();
live.Subscribe(x => Console.WriteLine(x), () => Console.WriteLine("completed"));

live.FastForEach(new[] { 7, 8, 9 });
```

Output:

```text
7
8
9
```

It only sends the values. It does not complete the observer, so you decide when the stream is finished.

## In a ReactiveUI app

ReactiveUI gives you a main thread scheduler, and `WitnessOn` is how you move work onto it. See
[scheduling](../handbook/scheduling.md). When a view activates, ReactiveUI hands you a group to add your
subscriptions to, and disposes that group when the view goes away. See
[when activated](../handbook/when-activated.md).

## Every utility operator at a glance

| Operator | Second name | What it does |
|---|---|---|
| `Subscribe` | — | Starts a stream and runs your callbacks. Throws errors at the sender if you leave out the error callback. |
| `SubscribePrimitives` | — | The same as `Subscribe`, with a name that cannot clash with System.Reactive. |
| `SubscribeSafe` | — | Subscribes, and sends exceptions from your callbacks to your error callback. |
| `SubscribeSafePrimitives` | — | The same, with a name that cannot clash. |
| `WitnessOn` | `ObserveOn` | Runs your callbacks through a sequencer. |
| `SubscribeOn` | — | Runs the work of subscribing through a sequencer. |
| `Synchronize` | — | Handles one value at a time, behind a lock. |
| `Serialize` | — | Handles one value at a time, without holding a lock while your callback runs. |
| `Tap` | `Do` | Runs your code on each value and passes it on unchanged. |
| `TapWith` | `DoWith` | The same, with a state object so your callback can be `static`. |
| `AsObservable` | — | Hands back a read-only view that cannot be cast back to the signal. |
| `ToSignal` on a stream | — | Hands back the same stream after a `null` check. |
| `DisposeWith(group)` | — | Adds a disposable to a group and hands it back. |
| `DisposeWith()` | — | Wraps a disposable so it is disposed only once, optionally running an action first. |
| `FastForEach` | — | Sends every item of a collection to a witness. |

## The types behind these operators

`TapSignal<T>`, `SynchronizeSignal<T>`, `SerializeSignal<T>` and `SubscribeSafeWitness<T>` are public classes
in `ReactiveUI.Primitives.Advanced`. `TapWithSignal<T, TState>` is in `ReactiveUI.Primitives.Signals`. Each
takes its source through the constructor.

```csharp
using ReactiveUI.Primitives.Advanced;

// the operator
IObservable<int> a = live.Serialize();

// the same thing, built directly
IObservable<int> b = new SerializeSignal<int>(live);
```

Calling the operator is the normal path. Construct the type when you are writing an operator of your own and
want to place one inside it.

## The two package flavours

Every operator here ships twice. `ReactiveUI.Primitives` puts them under `ReactiveUI.Primitives.*`.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and puts them under
`ReactiveUI.Primitives.Reactive.*`. They behave the same in both.
