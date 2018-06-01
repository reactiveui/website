## Combined Commands

At times it can be useful to have several commands aggregated into one. As an example, consider a browser that allows the user to clear individual caches \(browsing history, download history, cookies\), or clear all caches. There would be a command for clearing each individual cache, each of which might have its own logic to dictate the executability of the command. It would be onerous and error-prone to have to repeat or combine all this logic for the command that clears all caches. Combined commands provide an elegant means of addressing this situation:

```cs
IObservable<bool> canClearBrowsingHistory = ...;
var clearBrowsingHistoryCommand = ReactiveCommand.CreateFromObservable(
    this.ClearBrowsingHistory,
    canClearBrowsingHistory);

IObservable<bool> canClearDownloadHistory = ...;
var clearDownloadHistoryCommand = ReactiveCommand.CreateFromObservable(
    this.ClearDownloadHistory,
    canClearDownloadHistory);

IObservable<bool> canClearCookies = ...;
var clearCookiesCommand = ReactiveCommand.CreateFromObservable(
    this.ClearCookies,
    canClearCookies);

// combine all these commands into one "parent" command
var clearAllCommand = ReactiveCommand
    .CreateCombined(
        new [] { clearBrowsingHistoryCommand, clearDownloadHistoryCommand, clearCookiesCommand });
```

The combined command will execute the child commands asynchronously when executed.

The combined command respects the executability of all child commands. That is, if any child command cannot currently execute, neither can the combined command. In addition, it is also possible for you to pass in _extra_ executability logic when creating your combined command:

```cs
IObservable<bool> canClearAll = ...;
var clearAllCommand = ReactiveCommand
    .CreateCombined(
        new [] { clearBrowsingHistoryCommand, clearDownloadHistoryCommand, clearCookiesCommand },
        canClearAll);
```

In this case, `clearAllCommand` will only be executable if all child commands are executable _and_ the latest value from `canClearAll` is `true`.

> **Hint** All child commands provided to the `CreateCombined` method must be of the same type. You cannot combine, say, a `ReactiveCommand<Unit, Unit>` with a `ReactiveCommand<int, Unit>`. Nor can you combine, say, a `ReactiveCommand<Unit, Unit>` with a `ReactiveCommand<Unit, int>`. This is because all child commands will receive the parameter provided to the combined command, and the result of executing the combined command is a list of all child results.



