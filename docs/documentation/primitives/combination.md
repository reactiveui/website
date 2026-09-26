---
Order: 4
---
# Combination

A combination operator takes two or more streams and makes one stream out of them. The operators differ in
one thing: when the new stream sends a value. Some send a value whenever any source does. Some wait for every
source. Some only listen to one side and read the other.

Picking the right one is mostly about that timing. The comparison near the end of this page runs four of them
on the same input, so you can see the difference side by side.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;
```

Many examples here use a `Signal<T>`. A `Signal<T>` is a stream you push values into by hand with `OnNext`.
That lets you choose exactly when each source sends a value, so you can see what the operator does at each
step.

## Your first combination

You have a form with a first name box and a last name box. You want a full name that updates whenever either
box changes.

**1. Make a stream for each box.**

```csharp
var first = new Signal<string>();
var last = new Signal<string>();
```

**2. Combine them.** `SyncLatest` keeps the newest value from each side. Whenever either side sends a value, it
calls your lambda with both newest values.

```csharp
IObservable<string> fullName = first.SyncLatest(last, (f, l) => $"{f} {l}");
```

**3. Subscribe.**

```csharp
fullName.Subscribe(name => Console.WriteLine(name));
```

**4. Push values and watch.**

```csharp
first.OnNext("Ada");         // prints nothing
last.OnNext("Lovelace");     // prints Ada Lovelace
first.OnNext("Augusta");     // prints Augusta Lovelace
```

Trace it one push at a time:

| You push | `first` holds | `last` holds | Output |
|---|---|---|---|
| `first` sends `Ada` | `Ada` | nothing yet | nothing |
| `last` sends `Lovelace` | `Ada` | `Lovelace` | `Ada Lovelace` |
| `first` sends `Augusta` | `Augusta` | `Lovelace` | `Augusta Lovelace` |

The first push printed nothing. `SyncLatest` cannot build a full name until both sides have sent at least one
value. Once they have, every push from either side produces a new full name.

## Adding values at the start or the end

### `Lead`

`Lead` sends one value before the source's own values.

Input: `2, 3`

```csharp
Signal.Range(2, 2)
      .Lead(0)
      .Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
0 2 3
```

Use it to give a stream a starting value, so a subscriber has something to show before the first real value
arrives.

### `Prepend`

`Prepend` sends values before the source's own values. It takes one value, several values, or a collection.

Input: `3, 4`

```csharp
Signal.Range(3, 2)
      .Prepend(1, 2)
      .Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
1 2 3 4
```

With one value, `Prepend(value)` does the same job as `Lead`.

### `StartWith`

`StartWith` sends several values before the source's own values, like `Prepend`. It takes the values directly or
as a collection.

Input: `3, 4`

```csharp
var header = new List<int> { 1, 2 };

Signal.Range(3, 2)
      .StartWith(header)
      .Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
1 2 3 4
```

### `Append`

`Append` sends one extra value after the source completes.

Input: `1, 2`, then completion

```csharp
Signal.Range(1, 2)
      .Append(99)
      .Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
1 2 99
```

The `99` only arrives when the source completes. A source that never completes never sends it.

## One source after another

### `Concat`

`Concat` runs a second stream after the first one completes. The second stream does not start until the first
has finished.

Input: `1, 2`, then `10, 11`

```csharp
Signal.Range(1, 2)
      .Concat(Signal.Range(10, 2))
      .Subscribe(x => Console.Write($"{x} "), () => Console.WriteLine("| done"));
```

Output:

```text
1 2 10 11 | done
```

### `Concat` on a stream of streams

A stream can carry other streams as its values. `Concat` on such a stream runs the inner streams one at a time,
in the order they arrive.

Input: a stream that carries two streams, `1, 2` and `10, 11`

```csharp
IObservable<IObservable<int>> pages = Signal.FromEnumerable(new[]
{
    Signal.Range(1, 2),
    Signal.Range(10, 2),
});

pages.Concat().Subscribe(x => Console.Write($"{x} "), () => Console.WriteLine("| done"));
```

Output:

```text
1 2 10 11 | done
```

### `Concat` on a stream of tasks

`Concat` on a stream of tasks waits for each task in turn and sends its result. The results come out in the
order the tasks arrived, not the order they finished.

Input: two tasks, where the second finishes first

```csharp
var firstTask = new TaskCompletionSource<int>();
var secondTask = new TaskCompletionSource<int>();

var tasks = new Signal<Task<int>>();
tasks.Concat().Subscribe(x => Console.Write($"{x} "), () => Console.WriteLine("| done"));

