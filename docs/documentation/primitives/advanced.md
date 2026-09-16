---
Order: 16
---
# Writing your own operator

The operators are built from public parts in `ReactiveUI.Primitives.Advanced`, so you can build your own operator,
signal or sequencer the same way. This page covers those parts.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Advanced;
using ReactiveUI.Primitives.Concurrency;
using ReactiveUI.Primitives.Disposables;
using ReactiveUI.Primitives.Extensions;
using ReactiveUI.Primitives.Signals;
```

Reach for these only when no operator does the job. Most apps never need them. For async streams, see
[writing your own async operator](async/advanced.md).

## Two words first

An operator has two halves. The **signal** is the `IObservable<T>` you hand back. The **sink** is the object it
subscribes to the source: it receives each value, does its work, and passes results on to the **witness**, the
`IObserver<T>` that subscribed to you.

Three rules keep an operator correct:

- Send nothing after `OnError` or `OnCompleted`.
- When your witness throws, stop and release the source subscription.
- Never call your witness from two threads at once.

## An operator in four helpers

This operator doubles every value. The helpers below it do the fiddly parts.

```csharp
var numbers = new Signal<int>();

using IDisposable subscription = new DoubleSignal(numbers).Subscribe(
    static x => Console.WriteLine($"got {x}"),
    static () => Console.WriteLine("completed"));

numbers.OnNext(1);
numbers.OnNext(2);
numbers.OnCompleted();
numbers.OnNext(3);   // ignored: the stream has completed

public sealed class DoubleSignal(IObservable<int> source) : IObservable<int>
{
    public IDisposable Subscribe(IObserver<int> witness)
    {
        var sink = new DoubleSink(witness);
        sink.SetUpstream(source.Subscribe(sink));
        return sink;
    }

    private sealed class DoubleSink(IObserver<int> downstream) : IObserver<int>, IDisposable
    {
        private IDisposable? _upstream;
        private bool _done;

        public void SetUpstream(IDisposable subscription) => SubscriptionSlots.Assign(ref _upstream, subscription);

        public void OnNext(int value)
        {
            if (!_done)
            {
                SinkDelivery.Next(downstream, value * 2, this);
            }
        }

        public void OnError(Exception error) => SinkTerminal.Fault(downstream, error, this, ref _done);

        public void OnCompleted() => SinkTerminal.Complete(downstream, this, ref _done);

        public void Dispose() => SubscriptionSlots.Release(ref _upstream);
    }
}
```

Output:

```text
got 2
got 4
completed
```

### `SubscriptionSlots`

`SubscriptionSlots.Assign(ref slot, subscription)` stores a subscription in a field. `Release(ref slot)` disposes it
and marks the field as released. A subscription assigned after the release is disposed at once. That matters because
the source can complete, and your sink dispose itself, before `Subscribe` has even returned the subscription.

### `SinkDelivery.Next`

`SinkDelivery.Next(witness, value, sink)` sends a value. If the witness throws, it disposes the sink, then lets the
exception carry on up to whoever sent the value.

### `SinkTerminal`

`SinkTerminal.Fault` sends an error and `SinkTerminal.Complete` sends a completion, each disposing the sink. The forms
with `ref bool done` set the flag, and do nothing if it was set already, so a second terminal is ignored. A
`Complete` overload sends one last value before the completion.

### `SinkSubscription`

`SinkSubscription.Set(ref field, subscription)` stores a subscription once. A second value is disposed at once, and the
first stays. `SinkSubscription.Dispose(ref field)` disposes what the field holds.

```csharp
IDisposable? upstream = null;
var first = new BooleanDisposable();
var second = new BooleanDisposable();

SinkSubscription.Set(ref upstream, first);
SinkSubscription.Set(ref upstream, second);
Console.WriteLine($"first disposed: {first.IsDisposed}, second disposed: {second.IsDisposed}");

SinkSubscription.Dispose(ref upstream);
Console.WriteLine($"first disposed: {first.IsDisposed}");
```

Output:

```text
first disposed: False, second disposed: True
first disposed: True
```

## Witnesses

### `Witness.Create`

`Witness.Create` builds an `IObserver<T>` from lambdas: one for values, and optionally ones for errors and completion.

```csharp
IObserver<string> printer = Witness.Create<string>(static s => Console.WriteLine(s), static () => Console.WriteLine("end"));

