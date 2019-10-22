title: Qbservable.Finally<TSource>()
---
# Qbservable.Finally\<TSource\> Method

Invokes a specified action after source observable sequence terminates normally or by an exception.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Finally(Of TSource) ( _
    source As IQbservable(Of TSource), _
    finallyAction As Expression(Of Action) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim finallyAction As Expression(Of Action)
Dim returnValue As IQbservable(Of TSource)

returnValue = source.Finally(finallyAction)
```

```csharp
public static IQbservable<TSource> Finally<TSource>(
    this IQbservable<TSource> source,
    Expression<Action> finallyAction
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Finally(
    IQbservable<TSource>^ source, 
    Expression<Action^>^ finallyAction
)
```

```fsharp
static member Finally : 
        source:IQbservable<'TSource> * 
        finallyAction:Expression<Action> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence.

- finallyAction  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Bb534741)\>  
  Action to invoke after the source queryable observable sequence terminates.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
Source sequence with the action-invoking termination behavior applied.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








