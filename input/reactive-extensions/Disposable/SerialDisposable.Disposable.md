title: SerialDisposable.Disposable Property
---
# SerialDisposable.Disposable Property

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
Dim instance As SerialDisposable
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
Returns the underlying[IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9).

## Remarks

If the SerialDisposable has already been disposed, assignment to this property causes immediate disposal of the given disposable object. Assigning this property disposes the previous disposable object.

## See Also

#### Reference

[SerialDisposable Class](SerialDisposable/SerialDisposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)






