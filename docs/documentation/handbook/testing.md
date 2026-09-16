---
Order: 19
---
# Testing

View models wait: a command runs for a few seconds, a search waits for typing to stop. A test that waits in real time
is slow, and its result can change from run to run. ReactiveUI lets a test move time forward itself instead.

The tools are in the `ReactiveUI.Testing` package. Install it into your test project.

```csharp
using ReactiveUI;
using ReactiveUI.Primitives;
using ReactiveUI.Primitives.Concurrency;
using ReactiveUI.Primitives.Signals;
using ReactiveUI.Testing;
```

## Your first test with a virtual clock

A `VirtualClock` is a [sequencer](../primitives/scheduling.md) whose time only moves when you call `AdvanceBy`. Work
scheduled on it runs when the clock reaches the work's due time, on the test's thread.

**1. Let the view model take a sequencer.** Fall back to `RxSchedulers.MainThreadScheduler` when none is given. Pass
it to the command's `outputScheduler` and to `ToProperty`:

```csharp
public interface ILoginService
{
    IObservable<RxVoid> LogIn();
}

public sealed class LoginViewModel : ReactiveObject
{
    private readonly ObservableAsPropertyHelper<bool> _isBusy;

    public LoginViewModel(ILoginService service, ISequencer? sequencer = null)
    {
        sequencer ??= RxSchedulers.MainThreadScheduler;

        Login = ReactiveCommand.CreateFromObservable(service.LogIn, outputScheduler: sequencer);
        _isBusy = Login.IsExecuting.ToProperty(this, x => x.IsBusy, scheduler: sequencer);
    }

    public ReactiveCommand<RxVoid, RxVoid> Login { get; }

    public bool IsBusy => _isBusy.Value;
}
```

**2. Give the test a fake service that takes two seconds of clock time.** `Signal.After` sends one value after a delay,
on the sequencer you give:

```csharp
public sealed class FakeLoginService(ISequencer clock) : ILoginService
{
    public IObservable<RxVoid> LogIn() => Signal.After(TimeSpan.FromSeconds(2), clock).Select(_ => RxVoid.Default);
}
```

**3. Run the test inside `With`, and move the clock.** `With` also replaces `RxSchedulers.MainThreadScheduler` and
`RxSchedulers.TaskpoolScheduler` with the clock until the block ends:

```csharp
[Fact]
public void IsBusyWhileLoggingIn() => new VirtualClock().With(clock =>
{
    var model = new LoginViewModel(new FakeLoginService(clock), clock);
    Assert.False(model.IsBusy);

    // A command runs only when something subscribes to Execute.
    model.Login.Execute().Subscribe();

    clock.AdvanceBy(TimeSpan.FromSeconds(1));
    Assert.True(model.IsBusy);

    clock.AdvanceBy(TimeSpan.FromSeconds(2));
    Assert.False(model.IsBusy);
});
```

The test covers three seconds of login and finishes at once.

## Passing a sequencer, or replacing the defaults

There are two ways to get the clock into a view model:

- **Pass it in**, as `LoginViewModel` does. The test reads plainly, and nothing global changes.
- **Rely on `With`**, which swaps `RxSchedulers.MainThreadScheduler` and `RxSchedulers.TaskpoolScheduler` for the block.

`With` does not reach everything. A `ReactiveCommand` sends results through `RxSchedulers.MainThreadScheduler` by
default, so `With` covers it. An `ObservableAsPropertyHelper` with no `scheduler` raises `PropertyChanged` on the
thread its value arrived on, so `With` does not change it. Pass a sequencer to `ToProperty` when a test needs to control
it.

If a view model needs several sequencers, group them behind one interface and pass that instead:

```csharp
public interface ISequencerProvider
{
    ISequencer MainThread { get; }

    ISequencer TaskPool { get; }
}
```

In a test, return the same `VirtualClock` from both.

## More on virtual time

- [Testing with a virtual clock](../primitives/scheduling.md#testing-with-a-virtual-clock) covers `AdvanceTo`,
  `Start`, `Sleep` and the rest of `VirtualClock`.
- [Scheduling](scheduling.md) covers the two ReactiveUI sequencers.
