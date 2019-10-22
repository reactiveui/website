title: CompositeDisposable Constructor
---
# CompositeDisposable Constructor

Initializes a new instance of the [CompositeDisposable](CompositeDisposable\CompositeDisposable.md) class from a group of disposables.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New
```

```vb
'Usage

Dim instance As New CompositeDisposable()
```

```csharp
public CompositeDisposable()
```

```c++
public:
CompositeDisposable()
```

```fsharp
new : unit -> CompositeDisposable
```

```jscript
public function CompositeDisposable()
```

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable\CompositeDisposable.md)

[CompositeDisposable Overload](CompositeDisposable\CompositeDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)





# CompositeDisposable Constructor (IEnumerable\<IDisposable\>)

Initializes a new instance of the [CompositeDisposable](CompositeDisposable\CompositeDisposable.md) class from a group of disposables.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New ( _
    disposables As IEnumerable(Of IDisposable) _
)
```

```vb
'Usage
Dim disposables As IEnumerable(Of IDisposable)

Dim instance As New CompositeDisposable(disposables)
```

```csharp
public CompositeDisposable(
    IEnumerable<IDisposable> disposables
)
```

```c++
public:
CompositeDisposable(
    IEnumerable<IDisposable^>^ disposables
)
```

```fsharp
new : 
        disposables:IEnumerable<IDisposable> -> CompositeDisposable
```

```jscript
public function CompositeDisposable(
    disposables : IEnumerable<IDisposable>
)
```

#### Parameters

- disposables  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\>  
  The disposables that will be disposed together.

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable\CompositeDisposable.md)

[CompositeDisposable Overload](CompositeDisposable\CompositeDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)






# CompositeDisposable Methods

Include Protected Members  
Include Inherited Members

The [CompositeDisposable](CompositeDisposable\CompositeDisposable.md) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Add](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.compositedisposable.add(system.idisposable)(v=VS.103))Adds a disposable to the CompositeDisposable or disposes the disposable if the CompositeDisposable is disposed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Clear](Clear\CompositeDisposable.Clear.md)Removes and disposes all disposables from the GroupDisposable, but does not dispose the CompositeDisposable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Contains](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.compositedisposable.contains(system.idisposable)(v=VS.103))Determines whether the CompositeDisposable contains a specific disposable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CopyTo](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.compositedisposable.copyto(system.idisposable%5b%5d%2csystem.int32)(v=VS.103))Copies the disposables contained in the CompositeDisposable to an array, starting at a particular array index.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose\CompositeDisposable.Dispose.md)Disposes all disposables in the group and removes them from the group.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetEnumerator](GetEnumerator\CompositeDisposable.GetEnumerator.md)Returns an enumerator that iterates through the CompositeDisposable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Remove](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.compositedisposable.remove(system.idisposable)(v=VS.103))Removes and disposes the first occurrence of a disposable from the CompositeDisposable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Extension Methods

