# Qbservable.Timestamp\<TSource\> Method (IQbservable\<TSource\>, IScheduler)

Records the timestamp for each value in a queryable observable sequence with the specified source and scheduler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Timestamp(Of TSource) ( _
    source As IQbservable(Of TSource), _
    scheduler As IScheduler _
) As IQbservable(Of Timestamped(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of Timestamped(Of TSource))

returnValue = source.Timestamp(scheduler)
```

```csharp
public static IQbservable<Timestamped<TSource>> Timestamp<TSource>(
    this IQbservable<TSource> source,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<Timestamped<TSource>>^ Timestamp(
    IQbservable<TSource>^ source, 
    IScheduler^ scheduler
)
```

```fsharp
static member Timestamp : 
        source:IQbservable<'TSource> * 
        scheduler:IScheduler -> IQbservable<Timestamped<'TSource>> 
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
  The source sequence to timestamp values for.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to compute timestamps.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Timestamped](Timestamped\Timestamped(T).md)\<TSource\>\>  
A queryable observable sequence with timestamp information on values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Timestamp Overload](Timestamp\Qbservable.Timestamp.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Timestamp\<TSource\> Method (IQbservable\<TSource\>)

Records the timestamp for each value in a queryable observable sequence with the specified source.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Timestamp(Of TSource) ( _
    source As IQbservable(Of TSource) _
) As IQbservable(Of Timestamped(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim returnValue As IQbservable(Of Timestamped(Of TSource))

returnValue = source.Timestamp()
```

```csharp
public static IQbservable<Timestamped<TSource>> Timestamp<TSource>(
    this IQbservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<Timestamped<TSource>>^ Timestamp(
    IQbservable<TSource>^ source
)
```

```fsharp
static member Timestamp : 
        source:IQbservable<'TSource> -> IQbservable<Timestamped<'TSource>> 
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
  The source sequence to timestamp values for.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Timestamped](Timestamped\Timestamped(T).md)\<TSource\>\>  
A queryable observable sequence with timestamp information on values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Timestamp Overload](Timestamp\Qbservable.Timestamp.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
