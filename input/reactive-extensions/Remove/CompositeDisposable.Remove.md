title: CompositeDisposable.Remove()
---
# CompositeDisposable.Remove Method

Removes and disposes the first occurrence of a disposable from the CompositeDisposable.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables/System.Reactive.Disposables)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Remove ( _
    item As IDisposable _
) As Boolean
```

```vb
'Usage
Dim instance As CompositeDisposable
Dim item As IDisposable
Dim returnValue As Boolean

returnValue = instance.Remove(item)
```

```csharp
public bool Remove(
    IDisposable item
)
```

```c++
public:
virtual bool Remove(
    IDisposable^ item
) sealed
```

```fsharp
abstract Remove : 
        item:IDisposable -> bool 
override Remove : 
        item:IDisposable -> bool 
```

```jscript
public final function Remove(
    item : IDisposable
) : boolean
```

#### Parameters

- item  
  Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
  The disposable to remove.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)

#### Implements

[ICollection\<T\>.Remove(T)](https://msdn.microsoft.com/en-us/library/m:system.collections.generic.icollection%601.remove(%600)(v=VS.103))

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable/CompositeDisposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)
