title: Notification.CreateOnError<T>()
---
# Notification.CreateOnError\<T\> Method

Creates an object that represents an OnError notification to an observer.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function CreateOnError(Of T) ( _
    error As Exception _
) As Notification(Of T)
```

```vb
'Usage
Dim error As Exception
Dim returnValue As Notification(Of T)

returnValue = Notification.CreateOnError(error)
```

```csharp
public static Notification<T> CreateOnError<T>(
    Exception error
)
```

```c++
public:
generic<typename T>
static Notification<T>^ CreateOnError(
    Exception^ error
)
```

```fsharp
static member CreateOnError : 
        error:Exception -> Notification<'T> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T  
  The notification argument type.

#### Parameters

- error  
  Type: [System.Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)  
  The exception contained in the notification.

#### Return Value

Type: [System.Reactive.Notification](Notification/Notification(T))\<T\>  
The OnError notification containing the exception.

## See Also

#### Reference

[Notification Class](Notification/Notification)

[System.Reactive Namespace](System.Reactive/System.Reactive)







