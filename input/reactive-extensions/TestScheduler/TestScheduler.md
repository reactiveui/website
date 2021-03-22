title: TestScheduler Constructor
---
# TestScheduler Constructor

Initializes a new instance of the [TestScheduler](TestScheduler/TestScheduler) class.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Sub New
```

```vb
'Usage

Dim instance As New TestScheduler()
```

```csharp
public TestScheduler()
```

```c++
public:
TestScheduler()
```

```fsharp
new : unit -> TestScheduler
```

```javascript
public function TestScheduler()
```

## See Also

#### Reference

[TestScheduler Class](TestScheduler/TestScheduler)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

# TestScheduler Methods

Include Protected Members  
Include Inherited Members

The [TestScheduler](TestScheduler/TestScheduler) type exposes the following members.

## Methods

NameDescription![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Add](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.add(system.int64%2csystem.int64)(v=VS.103))Adds a relative virtual time to an absolute virtual time value. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.Add(TAbsolute, TRelative)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.add(%600%2c%601)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceBy](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceby(%601)(v=VS.103))Advances the scheduler's clock by the specified relative time, running all work scheduled for that timespan. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceTo](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceto(%600)(v=VS.103))Advances the scheduler's clock to the specified time, running all work till that point. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CreateColdObservable<T>](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.createcoldobservable%60%601(microsoft.reactive.testing.recorded%7bsystem.reactive.notification%7b%60%600%7d%7d%5b%5d)(v=VS.103))Creates a cold observable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CreateHotObservable<T>](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.createhotobservable%60%601(microsoft.reactive.testing.recorded%7bsystem.reactive.notification%7b%60%600%7d%7d%5b%5d)(v=VS.103))Creates a hot observable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CreateObserver<T>](CreateObserver/TestScheduler.CreateObserver(T))Creates a testable observer.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[GetNext](GetNext/VirtualTimeScheduler(TAbsolute,)Gets the next scheduled item to be executed. (Inherited from [VirtualTimeScheduler<TAbsolute, TRelative>](VirtualTimeScheduler/VirtualTimeScheduler(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, Int64, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.scheduleabsolute%60%601(%60%600%2csystem.int64%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at the specified virtual time. (Overrides [VirtualTimeScheduler<TAbsolute, TRelative>.ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimescheduler%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimescheduler%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeScheduler<TAbsolute, TRelative>](VirtualTimeScheduler/VirtualTimeScheduler(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleRelative<TState>](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedulerelative%60%601(%60%600%2c%601%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Start()](Start/VirtualTimeSchedulerBase(TAbsolute,)Starts the virtual time scheduler. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Start<T>(Func<IObservable<T>>)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.start%60%601(system.func%7bsystem.iobservable%7b%60%600%7d%7d)(v=VS.103))Starts the test scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Start<T>(Func<IObservable<T>>, Int64)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.start%60%601(system.func%7bsystem.iobservable%7b%60%600%7d%7d%2csystem.int64)(v=VS.103))Starts the test scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Start<T>(Func<IObservable<T>>, Int64, Int64, Int64)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.start%60%601(system.func%7bsystem.iobservable%7b%60%600%7d%7d%2csystem.int64%2csystem.int64%2csystem.int64)(v=VS.103))Starts the test scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Stop](Stop/VirtualTimeSchedulerBase(TAbsolute,)Stops the virtual time scheduler. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToDateTimeOffset](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.todatetimeoffset(system.int64)(v=VS.103))Converts the absolute virtual time value to a DateTimeOffset value. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.ToDateTimeOffset(TAbsolute)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.todatetimeoffset(%600)(v=VS.103)).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToRelative](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.torelative(system.timespan)(v=VS.103))Converts the TimeSpan value to a relative virtual time value. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.ToRelative(TimeSpan)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.torelative(system.timespan)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Extension Methods

NameDescription![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule](https://msdn.microsoft.com/en-us/library/m:reactivetests.extensions.schedule(microsoft.reactive.testing.testscheduler%2csystem.action%2csystem.int64)(v=VS.103))(Defined by [Extensions](Extensions/Extensions).)Top

## See Also

#### Reference

[TestScheduler Class](TestScheduler/TestScheduler)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

# TestScheduler Class

Base class for testing Rx code.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  [System.Reactive.Concurrency.VirtualTimeSchedulerBase](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek), [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
    [System.Reactive.Concurrency.VirtualTimeScheduler](VirtualTimeScheduler/VirtualTimeScheduler(TAbsolute,)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek), [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
      Microsoft.Reactive.Testing.TestScheduler

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Class TestScheduler _
    Inherits VirtualTimeScheduler(Of Long, Long)
```

```vb
'Usage
Dim instance As TestScheduler
```

```csharp
public class TestScheduler : VirtualTimeScheduler<long, long>
```

```c++
public ref class TestScheduler : public VirtualTimeScheduler<long long, long long>
```

```fsharp
type TestScheduler =  
    class
        inherit VirtualTimeScheduler<int64, int64>
    end
```

```javascript
public class TestScheduler extends VirtualTimeScheduler<long, long>
```

The TestScheduler type exposes the following members.

