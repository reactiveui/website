title: Observable.Timer(DateTimeOffset, TimeSpan)
---
# Observable.Timer Method (DateTimeOffset, TimeSpan)

Returns an observable sequence that produces a value at due time and then after each period.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Timer ( _
    dueTime As DateTimeOffset, _
    period As TimeSpan _
) As IObservable(Of Long)
```

```vb
'Usage
Dim dueTime As DateTimeOffset
Dim period As TimeSpan
Dim returnValue As IObservable(Of Long)

returnValue = Observable.Timer(dueTime, _
    period)
```

```csharp
public static IObservable<long> Timer(
    DateTimeOffset dueTime,
    TimeSpan period
)
```

```c++
public:
static IObservable<long long>^ Timer(
    DateTimeOffset dueTime, 
    TimeSpan period
)
```

```fsharp
static member Timer : 
        dueTime:DateTimeOffset * 
        period:TimeSpan -> IObservable<int64> 
```

```javascript
public static function Timer(
    dueTime : DateTimeOffset, 
    period : TimeSpan
) : IObservable<long>
```

#### Parameters

- dueTime  
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
  The absolute time at which to produce the first value.

- period  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The period to produce subsequent values.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
An observable sequence that produces a value at due time and then after each period.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Timer Overload](Timer/Observable.Timer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Timer Method (TimeSpan, TimeSpan, IScheduler)

Returns an observable sequence that produces a value after due time has elapsed and then each period.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Timer ( _
    dueTime As TimeSpan, _
    period As TimeSpan, _
    scheduler As IScheduler _
) As IObservable(Of Long)
```

```vb
'Usage
Dim dueTime As TimeSpan
Dim period As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of Long)

returnValue = Observable.Timer(dueTime, _
    period, scheduler)
```

```csharp
public static IObservable<long> Timer(
    TimeSpan dueTime,
    TimeSpan period,
    IScheduler scheduler
)
```

```c++
public:
static IObservable<long long>^ Timer(
    TimeSpan dueTime, 
    TimeSpan period, 
    IScheduler^ scheduler
)
```

```fsharp
static member Timer : 
        dueTime:TimeSpan * 
        period:TimeSpan * 
        scheduler:IScheduler -> IObservable<int64> 
```

```javascript
public static function Timer(
    dueTime : TimeSpan, 
    period : TimeSpan, 
    scheduler : IScheduler
) : IObservable<long>
```

#### Parameters

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The relative time at which to produce the first value.

- period  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The period to produce subsequent values.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to run the timer on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
An observable sequence that produces a value after due time has elapsed and then each period.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Timer Overload](Timer/Observable.Timer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Timer Method (TimeSpan, IScheduler)

Returns an observable sequence that produces a value after the due time has elapsed.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Timer ( _
    dueTime As TimeSpan, _
    scheduler As IScheduler _
) As IObservable(Of Long)
```

```vb
'Usage
Dim dueTime As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of Long)

returnValue = Observable.Timer(dueTime, _
    scheduler)
```

```csharp
public static IObservable<long> Timer(
    TimeSpan dueTime,
    IScheduler scheduler
)
```

```c++
public:
static IObservable<long long>^ Timer(
    TimeSpan dueTime, 
    IScheduler^ scheduler
)
```

```fsharp
static member Timer : 
        dueTime:TimeSpan * 
        scheduler:IScheduler -> IObservable<int64> 
```

```javascript
public static function Timer(
    dueTime : TimeSpan, 
    scheduler : IScheduler
) : IObservable<long>
```

#### Parameters

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The relative time at which to produce the value.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to run the timer on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
An observable sequence that produces a value after the due time has elapsed.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Timer Overload](Timer/Observable.Timer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Timer Method (TimeSpan, TimeSpan)

Returns an observable sequence that produces a value after due time has elapsed and then after each period.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Timer ( _
    dueTime As TimeSpan, _
    period As TimeSpan _
) As IObservable(Of Long)
```

```vb
'Usage
Dim dueTime As TimeSpan
Dim period As TimeSpan
Dim returnValue As IObservable(Of Long)

returnValue = Observable.Timer(dueTime, _
    period)
```

```csharp
public static IObservable<long> Timer(
    TimeSpan dueTime,
    TimeSpan period
)
```

```c++
public:
static IObservable<long long>^ Timer(
    TimeSpan dueTime, 
    TimeSpan period
)
```

```fsharp
static member Timer : 
        dueTime:TimeSpan * 
        period:TimeSpan -> IObservable<int64> 
```

```javascript
public static function Timer(
    dueTime : TimeSpan, 
    period : TimeSpan
) : IObservable<long>
```

#### Parameters

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The relative time at which to produce the first value.

- period  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The period to produce subsequent values.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
An observable sequence that produces a value after due time has elapsed and then after each period.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Timer Overload](Timer/Observable.Timer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Timer Method (DateTimeOffset, IScheduler)

Returns an observable sequence that produces a value at due time.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Timer ( _
    dueTime As DateTimeOffset, _
    scheduler As IScheduler _
) As IObservable(Of Long)
```

```vb
'Usage
Dim dueTime As DateTimeOffset
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of Long)

returnValue = Observable.Timer(dueTime, _
    scheduler)
