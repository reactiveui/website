title: ScheduledDisposable Properties
---
# ScheduledDisposable Properties

Include Protected Members  
Include Inherited Members

The [ScheduledDisposable](ScheduledDisposable/ScheduledDisposable) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Disposable](Disposable/ScheduledDisposable.Disposable)Gets a value that indicates the underlying disposable.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsDisposed](IsDisposed/ScheduledDisposable.IsDisposed)Gets a value that indicates whether the object is disposed.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Scheduler](Scheduler/ScheduledDisposable.Scheduler)Gets a value that indicates the scheduler.Top

## See Also

#### Reference

[ScheduledDisposable Class](ScheduledDisposable/ScheduledDisposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)

# ScheduledDisposable Class

Represents an object that schedules units of work on a provided scheduler.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Disposables.ScheduledDisposable

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables/System.Reactive.Disposables)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public NotInheritable Class ScheduledDisposable _
    Implements IDisposable
```

```vb
'Usage
Dim instance As ScheduledDisposable
```

```csharp
public sealed class ScheduledDisposable : IDisposable
```

```c++
public ref class ScheduledDisposable sealed : IDisposable
```

```fsharp
[<SealedAttribute>]
type ScheduledDisposable =  
    class
        interface IDisposable
    end
```

```jscript
public final class ScheduledDisposable implements IDisposable
```

The ScheduledDisposable type exposes the following members.

## Constructors

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ScheduledDisposable](https://msdn.microsoft.com/en-us/library/m:system.reactive.disposables.scheduleddisposable.#ctor(system.reactive.concurrency.ischeduler%2csystem.idisposable)(v=VS.103))Initializes a new instance of the ScheduledDisposable class that uses a scheduler on which to dispose the disposable.Top

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Disposable](Disposable/ScheduledDisposable.Disposable)Gets a value that indicates the underlying disposable.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[IsDisposed](IsDisposed/ScheduledDisposable.IsDisposed)Gets a value that indicates whether the object is disposed.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Scheduler](Scheduler/ScheduledDisposable.Scheduler)Gets a value that indicates the scheduler.Top

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose/ScheduledDisposable.Dispose)Disposes the wrapped disposable on the provided scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)

# ScheduledDisposable Methods

Include Protected Members  
Include Inherited Members

The [ScheduledDisposable](ScheduledDisposable/ScheduledDisposable) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Dispose](Dispose/ScheduledDisposable.Dispose)Disposes the wrapped disposable on the provided scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## See Also

#### Reference

[ScheduledDisposable Class](ScheduledDisposable/ScheduledDisposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)

# ScheduledDisposable Constructor

Initializes a new instance of the [ScheduledDisposable](ScheduledDisposable/ScheduledDisposable) class that uses a scheduler on which to dispose the disposable.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables/System.Reactive.Disposables)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New ( _
    scheduler As IScheduler, _
    disposable As IDisposable _
)
```

```vb
'Usage
Dim scheduler As IScheduler
Dim disposable As IDisposable

Dim instance As New ScheduledDisposable(scheduler, _
    disposable)
```

```csharp
public ScheduledDisposable(
    IScheduler scheduler,
    IDisposable disposable
)
```

```c++
public:
ScheduledDisposable(
    IScheduler^ scheduler, 
    IDisposable^ disposable
)
```

```fsharp
new : 
        scheduler:IScheduler * 
        disposable:IDisposable -> ScheduledDisposable
```

```jscript
public function ScheduledDisposable(
    scheduler : IScheduler, 
    disposable : IDisposable
)
```

#### Parameters

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The specified scheduler.

- disposable  
  Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
  The underlying disposable.

## See Also

#### Reference

[ScheduledDisposable Class](ScheduledDisposable/ScheduledDisposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)
