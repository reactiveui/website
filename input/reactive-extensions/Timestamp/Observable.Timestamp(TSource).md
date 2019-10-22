title: Observable.Timestamp<TSource>(IObservable<TSource>, IScheduler)
---
# Observable.Timestamp\<TSource\> Method (IObservable\<TSource\>, IScheduler)

Records the timestamp for each value in an observable sequence with the specified source and scheduler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Timestamp(Of TSource) ( _
    source As IObservable(Of TSource), _
    scheduler As IScheduler _
) As IObservable(Of Timestamped(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of Timestamped(Of TSource))

returnValue = source.Timestamp(scheduler)
```

```csharp
public static IObservable<Timestamped<TSource>> Timestamp<TSource>(
    this IObservable<TSource> source,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<Timestamped<TSource>>^ Timestamp(
    IObservable<TSource>^ source, 
    IScheduler^ scheduler
)
```

```fsharp
static member Timestamp : 
        source:IObservable<'TSource> * 
        scheduler:IScheduler -> IObservable<Timestamped<'TSource>> 
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
  The source sequence to timestamp values for.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to compute timestamps.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Timestamped](Timestamped\Timestamped(T).md)\<TSource\>\>  
An observable sequence with timestamp information on values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Timestamp Overload](Timestamp\Observable.Timestamp.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Timestamp\<TSource\> Method (IObservable\<TSource\>)

Records the timestamp for each value in an observable sequence with the specified source.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Timestamp(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IObservable(Of Timestamped(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IObservable(Of Timestamped(Of TSource))

returnValue = source.Timestamp()
```

```csharp
public static IObservable<Timestamped<TSource>> Timestamp<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<Timestamped<TSource>>^ Timestamp(
    IObservable<TSource>^ source
)
```

```fsharp
static member Timestamp : 
        source:IObservable<'TSource> -> IObservable<Timestamped<'TSource>> 
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
  The source sequence to timestamp values for.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Timestamped](Timestamped\Timestamped(T).md)\<TSource\>\>  
An observable sequence with timestamp information on values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Timestamp Overload](Timestamp\Observable.Timestamp.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
