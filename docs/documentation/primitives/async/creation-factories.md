---
Order: 1
---
# Async creation factories

A **factory** builds a stream for you. The async factories are static methods on `SignalAsync`, plus a few extension
methods that turn something you already have into an async stream.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Async;
```

The examples read a whole stream with `ToListAsync`, which you can read about on the [results](results.md) page.

## Single values and empty streams

### `Emit`

`Emit` sends one value, then completes.

```csharp
List<int> values = await SignalAsync.Emit(42).ToListAsync();
Console.WriteLine(string.Join(", ", values));
```

Output:

```text
42
```

### `Empty`

`Empty` completes at once and sends nothing.

```csharp
List<int> values = await SignalAsync.Empty<int>().ToListAsync();
Console.WriteLine(values.Count);
```

Output:

```text
0
```

### `Never`

`Never` sends nothing and never ends. It is useful in tests, and as a placeholder for "no stream yet". A terminal such
as `ToListAsync` would wait forever, so give it a `CancellationToken` or a time limit.

```csharp
using var timeout = new CancellationTokenSource(TimeSpan.FromMilliseconds(100));

try
{
    await SignalAsync.Never<int>().ToListAsync(timeout.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("gave up waiting");
}
```

Output:

```text
gave up waiting
```

### `Fail`

`Fail` ends the stream at once with the exception you give.

```csharp
try
{
    await SignalAsync.Fail<int>(new InvalidOperationException("no data")).ToListAsync();
}
catch (InvalidOperationException error)
{
    Console.WriteLine(error.Message);
}
```

Output:

```text
no data
```

## Sequences

### `Range`

`Range` sends a run of `int` values, counting up from a start value, then completes.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.Range(5, 3).ToListAsync()));
```

Output:

```text
5, 6, 7
```

### `FromEnumerable` and `FromAsyncEnumerable`

`FromEnumerable` sends each item of a collection. `FromAsyncEnumerable` sends each item of an `IAsyncEnumerable<T>`,
waiting for each one. Each subscriber reads the collection from the start.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.FromEnumerable(["a", "b"]).ToListAsync()));
Console.WriteLine(string.Join(", ", await SignalAsync.FromAsyncEnumerable(CountAsync()).ToListAsync()));

static async IAsyncEnumerable<int> CountAsync()
{
    for (var i = 1; i <= 3; i++)
    {
        await Task.Delay(10);
        yield return i;
    }
}
```

Output:

```text
a, b
1, 2, 3
```

### `ToAsyncSignal`

`ToAsyncSignal` is an extension method that does the same job from the other side. It turns an `IEnumerable<T>`, an
`IAsyncEnumerable<T>` or a `Task<T>` into an async stream. On a `Task` with no result, it sends `RxVoid` when the task
finishes.

```csharp
Console.WriteLine(string.Join(", ", await new[] { 1, 2 }.ToAsyncSignal().ToListAsync()));
Console.WriteLine(string.Join(", ", await Task.FromResult("done").ToAsyncSignal().ToListAsync()));
```

Output:

```text
1, 2
done
```

## Async work

### `FromAsync`

`FromAsync` runs an async method once for each subscriber, and sends its result. The method gets a
`CancellationToken` that cancels when the subscription is disposed.

```csharp
IObservableAsync<string> page = SignalAsync.FromAsync(static async cancellationToken =>
{
    await Task.Delay(50, cancellationToken);
    return "page loaded";
});

Console.WriteLine(await page.FirstAsync());
```

Output:

```text
page loaded
```

On a `Func<CancellationToken, ValueTask>` with no result, the extension method `FromAsync()` sends `RxVoid` when the
work finishes.

### `Start`

`Start` runs a plain method and sends what it returns. Pass a `TaskScheduler` to choose where it runs. On an `Action`,
the extension method `Start()` runs the action and sends `RxVoid`.

```csharp
Console.WriteLine(await SignalAsync.Start(static () => 6 * 7).FirstAsync());
```

Output:

```text
42
```

### `Defer`

`Defer` calls your factory each time someone subscribes, and subscribes to the stream it returns. Each subscriber gets
a fresh stream. The async overload lets the factory `await` before it builds the stream.

```csharp
var calls = 0;
IObservableAsync<int> fresh = SignalAsync.Defer(() => SignalAsync.Emit(++calls));

Console.WriteLine(await fresh.FirstAsync());
Console.WriteLine(await fresh.FirstAsync());
```

Output:

```text
1
2
```

The factory changes `calls`, so it cannot be `static`.

### `Use`

`Use` creates a resource for each subscriber, builds a stream from it, and disposes the resource when the stream ends.
The resource is an `IAsyncDisposable`, created by an async factory.

```csharp
IObservableAsync<string> lines = SignalAsync.Use(
    static _ => ValueTask.FromResult(new Connection()),
    static connection => SignalAsync.Emit(connection.Read()));

Console.WriteLine(await lines.FirstAsync());

