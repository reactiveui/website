---
Order: 12
---
# Disposables

Every subscription is an `IDisposable`. Disposing it stops the subscription. A real app holds many of them, and
some come and go as the user works. The types on this page group them, swap them, and make sure each one is
disposed exactly once.

They ship in the `ReactiveUI.Disposables` package, under the `ReactiveUI.Primitives.Disposables` namespace.
`ReactiveUI.Primitives` references it for you.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Disposables;
using ReactiveUI.Primitives.Signals;
```

## Your first group of subscriptions

A screen listens to clicks and key presses. When the screen closes, both subscriptions must stop.

**1. Make a group.** A `MultipleDisposable` holds any number of disposables.

```csharp
var subscriptions = new MultipleDisposable();
```

**2. Add each subscription.**

```csharp
subscriptions.Add(clicks.Subscribe(x => Console.WriteLine($"click {x}")));
subscriptions.Add(keys.Subscribe(k => Console.WriteLine($"key {k}")));
```

**3. Dispose the group once.** Every subscription in it stops.

```csharp
clicks.OnNext(1);        // prints click 1
subscriptions.Dispose();
clicks.OnNext(2);        // prints nothing
keys.OnNext('a');        // prints nothing
```

`DisposeWith` adds a subscription to a group on the same line that creates it. See
[cleaning up subscriptions](utility.md#cleaning-up-subscriptions).

## Every disposable here is safe to dispose twice

All the types on this page ignore a second `Dispose`. Most also implement `IsDisposed`, an interface that adds a
`bool IsDisposed` property, so you can check whether it has happened.

Once disposed, a type that holds other disposables **disposes anything you give it afterwards straight away**.
A subscription added to a closed group can never leak.

## Running code on dispose

### `ActionDisposable`

`ActionDisposable` runs an `Action` the first time you dispose it.

```csharp
var connection = new ActionDisposable(() => Console.WriteLine("closed"));

Console.WriteLine(connection.IsDisposed);   // False
connection.Dispose();                       // prints closed
connection.Dispose();                       // prints nothing
Console.WriteLine(connection.IsDisposed);   // True
```

### `Scope`

`Scope` is a static class of shortcuts for making disposables.

| Member | Hands back |
|---|---|
| `Scope.Create(action)` | A disposable that runs `action` once. A `null` action gives you `Scope.Empty`. |
| `Scope.Create(state, action)` | The same, passing `state` to the action, so the lambda can be `static`. |
| `Scope.Combine(first, second)` | One disposable that disposes both, in order. |
| `Scope.Combine(params disposables)` | One disposable that disposes all of them, in order. |
| `Scope.Empty` | A disposable that does nothing, the same object as `EmptyDisposable.Instance`. |

```csharp
IDisposable timer = Scope.Create(() => Console.WriteLine("timer stopped"));
IDisposable file = Scope.Create("report.pdf", static name => Console.WriteLine($"closing {name}"));

IDisposable both = Scope.Combine(timer, file);
both.Dispose();
both.Dispose();
```

Output:

```text
timer stopped
closing report.pdf
```

### `EmptyDisposable`

`EmptyDisposable.Instance` does nothing when disposed. Return it when a method must hand back an `IDisposable` but
has nothing to clean up, such as a `Signal.Create` subscribe lambda that completes straight away.

```csharp
IObservable<int> finished = Signal.Create<int>(static witness =>
{
    witness.OnCompleted();
    return EmptyDisposable.Instance;
});

finished.Subscribe(static x => Console.WriteLine(x), static () => Console.WriteLine("completed"));
```

Output:

```text
completed
```

## Recording that dispose happened

### `BooleanDisposable`

`BooleanDisposable` does nothing except set `IsDisposed`. Hand it out as a cancel flag, then check the flag in
a loop.

```csharp
var cancelled = new BooleanDisposable();

Console.WriteLine(cancelled.IsDisposed);   // False
cancelled.Dispose();
Console.WriteLine(cancelled.IsDisposed);   // True
```

### `CancellationDisposable`

`CancellationDisposable` cancels a `CancellationTokenSource` when you dispose it. Pass its `Token` to `async`
work, so disposing a subscription also cancels the work it started.

```csharp
var cancel = new CancellationDisposable();
cancel.Token.Register(() => Console.WriteLine("token cancelled"));

