# CompositeDisposable.IsReadOnly Property

Always returns false.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public ReadOnly Property IsReadOnly As Boolean
    Get
```

```vb
'Usage
Dim instance As CompositeDisposable
Dim value As Boolean

value = instance.IsReadOnly
```

```csharp
public bool IsReadOnly { get; }
```

```c++
public:
virtual property bool IsReadOnly {
    bool get () sealed;
}
```

```fsharp
abstract IsReadOnly : bool
override IsReadOnly : bool
```

```jscript
final function get IsReadOnly () : boolean
```

#### Property Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
false in all cases.

#### Implements

[ICollection\<T\>.IsReadOnly](https://msdn.microsoft.com/en-us/library/0cfatk9t)

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable\CompositeDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)






