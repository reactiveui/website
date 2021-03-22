title: Notification<T>.HasValue Property
---
# Notification\<T\>.HasValue Property

Returns a value that indicates whether the notification has a value.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public MustOverride ReadOnly Property HasValue As Boolean
    Get
```

```vb
'Usage
Dim instance As Notification
Dim value As Boolean

value = instance.HasValue
```

```csharp
public abstract bool HasValue { get; }
```

```c++
public:
virtual property bool HasValue {
    bool get () abstract;
}
```

```fsharp
abstract HasValue : bool
```

```javascript
abstract function get HasValue () : boolean
```

#### Property Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if notification has a value; otherwise, false.

## See Also

#### Reference

[Notification\<T\> Class](Notification/Notification(T))

[System.Reactive Namespace](System.Reactive/System.Reactive)





