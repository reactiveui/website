title: ControlScheduler.Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)
---
# ControlScheduler.Schedule\<TState\> Method (TState, DateTimeOffset, Func\<IScheduler, TState, IDisposable\>)

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive.Windows.Forms (in System.Reactive.Windows.Forms.dll)

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
Dim instance As ControlScheduler
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

#### Parameters

- state  
  Type: TState

- dueTime  
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)

- action  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<[IScheduler](IScheduler/IScheduler), TState, [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\>

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)

#### Implements

[IScheduler.Schedule\<TState\>(TState, DateTimeOffset, Func\<IScheduler, TState, IDisposable\>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.ischeduler.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))

## See Also

#### Reference

[ControlScheduler Class](ControlScheduler/ControlScheduler)

[Schedule Overload](Schedule/ControlScheduler.Schedule)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)

# ControlScheduler.Schedule\<TState\> Method (TState, Func\<IScheduler, TState, IDisposable\>)

Schedules an action to be executed on the message loop associated with the control.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive.Windows.Forms (in System.Reactive.Windows.Forms.dll)

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
Dim instance As ControlScheduler
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
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<[IScheduler](IScheduler/IScheduler), TState, [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\>  
  The action to be executed.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
The disposable object used to cancel the scheduled action (best effort).

#### Implements

[IScheduler.Schedule\<TState\>(TState, Func\<IScheduler, TState, IDisposable\>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.ischeduler.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))

## See Also

#### Reference

[ControlScheduler Class](ControlScheduler/ControlScheduler)

[Schedule Overload](Schedule/ControlScheduler.Schedule)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)

# ControlScheduler.Schedule\<TState\> Method (TState, TimeSpan, Func\<IScheduler, TState, IDisposable\>)

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive.Windows.Forms (in System.Reactive.Windows.Forms.dll)

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
Dim instance As ControlScheduler
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

#### Parameters

- state  
  Type: TState

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)

- action  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<[IScheduler](IScheduler/IScheduler), TState, [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\>

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)

#### Implements

[IScheduler.Schedule\<TState\>(TState, TimeSpan, Func\<IScheduler, TState, IDisposable\>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.ischeduler.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))

## See Also

#### Reference

[ControlScheduler Class](ControlScheduler/ControlScheduler)

[Schedule Overload](Schedule/ControlScheduler.Schedule)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)
