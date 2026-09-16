---
Order: 2
---
# Transformation

A stream gives you values. Often they are not the values you want. A transformation operator takes each value
and produces something else from it.

An **operator** is an extension method on `IObservable<T>`. It does not change the stream you call it on. It
returns a new stream, so you chain calls to build a pipeline.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;
```

The first `using` brings in the whole synchronous operator set. They are all extension methods on a single
class, `LinqExtensions`. The second brings in `Signal`, which the examples use to build their streams.

A few operators here send a wrapper type rather than a plain value: `Spark<T>`, `Moment<T>` and
`TimeInterval<T>`. Those live in a second namespace, so add this when you name one of them:

```csharp
using ReactiveUI.Primitives.Core;
```

## Your first transformation

You have a stream of orders. You want a stream of their totals, formatted for display.

**1. Start with the stream.**

```csharp
IObservable<Order> orders = Signal.FromEnumerable([
    new Order("Ada", 3, 10m),
    new Order("Grace", 1, 25m),
]);
```

**2. Turn each order into a total.** `Select` runs a lambda on each value and sends what the lambda returns.

```csharp
IObservable<decimal> totals = orders.Select(order => order.Quantity * order.Price);
```

**3. Turn each total into text.** `Select` again. Each call adds one step.

```csharp
IObservable<string> lines = totals.Select(total => $"{total:C}");
```

**4. Subscribe.**

```csharp
lines.Subscribe(line => Console.WriteLine(line));
```

Output:

```text
$30.00
$25.00
```

Two values went in and two came out. `Select` never adds or drops values. It only changes them.

## Changing each value

### `Select`

`Select` runs a lambda on every value.

Input: `1, 2, 3`

```csharp
IObservable<int> doubled = Signal.FromEnumerable([1, 2, 3])
                                 .Select(x => x * 2);

doubled.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
2 4 6
```

### `Select` with an index

This `Select` overload also gives you the position of each value, counting from zero.

Input: `"a", "b", "c"`

```csharp
IObservable<string> numbered = Signal.FromEnumerable(["a", "b", "c"])
                                     .Select((value, index) => $"{index}: {value}");

numbered.Subscribe(x => Console.WriteLine(x));
```

Output:

```text
0: a
1: b
2: c
```

### `MapWith`

`MapWith` passes a state value into the lambda as its first argument. The lambda then captures nothing, so you
can mark it `static` and it allocates no closure. See [mark lambdas static](best-practices.md#mark-lambdas-static).

Input: `1, 2, 3`

```csharp
var factor = 10;

IObservable<int> scaled = Signal.FromEnumerable([1, 2, 3])
                                .MapWith(factor, static (f, x) => x * f);

scaled.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
10 20 30
```

The result matches `Select(x => x * factor)`. Only the allocation differs, so reach for `MapWith` on a hot path.

### `Choose`

`Choose` changes a value and drops it in one step. Your lambda returns a `(bool, T)` tuple: whether to keep the
result, and the result itself.

Input: `"1", "two", "3"`

```csharp
IObservable<int> parsed = Signal.FromEnumerable(["1", "two", "3"])
                                .Choose(text => (int.TryParse(text, out var n), n));

parsed.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
1 3
```

`"two"` fails to parse, so the `bool` is `false` and nothing is sent for it. Doing this with `Select` then
`Where` would run the parse twice or leak a null. `Choose` runs it once.

## Changing the type

### `Cast`

`Cast` casts every value to a type you name. It works on a stream of `object?`.

Input: `1, 2, 3` boxed as `object`

```csharp
IObservable<object?> boxed = Signal.FromEnumerable<object?>([1, 2, 3]);

IObservable<int> unboxed = boxed.Cast<int>();

unboxed.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
1 2 3
```

A value of the wrong type ends the stream with an `InvalidCastException`.

Input: `1, "two", 3`

```csharp
Signal.FromEnumerable<object?>([1, "two", 3])
      .Cast<int>()
      .Subscribe(
          x => Console.WriteLine(x),
          error => Console.WriteLine($"error: {error.GetType().Name}"));
```

Output:

```text
1
error: InvalidCastException
```

When you would rather skip the odd value than fail, use `OfType`, which is on the [filtering page](filtering.md).

## Flattening a stream of streams

Sometimes your lambda returns a stream rather than a value. Calling `Select` then leaves you holding an
`IObservable<IObservable<T>>`: a stream whose values are themselves streams. Flattening turns that back into a
single stream of values.

The inner streams are the ones your lambda returns. How the operator handles two inner streams that overlap
in time is the whole difference between the operators below.

### `SelectMany`

`SelectMany` subscribes to every inner stream as soon as it appears, and sends values from all of them as they
arrive.

Input: ids `1, 2`, where loading id 1 takes 30ms and id 2 takes 10ms

```csharp
IObservable<string> rows = Signal.FromEnumerable([1, 2])
                                 .SelectMany(id => LoadRow(id));