```

```csharp
public static IObservable<long> Timer(
    DateTimeOffset dueTime,
    IScheduler scheduler
)
```

```c++
public:
static IObservable<long long>^ Timer(
    DateTimeOffset dueTime, 
    IScheduler^ scheduler
)
```

```fsharp
static member Timer : 
        dueTime:DateTimeOffset * 
        scheduler:IScheduler -> IObservable<int64> 
```

```javascript
public static function Timer(
    dueTime : DateTimeOffset, 
    scheduler : IScheduler
) : IObservable<long>
```

#### Parameters

- dueTime  
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
  The absolute time at which to produce the value.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to run the timer on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
An observable sequence that produces a value at due time.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Timer Overload](Timer/Observable.Timer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Timer Method (DateTimeOffset, TimeSpan, IScheduler)

Returns an observable sequence that produces a value at due time and then after each period.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Timer ( _
    dueTime As DateTimeOffset, _
    period As TimeSpan, _
    scheduler As IScheduler _
) As IObservable(Of Long)
```

```vb
'Usage
Dim dueTime As DateTimeOffset
Dim period As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of Long)

returnValue = Observable.Timer(dueTime, _
    period, scheduler)
```

```csharp
public static IObservable<long> Timer(
    DateTimeOffset dueTime,
    TimeSpan period,
    IScheduler scheduler
)
```

```c++
public:
static IObservable<long long>^ Timer(
    DateTimeOffset dueTime, 
    TimeSpan period, 
    IScheduler^ scheduler
)
```

```fsharp
static member Timer : 
        dueTime:DateTimeOffset * 
        period:TimeSpan * 
        scheduler:IScheduler -> IObservable<int64> 
```

```javascript
public static function Timer(
    dueTime : DateTimeOffset, 
    period : TimeSpan, 
    scheduler : IScheduler
) : IObservable<long>
```

#### Parameters

- dueTime  
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
  The absolute time at which to produce the first value.

- period  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The period to produce subsequent values.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to run the timer on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
An observable sequence that produces a value at due time and then after each period.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Timer Overload](Timer/Observable.Timer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Timer Method (DateTimeOffset)

Returns an observable sequence that produces a value at due time.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Timer ( _
    dueTime As DateTimeOffset _
) As IObservable(Of Long)
```

```vb
'Usage
Dim dueTime As DateTimeOffset
Dim returnValue As IObservable(Of Long)

returnValue = Observable.Timer(dueTime)
```

```csharp
public static IObservable<long> Timer(
    DateTimeOffset dueTime
)
```

```c++
public:
static IObservable<long long>^ Timer(
    DateTimeOffset dueTime
)
```

```fsharp
static member Timer : 
        dueTime:DateTimeOffset -> IObservable<int64> 
```

```javascript
public static function Timer(
    dueTime : DateTimeOffset
) : IObservable<long>
```

#### Parameters

- dueTime  
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
  The absolute time at which to produce the value.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
An observable sequence that produces a value at due time.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Timer Overload](Timer/Observable.Timer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Timer Method (TimeSpan)

Returns an observable sequence that produces a value after the due time has elapsed.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Timer ( _
    dueTime As TimeSpan _
) As IObservable(Of Long)
```

```vb
'Usage
Dim dueTime As TimeSpan
Dim returnValue As IObservable(Of Long)

returnValue = Observable.Timer(dueTime)
```

```csharp
public static IObservable<long> Timer(
    TimeSpan dueTime
)
```

```c++
public:
static IObservable<long long>^ Timer(
    TimeSpan dueTime
)
```

```fsharp
static member Timer : 
        dueTime:TimeSpan -> IObservable<int64> 
```

```javascript
public static function Timer(
    dueTime : TimeSpan
) : IObservable<long>
```

#### Parameters

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The relative time at which to produce the value.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
An observable sequence that produces a value after the due time has elapsed.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Timer Overload](Timer/Observable.Timer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Timer Method

Include Protected Members  
Include Inherited Members

Returns an observable sequence of timer.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(DateTimeOffset)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.timer(system.datetimeoffset)(v=VS.103))Returns an observable sequence that produces a value at due time.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(TimeSpan)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.timer(system.timespan)(v=VS.103))Returns an observable sequence that produces a value after the due time has elapsed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(DateTimeOffset, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.timer(system.datetimeoffset%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Returns an observable sequence that produces a value at due time.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(DateTimeOffset, TimeSpan)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.timer(system.datetimeoffset%2csystem.timespan)(v=VS.103))Returns an observable sequence that produces a value at due time and then after each period.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(TimeSpan, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.timer(system.timespan%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Returns an observable sequence that produces a value after the due time has elapsed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(TimeSpan, TimeSpan)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.timer(system.timespan%2csystem.timespan)(v=VS.103))Returns an observable sequence that produces a value after due time has elapsed and then after each period.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(DateTimeOffset, TimeSpan, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.timer(system.datetimeoffset%2csystem.timespan%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Returns an observable sequence that produces a value at due time and then after each period.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(TimeSpan, TimeSpan, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.timer(system.timespan%2csystem.timespan%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Returns an observable sequence that produces a value after due time has elapsed and then each period.Top

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
