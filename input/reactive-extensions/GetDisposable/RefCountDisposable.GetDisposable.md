title: RefCountDisposable.GetDisposable()
---
# RefCountDisposable.GetDisposable Method

Returns a dependent disposable that when disposed decreases the refcount on the underlying disposable.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables/System.Reactive.Disposables)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function GetDisposable As IDisposable
```

```vb
'Usage
Dim instance As RefCountDisposable
Dim returnValue As IDisposable

returnValue = instance.GetDisposable()
```

```csharp
public IDisposable GetDisposable()
```

```c++
public:
IDisposable^ GetDisposable()
```

```fsharp
member GetDisposable : unit -> IDisposable 
```

```javascript
public function GetDisposable() : IDisposable
```

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
A dependent disposable contributing to the reference count that manages the underlying disposable's lifetime.

## See Also

#### Reference

[RefCountDisposable Class](RefCountDisposable/RefCountDisposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)





