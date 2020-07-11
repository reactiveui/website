title: IScheduler Interface
---
# IScheduler Interface

Represents an object that schedules units of work.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Interface IScheduler
```

```vb
'Usage
Dim instance As IScheduler
```

```csharp
public interface IScheduler
```

```c++
public interface class IScheduler
```

```fsharp
type IScheduler =  interface end
```

```jscript
public interface IScheduler
```

The IScheduler type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Now](Now/IScheduler.Now)Gets the scheduler's notion of current time.Top

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.ischeduler.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.ischeduler.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.ischeduler.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime.Top

## Extension Methods

NameDescription![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action<Action>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action%7bsystem.action%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed at dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action<Action<TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action%7bsystem.action%7bsystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action<Action<DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action%7bsystem.action%7bsystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, Action<TState, Action<TState>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.action%7b%60%600%2csystem.action%7b%60%600%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, TimeSpan, Action<TState, Action<TState, TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.timespan%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, DateTimeOffset, Action<TState, Action<TState, DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.datetimeoffset%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively at each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)Top

## See Also

#### Reference

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)








# IScheduler Methods

Include Protected Members  
Include Inherited Members

The [IScheduler](IScheduler/IScheduler) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.ischeduler.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.ischeduler.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.ischeduler.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime.Top

## Extension Methods

NameDescription![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action<Action>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action%7bsystem.action%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed at dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action<Action<TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action%7bsystem.action%7bsystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action<Action<DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action%7bsystem.action%7bsystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, Action<TState, Action<TState>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.action%7b%60%600%2csystem.action%7b%60%600%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, TimeSpan, Action<TState, Action<TState, TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.timespan%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, DateTimeOffset, Action<TState, Action<TState, DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.datetimeoffset%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively at each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)Top

## See Also

#### Reference

[IScheduler Interface](IScheduler/IScheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)






# IScheduler Properties

Include Protected Members  
Include Inherited Members

The [IScheduler](IScheduler/IScheduler) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Now](Now/IScheduler.Now)Gets the scheduler's notion of current time.Top

## See Also

#### Reference

[IScheduler Interface](IScheduler/IScheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)




