---
Order: 29
---
# Extension helpers

View model code keeps needing the same few helpers: drop `null` values, retry with a delay, notice a stream that has
gone quiet, or block a test until a value arrives. These helpers ship in the `ReactiveUI.Primitives` package that
ReactiveUI is built on, so there is nothing extra to install.

Add the namespace:

```csharp
using ReactiveUI.Primitives.Extensions;
```

The helpers are documented, each with an example, in the Primitives section:

| You want to | Helpers | Page |
|---|---|---|
| Filter or reshape values, such as dropping `null` or combining `bool` streams | `WhereIsNotNull`, `SkipWhileNull`, `WhereTrue`, `WhereFalse`, `AsSignal`, `LatestOrDefault`, `CombineLatestValuesAreAllTrue` | [Values](../../../primitives/extensions/values.md) |
| Batch values, wait for a pause, or spot a quiet stream | `BufferUntil`, `BufferUntilIdle`, `ThrottleDistinct`, `DetectStale`, `Heartbeat` | [Timing](../../../primitives/extensions/timing.md) |
| Retry and recover | `RetryWithDelay`, `CatchAndReturn` | [Errors](../../../primitives/extensions/errors.md) |
| Run async work for each value, or turn a stream into a task | `SelectAsync`, `SelectLatestAsync`, `SubscribeAsync`, `ToHotTask` | [Tasks](../../../primitives/extensions/tasks.md) |
| Hold state, or block a test until a value arrives | `WaitForValue`, `WaitForError`, `WaitForCompletion`, `SubscribeGetValue` | [State and testing](../../../primitives/extensions/state-and-testing.md) |

The overview, [extension helpers](../../../primitives/extensions/index.md), walks through a first helper.

Streams where the sender waits for each subscriber, `IObservableAsync<T>`, have their own operators in the
`ReactiveUI.Primitives.Async` package. See [async streams](../../../primitives/async/index.md).

Code that uses System.Reactive's types gets the same helpers from the `ReactiveUI.Primitives.Reactive` package. See
[the `.Reactive` packages](../../../primitives/system-reactive.md#the-reactive-packages).
