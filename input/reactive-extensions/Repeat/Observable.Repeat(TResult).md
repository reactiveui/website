title: Observable.Repeat<TResult>(TResult, Int32, IScheduler)
---
# Observable.Repeat\<TResult\> Method (TResult, Int32, IScheduler)

Generates an observable sequence that repeats the given element of the specified number of times.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Repeat(Of TResult) ( _
    value As TResult, _
    repeatCount As Integer, _
    scheduler As IScheduler _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim value As TResult
Dim repeatCount As Integer
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Repeat(value, _
    repeatCount, scheduler)
```

```csharp
public static IObservable<TResult> Repeat<TResult>(
    TResult value,
    int repeatCount,
    IScheduler scheduler
)
```

```c++
public:
generic<typename TResult>
static IObservable<TResult>^ Repeat(
    TResult value, 
    int repeatCount, 
    IScheduler^ scheduler
)
```

```fsharp
static member Repeat : 
        value:'TResult * 
        repeatCount:int * 
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
  The element to repeat.

- repeatCount  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The number of times to repeat the element.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to run the producer loop on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence that repeats the given element of the specified number of times.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Repeat Overload](Repeat/Observable.Repeat)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Repeat\<TResult\> Method (TResult, Int32)

Generates an observable sequence that repeats the given element the specified number of times.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Repeat(Of TResult) ( _
    value As TResult, _
    repeatCount As Integer _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim value As TResult
Dim repeatCount As Integer
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Repeat(value, _
    repeatCount)
```

```csharp
public static IObservable<TResult> Repeat<TResult>(
    TResult value,
    int repeatCount
)
```

```c++
public:
generic<typename TResult>
static IObservable<TResult>^ Repeat(
    TResult value, 
    int repeatCount
)
```

```fsharp
static member Repeat : 
        value:'TResult * 
        repeatCount:int -> IObservable<'TResult> 
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
  The element to repeat.

- repeatCount  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The number of times to repeat the element.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence that repeats the given element the specified number of times.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Repeat Overload](Repeat/Observable.Repeat)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Repeat\<TResult\> Method (TResult)

Generates an observable sequence that repeats the given element infinitely.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Repeat(Of TResult) ( _
    value As TResult _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim value As TResult
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Repeat(value)
```

```csharp
public static IObservable<TResult> Repeat<TResult>(
    TResult value
)
```

```c++
public:
generic<typename TResult>
static IObservable<TResult>^ Repeat(
    TResult value
)
```

```fsharp
static member Repeat : 
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
  The element to repeat.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence that repeats the given element infinitely.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Repeat Overload](Repeat/Observable.Repeat)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Repeat\<TResult\> Method (TResult, IScheduler)

Generates an observable sequence that repeats the given element infinitely.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Repeat(Of TResult) ( _
    value As TResult, _
    scheduler As IScheduler _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim value As TResult
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Repeat(value, _
    scheduler)
```

```csharp
public static IObservable<TResult> Repeat<TResult>(
    TResult value,
    IScheduler scheduler
)
```

```c++
public:
generic<typename TResult>
static IObservable<TResult>^ Repeat(
    TResult value, 
    IScheduler^ scheduler
)
```

```fsharp
static member Repeat : 
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
  The element to repeat.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to run the producer loop on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
The observable sequence that repeats the given element infinitely.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Repeat Overload](Repeat/Observable.Repeat)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
