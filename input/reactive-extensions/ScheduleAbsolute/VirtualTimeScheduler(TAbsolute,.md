title: VirtualTimeScheduler<TAbsolute, TRelative>.ScheduleAbsolute()
---
# VirtualTimeScheduler\<TAbsolute, TRelative\>.ScheduleAbsolute Method

Include Protected Members  
Include Inherited Members

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimescheduler%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)Top

## See Also

#### Reference

[VirtualTimeScheduler\<TAbsolute, TRelative\> Class](VirtualTimeScheduler/VirtualTimeScheduler(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)

# VirtualTimeScheduler\<TAbsolute, TRelative\>.ScheduleAbsolute\<TState\> Method (TState, TAbsolute, Func\<IScheduler, TState, IDisposable\>)

Schedules an action to be executed at dueTime.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Overrides Function ScheduleAbsolute(Of TState) ( _
    state As TState, _
    dueTime As TAbsolute, _
    action As Func(Of IScheduler, TState, IDisposable) _
) As IDisposable
```

```vb
'Usage
Dim instance As VirtualTimeScheduler
Dim state As TState
Dim dueTime As TAbsolute
Dim action As Func(Of IScheduler, TState, IDisposable)
Dim returnValue As IDisposable

returnValue = instance.ScheduleAbsolute(state, _
    dueTime, action)
```

```csharp
public override IDisposable ScheduleAbsolute<TState>(
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
) override
```

```fsharp
abstract ScheduleAbsolute : 
        state:'TState * 
        dueTime:'TAbsolute * 
        action:Func<IScheduler, 'TState, IDisposable> -> IDisposable 
override ScheduleAbsolute : 
        state:'TState * 
        dueTime:'TAbsolute * 
        action:Func<IScheduler, 'TState, IDisposable> -> IDisposable 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The state argument type.

#### Parameters

- state  
  Type: TState  
  The state passed to the action to be executed.

- dueTime  
  Type: [TAbsolute](VirtualTimeScheduler/VirtualTimeScheduler(TAbsolute,)  
  The absolute time at which to execute the action.

- action  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<[IScheduler](IScheduler/IScheduler), TState, [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\>  
  The action to be executed.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
The disposable object used to cancel the scheduled action (best effort).

## See Also

#### Reference

[VirtualTimeScheduler\<TAbsolute, TRelative\> Class](VirtualTimeScheduler/VirtualTimeScheduler(TAbsolute,)

[ScheduleAbsolute Overload](ScheduleAbsolute/VirtualTimeScheduler(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)