printer.OnNext("hello");
printer.OnCompleted();
```

Output:

```text
hello
end
```

### `Witness.Safe`

`Witness.Safe` wraps a witness so it can never be called after it throws or ends. When the wrapped witness throws, the
wrapper disposes the `IDisposable` you pass, such as the source subscription, and ignores later calls.

```csharp
var sourceSubscription = new BooleanDisposable();

IObserver<int> safe = Witness.Safe(
    Witness.Create<int>(static x =>
    {
        if (x == 2)
        {
            throw new InvalidOperationException("cannot handle 2");
        }

        Console.WriteLine($"handled {x}");
    }),
    sourceSubscription);

safe.OnNext(1);

try
{
    safe.OnNext(2);
}
catch (InvalidOperationException error)
{
    Console.WriteLine(error.Message);
}

safe.OnNext(3);   // ignored
Console.WriteLine($"subscription disposed: {sourceSubscription.IsDisposed}");
```

Output:

```text
handled 1
cannot handle 2
subscription disposed: True
```

### `WitnessTeardown.Dispose`

`WitnessTeardown.Dispose(ref disposedFlag, ref cancel)` runs a witness's teardown once. It sets an `int` flag, disposes
the `IDisposable` field and clears it, and hands back `true` only on the first call.

```csharp
int disposed = 0;
IDisposable? cancel = new BooleanDisposable();

Console.WriteLine(WitnessTeardown.Dispose(ref disposed, ref cancel));
Console.WriteLine(WitnessTeardown.Dispose(ref disposed, ref cancel));
```

Output:

```text
True
False
```

## Timers and events

### `TimerSlot.Arm`

`TimerSlot.Arm(slot, sequencer, delay, tick)` schedules `tick` after a delay, and stores the timer in a
`SingleReplaceableDisposable`. Arming again cancels the timer before. A quiet-period operator such as `Calm` works
this way: each value re-arms the timer.

```csharp
var clock = new VirtualClock();
var timer = new SingleReplaceableDisposable();

TimerSlot.Arm(timer, clock, TimeSpan.FromMilliseconds(100), static () => Console.WriteLine("first timer fired"));
clock.AdvanceBy(TimeSpan.FromMilliseconds(50));
TimerSlot.Arm(timer, clock, TimeSpan.FromMilliseconds(100), static () => Console.WriteLine("second timer fired"));
clock.AdvanceBy(TimeSpan.FromMilliseconds(500));
```

Output:

```text
second timer fired
```

### `EventHandlerScope.Attach`

`EventHandlerScope.Attach(handler, addHandler, removeHandler, sequencer)` attaches an event handler and hands back an
`IDisposable` that detaches it. Pass a sequencer to attach and detach through it, as a UI event may require, or `null` to
do both on the calling thread.

```csharp
var button = new FakeButton();

IDisposable scope = EventHandlerScope.Attach<EventHandler>(
    static (_, _) => Console.WriteLine("clicked"),
    handler => button.Clicked += handler,
    handler => button.Clicked -= handler,
    null);

button.Click();
scope.Dispose();
button.Click();   // nothing: the handler is detached

public sealed class FakeButton
{
    public event EventHandler? Clicked;

    public void Click() => Clicked?.Invoke(this, EventArgs.Empty);
}
```

Output:

```text
clicked
```

## Delivering from many threads

Several threads may send values to one sink at the same time. A `lock` around the witness call stops them overlapping,
but it holds the lock while your subscriber's code runs. If that code waits on another thread that needs the same lock,
both threads wait forever. That is a **deadlock**. The types below deliver one value at a time **without** holding a
lock while the witness runs.

They are record structs, meant to be held as a field and called in place. A copy is a separate queue, so:

- Declare the field **without** `readonly`. A `readonly` struct field gives every call a fresh copy.
- Never copy it into a local, and pass it by `ref`.

Nothing throws when you get this wrong. Values are simply lost.

### `IDrainTarget`

Each type below calls back to you to deliver queued work. You pass that callback as a struct that implements
`IDrainTarget`, with one method, `Drain`. A struct allocates nothing.

### `SerializedDelivery<T>`

`SerializedDelivery<T>` delivers to one witness. `OnNext` delivers at once when no other thread is delivering, and
queues the value otherwise. The thread that is delivering picks up queued values before it returns.

```csharp
var sink = new CountingSink();

