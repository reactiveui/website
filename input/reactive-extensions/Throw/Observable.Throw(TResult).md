title: Observable.Throw<TResult>(Exception)
---
# Observable.Throw\<TResult\> Method (Exception)

Returns an observable sequence that terminates with an exception.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Throw(Of TResult) ( _
    exception As Exception _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim exception As Exception
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Throw(exception)
```

```csharp
public static IObservable<TResult> Throw<TResult>(
    Exception exception
)
```

```c++
public:
generic<typename TResult>
static IObservable<TResult>^ Throw(
    Exception^ exception
)
```

```fsharp
static member Throw : 
        exception:Exception -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- exception  
  Type: [System.Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)  
  The exception object used for the sequence’s termination.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
Observable sequence that terminates exceptionally with the specified exception object.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Throw Overload](Throw\Observable.Throw.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Throw\<TResult\> Method (Exception, IScheduler)

Returns an observable sequence that terminates with an exception with the specified scheduler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Throw(Of TResult) ( _
    exception As Exception, _
    scheduler As IScheduler _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim exception As Exception
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Throw(exception, _
    scheduler)
```

```csharp
public static IObservable<TResult> Throw<TResult>(
    Exception exception,
    IScheduler scheduler
)
```

```c++
public:
generic<typename TResult>
static IObservable<TResult>^ Throw(
    Exception^ exception, 
    IScheduler^ scheduler
)
```

```fsharp
static member Throw : 
        exception:Exception * 
        scheduler:IScheduler -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- exception  
  Type: [System.Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)  
  The exception object used for the sequence’s termination.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to send the exceptional termination call on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
Observable sequence that terminates exceptionally with the specified exception object.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Throw Overload](Throw\Observable.Throw.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
