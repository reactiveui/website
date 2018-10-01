# ObservableTest.FromEvent\_VarianceCheck.E3 Event

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Event E3 As Action(Of String, EventArgs)
```

```vb
'Usage
Dim instance As ObservableTest..::..FromEvent_VarianceCheck
Dim handler As Action(Of String, EventArgs)

AddHandler instance.E3, handler
```

```csharp
public event Action<string, EventArgs> E3
```

```c++
public:
 event Action<String^, EventArgs^>^ E3 {
    void add (Action<String^, EventArgs^>^ value);
    void remove (Action<String^, EventArgs^>^ value);
}
```

```fsharp
member E3 : IEvent<Action<string, EventArgs>,
    EventArgs>
```

```jscript
JScript supports the use of events, but not the declaration of new ones.
```

## See Also

#### Reference

[ObservableTest.FromEvent\_VarianceCheck Class](ObservableTest.FromEvent\ObservableTest.FromEvent_VarianceCheck.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)




