title: TestScheduler.CreateObserver<T>()
---
# TestScheduler.CreateObserver\<T\> Method

Creates a testable observer.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Function CreateObserver(Of T) As ITestableObserver(Of T)
```

```vb
'Usage
Dim instance As TestScheduler
Dim returnValue As ITestableObserver(Of T)

returnValue = instance.CreateObserver()
```

```csharp
public ITestableObserver<T> CreateObserver<T>()
```

```c++
public:
generic<typename T>
ITestableObserver<T>^ CreateObserver()
```

```fsharp
member CreateObserver : unit -> ITestableObserver<'T> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

#### Return Value

Type: [Microsoft.Reactive.Testing.ITestableObserver](ITestableObserver/ITestableObserver(T))\<T\>  
New testable observer object.

## See Also

#### Reference

[TestScheduler Class](TestScheduler/TestScheduler)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)






