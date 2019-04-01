# HistoricalScheduler Methods

Include Protected Members  
Include Inherited Members

The [HistoricalScheduler](HistoricalScheduler\HistoricalScheduler.md) type exposes the following members.

## Methods

NameDescription![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Add](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.historicalschedulerbase.add(system.datetimeoffset%2csystem.timespan)(v=VS.103))Adds a relative time to an absolute time value. (Inherited from [HistoricalSchedulerBase](HistoricalSchedulerBase\HistoricalSchedulerBase.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceBy](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceby(%601)(v=VS.103))Advances the scheduler's clock by the specified relative time, running all work scheduled for that timespan. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceTo](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceto(%600)(v=VS.103))Advances the scheduler's clock to the specified time, running all work till that point. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[GetNext](GetNext\HistoricalScheduler.GetNext.md)Gets the next scheduled item to be executed. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.GetNext()](GetNext\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.historicalscheduler.scheduleabsolute%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleRelative<TState>](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedulerelative%60%601(%60%600%2c%601%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Start](Start\VirtualTimeSchedulerBase(TAbsolute,.md)Starts the virtual time scheduler. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Stop](Stop\VirtualTimeSchedulerBase(TAbsolute,.md)Stops the virtual time scheduler. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToDateTimeOffset](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.historicalschedulerbase.todatetimeoffset(system.datetimeoffset)(v=VS.103))Converts the absolute time value to a DateTimeOffset value. (Inherited from [HistoricalSchedulerBase](HistoricalSchedulerBase\HistoricalSchedulerBase.md).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToRelative](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.historicalschedulerbase.torelative(system.timespan)(v=VS.103))Converts the TimeSpan value to a relative time value. (Inherited from [HistoricalSchedulerBase](HistoricalSchedulerBase\HistoricalSchedulerBase.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## See Also

#### Reference

[HistoricalScheduler Class](HistoricalScheduler\HistoricalScheduler.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)





# HistoricalScheduler Properties

Include Protected Members  
Include Inherited Members

The [HistoricalScheduler](HistoricalScheduler\HistoricalScheduler.md) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Clock](Clock\VirtualTimeSchedulerBase(TAbsolute,.md)Gets the scheduler's absolute time clock value. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Protected property](https://reactiveui.net/assets/img/Hh211972.protproperty(en-us,VS.103).gif "Protected property")[Comparer](Comparer\VirtualTimeSchedulerBase(TAbsolute,.md)Gets the comparer used to compare absolute time values. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsEnabled](IsEnabled\VirtualTimeSchedulerBase(TAbsolute,.md)Gets whether the scheduler is enabled to run work. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Now](Now\VirtualTimeSchedulerBase(TAbsolute,.md)Gets the scheduler's notion of current time. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)Top

## See Also

#### Reference

[HistoricalScheduler Class](HistoricalScheduler\HistoricalScheduler.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)





# HistoricalScheduler Class

Provides a virtual time scheduler that uses DateTimeOffset for absolute time and TimeSpan for relative time.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  [System.Reactive.Concurrency.VirtualTimeSchedulerBase](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md)\<[DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783), [TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)\>  
    [System.Reactive.Concurrency.HistoricalSchedulerBase](HistoricalSchedulerBase\HistoricalSchedulerBase.md)  
      System.Reactive.Concurrency.HistoricalScheduler

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Class HistoricalScheduler _
    Inherits HistoricalSchedulerBase
```

```vb
'Usage
Dim instance As HistoricalScheduler
```

```csharp
public class HistoricalScheduler : HistoricalSchedulerBase
```

```c++
public ref class HistoricalScheduler : public HistoricalSchedulerBase
```

```fsharp
type HistoricalScheduler =  
    class
        inherit HistoricalSchedulerBase
    end
```

```jscript
public class HistoricalScheduler extends HistoricalSchedulerBase
```

The HistoricalScheduler type exposes the following members.

## Constructors

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[HistoricalScheduler](HistoricalScheduler\HistoricalScheduler.md)Initializes a new instance of the HistoricalScheduler class.Top

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Clock](Clock\VirtualTimeSchedulerBase(TAbsolute,.md)Gets the scheduler's absolute time clock value. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Protected property](https://reactiveui.net/assets/img/Hh211972.protproperty(en-us,VS.103).gif "Protected property")[Comparer](Comparer\VirtualTimeSchedulerBase(TAbsolute,.md)Gets the comparer used to compare absolute time values. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsEnabled](IsEnabled\VirtualTimeSchedulerBase(TAbsolute,.md)Gets whether the scheduler is enabled to run work. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Now](Now\VirtualTimeSchedulerBase(TAbsolute,.md)Gets the scheduler's notion of current time. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)Top

## Methods

NameDescription![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Add](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.historicalschedulerbase.add(system.datetimeoffset%2csystem.timespan)(v=VS.103))Adds a relative time to an absolute time value. (Inherited from [HistoricalSchedulerBase](HistoricalSchedulerBase\HistoricalSchedulerBase.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceBy](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceby(%601)(v=VS.103))Advances the scheduler's clock by the specified relative time, running all work scheduled for that timespan. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceTo](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceto(%600)(v=VS.103))Advances the scheduler's clock to the specified time, running all work till that point. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[GetNext](GetNext\HistoricalScheduler.GetNext.md)Gets the next scheduled item to be executed. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.GetNext()](GetNext\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.historicalscheduler.scheduleabsolute%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleRelative<TState>](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedulerelative%60%601(%60%600%2c%601%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Start](Start\VirtualTimeSchedulerBase(TAbsolute,.md)Starts the virtual time scheduler. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Stop](Stop\VirtualTimeSchedulerBase(TAbsolute,.md)Stops the virtual time scheduler. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToDateTimeOffset](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.historicalschedulerbase.todatetimeoffset(system.datetimeoffset)(v=VS.103))Converts the absolute time value to a DateTimeOffset value. (Inherited from [HistoricalSchedulerBase](HistoricalSchedulerBase\HistoricalSchedulerBase.md).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToRelative](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.historicalschedulerbase.torelative(system.timespan)(v=VS.103))Converts the TimeSpan value to a relative time value. (Inherited from [HistoricalSchedulerBase](HistoricalSchedulerBase\HistoricalSchedulerBase.md).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)










# HistoricalScheduler Constructor

Initializes a new instance of the [HistoricalScheduler](HistoricalScheduler\HistoricalScheduler.md) class.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New
```

```vb
'Usage

Dim instance As New HistoricalScheduler()
```

```csharp
public HistoricalScheduler()
```

```c++
public:
HistoricalScheduler()
```

```fsharp
new : unit -> HistoricalScheduler
```

```jscript
public function HistoricalScheduler()
```

## See Also

#### Reference

[HistoricalScheduler Class](HistoricalScheduler\HistoricalScheduler.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)




