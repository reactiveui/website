title: Qbservable.Throttle<TSource>(IQbservable<TSource>, TimeSpan, IScheduler)
---
# Qbservable.Throttle\<TSource\> Method (IQbservable\<TSource\>, TimeSpan, IScheduler)

Ignores the values from a queryable observable sequence which are followed by another value before due time with the specified source, dueTime and scheduler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Throttle(Of TSource) ( _
    source As IQbservable(Of TSource), _
    dueTime As TimeSpan, _
    scheduler As IScheduler _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim dueTime As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of TSource)

returnValue = source.Throttle(dueTime, _
    scheduler)
```

```csharp
public static IQbservable<TSource> Throttle<TSource>(
    this IQbservable<TSource> source,
    TimeSpan dueTime,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Throttle(
    IQbservable<TSource>^ source, 
    TimeSpan dueTime, 
    IScheduler^ scheduler
)
```

```fsharp
static member Throttle : 
        source:IQbservable<'TSource> * 
        dueTime:TimeSpan * 
        scheduler:IScheduler -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to throttle.

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The duration of the throttle period for each value.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run the throttle timers on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
The values from a queryable observable sequence which are followed by another value before due time.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Throttle Overload](Throttle\Qbservable.Throttle.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Throttle\<TSource\> Method (IQbservable\<TSource\>, TimeSpan)

Ignores the values from a queryable observable sequence which are followed by another value before due time with the specified source and dueTime.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Throttle(Of TSource) ( _
    source As IQbservable(Of TSource), _
    dueTime As TimeSpan _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim dueTime As TimeSpan
Dim returnValue As IQbservable(Of TSource)

returnValue = source.Throttle(dueTime)
```

```csharp
public static IQbservable<TSource> Throttle<TSource>(
    this IQbservable<TSource> source,
    TimeSpan dueTime
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Throttle(
    IQbservable<TSource>^ source, 
    TimeSpan dueTime
)
```

```fsharp
static member Throttle : 
        source:IQbservable<'TSource> * 
        dueTime:TimeSpan -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to throttle.

- dueTime  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The duration of the throttle period for each value.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
The values from a queryable observable sequence which are followed by another value before due time.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Throttle Overload](Throttle\Qbservable.Throttle.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
