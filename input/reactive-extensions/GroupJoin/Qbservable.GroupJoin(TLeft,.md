title: Qbservable.GroupJoin<TLeft, TRight, TLeftDuration, TRightDuration, TResult>()
---
# Qbservable.GroupJoin\<TLeft, TRight, TLeftDuration, TRightDuration, TResult\> Method

Correlates the elements of two sequences based on overlapping durations, and groups the results.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupJoin(Of TLeft, TRight, TLeftDuration, TRightDuration, TResult) ( _
    left As IQbservable(Of TLeft), _
    right As IObservable(Of TRight), _
    leftDurationSelector As Expression(Of Func(Of TLeft, IObservable(Of TLeftDuration))), _
    rightDurationSelector As Expression(Of Func(Of TRight, IObservable(Of TRightDuration))), _
    resultSelector As Expression(Of Func(Of TLeft, IObservable(Of TRight), TResult)) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim left As IQbservable(Of TLeft)
Dim right As IObservable(Of TRight)
Dim leftDurationSelector As Expression(Of Func(Of TLeft, IObservable(Of TLeftDuration)))
Dim rightDurationSelector As Expression(Of Func(Of TRight, IObservable(Of TRightDuration)))
Dim resultSelector As Expression(Of Func(Of TLeft, IObservable(Of TRight), TResult))
Dim returnValue As IQbservable(Of TResult)

returnValue = left.GroupJoin(right, _
    leftDurationSelector, rightDurationSelector, _
    resultSelector)
```

```csharp
public static IQbservable<TResult> GroupJoin<TLeft, TRight, TLeftDuration, TRightDuration, TResult>(
    this IQbservable<TLeft> left,
    IObservable<TRight> right,
    Expression<Func<TLeft, IObservable<TLeftDuration>>> leftDurationSelector,
    Expression<Func<TRight, IObservable<TRightDuration>>> rightDurationSelector,
    Expression<Func<TLeft, IObservable<TRight>, TResult>> resultSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TLeft, typename TRight, typename TLeftDuration, typename TRightDuration, typename TResult>
static IQbservable<TResult>^ GroupJoin(
    IQbservable<TLeft>^ left, 
    IObservable<TRight>^ right, 
    Expression<Func<TLeft, IObservable<TLeftDuration>^>^>^ leftDurationSelector, 
    Expression<Func<TRight, IObservable<TRightDuration>^>^>^ rightDurationSelector, 
    Expression<Func<TLeft, IObservable<TRight>^, TResult>^>^ resultSelector
)
```

```fsharp
static member GroupJoin : 
        left:IQbservable<'TLeft> * 
        right:IObservable<'TRight> * 
        leftDurationSelector:Expression<Func<'TLeft, IObservable<'TLeftDuration>>> * 
        rightDurationSelector:Expression<Func<'TRight, IObservable<'TRightDuration>>> * 
        resultSelector:Expression<Func<'TLeft, IObservable<'TRight>, 'TResult>> -> IQbservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TLeft  
  The type of left.

- TRight  
  The type of right.

- TLeftDuration  
  The type of left duration.

- TRightDuration  
  The type of right duration.

- TResult  
  The type of result.

#### Parameters

- left  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TLeft\>  
  The left queryable observable sequence to join elements for.

- right  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TRight\>  
  The right observable sequence to join elements for.

- leftDurationSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TLeft, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TLeftDuration\>\>\>  
  A function to select the duration of each element of the left queryable observable sequence, used to determine overlap.

- rightDurationSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TRight, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TRightDuration\>\>\>  
  A function to select the duration of each element of the right queryable observable sequence, used to determine overlap.

- resultSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TLeft, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TRight\>, TResult\>\>  
  A function invoked to compute a result element for any element of the left sequence with overlapping elements from the right queryable observable sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>  
A queryable observable sequence that contains result elements computed from source elements that have an overlapping duration.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TLeft\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








