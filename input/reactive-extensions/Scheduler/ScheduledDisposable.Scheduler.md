# ScheduledDisposable.Scheduler Property

Gets a value that indicates the scheduler.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Property Scheduler As IScheduler
    Get
    Private Set
```

```vb
'Usage
Dim instance As ScheduledDisposable
Dim value As IScheduler

value = instance.Scheduler
```

```csharp
public IScheduler Scheduler { get; private set; }
```

```c++
public:
property IScheduler^ Scheduler {
    IScheduler^ get ();
    private: void set (IScheduler^ value);
}
```

```fsharp
member Scheduler : IScheduler with get, private set
```

```jscript
function get Scheduler () : IScheduler
private function set Scheduler (value : IScheduler)
```

#### Property Value

Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
The scheduler.

## See Also

#### Reference

[ScheduledDisposable Class](ScheduledDisposable\ScheduledDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)
