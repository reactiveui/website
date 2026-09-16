---
Order: 11
---
# Async type reference

The async operators are built from public types too. The async pages show the ones you are most likely to use. This
page lists the rest, so every public async type has a home. For the words **signal**, **witness**, **subscription**,
**coordinator** and **extensions class**, see the [type reference](../types.md#the-words-used-on-this-page).

You never need these to use async streams. Call the operators. See
[writing your own async operator](advanced.md) for how the pieces fit together.

## `ReactiveUI.Primitives.Async`

| Type | What it is | Documented with |
|---|---|---|
| `AsyncContextExtensions` | Holds `IsSameAsCurrentAsyncContext`. | [Utility](utility.md) |
| `AsyncContextSwitcherAwaitable` | What you `await` to move onto an `AsyncContext`. | [Utility](utility.md) |
| `ChainEnumerableSignal<T>` | `Concat` on a collection of async streams. | [Combination](combination.md) |
| `ChainSignalSourcesSignal<T>` | `Concat` on an async stream of async streams. | [Combination](combination.md) |
| `ContextSwitchSignalAsync<T>` | The signal behind `Yield`. It moves each notification onto an `AsyncContext`. | [Utility](utility.md) |
| `DisposableAsyncExtensions` | Holds `ToDisposableAsync`. | [Advanced](advanced.md) |
| `OptionalExtensions` | Holds `TryGetValue` for `Optional<T>`. | [Advanced](advanced.md) |
| `SignalAsyncExtensions` | Holds every async operator and terminal method. | [Async streams](index.md) |
| `SignalExtensions` | Holds `AsObserverAsync` and `MapValues` for async signals. | [Signals](signals.md) |
| `SwitchToSignal<T>` | The signal behind `SwitchTo`. | [Combination](combination.md) |

## `ReactiveUI.Primitives.Async.Advanced`

### Creating streams

| Type | What it is | Documented with |
|---|---|---|
| `AsyncEnumerableSignal<T>`, `AsyncEnumerableSubscription<T>` | Send the items of an `IAsyncEnumerable<T>`. | [`FromAsyncEnumerable`](creation-factories.md) |
| `BackgroundJobSignal<T>` | The signal behind `CreateAsBackgroundJob`. | [Creation factories](creation-factories.md) |
| `EnumerableSignal<T>`, `EnumerableSubscription<T>` | Send the items of a collection, for `FromEnumerable` and `ToAsyncSignal`. | [`FromEnumerable`](creation-factories.md) |
| `FromAsyncSignal`, `FromAsyncSubscription` | `FromAsync` for work with no result. They send `RxVoid` when it finishes. | [`FromAsync`](creation-factories.md) |
| `FromAsyncSignal<T>`, `FromAsyncSubscription<T>` | `FromAsync` for work with a result. | [`FromAsync`](creation-factories.md) |
| `IntervalSignal`, `IntervalSubscription` | The signal and subscription behind `Interval`. The counter starts at `0`. | [`Interval`](creation-factories.md) |
| `ITaskSignalJob<T>` | The job a `TaskSignalState` runs for one subscription. | [Advanced](advanced.md) |
| `SequenceSignal`, `SequenceSubscription` | The signal and subscription behind `Range`. | [`Range`](creation-factories.md) |
| `StartSignal`, `StartSubscription` | `Start` with an action. It sends `RxVoid` when the action finishes. | [`Start`](creation-factories.md) |
| `StartSignal<TResult>`, `StartSubscription<TResult>` | `Start` with a function. It sends the result. | [`Start`](creation-factories.md) |
| `TaskResultSignal<T>`, `TaskResultSubscription<T>` | `ToAsyncSignal` on a `Task<T>`: sends the task's result. | [`ToAsyncSignal`](creation-factories.md) |
| `TaskSignalState` | Runs one cancellable job for one subscriber, and stops it on dispose. | [Advanced](advanced.md) |
| `TaskToAsyncSignal`, `ToAsyncSignalSubscription` | `ToAsyncSignal` on a `Task`: sends `RxVoid` when it completes. | [`ToAsyncSignal`](creation-factories.md) |
| `TimerSignal`, `TimerSubscription` | The signal and subscription behind `Timer`, `After` and `Every`. | [Creation factories](creation-factories.md) |
| `UsingSignal<TResource, T>`, `UsingWitness<TResource, T>` | Create a resource for each subscriber and dispose it with the subscription. | [Creation factories](creation-factories.md) |

### Operators

| Type | What it is | Documented with |
|---|---|---|
| `CallbackWitnessAsync<T>` | The witness `SubscribeAsync` builds from your lambdas. An error with no lambda goes to the unhandled exception handler. | [`SubscribeAsync`](results.md) |
| `DoOnSubscribeSignal<T>`, `DoOnSubscribeAsyncSignal<T>` | `DoOnSubscribe` with a plain and an async lambda. | [Utility](utility.md) |
| `FlatMapSignal<TSource, TResult>`, `FlatMapCoordinator<TResult>`, `FlatMapWitness<TSource, TResult>`, `FlatMapWitness<TResult>` | `SelectMany`: the signal, its coordinator, and the witnesses for the source and each inner stream. | [`SelectMany`](transformation.md) |
| `LeadSubscription<T>` | The subscription behind `Prepend`. | [`Prepend`](combination.md) |
| `LogErrorsSignal<T>`, `LogErrorsWitness<T>` | The signal and witness behind `LogErrors`. | [Error handling](error-handling.md) |
| `ReattemptSubscription<T>`, `ReattemptWitness<T>` | The subscription and witness behind `Reattempt`. | [Error handling](error-handling.md) |
| `RelayWitnessAsync<T>` | The witness behind `Wrap`. It passes each notification on to another async witness. | [Utility](utility.md) |
| `SingleElementWitness<T>` | The witness behind `SingleAsync` and `SingleOrDefaultAsync`. It fails as soon as a second match arrives. | [Results](results.md) |
| `WitnessOnWitness<T>` | The witness behind `WitnessOn`. It moves each notification onto the `AsyncContext` first. | [Utility](utility.md) |

### `SyncLatest`

| Type | What it is | Documented with |
|---|---|---|
| `SyncLatest2Signal<T1, T2, TResult>` to `SyncLatest16Signal<...>` | `SyncLatest` on 2 to 16 streams. | [`SyncLatest`](combination.md) |
| `SyncLatest2Coordinator<T1, T2, TResult>` to `SyncLatest16Coordinator<...>` | The built-in coordinators for 2 to 16 streams. | [Combining the latest values yourself](advanced.md#combining-the-latest-values-yourself) |
| `SyncLatest2State<T1, T2>` to `SyncLatest16State<...>` | Structs that hold the source streams for each coordinator. | [Combining the latest values yourself](advanced.md#combining-the-latest-values-yourself) |
| `SyncLatestEnumerableSignal<TSource, TResult>`, `SyncLatestEnumerableCoordinator<TSource, TResult>`, `SyncLatestEnumerableWitness<TSource, TResult>` | `SyncLatest` on a collection of streams. | [`SyncLatest`](combination.md) |
| `SyncLatestWitness<TSource, TResult>` | The witness the built-in coordinators subscribe to each source. | [Combining the latest values yourself](advanced.md#combining-the-latest-values-yourself) |
| `SyncLatestCoordinatorExtensions` | Holds `SubscribeSourcesAsync` and `SubscribeToSlotAsync`. | [Combining the latest values yourself](advanced.md#combining-the-latest-values-yourself) |

### Plumbing

| Type | What it is | Documented with |
|---|---|---|
| `AsyncSerialGate`, `AsyncSerialGate.Lease` | Lets one piece of async work in at a time. Disposing the `Lease` lets the next one in. The same thread may enter again while it holds the gate. | [Advanced](advanced.md) |
| `PooledDelaySource` | Reusable, cancellable delays on a `TimeProvider`, used by `Throttle`. | [Time](time.md) |

## `ReactiveUI.Primitives.Async.Helpers`

| Type | What it is | Documented with |
|---|---|---|
| `DisposalHelper` | Methods that dispose something only once, using an `int` flag. | [Advanced](advanced.md) |
| `SubscriptionHelper` | Subscribes and disposes the subscription again if a later step throws. | [Advanced](advanced.md) |

## The two package flavours

`ReactiveUI.Primitives.Async.Reactive` has every type on this page, compiled against System.Reactive.
