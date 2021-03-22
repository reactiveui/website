title: TestScheduler.Start<T>(Func<IObservable<T>>)
---
# TestScheduler.Start\<T\> Method (Func\<IObservable\<T\>\>)

Starts the test scheduler.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Function Start(Of T) ( _
    create As Func(Of IObservable(Of T)) _
) As ITestableObserver(Of T)
```

```vb
'Usage
Dim instance As TestScheduler
Dim create As Func(Of IObservable(Of T))
Dim returnValue As ITestableObserver(Of T)

returnValue = instance.Start(create)
```

```csharp
public ITestableObserver<T> Start<T>(
    Func<IObservable<T>> create
)
```

```c++
public:
generic<typename T>
ITestableObserver<T>^ Start(
    Func<IObservable<T>^>^ create
)
```

```fsharp
member Start : 
        create:Func<IObservable<'T>> -> ITestableObserver<'T> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

#### Parameters

- create  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<T\>\>  
  The factory method to create an observable sequence.

#### Return Value

Type: [Microsoft.Reactive.Testing.ITestableObserver](ITestableObserver/ITestableObserver(T))\<T\>  
The testable observer with recordings of notifications that occurred.

## See Also

#### Reference

[TestScheduler Class](TestScheduler/TestScheduler)

[Start Overload](Start/TestScheduler.Start)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

# TestScheduler.Start\<T\> Method (Func\<IObservable\<T\>\>, Int64)

Starts the test scheduler.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Function Start(Of T) ( _
    create As Func(Of IObservable(Of T)), _
    disposed As Long _
) As ITestableObserver(Of T)
```

```vb
'Usage
Dim instance As TestScheduler
Dim create As Func(Of IObservable(Of T))
Dim disposed As Long
Dim returnValue As ITestableObserver(Of T)

returnValue = instance.Start(create, disposed)
```

```csharp
public ITestableObserver<T> Start<T>(
    Func<IObservable<T>> create,
    long disposed
)
```

```c++
public:
generic<typename T>
ITestableObserver<T>^ Start(
    Func<IObservable<T>^>^ create, 
    long long disposed
)
```

```fsharp
member Start : 
        create:Func<IObservable<'T>> * 
        disposed:int64 -> ITestableObserver<'T> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

#### Parameters

- create  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<T\>\>  
  The factory method to create an observable sequence.

- disposed  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  The virtual time at which to dispose the subscription.

#### Return Value

Type: [Microsoft.Reactive.Testing.ITestableObserver](ITestableObserver/ITestableObserver(T))\<T\>  
The testable observer with recordings of notifications that occurred.

## See Also

#### Reference

[TestScheduler Class](TestScheduler/TestScheduler)

[Start Overload](Start/TestScheduler.Start)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

# TestScheduler.Start\<T\> Method (Func\<IObservable\<T\>\>, Int64, Int64, Int64)

Starts the test scheduler.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Function Start(Of T) ( _
    create As Func(Of IObservable(Of T)), _
    created As Long, _
    subscribed As Long, _
    disposed As Long _
) As ITestableObserver(Of T)
```

```vb
'Usage
Dim instance As TestScheduler
Dim create As Func(Of IObservable(Of T))
Dim created As Long
Dim subscribed As Long
Dim disposed As Long
Dim returnValue As ITestableObserver(Of T)

returnValue = instance.Start(create, created, _
    subscribed, disposed)
```

```csharp
public ITestableObserver<T> Start<T>(
    Func<IObservable<T>> create,
    long created,
    long subscribed,
    long disposed
)
```

```c++
public:
generic<typename T>
ITestableObserver<T>^ Start(
    Func<IObservable<T>^>^ create, 
    long long created, 
    long long subscribed, 
    long long disposed
)
```

```fsharp
member Start : 
        create:Func<IObservable<'T>> * 
        created:int64 * 
        subscribed:int64 * 
        disposed:int64 -> ITestableObserver<'T> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

#### Parameters

- create  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<T\>\>  
  Factory method to create an observable sequence.

- created  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  Virtual time at which to invoke the factory to create an observable sequence.

- subscribed  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  The virtual time at which to subscribe to the created observable sequence.

- disposed  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  The virtual time at which to dispose the subscription.

#### Return Value

Type: [Microsoft.Reactive.Testing.ITestableObserver](ITestableObserver/ITestableObserver(T))\<T\>  
The testable observer with recordings of notifications that occurred.

## See Also

#### Reference

[TestScheduler Class](TestScheduler/TestScheduler)

[Start Overload](Start/TestScheduler.Start)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)
