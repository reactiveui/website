---
Order: 10
---
# Signals you push values into

Most streams on these pages produce their own values. A **signal** is different: you push values into it
yourself, and it passes each one on to everyone subscribed. It is a stream and a subscriber at the same time.

You push a value with `OnNext`, end it successfully with `OnCompleted`, or end it with an error with `OnError`.
Those are the same three methods a subscriber has, which is why a signal counts as both. Other libraries call
this kind of object a **subject**.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;
```

## Your first signal

You are writing a chat window. New messages arrive from the network, and two parts of your app need them: the
screen and a log.

**1. Create the signal.**

```csharp
var messages = new Signal<string>();
```

**2. Subscribe both listeners.** The log also watches for the chat closing.

```csharp
IDisposable screen = messages.Subscribe(m => Console.WriteLine($"screen: {m}"));

IDisposable log = messages.Subscribe(
    m => Console.WriteLine($"log:    {m}"),
    () => Console.WriteLine("log:    chat closed"));
```

**3. Push messages in.** Each one reaches both listeners, in the order they subscribed.

```csharp
messages.OnNext("hello");
messages.OnNext("how are you?");
```

**4. Let one listener go.** Disposing a subscription removes that listener. The rest carry on.

```csharp
screen.Dispose();
messages.OnNext("anyone there?");
```

**5. Close the chat.**

```csharp
messages.OnCompleted();
```

Output, for all five steps:

```text
screen: hello
log:    hello
screen: how are you?
log:    how are you?
log:    anyone there?
log:    chat closed
```

After `screen.Dispose()`, only the log received `anyone there?`.

## Choosing a signal

Every signal on this page lets you push values in. They differ in what a subscriber gets when it joins late,
and in how they deliver.

| Signal | A late subscriber gets | Use it for |
|---|---|---|
| `Signal<T>` | Only values sent after it joins. | Events: clicks, messages, notifications. |
| `BehaviorSignal<T>` | The current value, then new ones. | A value that always has a current setting. |
| `StateSignal<T>` | The current value, then new ones. You can also set `Value` directly. | State you read and write, such as a score. |
| `ReplaySignal<T>` | Past values it remembers, then new ones. | History a late screen should catch up on. |
| `CurrentValueSignal<T>` | The value read at the moment it joins, then changes. | A value that lives on some other object. |
| `SerializedSignal<T>` | Only values sent after it joins. | Values pushed from many threads at once. |
| `ScheduledSignal<T>` | Only values sent after it joins, delivered through a sequencer. | Handing values to a particular thread. |
| `DelayableNotificationSignal<T>` | Values held back while paused. | Batching changes while an edit is in progress. |
| `AsyncSignal<T>` | Only the last value, when it completes. | A single result you want to `await`. |
| `PrioritySemaphoreSignal<T>` | Values let through a few at a time, lowest first. | Limiting how much work runs at once. |

## `Signal<T>`

`Signal<T>` is the plain signal. It passes each value to whoever is subscribed right then. It remembers
nothing.

A subscriber that joins after the signal has completed gets only the completion. It never sees the earlier
values. The same goes for an error.

```csharp
var messages = new Signal<string>();
messages.OnNext("hello");
messages.OnCompleted();

messages.Subscribe(
    m => Console.WriteLine($"late: {m}"),
    () => Console.WriteLine("late: already closed"));   // prints late: already closed
```

`HasObservers` tells you whether anyone is subscribed right now. `SubscribeAction` subscribes a value callback
only. Calling `Subscribe` with just a value callback uses it for you.

```csharp
var messages = new Signal<string>();
Console.WriteLine(messages.HasObservers);   // False

IDisposable sub = messages.SubscribeAction(m => Console.WriteLine(m));
Console.WriteLine(messages.HasObservers);   // True

