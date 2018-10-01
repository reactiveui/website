# Observable.PublishLast Method

Include Protected Members  
Include Inherited Members

Returns a connectable observable sequence that shares a single subscription that contains the last notification only.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[PublishLast<TSource>(IObservable<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.publishlast%60%601(system.iobservable%7b%60%600%7d)(v=VS.103))Returns a connectable observable sequence that shares a single subscription to the underlying sequence containing only the last notification.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[PublishLast<TSource, TResult>(IObservable<TSource>, Func<IObservable<TSource>, IObservable<TResult>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.publishlast%60%602(system.iobservable%7b%60%600%7d%2csystem.func%7bsystem.iobservable%7b%60%600%7d%2csystem.iobservable%7b%60%601%7d%7d)(v=VS.103))Returns an observable sequence that is the result of invoking the selector on a connectable observable sequence that shares a single subscription to the underlying sequence containing only the last notification.Top

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
