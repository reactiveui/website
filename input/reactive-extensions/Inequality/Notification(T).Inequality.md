title: Notification<T>.Inequality Operator
---
# Notification\<T\>.Inequality Operator

Indicates whether left and right arguments are not equal.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Operator <> ( _
    left As Notification(Of T), _
    right As Notification(Of T) _
) As Boolean
```

```vb
'Usage
Dim left As Notification(Of T)
Dim right As Notification(Of T)
Dim returnValue As Boolean

returnValue = (left <> right)
```

```csharp
public static bool operator !=(
    Notification<T> left,
    Notification<T> right
)
```

```c++
public:
static bool operator !=(
    Notification<T>^ left, 
    Notification<T>^ right
)
```

```fsharp
static let inline (<>)
        left:Notification<'T> * 
        right:Notification<'T>  : bool
```

```jscript
JScript supports the use of overloaded operators, but not the declaration of new ones.
```

#### Parameters

- left  
  Type: [System.Reactive.Notification](Notification\Notification(T).md)\<[T](Notification\Notification(T).md)\>  
  The left argument.

- right  
  Type: [System.Reactive.Notification](Notification\Notification(T).md)\<[T](Notification\Notification(T).md)\>  
  The right argument.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if both arguments are not equal; otherwise, false.

## See Also

#### Reference

[Notification\<T\> Class](Notification\Notification(T).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)






