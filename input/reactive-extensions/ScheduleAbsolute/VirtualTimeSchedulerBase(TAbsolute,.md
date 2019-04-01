# VirtualTimeSchedulerBase\<TAbsolute, TRelative\>.ScheduleAbsolute\<TState\> Method

Schedules an action to be executed at dueTime.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public MustOverride Function ScheduleAbsolute(Of TState) ( _
    state As TState, _
    dueTime As TAbsolute, _
    action As Func(Of IScheduler, TState, IDisposable) _
) As IDisposable
```

```vb
'Usage
Dim instance As VirtualTimeSchedulerBase
Dim state As TState
Dim dueTime As TAbsolute
Dim action As Func(Of IScheduler, TState, IDisposable)
Dim returnValue As IDisposable

returnValue = instance.ScheduleAbsolute(state, _
    dueTime, action)
```

```csharp
public abstract IDisposable ScheduleAbsolute<TState>(
    TState state,
    TAbsolute dueTime,
    Func<IScheduler, TState, IDisposable> action
)
```

```c++
public:
generic<typename TState>
virtual IDisposable^ ScheduleAbsolute(
    TState state, 
    TAbsolute dueTime, 
    Func<IScheduler^, TState, IDisposable^>^ action
) abstract
```

```fsharp
abstract ScheduleAbsolute : 
        state:'TState * 
        dueTime:'TAbsolute * 
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
  Type: [TAbsolute](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md)  
  Absolute time at which to execute the action.

- action  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<[IScheduler](IScheduler\IScheduler.md), TState, [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\>  
  The action to be executed.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
The [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9) object used to cancel the scheduled action (best effort).

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)