## Constructors

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[TestScheduler](TestScheduler/TestScheduler)Initializes a new instance of the TestScheduler class.Top

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Clock](Clock/VirtualTimeSchedulerBase(TAbsolute,)Gets the scheduler's absolute time clock value. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Protected property](https://reactiveui.net/assets/img/Hh211972.protproperty(en-us,VS.103).gif "Protected property")[Comparer](Comparer/VirtualTimeSchedulerBase(TAbsolute,)Gets the comparer used to compare absolute time values. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsEnabled](IsEnabled/VirtualTimeSchedulerBase(TAbsolute,)Gets whether the scheduler is enabled to run work. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Now](Now/VirtualTimeSchedulerBase(TAbsolute,)Gets the scheduler's notion of current time. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)Top

## Methods

NameDescription![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Add](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.add(system.int64%2csystem.int64)(v=VS.103))Adds a relative virtual time to an absolute virtual time value. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.Add(TAbsolute, TRelative)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.add(%600%2c%601)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceBy](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceby(%601)(v=VS.103))Advances the scheduler's clock by the specified relative time, running all work scheduled for that timespan. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AdvanceTo](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.advanceto(%600)(v=VS.103))Advances the scheduler's clock to the specified time, running all work till that point. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CreateColdObservable<T>](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.createcoldobservable%60%601(microsoft.reactive.testing.recorded%7bsystem.reactive.notification%7b%60%600%7d%7d%5b%5d)(v=VS.103))Creates a cold observable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CreateHotObservable<T>](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.createhotobservable%60%601(microsoft.reactive.testing.recorded%7bsystem.reactive.notification%7b%60%600%7d%7d%5b%5d)(v=VS.103))Creates a hot observable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CreateObserver<T>](CreateObserver/TestScheduler.CreateObserver(T))Creates a testable observer.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[GetNext](GetNext/VirtualTimeScheduler(TAbsolute,)Gets the next scheduled item to be executed. (Inherited from [VirtualTimeScheduler<TAbsolute, TRelative>](VirtualTimeScheduler/VirtualTimeScheduler(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, DateTimeOffset, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.datetimeoffset%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Schedule<TState>(TState, TimeSpan, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedule%60%601(%60%600%2csystem.timespan%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed after dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, Int64, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.scheduleabsolute%60%601(%60%600%2csystem.int64%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at the specified virtual time. (Overrides [VirtualTimeScheduler<TAbsolute, TRelative>.ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimescheduler%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimescheduler%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeScheduler<TAbsolute, TRelative>](VirtualTimeScheduler/VirtualTimeScheduler(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleAbsolute<TState>(TState, TAbsolute, Func<IScheduler, TState, IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.scheduleabsolute%60%601(%60%600%2c%600%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduleRelative<TState>](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.schedulerelative%60%601(%60%600%2c%601%2csystem.func%7bsystem.reactive.concurrency.ischeduler%2c%60%600%2csystem.idisposable%7d)(v=VS.103))Schedules an action to be executed at dueTime. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Start()](Start/VirtualTimeSchedulerBase(TAbsolute,)Starts the virtual time scheduler. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Start<T>(Func<IObservable<T>>)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.start%60%601(system.func%7bsystem.iobservable%7b%60%600%7d%7d)(v=VS.103))Starts the test scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Start<T>(Func<IObservable<T>>, Int64)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.start%60%601(system.func%7bsystem.iobservable%7b%60%600%7d%7d%2csystem.int64)(v=VS.103))Starts the test scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Start<T>(Func<IObservable<T>>, Int64, Int64, Int64)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.start%60%601(system.func%7bsystem.iobservable%7b%60%600%7d%7d%2csystem.int64%2csystem.int64%2csystem.int64)(v=VS.103))Starts the test scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Stop](Stop/VirtualTimeSchedulerBase(TAbsolute,)Stops the virtual time scheduler. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToDateTimeOffset](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.todatetimeoffset(system.int64)(v=VS.103))Converts the absolute virtual time value to a DateTimeOffset value. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.ToDateTimeOffset(TAbsolute)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.todatetimeoffset(%600)(v=VS.103)).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[ToRelative](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.testscheduler.torelative(system.timespan)(v=VS.103))Converts the TimeSpan value to a relative virtual time value. (Overrides [VirtualTimeSchedulerBase<TAbsolute, TRelative>.ToRelative(TimeSpan)](https://msdn.microsoft.com/en-us/library/m:system.reactive.concurrency.virtualtimeschedulerbase%602.torelative(system.timespan)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Extension Methods

NameDescription![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Schedule](https://msdn.microsoft.com/en-us/library/m:reactivetests.extensions.schedule(microsoft.reactive.testing.testscheduler%2csystem.action%2csystem.int64)(v=VS.103))(Defined by [Extensions](Extensions/Extensions).)Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

# TestScheduler Properties

Include Protected Members  
Include Inherited Members

The [TestScheduler](TestScheduler/TestScheduler) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Clock](Clock/VirtualTimeSchedulerBase(TAbsolute,)Gets the scheduler's absolute time clock value. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Protected property](https://reactiveui.net/assets/img/Hh211972.protproperty(en-us,VS.103).gif "Protected property")[Comparer](Comparer/VirtualTimeSchedulerBase(TAbsolute,)Gets the comparer used to compare absolute time values. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsEnabled](IsEnabled/VirtualTimeSchedulerBase(TAbsolute,)Gets whether the scheduler is enabled to run work. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Now](Now/VirtualTimeSchedulerBase(TAbsolute,)Gets the scheduler's notion of current time. (Inherited from [VirtualTimeSchedulerBase<TAbsolute, TRelative>](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,).)Top

## See Also

#### Reference

[TestScheduler Class](TestScheduler/TestScheduler)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)
