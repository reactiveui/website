---
Order: 29
---
# ReactiveUI Extensions

The [**ReactiveUI.Extensions**](https://github.com/reactiveui/Extensions) package is a single-assembly collection of reactive helper operators that complement ReactiveUI and `System.Reactive`. It is a regular NuGet package and does not pull in any UI dependencies.

## Installation

```xml
<PackageReference Include="ReactiveUI.Extensions" Version="*" />
```

```csharp
using ReactiveUI.Extensions;
```

## What's in the box

The package surfaces a handful of static classes — `ReactiveExtensions`, `ObservableSubscriptionExtensions`, `ObserverExtensions`, the `Async` operator family, and `ScheduleSafe`/`Continuation` helpers. Below is a tour of the most commonly used members; see [the source](https://github.com/reactiveui/Extensions/tree/main/src/ReactiveUI.Extensions) for the full list.

### Filtering & shaping observables

```csharp
// Drop nulls
source.WhereIsNotNull();

// Drop until the first non-null value, then forward everything after
source.SkipWhileNull();

// Coerce any observable into IObservable<Unit>
source.AsSignal();

// Convenience filters for bool streams
source.WhereTrue();
source.WhereFalse();
```

### Combining booleans

```csharp
// True when every source is true / false right now
new[] { obsA, obsB, obsC }.CombineLatestValuesAreAllTrue();
new[] { obsA, obsB, obsC }.CombineLatestValuesAreAllFalse();
```

### Buffering & throttling

```csharp
// Buffer characters between two delimiters into a string
charStream.BufferUntil('<', '>');

// Buffer values until the source has been idle for the given window
source.BufferUntilIdle(TimeSpan.FromMilliseconds(200));

// Emit the latest value, or a default, when the source has been quiet
source.LatestOrDefault(TimeSpan.FromSeconds(1), defaultValue);
```

### Detect stale data / heartbeats

```csharp
// Wrap values so subscribers can tell when the latest value is "stale"
source.DetectStale(TimeSpan.FromSeconds(5));

// Periodic heartbeat carrying the most recent value
source.Heartbeat(TimeSpan.FromSeconds(1));
```

### Conditional scheduling

```csharp
// Hop to a scheduler only when a predicate is true
source.ObserveOnIf(condition, scheduler);
```

### Retry with delay

```csharp
source.RetryWithDelay(TimeSpan.FromSeconds(2));
source.RetryWithDelay(TimeSpan.FromSeconds(2), retryCount: 3);
```

### Synchronous helpers (handy in tests / scripts)

`ObservableSubscriptionExtensions` adds blocking convenience extensions for testing scenarios:

```csharp
T? value     = source.WaitForValue();
T? value     = source.WaitForValue(TimeSpan.FromSeconds(1));
Exception? e = source.WaitForError();
source.WaitForCompletion();

// Subscribe-and-collect helpers
var v = source.SubscribeGetValue();
var ex = source.SubscribeGetError();
```

> These methods block the calling thread — use them in tests or one-off scripts, not on the UI thread.

### Async operator family

The `Async` namespace contains an alternative set of operators implemented on top of `IAsyncObservable<T>` (e.g. `CombineLatest`, `Catch`, custom factories, error-handling). See [`Async`](https://github.com/reactiveui/Extensions/tree/main/src/ReactiveUI.Extensions/Async) in source for the full surface.

### Other helpers

- `ScheduleSafe(this IScheduler?, Action)` — schedule that no-ops on a null scheduler.
- `FastForEach<T>(this IObserver<T>, IEnumerable<T>)` — push a collection through an observer.
- `Continuation` — fluent chaining helpers for `IObservable<Unit>` work.

## When to reach for it

ReactiveUI.Extensions is intentionally a grab-bag — pull it in when one of these operators saves you a custom implementation, but you don't need it just to use ReactiveUI. The core `System.Reactive` operators cover most everyday work; this package fills in the gaps that come up repeatedly in reactive view-model code.

## Additional Resources

- [ReactiveUI.Extensions repository](https://github.com/reactiveui/Extensions)
- [ReactiveUI Handbook](../index.md)
- [System.Reactive (Rx.NET) documentation](https://github.com/dotnet/reactive)
