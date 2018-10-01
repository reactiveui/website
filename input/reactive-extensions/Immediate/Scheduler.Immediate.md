# Scheduler.Immediate Property

Gets the scheduler that schedules work immediately on the current thread.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared ReadOnly Property Immediate As ImmediateScheduler
    Get
```

```vb
'Usage
Dim value As ImmediateScheduler

value = Scheduler.Immediate
```

```csharp
public static ImmediateScheduler Immediate { get; }
```

```c++
public:
static property ImmediateScheduler^ Immediate {
    ImmediateScheduler^ get ();
}
```

```fsharp
static member Immediate : ImmediateScheduler
```

```jscript
static function get Immediate () : ImmediateScheduler
```

#### Property Value

Type: [System.Reactive.Concurrency.ImmediateScheduler](ImmediateScheduler\ImmediateScheduler.md)  
The immediate scheduler.

## Remarks

The Immediate scheduler starts the specified action immediately on the current thread.

## See Also

#### Reference

[Scheduler Class](Scheduler\Scheduler.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)






