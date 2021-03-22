title: SingleAssignmentDisposable.Disposable Property
---
# SingleAssignmentDisposable.Disposable Property

Gets or sets the underlying disposable.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables/System.Reactive.Disposables)  
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
Dim instance As SingleAssignmentDisposable
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

```javascript
function get Disposable () : IDisposable
function set Disposable (value : IDisposable)
```

#### Property Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
Returns the underlying [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9).

## Remarks

If the SingleAssignmentDisposable has already been assigned, then attempts to set the underlying object will throw an InvalidOperationException.

## See Also

#### Reference

[SingleAssignmentDisposable Class](SingleAssignmentDisposable/SingleAssignmentDisposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)






