title: Qbservable.Scan()
---
# Qbservable.Scan Method

Include Protected Members  
Include Inherited Members

Applies an accumulator function over a queryable observable sequence and returns each intermediate result.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Scan<TSource>(IQbservable<TSource>, Expression<Func<TSource, TSource, TSource>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.scan%60%601(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.linq.expressions.expression%7bsystem.func%7b%60%600%2c%60%600%2c%60%600%7d%7d)(v=VS.103))Applies an accumulator function over a queryable observable sequence and returns each intermediate result with the specified source and accumulator.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Scan<TSource, TAccumulate>(IQbservable<TSource>, TAccumulate, Expression<Func<TAccumulate, TSource, TAccumulate>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.scan%60%602(system.reactive.linq.iqbservable%7b%60%600%7d%2c%60%601%2csystem.linq.expressions.expression%7bsystem.func%7b%60%601%2c%60%600%2c%60%601%7d%7d)(v=VS.103))Applies an accumulator function over a queryable observable sequence and returns each intermediate result with the specified source, seed and accumulator.Top

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
