title: Subscription.Inequality Operator
---
# Subscription.Inequality Operator

Checks whether the two given subscription objects are not equal.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Shared Operator <> ( _
    left As Subscription, _
    right As Subscription _
) As Boolean
```

```vb
'Usage
Dim left As Subscription
Dim right As Subscription
Dim returnValue As Boolean

returnValue = (left <> right)
```

```csharp
public static bool operator !=(
    Subscription left,
    Subscription right
)
```

```c++
public:
static bool operator !=(
    Subscription left, 
    Subscription right
)
```

```fsharp
static let inline (<>)
        left:Subscription * 
        right:Subscription  : bool
```

```jscript
JScript supports the use of overloaded operators, but not the declaration of new ones.
```

#### Parameters

- left  
  Type: [Microsoft.Reactive.Testing.Subscription](Subscription\Subscription.md)  
  The first object to check for equality.

- right  
  Type: [Microsoft.Reactive.Testing.Subscription](Subscription\Subscription.md)  
  The second object to check for equality.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if both objects are not equal; otherwise, false.

## See Also

#### Reference

[Subscription Structure](Subscription\Subscription.md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)






