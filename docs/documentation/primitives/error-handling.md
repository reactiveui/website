---
Order: 6
---
# Error handling

A stream ends in one of two ways. It **completes** when it has nothing more to send. It **fails** when
something goes wrong, and hands your error callback an exception. Once a stream has failed, it sends nothing
more.

An error handling operator decides what happens instead of that failure. It can swap in another stream, carry
on with a fallback, try the whole thing again, or run some clean-up code however the stream ends.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;
```

## Your first recovery

You read a temperature sensor. The sensor sometimes drops off the network partway through.

**1. Start with a stream that fails partway.** `Concat` runs the second stream after the first. Here the second
one fails straight away, which stands in for the sensor dropping off.

```csharp
IObservable<int> readings = Signal.Range(20, 2)
    .Concat(Signal.Fail<int>(new InvalidOperationException("sensor lost")));
```

**2. See what happens with no error handling.**

```csharp
readings.Subscribe(
    x => Console.WriteLine(x),
    error => Console.WriteLine($"failed: {error.Message}"));
```

Output:

```text
20
21
failed: sensor lost
```

The stream sent two readings, then failed. Nothing more arrives after a failure.

**3. Recover from the failure.** `Recover` catches the error and switches to a stream you build. Here it sends
the last reading you trust.

```csharp
readings.Recover(error => Signal.Emit(19))
        .Subscribe(x => Console.WriteLine(x), () => Console.WriteLine("done"));
```

Output:

```text
20
21
19
done
```

Your subscriber never saw the error. It saw two real readings, then the fallback, then a normal completion.

## Switching to another stream

### `Recover`

`Recover` catches an error and switches to the stream your lambda returns. The lambda receives the exception, so it
can decide what to send.

The values that arrived before the error still reach your subscriber. `Recover` only replaces what comes after
it. The walkthrough above shows it working.

A lambda like `error => ...` catches every error. Give the lambda's parameter an exception type, and `Recover`
catches only that type. Every other error passes straight through to your subscriber.

Input: a page that fails with a `TimeoutException`

```csharp
IObservable<string> page = Signal.Emit("loading")
    .Concat(Signal.Fail<string>(new TimeoutException("too slow")));

page.Recover((TimeoutException error) => Signal.Emit("try again later"))
    .Subscribe(x => Console.WriteLine(x));
```

Output:

```text
loading
try again later
```

The same code on a page that fails in a different way:

```csharp
IObservable<string> broken = Signal.Emit("loading")
    .Concat(Signal.Fail<string>(new InvalidOperationException("bad data")));

broken.Recover((TimeoutException error) => Signal.Emit("try again later"))
      .Subscribe(
          x => Console.WriteLine(x),
          error => Console.WriteLine($"failed: {error.Message}"));
```

Output:

```text
loading
failed: bad data
```

`Recover` let the `InvalidOperationException` through, because it only catches `TimeoutException`.

> [!IMPORTANT]
> Say which exception you want by giving the lambda's parameter a type: `(TimeoutException error) => ...`.
> Writing `page.Recover<TimeoutException>(...)` does not compile. This `Recover` has two type parameters, the
> type of value and the type of exception, and C# will not let you name only one of them. To name the types
> yourself, name both: `page.Recover<string, TimeoutException>(error => ...)`.

### `Recover` on a list of streams

`Recover` on a collection of streams tries each one in turn. It moves to the next stream only when the current
one fails. It stops at the first stream that completes without failing.

Input: a primary source that is down, then a backup, then a cache

```csharp
IObservable<string> primary = Signal.Fail<string>(new InvalidOperationException("primary down"));
IObservable<string> backup = Signal.Emit("from backup");
IObservable<string> cache = Signal.Emit("from cache");

new[] { primary, backup, cache }.Recover()
    .Subscribe(x => Console.WriteLine(x), () => Console.WriteLine("done"));
```

Output:

```text
from backup
done
```

The backup worked, so `Recover` never tried the cache.

If every stream fails, your subscriber receives the error from the last one:

```csharp
IObservable<string> first = Signal.Fail<string>(new InvalidOperationException("first down"));
IObservable<string> second = Signal.Fail<string>(new InvalidOperationException("second down"));

new[] { first, second }.Recover()
    .Subscribe(
        x => Console.WriteLine(x),
        error => Console.WriteLine($"failed: {error.Message}"));
```

Output:

```text
failed: second down
```

`Signal.Recover(primary, backup, cache)` does the same, taking the streams as arguments.

## Carrying on with a fallback

### `Resume`

`Resume` carries on with a fallback stream when the source fails. Unlike `Recover`, you give it the fallback
stream directly rather than a lambda that builds one.

```csharp
readings.Resume(Signal.Emit(19))
        .Subscribe(x => Console.WriteLine(x), () => Console.WriteLine("done"));
