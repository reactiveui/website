# Observable.And\<TLeft, TRight\> Method

Matches when both observable sequences have an available value.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function And(Of TLeft, TRight) ( _
    left As IObservable(Of TLeft), _
    right As IObservable(Of TRight) _
) As Pattern(Of TLeft, TRight)
```

```vb
'Usage
Dim left As IObservable(Of TLeft)
Dim right As IObservable(Of TRight)
Dim returnValue As Pattern(Of TLeft, TRight)

returnValue = left.And(right)
```

```csharp
public static Pattern<TLeft, TRight> And<TLeft, TRight>(
    this IObservable<TLeft> left,
    IObservable<TRight> right
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TLeft, typename TRight>
static Pattern<TLeft, TRight>^ And(
    IObservable<TLeft>^ left, 
    IObservable<TRight>^ right
)
```

```fsharp
static member And : 
        left:IObservable<'TLeft> * 
        right:IObservable<'TRight> -> Pattern<'TLeft, 'TRight> 
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
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TLeft\>  
  The left observable sequence have an available value.

- right  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TRight\>  
  The right observable sequence have an available value.

#### Return Value

Type: [System.Reactive.Joins.Pattern](Pattern\Pattern(T1,.md)\<TLeft, TRight\>  
The observable sequences have an available value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TLeft\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








