# ITestableObservable\<T\>.Messages Property

Gets the recorded notifications sent by the observable.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
ReadOnly Property Messages As IList(Of Recorded(Of Notification(Of T)))
    Get
```

```vb
'Usage
Dim instance As ITestableObservable
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

Type: [System.Collections.Generic.IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<[Recorded](Recorded\Recorded(T).md)\<[Notification](Notification\Notification(T).md)\<[T](ITestableObservable\ITestableObservable(T).md)\>\>\>

## See Also

#### Reference

[ITestableObservable\<T\> Interface](ITestableObservable\ITestableObservable(T).md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)





