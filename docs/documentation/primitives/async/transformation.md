---
Order: 2
---
# Async transformation

A transformation operator changes each value into something else. On an async stream, most of them also take an
async lambda: one that gets a `CancellationToken` and returns a `ValueTask`. The stream waits for it before the next
value.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Async;
```

## Changing each value

### `Select`

`Select` runs a lambda on each value and sends the result.

```csharp
List<int> doubled = await SignalAsync.Range(1, 3)
                                     .Select(static x => x * 2)
                                     .ToListAsync();

Console.WriteLine(string.Join(", ", doubled));
```

Output:

```text
2, 4, 6
```

The async form awaits your lambda for each value, in order:

```csharp
List<string> names = await SignalAsync.Range(1, 2)
                                      .Select(static async (id, cancellationToken) =>
                                      {
                                          await Task.Delay(10, cancellationToken);
                                          return $"user {id}";
                                      })
                                      .ToListAsync();

Console.WriteLine(string.Join(", ", names));
```

Output:

```text
user 1, user 2
```

### `MapWith`

`MapWith` passes a state object to your lambda along with each value. The lambda can then be `static`, so it captures
nothing and allocates nothing per call. See [best practices](../best-practices.md#mark-lambdas-static).

```csharp
var prefix = "item";

List<string> labels = await SignalAsync.Range(1, 2)
                                       .MapWith(prefix, static (p, x) => $"{p} {x}")
                                       .ToListAsync();

Console.WriteLine(string.Join(", ", labels));
```

Output:

```text
item 1, item 2
```

### `Cast` and `OfType`

`Cast` casts each value to a type, and fails the stream on a value that does not fit. `OfType` keeps only the values
of that type, and drops the rest. `OfType` works for reference types.

Name both types when you call them: the type the stream holds, then the type you want.

```csharp
IObservableAsync<object> mixed = SignalAsync.FromEnumerable<object>(["a", 1, "b"]);

Console.WriteLine(string.Join(", ", await mixed.OfType<object, string>().ToListAsync()));

try
{
    await mixed.Cast<object, string>().ToListAsync();
}
catch (InvalidCastException)
{
    Console.WriteLine("1 is not a string");
}
```

Output:

```text
a, b
1 is not a string
```

On an `IObservableAsync<object?>`, the short forms `CastTo<TResult>()` and `KeepType<TResult>()` take one type:

```csharp
IObservableAsync<object?> untyped = SignalAsync.FromEnumerable<object?>(["a", 1, "b"]);
Console.WriteLine(string.Join(", ", await untyped.KeepType<string>().ToListAsync()));
```

Output:

```text
a, b
```

If you write `OfType<string>()` with one type, the compiler error names `AsyncEnumerable.OfType` from `System.Linq`.
That is the nearest method it found, not the one you want: add the stream's type, or use `KeepType`.

### `Not`

`Not` flips each `bool`.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.FromEnumerable([true, false]).Not().ToListAsync()));
```

Output:

```text
False, True
```

### `AsSignal`

`AsSignal` replaces each value with `RxVoid`, a value that carries no data. Use it when only the fact that something
happened matters.

```csharp
List<RxVoid> pulses = await SignalAsync.Range(1, 3).AsSignal().ToListAsync();
Console.WriteLine(pulses.Count);
```

Output:

```text
3
```

## Flattening

### `SelectMany`

`SelectMany` turns each value into a stream of its own, and sends every value from all of them. It subscribes to the
inner streams as they arrive, so their values can interleave.

```csharp
List<string> orders = await SignalAsync.Range(1, 2)
                                       .SelectMany(static user => SignalAsync.FromEnumerable([$"user {user} order A", $"user {user} order B"]))
                                       .ToListAsync();

Console.WriteLine(orders.Count);
```

Output:

```text
4
```

An async overload lets your lambda `await` before it returns the inner stream. Another overload takes a second lambda
that combines each source value with each inner value.

### `ForEach`

`ForEach` works on a stream of collections. It sends every item of each collection as a value of its own.

