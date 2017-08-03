# Prefer Observable As Property Helpers to setting properties explicitly

When a property's value depends on another property, a set of properties, or an 
observable stream, rather than set the value explicitly, use 
`ObservableAsPropertyHelper` with `WhenAny` wherever possible.

## Do

```csharp
public class RepositoryViewModel : ReactiveObject
{
  public RepositoryViewModel()
  {
    canDoIt = this.WhenAny(x => x.StuffFetched, y => y.OtherStuffNotBusy, (x, y) => x && y)
      .ToProperty(this, x => x.CanDoIt);
  }

  readonly ObservableAsPropertyHelper<bool> canDoIt;
  public bool CanDoIt
  {
    get { return canDoIt.Value; }  
  }	
}
```

## Don't

```csharp
this.WhenAny(x => x.StuffFetched, y => y.OtherStuffNotBusy, (x, y) => x && y)
  .ObserveOn(RxApp.MainThreadScheduler)
  .Subscribe(x => CanDoIt = x);
```

## Why?
1. `ObservableAsPropertyHelper` is as a kind of "proof" that a given property has one source of change (the pipeline against which you call `ToProperty`). If it's just a plain old property, it can be set from multiple places leading to spaghetti code. :spaghetti:
2. `ObservableAsPropertyHelper` will take care of raising `INotifyPropertyChanged` events - if you're creating read-only properties, this can save so much boilerplate code.
3. `WhenAny` lets you combine multiple properties, treat their changes as observable streams, and craft ViewModel-specific outputs.
4. Scheduling. Simply pass in a value for the `scheduler` parameter, which avoids you needing to call `ObserveOn` yourself.
5. Laziness. Simply set `deferSubscription` to `true` and now your property is lazily-evaluated and will defer the subscription to the observable source until the first call.