```

Output:

```text
20
21
19
done
```

If the source completes without failing, `Resume` completes too. It does not run the fallback.

### `OnErrorResumeNext`

`OnErrorResumeNext` carries on with a second stream when the first one ends, **however** it ends. It moves on
after a failure, and it also moves on after a normal completion.

`Signal.OnErrorResumeNext` takes several streams and runs them one after another. It ignores the failure of
every one of them.

```csharp
Signal.OnErrorResumeNext(
        Signal.Range(1, 2).Concat(Signal.Fail<int>(new InvalidOperationException("a"))),
        Signal.Emit(10),
        Signal.Fail<int>(new InvalidOperationException("b")))
      .Subscribe(x => Console.Write($"{x} "), () => Console.WriteLine("done"));
```

Output:

```text
1 2 10 done
```

The first stream failed after `1 2`, and the third failed at once. Neither failure reached your subscriber. The
combined stream completed normally.

### `Resume` against `OnErrorResumeNext`

The two only differ when the source does **not** fail.

Input: `1, 2`, then a normal completion, with `99` as the fallback

| Operator | Output | Runs the fallback when the source |
|---|---|---|
| `Resume` | `1 2`, then done | fails. |
| `OnErrorResumeNext` | `1 2 99`, then done | fails or completes. |

Use `Resume` when the fallback is only for when things go wrong. Use `OnErrorResumeNext` when you always want
the second stream to follow, and you do not care whether the first one failed.

## Trying again

### `Reattempt`

`Reattempt` subscribes to the source again when it fails. The `int` you give is how many **extra** tries it
makes. If every try fails, your subscriber gets the error from the last one.

Input: a request that fails twice, then works

```csharp
var attempts = 0;

IObservable<string> request = Signal.Lazy(() =>
{
    attempts++;
    return attempts < 3
        ? Signal.Fail<string>(new TimeoutException("too slow"))
        : Signal.Emit("ok");
});

request.Reattempt(2)
       .Subscribe(x => Console.WriteLine($"{x} after {attempts} attempts"));
```

Output:

```text
ok after 3 attempts
```

`Reattempt(2)` allowed two extra tries, so the source ran three times in total. `Signal.Lazy` builds a fresh
stream for each try, which is what lets each attempt behave differently.

When every try fails:

```csharp
IObservable<string> alwaysFails = Signal.Fail<string>(new TimeoutException("too slow"));

alwaysFails.Reattempt(2)
           .Subscribe(
               x => Console.WriteLine(x),
               error => Console.WriteLine($"gave up: {error.Message}"));
```

Output:

```text
gave up: too slow
```

> [!WARNING]
> Values sent by a failed try are not taken back. Your subscriber still receives them, and then receives the
> values from the next try too.
>
> ```csharp
> var attempt = 0;
>
> IObservable<int> flaky = Signal.Lazy(() =>
> {
>     attempt++;
>     return attempt < 3
>         ? Signal.Emit(attempt).Concat(Signal.Fail<int>(new TimeoutException("too slow")))
>         : Signal.Emit(attempt);
> });
>
> flaky.Reattempt(2).Subscribe(x => Console.Write($"{x} "));
> ```
>
> Output: `1 2 3`. The first two tries each sent a value before they failed, and those values arrived. If a
> half-finished try would leave your screen or your data in a bad state, clear it before you try again.

### `Retry`

`Retry` also subscribes again when the source fails, but the `int` you give is the **total** number of runs, first run
included. `Retry(3)` runs the source up to three times, the same as `Reattempt(2)`.

```csharp
var runs = 0;

IObservable<string> flakyRequest = Signal.Lazy(() =>
{
    runs++;
    return Signal.Fail<string>(new TimeoutException($"run {runs} too slow"));
});

flakyRequest.Retry(3)
            .Subscribe(
                x => Console.WriteLine(x),
                error => Console.WriteLine($"gave up: {error.Message}"));
```

Output:

```text
gave up: run 3 too slow
```

`Retry(1)` runs the source once and never tries again. `Retry(0)` completes without running it.

### `Repeat`

`Repeat` runs the source again each time it **completes**. Give it an `int` and that is how many times the
source runs in **total**. Leave it out and it repeats for ever.

Input: `1, 2`

```csharp
Signal.Range(1, 2).Repeat(3).Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
1 2 1 2 1 2
```

The source ran three times. Repeating for ever needs something to stop it, such as `Take`:

```csharp
Signal.Range(1, 2).Repeat().Take(5).Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
1 2 1 2 1
```

`Repeat` only repeats after a completion. A failure stops it, and the error goes straight to your subscriber:

```csharp
readings.Repeat(3)
        .Subscribe(
            x => Console.WriteLine(x),
            error => Console.WriteLine($"failed: {error.Message}"));
