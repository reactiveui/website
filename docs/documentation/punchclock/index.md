---
Order: 15
---
# Schedule work with Punchclock

When a screen opens, a profile request may need to run ahead of background downloads.
`OperationQueue` limits how many operations run at once. When a slot opens, the queue
considers unkeyed work first. Within each group, higher priorities run first.
Work already running keeps its slot until it finishes.

## Queue a task

Install `Punchclock` and import `Punchclock`. Keep the queue alive while its jobs run.
Here two tasks can run at once, and operations with the key `"profile"` run one at a time.
Pass the token to the HTTP call as well so an active download can stop.

```csharp
using var queue = new OperationQueue(maximumConcurrent: 2);
var profile = await queue.Enqueue(10, "profile",
    () => client.GetStringAsync(profileUri, cancellationToken), cancellationToken);
```

Pass a delegate that starts the operation. Creating the download task before calling
`Enqueue` starts it outside the queue. Use the overload without a key for independent
work. Both forms accept delegates that return `Task` or `Task<T>`.

## Pause and drain a queue

Use `PauseQueue()` to hold waiting work until a batch is ready. Running operations
continue. Every caller that pauses gets a disposable handle. Disposing the last handle
allows the next operation to start.

```csharp
Task<string> foreground;
Task<string> background;
using (queue.PauseQueue())
{
    background = queue.Enqueue(0, () => client.GetStringAsync(indexUri, cancellationToken));
    foreground = queue.Enqueue(10, () => client.GetStringAsync(profileUri, cancellationToken));
}
await Task.WhenAll(foreground, background);
```

`SetMaximumConcurrent` changes the number of slots for future starts. The default
constructor creates four slots. To vary the order of equal-priority jobs on different
keys, set `randomizeEqualPriority` and supply a seed for repeatable scheduling.
A constructor that takes a sequencer lets a test control when work runs.

When the owner closes, call `ShutdownQueue()` and await its completion before disposing
the queue. This lets pending and active jobs finish.

## Queue an observable

An observable reports values to a subscriber. `EnqueueObservableOperation` accepts a
factory for the observable and shares the task queue's priorities and keys. An optional
cancellation observable lets a screen withdraw a queued preview when the screen closes.
This example uses `Signal` from `ReactiveUI.Primitives.Signals` and the subscription
helpers from `ReactiveUI.Primitives`.

```csharp
using var closed = new Signal<bool>();
using var subscription = queue.EnqueueObservableOperation(
    0, "preview", closed, () => LoadPreview()).Subscribe(ShowPreview);
closed.OnNext(true);
```

The `Punchclock.Reactive` package exposes the same queue operations in
`Punchclock.Reactive`. Its scheduler type is `System.Reactive.Concurrency.IScheduler`
and its shutdown signal uses `System.Reactive.Unit`.

The example runs both package variants against the same checks for priority, keyed edits,
cancellation, observable results and shutdown. [Run the queue examples](https://github.com/reactiveui/punchclock/tree/main/src/examples).
