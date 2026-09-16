---
Order: 1
---
# Creation factories

You have a value, a list, a task, an event or a clock. None of them is a stream. A creation factory turns one
of them into a stream, so the operators can work on it.

A **stream** is a source that pushes values to you over time. Its .NET type is `IObservable<T>`. You call
`Subscribe` on it to start listening. It then pushes you each value, and ends in one of two ways: it
**completes** when it has nothing more to send, or it **errors** when something went wrong.

Every factory on this page is a static method on the `Signal` class.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;
```

## Your first factory

Say you have an async method that fetches a price. You want it as a stream.

**1. Pick the factory that matches what you have.** You have a method that returns a task, so use `FromAsync`.

```csharp
IObservable<decimal> prices = Signal.FromAsync(() => FetchPriceAsync());
```

**2. Nothing has run yet.** Building a stream does not start it. `FetchPriceAsync` has not been called.

**3. Subscribe.** This starts the work.

```csharp
IDisposable subscription = prices.Subscribe(
    price => Console.WriteLine($"price: {price}"),
    error => Console.WriteLine($"failed: {error.Message}"),
    () => Console.WriteLine("done"));
```

Output:

```text
price: 42.50
done
```

**4. Dispose when you are finished.** Disposing stops you listening. Here it also cancels the fetch.

```csharp
subscription.Dispose();
```

That is the whole shape. The rest of this page is one section per factory.

## Cold and hot

Nearly every factory here builds a **cold** stream. Cold means each subscriber gets its own private run. Two
subscribers to the `prices` stream above call `FetchPriceAsync` twice.

A **hot** stream runs once and shares that one run with everyone listening. `Signal.FromTask` is the one
factory on this page that is hot, because the task it wraps is already running.

This matters more often than it sounds, so each section below says which kind it builds.

## Sequencers

Many factories take an `ISequencer`. A **sequencer** decides which thread runs your callback, and when.

| Sequencer | Where your callback runs |
|---|---|
| `Sequencer.Immediate` | On the thread that asked, right away. |
| `Sequencer.CurrentThread` | On the thread that asked, after the current work finishes. |
| `Sequencer.Default` | On a background thread from the thread pool. |

```csharp
using ReactiveUI.Primitives.Concurrency;
```

Leave the argument off and the factory picks a sensible default for what it does.

## One value, then done

### `Emit`

`Emit` sends one value and completes. Cold, so every subscriber gets the value.

```csharp
IObservable<int> answer = Signal.Emit(42);

answer.Subscribe(x => Console.WriteLine(x), () => Console.WriteLine("done"));
```

Output:

```text
42
done
```

`Return` is a second name for `Emit`. Both build the same thing.

```csharp
IObservable<int> same = Signal.Return(42);
```

These overloads exist:

| Call | What it does |
|---|---|
| `Signal.Emit(value)` | Sends the value as soon as you subscribe. |
| `Signal.Emit(value, sequencer)` | Sends the value on that sequencer. |
| `Signal.Emit(true)` | Returns a shared stream for `true`. No allocation. |
| `Signal.Emit(7)` | Sends one `int`. |
| `Signal.Emit(RxVoid.Default)` | Returns the shared "something happened" stream. |
| `Signal.EmitRxVoid()` | The same shared stream, with no argument to pass. |
| `Signal.Return(value)` | Second name for `Signal.Emit(value)`. |
| `Signal.Return(value, sequencer)` | Second name for the sequencer form. |

`RxVoid` is a type with exactly one value. Use it when only the fact that something happened matters, and
there is no value to carry.

```csharp
IObservable<RxVoid> saved = Signal.EmitRxVoid();

saved.Subscribe(_ => Console.WriteLine("the save finished"));
```

Output:

```text
the save finished
```

### `None`

`None` completes straight away and sends no values at all.

```csharp
IObservable<int> nothing = Signal.None<int>();

nothing.Subscribe(
    x => Console.WriteLine($"value: {x}"),
    () => Console.WriteLine("done"));
```

Output:

```text
done
```

`Empty` is a second name for `None`.

Two of the overloads take a value they never send. They read the type off that value, so you do not have to
write the type parameter yourself:

```csharp
int example = 0;

