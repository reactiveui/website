# Qbservable.Select Method

Include Protected Members  
Include Inherited Members

Projects each element of a queryable observable sequence.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Select<TSource, TResult>(IQbservable<TSource>, Expression<Func<TSource, TResult>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.select%60%602(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.linq.expressions.expression%7bsystem.func%7b%60%600%2c%60%601%7d%7d)(v=VS.103))Projects each element of a queryable observable sequence into a new form with the specified source and selector.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Select<TSource, TResult>(IQbservable<TSource>, Expression<Func<TSource, Int32, TResult>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.select%60%602(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.linq.expressions.expression%7bsystem.func%7b%60%600%2csystem.int32%2c%60%601%7d%7d)(v=VS.103))Projects each element of a queryable observable sequence into a new form by incorporating the element’s index with the specified source and selector.Top

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
