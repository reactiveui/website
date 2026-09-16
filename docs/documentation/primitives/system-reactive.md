---
Order: 20
---
# ReactiveUI.Primitives and System.Reactive

System.Reactive is the original Reactive Extensions library for .NET, and ReactiveUI supports it. This page sets
out how ReactiveUI.Primitives differs from it, why, how to get the most from Primitives, and how to use the two
together.

## The short version

- Both are built on the same interfaces, `IObservable<T>` and `IObserver<T>`, which ship in .NET itself. A stream
  from one library works with operators from the other.
- Most operators answer to their System.Reactive names, such as `Throttle` and `DistinctUntilChanged`. Most also
  have a Primitives name, such as `Calm` and `Unique`. Both names build the same thing.
- A few types sit outside .NET, so Primitives has its own: `RxVoid` for `Unit`, `ISequencer` for `IScheduler`,
  and signals for subjects.
- Primitives builds each operator as its own purpose-built sink. That is a design choice: more types in the
  library, in exchange for speed and fewer allocations.
- Primitives has no reflection-based APIs, so every part of it works with trimming and Native AOT.
- The `.Reactive` packages compile the same Primitives source against System.Reactive's types, for apps and
  libraries that must keep them.

## Words that differ

| System.Reactive | ReactiveUI.Primitives | What it is |
|---|---|---|
| Subject | **Signal** | A stream you push values into with `OnNext`. |
| Scheduler, `IScheduler` | **Sequencer**, `ISequencer` | Decides where and when work runs. See [sequencers and scheduling](scheduling.md). |
| `Unit` | `RxVoid` | A value that carries no data, for streams that only say "something happened". |
| `Notification<T>` | `Spark<T>` | A value, error or completion held as data. `Spark` produces them. |
| `Timestamped<T>` | `Moment<T>` | A value with the time it arrived. `Timestamp` produces them. |
| `TestScheduler` | `VirtualClock` | A sequencer whose time you move by hand, for tests. |

## Type mapping

### Signals and subjects

| System.Reactive | ReactiveUI.Primitives |
|---|---|
| `Subject<T>` | `Signal<T>` |
| `BehaviorSubject<T>` | `BehaviorSignal<T>`, or `StateSignal<T>` for a settable `Value` |
| `ReplaySubject<T>` | `ReplaySignal<T>` |
| `AsyncSubject<T>` | `AsyncSignal<T>` |
| `Subject<T>` with `Synchronize` | `SerializedSignal<T>` |

See [signals you push values into](signals.md).

### Disposables

| System.Reactive | ReactiveUI.Primitives |
|---|---|
| `Disposable.Create(action)` | `Scope.Create(action)` |
| `Disposable.Empty` | `Scope.Empty` or `EmptyDisposable.Instance` |
| `CompositeDisposable` | `MultipleDisposable` |
| `SerialDisposable` | `SingleReplaceableDisposable` |
| `SingleAssignmentDisposable` | `SingleDisposable` |
| `BooleanDisposable`, `CancellationDisposable` | The same names |

See [disposables](disposables.md).

### Schedulers and sequencers

| System.Reactive | ReactiveUI.Primitives |
|---|---|
| `ImmediateScheduler.Instance` | `Sequencer.Immediate` |
| `CurrentThreadScheduler.Instance` | `Sequencer.CurrentThread` |
| `TaskPoolScheduler.Default` | `Sequencer.Default` |
| `ThreadPoolScheduler.Instance` | `ThreadPoolSequencer.Instance` |
| `SynchronizationContextScheduler` | `SynchronizationContextSequencer` |
| `DispatcherScheduler` (WPF) | `DispatcherSequencer` |
| `ControlScheduler` (WinForms) | `ControlSequencer` |
| `TestScheduler` | `VirtualClock` |

## Operator names

The operators Primitives shares with System.Reactive answer to the System.Reactive name, so most System.Reactive
code reads and compiles the same. Most also have a Primitives name. Both names build the same thing, and you can use
whichever suits you. The System.Reactive names also match other reactive libraries, such as RxJS.

