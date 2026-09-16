---
Order: 5
---
# State, property changes and tests

These helpers hold a current value, turn a property change into a stream, run code when someone subscribes or
disposes, and block a test until a result arrives.

```csharp
using System.ComponentModel;
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Extensions;
using ReactiveUI.Primitives.Signals;
```

## A current value

### `ToReadOnlyBehavior`

`ToReadOnlyBehavior` builds a value holder with two sides. The `Observer` side takes new values with `OnNext`. The
`Observable` side is a stream: each new subscriber gets the current value at once, then every change. Hand the
`Observable` side to other code, and keep the `Observer` side to yourself, so only you can change the value.

```csharp
var (status, setStatus) = ReactiveExtensions.ToReadOnlyBehavior("idle");

status.Subscribe(static s => Console.WriteLine($"first: {s}"));

setStatus.OnNext("busy");

status.Subscribe(static s => Console.WriteLine($"late: {s}"));
```

Output:

```text
first: idle
first: busy
late: busy
```

The pair is a `CurrentValueSubject<T>` behind the scenes. Construct one directly when you want to read `Value` too.
For a value you set through a property, see [`StateSignal<T>`](../signals.md).

### `ReplayLastOnSubscribe`

`ReplayLastOnSubscribe` sends a starting value to each new subscriber at once, then the source's values. Each
subscriber gets its own subscription and its own starting value, so a subscriber that arrives later gets the starting
value too, not the newest value.

```csharp
var temperature = new Signal<int>();
IObservable<int> readings = temperature.ReplayLastOnSubscribe(20);

readings.Subscribe(static t => Console.WriteLine($"early {t}"));
temperature.OnNext(22);
readings.Subscribe(static t => Console.WriteLine($"late {t}"));
```

Output:

```text
early 20
early 22
late 20
```

