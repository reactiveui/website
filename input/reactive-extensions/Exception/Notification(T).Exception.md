title: Notification<T>.Exception Property
---
# Notification\<T\>.Exception Property

Returns the exception of an OnError notification or returns null.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public MustOverride ReadOnly Property Exception As Exception
    Get
```

```vb
'Usage
Dim instance As Notification
Dim value As Exception

value = instance.Exception
```

```csharp
public abstract Exception Exception { get; }
```

```c++
public:
virtual property Exception^ Exception {
    Exception^ get () abstract;
}
```

```fsharp
abstract Exception : Exception
```

```jscript
abstract function get Exception () : Exception
```

#### Property Value

Type: [System.Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)  
The exception of an OnError notification or null.

## See Also

#### Reference

[Notification\<T\> Class](Notification/Notification(T))

[System.Reactive Namespace](System.Reactive/System.Reactive)





