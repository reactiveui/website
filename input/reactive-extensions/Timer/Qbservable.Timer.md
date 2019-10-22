title: Qbservable.Timer(IQbservableProvider, DateTimeOffset, TimeSpan, IScheduler)
---
# Qbservable.Timer Method (IQbservableProvider, DateTimeOffset, TimeSpan, IScheduler)

Returns a queryable observable sequence that produces a value at due time and then after each period.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Timer ( _
    provider As IQbservableProvider, _
    dueTime As DateTimeOffset, _
    period As TimeSpan, _
    scheduler As IScheduler _
) As IQbservable(Of Long)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim dueTime As DateTimeOffset
Dim period As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of Long)

returnValue = provider.Timer(dueTime, _
    period, scheduler)
```

```csharp
public static IQbservable<long> Timer(
    this IQbservableProvider provider,
    DateTimeOffset dueTime,
    TimeSpan period,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<long long>^ Timer(
    IQbservableProvider^ provider, 
    DateTimeOffset dueTime, 
    TimeSpan period, 
    IScheduler^ scheduler
)
```

```fsharp
static member Timer : 
        provider:IQbservableProvider * 
        dueTime:DateTimeOffset * 
        period:TimeSpan * 
        scheduler:IScheduler -> IQbservable<int64> 
```

```jscript
public static function Timer(
    provider : IQbservableProvider, 
    dueTime : DateTimeOffset, 
    period : TimeSpan, 
    scheduler : IScheduler
) : IQbservable<long>
```

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- dueTime  
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
  The absolute time at which to produce the first value.

- period  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The period to produce subsequent values.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run the timer on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
A queryable observable sequence that produces a value at due time and then after each period.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Timer Overload](Timer\Qbservable.Timer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Timer Method (IQbservableProvider, DateTimeOffset, TimeSpan)

Returns a queryable observable sequence that produces a value at due time and then after each period.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Timer ( _
    provider As IQbservableProvider, _
    dueTime As DateTimeOffset, _
    period As TimeSpan _
) As IQbservable(Of Long)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim dueTime As DateTimeOffset
Dim period As TimeSpan
Dim returnValue As IQbservable(Of Long)

returnValue = provider.Timer(dueTime, _
    period)
```

```csharp
public static IQbservable<long> Timer(
    this IQbservableProvider provider,
    DateTimeOffset dueTime,
    TimeSpan period
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<long long>^ Timer(
    IQbservableProvider^ provider, 
    DateTimeOffset dueTime, 
    TimeSpan period
)
```

```fsharp
static member Timer : 
        provider:IQbservableProvider * 
        dueTime:DateTimeOffset * 
        period:TimeSpan -> IQbservable<int64> 
```

```jscript
public static function Timer(
    provider : IQbservableProvider, 
    dueTime : DateTimeOffset, 
    period : TimeSpan
) : IQbservable<long>
```

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- dueTime  
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
  The absolute time at which to produce the first value.

- period  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The period to produce subsequent values.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
A queryable observable sequence that produces a value at due time and then after each period.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Timer Overload](Timer\Qbservable.Timer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Timer Method (IQbservableProvider, TimeSpan, TimeSpan)

Returns a queryable observable sequence that produces a value after due time has elapsed and then after each period.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Timer ( _
    provider As IQbservableProvider, _
    dueTime As TimeSpan, _
    period As TimeSpan _
) As IQbservable(Of Long)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim dueTime As TimeSpan
Dim period As TimeSpan
Dim returnValue As IQbservable(Of Long)

returnValue = provider.Timer(dueTime, _
    period)
```

```csharp
public static IQbservable<long> Timer(
    this IQbservableProvider provider,
    TimeSpan dueTime,
    TimeSpan period
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<long long>^ Timer(
    IQbservableProvider^ provider, 
    TimeSpan dueTime, 
    TimeSpan period
)
```

```fsharp
static member Timer : 
        provider:IQbservableProvider * 
        dueTime:TimeSpan * 
        period:TimeSpan -> IQbservable<int64> 
```

```jscript
public static function Timer(
    provider : IQbservableProvider, 
    dueTime : TimeSpan, 
    period : TimeSpan
) : IQbservable<long>
```

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The relative time at which to produce the first value.

- period  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The period to produce subsequent values.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
A queryable observable sequence that produces a value after due time has elapsed and then after each period.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Timer Overload](Timer\Qbservable.Timer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Timer Method (IQbservableProvider, DateTimeOffset, IScheduler)

Returns a queryable observable sequence that produces a value at due time.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Timer ( _
    provider As IQbservableProvider, _
    dueTime As DateTimeOffset, _
    scheduler As IScheduler _
) As IQbservable(Of Long)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim dueTime As DateTimeOffset
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of Long)

returnValue = provider.Timer(dueTime, _
    scheduler)
```

```csharp
public static IQbservable<long> Timer(
    this IQbservableProvider provider,
    DateTimeOffset dueTime,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<long long>^ Timer(
    IQbservableProvider^ provider, 
    DateTimeOffset dueTime, 
    IScheduler^ scheduler
)
```

```fsharp
static member Timer : 
        provider:IQbservableProvider * 
        dueTime:DateTimeOffset * 
        scheduler:IScheduler -> IQbservable<int64> 
```

```jscript
public static function Timer(
    provider : IQbservableProvider, 
    dueTime : DateTimeOffset, 
    scheduler : IScheduler
) : IQbservable<long>
```

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- dueTime  
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
  The absolute time at which to produce the value.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run the timer on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
A queryable observable sequence that produces a value at due time.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Timer Overload](Timer\Qbservable.Timer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Timer Method (IQbservableProvider, TimeSpan, TimeSpan, IScheduler)

Returns a queryable observable sequence that produces a value after due time has elapsed and then each period.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Timer ( _
    provider As IQbservableProvider, _
    dueTime As TimeSpan, _
    period As TimeSpan, _
    scheduler As IScheduler _
) As IQbservable(Of Long)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim dueTime As TimeSpan
Dim period As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of Long)

returnValue = provider.Timer(dueTime, _
    period, scheduler)
```

```csharp
public static IQbservable<long> Timer(
    this IQbservableProvider provider,
    TimeSpan dueTime,
    TimeSpan period,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<long long>^ Timer(
    IQbservableProvider^ provider, 
    TimeSpan dueTime, 
    TimeSpan period, 
    IScheduler^ scheduler
)
```

```fsharp
static member Timer : 
        provider:IQbservableProvider * 
        dueTime:TimeSpan * 
        period:TimeSpan * 
        scheduler:IScheduler -> IQbservable<int64> 
```

```jscript
public static function Timer(
    provider : IQbservableProvider, 
    dueTime : TimeSpan, 
    period : TimeSpan, 
    scheduler : IScheduler
) : IQbservable<long>
```

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The relative time at which to produce the first value.

- period  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The period to produce subsequent values.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run the timer on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
A queryable observable sequence that produces a value after due time has elapsed and then each period.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Timer Overload](Timer\Qbservable.Timer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Timer Method (IQbservableProvider, TimeSpan, IScheduler)

Returns a queryable observable sequence that produces a value after the due time has elapsed.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Timer ( _
    provider As IQbservableProvider, _
    dueTime As TimeSpan, _
    scheduler As IScheduler _
) As IQbservable(Of Long)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim dueTime As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of Long)

returnValue = provider.Timer(dueTime, _
    scheduler)
```

```csharp
public static IQbservable<long> Timer(
    this IQbservableProvider provider,
    TimeSpan dueTime,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<long long>^ Timer(
    IQbservableProvider^ provider, 
    TimeSpan dueTime, 
    IScheduler^ scheduler
)
```

```fsharp
static member Timer : 
        provider:IQbservableProvider * 
        dueTime:TimeSpan * 
        scheduler:IScheduler -> IQbservable<int64> 
```

```jscript
public static function Timer(
    provider : IQbservableProvider, 
    dueTime : TimeSpan, 
    scheduler : IScheduler
) : IQbservable<long>
```

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The relative time at which to produce the value.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run the timer on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
A queryable observable sequence that produces a value after the due time has elapsed.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Timer Overload](Timer\Qbservable.Timer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Timer Method (IQbservableProvider, TimeSpan)

Returns a queryable observable sequence that produces a value after the due time has elapsed.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Timer ( _
    provider As IQbservableProvider, _
    dueTime As TimeSpan _
) As IQbservable(Of Long)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim dueTime As TimeSpan
Dim returnValue As IQbservable(Of Long)

returnValue = provider.Timer(dueTime)
```

```csharp
public static IQbservable<long> Timer(
    this IQbservableProvider provider,
    TimeSpan dueTime
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<long long>^ Timer(
    IQbservableProvider^ provider, 
    TimeSpan dueTime
)
```

```fsharp
static member Timer : 
        provider:IQbservableProvider * 
        dueTime:TimeSpan -> IQbservable<int64> 
```

```jscript
public static function Timer(
    provider : IQbservableProvider, 
    dueTime : TimeSpan
) : IQbservable<long>
```

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The relative time at which to produce the value.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
A queryable observable sequence that produces a value after the due time has elapsed.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Timer Overload](Timer\Qbservable.Timer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Timer Method (IQbservableProvider, DateTimeOffset)

Returns a queryable observable sequence that produces a value at due time.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Timer ( _
    provider As IQbservableProvider, _
    dueTime As DateTimeOffset _
) As IQbservable(Of Long)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim dueTime As DateTimeOffset
Dim returnValue As IQbservable(Of Long)

returnValue = provider.Timer(dueTime)
```

```csharp
public static IQbservable<long> Timer(
    this IQbservableProvider provider,
    DateTimeOffset dueTime
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<long long>^ Timer(
    IQbservableProvider^ provider, 
    DateTimeOffset dueTime
)
```

```fsharp
static member Timer : 
        provider:IQbservableProvider * 
        dueTime:DateTimeOffset -> IQbservable<int64> 
```

```jscript
public static function Timer(
    provider : IQbservableProvider, 
    dueTime : DateTimeOffset
) : IQbservable<long>
```

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- dueTime  
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
  The absolute time at which to produce the value.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
A queryable observable sequence that produces a value at due time.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Timer Overload](Timer\Qbservable.Timer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Timer Method

Include Protected Members  
Include Inherited Members

Returns a queryable observable sequence of timer.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(IQbservableProvider, DateTimeOffset)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.timer(system.reactive.linq.iqbservableprovider%2csystem.datetimeoffset)(v=VS.103))Returns a queryable observable sequence that produces a value at due time.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(IQbservableProvider, TimeSpan)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.timer(system.reactive.linq.iqbservableprovider%2csystem.timespan)(v=VS.103))Returns a queryable observable sequence that produces a value after the due time has elapsed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(IQbservableProvider, DateTimeOffset, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.timer(system.reactive.linq.iqbservableprovider%2csystem.datetimeoffset%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Returns a queryable observable sequence that produces a value at due time.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(IQbservableProvider, DateTimeOffset, TimeSpan)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.timer(system.reactive.linq.iqbservableprovider%2csystem.datetimeoffset%2csystem.timespan)(v=VS.103))Returns a queryable observable sequence that produces a value at due time and then after each period.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(IQbservableProvider, TimeSpan, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.timer(system.reactive.linq.iqbservableprovider%2csystem.timespan%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Returns a queryable observable sequence that produces a value after the due time has elapsed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(IQbservableProvider, TimeSpan, TimeSpan)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.timer(system.reactive.linq.iqbservableprovider%2csystem.timespan%2csystem.timespan)(v=VS.103))Returns a queryable observable sequence that produces a value after due time has elapsed and then after each period.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(IQbservableProvider, DateTimeOffset, TimeSpan, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.timer(system.reactive.linq.iqbservableprovider%2csystem.datetimeoffset%2csystem.timespan%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Returns a queryable observable sequence that produces a value at due time and then after each period.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Timer(IQbservableProvider, TimeSpan, TimeSpan, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.timer(system.reactive.linq.iqbservableprovider%2csystem.timespan%2csystem.timespan%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Returns a queryable observable sequence that produces a value after due time has elapsed and then each period.Top

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
