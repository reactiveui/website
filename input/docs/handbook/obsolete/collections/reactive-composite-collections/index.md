NoTitle: true
Title: ReactiveCompositeCollections
---

One of the options available in the reactive ecosystem is the `ReactiveCompositeCollections` project  by Brad Phelan which introduces a new ``ICompositeCollection<T>`` that works just like you would expect with LINQ. You can use ``Select``, ``SelectMany``, ``Where`` just like with ``IEnumerable<T>`` or ``IObservable<T>`` and the result of all these operators is another ``ICompositeCollection<T>``

The library lives at [Reactive Composite Collections (github)](https://github.com/Weingartner/ReactiveCompositeCollections) and is available from NuGet at [Reactive Composite Collections (nuget)](https://www.nuget.org/packages/ReactiveCompositeCollections/)

# Example

Here's a unit test that models a typical scenario:
    
* a filter that a user can update via some UI binding. 
* a set of source data updated at runtime and needs to be combined 
* we wish to create a new dynamic list composed from the filter and the source lists. 

```csharp
public class FilterHolder : ReactiveObject
{
    private Func<int,bool> _Filter;
    public Func<int,bool> Filter { get => _Filter; set => this.RaiseAndSetIfChanged( ref _Filter, value ); }
}


[Fact]
public void DynamicFilterWithRxUIShouldWork()
{
    var sourceList0 = new CompositeSourceList<int>();
    var sourceList1 = new CompositeSourceList<int>();

    var filter = new FilterHolder {Filter = v => v > 5};

    // Using LINQ we can express the intent of the composition
    // and filtering very succinctly and using an API that
    // is consistent with IEnumerable and RX Extensions
    var target =
        from fn in filter.WhenAnyValue( p=>p.Filter )
        from s in new [] {sourceList0,  sourceList1 }.Concat()
        where fn(s)
        select s;

    sourceList0.AddRange( new []{0,1,2,3,4,5,6,7,8,9,10} );

    // Convert to an observable collection for testing purposes
    using (var oc = target.CreateObservableCollection( EqualityComparer<int>.Default ))
    {
        // Set the filter to be everything greater than 5
        oc.Should().Equal( 6, 7, 8, 9, 10 );
        sourceList0.Clear();
        oc.Should().Equal();

        sourceList0.AddRange( new []{0,1} );
        sourceList1.AddRange( new []{9,10} );

        oc.Should().Equal( 9, 10);

        // Set the filter to be everything less than 5
        filter.Filter =  v=>v<5;
        oc.Should().Equal( 0, 1);
    }
}
```

For more details head on over to [Reactive Composite Collections (github)](https://github.com/Weingartner/ReactiveCompositeCollections)
