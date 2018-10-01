# SynchronizationContextScheduler Methods

Include Protected Members  
Include Inherited Members

The [SynchronizationContextScheduler](SynchronizationContextScheduler\SynchronizationContextScheduler.md) type exposes the following members.

## Methods

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.synchronizationcontextscheduler.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.synchronizationcontextscheduler.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.synchronizationcontextscheduler.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Extension Methods

NameDescription![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action<Action>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action%7bsystem.action%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed at dueTime. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action<Action<TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action%7bsystem.action%7bsystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action<Action<DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action%7bsystem.action%7bsystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, Action<TState, Action<TState>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.action%7b%60%600%2csystem.action%7b%60%600%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, TimeSpan, Action<TState, Action<TState, TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.timespan%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, DateTimeOffset, Action<TState, Action<TState, DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.datetimeoffset%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively at each dueTime. (Defined by [Scheduler](Scheduler\Scheduler.md).)Top

## See Also

#### Reference

[SynchronizationContextScheduler Class](SynchronizationContextScheduler\SynchronizationContextScheduler.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)

# SynchronizationContextScheduler Constructor

Creates an object that schedules units of work on the provided SynchronizationContext.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New ( _
    context As SynchronizationContext _
)
```

```vb
'Usage
Dim context As SynchronizationContext

Dim instance As New SynchronizationContextScheduler(context)
```

```csharp
public SynchronizationContextScheduler(
    SynchronizationContext context
)
```

```c++
public:
SynchronizationContextScheduler(
    SynchronizationContext^ context
)
```

```fsharp
new : 
        context:SynchronizationContext -> SynchronizationContextScheduler
```

```jscript
public function SynchronizationContextScheduler(
    context : SynchronizationContext
)
```

#### Parameters

- context  
  Type: [System.Threading.SynchronizationContext](https://msdn.microsoft.com/en-us/library/wx31754f)  
  The synchronization context to schedule units of work on.

## See Also

#### Reference

[SynchronizationContextScheduler Class](SynchronizationContextScheduler\SynchronizationContextScheduler.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)

# SynchronizationContextScheduler Properties

Include Protected Members  
Include Inherited Members

The [SynchronizationContextScheduler](SynchronizationContextScheduler\SynchronizationContextScheduler.md) type exposes the following members.

## Properties

NameDescription![Public property](images\Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Now](Now\SynchronizationContextScheduler.Now.md)Gets the scheduler's notion of current time.Top

## See Also

#### Reference

[SynchronizationContextScheduler Class](SynchronizationContextScheduler\SynchronizationContextScheduler.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)

# SynchronizationContextScheduler Class

Represents an object that schedules units of work on a provided SynchronizationContext.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Concurrency.SynchronizationContextScheduler

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Class SynchronizationContextScheduler _
    Implements IScheduler
```

```vb
'Usage
Dim instance As SynchronizationContextScheduler
```

```csharp
public class SynchronizationContextScheduler : IScheduler
```

```c++
public ref class SynchronizationContextScheduler : IScheduler
```

```fsharp
type SynchronizationContextScheduler =  
    class
        interface IScheduler
    end
```

```jscript
public class SynchronizationContextScheduler implements IScheduler
```

The SynchronizationContextScheduler type exposes the following members.

## Constructors

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[SynchronizationContextScheduler](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.synchronizationcontextscheduler.#ctor(system.threading.synchronizationcontext)(v=VS.103))Creates an object that schedules units of work on the provided SynchronizationContext.Top

## Properties

NameDescription![Public property](images\Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Now](Now\SynchronizationContextScheduler.Now.md)Gets the scheduler's notion of current time.Top

## Methods

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.synchronizationcontextscheduler.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.synchronizationcontextscheduler.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.synchronizationcontextscheduler.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Extension Methods

NameDescription![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action<Action>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action%7bsystem.action%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed at dueTime. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action<Action<TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action%7bsystem.action%7bsystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action<Action<DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action%7bsystem.action%7bsystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, Action<TState, Action<TState>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.action%7b%60%600%2csystem.action%7b%60%600%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, TimeSpan, Action<TState, Action<TState, TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.timespan%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler\Scheduler.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, DateTimeOffset, Action<TState, Action<TState, DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.datetimeoffset%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively at each dueTime. (Defined by [Scheduler](Scheduler\Scheduler.md).)Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)