tasks.OnNext(firstTask.Task);
tasks.OnNext(secondTask.Task);
tasks.OnCompleted();

secondTask.SetResult(2);   // finishes first, but waits its turn
firstTask.SetResult(1);
```

Output:

```text
1 2 | done
```

The second task finished first. `Concat` still held its result back until the first task's result had gone out.

## Several sources at once

### `Merge` on two streams

`Merge` listens to two streams at the same time. It passes each value on as soon as it arrives, from either
stream.

Input: `a` and `b` push values in turn

```csharp
var a = new Signal<string>();
var b = new Signal<string>();

a.Merge(b).Subscribe(x => Console.Write($"{x} "));

a.OnNext("a1");
b.OnNext("b1");
b.OnNext("b2");
a.OnNext("a2");
```

Output:

```text
a1 b1 b2 a2
```

The values come out in the order they were pushed, whichever stream they came from.

### `Blend`

`Blend` does the same for any number of streams. It takes a stream of streams, or a collection of streams.

Input: two streams in a collection

```csharp
var a = new Signal<string>();
var b = new Signal<string>();

new[] { a, b }.Blend().Subscribe(x => Console.Write($"{x} "), () => Console.WriteLine("| done"));

b.OnNext("b1");
a.OnNext("a1");
a.OnCompleted();
b.OnNext("b2");
b.OnCompleted();
```

Output:

```text
b1 a1 b2 | done
```

`Blend` completes only when every stream has completed. `a` finished early, and `Blend` kept passing on values
from `b` until `b` finished too.

`Signal.Blend(a, b)` is the same operator called as a factory.

### `Blend` with a limit

`Blend(maxConcurrent)` caps how many streams it listens to at once. The rest wait their turn. When one stream
completes, the next one starts.

Input: the same two streams, with a limit of one

```csharp
var a = new Signal<string>();
var b = new Signal<string>();

new[] { a, b }.Blend(1).Subscribe(x => Console.Write($"{x} "), () => Console.WriteLine("| done"));

b.OnNext("b1");
a.OnNext("a1");
a.OnCompleted();
b.OnNext("b2");
b.OnCompleted();
```

Output:

```text
a1 b2 | done
```

Compare that with the output above. `b1` is missing.

> [!WARNING]
> A stream that waits its turn is not listening yet. `b` sent `b1` while `Blend` was still busy with `a`, so
> nobody received `b1` and it was lost. A `Signal<T>` sends values whether or not anyone is subscribed.
> Use a limit on streams like that only when losing early values is acceptable. A stream that starts its work
> when you subscribe, such as `Signal.Range`, loses nothing, because it waits for you.

### `BlendUnique`

`BlendUnique` merges streams, then drops any value equal to the one it passed on just before.

Input: two `int` streams

```csharp
var a = new Signal<int>();
var b = new Signal<int>();

LinqExtensions.BlendUnique(a, b).Subscribe(x => Console.Write($"{x} "));

a.OnNext(5);
b.OnNext(5);   // same as the last value, dropped
b.OnNext(6);
a.OnNext(6);   // same as the last value, dropped
a.OnNext(7);
b.OnNext(5);   // different from 7, so it gets through
```

Output:

```text
5 6 7 5
```

It compares each value only with the one just before. The last `5` got through because the value before it
was `7`. `BlendUnique` is a static method, so you call it on `LinqExtensions`. A second form takes an
`IEqualityComparer<T>` to decide what counts as equal.

### `Concat` against `Blend`

Both turn several streams into one. They differ in whether the streams run one after another or all at once.

| Operator | Listens to | Order of values |
|---|---|---|
| `Concat` | One stream at a time. The next starts when the last completes. | Every value from the first stream, then every value from the second. |
| `Blend` | Every stream at once. | Whatever order the values arrive in. |

Use `Concat` when order matters, such as pages you must show in sequence. Use `Blend` when you want every value
as soon as it exists, such as messages from several chat rooms.

## The first source to react

### `Race`

`Race` listens to several streams and keeps whichever one sends something first. It ignores the rest from then
on.

Input: two streams, where `b` sends first

```csharp
var a = new Signal<string>();
var b = new Signal<string>();

Signal.Race(a, b).Subscribe(x => Console.Write($"{x} "));

b.OnNext("b1");   // b wins
a.OnNext("a1");   // ignored from now on
b.OnNext("b2");
a.OnNext("a2");
```

Output:

```text
b1 b2
```

Use it to ask two servers the same question and take whichever answers first. On a stream of streams, call
`Race()`.

## Pairing values from two sources

### `Zip`

`Zip` lines values up by position. It matches the first value from one side with the first from the other,
then the second with the second, and so on.

Input: the left side sends `a, b` and completes. The right side sends `1, 2, 3`.

```csharp
var left = new Signal<string>();
var right = new Signal<int>();

