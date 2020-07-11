title: BehaviorSubject<T>.OnError()
---
# BehaviorSubject\<T\>.OnError Method

Notifies all subscribed observers with the exception.

**Namespace:**  [System.Reactive.Subjects](System.Reactive.Subjects/System.Reactive.Subjects)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub OnError ( _
    error As Exception _
)
```

```vb
'Usage
Dim instance As BehaviorSubject
Dim error As Exception

instance.OnError(error)
```

```csharp
public void OnError(
    Exception error
)
```

```c++
public:
virtual void OnError(
    Exception^ error
) sealed
```

```fsharp
abstract OnError : 
        error:Exception -> unit 
override OnError : 
        error:Exception -> unit 
```

```jscript
public final function OnError(
    error : Exception
)
```

#### Parameters

- error  
  Type: [System.Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)  
  The exception to send to all subscribed observers.

#### Implements

[IObserver\<T\>.OnError(Exception)](https://msdn.microsoft.com/en-us/library/m:system.iobserver%601.onerror(system.exception)(v=VS.103))

## See Also

#### Reference

[BehaviorSubject\<T\> Class](BehaviorSubject/BehaviorSubject(T))

[System.Reactive.Subjects Namespace](System.Reactive.Subjects/System.Reactive.Subjects)
