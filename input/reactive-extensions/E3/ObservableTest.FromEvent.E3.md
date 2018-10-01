# ObservableTest.FromEvent.E3 Event

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Event E3 As Action(Of Object, ObservableTest..::..FromEvent..::..TestEventArgs)
```

```vb
'Usage
Dim instance As ObservableTest..::..FromEvent
Dim handler As Action(Of Object, ObservableTest..::..FromEvent..::..TestEventArgs)

AddHandler instance.E3, handler
```

```csharp
public event Action<Object, ObservableTest..::..FromEvent..::..TestEventArgs> E3
```

```c++
public:
 event Action<Object^, ObservableTest..::..FromEvent..::..TestEventArgs^>^ E3 {
    void add (Action<Object^, ObservableTest..::..FromEvent..::..TestEventArgs^>^ value);
    void remove (Action<Object^, ObservableTest..::..FromEvent..::..TestEventArgs^>^ value);
}
```

```fsharp
member E3 : IEvent<Action<Object, ObservableTest..::..FromEvent..::..TestEventArgs>,
    ObservableTest..::..FromEvent..::..TestEventArgs>
```

```jscript
JScript supports the use of events, but not the declaration of new ones.
```

## See Also

#### Reference

[ObservableTest.FromEvent Class](ObservableTest.FromEvent\ObservableTest.FromEvent.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)




