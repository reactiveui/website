title: VirtualTimeSchedulerBase<TAbsolute, TRelative>.ScheduleRelative<TState>()
---
# VirtualTimeSchedulerBase\<TAbsolute, TRelative\>.ScheduleRelative\<TState\> Method

Schedules an action to be executed at dueTime.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function ScheduleRelative(Of TState) ( _
    state As TState, _
    dueTime As TRelative, _
    action As Func(Of IScheduler, TState, IDisposable) _
) As IDisposable
```

```vb
'Usage
Dim instance As VirtualTimeSchedulerBase
Dim state As TState
Dim dueTime As TRelative
Dim action As Func(Of IScheduler, TState, IDisposable)
Dim returnValue As IDisposable

returnValue = instance.ScheduleRelative(state, _
    dueTime, action)
```

```csharp
public IDisposable ScheduleRelative<TState>(
    TState state,
    TRelative dueTime,
    Func<IScheduler, TState, IDisposable> action
)
```

```c++
public:
generic<typename TState>
IDisposable^ ScheduleRelative(
    TState state, 
    TRelative dueTime, 
    Func<IScheduler^, TState, IDisposable^>^ action
)
```

```fsharp
member ScheduleRelative : 
        state:'TState * 
        dueTime:'TRelative * 
        action:Func<IScheduler, 'TState, IDisposable> -> IDisposable 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The state argument type.

#### Parameters

- state  
  Type: TState  
  State passed to the action to be executed.

- dueTime  
  Type: [TRelative](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)  
  Relative time after which to execute the action.

- action  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<[IScheduler](IScheduler/IScheduler), TState, [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\>  
  Action to be executed.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
The [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9) object used to cancel the scheduled action (best effort).

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)
