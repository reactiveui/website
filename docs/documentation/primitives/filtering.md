---
Order: 3
---
# Filtering

A stream often carries more than you need. A filtering operator drops the values you do not want and passes
the rest through unchanged.

Every operator here keeps values as they are. None of them changes a value. If you need to change values as
well as drop them, use `Choose`, which is on the transformation page.

```csharp
using ReactiveUI.Primitives;
```

## Your first filter

You have a stream of temperature readings. You only care about the ones above freezing, and only when the
reading actually changes.

**1. Start with the stream.**

```csharp
IObservable<int> readings = Signal.FromEnumerable([-5, 3, 3, 8, -1, 8]);
```

**2. Drop the ones you do not want.** `Keep` passes through only the values your test accepts.

```csharp
IObservable<int> aboveFreezing = readings.Keep(x => x > 0);
```

After this step the stream carries `3, 3, 8, 8`.

**3. Drop repeats.** `Unique` drops a value when it is the same as the one just before it.

```csharp
IObservable<int> changes = aboveFreezing.Unique();
```

**4. Subscribe.**

```csharp
changes.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
3 8
```

Trace it one step at a time:

| After | Stream carries |
|---|---|
| the source | `-5, 3, 3, 8, -1, 8` |
| `Keep(x => x > 0)` | `3, 3, 8, 8` |
| `Unique()` | `3, 8` |

The two `3`s collapsed because they sat next to each other. The two `8`s collapsed too, and that is the part
worth slowing down on. In the source they had `-1` between them. `Keep` dropped the `-1` first, so by the time
`Unique` saw the stream the two `8`s were neighbours.

Order matters. `Unique` only ever sees what the operator before it passed on. Put the two calls the other way
round and you get `3, 8, 8`, because `Unique` would then see the `-1` sitting between the `8`s.

## Testing each value

### `Keep`

`Keep` passes through the values your test accepts.

Input: `1, 2, 3, 4, 5, 6`

```csharp
IObservable<int> evens = Signal.FromEnumerable([1, 2, 3, 4, 5, 6])
                               .Keep(x => x % 2 == 0);

evens.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
2 4 6
```

`Where` is a second name for `Keep`. Both build the same thing.

### `KeepWith`

`KeepWith` passes an object of yours into the test as its first argument, so the test can be `static` and
allocates no closure.

Input: `1, 2, 3, 4, 5, 6`

```csharp
var minimum = 4;

IObservable<int> large = Signal.FromEnumerable([1, 2, 3, 4, 5, 6])
                               .KeepWith(minimum, static (min, x) => x >= min);

large.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
4 5 6
```

The result matches `Keep(x => x >= minimum)`. Only the allocation differs. `WhereWith` is a second name for
it.

## Dropping nulls and wrong types

### `KeepNotNull`

`KeepNotNull` drops nulls. It also narrows the type, so what comes out is not nullable and you do not need a
null check downstream.

Input: `"Ada", null, "Grace"`

```csharp
IObservable<string?> maybeNames = Signal.FromEnumerable<string?>(["Ada", null, "Grace"]);

IObservable<string> names = maybeNames.KeepNotNull();

names.Subscribe(x => Console.WriteLine(x.Length));   // no null check needed
```

Output:

```text
3
5
```

`WhereNotNull` is a second name for it. It works on reference types.

### `KeepType`

`KeepType` keeps only the values that are of the type you name, and drops the rest.

Input: `1, "two", 3, "four"` as `object`

```csharp
IObservable<object?> mixed = Signal.FromEnumerable<object?>([1, "two", 3, "four"]);

IObservable<int> numbers = mixed.KeepType<int>();

numbers.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
1 3
```

`OfType` is a second name for it.

### `KeepType` against `CastTo`

Both narrow a stream of `object?` to one type. They disagree about a value that does not fit.

Input: `1, "two", 3`

| Operator | Output | On a value of the wrong type |
|---|---|---|
| `KeepType<int>()` | `1 3` | Drops it and carries on. |
| `CastTo<int>()` | `1`, then an error | Ends the stream with `InvalidCastException`. |

Use `KeepType` when mixed types are expected. Use `CastTo` when a wrong type means a bug you want to hear
about. `CastTo` is on the transformation page.

## Taking and skipping by count

### `Take`

`Take` passes through at most the first few values, then completes. It does not wait for the source to end.

Input: `1, 2, 3, 4, 5`

```csharp
IObservable<int> firstThree = Signal.FromEnumerable([1, 2, 3, 4, 5]).Take(3);

firstThree.Subscribe(x => Console.Write($"{x} "), () => Console.WriteLine("| done"));
```

