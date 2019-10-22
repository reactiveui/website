title: MultipleAssignmentDisposable.Disposable Property
---
# MultipleAssignmentDisposable.Disposable Property

Gets or sets the underlying disposable.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Property Disposable As IDisposable
    Get
    Set
```

```vb
'Usage
Dim instance As MultipleAssignmentDisposable
Dim value As IDisposable

value = instance.Disposable

instance.Disposable = value
```

```csharp
public IDisposable Disposable { get; set; }
```

```c++
public:
property IDisposable^ Disposable {
    IDisposable^ get ();
    void set (IDisposable^ value);
}
```

```fsharp
member Disposable : IDisposable with get, set
```

```jscript
function get Disposable () : IDisposable
function set Disposable (value : IDisposable)
```

#### Property Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
Returns the underlying [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9).

## Remarks

If the MultipleAssignmentDisposable has already been disposed, assignment to this property causes immediate disposal of the given disposable object.

## See Also

#### Reference

[MultipleAssignmentDisposable Class](MultipleAssignmentDisposable\MultipleAssignmentDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)






