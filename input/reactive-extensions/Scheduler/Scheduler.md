title: Scheduler()s
---
# Scheduler Methods

Include Protected Members  
Include Inherited Members

The [Scheduler](Scheduler/Scheduler) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Normalize](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.normalize(system.timespan)(v=VS.103))Ensures that no time spans are negative.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action)(v=VS.103))Schedules an action to be executed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, Action<Action>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action%7bsystem.action%7d)(v=VS.103))Schedules an action to be executed recursively.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, DateTimeOffset, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, DateTimeOffset, Action<Action<DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action%7bsystem.action%7bsystem.datetimeoffset%7d%7d)(v=VS.103))Schedules an action to be executed after dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, TimeSpan, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action)(v=VS.103))Schedules an action to be executed after dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, TimeSpan, Action<Action<TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action%7bsystem.action%7bsystem.timespan%7d%7d)(v=VS.103))Schedules an action to be executed recursively after each dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule<TState>(IScheduler, TState, Action<TState, Action<TState>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.action%7b%60%600%2csystem.action%7b%60%600%7d%7d)(v=VS.103))Schedules an action to be executed recursively.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule<TState>(IScheduler, TState, DateTimeOffset, Action<TState, Action<TState, DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.datetimeoffset%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.datetimeoffset%7d%7d)(v=VS.103))Schedules an action to be executed recursively at each dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule<TState>(IScheduler, TState, TimeSpan, Action<TState, Action<TState, TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.timespan%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.timespan%7d%7d)(v=VS.103))Schedules an action to be executed recursively after each dueTime.Top

## See Also

#### Reference

[Scheduler Class](Scheduler/Scheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)

# Scheduler Class

Provides a set of static methods for creating Schedulers.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Concurrency.Scheduler

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public NotInheritable Class Scheduler
```

```vb
'Usage
```

```csharp
public static class Scheduler
```

```c++
[ExtensionAttribute]
public ref class Scheduler abstract sealed
```

```fsharp
[<AbstractClassAttribute>]
[<SealedAttribute>]
type Scheduler =  class end
```

```javascript
public final class Scheduler
```

The Scheduler type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[CurrentThread](CurrentThread/Scheduler.CurrentThread)Gets the scheduler that schedules work as soon as possible on the current thread.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Immediate](Immediate/Scheduler.Immediate)Gets the scheduler that schedules work immediately on the current thread.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[NewThread](NewThread/Scheduler.NewThread)Gets the scheduler that schedules work on a new thread.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Now](Now/Scheduler.Now)Represents a notion of time for this scheduler. Tasks being scheduled on a scheduler will adhere to the time denoted by this property.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[TaskPool](TaskPool/Scheduler.TaskPool)Gets the scheduler that schedules work on the default Task Factory.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ThreadPool](ThreadPool/Scheduler.ThreadPool)Gets the scheduler that schedules work on the ThreadPool.Top

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Normalize](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.normalize(system.timespan)(v=VS.103))Ensures that no time spans are negative.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action)(v=VS.103))Schedules an action to be executed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, Action<Action>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action%7bsystem.action%7d)(v=VS.103))Schedules an action to be executed recursively.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, DateTimeOffset, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, DateTimeOffset, Action<Action<DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action%7bsystem.action%7bsystem.datetimeoffset%7d%7d)(v=VS.103))Schedules an action to be executed after dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, TimeSpan, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action)(v=VS.103))Schedules an action to be executed after dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule(IScheduler, TimeSpan, Action<Action<TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action%7bsystem.action%7bsystem.timespan%7d%7d)(v=VS.103))Schedules an action to be executed recursively after each dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule<TState>(IScheduler, TState, Action<TState, Action<TState>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.action%7b%60%600%2csystem.action%7b%60%600%7d%7d)(v=VS.103))Schedules an action to be executed recursively.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule<TState>(IScheduler, TState, DateTimeOffset, Action<TState, Action<TState, DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.datetimeoffset%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.datetimeoffset%7d%7d)(v=VS.103))Schedules an action to be executed recursively at each dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Schedule<TState>(IScheduler, TState, TimeSpan, Action<TState, Action<TState, TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.timespan%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.timespan%7d%7d)(v=VS.103))Schedules an action to be executed recursively after each dueTime.Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)

# Scheduler Properties

Include Protected Members  
Include Inherited Members

The [Scheduler](Scheduler/Scheduler) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[CurrentThread](CurrentThread/Scheduler.CurrentThread)Gets the scheduler that schedules work as soon as possible on the current thread.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Immediate](Immediate/Scheduler.Immediate)Gets the scheduler that schedules work immediately on the current thread.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[NewThread](NewThread/Scheduler.NewThread)Gets the scheduler that schedules work on a new thread.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Now](Now/Scheduler.Now)Represents a notion of time for this scheduler. Tasks being scheduled on a scheduler will adhere to the time denoted by this property.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[TaskPool](TaskPool/Scheduler.TaskPool)Gets the scheduler that schedules work on the default Task Factory.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ThreadPool](ThreadPool/Scheduler.ThreadPool)Gets the scheduler that schedules work on the ThreadPool.Top

## See Also

#### Reference

[Scheduler Class](Scheduler/Scheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)