Output:

```text
1 2 3 | done
```

`Take` on an endless stream gives you a stream that ends, which is how you turn a timer into a countdown.

```csharp
Signal.Every(TimeSpan.FromSeconds(1)).Take(3).Subscribe(x => Console.WriteLine(x));
```

Output, one per second, then it stops on its own:

```text
0
1
2
```

### `Skip`

`Skip` ignores the first few values and passes through everything after them.

Input: `1, 2, 3, 4, 5`

```csharp
IObservable<int> rest = Signal.FromEnumerable([1, 2, 3, 4, 5]).Skip(2);

rest.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
3 4 5
```

## Taking and skipping by test

### `TakeWhile`

`TakeWhile` passes values through while your test holds. The first value that fails the test ends the stream.
That value is not sent.

Input: `1, 2, 3, 10, 4, 5`

```csharp
IObservable<int> small = Signal.FromEnumerable([1, 2, 3, 10, 4, 5])
                               .TakeWhile(x => x < 5);

small.Subscribe(x => Console.Write($"{x} "), () => Console.WriteLine("| done"));
```

Output:

```text
1 2 3 | done
```

The `4` and `5` never arrive, even though they pass the test. `TakeWhile` stopped at the `10`.

### `SkipWhile`

`SkipWhile` drops values while your test holds, then passes through everything after that. Once it starts
passing values, it never stops.

Input: `1, 2, 3, 10, 4, 5`

```csharp
IObservable<int> fromFirstLarge = Signal.FromEnumerable([1, 2, 3, 10, 4, 5])
                                        .SkipWhile(x => x < 5);

fromFirstLarge.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
10 4 5
```

The `4` and `5` arrive here, because the test stopped being consulted once `10` failed it.

### `TakeWhile` against `Keep`

This is worth seeing on one input, because the two are easy to mix up.

Input: `1, 2, 3, 10, 4, 5`, test `x < 5`

| Operator | Output | Rule |
|---|---|---|
| `Keep(x => x < 5)` | `1 2 3 4 5` | Tests every value, forever. |
| `TakeWhile(x => x < 5)` | `1 2 3` | Stops the stream at the first failure. |

## Stopping on another event

### `TakeUntil`

`TakeUntil` passes values through until a second stream sends anything. Then it completes.

Input: values every 100ms, a stop stream that fires at 250ms

```csharp
IObservable<long> ticks = Signal.Every(TimeSpan.FromMilliseconds(100));
IObservable<long> stop = Signal.After(TimeSpan.FromMilliseconds(250));

ticks.TakeUntil(stop).Subscribe(x => Console.Write($"{x} "), () => Console.WriteLine("| done"));
```

Output:

```text
0 1 | done
```

Only a value stops it. A stop stream that completes without sending anything does not stop it. Use
`Signal.After` rather than `Signal.None` when you want a deadline.

A second form takes a `CancellationToken` instead of a stream.

```csharp
IObservable<long> untilCancelled = ticks.TakeUntil(cancellationToken);
```

When the token is cancelled the stream completes normally. It does not error, so your completion callback runs
and your error callback does not.

## Dropping duplicates

Two operators drop duplicates and they mean different things by "duplicate".

### `Distinct`

`Distinct` remembers every value it has ever sent, and drops any value it has seen before.

Input: `1, 1, 2, 1, 3, 2`

```csharp
IObservable<int> seenOnce = Signal.FromEnumerable([1, 1, 2, 1, 3, 2]).Distinct();

seenOnce.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
1 2 3
```

Because it remembers every value, `Distinct` uses more memory the longer the stream runs. On an endless
stream of many different values, that memory is never released.

Pass an `IEqualityComparer<T>` to decide what counts as equal.

```csharp
IObservable<string> ignoringCase = names.Distinct(StringComparer.OrdinalIgnoreCase);
```

### `DistinctBy`

`DistinctBy` does the same, comparing a key you pick out of each value rather than the whole value.

Input: three people, two of them in London

```csharp
IObservable<Person> firstPerCity = people.DistinctBy(person => person.City);

firstPerCity.Subscribe(p => Console.WriteLine($"{p.Name} ({p.City})"));
```

Output:

```text
Ada (London)
Grace (Paris)
```

Katherine, also in London, is dropped because London was already seen. It also takes a comparer for the key.

### `Unique`

`Unique` compares each value only with the one immediately before it. It has no memory beyond that, so it
costs nothing to run on an endless stream.

Input: `1, 1, 2, 1, 3, 2`

```csharp
IObservable<int> changes = Signal.FromEnumerable([1, 1, 2, 1, 3, 2]).Unique();

changes.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
1 2 1 3 2
```

