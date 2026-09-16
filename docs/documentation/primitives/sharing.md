---
Order: 9
---
# Sharing one subscription

Most streams do their work again for every subscriber. Subscribe twice to a stream that downloads a price list,
and it downloads the list twice. The operators on this page let several subscribers share one run of the work.

Two words help here. A **cold** stream starts its work when you subscribe, and does it separately for each
subscriber. A **hot** stream is already running and sends the same values to everyone listening, whether or not
anyone has subscribed. `Signal.FromEnumerable` is cold. A `Signal<T>` you push values into is hot. Sharing
turns a cold stream into a hot one.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;
```

## Your first shared stream

You download a price list. One part of your screen shows the list. Another shows the total. Both need the same
prices, and you only want to download them once.

**1. Start with a cold stream.** This one prints a line each time it downloads, so you can count.

```csharp
var downloads = 0;

IObservable<string> prices = Signal.Lazy(() =>
{
    downloads++;
    Console.WriteLine($"download #{downloads}");
    return Signal.FromEnumerable(["apple 1.20", "pear 0.90"]);
});
```

**2. See the problem.** Subscribe twice without sharing.

```csharp
prices.Subscribe(p => Console.WriteLine($"list:  {p}"));
prices.Subscribe(p => Console.WriteLine($"total: {p}"));
```

Output:

```text
download #1
list:  apple 1.20
list:  pear 0.90
download #2
total: apple 1.20
total: pear 0.90
```

Two subscribers, two downloads.

**3. Share it.** `ShareLive` wraps the stream in a `ConnectableSignal<T>`. Subscribers can join it, but nothing
runs yet.

```csharp
ConnectableSignal<string> shared = prices.ShareLive();

shared.Subscribe(p => Console.WriteLine($"list:  {p}"));
shared.Subscribe(p => Console.WriteLine($"total: {p}"));
Console.WriteLine("nothing yet");
```

**4. Connect.** `Connect` subscribes to the real stream once, and passes every value to everyone who has joined.

```csharp
shared.Connect();
```

Output, for steps 3 and 4 together:

```text
nothing yet
download #1
list:  apple 1.20
total: apple 1.20
list:  pear 0.90
total: pear 0.90
```

One download. Both subscribers got every price.

## Sharing by hand

### `ShareLive`

`ShareLive` wraps a stream in a `ConnectableSignal<T>`. It does not subscribe to the stream until you call
`Connect`. Every subscriber shares that one subscription.

`Connect` hands back an `IDisposable`. Dispose it to disconnect from the stream, and everyone stops receiving
values.

```csharp
var ticks = new Signal<int>();
ConnectableSignal<int> shared = ticks.ShareLive();

shared.Subscribe(x => Console.WriteLine($"got {x}"));

IDisposable connection = shared.Connect();
ticks.OnNext(1);          // prints got 1

connection.Dispose();
ticks.OnNext(2);          // prints nothing
```

A subscriber that joins after values have gone out does not get them. It only sees what arrives after it
joins.

```csharp
var ticks = new Signal<int>();
ConnectableSignal<int> shared = ticks.ShareLive();
shared.Connect();

ticks.OnNext(1);
shared.Subscribe(x => Console.WriteLine($"late {x}"));
ticks.OnNext(2);          // prints late 2
```

### `ReplayLive`

`ReplayLive` works like `ShareLive`, and also remembers what it has sent. A subscriber that joins late receives
the remembered values first.

```csharp
var ticks = new Signal<int>();
ConnectableSignal<int> shared = ticks.ReplayLive();
shared.Connect();

ticks.OnNext(1);
ticks.OnNext(2);
ticks.OnNext(3);

shared.Subscribe(x => Console.WriteLine($"late {x}"));
```

Output:

```text
late 1
late 2
late 3
```

Remembering every value uses more memory the longer the stream runs. Give `ReplayLive` an `int` to remember
only that many of the most recent values:

```csharp
var ticks = new Signal<int>();
ConnectableSignal<int> shared = ticks.ReplayLive(2);
shared.Connect();

ticks.OnNext(1);
ticks.OnNext(2);
ticks.OnNext(3);

shared.Subscribe(x => Console.WriteLine($"late {x}"));   // late 2, late 3
```

Give it an `int` and a `TimeSpan`, and it forgets values older than that `TimeSpan` as well:

```csharp
var ticks = new Signal<int>();
ConnectableSignal<int> shared = ticks.ReplayLive(10, TimeSpan.FromSeconds(1));
shared.Connect();