left.Zip(right, (letter, number) => letter + number)
    .Subscribe(x => Console.Write($"{x} "), () => Console.WriteLine("| done"));

left.OnNext("a");
left.OnNext("b");
left.OnCompleted();

right.OnNext(1);
right.OnNext(2);
right.OnNext(3);
```

Output:

```text
a1 b2 | done
```

`Zip` completed straight after `b2`. The left side had finished, so `3` had nothing to pair with. `Zip` stops as
soon as no more pairs can form.

### `SyncLatest`

`SyncLatest` keeps the newest value from each side. When either side sends a value, it calls your lambda with
the newest value from both. It starts once both sides have sent something. The walkthrough at the top of this
page shows it step by step. Overloads take from 2 to 16 streams, calling your lambda with the newest value from
each one, in order, once every stream has sent at least one value.

### `CombineLatest` without a lambda

To get the newest values without writing a lambda, call `CombineLatest`. It works like `SyncLatest`, and hands you
the newest values as a tuple with named elements `First`, `Second`, `Third` and so on.

Input: a name, then two ages

```csharp
var name = new Signal<string>();
var age = new Signal<int>();

name.CombineLatest(age)
    .Subscribe(pair => Console.Write($"{pair.First} is {pair.Second}. "));

name.OnNext("Ada");
age.OnNext(36);
age.OnNext(37);
```

Output:

```text
Ada is 36. Ada is 37.
```

This saves you writing a lambda when all you want is the values together. It takes up to 16 streams.

### `CombineLatest` on a collection

When every stream carries the same type, `CombineLatest` on a collection hands you a list of the newest values.
The list is in the same order as your collection.

Input: three `int` streams

```csharp
var a = new Signal<int>();
var b = new Signal<int>();
var c = new Signal<int>();

new[] { a, b, c }.CombineLatest()
                 .Subscribe(values => Console.Write($"[{string.Join(",", values)}] "));

a.OnNext(1);
b.OnNext(2);
c.OnNext(3);    // prints [1,2,3]
b.OnNext(20);   // prints [1,20,3]
```

Output:

```text
[1,2,3] [1,20,3]
```

Use this form when the number of streams is not known until your app runs, such as one stream per open
document.

Pass a lambda to work on the list instead:

```csharp
new[] { a, b, c }.CombineLatest(values => values.Sum())
                 .Subscribe(total => Console.Write($"{total} "));
```

Output, for the same pushes:

```text
6 24
```

> [!IMPORTANT]
> To call the static form, pass an array. `LinqExtensions.CombineLatest(new[] { a, b })` hands you a list.
> Writing `LinqExtensions.CombineLatest(a, b)` does not. With the streams listed one by one, C# picks the tuple
> form above instead, and you get a tuple where you expected a list.

### `Latch`

`Latch` sends a value only when the left side sends. It pairs that value with the newest value the right side
has sent. The right side sending on its own sends nothing.

Input: button clicks on the left, temperature readings on the right

```csharp
var clicks = new Signal<string>();
var temperature = new Signal<int>();

clicks.Latch(temperature, (click, reading) => $"{click} at {reading}")
      .Subscribe(x => Console.WriteLine(x));

clicks.OnNext("click 1");        // no reading yet, dropped
temperature.OnNext(20);
temperature.OnNext(21);
clicks.OnNext("click 2");        // prints click 2 at 21
temperature.OnNext(22);
clicks.OnNext("click 3");        // prints click 3 at 22
```

Output:

```text
click 2 at 21
click 3 at 22
```

`click 1` was dropped, because the right side had not sent a reading yet. Use `Latch` when one stream says
*when* and the other says *what*, such as saving the current form whenever the user clicks Save.

### `ForkJoin`

`ForkJoin` waits for both sides to complete. Then it sends one value, built from the last value of each side.

Input: the left side sends `a, b, c`. The right side sends `1, 2`. Both complete.

```csharp
var left = new Signal<string>();
var right = new Signal<int>();

left.ForkJoin(right, (letter, number) => letter + number)
    .Subscribe(x => Console.Write($"{x} "), () => Console.WriteLine("| done"));

