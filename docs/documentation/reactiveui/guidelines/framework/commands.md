# Commands

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/guidelines/guidelines.csproj).

A view can call a view model's method directly from a click handler. Nothing about that connection knows whether
the method can currently run, so the control stays enabled even when clicking it would do nothing useful. Binding
the control to a [command](../../handbook/commands/index.md) instead lets the command disable the control through
its own `CanExecute`.

## Bind a button to a command

**1. Create the command.** The example view model's `Add` command wraps an async method with
`ReactiveCommand.CreateFromTask`, gated on whether `NewTitle` holds any non-blank text.

**2. Bind the button to it with `BindCommand`.** The binding wires the button's click to `Execute()`. It also wires
the button's enabled state to `CanExecute`, so a click while the command cannot run does nothing.

```csharp
using TodoListViewModel viewModel = new(InMemoryTodoStore.CreateSeeded());
_ = await viewModel.Load.Execute();
using TodoListView view = new() { ViewModel = viewModel };
using IDisposable binding = view.BindCommand(viewModel, x => x.Add, v => v.AddButton);

view.AddButton.PerformClick(); // NewTitle is blank: CanExecute is false, so the button is disabled and nothing happens.
Console.WriteLine(viewModel.Items.Count);

viewModel.NewTitle = "Submit Ada's grade";
Task<TodoItem> added = viewModel.Add.FirstAsync().ToTask();
view.AddButton.PerformClick();
TodoItem item = await added;

Console.WriteLine(item.Title);
Console.WriteLine(viewModel.Items.Count);
```

```text
4
Submit Ada's grade
5
```

The first click, while `NewTitle` is blank, changes nothing: `Add`'s `CanExecute` reads `false`, so `BindCommand`
leaves the button disabled and the click reaches nothing. Setting `NewTitle` makes `CanExecute` read `true`, so the
second click runs the command and adds the item.

## What a click handler cannot do

Wiring a button's click event straight to a method has no `CanExecute` to disable it. The button below stays
enabled after the one item it can submit is gone, so a second click still reaches the method.

```csharp
GradeRepository repository = new();
Button submitButton = new();
submitButton.Click += (_, _) => repository.Submit();

submitButton.PerformClick();
submitButton.PerformClick();
```

```text
Submitted Ada: 92
Nothing to submit, but the button let the click through anyway.
```

`repository.Submit()` has to guard itself against being called with nothing left to do, and print a message when
it is. A `ReactiveCommand` moves that guard into `CanExecute`, so the button disables itself and the guard code
does not need to run at all.

## Why bind to a command

- `CanExecute` lets the view disable a control automatically, instead of the view model tracking an `IsEnabled`
  flag by hand.
- An asynchronous command marshals its result back to the scheduler you gave it, so a subscriber does not need to
  do that itself. [Asynchronous commands](asynchronous-commands.md) and
  [UI thread and schedulers](ui-thread-and-schedulers.md) cover this.
- `IsExecuting` and `ThrownExceptions` track the command's in-flight state and failures for you, in one place, for
  every control bound to it.
