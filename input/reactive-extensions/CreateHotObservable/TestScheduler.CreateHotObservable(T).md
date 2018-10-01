# TestScheduler.CreateHotObservable\<T\> Method

Creates a hot observable.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Function CreateHotObservable(Of T) ( _
    ParamArray messages As Recorded(Of Notification(Of T))() _
) As ITestableObservable(Of T)
```

```vb
'Usage
Dim instance As TestScheduler
Dim messages As Recorded(Of Notification(Of T))()
Dim returnValue As ITestableObservable(Of T)

returnValue = instance.CreateHotObservable(messages)
```

```csharp
public ITestableObservable<T> CreateHotObservable<T>(
    params Recorded<Notification<T>>[] messages
)
```

```c++
public:
generic<typename T>
ITestableObservable<T>^ CreateHotObservable(
    ... array<Recorded<Notification<T>^>>^ messages
)
```

```fsharp
member CreateHotObservable : 
        messages:Recorded<Notification<'T>>[] -> ITestableObservable<'T> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

#### Parameters

- messages  
  Type: array\<[Microsoft.Reactive.Testing.Recorded](Recorded\Recorded(T).md)\<[Notification](Notification\Notification(T).md)\<T\>\>\[\]  
  Notifications to surface through the created sequence.

#### Return Value

Type: [Microsoft.Reactive.Testing.ITestableObservable](ITestableObservable\ITestableObservable(T).md)\<T\>  
Hot observable exhibiting the specified message behavior.

## See Also

#### Reference

[TestScheduler Class](TestScheduler\TestScheduler.md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)