rows.Subscribe(x => Console.WriteLine(x));
```

Output:

```text
row 2
row 1
```

Both loads run at once, so the faster one arrives first. Values can interleave, and the order you get is not
the order you asked in.

A second form pairs each inner value back with the outer value it came from:

Input: ids `1, 2`

```csharp
IObservable<string> labelled = Signal.FromEnumerable([1, 2])
    .SelectMany(
        collectionSelector: id => LoadRow(id),
        resultSelector: (id, row) => $"{id} -> {row}");

labelled.Subscribe(x => Console.WriteLine(x));
```

Output:

```text
2 -> row 2
1 -> row 1
```

### `SelectMany` over a collection

When your lambda returns a plain collection rather than a stream, `SelectMany` sends every item of every
collection.

Input: `"ada grace", "katherine"`

```csharp
IObservable<string> words = Signal.FromEnumerable(["ada grace", "katherine"])
                                  .SelectMany(line => line.Split(' '));

words.Subscribe(x => Console.WriteLine(x));
```

Output:

```text
ada
grace
katherine
```

Collections are walked in order, so the output order is predictable.

### `SelectMany` with a fixed stream

One `SelectMany` overload takes a stream instead of a lambda. It replaces every source value with that same
stream, merged.

Input: `1, 2`, inner stream `"a", "b"`

```csharp
IObservable<string> letters = Signal.FromEnumerable([1, 2])
                                    .SelectMany(Signal.FromEnumerable(["a", "b"]));

letters.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
a b a b
```

Use it when the source only says "go", and the values come from elsewhere.

### `SwitchTo`

`SwitchTo` follows only the newest inner stream, for when you hold a stream of streams. When a new inner stream
arrives, it drops the one it was following and subscribes to the new one.

Input: a stream that sends stream X, then stream Y

```csharp
IObservable<IObservable<int>> sources = Signal.FromEnumerable([slowStream, fastStream]);

sources.SwitchTo().Subscribe(x => Console.WriteLine(x));
```

Only `fastStream` is followed, because it arrived last. `Signal.Switch(sources)` is the same thing written as a
static call.

### `SwitchMap`

`SwitchMap` is `Select` followed by `SwitchTo`, fused into one step so it allocates less. When a new source value
arrives, it drops the inner stream it was following and subscribes to the new one.

Input: search terms `"ad"`, then `"ada"` 10ms later, where each search takes 30ms

```csharp
IObservable<string> results = searchTerms.SwitchMap(term => Search(term));

results.Subscribe(x => Console.WriteLine(x));
```

Output:

```text
results for ada
```

The search for `"ad"` was dropped before it could answer. This is what you want for a search box: you only
care about the latest keystroke. `searchTerms.Select(term => Search(term)).SwitchTo()` gives the same result.

### `SelectMany` against `SwitchTo`

Same input, two approaches, different answers.

Input: `"ad"` at 0ms, `"ada"` at 10ms. Each search takes 30ms.

| Operator | Output | Why |
|---|---|---|
| `SelectMany` | `results for ad`, then `results for ada` | Both searches run, both answer. |
| `Select` then `SwitchTo` | `results for ada` | The first search is dropped when the second term arrives. |

Use `SelectMany` when every result matters, such as saving each edit. Use `SwitchTo` when only the latest
matters, such as a search box or a selected item.

### `SwitchSelect`

`SwitchSelect` is `SwitchMap` for a stream that can contain nulls. A null does not switch away from the inner
stream you are already following. It is ignored.

Input: `person A`, `null`, `person B`

```csharp
IObservable<string> names = selectedPerson.SwitchSelect(person => WatchName(person));

names.Subscribe(x => Console.WriteLine(x));
```

Output:

```text
Ada
Beatrice
```

The null between them changed nothing, so the stream kept following person A until person B arrived.

## Turning notifications into values

A stream ends by completing or by erroring. Those endings are not values, so you cannot filter or log them the
way you filter values. `Spark` fixes that by turning every notification into a value.

### `Spark`

`Spark` sends a `Spark<T>` for each value, one for an error, and one for completion. The stream of sparks
then always completes normally.

Input: `1, 2`, then completion

```csharp
IObservable<Spark<int>> sparks = Signal.FromEnumerable([1, 2]).Spark();

sparks.Subscribe(spark => Console.WriteLine(spark.Kind));
```

Output:

```text
OnNext
OnNext
OnCompleted
```

With an error instead:

Input: `1`, then an error

```csharp
Signal.FromEnumerable([1])
      .Concat(Signal.Fail<int>(new InvalidOperationException("boom")))
      .Spark()
      .Subscribe(
          spark => Console.WriteLine(spark.Kind),
          () => Console.WriteLine("the spark stream completed normally"));