Only the second `1` was dropped, because it sat next to the first. `DistinctUntilChanged` is a second name for
`Unique`.

### `UniqueBy`

`UniqueBy` compares a key from each value with the key from the value before.

Input: readings from the same sensor, then a different one, then the first again

```csharp
IObservable<Reading> sensorChanges = readings.UniqueBy(r => r.SensorId);

sensorChanges.Subscribe(r => Console.WriteLine(r.SensorId));
```

Output:

```text
A
B
A
```

`DistinctUntilChangedBy` is a second name for it. Both take an optional comparer for the key.

### `Distinct` against `Unique`

The same input through both, side by side.

Input: `1, 1, 2, 1, 3, 2`

| Operator | Output | Rule | Memory |
|---|---|---|---|
| `Distinct()` | `1 2 3` | Drops a value seen anywhere earlier. | Grows with the number of different values. |
| `Unique()` | `1 2 1 3 2` | Drops a value only when it repeats the one before. | One value. |

Use `Unique` to react to change, such as a property that keeps reporting the same value. Use `Distinct` to
remove duplicates from a finite set, such as ids from a finished query. On an endless stream, `Unique` is
almost always the one you want.

## Dropping everything

### `IgnoreValues`

`IgnoreValues` drops every value and passes on only the ending. Use it when you care that a job finished, not
what it produced.

Input: `1, 2, 3`, then completion

```csharp
IObservable<int> finished = Signal.FromEnumerable([1, 2, 3]).IgnoreValues();

finished.Subscribe(
    x => Console.WriteLine($"value: {x}"),
    () => Console.WriteLine("done"));
```

Output:

```text
done
```

An error still comes through, so this is a clean way to wait for a job and hear about failure.
`IgnoreElements` is a second name for it.

## Filling in an empty stream

### `DefaultIfEmpty`

`DefaultIfEmpty` sends one value if the source completes without ever sending anything. A source that does
send values is passed through untouched.

Input: nothing, then completion

```csharp
IObservable<int> nothing = Signal.None<int>();

nothing.DefaultIfEmpty().Subscribe(x => Console.WriteLine(x));
```

Output:

```text
0
```

Give it your own fallback instead of `default`:

```csharp
Signal.None<string>()
      .DefaultIfEmpty("no results")
      .Subscribe(x => Console.WriteLine(x));
```

Output:

```text
no results
```

With a source that does send something:

Input: `1, 2`

```csharp
Signal.FromEnumerable([1, 2]).DefaultIfEmpty(99).Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
1 2
```

The fallback never appeared, because the source was not empty.

## Every filtering operator at a glance

| Operator | Second name | Drops |
|---|---|---|
| `Keep` | `Where` | Values your test rejects. |
| `KeepWith` | `WhereWith` | The same, with no closure allocated. |
| `KeepNotNull` | `WhereNotNull` | Nulls, and narrows the type. |
| `KeepType<T>` | `OfType<T>` | Values that are not a `T`. |
| `Take` | — | Everything after the first `n`. |
| `Skip` | — | The first `n`. |
| `TakeWhile` | — | Everything from the first failure onward. |
| `SkipWhile` | — | Values before the first failure. |
| `TakeUntil` | — | Everything after another stream fires. |
| `Distinct` | — | Values seen anywhere earlier. |
| `DistinctBy` | — | Values whose key was seen earlier. |
| `Unique` | `DistinctUntilChanged` | Values equal to the one before. |
| `UniqueBy` | `DistinctUntilChangedBy` | Values whose key equals the key before. |
| `IgnoreValues` | `IgnoreElements` | Every value. |
| `DefaultIfEmpty` | — | Nothing. It adds a value to an empty stream. |

## The types behind these operators

`KeepSignal`, `KeepWithSignal`, `KeepNotNullSignal`, `KeepTypeSignal`, `UniqueSignal` and `IgnoreValuesSignal`
are public classes in `ReactiveUI.Primitives.Advanced`, and each takes its source through the constructor.

```csharp
using ReactiveUI.Primitives.Advanced;

// the operator
IObservable<int> a = source.Unique();

// the same thing, built directly
IObservable<int> b = new UniqueSignal<int>(source, EqualityComparer<int>.Default);
```

Calling the operator is the normal path. Construct the type when you are writing an operator of your own and
want to place one inside it.

## The two package flavours

Every operator here ships twice. `ReactiveUI.Primitives` puts them under `ReactiveUI.Primitives.*`.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and puts them under
`ReactiveUI.Primitives.Reactive.*`. They behave the same in both.
