title: SynchronizationContextScheduler.Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)
---
# SynchronizationContextScheduler.Schedule\<TState\> Method (TState, TimeSpan, Func\<IScheduler, TState, IDisposable\>)

Schedules an action to be executed after dueTime.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Schedule(Of TState) ( _
    state As TState, _
    dueTime As TimeSpan, _
    action As Func(Of IScheduler, TState, IDisposable) _
) As IDisposable
```

```vb
'Usage
Dim instance As SynchronizationContextScheduler
Dim state As TState
Dim dueTime As TimeSpan
Dim action As Func(Of IScheduler, TState, IDisposable)
Dim returnValue As IDisposable

returnValue = instance.Schedule(state, _
    dueTime, action)
```

```csharp
public IDisposable Schedule<TState>(
    TState state,
    TimeSpan dueTime,
    Func<IScheduler, TState, IDisposable> action
)
```

```c++
public:
generic<typename TState>
virtual IDisposable^ Schedule(
    TState state, 
    TimeSpan dueTime, 
    Func<IScheduler^, TState, IDisposable^>^ action
) sealed
```

```fsharp
abstract Schedule : 
        state:'TState * 
        dueTime:TimeSpan * 
        action:Func<IScheduler, 'TState, IDisposable> -> IDisposable 
override Schedule : 
        state:'TState * 
        dueTime:TimeSpan * 
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
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  Relative time after which to execute the action.

- action  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<[IScheduler](IScheduler\IScheduler.md), TState, [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\>  
  Action to be executed.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
Disposable object used to cancel the scheduled action (best effort).

#### Implements

[IScheduler.Schedule\<TState\>(TState, TimeSpan, Func\<IScheduler, TState, IDisposable\>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.ischeduler.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))

## See Also

#### Reference

[SynchronizationContextScheduler Class](SynchronizationContextScheduler\SynchronizationContextScheduler.md)

[Schedule Overload](Schedule\SynchronizationContextScheduler.Schedule.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)

# SynchronizationContextScheduler.Schedule\<TState\> Method (TState, DateTimeOffset, Func\<IScheduler, TState, IDisposable\>)

Schedules an action to be executed at dueTime.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Schedule(Of TState) ( _
    state As TState, _
    dueTime As DateTimeOffset, _
    action As Func(Of IScheduler, TState, IDisposable) _
) As IDisposable
```

```vb
'Usage
Dim instance As SynchronizationContextScheduler
Dim state As TState
Dim dueTime As DateTimeOffset
Dim action As Func(Of IScheduler, TState, IDisposable)
Dim returnValue As IDisposable

returnValue = instance.Schedule(state, _
    dueTime, action)
```

```csharp
public IDisposable Schedule<TState>(
    TState state,
    DateTimeOffset dueTime,
    Func<IScheduler, TState, IDisposable> action
)
```

```c++
public:
generic<typename TState>
virtual IDisposable^ Schedule(
    TState state, 
    DateTimeOffset dueTime, 
    Func<IScheduler^, TState, IDisposable^>^ action
) sealed
```

```fsharp
abstract Schedule : 
        state:'TState * 
        dueTime:DateTimeOffset * 
        action:Func<IScheduler, 'TState, IDisposable> -> IDisposable 
override Schedule : 
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

#### Implements

[IScheduler.Schedule\<TState\>(TState, DateTimeOffset, Func\<IScheduler, TState, IDisposable\>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.ischeduler.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))

## See Also

#### Reference

[SynchronizationContextScheduler Class](SynchronizationContextScheduler\SynchronizationContextScheduler.md)

[Schedule Overload](Schedule\SynchronizationContextScheduler.Schedule.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)

# SynchronizationContextScheduler.Schedule\<TState\> Method (TState, Func\<IScheduler, TState, IDisposable\>)

Schedules an action to be executed.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Schedule(Of TState) ( _
    state As TState, _
    action As Func(Of IScheduler, TState, IDisposable) _
) As IDisposable
```

```vb
'Usage
Dim instance As SynchronizationContextScheduler
Dim state As TState
Dim action As Func(Of IScheduler, TState, IDisposable)
Dim returnValue As IDisposable

returnValue = instance.Schedule(state, _
    action)
```

```csharp
public IDisposable Schedule<TState>(
    TState state,
    Func<IScheduler, TState, IDisposable> action
)
```

```c++
public:
generic<typename TState>
virtual IDisposable^ Schedule(
    TState state, 
    Func<IScheduler^, TState, IDisposable^>^ action
) sealed
```

```fsharp
abstract Schedule : 
        state:'TState * 
        action:Func<IScheduler, 'TState, IDisposable> -> IDisposable 
override Schedule : 
        state:'TState * 
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
  The state passed to the action to be executed.

- action  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<[IScheduler](IScheduler\IScheduler.md), TState, [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\>  
  The action to be executed.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
The disposable object used to cancel the scheduled action (best effort).

#### Implements

[IScheduler.Schedule\<TState\>(TState, Func\<IScheduler, TState, IDisposable\>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.ischeduler.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))

## See Also

#### Reference

[SynchronizationContextScheduler Class](SynchronizationContextScheduler\SynchronizationContextScheduler.md)

[Schedule Overload](Schedule\SynchronizationContextScheduler.Schedule.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)
