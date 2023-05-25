NoTitle: true
---
`ReactiveCommand` is a Reactive Extensions and asynchronous aware implementation of the [`ICommand`](https://msdn.microsoft.com/en-us/library/system.windows.input.icommand.aspx) interface. `ICommand` is often used in the [MVVM design pattern](https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/commanding-overview) to allow the View to trigger business logic defined in the ViewModel. This allows for easier maintenance, unit testing, and the ability to reuse ViewModels across different UI frameworks. Examples of where a View might invoke a command include clicking a *Save* menu item, tapping a phone icon, or stretching an image. In these cases, the ViewModel will then invoke the business logic of saving outstanding changes, performing a phone call, or zooming into an image.

## Creating commands

A `ReactiveCommand` is created using static factory methods which allows you to create command logic that executes either synchronously or asynchronously. The following are the different static factory methods:

* `CreateFromObservable()` - Execute the logic using an `IObservable`.
* `CreateFromTask()` - Execute a C# [Task Parallel Library (TPL)](https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-based-asynchronous-programming) Task. This allows use also of the C# [async/await](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/async) operators. Read more on canceling commands [here](./canceling).
* `Create()` - Execute a synchronous Func or Action.
* `CreateCombined()` - Execute one or more commands. Read more on combining commands [here](#combining-commands).

`ReactiveCommand<TInput, TOutput>` adds the concept of Input and Output generic types. The *Input* is often passed in by the View and it's type is captured as `TInput`, and the *Output* is the result of executing the command which type is captured as `TOutput`. `ReactiveCommand<TInput, TOutput>` is `IObservable<TOutput>` which can be used like any other `IObservable`. For example, since the `ReactiveCommand` is `IObservable` you can `Subscribe()` to it like any other observable, and add the output to a List on your view model. The `Unit` type is a functional programming construct analogous to void and can be used in cases where you don't care about either the input and/or output value.

```cs
// A synchronous command taking a parameter and returning nothing.
// The Unit type is often used to denote the successfull completion
// of a void-returning method (C#) or a sub procedure (VB).
ReactiveCommand<int,Unit> command = ReactiveCommand.Create<int>(
    integer => Console.WriteLine(integer));

// This outputs: 42
command.Execute(42).Subscribe();
```

All of the static factory methods that the `ReactiveCommand` class has will parameterize the resulting `ReactiveCommand<TInput, TOutput>` to be the return result of the method (i.e. if your async method returns `Task<string>`, your command will be `ReactiveCommand<TInput, string>`). This means, that subscribing to the command itself returns the results of the async method as an `IObservable`.

```cs
// An asynchronous command created from IObservable<int> that 
// waits 2 seconds and then returns 42 integer.
var command = ReactiveCommand.CreateFromObservable<Unit, int>(
    _ => Observable.Return(42).Delay(TimeSpan.FromSeconds(2)));

// Subscribing to the observable returned by `Execute()` will 
// tick through the value `42` with a 2-second delay.
command.Execute(Unit.Default).Subscribe();

// We can also subscribe to _all_ values that a command
// emits by using the `Subscribe()` method on the
// ReactiveCommand itself.
command.Subscribe(value => Console.WriteLine(value));
```

## Synchronous commands

If your command is not CPU-intensive or I/O-bound then it probably makes sense to provide synchronous execution logic. You can do so by creating a command via `ReactiveCommand.Create`:

```cs
// Creates a command with synchronous execution logic
// which is always available for execution.
var command = ReactiveCommand.Create(
    () => Console.WriteLine("A reactive command is invoked!")
);
```

## Asynchronous commands

One of the most important features of `ReactiveCommand` is its built-in facilities for orchestrating asynchronous operations, commands will block re-execution while executing. `ReactiveCommand`s are fully integrated into the Reactive Extensions framework, providing an `.IsExecuting` property (of type `IObservable<bool>`) which tells you whether the command is currently executing. This is often useful if you want to trigger activity animations or you want to prevent other commands from executing while the command is executing.

It is important to know, that ReactiveCommand itself as an `IObservable` will never complete or OnError - errors that happen in the async method will instead show up on the `ThrownExceptions` property. If it is possible that your async method can throw an exception, you should subscribe to `ThrownExceptions` or the exception will be rethrown on the UI thread.

Three methods are provided for creating asynchronous commands:

* `CreateFromObservable()` - Execute the logic using an `IObservable`.
* `CreateFromTask()` - Execute a C# [Task Parallel Library (TPL)](https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-based-asynchronous-programming) Task. This allows use also of the C# [async/await](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/async) operators. Read more on canceling commands [here](./canceling).
* `CreateRunInBackground()` - Execute a method on a background thread allowing UI status to update.

```cs
// Here we declare a ReactiveCommand, an OAPH and a property.
private readonly ObservableAsPropertyHelper<List<User>> _users;
public ReactiveCommand<Unit, List<User>> LoadUsers { get; }
public List<User> Users => _users.Value;

// Create a command with asynchronous execution logic. The 
// command is always available for execution, it triggers
// LoadUsersAsync method which returns Task<List<User>>.
LoadUsers = ReactiveCommand.CreateFromTask(LoadUsersAsync);

// Update the UI with a new value when users are loaded.
// ToProperty extension method allows us to subscribe to 
// LoadUsers Observable, update our OAPH and notify the UI 
// that the value of Users property has changed.
_users = LoadUsers.ToProperty(
    this, x => x.Users, scheduler: RxApp.MainThreadScheduler);

// Here we subscribe to all exceptions thrown by our 
// command and log them using ReactiveUI logging system.
// If we forget to do this, our application will crash
// if anything goes wrong in LoadUsers command.
LoadUsers.ThrownExceptions.Subscribe(exception => 
{
    this.Log().Warn("Error!", exception);
});
```

> **Note** For performance based solutions you can also use the nameof() operator override of ToProperty() which won't use the Expression. Read more on ObservableAsPropertyHelper [here](../observable-as-property-helper).

`ReactiveCommand` guarantees the result of events are delivered to the provided `outputScheduler`. The executing logic thread safety is the user's responsibility but any result from the logic is guaranteed to arrive on the specified `outputScheduler`. Read more on scheduling [here](#controlling-scheduling).

## Controlling executability

A `ReactiveCommand` may or may not be executable in a given situation. For example, the command backing the *Save* menu item might be unavailable if there are no unsaved changes. We pass into the `ReactiveCommand` an `IObservable<bool>` of when the ReactiveCommand should be allowed to be executed. The `ReactiveCommand` uses an IObservable eventing system to determine if execution should be allowed which differs from other frameworks where you might have the command continuous poll if execution is allowed. The ReactiveCommand approach has some performance advantages in that the value is cached between the can execute observable being fired. You commonly will create your can execute observable using the [`WhenAnyValue` functions](../when-any) provided by the ReactiveUI framework: 

```cs
// Each time values of UserName and Password properties change,
// the canExecute observable is signalled and the logic that 
// determines if command execution should be allowed is executed.
var canExecute = this.WhenAnyValue(
    x => x.UserName, x => x.Password,
    (userName, password) => 
        !string.IsNullOrEmpty(userName) && 
        !string.IsNullOrEmpty(password));

// The command will be unavailable during execution of LogOnAsync
// method, or while UserName and Password ViewModel properties are 
// nulls or empty strings. In other words, canExecute supplements 
// the default executability behavior, it doesn't replace it.
var command = ReactiveCommand.CreateFromTask(LogOnAsync, canExecute);
```

Parameters, unlike in other frameworks, are typically *not used* in the canExecute conditions, instead, binding View properties to ViewModel properties and then using the `WhenAnyValue()` is far more common.

> **Warning** For performance reasons, `ReactiveCommand` does not marshal your `canExecute` observable to the main scheduler. You almost certainly want your `canExecute` observable to be ticking on the main thread, so be sure to add a call to `ObserveOn` if necessary.

## Handling exceptions

If the logic you provide to a `ReactiveCommand` can fail in expected ways, you need a means of dealing with those failures. For command execution, the pipeline you get back from `Execute` will tick any errors that occur in your execution logic. However, the subscription to this observable is often instigated by the binding infrastructure. As such, it's likely that you cannot even get a hold of the observable to observe any errors.

To address this dilemma, `ReactiveCommand` includes a `ThrownExceptions` observable (of type `IObservable<Exception>`). Any errors that occur in your execution logic will *also* tick through this observable. If you haven't subscribed to it, ReactiveUI will bring down your application by default. This forces you towards a pit of error-handling success.

```cs
// Here we prevent LoadCommand from bringing our app down.
LoadCommand.ThrownExceptions.Subscribe(error => { });
```

Use <a href="https://reactiveui.net/docs/handbook/default-exception-handler/">RxApp.DefaultExceptionHandler</a> if you'd like to override the default `ThrownExceptions` behaviour. This may be useful if you have crash analytics plugins installed and would like to handle all exceptions. 

It can be tempting to *always* add a subscription to `ThrownExceptions`, even if the only recourse is to just log the problem. However, it is advisable to treat this like any other exception handling and only handle problems you can redress. If, for example, your command merely updates a property in your view model and it should never fail, any subscription to `ThrownExceptions` will serve only to obscure implementation problems. That said, be aware of the potential for intermittent problems, such as network and I/O errors. As always, a strong suite of tests will help you identify where a subscription to `ThrownExceptions` makes sense.

> **Note** Your `canExecute` pipeline also has the potential to produce an error. Such cases are almost certainly a programmer error because you never want your `canExecute` pipeline to end in error. Even so, these errors will also tick through `ThrownExceptions`.

Unfortunately you can't filter exceptions from `ThrownExceptions`. It's an "all-or-nothing affair" as Kent Boogaart says in <a href="https://kent-boogaart.com/you-i-and-reactiveui/">his book</a>. You have to do it manually by handling the exceptions you can and forwarding all the others somewhere else like the `RxApp.DefaultExceptionHandler`. Use `RxApp.DefaultExceptionHandler.OnNext(exceptionICantHandle)`.

Assume your command calls another command that might throw an exception. Likely, you would like to handle that exception only once, but ReactiveUI will propagate it to `ThrownExceptions` observables for both commands. See an example:

```cs
// Create a command with implementation that might throw an exception.
var commandA = ReactiveCommand.CreateFromTask(() => throw new Exception());
commandA.ThrownExceptions.Subscribe(ex => ErrorInteraction.Handle("Error in A!"));

// Create a command that calls command A.
var commandB = ReactiveCommand.CreateFromTask(async () =>
{
    // If command A throws an exception, the .Execute() method call 
    // will also throw. That's why we get two error notifications - 
    // one from commandA.ThrownExceptions and another from 
    // commandB.ThrownExceptions.
    await commandA.Execute(); // <= Could throw here!
    DoSomethingElse();
});

// If anything goes wrong in command A, we get ErrorInteraction handled twice.
commandB.ThrownExceptions.Subscribe(ex => ErrorInteraction.Handle("Error in B!"));
```

The easiest way of resolving this issue is using `Throttle()` operator over merged `ThrownExceptions` from both commands. See [StackOverflow](https://stackoverflow.com/questions/26219105/what-is-the-reactiveui-way-to-handle-exceptions-when-executing-inferior-reactive). Read more on handling Interactions [here](../interactions).

```cs
// Now our ErrorInteraction will be handled only once if command A throws!
commandA.ThrownExceptions.Merge(commandB.ThrownExceptions)
    .Throttle(TimeSpan.FromMilliseconds(250), RxApp.MainThreadScheduler)
    .Subscribe(error => ErrorInteraction.Handle("Error in B!"));
```

## Invoking commands

The best way to execute ReactiveCommands is via the `Execute()` method:

```cs
// Create a command with asynchronous execution logic.
LoadUsers = ReactiveCommand.CreateFromTask(LoadUsersAsync);

// Invoke LoadUsers command using async/await syntax.
// We could also use .Subscribe() here.
var users = await LoadUsers.Execute();
Console.WriteLine("You've got {0} users!", users.Count());
```

Regardless of whether your command is synchronous or asynchronous in nature, you execute it via the `Execute` method. You get back an observable that will tick the command's result value when execution completes. Synchronous commands will execute immediately, so the observable you get back will already have completed. The returned observable is behavioral though, so subscribing after the fact will still tick through the result value.

> **Warning** As is often the case with idiomatic Rx, the observable returned by `Execute` is cold. That is, nothing will happen unless something subscribes to it or `await`s it. In those cases where you're calling `Execute` directly, it's very important to remember that it's lazy.

`ReactiveCommand` implements the `ICommand` for UI framework compatibility and backwards compatibility only. It is recommended you don't use the `ICommand` interface directly in your code. `ReactiveCommand` is explicitly derived from the `ICommand` interface to avoid users accidentally calling the non-reactive style methods. The `ICommand` methods do not lend well to long-running and also asynchronous commands, such as those that perform I/O operations. The `ICommand` also focuses on an imperative style of execution over the reactive style.`ReactiveCommand` provides methods and observable properties that are the equivalent of the `ICommand` interface. `Execute()` provides an Observable which you can `Subscribe()` to execute the logic of the `ReactiveCommand` and `CanExecute` is also exposed through a read-only property. Additionally `ReactiveCommand` provides the `IsExecuting` observable which is functionally not provided by the `ICommand` interface.

> **Hint** Try not to execute commands in the ViewModel constructor. If commands are invoked in the constructor, your ViewModel classes become more difficult to test, because you always have to mock out the effects of calling that commands, even if the thing you are testing is unrelated. Instead, use <a href="https://reactiveui.net/docs/handbook/when-activated/">WhenActivated</a>.

## Invoking commands in an Observable pipeline

At times it can be convenient to execute a command in response to some `Observable<T>` that isn't perhaps tied to a user interaction. For example, a feature that automatically saves the current document by executing a `ReactiveCommand` every 5 minutes. The `InvokeCommand` extension makes it easy to achieve this:

```cs
// Creates a hot Observable<T> that emits a new value every 5 
// minutes and invokes the SaveCommand<Unit, Unit>. Don't forget
// to dispose the subscription produced by InvokeCommand().
var interval = TimeSpan.FromMinutes(5);
Observable.Timer(interval, interval)
    .Select(time => Unit.Default)
    .InvokeCommand(this, x => x.SaveCommand);
```

> **Hint** `InvokeCommand` respects the command's executability. That is, if the command's `CanExecute` method returns `false`, `InvokeCommand` will not execute the command when the source observable ticks.

## Combining commands

At times it can be useful to have several commands aggregated into one. As an example, consider a browser that allows the user to clear individual caches \(browsing history, download history, cookies\), or clear all caches. There would be a command for clearing each individual cache, each of which might have its own logic to dictate the executability of the command. It would be onerous and error-prone to have to repeat or combine all this logic for the command that clears all caches. Combined commands provide an elegant means of addressing this situation:

```cs
var clearBrowsingHistory = ReactiveCommand.CreateFromObservable(
    this.ClearBrowsingHistoryAsync, canClearBrowsingHistory);

var clearDownloadHistory = ReactiveCommand.CreateFromObservable(
    this.ClearDownloadHistoryAsync, canClearDownloadHistory);

var clearCookies = ReactiveCommand.CreateFromObservable(
    this.ClearCookiesAsync, canClearCookies);

// Combine all these commands into one "parent" command.
// This "parent" command will respect the executability 
// of all child commands defined above.
var clearAll = ReactiveCommand.CreateCombined(
    new [] { clearBrowsingHistory, 
             clearDownloadHistory, 
             clearCookies });
```

The combined command will execute the child commands asynchronously when executed. The combined command respects the executability of all child commands. That is, if any child command cannot currently execute, neither can the combined command. In addition, it is also possible for you to pass in _extra_ executability logic when creating your combined command:

```cs
// In this case, `clearAll` command will only be 
// executable if all child commands are executable 
// _and_ the latest value from `canClearAll` is `true`.
IObservable<bool> canClearAll = ...;
var clearAll = ReactiveCommand.CreateCombined(
    new [] { clearBrowsingHistory, 
             clearDownloadHistory, 
             clearCookies },
    canClearAll);
```

All child commands provided to the `CreateCombined` method must be of the same type. You cannot combine, say, a `ReactiveCommand<Unit, Unit>` with a `ReactiveCommand<int, Unit>`. Nor can you combine, say, a `ReactiveCommand<Unit, Unit>` with a `ReactiveCommand<Unit, int>`. This is because all child commands will receive the parameter provided to the combined command, and the result of executing the combined command is a list of all child results.

## Controlling scheduling

By default, `ReactiveCommand` uses `RxApp.MainThreadScheduler` to surface events. That is, values from `CanExecute`, `IsExecuting`, `ThrownExceptions`, and result values from the command itself. Typically UI components are subscribed to these observables, so it's a sensible default. However, when writing unit tests for your view models, you may want more control over scheduling. All `Create*` methods take an optional `outputScheduler` parameter, so you can pass in a custom scheduler if you need to:

```cs
var command = ReactiveCommand.Create(() => { }, outputScheduler: someScheduler);
```

It's important to understand that the execution logic for a reactive command is *not* scheduled to execute on the provided scheduler (just as is the case for any `canExecute` observable you provide). Instead, it is left to the caller to implement any required scheduling inside their execution pipeline. This means it is entirely possible for your execution logic to execute on a thread other than that owned by the provided scheduler:

```cs
var command = ReactiveCommand.Create(
    () => Console.WriteLine(Environment.CurrentManagedThreadId), 
    outputScheduler: RxApp.MainThreadScheduler
);

// This will output the ID of the thread from which you make 
// this call, not necessarily the ID of the main thread!
command.Execute().Subscribe();
```

> **Note** If you're using ReactiveUI's `With` extension method in your tests, you can create commands using the default scheduling behavior. That's because the `With` extension method will switch out `RxApp.MainThreadScheduler` with the scheduler you provide it.

## Bindings

`ReactiveCommand` can be connected to the View by either using XAML binding on supported platforms, or using the inbuilt [ReactiveUI binding](../data-binding) method `BindCommand`. Use of BindCommand is preferred but not required where XAML bindings are supported. Read more on this [here](./binding-commands).

## Unit Testing

Read: [Using the Visual Studio Test Runner for Mobile Development](https://kent-boogaart.com/blog/using-the-visual-studio-test-runner-for-mobile-development)

Don't mock ReactiveCommands. ReactiveCommand itself is already designed around testability. Also, the likelihood that you will correctly mock ReactiveCommand semantics via Moq is pretty low, it's a pretty complicated class (and if you did, you would end up doing a ton of unnecessary work).