The rest of this site uses the LINQ name when LINQ has the operator, and the Primitives name otherwise.

| This site uses | System.Reactive name | Other Primitives name |
|---|---|---|
| `Select` | `Select` | `Map` |
| `Where` | `Where` | `Keep` |
| `SelectMany` | `SelectMany` | `FlatMap`, `Bind` |
| `Aggregate` | `Aggregate` | `Reduce` |
| `OfType` / `Cast` | `OfType` / `Cast` | `KeepType` / `CastTo` |
| `Concat` / `Zip` | `Concat` / `Zip` | `Chain` / `Pair` |
| `Range` / `Repeat` / `Empty` | `Range` / `Repeat` / `Empty` | `Sequence` / `Loop` / `None` |

Where LINQ has no such operator, this site uses the Primitives name:

| This site uses | System.Reactive name |
|---|---|
| `Fold` | `Scan` |
| `Tap` | `Do` |
| `Unique` | `DistinctUntilChanged` |
| `IgnoreValues` | `IgnoreElements` |
| `KeepNotNull` | `WhereNotNull` |
| `Spark` / `Unspark` | `Materialize` / `Dematerialize` |
| `Blend` | `Merge` |
| `Race` | `Amb` |
| `SwitchTo` | `Switch` |
| `SyncLatest` | `CombineLatest` |
| `Latch` | `WithLatestFrom` |
| `Calm` | `Throttle` |
| `Probe` | `Sample` |
| `Shift` | `Delay` |
| `DelayStart` | `DelaySubscription` |
| `Expire` | `Timeout` |
| `Recover` | `Catch` |
| `Reattempt(n)` | `Retry(n + 1)` |
| `WitnessOn` | `ObserveOn` |
| `ShareLive` / `ReplayLive` / `AutoShare` | `Publish` / `Replay` / `RefCount` |
| `Emit` / `Silent` / `Fail` | `Return` / `Never` / `Throw` |
| `Every` / `After` | `Interval` / `Timer` |
| `Lazy` / `Use` / `Unfold` | `Defer` / `Using` / `Generate` |

Some operators exist only in Primitives, such as `Choose`, `SwitchMap`, `MapWith` and `BlendUnique`. The operator
pages cover each one.

`Reattempt` and `Retry` build the same operator, but count differently. `Reattempt(n)` allows `n` extra tries, while
`Retry(n)` counts the first run too, so `Retry(3)` is `Reattempt(2)`.

## Behaviour that matches

These were checked side by side against System.Reactive 7.0.0, and behave the same:

- A subscriber that leaves out the error callback has the error thrown at the code that called `OnError`.
- A subscriber that throws from `OnNext` has the exception thrown at the code that called `OnNext`.
- `OnNext` on a disposed signal throws `ObjectDisposedException`.
- Values sent after `OnCompleted` are dropped, and a subscriber that arrives later gets the completion at once.
- `BehaviorSignal<T>`, `ReplaySignal<T>` and `AsyncSignal<T>` replay and await as their subject counterparts do.
- `Calm` and `Probe` send their pending value when the source completes, as `Throttle` and `Sample` do.
- `Expire` restarts its timer on each value, as `Timeout(TimeSpan)` does.
- `Retry(count)` and `Repeat(count)` run the source up to `count` times in total.
- `Fold`, `Aggregate` with a seed, `Buffer(count, skip)`, `Unique`, `SyncLatest`, `Blend(maxConcurrent)`,
  `OnErrorResumeNext`, and `FirstAsync` on an empty stream.

## Behaviour that differs

System.Reactive's `FirstAsync`, `FirstOrDefaultAsync`, `LastAsync` and `LastOrDefaultAsync` return an
`IObservable<T>` with one value. The Primitives methods of the same name return a `Task<T>`, so you `await` them
directly:

```csharp
int first = await signal.FirstAsync();
```

`GetAwaiter` and `RunAsync` return an `IAwaitSignal<T>` rather than System.Reactive's `AsyncSubject<T>`. You still
`await` a stream the same way.

