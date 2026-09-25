---
Order: 7
---
# Aggregation and results

Sometimes you do not want every value a stream sends. You want one answer about all of them: the total, how
many there were, whether any matched, or the last one.

An **aggregation** operator works through the values and sends that answer as a new stream. A **terminal**
operator goes one step further. It hands you the answer as a `Task`, so you can wait for it in ordinary code
with `await`.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;
```

## Your first aggregation

You have the prices of the items in a shopping basket. You want the total.

**1. Start with the prices.**

```csharp
IObservable<decimal> prices = Signal.FromEnumerable([3.50m, 5.00m, 2.25m]);
```

**2. Add them up.** `Aggregate` starts from a value you give it, called the **seed**. For each price, it calls
your lambda with the total so far and the new price, and keeps what the lambda returns. When the stream
completes, it sends the final total.

```csharp
IObservable<decimal> total = prices.Aggregate(0m, (sum, price) => sum + price);
```

**3. Subscribe.**

```csharp
total.Subscribe(x => Console.WriteLine($"total {x}"));
```

Output:

```text
total 10.75
```

Trace it one price at a time:

| Price | `sum` before | `sum + price` |
|---|---|---|
| `3.50` | `0` (the seed) | `3.50` |
| `5.00` | `3.50` | `8.50` |
| `2.25` | `8.50` | `10.75` |

`Aggregate` sent only the last row, once the prices ran out.

## Running totals and final totals

### `Fold`

`Fold` works the same way as `Aggregate`, but sends the total so far after **every** value.

```csharp
prices.Fold(0m, (sum, price) => sum + price)
      .Subscribe(x => Console.WriteLine($"so far {x}"));
```

Output:

```text
so far 3.50
so far 8.50
so far 10.75
```

Use `Fold` to keep a display up to date as values arrive, such as a running score.

### `Aggregate`

`Aggregate` sends only the final total, when the source completes. The walkthrough above shows it.

### `Fold` against `Aggregate`

| Operator | Second name | Sends | On an empty stream |
|---|---|---|---|
| `Fold` | `Scan` | The total after every value. | Nothing. |
| `Aggregate` | `Reduce` | The final total, once, on completion. | The seed. |

The empty case matters. An empty basket has no values to add up. `Aggregate` still sends `0`, the seed, so you
get a total. `Fold` sends nothing at all, because it only sends after a value.

```csharp
Signal.Empty<decimal>().Aggregate(0m, (sum, price) => sum + price)
      .Subscribe(x => Console.WriteLine($"Aggregate: {x}"));

Signal.Empty<decimal>().Fold(0m, (sum, price) => sum + price)
      .Subscribe(x => Console.WriteLine($"Fold: {x}"));
```

Output:

```text
Aggregate: 0
```

`Aggregate` and `Fold` only send a value when the source completes or sends. A stream that never completes never
gets a result from `Aggregate`.

## Counting

### `Count`

`Count` sends how many values the stream sent, when it completes. Give it a test, and it counts only the
values that pass.

```csharp
IObservable<string> names = Signal.FromEnumerable(["Ada", "Grace", "Alan"]);

names.Count().Subscribe(x => Console.WriteLine(x));
names.Count(name => name.StartsWith("A")).Subscribe(x => Console.WriteLine(x));
```

Output:

```text
3
2
```

### `LongCount`

`LongCount` does the same as `Count`, but sends a `long` instead of an `int`. Use it when a stream could send
more than about two billion values, which is more than an `int` can hold.

```csharp
names.LongCount().Subscribe(x => Console.WriteLine(x));   // 3
```

## Asking a yes or no question

These operators each send one `true` or `false`. They answer as soon as they know. They do not wait for the
stream to complete if an earlier value already settles the answer.

### `Any`

`Any` sends `true` as soon as the stream sends a value, and stops listening. Give it a test, and it waits for a
value that passes.

```csharp
var orders = new Signal<int>();

orders.Any().Subscribe(x => Console.WriteLine($"any orders: {x}"));

