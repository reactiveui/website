title: ReactiveTest Fields
---
# ReactiveTest Fields

Include Protected Members  
Include Inherited Members

The [ReactiveTest](ReactiveTest/ReactiveTest) type exposes the following members.

## Fields

NameDescription![Public field](https://reactiveui.net/assets/img/Hh314728.pubfield(en-us,VS.103).gif "Public field")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Created](Created/ReactiveTest.Created)Default virtual time used for creation of observable sequences in Rx tests.![Public field](https://reactiveui.net/assets/img/Hh314728.pubfield(en-us,VS.103).gif "Public field")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Disposed](Disposed/ReactiveTest.Disposed)Default virtual time used to dispose subscriptions in Rx tests.![Public field](https://reactiveui.net/assets/img/Hh314728.pubfield(en-us,VS.103).gif "Public field")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribed](Subscribed/ReactiveTest.Subscribed)Default virtual time used to subscribe to an observable sequence in Rx tests.Top

## See Also

#### Reference

[ReactiveTest Class](ReactiveTest/ReactiveTest)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

# ReactiveTest Methods

Include Protected Members  
Include Inherited Members

The [ReactiveTest](ReactiveTest/ReactiveTest) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[OnCompleted<T>](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactivetest.oncompleted%60%601(system.int64)(v=VS.103))Factory method for a recorded OnCompleted notification at a given time.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[OnError<T>](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactivetest.onerror%60%601(system.int64%2csystem.exception)(v=VS.103))Factory method for a recorded OnError notification at a given time with a given error.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[OnNext<T>](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactivetest.onnext%60%601(system.int64%2c%60%600)(v=VS.103))Factory method for a recorded OnNext notification at a given time with a given value.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe(Int64)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactivetest.subscribe(system.int64)(v=VS.103))Factory method for a recorded subscription.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe(Int64, Int64)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactivetest.subscribe(system.int64%2csystem.int64)(v=VS.103))Factory method for a recorded subscription.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## See Also

#### Reference

[ReactiveTest Class](ReactiveTest/ReactiveTest)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

# ReactiveTest Class

Base type to write tests for Rx code.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  Microsoft.Reactive.Testing.ReactiveTest  
    More...

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Class ReactiveTest
```

```vb
'Usage
Dim instance As ReactiveTest
```

```csharp
public class ReactiveTest
```

```c++
public ref class ReactiveTest
```

```fsharp
type ReactiveTest =  class end
```

```jscript
public class ReactiveTest
```

The ReactiveTest type exposes the following members.

## Constructors

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ReactiveTest](ReactiveTest/ReactiveTest)Initializes a new instance of the ReactiveTest class.Top

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[OnCompleted<T>](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactivetest.oncompleted%60%601(system.int64)(v=VS.103))Factory method for a recorded OnCompleted notification at a given time.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[OnError<T>](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactivetest.onerror%60%601(system.int64%2csystem.exception)(v=VS.103))Factory method for a recorded OnError notification at a given time with a given error.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[OnNext<T>](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactivetest.onnext%60%601(system.int64%2c%60%600)(v=VS.103))Factory method for a recorded OnNext notification at a given time with a given value.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe(Int64)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactivetest.subscribe(system.int64)(v=VS.103))Factory method for a recorded subscription.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe(Int64, Int64)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactivetest.subscribe(system.int64%2csystem.int64)(v=VS.103))Factory method for a recorded subscription.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Fields

NameDescription![Public field](https://reactiveui.net/assets/img/Hh314728.pubfield(en-us,VS.103).gif "Public field")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Created](Created/ReactiveTest.Created)Default virtual time used for creation of observable sequences in Rx tests.![Public field](https://reactiveui.net/assets/img/Hh314728.pubfield(en-us,VS.103).gif "Public field")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Disposed](Disposed/ReactiveTest.Disposed)Default virtual time used to dispose subscriptions in Rx tests.![Public field](https://reactiveui.net/assets/img/Hh314728.pubfield(en-us,VS.103).gif "Public field")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribed](Subscribed/ReactiveTest.Subscribed)Default virtual time used to subscribe to an observable sequence in Rx tests.Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  Microsoft.Reactive.Testing.ReactiveTest  
    [ReactiveTests.Tests.AsyncSubjectTest](AsyncSubjectTest/AsyncSubjectTest)  
    [ReactiveTests.Tests.BehaviorSubjectTest](BehaviorSubjectTest/BehaviorSubjectTest)  
    [ReactiveTests.Tests.NotificationTest](NotificationTest/NotificationTest)  
    [ReactiveTests.Tests.ObservableAggregateTest](ObservableAggregateTest/ObservableAggregateTest)  
    [ReactiveTests.Tests.ObservableAsyncTest](ObservableAsyncTest/ObservableAsyncTest)  
    [ReactiveTests.Tests.ObservableBindingTest](ObservableBindingTest/ObservableBindingTest)  
    [ReactiveTests.Tests.ObservableBlockingTest](ObservableBlockingTest/ObservableBlockingTest)  
    [ReactiveTests.Tests.ObservableConcurrencyReactiveTest](ObservableConcurrencyReactiveTest/ObservableConcurrencyReactiveTest)  
    [ReactiveTests.Tests.ObservableConversionTests](ObservableConversionTests/ObservableConversionTests)  
    [ReactiveTests.Tests.ObservableExtensionsTest](ObservableExtensionsTest/ObservableExtensionsTest)  
    [ReactiveTests.Tests.ObservableMultipleTest](ObservableMultipleTest/ObservableMultipleTest)  
    [ReactiveTests.Tests.ObservableSingleTest](ObservableSingleTest/ObservableSingleTest)  
    [ReactiveTests.Tests.ObservableStandardQueryOperatorTest](ObservableStandardQueryOperatorTest/ObservableStandardQueryOperatorTest)  
    [ReactiveTests.Tests.ObservableTest](ObservableTest/ObservableTest)  
    [ReactiveTests.Tests.ObservableTimeTest](ObservableTimeTest/ObservableTimeTest)  
    [ReactiveTests.Tests.ObservableWhensTest](ObservableWhensTest/ObservableWhensTest)  
    [ReactiveTests.Tests.ObserverTest](ObserverTest/ObserverTest)  
    [ReactiveTests.Tests.PrivateTypesTest](PrivateTypesTest/PrivateTypesTest)  
    [ReactiveTests.Tests.RegressionTest](RegressionTest/RegressionTest)  
    [ReactiveTests.Tests.ReplaySubjectTest](ReplaySubjectTest/ReplaySubjectTest)  
    [ReactiveTests.Tests.SubjectTest](SubjectTest/SubjectTest)

# ReactiveTest Constructor

Initializes a new instance of the [ReactiveTest](ReactiveTest/ReactiveTest) class.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Sub New
```

```vb
'Usage

Dim instance As New ReactiveTest()
```

```csharp
public ReactiveTest()
```

```c++
public:
ReactiveTest()
```

```fsharp
new : unit -> ReactiveTest
```

```jscript
public function ReactiveTest()
```

## See Also

#### Reference

[ReactiveTest Class](ReactiveTest/ReactiveTest)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)
