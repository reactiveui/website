# ObservableTest.FromEvent.E4 Event

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Event E4 As Action(Of Integer)
```

```vb
'Usage
Dim instance As ObservableTest..::..FromEvent
Dim handler As Action(Of Integer)

AddHandler instance.E4, handler
```

```csharp
public event Action<int> E4
```

```c++
public:
 event Action<int>^ E4 {
    void add (Action<int>^ value);
    void remove (Action<int>^ value);
}
```

```fsharp
member E4 : IEvent<Action<int>,
    EventArgs>
```

```jscript
JScript supports the use of events, but not the declaration of new ones.
```

## See Also

#### Reference

[ObservableTest.FromEvent Class](ObservableTest.FromEvent\ObservableTest.FromEvent.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)




