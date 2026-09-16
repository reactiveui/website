---
Order: 1
---
# Shaping values

These helpers filter, reshape and pick values. Each one does a job you could build from `Where`, `Select` and
friends, in one named call.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Extensions;
using ReactiveUI.Primitives.Signals;
```

The examples push values by hand into a `Signal<T>`, a stream you send values into with `OnNext`.

## Filtering

### `WhereIsNotNull`

`WhereIsNotNull` drops `null` values and passes the rest through.

```csharp
var names = new Signal<string>();

names.WhereIsNotNull()
     .Subscribe(static name => Console.WriteLine(name));

names.OnNext("Ada");
names.OnNext(null!);
names.OnNext("Grace");
```

Output:

```text
Ada
Grace
```

### `SkipWhileNull`

`SkipWhileNull` drops `null` values only until the first value that is not `null`. After that, it passes everything
through, `null` included. Use it for a value that starts empty and fills in once, such as a loaded setting.

```csharp
var selected = new Signal<string?>();

selected.SkipWhileNull()
        .Subscribe(static item => Console.WriteLine(item ?? "null"));

selected.OnNext(null);
selected.OnNext("apple");
selected.OnNext(null);
selected.OnNext("pear");
```

Output:

```text
apple
null
pear
```

### `WhereTrue`, `WhereFalse` and `Not`

These work on a stream of `bool`. `WhereTrue` passes only `true`. `WhereFalse` passes only `false`. `Not` flips each
value.

```csharp
var isBusy = new Signal<bool>();

isBusy.WhereTrue().Subscribe(static _ => Console.WriteLine("started"));
isBusy.WhereFalse().Subscribe(static _ => Console.WriteLine("finished"));
isBusy.Not().Subscribe(static canClick => Console.WriteLine($"button enabled: {canClick}"));

isBusy.OnNext(true);
isBusy.OnNext(false);
```

Output:

```text
started
button enabled: False
finished
button enabled: True
```

### `Filter`

`Filter` works on a stream of `string`. It passes the strings that match a regular expression. Give it a pattern, or
a `Regex` you have already built. A pattern gets a 30 second match timeout, so a slow pattern cannot hang your app.

```csharp
using System.Text.RegularExpressions;

var log = new Signal<string>();

log.Filter("^ERROR")
   .Subscribe(static line => Console.WriteLine($"error line: {line}"));

log.Filter(new Regex("warn", RegexOptions.IgnoreCase))
   .Subscribe(static line => Console.WriteLine($"warning line: {line}"));

log.OnNext("ERROR disk full");
log.OnNext("all good");
log.OnNext("WARN low memory");
```

Output:

```text
error line: ERROR disk full
warning line: WARN low memory
```

## Changing values

### `SelectConstant`

`SelectConstant` replaces every value with the same value. It is `Select(_ => constant)` without the lambda.

```csharp
var clicks = new Signal<int>();

clicks.SelectConstant("clicked")
      .Subscribe(static text => Console.WriteLine(text));

clicks.OnNext(1);
clicks.OnNext(2);
```

Output:

```text
clicked
clicked
```

### `AsSignal`

`AsSignal` replaces every value with `RxVoid`, a value that carries no data. Use it when you only care that
something happened, not what it was. See [`RxVoid`](../creation-factories.md).

```csharp
var saved = new Signal<string>();

IObservable<RxVoid> somethingSaved = saved.AsSignal();
somethingSaved.Subscribe(static _ => Console.WriteLine("saved"));

saved.OnNext("report.txt");
```

Output:

```text
saved
```

### `TrySelect`

`TrySelect` runs a lambda on each value and drops the results that are `null`. It filters and converts in one step,
which suits parsing.

```csharp
var input = new Signal<string>();

input.TrySelect(static text => int.TryParse(text, out var number) ? number : (int?)null)
     .Subscribe(static number => Console.WriteLine(number));

input.OnNext("1");
input.OnNext("one");
input.OnNext("3");
```

Output:

```text
1
3
```

### `WhereSelect`

`WhereSelect` takes a test and a lambda. It keeps the values that pass the test, then converts them. It is
`Where` followed by `Select`, as one operator.

```csharp
var numbers = new Signal<int>();

