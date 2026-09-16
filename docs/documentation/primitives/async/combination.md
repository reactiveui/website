---
Order: 4
---
# Async combination

A combination operator joins two or more streams into one.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Async;
using ReactiveUI.Primitives.Async.Signals;
```

## Latest values from each stream

### `SyncLatest`

`SyncLatest` waits until every stream has sent at least one value. From then on, each time any stream sends, it runs
your lambda on the latest value from every stream. It takes 2 to 16 streams.

```csharp
ISignalAsync<string> first = Signal.CreateBehavior("Ada");
ISignalAsync<string> last = Signal.CreateBehavior("Lovelace");

await using IAsyncDisposable subscription = await first
    .SyncLatest(last, static (f, l) => $"{f} {l}")
    .SubscribeAsync(static name => Console.WriteLine(name));

await last.OnNextAsync("King", CancellationToken.None);
```

Output:

```text
Ada Lovelace
Ada King
```

On a collection of streams of the same type, `SyncLatest()` sends an `IReadOnlyList<T>` of the latest values, or runs
your lambda on that list.

```csharp
IObservableAsync<int>[] sensors = [SignalAsync.Emit(20), SignalAsync.Emit(22), SignalAsync.Emit(21)];

IReadOnlyList<int> readings = await sensors.SyncLatest().FirstAsync();
Console.WriteLine(string.Join(", ", readings));
```

Output:

```text
20, 22, 21
```

### `CombineLatestValuesAreAllTrue` and `CombineLatestValuesAreAllFalse`

These work on a collection of `bool` streams. Once every stream has sent a value, they send whether all the latest
values are `true`, or all `false`.

```csharp
IObservableAsync<bool>[] checks = [SignalAsync.Emit(true), SignalAsync.Emit(true)];
Console.WriteLine(await checks.CombineLatestValuesAreAllTrue().FirstAsync());

IObservableAsync<bool>[] failures = [SignalAsync.Emit(false), SignalAsync.Emit(false)];
Console.WriteLine(await failures.CombineLatestValuesAreAllFalse().FirstAsync());
```

Output:

```text
True
True
```

### `GetMax` and `GetMin`

`GetMax` and `GetMin` send the largest or smallest of the latest values across several streams, once every stream has
sent one.

```csharp
Console.WriteLine(await SignalAsync.Emit(3).GetMax(SignalAsync.Emit(7), SignalAsync.Emit(5)).FirstAsync());
Console.WriteLine(await SignalAsync.Emit(3).GetMin(SignalAsync.Emit(7)).FirstAsync());
```

Output:

```text
7
3
```

## Pairing values in order

### `Zip`

`Zip` pairs the first value of one stream with the first of the other, the second with the second, and so on. It waits
for the slower stream. With no lambda, it sends a tuple of `First` and `Second`.

```csharp
IObservableAsync<string> letters = SignalAsync.FromEnumerable(["a", "b", "c"]);
IObservableAsync<int> numbers = SignalAsync.Range(1, 2);

Console.WriteLine(string.Join(", ", await letters.Zip(numbers, static (l, n) => $"{l}{n}").ToListAsync()));
Console.WriteLine(string.Join(", ", await letters.Zip(numbers).Select(static t => $"{t.First}-{t.Second}").ToListAsync()));
```

Output:

```text
a1, b2
a-1, b-2
```

`c` has no partner, so it is not sent.

## One stream after another

### `Concat`

`Concat` runs one stream, then the next when the first completes. It works on two streams, a stream of streams, or a
collection of streams.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.Range(1, 2).Concat(SignalAsync.Range(10, 2)).ToListAsync()));
```

Output:

```text
1, 2, 10, 11
```

### `Prepend`

`Prepend` sends one value, or a collection of values, before the stream's own values.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.Range(1, 2).Prepend(0).ToListAsync()));
Console.WriteLine(string.Join(", ", await SignalAsync.Range(3, 1).Prepend([1, 2]).ToListAsync()));
```

Output:

```text
0, 1, 2
1, 2, 3
```

## Streams at the same time

### `Blend`

`Blend` subscribes to streams at the same time, and sends values from each as they arrive. It works on two streams, a
stream of streams, or a collection of streams.

```csharp
IObservableAsync<string> clicks = SignalAsync.FromEnumerable(["click"]);
IObservableAsync<string> keys = SignalAsync.FromEnumerable(["key"]);

List<string> all = await clicks.Blend(keys).ToListAsync();
Console.WriteLine(all.Count);
```

Output:

```text
2
```

On a stream of streams, `Merge(maxConcurrent)` caps how many inner streams run at once. The rest wait their turn.

### `SwitchTo`

`SwitchTo` works on a stream of streams. It follows only the newest inner stream, and drops the one before it as soon
as a new one arrives. A search box that starts a new search on each key press is the usual fit.

```csharp
IObservableAsync<IObservableAsync<string>> searches = SignalAsync
    .FromEnumerable(["c", "ca", "cat"])
    .Select(static q => SignalAsync.FromAsync(async cancellationToken =>
    {
        await Task.Delay(q == "cat" ? 10 : 200, cancellationToken);   // stands in for a search
        return $"results for {q}";
    }));

Console.WriteLine(string.Join(", ", await searches.SwitchTo().ToListAsync()));
```

Output:

```text
results for cat
```

The searches for `c` and `ca` were still running when the next one arrived. `SwitchTo` cancelled them through their
`CancellationToken`, so their results never went out.

## Every operator on this page at a glance

| Operator | Second name | What it does |
|---|---|---|
| `SyncLatest` | `CombineLatest`, `PairLatest` for two | The latest value from each stream, whenever any sends. |
| `CombineLatestValuesAreAllTrue` / `...AllFalse` | — | Whether every latest `bool` is `true`, or `false`. |
| `GetMax` / `GetMin` | — | The largest or smallest latest value across streams. |
| `Zip` | `Pair` | Values paired in order. |
| `Concat` | `Chain` | One stream after another. |
| `Prepend` | `Lead`, `StartWith` | Values before the stream's own. |
| `Blend` | `Merge` | Streams at the same time. |
| `SwitchTo` | `Switch` | Only the newest inner stream. |
