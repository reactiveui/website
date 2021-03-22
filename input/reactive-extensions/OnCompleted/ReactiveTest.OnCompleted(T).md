title: ReactiveTest.OnCompleted<T>()
---
# ReactiveTest.OnCompleted\<T\> Method

Factory method for a recorded OnCompleted notification at a given time.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Shared Function OnCompleted(Of T) ( _
    ticks As Long _
) As Recorded(Of Notification(Of T))
```

```vb
'Usage
Dim ticks As Long
Dim returnValue As Recorded(Of Notification(Of T))

returnValue = ReactiveTest.OnCompleted(ticks)
```

```csharp
public static Recorded<Notification<T>> OnCompleted<T>(
    long ticks
)
```

```c++
public:
generic<typename T>
static Recorded<Notification<T>^> OnCompleted(
    long long ticks
)
```

```fsharp
static member OnCompleted : 
        ticks:int64 -> Recorded<Notification<'T>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

#### Parameters

- ticks  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  Recorded virtual time the OnCompleted notification occurs.

#### Return Value

Type: [Microsoft.Reactive.Testing.Recorded](Recorded/Recorded(T))\<[Notification](Notification/Notification(T))\<T\>\>  
Recorded OnCompleted notification.

## See Also

#### Reference

[ReactiveTest Class](ReactiveTest/ReactiveTest)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)
