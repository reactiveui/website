# Controlling Scheduling

By default, `ReactiveCommand` uses `RxApp.MainThreadScheduler` to surface events. That is, values from `CanExecute`, `IsExecuting`, `ThrownExceptions`, and result values from the command itself. Typically UI components are subscribed to these observables, so it's a sensible default. However, when writing unit tests for your view models, you may want more control over scheduling.

> **Warning** It's important to understand that the execution logic for a reactive command is *not* scheduled to execute on the provided scheduler (just as is the case for any `canExecute` observable you provide). Instead, it is left to the caller to implement any required scheduling inside their execution pipeline. This means it is entirely possible for your execution logic to execute on a thread other than that owned by the provided scheduler:
>
> ```cs
> var command = ReactiveCommand.Create(() => Console.WriteLine(Environment.CurrentManagedThreadId), outputScheduler: RxApp.MainThreadScheduler);
>
> // this will output the ID of the thread from which you make this call, not necessarily the ID of the main thread!
> command.Execute().Subscribe();
> ```

All `Create*` methods take an optional `outputScheduler` parameter, so you can pass in a custom scheduler if you need to:

```cs
var command = ReactiveCommand.Create(() => {}, outputScheduler: someScheduler);
```

> **Note** If you're using ReactiveUI's `With` extension method in your tests, you can create commands using the default scheduling behavior. That's because the `With` extension method will switch out `RxApp.MainThreadScheduler` with the scheduler you provide it.
