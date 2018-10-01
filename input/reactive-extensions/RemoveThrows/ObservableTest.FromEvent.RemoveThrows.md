# ObservableTest.FromEvent.RemoveThrows Event

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Event RemoveThrows As ObservableTest..::..FromEvent..::..TestEventHandler
```

```vb
'Usage
Dim instance As ObservableTest..::..FromEvent
Dim handler As ObservableTest..::..FromEvent..::..TestEventHandler

AddHandler instance.RemoveThrows, handler
```

```csharp
public event ObservableTest..::..FromEvent..::..TestEventHandler RemoveThrows
```

```c++
public:
 event ObservableTest..::..FromEvent..::..TestEventHandler^ RemoveThrows {
    void add (ObservableTest..::..FromEvent..::..TestEventHandler^ value);
    void remove (ObservableTest..::..FromEvent..::..TestEventHandler^ value);
}
```

```fsharp
member RemoveThrows : IEvent<ObservableTest..::..FromEvent..::..TestEventHandler,
    ObservableTest..::..FromEvent..::..TestEventArgs>
```

```jscript
JScript supports the use of events, but not the declaration of new ones.
```

## See Also

#### Reference

[ObservableTest.FromEvent Class](ObservableTest.FromEvent\ObservableTest.FromEvent.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)

[]: 
[]: 
[]: 
[]: 