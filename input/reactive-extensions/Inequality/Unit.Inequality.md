title: Unit.Inequality Operator
---
# Unit.Inequality Operator

Indicates whether first and second arguments are not equal.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Operator <> ( _
    first As Unit, _
    second As Unit _
) As Boolean
```

```vb
'Usage
Dim first As Unit
Dim second As Unit
Dim returnValue As Boolean

returnValue = (first <> second)
```

```csharp
public static bool operator !=(
    Unit first,
    Unit second
)
```

```c++
public:
static bool operator !=(
    Unit first, 
    Unit second
)
```

```fsharp
static let inline (<>)
        first:Unit * 
        second:Unit  : bool
```

```javascript
JScript supports the use of overloaded operators, but not the declaration of new ones.
```

#### Parameters

- first  
  Type: [System.Reactive.Unit](Unit/Unit)  
  The first Unit to compare, or null.

- second  
  Type: [System.Reactive.Unit](Unit/Unit)  
  The second Unit to compare, or null.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
Always returns false.

## See Also

#### Reference

[Unit Structure](Unit/Unit)

[System.Reactive Namespace](System.Reactive/System.Reactive)






