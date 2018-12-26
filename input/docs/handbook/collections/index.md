ReactiveUI recommends the use of DynamicData framework for collection based operations. DynamicData has replaced internally the use of [ReactiveList](../obsolete/collections)

# Overview of Dynamic Data

Dynamic Data is reactive collections based on Rx.Net. 

Whenever a change is made to one of Dynamic Data's collections a notification is produced. A notification reflects what has changed in the collection. This notification is represented as a `ChangeSet ` which can contain one or more changes.  Each item in the change set is represented as a `Change` which contains information about each individual change since the last notification.

The changes sets are published as an `IObservable<ChangeSet>`.  

This basic signature is the monad of Dynamic Data on which a rich set of Linq operators are provided which enable declarative querying and manipulation of data as it changes, and in a thread safe manner.

## Maintaining and consuming data 

Dynamic Data provides two specialized  `IObservable<ChangeSet>`  producing collections:

 1. `SourceCache<TObject, TKey>` for items which have a unique key.
 2. `SourceList<T>` for items which do not have a unique key.

These objects each provide an API for maintaining data which have typical collection methods such as add and remove.  The idea is you maintain data in one of these collections, then use the extensive Linq API to dynamicaly query the data in a similar manner as Linq-to-Objects.

To convert these collections into an `IObservable<ChangeSet>` you call `Connect()` at which point notifications can be observed and the provided Linq operators can be applied. The convention in dynamic data is that any consumer which calls `Connect` receive a notification of any items which are already in the collection plus any subsequent changes.

Additionally there are several other means of creating observable changes sets from existing collections which implement `INotifyCollectionChanged` and `IEnumerable<T>`.

## What it is not

Dynamic data collections are not an alternative implementation to ```ObservableCollection<T>```.  The architecture of it has been based first and foremost on domain driven concepts. The idea is you load and maintain your data in one of the provided collections which can then use operators to manipulate the data without the complexity of managing collections. It can be used to react to your collections however you want, be it binding to a screen or producing some other kind of notification. The collections can be connected to as many times as required and a single collection can in turn become the source of many other derived collections.

## For ReactiveUI users
  
Dynamic data is also not an alternative to ReactiveUI. ReactiveUI is a cross platform MVVM library whereas DynamicData is a cross platform library which aims take the pain out of the management of in memory data. They are sister projects, complement each other and are easily used side by side.

If you are already using ```ObservableCollection<T>``` the easiest and quickest way to try out dynamic data is to use the extension ```.ToObservableChangeSet()```  which produces an observable change set and subsequently enables Dynamic Data operators.

For example if you have an existing reactive list ```ObservableCollection<T> myList``` you can do something like this:

```cs
var myDerivedList = myList
    .ToObservableChangeSet()
    .Filter(t => t.Status = "Something")
    .AsObservableList();
```

And voila you have create a filtered observable list. Or if you specify a key

```cs
var myDerivedCache = myList
    .ToObservableChangeSet(t => t.Id)
    .Filter(t => t.Status = "Something")
    .AsObservableCache();
```

you have a derived observable cache.

A caveat to this approach is if you are using ```myList``` will likely not be thread safe. Assuming ```myList``` is bound to a screen, then the observable change set is created and notified on the UI thread which I avoid for all operations except binding. The other approach is to create a data source first and bind later.

```cs
var myList = new SourceList<T>() 
var myConnection = myList
    .Connect() // make the source an observable change set
    .\\some other operation
```
or similarly for the observable cache 
```cs
var myCache = new SourceCache<T, int>(t => t.Id) 
var myConnection = myCache
    .Connect() // make the source an observable change set
    .\\some other operation
```

The advantage of creating your own data sources is that they can be maintained on a background thread which frees up valuable main thread time. Then should there be a need, bind as follows:

```cs
ReadOnlyObservableCollection<T> bindingData;
var myBindingOperation = mySource
    .Sort(SortExpressonComparer<T>.Ascending(t => t.DateTime))
    .ObserveOn(RxApp.MainThreadScheduler) // Make sure this is only right before the Bind()
    .Bind(out bindingData)
    .Subscribe(); 
```
The API for the above is the same for cache and list.


## So what's the difference between an observable list and an observable cache

I get asked this question a lot and the answer is really simple.  If you have a unique id, you should use
an observable cache as it is dictionary based which will ensure no duplicates can be added and it notifies on adds, updates and removes, whereas list allows duplicates and only has no concept of an update.

There is another difference. The cache side of dynamic data is much more mature and has a wider range of operators. Having more operators is mainly because I found it easier to achieve good all round performance with the key based operators and do not want to add anything to Dynamic Data which inherently has poor performance.

## How to track changes in collections of Reactive Objects?

DynamicData supports change tracking for classes that implement the `INotifyPropertyChanged` interface — `ReactiveObject`s. For example, if you'd like to do a `WhenAnyValue` on each element in a collection of changing objects, use the `AutoRefresh()` DynamicData operator:

```cs
var databasesValid = collectionOfReactiveObjects
    .ToObservableChangeSet()
    .AutoRefresh(model => model.IsValid) // Subscribe only to IsValid property changes
    .ToCollection()                      // Get the new collection of items
    .Select(x => x.All(y => y.IsValid)); // Verify all elements satisfy a condition etc.
```

See more examples in [DynamicData.Snippets](https://github.com/RolandPheasant/DynamicData.Snippets/tree/master/DynamicData.Snippets) project.
