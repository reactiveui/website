---
Order: 6
---
# ReactiveUI.Primitives

ReactiveUI.Primitives is a small, fast library for reactive programming in .NET. Reactive programming
means working with values that arrive over time, such as button clicks, timer ticks, or network replies,
rather than values you already hold.

If you know LINQ, you already know the shape. LINQ queries a collection you hold and pulls values out of an
`IEnumerable<T>`. Reactive programming queries values that arrive over time: an `IObservable<T>` pushes each
value to you as it happens.

```
dotnet add package ReactiveUI.Primitives
```

## Your first stream

**1. Make a stream.** `Signal` holds the factories that build one.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;

IObservable<int> numbers = Signal.FromEnumerable([1, 2, 3, 4, 5]);
```

**2. Shape it.** `Keep` passes through the values a test accepts. `Map` runs a function on each value.

```csharp
IObservable<int> odds = numbers.Keep(x => x % 2 == 1)
                               .Map(x => x * 10);
```

**3. Subscribe.** Nothing runs until you do.

```csharp
odds.Subscribe(x => Console.WriteLine(x));   // 10, 30, 50
```

`Subscribe` hands back an `IDisposable`. Dispose it to stop listening.

## Two names for one operator

This library gives some operators a second name. `Map` is also `Select`, and `Keep` is also `Where`. Both
names build the same thing, so pick the one that reads better to you and use it consistently.

| Name | Other name | What it does |
|---|---|---|
| `Map` | `Select` | Runs a function on each value. |
| `Keep` | `Where` | Passes through the values a test accepts. |
| `Fold` | `Scan` | Emits the running total after every value. |
| `Reduce` | `Aggregate` | Emits one final total when the stream ends. |
| `Spark` | `Materialize` | Turns each notification into a value you can inspect. |

## The operator pages

Operators are grouped by what they do. Each page covers every operator in its group, with an example for
each one.

| Group | What it covers |
|---|---|
| [Creation factories](creation-factories.md) | Building a stream from a value, a collection, a task, an event or a timer. |
| Transformation | Changing each value, or flattening a stream of streams. |
| Filtering | Dropping values you do not want. |
| Combination | Joining two or more streams into one. |
| Time | Delaying, batching, sampling and timing out. |
| Error handling | Recovering from a failure, retrying, and cleaning up. |
| Aggregation and terminal | Reducing a stream to one value, or leaving the stream for a task. |
| Utility | Subscribing, scheduling, side effects and disposal. |

The async surface repeats those eight groups for `IObservableAsync<T>`, and separate pages cover subjects,
sequencers, disposables and the delivery types.

## The two package flavours

Every type ships twice. `ReactiveUI.Primitives` is the lean build with no dependency on System.Reactive.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and its types sit
under `ReactiveUI.Primitives.Reactive`. Pick the lean package unless your app already uses System.Reactive.

The full type list is in the [API reference](../../api/index.md).
