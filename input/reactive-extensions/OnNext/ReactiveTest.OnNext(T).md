title: ReactiveTest.OnNext<T>()
---
# ReactiveTest.OnNext\<T\> Method

Factory method for a recorded OnNext notification at a given time with a given value.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Shared Function OnNext(Of T) ( _
    ticks As Long, _
    value As T _
) As Recorded(Of Notification(Of T))
```

```vb
'Usage
Dim ticks As Long
Dim value As T
Dim returnValue As Recorded(Of Notification(Of T))

returnValue = ReactiveTest.OnNext(ticks, _
    value)
```

```csharp
public static Recorded<Notification<T>> OnNext<T>(
    long ticks,
    T value
)
```

```c++
public:
generic<typename T>
static Recorded<Notification<T>^> OnNext(
    long long ticks, 
    T value
)
```

```fsharp
static member OnNext : 
        ticks:int64 * 
        value:'T -> Recorded<Notification<'T>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

#### Parameters

- ticks  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  Recorded virtual time the OnNext notification occurs.

- value  
  Type: T  
  Recorded value stored in the OnNext notification.

#### Return Value

Type: [Microsoft.Reactive.Testing.Recorded](Recorded/Recorded(T))\<[Notification](Notification/Notification(T))\<T\>\>  
Recorded OnNext notification.

## See Also

#### Reference

[ReactiveTest Class](ReactiveTest/ReactiveTest)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)