ticks.OnNext(1);
Thread.Sleep(1500);
ticks.OnNext(2);

shared.Subscribe(x => Console.WriteLine($"late {x}"));   // late 2
```

`1` was more than a second old when the late subscriber joined, so it was forgotten.

### `Multicast`

`Multicast` lets you choose the **hub**, the object that receives each value and passes it on to every
subscriber. `ShareLive` and `ReplayLive` pick a hub for you. With `Multicast` you pass one in, such as a
`ReplaySignal<T>`.

```csharp
var ticks = new Signal<int>();
ConnectableSignal<int> shared = ticks.Multicast(new ReplaySignal<int>(1));
shared.Connect();

ticks.OnNext(1);
ticks.OnNext(2);

shared.Subscribe(x => Console.WriteLine($"late {x}"));   // late 2
```

The hub remembered one value, so the late subscriber got the last one. Any signal can be a hub.

## Connecting on its own

Calling `Connect` yourself is easy to forget, and so is disposing the connection. These operators connect for
you.

### `AutoShare`

`AutoShare` connects when the first subscriber arrives, and disconnects when the last one leaves. A new
subscriber after that connects again.

To see when it connects, this example wraps a `Signal<int>` in a stream that prints `connected` when something
subscribes to it, and `disconnected` when that subscription ends.

```csharp
using ReactiveUI.Primitives.Disposables;

var ticks = new Signal<int>();

IObservable<int> watched = Signal.Create<int>(witness =>
{
    Console.WriteLine("connected");
    IDisposable inner = ticks.Subscribe(witness);
    return new ActionDisposable(() =>
    {
        Console.WriteLine("disconnected");
        inner.Dispose();
    });
});

IObservable<int> auto = watched.ShareLive().AutoShare();

IDisposable first = auto.Subscribe(x => Console.WriteLine($"first {x}"));
IDisposable second = auto.Subscribe(x => Console.WriteLine($"second {x}"));
ticks.OnNext(1);

first.Dispose();
Console.WriteLine("first left");

second.Dispose();
Console.WriteLine("second left");
```

Output:

```text
connected
first 1
second 1
first left
disconnected
second left
```

Only one connection was made for both subscribers. It closed when the second one left, not the first.

It works by **reference counting**: keeping a count of how many subscribers are using the connection, and
releasing it when the count reaches zero.

### `AutoConnect`

`AutoConnect` connects when the first subscriber arrives, and **never** disconnects, even after every
subscriber has left. Use it for a stream that should keep running once started.

Give it an `int`, and it waits for that many subscribers before it connects:

```csharp
var ticks = new Signal<int>();
IObservable<int> auto = ticks.ShareLive().AutoConnect(2);

auto.Subscribe(x => Console.WriteLine($"first {x}"));
ticks.OnNext(1);          // prints nothing: only one subscriber so far

auto.Subscribe(x => Console.WriteLine($"second {x}"));
ticks.OnNext(2);          // prints first 2, then second 2
```

Waiting for everyone to arrive means nobody misses the first value. Note that `1` was sent before the stream
connected, so nobody received it.

Because `AutoConnect` never disconnects by itself, a third form hands you the connection so you can stop it:
`AutoConnect(1, connection => ...)`. Keep the connection, and dispose it when you are done.

### `ShareLatest`

`ShareLatest` is `ShareLive` and `AutoShare` in one call. It shares one subscription while anyone is listening,
and releases it when the last subscriber leaves.

```csharp
var ticks = new Signal<int>();
IObservable<int> shared = ticks.ShareLatest();

shared.Subscribe(x => Console.WriteLine($"first {x}"));
ticks.OnNext(1);

shared.Subscribe(x => Console.WriteLine($"second {x}"));
ticks.OnNext(2);
```

Output:

```text
first 1
first 2
second 2
```

> [!NOTE]
> Despite its name, `ShareLatest` does not hand the latest value to a subscriber that joins late. `second`
> never received `1`. For that, use `ReplayLive(1).AutoShare()`.

### `AutoShare` against `AutoConnect`

| Operator | Connects when | Disconnects when |
|---|---|---|
| `AutoShare` | The first subscriber arrives. | The last subscriber leaves. |
| `AutoConnect()` | The first subscriber arrives. | Never, unless you dispose the connection it hands you. |
| `AutoConnect(2)` | The second subscriber arrives. | Never, unless you dispose the connection it hands you. |

## Sharing inside one expression

### `Publish` with a lambda

Sometimes you want to use one stream twice in the same expression, such as pairing each reading with the one
before it. Written plainly, that subscribes to the stream twice. `Publish` with a lambda shares the stream
inside that lambda, so it subscribes once.

```csharp
var downloads = 0;

