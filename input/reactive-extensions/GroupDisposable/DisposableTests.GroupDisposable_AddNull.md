# DisposableTests.GroupDisposable\_AddNull Method

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
<TestMethodAttribute> _
<ExpectedExceptionAttribute(GetType(ArgumentNullException))> _
Public Sub GroupDisposable_AddNull
```

```vb
'Usage
Dim instance As DisposableTests

instance.GroupDisposable_AddNull()
```

```csharp
[TestMethodAttribute]
[ExpectedExceptionAttribute(typeof(ArgumentNullException))]
public void GroupDisposable_AddNull()
```

```c++
[TestMethodAttribute]
[ExpectedExceptionAttribute(typeof(ArgumentNullException))]
public:
void GroupDisposable_AddNull()
```

```fsharp
[<TestMethodAttribute>]
[<ExpectedExceptionAttribute(typeof(ArgumentNullException))>]
member GroupDisposable_AddNull : unit -> unit 
```

```jscript
public function GroupDisposable_AddNull()
```

## See Also

#### Reference

[DisposableTests Class](DisposableTests\DisposableTests.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)




