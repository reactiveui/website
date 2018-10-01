# Extensions.EnsureTrampoline Method

**Namespace:**  [ReactiveTests](ReactiveTests\ReactiveTests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Sub EnsureTrampoline ( _
    scheduler As CurrentThreadScheduler, _
    action As Action _
)
```

```vb
'Usage
Dim scheduler As CurrentThreadScheduler
Dim action As Action

scheduler.EnsureTrampoline(action)
```

```csharp
public static void EnsureTrampoline(
    this CurrentThreadScheduler scheduler,
    Action action
)
```

```c++
[ExtensionAttribute]
public:
static void EnsureTrampoline(
    CurrentThreadScheduler^ scheduler, 
    Action^ action
)
```

```fsharp
static member EnsureTrampoline : 
        scheduler:CurrentThreadScheduler * 
        action:Action -> unit 
```

```jscript
public static function EnsureTrampoline(
    scheduler : CurrentThreadScheduler, 
    action : Action
)
```

#### Parameters

- scheduler  
  Type: [System.Reactive.Concurrency.CurrentThreadScheduler](CurrentThreadScheduler\CurrentThreadScheduler.md)

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb534741)

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [CurrentThreadScheduler](CurrentThreadScheduler\CurrentThreadScheduler.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Extensions Class](Extensions\Extensions.md)

[ReactiveTests Namespace](ReactiveTests\ReactiveTests.md)






