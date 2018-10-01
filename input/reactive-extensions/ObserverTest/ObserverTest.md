# ObserverTest Class

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  [Microsoft.Reactive.Testing.ReactiveTest](ReactiveTest\ReactiveTest.md)  
    ReactiveTests.Tests.ObserverTest

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
<TestClassAttribute> _
Public Class ObserverTest _
    Inherits ReactiveTest
```

```vb
'Usage
Dim instance As ObserverTest
```

```csharp
[TestClassAttribute]
public class ObserverTest : ReactiveTest
```

```c++
[TestClassAttribute]
public ref class ObserverTest : public ReactiveTest
```

```fsharp
[<TestClassAttribute>]
type ObserverTest =  
    class
        inherit ReactiveTest
    end
```

```jscript
public class ObserverTest extends ReactiveTest
```

The ObserverTest type exposes the following members.

## Constructors

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ObserverTest](ObserverTest\ObserverTest.md)Top

## Methods

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AsObserver\_ArgumentChecking](AsObserver\ObserverTest.AsObserver_ArgumentChecking.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AsObserver\_Forwards](AsObserver\ObserverTest.AsObserver_Forwards.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AsObserver\_Hides](AsObserver\ObserverTest.AsObserver_Hides.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_ArgumentChecking](Create\ObserverTest.Create_ArgumentChecking.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNext](Create\ObserverTest.Create_OnNext.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNext\_HasError](Create\ObserverTest.Create_OnNext_HasError.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNextOnCompleted](Create\ObserverTest.Create_OnNextOnCompleted.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNextOnCompleted\_HasError](Create\ObserverTest.Create_OnNextOnCompleted_HasError.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNextOnError](Create\ObserverTest.Create_OnNextOnError.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNextOnError\_HitCompleted](Create\ObserverTest.Create_OnNextOnError_HitCompleted.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNextOnErrorOnCompleted1](Create\ObserverTest.Create_OnNextOnErrorOnCompleted1.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNextOnErrorOnCompleted2](Create\ObserverTest.Create_OnNextOnErrorOnCompleted2.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToNotifier\_ArgumentChecking](ToNotifier\ObserverTest.ToNotifier_ArgumentChecking.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToNotifier\_Forwards](ToNotifier\ObserverTest.ToNotifier_Forwards.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToObserver\_ArgumentChecking](ToObserver\ObserverTest.ToObserver_ArgumentChecking.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToObserver\_NotificationOnCompleted](ToObserver\ObserverTest.ToObserver_NotificationOnCompleted.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToObserver\_NotificationOnError](ToObserver\ObserverTest.ToObserver_NotificationOnError.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToObserver\_NotificationOnNext](ToObserver\ObserverTest.ToObserver_NotificationOnNext.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)









# ObserverTest Constructor

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New
```

```vb
'Usage

Dim instance As New ObserverTest()
```

```csharp
public ObserverTest()
```

```c++
public:
ObserverTest()
```

```fsharp
new : unit -> ObserverTest
```

```jscript
public function ObserverTest()
```

## See Also

#### Reference

[ObserverTest Class](ObserverTest\ObserverTest.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)





# ObserverTest Methods

Include Protected Members  
Include Inherited Members

The [ObserverTest](ObserverTest\ObserverTest.md) type exposes the following members.

## Methods

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AsObserver\_ArgumentChecking](AsObserver\ObserverTest.AsObserver_ArgumentChecking.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AsObserver\_Forwards](AsObserver\ObserverTest.AsObserver_Forwards.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[AsObserver\_Hides](AsObserver\ObserverTest.AsObserver_Hides.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_ArgumentChecking](Create\ObserverTest.Create_ArgumentChecking.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNext](Create\ObserverTest.Create_OnNext.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNext\_HasError](Create\ObserverTest.Create_OnNext_HasError.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNextOnCompleted](Create\ObserverTest.Create_OnNextOnCompleted.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNextOnCompleted\_HasError](Create\ObserverTest.Create_OnNextOnCompleted_HasError.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNextOnError](Create\ObserverTest.Create_OnNextOnError.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNextOnError\_HitCompleted](Create\ObserverTest.Create_OnNextOnError_HitCompleted.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNextOnErrorOnCompleted1](Create\ObserverTest.Create_OnNextOnErrorOnCompleted1.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Create\_OnNextOnErrorOnCompleted2](Create\ObserverTest.Create_OnNextOnErrorOnCompleted2.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103))(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](images\Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToNotifier\_ArgumentChecking](ToNotifier\ObserverTest.ToNotifier_ArgumentChecking.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToNotifier\_Forwards](ToNotifier\ObserverTest.ToNotifier_Forwards.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToObserver\_ArgumentChecking](ToObserver\ObserverTest.ToObserver_ArgumentChecking.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToObserver\_NotificationOnCompleted](ToObserver\ObserverTest.ToObserver_NotificationOnCompleted.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToObserver\_NotificationOnError](ToObserver\ObserverTest.ToObserver_NotificationOnError.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToObserver\_NotificationOnNext](ToObserver\ObserverTest.ToObserver_NotificationOnNext.md)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## See Also

#### Reference

[ObserverTest Class](ObserverTest\ObserverTest.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)




