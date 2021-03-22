title: Notification<T>.Value Property
---
# Notification\<T\>.Value Property

Returns the value of an OnNext notification or throws an exception.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public MustOverride ReadOnly Property Value As T
    Get
```

```vb
'Usage
Dim instance As Notification
Dim value As T

value = instance.Value
```

```csharp
public abstract T Value { get; }
```

```c++
public:
virtual property T Value {
    T get () abstract;
}
```

```fsharp
abstract Value : 'T
```

```javascript
abstract function get Value () : T
```

#### Property Value

Type: [T](Notification/Notification(T))  
The value of an OnNext notification.

## See Also

#### Reference

[Notification\<T\> Class](Notification/Notification(T))

[System.Reactive Namespace](System.Reactive/System.Reactive)