IObservable<int> readings = Signal.Lazy(() =>
{
    downloads++;
    return Signal.FromEnumerable([10, 13, 11, 17]);
});

readings.Publish(r => r.Zip(r.Skip(1), (before, after) => after - before))
        .Subscribe(change => Console.WriteLine($"change {change}"));

Console.WriteLine($"downloads: {downloads}");
```

Output:

```text
change 3
change -2
change 6
downloads: 1
```

Inside the lambda, `r` is the shared stream. Use it as many times as you like. Without `Publish`, the same
`Zip` call downloads twice.

## Keeping a current value

### `ToReadOnlyState`

`ToReadOnlyState` turns a stream into a `ReadOnlyState<T>`: an object with a `Value` you can read at any time,
and a `Changed` stream that tells you when it moves. It takes a starting value, and a lambda that turns each
value from the stream into the value you want to keep.

```csharp
var temperatures = new Signal<double>();

ReadOnlyState<string> display = temperatures.ToReadOnlyState("waiting", t => $"{t:0.0} C");

Console.WriteLine(display.Value);   // waiting

display.Changed.Subscribe(text => Console.WriteLine($"changed to {text}"));
temperatures.OnNext(21.5);

Console.WriteLine(display.Value);   // 21.5 C
```

Output:

```text
waiting
changed to waiting
changed to 21.5 C
21.5 C
```

`Changed` sends the current value as soon as you subscribe, then each new value. Subscribing to the state
itself does the same.

> [!NOTE]
> `Changed` reports every value the stream sends, even when the kept value has not changed.
>
> ```csharp
> ReadOnlyState<int> tens = readings.ToReadOnlyState(0, r => r / 10);
> tens.Changed.Subscribe(t => Console.WriteLine($"changed to {t}"));
>
> // readings sends 21, 25, 31
> // prints: changed to 0, changed to 2, changed to 2, changed to 3
> ```
>
> `21` and `25` both became `2`, and `Changed` reported `2` twice. Add `.Unique()` after `Changed` if you only
> want real changes.

A `ReadOnlyState<T>` holds a subscription to its stream. Dispose it when you no longer need it. After that,
reading `Value`, `Changed`, or subscribing throws an `ObjectDisposedException`.

In a ReactiveUI view model, `ToProperty` does a similar job and raises a property changed event for the screen.
See [observable as property helper](../handbook/observable-as-property-helper.md).

## Every sharing operator at a glance

| Operator | Second name | What it does |
|---|---|---|
| `ShareLive` | `Publish`, `Share` | Shares one subscription, once you call `Connect`. |
| `ReplayLive` | `Replay` | Does the same, and sends remembered values to late subscribers. |
| `Multicast` | — | Does the same, through a hub you choose. |
| `AutoShare` | `RefCount` | Connects on the first subscriber, disconnects after the last. |
| `AutoConnect` | — | Connects on the first subscriber, or after the count you give, and never disconnects. |
| `ShareLatest` | — | `ShareLive` and `AutoShare` in one call. Does not replay. |
| `Publish(selector)` | — | Shares a stream for the length of one expression. |
| `ToReadOnlyState` | — | Keeps a current `Value`, with a `Changed` stream. |

## The types behind these operators

`ConnectableSignal<T>` is in `ReactiveUI.Primitives`. It takes the source and a hub, and has a `Connect`
method. `AutoShareSignal<T>` and `AutoConnectSignal<T>` are in `ReactiveUI.Primitives.Advanced`.
`ReadOnlyState<T>` is in `ReactiveUI.Primitives.Signals`, and takes the source and a starting value.

```csharp
// the operator
ConnectableSignal<int> a = ticks.ShareLive();

// the same thing, built directly
var b = new ConnectableSignal<int>(ticks, new Signal<int>());
```

Calling the operator is the normal path. Construct the type when you are writing an operator of your own and
want to place one inside it.

## The two package flavours

Every operator here ships twice. `ReactiveUI.Primitives` puts them under `ReactiveUI.Primitives.*`.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and puts them under
`ReactiveUI.Primitives.Reactive.*`. They behave the same in both.
