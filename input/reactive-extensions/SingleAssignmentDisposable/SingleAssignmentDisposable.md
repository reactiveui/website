title: SingleAssignmentDisposable()s
---
# SingleAssignmentDisposable Methods

Include Protected Members  
Include Inherited Members

The [SingleAssignmentDisposable](SingleAssignmentDisposable\SingleAssignmentDisposable.md) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose\SingleAssignmentDisposable.Dispose.md)Disposes the underlying disposable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## See Also

#### Reference

[SingleAssignmentDisposable Class](SingleAssignmentDisposable\SingleAssignmentDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)

# SingleAssignmentDisposable Class

A SingleAssignmentDisposable only allows a single assignment of its disposable object. If it has already been assigned, attempts to set the underlying object will throw an InvalidOperationException.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Disposables.SingleAssignmentDisposable

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Class SingleAssignmentDisposable _
    Implements IDisposable
```

```vb
'Usage
Dim instance As SingleAssignmentDisposable
```

```csharp
public class SingleAssignmentDisposable : IDisposable
```

```c++
public ref class SingleAssignmentDisposable : IDisposable
```

```fsharp
type SingleAssignmentDisposable =  
    class
        interface IDisposable
    end
```

```jscript
public class SingleAssignmentDisposable implements IDisposable
```

The SingleAssignmentDisposable type exposes the following members.

## Constructors

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[SingleAssignmentDisposable](SingleAssignmentDisposable\SingleAssignmentDisposable.md)Initializes a new instance if the SingleAssignmentDisposable class.Top

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Disposable](Disposable\SingleAssignmentDisposable.Disposable.md)Gets or sets the underlying disposable.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsDisposed](IsDisposed\SingleAssignmentDisposable.IsDisposed.md)Gets a value indicating whether the object is disposed.Top

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose\SingleAssignmentDisposable.Dispose.md)Disposes the underlying disposable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)


# SingleAssignmentDisposable Constructor

Initializes a new instance if the [SingleAssignmentDisposable](SingleAssignmentDisposable\SingleAssignmentDisposable.md) class.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New
```

```vb
'Usage

Dim instance As New SingleAssignmentDisposable()
```

```csharp
public SingleAssignmentDisposable()
```

```c++
public:
SingleAssignmentDisposable()
```

```fsharp
new : unit -> SingleAssignmentDisposable
```

```jscript
public function SingleAssignmentDisposable()
```

## Remarks

A SingleAssignmentDisposable only allows a single assignment of its disposable object. If it has already been assigned, attempts to set the underlying object will throw an InvalidOperationException.

## See Also

#### Reference

[SingleAssignmentDisposable Class](SingleAssignmentDisposable\SingleAssignmentDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)

# SingleAssignmentDisposable Properties

Include Protected Members  
Include Inherited Members

The [SingleAssignmentDisposable](SingleAssignmentDisposable\SingleAssignmentDisposable.md) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Disposable](Disposable\SingleAssignmentDisposable.Disposable.md)Gets or sets the underlying disposable.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsDisposed](IsDisposed\SingleAssignmentDisposable.IsDisposed.md)Gets a value indicating whether the object is disposed.Top

## See Also

#### Reference

[SingleAssignmentDisposable Class](SingleAssignmentDisposable\SingleAssignmentDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)
