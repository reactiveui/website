---
Order: 3
---
# Errors and retries

When a stream **fails**, it sends an exception to its subscribers and stops. These helpers decide what happens
next: replace the failure with a value, log it, or subscribe again and retry, with or without a delay.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Extensions;
using ReactiveUI.Primitives.Signals;
```

The core operators for this job are on the [error handling](../error-handling.md) page. `Recover` switches to a
fallback stream, and `Reattempt` retries straight away. The helpers here are shorter calls for common cases, and add
delays between retries.

## A stream that fails twice

The retry examples use this method. It returns a stream that fails on its first two subscriptions and succeeds on the
third. **Subscribing again** runs a cold stream's work again, which is what a retry relies on.

```csharp
static IObservable<string> FlakyDownload(Func<int> nextAttempt) =>
    Signal.Lazy(() =>
    {
        var attempt = nextAttempt();
        Console.WriteLine($"attempt {attempt}");
        return attempt < 3
            ? Signal.Fail<string>(new TimeoutException($"attempt {attempt} timed out"))
            : Signal.Emit("downloaded");
    });
```

`Signal.Lazy` runs its lambda on each subscription, so every retry makes a fresh attempt.

## Replacing an error with a value

### `CatchAndReturn` and `CatchReturn`

`CatchAndReturn` catches any error. It sends the value you give, then completes. `CatchReturn` is the same helper
under a second name.

```csharp
Signal.Fail<int>(new InvalidOperationException("no data"))
      .CatchAndReturn(0)
      .Subscribe(static x => Console.WriteLine(x), static () => Console.WriteLine("completed"));
```

Output:

```text
0
completed
```

### `CatchAndReturn` for one exception type

Give `CatchAndReturn` a lambda that takes one exception type, and it catches only that type. The lambda builds the
value to send from the exception. Any other exception passes through as a failure.

Name the exception type on the lambda's parameter, and C# works out the rest:

```csharp
Signal.Fail<int>(new TimeoutException("slow"))
      .CatchAndReturn(static (TimeoutException error) => -1)
      .Subscribe(static x => Console.WriteLine(x), static () => Console.WriteLine("completed"));

Signal.Fail<int>(new InvalidOperationException("broken"))
      .CatchAndReturn(static (TimeoutException error) => -1)
      .Subscribe(static x => Console.WriteLine(x), static error => Console.WriteLine($"failed: {error.Message}"));
```

Output:

```text
-1
completed
failed: broken
```

### `CatchReturnUnit`

`CatchReturnUnit` works on a stream of `RxVoid`. It is `CatchAndReturn(RxVoid.Default)`: on an error, it sends one
`RxVoid` and completes. It suits a stream that only reports "done".

```csharp
Signal.Fail<RxVoid>(new InvalidOperationException("save failed"))
      .CatchReturnUnit()
      .Subscribe(static _ => Console.WriteLine("done anyway"));
```

Output:

```text
done anyway
```

## Ignoring an error

### `CatchIgnore`

`CatchIgnore` with no arguments catches any error and completes, sending nothing more.

```csharp
Signal.Fail<int>(new InvalidOperationException("no data"))
      .CatchIgnore()
      .Subscribe(static x => Console.WriteLine(x), static () => Console.WriteLine("completed"));
```

Output:

```text
completed
```

### `CatchIgnore` for one exception type

Give `CatchIgnore` an action that takes one exception type, and it catches only that type. It runs your action with
the exception, then completes. Any other exception passes through as a failure.

```csharp
Signal.Fail<int>(new TimeoutException("slow"))
      .CatchIgnore(static (TimeoutException error) => Console.WriteLine($"ignored: {error.Message}"))
      .Subscribe(static x => Console.WriteLine(x), static () => Console.WriteLine("completed"));
```

Output:

```text
ignored: slow
completed
```

If your action throws, that exception becomes the failure instead.

## Logging an error

### `LogErrors`

`LogErrors` runs your action with each error, then passes the error on. The stream still fails, so add a catch or
a retry after it if it should keep going.

```csharp
Signal.Fail<int>(new InvalidOperationException("boom"))
      .LogErrors(static error => Console.WriteLine($"log: {error.Message}"))
      .CatchAndReturn(0)
      .Subscribe(static x => Console.WriteLine(x));
```

Output:

```text
log: boom
0
```

## Retrying

Every retry helper here subscribes to the source again after a failure. Each one counts **retries**: the tries after
the first run. A count of 2 allows three runs in total, the same as [`Reattempt(2)`](../error-handling.md). When the
retries run out, the last error goes to your subscriber.

The delay helpers wait on `Sequencer.Default`, a background thread, unless you pass a sequencer.

| Helper | Retries | Wait before each retry |
|---|---|---|
| `OnErrorRetry()` | Until it succeeds | None |
| `OnErrorRetry(onError, ...)` | A count you give, or until it succeeds | None, or a fixed delay |
| `RetryWithFixedDelay` | A count you give | The same each time |
| `RetryWithBackoff` | A count you give | Grows each time |
| `RetryWithDelay` | A count you give | Whatever your lambda returns |
| `RetryForeverWithDelay` | Until it succeeds | The same each time |

Retrying forever is only safe for a failure that will clear up on its own. A bug that fails every time retries
forever too.

### `OnErrorRetry`

`OnErrorRetry()` with no arguments subscribes again after every failure, with no delay, until the source completes.

```csharp
var attempts = 0;

