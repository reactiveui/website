title: Observable.GroupBy()
---
# Observable.GroupBy Method

Include Protected Members  
Include Inherited Members

Groups the elements of an observable sequence.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[GroupBy<TSource, TKey>(IObservable<TSource>, Func<TSource, TKey>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.groupby%60%602(system.iobservable%7b%60%600%7d%2csystem.func%7b%60%600%2c%60%601%7d)(v=VS.103))Groups the elements of an observable sequence according to a specified key selector function.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[GroupBy<TSource, TKey>(IObservable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.groupby%60%602(system.iobservable%7b%60%600%7d%2csystem.func%7b%60%600%2c%60%601%7d%2csystem.collections.generic.iequalitycomparer%7b%60%601%7d)(v=VS.103))Groups the elements of an observable sequence according to a specified key selector function and comparer.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[GroupBy<TSource, TKey, TElement>(IObservable<TSource>, Func<TSource, TKey>, Func<TSource, TElement>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.groupby%60%603(system.iobservable%7b%60%600%7d%2csystem.func%7b%60%600%2c%60%601%7d%2csystem.func%7b%60%600%2c%60%602%7d)(v=VS.103))Groups the elements of an observable sequence and selects the resulting elements by using a specified function.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[GroupBy<TSource, TKey, TElement>(IObservable<TSource>, Func<TSource, TKey>, Func<TSource, TElement>, IEqualityComparer<TKey>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.groupby%60%603(system.iobservable%7b%60%600%7d%2csystem.func%7b%60%600%2c%60%601%7d%2csystem.func%7b%60%600%2c%60%602%7d%2csystem.collections.generic.iequalitycomparer%7b%60%601%7d)(v=VS.103))Groups the elements of an observable sequence according to a specified key selector function and comparer and selects the resulting elements by using a specified function.Top

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)