left.OnNext("a");
right.OnNext(1);
left.OnNext("b");
left.OnNext("c");
right.OnNext(2);
left.OnCompleted();
right.OnCompleted();
```

Output:

```text
c2 | done
```

Use it to run two jobs at once and act when both have finished, such as loading a user and their settings
before you show a page.

If either side completes without ever sending a value, `ForkJoin` has nothing to build from. It completes and
sends nothing.

```csharp
left.OnNext("a");
left.OnCompleted();
right.OnCompleted();   // right never sent anything
```

Output:

```text
| done
```

## `Zip`, `SyncLatest`, `Latch` and `ForkJoin` on one input

These four are the easiest to mix up. Here they all run on the same pushes, in the same order.

Pushes: the left sends `a`. The right sends `1`. The left sends `b`, then `c`. The right sends `2`. Then both
complete.

| You push | `Zip` | `SyncLatest` | `Latch` | `ForkJoin` |
|---|---|---|---|---|
| left sends `a` | — | — | — | — |
| right sends `1` | `a1` | `a1` | — | — |
| left sends `b` | — | `b1` | `b1` | — |
| left sends `c` | — | `c1` | `c1` | — |
| right sends `2` | `b2` | `c2` | — | — |
| both complete | done | done | done | `c2`, done |

The full output from each:

| Operator | Output | Sends a value when |
|---|---|---|
| `Zip` | `a1 b2` | Both sides have a value in the same position. |
| `SyncLatest` | `a1 b1 c1 c2` | Either side sends, once both have sent something. |
| `Latch` | `b1 c1` | The left side sends, once the right has sent something. |
| `ForkJoin` | `c2` | Both sides have completed. It sends one value. |

Read down one column to see one operator's rule at work:

- **`Zip`** waited at `b` and `c` because the right had only one value. When `2` arrived, it paired it with
  `b`, the next unmatched left value.
- **`SyncLatest`** sent something on every push after the first, because both sides had a value by then.
- **`Latch`** dropped `a`, because the right had sent nothing yet. It ignored `2`, because only the left side
  triggers it.
- **`ForkJoin`** stayed silent the whole time, then sent one value at the end.

## Every combination operator at a glance

| Operator | Second name | Sends a value when |
|---|---|---|
| `Lead` | — | Once, before the source's values. |
| `Prepend` | `StartWith` | Before the source's values. |
| `Append` | — | Once, after the source completes. |
| `Concat` | `Chain` | Each stream in turn, one after another. |
| `Merge(other)` | — | Either of two streams sends, as soon as it arrives. |
| `Blend` | `Merge` | Any stream sends, as soon as it arrives. |
| `Blend(maxConcurrent)` | `Merge(maxConcurrent)` | Any active stream sends, with a cap on how many run at once. |
| `BlendUnique` | — | Any stream sends a value different from the last one passed on. |
| `Race` | `Amb` | The first stream to send, and only that stream from then on. |
| `Zip` | `Pair` | Both sides have a value in the same position. |
| `SyncLatest` | `CombineLatest`, `PairLatest`, `FuseLatest` | Either side sends, once every side has sent something. |
| `CombineLatest` with no lambda | — | The same, handing you a tuple. |
| `CombineLatest` on a collection | — | The same, handing you a list. |
| `Latch` | `WithLatestFrom` | The left side sends, once the right has sent something. |
| `ForkJoin` | — | Both sides have completed. Once. |

`Signal` also offers several of these as factories that take the streams as arguments: `Signal.Concat`,
`Signal.Blend`, `Signal.Race`, `Signal.Pair`, `Signal.SyncLatest`, `Signal.PairLatest` and `Signal.ForkJoin`. They
behave the same as the operators.

## The types behind these operators

`LeadSignal<T>`, `PrependSignal<T>`, `StartWithEnumerableSignal<T>`, `ZipSignal<TLeft, TRight, TResult>`,
`CombineLatestSignal<TLeft, TRight, TResult>` and `LatchSignal<TLeft, TRight, TResult>` are public classes in
`ReactiveUI.Primitives.Advanced`. Each takes its sources through the constructor.

```csharp
using ReactiveUI.Primitives.Advanced;

// the operator
IObservable<string> a = left.Zip(right, (letter, number) => letter + number);

// the same thing, built directly
IObservable<string> b = new ZipSignal<string, int, string>(left, right, (letter, number) => letter + number);
```

Calling the operator is the normal path. Construct the type when you are writing an operator of your own and
want to place one inside it.

## The two package flavours

Every operator here ships twice. `ReactiveUI.Primitives` puts them under `ReactiveUI.Primitives.*`.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and puts them under
`ReactiveUI.Primitives.Reactive.*`. They behave the same in both.
