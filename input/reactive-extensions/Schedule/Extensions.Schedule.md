title: Extensions.Schedule()
---
# Extensions.Schedule Method

**Namespace:**  [ReactiveTests](ReactiveTests/ReactiveTests)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Schedule ( _
    scheduler As TestScheduler, _
    action As Action, _
    time As Long _
) As IDisposable
```

```vb
'Usage
Dim scheduler As TestScheduler
Dim action As Action
Dim time As Long
Dim returnValue As IDisposable

returnValue = scheduler.Schedule(action, _
    time)
```

```csharp
public static IDisposable Schedule(
    this TestScheduler scheduler,
    Action action,
    long time
)
```

```c++
[ExtensionAttribute]
public:
static IDisposable^ Schedule(
    TestScheduler^ scheduler, 
    Action^ action, 
    long long time
)
```

```fsharp
static member Schedule : 
        scheduler:TestScheduler * 
        action:Action * 
        time:int64 -> IDisposable 
```

```javascript
public static function Schedule(
    scheduler : TestScheduler, 
    action : Action, 
    time : long
) : IDisposable
```

#### Parameters

- scheduler  
  Type: [Microsoft.Reactive.Testing.TestScheduler](TestScheduler/TestScheduler)

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb534741)

- time  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [TestScheduler](TestScheduler/TestScheduler). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Extensions Class](Extensions/Extensions)

[ReactiveTests Namespace](ReactiveTests/ReactiveTests)