```

Output:

```text
OnNext
OnError
the spark stream completed normally
```

The error arrived as a value, so the subscriber's error callback never ran.

### `Unspark`

`Unspark` turns sparks back into real notifications. It is the other half of the pair.

```csharp
IObservable<int> original = Signal.FromEnumerable([1, 2])
                                  .Spark()
                                  .Unspark();

original.Subscribe(x => Console.Write($"{x} "), () => Console.WriteLine("done"));
```

Output:

```text
1 2 done
```

Between the two calls you can treat the ending as ordinary data: count errors, delay completion, or filter one kind
out.

## Attaching timing

### `Timestamp`

`Timestamp` attaches the current time to each value. It sends a `Moment<T>`, which carries the value and the
time it was sent.

The time comes from a sequencer, which is the library's clock and thread abstraction. Leave it out to use the real
clock. Here a `VirtualClock`, a clock you move forward by hand, keeps the output the same on every run. See
[sequencers and scheduling](scheduling.md).

Input: `"a"`, then `"b"` one second later

```csharp
using ReactiveUI.Primitives.Concurrency;

var clock = new VirtualClock(new DateTimeOffset(2026, 1, 1, 9, 0, 0, TimeSpan.Zero));
var letters = new Signal<string>();

letters.Timestamp(clock)
       .Subscribe(m => Console.WriteLine($"{m.Value} at {m.Timestamp:HH:mm:ss}"));

letters.OnNext("a");
clock.AdvanceBy(TimeSpan.FromSeconds(1));
letters.OnNext("b");
```

Output:

```text
a at 09:00:00
b at 09:00:01
```

### `TimeInterval`

`TimeInterval` attaches the gap since the previous value instead of the absolute time. It sends a
`TimeInterval<T>`.

Input: `"a"` at 0s, `"b"` at 1s, `"c"` at 3s

```csharp
var clock = new VirtualClock();
var letters = new Signal<string>();

IObservable<TimeInterval<string>> gaps = letters.TimeInterval(clock);
gaps.Subscribe(t => Console.WriteLine($"{t.Value} after {t.Interval.TotalSeconds}s"));

letters.OnNext("a");
clock.AdvanceBy(TimeSpan.FromSeconds(1));
letters.OnNext("b");
clock.AdvanceBy(TimeSpan.FromSeconds(2));
letters.OnNext("c");
```

Output:

```text
a after 0s
b after 1s
c after 2s
```

The first gap is measured from the moment you subscribed. It also takes an optional sequencer.

### `Timestamp` against `TimeInterval`

| Operator | Carries | Read it as |
|---|---|---|
| `Timestamp` | The clock time of each value | "when did this happen" |
| `TimeInterval` | The gap since the value before | "how long did that take" |

## Every transformation operator at a glance

| Operator | Second name | What it does |
|---|---|---|
| `Select` | `Map` | Runs a lambda on each value. |
| `Select((value, index) => ...)` | `MapIndexed` | The same, with each value's position. |
| `MapWith` | `SelectWith` | The same, passing a state value so the lambda can be `static`. |
| `Choose` | — | Changes each value and drops the ones your lambda rejects. |
| `Cast` | `CastTo` | Casts each value, failing on the wrong type. |
| `SelectMany` | `FlatMap`, `Bind` | Flattens every inner stream, all at once. |
| `SelectMany` over a collection | `FlatMapValues` | Flattens every item of every collection, in order. |
| `SwitchTo` | `Switch` | Follows only the newest inner stream. |
| `SwitchMap` | — | `Select` then `SwitchTo`, in one step. |
| `SwitchSelect` | — | `SwitchMap` that ignores nulls. |
| `Spark` | `Materialize` | Turns each notification into a `Spark<T>` value. |
| `Unspark` | `Dematerialize` | Turns `Spark<T>` values back into notifications. |
| `Timestamp` | — | Attaches the time to each value. |
| `TimeInterval` | — | Attaches the gap since the value before. |

## The types behind these operators

Every operator here is built from a class you can construct yourself: `MapSignal`, `MapIndexedSignal`,
`MapWithSignal`, `CastSignal`, `SelectManySignal`, `SwitchMapSignal`, `SwitchSignal`, `SparkSignal`,
`UnsparkSignal` and `TimeIntervalSignal`. They live in `ReactiveUI.Primitives.Advanced`.

```csharp
using ReactiveUI.Primitives.Advanced;

// the operator
IObservable<int> a = source.Select(x => x * 2);

// the same thing, built directly
IObservable<int> b = new MapSignal<int, int>(source, x => x * 2);
```

Calling the operator is the normal path. Construct the type when you are writing your own operator and want to
place one inside it.

## The two package flavours

Every operator here ships twice. `ReactiveUI.Primitives` puts them under `ReactiveUI.Primitives.*`.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and puts them under
`ReactiveUI.Primitives.Reactive.*`. They behave the same in both.