```csharp
IObservableAsync<IEnumerable<int>> pages = SignalAsync.FromEnumerable<IEnumerable<int>>([[1, 2], [3]]);
Console.WriteLine(string.Join(", ", await pages.ForEach().ToListAsync()));
```

Output:

```text
1, 2, 3
```

## Running totals

### `Fold`

`Fold` keeps a running total. It starts from a seed, runs your lambda on the total and each value, and sends the new
total each time.

```csharp
List<int> totals = await SignalAsync.FromEnumerable([10, 5, 1])
                                    .Fold(0, static (total, x) => total + x)
                                    .ToListAsync();

Console.WriteLine(string.Join(", ", totals));
```

Output:

```text
10, 15, 16
```

An async overload awaits the lambda, for a total that needs a lookup.

### `ScanWithInitial`

`ScanWithInitial` does what `Fold` does, and also sends the seed first.

```csharp
List<int> totals = await SignalAsync.FromEnumerable([5, 3])
                                    .ScanWithInitial(0, static (total, x) => total + x)
                                    .ToListAsync();

Console.WriteLine(string.Join(", ", totals));
```

Output:

```text
0, 5, 8
```

### `Pairwise`

`Pairwise` sends each value with the one before it, as a tuple of `Previous` and `Current`.

```csharp
List<string> changes = await SignalAsync.FromEnumerable([20, 22, 21])
                                        .Pairwise()
                                        .Select(static pair => $"{pair.Previous} -> {pair.Current}")
                                        .ToListAsync();

Console.WriteLine(string.Join(", ", changes));
```

Output:

```text
20 -> 22, 22 -> 21
```

## Grouping

### `GroupBy`

`GroupBy` splits one stream into a stream per key. It sends a `GroupedAsyncSignal<TKey, TValue>` the first time it
sees each key. That group is itself a stream, with a `Key` property, and it gets every value with that key.

```csharp
IObservableAsync<string> words = SignalAsync.FromEnumerable(["apple", "avocado", "banana", "blueberry", "apricot"]);

await words.GroupBy(static word => word[0])
           .ForEachAsync(static async (group, cancellationToken) =>
           {
               await group.SubscribeAsync(word => Console.WriteLine($"{group.Key}: {word}"), cancellationToken);
           });
```

Output:

```text
a: apple
a: avocado
b: banana
b: blueberry
a: apricot
```

An overload takes a lambda that builds the signal each group uses, so you can choose its type. See
[async signals](signals.md).

## Leaving the stream

### `ToAsyncEnumerable`

`ToAsyncEnumerable` turns the stream into an `IAsyncEnumerable<T>`, so you can read it with `await foreach`. Values
wait in a `Channel<T>` between the stream and your loop. You pass the method that builds the channel, which lets you
choose a bounded channel to cap how many values wait.

```csharp
using System.Threading.Channels;

IAsyncEnumerable<int> values = SignalAsync.Range(1, 3)
                                          .ToAsyncEnumerable(static () => Channel.CreateUnbounded<int>());

await foreach (var value in values)
{
    Console.WriteLine(value);
}
```

Output:

```text
1
2
3
```

## Every operator on this page at a glance

| Operator | Second name | What it does |
|---|---|---|
| `Select` | `Map` | Changes each value, with a plain or async lambda. |
| `MapWith` | — | `Select` with a state object, so the lambda can be `static`. |
| `Cast` | `CastTo` on `object?` | Casts each value, failing on a bad cast. |
| `OfType` | `KeepType` on `object?` | Keeps values of one type. |
| `Not` | — | Flips each `bool`. |
| `AsSignal` | — | Replaces each value with `RxVoid`. |
| `SelectMany` | `FlatMap`, `Bind` | Turns each value into a stream and sends all their values. |
| `ForEach` | — | Sends each item of each collection. |
| `Fold` | `Scan` | A running total. |
| `ScanWithInitial` | — | A running total that sends the seed first. |
| `Pairwise` | — | Each value with the one before it. |
| `GroupBy` | — | A stream per key. |
| `ToAsyncEnumerable` | — | Reads the stream with `await foreach`. |
