# CancellationDisposable.Token Property

Gets the CancellationToken used by this CancellationDisposable.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public ReadOnly Property Token As CancellationToken
    Get
```

```vb
'Usage
Dim instance As CancellationDisposable
Dim value As CancellationToken

value = instance.Token
```

```csharp
public CancellationToken Token { get; }
```

```c++
public:
property CancellationToken Token {
    CancellationToken get ();
}
```

```fsharp
member Token : CancellationToken
```

```jscript
function get Token () : CancellationToken
```

#### Property Value

Type: [System.Threading.CancellationToken](https://msdn.microsoft.com/en-us/library/Dd384802)  
The CancellationToken used by this CancellationDisposable.

## See Also

#### Reference

[CancellationDisposable Class](CancellationDisposable\CancellationDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)
