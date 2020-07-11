title: Qbservable.FromEventPattern<TEventArgs>(IQbservableProvider, Expression<Action<EventHandler<TEventArgs>>>, Expression<Action<EventHandler<TEventArgs>>>)
---
# Qbservable.FromEventPattern\<TEventArgs\> Method (IQbservableProvider, Expression\<Action\<EventHandler\<TEventArgs\>\>\>, Expression\<Action\<EventHandler\<TEventArgs\>\>\>)

Converts a .NET event, conforming to the standard .NET event pattern, to an queryable observable sequence with the specified add handler and remove handler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromEventPattern(Of TEventArgs As EventArgs) ( _
    provider As IQbservableProvider, _
    addHandler As Expression(Of Action(Of EventHandler(Of TEventArgs))), _
    removeHandler As Expression(Of Action(Of EventHandler(Of TEventArgs))) _
) As IQbservable(Of EventPattern(Of TEventArgs))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim addHandler As Expression(Of Action(Of EventHandler(Of TEventArgs)))
Dim removeHandler As Expression(Of Action(Of EventHandler(Of TEventArgs)))
Dim returnValue As IQbservable(Of EventPattern(Of TEventArgs))

returnValue = provider.FromEventPattern(addHandler, _
    removeHandler)
```

```csharp
public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs>(
    this IQbservableProvider provider,
    Expression<Action<EventHandler<TEventArgs>>> addHandler,
    Expression<Action<EventHandler<TEventArgs>>> removeHandler
)
where TEventArgs : EventArgs
```

```c++
[ExtensionAttribute]
public:
generic<typename TEventArgs>
where TEventArgs : EventArgs
static IQbservable<EventPattern<TEventArgs>^>^ FromEventPattern(
    IQbservableProvider^ provider, 
    Expression<Action<EventHandler<TEventArgs>^>^>^ addHandler, 
    Expression<Action<EventHandler<TEventArgs>^>^>^ removeHandler
)
```

```fsharp
static member FromEventPattern : 
        provider:IQbservableProvider * 
        addHandler:Expression<Action<EventHandler<'TEventArgs>>> * 
        removeHandler:Expression<Action<EventHandler<'TEventArgs>>> -> IQbservable<EventPattern<'TEventArgs>>  when 'TEventArgs : EventArgs
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TEventArgs  
  The type for the event.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- addHandler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[EventHandler](https://msdn.microsoft.com/en-us/library/db0etb8x)\<TEventArgs\>\>\>  
  The action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[EventHandler](https://msdn.microsoft.com/en-us/library/db0etb8x)\<TEventArgs\>\>\>  
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









# Qbservable.FromEventPattern\<TEventArgs\> Method (IQbservableProvider, Type, String)

Converts a .NET event, conforming to the standard .NET event pattern, to a queryable observable sequence, using reflection to find a static event.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromEventPattern(Of TEventArgs As EventArgs) ( _
    provider As IQbservableProvider, _
    type As Type, _
    eventName As String _
) As IQbservable(Of EventPattern(Of TEventArgs))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim type As Type
Dim eventName As String
Dim returnValue As IQbservable(Of EventPattern(Of TEventArgs))

returnValue = provider.FromEventPattern(type, _
    eventName)
```

```csharp
public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs>(
    this IQbservableProvider provider,
    Type type,
    string eventName
)
where TEventArgs : EventArgs
```

```c++
[ExtensionAttribute]
public:
generic<typename TEventArgs>
where TEventArgs : EventArgs
static IQbservable<EventPattern<TEventArgs>^>^ FromEventPattern(
    IQbservableProvider^ provider, 
    Type^ type, 
    String^ eventName
)
```

```fsharp
static member FromEventPattern : 
        provider:IQbservableProvider * 
        type:Type * 
        eventName:string -> IQbservable<EventPattern<'TEventArgs>>  when 'TEventArgs : EventArgs
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TEventArgs  
  The type for the event.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- type  
  Type: [System.Type](https://msdn.microsoft.com/en-us/library/42892f65)  
  The type that exposes the static event to convert.

- eventName  
  Type: [System.String](https://msdn.microsoft.com/en-us/library/s1wwdcbf)  
  The name of the event to convert.

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









# Qbservable.FromEventPattern\<TEventArgs\> Method (IQbservableProvider, Object, String)

Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence, using reflection to find an instance event.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromEventPattern(Of TEventArgs As EventArgs) ( _
    provider As IQbservableProvider, _
    target As Object, _
    eventName As String _
) As IQbservable(Of EventPattern(Of TEventArgs))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim target As Object
Dim eventName As String
Dim returnValue As IQbservable(Of EventPattern(Of TEventArgs))

returnValue = provider.FromEventPattern(target, _
    eventName)
```

```csharp
public static IQbservable<EventPattern<TEventArgs>> FromEventPattern<TEventArgs>(
    this IQbservableProvider provider,
    Object target,
    string eventName
)
where TEventArgs : EventArgs
```

```c++
[ExtensionAttribute]
public:
generic<typename TEventArgs>
where TEventArgs : EventArgs
static IQbservable<EventPattern<TEventArgs>^>^ FromEventPattern(
    IQbservableProvider^ provider, 
    Object^ target, 
    String^ eventName
)
```

```fsharp
static member FromEventPattern : 
        provider:IQbservableProvider * 
        target:Object * 
        eventName:string -> IQbservable<EventPattern<'TEventArgs>>  when 'TEventArgs : EventArgs
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TEventArgs  
  The type for the event.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- target  
  Type: [System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  Th object instance that exposes the event to convert.

- eventName  
  Type: [System.String](https://msdn.microsoft.com/en-us/library/s1wwdcbf)  
  The name of the event to convert.

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








