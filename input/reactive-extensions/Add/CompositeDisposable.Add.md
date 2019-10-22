title: CompositeDisposable.Add()
---
# CompositeDisposable.Add Method

Adds a disposable to the CompositeDisposable or disposes the disposable if the CompositeDisposable is disposed.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub Add ( _
    item As IDisposable _
)
```

```vb
'Usage
Dim instance As CompositeDisposable
Dim item As IDisposable

instance.Add(item)
```

```csharp
public void Add(
    IDisposable item
)
```

```c++
public:
virtual void Add(
    IDisposable^ item
) sealed
```

```fsharp
abstract Add : 
        item:IDisposable -> unit 
override Add : 
        item:IDisposable -> unit 
```

```jscript
public final function Add(
    item : IDisposable
)
```

#### Parameters

- item  
  Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
  The disposable to add.

#### Implements

[ICollection\<T\>.Add(T)](https://msdn.microsoft.com/en-us/library/m:system.collections.generic.icollection%601.add(%600)(v=VS.103))

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable\CompositeDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)
