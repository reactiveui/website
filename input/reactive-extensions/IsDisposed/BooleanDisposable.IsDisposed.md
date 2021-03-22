title: BooleanDisposable.IsDisposed Property
---
# BooleanDisposable.IsDisposed Property

Gets a value that indicates whether the object is disposed.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables/System.Reactive.Disposables)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Property IsDisposed As Boolean
    Get
    Private Set
```

```vb
'Usage
Dim instance As BooleanDisposable
Dim value As Boolean

value = instance.IsDisposed
```

```csharp
public bool IsDisposed { get; private set; }
```

```c++
public:
property bool IsDisposed {
    bool get ();
    private: void set (bool value);
}
```

```fsharp
member IsDisposed : bool with get, private set
```

```javascript
function get IsDisposed () : boolean
private function set IsDisposed (value : boolean)
```

#### Property Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if the object is disposed; otherwise, false.

## See Also

#### Reference

[BooleanDisposable Class](BooleanDisposable/BooleanDisposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)





