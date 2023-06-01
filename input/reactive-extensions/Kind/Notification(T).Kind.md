title: Notification<T>.Kind Property
---
# Notification\<T\>.Kind Property

Gets the kind of notification that is represented.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public MustOverride ReadOnly Property Kind As NotificationKind
    Get
```

```vb
'Usage
Dim instance As Notification
Dim value As NotificationKind

value = instance.Kind
```

```csharp
public abstract NotificationKind Kind { get; }
```

```c++
public:
virtual property NotificationKind Kind {
    NotificationKind get () abstract;
}
```

```fsharp
abstract Kind : NotificationKind
```

```javascript
abstract function get Kind () : NotificationKind
```

#### Property Value

Type: [System.Reactive.NotificationKind](NotificationKind/NotificationKind)  
One of the enumeration values that indicates the type of a notification.

## See Also

#### Reference

[Notification\<T\> Class](Notification/Notification(T))

[System.Reactive Namespace](System.Reactive/System.Reactive)





