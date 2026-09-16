---
Order: 7
---
# Async results

These operators end a pipeline. Instead of another stream, they hand you a `ValueTask` for one answer, so you `await`
them. Each one subscribes, waits for what it needs, and disposes its subscription.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Async;
```

Every operator here also takes a `CancellationToken`. Cancelling it stops the wait and throws
`OperationCanceledException`. If the stream fails, the operator throws the failure's exception.

## One value

### `FirstAsync` and `FirstOrDefaultAsync`

`FirstAsync` hands back the first value, or the first that passes a test. It throws `InvalidOperationException` if the
stream completes with no such value. `FirstOrDefaultAsync` hands back a default instead: `default`, or a value you give.

```csharp
Console.WriteLine(await SignalAsync.Range(1, 10).FirstAsync(static n => n % 2 == 0));
Console.WriteLine(await SignalAsync.Empty<int>().FirstOrDefaultAsync(-1));
```

Output:

```text
2
-1
```

`FirstAsync` stops the stream as soon as it has its value, so it works on a stream that never ends.

### `LastAsync` and `LastOrDefaultAsync`

`LastAsync` waits for the stream to complete, and hands back its final value, or the final one that passes a test.
`LastOrDefaultAsync` hands back a default when there is none.

```csharp
Console.WriteLine(await SignalAsync.Range(1, 5).LastAsync());
Console.WriteLine(await SignalAsync.Empty<int>().LastOrDefaultAsync(-1));
```

Output:

```text
5
-1
```

### `SingleAsync` and `SingleOrDefaultAsync`

`SingleAsync` expects exactly one value, or exactly one that passes a test. It throws `InvalidOperationException` for
none or for more than one. `SingleOrDefaultAsync` hands back a default for none, and still throws for more than one.

```csharp
Console.WriteLine(await SignalAsync.Emit("only").SingleAsync());

try
{
    await SignalAsync.Range(1, 2).SingleAsync();
}
catch (InvalidOperationException)
{
    Console.WriteLine("more than one value");
}
```

Output:

```text
only
more than one value
```

## Questions about the whole stream

### `CountAsync` and `LongCountAsync`

`CountAsync` counts the values, or the values that pass a test. `LongCountAsync` hands back a `long`.

### `AnyAsync` and `AllAsync`

`AnyAsync` answers whether any value arrived, or any passed a test. It stops at the first match. `AllAsync` answers
whether every value passes a test, and stops at the first that fails.

### `ContainsAsync`

`ContainsAsync` answers whether a value arrived, compared with `EqualityComparer<T>.Default` or a comparer you give.

```csharp
IObservableAsync<int> scores = SignalAsync.FromEnumerable([70, 85, 92]);

Console.WriteLine(await scores.CountAsync(static s => s >= 80));
Console.WriteLine(await scores.LongCountAsync());
Console.WriteLine(await scores.AnyAsync(static s => s > 90));
Console.WriteLine(await scores.AllAsync(static s => s >= 50));
Console.WriteLine(await scores.ContainsAsync(85));
```

Output:

```text
2
3
True
True
True
```

### `AggregateAsync`

`AggregateAsync` starts from a seed, runs your lambda on the running total and each value, and hands back the final
total when the stream completes. Give it a third lambda to change the final total into the result. The accumulator
lambda can also be async.

```csharp
IObservableAsync<int> amounts = SignalAsync.FromEnumerable([10, 5, 1]);

Console.WriteLine(await amounts.AggregateAsync(0, static (total, x) => total + x));
Console.WriteLine(await amounts.AggregateAsync(0, static (total, x) => total + x, static total => $"total: {total}"));
```

Output:

```text
16
total: 16
```

[`Fold`](transformation.md#fold) sends the running total after every value instead.

## Collecting every value

### `ToListAsync` and `CollectArrayAsync`

`ToListAsync` waits for the stream to complete and hands back every value in a `List<T>`, in the order they arrived.
`CollectArrayAsync` hands back an array.

```csharp
List<int> list = await SignalAsync.Range(1, 3).ToListAsync();
int[] array = await SignalAsync.Range(1, 3).CollectArrayAsync();

