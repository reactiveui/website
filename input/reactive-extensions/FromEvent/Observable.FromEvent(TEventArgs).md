title: Observable.FromEvent<TEventArgs>(Action<Action<TEventArgs>>, Action<Action<TEventArgs>>)
---
# Observable.FromEvent\<TEventArgs\> Method (Action\<Action\<TEventArgs\>\>, Action\<Action\<TEventArgs\>\>)

Converts a .NET event to an observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromEvent(Of TEventArgs) ( _
    addHandler As Action(Of Action(Of TEventArgs)), _
    removeHandler As Action(Of Action(Of TEventArgs)) _
) As IObservable(Of TEventArgs)
```

```vb
'Usage
Dim addHandler As Action(Of Action(Of TEventArgs))
Dim removeHandler As Action(Of Action(Of TEventArgs))
Dim returnValue As IObservable(Of TEventArgs)

returnValue = Observable.FromEvent(addHandler, _
    removeHandler)
```

```csharp
public static IObservable<TEventArgs> FromEvent<TEventArgs>(
    Action<Action<TEventArgs>> addHandler,
    Action<Action<TEventArgs>> removeHandler
)
```

```c++
public:
generic<typename TEventArgs>
static IObservable<TEventArgs>^ FromEvent(
    Action<Action<TEventArgs>^>^ addHandler, 
    Action<Action<TEventArgs>^>^ removeHandler
)
```

```fsharp
static member FromEvent : 
        addHandler:Action<Action<'TEventArgs>> * 
        removeHandler:Action<Action<'TEventArgs>> -> IObservable<'TEventArgs> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TEventArgs  
  The type of event.

#### Parameters

- addHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TEventArgs\>\>  
  Action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TEventArgs\>\>  
  Action that detaches the given event handler from the underlying .NET event.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TEventArgs\>  
Observable sequence that contains data representations of invocations of the underlying .NET event.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromEvent Overload](FromEvent/Observable.FromEvent)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)







