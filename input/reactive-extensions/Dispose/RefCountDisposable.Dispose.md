# RefCountDisposable.Dispose Method

Disposes the underlying disposable only when all dependent disposables have been disposed.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub Dispose
```

```vb
'Usage
Dim instance As RefCountDisposable

instance.Dispose()
```

```csharp
public void Dispose()
```

```c++
public:
virtual void Dispose() sealed
```

```fsharp
abstract Dispose : unit -> unit 
override Dispose : unit -> unit 
```

```jscript
public final function Dispose()
```

#### Implements

[IDisposable.Dispose()](https://msdn.microsoft.com/en-us/library/es4s3w1d)

## See Also

#### Reference

[RefCountDisposable Class](RefCountDisposable\RefCountDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)





