# Disable Just My Code

Turn off Visual Studio's [Just My Code](https://learn.microsoft.com/visualstudio/debugger/just-my-code) feature.
With it on, the debugger steps over framework code, including `IObservable<T>` pipelines and `async`/`await`
state machines, so **Step Into** on a `WhenAnyValue` or `Subscribe` call skips straight past it. Turning it off
lets you step through the framework's own code, which is where most reactive bugs actually show up.

See [debugging ReactiveUI](debug-symbols.md) for the full set of debugger settings SourceLink needs, including
this one.

## Break on first-chance exceptions

Reactive debugging tip: turn on breaking on thrown exceptions before they are handled.

> In Visual Studio, when exceptions are thrown or end up unhandled, the debugger can help you debug these by
> breaking just like it breaks when a breakpoint is hit. In this blog post we will look at the different
> classifications of exceptions and how to configure when the debugger will break for those exceptions.

[Understanding exceptions while debugging with Visual
Studio](https://devblogs.microsoft.com/devops/understanding-exceptions-while-debugging-with-visual-studio)

An exception inside a stream's callback ends that subscription: the stream calls `OnError` instead of `OnNext`
or `OnCompleted`, and stops. Breaking on the exception itself, rather than after it becomes an unhandled error,
puts you at the exact line that failed.
