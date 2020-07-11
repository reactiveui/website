title: Qbservable.And<TLeft, TRight>()
---
# Qbservable.And\<TLeft, TRight\> Method

Matches when both queryable observable sequences have an available value.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function And(Of TLeft, TRight) ( _
    left As IQbservable(Of TLeft), _
    right As IObservable(Of TRight) _
) As QueryablePattern(Of TLeft, TRight)
```

```vb
'Usage
Dim left As IQbservable(Of TLeft)
Dim right As IObservable(Of TRight)
Dim returnValue As QueryablePattern(Of TLeft, TRight)

returnValue = left.And(right)
```

```csharp
public static QueryablePattern<TLeft, TRight> And<TLeft, TRight>(
    this IQbservable<TLeft> left,
    IObservable<TRight> right
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TLeft, typename TRight>
static QueryablePattern<TLeft, TRight>^ And(
    IQbservable<TLeft>^ left, 
    IObservable<TRight>^ right
)
```

```fsharp
static member And : 
        left:IQbservable<'TLeft> * 
        right:IObservable<'TRight> -> QueryablePattern<'TLeft, 'TRight> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TLeft  
  The type of left.

- TRight  
  The type of right

#### Parameters

- left  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TLeft\>  
  The left queryable observable sequence have an available value.

- right  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TRight\>  
  The right queryable observable sequence have an available value.

#### Return Value

Type: [System.Reactive.Joins.QueryablePattern](QueryablePattern/QueryablePattern(T1,)\<TLeft, TRight\>  
The queryable observable sequences have an available value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TLeft\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








