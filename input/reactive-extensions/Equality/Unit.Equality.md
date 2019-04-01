# Unit.Equality Operator

Indicates whether first and second arguments are equal.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Operator = ( _
    first As Unit, _
    second As Unit _
) As Boolean
```

```vb
'Usage
Dim first As Unit
Dim second As Unit
Dim returnValue As Boolean

returnValue = (first = second)
```

```csharp
public static bool operator ==(
    Unit first,
    Unit second
)
```

```c++
public:
static bool operator ==(
    Unit first, 
    Unit second
)
```

```fsharp
static let inline (=)
        first:Unit * 
        second:Unit  : bool
```

```jscript
JScript supports the use of overloaded operators, but not the declaration of new ones.
```

#### Parameters

- first  
  Type: [System.Reactive.Unit](Unit\Unit.md)  
  The first Unit to compare, or null.

- second  
  Type: [System.Reactive.Unit](Unit\Unit.md)  
  The second Unit to compare, or null.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
Always returns true.

## See Also

#### Reference

[Unit Structure](Unit\Unit.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)






