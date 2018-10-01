# ObservableTest.FromEvent.E6 Event

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Event E6 As EventHandler(Of ObservableTest..::..FromEvent..::..TestEventArgs)
```

```vb
'Usage
Dim handler As EventHandler(Of ObservableTest..::..FromEvent..::..TestEventArgs)

AddHandler ObservableTest..::..FromEvent.E6, handler
```

```csharp
public static event EventHandler<ObservableTest..::..FromEvent..::..TestEventArgs> E6
```

```c++
public:
static  event EventHandler<ObservableTest..::..FromEvent..::..TestEventArgs^>^ E6 {
    void add (EventHandler<ObservableTest..::..FromEvent..::..TestEventArgs^>^ value);
    void remove (EventHandler<ObservableTest..::..FromEvent..::..TestEventArgs^>^ value);
}
```

```fsharp
member E6 : IEvent<EventHandler<ObservableTest..::..FromEvent..::..TestEventArgs>,
    ObservableTest..::..FromEvent..::..TestEventArgs>
```

```jscript
JScript supports the use of events, but not the declaration of new ones.
```

## See Also

#### Reference

[ObservableTest.FromEvent Class](ObservableTest.FromEvent\ObservableTest.FromEvent.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)




