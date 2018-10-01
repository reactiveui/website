# Qbservable.GroupBy Method

Include Protected Members  
Include Inherited Members

Groups the elements of a queryable observable sequence.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[GroupBy<TSource, TKey>(IQbservable<TSource>, Expression<Func<TSource, TKey>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.groupby%60%602(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.linq.expressions.expression%7bsystem.func%7b%60%600%2c%60%601%7d%7d)(v=VS.103))Groups the elements of a queryable observable sequence according to a specified key selector function.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[GroupBy<TSource, TKey>(IQbservable<TSource>, Expression<Func<TSource, TKey>>, IEqualityComparer<TKey>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.groupby%60%602(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.linq.expressions.expression%7bsystem.func%7b%60%600%2c%60%601%7d%7d%2csystem.collections.generic.iequalitycomparer%7b%60%601%7d)(v=VS.103))Groups the elements of a queryable observable sequence according to a specified key selector function and comparer.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[GroupBy<TSource, TKey, TElement>(IQbservable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TSource, TElement>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.groupby%60%603(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.linq.expressions.expression%7bsystem.func%7b%60%600%2c%60%601%7d%7d%2csystem.linq.expressions.expression%7bsystem.func%7b%60%600%2c%60%602%7d%7d)(v=VS.103))Groups the elements of a queryable observable sequence and selects the resulting elements by using a specified function.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[GroupBy<TSource, TKey, TElement>(IQbservable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<TSource, TElement>>, IEqualityComparer<TKey>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.groupby%60%603(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.linq.expressions.expression%7bsystem.func%7b%60%600%2c%60%601%7d%7d%2csystem.linq.expressions.expression%7bsystem.func%7b%60%600%2c%60%602%7d%7d%2csystem.collections.generic.iequalitycomparer%7b%60%601%7d)(v=VS.103))Groups the elements of a queryable observable sequence according to a specified key selector function and comparer and selects the resulting elements by using a specified function.Top

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)




