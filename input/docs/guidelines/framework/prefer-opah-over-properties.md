# Prefer Observable Property Helpers to setting properties explicitly

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

 - `ObservableAsPropertyHelper` will take care of raising `INotifyPropertyChanged`
   events - if you're creating read-only properties, this can save so much boilerplate
   code.
 - `WhenAny` lets you combine multiple properties, treat their changes as observable
  streams, and craft ViewModel-specific outputs.