`Signal.FromEventPattern(addHandler, removeHandler)` for a plain `EventHandler` sends `EventPattern<EventArgs>`,
where System.Reactive sends `EventPattern<object>`. `EventPattern<TEventArgs>` is Primitives' own type, in
`ReactiveUI.Primitives.Core`.

`ToEnumerable` blocks the calling thread until the stream completes, then hands back every value. System.Reactive's
`ToEnumerable` hands back each value as it arrives. Do not call it on a stream that never completes.

### Operators that look alike but behave differently

Some Primitives operators and [extension helpers](extensions/index.md) look close to a System.Reactive operator, but
behave differently. Each difference below was checked by running both libraries side by side. Check the row before you
swap one for the other.

| System.Reactive | Closest in Primitives | Difference |
|---|---|---|
| `ToEnumerable()` | `ToEnumerable()` | System.Reactive hands back each value as it arrives. Primitives blocks until the stream completes. |
| `Sample(sampler)` | `SampleLatest(trigger)` | `Sample` sends only when a new value arrived since the last sample. `SampleLatest` sends the latest value again on every trigger. |
| `Wait()` | `WaitForValue()` | `Wait` throws the stream's error, or `InvalidOperationException` when the stream is empty. `WaitForValue` returns `default` for both, and gives up after 30 seconds. |
| `Subscribe(..., token)` | `TakeUntil(token)` then `Subscribe` | Cancelling `Subscribe` stops silently. `TakeUntil` sends a completion. |
| `Min()`, `Max()` | `GetMin`, `GetMax` | `Min` and `Max` reduce one stream to one value. `GetMin` and `GetMax` compare the latest values of several streams. |
| `RetryWhen(handler)` | `RetryWithDelay`, `RetryWithBackoff`, `OnErrorRetry` | The helpers wait a delay you choose. Nothing in Primitives retries when another stream sends. |
| `SelectMany` with a `Task` | `SelectAsync`, `SelectAsyncConcurrent` | `SelectMany` runs the tasks at the same time and sends results as they finish. `SelectAsync` runs one at a time, in order. `SelectAsyncConcurrent(selector, int.MaxValue)` is the closest match. |
| `StartAsync(func)` | `Signal.FromAsync(func)` | `StartAsync` starts the task at once. `FromAsync` starts a task for each subscriber. `func().ToSignal()` starts it at once. |
| `RefCount(minObservers)` | `AutoConnect(subscriberCount)` | `RefCount` disconnects when the last subscriber leaves. `AutoConnect` never disconnects. |
| `Replay(window)` | `ReplayLive(bufferSize, window)` | `ReplayLive` needs a count as well. Pass `int.MaxValue` to limit by time alone. |

## What Primitives does not have

### Operators Primitives does not have yet

These System.Reactive operators have no Primitives version in this release:

| Group | Operators |
|---|---|
| Grouping and windows | `GroupBy`, `GroupByUntil`, `Window`, `Join`, `GroupJoin`, `And`, `Then`, `When` |
| Maths | `Min`, `Max`, `MinBy`, `MaxBy`, `Sum`, `Average` |
| Picking values | `TakeLast`, `TakeLastBuffer`, `SkipLast`, `SkipUntil`, `ElementAt`, `ElementAtOrDefault` |
| Results | `SingleAsync`, `SingleOrDefaultAsync`, `ForEachAsync`, `ToDictionary`, `ToLookup`, `SequenceEqual`, `Wait` |
| Blocking reads | `GetEnumerator`, `Latest`, `MostRecent`, `Next`, `Chunkify` |
| Error and repeat | `RetryWhen`, `RepeatWhen`, `DoWhile`, `ResetExceptionDispatchState` |
| Tasks and events | `StartAsync`, `ToAsync`, `ToEvent`, `ToEventPattern` |
| Experimental (`ObservableEx`) | `Expand`, `Let`, `ManySelect`, `ToListObservable` |

The async library, `ReactiveUI.Primitives.Async`, does have `GroupBy`, `ForEachAsync`, `SingleAsync` and
`SingleOrDefaultAsync`.

Some System.Reactive overloads also have no Primitives version. Among them are:

