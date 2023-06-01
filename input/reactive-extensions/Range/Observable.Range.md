title: Observable.Range(Int32, Int32)
---
# Observable.Range Method (Int32, Int32)

Generates an observable sequence of integral numbers within a specified range.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Range ( _
    start As Integer, _
    count As Integer _
) As IObservable(Of Integer)
```

```vb
'Usage
Dim start As Integer
Dim count As Integer
Dim returnValue As IObservable(Of Integer)

returnValue = Observable.Range(start, count)
```

```csharp
public static IObservable<int> Range(
    int start,
    int count
)
```

```c++
public:
static IObservable<int>^ Range(
    int start, 
    int count
)
```

```fsharp
static member Range : 
        start:int * 
        count:int -> IObservable<int> 
```

```javascript
public static function Range(
    start : int, 
    count : int
) : IObservable<int>
```

#### Parameters

- start  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The value of the first integer in the sequence.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The number of sequential integers to generate.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
An observable sequence that contains a range of sequential integral numbers.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Range Overload](Range/Observable.Range)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Range Method

Include Protected Members  
Include Inherited Members

Generates an observable sequence of integral numbers within a specified range.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Range(Int32, Int32)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.range(system.int32%2csystem.int32)(v=VS.103))Generates an observable sequence of integral numbers within a specified range.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Range(Int32, Int32, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.range(system.int32%2csystem.int32%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Generates an observable sequence of integral numbers within a specified range.Top

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Range Method (Int32, Int32, IScheduler)

Generates an observable sequence of integral numbers within a specified range.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Range ( _
    start As Integer, _
    count As Integer, _
    scheduler As IScheduler _
) As IObservable(Of Integer)
```

```vb
'Usage
Dim start As Integer
Dim count As Integer
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of Integer)

returnValue = Observable.Range(start, count, _
    scheduler)
```

```csharp
public static IObservable<int> Range(
    int start,
    int count,
    IScheduler scheduler
)
```

```c++
public:
static IObservable<int>^ Range(
    int start, 
    int count, 
    IScheduler^ scheduler
)
```

```fsharp
static member Range : 
        start:int * 
        count:int * 
        scheduler:IScheduler -> IObservable<int> 
```

```javascript
public static function Range(
    start : int, 
    count : int, 
    scheduler : IScheduler
) : IObservable<int>
```

#### Parameters

- start  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The value of the first integer in the sequence.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The number of sequential integers to generate.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to run the generator loop on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
An observable sequence that contains a range of sequential integral numbers.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Range Overload](Range/Observable.Range)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
