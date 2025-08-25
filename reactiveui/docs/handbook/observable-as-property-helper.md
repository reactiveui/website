# ObservableAsPropertyHelper (OAPH)

The ObservableAsPropertyHelper (OAPH) bridges IObservable<T> streams and read-only ViewModel properties. It exposes the latest value via a property and raises `INotifyPropertyChanging/Changed` notifications for you.

Key points:
- OAPH subscribes to the source observable and surfaces the latest value through `Value`.
- Delivery of notifications uses the scheduler you pass to `ToProperty` (defaults to `CurrentThreadScheduler`), so UI updates can be marshaled to `RxApp.MainThreadScheduler` when needed.
- You can supply an initial value and optionally defer subscription until the property is first read.
- Ideal for computed/read-only properties; use `RaiseAndSetIfChanged` for mutable properties.

## Basic Example
```csharp
readonly ObservableAsPropertyHelper<string> _firstName;
public string FirstName => _firstName.Value;

public MyViewModel()
{
    _firstName = this
        .WhenAnyValue(x => x.Name)
        .Where(n => !string.IsNullOrWhiteSpace(n))
        .Select(n => n.Split(' ')[0])
        .ToProperty(this, x => x.FirstName, scheduler: RxApp.MainThreadScheduler);
}
```

## ToProperty Overloads
`ToProperty` constructs an `ObservableAsPropertyHelper<T>` from an `IObservable<T>` and raises change notifications:
```csharp
public static ObservableAsPropertyHelper<TRet> ToProperty<TObj, TRet>(
    this IObservable<TRet> source,
    TObj owner,
    Expression<Func<TObj, TRet>> property,
    TRet initialValue = default,
    bool deferSubscription = false,
    IScheduler? scheduler = null)
```
- `initialValue`: Used before the first tick (or when deferring, until subscribed).
- `deferSubscription`: Subscribe on first property access (lazy).
- `scheduler`: Use `RxApp.MainThreadScheduler` for UI properties.

## nameof Optimization
Avoid expression compilation by using the `nameof` overload:
```csharp
_firstName = this
    .WhenAnyValue(x => x.Name)
    .Select(n => n.Split(' ')[0])
    .ToProperty(this, nameof(FirstName));
```

## Deferred Subscription
Delay work until the property is first read. Consider buffering the last value with `Replay(1)` if the source is hot.
```csharp
var status = GetStatus().Replay(1).RefCount();
_status = status.ToProperty(this, nameof(Status), deferSubscription: true);
```

## Error Handling
OAPH does not swallow errors. Ensure errors are handled upstream (e.g., `Catch`/`LoggedCatch`) or the subscription will terminate.

## Using Source Generators
ReactiveUI.SourceGenerators can generate OAPH-backed properties with `[ObservableAsProperty]`:
```csharp
using ReactiveUI.SourceGenerators;

public partial class StatusViewModel : ReactiveObject
{
    [ObservableAsProperty]
    private string _status;

    public StatusViewModel()
    {
        _statusHelper = StatusObservable().ToProperty(this, x => x.Status);
    }

    IObservable<string> StatusObservable() => Observable.Return("Ready");
}
```

## When To Use OAPH vs Property
- Use OAPH for computed/read-only values that derive from other observables or properties.
- Use `RaiseAndSetIfChanged` for writable state your ViewModel mutates directly.

## Advanced Notes
- OAPH behaves like a lazy/behavioral observable: it holds the last value and pushes changes on the configured scheduler.
- Combine with `WhenAnyValue`, `Select`, `Throttle`, `DistinctUntilChanged` for efficient UI updates.