orders.OnNext(42);   // prints any orders: True
```

`Any` answered the moment the first order arrived. It did not wait for `orders` to complete.

### `All`

`All` checks every value against a test. It sends `false` as soon as one fails. If every value passes, it
sends `true` when the stream completes.

```csharp
Signal.FromEnumerable([4, 8, 15])
      .All(x => x % 2 == 0)
      .Subscribe(x => Console.WriteLine(x));   // False
```

### `Contains`

`Contains` sends `true` as soon as the stream sends the value you are looking for. Give it an
`IEqualityComparer<T>` to decide what counts as a match.

```csharp
Signal.FromEnumerable(["Ada", "Grace"])
      .Contains("ADA", StringComparer.OrdinalIgnoreCase)
      .Subscribe(x => Console.WriteLine(x));   // True
```

### `IsEmpty`

`IsEmpty` sends `false` as soon as the stream sends a value. If the stream completes without sending anything,
it sends `true`.

```csharp
Signal.Empty<int>().IsEmpty().Subscribe(x => Console.WriteLine(x));   // True
```

### What each one says about an empty stream

An empty stream is one that completes without sending a value. Each question has a fixed answer for it:

| Operator | Answer on an empty stream | Why |
|---|---|---|
| `Any` | `false` | Nothing was sent, so nothing matched. |
| `All` | `true` | Nothing was sent, so nothing failed the test. |
| `Contains` | `false` | The value you wanted never arrived. |
| `IsEmpty` | `true` | Nothing was sent. |
| `Count` | `0` | Nothing was sent. |

`All` answering `true` surprises people. It follows the same rule as LINQ's `All`: a rule nobody broke counts
as kept.

## Gathering every value

### `ToList`

`ToList` gathers every value into a list and sends the list once, when the stream completes.

```csharp
Signal.Range(1, 3)
      .ToList()
      .Subscribe(list => Console.WriteLine(string.Join(", ", list)));   // 1, 2, 3
```

An empty stream gives you an empty list.

### `ToArray`

`ToArray` does the same, and sends an array instead of a list.

```csharp
Signal.Range(1, 3)
      .ToArray()
      .Subscribe(array => Console.WriteLine($"{array.Length} items"));   // 3 items
```

## Getting a result as a task

These operators turn a stream into a `Task<T>`, so you can `await` its result in an `async` method.

### `FirstAsync` and `LastAsync`

`FirstAsync` waits for the first value. It finishes as soon as that value arrives, without waiting for the
stream to complete. `LastAsync` waits for the stream to complete and gives you its last value.

```csharp
int first = await Signal.Range(5, 3).FirstAsync();   // 5
int last = await Signal.Range(5, 3).LastAsync();     // 7
```

If the stream completes without sending anything, both throw an `InvalidOperationException`.

### `FirstOrDefaultAsync` and `LastOrDefaultAsync`

These work like `FirstAsync` and `LastAsync`, but do not throw on an empty stream. They give you a fallback
instead: the `default` for the type, such as `0` or `null`, or a value you pass in.

```csharp
int fallback = await Signal.Empty<int>().FirstOrDefaultAsync(-1);   // -1
int lastFallback = await Signal.Empty<int>().LastOrDefaultAsync(-1); // -1
int zero = await Signal.Empty<int>().FirstOrDefaultAsync();         // 0, the default for an int
```

### `ToTask`

`ToTask` waits for the stream to complete and gives you its last value. On an empty stream it throws an
`InvalidOperationException`.

```csharp
int result = await Signal.Range(5, 3).ToTask();   // 7
```

`Signal.ToTask(source)` does the same, taking the stream as an argument.

`Signal.ToTaskOrDefault(source, defaultValue, cancellationToken)` gives you the default value on an empty stream
instead of throwing. It is what `LastOrDefaultAsync` runs on.

```csharp
using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(5));

