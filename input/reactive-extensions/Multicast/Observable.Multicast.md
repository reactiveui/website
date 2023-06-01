title: Observable.Multicast()
---
# Observable.Multicast Method

Include Protected Members  
Include Inherited Members

Returns an observable sequence that contains elements.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Multicast<TSource, TResult>(IObservable<TSource>, ISubject<TSource, TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.multicast%60%602(system.iobservable%7b%60%600%7d%2csystem.reactive.subjects.isubject%7b%60%600%2c%60%601%7d)(v=VS.103))Returns a connectable observable sequence that upon connection causes the source sequence to push results into the specified subject.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Multicast<TSource, TIntermediate, TResult>(IObservable<TSource>, Func<ISubject<TSource, TIntermediate>>, Func<IObservable<TIntermediate>, IObservable<TResult>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.multicast%60%603(system.iobservable%7b%60%600%7d%2csystem.func%7bsystem.reactive.subjects.isubject%7b%60%600%2c%60%601%7d%7d%2csystem.func%7bsystem.iobservable%7b%60%601%7d%2csystem.iobservable%7b%60%602%7d%7d)(v=VS.103))Returns an observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.Top

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)