NameDescription![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[AssertEqual<IDisposable>(IEnumerable<IDisposable>)](https://msdn.microsoft.com/en-us/library/m:reactivetests.extensions.assertequal%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.collections.generic.ienumerable%7b%60%600%7d)(v=VS.103))Overloaded. (Defined by [Extensions](Extensions\Extensions.md).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[AssertEqual<IDisposable>(array<IDisposable[])](https://msdn.microsoft.com/en-us/library/m:reactivetests.extensions.assertequal%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2c%60%600%5b%5d)(v=VS.103))Overloaded. (Defined by [Extensions](Extensions\Extensions.md).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Subscribe<IDisposable>(IObserver<IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.subscribe%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.iobserver%7b%60%600%7d)(v=VS.103))Overloaded. Subscribes an observer to an enumerable sequence with the specified source and observer. (Defined by [Observable](Observable\Observable.md).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Subscribe<IDisposable>(IObserver<IDisposable>, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.subscribe%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.iobserver%7b%60%600%7d%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Overloaded. Subscribes an observer to an enumerable sequence with the specified source and observer. (Defined by [Observable](Observable\Observable.md).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[ToObservable<IDisposable>()](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.toobservable%60%601(system.collections.generic.ienumerable%7b%60%600%7d)(v=VS.103))Overloaded. Converts an enumerable sequence to an observable sequence with a specified source. (Defined by [Observable](Observable\Observable.md).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[ToObservable<IDisposable>(IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.toobservable%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Overloaded. Converts an enumerable sequence to an observable sequence with a specified source and scheduler. (Defined by [Observable](Observable\Observable.md).)Top

## Explicit Interface Implementations

NameDescription![Explicit interface implemetation](https://reactiveui.net/assets/img/Hh212009.pubinterface(en-us,VS.103).gif "Explicit interface implemetation")![Private method](https://reactiveui.net/assets/img/Hh314705.privmethod(en-us,VS.103).gif "Private method")[IEnumerable.GetEnumerator](IEnumerable.GetEnumerator\CompositeDisposable.IEnumerable.GetEnumerator.md)Returns an enumerator that iterates through the CompositeDisposable.Top

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable\CompositeDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)







# CompositeDisposable Constructor (Int32)

Initializes a new instance of the [CompositeDisposable](CompositeDisposable\CompositeDisposable.md) class with the specified number of disposables.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New ( _
    capacity As Integer _
)
```

```vb
'Usage
Dim capacity As Integer

Dim instance As New CompositeDisposable(capacity)
```

```csharp
public CompositeDisposable(
    int capacity
)
```

```c++
public:
CompositeDisposable(
    int capacity
)
```

```fsharp
new : 
        capacity:int -> CompositeDisposable
```

```jscript
public function CompositeDisposable(
    capacity : int
)
```

#### Parameters

- capacity  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The number of disposables that the new CompositeDisposable can initially store.

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable\CompositeDisposable.md)

[CompositeDisposable Overload](CompositeDisposable\CompositeDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)






# CompositeDisposable Constructor (array\<IDisposable\[\])

Initializes a new instance of the [CompositeDisposable](CompositeDisposable\CompositeDisposable.md) class from a group of disposables.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New ( _
    ParamArray disposables As IDisposable() _
)
```

```vb
'Usage
Dim disposables As IDisposable()

Dim instance As New CompositeDisposable(disposables)
```

```csharp
public CompositeDisposable(
    params IDisposable[] disposables
)
```

```c++
public:
CompositeDisposable(
    ... array<IDisposable^>^ disposables
)
```

```fsharp
new : 
        disposables:IDisposable[] -> CompositeDisposable
```

```jscript
public function CompositeDisposable(
    ... disposables : IDisposable[]
)
```

#### Parameters

- disposables  
  Type: array\<[System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\[\]  
  The disposables that will be disposed together.

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable\CompositeDisposable.md)

[CompositeDisposable Overload](CompositeDisposable\CompositeDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)






# CompositeDisposable Properties

Include Protected Members  
Include Inherited Members

The [CompositeDisposable](CompositeDisposable\CompositeDisposable.md) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Count](Count\CompositeDisposable.Count.md)Gets the number of disposables contained in the CompositeDisposable.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsDisposed](IsDisposed\CompositeDisposable.IsDisposed.md)Gets a value that indicates whether the object is disposed.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsReadOnly](IsReadOnly\CompositeDisposable.IsReadOnly.md)Always returns false.Top

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable\CompositeDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)





# CompositeDisposable Class

Represents a group of Disposables that are disposed together.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Disposables.CompositeDisposable

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public NotInheritable Class CompositeDisposable _
    Implements ICollection(Of IDisposable), IEnumerable(Of IDisposable),  _
    IEnumerable, IDisposable
```

```vb
'Usage
Dim instance As CompositeDisposable
```

```csharp
public sealed class CompositeDisposable : ICollection<IDisposable>, 
    IEnumerable<IDisposable>, IEnumerable, IDisposable
```

```c++
public ref class CompositeDisposable sealed : ICollection<IDisposable^>, 
    IEnumerable<IDisposable^>, IEnumerable, IDisposable
```

```fsharp
[<SealedAttribute>]
type CompositeDisposable =  
    class
        interface ICollection<IDisposable>
        interface IEnumerable<IDisposable>
        interface IEnumerable
        interface IDisposable
    end