IObservable<int> a = Signal.None<int>();       // you name the type
IObservable<int> b = Signal.None(example);     // the type is read from example
```

| Call | What it does |
|---|---|
| `Signal.None<T>()` | Completes at once. |
| `Signal.None<T>(sequencer)` | Completes on that sequencer. |
| `Signal.None(example)` | Completes at once, taking `T` from the example value. |
| `Signal.None(sequencer, example)` | Both of the above. |
| `Signal.Empty<T>()` | Second name for `None<T>()`. |
| `Signal.Empty<T>(sequencer)` | Second name for the sequencer form. |

### `Silent`

`Silent` sends nothing and never ends. Use it as a stream that should never fire, such as a "no timeout"
placeholder.

```csharp
IObservable<int> quiet = Signal.Silent<int>();

quiet.Subscribe(
    x => Console.WriteLine($"value: {x}"),
    () => Console.WriteLine("done"));
```

Output: nothing at all. Neither callback ever runs.

`Never` is a second name for `Silent`. `Signal.Silent(example)` reads the type from a value it never sends.

### `None` against `Silent`

They look similar and behave very differently.

| Factory | Sends values | Ends |
|---|---|---|
| `Signal.None<int>()` | No | Yes, completes immediately |
| `Signal.Silent<int>()` | No | No, stays open forever |

An operator waiting for a stream to finish, such as `Chain`, moves on immediately after `None` and waits
forever after `Silent`.

### `Fail`

`Fail` sends no values and errors as soon as you subscribe.

```csharp
IObservable<int> broken = Signal.Fail<int>(new InvalidOperationException("no connection"));

broken.Subscribe(
    x => Console.WriteLine($"value: {x}"),
    error => Console.WriteLine($"error: {error.Message}"));
```

Output:

```text
error: no connection
```

`Throw` is a second name for `Fail`.

| Call | What it does |
|---|---|
| `Signal.Fail<T>(error)` | Errors as soon as you subscribe. |
| `Signal.Fail<T>(error, sequencer)` | Errors on that sequencer. |
| `Signal.Fail(error, example)` | Errors, taking `T` from the example value. |
| `Signal.Fail(error, sequencer, example)` | Both of the above. |
| `Signal.Throw<T>(error)` | Second name for `Fail<T>(error)`. |
| `Signal.Throw<T>(error, sequencer)` | Second name for the sequencer form. |

## Repeating, counting and generating

### `Loop`

`Loop` sends the same value again and again.

```csharp
IObservable<string> beats = Signal.Loop("tick", 3);

beats.Subscribe(x => Console.WriteLine(x), () => Console.WriteLine("done"));
```

Output:

```text
tick
tick
tick
done
```

Leave the count off and it repeats forever, so dispose the subscription to stop it.

```csharp
IObservable<string> forever = Signal.Loop("tick");
```

`Repeat` is a second name for `Loop`.

### `Sequence`

`Sequence` sends a run of consecutive integers, then completes.

```csharp
IObservable<int> numbers = Signal.Sequence(10, 4);

numbers.Subscribe(x => Console.WriteLine(x), () => Console.WriteLine("done"));
```

Output:

```text
10
11
12
13
done
```

The first argument is where to start. The second is how many to send, not where to stop. `Range` is a second
name for `Sequence`, and both take an optional sequencer.

### `Unfold`

`Unfold` walks a value forward step by step. You give it four things: the value to start at, a test that says
whether to keep going, a step that produces the next value, and a projection that turns each value into what
you send.

```csharp
IObservable<int> squares = Signal.Unfold(
    initialState: 1,
    condition: state => state <= 5,
    iterate: state => state + 1,
    resultSelector: state => state * state);

