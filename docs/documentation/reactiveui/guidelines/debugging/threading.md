# Thread troubleshooting

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/guidelines/guidelines.csproj).

The most common threading mistake in a ReactiveUI app is updating a bound property from a background thread.
On most XAML platforms (WPF, WinUI, WinForms) that throws an `InvalidOperationException` or a cross-thread
access error immediately. On Apple platforms it can instead cause erratic UI behavior or a race condition that
is hard to reproduce, without ever throwing.

## Find the source

[Disable Just My Code](disable-just-my-code.md) before you start. With it on, the debugger hides the framework
frames between your subscription and the crash, so you cannot see which observable triggered the update.

When the crash happens in a view's property setter, it usually means the view model changed on a background
thread and the binding pushed that change straight to the view. Walk the stack up from the setter until you
reach your own view model code; that is where the fix belongs.

## Only one WitnessOn per chain

Adding `WitnessOn(RxSchedulers.MainThreadScheduler)` after every step in a chain is a common first fix. It
works, but it schedules onto the main thread far more than the chain needs.

`AvoidWitnessOnAfterEveryStep` marshals after the fetch, again after a `Select`, and again after a second
`Select`:

```csharp
int fetched = await Signal.FromAsync(FetchLatestGradeAsync)
    .WitnessOn(RxSchedulers.MainThreadScheduler)
    .Select(static grade => grade)
    .WitnessOn(RxSchedulers.MainThreadScheduler)
    .Select(static grade => grade + 0)
    .WitnessOn(RxSchedulers.MainThreadScheduler)
    .FirstAsync();
```

`PreferWitnessOnAtTheBoundary` runs the same two `Select` steps first, then marshals once, right before the
value reaches the bound property:

```csharp
int fetched = await Signal.FromAsync(FetchLatestGradeAsync)
    .Select(static grade => grade)
    .Select(static grade => grade + 0)
    .WitnessOn(RxSchedulers.MainThreadScheduler)
    .FirstAsync();
```

Both set the same value here, since a console has no UI thread to crash:

```text
91
```

The difference is scheduling overhead: three hops onto the main thread against one. Identify the boundary where
your data moves from a background operation, such as a network or database call, to a UI update, and put
`WitnessOn` there and nowhere earlier.

## Let commands marshal their own results

`ReactiveCommand` marshals the results of `CreateFromTask` and `CreateFromObservable` to
`RxSchedulers.MainThreadScheduler` automatically, so a `Subscribe` against a command's results needs no
`WitnessOn` of its own.

```csharp
using ReactiveCommand<RxVoid, int> loadGrade = ReactiveCommand.CreateFromTask(FetchLatestGradeAsync);
using IDisposable subscription = loadGrade.Subscribe(grade => student.Grade = grade);

_ = await loadGrade.Execute();
Console.WriteLine(student.Grade);
```

```text
91
```

## At a glance

| Do | Instead of | Why |
|---|---|---|
| One `WitnessOn` at the boundary before `Subscribe` | `WitnessOn` after every step | Fewer hops onto the main thread. |
| Subscribe directly to a command's results | Marshaling a command's results yourself | The command already marshals them. |
| Turn off Just My Code before tracing a threading crash | Leaving it on | You see the framework frame that triggered the update. |