```

```jscript
public final class CompositeDisposable implements ICollection<IDisposable>, IEnumerable<IDisposable>, IEnumerable, IDisposable
```

The CompositeDisposable type exposes the following members.

## Constructors

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CompositeDisposable()](CompositeDisposable\CompositeDisposable.md)Initializes a new instance of the CompositeDisposable class from a group of disposables.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CompositeDisposable(IEnumerable<IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.compositedisposable.#ctor(system.collections.generic.ienumerable%7bsystem.idisposable%7d)(v=VS.103))Initializes a new instance of the CompositeDisposable class from a group of disposables.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CompositeDisposable(array<IDisposable[])](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.compositedisposable.#ctor(system.idisposable%5b%5d)(v=VS.103))Initializes a new instance of the CompositeDisposable class from a group of disposables.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CompositeDisposable(Int32)](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.compositedisposable.#ctor(system.int32)(v=VS.103))Initializes a new instance of the CompositeDisposable class with the specified number of disposables.Top

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Count](Count\CompositeDisposable.Count.md)Gets the number of disposables contained in the CompositeDisposable.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsDisposed](IsDisposed\CompositeDisposable.IsDisposed.md)Gets a value that indicates whether the object is disposed.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsReadOnly](IsReadOnly\CompositeDisposable.IsReadOnly.md)Always returns false.Top

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Add](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.compositedisposable.add(system.idisposable)(v=VS.103))Adds a disposable to the CompositeDisposable or disposes the disposable if the CompositeDisposable is disposed.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Clear](Clear\CompositeDisposable.Clear.md)Removes and disposes all disposables from the GroupDisposable, but does not dispose the CompositeDisposable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Contains](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.compositedisposable.contains(system.idisposable)(v=VS.103))Determines whether the CompositeDisposable contains a specific disposable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CopyTo](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.compositedisposable.copyto(system.idisposable%5b%5d%2csystem.int32)(v=VS.103))Copies the disposables contained in the CompositeDisposable to an array, starting at a particular array index.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose\CompositeDisposable.Dispose.md)Disposes all disposables in the group and removes them from the group.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetEnumerator](GetEnumerator\CompositeDisposable.GetEnumerator.md)Returns an enumerator that iterates through the CompositeDisposable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Remove](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.compositedisposable.remove(system.idisposable)(v=VS.103))Removes and disposes the first occurrence of a disposable from the CompositeDisposable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Extension Methods

NameDescription![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[AssertEqual<IDisposable>(IEnumerable<IDisposable>)](https://msdn.microsoft.com/en-us/library/m:reactivetests.extensions.assertequal%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.collections.generic.ienumerable%7b%60%600%7d)(v=VS.103))Overloaded. (Defined by [Extensions](Extensions\Extensions.md).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[AssertEqual<IDisposable>(array<IDisposable[])](https://msdn.microsoft.com/en-us/library/m:reactivetests.extensions.assertequal%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2c%60%600%5b%5d)(v=VS.103))Overloaded. (Defined by [Extensions](Extensions\Extensions.md).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Subscribe<IDisposable>(IObserver<IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.subscribe%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.iobserver%7b%60%600%7d)(v=VS.103))Overloaded. Subscribes an observer to an enumerable sequence with the specified source and observer. (Defined by [Observable](Observable\Observable.md).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Subscribe<IDisposable>(IObserver<IDisposable>, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.subscribe%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.iobserver%7b%60%600%7d%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Overloaded. Subscribes an observer to an enumerable sequence with the specified source and observer. (Defined by [Observable](Observable\Observable.md).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[ToObservable<IDisposable>()](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.toobservable%60%601(system.collections.generic.ienumerable%7b%60%600%7d)(v=VS.103))Overloaded. Converts an enumerable sequence to an observable sequence with a specified source. (Defined by [Observable](Observable\Observable.md).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[ToObservable<IDisposable>(IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.toobservable%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Overloaded. Converts an enumerable sequence to an observable sequence with a specified source and scheduler. (Defined by [Observable](Observable\Observable.md).)Top

## Explicit Interface Implementations

NameDescription![Explicit interface implemetation](https://reactiveui.net/assets/img/Hh212009.pubinterface(en-us,VS.103).gif "Explicit interface implemetation")![Private method](https://reactiveui.net/assets/img/Hh314705.privmethod(en-us,VS.103).gif "Private method")[IEnumerable.GetEnumerator](IEnumerable.GetEnumerator\CompositeDisposable.IEnumerable.GetEnumerator.md)Returns an enumerator that iterates through the CompositeDisposable.Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)












# CompositeDisposable Constructor

Include Protected Members  
Include Inherited Members

Initializes a new instance of the [CompositeDisposable](CompositeDisposable\CompositeDisposable.md) class.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CompositeDisposable()](CompositeDisposable\CompositeDisposable.md)Initializes a new instance of the [CompositeDisposable](CompositeDisposable\CompositeDisposable.md) class from a group of disposables.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CompositeDisposable(IEnumerable<IDisposable>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.compositedisposable.#ctor(system.collections.generic.ienumerable%7bsystem.idisposable%7d)(v=VS.103))Initializes a new instance of the [CompositeDisposable](CompositeDisposable\CompositeDisposable.md) class from a group of disposables.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CompositeDisposable(array<IDisposable[])](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.compositedisposable.#ctor(system.idisposable%5b%5d)(v=VS.103))Initializes a new instance of the [CompositeDisposable](CompositeDisposable\CompositeDisposable.md) class from a group of disposables.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[CompositeDisposable(Int32)](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.compositedisposable.#ctor(system.int32)(v=VS.103))Initializes a new instance of the [CompositeDisposable](CompositeDisposable\CompositeDisposable.md) class with the specified number of disposables.Top

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable\CompositeDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)