The [async `ReplayLastOnSubscribe`](../async/utility.md#replaylastonsubscribe) behaves differently: it shares one
subscription, and a late subscriber gets the newest value. To share the newest value on a synchronous stream, use
[`ReplayLive(1)`](../sharing.md).

## Property changes

### `ToPropertyObservable`

`ToPropertyObservable` works on any object that implements `INotifyPropertyChanged`. Give it a lambda that reads one
property. It sends the property's value when you subscribe, then again each time the object raises `PropertyChanged`
for that property.

```csharp
var person = new Person();

using var subscription = person.ToPropertyObservable(static p => p.Name)
                               .Subscribe(static name => Console.WriteLine($"name: '{name}'"));

person.Name = "Ada";
person.Name = "Grace";

public sealed class Person : INotifyPropertyChanged
{
    private string _name = "";

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
        }
    }
}
```

Output:

```text
name: ''
name: 'Ada'
name: 'Grace'
```

The lambda must read one property directly, such as `p => p.Name`. A lambda that calls a method, such as
`p => p.ToString()`, throws `ArgumentException`. The helper works with trimming and Native AOT.

In a ReactiveUI app, [`WhenAnyValue`](../../handbook/when-any.md) does this job for view models and follows chains of
properties.

## Running code on subscribe and dispose

### `DoOnSubscribe`

`DoOnSubscribe` runs an action each time someone subscribes, just before it subscribes to the source. If the action
throws, the exception comes out of the `Subscribe` call.

### `DoOnDispose`

`DoOnDispose` runs an action once, when the subscription is disposed, after the source subscription is released.

```csharp
IDisposable subscription = Signal.Never<int>()
    .DoOnSubscribe(static () => Console.WriteLine("subscribed"))
    .DoOnDispose(static () => Console.WriteLine("disposed"))
    .Subscribe(static x => Console.WriteLine(x));

subscription.Dispose();
subscription.Dispose();
```

Output:

```text
subscribed
disposed
```

Disposing twice runs the action once. [`Finally`](../error-handling.md) runs an action however the subscription ends,
including completion and failure.

## Waiting for a result in a test

These helpers block the calling thread, so use them in tests and console apps. Never call them on a UI thread: the
screen freezes until the stream ends. See [best practices](../best-practices.md).

### `WaitForValue`

`WaitForValue` subscribes and blocks until the stream completes or fails. It hands back the last value sent, or
`default` if the stream sent nothing or failed. If the stream does not end within 30 seconds, it throws
`TimeoutException`. Pass a `TimeSpan` for a different limit.

```csharp
int last = Signal.Emit(5)
                 .Shift(TimeSpan.FromMilliseconds(50))
                 .WaitForValue();

Console.WriteLine(last);

try
{
    Signal.Never<int>().WaitForValue(TimeSpan.FromMilliseconds(50));
}
catch (TimeoutException error)
{
    Console.WriteLine(error.Message);
}
```

Output:

```text
5
WaitForValue timed out after 0.05s.
```

### `WaitForError`

`WaitForError` blocks until the stream ends, and hands back its exception without throwing it. It hands back `null` if
the stream completed normally.

```csharp
Exception? error = Signal.Fail<int>(new InvalidOperationException("disk full"))
                         .WaitForError();

Console.WriteLine(error?.Message);
```

Output:

```text
disk full
```

### `WaitForCompletion`

`WaitForCompletion` works on a stream of `RxVoid`. It blocks until the stream ends, and throws the stream's exception
if it failed.

```csharp
Signal.Start(static () => Console.WriteLine("saving"))
      .WaitForCompletion();

Console.WriteLine("saved");
```

Output:

```text
saving
saved
```

Each `WaitFor` helper also takes a sequencer. It subscribes through that sequencer, so a stream that must be
subscribed on a particular thread still works.

### `SubscribeGetValue`, `SubscribeGetError` and `SubscribeAndComplete`

These subscribe, take what the stream sends **during the `Subscribe` call itself**, and dispose at once. They do not
wait. Use them for a stream that sends its values straight away, such as one built from a collection.

- `SubscribeGetValue` hands back the last value, or `default`.
- `SubscribeGetError` hands back the exception, or `null`.
- `SubscribeAndComplete` works on a stream of `RxVoid`, and throws its values away.

```csharp
Console.WriteLine(Signal.Range(1, 3).SubscribeGetValue());
Console.WriteLine(Signal.Fail<int>(new InvalidOperationException("bad")).SubscribeGetError()?.Message);
```

Output:

```text
3
bad
```

## Small helper types

### `Optional<T>`

`Optional<T>` holds a value that may be missing. `HasValue` tells you whether it holds one, and `Value` reads it.
`Optional.Some(value)` wraps a value. `default(Optional<T>)` and `Optional<T>.None` hold nothing, and
`Optional<T>.Create(value)` treats `null` as nothing.

```csharp
Optional<int> found = Optional.Some(3);
Optional<string> missing = Optional<string>.Create(null!);

Console.WriteLine($"{found.HasValue} {found.Value}");
Console.WriteLine(missing.HasValue);
```

Output:

```text
True 3
False
```

`Optional<T>` and `Optional` are in the `ReactiveUI.Primitives` namespace.

### `Spark` factories

A `Spark<T>` holds one notification as data: a value, an error, or completion. The [`Spark`](../transformation.md)
operator produces them. The static `Spark` class in `ReactiveUI.Primitives.Core` builds one by hand, which is useful
for test input.

```csharp
using ReactiveUI.Primitives.Core;

Console.WriteLine(Spark.CreateOnNext(5));
Console.WriteLine(Spark.CreateOnError<int>(new InvalidOperationException("broken")));
Console.WriteLine(Spark.CreateOnCompleted<int>());
```

Output:

```text
OnNext(5)
OnError(System.InvalidOperationException)
OnCompleted()
```

### `ObserverArrayHelpers`

These help when you write a signal of your own that keeps its subscribers in an array.

- `Broadcast(witnesses, value)` sends a value to every witness in the array, in order.
- `RemoveOrNull(current, witness, empty)` copies the array without one witness. It hands back `null` when that witness
  is not in the array, so you can skip replacing the array. When the last witness goes, it hands back the `empty`
  array you pass, so you reuse one empty array instead of allocating a new one.

A **witness** is the object that receives a stream's values: an `IObserver<T>`.

```csharp
using ReactiveUI.Primitives.Advanced;

var received = new List<int>();

IObserver<int>[] witnesses =
[
    Witness.Create<int>(received.Add),
    Witness.Create<int>(x => received.Add(x * 10)),
];

ObserverArrayHelpers.Broadcast(witnesses, 2);
Console.WriteLine(string.Join(", ", received));

IObserver<int>[]? remaining = ObserverArrayHelpers.RemoveOrNull(witnesses, witnesses[0], []);
Console.WriteLine(remaining?.Length);
```

Output:

```text
2, 20
1
```

## Every helper on this page at a glance

| Helper | What it does |
|---|---|
| `ToReadOnlyBehavior` | A value holder with a read side and a write side. |
| `ReplayLastOnSubscribe` | Sends a starting value to each new subscriber, then the source's values. |
| `ToPropertyObservable` | Sends a property's value now and on each change. |
| `DoOnSubscribe` | Runs an action on each subscribe. |
| `DoOnDispose` | Runs an action once, on dispose. |
| `WaitForValue` | Blocks until the stream ends, and hands back the last value. |
| `WaitForError` | Blocks until the stream ends, and hands back its exception. |
| `WaitForCompletion` | Blocks until an `RxVoid` stream ends, and throws if it failed. |
| `SubscribeGetValue` | The last value sent during `Subscribe`. |
| `SubscribeGetError` | The exception sent during `Subscribe`. |
| `SubscribeAndComplete` | Subscribes to an `RxVoid` stream and disposes at once. |
| `Optional.Some` | Wraps a value in an `Optional<T>`. |
| `Spark.CreateOnNext` / `CreateOnError` / `CreateOnCompleted` | Builds a notification by hand. |
| `ObserverArrayHelpers.Broadcast` / `RemoveOrNull` | Sends to, and removes from, an array of witnesses. |

## The types behind these helpers

- `CurrentValueSubject<T>` in `ReactiveUI.Primitives.Extensions` is the value holder behind `ToReadOnlyBehavior`. It
  has `Value`, `OnNext`, `OnError`, `OnCompleted` and `AsObservable()`, and implements `IDisposable`.
- `ReplayLastOnSubscribeObservable<T>`, `PropertyChangedObservable<T, TProperty>`, `DoOnSubscribeObservable<T>` and
  `DoOnDisposeObservable<T>` are public classes in `ReactiveUI.Primitives.Extensions.Operators`.
