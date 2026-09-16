---
Order: 3
---
# Async filtering

A filtering operator decides which values get through. Some also stop the stream early.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Async;
using ReactiveUI.Primitives.Async.Signals;
```

## Keeping values that pass a test

### `Where`

`Where` passes the values your test accepts. The async form awaits the test, for a check that needs a lookup.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.Range(1, 6).Where(static x => x % 2 == 0).ToListAsync()));

List<int> known = await SignalAsync.Range(1, 4)
                                   .Where(static async (id, cancellationToken) =>
                                   {
                                       await Task.Delay(5, cancellationToken);   // stands in for a database check
                                       return id != 3;
                                   })
                                   .ToListAsync();

Console.WriteLine(string.Join(", ", known));
```

Output:

```text
2, 4, 6
1, 2, 4
```

### `KeepWith`

`KeepWith` passes a state object to your test with each value, so the test can be `static`.

```csharp
var minimum = 3;
Console.WriteLine(string.Join(", ", await SignalAsync.Range(1, 5).KeepWith(minimum, static (min, x) => x >= min).ToListAsync()));
```

Output:

```text
3, 4, 5
```

### `WhereIsNotNull` and `SkipWhileNull`

`WhereIsNotNull` drops every `null`, and hands back a stream whose type cannot be `null`. `SkipWhileNull` drops
`null` values only until the first value that is not `null`.

```csharp
IObservableAsync<string?> names = SignalAsync.FromEnumerable<string?>([null, "Ada", null, "Grace"]);

Console.WriteLine(string.Join(", ", await names.WhereIsNotNull().ToListAsync()));
Console.WriteLine(string.Join(", ", (await names.SkipWhileNull().ToListAsync()).Select(static n => n ?? "null")));
```

Output:

```text
Ada, Grace
Ada, null, Grace
```

### `WhereTrue` and `WhereFalse`

These work on a stream of `bool`, and pass only `true` or only `false`.

```csharp
IObservableAsync<bool> flags = SignalAsync.FromEnumerable([true, false, true]);
Console.WriteLine($"{(await flags.WhereTrue().ToListAsync()).Count} true, {(await flags.WhereFalse().ToListAsync()).Count} false");
```

Output:

```text
2 true, 1 false
```

## Dropping repeats

### `Distinct` and `DistinctBy`

`Distinct` drops any value it has seen before, anywhere in the stream. `DistinctBy` compares a key you choose instead
of the whole value. Both take an `IEqualityComparer` as well.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.FromEnumerable([1, 1, 2, 1]).Distinct().ToListAsync()));
Console.WriteLine(string.Join(", ", await SignalAsync.FromEnumerable(["apple", "avocado", "banana"]).DistinctBy(static w => w[0]).ToListAsync()));
```

Output:

```text
1, 2
apple, banana
```

Every value `Distinct` has seen stays in memory, so a long-running stream keeps growing.

### `Unique` and `UniqueBy`

`Unique` drops a value only when it equals the one right before it. `UniqueBy` compares a key you choose.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.FromEnumerable([1, 1, 2, 1]).Unique().ToListAsync()));
Console.WriteLine(string.Join(", ", await SignalAsync.FromEnumerable([1, 3, 2, 5]).UniqueBy(static x => x % 2).ToListAsync()));
```

Output:

```text
1, 2, 1
1, 2, 5
```

### `LatestOrDefault`

`LatestOrDefault` sends a default value first. After that it sends the source's values, skipping any value equal to
the one it sent last.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.FromEnumerable([5, 5, 6]).LatestOrDefault(-1).ToListAsync()));
```

Output:

```text
-1, 5, 6
```

## Taking part of a stream

### `Take` and `Skip`

`Take` sends the first values up to a count, then completes. `Skip` drops the first values up to a count.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.Range(1, 10).Take(3).ToListAsync()));
Console.WriteLine(string.Join(", ", await SignalAsync.Range(1, 5).Skip(3).ToListAsync()));
```

Output:

```text
1, 2, 3
4, 5
```

### `TakeWhile` and `SkipWhile`

`TakeWhile` sends values while a test passes, and completes at the first value that fails. `SkipWhile` drops values
while a test passes, then sends everything from the first value that fails. Both have async forms.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.FromEnumerable([1, 2, 5, 1]).TakeWhile(static x => x < 3).ToListAsync()));
Console.WriteLine(string.Join(", ", await SignalAsync.FromEnumerable([1, 2, 5, 1]).SkipWhile(static x => x < 3).ToListAsync()));
```

Output:

```text
1, 2
5, 1
```

### `WaitUntil`

`WaitUntil` drops values until one passes the test, sends that one value, then completes.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.Range(1, 5).WaitUntil(static x => x >= 3).ToListAsync()));
```