public sealed class Connection : IAsyncDisposable
{
    public string Read() => "hello";

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
```

Output:

```text
hello
```

## Building your own stream

### `Create`

`Create` builds a stream from your own subscribe method. It gets the witness to send values to and a
`CancellationToken`, and returns an `IAsyncDisposable` that cleans up. A **witness** is the object that receives a
stream's values: here an `IObserverAsync<T>`.

```csharp
using ReactiveUI.Primitives.Async.Disposables;

IObservableAsync<int> countdown = SignalAsync.Create<int>(static async (witness, cancellationToken) =>
{
    await witness.OnNextAsync(3, cancellationToken);
    await witness.OnNextAsync(2, cancellationToken);
    await witness.OnNextAsync(1, cancellationToken);
    await witness.OnCompletedAsync(Result.Success);
    return DisposableAsync.Create(static () => ValueTask.CompletedTask);
});

Console.WriteLine(string.Join(", ", await countdown.ToListAsync()));
```

Output:

```text
3, 2, 1
```

`DisposableAsync.Create` builds an `IAsyncDisposable` from a method. See [writing your own operator](advanced.md).

### `CreateAsBackgroundJob`

`CreateAsBackgroundJob` runs your async method as a background job for each subscriber. Send values to the witness
from the job. The job's `CancellationToken` cancels when the subscription is disposed. Returning from the job does not
end the stream, so send the completion yourself.

```csharp
IObservableAsync<int> job = SignalAsync.CreateAsBackgroundJob<int>(static async (witness, cancellationToken) =>
{
    for (var i = 1; i <= 3; i++)
    {
        await Task.Delay(10, cancellationToken);
        await witness.OnNextAsync(i, cancellationToken);
    }

    await witness.OnCompletedAsync(Result.Success);
});

Console.WriteLine(string.Join(", ", await job.ToListAsync()));
```

Output:

```text
1, 2, 3
```

Overloads take a `TaskScheduler` to run the job on, or `startSynchronously: true` to start it on the subscribing
thread.

## Timers

The timers send `long` counters. `After`, `Timer` and `Interval` take an optional `TimeProvider`, so a test can drive
a fake clock. Pass `FakeTimeProvider` from the `Microsoft.Extensions.TimeProvider.Testing` package.

### `After`

`After` waits, sends `0`, then completes. Give it a period as well, and it keeps sending `1`, `2` and so on, once per
period.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.After(TimeSpan.FromMilliseconds(50)).ToListAsync()));

Console.WriteLine(string.Join(", ", await SignalAsync.After(TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(20))
                                                     .Take(3)
                                                     .ToListAsync()));
```

Output:

```text
0
0, 1, 2
```

### `Every`

`Every` sends `0`, `1`, `2` and so on, once per period, forever. The first value comes one period after you subscribe.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.Every(TimeSpan.FromMilliseconds(20)).Take(3).ToListAsync()));
```

Output:

```text
0, 1, 2
```

### `Interval`

`Interval` also ticks once per period, forever, but its counter starts at `1`.

```csharp
Console.WriteLine(string.Join(", ", await SignalAsync.Interval(TimeSpan.FromMilliseconds(20)).Take(3).ToListAsync()));
```

Output:

```text
1, 2, 3
```

## Many streams at once

### `Blend`

`Blend` subscribes to all the streams you give at once, and sends values from each as they arrive.

### `Chain`

`Chain` subscribes to the streams one after another. Each starts when the one before it completes.

```csharp
IObservableAsync<int> first = SignalAsync.Range(1, 2);
IObservableAsync<int> second = SignalAsync.Range(10, 2);

Console.WriteLine(string.Join(", ", await SignalAsync.Chain(first, second).ToListAsync()));
Console.WriteLine((await SignalAsync.Blend(first, second).ToListAsync()).Count);
```

Output:

```text
1, 2, 10, 11
4
```

The [combination](combination.md) page covers the same operators on a stream you already hold.

## `RxVoid` streams

`RxVoid` is a value that carries no data. It suits a stream that only says "something happened".

```csharp
Console.WriteLine((await SignalAsyncReactiveExtensions.EmitRxVoid().ToListAsync()).Count);
```

Output:

```text
1
```

## Every factory at a glance

| Factory | Second name | What it does |
|---|---|---|
| `SignalAsync.Emit` | `Return` | One value, then completes. |
| `SignalAsync.Empty` | `None` | Completes with no values. |
| `SignalAsync.Never` | — | Never sends and never ends. |
| `SignalAsync.Fail` | `Throw` | Ends with an exception. |
| `SignalAsync.Range` | `Sequence` | A run of `int` values. |
| `SignalAsync.FromEnumerable` | — | Each item of a collection. |
| `SignalAsync.FromAsyncEnumerable` | — | Each item of an `IAsyncEnumerable<T>`. |
| `ToAsyncSignal()` | — | A collection, an `IAsyncEnumerable<T>` or a task as a stream. |
| `SignalAsync.FromAsync` | — | The result of an async method, per subscriber. |
| `SignalAsync.Start` | — | The result of a plain method. |
| `SignalAsync.Defer` | — | A fresh stream per subscriber. |
| `SignalAsync.Use` | `Using` | A stream built from a resource that is disposed when the stream ends. |
| `SignalAsync.Create` | — | A stream from your own subscribe method. |
| `SignalAsync.CreateAsBackgroundJob` | — | A stream fed by a background job. |
| `SignalAsync.After` | `Timer` | `0` after a wait, then optionally once per period. |
| `SignalAsync.Every` | `Pulse` | A counter from `0`, once per period. |
| `SignalAsync.Interval` | — | A counter from `1`, once per period. |
| `SignalAsync.Blend` | — | All streams at once. |
| `SignalAsync.Chain` | — | Streams one after another. |
| `SignalAsyncReactiveExtensions.EmitRxVoid` | — | One `RxVoid`, then completes. |
| `Action.Start()` | — | Runs an action and sends `RxVoid`. |
| `Func<CancellationToken, ValueTask>.FromAsync()` | — | Runs async work and sends `RxVoid`. |
| `Task.ToAsyncSignal()` | — | Sends `RxVoid` when the task finishes. |
