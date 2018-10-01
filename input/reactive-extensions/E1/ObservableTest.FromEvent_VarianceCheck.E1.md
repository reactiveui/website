# ObservableTest.FromEvent\_VarianceCheck.E1 Event

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Event E1 As EventHandler(Of EventArgs)
```

```vb
'Usage
Dim instance As ObservableTest..::..FromEvent_VarianceCheck
Dim handler As EventHandler(Of EventArgs)

AddHandler instance.E1, handler
```

```csharp
public event EventHandler<EventArgs> E1
```

```c++
public:
 event EventHandler<EventArgs^>^ E1 {
    void add (EventHandler<EventArgs^>^ value);
    void remove (EventHandler<EventArgs^>^ value);
}
```

```fsharp
member E1 : IEvent<EventHandler<EventArgs>,
    EventArgs>
```

```jscript
JScript supports the use of events, but not the declaration of new ones.
```

## See Also

#### Reference

[ObservableTest.FromEvent\_VarianceCheck Class](ObservableTest.FromEvent\ObservableTest.FromEvent_VarianceCheck.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)




