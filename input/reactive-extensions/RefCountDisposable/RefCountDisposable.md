# RefCountDisposable Constructor

Initializes a new instance of the [RefCountDisposable](RefCountDisposable\RefCountDisposable.md) class with the specified disposable.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New ( _
    disposable As IDisposable _
)
```

```vb
'Usage
Dim disposable As IDisposable

Dim instance As New RefCountDisposable(disposable)
```

```csharp
public RefCountDisposable(
    IDisposable disposable
)
```

```c++
public:
RefCountDisposable(
    IDisposable^ disposable
)
```

```fsharp
new : 
        disposable:IDisposable -> RefCountDisposable
```

```jscript
public function RefCountDisposable(
    disposable : IDisposable
)
```

#### Parameters

- disposable  
  Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
  The underlying disposable.

## See Also

#### Reference

[RefCountDisposable Class](RefCountDisposable\RefCountDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)

# RefCountDisposable Class

Represents a disposable that only disposes its underlying disposable when all dependent disposables have been disposed.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Disposables.RefCountDisposable

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public NotInheritable Class RefCountDisposable _
    Implements IDisposable
```

```vb
'Usage
Dim instance As RefCountDisposable
```

```csharp
public sealed class RefCountDisposable : IDisposable
```

```c++
public ref class RefCountDisposable sealed : IDisposable
```

```fsharp
[<SealedAttribute>]
type RefCountDisposable =  
    class
        interface IDisposable
    end
```

```jscript
public final class RefCountDisposable implements IDisposable
```

The RefCountDisposable type exposes the following members.

## Constructors

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[RefCountDisposable](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.refcountdisposable.#ctor(system.idisposable)(v=VS.103))Initializes a new instance of the RefCountDisposable class with the specified disposable.Top

## Properties

NameDescription![Public property](images\Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsDisposed](IsDisposed\RefCountDisposable.IsDisposed.md)Gets a value that indicates whether the object is disposed.Top

## Methods

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose\RefCountDisposable.Dispose.md)Disposes the underlying disposable only when all dependent disposables have been disposed.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetDisposable](GetDisposable\RefCountDisposable.GetDisposable.md)Returns a dependent disposable that when disposed decreases the refcount on the underlying disposable.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)

# RefCountDisposable Methods

Include Protected Members  
Include Inherited Members

The [RefCountDisposable](RefCountDisposable\RefCountDisposable.md) type exposes the following members.

## Methods

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose\RefCountDisposable.Dispose.md)Disposes the underlying disposable only when all dependent disposables have been disposed.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetDisposable](GetDisposable\RefCountDisposable.GetDisposable.md)Returns a dependent disposable that when disposed decreases the refcount on the underlying disposable.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## See Also

#### Reference

[RefCountDisposable Class](RefCountDisposable\RefCountDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)

# RefCountDisposable Properties

Include Protected Members  
Include Inherited Members

The [RefCountDisposable](RefCountDisposable\RefCountDisposable.md) type exposes the following members.

## Properties

NameDescription![Public property](images\Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsDisposed](IsDisposed\RefCountDisposable.IsDisposed.md)Gets a value that indicates whether the object is disposed.Top

## See Also

#### Reference

[RefCountDisposable Class](RefCountDisposable\RefCountDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)