squares.Subscribe(x => Console.WriteLine(x), () => Console.WriteLine("done"));
```

Output:

```text
1
4
9
16
25
done
```

`Iterate` and `Generate` are second names for `Unfold`.

## Building a stream by hand

When no factory fits, write the subscribe logic yourself.

### `Create`

`Create` takes a function that receives a subscriber and returns an `IDisposable`. Call `OnNext` for each
value, then `OnCompleted` or `OnError`. Return a disposable that cleans up.

```csharp
IObservable<int> counted = Signal.Create<int>(observer =>
{
    observer.OnNext(1);
    observer.OnNext(2);
    observer.OnCompleted();

    return new ActionDisposable(() => Console.WriteLine("cleaned up"));
});

counted.Subscribe(x => Console.WriteLine(x));
```

Output:

```text
1
2
cleaned up
```

`ActionDisposable` lives in `ReactiveUI.Primitives.Disposables`. Return `EmptyDisposable.Instance` when there
is nothing to clean up.

Your function runs once per subscriber, so `Create` builds a cold stream.

| Call | What it does |
|---|---|
| `Signal.Create<T>(subscribe)` | Builds the stream from your function. |
| `Signal.Create<T>(subscribe, isRequiredSubscribeOnCurrentThread)` | Declares that subscribing must happen on the current thread, so operators can skip a thread hop. |
| `Signal.Create<T>(asyncSubscribe)` | Your function returns `Task<IDisposable>`, so it can await. |
| `Signal.Create<T>(asyncSubscribeWithToken)` | The same, and you get a `CancellationToken` that fires when the subscriber disposes. |

### `CreateSafe`

`CreateSafe` is `Create` with one difference. It matters when your subscriber's own callback throws.

| Factory | A subscriber callback throws |
|---|---|
| `Signal.Create` | The subscription stays alive. |
| `Signal.CreateSafe` | The subscription is released. |

Use `CreateSafe` when your stream is cold and each subscriber owns its own resources, so a throwing
subscriber releases the resources it was using.

```csharp
IObservable<int> safe = Signal.CreateSafe<int>(observer =>
{
    observer.OnNext(1);
    return new ActionDisposable(() => Console.WriteLine("released"));
});

safe.Subscribe(x => throw new InvalidOperationException("boom"));
```

Output:

```text
released
```

### `CreateWithState`

`CreateWithState` passes an object of yours through to the function. That lets you write the function as
`static`, so it captures nothing and allocates nothing.

```csharp
IObservable<int> fromState = Signal.CreateWithState(
    state: 5,
    subscribe: static (count, observer) =>
    {
        for (var i = 0; i < count; i++)
        {
            observer.OnNext(i);
        }

        observer.OnCompleted();
        return EmptyDisposable.Instance;
    });

fromState.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
0 1 2 3 4
```

The result is the same as `Create`. Only the allocation differs.

## Deciding when someone subscribes

### `Lazy`

`Lazy` calls your factory once per subscriber, at the moment they subscribe. Use it when the stream must read
something current.

```csharp
var clicks = 0;

IObservable<int> latest = Signal.Lazy(() => Signal.Emit(clicks));

clicks = 1;
latest.Subscribe(x => Console.WriteLine($"first: {x}"));

clicks = 2;
latest.Subscribe(x => Console.WriteLine($"second: {x}"));
```

Output:

```text
first: 1
second: 2
```

Without `Lazy`, `Signal.Emit(clicks)` would read `clicks` once, when you built the stream, and both
subscribers would see `0`.

`Defer` is a second name for `Lazy`, and it adds two async forms:

| Call | What it does |
|---|---|
| `Signal.Lazy(factory)` | Calls your factory per subscriber. |
| `Signal.Defer(factory)` | Second name for `Lazy`. |
| `Signal.Defer(asyncFactory)` | Your factory returns `Task<IObservable<T>>`, so it can await. |
| `Signal.Defer(asyncFactoryWithToken)` | The same, with a `CancellationToken`. |

### `If`

`If` checks a condition each time someone subscribes, then uses one stream or the other.

```csharp
var useCache = true;

IObservable<string> rows = Signal.If(
    () => useCache,
    thenSource: Signal.Emit("from cache"),
    elseSource: Signal.Emit("from server"));

rows.Subscribe(x => Console.WriteLine(x));

