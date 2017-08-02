# Reactive Commands

Reactive commands also encapsulate logic to execute in response to user actions, but they do so via a reactive API. For example, instead of querying the command for its executability, you can use the `CanExecute` property, which is of type `IObservable<bool>`. Similarly, an `IsExecuting` property \(also of type `IObservable<bool>`\) tells you whether the command is currently executing. This could be a useful trigger to enable an activity animation, for example. And when you execute the command, you get back an `IObservable<TResult>`. The `TResult` is the "result" of executing your command \(often just `Unit` if your command doesn't need to return anything of importance\).

The fact that executing a command returns an observable of the result and not the result itself makes it clear that reactive commands are inherently asynchronous. You can kick off a CPU- or I/O-bound command without blocking your UI. Once completed, the result ticks through the observable and your UI can respond accordingly. While the command is executing, it is unavailable \(i.e. `CanExecute` will tick `false`\). Any UI elements bound to that command will automatically disable themselves while the command is executing.

Reactive commands are themselves observable. Whenever any execution of the command completes, the result will tick through the command itself.

An important truth of reactive commands is that they guarantee to deliver events on a given scheduler, but they make no attempt to marshal user-supplied pipelines \(including execution logic\) to that scheduler. Interacting with commands from the correct thread is therefore the responsibility of the caller. But any event you receive from a reactive command is guaranteed to be surfaced via the provided scheduler.

> **Hint** All reactive commands also implement `ICommand`, but do so explicitly. This is to encourage the use of the reactive APIs rather than the imperative ones provided by `ICommand`. However, it also allows reactive commands to integrate seamlessly into frameworks that work against `ICommand`.



