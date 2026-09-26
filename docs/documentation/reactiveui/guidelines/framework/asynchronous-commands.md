# Asynchronous commands

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/guidelines/guidelines.csproj).

A [command](../../handbook/commands/index.md) reports whether it can run, whether it is running, and what it fails
with. Those reports come from the command's own work. Start that work from a plain `Subscribe` instead of from the
command, and the command has nothing to report. `IsExecuting` finishes as soon as the command's synchronous action
returns. `ThrownExceptions` never sees a failure. And nothing stops a second click from starting the work again
while the first run is still going.

## Let the command do the work

**1. Create the command with `ReactiveCommand.CreateFromTask`.** Pass the async method directly, so the command's
own action is the work, not a wrapper around it.

```csharp
using ReactiveCommand<RxVoid, RxVoid> submit = ReactiveCommand.CreateFromTask(SubmitGradesAsync);
using IDisposable errorSubscription = submit.ThrownExceptions.Subscribe(static ex => Console.WriteLine($"error: {ex.Message}"));

using IDisposable executeSubscription = submit.Execute().Subscribe(static _ => { });
bool executingRightAfterTheClick = await submit.IsExecuting.FirstAsync();
await Task.Delay(50);

Console.WriteLine(executingRightAfterTheClick);
Console.WriteLine(Volatile.Read(ref _submitCount));
```

```text
True
1
```

**2. Read `IsExecuting`.** It reads `true` right after the click, because the command's action, `SubmitGradesAsync`,
is still running. `ThrownExceptions` is ready to catch anything that method throws, and `CanExecute` reads `false`
while it runs, so a second click before the first finishes does nothing.

## What goes wrong with Subscribe

The command below looks similar: it creates a command and calls an async method. The method call just happens
inside `Subscribe` instead of being the command's own action.

```csharp
using ReactiveCommand<RxVoid, RxVoid> submit = ReactiveCommand.Create(static () => { });
using IDisposable subscription = submit.Subscribe(static unused => _ = SubmitGradesAsync());

submit.Execute().Subscribe(static _ => { });
bool executingRightAfterTheClick = await submit.IsExecuting.FirstAsync();
await Task.Delay(50); // give the fire-and-forget work time to finish

Console.WriteLine(executingRightAfterTheClick);
Console.WriteLine(Volatile.Read(ref _submitCount));
```

```text
False
1
```

`IsExecuting` reads `false` immediately, even though `SubmitGradesAsync` is still running when that read happens:
the command's own action, the empty lambda passed to `ReactiveCommand.Create`, already finished. `ThrownExceptions`
subscribed the same way would never see a failure from `SubmitGradesAsync`, because the command never runs it.
`CanExecute` would read `true` throughout, so nothing stops a second click from starting a second, overlapping
submission.

## At a glance

| Symptom | Cause | Fix |
| --- | --- | --- |
| `IsExecuting` finishes immediately | The async call runs inside `Subscribe`, not the command's own action | Pass the async method to `ReactiveCommand.CreateFromTask` |
| `ThrownExceptions` never fires | The command's action cannot see an exception thrown after it returns | Let the command's action be the code that can throw |
| A second click starts overlapping work | `CanExecute` only reflects the command's own action | Use an async command, whose `CanExecute` reads `false` while it runs |
