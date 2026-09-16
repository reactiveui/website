---
Order: 17
---
# Type reference

Every operator in ReactiveUI.Primitives is built from public types, so you can construct them yourself when you write
an operator of your own. The operator pages show the types you are most likely to use. This page lists the rest, so
every public type has a home.

You never need these to use the library. Call the operators. For async streams, see the
[async type reference](async/types.md).

## The words used on this page

| Word | What it is |
|---|---|
| **Signal** | A class that implements `IObservable<T>`. Subscribing to it starts the operator's work. |
| **Witness** or **sink** | A class that implements `IObserver<T>`. The signal subscribes it to the source, and it passes results on. |
| **Subscription** | A class you dispose to stop a running signal. |
| **Coordinator** | A class that holds the state for one subscription when an operator watches more than one thing. |
| **Aggregator** | A small struct that holds a running result, such as a count, and hands back a new copy for each value. |
| **Extensions class** | A static class that holds extension methods. Its methods are documented as operators. |

Each table below says where the operator behind a type is documented. See [writing your own operator](advanced.md)
for how the pieces fit together.

## `ReactiveUI.Primitives`

| Type | What it is | Documented with |
|---|---|---|
| `AnonymousSignal<T>` | A signal made from a subscribe lambda, for code of your own. | [Writing your own operator](advanced.md) |
| `ConnectableSignalExtensions` | Holds the sharing operators for connectable signals. | [Sharing](sharing.md) |
| `ConnectableSignalRxNameExtensions` | Holds the System.Reactive names of the sharing operators, such as `Publish` and `RefCount`. | [Sharing](sharing.md) |
| `ExceptionExtensions` | Holds `Throw`, which rethrows an exception without losing its stack trace. | [Error handling](error-handling.md) |
| `Handle<T>`, `Handle<T1, T2>`, `Handle<T1, T2, T3>` | Shared, cached lambdas that do nothing or rethrow, so operators need not allocate their own. | [Writing your own operator](advanced.md) |
| `SubscribeExtensions` | Holds the `Subscribe` overloads that take lambdas. | [Utility](utility.md) |

## `ReactiveUI.Primitives.Advanced`

### Creating streams

