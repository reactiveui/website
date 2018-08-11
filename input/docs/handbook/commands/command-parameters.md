# Command Parameters

Optionally, your command's execution logic can take a parameter. To do this, you need only use an appropriate overload of `Create*` when creating your `ReactiveCommand`:

```cs
// A synchronous command taking a parameter.
var command1 = ReactiveCommand.Create<int, Unit>(
    x => Console.WriteLine($"Received parameter with type {x.GetType().Name}: {x}.")
);

// This outputs: "Received parameter with type Int32: 42"
command1.Execute(42);

// An asynchronous command taking a parameter.
var command2 = ReactiveCommand.CreateFromObservable<int, Unit>(x => Observable.Return(x)
    .Do(i => Console.WriteLine($"Received parameter with type {x.GetType().Name}: {x}."))
);

// This outputs: "Received parameter with type Int32: 42"
command2.Execute(42);
```

The parameter's type is captured as `TParam` in `ReactiveCommand<TParam, TResult>`. The type of both `command1` and `command2` above is `ReactiveCommand<int, Unit>`. Generally, you should avoid using command parameters. It is usually more appropriate for your view model to define properties for any state that your command's execution logic relies on.

