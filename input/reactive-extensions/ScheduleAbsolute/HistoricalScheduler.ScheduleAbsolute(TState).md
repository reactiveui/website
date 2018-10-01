# HistoricalScheduler.ScheduleAbsolute\<TState\> Method (TState, DateTimeOffset, Func\<IScheduler, TState, IDisposable\>)

Schedules an action to be executed at dueTime.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Overrides Function ScheduleAbsolute(Of TState) ( _
    state As TState, _
    dueTime As DateTimeOffset, _
    action As Func(Of IScheduler, TState, IDisposable) _
) As IDisposable
```

```vb
'Usage
Dim instance As HistoricalScheduler
Dim state As TState
Dim dueTime As DateTimeOffset
Dim action As Func(Of IScheduler, TState, IDisposable)
Dim returnValue As IDisposable

returnValue = instance.ScheduleAbsolute(state, _
    dueTime, action)
```

```csharp
public override IDisposable ScheduleAbsolute<TState>(
    TState state,
    DateTimeOffset dueTime,
    Func<IScheduler, TState, IDisposable> action
)
```

```c++
public:
generic<typename TState>
virtual IDisposable^ ScheduleAbsolute(
    TState state, 
    DateTimeOffset dueTime, 
    Func<IScheduler^, TState, IDisposable^>^ action
) override
```

```fsharp
abstract ScheduleAbsolute : 
        state:'TState * 
        dueTime:DateTimeOffset * 
        action:Func<IScheduler, 'TState, IDisposable> -> IDisposable 
override ScheduleAbsolute : 
        state:'TState * 
        dueTime:DateTimeOffset * 
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
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
  Absolute time at which to execute the action.

- action  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<[IScheduler](IScheduler\IScheduler.md), TState, [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\>  
  Action to be executed.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
Disposable object used to cancel the scheduled action (best effort).

## See Also

#### Reference

[HistoricalScheduler Class](HistoricalScheduler\HistoricalScheduler.md)

[ScheduleAbsolute Overload](ScheduleAbsolute\HistoricalScheduler.ScheduleAbsolute.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)
