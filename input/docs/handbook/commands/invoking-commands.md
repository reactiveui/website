# Invoking Commands

At times it can be convenient to execute a command in response to some observable that isn't perhaps tied to a user interaction. For example, a feature that automatically saves the current document by executing a `SaveCommand` every 5 minutes. The `InvokeCommand` extension makes it easy to achieve this:

```cs
var interval = TimeSpan.FromMinutes(5);
Observable
    .Timer(interval, interval)
    .InvokeCommand(this.ViewModel, x => x.SaveCommand);
```

> **Hint** `InvokeCommand` respects the command's executability. That is, if the command's `CanExecute` method returns `false`, `InvokeCommand` will not execute the command when the source observable ticks.