numbers.WhereSelect(static x => x % 2 == 0, static x => x * 10)
       .Subscribe(static x => Console.WriteLine(x));

numbers.OnNext(1);
numbers.OnNext(2);
numbers.OnNext(4);
```

Output:

```text
20
40
```

### `ScanWithInitial`

`ScanWithInitial` keeps a running total, like [`Fold`](../aggregation.md). The difference is that it sends the
starting value first, as soon as you subscribe. A display that shows the total gets something to show straight
away.

```csharp
var added = new Signal<int>();

added.ScanWithInitial(0, static (total, x) => total + x)
     .Subscribe(static total => Console.WriteLine(total));

added.OnNext(5);
added.OnNext(3);
```

Output:

```text
0
5
8
```

### `Pairwise`

`Pairwise` sends each value together with the one before it, as a tuple of `Previous` and `Current`. The first value
has nothing before it, so it produces no pair.

```csharp
var temperatures = new Signal<int>();

temperatures.Pairwise()
            .Subscribe(static pair => Console.WriteLine($"{pair.Previous} -> {pair.Current}"));

temperatures.OnNext(20);
temperatures.OnNext(22);
temperatures.OnNext(21);
```

Output:

```text
20 -> 22
22 -> 21
```

### `LatestOrDefault`

`LatestOrDefault` sends a default value as soon as you subscribe. After that it sends the source's values, skipping
any value equal to the one it sent last.

```csharp
var score = new Signal<int>();

score.LatestOrDefault(-1)
     .Subscribe(static x => Console.WriteLine(x));

score.OnNext(5);
score.OnNext(5);
score.OnNext(6);
```

Output:

```text
-1
5
6
```

## Collections and text

### `ForEach`

`ForEach` works on a stream of collections. It sends every item of each collection as a value of its own.

```csharp
var pages = new Signal<IEnumerable<int>>();

pages.ForEach()
     .Subscribe(static item => Console.WriteLine(item));

pages.OnNext([1, 2]);
pages.OnNext([3]);
```

Output:

```text
1
2
3
```

Pass a [sequencer](../scheduling.md) to send the items through it.

### `FromArray`

`FromArray` turns any `IEnumerable<T>` into a stream that sends each item, then completes. It is an extension
method on the collection, so it reads left to right.

```csharp
new[] { 1, 2, 3 }.FromArray()
                 .Subscribe(static x => Console.WriteLine(x), static () => Console.WriteLine("completed"));
```

Output:

```text
1
2
3
completed
```

[`Signal.FromEnumerable`](../creation-factories.md) does the same job as a factory.

### `Shuffle`

`Shuffle` works on a stream of arrays. It shuffles each array into a random order before passing it on. It shuffles
the array you sent **in place**, so copy the array first if you still need the original order.

```csharp
var decks = new Signal<int[]>();

decks.Shuffle()
     .Subscribe(static deck => Console.WriteLine(string.Join(", ", deck)));

decks.OnNext([1, 2, 3, 4]);   // prints the four numbers in a random order
```

### `BufferUntil`

`BufferUntil` works on a stream of `char`. It collects characters from a start character to an end character, and
sends them as one `string` that includes both. Characters outside a start and end pair are dropped. When the stream
completes, it sends any unfinished string it holds.

```csharp
var serial = new Signal<char>();

serial.BufferUntil('<', '>')
      .Subscribe(static message => Console.WriteLine(message));

foreach (var c in "noise<hello>more<bye")
{
    serial.OnNext(c);
}

serial.OnCompleted();
```

Output:

```text
<hello>
<bye
```

### `OnNext` with several values

This `OnNext` is an extension on any observer, including a signal. It sends several values in one call, in order.

```csharp
var numbers = new Signal<int>();
numbers.Subscribe(static x => Console.WriteLine(x));

numbers.OnNext(1, 2, 3);
```

Output:

```text
1
2
3
```

## Splitting and picking

### `Partition`

`Partition` splits one stream into two. Values that pass the test go to `True`, and the rest go to `False`.

```csharp
var numbers = new Signal<int>();

