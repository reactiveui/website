# Observable.DistinctUntilChanged Method

Include Protected Members  
Include Inherited Members

Returns an observable sequence that contains only distinct contiguous elements.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[DistinctUntilChanged<TSource>(IObservable<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.distinctuntilchanged%60%601(system.iobservable%7b%60%600%7d)(v=VS.103))Returns an observable sequence that contains only distinct contiguous elements with a specified source.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[DistinctUntilChanged<TSource>(IObservable<TSource>, IEqualityComparer<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.distinctuntilchanged%60%601(system.iobservable%7b%60%600%7d%2csystem.collections.generic.iequalitycomparer%7b%60%600%7d)(v=VS.103))Returns an observable sequence that contains only distinct contiguous elements according to the comparer.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[DistinctUntilChanged<TSource, TKey>(IObservable<TSource>, Func<TSource, TKey>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.distinctuntilchanged%60%602(system.iobservable%7b%60%600%7d%2csystem.func%7b%60%600%2c%60%601%7d)(v=VS.103))Returns an observable sequence that contains only distinct contiguous elements according to the keySelector.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[DistinctUntilChanged<TSource, TKey>(IObservable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.distinctuntilchanged%60%602(system.iobservable%7b%60%600%7d%2csystem.func%7b%60%600%2c%60%601%7d%2csystem.collections.generic.iequalitycomparer%7b%60%601%7d)(v=VS.103))Returns an observable sequence that contains only distinct contiguous elements according to the keySelector and the comparer.Top

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)