- `Aggregate` and `Scan` without a seed.
- `Where`, `SelectMany`, `SkipWhile` and `TakeWhile` with an index.
- `Skip(TimeSpan)`.
- `Buffer` with a closing selector, a time shift, or a time and a count.
- `Throttle`, `Delay` and `Timeout` that take a stream per value.
- `Zip` over more than two streams.
- `RefCount` with a disconnect delay.
- `Subscribe` with a `CancellationToken`.

If you rely on one of these, keep that part of your code on System.Reactive. The two work together, as shown below.

### Covered under another name

Some System.Reactive calls have no overload of the same name, but Primitives does the same job another way:

| System.Reactive | ReactiveUI.Primitives |
|---|---|
| `Distinct(keySelector)` | `DistinctBy(keySelector)` |
| `DistinctUntilChanged(keySelector)` | `UniqueBy(keySelector)` |
| `Observable.Amb(a, b)`, `Observable.Amb(sources)` | `Signal.Race(a, b)` |
| `Observable.Catch(sources)` | `Signal.Recover(sources)`, or `Recover()` on a collection |
| `Retry()` | `OnErrorRetry()`, from the [extension helpers](extensions/errors.md) |
| `DeferAsync(factory)` | `Signal.Defer(factory)` with an async factory |
| `Empty(witness)`, `Never(witness)`, `Throw(error, witness)` | `Signal.None(witness)`, `Signal.Silent(witness)`, `Signal.Fail(error, witness)` |
| `Timeout(dueTime, other)` | `Expire(dueTime)` followed by a catch of `TimeoutException` that switches to `other` |
| `PublishLast()` | `Multicast(new AsyncSignal<T>())` |
| `Publish(initialValue)` | `Multicast(new BehaviorSignal<T>(initialValue))` |
| `ObserveOn(context)`, `SubscribeOn(context)` with a `SynchronizationContext` | The same operator with `new SynchronizationContextSequencer(context)` |
| `Take(TimeSpan)`, `TakeUntil(DateTimeOffset)` | `TakeUntil(Signal.After(...))` |
| `Observable.For(items, selector)` | `Signal.Concat(items.Select(selector))` |

### Left out on purpose: APIs built on reflection

**Reflection** is .NET code that looks up types, members and events by name while the app runs, instead of the
compiler binding them at build time. A trimmer cannot see a member that is only named in a string, so it can remove
it. The failure then happens on the user's device, not at build time. Supporting trimming and Native AOT everywhere
was a large part of why Primitives exists, so it leaves out every System.Reactive API that relies on reflection.

Only a few parts of System.Reactive use reflection: 18 overloads of `FromEvent` and `FromEventPattern`, plus the
`IQbservable<T>` query operators. Every other System.Reactive operator is compiled normally.

| System.Reactive API | What it does at run time |
|---|---|
| `Observable.FromEventPattern(object target, string eventName)`, and the forms that take a `Type`, a sender type or a scheduler | Finds the event by its name. These are marked as unsafe for trimming. |
| `Observable.FromEventPattern<TDelegate, TEventArgs>(addHandler, removeHandler)` with no conversion | Builds the handler delegate by reflection. |
| `Observable.FromEventPattern<TDelegate, TSender, TEventArgs>(addHandler, removeHandler)` | Builds the handler delegate by reflection. |
| `Observable.FromEvent<TDelegate, TEventArgs>(addHandler, removeHandler)` with no conversion | Builds the handler delegate by reflection. |
| `IQbservable<T>` and the `Qbservable` operators | Build and rewrite expression trees, and create generic types and methods at run time. |

Primitives binds every event handler at compile time instead. `Signal.FromEventPattern` works with
`EventHandler<TEventArgs>`, so it needs `TEventArgs` to derive from `EventArgs`. For an event whose argument is any
other type, or whose delegate is not an `EventHandler`, use `Signal.FromEvent` with a conversion. It places no limit on
the argument type:

```csharp
IObservable<int> ticks = Signal.FromEvent<EventHandler<int>, int>(
    conversion: action => (_, value) => action(value),
    addHandler: handler => ticker.Ticked += handler,
    removeHandler: handler => ticker.Ticked -= handler);
```

