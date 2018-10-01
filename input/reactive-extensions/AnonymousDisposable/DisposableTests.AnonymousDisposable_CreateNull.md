# DisposableTests.AnonymousDisposable\_CreateNull Method

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExpectedExceptionAttribute(GetType(ArgumentNullException))> _
<TestMethodAttribute> _
Public Sub AnonymousDisposable_CreateNull
```

```vb
'Usage
Dim instance As DisposableTests

instance.AnonymousDisposable_CreateNull()
```

```csharp
[ExpectedExceptionAttribute(typeof(ArgumentNullException))]
[TestMethodAttribute]
public void AnonymousDisposable_CreateNull()
```

```c++
[ExpectedExceptionAttribute(typeof(ArgumentNullException))]
[TestMethodAttribute]
public:
void AnonymousDisposable_CreateNull()
```

```fsharp
[<ExpectedExceptionAttribute(typeof(ArgumentNullException))>]
[<TestMethodAttribute>]
member AnonymousDisposable_CreateNull : unit -> unit 
```

```jscript
public function AnonymousDisposable_CreateNull()
```

## See Also

#### Reference

[DisposableTests Class](DisposableTests\DisposableTests.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)




