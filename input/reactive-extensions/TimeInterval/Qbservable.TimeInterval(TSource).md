title: Qbservable.TimeInterval<TSource>(IQbservable<TSource>, IScheduler)
---
# Qbservable.TimeInterval\<TSource\> Method (IQbservable\<TSource\>, IScheduler)

Records the time interval between consecutive values in a queryable observable sequence with the specified source and scheduler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function TimeInterval(Of TSource) ( _
    source As IQbservable(Of TSource), _
    scheduler As IScheduler _
) As IQbservable(Of TimeInterval(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of TimeInterval(Of TSource))

returnValue = source.TimeInterval(scheduler)
```

```csharp
public static IQbservable<TimeInterval<TSource>> TimeInterval<TSource>(
    this IQbservable<TSource> source,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TimeInterval<TSource>>^ TimeInterval(
    IQbservable<TSource>^ source, 
    IScheduler^ scheduler
)
```

```fsharp
static member TimeInterval : 
        source:IQbservable<'TSource> * 
        scheduler:IScheduler -> IQbservable<TimeInterval<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence to record time intervals for.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler used to compute time intervals.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[TimeInterval](TimeInterval/TimeInterval(T))\<TSource\>\>  
A queryable observable sequence with time interval information on values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[TimeInterval Overload](TimeInterval/Qbservable.TimeInterval)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.TimeInterval\<TSource\> Method (IQbservable\<TSource\>)

Records the time interval between consecutive values in a queryable observable sequence with the specified source.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function TimeInterval(Of TSource) ( _
    source As IQbservable(Of TSource) _
) As IQbservable(Of TimeInterval(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim returnValue As IQbservable(Of TimeInterval(Of TSource))

returnValue = source.TimeInterval()
```

```csharp
public static IQbservable<TimeInterval<TSource>> TimeInterval<TSource>(
    this IQbservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TimeInterval<TSource>>^ TimeInterval(
    IQbservable<TSource>^ source
)
```

```fsharp
static member TimeInterval : 
        source:IQbservable<'TSource> -> IQbservable<TimeInterval<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence to record time intervals for.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[TimeInterval](TimeInterval/TimeInterval(T))\<TSource\>\>  
A queryable observable sequence with time interval information on values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[TimeInterval Overload](TimeInterval/Qbservable.TimeInterval)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
