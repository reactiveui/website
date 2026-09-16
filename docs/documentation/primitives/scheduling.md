---
Order: 11
---
# Sequencers and scheduling

A **sequencer** decides where and when a piece of work runs. It might run the work straight away, on the thread
pool, on the UI thread, or after a delay. Operators that need a thread or a timer, such as `WitnessOn`, `Shift`
and `Calm`, take a sequencer so you can choose.

A sequencer implements `ISequencer`, in `ReactiveUI.Primitives.Concurrency`.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Concurrency;
```

## Your first scheduled work

You want to save a file 200 ms after the user stops typing, and cancel the save if they type again.

**1. Pick a sequencer.** `Sequencer.Default` runs work on the thread pool.

```csharp
ISequencer sequencer = Sequencer.Default;
```

**2. Schedule the work.** `Schedule` with a `TimeSpan` runs your lambda after that delay. It hands back an
`IDisposable`.

```csharp
IDisposable pending = sequencer.Schedule(TimeSpan.FromMilliseconds(200), () => Console.WriteLine("saved"));
```

Output, 200 ms later:

```text
saved
```

**3. Dispose to cancel.** Disposing before the delay ends stops the work from running.

```csharp
IDisposable cancelled = sequencer.Schedule(TimeSpan.FromMilliseconds(200), () => Console.WriteLine("never printed"));
cancelled.Dispose();
```

Nothing is printed. Disposing after the work has run does nothing.

## The built-in sequencers

`Sequencer` holds the three you use most:

| Property | Type | Runs work |
|---|---|---|
| `Sequencer.Immediate` | `ImmediateSequencer` | Straight away, on the calling thread. |
| `Sequencer.CurrentThread` | `CurrentThreadSequencer` | On the calling thread, after the work already running there finishes. |
| `Sequencer.Default` | `TaskPoolSequencer` | On the thread pool. |

### `ImmediateSequencer`

`Sequencer.Immediate` runs your work inside the `Schedule` call. `Schedule` returns only after the work has
finished.

```csharp
Console.WriteLine("before");
Sequencer.Immediate.Schedule(() => Console.WriteLine("work"));
Console.WriteLine("after");
```

Output:

```text
before
work
after
```

Work scheduled from inside other work runs at once too, in the middle of the outer work:

```csharp
Sequencer.Immediate.Schedule(() =>
{
    Console.WriteLine("outer start");
    Sequencer.Immediate.Schedule(() => Console.WriteLine("inner"));
    Console.WriteLine("outer end");
});
```

Output:

```text
outer start
inner
outer end
```

> [!WARNING]
> Given a delay, `Sequencer.Immediate` **blocks** the calling thread for that long, then runs the work. Never
> give it a delay on the UI thread.

`ImmediateSequencer.Instance` is the same object as `Sequencer.Immediate`. The static
`ImmediateSequencer.Schedule(action)` runs an action straight away without needing the instance.

### `CurrentThreadSequencer`

`Sequencer.CurrentThread` also runs work on the calling thread. The difference shows when work schedules more
work. The inner work waits in a queue until the outer work finishes. This queue is called a **trampoline**.

```csharp
Sequencer.CurrentThread.Schedule(() =>
{
    Console.WriteLine("outer start");
    Sequencer.CurrentThread.Schedule(() => Console.WriteLine("inner"));
    Console.WriteLine("outer end");
});
```

Output:

```text
outer start
outer end
inner
```

The trampoline keeps work in order, and stops work that schedules itself again from growing the call stack
until it overflows.

`CurrentThreadSequencer.IsScheduleRequired` tells you whether the trampoline is already running on this thread.
It is `true` outside any scheduled work, and `false` inside it.

```csharp
Console.WriteLine(CurrentThreadSequencer.IsScheduleRequired);   // True
Sequencer.CurrentThread.Schedule(() =>
    Console.WriteLine(CurrentThreadSequencer.IsScheduleRequired)); // False
```

Like `Immediate`, it blocks the calling thread when you give it a delay. `CurrentThreadSequencer.Instance` is
the same object as `Sequencer.CurrentThread`.

### `TaskPoolSequencer`

`Sequencer.Default` runs work on the thread pool, through a `TaskFactory`. `Schedule` returns straight away.

```csharp
Sequencer.Default.Schedule(() =>
    Console.WriteLine($"thread pool: {Thread.CurrentThread.IsThreadPoolThread}"));
```

Output:

```text
thread pool: True
```

`TaskPoolSequencer.Default` and `TaskPoolSequencer.Instance` are the same object as `Sequencer.Default`.

Build your own with a `TaskFactory` to control how tasks start. An exception thrown by your work goes to
`UnhandledExceptionHandler`, if you set one:

```csharp
var sequencer = new TaskPoolSequencer(new TaskFactory());
sequencer.UnhandledExceptionHandler = error => Console.WriteLine($"work failed: {error.Message}");

