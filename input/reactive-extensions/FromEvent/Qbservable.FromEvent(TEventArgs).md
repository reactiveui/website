title: Qbservable.FromEvent<TEventArgs>(IQbservableProvider, Expression<Action<Action<TEventArgs>>>, Expression<Action<Action<TEventArgs>>>)
---
# Qbservable.FromEvent\<TEventArgs\> Method (IQbservableProvider, Expression\<Action\<Action\<TEventArgs\>\>\>, Expression\<Action\<Action\<TEventArgs\>\>\>)

Converts a .NET event to a queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromEvent(Of TEventArgs) ( _
    provider As IQbservableProvider, _
    addHandler As Expression(Of Action(Of Action(Of TEventArgs))), _
    removeHandler As Expression(Of Action(Of Action(Of TEventArgs))) _
) As IQbservable(Of TEventArgs)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim addHandler As Expression(Of Action(Of Action(Of TEventArgs)))
Dim removeHandler As Expression(Of Action(Of Action(Of TEventArgs)))
Dim returnValue As IQbservable(Of TEventArgs)

returnValue = provider.FromEvent(addHandler, _
    removeHandler)
```

```csharp
public static IQbservable<TEventArgs> FromEvent<TEventArgs>(
    this IQbservableProvider provider,
    Expression<Action<Action<TEventArgs>>> addHandler,
    Expression<Action<Action<TEventArgs>>> removeHandler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TEventArgs>
static IQbservable<TEventArgs>^ FromEvent(
    IQbservableProvider^ provider, 
    Expression<Action<Action<TEventArgs>^>^>^ addHandler, 
    Expression<Action<Action<TEventArgs>^>^>^ removeHandler
)
```

```fsharp
static member FromEvent : 
        provider:IQbservableProvider * 
        addHandler:Expression<Action<Action<'TEventArgs>>> * 
        removeHandler:Expression<Action<Action<'TEventArgs>>> -> IQbservable<'TEventArgs> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TEventArgs  
  The type of event.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- addHandler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TEventArgs\>\>\>  
  Action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TEventArgs\>\>\>  
  Action that detaches the given event handler from the underlying .NET event.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TEventArgs\>  
Observable sequence that contains data representations of invocations of the underlying .NET event.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[FromEvent Overload](FromEvent/Qbservable.FromEvent)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








