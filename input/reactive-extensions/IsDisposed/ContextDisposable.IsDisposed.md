title: ContextDisposable.IsDisposed Property
---
# ContextDisposable.IsDisposed Property

Gets a value that indicates whether the object is disposed.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public ReadOnly Property IsDisposed As Boolean
    Get
```

```vb
'Usage
Dim instance As ContextDisposable
Dim value As Boolean

value = instance.IsDisposed
```

```csharp
public bool IsDisposed { get; }
```

```c++
public:
property bool IsDisposed {
    bool get ();
}
```

```fsharp
member IsDisposed : bool
```

```jscript
function get IsDisposed () : boolean
```

#### Property Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if the object is disposed; otherwise, false.

## See Also

#### Reference

[ContextDisposable Class](ContextDisposable\ContextDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)





