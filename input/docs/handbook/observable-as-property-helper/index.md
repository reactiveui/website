The ObservableAsPropertyHelper (OAPH) is a class that simplifies the interop between an IObservable and a property on your View Model. It allows you to have a property which reflects the latest value that has been sent through the IObservable<T> stream. 
  
`ObservableAsPropertyHelper<T>` is very similar to a Lazy<T> in so far as it provides a Value member which provides the latest value of the Observable<T>. They are often read-only and reflect the IObservable<T> stream. It is common to combine ObservableAsPropertyHelper<T> with the `WhenAny` extensions. 

### ToProperty()
`ToProperty` allows you to construct an `ObservableAsPropertyHelper<T>` from an `IObservable<T>`. It also provides the interaction with the `INotifyPropertyChanged` and `INotifyPropertyChanging` interfaces for a ViewModel. When a new value has been added to the `IObservable<T>`, it will use the overload methods in the IReactiveObject interface to trigger the required events.

`ToProperty` is an extension method on `IObservable<T>` and semantically acts like a "Subscribe".

### Property vs ObservableAsPropertyHelper

You should use a property and `RaiseAndSetIfChanged` if you are intending to mutate the value.

`ObservableAsPropertyHelper` properties are useful for when you have "calculated" values, for example, if their value is solely the result of other properties. You will also use the `ObservableAsPropertyHelper` when you want to expose the latest value from a `IObservable<T>`.

`ObservableAsPropertyHelper` properties are helpful to remove spaghetti code where different methods and components may be mutating multiple locations. They also clearly define what values are used in the calculation of the value and help describe the dependent properties for the `ObservabeAsPropertyHelper`, unlike settable properties where you have to search the code base further for location where the value is mutating.

# Example
First, we need to be able to declare an Output Property, using a class called
`ObservableAsPropertyHelper<T>`:

```cs
readonly ObservableAsPropertyHelper<string> firstName;
public string FirstName => firstName.Value;
```

Similar to read-write properties, this code should always be 100% boilerplate.
Next, we'll use a helper method `ToProperty` to initialize `firstName` in the
constructor. `ToProperty` has two overloads, one where it directly returns the OAPH
and another where it is returned in a `out` parameter:

```cs
this.WhenAnyValue(x => x.Name)
    .Select(name => name.Split(' ')[0])
    .ToProperty(this, x => x.FirstName, out firstName);
```
or
```cs
firstName = this
    .WhenAnyValue(x => x.Name)
    .Select(name => name.Split(' ')[0])
    .ToProperty(this, x => x.FirstName);
```

Here, `ToProperty` creates an `ObservableAsPropertyHelper` instance which will
signal that the "FirstName" property has changed.

# Performance considerations

## nameof() instead of using default Index.
For performance based solutions you can also use the nameof() operator override of `ToProperty()`
which won't use the Expression.
```cs
firstName = this
    .WhenAnyValue(x => x.Name)
    .Select(name => name.Split(' ')[0])
    .ToProperty(this, nameof(FirstName));
```

## Defer Subscription
If you are creating a large number of OAPH, consider deferring your subscription. `ToProperty` also allows you to deferSubscription to the underlying `IObservable<T>`. Deferring the subscription not have the OAPH Subscribe to the base IObserable<T> until the Value property has been accessed. This is especially useful if you have more complex IObservable<T> because  This approach is very close the approach that Lazy<T> takes. 

```cs
var nameStatusObservable = this
    .WhenAnyValue(x => x.Name)
    .Select(name => GetLatestStatus(name));
name = nameStatusObservable
    .ToProperty(this, nameof(FirstName), deferSubscription: true);

// nameStatusObservable won't be subscribed until the 
// Name property is accessed.
private readonly ObservableAsPropertyHelper<string> name;
public string Name => name.Value; 
``` 

One caveat of deferring the subscription is if you aren't careful you'll have a invalid value until a new value is added to the `IObservable<T>`. If using Hot Observables consider using a `ReplaySubject<T>` and limiting the `ReplaySubject<T>` to 1 previous value in the constructor. 

```cs
public class StatusViewModel : ReactiveObject
{
    private readonly ObservableAsPropertyHelper<string> status;
    public string Status => status.Value;
    
    public StatusViewModel()
    {
        var replayStatus = new ReplaySubject<string>(1);
        // Invoke the replayStatus subject somewhere...
        
        IObservable<string> statusObservable = replayStatus; 
        status = statusObservable.ToProperty(this, nameof(Status), deferSubscription: true);
    }
}
```
