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
```

That one `using` brings in the whole synchronous operator set. They are all extension methods on a single
class, `LinqExtensions`.

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

**2. Turn each order into a total.** `Map` runs a function on each value and sends what the function returns.

```csharp
IObservable<decimal> totals = orders.Map(order => order.Quantity * order.Price);
```

**3. Turn each total into text.** `Map` again. Each call adds one step.

```csharp
IObservable<string> lines = totals.Map(total => $"{total:C}");
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

Two values went in and two came out. `Map` never adds or drops values. It only changes them.

## Changing each value

### `Map`

`Map` applies a function to every value.

Input: `1, 2, 3`

```csharp
IObservable<int> doubled = Signal.FromEnumerable([1, 2, 3])
                                 .Map(x => x * 2);

doubled.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
2 4 6
```

`Select` is a second name for `Map`. Both build the same thing, so pick one and stay with it.

```csharp
IObservable<int> same = Signal.FromEnumerable([1, 2, 3]).Select(x => x * 2);
```

### `MapIndexed`

`MapIndexed` also gives you the position of each value, counting from zero.

Input: `"a", "b", "c"`

```csharp
IObservable<string> numbered = Signal.FromEnumerable(["a", "b", "c"])
                                     .MapIndexed((value, index) => $"{index}: {value}");

numbered.Subscribe(x => Console.WriteLine(x));
```

Output:

```text
0: a
1: b
2: c
```

`Select` has a matching overload that takes the same two-argument function.

### `MapWith`

`MapWith` passes an object of yours into the function as its first argument. Your function then captures
nothing, so it can be `static` and allocates no closure.

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

The result matches `Map(x => x * factor)`. Only the allocation differs, so reach for `MapWith` on a hot path.
`SelectWith` is a second name for it.

### `Choose`

`Choose` changes a value and drops it in one step. Your function returns a pair: a flag saying whether to keep
the result, and the result itself.

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

`"two"` fails to parse, so the flag is false and nothing is sent for it. Doing this with `Map` then a filter
would run the parse twice or leak a null. `Choose` runs it once.

## Changing the type

### `CastTo`

`CastTo` casts every value to a type you name. It works on a stream of `object?`.

Input: `1, 2, 3` boxed as `object`

```csharp
IObservable<object?> boxed = Signal.FromEnumerable<object?>([1, 2, 3]);

IObservable<int> unboxed = boxed.CastTo<int>();

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
      .CastTo<int>()
      .Subscribe(
          x => Console.WriteLine(x),
          error => Console.WriteLine($"error: {error.GetType().Name}"));
```

Output:

```text
1
error: InvalidCastException
```

`Cast` is a second name for `CastTo`. When you would rather skip the odd value than fail, use `KeepType`,
which is on the filtering page.

## Flattening a stream of streams

Sometimes your function returns a stream rather than a value. Calling `Map` then leaves you holding an
`IObservable<IObservable<T>>`: a stream whose values are themselves streams. Flattening turns that back into a
single stream of values.

The inner streams are the ones your function returns. How the operator handles two inner streams that overlap
in time is the whole difference between the operators below.

### `FlatMap`

`FlatMap` subscribes to every inner stream as soon as it appears, and sends values from all of them as they
arrive.

Input: ids `1, 2`, where loading id 1 takes 30ms and id 2 takes 10ms

```csharp
IObservable<string> rows = Signal.FromEnumerable([1, 2])
                                 .FlatMap(id => LoadRow(id));

rows.Subscribe(x => Console.WriteLine(x));
```

Output:

```text
row 2
row 1
```

Both loads run at once, so the faster one arrives first. Values can interleave, and the order you get is not
the order you asked in.

`Bind` and `SelectMany` are second names for `FlatMap`.

A second form pairs each inner value back with the outer value it came from:

Input: ids `1, 2`

```csharp
IObservable<string> labelled = Signal.FromEnumerable([1, 2])
    .FlatMap(
        collectionSelector: id => LoadRow(id),
        resultSelector: (id, row) => $"{id} -> {row}");

labelled.Subscribe(x => Console.WriteLine(x));
```

Output:

```text
2 -> row 2
1 -> row 1
```

### `FlatMapValues`

`FlatMapValues` is for when your function returns a plain collection rather than a stream. It sends every item
of every collection.

Input: `"ada grace", "katherine"`

```csharp
IObservable<string> words = Signal.FromEnumerable(["ada grace", "katherine"])
                                  .FlatMapValues(line => line.Split(' '));

words.Subscribe(x => Console.WriteLine(x));
```

