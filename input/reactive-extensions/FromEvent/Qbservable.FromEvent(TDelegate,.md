title: Qbservable.FromEvent<TDelegate, TEventArgs>(IQbservableProvider, Expression<Action<TDelegate>>, Expression<Action<TDelegate>>)
---
# Qbservable.FromEvent\<TDelegate, TEventArgs\> Method (IQbservableProvider, Expression\<Action\<TDelegate\>\>, Expression\<Action\<TDelegate\>\>)

Converts a .NET event to a queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromEvent(Of TDelegate, TEventArgs) ( _
    provider As IQbservableProvider, _
    addHandler As Expression(Of Action(Of TDelegate)), _
    removeHandler As Expression(Of Action(Of TDelegate)) _
) As IQbservable(Of TEventArgs)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim addHandler As Expression(Of Action(Of TDelegate))
Dim removeHandler As Expression(Of Action(Of TDelegate))
Dim returnValue As IQbservable(Of TEventArgs)

returnValue = provider.FromEvent(addHandler, _
    removeHandler)
```

```csharp
public static IQbservable<TEventArgs> FromEvent<TDelegate, TEventArgs>(
    this IQbservableProvider provider,
    Expression<Action<TDelegate>> addHandler,
    Expression<Action<TDelegate>> removeHandler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TDelegate, typename TEventArgs>
static IQbservable<TEventArgs>^ FromEvent(
    IQbservableProvider^ provider, 
    Expression<Action<TDelegate>^>^ addHandler, 
    Expression<Action<TDelegate>^>^ removeHandler
)
```

```fsharp
static member FromEvent : 
        provider:IQbservableProvider * 
        addHandler:Expression<Action<'TDelegate>> * 
        removeHandler:Expression<Action<'TDelegate>> -> IQbservable<'TEventArgs> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TDelegate  
  The type of delegate.

- TEventArgs  
  The type of event.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- addHandler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>\>  
  Action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>\>  
  Action that detaches the given event handler from the underlying .NET event.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TEventArgs\>  
The queryable observable sequence that contains data representations of invocations of the underlying .NET event.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromEvent Overload](FromEvent\Qbservable.FromEvent.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromEvent\<TDelegate, TEventArgs\> Method (IQbservableProvider, Expression\<Func\<Action\<TEventArgs\>, TDelegate\>\>, Expression\<Action\<TDelegate\>\>, Expression\<Action\<TDelegate\>\>)

Converts a .NET event to a queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromEvent(Of TDelegate, TEventArgs) ( _
    provider As IQbservableProvider, _
    conversion As Expression(Of Func(Of Action(Of TEventArgs), TDelegate)), _
    addHandler As Expression(Of Action(Of TDelegate)), _
    removeHandler As Expression(Of Action(Of TDelegate)) _
) As IQbservable(Of TEventArgs)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim conversion As Expression(Of Func(Of Action(Of TEventArgs), TDelegate))
Dim addHandler As Expression(Of Action(Of TDelegate))
Dim removeHandler As Expression(Of Action(Of TDelegate))
Dim returnValue As IQbservable(Of TEventArgs)

returnValue = provider.FromEvent(conversion, _
    addHandler, removeHandler)
```

```csharp
public static IQbservable<TEventArgs> FromEvent<TDelegate, TEventArgs>(
    this IQbservableProvider provider,
    Expression<Func<Action<TEventArgs>, TDelegate>> conversion,
    Expression<Action<TDelegate>> addHandler,
    Expression<Action<TDelegate>> removeHandler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TDelegate, typename TEventArgs>
static IQbservable<TEventArgs>^ FromEvent(
    IQbservableProvider^ provider, 
    Expression<Func<Action<TEventArgs>^, TDelegate>^>^ conversion, 
    Expression<Action<TDelegate>^>^ addHandler, 
    Expression<Action<TDelegate>^>^ removeHandler
)
```

```fsharp
static member FromEvent : 
        provider:IQbservableProvider * 
        conversion:Expression<Func<Action<'TEventArgs>, 'TDelegate>> * 
        addHandler:Expression<Action<'TDelegate>> * 
        removeHandler:Expression<Action<'TDelegate>> -> IQbservable<'TEventArgs> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TDelegate  
  The type of delegate.

- TEventArgs  
  The type of event.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- conversion  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TEventArgs\>, TDelegate\>\>  
  A function used to convert the given event handler to a delegate compatible with the underlying .NET event. The resulting delegate is used in calls to the addHandler and removeHandler action parameters.

- addHandler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>\>  
  Action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>\>  
  Action that detaches the given event handler from the underlying .NET event.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TEventArgs\>  
The queryable observable sequence that contains data representations of invocations of the underlying .NET event.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromEvent Overload](FromEvent\Qbservable.FromEvent.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








