title: TestScheduler.ScheduleAbsolute<TState>(TState, Int64, Func<IScheduler, TState, IDisposable>)
---
# TestScheduler.ScheduleAbsolute\<TState\> Method (TState, Int64, Func\<IScheduler, TState, IDisposable\>)

Schedules an action to be executed at the specified virtual time.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Overrides Function ScheduleAbsolute(Of TState) ( _
    state As TState, _
    dueTime As Long, _
    action As Func(Of IScheduler, TState, IDisposable) _
) As IDisposable
```

```vb
'Usage
Dim instance As TestScheduler
Dim state As TState
Dim dueTime As Long
Dim action As Func(Of IScheduler, TState, IDisposable)
Dim returnValue As IDisposable

returnValue = instance.ScheduleAbsolute(state, _
    dueTime, action)
```

```csharp
public override IDisposable ScheduleAbsolute<TState>(
    TState state,
    long dueTime,
    Func<IScheduler, TState, IDisposable> action
)
```

```c++
public:
generic<typename TState>
virtual IDisposable^ ScheduleAbsolute(
    TState state, 
    long long dueTime, 
    Func<IScheduler^, TState, IDisposable^>^ action
) override
```

```fsharp
abstract ScheduleAbsolute : 
        state:'TState * 
        dueTime:int64 * 
        action:Func<IScheduler, 'TState, IDisposable> -> IDisposable 
override ScheduleAbsolute : 
        state:'TState * 
        dueTime:int64 * 
        action:Func<IScheduler, 'TState, IDisposable> -> IDisposable 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The type of state.

#### Parameters

- state  
  Type: TState  
  The state passed to the action to be executed.

- dueTime  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  Absolute virtual time at which to execute the action.

- action  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<[IScheduler](IScheduler\IScheduler.md), TState, [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\>  
  The action to be executed.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
The disposable object used to cancel the scheduled action.

## See Also

#### Reference

[TestScheduler Class](TestScheduler\TestScheduler.md)

[ScheduleAbsolute Overload](ScheduleAbsolute\TestScheduler.ScheduleAbsolute.md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)