Output:

```text
ada
grace
katherine
```

Collections are walked in order, so unlike `FlatMap` the output order is predictable. `SelectMany` has an
overload that takes the same collection-returning function.

### `SelectMany` with a fixed stream

One `SelectMany` overload takes a stream instead of a function. It replaces every source value with that same
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

### `SwitchMap`

`SwitchMap` follows only the newest inner stream. When a new source value arrives, it drops the inner stream
it was following and subscribes to the new one.

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
care about the latest keystroke.

### `FlatMap` against `SwitchMap`

Same input, two operators, different answers.

Input: `"ad"` at 0ms, `"ada"` at 10ms. Each search takes 30ms.

| Operator | Output | Why |
|---|---|---|
| `FlatMap` | `results for ad`, then `results for ada` | Both searches run, both answer. |
| `SwitchMap` | `results for ada` | The first search is dropped when the second term arrives. |

Use `FlatMap` when every result matters, such as saving each edit. Use `SwitchMap` when only the latest
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

### `SwitchTo`

`SwitchTo` does the switching part on its own, for when you already hold a stream of streams.

Input: a stream that sends stream X, then stream Y

```csharp
IObservable<IObservable<int>> sources = Signal.FromEnumerable([slowStream, fastStream]);

sources.SwitchTo().Subscribe(x => Console.WriteLine(x));
```

Only `fastStream` is followed, because it arrived last. `Switch` is a second name for `SwitchTo`, and
`Signal.Switch(sources)` is the same thing written as a static call.

`SwitchMap` is exactly `Map` followed by `SwitchTo`, fused into one step so it allocates less.

## Turning notifications into values

A stream ends by completing or by erroring. Those endings are not values, so you cannot filter or log them the
way you filter values. `Spark` fixes that by turning every notification into a value.

### `Spark`

`Spark` sends a `Spark<T>` for each value, one for an error, and one for completion. The stream of sparks then
always completes normally.

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
      .Chain(Signal.Fail<int>(new InvalidOperationException("boom")))
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

The error arrived as a value, so the subscriber's error callback never ran. `Materialize` is a second name for
`Spark`.

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

Between the two calls you can treat the ending as ordinary data: count errors, delay completion, or filter one
kind out. `Dematerialize` is a second name for `Unspark`.

## Attaching timing

### `Timestamp`

`Timestamp` attaches the current time to each value. It sends a `Moment<T>`, which carries the value and the
time it was sent.

Input: `"a"`, `"b"` one second later

```csharp
IObservable<Moment<string>> stamped = letters.Timestamp();

stamped.Subscribe(m => Console.WriteLine($"{m.Value} at {m.Timestamp:HH:mm:ss}"));
```

Output:

```text
a at 09:00:00
b at 09:00:01
```

The time comes from a sequencer, which is the library's clock and thread abstraction. Pass one as an argument
to use a different clock, which is how you make a test deterministic.

```csharp
IObservable<Moment<string>> onTestClock = letters.Timestamp(testSequencer);
```

### `TimeInterval`

`TimeInterval` attaches the gap since the previous value instead of the absolute time. It sends a
`TimeInterval<T>`.

Input: `"a"` at 0s, `"b"` at 1s, `"c"` at 3s

```csharp
IObservable<TimeInterval<string>> gaps = letters.TimeInterval();

gaps.Subscribe(t => Console.WriteLine($"{t.Value} after {t.Interval.TotalSeconds}s"));
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

## The types behind these operators

Every operator here is built from a class you can construct yourself: `MapSignal`, `MapIndexedSignal`,
`MapWithSignal`, `CastSignal`, `SelectManySignal`, `SwitchMapSignal`, `SwitchSignal`, `SparkSignal`,
`UnsparkSignal` and `TimeIntervalSignal`. They live in `ReactiveUI.Primitives.Advanced`.

```csharp
using ReactiveUI.Primitives.Advanced;

// the operator
IObservable<int> a = source.Map(x => x * 2);

// the same thing, built directly
IObservable<int> b = new MapSignal<int, int>(source, x => x * 2);
```

Calling the operator is the normal path. Construct the type when you are writing your own operator and want to
place one inside it.

## The two package flavours

Every operator here ships twice. `ReactiveUI.Primitives` puts them under `ReactiveUI.Primitives.*`.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and puts them under
`ReactiveUI.Primitives.Reactive.*`. They behave the same in both.
