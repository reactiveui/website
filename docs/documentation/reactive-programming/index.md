---
Order: 12
---
# Reactive Programming

ReactiveUI view models describe how values change over time: a search runs when the user stops typing, a button
enables when a form is valid, a label follows a property. Reactive programming gives you one tool for all of those:
the **stream**.

A stream is an `IObservable<T>`. It **pushes** values to you as they happen, the way an event does, and you can shape
it with operators, the way LINQ shapes a collection. That is why reactive programming is often called "LINQ for
events".

ReactiveUI is built on [ReactiveUI.Primitives](../primitives/index.md), which supplies the streams, the operators and the
sequencers that decide which thread code runs on.

## Where to start

| You want to | Read |
|---|---|
| Understand why streams beat events, tasks and locks for this work | [Why Primitives](../primitives/why-primitives.md) |
| Build your first stream and subscribe to it | [The Primitives overview](../primitives/index.md) |
| See the streams a ReactiveUI app works with every day | [Streams in ReactiveUI](observables.md) |
| Find the operator for a job | [Operators in ReactiveUI](operators.md) |
| Avoid leaks and frozen screens | [Best practices](../primitives/best-practices.md) |
| Move code from System.Reactive | [ReactiveUI.Primitives and System.Reactive](../primitives/system-reactive.md) |

## The three things a stream sends

A stream sends three kinds of notification to each subscriber:

- **a value**, as many times as it likes;
- **completion**, once, to say no more values will come;
- **failure**, once, with the exception that ended it.

You **subscribe** to receive them. `Subscribe` hands back an `IDisposable`: dispose it to stop. In a view or view
model, [`WhenActivated`](../handbook/when-activated.md) disposes subscriptions for you when the view goes away.

## Threads

Code that updates the screen must run on the UI thread. `RxSchedulers.MainThreadScheduler` is ReactiveUI's
[sequencer](../primitives/scheduling.md) for that thread, and `RxSchedulers.TaskpoolScheduler` runs work in the
background. Pass `RxSchedulers.MainThreadScheduler` to `WitnessOn` to move a stream's values onto the UI thread. See
[scheduling](../handbook/scheduling.md) and [UI platforms](../primitives/platforms.md).

## See also

- [Testing](../handbook/testing.md), for moving time forward yourself instead of waiting.
- [Videos](videos.md)