Console.WriteLine(await Signal.ToTaskOrDefault(Signal.Empty<int>(), -1, timeout.Token));
Console.WriteLine(await Signal.ToTaskOrDefault(Signal.Range(1, 3), -1, timeout.Token));
```

Output:

```text
-1
3
```

### `await` a stream directly

You can put `await` straight in front of a stream. It behaves like `ToTask`, and gives you the last value.

```csharp
int result = await Signal.Range(5, 3);   // 7
```

This works because the library gives every stream a `GetAwaiter` method, which is what `await` looks for.

To be able to stop waiting, call `GetAwaiter` yourself with a `CancellationToken`, and await what it gives you.
Cancelling the token throws an `OperationCanceledException`.

```csharp
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

int result = await liveReadings.GetAwaiter(cts.Token);
```

### `RunAsync`

`RunAsync` subscribes to the stream **straight away**, and hands you something to `await` later. Awaiting a
stream only subscribes at the moment you write `await`.

That difference matters for a `Signal<T>`, which sends values whether or not anyone is listening:

```csharp
var live = new Signal<int>();
var running = Signal.RunAsync(live);   // subscribes now

live.OnNext(1);
live.OnNext(2);
live.OnCompleted();

int lastValue = await running;   // 2
```

`RunAsync` was already listening when `1` and `2` were sent, so it caught them.

Awaiting the same signal after it has already completed does not work:

```csharp
var live = new Signal<int>();

live.OnNext(1);
live.OnNext(2);
live.OnCompleted();

int lastValue = await live;   // throws InvalidOperationException
```

`await live` subscribed after `live` had finished. It only saw the completion, with no values, so there was no
last value to give you. Use `RunAsync` to start listening before the values you care about are sent.

### Counting and gathering as tasks

Most of the aggregations above also have a task form, named with `Async` on the end.

```csharp
int count = await Signal.Range(1, 5).CountAsync(x => x > 3);           // 2
bool any = await Signal.Range(1, 5).AnyAsync(x => x > 3);              // True
IList<int> list = await Signal.Range(1, 3).ToListAsync();              // 1, 2, 3
int[] array = await Signal.Range(1, 3).ToArrayAsync();                 // 1, 2, 3
```

`CountAsync`, `AnyAsync`, `ToListAsync` and `ToArrayAsync` follow the same rules as their stream versions.

### Giving up on the wait

Waiting for a stream that never sends can leave your code stuck. Most of the task operators take a
`CancellationToken`. When the token is cancelled, the task throws a `TaskCanceledException`.

```csharp
using var cts = new CancellationTokenSource();

var waiting = new Signal<int>().FirstAsync(cts.Token);
cts.Cancel();

int value = await waiting;   // throws TaskCanceledException
```

To give up after a set time, create the source with that time: `new CancellationTokenSource(TimeSpan.FromSeconds(5))`.

You can also add a cancellation token to a task you already have, with `ToTask`:

```csharp
using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(30));

Task<string> download = DownloadAsync();
string page = await download.ToTask(timeout.Token);
```

### `FirstAsTask` and `FirstAsValueTask`

These two helpers wait for the first value, like `FirstAsync`. You call them on a helper class, passing the
stream in.

```csharp
using ReactiveUI.Primitives.Extensions;

int first = await FirstAsTaskHelper.FirstAsTask(Signal.Range(5, 3));                    // 5
int firstAgain = await FirstAsValueTaskHelper<int>.FirstAsValueTask(Signal.Range(5, 3)); // 5
```

`FirstAsValueTask` returns a `ValueTask<T>`, which avoids allocating a `Task<T>` when the result is ready at
once.

## Handling cancellation

`HandleCancellation` waits for a task and treats cancellation as normal. Instead of throwing when the task is
cancelled, it runs your callback and carries on. A task that returns a value gives you the `default` for that
type.

```csharp
using var cts = new CancellationTokenSource();

Task<int> countTask = CountVisitorsAsync(cts.Token);
cts.Cancel();

int result = await countTask.HandleCancellation(() => Console.WriteLine("cancelled"));
```

If `countTask` was cancelled, that prints `cancelled`, and `result` is `0`, the `default` for an `int`. A task
that fails for any other reason still throws. Only cancellation is handled.

It works the same way on a `Task` that returns nothing:

```csharp
await job.HandleCancellation(() => Console.WriteLine("cancelled"));
Console.WriteLine("carried on");
```

It also works straight on a stream. Give it a `CancellationToken`, and it waits for the stream's last value. If
the token is cancelled first, it stops waiting, runs your callback, and gives you the `default`.

```csharp
using var cts = new CancellationTokenSource();