cancel.Dispose();                                    // prints token cancelled
Console.WriteLine(cancel.Token.IsCancellationRequested);   // True
```

`new CancellationDisposable(source)` uses a `CancellationTokenSource` you already have. Disposing cancels your
source, but does not dispose it. You still own the source.

## Groups of disposables

### `MultipleDisposable`

`MultipleDisposable` holds a group of disposables and disposes them together, in the order you added them. It is
an `ICollection<IDisposable>`, so you can add, remove, count and loop over what it holds.

```csharp
var group = new MultipleDisposable(first, second);   // or new MultipleDisposable() and Add

group.Add(third);
Console.WriteLine(group.Count);            // 3
Console.WriteLine(group.Contains(third));  // True
```

Removing and clearing **dispose** what they take out:

| Member | What it does |
|---|---|
| `Add(item)` | Adds `item`. On a disposed group, disposes `item` straight away instead. |
| `Remove(item)` | Takes `item` out and disposes it. Returns `true` if it was there. |
| `Clear()` | Takes everything out and disposes it. The group stays open for more. |
| `Dispose()` | Disposes everything and closes the group. `Count` becomes `0`. |
| `Contains(item)`, `Count`, `CopyTo`, `foreach` | Read what the group holds right now. |

Use `Clear` when a screen reloads its data: the old subscriptions stop, and the group is ready for the new ones.

The constructor also takes two, three, or a `params` array of disposables.

`MultipleDisposable.Create(params disposables)` is lighter. It hands back a plain `IDisposable` that disposes a
fixed set, with no `Add` or `Remove`.

```csharp
IDisposable pair = MultipleDisposable.Create(first, second);
pair.Dispose();
```

### `DisposableBag`

`DisposableBag` is a smaller group with only `Add`, `Dispose` and `IsDisposed`. It disposes in the order you
added, once. `Add(null)` is ignored. It holds its first two disposables without allocating an array, so it
suits the common case of an object that owns two or three subscriptions.

```csharp
var bag = new DisposableBag(clicksSubscription, keysSubscription);
bag.Add(timerSubscription);

bag.Dispose();
```

### `DisposableSet`

`DisposableSet` is the same group as a `record struct`, for writing your own types. Held as a field, it needs no
object of its own at all.

```csharp
public sealed class PriceFeed : IDisposable
{
    private DisposableSet _resources;   // not readonly

    public void Track(IDisposable resource) => _resources.Add(resource);

    public void Dispose() => _resources.Dispose();
}
```

> [!WARNING]
> Keep a `DisposableSet` field **non-readonly**, and never copy it into a local variable. A `readonly` field or a
> copy is a separate set: what you add to it is not in the field, and disposing the field does not dispose it.

It has the same members as `MultipleDisposable`, and `Snapshot()`, which copies what it holds into a new
`List<IDisposable>`. Its constructor takes two, three, or an array of disposables. The array form skips `null`
entries.

## Holding one disposable at a time

These types hold one inner disposable. They differ in whether you can replace it, and whether replacing it
disposes the old one.

| Type | Assign with | Assign twice | The old value on replace |
|---|---|---|---|
| `SingleDisposable`, `AssignmentSlot` | `Create(value)` | Throws `InvalidOperationException` | — |
| `OnceDisposable` | `Disposable = value` | Throws `InvalidOperationException` | — |
| `SingleReplaceableDisposable`, `Slot` | `Create(value)` | Allowed | Disposed |
| `SwapDisposable` | `Disposable = value` | Allowed | Disposed |
| `MutableDisposable` | `Disposable = value` | Allowed | **Not** disposed |

In every one, disposing the holder disposes the value it holds, and a value assigned after that is disposed
straight away.

The slots that take an `Action` in their constructor run it once, when the slot is disposed, whether or not a
value was ever assigned. The two families run it in opposite order:

| Type | On `Dispose` |
|---|---|
| `SingleDisposable`, `AssignmentSlot` | Runs the action, **then** disposes the value. |
| `SingleReplaceableDisposable`, `Slot` | Disposes the value, **then** runs the action. |

### `SingleDisposable`

Use `SingleDisposable` when you create the holder first and the value later, but only ever once. A common case is
a subscription that needs to dispose itself from inside its own callback.

```csharp
var slot = new SingleDisposable();

