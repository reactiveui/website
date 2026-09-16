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

New to streams, or wondering why ReactiveUI uses this library? Start with [why Primitives](why-primitives.md).

## Your first stream

**1. Make a stream.** `Signal` holds the factories that build one.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;

IObservable<int> numbers = Signal.FromEnumerable([1, 2, 3, 4, 5]);
```

**2. Shape it.** `Where` passes through the values a test accepts. `Select` runs a lambda on each value.

```csharp
IObservable<int> odds = numbers.Where(x => x % 2 == 1)
                               .Select(x => x * 10);
```

**3. Subscribe.** Nothing runs until you do.

```csharp
odds.Subscribe(x => Console.WriteLine(x));   // 10, 30, 50
```

`Subscribe` hands back an `IDisposable`. Dispose it to stop listening.

## Two names for one operator

Many operators have two names, and both build the same thing. One matches LINQ or the Primitives style, such as
`Where` or `Calm`. The other matches the names used by other reactive libraries, such as System.Reactive and
RxJS, such as `Throttle`. Use whichever suits you and your team.

This site uses the LINQ name when LINQ has the operator, and the Primitives name otherwise. Each page lists the
other names in the table at its end, and the [System.Reactive page](system-reactive.md#operator-names) lists them
all.

## The operator pages

Operators are grouped by what they do. Each page covers every operator in its group, with an example for
each one.

| Group | What it covers |
|---|---|
| [Creation factories](creation-factories.md) | Building a stream from a value, a collection, a task, an event or a timer. |
| [Transformation](transformation.md) | Changing each value, or flattening a stream of streams. |
| [Filtering](filtering.md) | Dropping values you do not want. |
| [Combination](combination.md) | Joining two or more streams into one. |
| [Time](time.md) | Delaying, batching, sampling and timing out. |
| [Error handling](error-handling.md) | Recovering from a failure, retrying, and cleaning up. |
| [Aggregation and results](aggregation.md) | Reducing a stream to one answer, or waiting for a result with `await`. |
| [Utility](utility.md) | Subscribing, choosing where code runs, peeking at values, and cleaning up. |
| [Sharing one subscription](sharing.md) | Letting several subscribers share one run of a stream, and keeping a current value. |
| [Signals you push values into](signals.md) | `Signal<T>` and the other signals: remembering values, many threads, commands and cancellable work. |
| [Sequencers and scheduling](scheduling.md) | Choosing where and when work runs, and testing with a virtual clock. |
| [UI platforms](platforms.md) | Updating the screen from a stream on WPF, WinForms, WinUI, Avalonia, MAUI, Blazor, Android and Apple. |
| [Disposables](disposables.md) | Grouping subscriptions, swapping the latest one, and disposing each exactly once. |
| [ReactiveUI.Primitives and System.Reactive](system-reactive.md) | Names, types and behaviour that differ, AOT and speed, and using both together. |
| [Best practices](best-practices.md) | Habits that avoid leaks, frozen screens and slow tests. |
| [Extension helpers](extensions/index.md) | Ready-made helpers: retries with delays, async work per value, spotting a quiet stream, and waiting in tests. |
| [Async streams](async/index.md) | `IObservableAsync<T>`, where the sender waits for each subscriber, with its own operator pages. |

## The two package flavours

Every type ships twice. `ReactiveUI.Primitives` is the lean build with no dependency on System.Reactive.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and its types sit
under `ReactiveUI.Primitives.Reactive`. Pick the lean package unless your app must share System.Reactive types
with other code. See [using both libraries together](system-reactive.md#using-both-libraries-together).
