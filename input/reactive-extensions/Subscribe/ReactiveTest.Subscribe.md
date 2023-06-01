title: ReactiveTest.Subscribe(Int64)
---
# ReactiveTest.Subscribe Method (Int64)

Factory method for a recorded subscription.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Shared Function Subscribe ( _
    start As Long _
) As Subscription
```

```vb
'Usage
Dim start As Long
Dim returnValue As Subscription

returnValue = ReactiveTest.Subscribe(start)
```

```csharp
public static Subscription Subscribe(
    long start
)
```

```c++
public:
static Subscription Subscribe(
    long long start
)
```

```fsharp
static member Subscribe : 
        start:int64 -> Subscription 
```

```javascript
public static function Subscribe(
    start : long
) : Subscription
```

#### Parameters

- start  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  The virtual time indicating when the subscription was created.

#### Return Value

Type: [Microsoft.Reactive.Testing.Subscription](Subscription/Subscription)  
The subscription object.

## See Also

#### Reference

[ReactiveTest Class](ReactiveTest/ReactiveTest)

[Subscribe Overload](Subscribe/ReactiveTest.Subscribe)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

# ReactiveTest.Subscribe Method

Include Protected Members  
Include Inherited Members

Factory method for a recorded subscription.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe(Int64)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactivetest.subscribe(system.int64)(v=VS.103))Factory method for a recorded subscription.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Subscribe(Int64, Int64)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.reactivetest.subscribe(system.int64%2csystem.int64)(v=VS.103))Factory method for a recorded subscription.Top

## See Also

#### Reference

[ReactiveTest Class](ReactiveTest/ReactiveTest)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

# ReactiveTest.Subscribe Method (Int64, Int64)

Factory method for a recorded subscription.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Shared Function Subscribe ( _
    start As Long, _
    end As Long _
) As Subscription
```

```vb
'Usage
Dim start As Long
Dim end As Long
Dim returnValue As Subscription

returnValue = ReactiveTest.Subscribe(start, _
    end)
```

```csharp
public static Subscription Subscribe(
    long start,
    long end
)
```

```c++
public:
static Subscription Subscribe(
    long long start, 
    long long end
)
```

```fsharp
static member Subscribe : 
        start:int64 * 
        end:int64 -> Subscription 
```

```javascript
public static function Subscribe(
    start : long, 
    end : long
) : Subscription
```

#### Parameters

- start  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  The virtual time indicating when the subscription was created.

- end  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  The virtual time indicating when the subscription was disposed.

#### Return Value

Type: [Microsoft.Reactive.Testing.Subscription](Subscription/Subscription)  
The subscription object.

## See Also

#### Reference

[ReactiveTest Class](ReactiveTest/ReactiveTest)

[Subscribe Overload](Subscribe/ReactiveTest.Subscribe)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)
