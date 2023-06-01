title: Scheduler.Schedule(IScheduler, Action)
---
# Scheduler.Schedule Method (IScheduler, Action)

Schedules an action to be executed.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Schedule ( _
    scheduler As IScheduler, _
    action As Action _
) As IDisposable
```

```vb
'Usage
Dim scheduler As IScheduler
Dim action As Action
Dim returnValue As IDisposable

returnValue = scheduler.Schedule(action)
```

```csharp
public static IDisposable Schedule(
    this IScheduler scheduler,
    Action action
)
```

```c++
[ExtensionAttribute]
public:
static IDisposable^ Schedule(
    IScheduler^ scheduler, 
    Action^ action
)
```

```fsharp
static member Schedule : 
        scheduler:IScheduler * 
        action:Action -> IDisposable 
```

```javascript
public static function Schedule(
    scheduler : IScheduler, 
    action : Action
) : IDisposable
```

#### Parameters

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to execute the action on.

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb534741)  
  The action to execute.

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

# Scheduler.Schedule Method (IScheduler, DateTimeOffset, Action\<Action\<DateTimeOffset\>\>)

Schedules an action to be executed after dueTime.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Schedule ( _
    scheduler As IScheduler, _
    dueTime As DateTimeOffset, _
    action As Action(Of Action(Of DateTimeOffset)) _
) As IDisposable
```

```vb
'Usage
Dim scheduler As IScheduler
Dim dueTime As DateTimeOffset
Dim action As Action(Of Action(Of DateTimeOffset))
Dim returnValue As IDisposable

returnValue = scheduler.Schedule(dueTime, _
    action)
```

```csharp
public static IDisposable Schedule(
    this IScheduler scheduler,
    DateTimeOffset dueTime,
    Action<Action<DateTimeOffset>> action
)
```

```c++
[ExtensionAttribute]
public:
static IDisposable^ Schedule(
    IScheduler^ scheduler, 
    DateTimeOffset dueTime, 
    Action<Action<DateTimeOffset>^>^ action
)
```

```fsharp
static member Schedule : 
        scheduler:IScheduler * 
        dueTime:DateTimeOffset * 
        action:Action<Action<DateTimeOffset>> -> IDisposable 
```

```javascript
public static function Schedule(
    scheduler : IScheduler, 
    dueTime : DateTimeOffset, 
    action : Action<Action<DateTimeOffset>>
) : IDisposable
```

#### Parameters

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to execute the action on.

- dueTime  
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
  The relative time after which to execute the action.

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)\>\>  
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

# Scheduler.Schedule Method (IScheduler, DateTimeOffset, Action)

Schedules an action to be executed at dueTime.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Schedule ( _
    scheduler As IScheduler, _
    dueTime As DateTimeOffset, _
    action As Action _
) As IDisposable
```

```vb
'Usage
Dim scheduler As IScheduler
Dim dueTime As DateTimeOffset
Dim action As Action
Dim returnValue As IDisposable

returnValue = scheduler.Schedule(dueTime, _
    action)
```

```csharp
public static IDisposable Schedule(
    this IScheduler scheduler,
    DateTimeOffset dueTime,
    Action action
)
```

```c++
[ExtensionAttribute]
public:
static IDisposable^ Schedule(
    IScheduler^ scheduler, 
    DateTimeOffset dueTime, 
    Action^ action
)
```

```fsharp
static member Schedule : 
        scheduler:IScheduler * 
        dueTime:DateTimeOffset * 
        action:Action -> IDisposable 
```

```javascript
public static function Schedule(
    scheduler : IScheduler, 
    dueTime : DateTimeOffset, 
    action : Action
) : IDisposable
```

#### Parameters

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to execute the action on.

- dueTime  
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
  The absolute time at which to execute the action.

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb534741)  
  The action to execute.

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

# Scheduler.Schedule Method (IScheduler, TimeSpan, Action)

Schedules an action to be executed after dueTime.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Schedule ( _
    scheduler As IScheduler, _
    dueTime As TimeSpan, _
    action As Action _
) As IDisposable
```

```vb
'Usage
Dim scheduler As IScheduler
Dim dueTime As TimeSpan
Dim action As Action
Dim returnValue As IDisposable

returnValue = scheduler.Schedule(dueTime, _
    action)
```

```csharp
public static IDisposable Schedule(
    this IScheduler scheduler,
    TimeSpan dueTime,
    Action action
)
```

