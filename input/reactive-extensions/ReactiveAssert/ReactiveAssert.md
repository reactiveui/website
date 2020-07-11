title: ReactiveAssert Class
---
# ReactiveAssert Class

Represents a helper class to write asserts in Rx unit tests.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  Microsoft.Reactive.Testing.ReactiveAssert

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public NotInheritable Class ReactiveAssert
```

```vb
'Usage
```

```csharp
public static class ReactiveAssert
```

```c++
public ref class ReactiveAssert abstract sealed
```

```fsharp
[<AbstractClassAttribute>]
[<SealedAttribute>]
type ReactiveAssert =  class end
```

```jscript
public final class ReactiveAssert
```

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[AreElementsEqual<T>(IEnumerable<T>, IEnumerable<T>)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.areelementsequal%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.collections.generic.ienumerable%7b%60%600%7d)(v=VS.103))Asserts that both enumerable sequences have equal length and equal elements.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[AreElementsEqual<T>(IObservable<T>, IObservable<T>)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.areelementsequal%60%601(system.iobservable%7b%60%600%7d%2csystem.iobservable%7b%60%600%7d)(v=VS.103))Asserts that both observable sequences have equal length and equal elements.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[AreElementsEqual<T>(IEnumerable<T>, IEnumerable<T>, String)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.areelementsequal%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.collections.generic.ienumerable%7b%60%600%7d%2csystem.string)(v=VS.103))Asserts that both enumerable sequences have equal length and equal elements.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[AreElementsEqual<T>(IObservable<T>, IObservable<T>, String)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.areelementsequal%60%601(system.iobservable%7b%60%600%7d%2csystem.iobservable%7b%60%600%7d%2csystem.string)(v=VS.103))Asserts that both observable sequences have equal length and equal elements.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Throws<TException>(Action)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.throws%60%601(system.action)(v=VS.103))Asserts that the given action throws an exception of the type specified in the generic parameter.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Throws<TException>(Action, String)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.throws%60%601(system.action%2csystem.string)(v=VS.103))Asserts that the given action throws an exception of the type specified in the generic parameter.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Throws<TException>(TException, Action)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.throws%60%601(%60%600%2csystem.action)(v=VS.103))![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Throws<TException>(TException, Action, String)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.throws%60%601(%60%600%2csystem.action%2csystem.string)(v=VS.103))Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

# ReactiveAssert Methods

Include Protected Members  
Include Inherited Members

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[AreElementsEqual<T>(IEnumerable<T>, IEnumerable<T>)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.areelementsequal%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.collections.generic.ienumerable%7b%60%600%7d)(v=VS.103))Asserts that both enumerable sequences have equal length and equal elements.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[AreElementsEqual<T>(IObservable<T>, IObservable<T>)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.areelementsequal%60%601(system.iobservable%7b%60%600%7d%2csystem.iobservable%7b%60%600%7d)(v=VS.103))Asserts that both observable sequences have equal length and equal elements.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[AreElementsEqual<T>(IEnumerable<T>, IEnumerable<T>, String)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.areelementsequal%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.collections.generic.ienumerable%7b%60%600%7d%2csystem.string)(v=VS.103))Asserts that both enumerable sequences have equal length and equal elements.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[AreElementsEqual<T>(IObservable<T>, IObservable<T>, String)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.areelementsequal%60%601(system.iobservable%7b%60%600%7d%2csystem.iobservable%7b%60%600%7d%2csystem.string)(v=VS.103))Asserts that both observable sequences have equal length and equal elements.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Throws<TException>(Action)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.throws%60%601(system.action)(v=VS.103))Asserts that the given action throws an exception of the type specified in the generic parameter.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Throws<TException>(Action, String)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.throws%60%601(system.action%2csystem.string)(v=VS.103))Asserts that the given action throws an exception of the type specified in the generic parameter.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Throws<TException>(TException, Action)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.throws%60%601(%60%600%2csystem.action)(v=VS.103))![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Throws<TException>(TException, Action, String)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactiveassert.throws%60%601(%60%600%2csystem.action%2csystem.string)(v=VS.103))Top

## See Also

#### Reference

[ReactiveAssert Class](ReactiveAssert/ReactiveAssert)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)