sub.Dispose();
Console.WriteLine(messages.HasObservers);   // False
```

When you have finished with a signal, dispose it. After that, pushing a value into it or subscribing to it
throws an `ObjectDisposedException`, and `IsDisposed` is `true`.

A plain `Signal<T>` does not stop two threads from pushing at the same moment, so your callback can run twice
at once. If values can arrive from several threads, use `SerializedSignal<T>` below.

## Signals that remember

### `BehaviorSignal<T>`

`BehaviorSignal<T>` always holds a current value. You give it a starting value. A new subscriber receives the
current value straight away, then every value after it.

```csharp
var volume = new BehaviorSignal<int>(50);

volume.Subscribe(v => Console.WriteLine($"first:  {v}"));
volume.OnNext(70);
volume.Subscribe(v => Console.WriteLine($"second: {v}"));

Console.WriteLine($"Value is {volume.Value}");
```

Output:

```text
first:  50
first:  70
second: 70
Value is 70
```

`second` joined late, and still got the current volume of `70`.

Read the current value any time with `Value`, or with `TryGetValue`, which tells you whether there is one.
Pushing the same value again sends it again. `BehaviorSignal<T>` does not skip repeats.

> [!NOTE]
> Once a `BehaviorSignal<T>` has completed, a new subscriber gets only the completion, not the current value.
> `Value` and `TryGetValue` still give you the last value.

### `StateSignal<T>`

`StateSignal<T>` works like `BehaviorSignal<T>`, and adds a `Value` you can **set**. Setting it pushes the new
value to every subscriber.

```csharp
var score = new StateSignal<int>(0);

score.Subscribe(s => Console.WriteLine($"score {s}"));

score.Value = 10;
score.Value += 5;

Console.WriteLine($"Value is {score.Value}");
```

Output:

```text
score 0
score 10
score 15
Value is 15
```

`Changed` is a stream of the value, including the current value when you subscribe. Setting the same value
twice sends it twice. It does not skip repeats.

`Refresh` sends the current value again without changing it. Use it when the value is an object you changed
inside, and subscribers need to know.

```csharp
var score = new StateSignal<int>(3);
score.Subscribe(s => Console.WriteLine($"score {s}"));

score.Refresh();   // prints score 3 again
```

`ToReadOnlyState` on a `StateSignal<T>` gives you a `ProjectedReadOnlyState`: a read-only value worked out from
the state, which stays up to date as the state changes.

```csharp
var score = new StateSignal<int>(0);
ProjectedReadOnlyState<int, string> label = score.ToReadOnlyState(s => $"Score: {s}");

score.Value = 7;
Console.WriteLine(label.Value);   // Score: 7
```

`ProjectedReadOnlyState<int, string>.Create(score, s => $"Score: {s}")` builds the same thing. The version of
`ToReadOnlyState` for any stream is on the [sharing page](sharing.md).

### `ReplaySignal<T>`

`ReplaySignal<T>` remembers values it has sent, and replays them to a subscriber that joins late. With no
arguments it remembers everything. Give it an `int` to remember only that many of the most recent values.

```csharp
var history = new ReplaySignal<string>(2);

history.OnNext("one");
history.OnNext("two");
history.OnNext("three");

history.Subscribe(m => Console.WriteLine($"late: {m}"));
```

Output:

```text
late: two
late: three
```

Give it a `TimeSpan` to forget values older than that. It also takes a sequencer, to decide the clock it uses
and the thread it replays on. Here a `VirtualClock`, from the [time page](time.md), moves time on by hand:

```csharp
using ReactiveUI.Primitives.Concurrency;

var clock = new VirtualClock();
var recent = new ReplaySignal<string>(TimeSpan.FromSeconds(10), clock);

recent.OnNext("old");
clock.AdvanceBy(TimeSpan.FromSeconds(15));
recent.OnNext("new");

