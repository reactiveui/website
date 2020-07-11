title: ITestableObserver<T>.Messages Property
---
# ITestableObserver\<T\>.Messages Property

Gets the recorded notifications received by the observer.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
ReadOnly Property Messages As IList(Of Recorded(Of Notification(Of T)))
    Get
```

```vb
'Usage
Dim instance As ITestableObserver
Dim value As IList(Of Recorded(Of Notification(Of T)))

value = instance.Messages
```

```csharp
IList<Recorded<Notification<T>>> Messages { get; }
```

```c++
property IList<Recorded<Notification<T>^>>^ Messages {
    IList<Recorded<Notification<T>^>>^ get ();
}
```

```fsharp
abstract Messages : IList<Recorded<Notification<'T>>>
```

```jscript
function get Messages () : IList<Recorded<Notification<T>>>
```

#### Property Value

Type: [System.Collections.Generic.IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<[Recorded](Recorded/Recorded(T))\<[Notification](Notification/Notification(T))\<[T](ITestableObserver/ITestableObserver(T))\>\>\>

## See Also

#### Reference

[ITestableObserver\<T\> Interface](ITestableObserver/ITestableObserver(T))

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)





