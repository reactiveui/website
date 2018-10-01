# Recorded\<T\>.Equality Operator

Checks whether the two given recorded objects are equal.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Shared Operator = ( _
    left As Recorded(Of T), _
    right As Recorded(Of T) _
) As Boolean
```

```vb
'Usage
Dim left As Recorded(Of T)
Dim right As Recorded(Of T)
Dim returnValue As Boolean

returnValue = (left = right)
```

```csharp
public static bool operator ==(
    Recorded<T> left,
    Recorded<T> right
)
```

```c++
public:
static bool operator ==(
    Recorded<T> left, 
    Recorded<T> right
)
```

```fsharp
static let inline (=)
        left:Recorded<'T> * 
        right:Recorded<'T>  : bool
```

```jscript
JScript supports the use of overloaded operators, but not the declaration of new ones.
```

#### Parameters

- left  
  Type: [Microsoft.Reactive.Testing.Recorded](Recorded\Recorded(T).md)\<[T](Recorded\Recorded(T).md)\>  
  First object to check for equality.

- right  
  Type: [Microsoft.Reactive.Testing.Recorded](Recorded\Recorded(T).md)\<[T](Recorded\Recorded(T).md)\>  
  Second object to check for equality.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
True if both objects are equal; otherwise, false.

## See Also

#### Reference

[Recorded\<T\> Structure](Recorded\Recorded(T).md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)






