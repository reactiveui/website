title: CompositeDisposable.CopyTo()
---
# CompositeDisposable.CopyTo Method

Copies the disposables contained in the CompositeDisposable to an array, starting at a particular array index.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables/System.Reactive.Disposables)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub CopyTo ( _
    array As IDisposable(), _
    arrayIndex As Integer _
)
```

```vb
'Usage
Dim instance As CompositeDisposable
Dim array As IDisposable()
Dim arrayIndex As Integer

instance.CopyTo(array, arrayIndex)
```

```csharp
public void CopyTo(
    IDisposable[] array,
    int arrayIndex
)
```

```c++
public:
virtual void CopyTo(
    array<IDisposable^>^ array, 
    int arrayIndex
) sealed
```

```fsharp
abstract CopyTo : 
        array:IDisposable[] * 
        arrayIndex:int -> unit 
override CopyTo : 
        array:IDisposable[] * 
        arrayIndex:int -> unit 
```

```jscript
public final function CopyTo(
    array : IDisposable[], 
    arrayIndex : int
)
```

#### Parameters

- array  
  Type: array\<[System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\[\]  
  The array to copy the contained disposables to.

- arrayIndex  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The target index at which to copy the first disposable of the group.

#### Implements

[ICollection\<T\>.CopyTo(array\<T\[\], Int32)](https://msdn.microsoft.com/en-us/library/m:system.collections.generic.icollection%601.copyto(%600%5b%5d%2csystem.int32)(v=VS.103))

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable/CompositeDisposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)






