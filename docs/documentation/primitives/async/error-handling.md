---
Order: 6
---
# Async error handling

An async stream has two kinds of error, as the [async overview](index.md#errors-that-end-a-stream-and-errors-that-do-not)
explains. A **failure** ends the stream. A **resumable error** reports a problem, and the stream carries on. The
operators on this page deal with one or both.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Async;
```

The examples read a stream with this method. It prints each value, each resumable error, and how the stream ended.

```csharp
static async Task ShowAsync<T>(IObservableAsync<T> stream)
{
    await using IAsyncDisposable subscription = await stream.SubscribeAsync(
        static (value, _) => { Console.WriteLine($"value {value}"); return default; },
        static (error, _) => { Console.WriteLine($"resumable: {error.Message}"); return default; },
        static result => { Console.WriteLine(result.IsSuccess ? "completed" : $"failed: {result.Exception!.Message}"); return default; });

    await Task.Delay(50);
}
```

## Recovering from a failure

### `Recover`

`Recover` watches for a failure. When one arrives, it runs your lambda with the exception and carries on with the
stream the lambda returns. If the lambda throws, the stream fails with that exception.

```csharp
IObservableAsync<int> broken = SignalAsync.Fail<int>(new InvalidOperationException("no data"));

await ShowAsync(broken.Recover(static error => SignalAsync.Emit(-1)));
```

Output:

```text
value -1
completed
```

An overload of `Catch` also takes a lambda for resumable errors, which then do not reach your subscriber.

### `Resume`

`Resume` carries on with a fixed fallback stream after a failure.

```csharp
await ShowAsync(SignalAsync.Fail<int>(new InvalidOperationException("no data")).Resume(SignalAsync.Range(1, 2)));
```

Output:

```text
value 1
value 2
completed
```

### `CatchAndReturn`

`CatchAndReturn` sends one fallback value after a failure, then completes. Give it a lambda that takes one exception
type to catch only that type, and build the value from the exception.

```csharp
await ShowAsync(SignalAsync.Fail<int>(new InvalidOperationException("no data")).CatchAndReturn(0));
```

Output:

```text
value 0
completed
```

### `CatchIgnore`

`CatchIgnore` turns a failure into a normal completion. Give it an action that takes one exception type to catch only
that type. Other failures pass through.

```csharp
await ShowAsync(SignalAsync.Fail<int>(new InvalidOperationException("no data")).CatchIgnore());

await ShowAsync(SignalAsync.Fail<int>(new TimeoutException("slow"))
                           .CatchIgnore<int, TimeoutException>(static error => Console.WriteLine($"ignored: {error.Message}")));
```

Output:

```text
completed
ignored: slow
completed
```

Name both types when you call the typed form: the type the stream holds, then the exception type.

## Retrying

### `Retry` and `Reattempt`

`Retry()` subscribes again after every failure, until the stream completes. `Retry(n)` runs the stream at most `n`
times in total. `Reattempt(n)` allows `n` tries **after** the first run, so `Reattempt(2)` runs it up to three times.

The example stream fails on its first two runs and succeeds on the third:

```csharp
var runs = 0;
IObservableAsync<int> flaky = SignalAsync.Defer(() =>
    ++runs < 3 ? SignalAsync.Fail<int>(new InvalidOperationException($"run {runs} failed")) : SignalAsync.Emit(runs));

await ShowAsync(flaky.Retry(2));

runs = 0;
await ShowAsync(flaky.Reattempt(2));
```

Output:

```text
failed: run 2 failed
value 3
completed
```

`Retry(2)` stopped after two runs. `Reattempt(2)` allowed the third run, which worked. The `Defer` lambda changes
`runs`, so it cannot be `static`.

## Resumable errors

The example source sends a value, a resumable error, then another value:

```csharp
IObservableAsync<int> readings = SignalAsync.Create<int>(static async (witness, cancellationToken) =>
{
    await witness.OnNextAsync(1, cancellationToken);
    await witness.OnErrorResumeAsync(new InvalidOperationException("sensor glitch"), cancellationToken);
    await witness.OnNextAsync(2, cancellationToken);
    await witness.OnCompletedAsync(Result.Success);
    return ReactiveUI.Primitives.Async.Disposables.DisposableAsync.Create(static () => ValueTask.CompletedTask);
});

await ShowAsync(readings);
```

Output:

```text
value 1
resumable: sensor glitch
value 2
completed
```

### `OnErrorResumeAsFailure`

`OnErrorResumeAsFailure` turns a resumable error into a failure, so the first problem ends the stream.

```csharp
await ShowAsync(readings.OnErrorResumeAsFailure());
```

Output:

```text
value 1
failed: sensor glitch
```

### `CatchAndIgnoreErrorResume`

`CatchAndIgnoreErrorResume` recovers from a failure like `Recover`. It also takes resumable errors away from your
subscriber, and sends them to the global handler instead. See
[`UnhandledExceptionHandler`](advanced.md#unhandledexceptionhandler).

```csharp
UnhandledExceptionHandler.Register(static error => Console.WriteLine($"global handler: {error.Message}"));

await ShowAsync(readings.CatchAndIgnoreErrorResume(static error => SignalAsync.Emit(-1)));
```

Output:

```text
value 1
global handler: sensor glitch
value 2
completed
```

### `LogErrors`

`LogErrors` runs your action with each resumable error as it passes, and changes nothing. A failure that ends the
stream is not passed to the action.

```csharp
await ShowAsync(readings.LogErrors(static error => Console.WriteLine($"log: {error.Message}")));
```

Output:

```text
value 1
log: sensor glitch
resumable: sensor glitch
value 2
completed
```

## Every operator on this page at a glance

| Operator | Second name | What it does |
|---|---|---|
| `Recover` | `Catch`, `Rescue` | Carries on with a stream built from the failure. |
| `Resume` | — | Carries on with a fixed stream after a failure. |
| `CatchAndReturn` | — | Sends a fallback value after a failure. |
| `CatchIgnore` | — | Completes instead of failing. |
| `Retry` | — | Subscribes again after a failure, up to a total number of runs. |
| `Reattempt` | — | Subscribes again after a failure, up to a number of extra tries. |
| `OnErrorResumeAsFailure` | — | Turns resumable errors into a failure. |
| `CatchAndIgnoreErrorResume` | — | `Recover`, sending resumable errors to the global handler. |
| `LogErrors` | — | Runs an action with each resumable error. |
