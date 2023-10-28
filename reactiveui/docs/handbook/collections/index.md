---
NoTitle: true
---
ReactiveUI recommends the use of [DynamicData](https://github.com/reactivemarbles/DynamicData) for collection based operations.

> DynamicData has replaced internally the use of [ReactiveList](~/docs/handbook/obsolete/collections/reactive-list.md)

## Overview of Dynamic Data

Dynamic Data is reactive collections based on [Reactive Extensions for .NET](~/docs/reactive-programming/index.md). 

Whenever a change is made to one of Dynamic Data's collections a notification is produced. A notification reflects what has changed in the collection. This notification is represented as a `ChangeSet` which can contain one or more changes.  Each item in the change set is represented as a `Change` which contains information about each individual change since the last notification.

The changes sets are published as an `IObservable<ChangeSet>`.  

This basic signature is the monad of Dynamic Data on which a rich set of Linq operators are provided which enable declarative querying and manipulation of data as it changes, and in a thread safe manner.

## Maintaining and consuming data

Dynamic Data provides two specialized  `IObservable<ChangeSet>`  producing collections:

 1. `SourceCache<TObject, TKey>` for items which have a unique key.
 2. `SourceList<T>` for items which do not have a unique key.

These objects each provide an API for maintaining data which have typical collection methods such as add and remove.  The idea is you maintain data in one of these collections, then use the extensive Linq API to dynamically query the data in a similar manner as Linq-to-Objects.

To convert these collections into an `IObservable<ChangeSet>` you call `Connect()` at which point notifications can be observed and the provided Linq operators can be applied. The convention in dynamic data is that any consumer which calls `Connect` receive a notification of any items which are already in the collection plus any subsequent changes.

Additionally there are several other means of creating observable changes sets from existing collections which implement `INotifyCollectionChanged` and `IEnumerable<T>`.

## What it is not

Dynamic data collections are not an alternative implementation to ```ObservableCollection<T>```.  The architecture of it has been based first and foremost on domain driven concepts. The idea is you load and maintain your data in one of the provided collections which can then use operators to manipulate the data without the complexity of managing collections. It can be used to react to your collections however you want, be it binding to a screen or producing some other kind of notification. The collections can be connected to as many times as required and a single collection can in turn become the source of many other derived collections.

## How to use it

If you are already using ```ObservableCollection<T>``` the easiest and quickest way to try out dynamic data is to use the extension ```.ToObservableChangeSet()```  which produces an observable change set and subsequently enables Dynamic Data operators.

For example if you have an existing reactive list ```ObservableCollection<T> myList``` you can do something like this:

```cs
// 'myList' is ObservableCollection<T>
// 'myDerivedList' is IObservableList<T>
var myDerivedList = myList
    .ToObservableChangeSet()
    .Filter(t => t.Status == "Something")
    .AsObservableList();
```

And voila you have create a filtered observable list. Or if you specify a key

```cs
// 'myList' is ObservableCollection<T>
// 'myDerivedCache' is IObservableCache<T, TKey>
var myDerivedCache = myList
    .ToObservableChangeSet(t => t.Id)
    .Filter(t => t.Status == "Something")
    .AsObservableCache();
```

you have a derived observable cache.

A caveat to this approach is if you are using ```myList``` will likely not be thread safe. Assuming ```myList``` is bound to a screen, then the observable change set is created and notified on the UI thread which is recommended to avoid for all operations except binding. The other approach is to create a data source first and bind later.

```cs
var myList = new SourceList<T>()
var disposable = myList
    .Connect() // make the source an observable change set
    .\\some other operation
```

or similarly for the observable cache

```cs
var myCache = new SourceCache<T, int>(t => t.Id) 
var disposable = myCache
    .Connect() // make the source an observable change set
    .\\some other operation
```

The advantage of creating your own data sources is that they can be maintained on a background thread which frees up valuable main thread time. Then should there be a need, bind as follows:

```cs
ReadOnlyObservableCollection<T> bindingData;
var disposable = mySource
    .Connect() // make the source an observable change set
    .Sort(SortExpressionComparer<T>.Ascending(t => t.DateTime))
    .ObserveOn(RxApp.MainThreadScheduler) 
    // Make sure this line^^ is only right before the Bind()
    // This may be important to avoid threading issues if
    // 'mySource' is updated on a different thread.
    .Bind(out bindingData)
    .Subscribe(); 
```

The API for the above is the same for cache and list.

## So what's the difference between a SourceList and a SourceCache

If you have a unique id, you should use an observable cache as it is dictionary based which will ensure no duplicates can be added and it notifies on adds, updates and removes, whereas list allows duplicates and only has no concept of an update. `SourceCache` has several performance advantages over `SourceList`, so if possible, always prefer `SourceCache` over `SourceList`.

There is another difference. The cache side of dynamic data is much more mature and has a wider range of operators. Having more operators is mainly because I found it easier to achieve good all round performance with the key based operators and do not want to add anything to Dynamic Data which inherently has poor performance.

## Using DynamicData with ReactiveUI

When building applications with ReactiveUI and DynamicData, you have a choice to work with mutable or with immutable collections. When working with immutable ones, using an `ObservableAsPropertyHelper<T>` is enough in simple cases. The `ObservableAsPropertyHelper<T>` represents an `Observable<T>`, a stream of values over time. You can treat those values as events, and the new values as event arguments. This means if you are using immutable collections, you can treat them as event arguments and update a property with a new collection each time it changes. See [ObservableAsPropertyHelper Handbook section](~/docs/handbook/observable-as-property-helper/index.md) to learn how to use this feature. Note, that creating a new collection for each update degrades performance and should be generally avoided, prefer to use DynamicData instead.

### An Example

Imagine your application needs a service that will expose a collection mutated by a background worker. You need to get change notifications from it somehow to synchronize it with the user interface. Here DynamicData comes to the rescue. You expose an `IObservable<IChangeSet<bool>>` from your service to the outer world, and DynamicData takes care of allowing you to observe changes of your mutable `SourceList` of items. Use the `.Connect()` operator to turn your `SourceList<T>` to an observable change set `IObservable<IChangeSet<bool>>`.

```cs
public class Service 
{
    private readonly SourceList<bool> _items = new SourceList<bool>();

    // We expose the Connect() since we are interested in a stream of changes.
    // If we have more than one subscriber, and the subscribers are known, 
    // it is recommended you look into the Reactive Extension method Publish().
    public IObservable<IChangeSet<bool>> Connect() => _items.Connect();

    public Service()
    {        
        // With DynamicData you can easily manage mutable datasets,
        // even if they are extremely large. In this complex scenario 
        // a service mutates the collection, by using .Add(), .Remove(), 
        // .Clear(), .Insert(), etc. DynamicData takes care of
        // allowing you to observe all of those changes.
        _items.Add(true);
        _items.RemoveAt(0);
        _items.Add(false);
    }
}
```

DynamicData uses .NET types to expose to the outside world, such as `ReadOnlyObservableCollection<T>`, rather than exposing their own types. `IObservable<IChangeSet<T>>` (and `IObservable<IChangeSet<TObject, TKey>>`) are the two base observables you can create derived based functionality from. `IObservable<IChangeSet<T>>` indicates what has changed to a collection. The first time you use `ToObservableChangeSet()` it emits the current state of the collection.

`SourceList`, `SourceCache` are multithreaded aware and optimised to create `IObservable<IChangeSet<T>>` and `IObservable<IChangeSet<TObject, TKey>>`. Generally SourceList/SourceCache are meant to be private to your classes, and you expose using the `Bind()` method. You generate the change sets by using the `Connect()` method on them.

Using the powerful DynamicData operators, you convert the `IObservable<IChangeSet<T>>` to a `ReadOnlyObservableCollection<T>` to which you can easily bind the platform-specific user interface. Declaring the read-only collection as a field or as a variable is required for the `.Bind()` operator to work as it uses `out` variables.

```cs
public class ViewModel : ReactiveObject
{
    private readonly ReadOnlyObservableCollection<bool> _items;
    public ReadOnlyObservableCollection<bool> Items => _items;

    public ViewModel()
    {
        var service = new Service();
        service.Connect()
            // Transform in DynamicData works like Select in
            // LINQ, it observes changes in one collection, and
            // projects it's elements to another collection.
            .Transform(x => !x)
            // Filter is basically the same as .Where() operator
            // from LINQ. See all operators in DynamicData docs.
            .Filter(x => x)
            // Ensure the updates arrive on the UI thread.
            .ObserveOn(RxApp.MainThreadScheduler)
            // We .Bind() and now our mutable Items collection 
            // contains the new items and the GUI gets refreshed.
            .Bind(out _items)
            .Subscribe();
    }
}
```

> **Note** If you are updating an observable list or an observable cache from a background thread, adding `.ObserveOn(RxApp.MainThreadScheduler)` right before a call to `.Bind()` might be neccessary, to ensure the updates arrive on the UI thread.

`ObservableCollectionExtended<T>` is a good single threaded collection where you don't need to do derived based functionality. To synchronize two collections in your view model, declare one of your collections as `ObservableCollectionExtended<T>` and another one as `ReadOnlyObservableCollection<T>`. Then you apply the `.ToObservableChangeSet()` operator to your observable collection that turns it to `IObservable<IChangeSet<T>>`.

```cs
public class SynchronizedCollectionsViewModel : ReactiveObject
{
    private readonly ReadOnlyObservableCollection<bool> _derived;
    public ReadOnlyObservableCollection<bool> Derived => _derived;

    public ObservableCollectionExtended<bool> Source { get; }

    public SynchronizedCollectionsViewModel()
    {
        Source = new ObservableCollectionExtended<bool>();

        // Use the ToObservableChangeSet operator to convert
        // the observable collection to IObservable<IChangeSet<T>>
        // which describes the changes. Then, use any DD operators
        // to transform the collection. 
        Source.ToObservableChangeSet()
            .Transform(value => !value)
            // No need to use the .ObserveOn() operator here, as
            // ObservableCollectionExtended is single-threaded.
            .Bind(out _derived)
            .Subscribe();
        
        // Update the source collection and the derived
        // collection will update as well.
        Source.Add(true);
        Source.RemoveAt(0);
        Source.Add(false);
        Source.Add(true);
    }
}
```

## Tracking Changes in Collections of Reactive Objects

DynamicData supports change tracking for classes that implement the `INotifyPropertyChanged` interface — `ReactiveObject`s. For example, if you'd like to do a `WhenAnyValue` on each element in a collection of changing objects, use the `AutoRefresh()` DynamicData operator:

```cs
// 'collectionOfReactiveObjects' is ObservableCollection<T>
// Here, T inherits from the ReactiveObject class.
// 'databasesValid' is IObservable<bool>
var databasesValid = collectionOfReactiveObjects
    .ToObservableChangeSet()
    .AutoRefresh(model => model.IsValid) // Subscribe only to IsValid property changes
    .ToCollection()                      // Get the new collection of items
    .Select(x => x.All(y => y.IsValid)); // Verify all elements satisfy a condition etc.

// Then you can convert IObservable<bool> to a view model property.
// '_databasesValid' is of type ObservableAsPropertyHelper<bool> here.
_databasesValid = databasesValid.ToProperty(this, x => x.DatabasesValid);
```

> **Note** `ToCollection()` works pretty differently internally, it re-generates the entire list every time while SourceCache/SourceList `Bind()` does addition/removals etc. `ToCollection()` is only meant for aggregation based on operations where you really need a full collection each time as an observable.

## Converting ReactiveList to DynamicData

If you are using `ReactiveList<T>`, and only adding/removing from the UI thread use `ObservableCollectionExtended<T>`. It provides similar functionality where you `AddRange()` and suppress notifications. This approach should only be used if you are doing Single Threaded operations and wanting to mutate your data.

A lot of users try to do the following even though it's unnecessary for single threaded applications.

```cs
var myList = new SourceList<T>()
var disposable = myList
    .Connect() // make the source an observable change set
    .ObserveOn(RxApp.MainThreadScheduler)
    .Bind(out _myOutputList)
    .Subscribe();
```

> **Important Note** A common mistake a lot of users make is trying to expose DynamicData classes to the world. Use the `Bind()` method instead to expose your data through a `ReadOnlyObservableCollection<T>` field, which you then expose as a property.

Try to reuse your `IObservableChangeSet<T>` where it makes sense. It's a expensive operation to generate and you can use the Reactive Extension's method `Publish()`.

```cs
// Use standard rx Publish() / Connect() to share published change sets.
// 'shared' is of type IObservable<IChangeSet<T>>
var shared = _source
    .Connect()
    .Publish();

// 'selectedChanged' if of type IObservable<Unit>
var selectedChanged = shared
    .WhenPropertyChanged(si => si.IsSelected)
    .Select(changes => Unit.Default)
    .StartWith(Unit.Default);

// Apply other operations to the shared connection.
shared.ToCollection().CombineLatest(selectedChanged, (items, _) => items);
shared.Maximum(i => i).Subscribe(max => Max = max);
shared.Connect();
```

[Common operations](https://github.com/RolandPheasant/DynamicData#consuming-observable-change-sets) in DynamicData have slightly different names than Reactive Extension operators.
  * `Where()` is `Filter()`
  * `Select()` is `Transform()`
  * `SelectMany()` is `TransformMany()`

## Explore DynamicData

* [DynamicData GitHub page](https://github.com/reactivemarbles/DynamicData)
* [DynamicData Snippets](https://github.com/RolandPheasant/DynamicData.Snippets) - Snippets curated based on small example problems
* [DynamicData Trader App](https://github.com/RolandPheasant/Dynamic.Trader) - A sample stock trading application showing off various implementations.
* [DynamicData Tail Blazer](https://github.com/RolandPheasant/TailBlazer) - A sample closer to a end application.
* [DynamicData Samplz](https://github.com/RolandPheasant/DynamicData.Samplz) - More advanced snippets.
