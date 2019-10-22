title: Qbservable.Interval(IQbservableProvider, TimeSpan)
---
# Qbservable.Interval Method (IQbservableProvider, TimeSpan)

Returns a queryable observable sequence that produces a value after each period.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Interval ( _
    provider As IQbservableProvider, _
    period As TimeSpan _
) As IQbservable(Of Long)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim period As TimeSpan
Dim returnValue As IQbservable(Of Long)

returnValue = provider.Interval(period)
```

```csharp
public static IQbservable<long> Interval(
    this IQbservableProvider provider,
    TimeSpan period
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<long long>^ Interval(
    IQbservableProvider^ provider, 
    TimeSpan period
)
```

```fsharp
static member Interval : 
        provider:IQbservableProvider * 
        period:TimeSpan -> IQbservable<int64> 
```

```jscript
public static function Interval(
    provider : IQbservableProvider, 
    period : TimeSpan
) : IQbservable<long>
```

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- period  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The period for producing the values in the resulting sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
A queryable observable sequence that produces a value after each period.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Interval Overload](Interval\Qbservable.Interval.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Qbservable.Interval Method (IQbservableProvider, TimeSpan, IScheduler)

Returns a queryable observable sequence that produces a value after each period.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Interval ( _
    provider As IQbservableProvider, _
    period As TimeSpan, _
    scheduler As IScheduler _
) As IQbservable(Of Long)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim period As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of Long)

returnValue = provider.Interval(period, _
    scheduler)
```

```csharp
public static IQbservable<long> Interval(
    this IQbservableProvider provider,
    TimeSpan period,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<long long>^ Interval(
    IQbservableProvider^ provider, 
    TimeSpan period, 
    IScheduler^ scheduler
)
```

```fsharp
static member Interval : 
        provider:IQbservableProvider * 
        period:TimeSpan * 
        scheduler:IScheduler -> IQbservable<int64> 
```

```jscript
public static function Interval(
    provider : IQbservableProvider, 
    period : TimeSpan, 
    scheduler : IScheduler
) : IQbservable<long>
```

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- period  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The period for producing the values in the resulting sequence.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run the timer on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
A queryable observable sequence that produces a value after each period.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Interval Overload](Interval\Qbservable.Interval.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Qbservable.Interval Method

Include Protected Members  
Include Inherited Members

Returns an observable sequence that produces a value after each period.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Interval(IQbservableProvider, TimeSpan)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.interval(system.reactive.linq.iqbservableprovider%2csystem.timespan)(v=VS.103))Returns a queryable observable sequence that produces a value after each period.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Interval(IQbservableProvider, TimeSpan, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.interval(system.reactive.linq.iqbservableprovider%2csystem.timespan%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Returns a queryable observable sequence that produces a value after each period.Top

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)




