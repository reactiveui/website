title: ScheduledDisposable.Disposable Property
---
# ScheduledDisposable.Disposable Property

Gets a value that indicates the underlying disposable.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables/System.Reactive.Disposables)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Property Disposable As IDisposable
    Get
    Private Set
```

```vb
'Usage
Dim instance As ScheduledDisposable
Dim value As IDisposable

value = instance.Disposable
```

```csharp
public IDisposable Disposable { get; private set; }
```

```c++
public:
property IDisposable^ Disposable {
    IDisposable^ get ();
    private: void set (IDisposable^ value);
}
```

```fsharp
member Disposable : IDisposable with get, private set
```

```javascript
function get Disposable () : IDisposable
private function set Disposable (value : IDisposable)
```

#### Property Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
The underlying disposable.

## See Also

#### Reference

[ScheduledDisposable Class](ScheduledDisposable/ScheduledDisposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)