```

Output:

```text
20
21
failed: sensor lost
```

### `Reattempt` against `Repeat`

Both run the source again, and their `int` arguments mean different things. This is easy to get wrong.

| Operator | Runs the source again after | The `int` you give is |
|---|---|---|
| `Reattempt(2)` | A failure | Extra tries. The source runs up to 3 times in total. |
| `Repeat(2)` | A completion | Total runs. The source runs 2 times in total. |

## Cleaning up however it ends

### `Finally`

`Finally` runs a piece of code once, when the subscription ends. It runs whether the stream completed, failed,
or you disposed the subscription. Use it to release something the stream was using, such as closing a file.

```csharp
Signal.Range(1, 2)
      .Finally(() => Console.WriteLine("cleaned up"))
      .Subscribe(x => Console.WriteLine(x), () => Console.WriteLine("done"));
```

Output:

```text
1
2
done
cleaned up
```

Your clean-up code runs **after** your subscriber's own completion or error callback.

It also runs when you dispose the subscription early. It only ever runs once, even if you dispose twice.

```csharp
var live = new Signal<int>();

IDisposable subscription = live.Finally(() => Console.WriteLine("cleaned up"))
                               .Subscribe(x => Console.WriteLine(x));

live.OnNext(1);
subscription.Dispose();
subscription.Dispose();
```

Output:

```text
1
cleaned up
```

## Throwing an error you caught

Sometimes you keep an error from a stream and want to throw it later, in ordinary code. Two helpers do that.

Both keep the exception's original stack trace. Writing `throw problem;` yourself resets the stack trace to the
line that throws it, and you lose where the error really came from. Prefer these helpers over `throw problem;`
whenever you throw an exception you did not just create.

### `Rethrow`

`Rethrow` throws the exception if there is one. If the exception is `null`, it does nothing. That saves you
writing an `if` around it.

```csharp
Exception? problem = null;
readings.Subscribe(x => { }, error => problem = error);

problem.Rethrow();   // throws InvalidOperationException: sensor lost
```

### `Throw`

`Throw` throws an exception you know is not `null`. Use it when you have already checked the exception is there
and do not need the `null` check that `Rethrow` makes for you.

```csharp
problem.Throw();   // throws InvalidOperationException: sensor lost
```

## Errors in a ReactiveUI app

In a ReactiveUI app, an error that reaches a command or a binding with nothing to handle it goes to ReactiveUI's
default exception handler. The operators on this page let you deal with an error closer to where it happens,
before it gets that far. See [the default exception handler](../reactiveui/handbook/default-exception-handler.md).

## Every error handling operator at a glance

| Operator | Second name | What it does |
|---|---|---|
| `Recover(error => ...)` | `Rescue` | Switches to the stream your lambda builds when the source fails. |
| `Recover((TException error) => ...)` | `Catch` | The same, only for one type of exception. |
| `Recover()` on a collection | `Signal.Recover` | Tries each stream in turn until one completes without failing. |
| `Resume` | — | Carries on with a fallback stream when the source fails. |
| `OnErrorResumeNext` | — | Carries on with a second stream when the first fails or completes. |
| `Reattempt(n)` | — | Tries the source again after a failure, `n` extra times. |
| `Retry(n)` | — | Tries the source again after a failure, up to `n` runs in total. |
| `Repeat` | — | Runs the source again after a completion, a set number of times in total or for ever. |
| `Finally` | `OnCleanup` | Runs your code once when the subscription ends, however it ends. |
| `Rethrow` | — | Throws an exception if it is not `null`, keeping its stack trace. |
| `Throw` | — | Throws an exception you know is not `null`, keeping its stack trace. |

## The types behind these operators

`RecoverSignal<T, TException>`, `ResumeSignal<T>`, `ReattemptSignal<T>`, `RepeatSourceSignal<T>`,
`OnErrorResumeNextSignal<T>` and `FinallySignal<T>` are public classes in `ReactiveUI.Primitives.Advanced`. Each takes its source through the
constructor. `RepeatSourceSignal<T>` takes the number of runs as an `int?`, where `null` means repeat for ever.

```csharp
using ReactiveUI.Primitives.Advanced;

// the operator
IObservable<string> a = request.Reattempt(2);

// the same thing, built directly
IObservable<string> b = new ReattemptSignal<string>(request, 2);
```

`OnErrorResumeNextSignal<T>` takes its streams as a collection, and runs them one after another however each one ends:

```csharp
new OnErrorResumeNextSignal<int>([
        Signal.Emit(1).Concat(Signal.Fail<int>(new TimeoutException("cache timed out"))),
        Signal.Emit(2),
        Signal.Emit(3)])
    .Subscribe(static x => Console.Write($"{x} "), static () => Console.WriteLine("done"));
```

Output:

```text
1 2 3 done
```

Calling the operator is the normal path. Construct the type when you are writing an operator of your own and
want to place one inside it.

## The two package flavours

Every operator here ships twice. `ReactiveUI.Primitives` puts them under `ReactiveUI.Primitives.*`.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and puts them under
`ReactiveUI.Primitives.Reactive.*`. They behave the same in both.
