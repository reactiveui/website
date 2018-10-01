# ObservableTest.FromEvent.E1 Event

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Event E1 As ObservableTest..::..FromEvent..::..TestEventHandler
```

```vb
'Usage
Dim instance As ObservableTest..::..FromEvent
Dim handler As ObservableTest..::..FromEvent..::..TestEventHandler

AddHandler instance.E1, handler
```

```csharp
public event ObservableTest..::..FromEvent..::..TestEventHandler E1
```

```c++
public:
 event ObservableTest..::..FromEvent..::..TestEventHandler^ E1 {
    void add (ObservableTest..::..FromEvent..::..TestEventHandler^ value);
    void remove (ObservableTest..::..FromEvent..::..TestEventHandler^ value);
}
```

```fsharp
member E1 : IEvent<ObservableTest..::..FromEvent..::..TestEventHandler,
    ObservableTest..::..FromEvent..::..TestEventArgs>
```

```jscript
JScript supports the use of events, but not the declaration of new ones.
```

## See Also

#### Reference

[ObservableTest.FromEvent Class](ObservableTest.FromEvent\ObservableTest.FromEvent.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)