Task producerA = Task.Run(() => { for (var i = 0; i < 1000; i++) sink.OnNext(i); });
Task producerB = Task.Run(() => { for (var i = 0; i < 1000; i++) sink.OnNext(i); });
await Task.WhenAll(producerA, producerB);
sink.OnCompleted();

Console.WriteLine($"received {sink.Count}, overlapped: {sink.Overlapped}, completed: {sink.Completed}");

public sealed class CountingSink
{
    private SerializedDelivery<int> _delivery = new();
    private readonly IObserver<int> _witness;
    private int _inside;

    public CountingSink() =>
        _witness = Witness.Create<int>(
            _ =>
            {
                if (Interlocked.Increment(ref _inside) > 1)
                {
                    Overlapped = true;
                }

                Count++;
                Interlocked.Decrement(ref _inside);
            },
            () => Completed = true);

    public int Count { get; private set; }

    public bool Overlapped { get; private set; }

    public bool Completed { get; private set; }

    public void OnNext(int value) => _delivery.OnNext(_witness, value, new PendingDrain(this));

    public void OnCompleted() => _delivery.OnCompleted(new PendingDrain(this));

    private void DeliverQueued() => _ = _delivery.DrainTo(_witness);

    private readonly struct PendingDrain(CountingSink owner) : IDrainTarget
    {
        public void Drain() => owner.DeliverQueued();
    }
}
```

Output:

```text
received 2000, overlapped: False, completed: True
```

When you need your own lock to decide the order, use the `Post`, `PostError` and `PostCompleted` methods inside the
lock. They queue without delivering. Call `Flush` after you release the lock. `TryClaim` and `DeliverClaimed` deliver
directly when you already know nothing else is delivering.

### `SerializedWitness<T>`

`SerializedWitness<T>` is the same thing as a class. It wraps a witness, and you call `OnNext`, `OnError` and
`OnCompleted` on it from any thread.

### `SerializedBroadcaster<T>` and `SerializedBroadcast<T>`

`SerializedBroadcaster<T>` is the subscriber list of a signal of your own. Add and remove `SerializedWitness<T>`
subscribers, and post values, inside your lock, so every subscriber sees the same order. Each post hands back a
`SerializedBroadcast<T>`. **Always** call its `Flush` after you release the lock, or the subscribers it claimed deliver
nothing more.

```csharp
var hub = new MiniHub<int>();
var one = new SerializedWitness<int>(Witness.Create<int>(static x => Console.WriteLine($"one got {x}")));
var two = new SerializedWitness<int>(Witness.Create<int>(static x => Console.WriteLine($"two got {x}")));

hub.Add(one);
hub.Add(two);
hub.OnNext(5);
hub.Remove(one);
hub.OnNext(6);

public sealed class MiniHub<T>
{
    private readonly Lock _lock = new();
    private SerializedBroadcaster<T> _subscribers;

    public void Add(SerializedWitness<T> witness)
    {
        lock (_lock)
        {
            _subscribers.Add(witness);
        }
    }

    public void Remove(SerializedWitness<T> witness)
    {
        lock (_lock)
        {
            _subscribers.Remove(witness);
        }
    }

    public void OnNext(T value)
    {
        SerializedBroadcast<T> batch;

        lock (_lock)
        {
            batch = _subscribers.PostNext(value);
        }

        batch.Flush();
    }
}
```

Output:

```text
one got 5
two got 5
two got 6
```

### `CurrentValueDelivery<T>`, `CurrentValueWitness<T>` and `ICurrentValueReader<T>`

These deliver a value that something else owns, such as a control property. Instead of passing the value, you tell
them it **changed**, and they read it again with an `ICurrentValueReader<T>`. When changes arrive faster than they can
deliver, they deliver only the latest. Give them a comparer, and they skip a value equal to the last one delivered.

`CurrentValueWitness<T>` is the class form: call `Start()` to send the current value, `Changed()` from any thread, and
`Complete()` or `Fault(error)` to end. `CurrentValueDelivery<T>` is the struct form, held as a field.

```csharp
var thermostat = new Thermostat();

var temperatures = new CurrentValueWitness<int>(
    Witness.Create<int>(static t => Console.WriteLine($"temperature {t}")),
    new ThermostatReader(thermostat),
    EqualityComparer<int>.Default);

temperatures.Start();
thermostat.Degrees = 21;
temperatures.Changed();
temperatures.Changed();   // still 21: skipped
thermostat.Degrees = 22;
temperatures.Changed();

