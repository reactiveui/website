title: CompositeDisposable.Contains()
---
# CompositeDisposable.Contains Method

Determines whether the CompositeDisposable contains a specific disposable.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables/System.Reactive.Disposables)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Contains ( _
    item As IDisposable _
) As Boolean
```

```vb
'Usage
Dim instance As CompositeDisposable
Dim item As IDisposable
Dim returnValue As Boolean

returnValue = instance.Contains(item)
```

```csharp
public bool Contains(
    IDisposable item
)
```

```c++
public:
virtual bool Contains(
    IDisposable^ item
) sealed
```

```fsharp
abstract Contains : 
        item:IDisposable -> bool 
override Contains : 
        item:IDisposable -> bool 
```

```jscript
public final function Contains(
    item : IDisposable
) : boolean
```

#### Parameters

- item  
  Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
  The disposable to search for.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if the disposable was found; otherwise, false.

#### Implements

[ICollection\<T\>.Contains(T)](https://msdn.microsoft.com/en-us/library/m:system.collections.generic.icollection%601.contains(%600)(v=VS.103))

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable/CompositeDisposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)







