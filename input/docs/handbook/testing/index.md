NoTitle: true
---
ReactiveUI includes a few tools to help testing, built on what Reactive Extensions for .NET already include. The utilities are included in the `ReactiveUI.Testing` NuGet package. Make sure to install it into your unit tests project.

## Custom Scheduler

> Scheduling, and therefore threading, is generally avoided in test scenarios as it can introduce race conditions which may lead to non-deterministic tests — [Intro to Rx @ Testing Rx](http://introtorx.com/Content/v1.0.10621.0/16_TestingRx.html#TestingRx)

By default, `ReactiveCommand` uses `RxApp.MainThreadScheduler` and `ObservableAsPropertyHelper` uses `CurrentThreadScheduler.Instance`, but this behavior can be easily overriden:

```cs
// Inject custom schedulers into your view model.
public LoginViewModel(IScheduler customMainThreadScheduler = null)
{
  // If custom main thread scheduler isn't initialized, fallback to default one.
  // For current thread scheduler, use CurrentThreadScheduler.Instance;
  // For task pool scheduler, use RxApp.TaskPoolScheduler.
  customMainThreadScheduler = customMainThreadScheduler ?? RxApp.MainThreadScheduler;

  // Initialize a reactive command with a custom output scheduler.
  _loginCommand = ReactiveCommand.CreateFromTask(
    async () => { /* login logic */ },
    outputScheduler: customMainThreadScheduler
  );

  // Initialize an OAPH with a custom output scheduler.
  _errorMessage = _loginCommand.ThrownExceptions
    .Select(exception => exception.Message)
    .ToProperty(this, x => x.ErrorMessage, 
      scheduler: customMainThreadScheduler);
}
```

If the view model needs multiple schedulers, you can use the `ISchedulerProvider` pattern described at [Testing Reactive Extensions](http://introtorx.com/Content/v1.0.10621.0/16_TestingRx.html) page:

```cs
public interface ISchedulerProvider
{
  IScheduler MainThread { get; }
  IScheduler CurrentThread { get; } 
  IScheduler TaskPool { get; } 
}
```

## Unit Tests

Then, in unit tests project, you can inject a `TestScheduler` instance that allows you to play with time. There are a few more utility classes to help handle testing, see details on the [API documentation site](../../../api/reactiveui.testing/). 

```cs
new TestScheduler().With(scheduler =>
{
  // Initialize a new view model using the TestScheduler.
  var model = new LoginViewModel(scheduler);
  
  // Play with time.
  scheduler.AdvanceByMs(2 * 100);
});
```

## Replacing Schedulers Without Injecting Them

The `With` method also replaces the schedulers ReactiveUI is using. This means, that both `RxApp.MainThreadScheduler` and `RxApp.TaskPoolScheduler` will stay replaced with `TestScheduler` until the `With` method returns. 

`ReactiveCommand`s use `RxApp.MainThreadScheduler` as an output scheduler by default, but OAPHs don't. OAPHs use `CurrentThreadScheduler.Instance`, which won't get replaced during test execution. 

> Just a judgement call for performance. It’s assumed that more often than not the observable that pipes into ToProperty is already ticking on the main thread, so scheduling work on the main thread is superfluous and degrades performance. — [Kent Boogaart @ You, I and ReactiveUI](https://kent-boogaart.com/you-i-and-reactiveui/)

That's why in some cases you may need to replace the default scheduler with the `RxApp.MainThreadScheduler` by hand to handle unit testing.

```cs
_errorMessage = _loginCommand.ThrownExceptions
  .Select(exception => exception.Message)
  .ToProperty(this, x => x.ErrorMessage, 
    scheduler: RxApp.MainThreadScheduler);
```

## Playing With Ticks

If you have an asynchronous scenario in your view model implementation (and you do, for sure), you can use `.AdvanceBy` method to play with ticks. See [AdvanceBy](http://introtorx.com/Content/v1.0.10621.0/16_TestingRx.html#AdvanceBy) docs for details. The example below provides a demo view model code and a unit test for it.

```cs
public sealed class LoginViewModel 
{
  private readonly ObservableAsPropertyHelper<bool> _isBusy;
  
  public LoginViewModel(
    IScheduler currentThread,
    IScheduler mainThread,
    IProvider provider)
  {
    // Create a command using an injected scheduler.
    Login = ReactiveCommand.CreateFromTask(
      () => provider.OAuth(), 
      outputScheduler: mainThread);

    // Create an OAPH using an injected scheduler.
    _isBusy = Login.IsExecuting
      .ToProperty(this, x => x.IsBusy, scheduler: currentThread);
  }
  
  public bool IsBusy => _isBusy.Value;
 
  public ReactiveCommand<Unit, Unit> Login { get; }
}
```

The example below uses [NSubstitute](https://github.com/nsubstitute/NSubstitute) to generate a stub for `IProvider` and [FluentAssertions](https://github.com/fluentassertions/fluentassertions) to check view model state.

```cs
[Fact]
public void ShouldBeBusyWhenLoggingIn() => new TestScheduler().With(scheduler =>
{
  // Use NSubstute to generate stubs and mocks.
  var provider = Substitute.For<IProvider>();
  var model = new LoginViewModel(scheduler, scheduler, provider);
  
  // IsBusy indicator should be false on init.
  model.IsBusy.Should().BeFalse();
  
  // A test needs to subscribe to a ReactiveCommand,
  // because the execution is lazy and won't trigger
  // with no subscribers.
  model.Login.Execute().Subscribe();

  // We advance our scheduler with two ticks.
  // On the first tick, IsExecuting emits a new value,
  // and the second tick invokes IsBusy.
  scheduler.AdvanceBy(2);    
  model.IsBusy.Should().BeTrue();
  
  // We advance our scheduler with two ticks.
  // On the first tick, IsExecuting emits a new value,
  // and the second tick invokes IsBusy. 
  scheduler.AdvanceBy(2);
  model.IsBusy.Should().BeFalse();
});
```