FlakyDownload(() => ++attempts)
    .OnErrorRetry()
    .Subscribe(static result => Console.WriteLine(result));
```

Output:

```text
attempt 1
attempt 2
attempt 3
downloaded
```

The other overloads take an action that runs on each failure, which suits logging. Add a retry count, a delay, and a
sequencer for the delay, in that order. The action is typed to one exception type, and runs only for that type.

```csharp
var attempts = 0;

FlakyDownload(() => ++attempts)
    .OnErrorRetry(
        static (TimeoutException error) => Console.WriteLine($"retrying after: {error.Message}"),
        retryCount: 5,
        delay: TimeSpan.FromMilliseconds(100))
    .Subscribe(static result => Console.WriteLine(result));
```

Output:

```text
attempt 1
retrying after: attempt 1 timed out
attempt 2
retrying after: attempt 2 timed out
attempt 3
downloaded
```

The action runs for the last failure too, when the retries run out. A failure of another exception type is not
retried: it goes straight to your subscriber.

### `RetryWithFixedDelay`

`RetryWithFixedDelay` waits the same time before each retry.

```csharp
var attempts = 0;

FlakyDownload(() => ++attempts)
    .RetryWithFixedDelay(retryCount: 5, delay: TimeSpan.FromMilliseconds(100))
    .Subscribe(static result => Console.WriteLine(result));
```

Output, with 100 ms between the attempts:

```text
attempt 1
attempt 2
attempt 3
downloaded
```

### `RetryWithBackoff`

`RetryWithBackoff` doubles the wait after each retry. This is **exponential backoff**: a busy server gets more room
to recover each time, instead of a flood of instant retries.

```csharp
var attempts = 0;

FlakyDownload(() => ++attempts)
    .RetryWithBackoff(maxRetries: 3, initialDelay: TimeSpan.FromMilliseconds(50))
    .Subscribe(static result => Console.WriteLine(result));
```

Output:

| Time | Output |
|---|---|
| 0 ms | `attempt 1` |
| 50 ms | `attempt 2` |
| 150 ms | `attempt 3`, then `downloaded` |

The waits were 50 ms, then 100 ms. The longer overload sets the multiplier, a longest wait, and the sequencer:

```csharp
IObservable<string> patient = FlakyDownload(() => 3).RetryWithBackoff(
    maxRetries: 10,
    initialDelay: TimeSpan.FromMilliseconds(100),
    backoffFactor: 1.5,
    maxDelay: TimeSpan.FromSeconds(5),
    scheduler: null);   // null uses Sequencer.Default
```

### `RetryWithDelay`

`RetryWithDelay` asks your lambda how long to wait before each retry. The lambda gets the retry number, starting at
`0` for the first retry.

```csharp
var attempts = 0;

FlakyDownload(() => ++attempts)
    .RetryWithDelay(
        retryCount: 5,
        delaySelector: static retry => TimeSpan.FromMilliseconds(100 * (retry + 1)))
    .Subscribe(static result => Console.WriteLine(result));
```

Output, with 100 ms before the first retry and 200 ms before the second:

```text
attempt 1
attempt 2
attempt 3
downloaded
```

### `RetryForeverWithDelay`

`RetryForeverWithDelay` waits the same time before each retry, and never gives up.

```csharp
var attempts = 0;

FlakyDownload(() => ++attempts)
    .RetryForeverWithDelay(TimeSpan.FromMilliseconds(100))
    .Subscribe(static result => Console.WriteLine(result));
```

Output:

```text
attempt 1
attempt 2
attempt 3
downloaded
```

## Every helper on this page at a glance

| Helper | Second name | What it does |
|---|---|---|
| `CatchAndReturn(value)` | `CatchReturn` | On any error, sends a value and completes. |
| `CatchAndReturn(lambda)` | — | On one exception type, sends a value built from it and completes. |
| `CatchReturnUnit` | — | On any error, sends `RxVoid` and completes. |
| `CatchIgnore()` | — | On any error, completes. |
| `CatchIgnore(action)` | — | On one exception type, runs an action and completes. |
| `LogErrors` | — | Runs an action with each error, and still fails. |
| `OnErrorRetry` | — | Retries, with an optional action, count and delay. |
| `RetryWithFixedDelay` | — | Retries a set number of times, with the same wait. |
| `RetryWithBackoff` | — | Retries with a wait that grows each time. |
| `RetryWithDelay` | — | Retries with a wait your lambda chooses. |
| `RetryForeverWithDelay` | — | Retries forever, with the same wait. |

## The types behind these helpers

`CatchReturnObservable<T>`, `CatchIgnoreEmptyObservable<T>`, `CatchIgnoreObservable<T, TException>`,
`CatchAndReturnWithFactoryObservable<T, TException>`, `LogErrorsObservable<T>` and `RetryForeverObservable<T>` are
public classes in `ReactiveUI.Primitives.Extensions.Operators`. Each takes its source through the constructor. The
delayed retry helpers build internal types, so call the helper.
