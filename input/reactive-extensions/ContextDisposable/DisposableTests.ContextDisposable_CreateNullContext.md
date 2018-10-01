# DisposableTests.ContextDisposable\_CreateNullContext Method

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
<TestMethodAttribute> _
<ExpectedExceptionAttribute(GetType(ArgumentNullException))> _
Public Sub ContextDisposable_CreateNullContext
```

```vb
'Usage
Dim instance As DisposableTests

instance.ContextDisposable_CreateNullContext()
```

```csharp
[TestMethodAttribute]
[ExpectedExceptionAttribute(typeof(ArgumentNullException))]
public void ContextDisposable_CreateNullContext()
```

```c++
[TestMethodAttribute]
[ExpectedExceptionAttribute(typeof(ArgumentNullException))]
public:
void ContextDisposable_CreateNullContext()
```

```fsharp
[<TestMethodAttribute>]
[<ExpectedExceptionAttribute(typeof(ArgumentNullException))>]
member ContextDisposable_CreateNullContext : unit -> unit 
```

```jscript
public function ContextDisposable_CreateNullContext()
```

## See Also

#### Reference

[DisposableTests Class](DisposableTests\DisposableTests.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)