useCache = false;
rows.Subscribe(x => Console.WriteLine(x));
```

Output:

```text
from cache
from server
```

With no `elseSource`, a false condition gives you an empty stream that completes at once.

### `Case`

`Case` picks a stream out of a dictionary, using a key it reads at subscribe time.

```csharp
var mode = "fast";

var sources = new Dictionary<string, IObservable<string>>
{
    ["fast"] = Signal.Emit("cached answer"),
    ["slow"] = Signal.Emit("computed answer"),
};

IObservable<string> answer = Signal.Case(() => mode, sources);

answer.Subscribe(x => Console.WriteLine(x));

mode = "slow";
answer.Subscribe(x => Console.WriteLine(x));
```

Output:

```text
cached answer
computed answer
```

A key that is not in the dictionary gives an empty stream. Pass a third argument to use a fallback stream
instead.

### `Use`

`Use` ties a resource to the subscription. It builds the resource when someone subscribes, builds the stream
from it, and disposes the resource when the stream ends or the subscriber leaves.

```csharp
IObservable<string> lines = Signal.Use(
    resourceFactory: () => new StreamReader("names.txt"),
    signalFactory: reader => Signal.FromEnumerable(ReadAll(reader)));

lines.Subscribe(x => Console.WriteLine(x), () => Console.WriteLine("reader disposed"));
```

Output:

```text
Ada
Grace
reader disposed
```

Each subscriber gets its own reader, and its own reader is disposed. `Using` is a second name for `Use`.

## From a collection

### `FromEnumerable`

`FromEnumerable` walks a collection and sends each item, then completes.

```csharp
IObservable<string> names = Signal.FromEnumerable(["Ada", "Grace", "Katherine"]);

names.Subscribe(x => Console.WriteLine(x), () => Console.WriteLine("done"));
```

Output:

```text
Ada
Grace
Katherine
done
```

It is cold, so each subscriber walks the collection again. Pass a `CancellationToken` as a second argument to
stop walking when the token fires.

### `FromAsyncEnumerable`

`FromAsyncEnumerable` does the same for an `IAsyncEnumerable<T>`.

```csharp
IObservable<string> rows = Signal.FromAsyncEnumerable(QueryRowsAsync());

rows.Subscribe(x => Console.WriteLine(x));
```

Output:

```text
row 1
row 2
```

It is not available on .NET Framework targets. Every other factory on this page is available everywhere.

### `ToSignal` on a collection

`ToSignal` is the same thing written as an extension method, so it reads left to right.

```csharp
IObservable<int> numbers = new[] { 1, 2, 3 }.ToSignal();

numbers.Subscribe(x => Console.Write($"{x} "));
```

Output:

```text
1 2 3
```

`ToObservable` is a second name for it, and adds a sequencer form.

| Call | What it does |
|---|---|
| `values.ToSignal()` | Sends each item, then completes. |
| `values.ToSignal(cancellationToken)` | Stops walking when the token fires. |
| `values.ToObservable()` | Second name for `ToSignal()`. |
| `values.ToObservable(sequencer)` | Sends the items on that sequencer. |
| `values.ToObservable(cancellationToken)` | Second name for the cancellable form. |

## From a task

### `FromTask` against `FromAsync`

These two look alike and differ in the way that matters most: when the work runs.

```csharp
// The task is already running. Everyone shares its one result.
Task<int> running = FetchAsync();
IObservable<int> hot = Signal.FromTask(running);

// The factory runs once per subscriber. Two subscribers means two fetches.
IObservable<int> cold = Signal.FromAsync(() => FetchAsync());
```

Subscribe twice to each:

| Stream | Fetches performed | Result each subscriber sees |
|---|---|---|
| `hot` | 1 | The same value |
| `cold` | 2 | Its own value |

`FromTask` also takes a short cut when the task has already finished: it sends the result without scheduling
anything.

```csharp
IObservable<int> done = Signal.FromTask(Task.FromResult(7));

done.Subscribe(x => Console.WriteLine(x), () => Console.WriteLine("done"));
```

Output:

```text
7
done
```

### `FromAsync`

`FromAsync` starts the work when someone subscribes and sends the result.

```csharp
IObservable<string> body = Signal.FromAsync(token => httpClient.GetStringAsync(url, token));

