title: Qbservable.Distinct()
---
# Qbservable.Distinct Method

Include Protected Members  
Include Inherited Members

Returns a queryable observable sequence that contains only distinct elements.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Distinct<TSource>(IQbservable<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.distinct%60%601(system.reactive.linq.iqbservable%7b%60%600%7d)(v=VS.103))Returns a queryable observable sequence that contains only distinct elements with a specified source.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Distinct<TSource>(IQbservable<TSource>, IEqualityComparer<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.distinct%60%601(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.collections.generic.iequalitycomparer%7b%60%600%7d)(v=VS.103))Returns a queryable observable sequence that contains only distinct elements according to the comparer.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Distinct<TSource, TKey>(IQbservable<TSource>, Expression<Func<TSource, TKey>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.distinct%60%602(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.linq.expressions.expression%7bsystem.func%7b%60%600%2c%60%601%7d%7d)(v=VS.103))Returns a queryable observable sequence that contains only distinct elements according to the keySelector.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Distinct<TSource, TKey>(IQbservable<TSource>, Expression<Func<TSource, TKey>>, IEqualityComparer<TKey>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.distinct%60%602(system.reactive.linq.iqbservable%7b%60%600%7d%2csystem.linq.expressions.expression%7bsystem.func%7b%60%600%2c%60%601%7d%7d%2csystem.collections.generic.iequalitycomparer%7b%60%601%7d)(v=VS.103))Returns a queryable observable sequence that contains only distinct elements according to the keySelector and comparer.Top

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)