## Native AOT

Native AOT compiles an app to machine code ahead of time, with no JIT. It trims away code the app does not use, so
anything the library finds by reflection can break. Every Primitives package is marked `IsAotCompatible`.

The same app was published with Native AOT against each library, on .NET 10, for `linux-x64`. The app
uses a signal or subject, `Select`, `Where`, `Scan`, `CombineLatest`, `Throttle`, `DistinctUntilChanged`, `SelectMany`,
`Buffer`, `Replay`, `RefCount`, `FromEventPattern`, `Timeout`, `Catch`, `ObserveOn`, `Merge`, `Switch` and
`Materialize`. Both native binaries ran and printed the same output as under the JIT.

| Check | System.Reactive 7.0.0 | ReactiveUI.Primitives |
|---|---|---|
| Trim and AOT warnings for the app | 0 | 0 |
| Native binary size for the app | 2.98 MB | 2.34 MB |
| Warnings with the **whole** library kept, not just what the app uses | 26 | 0 |
| Native binary size with the whole library kept | 9.25 MB | 7.12 MB |

Keeping the whole library shows what a library author sees when every public API must be AOT-safe. System.Reactive's
26 warnings all come from its `IQbservable` query providers and join patterns, which build expression trees at run
time.

Calling a reflection-based API from an app is where it matters most. The same Native AOT publish with
`Observable.FromEventPattern<TickArgs>(ticker, "Ticked")` gives 2 trim warnings, and the app then fails when it runs:

```text
Unhandled exception. System.InvalidOperationException: Could not find event 'Ticked' on object of type 'Ticker'.
```

Plain strongly typed System.Reactive code is AOT-clean. The difference is that nothing in Primitives can take you
down the reflection path.

## Performance

The Primitives repository benchmarks each operator against the closest System.Reactive version of the same work,
timing each call and counting the bytes it allocates. In its published run, Primitives was faster than
System.Reactive in 151 of 157 comparisons. A few rows from that run, as mean time and bytes allocated per call:

| Scenario | ReactiveUI.Primitives | System.Reactive |
|---|---|---|
| `FirstAsync` | 5.9 ns, 56 B | 2,582 ns, 2,792 B |
| `CombineLatest` | 41 ns, 192 B | 3,328 ns, 2,824 B |
| `ObserveOn` immediate | 27 ns, 96 B | 17,127 ns, 11,307 B |
| `Delay` over a range | 165 ns, 536 B | 6,285 ns, 39,584 B |
| Subject multicast to 4 | 3,417 ns, 600 B | 3,269 ns, 728 B |
| Subject subscribe and dispose 8 | 352 ns, 704 B | 318 ns, 1,288 B |