sequencer.Schedule(() => throw new InvalidOperationException("disk full"));
```

Output:

```text
work failed: disk full
```

### `ThreadPoolSequencer`

`ThreadPoolSequencer.Instance` queues work straight onto the .NET thread pool, without creating a `Task`.

```csharp
ThreadPoolSequencer.Instance.Schedule(() => Console.WriteLine("ran on the thread pool"));
```

### `SynchronizationContextSequencer`

A `SynchronizationContext` is how a UI framework lets other threads post work back to its UI thread. WPF,
WinForms, WinUI, MAUI and Blazor each install one on their UI thread.

`SynchronizationContextSequencer` posts your work through a context. `SynchronizationContextSequencer.Current`
builds one from the context on the calling thread, so call it on the UI thread:

```csharp
// on the UI thread
var ui = SynchronizationContextSequencer.Current;

// later, from any thread
ui.Schedule(() => statusLabel.Text = "done");
```

On a thread with no context, `Current` throws an `InvalidOperationException` with the message
`There is no current synchronization context.` You can also pass a context in yourself:
`new SynchronizationContextSequencer(context)`. The `Context` property hands it back.

### `WasmSequencer`

`WasmSequencer.Default` is for WebAssembly apps, which run on a single thread in the browser.

```csharp
WasmSequencer.Default.Schedule(() => Console.WriteLine("ran"));
```

### Which one to pick

| You want | Use |
|---|---|
| Work to happen inline, now | `Sequencer.Immediate` |
| Work on the calling thread, in order, without deep call stacks | `Sequencer.CurrentThread` |
| Work off the calling thread | `Sequencer.Default` |
| Work on the UI thread | The UI sequencer for your platform. See [UI platforms](platforms.md). |
| Tests that control time | `VirtualClock`, below |

## Scheduling work yourself

Every sequencer has these `Schedule` forms. The examples use a `VirtualClock`, described below, so you can see
exactly when work runs.

### Now, after a delay, or at a set time

```csharp
var clock = new VirtualClock();
var start = clock.Now;

clock.Schedule(() => Console.WriteLine("as soon as possible"));
clock.Schedule(TimeSpan.FromSeconds(2), () => Console.WriteLine("after 2 seconds"));
clock.Schedule(start + TimeSpan.FromSeconds(5), () => Console.WriteLine("at 5 seconds"));

clock.AdvanceBy(TimeSpan.FromSeconds(10));
```

Output:

```text
as soon as possible
after 2 seconds
at 5 seconds
```

The `DateTimeOffset` form works out the delay from the sequencer's `Now`. A time in the past runs as soon as
possible.

### Passing state

Each form also takes a state value that is handed to your lambda. That lets you mark the lambda `static`. See
[mark lambdas static](best-practices.md#mark-lambdas-static).

```csharp
clock.Schedule("report.pdf", static name => Console.WriteLine($"saving {name}"));
clock.Schedule("report.pdf", TimeSpan.FromSeconds(1), static name => Console.WriteLine($"uploading {name}"));
```

`ScheduleAction(state, action)` does the same as `Schedule(state, action)`, with a name that cannot be confused
with the other overloads.

A further set of overloads takes a `Func<ISequencer, TState, IDisposable>`. Your lambda receives the sequencer
too, so it can schedule follow-up work, and returns an `IDisposable` for that work:

```csharp
using ReactiveUI.Primitives.Disposables;

clock.Schedule(3, TimeSpan.FromSeconds(1), static (sequencer, left) =>
{
    Console.WriteLine($"{left} left");
    return EmptyDisposable.Instance;
});
```

There is also `Schedule(state, dueTimestamp, action)`, which takes a `long` timestamp. See
[`Now` and `Timestamp`](#now-and-timestamp).

### Repeating work

`Schedule` with an `Action<Action>` hands your lambda an action that schedules it again. Call it to run once
more.

```csharp
var attempt = 0;

clock.Schedule(again =>
{
    attempt++;
    Console.WriteLine($"attempt {attempt}");

    if (attempt < 3)
    {
        again();
    }
});
```

Output:

```text
attempt 1
attempt 2
attempt 3
```

Disposing what `Schedule` returned stops any repeat that has not run yet.

### `ScheduleSafe`

`ScheduleSafe` accepts a sequencer that may be `null`. With a sequencer, it schedules as normal. With `null`, it
runs the work straight away on the calling thread.

```csharp
using ReactiveUI.Primitives.Extensions;