```c++
[ExtensionAttribute]
public:
static IDisposable^ Schedule(
    IScheduler^ scheduler, 
    TimeSpan dueTime, 
    Action^ action
)
```

```fsharp
static member Schedule : 
        scheduler:IScheduler * 
        dueTime:TimeSpan * 
        action:Action -> IDisposable 
```

```javascript
public static function Schedule(
    scheduler : IScheduler, 
    dueTime : TimeSpan, 
    action : Action
) : IDisposable
```

#### Parameters

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to execute the action on.

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The relative time after which to execute the action.

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb534741)  
  The action to execute.

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

# Scheduler.Schedule Method

Include Protected Members  
Include Inherited Members

Schedules an action to be executed.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action)(v=VS.103))Schedules an action to be executed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, Action<Action>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action%7bsystem.action%7d)(v=VS.103))Schedules an action to be executed recursively.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, DateTimeOffset, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, DateTimeOffset, Action<Action<DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action%7bsystem.action%7bsystem.datetimeoffset%7d%7d)(v=VS.103))Schedules an action to be executed after dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, TimeSpan, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action)(v=VS.103))Schedules an action to be executed after dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, TimeSpan, Action<Action<TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action%7bsystem.action%7bsystem.timespan%7d%7d)(v=VS.103))Schedules an action to be executed recursively after each dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule<TState>(IScheduler, TState, Action<TState, Action<TState>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.action%7b%60%600%2csystem.action%7b%60%600%7d%7d)(v=VS.103))Schedules an action to be executed recursively.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule<TState>(IScheduler, TState, DateTimeOffset, Action<TState, Action<TState, DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.datetimeoffset%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.datetimeoffset%7d%7d)(v=VS.103))Schedules an action to be executed recursively at each dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule<TState>(IScheduler, TState, TimeSpan, Action<TState, Action<TState, TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.timespan%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.timespan%7d%7d)(v=VS.103))Schedules an action to be executed recursively after each dueTime.Top

## See Also

#### Reference

[Scheduler Class](Scheduler/Scheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)

# Scheduler.Schedule Method (IScheduler, Action\<Action\>)

Schedules an action to be executed recursively.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Schedule ( _
    scheduler As IScheduler, _
    action As Action(Of Action) _
) As IDisposable
```

```vb
'Usage
Dim scheduler As IScheduler
Dim action As Action(Of Action)
Dim returnValue As IDisposable

returnValue = scheduler.Schedule(action)
```

```csharp
public static IDisposable Schedule(
    this IScheduler scheduler,
    Action<Action> action
)
```

```c++
[ExtensionAttribute]
public:
static IDisposable^ Schedule(
    IScheduler^ scheduler, 
    Action<Action^>^ action
)
```

```fsharp
static member Schedule : 
        scheduler:IScheduler * 
        action:Action<Action> -> IDisposable 
```

```javascript
public static function Schedule(
    scheduler : IScheduler, 
    action : Action<Action>
) : IDisposable
```

#### Parameters

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to execute the recursive action on.

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Action](https://msdn.microsoft.com/en-us/library/Bb534741)\>  
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

# Scheduler.Schedule Method (IScheduler, TimeSpan, Action\<Action\<TimeSpan\>\>)

Schedules an action to be executed recursively after each dueTime.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Schedule ( _
    scheduler As IScheduler, _
    dueTime As TimeSpan, _
    action As Action(Of Action(Of TimeSpan)) _
) As IDisposable
```

```vb
'Usage
Dim scheduler As IScheduler
Dim dueTime As TimeSpan
Dim action As Action(Of Action(Of TimeSpan))
Dim returnValue As IDisposable

returnValue = scheduler.Schedule(dueTime, _
    action)
```

```csharp
public static IDisposable Schedule(
    this IScheduler scheduler,
    TimeSpan dueTime,
    Action<Action<TimeSpan>> action
)
```

```c++
[ExtensionAttribute]
public:
static IDisposable^ Schedule(
    IScheduler^ scheduler, 
    TimeSpan dueTime, 
    Action<Action<TimeSpan>^>^ action
)
```

```fsharp
static member Schedule : 
        scheduler:IScheduler * 
        dueTime:TimeSpan * 
        action:Action<Action<TimeSpan>> -> IDisposable 
```

```javascript
public static function Schedule(
    scheduler : IScheduler, 
    dueTime : TimeSpan, 
    action : Action<Action<TimeSpan>>
) : IDisposable
```

#### Parameters

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to execute the action on.

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The relative time after which to execute the action.

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)\>\>  
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
