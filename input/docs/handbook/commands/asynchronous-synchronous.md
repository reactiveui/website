## Asynchronous versus Synchronous Commands

Even though the API presented by `ReactiveCommand` is asynchronous, you are not required to perform your execution logic asynchronously. If your command is not CPU-intensive or I/O-bound then it probably makes sense to provide synchronous execution logic. You can do so by creating a command via `ReactiveCommand.Create`:

```cs
var command = ReactiveCommand.Create(() => Console.WriteLine("a synchronous reactive command));
```

There are several overloads of `Create` to facilitate commands that take parameters or return interesting values when they execute. These will be discussed in more detail below.

If, on the other hand, your command's logic _is_ CPU- or I/O-bound, you'll want to use `CreateFromObservable` or `CreateFromTask`:

```cs
// here we're using observables to model asynchrony
var command1 = ReactiveCommand.CreateFromObservable(() => Observable.Return(Unit.Default).Delay(TimeSpan.FromSeconds(3)));

// here we're using the TPL to model asynchrony
var command2 = ReactiveCommand.CreateFromTask(async () =>
    {
        await Task.Delay(TimeSpan.FromSeconds(3)); 
    });
```

Again, several overloads exist for commands taking parameters and returning values.

Regardless of whether your command is synchronous or asynchronous in nature, you execute it via the `Execute` method. You get back an observable that will tick the command's result value when execution completes. Synchronous commands will execute _immediately_, so the observable you get back will already have completed. The returned observable is behavioral though, so subscribing after the fact will still tick through the result value.

ReactiveCommand has built-in support for background operations and guarantees that this block will only run exactly once at a time, and that the CanExecute will auto-disable and that property IsExecuting will be set accordingly whilst it is running.

> **Warning** As is often the case with idiomatic Rx, the observable returned by `Execute` is cold. That is, nothing will happen unless something subscribes to it. This subscription is often instigated by the binding infrastructure. But in those cases where you're calling `Execute` directly, it's very important to remember that it's lazy.



