# Observer Class

Provides a set of static methods for creating observers.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Observer

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public NotInheritable Class Observer
```

```vb
'Usage
```

```csharp
public static class Observer
```

```c++
[ExtensionAttribute]
public ref class Observer abstract sealed
```

```fsharp
[<AbstractClassAttribute>]
[<SealedAttribute>]
type Observer =  class end
```

```jscript
public final class Observer
```

The Observer type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[AsObserver<T>](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.asobserver%60%601(system.iobserver%7b%60%600%7d)(v=VS.103))Hides the identity of an observer.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Create<T>(Action<T>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.create%60%601(system.action%7b%60%600%7d)(v=VS.103))Creates an observer from the specified OnNext action.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Create<T>(Action<T>, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.create%60%601(system.action%7b%60%600%7d%2csystem.action)(v=VS.103))Creates an observer from the specified OnNext and OnCompleted actions.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Create<T>(Action<T>, Action<Exception>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.create%60%601(system.action%7b%60%600%7d%2csystem.action%7bsystem.exception%7d)(v=VS.103))Creates an observer from the specified OnNext and OnError actions.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Create<T>(Action<T>, Action<Exception>, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.create%60%601(system.action%7b%60%600%7d%2csystem.action%7bsystem.exception%7d%2csystem.action)(v=VS.103))Creates an observer from the specified OnNext, OnError, and OnCompleted actions.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Synchronize<T>(IObserver<T>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.synchronize%60%601(system.iobserver%7b%60%600%7d)(v=VS.103))![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Synchronize<T>(IObserver<T>, Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.synchronize%60%601(system.iobserver%7b%60%600%7d%2csystem.object)(v=VS.103))![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToNotifier<T>](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.tonotifier%60%601(system.iobserver%7b%60%600%7d)(v=VS.103))Creates a notification callback from an observer.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToObserver<T>](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.toobserver%60%601(system.action%7bsystem.reactive.notification%7b%60%600%7d%7d)(v=VS.103))Creates an observer from a notification callback.Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive Namespace](System.Reactive\System.Reactive.md)








# Observer Methods

Include Protected Members  
Include Inherited Members

The [Observer](Observer\Observer.md) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[AsObserver<T>](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.asobserver%60%601(system.iobserver%7b%60%600%7d)(v=VS.103))Hides the identity of an observer.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Create<T>(Action<T>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.create%60%601(system.action%7b%60%600%7d)(v=VS.103))Creates an observer from the specified OnNext action.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Create<T>(Action<T>, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.create%60%601(system.action%7b%60%600%7d%2csystem.action)(v=VS.103))Creates an observer from the specified OnNext and OnCompleted actions.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Create<T>(Action<T>, Action<Exception>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.create%60%601(system.action%7b%60%600%7d%2csystem.action%7bsystem.exception%7d)(v=VS.103))Creates an observer from the specified OnNext and OnError actions.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Create<T>(Action<T>, Action<Exception>, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.create%60%601(system.action%7b%60%600%7d%2csystem.action%7bsystem.exception%7d%2csystem.action)(v=VS.103))Creates an observer from the specified OnNext, OnError, and OnCompleted actions.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Synchronize<T>(IObserver<T>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.synchronize%60%601(system.iobserver%7b%60%600%7d)(v=VS.103))![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Synchronize<T>(IObserver<T>, Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.synchronize%60%601(system.iobserver%7b%60%600%7d%2csystem.object)(v=VS.103))![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToNotifier<T>](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.tonotifier%60%601(system.iobserver%7b%60%600%7d)(v=VS.103))Creates a notification callback from an observer.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToObserver<T>](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.toobserver%60%601(system.action%7bsystem.reactive.notification%7b%60%600%7d%7d)(v=VS.103))Creates an observer from a notification callback.Top

## See Also

#### Reference

[Observer Class](Observer\Observer.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)