Task<int> waiting = liveReadings.HandleCancellation(() => Console.WriteLine("cancelled"), cts.Token);
cts.Cancel();

int reading = await waiting;   // prints cancelled, and reading is 0
```

If the stream completes before you cancel, you get its last value instead.

## Blocking until a stream completes

### `ToEnumerable`

`ToEnumerable` turns a stream into an ordinary collection you can loop over with `foreach`.

```csharp
foreach (var x in Signal.Range(1, 3).ToEnumerable())
{
    Console.WriteLine(x);
}
```

Output:

```text
1
2
3
```

> [!WARNING]
> `ToEnumerable` **blocks**. The thread that calls it stops and waits until the stream completes. Never call it
> on a UI thread, or your app freezes until the stream ends. A stream that never completes blocks for ever.
> In most code, prefer `await` with `CollectListAsync`.

## Waiting for results in a ReactiveUI app

A ReactiveUI command's `Execute` method returns a stream. The task operators on this page are how you wait for
its result in ordinary code, for example `await command.Execute().FirstAsync()`. See
[commands](../reactiveui/handbook/commands/index.md).

## Every aggregation and result operator at a glance

| Operator | Second name | Gives you |
|---|---|---|
| `Fold` | `Scan` | A stream of the total after every value. |
| `Aggregate` | `Reduce` | A stream of the final total, on completion. |
| `Count` / `LongCount` | — | A stream of how many values were sent. |
| `Any` | — | A stream of whether any value was sent, or passed a test. |
| `All` | — | A stream of whether every value passed a test. |
| `Contains` | — | A stream of whether a value was sent. |
| `IsEmpty` | — | A stream of whether no value was sent. |
| `ToList` / `ToArray` | `CollectList` / `CollectArray` | A stream of every value, gathered. |
| `FirstAsync` / `LastAsync` | — | A task of the first or last value. |
| `FirstOrDefaultAsync` / `LastOrDefaultAsync` | — | The same, with a fallback on an empty stream. |
| `ToTask` | `Signal.ToTask` | A task of the last value. |
| `Signal.ToTaskOrDefault` | — | A task of the last value, or a default on an empty stream. |
| `await stream` | `GetAwaiter` | The last value. |
| `RunAsync` | — | Subscribes now, and gives you the last value later. |
| `CountAsync`, `AnyAsync`, `ToListAsync`, `ToArrayAsync` | `CollectListAsync`, `CollectArrayAsync` | Tasks of the same answers. |
| `FirstAsTask` / `FirstAsValueTask` | — | A task of the first value, called on a helper class. |
| `HandleCancellation` | — | Waits for a task or a stream, and treats cancellation as normal. |
| `ToEnumerable` | — | A collection you can loop over. Blocks. |

## The types behind these operators

`FoldSignal<TSource, TAccumulate>`, `ReduceSignal<TSource, TAccumulate>` and `IsEmptySignal<T>` are public
classes in `ReactiveUI.Primitives.Advanced`. Each takes its source through the constructor. Awaiting a stream
gives you an `IAwaitSignal<T>`, from `ReactiveUI.Primitives.Signals`.

```csharp
using ReactiveUI.Primitives.Advanced;

// the operator
IObservable<decimal> a = prices.Aggregate(0m, (sum, price) => sum + price);

// the same thing, built directly
IObservable<decimal> b = new ReduceSignal<decimal, decimal>(prices, 0m, (sum, price) => sum + price);
```

Calling the operator is the normal path. Construct the type when you are writing an operator of your own and
want to place one inside it.

## The two package flavours

Every operator here ships twice. `ReactiveUI.Primitives` puts them under `ReactiveUI.Primitives.*`.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and puts them under
`ReactiveUI.Primitives.Reactive.*`. They behave the same in both.
