# QbservableTest.GetTypeName Method

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function GetTypeName ( _
    t As Type, _
    correct As Boolean _
) As String
```

```vb
'Usage
Dim t As Type
Dim correct As Boolean
Dim returnValue As String

returnValue = QbservableTest.GetTypeName(t, _
    correct)
```

```csharp
public static string GetTypeName(
    Type t,
    bool correct
)
```

```c++
public:
static String^ GetTypeName(
    Type^ t, 
    bool correct
)
```

```fsharp
static member GetTypeName : 
        t:Type * 
        correct:bool -> string 
```

```jscript
public static function GetTypeName(
    t : Type, 
    correct : boolean
) : String
```

#### Parameters

- t  
  Type: [System.Type](https://msdn.microsoft.com/en-us/library/42892f65)

- correct  
  Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)

#### Return Value

Type: [System.String](https://msdn.microsoft.com/en-us/library/s1wwdcbf)

## See Also

#### Reference

[QbservableTest Class](QbservableTest\QbservableTest.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)






