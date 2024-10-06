---
NoTitle: true
---
If your command's execution logic can take a long time to complete, it can be useful to allow the execution to be canceled. This cancelation support can be used internally by your view models, or exposed so that users have a say in the matter.

## Basic Cancelation

At its most primitive form, canceling a command's execution involves disposing the execution subscription:

```cs
var subscription = someReactiveCommand.Execute().Subscribe();

// This cancels the command's execution.
subscription.Dispose();
```

However, this requires you to obtain, and keep a hold of the subscription. If you're using bindings to execute your commands you won't have access to the subscription.

## Canceling via Another Observable

Rx itself has intrinsic support for canceling one observable when another observable ticks. It provides this via the `TakeUntil` operator:

```cs
var cancel = new Subject<Unit>();
var command = ReactiveCommand
    .CreateFromObservable(
        () => Observable
            .Return(Unit.Default)
            .Delay(TimeSpan.FromSeconds(3))
            .TakeUntil(cancel));

// Somewhere else.
command.Execute().Subscribe();

// This cancels the above execution.
cancel.OnNext(Unit.Default);
```

Of course, you wouldn't normally create a subject specifically for cancelation. Normally you already have some other observable that you want to use as a cancelation signal. An obvious example is having one command cancel another:

```cs
public class SomeViewModel : ReactiveObject
{
    public SomeViewModel()
    {
        this.CancelableCommand = ReactiveCommand
            .CreateFromObservable(
                () => Observable
                    .Return(Unit.Default)
                    .Delay(TimeSpan.FromSeconds(3))
                    .TakeUntil(this.CancelCommand));
        this.CancelCommand = ReactiveCommand.Create(
            () => { },
            this.CancelableCommand.IsExecuting);
    }

    public ReactiveCommand<Unit, Unit> CancelableCommand { get; }

    public ReactiveCommand<Unit, Unit> CancelCommand { get; }
}
```

Here we have a view model with a command, `CancelableCommand`, that can be canceled by executing another command, `CancelCommand`. Notice how `CancelCommand` can only be executed when `CancelableCommand` is executing.

> **Note** At first glance there may appear to be an irresolvable circular dependency between `CancelableCommand` and `CancelCommand`. However, note that `CancelableCommand` does not need to resolve its execution pipeline until it is executed. So as long as `CancelCommand` exists before `CancelableCommand` is executed, the circular dependency is resolved.

## Cancelation with the Task Parallel Library

Cancelation in the TPL is handled with `CancellationToken` and `CancellationTokenSource`. Rx operators that provide TPL integration will normally have overloads that will pass you a `CancellationToken` with which to create your `Task`. The idea of these overloads is that the `CancellationToken` you receive will be canceled if the subscription is disposed. So you should pass the token through to all relevant asynchronous operations. `ReactiveCommand` provides similar overloads for `CreateFromTask`.

Consider the following example:

```cs
public class SomeViewModel : ReactiveObject
{
    public SomeViewModel()
    {
        this.CancelableCommand = ReactiveCommand
            .CreateFromTask(
                ct => this.DoSomethingAsync(ct));
    }

    public ReactiveCommand<Unit, Unit> CancelableCommand { get; }

    private async Task DoSomethingAsync(CancellationToken ct)
    {
        await Task.Delay(TimeSpan.FromSeconds(3), ct);
    }
}
```

There are several important things to note here:

1. Our `DoSomethingAsync` method takes a `CancellationToken`
2. This token is passed through to `Task.Delay` so that the delay will end early if the token is canceled.
3. We use an appropriate overload of `CreateFromTask` so that we have a token to pass through to `DoSomethingAsync`. This token will be automatically canceled if the execution subscription is disposed.

The above code allows us to do something like this:

```cs
var subscription = viewModel
    .CancelableCommand
    .Execute()
    .Subscribe();

// This cancels the execution.
subscription.Dispose();
```

But what if we want to cancel the execution based on an external factor, just as we did with observables? Since we only have access to the `CancellationToken` and not the `CancellationTokenSource`, it's not immediately obvious how we can achieve this.

Besides forgoing TPL completely \(which is recommended if possible, but not always practical\), there are actually quite a few ways to achieve this. Perhaps the easiest is to use `CreateFromObservable` instead:

Ideally avoid using `Observable.FromAsync` with a cancelation token as Exceptions do not bubble as expected, Replace any calls that use this with a ReactiveCommand and initialise it with the `ReactiveCommand.CreateFromTask(async (ct) =>{});` method.

```cs
public class SomeViewModel : ReactiveObject
{
    public SomeViewModel()
    {
        // Handle class exceptions
        ThrownExceptions
            .Subscribe(ex => Console.Out.WriteLine("SomeViewModel threw:" + ex.Message));

        // Create a command to execute the asynchronous operation
        this.DoSomethingCommand = ReactiveCommand
            .CreateFromTask(ct => this.DoSomethingAsync(ct));

        // Create a command for bindings to execute the asynchronous operation
        // This can be skipped if you don't need to bind to the command and just want to execute it
        // i.e. var disposable = DoSomethingCommand.Execute().TakeUntil(this.CancelCommand).Subscribe();
        // This will execute the command and cancel it when the `CancelCommand` is executed but can also be canceled by disposing the disposable
        this.CancelableCommand = ReactiveCommand
            .CreateFromObservable(
                () => DoSomethingCommand.Execute()
                    .TakeUntil(this.CancelCommand));

        // Create a command to cancel the asynchronous operation
        this.CancelCommand = ReactiveCommand
            .Create(() => { },
            this.CancelableCommand.IsExecuting);

        // Handle exceptions
        CancelableCommand.ThrownExceptions
            .Subscribe(ex => Console.Out.WriteLine("CancelableCommand threw:" + ex.Message));
        DoSomethingCommand.ThrownExceptions
            .Subscribe(ex => Console.Out.WriteLine("DoSomethingCommand threw:" + ex.Message));
    }

    public ReactiveCommand<Unit, Unit> CancelableCommand { get; }

    public ReactiveCommand<Unit, Unit> DoSomethingCommand { get; }

    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    private async Task DoSomethingAsync(CancellationToken ct)
    {
        await Task.Delay(TimeSpan.FromSeconds(3), ct);
    }
}
```

This approach allows us to use exactly the same technique as with the pure Rx solution discussed above. The difference is that our observable pipeline includes execution of TPL-based asychronous code.
