title: VirtualTimeSchedulerBase<TAbsolute, TRelative> Constructor
---
# VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Constructor

Creates a new virtual time scheduler with the default value of TAbsolute for the initial clock value.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Protected Sub New
```

```vb
'Usage

Dim instance As New VirtualTimeSchedulerBase()
```

```csharp
protected VirtualTimeSchedulerBase()
```

```c++
protected:
VirtualTimeSchedulerBase()
```

```fsharp
new : unit -> VirtualTimeSchedulerBase
```

```javascript
protected function VirtualTimeSchedulerBase()
```

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Overload](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)

# VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Constructor (TAbsolute, IComparer\<TAbsolute\>)

Creates a new virtual time scheduler.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
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

Dim instance As New VirtualTimeSchedulerBase(initialClock, _
    comparer)
```

```csharp
protected VirtualTimeSchedulerBase(
    TAbsolute initialClock,
    IComparer<TAbsolute> comparer
)
```

```c++
protected:
VirtualTimeSchedulerBase(
    TAbsolute initialClock, 
    IComparer<TAbsolute>^ comparer
)
```

```fsharp
new : 
        initialClock:'TAbsolute * 
        comparer:IComparer<'TAbsolute> -> VirtualTimeSchedulerBase
```

```javascript
protected function VirtualTimeSchedulerBase(
    initialClock : TAbsolute, 
    comparer : IComparer<TAbsolute>
)
```

#### Parameters

- initialClock  
  Type: [TAbsolute](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)  
  The initial value for the clock.

- comparer  
  Type: [System.Collections.Generic.IComparer](https://msdn.microsoft.com/en-us/library/8ehhxeaf)\<[TAbsolute](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)\>  
  The comparer to determine causality of events based on absolute time.

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Overload](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)

# VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class

