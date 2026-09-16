---
Order: 14
---
# Async streams

`IObservable<T>` calls your subscriber with a plain method call. If your subscriber starts async work, such as a
database write, the stream does not wait for it. Values pile up, and calls overlap.

The `ReactiveUI.Primitives.Async` package gives you streams built on `await` instead. Every step returns a
`ValueTask`, so the code that sends a value waits until each subscriber has finished with it. A slow subscriber slows
the sender down, and nothing piles up. This is called **backpressure**.

```
dotnet add package ReactiveUI.Primitives.Async
```

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Async;
using ReactiveUI.Primitives.Async.Signals;
```

## The types

| Type | What it is |
|---|---|
| `IObservableAsync<T>` | An async stream. `SubscribeAsync` returns a `ValueTask<IAsyncDisposable>`. |
| `IObserverAsync<T>` | Receives the values. Its three methods, `OnNextAsync`, `OnErrorResumeAsync` and `OnCompletedAsync`, each return a `ValueTask`. |
| `ISignalAsync<T>` | An async **signal**: a stream you send values into yourself. It is both of the above. |
| `Result` | How a stream ended. `Result.Success`, or `Result.Failure(exception)`. |

## Your first async stream

You save each order to a database. Each save takes time, and you want to save one at a time.

**1. Make a signal.** `Signal.Create<T>` in `ReactiveUI.Primitives.Async.Signals` builds an async signal.

```csharp
ISignalAsync<int> orders = Signal.Create<int>();
```

**2. Shape it and subscribe.** Operators work as they do on `IObservable<T>`. `SubscribeAsync` takes an async lambda
that gets a `CancellationToken`. Pass the token on to the async methods you call. `await using` disposes the
subscription at the end of the block.

```csharp
await using IAsyncDisposable subscription = await orders
    .Where(static id => id > 0)
    .SubscribeAsync(static async (id, cancellationToken) =>
    {
        await Task.Delay(100, cancellationToken);   // stands in for the database write
        Console.WriteLine($"saved order {id}");
    });
```

**3. Send values, and await each one.**

```csharp
await orders.OnNextAsync(1, CancellationToken.None);
Console.WriteLine("sent 1");

await orders.OnNextAsync(-5, CancellationToken.None);
Console.WriteLine("sent -5");

await orders.OnNextAsync(2, CancellationToken.None);
Console.WriteLine("sent 2");

await orders.OnCompletedAsync(Result.Success);
```

Output:

```text
saved order 1
sent 1
sent -5
saved order 2
sent 2
```

`sent 1` printed only after order 1 was saved. The sender waited for the subscriber. `-5` did not pass `Where`, so
nothing waited.

## Errors that end a stream, and errors that do not

A sync stream has one kind of error, and it ends the stream. An async stream has two.

- **A resumable error** reports a problem and carries on. The sender calls `OnErrorResumeAsync`. More values can
  follow.
- **A failure** ends the stream. The sender calls `OnCompletedAsync` with `Result.Failure(exception)`. Nothing
  follows.

```csharp
ISignalAsync<int> readings = Signal.Create<int>();

await using IAsyncDisposable subscription = await readings.SubscribeAsync(
    static (value, _) => { Console.WriteLine($"value {value}"); return default; },
    static (error, _) => { Console.WriteLine($"resumable: {error.Message}"); return default; },
    static result => { Console.WriteLine($"ended, success: {result.IsSuccess}"); return default; });

await readings.OnNextAsync(1, CancellationToken.None);
await readings.OnErrorResumeAsync(new InvalidOperationException("sensor glitch"), CancellationToken.None);
await readings.OnNextAsync(2, CancellationToken.None);
await readings.OnCompletedAsync(Result.Failure(new InvalidOperationException("sensor lost")));
```

Output:

```text
value 1
resumable: sensor glitch
value 2
ended, success: False
```

The terminal operators, such as `ToListAsync`, throw the failure's exception when you await them.

## The async pages

| Page | What it covers |
|---|---|
| [Creation factories](creation-factories.md) | Building a stream from a value, a collection, a task, a timer or your own code. |
| [Transformation](transformation.md) | Changing each value, flattening, grouping and running totals. |
| [Filtering](filtering.md) | Dropping values, and stopping a stream early. |
| [Combination](combination.md) | Joining two or more streams. |
| [Time](time.md) | Delaying, waiting for quiet, sampling and timing out. |
| [Error handling](error-handling.md) | Recovering, retrying and logging. |
| [Results](results.md) | Awaiting one answer: the first value, a count, a list. |
| [Utility and sharing](utility.md) | Side effects, sharing one subscription, and choosing where callbacks run. |
| [Async signals](signals.md) | The signals you send values into, and their options. |
| [Writing your own operator](advanced.md) | Witness contracts, async disposables and the global error handler. |

## Sync or async?

| Use | When |
|---|---|
| `IObservable<T>` | Subscribers do quick work, such as updating the screen. You want the lowest overhead. |
| `IObservableAsync<T>` | Subscribers do async work, and the sender must wait for them. |

`IObservableAsync<T>` is a different interface, so a sync operator does not apply to it, and the other way round.
Most operators exist on both, under the same names.

## The two package flavours

`ReactiveUI.Primitives.Async` works with the lean `ReactiveUI.Primitives`. `ReactiveUI.Primitives.Async.Reactive` is
the same source compiled against System.Reactive, for apps that use its `Unit` and `IScheduler`. They behave the same
in both.