recent.Subscribe(m => Console.WriteLine($"late: {m}"));
clock.AdvanceBy(TimeSpan.FromTicks(1));   // prints late: new
```

`old` was 15 seconds old, past the 10-second limit, so it was forgotten.

Unlike `Signal<T>` and `BehaviorSignal<T>`, a `ReplaySignal<T>` still replays its values after it has completed,
then sends the completion.

### `CurrentValueSubject<T>`

`CurrentValueSubject<T>`, in `ReactiveUI.Primitives.Extensions`, holds a current value like `BehaviorSignal<T>`.
A late subscriber gets the current value first. Its `AsObservable` gives you a read-only view that cannot be used
to push values.

```csharp
using ReactiveUI.Primitives.Extensions;

var subject = new CurrentValueSubject<int>(1);
subject.OnNext(2);

subject.Subscribe(v => Console.WriteLine($"late {v}"));   // prints late 2
IObservable<int> readOnly = subject.AsObservable();
```

## Reading a value that lives somewhere else

### `CurrentValueSignal<T>`

Sometimes the value you want to watch already lives on another object, such as a property on a view model.
`CurrentValueSignal<T>` turns it into a stream. You give it two things: a way to **read** the value, and a way to
hear that it **changed**.

Each subscriber reads the value when it subscribes, then again every time you are told it changed.

```csharp
using System.ComponentModel;
using ReactiveUI.Primitives.Disposables;

var person = new Person { Name = "Ada" };

IObservable<string> names = new CurrentValueSignal<string>(
    () => person.Name,
    onChanged =>
    {
        void Handler(object? sender, PropertyChangedEventArgs e) => onChanged();
        person.PropertyChanged += Handler;
        return new ActionDisposable(() => person.PropertyChanged -= Handler);
    });

IDisposable subscription = names.Subscribe(name => Console.WriteLine(name));

person.Name = "Grace";

subscription.Dispose();
person.Name = "Alan";
```

Output:

```text
Ada
Grace
```

The second argument attaches to the `PropertyChanged` event and returns a disposable that detaches again.
Disposing the subscription ran that disposable, so `Alan` was never read.

If changes arrive while a delivery is still running, you get the latest value, not every value in between.
Pass an `IEqualityComparer<T>` as a third argument to skip a value that equals the last one sent. Without a
comparer, a change event that leaves the value the same still sends it again.

Watching properties is exactly what ReactiveUI's `WhenAnyValue` does for you, so in a ReactiveUI app you rarely
need to build this by hand. See [WhenAny](../handbook/when-any.md).

## Sending from many threads

### `SerializedSignal<T>`

`SerializedSignal<T>` lets many threads push values at the same time, and still hands your callback one value at
a time. It does this without holding a lock while your callback runs, so a callback that waits on another thread
cannot freeze the thread that sent the value. The [utility page](utility.md) explains why that matters, under
`Serialize`.

`Signal.Serialized<T>()` creates one:

```csharp
SerializedSignal<int> safe = Signal.Serialized<int>();
var busy = 0;
var overlaps = 0;

safe.Subscribe(x =>
{
    if (Interlocked.Increment(ref busy) > 1)
    {
        Interlocked.Increment(ref overlaps);
    }

    Thread.SpinWait(1000);
    Interlocked.Decrement(ref busy);
});

Parallel.For(0, 4, _ =>
{
    for (var i = 0; i < 200; i++)
    {
        safe.OnNext(i);
    }
});

Console.WriteLine($"overlaps: {overlaps}");   // overlaps: 0
```

Four threads pushed 800 values between them, and the callback never ran twice at once. A plain `Signal<int>`
in the same code overlaps hundreds of times.

To protect a signal you already have, wrap it: `Signal.Serialized(messages)`. Values you push into the wrapper
reach the original signal's subscribers.

## Delivering through a sequencer

### `ScheduledSignal<T>`

`ScheduledSignal<T>` hands each value to its subscribers through a sequencer, rather than straight away. Use it
when subscribers must run on one particular thread, such as the UI thread. `Signal.Scheduled<T>(sequencer)`
creates one.

```csharp
var clock = new VirtualClock();
ScheduledSignal<string> updates = Signal.Scheduled<string>(clock);

