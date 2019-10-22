title: Observable.Select()
---
# Observable.Select Method

Include Protected Members  
Include Inherited Members

Projects each element of an observable sequence.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Select<TSource, TResult>(IObservable<TSource>, Func<TSource, TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.select%60%602(system.iobservable%7b%60%600%7d%2csystem.func%7b%60%600%2c%60%601%7d)(v=VS.103))Projects each element of an observable sequence into a new form with the specified source and selector.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Select<TSource, TResult>(IObservable<TSource>, Func<TSource, Int32, TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.select%60%602(system.iobservable%7b%60%600%7d%2csystem.func%7b%60%600%2csystem.int32%2c%60%601%7d)(v=VS.103))Projects each element of an observable sequence into a new form by incorporating the element’s index with the specified source and selector.Top

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
