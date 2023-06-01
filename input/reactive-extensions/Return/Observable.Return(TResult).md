title: Observable.Return<TResult>(TResult, IScheduler)
---
# Observable.Return\<TResult\> Method (TResult, IScheduler)

Returns an observable sequence that contains a single value with a specified value and scheduler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Return(Of TResult) ( _
    value As TResult, _
    scheduler As IScheduler _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim value As TResult
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Return(value, _
    scheduler)
```

```csharp
public static IObservable<TResult> Return<TResult>(
    TResult value,
    IScheduler scheduler
)
```

```c++
public:
generic<typename TResult>
static IObservable<TResult>^ Return(
    TResult value, 
    IScheduler^ scheduler
)
```

```fsharp
static member Return : 
        value:'TResult * 
        scheduler:IScheduler -> IObservable<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- value  
  Type: TResult  
  The single element in the resulting observable sequence.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to send the single element on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
Observable sequence containing the single specified element.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Return Overload](Return/Observable.Return)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Return\<TResult\> Method (TResult)

Returns an observable sequence that contains a single element with a specified value.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Return(Of TResult) ( _
    value As TResult _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim value As TResult
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Return(value)
```

```csharp
public static IObservable<TResult> Return<TResult>(
    TResult value
)
```

```c++
public:
generic<typename TResult>
static IObservable<TResult>^ Return(
    TResult value
)
```

```fsharp
static member Return : 
        value:'TResult -> IObservable<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- value  
  Type: TResult  
  The single element in the resulting observable sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
Observable sequence containing the single specified element.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Return Overload](Return/Observable.Return)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
