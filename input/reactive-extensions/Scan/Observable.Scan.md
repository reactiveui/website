# Observable.Scan Method

Include Protected Members  
Include Inherited Members

Applies an accumulator function over an observable sequence and returns each intermediate result.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Scan<TSource>(IObservable<TSource>, Func<TSource, TSource, TSource>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.scan%60%601(system.iobservable%7b%60%600%7d%2csystem.func%7b%60%600%2c%60%600%2c%60%600%7d)(v=VS.103))Applies an accumulator function over an observable sequence and returns each intermediate result with the specified source and accumulator.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Scan<TSource, TAccumulate>(IObservable<TSource>, TAccumulate, Func<TAccumulate, TSource, TAccumulate>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.scan%60%602(system.iobservable%7b%60%600%7d%2c%60%601%2csystem.func%7b%60%601%2c%60%600%2c%60%601%7d)(v=VS.103))Applies an accumulator function over an observable sequence and returns each intermediate result with the specified source, seed and accumulator.Top

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