updates.Subscribe(u => Console.WriteLine($"got {u}"));
updates.OnNext("ready");
Console.WriteLine("sent ready");

clock.AdvanceBy(TimeSpan.FromTicks(1));
```

Output:

```text
sent ready
got ready
```

Give it a second argument, an observer, and that observer receives values pushed while nobody is subscribed,
so they are not lost.

## Holding values back

### `DelayableNotificationSignal<T>`

`DelayableNotificationSignal<T>` holds values back while you tell it to wait, then sends them together when you
call `Flush`. `Signal.Delayable` creates one. It takes two lambdas: one that says whether to hold values back
right now, and one that tidies up the waiting values before they are sent.

```csharp
var paused = true;

DelayableNotificationSignal<string> changes = Signal.Delayable<string>(
    () => paused,
    waiting => waiting.Distinct());

changes.Subscribe(c => Console.WriteLine($"changed {c}"));

changes.OnNext("name");
changes.OnNext("age");
changes.OnNext("name");
Console.WriteLine("(nothing sent yet)");

paused = false;
changes.Flush();

changes.OnNext("email");
```

Output:

```text
(nothing sent yet)
changed name
changed age
changed email
```

While paused, three changes waited. `Flush` sent them through `Distinct`, so the repeated `name` went once. Once
unpaused, `email` went straight through.

`Flush` sends whatever is waiting, even if you are still paused. Use this to update a screen once after a batch
of edits, instead of once per edit.

## Waiting for one final result

### `AsyncSignal<T>`

`AsyncSignal<T>` is a signal you can `await`. It keeps only the **last** value. When you complete it, it sends
that last value to its subscribers, and any `await` on it finishes with that value.

```csharp
var download = new AsyncSignal<int>();

download.Subscribe(p => Console.WriteLine($"subscriber got {p}"));

download.OnNext(10);
download.OnNext(60);
download.OnNext(100);
download.OnCompleted();

int finalValue = await download;
Console.WriteLine($"awaited {finalValue}");
```

Output:

```text
subscriber got 100
awaited 100
```

The subscriber did not see `10` or `60`. `AsyncSignal<T>` only ever sends one value, the last, and only when it
completes. `IsCompleted` tells you whether it has completed, and `Value` gives you the last value.

If it completes without ever receiving a value, awaiting it throws an `InvalidOperationException`.

## Limiting how many at a time

### `PrioritySemaphoreSignal<T>`

`PrioritySemaphoreSignal<T>` lets only a set number of values through, then holds the rest until you say there
is room. A **semaphore** is a counter that limits how many things can go ahead at once.

`T` must implement `IComparable`, as `int` and `string` do. The first values go straight through, up to the limit. After
that, waiting values queue up, and the **lowest** comes out first. Each call to `Release` makes room for one
more.

```csharp
var jobs = new PrioritySemaphoreSignal<int>(2);

jobs.Subscribe(job => Console.WriteLine($"running job {job}"), () => Console.WriteLine("all done"));

jobs.OnNext(5);
jobs.OnNext(1);
jobs.OnNext(3);
jobs.OnNext(4);
jobs.OnNext(2);
Console.WriteLine("(two running, three waiting)");

jobs.Release();
jobs.Release();

jobs.OnCompleted();
```

Output:

```text
running job 5
running job 1
(two running, three waiting)
running job 2
running job 3
running job 4
all done
```

`5` and `1` went straight through, filling the two places. `3`, `4` and `2` waited. Each `Release` let the
lowest waiting job go: `2`, then `3`. Completing the signal sent the last waiting job, `4`, whatever the limit.

Raise `MaximumCount` to let more through straight away:

```csharp
var jobs = new PrioritySemaphoreSignal<int>(1);
jobs.Subscribe(job => Console.WriteLine($"running job {job}"));

