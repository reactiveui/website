title: EventLoopScheduler Constructor
---
# EventLoopScheduler Constructor

Include Protected Members  
Include Inherited Members

Initializes a new instance of the [EventLoopScheduler](EventLoopScheduler/EventLoopScheduler) class.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[EventLoopScheduler()](EventLoopScheduler/EventLoopScheduler)Creates an object that schedules units of work on a designated thread.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[EventLoopScheduler(Func<ThreadStart, Thread>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.eventloopscheduler.#ctor(system.func%7bsystem.threading.threadstart%2csystem.threading.thread%7d)(v=VS.103))Creates an object that schedules units of work on a designated thread.Top

## See Also

#### Reference

[EventLoopScheduler Class](EventLoopScheduler/EventLoopScheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)





# EventLoopScheduler Methods

Include Protected Members  
Include Inherited Members

The [EventLoopScheduler](EventLoopScheduler/EventLoopScheduler) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose/EventLoopScheduler.Dispose)Ends the thread associated with this scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.eventloopscheduler.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.eventloopscheduler.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.eventloopscheduler.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Extension Methods

NameDescription![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action<Action>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action%7bsystem.action%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed at dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action<Action<TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action%7bsystem.action%7bsystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action<Action<DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action%7bsystem.action%7bsystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, Action<TState, Action<TState>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.action%7b%60%600%2csystem.action%7b%60%600%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, TimeSpan, Action<TState, Action<TState, TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.timespan%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, DateTimeOffset, Action<TState, Action<TState, DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.datetimeoffset%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively at each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)Top

## See Also

#### Reference

[EventLoopScheduler Class](EventLoopScheduler/EventLoopScheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)






# EventLoopScheduler Properties

Include Protected Members  
Include Inherited Members

The [EventLoopScheduler](EventLoopScheduler/EventLoopScheduler) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Now](Now/EventLoopScheduler.Now)Gets the scheduler's notion of current time.Top

## See Also

#### Reference

[EventLoopScheduler Class](EventLoopScheduler/EventLoopScheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)





# EventLoopScheduler Class

Represents an object that schedules units of work on a designated thread.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Concurrency.EventLoopScheduler

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public NotInheritable Class EventLoopScheduler _
    Implements IScheduler, IDisposable
```

```vb
'Usage
Dim instance As EventLoopScheduler
```

```csharp
public sealed class EventLoopScheduler : IScheduler, 
    IDisposable
```

```c++
public ref class EventLoopScheduler sealed : IScheduler, 
    IDisposable
```

```fsharp
[<SealedAttribute>]
type EventLoopScheduler =  
    class
        interface IScheduler
        interface IDisposable
    end
```

```javascript
public final class EventLoopScheduler implements IScheduler, IDisposable
```

The EventLoopScheduler type exposes the following members.

## Constructors

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[EventLoopScheduler()](EventLoopScheduler/EventLoopScheduler)Creates an object that schedules units of work on a designated thread.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[EventLoopScheduler(Func<ThreadStart, Thread>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.eventloopscheduler.#ctor(system.func%7bsystem.threading.threadstart%2csystem.threading.thread%7d)(v=VS.103))Creates an object that schedules units of work on a designated thread.Top

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Now](Now/EventLoopScheduler.Now)Gets the scheduler's notion of current time.Top

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose/EventLoopScheduler.Dispose)Ends the thread associated with this scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.eventloopscheduler.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.eventloopscheduler.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.eventloopscheduler.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Extension Methods

NameDescription![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action<Action>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action%7bsystem.action%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed at dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action<Action<TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action%7bsystem.action%7bsystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action<Action<DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action%7bsystem.action%7bsystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, Action<TState, Action<TState>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.action%7b%60%600%2csystem.action%7b%60%600%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, TimeSpan, Action<TState, Action<TState, TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.timespan%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, DateTimeOffset, Action<TState, Action<TState, DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.datetimeoffset%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively at each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)











# EventLoopScheduler Constructor

Creates an object that schedules units of work on a designated thread.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New
```

```vb
'Usage

Dim instance As New EventLoopScheduler()
```

```csharp
public EventLoopScheduler()
```

```c++
public:
EventLoopScheduler()
```

```fsharp
new : unit -> EventLoopScheduler
```

```javascript
public function EventLoopScheduler()
```

## See Also

#### Reference

[EventLoopScheduler Class](EventLoopScheduler/EventLoopScheduler)

[EventLoopScheduler Overload](EventLoopScheduler/EventLoopScheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)





# EventLoopScheduler Constructor (Func\<ThreadStart, Thread\>)

Creates an object that schedules units of work on a designated thread.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New ( _
    threadFactory As Func(Of ThreadStart, Thread) _
)
```

```vb
'Usage
Dim threadFactory As Func(Of ThreadStart, Thread)

Dim instance As New EventLoopScheduler(threadFactory)
```

```csharp
public EventLoopScheduler(
    Func<ThreadStart, Thread> threadFactory
)
```

```c++
public:
EventLoopScheduler(
    Func<ThreadStart^, Thread^>^ threadFactory
)
```

```fsharp
new : 
        threadFactory:Func<ThreadStart, Thread> -> EventLoopScheduler
```

```javascript
public function EventLoopScheduler(
    threadFactory : Func<ThreadStart, Thread>
)
```

#### Parameters

- threadFactory  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[ThreadStart](https://msdn.microsoft.com/en-us/library/57s77029), [Thread](https://msdn.microsoft.com/en-us/library/bkb1k2x8)\>  
  The factory function for thread creation.

## See Also

#### Reference

[EventLoopScheduler Class](EventLoopScheduler/EventLoopScheduler)

[EventLoopScheduler Overload](EventLoopScheduler/EventLoopScheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)