IDisposable subscription = body.Subscribe(x => Console.WriteLine(x.Length));

subscription.Dispose();   // cancels the request
```

| Call | What it does |
|---|---|
| `Signal.FromAsync(() => task)` | Starts the task per subscriber. |
| `Signal.FromAsync(token => task)` | The same, and disposing cancels the task. |
| `Signal.FromAsync(token => task, cancellationToken)` | The same, and your own token cancels it too. |

### `ToSignal` on a task

`ToSignal` on a `Task<T>` is the extension form of `Signal.FromTask`. It is hot, because the task is already
running.

```csharp
IObservable<int> result = FetchAsync().ToSignal();

result.Subscribe(x => Console.WriteLine(x));
```

`ToObservable` is a second name for it.

### `FromTask` with a cancellation source

There is a second `FromTask` family that hands your work the `CancellationTokenSource` the subscription owns.
It returns an `ITaskSignal<T>`, which is a stream you can also cancel directly.

```csharp
ITaskSignal<int> job = Signal.FromTask(async cts =>
{
    await Task.Delay(1000, cts.Token);
    return 7;
});

IDisposable subscription = job.Subscribe(x => Console.WriteLine(x));

subscription.Dispose();   // cancels the token source, so the delay stops
```

Each form takes the work first, then optional extras:

| Call | What it does |
|---|---|
| `Signal.FromTask(work)` | Runs the work, cancelling it on dispose. |
| `Signal.FromTask(work, sequencer)` | The same, sending the result on that sequencer. |
| `Signal.FromTask(work, sequencer, cts)` | The same, watching a cancellation source you own. |

Each of the three has a generic form that returns `ITaskSignal<TResult>` instead of `ITaskSignal<RxVoid>`.

## From an event

An event is already a source of values over time. These factories turn one into a stream so you can filter
and combine it.

### `FromEvent`

`FromEvent` takes a pair of callbacks: one that attaches a handler, one that removes it.

```csharp
IObservable<string> messages = Signal.FromEvent<string>(
    addHandler: handler => chat.MessageReceived += handler,
    removeHandler: handler => chat.MessageReceived -= handler);

IDisposable subscription = messages.Subscribe(x => Console.WriteLine(x));

subscription.Dispose();   // removes the handler
```

Output:

```text
hello
```

The handler is attached when someone subscribes and removed when they dispose, so you cannot leak it.

When the event uses its own delegate type, pass a conversion that wraps an `Action<TEventArgs>` in that type:

```csharp
IObservable<PriceArgs> prices = Signal.FromEvent<PriceHandler, PriceArgs>(
    conversion: action => new PriceHandler(args => action(args)),
    addHandler: handler => feed.PriceChanged += handler,
    removeHandler: handler => feed.PriceChanged -= handler);
```

A third argument sets the sequencer used to attach and remove the handler.

### `FromEventPattern`

Most .NET events follow the `(object sender, TEventArgs args)` shape. `FromEventPattern` handles that shape
and hands you both parts.

```csharp
IObservable<EventPattern<PropertyChangedEventArgs>> changes =
    Signal.FromEventPattern<PropertyChangedEventArgs>(
        handler => person.PropertyChanged += handler,
        handler => person.PropertyChanged -= handler);

changes.Subscribe(e => Console.WriteLine(e.EventArgs.PropertyName));
```

Output:

```text
Name
```

The family covers the shapes you meet:

| Call | Use it when |
|---|---|
| `Signal.FromEventPattern(add, remove)` | The event is a plain `EventHandler`. |
| `Signal.FromEventPattern<TArgs>(add, remove)` | The event is `EventHandler<TArgs>`. |
| `Signal.FromEventPattern<THandler, TArgs>(add, remove)` | The event uses its own delegate type. |
| `Signal.FromEventPattern<THandler, TArgs>(conversion, add, remove)` | The same, and the delegate needs an explicit conversion. |
| `Signal.FromEventPattern<THandler, TSender, TArgs>(conversion, add, remove)` | The same, and you want the sender's real type instead of `object`. |

Every one of them takes an optional `ISequencer` as a final argument.

## Running work

### `Start`

`Start` runs a piece of work once and sends the result.

```csharp
IObservable<int> total = Signal.Start(() => CountLines("names.txt"));

