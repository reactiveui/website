# ObservableTest.FromEvent\_VarianceCheck.E2 Event

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Event E2 As EventHandler(Of CancelEventArgs)
```

```vb
'Usage
Dim instance As ObservableTest..::..FromEvent_VarianceCheck
Dim handler As EventHandler(Of CancelEventArgs)

AddHandler instance.E2, handler
```

```csharp
public event EventHandler<CancelEventArgs> E2
```

```c++
public:
 event EventHandler<CancelEventArgs^>^ E2 {
    void add (EventHandler<CancelEventArgs^>^ value);
    void remove (EventHandler<CancelEventArgs^>^ value);
}
```

```fsharp
member E2 : IEvent<EventHandler<CancelEventArgs>,
    CancelEventArgs>
```

```jscript
JScript supports the use of events, but not the declaration of new ones.
```

## See Also

#### Reference

[ObservableTest.FromEvent\_VarianceCheck Class](ObservableTest.FromEvent\ObservableTest.FromEvent_VarianceCheck.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)




