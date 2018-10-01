# ContextDisposable Properties

Include Protected Members  
Include Inherited Members

The [ContextDisposable](ContextDisposable\ContextDisposable.md) type exposes the following members.

## Properties

NameDescription![Public property](images\Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Context](Context\ContextDisposable.Context.md)Gets the provided SynchronizationContext.![Public property](images\Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsDisposed](IsDisposed\ContextDisposable.IsDisposed.md)Gets a value that indicates whether the object is disposed.Top

## See Also

#### Reference

[ContextDisposable Class](ContextDisposable\ContextDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)





# ContextDisposable Constructor

Initializes a new instance of the [ContextDisposable](ContextDisposable\ContextDisposable.md) class that uses a SynchronizationContext on which to dispose the disposable.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New ( _
    context As SynchronizationContext, _
    disposable As IDisposable _
)
```

```vb
'Usage
Dim context As SynchronizationContext
Dim disposable As IDisposable

Dim instance As New ContextDisposable(context, _
    disposable)
```

```csharp
public ContextDisposable(
    SynchronizationContext context,
    IDisposable disposable
)
```

```c++
public:
ContextDisposable(
    SynchronizationContext^ context, 
    IDisposable^ disposable
)
```

```fsharp
new : 
        context:SynchronizationContext * 
        disposable:IDisposable -> ContextDisposable
```

```jscript
public function ContextDisposable(
    context : SynchronizationContext, 
    disposable : IDisposable
)
```

#### Parameters

- context  
  Type: [System.Threading.SynchronizationContext](https://msdn.microsoft.com/en-us/library/wx31754f)  
  The context to perform disposal on.

- disposable  
  Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
  The disposable whose Dispose operation to run on the given synchronization context.

## See Also

#### Reference

[ContextDisposable Class](ContextDisposable\ContextDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)






# ContextDisposable Class

Represents a thread-affine IDisposable.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Disposables.ContextDisposable

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public NotInheritable Class ContextDisposable _
    Implements IDisposable
```

```vb
'Usage
Dim instance As ContextDisposable
```

```csharp
public sealed class ContextDisposable : IDisposable
```

```c++
public ref class ContextDisposable sealed : IDisposable
```

```fsharp
[<SealedAttribute>]
type ContextDisposable =  
    class
        interface IDisposable
    end
```

```jscript
public final class ContextDisposable implements IDisposable
```

The ContextDisposable type exposes the following members.

## Constructors

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ContextDisposable](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.contextdisposable.#ctor(system.threading.synchronizationcontext%2csystem.idisposable)(v=VS.103))Initializes a new instance of the ContextDisposable class that uses a SynchronizationContext on which to dispose the disposable.Top

## Properties

NameDescription![Public property](images\Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Context](Context\ContextDisposable.Context.md)Gets the provided SynchronizationContext.![Public property](images\Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsDisposed](IsDisposed\ContextDisposable.IsDisposed.md)Gets a value that indicates whether the object is disposed.Top

## Methods

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose\ContextDisposable.Dispose.md)Disposes the wrapped disposable on the provided SynchronizationContext.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)










# ContextDisposable Methods

Include Protected Members  
Include Inherited Members

The [ContextDisposable](ContextDisposable\ContextDisposable.md) type exposes the following members.

## Methods

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose\ContextDisposable.Dispose.md)Disposes the wrapped disposable on the provided SynchronizationContext.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## See Also

#### Reference

[ContextDisposable Class](ContextDisposable\ContextDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)




