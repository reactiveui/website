title: ContextDisposable.Context Property
---
# ContextDisposable.Context Property

Gets the provided SynchronizationContext.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables/System.Reactive.Disposables)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Property Context As SynchronizationContext
    Get
    Private Set
```

```vb
'Usage
Dim instance As ContextDisposable
Dim value As SynchronizationContext

value = instance.Context
```

```csharp
public SynchronizationContext Context { get; private set; }
```

```c++
public:
property SynchronizationContext^ Context {
    SynchronizationContext^ get ();
    private: void set (SynchronizationContext^ value);
}
```

```fsharp
member Context : SynchronizationContext with get, private set
```

```jscript
function get Context () : SynchronizationContext
private function set Context (value : SynchronizationContext)
```

#### Property Value

Type: [System.Threading.SynchronizationContext](https://msdn.microsoft.com/en-us/library/wx31754f)  
The provided context.

## See Also

#### Reference

[ContextDisposable Class](ContextDisposable/ContextDisposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)





