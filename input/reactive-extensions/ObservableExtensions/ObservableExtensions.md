title: ObservableExtensions Class
---
# ObservableExtensions Class

Provides a set of static methods for subscribing delegates to observables.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.ObservableExtensions

**Namespace:**  [System](System/System)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public NotInheritable Class ObservableExtensions
```

```vb
'Usage
```

```csharp
public static class ObservableExtensions
```

```c++
[ExtensionAttribute]
public ref class ObservableExtensions abstract sealed
```

```fsharp
[<AbstractClassAttribute>]
[<SealedAttribute>]
type ObservableExtensions =  class end
```

```jscript
public final class ObservableExtensions
```

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe<TSource>(IObservable<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.observableextensions.subscribe%60%601(system.iobservable%7b%60%600%7d)(v=VS.103))Evaluates the observable sequence with a specified source.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe<TSource>(IObservable<TSource>, Action<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.observableextensions.subscribe%60%601(system.iobservable%7b%60%600%7d%2csystem.action%7b%60%600%7d)(v=VS.103))Subscribes an element handler to an observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe<TSource>(IObservable<TSource>, Action<TSource>, Action)](https://msdn.microsoft.com/en-us/library/m:system.observableextensions.subscribe%60%601(system.iobservable%7b%60%600%7d%2csystem.action%7b%60%600%7d%2csystem.action)(v=VS.103))Subscribes an element handler and a completion handler to an observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe<TSource>(IObservable<TSource>, Action<TSource>, Action<Exception>)](https://msdn.microsoft.com/en-us/library/m:system.observableextensions.subscribe%60%601(system.iobservable%7b%60%600%7d%2csystem.action%7b%60%600%7d%2csystem.action%7bsystem.exception%7d)(v=VS.103))Subscribes an element handler and an exception handler to an observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe<TSource>(IObservable<TSource>, Action<TSource>, Action<Exception>, Action)](https://msdn.microsoft.com/en-us/library/m:system.observableextensions.subscribe%60%601(system.iobservable%7b%60%600%7d%2csystem.action%7b%60%600%7d%2csystem.action%7bsystem.exception%7d%2csystem.action)(v=VS.103))Subscribes an element handler, an exception handler, and a completion handler to an observable sequence.Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System Namespace](System/System)








# ObservableExtensions Methods

Include Protected Members  
Include Inherited Members

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe<TSource>(IObservable<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.observableextensions.subscribe%60%601(system.iobservable%7b%60%600%7d)(v=VS.103))Evaluates the observable sequence with a specified source.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe<TSource>(IObservable<TSource>, Action<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.observableextensions.subscribe%60%601(system.iobservable%7b%60%600%7d%2csystem.action%7b%60%600%7d)(v=VS.103))Subscribes an element handler to an observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe<TSource>(IObservable<TSource>, Action<TSource>, Action)](https://msdn.microsoft.com/en-us/library/m:system.observableextensions.subscribe%60%601(system.iobservable%7b%60%600%7d%2csystem.action%7b%60%600%7d%2csystem.action)(v=VS.103))Subscribes an element handler and a completion handler to an observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe<TSource>(IObservable<TSource>, Action<TSource>, Action<Exception>)](https://msdn.microsoft.com/en-us/library/m:system.observableextensions.subscribe%60%601(system.iobservable%7b%60%600%7d%2csystem.action%7b%60%600%7d%2csystem.action%7bsystem.exception%7d)(v=VS.103))Subscribes an element handler and an exception handler to an observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe<TSource>(IObservable<TSource>, Action<TSource>, Action<Exception>, Action)](https://msdn.microsoft.com/en-us/library/m:system.observableextensions.subscribe%60%601(system.iobservable%7b%60%600%7d%2csystem.action%7b%60%600%7d%2csystem.action%7bsystem.exception%7d%2csystem.action)(v=VS.103))Subscribes an element handler, an exception handler, and a completion handler to an observable sequence.Top

## See Also

#### Reference

[ObservableExtensions Class](ObservableExtensions/ObservableExtensions)

[System Namespace](System/System)