Console.WriteLine($"{list.Count} {array.Length}");
```

Output:

```text
3 3
```

### `ToDictionaryAsync`

`ToDictionaryAsync` hands back a `Dictionary<TKey, TValue>`, keyed by a lambda you give. A second lambda chooses the
value. Two values with the same key throw `ArgumentException`, as `Dictionary.Add` does.

```csharp
IObservableAsync<string> fruit = SignalAsync.FromEnumerable(["apple", "banana"]);

Dictionary<char, int> lengths = await fruit.ToDictionaryAsync(static f => f[0], static f => f.Length);
Console.WriteLine($"a: {lengths['a']}, b: {lengths['b']}");
```

Output:

```text
a: 5, b: 6
```

## Running code for each value

### `ForEachAsync`

`ForEachAsync` runs your callback for every value, and completes when the stream completes. The async callback is
awaited before the next value, so values are handled one at a time.

```csharp
await SignalAsync.FromEnumerable(["ann", "bob"])
                 .ForEachAsync(static async (name, cancellationToken) =>
                 {
                     await Task.Delay(10, cancellationToken);
                     Console.WriteLine($"welcome {name}");
                 });
```

Output:

```text
welcome ann
welcome bob
```

### `WaitCompletionAsync`

`WaitCompletionAsync` ignores the values and completes when the stream completes. It throws if the stream failed.

```csharp
await SignalAsync.Range(1, 3).WaitCompletionAsync();
Console.WriteLine("finished");
```

Output:

```text
finished
```

## Subscribing

### `SubscribeAsync`

`SubscribeAsync` starts the stream and hands back an `IAsyncDisposable`. Dispose it to stop. Unlike the operators above,
it does not wait for the stream to end.

| Overload | Callbacks |
|---|---|
| `SubscribeAsync()` | None: the values are ignored. |
| `SubscribeAsync(onNext)` | A plain `Action<T>`. |
| `SubscribeAsync(onNextAsync)` | An async lambda that gets a `CancellationToken`. |
| `SubscribeAsync(onNextAsync, onErrorResumeAsync)` | Adds a lambda for resumable errors. |
| `SubscribeAsync(onNextAsync, onErrorResumeAsync, onCompletedAsync)` | Adds a lambda that gets the `Result`. |

Each form also takes a `CancellationToken` that stops the subscription. The [async overview](index.md) shows the full
form.

```csharp
await using IAsyncDisposable subscription = await SignalAsync.Range(1, 2)
    .SubscribeAsync(static value => Console.WriteLine(value));

await Task.Delay(50);
```

Output:

```text
1
2
```

## Every operator on this page at a glance

| Operator | Second name | Hands back |
|---|---|---|
| `FirstAsync` / `FirstOrDefaultAsync` | — | The first value. |
| `LastAsync` / `LastOrDefaultAsync` | — | The final value. |
| `SingleAsync` / `SingleOrDefaultAsync` | — | The only value. |
| `CountAsync` / `LongCountAsync` | — | How many values. |
| `AnyAsync` / `AllAsync` | — | Whether any, or all, pass a test. |
| `ContainsAsync` | — | Whether a value arrived. |
| `AggregateAsync` | `ReduceAsync` | The final running total. |
| `ToListAsync` | `CollectListAsync` | Every value, as a list. |
| `CollectArrayAsync` | — | Every value, as an array. |
| `ToDictionaryAsync` | — | Every value, keyed. |
| `ForEachAsync` | — | Nothing: runs a callback for each value. |
| `WaitCompletionAsync` | — | Nothing: waits for the end. |
| `SubscribeAsync` | — | An `IAsyncDisposable` for a running subscription. |
