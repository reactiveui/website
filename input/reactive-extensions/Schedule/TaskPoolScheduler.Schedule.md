title: TaskPoolScheduler.Schedule()
---
# TaskPoolScheduler.Schedule Method

Include Protected Members  
Include Inherited Members

Schedules an action to be executed.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.taskpoolscheduler.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.taskpoolscheduler.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime, using a System.Threading.Timer object.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.taskpoolscheduler.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime, using a System.Threading.Timer object.Top

## See Also

#### Reference

[TaskPoolScheduler Class](TaskPoolScheduler/TaskPoolScheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)
