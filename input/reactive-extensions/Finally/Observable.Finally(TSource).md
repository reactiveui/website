title: Observable.Finally<TSource>()
---
# Observable.Finally\<TSource\> Method

Invokes a specified action after source observable sequence terminates normally or by an exception.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Finally(Of TSource) ( _
    source As IObservable(Of TSource), _
    finallyAction As Action _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim finallyAction As Action
Dim returnValue As IObservable(Of TSource)

returnValue = source.Finally(finallyAction)
```

```csharp
public static IObservable<TSource> Finally<TSource>(
    this IObservable<TSource> source,
    Action finallyAction
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Finally(
    IObservable<TSource>^ source, 
    Action^ finallyAction
)
```

```fsharp
static member Finally : 
        source:IObservable<'TSource> * 
        finallyAction:Action -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence.

- finallyAction  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb534741)  
  Action to invoke after the source observable sequence terminates.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
Source sequence with the action-invoking termination behavior applied.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








