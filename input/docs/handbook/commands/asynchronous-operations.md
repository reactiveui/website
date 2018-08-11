### Creating commands

To use ReactiveCommand, create it using one of the following methods:

* **Create** - Creates a standard ReactiveCommand with synchronous execution logic;
* **CreateFromObservable** - Creates a command from `IObservable<T>`;
* **CreateFromTask** - Creates a command from `Task` or `Task<T>`.

All of these methods will parameterize the resulting ReactiveCommand to be the return result of the method (i.e. if your async method returns `Task<String>`, your Command will be `ReactiveCommand<String>`). This means, that Subscribing to the Command itself returns the results of the async method as an Observable. 

### Synchronous operations

If your command is not CPU-intensive or I/O-bound then it probably makes sense to provide synchronous execution logic. You can do so by creating a command via `ReactiveCommand.Create`:

```cs
// Create a command with synchronous execution logic.
var command = ReactiveCommand.Create(
    () => Console.WriteLine("A command is invoked!")
);
```

### Asynchronous operations

One of the most important features of ReactiveCommand is its built-in facilities for orchestrating asynchronous operations. If your command's logic is CPU- or I/O-bound, you'll want to create an asynchronous ReactiveCommand command using `CreateFromObservable` or `CreateFromTask` methods. Here's a simple example:

```cs
// Create a command with asynchronous execution logic.
LoadUsersAndAvatars = ReactiveCommand.CreateFromTask(async () => 
{
    var users = await LoadUsers();
    foreach (var u in users) u.Avatar = await LoadAvatar(u.Id);
    return users;
});

// Update the UI with a new value.
LoadUsersAndAvatars
    .ObserveOn(RxApp.MainThreadScheduler)
    .ToProperty(this, x => x.Users, out users);

// Log all the exceptions.
LoadUsersAndAvatars
    .ThrownExceptions
    .Subscribe(ex => this.Log().WarnException("Failed to load users", ex));
```

ReactiveCommand itself doesn't guarantee that its results will be delivered on the UI thread, so extra `ObserveOn`s may be necessary. Use `.ObserveOn(RxApp.MainThreadScheduler)` if you'd like to update the UI.

It is important to know, that ReactiveCommand itself as an `IObservable` will never complete or OnError - errors that happen in the async method will instead show up on the `ThrownExceptions` property. If it is possible that your async method can throw an exception (and most can!), you **must** Subscribe to `ThrownExceptions` or the exception will be rethrown on the UI thread.

### Invoking commands

The best way to execute ReactiveCommands is via the `Execute` method:

```cs
// Create a command with asynchronous execution logic.
LoadUsersAndAvatars = ReactiveCommand.CreateFromTask(async () => 
{
    var users = await LoadUsers();
    foreach (var u in users) u.Avatar = await LoadAvatar(u.Id);
    return users;
});

// Invoke LoadUsersAndAvatars command using async/await syntax.
var results = await LoadUsersAndAvatars.Execute();
Console.WriteLine("You've got {0} users!", results.Count());
```

Regardless of whether your command is synchronous or asynchronous in nature, you execute it via the `Execute` method. You get back an observable that will tick the command's result value when execution completes. Synchronous commands will execute immediately, so the observable you get back will already have completed. The returned observable is behavioral though, so subscribing after the fact will still tick through the result value.

> **Warning** As is often the case with idiomatic Rx, the observable returned by `Execute` is cold. That is, nothing will happen unless something subscribes to it or `await`s it. In those cases where you're calling `Execute` directly, it's very important to remember that it's lazy.

For binding to XAML UI frameworks, the `ICommand.Execute(object)` method is still provided, but is deliberately hidden using explicit `ICommand` interface implementation, you should avoid using it.

> **Hint** Try not to execute commands in the ViewModel constructor. If commands are invoked in the constructor, your ViewModel classes become more difficult to test, because you always have to mock out the effects of calling that commands, even if the thing you are testing is unrelated. Instead, use <a href="https://reactiveui.net/docs/handbook/when-activated/">WhenActivated</a>.

At times it can be convenient to execute a command in response to some observable that isn't perhaps tied to a user interaction. For example, a feature that automatically saves the current document by executing a `ReactiveCommand` every 5 minutes. The `InvokeCommand` extension makes it easy to achieve this:

```cs
// Creates a hot Observable<T> that emits a new value every 5 
// minutes and invokes the SaveCommand<Unit, Unit>. Don't forget
// to dispose the subscription produced by InvokeCommand().
var interval = TimeSpan.FromMinutes(5);
Observable
    .Timer(interval, interval)
    .Select(time => Unit.Default)
    .InvokeCommand(this, x => x.SaveCommand);
```

> **Hint** `InvokeCommand` respects the command's executability. That is, if the command's `CanExecute` method returns `false`, `InvokeCommand` will not execute the command when the source observable ticks.

### Why CreateFromTask?

Since ReactiveCommand itself is an Observable, it's quite easy to invoke async actions based on a ReactiveCommand. Something like:

```cs
searchButton
    .SelectMany(async query => await ExecuteSearch(query))
    .ObserveOn(RxApp.MainThreadScheduler)
    .ToProperty(this, x => x.SearchResults, out searchResults);
```

However, while this pattern is approachable if you're handy with Rx, one thing that ends up being Difficult™ is to disable the Command itself when the search is running (i.e. to prevent more than one search from running at the same time). `CreateFromTask` does the work to make this happen for you.

Another difficult aspect of this code is that it can't handle exceptions - if `executeSearch` ever fails once, it will never signal again because of the Rx Contract. ReactiveCommand handles marshaling exceptions to the `ThrownExceptions` property, which can be handled.

### Common Patterns

This example from UserError also illustrates the canonical usage of `CreateFromTask`:

```cs
// When the command is invoked, LoadTweets will be run in the background.
LoadTweetsCommand = ReactiveCommand.CreateFromTask(LoadTweets)

// The result will be Observed on the Main thread, and ToProperty 
// will then store it in an Output Property named Tweets.
LoadTweetsCommand
    .ObserveOn(RxApp.MainThreadScheduler)
    .ToProperty(this, x => x.Tweets, out theTweets);

var errorMessage = "The Tweets could not be loaded";
var errorResolution = "Check your Internet connection";

// Any exceptions thrown by LoadTweets will end up being
// sent through ThrownExceptions. Use ObserveOn to specify
// a scheduler, use RxApp.MainThreadScheduler to
// deliver exceptions to the main thread.
LoadTweetsCommand.ThrownExceptions
    .Select(ex => new UserError(errorMessage, errorResolution))
    .Subscribe(error => UserError.Throw(error));
```

### Default Exception Handler

The default behaviour of ReactiveUI is to crash the application whenever an object that has a `ThrownExceptions` property doesn't have a subscription. Use <a href="https://reactiveui.net/docs/handbook/default-exception-handler/">RxApp.DefaultExceptionHandler</a> if you'd like to override this behaviour.
