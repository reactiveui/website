# QbservableTest.GetSignature Method

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function GetSignature ( _
    m As MethodInfo, _
    correct As Boolean _
) As String
```

```vb
'Usage
Dim m As MethodInfo
Dim correct As Boolean
Dim returnValue As String

returnValue = QbservableTest.GetSignature(m, _
    correct)
```

```csharp
public static string GetSignature(
    MethodInfo m,
    bool correct
)
```

```c++
public:
static String^ GetSignature(
    MethodInfo^ m, 
    bool correct
)
```

```fsharp
static member GetSignature : 
        m:MethodInfo * 
        correct:bool -> string 
```

```jscript
public static function GetSignature(
    m : MethodInfo, 
    correct : boolean
) : String
```

#### Parameters

- m  
  Type: [System.Reflection.MethodInfo](https://msdn.microsoft.com/en-us/library/1wa35kh5)

- correct  
  Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)

#### Return Value

Type: [System.String](https://msdn.microsoft.com/en-us/library/s1wwdcbf)

## See Also

#### Reference

[QbservableTest Class](QbservableTest\QbservableTest.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)






