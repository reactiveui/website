title: Observable.Zip()
---
# Observable.Zip Method

Include Protected Members  
Include Inherited Members

Merges two observable sequences into one observable sequence.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Zip<TFirst, TSecond, TResult>(IObservable<TFirst>, IEnumerable<TSecond>, Func<TFirst, TSecond, TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.zip%60%603(system.iobservable%7b%60%600%7d%2csystem.collections.generic.ienumerable%7b%60%601%7d%2csystem.func%7b%60%600%2c%60%601%2c%60%602%7d)(v=VS.103))Merges an observable sequence and an enumerable sequence into one observable sequence by using the selector function.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Zip<TFirst, TSecond, TResult>(IObservable<TFirst>, IObservable<TSecond>, Func<TFirst, TSecond, TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.zip%60%603(system.iobservable%7b%60%600%7d%2csystem.iobservable%7b%60%601%7d%2csystem.func%7b%60%600%2c%60%601%2c%60%602%7d)(v=VS.103))Merges two observable sequences into one observable sequence by combining their elements in a pairwise fashion.Top

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
