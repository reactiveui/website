title: Scheduler.TaskPool Property
---
# Scheduler.TaskPool Property

Gets the scheduler that schedules work on the default Task Factory.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared ReadOnly Property TaskPool As TaskPoolScheduler
    Get
```

```vb
'Usage
Dim value As TaskPoolScheduler

value = Scheduler.TaskPool
```

```csharp
public static TaskPoolScheduler TaskPool { get; }
```

```c++
public:
static property TaskPoolScheduler^ TaskPool {
    TaskPoolScheduler^ get ();
}
```

```fsharp
static member TaskPool : TaskPoolScheduler
```

```jscript
static function get TaskPool () : TaskPoolScheduler
```

#### Property Value

Type: [System.Reactive.Concurrency.TaskPoolScheduler](TaskPoolScheduler/TaskPoolScheduler)  
The task pool scheduler.

## Remarks

The TaskPool scheduler schedules actions to execute using the Task Factory from the Task Programming Library (TPL). This scheduler is ideal for short running operations.

## See Also

#### Reference

[Scheduler Class](Scheduler/Scheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)