Output:

```text
3
```

## Stopping on a signal from outside

### `TakeUntil`

`TakeUntil` sends values until something tells it to stop, then completes. The stop can be:

| Stop | Overload |
|---|---|
| A value that passes a test | `TakeUntil(predicate)`, plain or async. The matching value is sent, then the stream completes. |
| Another stream sending a value | `TakeUntil(other)` |
| A `CancellationToken` cancelling | `TakeUntil(cancellationToken)` |
| A `Task` finishing | `TakeUntil(task)` |
| Your own stop callback | `TakeUntil(stopSignal)`, with a `CompletionSignalDelegate` |

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.Range(1, 5).TakeUntil(static x => x >= 3).ToListAsync()));

ISignalAsync<string> messages = Signal.Create<string>();
var saved = new TaskCompletionSource();

Task<List<string>> collected = messages.TakeUntil(saved.Task).ToListAsync().AsTask();

await messages.OnNextAsync("draft 1", CancellationToken.None);
await messages.OnNextAsync("draft 2", CancellationToken.None);
saved.SetResult();
await messages.OnNextAsync("too late", CancellationToken.None);

Console.WriteLine(string.Join(", ", await collected));
```

Output:

```text
1, 2, 3
draft 1, draft 2
```

The other-stream and task overloads take a `TakeUntilOptions`. Set `SourceFailsWhenOtherFails` to `true` to fail the
stream when the stopping stream or task fails. By default a failure there just stops the stream.

A `CompletionSignalDelegate` gets a method to call with a `Result` when it is time to stop, and returns an
`IAsyncDisposable` that `TakeUntil` disposes when the stream ends.

## Splitting and dropping busy values

### `Partition`

`Partition` splits one stream into two. Values that pass the test go to `True`, and the rest go to `False`. Both halves
share one subscription to the source, so subscribe to both before any value is sent.

```csharp
ISignalAsync<int> numbers = Signal.Create<int>();
var (evens, odds) = numbers.Partition(static x => x % 2 == 0);

await using IAsyncDisposable evenSubscription = await evens.SubscribeAsync(static x => Console.WriteLine($"even {x}"));
await using IAsyncDisposable oddSubscription = await odds.SubscribeAsync(static x => Console.WriteLine($"odd {x}"));

for (var i = 1; i <= 3; i++)
{
    await numbers.OnNextAsync(i, CancellationToken.None);
}
```

Output:

```text
odd 1
even 2
odd 3
```

### `DropIfBusy`

`DropIfBusy` runs an async method with each value, then passes the value on. A sender that awaits `OnNextAsync` waits
for the method, as with any async operator. A value sent at the same time, without waiting for the one before, is
dropped while the method is still running.

```csharp
ISignalAsync<int> presses = Signal.Create<int>();

await using IAsyncDisposable subscription = await presses
    .DropIfBusy(static async (press, cancellationToken) => await Task.Delay(100, cancellationToken))
    .SubscribeAsync(static press => Console.WriteLine($"refreshed for press {press}"));

ValueTask first = presses.OnNextAsync(1, CancellationToken.None);
ValueTask second = presses.OnNextAsync(2, CancellationToken.None);   // dropped: still busy with press 1

await first;
await second;
```

Output:

```text
refreshed for press 1
```

## Every operator on this page at a glance

| Operator | Second name | What it does |
|---|---|---|
| `Where` | `Keep` | Passes values that pass a test. |
| `KeepWith` | — | `Where` with a state object. |
| `WhereIsNotNull` | `KeepNotNull` | Drops `null`. |
| `SkipWhileNull` | — | Drops `null` until the first value that is not `null`. |
| `WhereTrue` / `WhereFalse` | — | Passes only `true`, or only `false`. |
| `Distinct` | — | Drops any value seen before. |
| `DistinctBy` | — | `Distinct` by a key. |
| `Unique` | `DistinctUntilChanged` | Drops a value equal to the one before it. |
| `UniqueBy` | `DistinctUntilChangedBy` | `Unique` by a key. |
| `LatestOrDefault` | — | A default first, then values that differ from the last one sent. |
| `Take` / `Skip` | — | The first values up to a count, or all after them. |
| `TakeWhile` / `SkipWhile` | — | Values while a test passes, or from the first that fails. |
| `WaitUntil` | — | The first value that passes a test, then completes. |
| `TakeUntil` | — | Values until a test, a stream, a token, a task or a callback says stop. |
| `Partition` | — | Splits a stream in two by a test. |
| `DropIfBusy` | — | Runs async work, dropping values that arrive while it runs. |
