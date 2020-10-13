# How Dynamic Data Behaves

Using a `SourceCache<TObject, TKey>` is really important, the key helps identify whether an object currently exists in the SourceCache.  However there is an assumption that is easy to get caught out on.

> **Note** Because there is generally a Sorter when Connecting to the SourceCache, it will NOT trigger a REPLACE but instead will first trigger a REMOVE and then an ADD.  The problem with this is that in the UI, it will delete the view cell and re-render it.  Even if it is a single property in the ItemViewModel.


## How "should" we use Dynamic-Data/SourceCache?

This is a guide that helps improve overall performance of Collections without the overhead of deleting and re-rendering entire cells.  Especially if using Reactive UI for Xamarin.iOS and Xamarin.Android natively.

The following steps are required

1. SourceCache should contain ItemViewModels.
2. DON'T use Transform when connecting the ReadOnlyObservableCollection to the SourceCache.
3. Use the EditDiff when comparing the new list with the list in SourceCache.
4. In the EditDiff, if the objects are not "EQUAL", then Update each property in the ItemViewModel with the new properties (The whole reason RaiseAndSetIfChanged exists and indeed all the bindings that were created)

> **Note**  We will use the term ItemViewModel to indicate a ViewModel that inherits from ReactiveObject and is a ViewModel that has many in a Collection.  It's an "item" in a collection.


### SourceCache contains ItemViewModels

One assumption is that the SourceCache should contain a list of DataModels that have come from a server or some other area and then these are "Transformed" into ItemViewModels.

```cs
private readonly SourceCache<BaseDataModel, string> _items = new SourceCache<BaseDataModel, string>(i => i.Id);

public MyMainViewModel()
  {
    _items
          .Connect()
          .Transform(baseDataModel => {
            // different ItemViewModels can be created depending on the BaseDataModel
              return new SomethingItemViewModel(baseDataModel);
            })
          .Sort(_sortExpressionComparer)
          .ObserveOn(RxApp.MainThreadScheduler)
          .Bind(out _itemViewModelCollection)
          .Subscribe()
          .DisposeWith(_compositeDisposable);
  }
```

The solution is to have the BaseItemViewModel in the SourceCache like so.

```cs
private readonly SourceCache<BaseItemViewModel, string> _items = new SourceCache<BaseItemViewModel, string>(i => i.Id);

public MyMainViewModel()
  {
    _items
          .Connect()
          .Sort(_sortExpressionComparer)
          .ObserveOn(RxApp.MainThreadScheduler)
          .Bind(out _itemViewModelCollection)
          .Subscribe()
          .DisposeWith(_compositeDisposable);
  }
```

This helps solve one aspect of performance and problematic animations in the UI lists that we aren't wanting.


### Avoid Transform

In the example above, you'll notice that we took out the Transform.  The Transform is needed in that example to convert a DataModel that doesn't inherit from ReactiveObject to an ItemViewModel that does inherit from ReactiveObject.

The assumption is that because these tools are available and that the pattern is discussed in our own documentation, that it is considered "Best Practice" and it should just work as expected out of the box.  Unfortunately it doesn't and can be problematic if you have a ItemViewModel Collection that has different types of Cells or Views inside a list.


### Solving the problem with EditDiff

Consider the following code snippet:-

```cs

      _items.EditDiff(newItemViewModelCollection, (currentItemViewModel, newItemViewModel) => 
      {
          if (currentItemViewModelModel.Equals(newItemViewModel)) 
          {
               return true;
          }
          return false;
      }

```

You'll notice that there is a currentItemViewModel and the newItemViewModel.  Using the SourceCache, it has already found that BOTH of these have the same "Key" which is an Id string field as per the code further up the screen.  So now it needs to determine whether any properties have changed, which is why the Equals is there.

The problem with this is that it requires that each ItemViewModel, implements it's own Equals override as well as GetHashCode() override.  While our software IDE can automatically generate these for us, they are a pain to maintain and they make the class look messy and more unreadable.

An alternative to this is as follows:-

```cs

      _items.EditDiff(newItemViewModelCollection, (currentItemViewModel, newItemViewModel) => 
      {
          if (currentItemViewModel is HeaderItemViewModel currentheaderItemViewModel && newItemViewModel is HeaderItemViewModel newHeaderItemViewModel)
          {
            currentHeaderItemViewModel.UpdateViewModel(newHeaderItemViewModel)
          }
          return true;
      }

```

We have cast the BaseItemViewModel to a HeaderItemViewModel and implemented a method called UpdateViewModel as per the following snippets:-

```cs

public class BaseItemViewModel : ReactiveObject
{
  private string _id;

  public BaseItemViewModel(string id)
  {
    Id = id;
  }

  public string Id
  {
    get => _id;
    private set => this.RaiseAndSetIfChanged(ref _id, value);
  }
}

```

```cs

public class HeaderItemViewModel : BaseItemViewModel
{
  private string _title;

  public HeaderItemViewModel(string id, string title) : base(id)
  {
    Title = title;
  }
  public string Title
  {
    get => _title;
    private set => this.RaiseAndSetIfChanged(ref _title, value);
  }

  public void UpdateViewModel(HeaderItemViewModel headerItemViewModel)
  {
    Title = headerItemViewModel.Title;
  }
  
}

```

You may be asking yourself.... "What's the point of EditDiff if I have to do this?"

It's a good question, however what we do get for free is the managing of the Collection, if an item with a unique Key (in this case a property called Id) is either added or deleted, EditDiff will do that for us.  Then we leverage the power of ReactiveUI in the UpdateViewModel method that is created which will only notify the view to update if the property is changed with the RaiseAndSetIfChanged method.


> **Note** You'll see that we have marked the "Set" as private and initialize the properties in the constructor.  As "Best Practice" we have done this for the code to be Immutable or unable to be changed from outside the class.

