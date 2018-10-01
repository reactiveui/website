# TestCrashTaskPool.ad\_UnhandledException Method

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Sub ad_UnhandledException ( _
    sender As Object, _
    e As UnhandledExceptionEventArgs _
)
```

```vb
'Usage
Dim sender As Object
Dim e As UnhandledExceptionEventArgs

TestCrashTaskPool.ad_UnhandledException(sender, _
    e)
```

```csharp
public static void ad_UnhandledException(
    Object sender,
    UnhandledExceptionEventArgs e
)
```

```c++
public:
static void ad_UnhandledException(
    Object^ sender, 
    UnhandledExceptionEventArgs^ e
)
```

```fsharp
static member ad_UnhandledException : 
        sender:Object * 
        e:UnhandledExceptionEventArgs -> unit 
```

```jscript
public static function ad_UnhandledException(
    sender : Object, 
    e : UnhandledExceptionEventArgs
)
```

#### Parameters

- sender  
  Type: [System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)

- e  
  Type: [System.UnhandledExceptionEventArgs](https://msdn.microsoft.com/en-us/library/716y7t7e)

## See Also

#### Reference

[TestCrashTaskPool Class](TestCrashTaskPool\TestCrashTaskPool.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)





