title: Observable.Empty<TResult>()
---
# Observable.Empty\<TResult\> Method

Returns an empty observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Empty(Of TResult) As IObservable(Of TResult)
```

```vb
'Usage
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Empty()
```

```csharp
public static IObservable<TResult> Empty<TResult>()
```

```c++
public:
generic<typename TResult>
static IObservable<TResult>^ Empty()
```

```fsharp
static member Empty : unit -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
The observable sequence with no elements.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Empty Overload](Empty/Observable.Empty)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)







# Observable.Empty\<TResult\> Method (IScheduler)

Returns an empty observable sequence with the specified scheduler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Empty(Of TResult) ( _
    scheduler As IScheduler _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Empty(scheduler)
```

```csharp
public static IObservable<TResult> Empty<TResult>(
    IScheduler scheduler
)
```

```c++
public:
generic<typename TResult>
static IObservable<TResult>^ Empty(
    IScheduler^ scheduler
)
```

```fsharp
static member Empty : 
        scheduler:IScheduler -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to send the termination call.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
The observable sequence with no elements.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Empty Overload](Empty/Observable.Empty)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)







