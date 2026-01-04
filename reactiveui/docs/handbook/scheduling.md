# Scheduling in ReactiveUI

Scheduling is a core part of writing any app that uses the Reactive Extensions, as all operations are deferred (i.e. run on other threads or on the UI thread). Schedulers allow apps to control what context code runs in, and it is important that libraries that run code on other threads are scheduler-aware. ReactiveUI provides two app-wide schedulers that should be used in-place of other schedulers such as the built-in Rx schedulers:

* **RxSchedulers.MainThreadScheduler** - This scheduler executes on the UI thread. On XAML-based platforms, this is equivalent to Dispatcher.BeginInvoke.

* **RxSchedulers.TaskpoolScheduler** - This scheduler executes code via the TPL taskpool. This is equivalent to Task.Run.

To use these two inbuilt schedulers use the `ObserveOn` operator in your Observable chain:

```cs
this.WhenAnyValue(x => x.MyImportantProperty).ObserveOn(RxSchedulers.MainThreadScheduler).Subscribe(x => ...);
```

To control where a `ReactiveControl` runs the `Subscribe` you can pass in a scheduler. By default it will use whatever the current thread's scheduler is, so if you initialize from the UI thread it will use that thread.

```cs
MyCommand = ReactiveCommand.Create<Unit, string>(_ => ...do stuff..., outputScheduler: RxApp.
MainThreadScheduler);
```

To control where a `ObservableAsPropertyHelper` triggers the `INotifyPropertyChanged` events from pass in a scheduler. By default it will use whatever the current thread's scheduler is, so if you initialize from the UI thread it will use that thread.

```cs
public class MyVm : ReactiveObject
{
  private readonly ObservableAsPropertyHelper<bool> _isRunning;

  public MyVm()
  {
    MyCommand = ReactiveCommand.Create<Unit, string>(_ => ...do stuff..., outputScheduler: RxSchedulers.MainThreadScheduler);
    _isRunning = MyCommand.IsExecuting.ToProperty(this, nameof(IsRunning), scheduler: RxSchedulers.MainThreadScheduler);  
  }

  public ReactiveCommand<Unit, string> MyCommand { get; }
  public bool IsRunning => _isRunning.Value;
```

**Note**: Often on the iOS platform you need to pass in the main thread scheduler, since the default scheduler may not be the correct one.

## When should I care about scheduling

You should try to attempt to remove all sources of concurrency other than scheduling via RxApp. This isn't always possible, but threads created via `new Thread()` or `Task.Run` can't be controlled in a unit test. The most straightforward way to fix these is by replacing them with `Observable.Start`:

### Old

```csharp
var result = await Task.Run(() => {
    int number = ThisCalculationTakesALongTime();
    return number;
});

Dispatcher.BeginInvoke(new Action(() => DoAThing()));
```

### New

```csharp
var result = await Observable.Start(() => {
    int number = ThisCalculationTakesALongTime();
    return number;
}, RxSchedulers.TaskpoolScheduler);

RxSchedulers.MainThreadScheduler.Schedule(() => DoAThing());
```

If you create a shared component, you should also consider allowing the scheduler being specified as an optional constructor parameter.

## Testing schedulers

In a unit test runner, by default, the `MainThreadScheduler` runs code immediately instead of on the (non-existent) UI thread. The `TaskpoolScheduler` is left unchanged by default. The best way to run under an alternate scheduler is via the `With` method, most often used with `TestScheduler`. This replaces both schedulers with the specified scheduler:

```csharp
new TestScheduler().With(sheduler => 
{
    // Code run in this block will have both RxSchedulers.MainThreadScheduler
    // and RxSchedulers.TaskpoolScheduler assigned to the new TestScheduler.
});
```
