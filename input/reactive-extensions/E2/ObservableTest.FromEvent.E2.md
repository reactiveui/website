# ObservableTest.FromEvent.E2 Event

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Event E2 As EventHandler(Of ObservableTest..::..FromEvent..::..TestEventArgs)
```

```vb
'Usage
Dim instance As ObservableTest..::..FromEvent
Dim handler As EventHandler(Of ObservableTest..::..FromEvent..::..TestEventArgs)

AddHandler instance.E2, handler
```

```csharp
public event EventHandler<ObservableTest..::..FromEvent..::..TestEventArgs> E2
```

```c++
public:
 event EventHandler<ObservableTest..::..FromEvent..::..TestEventArgs^>^ E2 {
    void add (EventHandler<ObservableTest..::..FromEvent..::..TestEventArgs^>^ value);
    void remove (EventHandler<ObservableTest..::..FromEvent..::..TestEventArgs^>^ value);
}
```

```fsharp
member E2 : IEvent<EventHandler<ObservableTest..::..FromEvent..::..TestEventArgs>,
    ObservableTest..::..FromEvent..::..TestEventArgs>
```

```jscript
JScript supports the use of events, but not the declaration of new ones.
```

## See Also

#### Reference

[ObservableTest.FromEvent Class](ObservableTest.FromEvent\ObservableTest.FromEvent.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)




