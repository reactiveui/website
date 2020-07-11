title: Qbservable.Then<TSource, TResult>()
---
# Qbservable.Then\<TSource, TResult\> Method

Matches when the queryable observable sequence has an available value and projects the value.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Then(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    selector As Expression(Of Func(Of TSource, TResult)) _
) As QueryablePlan(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim selector As Expression(Of Func(Of TSource, TResult))
Dim returnValue As QueryablePlan(Of TResult)

returnValue = source.Then(selector)
```

```csharp
public static QueryablePlan<TResult> Then<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, TResult>> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static QueryablePlan<TResult>^ Then(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, TResult>^>^ selector
)
```

```fsharp
static member Then : 
        source:IQbservable<'TSource> * 
        selector:Expression<Func<'TSource, 'TResult>> -> QueryablePlan<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TResult  
  The type of result.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The queryable observable source sequence.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TResult\>\>  
  A transform function to apply to each source element.

#### Return Value

Type: [System.Reactive.Joins.QueryablePlan](QueryablePlan/QueryablePlan(TResult))\<TResult\>  
The queryable observable sequence has an available value and projects the value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