public sealed class Thermostat
{
    public int Degrees { get; set; } = 20;
}

public readonly struct ThermostatReader(Thermostat thermostat) : ICurrentValueReader<int>
{
    public int Read() => thermostat.Degrees;
}
```

Output:

```text
temperature 20
temperature 21
temperature 22
```

### `DeliveryGate` and `DeliveryGateState`

`DeliveryGate` is the lowest level: the gate the types above are built on, for a sink that keeps its own queue. Hold a
`DeliveryGateState` field and pass it by `ref`.

1. Call `TryEnter`. If it hands back `true`, deliver directly, then call `Exit` with your drain. If the delivery throws,
   call `Reset` instead, then rethrow.
2. If `TryEnter` hands back `false`, another thread is delivering. Queue your work, then call `Signal` with your drain.
   `Signal` either drains on your thread or hands the work to the delivering thread.

Enter, deliver and exit on the same thread, with no `await` in between. `TryEnterReentrant` also succeeds on the thread
already delivering, so a nested delivery runs at once instead of after the outer one.

```csharp
var gated = new GatedCounter();

await Task.WhenAll(
    Task.Run(() => { for (var i = 0; i < 1000; i++) gated.Deliver(); }),
    Task.Run(() => { for (var i = 0; i < 1000; i++) gated.Deliver(); }));

Console.WriteLine($"delivered {gated.Count}, most at once {gated.MostAtOnce}");

public sealed class GatedCounter
{
    private readonly System.Collections.Concurrent.ConcurrentQueue<int> _queue = new();
    private DeliveryGateState _gate;
    private int _inside;

    public int Count { get; private set; }

    public int MostAtOnce { get; private set; }

    public void Deliver()
    {
        _queue.Enqueue(1);

        if (DeliveryGate.TryEnter(ref _gate))
        {
            DeliverQueued();
            DeliveryGate.Exit(ref _gate, new PendingDrain(this));
            return;
        }

        DeliveryGate.Signal(ref _gate, new PendingDrain(this));
    }

    private void DeliverQueued()
    {
        while (_queue.TryDequeue(out _))
        {
            MostAtOnce = Math.Max(MostAtOnce, Interlocked.Increment(ref _inside));
            Count++;
            Interlocked.Decrement(ref _inside);
        }
    }

    private readonly struct PendingDrain(GatedCounter owner) : IDrainTarget
    {
        public void Drain() => owner.DeliverQueued();
    }
}
```

Output:

```text
delivered 2000, most at once 1
```

`Signal` waits up to `DeliveryGate.DefaultWaitBudget`, 20 ms, for the other thread before it hands its work over. An
overload takes a different budget.

#### A delivery inside a delivery

Delivering a value can cause another delivery on the same thread, such as a subscriber that writes back to the
stream. `TryEnter` hands back `false` for it, so the nested value is queued and delivered after the outer one ends.
`TryEnterReentrant` hands back `true`, so the nested value is delivered at once, inside the outer one. Either way,
call `Exit` once for each successful enter.

```csharp
var queued = new NestedLog(reentrant: false);
queued.Write("outer");
Console.WriteLine(string.Join(", ", queued.Lines));

var nested = new NestedLog(reentrant: true);
nested.Write("outer");
Console.WriteLine(string.Join(", ", nested.Lines));

public sealed class NestedLog(bool reentrant)
{
    private readonly System.Collections.Concurrent.ConcurrentQueue<string> _queue = new();
    private DeliveryGateState _gate;

    public List<string> Lines { get; } = [];

    public void Write(string line)
    {
        var entered = reentrant ? DeliveryGate.TryEnterReentrant(ref _gate) : DeliveryGate.TryEnter(ref _gate);

        if (!entered)
        {
            _queue.Enqueue(line);
            DeliveryGate.Signal(ref _gate, new QueueDrain(this));
            return;
        }

        Lines.Add($"start {line}");
        if (line == "outer")
        {
            Write("inner");
        }

        Lines.Add($"end {line}");
        DeliveryGate.Exit(ref _gate, new QueueDrain(this));
    }

    private void DeliverQueued()
    {
        while (_queue.TryDequeue(out var line))
        {
            Lines.Add($"start {line}");
            Lines.Add($"end {line}");
        }
    }

