title: Observable.FromEventPattern<TDelegate, TEventArgs>(Func<EventHandler<TEventArgs>, TDelegate>, Action<TDelegate>, Action<TDelegate>)
---
# Observable.FromEventPattern\<TDelegate, TEventArgs\> Method (Func\<EventHandler\<TEventArgs\>, TDelegate\>, Action\<TDelegate\>, Action\<TDelegate\>)

Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence with the specified conversion, add handler and remove handler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromEventPattern(Of TDelegate, TEventArgs As EventArgs) ( _
    conversion As Func(Of EventHandler(Of TEventArgs), TDelegate), _
    addHandler As Action(Of TDelegate), _
    removeHandler As Action(Of TDelegate) _
) As IObservable(Of EventPattern(Of TEventArgs))
```

```vb
'Usage
Dim conversion As Func(Of EventHandler(Of TEventArgs), TDelegate)
Dim addHandler As Action(Of TDelegate)
Dim removeHandler As Action(Of TDelegate)
Dim returnValue As IObservable(Of EventPattern(Of TEventArgs))

returnValue = Observable.FromEventPattern(conversion, _
    addHandler, removeHandler)
```

```csharp
public static IObservable<EventPattern<TEventArgs>> FromEventPattern<TDelegate, TEventArgs>(
    Func<EventHandler<TEventArgs>, TDelegate> conversion,
    Action<TDelegate> addHandler,
    Action<TDelegate> removeHandler
)
where TEventArgs : EventArgs
```

```c++
public:
generic<typename TDelegate, typename TEventArgs>
where TEventArgs : EventArgs
static IObservable<EventPattern<TEventArgs>^>^ FromEventPattern(
    Func<EventHandler<TEventArgs>^, TDelegate>^ conversion, 
    Action<TDelegate>^ addHandler, 
    Action<TDelegate>^ removeHandler
)
```

```fsharp
static member FromEventPattern : 
        conversion:Func<EventHandler<'TEventArgs>, 'TDelegate> * 
        addHandler:Action<'TDelegate> * 
        removeHandler:Action<'TDelegate> -> IObservable<EventPattern<'TEventArgs>>  when 'TEventArgs : EventArgs
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TDelegate  
  The type of delegate.

- TEventArgs  
  The type of event.

#### Parameters

- conversion  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[EventHandler](https://msdn.microsoft.com/en-us/library/db0etb8x)\<TEventArgs\>, TDelegate\>  
  A function used to convert the given event handler to a delegate compatible with the underlying .NET event.

- addHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>  
  The action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>  
  The action that detaches the given event handler from the underlying .NET event.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[EventPattern](EventPattern/EventPattern(TEventArgs))\<TEventArgs\>\>  
The observable sequence that contains data representations of invocations of the underlying .NET event.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromEventPattern Overload](FromEventPattern/Observable.FromEventPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromEventPattern\<TDelegate, TEventArgs\> Method (Action\<TDelegate\>, Action\<TDelegate\>)

Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence with the specified add handler and remove handler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromEventPattern(Of TDelegate, TEventArgs As EventArgs) ( _
    addHandler As Action(Of TDelegate), _
    removeHandler As Action(Of TDelegate) _
) As IObservable(Of EventPattern(Of TEventArgs))
```

```vb
'Usage
Dim addHandler As Action(Of TDelegate)
Dim removeHandler As Action(Of TDelegate)
Dim returnValue As IObservable(Of EventPattern(Of TEventArgs))

returnValue = Observable.FromEventPattern(addHandler, _
    removeHandler)
```

```csharp
public static IObservable<EventPattern<TEventArgs>> FromEventPattern<TDelegate, TEventArgs>(
    Action<TDelegate> addHandler,
    Action<TDelegate> removeHandler
)
where TEventArgs : EventArgs
```

```c++
public:
generic<typename TDelegate, typename TEventArgs>
where TEventArgs : EventArgs
static IObservable<EventPattern<TEventArgs>^>^ FromEventPattern(
    Action<TDelegate>^ addHandler, 
    Action<TDelegate>^ removeHandler
)
```

```fsharp
static member FromEventPattern : 
        addHandler:Action<'TDelegate> * 
        removeHandler:Action<'TDelegate> -> IObservable<EventPattern<'TEventArgs>>  when 'TEventArgs : EventArgs
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TDelegate  
  The type of delegate.

- TEventArgs  
  The type of event.

#### Parameters

- addHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>  
  The action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TDelegate\>  
  The action that detaches the given event handler from the underlying .NET event.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[EventPattern](EventPattern/EventPattern(TEventArgs))\<TEventArgs\>\>  
The observable sequence that contains data representations of invocations of the underlying .NET event.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromEventPattern Overload](FromEventPattern/Observable.FromEventPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)







