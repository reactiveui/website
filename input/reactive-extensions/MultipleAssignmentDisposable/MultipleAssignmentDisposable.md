title: MultipleAssignmentDisposable Constructor
---
# MultipleAssignmentDisposable Constructor

Initializes a new instance of the [MultipleAssignmentDisposable](MultipleAssignmentDisposable\MultipleAssignmentDisposable.md) class with no current underlying disposable.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New
```

```vb
'Usage

Dim instance As New MultipleAssignmentDisposable()
```

```csharp
public MultipleAssignmentDisposable()
```

```c++
public:
MultipleAssignmentDisposable()
```

```fsharp
new : unit -> MultipleAssignmentDisposable
```

```jscript
public function MultipleAssignmentDisposable()
```

## Remarks

By default, the MultipleAssignmentDisposable will dispose the current value of the Disposable property before assigning a new value.

## See Also

#### Reference

[MultipleAssignmentDisposable Class](MultipleAssignmentDisposable\MultipleAssignmentDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)






# MultipleAssignmentDisposable Methods

Include Protected Members  
Include Inherited Members

The [MultipleAssignmentDisposable](MultipleAssignmentDisposable\MultipleAssignmentDisposable.md) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose\MultipleAssignmentDisposable.Dispose.md)Disposes the underlying disposable as well as all future replacements.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## See Also

#### Reference

[MultipleAssignmentDisposable Class](MultipleAssignmentDisposable\MultipleAssignmentDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)





# MultipleAssignmentDisposable Properties

Include Protected Members  
Include Inherited Members

The [MultipleAssignmentDisposable](MultipleAssignmentDisposable\MultipleAssignmentDisposable.md) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Disposable](Disposable\MultipleAssignmentDisposable.Disposable.md)Gets or sets the underlying disposable.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsDisposed](IsDisposed\MultipleAssignmentDisposable.IsDisposed.md)Top

## See Also

#### Reference

[MultipleAssignmentDisposable Class](MultipleAssignmentDisposable\MultipleAssignmentDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)





# MultipleAssignmentDisposable Class

Represents a disposable whose underlying disposable can be swapped for another disposable.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Disposables.MultipleAssignmentDisposable

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public NotInheritable Class MultipleAssignmentDisposable _
    Implements IDisposable
```

```vb
'Usage
Dim instance As MultipleAssignmentDisposable
```

```csharp
public sealed class MultipleAssignmentDisposable : IDisposable
```

```c++
public ref class MultipleAssignmentDisposable sealed : IDisposable
```

```fsharp
[<SealedAttribute>]
type MultipleAssignmentDisposable =  
    class
        interface IDisposable
    end
```

```jscript
public final class MultipleAssignmentDisposable implements IDisposable
```

The MultipleAssignmentDisposable type exposes the following members.

## Constructors

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[MultipleAssignmentDisposable](MultipleAssignmentDisposable\MultipleAssignmentDisposable.md)Initializes a new instance of the MultipleAssignmentDisposable class with no current underlying disposable.Top

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Disposable](Disposable\MultipleAssignmentDisposable.Disposable.md)Gets or sets the underlying disposable.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsDisposed](IsDisposed\MultipleAssignmentDisposable.IsDisposed.md)Top

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose\MultipleAssignmentDisposable.Dispose.md)Disposes the underlying disposable as well as all future replacements.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)









