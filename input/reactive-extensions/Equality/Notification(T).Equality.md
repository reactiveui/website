title: Notification<T>.Equality Operator
---
# Notification\<T\>.Equality Operator

Indicates whether left and right arguments are equal.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Operator = ( _
    left As Notification(Of T), _
    right As Notification(Of T) _
) As Boolean
```

```vb
'Usage
Dim left As Notification(Of T)
Dim right As Notification(Of T)
Dim returnValue As Boolean

returnValue = (left = right)
```

```csharp
public static bool operator ==(
    Notification<T> left,
    Notification<T> right
)
```

```c++
public:
static bool operator ==(
    Notification<T>^ left, 
    Notification<T>^ right
)
```

```fsharp
static let inline (=)
        left:Notification<'T> * 
        right:Notification<'T>  : bool
```

```javascript
JScript supports the use of overloaded operators, but not the declaration of new ones.
```

#### Parameters

- left  
  Type: [System.Reactive.Notification](Notification/Notification(T))\<[T](Notification/Notification(T))\>  
  The left argument.

- right  
  Type: [System.Reactive.Notification](Notification/Notification(T))\<[T](Notification/Notification(T))\>  
  The right argument.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if both arguments are equal; otherwise, false.

## See Also

#### Reference

[Notification\<T\> Class](Notification/Notification(T))

[System.Reactive Namespace](System.Reactive/System.Reactive)






