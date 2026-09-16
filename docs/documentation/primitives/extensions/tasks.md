---
Order: 4
---
# Tasks and async work

These helpers mix streams with `Task` and `async` code. They run an async method for each value, cap how much runs at
once, and turn a stream into a `Task` you can `await`.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Extensions;
using ReactiveUI.Primitives.Signals;
```

Each helper answers two questions differently: **how many** async calls run at once, and **which results** go out.

| Helper | Calls at once | Results sent |
|---|---|---|
| `SelectAsync` / `SelectAsyncSequential` | One | Every result, in source order |
| `SelectAsyncConcurrent` | Up to a limit | Every result, as each call finishes |
| `SelectLatestAsync` | One that counts | Only the result for the newest value |
| `DropIfBusy` | One | The value, after the call; values that arrive during a call are dropped |

The examples call this method, which waits and then returns a result:

```csharp
static async Task<string> LoadAsync(int id)
{
    await Task.Delay(id == 1 ? 300 : 50);
    return $"item {id}";
}
```

## Running async work for each value

### `SelectAsync` and `SelectAsyncSequential`

`SelectAsync` runs an async method for each value, one call at a time. It waits for a call to finish before starting
the next, so results go out in the same order as the values. `SelectAsyncSequential` is the same helper under a second
name.

```csharp
var ids = new Signal<int>();

ids.SelectAsync(static id => LoadAsync(id))
   .Subscribe(static item => Console.WriteLine(item));

ids.OnNext(1);   // takes 300 ms
ids.OnNext(2);   // takes 50 ms
```

Output:

| Time | Output |
|---|---|
| 300 ms | `item 1` |
| 350 ms | `item 2` |

`item 2` was quicker to load, but it still waited for `item 1`.

An overload takes a lambda with a `CancellationToken`, so you can pass it to methods that ask for one. The token
cancels when you dispose the subscription, which stops a call that is still running.

### `SelectAsyncConcurrent`

`SelectAsyncConcurrent` runs up to the number of calls you give at the same time. Each result goes out as soon as its
call finishes, so results can arrive in a different order from the values.

```csharp
var ids = new Signal<int>();

ids.SelectAsyncConcurrent(static id => LoadAsync(id), maxConcurrency: 2)
   .Subscribe(static item => Console.WriteLine(item));

ids.OnNext(1);   // takes 300 ms
ids.OnNext(2);   // takes 50 ms
ids.OnNext(3);   // waits for a free slot, then takes 50 ms
```

Output:

| Time | Output |
|---|---|
| 50 ms | `item 2` |
| 100 ms | `item 3` |
| 300 ms | `item 1` |

A limit protects what the calls use, such as a server or a database connection pool. Without one, a burst of values
starts a burst of calls.

### `SelectLatestAsync`

`SelectLatestAsync` cares only about the newest value. When a new value arrives while a call is still running, the
result of the older call is thrown away. A search box is the usual fit: results for an old search term must never
overwrite results for the new one.

```csharp
var ids = new Signal<int>();

ids.SelectLatestAsync(static id => LoadAsync(id))
   .Subscribe(static item => Console.WriteLine(item));

ids.OnNext(1);   // at 0 ms
ids.OnNext(2);   // at 20 ms, before item 1 has loaded
```

Output:

```text
item 2
```

### `DropIfBusy`

`DropIfBusy` runs an async method with each value, then passes the value on. While a call is running, any value that
arrives is dropped, not queued. It suits a Refresh button: a second press during a refresh does nothing.

```csharp
var refreshPresses = new Signal<int>();

refreshPresses.DropIfBusy(static async press =>
              {
                  Console.WriteLine($"refreshing for press {press}");
                  await Task.Delay(100);
              })
              .Subscribe(static press => Console.WriteLine($"refreshed for press {press}"));

refreshPresses.OnNext(1);
refreshPresses.OnNext(2);   // dropped: press 1 is still refreshing
```

Output:

```text
refreshing for press 1
refreshed for press 1
```

If the method throws, the stream fails with that exception.

### `WithLimitedConcurrency`

`WithLimitedConcurrency` works on a collection of tasks, not a stream. It runs at most the number you give at once, and
sends each result as its task finishes. Build the tasks with an iterator method, as below, so each one starts only
when a slot is free. A list of tasks that have all started runs them all at once, whatever the limit.

```csharp
static IEnumerable<Task<string>> LoadAll()
{
    for (var id = 1; id <= 4; id++)
    {
        yield return LoadAsync(id);
    }
}

LoadAll().WithLimitedConcurrency(2)
         .Subscribe(static item => Console.WriteLine(item), static () => Console.WriteLine("completed"));