The last two rows are cases where System.Reactive is as fast or faster. The full table, the method, and how to run
it yourself are in the
[Primitives benchmarks](https://github.com/reactiveui/Primitives#benchmarks).

## Why the operators are built differently

System.Reactive builds operators from a small set of general parts. `Synchronize`, for example, is one operator you
add to any chain when you need values one at a time. That keeps the library small, and it reads well.

Primitives takes the other path. Each operator has its own **sink**, the object that receives values from the
source and passes results on. A sink that needs to deliver one value at a time builds that in, with the threading
that suits its job. The result is more types in the library, and no general layer between the sink and your code.

Neither path is wrong. ReactiveUI chose the dedicated path because streams sit under every ReactiveUI library, so
their speed and allocations reach every app. The same choice shows in the public types:

- **Signals for a job.** `SerializedSignal<T>` orders values from many threads. `StateSignal<T>` holds a
  settable current value. `CommandSignal<TResult>` runs work with `IsRunning` and `CanRun`. See
  [signals](signals.md).
- **Embedded state.** `DisposableSet` and the delivery types are structs you hold as a field in your own type,
  with no extra object. See [disposables](disposables.md).
- **Public operator types.** Each operator's sink is a public class, so you can build your own operators from the
  same parts. Every operator page lists them.

## Getting the most from Primitives

- **Send from many threads through `SerializedSignal<T>`**, rather than a subject followed by `Synchronize`.
- **Hold changing state in `StateSignal<T>`.** Set `Value` to send, and read `Value` at any time.
- **Use the `With` operators with `static` lambdas** on paths that run often. See
  [best practices](best-practices.md#mark-lambdas-static).
- **Prefer `Serialize` to `Synchronize`.** It holds no lock while your callback runs.
- **Test with `VirtualClock`.** The time operators take a sequencer.
- **Use `Signal.FromEvent` with a conversion** for events, so nothing depends on reflection.

## Using both libraries together

### Streams pass straight between them

A System.Reactive subject works with Primitives operators, and a Primitives signal works with System.Reactive
operators. Both are `IObservable<T>`.

### Importing both in one file

System.Reactive and Primitives both add extension methods named `Subscribe`, `Select`, `Where` and so on to
`IObservable<T>`. When a project references both libraries and a file imports both namespaces, C# cannot tell which
one you mean, and the call does not compile. `Subscribe` clashes even without the System.Reactive `using`, because
System.Reactive declares it in the `System` namespace.

Two ways around it:

- **Call `SubscribePrimitives`** instead of `Subscribe`. It does the same job, with a name that cannot clash. See
  [`SubscribePrimitives`](utility.md#subscribeprimitives).
- **Call System.Reactive through an alias** for the operators you need from it.

```csharp
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Signals;
using Rx = System.Reactive.Linq.Observable;

var rxSubject = new System.Reactive.Subjects.Subject<int>();
var signal = new Signal<int>();

IObservable<int> fromRx = Rx.Where(rxSubject, x => x > 1);
IObservable<int> fromPrimitives = signal.Where(x => x > 1);

fromRx.SubscribePrimitives(x => Console.WriteLine($"rx {x}"));
fromPrimitives.SubscribePrimitives(x => Console.WriteLine($"primitives {x}"));
```

### The `.Reactive` packages

Some code has to keep System.Reactive's types in its public API: a library other apps depend on, or a framework
that takes an `IScheduler`. The `.Reactive` packages are for that. `ReactiveUI.Primitives.Reactive` is the same
Primitives source, compiled so that `ISequencer` is System.Reactive's `IScheduler` and `RxVoid` is its `Unit`. Its
namespaces start `ReactiveUI.Primitives.Reactive`.

```csharp
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using ReactiveUI.Primitives.Reactive;
using ReactiveUI.Primitives.Reactive.Disposables;

var clicks = new Subject<Unit>();
var subscriptions = new ContainerDisposable();

clicks.Throttle(TimeSpan.FromMilliseconds(50), new EventLoopScheduler())
      .Select(_ => "clicked")
      .Subscribe(Console.WriteLine)
      .DisposeWith(subscriptions);

CompositeDisposable composite = subscriptions;
```

Here a System.Reactive subject, `Unit` and scheduler go straight into Primitives operators.

`ContainerDisposable` is a `MultipleDisposable` that converts to a `CompositeDisposable`, so you can hand it to code
that expects one. Disposing the container also disposes anything added through the composite.

`ReactiveUI.Primitives`, `ReactiveUI.Primitives.Async` and each platform package have a `.Reactive` partner, such as
`ReactiveUI.Primitives.Wpf.Reactive`.

### ReactiveUI's packages

ReactiveUI follows the same pattern. Both sets run on Primitives. They differ only in the types their public API
uses.

| Packages | Public types |
|---|---|
| `ReactiveUI`, `ReactiveUI.Wpf`, `ReactiveUI.Maui` and the other platform packages | `RxVoid`, `ISequencer`, `Signal<T>` |
| `ReactiveUI.Reactive`, `ReactiveUI.Wpf.Reactive`, `ReactiveUI.Maui.Reactive` and so on | `Unit`, `IScheduler`, `Subject<T>` |

Choose the `.Reactive` packages when your app uses another library that needs System.Reactive's types, or to
upgrade existing code without changing it.

### Moving code across

The [Primitives README](https://github.com/reactiveui/Primitives#moving-from-systemreactive) has a step-by-step
migration guide, for moving a project across in one go or for publishing a `.Reactive` package beside a lean one.
