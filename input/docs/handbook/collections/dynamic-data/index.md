# How Dynamic Data Behaves

Using a `SourceCache<TObject, TKey>` is really important, the key helps identify whether an object currently exists in the SourceCache or not.

However there is an assumption that is easy to get caught out on.

> **Note**   Because there is generally a Sorter when connecting to the SourceCache, it will NOT trigger a REPLACE modification.  Instead SourceCache triggers a REMOVE and then an ADD.  The problem with this is reflected in the UI, as the list will delete the view cell and re-render it.


## How "should" we use Dynamic-Data/SourceCache?

This guide will help improve overall performance of Collections without the overhead of deleting and re-rendering entire cells.  Especially if using ReactiveUI for Xamarin.iOS and Xamarin.Android natively rather than Xamarin.Forms.

The following steps are discussed..

1. SourceCache should contain ItemViewModels.
2. DON'T use Transform when connecting the ReadOnlyObservableCollection to the SourceCache.
3. Use the EditDiff when comparing the new list with the current list in SourceCache.
4. In the EditDiff, update each property in the ItemViewModel with the new values (The whole reason RaiseAndSetIfChanged exists and indeed all the bindings that were created)

> **Note**  We will use the term ItemViewModel to indicate a ViewModel that inherits from ReactiveObject and is a ViewModel that has many in a Collection.  It's an "item" in a collection.


## SourceCache contains ItemViewModels

One assumption is that the SourceCache should contain a list of DataModels that have come from a server or some other source and then these are "Transformed" into ItemViewModels.

This code snippet demonstrates the Transform pattern.

```cs
private readonly SourceCache<BaseDataModel, string> _items = 
    new SourceCache<BaseDataModel, string>(i => i.Id);

public MyMainViewModel()
  {
    _items
          .Connect()
          .Transform(baseDataModel => {
            // different ItemViewModels can be created depending on the BaseDataModel
              return new HeaderItemViewModel(baseDataModel);
            })
          .Sort(_sortExpressionComparer)
          .ObserveOn(RxApp.MainThreadScheduler)
          .Bind(out _itemViewModelCollection)
          .Subscribe()
          .DisposeWith(_compositeDisposable);
  }
```

The solution is to have a BaseItemViewModel which has the Id property as the Key in the SourceCache like so.

```cs
private readonly SourceCache<BaseItemViewModel, string> _items = 
    new SourceCache<BaseItemViewModel, string>(i => i.Id);

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

This helps solve one aspect of performance and problematic rendering animations that can be seen in the UI.


## Avoid Transform

In the example above, you'll notice that we took out the Transform.  The Transform was previously required to convert a DataModel that doesn't inherit from ReactiveObject to an ItemViewModel that DOES inherit from ReactiveObject.

The assumption is that because Transform is available and that the pattern is discussed in our own documentation, that it is considered "Best Practice", it should just work as expected out of the box.  Unfortunately it doesn't, but there is a better solution.


## Solving the problem with EditDiff

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

Please note the currentItemViewModel and the newItemViewModel.  SourceCache has already found that BOTH of these ItemViewModels have the same "Key" which is an Id string field as per the code further up the screen.  SourceCache needs to determine whether these too ItemViewModels are the same, which is why the Equals is there.  Returning `false` will trigger the REMOVE/ADD while returning `true` will not trigger any change.

The problem with this code is that it requires that each ItemViewModel implements it's own Equals and GetHashCode() overrides.  While our software IDE's can automatically generate these for us, they are a pain to maintain and they make the class look messy and more unreadable.

The alternative is as follows:-

```cs

      _items.EditDiff(newItemViewModelCollection, (currentItemViewModel, newItemViewModel) => 
      {
          if (currentItemViewModel is HeaderItemViewModel currentheaderItemViewModel
              && newItemViewModel is HeaderItemViewModel newHeaderItemViewModel)
          {
            currentHeaderItemViewModel.UpdateViewModel(newHeaderItemViewModel)
          }
          return true;
      }

```

In the code above we have cast the BaseItemViewModel to a HeaderItemViewModel and then called UpdateViewModel, a method we have created.

The HeaderItemVewModel classes are as follows:-

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

  protected void UpdateViewModel(HeaderItemViewModel headerItemViewModel)
  {
    Title = headerItemViewModel.Title;
  }
  
}

```

Using the approach above, we leverage the power of ReactiveUI.  The UpdateViewModel sets the value of the Title, which will only notify the view to update if the Title is different.

On the UI, only the TextView element will update, NOT the entire cell being deleted and re-rendered.

You may be asking yourself.... "What's the point of EditDiff if I have to do this?"

It's a good question, however what we do get for free is the managing of the Collection, if an item with a unique Key (in this case a property called Id) is either added or deleted, EditDiff will do that for us. 


> **Note** You'll see that we have marked the "Set" as private and initialize the properties in the constructor.  As "Best Practice" we have done this for the code to be Immutable or unable to be changed from outside the class.

