title: ReactiveTest.OnError<T>()
---
# ReactiveTest.OnError\<T\> Method

Factory method for a recorded OnError notification at a given time with a given error.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Shared Function OnError(Of T) ( _
    ticks As Long, _
    exception As Exception _
) As Recorded(Of Notification(Of T))
```

```vb
'Usage
Dim ticks As Long
Dim exception As Exception
Dim returnValue As Recorded(Of Notification(Of T))

returnValue = ReactiveTest.OnError(ticks, _
    exception)
```

```csharp
public static Recorded<Notification<T>> OnError<T>(
    long ticks,
    Exception exception
)
```

```c++
public:
generic<typename T>
static Recorded<Notification<T>^> OnError(
    long long ticks, 
    Exception^ exception
)
```

```fsharp
static member OnError : 
        ticks:int64 * 
        exception:Exception -> Recorded<Notification<'T>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

#### Parameters

- ticks  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  Recorded virtual time the OnError notification occurs.

- exception  
  Type: [System.Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)  
  Recorded exception stored in the OnError notification.

#### Return Value

Type: [Microsoft.Reactive.Testing.Recorded](Recorded/Recorded(T))\<[Notification](Notification/Notification(T))\<T\>\>  
Recorded OnError notification.

## See Also

#### Reference

[ReactiveTest Class](ReactiveTest/ReactiveTest)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