var (evens, odds) = numbers.Partition(static x => x % 2 == 0);

evens.Subscribe(static x => Console.WriteLine($"even {x}"));
odds.Subscribe(static x => Console.WriteLine($"odd {x}"));

numbers.OnNext(1);
numbers.OnNext(2);
numbers.OnNext(3);
```

Output:

```text
odd 1
even 2
odd 3
```

Give `Partition` a **hot** stream: one that sends values whether or not anyone is listening, such as a signal.
For a **cold** stream, one that starts its work when you subscribe, such as `Signal.Range`, share it first with
[`ShareLive`](../sharing.md).

### `TakeUntil` with a test

`TakeUntil` with a lambda passes values through until one passes the test. It sends that value too, then
completes.

```csharp
var progress = new Signal<int>();

progress.TakeUntil(static percent => percent >= 100)
        .Subscribe(static p => Console.WriteLine(p), static () => Console.WriteLine("completed"));

progress.OnNext(50);
progress.OnNext(100);
progress.OnNext(120);
```

Output:

```text
50
100
completed
```

[`TakeUntil`](../filtering.md) with another stream or a `CancellationToken` stops on a signal from outside instead.

### `WaitUntil`

`WaitUntil` drops values until one passes the test. It sends that one value, then completes.

```csharp
var status = new Signal<int>();

status.WaitUntil(static code => code == 200)
      .Subscribe(static code => Console.WriteLine(code), static () => Console.WriteLine("completed"));

status.OnNext(404);
status.OnNext(200);
status.OnNext(500);
```

Output:

```text
200
completed
```

### `SwitchIfEmpty`

`SwitchIfEmpty` watches a stream. If it completes without sending anything, `SwitchIfEmpty` subscribes to a
fallback stream instead.

```csharp
Signal.Empty<int>()
      .SwitchIfEmpty(Signal.Emit(42))
      .Subscribe(static x => Console.WriteLine(x));

Signal.Emit(1)
      .SwitchIfEmpty(Signal.Emit(42))
      .Subscribe(static x => Console.WriteLine(x));
```

Output:

```text
42
1
```

### `FirstMatchFromCandidates`

`FirstMatchFromCandidates` tries a list of options in order and stops at the first one that works. For each option
it runs a lambda that returns a stream, converts the stream's value, and tests the result. An option whose stream
fails is skipped. If no option passes, it sends a fallback value.

It suits "look in each place until you find it", such as a list of config files.

```csharp
string[] files = ["user.json", "team.json", "default.json"];

files.FirstMatchFromCandidates(
        project: static file => LoadText(file),
        transform: static text => text.Length,
        predicate: static length => length > 0,
        fallback: -1)
     .Subscribe(static length => Console.WriteLine(length));

static IObservable<string> LoadText(string file) =>
    file == "user.json"
        ? Signal.Fail<string>(new FileNotFoundException(file))
        : Signal.Emit($"{{ \"from\": \"{file}\" }}");
```

Output:

```text
23
```

`user.json` failed, so it was skipped. `team.json` loaded, and its length passed the test.

## Combining

### `GetMax` and `GetMin`

`GetMax` and `GetMin` watch several streams of numbers. Once every stream has sent a value, they send the largest or
smallest of the latest values, and send again whenever one changes. They work on any `struct` that implements
`IComparable<T>`, such as `int`, `double` or `DateTime`.

```csharp
var a = new Signal<int>();
var b = new Signal<int>();

a.GetMax(b).Subscribe(static x => Console.WriteLine($"max {x}"));

a.OnNext(3);    // b has no value yet
b.OnNext(7);
a.OnNext(9);
```

Output:

```text
max 7
max 9
```

### `CombineLatestValuesAreAllTrue` and `CombineLatestValuesAreAllFalse`

These work on a collection of `bool` streams. Once every stream has sent a value, they send whether all the latest
values are `true`, or all `false`. A form's Save button is a good fit: enable it when every field is valid.

```csharp
var nameValid = new Signal<bool>();
var emailValid = new Signal<bool>();