total.Subscribe(x => Console.WriteLine($"lines: {x}"));
```

Output:

```text
lines: 3
```

The work runs on `Sequencer.Default`, which is a background thread, so it does not block the caller. Pass a
sequencer as a second argument to choose another one.

An `Action` overload runs work with no result and sends `RxVoid` when it finishes.

```csharp
IObservable<RxVoid> saved = Signal.Start(() => File.WriteAllText("out.txt", "hi"));

saved.Subscribe(_ => Console.WriteLine("written"));
```

Output:

```text
written
```

`Start` runs the work once per subscriber.

## From a clock

### `After`

`After` sends the number `0` once, after a delay, then completes.

```csharp
IObservable<long> ready = Signal.After(TimeSpan.FromSeconds(2));

ready.Subscribe(x => Console.WriteLine($"fired: {x}"), () => Console.WriteLine("done"));
```

Output, two seconds later:

```text
fired: 0
done
```

Give it a second `TimeSpan` and it keeps going, counting up.

```csharp
IObservable<long> ticks = Signal.After(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));

ticks.Subscribe(x => Console.WriteLine(x));
```

Output: `0` after one second, then `1`, `2`, `3` every two seconds after that. It never completes, so dispose
the subscription to stop it.

`Timer` is a second name for `After`.

| Call | What it does |
|---|---|
| `Signal.After(dueTime)` | Sends `0` after the delay, then completes. |
| `Signal.After(dueTimeOffset)` | Sends `0` at an absolute time, then completes. |
| `Signal.After(dueTime, period)` | Sends `0`, then counts up every period, forever. |
| `Signal.Timer(...)` | Second name for each of the three, each also taking a sequencer. |

### `Every`

`Every` counts up on a fixed period, forever. There is no initial delay to set; the first value arrives after
one full period.

```csharp
IObservable<long> seconds = Signal.Every(TimeSpan.FromSeconds(1));

IDisposable subscription = seconds.Subscribe(x => Console.WriteLine(x));
```

Output, one per second:

```text
0
1
2
```

Dispose the subscription to stop it. `Interval` and `Pulse` are second names for `Every`, and each takes an
optional sequencer.

### `After` against `Every`

```csharp
Signal.After(TimeSpan.FromSeconds(5))                        // 0 at 5s, then completes
Signal.After(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(1))  // 0 at 5s, 1 at 6s, 2 at 7s, …
Signal.Every(TimeSpan.FromSeconds(1))                        // 0 at 1s, 1 at 2s, 2 at 3s, …
```

Use `After` for a deadline. Use `Every` for a heartbeat.

## Emitting inside `Subscribe`

`Observables.Return` sends one value and completes, like `Signal.Emit`. The difference is that it does the
work inside your call to `Subscribe`, before `Subscribe` returns.

```csharp
using ReactiveUI.Primitives.Extensions;

Console.WriteLine("before");
Observables.Return(1).Subscribe(x => Console.WriteLine(x));
Console.WriteLine("after");
```

Output:

```text
before
1
after
```

Reach for it when you need the value to have arrived by the time the next line runs.

## Factories that join streams

`Signal` also carries static factories that build one stream out of several: `Blend`, `Chain`, `Race`,
`Switch`, `Pair`, `SyncLatest`, `PairLatest`, `ForkJoin`, `OnErrorResumeNext`, `Recover`, `Merge` and
`Concat`. They are covered with the operators that do the same job, on the combination page.

Factories that build a stream you push values into yourself, such as `Signal.Serialized`, `Signal.Scheduled`
and `Signal.Delayable`, are covered on the subjects page.

## The two package flavours

Every type here ships twice. `ReactiveUI.Primitives` puts them under `ReactiveUI.Primitives.*`.
`ReactiveUI.Primitives.Reactive` is the same source compiled against System.Reactive, and puts them under
`ReactiveUI.Primitives.Reactive.*`. The factories behave the same in both.
