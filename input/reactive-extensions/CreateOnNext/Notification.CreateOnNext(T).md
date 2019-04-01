# Notification.CreateOnNext\<T\> Method

Creates an object that represents an OnNext notification to an observer.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function CreateOnNext(Of T) ( _
    value As T _
) As Notification(Of T)
```

```vb
'Usage
Dim value As T
Dim returnValue As Notification(Of T)

returnValue = Notification.CreateOnNext(value)
```

```csharp
public static Notification<T> CreateOnNext<T>(
    T value
)
```

```c++
public:
generic<typename T>
static Notification<T>^ CreateOnNext(
    T value
)
```

```fsharp
static member CreateOnNext : 
        value:'T -> Notification<'T> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T  
  The notification argument type.

#### Parameters

- value  
  Type: T  
  The value contained in the notification.

#### Return Value

Type: [System.Reactive.Notification](Notification\Notification(T).md)\<T\>  
The OnNext notification containing the value.

## See Also

#### Reference

[Notification Class](Notification\Notification.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)







