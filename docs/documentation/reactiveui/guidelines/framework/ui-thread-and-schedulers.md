# UI thread and schedulers

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/guidelines/guidelines.csproj).

A **sequencer** is ReactiveUI's word for the thing an operator schedules work onto — a UI dispatcher, the
thread pool, or a test clock. `RxSchedulers.MainThreadScheduler` is the sequencer for the thread that owns
your views. Set a property a view binds to from any other thread, and most XAML platforms throw or corrupt
the view.

## Marshal at the boundary

`WitnessOn(RxSchedulers.MainThreadScheduler)` marks the point where a value crosses onto the main thread.
Everything after it runs there; everything before it can run wherever the source produced the value.

`PreferWitnessOnAtTheBoundary` fetches a grade on a background thread, then uses `WitnessOn` right before the
`Subscribe` that sets the bound property:

```csharp
using IDisposable subscription = Signal.FromAsync(FetchLatestGradeAsync)
    .WitnessOn(RxSchedulers.MainThreadScheduler)
    .Subscribe(grade =>
    {
        student.Grade = grade;
        completion.SetResult();
    });
```

Without it, the subscription still runs here, since a console has no UI thread to protect. The same code on a
UI platform can crash or corrupt the view:

```csharp
Student student = new("Ada", "Robotics Club");
int grade = await FetchLatestGradeAsync();
student.Grade = grade;
```

Both print the same value here:

```text
91
```

## Pass the scheduler to the operation instead

For an operation you write yourself, passing the sequencer in lets the operation deliver its own result there,
instead of adding a separate `WitnessOn` step afterward. `PreferPassingTheSchedulerToTheOperation` calls a
fetch that takes an `ISequencer` and schedules its own continuation on it:

```csharp
int grade = await FetchLatestGradeOnAsync(RxSchedulers.MainThreadScheduler);
student.Grade = grade;
```

This is worth doing for a longer-running or multi-step operation. It removes a `WitnessOn` step that would
otherwise sit between two steps that already run on the target sequencer.

## Only one WitnessOn, at the boundary

Adding `WitnessOn` after every step in a chain is a common instinct when chasing down a threading crash. It
stops the crash, but schedules more work onto the main thread than the chain needs. See [only one WitnessOn per
chain](../debugging/threading.md#only-one-witnesson-per-chain) for the same chain with three `WitnessOn` calls
against the one that needs only one.

## At a glance

| Do | Instead of | Why |
|---|---|---|
| `WitnessOn(RxSchedulers.MainThreadScheduler)` right before `Subscribe` | Subscribing with no marshaling | The update lands on the thread the view expects. |
| Pass `RxSchedulers.MainThreadScheduler` into a multi-step operation | A separate `WitnessOn` after it returns | One scheduling hop instead of two. |
