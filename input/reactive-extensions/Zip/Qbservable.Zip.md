title: Qbservable.Zip()
---
# Qbservable.Zip Method

Include Protected Members  
Include Inherited Members

Merges two queryable observable sequences into one queryable observable sequence.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Zip<TFirst, TSecond, TResult>(IQbservable<TFirst>, IEnumerable<TSecond>, Expression<Func<TFirst, TSecond, TResult>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.zip%60%603(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.collections.generic.ienumerable%7b%60%601%7d%2csystem.linq.expressions.expression%7bsystem.func%7b%60%600%2c%60%601%2c%60%602%7d%7d)(v=VS.103))Merges a queryable observable sequence and an enumerable sequence into one queryable observable sequence by using the selector function.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Zip<TFirst, TSecond, TResult>(IQbservable<TFirst>, IObservable<TSecond>, Expression<Func<TFirst, TSecond, TResult>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.zip%60%603(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.iobservable%7b%60%601%7d%2csystem.linq.expressions.expression%7bsystem.func%7b%60%600%2c%60%601%2c%60%602%7d%7d)(v=VS.103))Merges two queryable observable sequences into one queryable observable sequence by combining their elements in a pairwise fashion.Top

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
