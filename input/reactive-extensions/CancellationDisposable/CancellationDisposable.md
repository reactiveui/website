# CancellationDisposable Methods

Include Protected Members  
Include Inherited Members

The [CancellationDisposable](CancellationDisposable\CancellationDisposable.md) type exposes the following members.

## Methods

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose\CancellationDisposable.Dispose.md)Cancels the CancellationTokenSource.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## See Also

#### Reference

[CancellationDisposable Class](CancellationDisposable\CancellationDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)





# CancellationDisposable Constructor (CancellationTokenSource)

Initializes a new instance of the [CancellationDisposable](CancellationDisposable\CancellationDisposable.md) class that uses an existing CancellationTokenSource.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New ( _
    cts As CancellationTokenSource _
)
```

```vb
'Usage
Dim cts As CancellationTokenSource

Dim instance As New CancellationDisposable(cts)
```

```csharp
public CancellationDisposable(
    CancellationTokenSource cts
)
```

```c++
public:
CancellationDisposable(
    CancellationTokenSource^ cts
)
```

```fsharp
new : 
        cts:CancellationTokenSource -> CancellationDisposable
```

```jscript
public function CancellationDisposable(
    cts : CancellationTokenSource
)
```

#### Parameters

- cts  
  Type: [System.Threading.CancellationTokenSource](https://msdn.microsoft.com/en-us/library/Dd321629)  
  The CancellationTokenSource used for cancellation.

## See Also

#### Reference

[CancellationDisposable Class](CancellationDisposable\CancellationDisposable.md)

[CancellationDisposable Overload](CancellationDisposable\CancellationDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)






# CancellationDisposable Constructor

Include Protected Members  
Include Inherited Members

Initializes a new instance of the [CancellationDisposable](CancellationDisposable\CancellationDisposable.md) class.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CancellationDisposable()](CancellationDisposable\CancellationDisposable.md)Initializes a new instance of the [CancellationDisposable](CancellationDisposable\CancellationDisposable.md) class that uses a new CancellationTokenSource.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CancellationDisposable(CancellationTokenSource)](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.cancellationdisposable.#ctor(system.threading.cancellationtokensource)(v=VS.103))Initializes a new instance of the [CancellationDisposable](CancellationDisposable\CancellationDisposable.md) class that uses an existing CancellationTokenSource.Top

## See Also

#### Reference

[CancellationDisposable Class](CancellationDisposable\CancellationDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)





# CancellationDisposable Constructor

Initializes a new instance of the [CancellationDisposable](CancellationDisposable\CancellationDisposable.md) class that uses a new CancellationTokenSource.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New
```

```vb
'Usage

Dim instance As New CancellationDisposable()
```

```csharp
public CancellationDisposable()
```

```c++
public:
CancellationDisposable()
```

```fsharp
new : unit -> CancellationDisposable
```

```jscript
public function CancellationDisposable()
```

## See Also

#### Reference

[CancellationDisposable Class](CancellationDisposable\CancellationDisposable.md)

[CancellationDisposable Overload](CancellationDisposable\CancellationDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)





# CancellationDisposable Properties

Include Protected Members  
Include Inherited Members

The [CancellationDisposable](CancellationDisposable\CancellationDisposable.md) type exposes the following members.

## Properties

NameDescription![Public property](images\Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsDisposed](IsDisposed\CancellationDisposable.IsDisposed.md)Gets a value that indicates whether the object is disposed.![Public property](images\Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Token](Token\CancellationDisposable.Token.md)Gets the CancellationToken used by this CancellationDisposable.Top

## See Also

#### Reference

[CancellationDisposable Class](CancellationDisposable\CancellationDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)





# CancellationDisposable Class

Represents an IDisposable that can be checked for cancellation status.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Disposables.CancellationDisposable

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public NotInheritable Class CancellationDisposable _
    Implements IDisposable
```

```vb
'Usage
Dim instance As CancellationDisposable
```

```csharp
public sealed class CancellationDisposable : IDisposable
```

```c++
public ref class CancellationDisposable sealed : IDisposable
```

```fsharp
[<SealedAttribute>]
type CancellationDisposable =  
    class
        interface IDisposable
    end
```

```jscript
public final class CancellationDisposable implements IDisposable
```

The CancellationDisposable type exposes the following members.

## Constructors

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CancellationDisposable()](CancellationDisposable\CancellationDisposable.md)Initializes a new instance of the CancellationDisposable class that uses a new CancellationTokenSource.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CancellationDisposable(CancellationTokenSource)](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.cancellationdisposable.#ctor(system.threading.cancellationtokensource)(v=VS.103))Initializes a new instance of the CancellationDisposable class that uses an existing CancellationTokenSource.Top

## Properties

NameDescription![Public property](images\Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsDisposed](IsDisposed\CancellationDisposable.IsDisposed.md)Gets a value that indicates whether the object is disposed.![Public property](images\Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Token](Token\CancellationDisposable.Token.md)Gets the CancellationToken used by this CancellationDisposable.Top

## Methods

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose\CancellationDisposable.Dispose.md)Cancels the CancellationTokenSource.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)









