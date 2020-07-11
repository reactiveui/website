title: Observable.FromEvent(Action<Action>, Action<Action>)
---
# Observable.FromEvent Method (Action\<Action\>, Action\<Action\>)

Converts a .NET event to an observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromEvent ( _
    addHandler As Action(Of Action), _
    removeHandler As Action(Of Action) _
) As IObservable(Of Unit)
```

```vb
'Usage
Dim addHandler As Action(Of Action)
Dim removeHandler As Action(Of Action)
Dim returnValue As IObservable(Of Unit)

returnValue = Observable.FromEvent(addHandler, _
    removeHandler)
```

```csharp
public static IObservable<Unit> FromEvent(
    Action<Action> addHandler,
    Action<Action> removeHandler
)
```

```c++
public:
static IObservable<Unit>^ FromEvent(
    Action<Action^>^ addHandler, 
    Action<Action^>^ removeHandler
)
```

```fsharp
static member FromEvent : 
        addHandler:Action<Action> * 
        removeHandler:Action<Action> -> IObservable<Unit> 
```

```jscript
public static function FromEvent(
    addHandler : Action<Action>, 
    removeHandler : Action<Action>
) : IObservable<Unit>
```

#### Parameters

- addHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Action](https://msdn.microsoft.com/en-us/library/Bb534741)\>  
  Action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Action](https://msdn.microsoft.com/en-us/library/Bb534741)\>  
  Action that detaches the given event handler from the underlying .NET event.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit/Unit)\>  
Observable sequence that contains data representations of invocations of the underlying .NET event.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromEvent Overload](FromEvent/Observable.FromEvent)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)







# Observable.FromEvent Method

Include Protected Members  
Include Inherited Members

Converts a .NET event to an observable sequence.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEvent<TEventArgs>(Action<Action<TEventArgs>>, Action<Action<TEventArgs>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.fromevent%60%601(system.action%7bsystem.action%7b%60%600%7d%7d%2csystem.action%7bsystem.action%7b%60%600%7d%7d)(v=VS.103))Converts a .NET event to an observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEvent(Action<Action>, Action<Action>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.fromevent(system.action%7bsystem.action%7d%2csystem.action%7bsystem.action%7d)(v=VS.103))Converts a .NET event to an observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEvent<TDelegate, TEventArgs>(Action<TDelegate>, Action<TDelegate>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.fromevent%60%602(system.action%7b%60%600%7d%2csystem.action%7b%60%600%7d)(v=VS.103))Converts a .NET event to an observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEvent<TDelegate, TEventArgs>(Func<Action<TEventArgs>, TDelegate>, Action<TDelegate>, Action<TDelegate>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.fromevent%60%602(system.func%7bsystem.action%7b%60%601%7d%2c%60%600%7d%2csystem.action%7b%60%600%7d%2csystem.action%7b%60%600%7d)(v=VS.103))Converts a .NET event to an observable sequence.Top

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)