| Type | What it is | Documented with |
|---|---|---|
| `AfterSignal`, `AfterSubscription` | The signal and subscription behind `Signal.After`. | [Creation factories](creation-factories.md) |
| `AsyncCreateSignal<T>` | A signal made from an async subscribe lambda. | [`Signal.Create`](creation-factories.md) |
| `AsyncDeferSignal<T>` | A signal whose source is built by an async lambda on each subscription. | [`Signal.Lazy`](creation-factories.md) |
| `AsyncEnumerableSignal<T>` | A signal that sends the items of an `IAsyncEnumerable<T>`. | [Creation factories](creation-factories.md) |
| `AsyncSubscriptionLifetime` | Holds the cancellation and the inner subscription for the async creation signals. | [Creation factories](creation-factories.md) |
| `CreateSink<T>` | The witness behind `Signal.Create`. It passes notifications on until the stream ends. | [`Signal.Create`](creation-factories.md) |
| `EmptySignal<T>` | Completes without a value, on a sequencer. | [`Signal.Empty`](creation-factories.md) |
| `EmptyWitness<T>` | A witness whose missing lambdas do nothing, used by the `Subscribe` overloads. | [Utility](utility.md) |
| `EverySignal` | The timer behind `Signal.Every`. | [`Signal.Every`](creation-factories.md) |
| `FromAsyncSignal<T>`, `FromAsyncSubscription<T>` | The signal and subscription behind `Signal.FromAsync`. Each subscriber gets its own task and token. | [`Signal.FromAsync`](creation-factories.md) |
| `FromAsyncExternalCancellationSignal<T>` | `Signal.FromAsync` with a token you pass in. Cancelling it fails the stream. | [`Signal.FromAsync`](creation-factories.md) |
| `FromEnumerableSignal<T>` | Sends the items of a collection. | [`Signal.FromEnumerable`](creation-factories.md) |
| `FromEventConversionSignal<TEventHandler, TCallback, TResult>` | `Signal.FromEvent` with a conversion lambda. | [From an event](creation-factories.md#from-an-event) |
| `FromEventPatternSignal<TEventHandler, TEventArgs>` | The signal behind `Signal.FromEventPattern`. | [From an event](creation-factories.md#from-an-event) |
| `GuardedWitness<T>` | The witness behind `Signal.CreateSafe`. It releases the source when the stream ends or your callback throws. | [`Signal.CreateSafe`](creation-factories.md) |
| `ImmediateReturnSignal<T>`, `ImmutableReturnTrueSignal`, `ImmutableReturnFalseSignal`, `ImmutableReturnInt32Signal`, `ImmutableReturnRxVoidSignal` | `Signal.Emit` on the calling thread. The fixed-value types are shared single instances. | [`Signal.Emit`](creation-factories.md) |
| `ImmediateThrowSignal<T>` | `Signal.Fail` on the calling thread. | [`Signal.Fail`](creation-factories.md) |
| `ImmutableEmptySignal<T>` | `Signal.Empty` on the calling thread, as a shared instance. | [`Signal.Empty`](creation-factories.md) |
| `ImmutableNeverSignal<T>` | `Signal.Silent`: never sends and never ends. | [`Signal.Silent`](creation-factories.md) |
| `LoopSignal<T>` | Sends the same value over and over until disposed, for `Signal.Repeat` with no count. | [Creation factories](creation-factories.md) |
| `RangeSignal` | The signal behind `Signal.Range`. | [`Signal.Range`](creation-factories.md) |
| `RepeatSignal<T>` | Sends one value a set number of times. | [`Signal.Repeat`](creation-factories.md) |
| `ReturnSignal<T>` | `Signal.Emit` on a sequencer. | [`Signal.Emit`](creation-factories.md) |
| `ScheduledEnumerableSignal<T>` | Sends the items of a collection on a sequencer. | [`Signal.FromEnumerable`](creation-factories.md) |
| `SequenceSignal` | Sends a run of integers on a sequencer, for `Signal.Range` with a sequencer. | [Creation factories](creation-factories.md) |
| `StartSignal`, `StartSignal<T>` | The signals behind `Signal.Start`, for an action and for a function. | [`Signal.Start`](creation-factories.md) |
| `TaskInstanceSignal<T>`, `TaskInstanceSubscription` | Sends the result of a task you already have. | [Creation factories](creation-factories.md) |
| `ThrowSignal<T>` | `Signal.Fail` on a sequencer. | [`Signal.Fail`](creation-factories.md) |
| `UnfoldSignal<TState, TResult>` | The signal behind `Signal.Unfold`. | [`Signal.Unfold`](creation-factories.md) |
| `UseSignal<TResource, T>` | The signal behind `Signal.Use`. It disposes the resource once, when the stream ends or you dispose. | [`Signal.Use`](creation-factories.md) |

### Changing and filtering values

| Type | What it is | Documented with |
|---|---|---|
| `AsObservableSignal<T>` | Hides the type of the source. | [Utility](utility.md) |
| `CastWitness<TResult>` | The witness behind `Cast`. | [Filtering](filtering.md) |
| `DefaultIfEmptyWitness<T>` | The witness behind `DefaultIfEmpty`. | [Filtering](filtering.md) |
| `DistinctWitness<T>`, `DistinctByWitness<T, TKey>` | The witnesses behind `Distinct` and `DistinctBy`. | [Filtering](filtering.md) |
| `IgnoreValuesWitness<T>` | The witness behind `IgnoreValues`: drops every value and keeps the ending. | [Filtering](filtering.md) |
| `KeepNotNullWitness<T>` | The witness behind `WhereNotNull`. | [Filtering](filtering.md) |
| `KeepTypeWitness<TResult>` | The witness behind `OfType`. | [Filtering](filtering.md) |
| `MapIndexedWitness<TSource, TResult>` | The witness behind `Select` with an index. | [Transformation](transformation.md) |
| `SelectManyEnumerableSignal<TSource, TResult>`, `SelectManyEnumerableWitness<TSource, TResult>` | `SelectMany` where each value turns into a collection. | [Transformation](transformation.md) |
| `SelectManyResultSignal<TSource, TCollection, TResult>` | `SelectMany` with a result lambda. | [Transformation](transformation.md) |
| `SkipWitness<T>`, `SkipWhileWitness<T>` | The witnesses behind `Skip` and `SkipWhile`. | [Filtering](filtering.md) |
| `SparkWitness<T>`, `UnsparkWitness<T>` | The witnesses behind `Spark` and `Unspark`, which turn notifications into values and back. | [Transformation](transformation.md) |
| `SwitchWitness<T>` | The witness behind `SwitchTo`. | [Transformation](transformation.md) |
| `TakeWitness<T>`, `TakeWhileWitness<T>` | The witnesses behind `Take` and `TakeWhile`. | [Filtering](filtering.md) |
| `TimeIntervalWitness<T>` | The witness behind `TimeInterval`. | [Transformation](transformation.md) |
| `UniqueWitness<T>`, `UniqueByWitness<T, TKey>` | The witnesses behind `Unique` and `UniqueBy`. | [Filtering](filtering.md) |

### Joining streams

| Type | What it is | Documented with |
|---|---|---|
| `AppendWitness<T>`, `AppendDelegateWitness<T>` | The witnesses behind `Append`. | [Combination](combination.md) |
| `BlendSignal<T>`, `EnumerableBlendSignal<T>`, `BlendWitness<T>` | `Blend` on a stream of streams and on a collection. | [Combination](combination.md) |
| `ChainSignal<T>`, `ChainWitness<T>` | The signal and witness behind `Concat`. | [Combination](combination.md) |
| `ForkJoinSignal<TLeft, TRight, TResult>`, `ForkJoinWitness<TLeft, TRight, TResult>` | `ForkJoin` on two streams: the final value of each, once both complete. | [Combination](combination.md) |
| `MaxConcurrentEnumerableBlendSignal<T>` | `Blend` on a collection, with a limit on how many run at once. | [Combination](combination.md) |
| `MergeSignal<T>` | `Blend` on two streams or a collection, with an optional limit. | [Combination](combination.md) |
| `PairSignal<TLeft, TRight, TResult>`, `PairWitness<TLeft, TRight, TResult>` | The signal and witness behind `Zip`. | [Combination](combination.md) |
| `RaceSignal<T>`, `RaceWitness<T>` | The signal and witness behind `Race`. | [Combination](combination.md) |
| `RangeCombineLatestSignal<TResult>`, `RangeConcatSignal`, `RangeForkJoinSignal<TResult>`, `RangeSyncLatestSignal<TResult>`, `RangeWithLatestSignal<TResult>`, `RangeZipSignal<TResult>` | Faster forms of `SyncLatest`, `Concat`, `ForkJoin`, `Latch` and `Zip` used when every source is a `Signal.Range`. | [Combination](combination.md) |
| `SyncLatestSignal<TLeft, TRight, TResult>`, `SyncLatestWitness<TLeft, TRight, TResult>` | `SyncLatest` on two streams. | [Combination](combination.md) |
| `TaskChainSignal<T>` | `Concat` on a stream of tasks. | [Combination](combination.md) |

### Time, sharing and threads

| Type | What it is | Documented with |
|---|---|---|
| `AutoShareSubscription<T>` | The subscription handle behind `AutoShare`, which counts subscribers. | [Sharing](sharing.md) |
| `BufferSignal<T>`, `BufferWitness<T>`, `BufferEachWitness<T>` | `Buffer` by time and by count. `BufferEachWitness<T>` handles a count of one. | [Time](time.md) |
| `CollectWitness<T>`, `CollectListWitness<T>`, `CollectArrayWitness<T>` | `CollectWitness<T>` is behind `Buffer` by time. The other two are behind `ToList` and `ToArray`. | [Time](time.md), [Aggregation](aggregation.md) |
| `EmitIfQuietSignal<T>`, `EmitIfQuietWitness<T>` | The signal and witness behind `EmitIfQuiet`. | [Time](time.md) |
| `PublishSelectorSignal<TSource, TResult>` | `Publish(selector)`: shares a stream for one expression. | [Sharing](sharing.md) |
| `SerializeWitness<T>` | The witness behind `Serialize`. | [Utility](utility.md) |
| `SynchronizeGateSignal<T>`, `SynchronizeObjectSignal<T>`, `SynchronizeObjectWitness<T>`, `SynchronizeWitness<T>` | `Synchronize`, with a shared gate or an object you lock on. | [Utility](utility.md) |
| `TapWitness<T>` | The witness behind `Tap`. | [Utility](utility.md) |

### Results

| Type | What it is | Documented with |
|---|---|---|
| `AggregateWitness<T, TResult, TAggregator>` | Runs an aggregator over every value and sends its result at the end. | [Aggregation](aggregation.md) |
| `AllPredicateWitness<T>`, `AnyWitness<T>`, `AnyPredicateWitness<T>`, `ContainsWitness<T>`, `IsEmptyWitness<T>` | The witnesses behind `All`, `Any`, `Contains` and `IsEmpty`. | [Aggregation](aggregation.md) |
| `CountAggregator<T>`, `CountPredicateAggregator<T>`, `LongCountAggregator<T>`, `LongCountPredicateAggregator<T>` | The aggregators behind `Count` and `LongCount`. | [Aggregation](aggregation.md) |
| `DistinctByCountAggregator<T, TKey>`, `DistinctByLongCountAggregator<T, TKey>` | Aggregators that count distinct keys. | [Aggregation](aggregation.md) |
| `FoldWitness<TSource, TAccumulate>` | The witness behind `Fold`. | [Aggregation](aggregation.md) |
| `IAggregator<T, TResult, TSelf>` | The interface an aggregator implements: `Add` hands back a new state, and `Result` reads it. | [Aggregation](aggregation.md) |
| `ReduceWitness<TSource, TAccumulate>` | The witness behind `Aggregate`. | [Aggregation](aggregation.md) |
| `TaskAnyWitness<T>`, `TaskCountWitness<T>` | The witnesses behind `AnyAsync` and `CountAsync`. | [Aggregation](aggregation.md) |

### Witnesses and plumbing

| Type | What it is | Documented with |
|---|---|---|
| `CallbackWitness<T>` | A witness that calls lambdas, used by `SubscribeAsync`. | [Tasks](extensions/tasks.md) |
| `CopyOnWriteList<T>` | A list that makes a new array on each change, so readers never need a lock. Signals hold their subscribers in one. | [Signals](signals.md) |
| `DisposedMarker` | The value a subscription slot holds once released. | [`SubscriptionSlots`](advanced.md#subscriptionslots) |
| `DisposedWitness<T>` | A witness that throws `ObjectDisposedException`. `AsyncSignal<T>` swaps it in once disposed. | [Signals](signals.md) |
| `ForwardingWitness<T>` | A witness that passes every notification to another witness. | [Writing your own operator](advanced.md) |
| `ListWitness<T>` | A witness that sends each notification to a list of witnesses, such as the subscribers of `AsyncSignal<T>`. | [Signals](signals.md) |
| `RepeatSourceWitness<T>` | The witness behind `Repeat` on a stream. It drops a second ending. | [Error handling](error-handling.md) |
| `SignalSubscription` | Shared methods that signals use to subscribe their witnesses. | [Writing your own operator](advanced.md) |
| `StatefulWitness<T, TState>` | A witness that calls lambdas with a state value, so they can be `static`. | [`Witness.Create`](advanced.md#witnesscreate) |
| `ThrowWitness<T>` | A witness that ignores values and throws any error it gets. | [Error handling](error-handling.md) |

## `ReactiveUI.Primitives.Concurrency`

| Type | What it is | Documented with |
|---|---|---|
| `SequencerExtensions` | Holds the `Schedule` overloads for any `ISequencer`. | [Scheduling](scheduling.md) |
| `VirtualTimeSequencerExtensions` | Holds helpers for virtual time sequencers. | [Testing with a virtual clock](scheduling.md#testing-with-a-virtual-clock) |
| `IScheduledItem<TAbsolute>`, `ScheduledItem<TAbsolute>`, `ScheduledItem` | A piece of work waiting in a queue for its due time, and the method that creates one. | [Writing your own sequencer](scheduling.md#writing-your-own-sequencer) |
| `SequencerQueue<TAbsolute>` | The queue a virtual time sequencer keeps its work in, ordered by due time. | [Writing your own sequencer](scheduling.md#writing-your-own-sequencer) |
| `IStopwatchProvider` | Something that can start an `IStopwatch`. `VirtualClock` is one. | [Testing with a virtual clock](scheduling.md#testing-with-a-virtual-clock) |
| `DispatcherQueueSequencerExtensions` | Holds `ToSequencer` for a WinUI `DispatcherQueue`. | [UI platforms](platforms.md#winui) |
| `MauiDispatcherSequencerExtensions` | Holds `ToSequencer` for a MAUI `IDispatcher`. | [UI platforms](platforms.md#maui) |

`BlazorRendererSequencerExtensions`, in `ReactiveUI.Primitives.Blazor.Concurrency`, holds `ToSequencer` for a Blazor
`Dispatcher`. See [UI platforms](platforms.md#blazor).

## `ReactiveUI.Primitives.Core`

| Type | What it is | Documented with |
|---|---|---|
| `PriorityQueue<T>` | A priority queue that keeps items with equal priority in the order they were added. The sequencers and `PrioritySemaphoreSignal<T>` use it. | [Scheduling](scheduling.md), [Signals](signals.md) |
| `SparkKind` | Whether a `Spark` holds a value, an error or a completion. | [Transformation](transformation.md) |

## `ReactiveUI.Primitives.Signals`

| Type | What it is | Documented with |
|---|---|---|
| `Awaiter`, `AwaitWitness<T>` | What makes `await stream` work. | [`await` a stream directly](aggregation.md) |
| `Broadcaster<T>` | The struct a signal uses to send to its subscribers. It needs no allocation for one subscriber. | [Signals](signals.md) |
| `CommandExecution<TResult>` | What `CommandSignal<TResult>` hands back when run. You can `await` it. | [`CommandSignal<TResult>`](signals.md) |
| `DelegateWitness<T>` | The witness `Witness.Create` builds. It calls your lambdas, and skips an ending with no lambda. | [`Witness.Create`](advanced.md#witnesscreate) |
| `ObserverHandler<T>` | The subscription a signal hands back. Disposing it removes the subscriber once. | [Signals](signals.md) |
| `SignalExtensions` | Holds the operators on `IObservable<T>` that live in this namespace, such as `WitnessOn`, `OnCleanup` and `Recover`. | [Utility](utility.md), [Error handling](error-handling.md) |
| `StateSignalExtensions` | Holds `ToReadOnlyState`. | [Sharing](sharing.md) |

## `ReactiveUI.Primitives.Extensions`

| Type | What it is | Documented with |
|---|---|---|
| `ObservableSubscriptionExtensions` | Holds `WaitForValue`, `WaitForError` and the other helpers that block until a stream sends. | [State and testing](extensions/state-and-testing.md) |
| `ObserverExtensions` | Holds `FastForEach`, which sends every item of a collection to a witness. | [Utility](utility.md) |
| `DrainNotificationKind` | Whether a queued notification is a value, an error or a completion, for helpers that queue notifications such as `Conflate`. | [Timing](extensions/timing.md) |

## `ReactiveUI.Primitives.Extensions.Operators`

| Type | What it is | Documented with |
|---|---|---|
| `BinaryMinMaxObservable<T>`, `MinMaxObservable<T>` | The streams behind `GetMax` and `GetMin`. | [Values](extensions/values.md) |
| `BooleanReduceObservable` | The stream behind `CombineLatestValuesAreAllTrue` and `CombineLatestValuesAreAllFalse`. | [Values](extensions/values.md) |
| `BufferUntilObservable` | The stream behind `BufferUntil`. | [Values](extensions/values.md) |
| `CachedObservables` | Holds `UnitDefault`, a shared stream that sends one `RxVoid` and completes. | [Values](extensions/values.md) |
| `FilterRegexObservable` | The stream behind `Filter`, which keeps strings that match a regular expression. | [Values](extensions/values.md) |
| `FirstMatchFromCandidatesObservable<TKey, TRaw, TResult>` | The stream behind `FirstMatchFromCandidates`. | [Values](extensions/values.md) |
| `LatestOrDefaultObservable<T>` | The stream behind `LatestOrDefault`. | [Values](extensions/values.md) |
| `NotObservable` | The stream behind `Not`. | [Values](extensions/values.md) |
| `SelectConstantObservable<TSource, TResult>` | Sends the same value for every source value. | [Values](extensions/values.md) |
| `SelectManyThenObservable<TSource, TMid, TResult>` | The stream behind `SelectManyThen`. | [Values](extensions/values.md) |
| `ShuffleObservable<T>` | The stream behind `Shuffle`. | [Values](extensions/values.md) |
| `SkipWhileNullObservable<T>` | The stream behind `SkipWhileNull`. | [Values](extensions/values.md) |
| `SwitchIfEmptyObservable<T>` | The stream behind `SwitchIfEmpty`. | [Values](extensions/values.md) |
| `FirstMatchFromCandidatesObservable<TKey, TRaw, TResult>.SyncProbe` | A witness that records whether a candidate stream sent a value, failed or completed straight away. | [Values](extensions/values.md) |
| `TakeUntilInclusiveObservable<T>` | The stream behind `TakeUntil` with a test. | [Values](extensions/values.md) |
| `WaitUntilObservable<T>` | The stream behind `WaitUntil`. | [Values](extensions/values.md) |
| `WhereTrueObservable`, `WhereFalseObservable` | The streams behind `WhereTrue` and `WhereFalse`. | [Values](extensions/values.md) |
| `WhereSelectObservable<TIn, TOut>` | Filters and changes values in one step. | [Values](extensions/values.md) |

## The two package flavours

`ReactiveUI.Primitives.Reactive` has every type on this page under `ReactiveUI.Primitives.Reactive.*`, compiled against
System.Reactive.
