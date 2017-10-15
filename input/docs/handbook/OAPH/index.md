# Overview
The ObservableAsPropertyHelper, which is often abbreviated OAPH, is a class that will simplify the interop between a IObservable
and a property on your View Model. It will allow you to have a property which reflects the latest value that has been sent through the 
IObservable<T> stream. `ObservableAsPropertyHelper<T>` properties are a way to take *Observables* and convert them into
*ViewModel Properties*. 

`ObservableAsPropertyHelper<T>` is often used with Extension Method(MixIn) for `IObservable<T>` called `ToProperty()`. `ToProperty` allows to construct to a `ObservableAsPropertyHelper<T>` for a particular `IObservable<T>` and `ToProperty()` will provide interaction with the 
`INotifyPropertyChanged` and `INotifyPropertyChanging` interfaces for the ViewModel. When a new value has been added to the `IObservable<T>`, it will use the overload methods in the IReactiveObject interface to trigger the required events.

`ObservableAsPropertyHelper<T>` is very similar to a Lazy<T> in so far as it provides a Value member which provides the latest value of the Observable<T>. They are often read-only and reflecting the IObservable<T> stream. It is common to combine ObservableAsPropertyHelper<T> with the `WhenAny` extensions. 

# Example
First, we need to be able to declare an Output Property, using a class called
`ObservableAsPropertyHelper<T>`:

```cs
readonly ObservableAsPropertyHelper<string> firstName;
public string FirstName {
    get { return firstName.Value; }
}
```

Similar to read-write properties, this code should always be 100% boilerplate.
Next, we'll use a helper method `ToProperty` to initialize `firstName` in the
constructor. `ToProperty` has two overloads, one where it directly returns the OAPH
and another where it is returned in a `out` parameter:

```cs
this.WhenAnyValue(x => x.Name)
    .Select(x => x.Split(' ')[0])
    .ToProperty(this, x => x.FirstName, out firstName);
```
or
```cs
firstName = this.WhenAnyValue(x => x.Name)
    .Select(x => x.Split(' ')[0])
    .ToProperty(this, x => x.FirstName);
```

Here, `ToProperty` creates an `ObservableAsPropertyHelper` instance which will
signal that the "FirstName" property has changed. `ToProperty` is an extension
method on `IObservable<T>` and semantically acts like a "Subscribe".

# Performance considerations

## nameof() instead of using default Index.
For performance based solutions you can also use the nameof() operator override of `ToProperty()`
which won't use the Expression.
```cs
firstName = this.WhenAnyValue(x => x.Name)
    .Select(x => x.Split(' ')[0])
    .ToProperty(this, nameof(FirstName));
```

## Defer Subscription
If you are creating a large number of OAPH, consider deferring your subscription. `ToProperty` also allows you to deferSubscription to the underlying `IObservable<T>`. Deferring the subscription not have the OAPH Subscribe to the base IObserable<T> until the Value property has been accessed. This is especially useful if you have more complex IObservable<T> because  This approach is very close the approach that Lazy<T> takes. 

```cs
var nameStatusObservable = this.WhenAnyValue(x => x.Name).Select(x => GetLatestStatus(x));
name = nameStatusObservable.ToProperty(this, nameof(FirstName), deferSubscription: true);

public string Name => name.Value; // nameStatusObservable won't be subscribed until the Name property is accessed. 
``` 

One cavest of deferring the subscription is if you aren't careful you'll have a invalid value until a new value is added to the IObservable<T>. If using Hot Observables consider using a `ReplaySubject<T>` and limiting the ReplaySubject<T> to 1 previous value in the constructor. 

```cs
public class StatusManager
{
    private ReplaySubject<string> replayStatus = new ReplaySubject<string>(1);
    // Invoke the replayStatus observable somewhere

    public IObservable<string> StatusObservable => replayStatus;
}

public class StatusViewModel
{
    private ObservableAsPropertyHelper<string> status;
    public StatusViewModel(IObservable<string> statusObservable)
    {
        status = statusObservable.ToProperty(this, nameof(Status), deferSubscription: true);
    }

    public string Status => status.Value;
}
```
