# TestBase.RunAsync Method

**Namespace:**  [ReactiveTests](ReactiveTests\ReactiveTests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub RunAsync ( _
    a As Action(Of Waiter) _
)
```

```vb
'Usage
Dim instance As TestBase
Dim a As Action(Of Waiter)

instance.RunAsync(a)
```

```csharp
public void RunAsync(
    Action<Waiter> a
)
```

```c++
public:
void RunAsync(
    Action<Waiter^>^ a
)
```

```fsharp
member RunAsync : 
        a:Action<Waiter> -> unit 
```

```jscript
public function RunAsync(
    a : Action<Waiter>
)
```

#### Parameters

- a  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Waiter](Waiter\Waiter.md)\>

## See Also

#### Reference

[TestBase Class](TestBase\TestBase.md)

[ReactiveTests Namespace](ReactiveTests\ReactiveTests.md)

[]: 
[]: 
[]: 
[]: 
[]: 