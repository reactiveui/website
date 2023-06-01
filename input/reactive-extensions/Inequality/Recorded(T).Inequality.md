title: Recorded<T>.Inequality Operator
---
# Recorded\<T\>.Inequality Operator

Checks whether the two given recorded objects are not equal.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Shared Operator <> ( _
    left As Recorded(Of T), _
    right As Recorded(Of T) _
) As Boolean
```

```vb
'Usage
Dim left As Recorded(Of T)
Dim right As Recorded(Of T)
Dim returnValue As Boolean

returnValue = (left <> right)
```

```csharp
public static bool operator !=(
    Recorded<T> left,
    Recorded<T> right
)
```

```c++
public:
static bool operator !=(
    Recorded<T> left, 
    Recorded<T> right
)
```

```fsharp
static let inline (<>)
        left:Recorded<'T> * 
        right:Recorded<'T>  : bool
```

```javascript
JScript supports the use of overloaded operators, but not the declaration of new ones.
```

#### Parameters

- left  
  Type: [Microsoft.Reactive.Testing.Recorded](Recorded/Recorded(T))\<[T](Recorded/Recorded(T))\>  
  First object to check for equality.

- right  
  Type: [Microsoft.Reactive.Testing.Recorded](Recorded/Recorded(T))\<[T](Recorded/Recorded(T))\>  
  Second object to check for equality.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
True if both objects are inequal; otherwise, false.

## See Also

#### Reference

[Recorded\<T\> Structure](Recorded/Recorded(T))

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)