```

Output:

```text
item 2
item 3
item 4
item 1
completed
```

The stream completes once every task has finished. A task that fails or is cancelled fails the stream.

## Subscribing with async code

### `SubscribeAsync` and `SubscribeSynchronous`

A plain `Subscribe` with an `async` lambda starts each call and moves on at once, so calls overlap and an exception
from the lambda never reaches your error callback. `SubscribeAsync` takes a lambda that returns a `ValueTask`. It queues the values and runs your
lambda on one value at a time. The code that sends values is not held up while it waits. `SubscribeSynchronous` is the
same helper under a second name.

```csharp
var messages = new Signal<string>();

messages.SubscribeAsync(
    static async message =>
    {
        await Task.Delay(100);
        Console.WriteLine($"saved {message}");
    },
    static () => Console.WriteLine("all saved"));

messages.OnNext("a");
messages.OnNext("b");
messages.OnCompleted();
Console.WriteLine("sender carried on");
```

Output:

```text
sender carried on
saved a
saved b
all saved
```

The completion callback runs after the queue is empty. If your lambda throws, the exception goes to the error callback
of the overloads that take one.

### `SynchronizeAsync` and `SynchronizeSynchronous`

These send each value paired with a handle, as a tuple of `Value` and `Sync`. Dispose `Sync` when you have finished
with the value. `SynchronizeSynchronous` is the same helper under a second name.

```csharp
var jobs = new Signal<int>();

jobs.SynchronizeAsync()
    .Subscribe(static job =>
    {
        Console.WriteLine($"job {job.Value}");
        job.Sync.Dispose();
    });

jobs.OnNext(1);
```

Output:

```text
job 1
```

Values reach your subscriber as they arrive, whether or not you have disposed the handle for the value before.

## Turning a stream into a task

### `ToHotTask` and `ToHotValueTask`

`ToHotTask` subscribes straight away and hands back a `Task<T>` for the **first** value. Because it subscribes at the
call, it does not miss a value sent before you `await` the task. The task fails if the stream fails, and fails with
`InvalidOperationException` if the stream completes with no value.

```csharp
var replies = new Signal<string>();

Task<string> firstReply = replies.ToHotTask();

replies.OnNext("hello");
replies.OnNext("again");

Console.WriteLine(await firstReply);
```

Output:

```text
hello
```

`ToHotValueTask` does the same and hands back a `ValueTask<T>`, which allocates less. Await a `ValueTask<T>` exactly
once, and never read it twice.

Call either before the value is sent. A value sent before the call is gone, and the task waits for the next one.

[`FirstAsync`](../aggregation.md) also gives you a task for the first value.

## Every helper on this page at a glance

| Helper | Second name | What it does |
|---|---|---|
| `SelectAsync` | `SelectAsyncSequential` | Runs an async method for each value, one at a time, in order. |
| `SelectAsyncConcurrent` | — | Runs an async method for each value, up to a limit at once. |
| `SelectLatestAsync` | — | Runs an async method for each value, and keeps only the newest result. |
| `DropIfBusy` | — | Runs an async method, and drops values that arrive while it runs. |
| `WithLimitedConcurrency` | — | Runs a collection of tasks, up to a limit at once. |
| `SubscribeAsync` | `SubscribeSynchronous` | Subscribes with an async lambda that runs on one value at a time. |
| `SynchronizeAsync` | `SynchronizeSynchronous` | Sends each value with a handle to dispose when you finish. |
| `ToHotTask` | — | Subscribes now and gives a `Task<T>` for the first value. |
| `ToHotValueTask` | — | The same, as a `ValueTask<T>`. |

## The types behind these helpers

`SelectAsyncSequentialObservable<T, TResult>`, `SelectAsyncConcurrentObservable<T, TResult>`,
`SelectLatestAsyncObservable<T, TResult>`, `DropIfBusyObservable<T>`, `SubscribeAsyncObservable<T>` and
`SynchronizeAsyncObservable<T>` are public classes in `ReactiveUI.Primitives.Extensions.Operators`.

In `ReactiveUI.Primitives.Extensions`:

- `ConcurrencyLimiter<T>` is the stream behind `WithLimitedConcurrency`. Its constructor takes the tasks and the limit.
- `FirstAsTaskHelper.FirstAsTask(signal)` and `FirstAsValueTaskHelper<T>.FirstAsValueTask(signal)` are the static
  methods behind `ToHotTask` and `ToHotValueTask`.
- `Continuation` hands an item to a witness with a release handle, and gives back a `Task` that completes when the
  handle is disposed. Its `Lock` and `LockValueTask` methods drop an item while an earlier handle is still held.
