title: Scheduler.CurrentThread Property
---
# Scheduler.CurrentThread Property

Gets the scheduler that schedules work as soon as possible on the current thread.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared ReadOnly Property CurrentThread As CurrentThreadScheduler
    Get
```

```vb
'Usage
Dim value As CurrentThreadScheduler

value = Scheduler.CurrentThread
```

```csharp
public static CurrentThreadScheduler CurrentThread { get; }
```

```c++
public:
static property CurrentThreadScheduler^ CurrentThread {
    CurrentThreadScheduler^ get ();
}
```

```fsharp
static member CurrentThread : CurrentThreadScheduler
```

```javascript
static function get CurrentThread () : CurrentThreadScheduler
```

#### Property Value

Type: [System.Reactive.Concurrency.CurrentThreadScheduler](CurrentThreadScheduler/CurrentThreadScheduler)  
The current thread scheduler.

## Remarks

The CurrentThread scheduler will schedule actions to be performed on the thread that makes the original call. The action is not executed immediately, but is placed in a queue and only executed after the current action is complete.

## See Also

#### Reference

[Scheduler Class](Scheduler/Scheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)