new[] { nameValid, emailValid }
    .CombineLatestValuesAreAllTrue()
    .Subscribe(static canSave => Console.WriteLine($"can save: {canSave}"));

nameValid.OnNext(true);
emailValid.OnNext(false);
emailValid.OnNext(true);
```

Output:

```text
can save: False
can save: True
```

### `SelectManyThen`

`SelectManyThen` runs two steps that each return a stream, one after the other, for each value. It is
[`SelectMany`](../transformation.md) twice, as one operator.

```csharp
Signal.Emit(1)
      .SelectManyThen(
          static id => Signal.Emit(id * 10),       // look up an order
          static order => Signal.Emit(order + 1))  // then its invoice
      .Subscribe(static x => Console.WriteLine(x));
```

Output:

```text
11
```

### `RunAll`

`RunAll` works on a list of `IObservable<RxVoid>` steps. It subscribes to them one at a time, in order, waiting for
each to complete before starting the next. When the last completes, it sends one `RxVoid` and completes.

```csharp
IObservable<RxVoid>[] steps =
[
    Signal.Lazy(static () => { Console.WriteLine("step 1"); return Signal.Emit(RxVoid.Default); }),
    Signal.Lazy(static () => { Console.WriteLine("step 2"); return Signal.Emit(RxVoid.Default); }),
];

steps.RunAll()
     .Subscribe(static _ => Console.WriteLine("all done"));
```

Output:

```text
step 1
step 2
all done
```

## Every helper on this page at a glance

| Helper | What it does |
|---|---|
| `WhereIsNotNull` | Drops `null` values. |
| `SkipWhileNull` | Drops `null` values until the first value that is not `null`. |
| `WhereTrue` / `WhereFalse` | Passes only `true`, or only `false`. |
| `Not` | Flips each `bool`. |
| `Filter` | Passes strings that match a regular expression. |
| `SelectConstant` | Replaces every value with one value. |
| `AsSignal` | Replaces every value with `RxVoid`. |
| `TrySelect` | Converts each value and drops `null` results. |
| `WhereSelect` | Filters, then converts. |
| `ScanWithInitial` | A running total that sends the starting value first. |
| `Pairwise` | Sends each value with the one before it. |
| `LatestOrDefault` | Sends a default first, then values that differ from the last one sent. |
| `ForEach` | Sends each item of each collection. |
| `FromArray` | Turns a collection into a stream. |
| `Shuffle` | Shuffles each array in place. |
| `BufferUntil` | Collects characters between a start and an end character. |
| `OnNext(params T[])` | Sends several values to an observer. |
| `Partition` | Splits a stream in two by a test. |
| `TakeUntil(predicate)` | Passes values up to and including the first that passes a test. |
| `WaitUntil` | Sends the first value that passes a test, then completes. |
| `SwitchIfEmpty` | Uses a fallback stream when the source completes empty. |
| `FirstMatchFromCandidates` | Tries options in order and sends the first result that passes a test. |
| `GetMax` / `GetMin` | The largest or smallest latest value across streams. |
| `CombineLatestValuesAreAllTrue` / `...AllFalse` | Whether every latest `bool` is `true`, or `false`. |
| `SelectManyThen` | Two stream-returning steps in a row. |
| `RunAll` | Runs `RxVoid` steps one after another. |

## The types behind these helpers

Most helpers here have a public class in `ReactiveUI.Primitives.Extensions.Operators`, such as
`WhereIsNotNullObservable<T>`, `PairwiseObservable<T>`, `PartitionObservable<T>` and `TrySelectObservable<T, TOut>`.
`ScanWithInitialObservable<T, TAccumulate>` is in `ReactiveUI.Primitives.Extensions`. Each takes its source through
the constructor. Call the helper in normal code, and construct the class when you write an operator of your own.

`Observables.Return(value)` and `SingleValueSignal<T>` in `ReactiveUI.Primitives.Extensions` build a stream that
sends one value and completes, like [`Signal.Emit`](../creation-factories.md).