ISequencer? maybe = null;
maybe.ScheduleSafe(() => Console.WriteLine("ran straight away"));
```

Given a delay and `null`, it blocks the calling thread for the delay, then runs the work.

### `Sequencer.Normalize`

`Sequencer.Normalize` turns a negative `TimeSpan` into `TimeSpan.Zero`, and leaves any other value alone. Use it
before scheduling a delay you worked out yourself, which may have fallen into the past.

```csharp
Sequencer.Normalize(TimeSpan.FromSeconds(-5));   // 00:00:00
Sequencer.Normalize(TimeSpan.FromSeconds(2));    // 00:00:02
```

## Now and Timestamp

Every sequencer has two clocks:

- `Now` is the sequencer's idea of the current date and time, as a `DateTimeOffset`.
- `Timestamp` is a `long` that only ever goes up. The real sequencers use `Stopwatch.GetTimestamp()`, so the
  difference between two timestamps is safe to measure, even when the system clock changes.

```csharp
using System.Diagnostics;

long before = sequencer.Timestamp;
DoWork();
TimeSpan elapsed = Stopwatch.GetElapsedTime(before, sequencer.Timestamp);
```

## Writing your own sequencer

`ISequencer` has four members: `Now`, `Timestamp`, `Schedule(IWorkItem)` and
`Schedule(IWorkItem, long dueTimestamp)`. Every `Schedule` overload on this page is an extension method built on
those two. An `IWorkItem` is one piece of work, with a single `Execute` method.

```csharp
Sequencer.Immediate.Schedule(new SaveWork());   // prints saving

sealed class SaveWork : IWorkItem
{
    public void Execute() => Console.WriteLine("saving");
}
```

`Schedule(IWorkItem)` returns nothing. Use the `Action` forms when you need to cancel.

Implement `ISequencer` only when none of the built-in sequencers fits, such as a game loop that runs work once
per frame.

## Testing with a virtual clock

Code that waits is slow to test with real time, and the result can change from run to run. A `VirtualClock` is
a sequencer whose time only moves when you move it. Work scheduled on it runs when the clock reaches its due
time, on your test's thread.

### `VirtualClock`

```csharp
var clock = new VirtualClock(new DateTimeOffset(2026, 1, 1, 9, 0, 0, TimeSpan.Zero));

clock.Schedule(TimeSpan.FromMinutes(30), () => Console.WriteLine($"reminder at {clock.Now:HH:mm}"));

clock.AdvanceBy(TimeSpan.FromMinutes(29));
Console.WriteLine($"now {clock.Now:HH:mm}");

clock.AdvanceTo(new DateTimeOffset(2026, 1, 1, 10, 0, 0, TimeSpan.Zero));
Console.WriteLine($"now {clock.Now:HH:mm}");
```

Output:

```text
now 09:29
reminder at 09:30
now 10:00
```

- `new VirtualClock()` starts at `DateTimeOffset.MinValue`. Pass a `DateTimeOffset` to start somewhere readable.
- `AdvanceBy` moves the clock forward by a `TimeSpan`, running each piece of work that falls due, in time order.
- `AdvanceTo` does the same, up to a `DateTimeOffset`.
- `Now` and `Clock` both give the current virtual time.
- Moving backwards throws an `ArgumentOutOfRangeException`.

Pass the clock to any operator that takes a sequencer:

```csharp
using ReactiveUI.Primitives.Signals;

Signal.Emit("ping")
      .Shift(TimeSpan.FromSeconds(5), clock)
      .Subscribe(x => Console.WriteLine($"{x} at {clock.Now.Second}s"));

clock.AdvanceBy(TimeSpan.FromSeconds(5));   // ping at 5s
```

The [time page](time.md) tests its operators this way.

### `Sleep`

`Sleep` moves the clock forward **without** running any work. Work that fell due in the gap runs on the next
`AdvanceBy`, `AdvanceTo` or `Start`, at the later time.

```csharp
var clock = new VirtualClock(new DateTimeOffset(2026, 1, 1, 9, 0, 0, TimeSpan.Zero));
clock.Schedule(TimeSpan.FromSeconds(10), () => Console.WriteLine($"ran at {clock.Now:HH:mm:ss}"));

clock.Sleep(TimeSpan.FromSeconds(20));
Console.WriteLine($"slept to {clock.Now:HH:mm:ss}");

clock.AdvanceBy(TimeSpan.FromSeconds(1));
```

Output:

```text
slept to 09:00:20
ran at 09:00:20
```

Use it to test how your code copes when work runs late.

### `Start` and `Stop`

`Start` runs **every** scheduled piece of work, moving the clock to each one's due time, until none are left.
`Stop`, called from inside some work, makes `Start` return after that work. `IsEnabled` is `true` while `Start`
is running.

```csharp
var clock = new VirtualClock();

clock.Schedule(TimeSpan.FromMinutes(1), () =>
{
    Console.WriteLine("1 minute, stopping");
    clock.Stop();
});
clock.Schedule(TimeSpan.FromHours(1), () => Console.WriteLine("1 hour"));

