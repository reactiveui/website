title: CurrentThreadScheduler.ScheduleRequired Property
---
# CurrentThreadScheduler.ScheduleRequired Property

Gets a value indicating whether the caller must call a schedule method.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public ReadOnly Property ScheduleRequired As Boolean
    Get
```

```vb
'Usage
Dim instance As CurrentThreadScheduler
Dim value As Boolean

value = instance.ScheduleRequired
```

```csharp
public bool ScheduleRequired { get; }
```

```c++
public:
property bool ScheduleRequired {
    bool get ();
}
```

```fsharp
member ScheduleRequired : bool
```

```jscript
function get ScheduleRequired () : boolean
```

#### Property Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
Returns [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50).

## See Also

#### Reference

[CurrentThreadScheduler Class](CurrentThreadScheduler/CurrentThreadScheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)
