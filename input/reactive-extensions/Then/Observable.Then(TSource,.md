title: Observable.Then<TSource, TResult>()
---
# Observable.Then\<TSource, TResult\> Method

Matches when the observable sequence has an available value and projects the value.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Then(Of TSource, TResult) ( _
    source As IObservable(Of TSource), _
    selector As Func(Of TSource, TResult) _
) As Plan(Of TResult)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim selector As Func(Of TSource, TResult)
Dim returnValue As Plan(Of TResult)

returnValue = source.Then(selector)
```

```csharp
public static Plan<TResult> Then<TSource, TResult>(
    this IObservable<TSource> source,
    Func<TSource, TResult> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static Plan<TResult>^ Then(
    IObservable<TSource>^ source, 
    Func<TSource, TResult>^ selector
)
```

```fsharp
static member Then : 
        source:IObservable<'TSource> * 
        selector:Func<'TSource, 'TResult> -> Plan<'TResult> 
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
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The observable source sequence.

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TResult\>  
  A transform function to apply to each source element.

#### Return Value

Type: [System.Reactive.Joins.Plan](Plan\Plan(TResult).md)\<TResult\>  
The observable sequence has an available value and projects the value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
