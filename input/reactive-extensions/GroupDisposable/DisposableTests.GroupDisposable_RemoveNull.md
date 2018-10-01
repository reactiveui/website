# DisposableTests.GroupDisposable\_RemoveNull Method

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExpectedExceptionAttribute(GetType(ArgumentNullException))> _
<TestMethodAttribute> _
Public Sub GroupDisposable_RemoveNull
```

```vb
'Usage
Dim instance As DisposableTests

instance.GroupDisposable_RemoveNull()
```

```csharp
[ExpectedExceptionAttribute(typeof(ArgumentNullException))]
[TestMethodAttribute]
public void GroupDisposable_RemoveNull()
```

```c++
[ExpectedExceptionAttribute(typeof(ArgumentNullException))]
[TestMethodAttribute]
public:
void GroupDisposable_RemoveNull()
```

```fsharp
[<ExpectedExceptionAttribute(typeof(ArgumentNullException))>]
[<TestMethodAttribute>]
member GroupDisposable_RemoveNull : unit -> unit 
```

```jscript
public function GroupDisposable_RemoveNull()
```

## See Also

#### Reference

[DisposableTests Class](DisposableTests\DisposableTests.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)