jobs.OnNext(9);
jobs.OnNext(3);
jobs.OnNext(7);

jobs.MaximumCount = 3;   // lets 3 and 7 through
```

## A command you can run

### `CommandSignal<TResult>`

`CommandSignal<TResult>` wraps a piece of work you run on request, such as saving a form. It reports what
happens as streams: `Results` for each result, `Faults` for each failure, and `IsRunning` for whether the work
is in progress.

```csharp
using var save = new CommandSignal<string>(async token =>
{
    await Task.Delay(50, token);
    return "saved";
});

save.IsRunning.Subscribe(running => Console.WriteLine($"running: {running}"));
save.Results.Subscribe(result => Console.WriteLine($"result: {result}"));

string outcome = await save.ExecuteAsync();
Console.WriteLine($"awaited: {outcome}");
```

Output:

```text
running: False
running: True
result: saved
running: False
awaited: saved
```

`ExecuteAsync` runs the work, and you can `await` it for the result. Subscribing to the command itself is the
same as subscribing to `Results`. The work can be synchronous too: `new CommandSignal<string>(() => "saved")`.

Give the command a second argument, a stream of `bool`, to control when it may run. `CanRun` tells you whether
it may run right now.

```csharp
var formIsValid = new BehaviorSignal<bool>(false);
using var save = new CommandSignal<string>(() => "saved", formIsValid);

Console.WriteLine(save.CanRun);   // False
await save.ExecuteAsync();        // throws InvalidOperationException: Command cannot run.

formIsValid.OnNext(true);
Console.WriteLine(save.CanRun);   // True
```

When the work throws, the exception goes to `Faults` first, then `ExecuteAsync` throws it too:

```csharp
using var save = new CommandSignal<string>(() => throw new InvalidOperationException("disk full"));

save.Faults.Subscribe(error => Console.WriteLine($"fault: {error.Message}"));
await save.ExecuteAsync();   // prints fault: disk full, then throws
```

Pass a cancellation token to `ExecuteAsync` to stop the work. A cancelled run also reaches `Faults`, as a
`TaskCanceledException`.

In a ReactiveUI app, `ReactiveCommand` does this job and binds straight to buttons. See
[commands](../handbook/commands/index.md).

## Work that can be cancelled

### `ITaskSignal<T>`

`Signal.FromTask` runs a piece of async work when you subscribe, and hands the work a cancellation source.
Disposing the subscription cancels the work. It gives you an `ITaskSignal<T>`, which also tells you whether
cancellation has been asked for.

```csharp
ITaskSignal<string> report = Signal.FromTask<string>(async cancellation =>
{
    await Task.Delay(5000, cancellation.Token);
    return "report ready";
});

IDisposable subscription = report.Subscribe(r => Console.WriteLine(r));

subscription.Dispose();
Console.WriteLine($"cancelled: {report.IsCancellationRequested}");   // cancelled: True
```

The report never printed, because disposing cancelled the work before it finished. `TaskSignal.Create` builds
an `ITaskSignal<T>` from a stream you return, when the work is itself a stream.

## The signal interfaces

These describe what a signal can do, so you can accept any kind of signal in your own code.

| Interface | What it is |
|---|---|
| `ISignal<T>` | A signal: a stream and a subscriber of the same type, with `HasObservers` and `IsDisposed`. |
| `ISignal<TSource, TResult>` | A signal that takes one type in and sends another out. |
| `IAwaitSignal<T>` | A signal you can `await`, with `IsCompleted` and `GetResult`. `AsyncSignal<T>` is one. |
| `ITaskSignal<T>` | A stream backed by cancellable work, with `CancellationTokenSource` and `IsCancellationRequested`. |

## The two package flavours

Every type here ships twice. `ReactiveUI.Primitives` puts them under `ReactiveUI.Primitives.*`.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and puts them under
`ReactiveUI.Primitives.Reactive.*`. They behave the same in both.