    private readonly struct QueueDrain(NestedLog owner) : IDrainTarget
    {
        public void Drain() => owner.DeliverQueued();
    }
}
```

Output:

```text
start outer, end outer, start inner, end inner
start outer, start inner, end inner, end outer
```

#### When a delivery throws

A delivery that throws must still release the gate, or no thread could deliver again. Call `Reset` instead of `Exit`.
`Reset` hands back `true` when other threads queued work while the delivery ran. Call `Signal` then, so that work is
not stranded.

```csharp
var log = new FailingLog();

try
{
    log.Write("bad");
}
catch (InvalidOperationException error)
{
    Console.WriteLine($"caught: {error.Message}");
}

log.Write("good");
Console.WriteLine(string.Join(", ", log.Lines));

public sealed class FailingLog
{
    private readonly System.Collections.Concurrent.ConcurrentQueue<string> _queue = new();
    private DeliveryGateState _gate;

    public List<string> Lines { get; } = [];

    public void Write(string line)
    {
        _queue.Enqueue(line);

        if (!DeliveryGate.TryEnter(ref _gate))
        {
            DeliveryGate.Signal(ref _gate, new QueueDrain(this));
            return;
        }

        try
        {
            DeliverQueued();
        }
        catch
        {
            if (DeliveryGate.Reset(ref _gate))
            {
                DeliveryGate.Signal(ref _gate, new QueueDrain(this));
            }

            throw;
        }

        DeliveryGate.Exit(ref _gate, new QueueDrain(this));
    }

    private void DeliverQueued()
    {
        while (_queue.TryDequeue(out var line))
        {
            if (line == "bad")
            {
                throw new InvalidOperationException("the disk is full");
            }

            Lines.Add(line);
        }
    }

    private readonly struct QueueDrain(FailingLog owner) : IDrainTarget
    {
        public void Drain() => owner.DeliverQueued();
    }
}
```

Output:

```text
caught: the disk is full
good
```

The second `Write` gets the gate, so the first failure left nothing locked.

## Constructing operator types directly

Every operator has a public class behind it, named after the operator: `KeepSignal<T>` for `Where`, `UniqueSignal<T>`
for `Unique`, `FoldSignal<TSource, TAccumulate>` for `Fold`, and a `...Witness<T>` for its sink. Each takes its source
through the constructor. Each operator page lists the types behind it.

```csharp
IObservable<int> viaOperator = numbers.Where(static x => x > 0);
IObservable<int> viaType = new KeepSignal<int>(numbers, static x => x > 0);
```

Call the operator in normal code. Construct the type when you place one inside an operator of your own and want to skip
the extension call.

## Marker interfaces

Implement these on your own signal to let the operators take a faster path.

| Interface | What it tells the operators |
|---|---|
| `IInlineSignal<T>` | Your signal has a `Subscribe(onNext, onError, onCompleted)` that takes delegates, so no witness object is allocated. |
| `IRequireCurrentThread<T>` | Your signal must be subscribed on the calling thread. |
| `IAsyncEnumerableBackedSignal<T>` | Your signal wraps an `IAsyncEnumerable<T>`, which a consumer can read directly. |

To write a sequencer for a UI framework that has none, see [sequencers](scheduling.md).

## At a glance

| Member | What it does |
|---|---|
| `SubscriptionSlots.Assign` / `Release` | Stores the source subscription, and disposes it once. |
| `SinkDelivery.Next` | Sends a value, disposing the sink if the witness throws. |
| `SinkTerminal.Fault` / `Complete` | Sends a terminal once, disposing the sink. |
| `SinkSubscription.Set` / `Dispose` | Stores a subscription once, and disposes it. |
| `Witness.Create` | A witness from lambdas. |
| `Witness.Safe` | A witness that stops after it throws. |
| `WitnessTeardown.Dispose` | Runs a teardown once. |
| `TimerSlot.Arm` | Schedules a tick, cancelling the one before. |
| `EventHandlerScope.Attach` | Attaches a handler, and detaches it on dispose. |
| `SerializedDelivery<T>` / `SerializedWitness<T>` | One-at-a-time delivery to one witness, with no lock held. |
| `SerializedBroadcaster<T>` / `SerializedBroadcast<T>` | One-at-a-time delivery to many subscribers. |
| `CurrentValueDelivery<T>` / `CurrentValueWitness<T>` / `ICurrentValueReader<T>` | Delivery of the latest value of something you read. |
| `DeliveryGate` / `DeliveryGateState` / `IDrainTarget` | The gate the delivery types are built on. |