slot.Create(stream.Subscribe(x =>
{
    if (x == 0)
    {
        slot.Dispose();
    }
}));
```

A second `Create` throws `InvalidOperationException` with the message
`The disposable slot has already been assigned.`

The constructor can take the value, an `Action`, or both.

`DisposeWith()` on any disposable wraps it in a `SingleDisposable`. See
[`DisposeWith` on its own](utility.md#disposewith-on-its-own).

### `OnceDisposable`

`OnceDisposable` does the same job through a property. `IsAssigned` tells you whether a value has been set.

```csharp
var once = new OnceDisposable();

Console.WriteLine(once.IsAssigned);   // False
once.Disposable = subscription;
Console.WriteLine(once.IsAssigned);   // True
```

Setting `Disposable` a second time throws `InvalidOperationException`. After you dispose it, `Disposable` reads
as `null`.

### `SingleReplaceableDisposable`

Use `SingleReplaceableDisposable` for "only the latest one counts". Each `Create` disposes the value it
replaces. A search box is the classic case: a new search cancels the one still running.

```csharp
var currentSearch = new SingleReplaceableDisposable();

currentSearch.Create(Search("r"));
currentSearch.Create(Search("rx"));    // disposes the "r" search
currentSearch.Dispose();               // disposes the "rx" search
```

The constructor can take a first value, an `Action`, or both.

### `SwapDisposable`

`SwapDisposable` does the same job through a property. Setting `Disposable` disposes the old value. After you
dispose it, `Disposable` reads as `null`.

```csharp
var swap = new SwapDisposable();

swap.Disposable = Search("r");
swap.Disposable = Search("rx");   // disposes the "r" search
swap.Dispose();                   // disposes the "rx" search
```

### `MutableDisposable`

`MutableDisposable` lets you replace the value **without** disposing the old one. Disposing the holder disposes
only the value it holds at that moment. Use it when something else owns the old value and will dispose it
itself.

```csharp
var mutable = new MutableDisposable();

mutable.Disposable = first;
mutable.Disposable = second;   // first is not disposed
mutable.Dispose();             // disposes second only
```

## In a ReactiveUI app

`WhenActivated` hands you a group for a view's subscriptions and disposes it when the view deactivates. See
[when activated](../handbook/when-activated.md). The [best practices page](best-practices.md#dispose-every-subscription)
explains why every subscription needs disposing.

## Every disposable at a glance

| Type | Second name | What it does |
|---|---|---|
| `ActionDisposable` | — | Runs an action on the first dispose. |
| `Scope` | — | Shortcuts: `Create`, `Combine`, `Empty`. |
| `EmptyDisposable` | — | Does nothing. |
| `BooleanDisposable` | — | Records that dispose happened. |
| `CancellationDisposable` | — | Cancels a token on dispose. |
| `MultipleDisposable` | `Pocket` | A group you can add to, remove from and clear. |
| `DisposableBag` | — | A small group with `Add` only. |
| `DisposableSet` | — | A group held as a struct field in your own type. |
| `SingleDisposable` | `AssignmentSlot` | Holds one value, assigned once with `Create`. |
| `OnceDisposable` | — | Holds one value, assigned once through a property. |
| `SingleReplaceableDisposable` | `Slot` | Holds the latest value, disposing the one before. |
| `SwapDisposable` | — | The same, through a property. |
| `MutableDisposable` | — | Holds the latest value, without disposing the one before. |
| `IsDisposed` | — | The interface with the `IsDisposed` property. |

The async versions, `MultipleDisposableAsync`, `SingleAssignmentDisposableAsync` and
`SingleReplaceableDisposableAsync`, are covered in [async disposal](async/advanced.md#async-disposal).
