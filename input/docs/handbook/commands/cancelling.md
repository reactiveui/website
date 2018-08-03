## Cancelling

If your command's execution logic can take a long time to complete, it can be useful to allow the execution to be cancelled. This cancellation support can be used internally by your view models, or exposed so that users have a say in the matter.

### Basic Cancellation

At its most primitive form, cancelling a command's execution involves disposing the execution subscription:

```cs
var subscription = someReactiveCommand
    .Execute()
    .Subscribe();

// this cancels the command's execution
subscription.Dispose();
```

However, this requires you to obtain, and keep a hold of the subscription. If you're using bindings to execute your commands you won't have access to the subscription.

### cancelling via Another Observable

Rx itself has intrinsic support for cancelling one observable when another observable ticks. It provides this via the `TakeUntil` operator:

```cs
var cancel = new Subject<Unit>();
var command = ReactiveCommand
    .CreateFromObservable(
        () => Observable
            .Return(Unit.Default)
            .Delay(TimeSpan.FromSeconds(3))
            .TakeUntil(cancel));

// somewhere else
command.Execute().Subscribe();

// this cancels the above execution
cancel.OnNext(Unit.Default);
```

Of course, you wouldn't normally create a subject specifically for cancellation. Normally you already have some other observable that you want to use as a cancellation signal. An obvious example is having one command cancel another:

```cs
public class SomeViewModel : ReactiveObject
{
    public SomeViewModel()
    {
        this.CancellableCommand = ReactiveCommand
            .CreateFromObservable(
                () => Observable
                    .Return(Unit.Default)
                    .Delay(TimeSpan.FromSeconds(3))
                    .TakeUntil(this.CancelCommand));
        this.CancelCommand = ReactiveCommand.Create(
            () => { },
            this.CancellableCommand.IsExecuting);
    }

    public ReactiveCommand<Unit, Unit> CancellableCommand
    {
        get;
        private set;
    }

    public ReactiveCommand<Unit, Unit> CancelCommand
    {
        get;
        private set;
    }
}
```

Here we have a view model with a command, `CancellableCommand`, that can be cancelled by executing another command, `CancelCommand`. Notice how `CancelCommand` can only be executed when `CancellableCommand` is executing.

> **Note** At first glance there may appear to be an irresolvable circular dependency between `CancellableCommand` and `CancelCommand`. However, note that `CancellableCommand` does not need to resolve its execution pipeline until it is executed. So as long as `CancelCommand` exists before `CancellableCommand` is executed, the circular dependency is resolved.

### Cancellation with the Task Parallel Library

Cancellation in the TPL is handled with `CancellationToken` and `CancellationTokenSource`. Rx operators that provide TPL integration will normally have overloads that will pass you a `CancellationToken` with which to create your `Task`. The idea of these overloads is that the `CancellationToken` you receive will be cancelled if the subscription is disposed. So you should pass the token through to all relevant asynchronous operations. `ReactiveCommand` provides similar overloads for `CreateFromTask`.

Consider the following example:

```cs
public class SomeViewModel : ReactiveObject
{
    public SomeViewModel()
    {
        this.CancellableCommand = ReactiveCommand
            .CreateFromTask(
                ct => this.DoSomethingAsync(ct));
    }

    public ReactiveCommand<Unit, Unit> CancellableCommand
    {
        get;
        private set;
    }

    private async Task DoSomethingAsync(CancellationToken ct)
    {
        await Task.Delay(TimeSpan.FromSeconds(3), ct);
    }
}
```

There are several important things to note here:

1. Our `DoSomethingAsync` method takes a `CancellationToken`
2. This token is passed through to `Task.Delay` so that the delay will end early if the token is cancelled.
3. We use an appropriate overload of `CreateFromTask` so that we have a token to pass through to `DoSomethingAsync`. This token will be automatically cancelled if the execution subscription is disposed.

The above code allows us to do something like this:

```cs
var subscription = viewModel
    .CancellableCommand
    .Execute()
    .Subscribe();

// this cancels the execution
subscription.Dispose();
```

But what if we want to cancel the execution based on an external factor, just as we did with observables? Since we only have access to the `CancellationToken` and not the `CancellationTokenSource`, it's not immediately obvious how we can achieve this.

Besides forgoing TPL completely \(which is recommended if possible, but not always practical\), there are actually quite a few ways to achieve this. Perhaps the easiest is to use `CreateFromObservable` instead:

```cs
public class SomeViewModel : ReactiveObject
{
    public SomeViewModel()
    {
        this.CancellableCommand = ReactiveCommand
            .CreateFromObservable(
                () => Observable
                    .StartAsync(ct => this.DoSomethingAsync(ct))
                    .TakeUntil(this.CancelCommand));
        this.CancelCommand = ReactiveCommand.Create(
            () => { },
            this.CancellableCommand.IsExecuting);
    }

    public ReactiveCommand<Unit, Unit> CancellableCommand
    {
        get;
        private set;
    }

    public ReactiveCommand<Unit, Unit> CancelCommand
    {
        get;
        private set;
    }

    private async Task DoSomethingAsync(CancellationToken ct)
    {
        await Task.Delay(TimeSpan.FromSeconds(3), ct);
    }
}
```

This approach allows us to use exactly the same technique as with the pure Rx solution discussed above. The difference is that our observable pipeline includes execution of TPL-based asychronous code.

