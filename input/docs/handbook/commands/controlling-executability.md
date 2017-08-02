# Controlling Executability

Reactive commands automatically become unavailable whilst they're executing. That is, their `CanExecute` observable will tick `false` when execution commences, and then `true` once execution completes. However, there are times you will want to further control a command's executability. For example, you might want a login command to become disabled until the user has entered both a user name and a password.

To do this, you can pass in an `IObservable<bool>` for the `canExecute` parameter when you create the command:

```cs
var canExecute = this
    .WhenAnyValue(
        x => x.UserName,
        x => x.Password,
        (u, p) => !string.IsNullOrEmpty(u) && !string.IsNullOrEmpty(p));
var command = ReactiveCommand.CreateFromObservable(this.LogOnAsync, canExecute);
```

Here, `command` has additional constraints on its executability. Namely, both `UserName` and `Password` must be provided. Of course, `command` is still unavailable during execution of `LogOnAsync`, even if `UserName` and `Password` are both currently valid. In other words, `canExecute` *supplements* the default executability behavior; it doesn't replace it.

> **Warning** For performance reasons, `ReactiveCommand` does not marshal your `canExecute` observable to the main scheduler. You almost certainly want your `canExecute` observable to be ticking on the main thread, so be sure to add a call to `ObserveOn` if necessary.
