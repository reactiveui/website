title: Scheduler.Schedule<TState>(IScheduler, TState, Action<TState, Action<TState>>)
---
# Scheduler.Schedule\<TState\> Method (IScheduler, TState, Action\<TState, Action\<TState\>\>)

Schedules an action to be executed recursively.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Schedule(Of TState) ( _
    scheduler As IScheduler, _
    state As TState, _
    action As Action(Of TState, Action(Of TState)) _
) As IDisposable
```

```vb
'Usage
Dim scheduler As IScheduler
Dim state As TState
Dim action As Action(Of TState, Action(Of TState))
Dim returnValue As IDisposable

returnValue = scheduler.Schedule(state, _
    action)
```

```csharp
public static IDisposable Schedule<TState>(
    this IScheduler scheduler,
    TState state,
    Action<TState, Action<TState>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TState>
static IDisposable^ Schedule(
    IScheduler^ scheduler, 
    TState state, 
    Action<TState, Action<TState>^>^ action
)
```

```fsharp
static member Schedule : 
        scheduler:IScheduler * 
        state:'TState * 
        action:Action<'TState, Action<'TState>> -> IDisposable 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The state argument type.

#### Parameters

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to execute the recursive action on.

- state  
  Type: TState  
  The state passed to the action to be executed.

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb549311)\<TState, [Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TState\>\>  
  The action to execute recursively.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
The disposable object used to cancel the scheduled action (best effort).

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IScheduler](IScheduler/IScheduler). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Scheduler Class](Scheduler/Scheduler)

[Schedule Overload](Schedule/Scheduler.Schedule)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)

# Scheduler.Schedule\<TState\> Method (IScheduler, TState, TimeSpan, Action\<TState, Action\<TState, TimeSpan\>\>)

Schedules an action to be executed recursively after each dueTime.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Schedule(Of TState) ( _
    scheduler As IScheduler, _
    state As TState, _
    dueTime As TimeSpan, _
    action As Action(Of TState, Action(Of TState, TimeSpan)) _
) As IDisposable
```

```vb
'Usage
Dim scheduler As IScheduler
Dim state As TState
Dim dueTime As TimeSpan
Dim action As Action(Of TState, Action(Of TState, TimeSpan))
Dim returnValue As IDisposable

returnValue = scheduler.Schedule(state, _
    dueTime, action)
```

```csharp
public static IDisposable Schedule<TState>(
    this IScheduler scheduler,
    TState state,
    TimeSpan dueTime,
    Action<TState, Action<TState, TimeSpan>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TState>
static IDisposable^ Schedule(
    IScheduler^ scheduler, 
    TState state, 
    TimeSpan dueTime, 
    Action<TState, Action<TState, TimeSpan>^>^ action
)
```

```fsharp
static member Schedule : 
        scheduler:IScheduler * 
        state:'TState * 
        dueTime:TimeSpan * 
        action:Action<'TState, Action<'TState, TimeSpan>> -> IDisposable 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The state argument type.

#### Parameters

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to execute the recursive action on.

- state  
  Type: TState  
  The state passed to the action to be executed.

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The relative time after which to execute the action for the first time.

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb549311)\<TState, [Action](https://msdn.microsoft.com/en-us/library/Bb549311)\<TState, [TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)\>\>  
  The action to execute recursively.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
The disposable object used to cancel the scheduled action (best effort).

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IScheduler](IScheduler/IScheduler). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Scheduler Class](Scheduler/Scheduler)

[Schedule Overload](Schedule/Scheduler.Schedule)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)

# Scheduler.Schedule\<TState\> Method (IScheduler, TState, DateTimeOffset, Action\<TState, Action\<TState, DateTimeOffset\>\>)

Schedules an action to be executed recursively at each dueTime.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Schedule(Of TState) ( _
    scheduler As IScheduler, _
    state As TState, _
    dueTime As DateTimeOffset, _
    action As Action(Of TState, Action(Of TState, DateTimeOffset)) _
) As IDisposable
```

```vb
'Usage
Dim scheduler As IScheduler
Dim state As TState
Dim dueTime As DateTimeOffset
Dim action As Action(Of TState, Action(Of TState, DateTimeOffset))
Dim returnValue As IDisposable

returnValue = scheduler.Schedule(state, _
    dueTime, action)
```

```csharp
public static IDisposable Schedule<TState>(
    this IScheduler scheduler,
    TState state,
    DateTimeOffset dueTime,
    Action<TState, Action<TState, DateTimeOffset>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TState>
static IDisposable^ Schedule(
    IScheduler^ scheduler, 
    TState state, 
    DateTimeOffset dueTime, 
    Action<TState, Action<TState, DateTimeOffset>^>^ action
)
```

```fsharp
static member Schedule : 
        scheduler:IScheduler * 
        state:'TState * 
        dueTime:DateTimeOffset * 
        action:Action<'TState, Action<'TState, DateTimeOffset>> -> IDisposable 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The state argument type.

#### Parameters

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to execute the recursive action on.

- state  
  Type: TState  
  The state passed to the action to be executed.

- dueTime  
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
  The absolute time at which to execute the action for the first time.

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb549311)\<TState, [Action](https://msdn.microsoft.com/en-us/library/Bb549311)\<TState, [DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)\>\>  
  The action to execute recursively.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
The disposable object used to cancel the scheduled action (best effort).

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IScheduler](IScheduler/IScheduler). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Scheduler Class](Scheduler/Scheduler)

[Schedule Overload](Schedule/Scheduler.Schedule)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)
