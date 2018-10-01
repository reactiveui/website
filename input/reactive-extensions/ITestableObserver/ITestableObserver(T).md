# ITestableObserver\<T\> Properties

Include Protected Members  
Include Inherited Members

The [ITestableObserver\<T\>](ITestableObserver\ITestableObserver(T).md) type exposes the following members.

## Properties

NameDescription![Public property](images\Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Messages](Messages\ITestableObserver(T).Messages.md)Gets the recorded notifications received by the observer.Top

## See Also

#### Reference

[ITestableObserver\<T\> Interface](ITestableObserver\ITestableObserver(T).md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)





# ITestableObserver\<T\> Interface

Defines an observer that records received notifications.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Interface ITestableObserver(Of T) _
    Inherits IObserver(Of T)
```

```vb
'Usage
Dim instance As ITestableObserver(Of T)
```

```csharp
public interface ITestableObserver<T> : IObserver<T>
```

```c++
generic<typename T>
public interface class ITestableObserver : IObserver<T>
```

```fsharp
type ITestableObserver<'T> =  
    interface
        interface IObserver<'T>
    end
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

The ITestableObserver\<T\> type exposes the following members.

## Properties

NameDescription![Public property](images\Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Messages](Messages\ITestableObserver(T).Messages.md)Gets the recorded notifications received by the observer.Top

## Methods

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[OnCompleted](https://msdn.microsoft.com/en-us/library/Dd782982)(Inherited from [IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)<T>.)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[OnError](https://msdn.microsoft.com/en-us/library/m:system.iobserver%601.onerror(system.exception)(v=VS.103))(Inherited from [IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)<T>.)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[OnNext](https://msdn.microsoft.com/en-us/library/m:system.iobserver%601.onnext(%600)(v=VS.103))(Inherited from [IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)<T>.)Top

## Extension Methods

NameDescription![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[AsObserver<T>](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.asobserver%60%601(system.iobserver%7b%60%600%7d)(v=VS.103))Hides the identity of an observer. (Defined by [Observer](Observer\Observer.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[ToNotifier<T>](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.tonotifier%60%601(system.iobserver%7b%60%600%7d)(v=VS.103))Creates a notification callback from an observer. (Defined by [Observer](Observer\Observer.md).)Top

## See Also

#### Reference

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)









# ITestableObserver\<T\> Methods

Include Protected Members  
Include Inherited Members

The [ITestableObserver\<T\>](ITestableObserver\ITestableObserver(T).md) type exposes the following members.

## Methods

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[OnCompleted](https://msdn.microsoft.com/en-us/library/Dd782982)(Inherited from [IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)<[T](ITestableObserver\ITestableObserver(T).md)>.)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[OnError](https://msdn.microsoft.com/en-us/library/m:system.iobserver%601.onerror(system.exception)(v=VS.103))(Inherited from [IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)<[T](ITestableObserver\ITestableObserver(T).md)>.)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[OnNext](https://msdn.microsoft.com/en-us/library/m:system.iobserver%601.onnext(%600)(v=VS.103))(Inherited from [IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)<[T](ITestableObserver\ITestableObserver(T).md)>.)Top

## Extension Methods

NameDescription![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[AsObserver<T>](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.asobserver%60%601(system.iobserver%7b%60%600%7d)(v=VS.103))Hides the identity of an observer. (Defined by [Observer](Observer\Observer.md).)![Public Extension Method](images\Hh229625.pubextension(en-us,VS.103).gif "Public Extension Method")[ToNotifier<T>](https://msdn.microsoft.com/en-us/library/m:system.reactive.observer.tonotifier%60%601(system.iobserver%7b%60%600%7d)(v=VS.103))Creates a notification callback from an observer. (Defined by [Observer](Observer\Observer.md).)Top

## See Also

#### Reference

[ITestableObserver\<T\> Interface](ITestableObserver\ITestableObserver(T).md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)





