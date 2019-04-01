# Observable.TimeInterval\<TSource\> Method (IObservable\<TSource\>, IScheduler)

Records the time interval between consecutive values in an observable sequence with the specified source and scheduler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function TimeInterval(Of TSource) ( _
    source As IObservable(Of TSource), _
    scheduler As IScheduler _
) As IObservable(Of TimeInterval(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TimeInterval(Of TSource))

returnValue = source.TimeInterval(scheduler)
```

```csharp
public static IObservable<TimeInterval<TSource>> TimeInterval<TSource>(
    this IObservable<TSource> source,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TimeInterval<TSource>>^ TimeInterval(
    IObservable<TSource>^ source, 
    IScheduler^ scheduler
)
```

```fsharp
static member TimeInterval : 
        source:IObservable<'TSource> * 
        scheduler:IScheduler -> IObservable<TimeInterval<'TSource>> 
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
  The source sequence to record time intervals for.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to compute time intervals.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[TimeInterval](TimeInterval\TimeInterval(T).md)\<TSource\>\>  
An observable sequence with time interval information on values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[TimeInterval Overload](TimeInterval\Observable.TimeInterval.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.TimeInterval\<TSource\> Method (IObservable\<TSource\>)

Records the time interval between consecutive values in an observable sequence with the specified source.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function TimeInterval(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IObservable(Of TimeInterval(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IObservable(Of TimeInterval(Of TSource))

returnValue = source.TimeInterval()
```

```csharp
public static IObservable<TimeInterval<TSource>> TimeInterval<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TimeInterval<TSource>>^ TimeInterval(
    IObservable<TSource>^ source
)
```

```fsharp
static member TimeInterval : 
        source:IObservable<'TSource> -> IObservable<TimeInterval<'TSource>> 
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
  The source sequence to record time intervals for.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[TimeInterval](TimeInterval\TimeInterval(T).md)\<TSource\>\>  
An observable sequence with time interval information on values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[TimeInterval Overload](TimeInterval\Observable.TimeInterval.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)