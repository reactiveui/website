title: Notification.CreateOnCompleted<T>()
---
# Notification.CreateOnCompleted\<T\> Method

Creates an object that represents an OnCompleted notification to an observer.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function CreateOnCompleted(Of T) As Notification(Of T)
```

```vb
'Usage
Dim returnValue As Notification(Of T)

returnValue = Notification.CreateOnCompleted()
```

```csharp
public static Notification<T> CreateOnCompleted<T>()
```

```c++
public:
generic<typename T>
static Notification<T>^ CreateOnCompleted()
```

```fsharp
static member CreateOnCompleted : unit -> Notification<'T> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T  
  The notification argument type.

#### Return Value

Type: [System.Reactive.Notification](Notification/Notification(T))\<T\>  
The OnCompleted notification.

## See Also

#### Reference

[Notification Class](Notification/Notification)

[System.Reactive Namespace](System.Reactive/System.Reactive)






