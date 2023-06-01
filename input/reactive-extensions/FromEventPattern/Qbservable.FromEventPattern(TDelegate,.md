title: Qbservable.FromEventPattern<TDelegate, TEventArgs>(IQbservableProvider, Expression<Func<EventHandler<TEventArgs>, TDelegate>>, Expression<Action<TDelegate>>, Expression<Action<TDelegate>>)
---
# Qbservable.FromEventPattern\<TDelegate, TEventArgs\> Method (IQbservableProvider, Expression\<Func\<EventHandler\<TEventArgs\>, TDelegate\>\>, Expression\<Action\<TDelegate\>\>, Expression\<Action\<TDelegate\>\>)

Converts a .NET event, conforming to the standard .NET event pattern, to an queryable observable sequence with the specified conversion, add handler and remove handler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromEventPattern(Of TDelegate, TEventArgs As EventArgs) ( _
    provider As IQbservableProvider, _
    conversion As Expression(Of Func(Of EventHandler(Of TEventArgs), TDelegate)), _
    addHandler As Expression(Of Action(Of TDelegate)), _
    removeHandler As Expression(Of Action(Of TDelegate)) _
) As IQbservable(Of EventPattern(Of TEventArgs))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim conversion As Expression(Of Func(Of EventHandler(Of TEventArgs), TDelegate))
Dim addHandler As Expression(Of Action(Of TDelegate))
Dim removeHandler As Expression(Of Action(Of TDelegate))
Dim returnValue As IQbservable(Of EventPattern(Of TEventArgs))

returnValue = provider.FromEventPattern(conversion, _
    addHandler, removeHandler)
```

```csharp
public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TDelegate, TEventArgs>(
    this IQbservableProvider provider,
    Expression<Func<EventHandler<TEventArgs>, TDelegate>> conversion,
    Expression<Action<TDelegate>> addHandler,
    Expression<Action<TDelegate>> removeHandler
)
where TEventArgs : EventArgs
```

```c++
[ExtensionAttribute]
public:
generic<typename TDelegate, typename TEventArgs>
where TEventArgs : EventArgs
static IQbservable<EventPattern<TEventArgs>^>^ FromEventPattern(
    IQbservableProvider^ provider, 
    Expression<Func<EventHandler<TEventArgs>^, TDelegate>^>^ conversion, 
    Expression<Action<TDelegate>^>^ addHandler, 
    Expression<Action<TDelegate>^>^ removeHandler
)
```

```fsharp
static member FromEventPattern : 
        provider:IQbservableProvider * 
        conversion:Expression<Func<EventHandler<'TEventArgs>, 'TDelegate>> * 
        addHandler:Expression<Action<'TDelegate>> * 
        removeHandler:Expression<Action<'TDelegate>> -> IQbservable<EventPattern<'TEventArgs>>  when 'TEventArgs : EventArgs
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TDelegate  
  The type of delegate.

- TEventArgs  
  The type for the event.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- conversion  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[EventHandler](https://msdn.microsoft.com/en-us/library/db0etb8x)\<TEventArgs\>, TDelegate\>\>  
  A function used to convert the given event handler to a delegate compatible with the underlying .NET event.

- addHandler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>\>  
  The action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>\>  
  The action that detaches the given event handler from the underlying .NET event.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[EventPattern](EventPattern/EventPattern(TEventArgs))\<TEventArgs\>\>  
The queryable observable sequence that contains data representations of invocations of the underlying .NET event.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[FromEventPattern Overload](FromEventPattern/Qbservable.FromEventPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.FromEventPattern\<TDelegate, TEventArgs\> Method (IQbservableProvider, Expression\<Action\<TDelegate\>\>, Expression\<Action\<TDelegate\>\>)

Converts a .NET event, conforming to the standard .NET event pattern, to an queryable observable sequence with the specified add handler and remove handler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromEventPattern(Of TDelegate, TEventArgs As EventArgs) ( _
    provider As IQbservableProvider, _
    addHandler As Expression(Of Action(Of TDelegate)), _
    removeHandler As Expression(Of Action(Of TDelegate)) _
) As IQbservable(Of EventPattern(Of TEventArgs))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim addHandler As Expression(Of Action(Of TDelegate))
Dim removeHandler As Expression(Of Action(Of TDelegate))
Dim returnValue As IQbservable(Of EventPattern(Of TEventArgs))

returnValue = provider.FromEventPattern(addHandler, _
    removeHandler)
```

```csharp
public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TDelegate, TEventArgs>(
    this IQbservableProvider provider,
    Expression<Action<TDelegate>> addHandler,
    Expression<Action<TDelegate>> removeHandler
)
where TEventArgs : EventArgs
```

```c++
[ExtensionAttribute]
public:
generic<typename TDelegate, typename TEventArgs>
where TEventArgs : EventArgs
static IQbservable<EventPattern<TEventArgs>^>^ FromEventPattern(
    IQbservableProvider^ provider, 
    Expression<Action<TDelegate>^>^ addHandler, 
    Expression<Action<TDelegate>^>^ removeHandler
)
```

```fsharp
static member FromEventPattern : 
        provider:IQbservableProvider * 
        addHandler:Expression<Action<'TDelegate>> * 
        removeHandler:Expression<Action<'TDelegate>> -> IQbservable<EventPattern<'TEventArgs>>  when 'TEventArgs : EventArgs
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TDelegate  
  The type of delegate.

- TEventArgs  
  The type for the event.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- addHandler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>\>  
  The action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>\>  
  The action that detaches the given event handler from the underlying .NET event.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[EventPattern](EventPattern/EventPattern(TEventArgs))\<TEventArgs\>\>  
The queryable observable sequence that contains data representations of invocations of the underlying .NET event.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[FromEventPattern Overload](FromEventPattern/Qbservable.FromEventPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








