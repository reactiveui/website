# ObservableTest.FromEvent.TestEventHandler Delegate

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Delegate Sub TestEventHandler ( _
    sender As Object, _
    eventArgs As ObservableTest..::..FromEvent..::..TestEventArgs _
)
```

```vb
'Usage
Dim instance As New TestEventHandler(AddressOf HandlerMethod)
```

```csharp
public delegate void TestEventHandler(
    Object sender,
    ObservableTest..::..FromEvent..::..TestEventArgs eventArgs
)
```

```c++
public delegate void TestEventHandler(
    Object^ sender, 
    ObservableTest..::..FromEvent..::..TestEventArgs^ eventArgs
)
```

```fsharp
type TestEventHandler = 
    delegate of 
        sender:Object * 
        eventArgs:ObservableTest..::..FromEvent..::..TestEventArgs -> unit
```

```jscript
JScript supports the use of delegates, but not the declaration of new ones.
```

#### Parameters

- sender  
  Type: [System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)

- eventArgs  
  Type: [ReactiveTests.Tests.ObservableTest.FromEvent.TestEventArgs](ObservableTest.FromEvent.TestEventArgs\ObservableTest.FromEvent.TestEventArgs.md)

## See Also

#### Reference

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)