Represents the base class for virtual time schedulers.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Concurrency.VirtualTimeSchedulerBase\<TAbsolute, TRelative\>  
    [System.Reactive.Concurrency.HistoricalSchedulerBase](HistoricalSchedulerBase/HistoricalSchedulerBase)  
    [System.Reactive.Concurrency.VirtualTimeScheduler\<TAbsolute, TRelative\>](VirtualTimeScheduler/VirtualTimeScheduler(TAbsolute,)

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public MustInherit Class VirtualTimeSchedulerBase(Of TAbsolute, TRelative) _
    Implements IScheduler
```

```vb
'Usage
Dim instance As VirtualTimeSchedulerBase(Of TAbsolute, TRelative)
```

```csharp
public abstract class VirtualTimeSchedulerBase<TAbsolute, TRelative> : IScheduler
```

```c++
generic<typename TAbsolute, typename TRelative>
public ref class VirtualTimeSchedulerBase abstract : IScheduler
```

```fsharp
[<AbstractClassAttribute>]
type VirtualTimeSchedulerBase<'TAbsolute, 'TRelative> =  
    class
        interface IScheduler
    end
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TAbsolute  
  The absolute time argument type.

- TRelative  
  The relative time argument type.

The VirtualTimeSchedulerBase\<TAbsolute, TRelative\> type exposes the following members.

## Constructors

NameDescription![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[VirtualTimeSchedulerBase<TAbsolute, TRelative>()](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)Creates a new virtual time scheduler with the default value of TAbsolute for the initial clock value.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[VirtualTimeSchedulerBase<TAbsolute, TRelative>(TAbsolute, IComparer<TAbsolute>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.#ctor(%600%2csystem.collections.generic.icomparer%7b%600%7d)(v=VS.103))Creates a new virtual time scheduler.Top

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Clock](Clock/VirtualTimeSchedulerBase(TAbsolute,)Gets the scheduler's absolute time clock value.![Protected property](https://reactiveui.net/assets/img/Hh211972.protproperty(en-us,VS.103).gif "Protected property")[Comparer](Comparer/VirtualTimeSchedulerBase(TAbsolute,)Gets the comparer used to compare absolute time values.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsEnabled](IsEnabled/VirtualTimeSchedulerBase(TAbsolute,)Gets whether the scheduler is enabled to run work.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Now](Now/VirtualTimeSchedulerBase(TAbsolute,)Gets the scheduler's notion of current time.Top

## Methods

NameDescription![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Add](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.add(%600%2c%601)(v=VS.103))Adds a relative time to an absolute time value.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceBy](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceby(%601)(v=VS.103))Advances the scheduler's clock by the specified relative time, running all work scheduled for that timespan.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceTo](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceto(%600)(v=VS.103))Advances the scheduler's clock to the specified time, running all work till that point.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[GetNext](GetNext/VirtualTimeSchedulerBase(TAbsolute,)Gets the next scheduled item to be executed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleRelative<TState>](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedulerelative%60%601(%60%600%2c%601%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Start](Start/VirtualTimeSchedulerBase(TAbsolute,)Starts the virtual time scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Stop](Stop/VirtualTimeSchedulerBase(TAbsolute,)Stops the virtual time scheduler.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToDateTimeOffset](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.todatetimeoffset(%600)(v=VS.103))Converts the absolute time value to a DateTimeOffset value.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToRelative](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.torelative(system.timespan)(v=VS.103))Converts the TimeSpan value to a relative time value.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Extension Methods

NameDescription![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action<Action>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action%7bsystem.action%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed at dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action<Action<TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action%7bsystem.action%7bsystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action<Action<DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action%7bsystem.action%7bsystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, Action<TState, Action<TState>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.action%7b%60%600%2csystem.action%7b%60%600%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, TimeSpan, Action<TState, Action<TState, TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.timespan%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, DateTimeOffset, Action<TState, Action<TState, DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.datetimeoffset%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively at each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)

# VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Properties

Include Protected Members  
Include Inherited Members

The [VirtualTimeSchedulerBase\<TAbsolute, TRelative\>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Clock](Clock/VirtualTimeSchedulerBase(TAbsolute,)Gets the scheduler's absolute time clock value.![Protected property](https://reactiveui.net/assets/img/Hh211972.protproperty(en-us,VS.103).gif "Protected property")[Comparer](Comparer/VirtualTimeSchedulerBase(TAbsolute,)Gets the comparer used to compare absolute time values.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsEnabled](IsEnabled/VirtualTimeSchedulerBase(TAbsolute,)Gets whether the scheduler is enabled to run work.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Now](Now/VirtualTimeSchedulerBase(TAbsolute,)Gets the scheduler's notion of current time.Top

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)

# VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Constructor

Include Protected Members  
Include Inherited Members

Initializes a new instance of the [VirtualTimeSchedulerBase\<TAbsolute, TRelative\>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,) class.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[VirtualTimeSchedulerBase<TAbsolute, TRelative>()](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)Creates a new virtual time scheduler with the default value of TAbsolute for the initial clock value.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[VirtualTimeSchedulerBase<TAbsolute, TRelative>(TAbsolute, IComparer<TAbsolute>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.#ctor(%600%2csystem.collections.generic.icomparer%7b%600%7d)(v=VS.103))Creates a new virtual time scheduler.Top

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)

# VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Methods

Include Protected Members  
Include Inherited Members

The [VirtualTimeSchedulerBase\<TAbsolute, TRelative\>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,) type exposes the following members.

## Methods

NameDescription![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Add](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.add(%600%2c%601)(v=VS.103))Adds a relative time to an absolute time value.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceBy](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceby(%601)(v=VS.103))Advances the scheduler's clock by the specified relative time, running all work scheduled for that timespan.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceTo](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceto(%600)(v=VS.103))Advances the scheduler's clock to the specified time, running all work till that point.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[GetNext](GetNext/VirtualTimeSchedulerBase(TAbsolute,)Gets the next scheduled item to be executed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleRelative<TState>](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedulerelative%60%601(%60%600%2c%601%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Start](Start/VirtualTimeSchedulerBase(TAbsolute,)Starts the virtual time scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Stop](Stop/VirtualTimeSchedulerBase(TAbsolute,)Stops the virtual time scheduler.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToDateTimeOffset](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.todatetimeoffset(%600)(v=VS.103))Converts the absolute time value to a DateTimeOffset value.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToRelative](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.torelative(system.timespan)(v=VS.103))Converts the TimeSpan value to a relative time value.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Extension Methods

NameDescription![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(Action<Action>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.action%7bsystem.action%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action)(v=VS.103))Overloaded. Schedules an action to be executed at dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(TimeSpan, Action<Action<TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.timespan%2csystem.action%7bsystem.action%7bsystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule(DateTimeOffset, Action<Action<DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule(system.reactive.concurrency.ischeduler%2csystem.datetimeoffset%2csystem.action%7bsystem.action%7bsystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed after dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, Action<TState, Action<TState>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.action%7b%60%600%2csystem.action%7b%60%600%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, TimeSpan, Action<TState, Action<TState, TimeSpan>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.timespan%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.timespan%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively after each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule<TState>(TState, DateTimeOffset, Action<TState, Action<TState, DateTimeOffset>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.scheduler.schedule%60%601(system.reactive.concurrency.ischeduler%2c%60%600%2csystem.datetimeoffset%2csystem.action%7b%60%600%2csystem.action%7b%60%600%2csystem.datetimeoffset%7d%7d)(v=VS.103))Overloaded. Schedules an action to be executed recursively at each dueTime. (Defined by [Scheduler](Scheduler/Scheduler).)Top

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)
