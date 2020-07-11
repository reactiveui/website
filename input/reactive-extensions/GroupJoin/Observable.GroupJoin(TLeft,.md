title: Observable.GroupJoin<TLeft, TRight, TLeftDuration, TRightDuration, TResult>()
---
# Observable.GroupJoin\<TLeft, TRight, TLeftDuration, TRightDuration, TResult\> Method

Correlates the elements of two sequences based on overlapping durations, and groups the results.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupJoin(Of TLeft, TRight, TLeftDuration, TRightDuration, TResult) ( _
    left As IObservable(Of TLeft), _
    right As IObservable(Of TRight), _
    leftDurationSelector As Func(Of TLeft, IObservable(Of TLeftDuration)), _
    rightDurationSelector As Func(Of TRight, IObservable(Of TRightDuration)), _
    resultSelector As Func(Of TLeft, IObservable(Of TRight), TResult) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim left As IObservable(Of TLeft)
Dim right As IObservable(Of TRight)
Dim leftDurationSelector As Func(Of TLeft, IObservable(Of TLeftDuration))
Dim rightDurationSelector As Func(Of TRight, IObservable(Of TRightDuration))
Dim resultSelector As Func(Of TLeft, IObservable(Of TRight), TResult)
Dim returnValue As IObservable(Of TResult)

returnValue = left.GroupJoin(right, _
    leftDurationSelector, rightDurationSelector, _
    resultSelector)
```

```csharp
public static IObservable<TResult> GroupJoin<TLeft, TRight, TLeftDuration, TRightDuration, TResult>(
    this IObservable<TLeft> left,
    IObservable<TRight> right,
    Func<TLeft, IObservable<TLeftDuration>> leftDurationSelector,
    Func<TRight, IObservable<TRightDuration>> rightDurationSelector,
    Func<TLeft, IObservable<TRight>, TResult> resultSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TLeft, typename TRight, typename TLeftDuration, typename TRightDuration, typename TResult>
static IObservable<TResult>^ GroupJoin(
    IObservable<TLeft>^ left, 
    IObservable<TRight>^ right, 
    Func<TLeft, IObservable<TLeftDuration>^>^ leftDurationSelector, 
    Func<TRight, IObservable<TRightDuration>^>^ rightDurationSelector, 
    Func<TLeft, IObservable<TRight>^, TResult>^ resultSelector
)
```

```fsharp
static member GroupJoin : 
        left:IObservable<'TLeft> * 
        right:IObservable<'TRight> * 
        leftDurationSelector:Func<'TLeft, IObservable<'TLeftDuration>> * 
        rightDurationSelector:Func<'TRight, IObservable<'TRightDuration>> * 
        resultSelector:Func<'TLeft, IObservable<'TRight>, 'TResult> -> IObservable<'TResult> 
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
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TLeft\>  
  The left observable sequence to join elements for.

- right  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TRight\>  
  The right observable sequence to join elements for.

- leftDurationSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TLeft, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TLeftDuration\>\>  
  A function to select the duration of each element of the left observable sequence, used to determine overlap.

- rightDurationSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TRight, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TRightDuration\>\>  
  A function to select the duration of each element of the right observable sequence, used to determine overlap.

- resultSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TLeft, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TRight\>, TResult\>  
  A function invoked to compute a result element for any element of the left sequence with overlapping elements from the right observable sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence that contains result elements computed from source elements that have an overlapping duration.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TLeft\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








