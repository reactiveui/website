ReactiveUI recommends the use of DynamicData framework for collection based operations. DynamicData has replaced internally the use of [ReactiveList](../obsolete/collections)

# Overview of Dynamic Data

DynamicData is reactive collections based on Rx.Net.  

DynamicData is built around the concept of Observables of ```IChangeSet``` which contains a set of ```IChange``` items.

A ```IChange``` specifies changes that individual actions that have occurred within a collection. For example adding or deleting items. The ```IChangeSet``` contains a series of changes that have occurred to the collection.

DynamicData contains several ```IChangeSet``` aware Collections such as the ```SourceList<T>``` and ```SourceCache<TObject, TKey>```. The ```SourceList<T>``` is for items that do not have a unique key for item, and the ```SourceCache<TObject, TKey>``` is for objects that should maintain a common key.

You can then ```Connect()``` to IObservable of ChangeSet's for each container to receive the notifications about items already inside the container and any subsequent changes to the collection. The convention in dynamic data is that any consumer which calls ```Connect``` receive a notification of any items which are already in the collection plus subsequent changes. The simplicity of this concept enables the definition of a myriad of specialised operators which allows the consumer to easily define how they want the results of the reactive chain to be shaped and how it should behave.

DynamicData can also hook into any existing Container that derives of ```INotifyCollectionChanged``` and ```IEnumerable<T>```, these include ```ObservableCollection<T>```, and also ```ObservableCollectionExtended<T>``` which is provided by DynamicData.

DynamicData is designed to hook into existing collection classes. For example after you apply the chain of Reactive operators on your IObservable of ChangeSet's you often will ```Bind()``` your results into a ```ReadOnlyObservableCollection<T>``` for consumption by your GUI framework.

## What it is not

Dynamic data collections are not an alternative implementation to ```ObservableCollection<T>```.   The architecture of it has been based first and foremost on domain driven concepts. The idea is you load and maintain your data in one of the provided collections which can then use operators to manipulate the data without the complexity of managing collections. It can be used to react to your collections however you want, be it binding to a screen or producing some other kind of notification. The collections can be connected to as many times as required and a single collection can in turn become the source of many other derived collections.

## For ReactiveUI users
  
Dynamic data is also not an alternative to ReactiveUI. ReactiveUI is a cross platform MVVM library whereas DynamicData is a cross platform library which aims take the pain out of the management of in memory data. They are sister projects, complement each other and are easily used side by side.

If you are already using ```ObservableCollection<T>``` the easiest and quickest way to try out dynamic data is to use the extension ```.ToObservableChangeSet()```  which produces an observable change set and subsequently enables Dynamic Data operators.

For example if you have an existing reactive list ```ObservableCollection<T> myList``` you can do something like this:

```cs
var myDerivedList = myList.ToObservableChangeSet()
    .Filter(t=> t.Status = "Something")
    .AsObservableList();
```

And voila you have create a filtered observable list. Or if you specify a key

```cs
var myDerivedCache = myList.ToObservableChangeSet(t = >t.Id)
	.Filter(t=> t.Status = "Something")
	.AsObservableCache();
```

you have a derived observable cache.

A caveat to this approach is if you are using ```myList``` will likely not be thread safe. Assuming ```myList``` is bound to a screen, then the observable change set is created and notified on the UI thread which I avoid for all operations except binding. The other approach is to create a data source first and bind later.

```cs
var myList = new SourceList<T>() 
var myConnection = myList.Connect() //make the source an observable change set
	.\\some other operation
```
or similarly for the observable cache 
```cs
var myCache = new SourceCache<T, int>(t=>t.Id) 
var myConnection = myCache.Connect() //make the source an observable change set
	.\\some other operation
```

The advantage of creating your own data sources is that they can be maintained on a background thread which frees up valuable main thread time. Then should there be a need, bind as follows:

```cs
ReadOnlyObservableCollection<T> bindingData;
var myBindingOperation = mySource
		.Sort(SortExpressonComparer<T>.Ascending(t=>t.DateTime))
		.ObserveOn(RxApp.MainThreadScheduler)
		.Bind(out bindingData)
		.Subscribe(); 
```
The API for the above is the same for cache and list.

*NB: need to install DynamicData.ReactiveUI package to have this functionality on reactive list.

## So what's the difference between an observable list and an observable cache

I get asked this question a lot and the answer is really simple.  If you have a unique id, you should use
an observable cache as it is dictionary based which will ensure no duplicates can be added and it notifies on adds, updates and removes, whereas list allows duplicates and only has no concept of an update.

There is another difference. The cache side of dynamic data is much more mature and has a wider range of operators. Having more operators is mainly because I found it easier to achieve good all round performance with the key based operators and do not want to add anything to Dynamic Data which inherently has poor performance.