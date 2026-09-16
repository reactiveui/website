---
Order: 5
---
# Async time

A time operator changes *when* values arrive. It can hold values back, wait for a pause, sample on a timer, or give up
when nothing arrives.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Async;
using ReactiveUI.Primitives.Async.Signals;
```

`Delay`, `Throttle`, `ThrottleDistinct`, `DebounceUntil`, `Timeout` and `Probe` take an optional `TimeProvider`. A
test passes `FakeTimeProvider`, from the `Microsoft.Extensions.TimeProvider.Testing` package, and moves time forward by
hand, so it never waits for real time.

The examples send values into an async signal with a pause between them, so you can see what happens at each moment.

## Holding values back

### `Delay`

`Delay` holds every value back by the same amount of time. Errors and completion pass straight through.

```csharp
ISignalAsync<string> messages = Signal.Create<string>();
var clock = System.Diagnostics.Stopwatch.StartNew();

await using IAsyncDisposable subscription = await messages
    .Delay(TimeSpan.FromMilliseconds(200))
    .SubscribeAsync(message => Console.WriteLine($"{message}, waited 200 ms: {clock.ElapsedMilliseconds >= 200}"));

await messages.OnNextAsync("hello", CancellationToken.None);
await Task.Delay(400);
```

Output:

```text
hello, waited 200 ms: True
```

## Waiting for a pause

### `Throttle`

`Throttle` holds each value. If a newer value arrives before the wait is over, it drops the older one and waits
again. It sends a value only once the stream has been quiet for the time you give. A search box that searches when the
user stops typing is the usual fit.

```csharp
ISignalAsync<string> keystrokes = Signal.Create<string>();

await using IAsyncDisposable subscription = await keystrokes
    .Throttle(TimeSpan.FromMilliseconds(200))
    .SubscribeAsync(static text => Console.WriteLine($"search for {text}"));

await keystrokes.OnNextAsync("c", CancellationToken.None);
await keystrokes.OnNextAsync("ca", CancellationToken.None);
await keystrokes.OnNextAsync("cat", CancellationToken.None);
await Task.Delay(400);
```

Output:

```text
search for cat
```

### `ThrottleDistinct`

`ThrottleDistinct` waits for a pause like `Throttle`, and also drops a value equal to the last one it sent.

```csharp
ISignalAsync<string> keystrokes = Signal.Create<string>();

await using IAsyncDisposable subscription = await keystrokes
    .ThrottleDistinct(TimeSpan.FromMilliseconds(100))
    .SubscribeAsync(static text => Console.WriteLine($"search for {text}"));

await keystrokes.OnNextAsync("cat", CancellationToken.None);
await Task.Delay(300);
await keystrokes.OnNextAsync("cat", CancellationToken.None);   // the same term again
await Task.Delay(300);
```

Output:

```text
search for cat
```

### `DebounceUntil`

`DebounceUntil` waits for a pause like `Throttle`, except that a value that passes your test goes out at once, and
drops the value that was waiting.

```csharp
ISignalAsync<int> progress = Signal.Create<int>();

await using IAsyncDisposable subscription = await progress
    .DebounceUntil(TimeSpan.FromMilliseconds(200), static percent => percent == 100)
    .SubscribeAsync(static p => Console.WriteLine($"{p}%"));

await progress.OnNextAsync(40, CancellationToken.None);
await progress.OnNextAsync(100, CancellationToken.None);
await Task.Delay(400);
```

Output:

```text
100%
```

## Sampling

### `Probe`

`Probe` sends the newest value once each period has passed, and drops the values in between. A value still waiting
when the stream completes goes out before the completion.

```csharp
ISignalAsync<int> position = Signal.Create<int>();

await using IAsyncDisposable subscription = await position
    .Probe(TimeSpan.FromMilliseconds(200))
    .SubscribeAsync(static p => Console.WriteLine($"position {p}"));

await position.OnNextAsync(1, CancellationToken.None);
await position.OnNextAsync(2, CancellationToken.None);
await position.OnNextAsync(3, CancellationToken.None);
await Task.Delay(400);
```

Output:

```text
position 3
```

## Giving up

### `Timeout`

`Timeout` fails the stream with `TimeoutException` when the gap between two values, or before the first value, grows
longer than the time you give. Each value restarts the wait.

```csharp
IObservableAsync<string> slow = SignalAsync.After(TimeSpan.FromSeconds(10)).Select(static _ => "done");

try
{
    await slow.Timeout(TimeSpan.FromMilliseconds(100)).WaitCompletionAsync();
}
catch (TimeoutException)
{
    Console.WriteLine("gave up after 100 ms");
}
```

Output:

```text
gave up after 100 ms
```

Give `Timeout` a fallback stream, and it switches to that stream instead of failing.

```csharp
IObservableAsync<string> slow = SignalAsync.After(TimeSpan.FromSeconds(10)).Select(static _ => "fresh");

Console.WriteLine(await slow.Timeout(TimeSpan.FromMilliseconds(100), SignalAsync.Emit("cached")).FirstAsync());
```

Output:

```text
cached
```

## Every operator on this page at a glance

| Operator | Second name | What it does |
|---|---|---|
| `Delay` | `Shift` | Holds every value back by the same time. |
| `Throttle` | — | The newest value once the stream is quiet. |
| `ThrottleDistinct` | — | `Throttle`, dropping a value equal to the last one sent. |
| `DebounceUntil` | — | `Throttle`, with values that pass a test going out at once. |
| `Probe` | `Sample` | The newest value once per period. |
| `Timeout` | `Expire` | Fails, or switches to a fallback, when a value is too slow. |

`Shift` and `Expire` always use the system clock. Use `Delay` and `Timeout` when a test needs a `TimeProvider`.
