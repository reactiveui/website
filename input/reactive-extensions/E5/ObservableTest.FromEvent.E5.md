# ObservableTest.FromEvent.E5 Event

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Event E5 As EventHandler
```

```vb
'Usage
Dim instance As ObservableTest..::..FromEvent
Dim handler As EventHandler

AddHandler instance.E5, handler
```

```csharp
public event EventHandler E5
```

```c++
public:
 event EventHandler^ E5 {
    void add (EventHandler^ value);
    void remove (EventHandler^ value);
}
```

```fsharp
member E5 : IEvent<EventHandler,
    EventArgs>
```

```jscript
JScript supports the use of events, but not the declaration of new ones.
```

## See Also

#### Reference

[ObservableTest.FromEvent Class](ObservableTest.FromEvent\ObservableTest.FromEvent.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)




