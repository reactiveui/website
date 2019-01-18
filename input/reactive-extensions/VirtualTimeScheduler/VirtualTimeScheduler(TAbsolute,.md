# VirtualTimeScheduler\<TAbsolute, TRelative\> Methods

Include Protected Members  
Include Inherited Members

The [VirtualTimeScheduler\<TAbsolute, TRelative\>](VirtualTimeScheduler\VirtualTimeScheduler(TAbsolute,.md) type exposes the following members.

## Methods

NameDescription![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Add](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.add(%600%2c%601)(v=VS.103))Adds a relative time to an absolute time value. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceBy](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceby(%601)(v=VS.103))Advances the scheduler's clock by the specified relative time, running all work scheduled for that timespan. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceTo](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceto(%600)(v=VS.103))Advances the scheduler's clock to the specified time, running all work till that point. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[GetNext](GetNext\VirtualTimeScheduler(TAbsolute,.md)Gets the next scheduled item to be executed. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.GetNext()](GetNext\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimescheduler%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleRelative<TState>](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedulerelative%60%601(%60%600%2c%601%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Start](Start\VirtualTimeSchedulerBase(TAbsolute,.md)Starts the virtual time scheduler. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Stop](Stop\VirtualTimeSchedulerBase(TAbsolute,.md)Stops the virtual time scheduler. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToDateTimeOffset](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.todatetimeoffset(%600)(v=VS.103))Converts the absolute time value to a DateTimeOffset value. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToRelative](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.torelative(system.timespan)(v=VS.103))Converts the TimeSpan value to a relative time value. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## See Also

#### Reference

[VirtualTimeScheduler\<TAbsolute, TRelative\> Class](VirtualTimeScheduler\VirtualTimeScheduler(TAbsolute,.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)

# VirtualTimeScheduler\<TAbsolute, TRelative\> Properties

Include Protected Members  
Include Inherited Members

The [VirtualTimeScheduler\<TAbsolute, TRelative\>](VirtualTimeScheduler\VirtualTimeScheduler(TAbsolute,.md) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Clock](Clock\VirtualTimeSchedulerBase(TAbsolute,.md)Gets the scheduler's absolute time clock value. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Protected property](https://reactiveui.net/assets/img/Hh211972.protproperty(en-us,VS.103).gif "Protected property")[Comparer](Comparer\VirtualTimeSchedulerBase(TAbsolute,.md)Gets the comparer used to compare absolute time values. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsEnabled](IsEnabled\VirtualTimeSchedulerBase(TAbsolute,.md)Gets whether the scheduler is enabled to run work. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Now](Now\VirtualTimeSchedulerBase(TAbsolute,.md)Gets the scheduler's notion of current time. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)Top

## See Also

#### Reference

[VirtualTimeScheduler\<TAbsolute, TRelative\> Class](VirtualTimeScheduler\VirtualTimeScheduler(TAbsolute,.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)

# VirtualTimeScheduler\<TAbsolute, TRelative\> Constructor

Include Protected Members  
Include Inherited Members

Initializes a new instance of the [VirtualTimeScheduler\<TAbsolute, TRelative\>](VirtualTimeScheduler\VirtualTimeScheduler(TAbsolute,.md) class.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[VirtualTimeScheduler<TAbsolute, TRelative>()](VirtualTimeScheduler\VirtualTimeScheduler(TAbsolute,.md)Creates a new virtual time scheduler with the default value of TAbsolute for the initial clock value.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[VirtualTimeScheduler<TAbsolute, TRelative>(TAbsolute, IComparer<TAbsolute>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimescheduler%602.#ctor(%600%2csystem.collections.generic.icomparer%7b%600%7d)(v=VS.103))Creates a new virtual time scheduler.Top

## See Also

#### Reference

[VirtualTimeScheduler\<TAbsolute, TRelative\> Class](VirtualTimeScheduler\VirtualTimeScheduler(TAbsolute,.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)

# VirtualTimeScheduler\<TAbsolute, TRelative\> Class

Represents the base class for virtual time schedulers using a priority queue for scheduled items.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  [System.Reactive.Concurrency.VirtualTimeSchedulerBase](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md)\<TAbsolute, TRelative\>  
    System.Reactive.Concurrency.VirtualTimeScheduler\<TAbsolute, TRelative\>  
      [Microsoft.Reactive.Testing.TestScheduler](TestScheduler\TestScheduler.md)

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public MustInherit Class VirtualTimeScheduler(Of TAbsolute, TRelative) _
    Inherits VirtualTimeSchedulerBase(Of TAbsolute, TRelative)
```

```vb
'Usage
Dim instance As VirtualTimeScheduler(Of TAbsolute, TRelative)
```

```csharp
public abstract class VirtualTimeScheduler<TAbsolute, TRelative> : VirtualTimeSchedulerBase<TAbsolute, TRelative>
```

```c++
generic<typename TAbsolute, typename TRelative>
public ref class VirtualTimeScheduler abstract : public VirtualTimeSchedulerBase<TAbsolute, TRelative>
```

```fsharp
[<AbstractClassAttribute>]
type VirtualTimeScheduler<'TAbsolute, 'TRelative> =  
    class
        inherit VirtualTimeSchedulerBase<'TAbsolute, 'TRelative>
    end
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TAbsolute  
  The absolute time argument type.

- TRelative  
  The relative time argument type.

The VirtualTimeScheduler\<TAbsolute, TRelative\> type exposes the following members.

## Constructors

NameDescription![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[VirtualTimeScheduler<TAbsolute, TRelative>()](VirtualTimeScheduler\VirtualTimeScheduler(TAbsolute,.md)Creates a new virtual time scheduler with the default value of TAbsolute for the initial clock value.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[VirtualTimeScheduler<TAbsolute, TRelative>(TAbsolute, IComparer<TAbsolute>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimescheduler%602.#ctor(%600%2csystem.collections.generic.icomparer%7b%600%7d)(v=VS.103))Creates a new virtual time scheduler.Top

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Clock](Clock\VirtualTimeSchedulerBase(TAbsolute,.md)Gets the scheduler's absolute time clock value. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Protected property](https://reactiveui.net/assets/img/Hh211972.protproperty(en-us,VS.103).gif "Protected property")[Comparer](Comparer\VirtualTimeSchedulerBase(TAbsolute,.md)Gets the comparer used to compare absolute time values. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsEnabled](IsEnabled\VirtualTimeSchedulerBase(TAbsolute,.md)Gets whether the scheduler is enabled to run work. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Now](Now\VirtualTimeSchedulerBase(TAbsolute,.md)Gets the scheduler's notion of current time. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)Top

## Methods

NameDescription![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Add](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.add(%600%2c%601)(v=VS.103))Adds a relative time to an absolute time value. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceBy](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceby(%601)(v=VS.103))Advances the scheduler's clock by the specified relative time, running all work scheduled for that timespan. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceTo](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceto(%600)(v=VS.103))Advances the scheduler's clock to the specified time, running all work till that point. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[GetNext](GetNext\VirtualTimeScheduler(TAbsolute,.md)Gets the next scheduled item to be executed. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.GetNext()](GetNext\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimescheduler%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleRelative<TState>](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedulerelative%60%601(%60%600%2c%601%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Start](Start\VirtualTimeSchedulerBase(TAbsolute,.md)Starts the virtual time scheduler. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Stop](Stop\VirtualTimeSchedulerBase(TAbsolute,.md)Stops the virtual time scheduler. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToDateTimeOffset](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.todatetimeoffset(%600)(v=VS.103))Converts the absolute time value to a DateTimeOffset value. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToRelative](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.torelative(system.timespan)(v=VS.103))Converts the TimeSpan value to a relative time value. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)

# VirtualTimeScheduler\<TAbsolute, TRelative\> Constructor (TAbsolute, IComparer\<TAbsolute\>)

Creates a new virtual time scheduler.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Protected Sub New ( _
    initialClock As TAbsolute, _
    comparer As IComparer(Of TAbsolute) _
)
```

```vb
'Usage
Dim initialClock As TAbsolute
Dim comparer As IComparer(Of TAbsolute)

Dim instance As New VirtualTimeScheduler(initialClock, _
    comparer)
```

```csharp
protected VirtualTimeScheduler(
    TAbsolute initialClock,
    IComparer<TAbsolute> comparer
)
```

```c++
protected:
VirtualTimeScheduler(
    TAbsolute initialClock, 
    IComparer<TAbsolute>^ comparer
)
```

```fsharp
new : 
        initialClock:'TAbsolute * 
        comparer:IComparer<'TAbsolute> -> VirtualTimeScheduler
```

```jscript
protected function VirtualTimeScheduler(
    initialClock : TAbsolute, 
    comparer : IComparer<TAbsolute>
)
```

#### Parameters

- initialClock  
  Type: [TAbsolute](VirtualTimeScheduler\VirtualTimeScheduler(TAbsolute,.md)  
  The initial value for the clock.

- comparer  
  Type: [System.Collections.Generic.IComparer](https://msdn.microsoft.com/en-us/library/8ehhxeaf)\<[TAbsolute](VirtualTimeScheduler\VirtualTimeScheduler(TAbsolute,.md)\>  
  The comparer to determine causality of events based on absolute time.

## See Also

#### Reference

[VirtualTimeScheduler\<TAbsolute, TRelative\> Class](VirtualTimeScheduler\VirtualTimeScheduler(TAbsolute,.md)

[VirtualTimeScheduler\<TAbsolute, TRelative\> Overload](VirtualTimeScheduler\VirtualTimeScheduler(TAbsolute,.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)

# VirtualTimeScheduler\<TAbsolute, TRelative\> Constructor

Creates a new virtual time scheduler with the default value of TAbsolute for the initial clock value.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Protected Sub New
```

```vb
'Usage

Dim instance As New VirtualTimeScheduler()
```

```csharp
protected VirtualTimeScheduler()
```

```c++
protected:
VirtualTimeScheduler()
```

```fsharp
new : unit -> VirtualTimeScheduler
```

```jscript
protected function VirtualTimeScheduler()
```

## See Also

#### Reference

[VirtualTimeScheduler\<TAbsolute, TRelative\> Class](VirtualTimeScheduler\VirtualTimeScheduler(TAbsolute,.md)

[VirtualTimeScheduler\<TAbsolute, TRelative\> Overload](VirtualTimeScheduler\VirtualTimeScheduler(TAbsolute,.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)
