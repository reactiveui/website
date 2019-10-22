title: Observable.Start<TSource>(Func<TSource>)
---
# Observable.Start\<TSource\> Method (Func\<TSource\>)

Invokes the function asynchronously.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Start(Of TSource) ( _
    function As Func(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim function As Func(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = Observable.Start(function)
```

```csharp
public static IObservable<TSource> Start<TSource>(
    Func<TSource> function
)
```

```c++
public:
generic<typename TSource>
static IObservable<TSource>^ Start(
    Func<TSource>^ function
)
```

```fsharp
static member Start : 
        function:Func<'TSource> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- function  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<TSource\>  
  The function used to synchronization.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The function asynchronously.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Start Overload](Start\Observable.Start.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Start\<TSource\> Method (Func\<TSource\>, IScheduler)

Invokes the function asynchronously.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Start(Of TSource) ( _
    function As Func(Of TSource), _
    scheduler As IScheduler _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim function As Func(Of TSource)
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TSource)

returnValue = Observable.Start(function, _
    scheduler)
```

```csharp
public static IObservable<TSource> Start<TSource>(
    Func<TSource> function,
    IScheduler scheduler
)
```

```c++
public:
generic<typename TSource>
static IObservable<TSource>^ Start(
    Func<TSource>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member Start : 
        function:Func<'TSource> * 
        scheduler:IScheduler -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- function  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<TSource\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The function asynchronously.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Start Overload](Start\Observable.Start.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
