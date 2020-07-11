title: MockEnumerable<T> Class
---
# MockEnumerable\<T\> Class

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  ReactiveTests.MockEnumerable\<T\>

**Namespace:**  [ReactiveTests](ReactiveTests/ReactiveTests)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Class MockEnumerable(Of T) _
    Implements IEnumerable(Of T), IEnumerable
```

```vb
'Usage
Dim instance As MockEnumerable(Of T)
```

```csharp
public class MockEnumerable<T> : IEnumerable<T>, 
    IEnumerable
```

```c++
generic<typename T>
public ref class MockEnumerable : IEnumerable<T>, 
    IEnumerable
```

```fsharp
type MockEnumerable<'T> =  
    class
        interface IEnumerable<'T>
        interface IEnumerable
    end
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

The MockEnumerable\<T\> type exposes the following members.

## Constructors

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[MockEnumerable<T>](https://msdn.microsoft.com/en-us/library/m:reactivetests.mockenumerable%601.#ctor(microsoft.reactive.testing.testscheduler%2csystem.collections.generic.ienumerable%7b%600%7d)(v=VS.103))Top

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetEnumerator](GetEnumerator/MockEnumerable(T).GetEnumerator)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Extension Methods

NameDescription![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[AssertEqual<T>(IEnumerable<T>)](https://msdn.microsoft.com/en-us/library/m:reactivetests.extensions.assertequal%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.collections.generic.ienumerable%7b%60%600%7d)(v=VS.103))Overloaded. (Defined by [Extensions](Extensions/Extensions).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[AssertEqual<T>(array<T[])](https://msdn.microsoft.com/en-us/library/m:reactivetests.extensions.assertequal%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2c%60%600%5b%5d)(v=VS.103))Overloaded. (Defined by [Extensions](Extensions/Extensions).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Subscribe<T>(IObserver<T>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.subscribe%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.iobserver%7b%60%600%7d)(v=VS.103))Overloaded. Subscribes an observer to an enumerable sequence with the specified source and observer. (Defined by [Observable](Observable/Observable).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Subscribe<T>(IObserver<T>, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.subscribe%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.iobserver%7b%60%600%7d%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Overloaded. Subscribes an observer to an enumerable sequence with the specified source and observer. (Defined by [Observable](Observable/Observable).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[ToObservable<T>()](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.toobservable%60%601(system.collections.generic.ienumerable%7b%60%600%7d)(v=VS.103))Overloaded. Converts an enumerable sequence to an observable sequence with a specified source. (Defined by [Observable](Observable/Observable).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[ToObservable<T>(IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.toobservable%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Overloaded. Converts an enumerable sequence to an observable sequence with a specified source and scheduler. (Defined by [Observable](Observable/Observable).)Top

## Fields

NameDescription![Public field](https://reactiveui.net/assets/img/Hh314728.pubfield(en-us,VS.103).gif "Public field")[Scheduler](Scheduler/MockEnumerable(T).Scheduler)![Public field](https://reactiveui.net/assets/img/Hh314728.pubfield(en-us,VS.103).gif "Public field")[Subscriptions](Subscriptions/MockEnumerable(T).Subscriptions)Top

## Explicit Interface Implementations

NameDescription![Explicit interface implemetation](https://reactiveui.net/assets/img/Hh212009.pubinterface(en-us,VS.103).gif "Explicit interface implemetation")![Private method](https://reactiveui.net/assets/img/Hh314705.privmethod(en-us,VS.103).gif "Private method")[IEnumerable.GetEnumerator](IEnumerable.GetEnumerator/MockEnumerable(T).IEnumerable.GetEnumerator)Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[ReactiveTests Namespace](ReactiveTests/ReactiveTests)













# MockEnumerable\<T\> Methods

Include Protected Members  
Include Inherited Members

The [MockEnumerable\<T\>](MockEnumerable/MockEnumerable(T)) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetEnumerator](GetEnumerator/MockEnumerable(T).GetEnumerator)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Extension Methods

NameDescription![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[AssertEqual<T>(IEnumerable<T>)](https://msdn.microsoft.com/en-us/library/m:reactivetests.extensions.assertequal%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.collections.generic.ienumerable%7b%60%600%7d)(v=VS.103))Overloaded. (Defined by [Extensions](Extensions/Extensions).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[AssertEqual<T>(array<T[])](https://msdn.microsoft.com/en-us/library/m:reactivetests.extensions.assertequal%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2c%60%600%5b%5d)(v=VS.103))Overloaded. (Defined by [Extensions](Extensions/Extensions).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Subscribe<T>(IObserver<T>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.subscribe%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.iobserver%7b%60%600%7d)(v=VS.103))Overloaded. Subscribes an observer to an enumerable sequence with the specified source and observer. (Defined by [Observable](Observable/Observable).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[Subscribe<T>(IObserver<T>, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.subscribe%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.iobserver%7b%60%600%7d%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Overloaded. Subscribes an observer to an enumerable sequence with the specified source and observer. (Defined by [Observable](Observable/Observable).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[ToObservable<T>()](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.toobservable%60%601(system.collections.generic.ienumerable%7b%60%600%7d)(v=VS.103))Overloaded. Converts an enumerable sequence to an observable sequence with a specified source. (Defined by [Observable](Observable/Observable).)![Public Extension Method](https://reactiveui.net/assets/img/Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[ToObservable<T>(IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.toobservable%60%601(system.collections.generic.ienumerable%7b%60%600%7d%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Overloaded. Converts an enumerable sequence to an observable sequence with a specified source and scheduler. (Defined by [Observable](Observable/Observable).)Top

## Explicit Interface Implementations

NameDescription![Explicit interface implemetation](https://reactiveui.net/assets/img/Hh212009.pubinterface(en-us,VS.103).gif "Explicit interface implemetation")![Private method](https://reactiveui.net/assets/img/Hh314705.privmethod(en-us,VS.103).gif "Private method")[IEnumerable.GetEnumerator](IEnumerable.GetEnumerator/MockEnumerable(T).IEnumerable.GetEnumerator)Top

## See Also

#### Reference

[MockEnumerable\<T\> Class](MockEnumerable/MockEnumerable(T))

[ReactiveTests Namespace](ReactiveTests/ReactiveTests)







# MockEnumerable\<T\> Fields

Include Protected Members  
Include Inherited Members

The [MockEnumerable\<T\>](MockEnumerable/MockEnumerable(T)) type exposes the following members.

## Fields

NameDescription![Public field](https://reactiveui.net/assets/img/Hh314728.pubfield(en-us,VS.103).gif "Public field")[Scheduler](Scheduler/MockEnumerable(T).Scheduler)![Public field](https://reactiveui.net/assets/img/Hh314728.pubfield(en-us,VS.103).gif "Public field")[Subscriptions](Subscriptions/MockEnumerable(T).Subscriptions)Top

## See Also

#### Reference

[MockEnumerable\<T\> Class](MockEnumerable/MockEnumerable(T))

[ReactiveTests Namespace](ReactiveTests/ReactiveTests)





# MockEnumerable\<T\> Constructor

**Namespace:**  [ReactiveTests](ReactiveTests/ReactiveTests)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New ( _
    scheduler As TestScheduler, _
    underlyingEnumerable As IEnumerable(Of T) _
)
```

```vb
'Usage
Dim scheduler As TestScheduler
Dim underlyingEnumerable As IEnumerable(Of T)

Dim instance As New MockEnumerable(scheduler, _
    underlyingEnumerable)
```

```csharp
public MockEnumerable(
    TestScheduler scheduler,
    IEnumerable<T> underlyingEnumerable
)
```

```c++
public:
MockEnumerable(
    TestScheduler^ scheduler, 
    IEnumerable<T>^ underlyingEnumerable
)
```

```fsharp
new : 
        scheduler:TestScheduler * 
        underlyingEnumerable:IEnumerable<'T> -> MockEnumerable
```

```jscript
public function MockEnumerable(
    scheduler : TestScheduler, 
    underlyingEnumerable : IEnumerable<T>
)
```

#### Parameters

- scheduler  
  Type: [Microsoft.Reactive.Testing.TestScheduler](TestScheduler/TestScheduler)

- underlyingEnumerable  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[T](MockEnumerable/MockEnumerable(T))\>

## See Also

#### Reference

[MockEnumerable\<T\> Class](MockEnumerable/MockEnumerable(T))

[ReactiveTests Namespace](ReactiveTests/ReactiveTests)