clock.Start();
Console.WriteLine($"stopped, running: {clock.IsEnabled}");
clock.Start();
```

Output:

```text
1 minute, stopping
stopped, running: False
1 hour
```

> [!WARNING]
> `Start` never returns while work keeps scheduling more work, such as a timer that repeats for ever. Use
> `AdvanceBy` for those.

### `StartStopwatch`

`StartStopwatch` hands you an `IStopwatch` that measures virtual time. Its `Elapsed` property is a `TimeSpan`.

```csharp
IStopwatch watch = clock.StartStopwatch();
clock.AdvanceBy(TimeSpan.FromSeconds(90));
Console.WriteLine(watch.Elapsed);   // 00:01:30
```

### `ScheduleAbsolute` and `ScheduleRelative`

These two schedule work at a virtual time. `ScheduleAbsolute` takes the time to run at. `ScheduleRelative`
takes a delay from the current virtual time.

```csharp
var clock = new VirtualClock();
var start = clock.Now;

clock.ScheduleRelative(TimeSpan.FromSeconds(2), () => Console.WriteLine("2 seconds from now"));
clock.ScheduleAbsolute(start + TimeSpan.FromSeconds(1), () => Console.WriteLine("at 1 second"));

clock.AdvanceBy(TimeSpan.FromSeconds(3));
```

Output:

```text
at 1 second
2 seconds from now
```

Both also take a state value and a `Func<ISequencer, TState, IDisposable>`, like `Schedule`.

### `VirtualTimeSequencer<TAbsolute, TRelative>`

`VirtualClock` counts time with `DateTimeOffset` and `TimeSpan`. `VirtualTimeSequencer<TAbsolute, TRelative>`
lets you count it any way you like, such as turns in a game. You give it five things:

1. The starting time.
2. An `IComparer<TAbsolute>` to put times in order.
3. A lambda that adds a relative time to an absolute time.
4. A lambda that turns an absolute time into a `DateTimeOffset`, for `Now`.
5. A lambda that turns a `TimeSpan` into a relative time, for the `TimeSpan` overloads.

```csharp
var turns = new VirtualTimeSequencer<long, long>(
    0L,
    Comparer<long>.Default,
    static (turn, add) => turn + add,
    static turn => DateTimeOffset.UnixEpoch.AddMinutes(turn),
    static span => (long)span.TotalMinutes);

turns.ScheduleRelative(3L, () => Console.WriteLine($"event on turn {turns.Clock}"));

turns.AdvanceBy(2L);
Console.WriteLine($"turn {turns.Clock}");
turns.AdvanceBy(1L);
```

Output:

```text
turn 2
event on turn 3
```

It has the same members as `VirtualClock`: `Clock`, `AdvanceBy`, `AdvanceTo`, `Sleep`, `Start`, `Stop`,
`IsEnabled`, `StartStopwatch`, `ScheduleAbsolute` and `ScheduleRelative`, all in your own time types.

## Every scheduling member at a glance

| Member | What it does |
|---|---|
| `Sequencer.Immediate` | Runs work inline, now. |
| `Sequencer.CurrentThread` | Runs work on the calling thread, queued behind work already running. |
| `CurrentThreadSequencer.IsScheduleRequired` | Whether the calling thread is outside any queued work. |
| `Sequencer.Default` | Runs work on the thread pool through tasks. |
| `new TaskPoolSequencer(factory)` | Runs work through your `TaskFactory`. |
| `TaskPoolSequencer.UnhandledExceptionHandler` | Receives exceptions thrown by scheduled work. |
| `ThreadPoolSequencer.Instance` | Queues work straight onto the thread pool. |
| `SynchronizationContextSequencer` | Posts work through a `SynchronizationContext`. |
| `WasmSequencer.Default` | Runs work in a WebAssembly app. |
| UI sequencers | Run work on a platform's UI thread. See [UI platforms](platforms.md). |
| `Schedule(action)` | Runs work as soon as possible. |
| `Schedule(TimeSpan, action)` | Runs work after a delay. |
| `Schedule(DateTimeOffset, action)` | Runs work at a set time. |
| `Schedule(state, ...)` | The same, passing a state value. |
| `Schedule(Action<Action>)` | Runs work that can schedule itself again. |
| `ScheduleAction(state, action)` | Runs work with state, as soon as possible. |
| `ScheduleSafe` | Schedules on a sequencer that may be `null`. |
| `Sequencer.Normalize` | Turns a negative `TimeSpan` into zero. |
| `VirtualClock` | A sequencer whose time you move by hand. |
| `VirtualTimeSequencer<TAbsolute, TRelative>` | The same, with your own time types. |

## The two package flavours

Every sequencer ships twice. `ReactiveUI.Primitives` puts them under `ReactiveUI.Primitives.Concurrency`.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and puts them under
`ReactiveUI.Primitives.Reactive.Concurrency`. They behave the same in both.
