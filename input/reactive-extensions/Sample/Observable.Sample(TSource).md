title: Observable.Sample<TSource>(IObservable<TSource>, TimeSpan, IScheduler)
---
# Observable.Sample\<TSource\> Method (IObservable\<TSource\>, TimeSpan, IScheduler)

Samples the observable sequence at each interval with the specified source, interval and scheduler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Sample(Of TSource) ( _
    source As IObservable(Of TSource), _
    interval As TimeSpan, _
    scheduler As IScheduler _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim interval As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TSource)

returnValue = source.Sample(interval, _
    scheduler)
```

```csharp
public static IObservable<TSource> Sample<TSource>(
    this IObservable<TSource> source,
    TimeSpan interval,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Sample(
    IObservable<TSource>^ source, 
    TimeSpan interval, 
    IScheduler^ scheduler
)
```

```fsharp
static member Sample : 
        source:IObservable<'TSource> * 
        interval:TimeSpan * 
        scheduler:IScheduler -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to sample.

- interval  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The interval at which to sample.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to run the sampling timer on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The sampled observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Sample Overload](Sample/Observable.Sample)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Sample\<TSource\> Method (IObservable\<TSource\>, TimeSpan)

Samples the observable sequence at each interval.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Sample(Of TSource) ( _
    source As IObservable(Of TSource), _
    interval As TimeSpan _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim interval As TimeSpan
Dim returnValue As IObservable(Of TSource)

returnValue = source.Sample(interval)
```

```csharp
public static IObservable<TSource> Sample<TSource>(
    this IObservable<TSource> source,
    TimeSpan interval
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Sample(
    IObservable<TSource>^ source, 
    TimeSpan interval
)
```

```fsharp
static member Sample : 
        source:IObservable<'TSource> * 
        interval:TimeSpan -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to sample.

- interval  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The interval at which to sample.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The sampled observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Sample Overload](Sample/Observable.Sample)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
