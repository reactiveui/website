# Qbservable.Publish Method

Include Protected Members  
Include Inherited Members

Returns a connectable observable sequence that shares a single subscription.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Publish<TSource, TResult>(IQbservable<TSource>, Expression<Func<IObservable<TSource>, IObservable<TResult>>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.publish%60%602(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.linq.expressions.expression%7bsystem.func%7bsystem.iobservable%7b%60%600%7d%2csystem.iobservable%7b%60%601%7d%7d%7d)(v=VS.103))Returns a queryable observable sequence that is the result of invoking the selector on a connectable queryable observable sequence that shares a single subscription to the underlying sequence.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Publish<TSource, TResult>(IQbservable<TSource>, Expression<Func<IObservable<TSource>, IObservable<TResult>>>, TSource)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.publish%60%602(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.linq.expressions.expression%7bsystem.func%7bsystem.iobservable%7b%60%600%7d%2csystem.iobservable%7b%60%601%7d%7d%7d%2c%60%600)(v=VS.103))Returns a queryable observable sequence that is the result of invoking the selector on a connectable observable sequence that shares a single subscription to the underlying sequence.Top

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)