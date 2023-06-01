title: TimeInterval<T>.Inequality Operator
---
# TimeInterval\<T\>.Inequality Operator

Indicates whether first and second arguments are not equal.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Operator <> ( _
    first As TimeInterval(Of T), _
    second As TimeInterval(Of T) _
) As Boolean
```

```vb
'Usage
Dim first As TimeInterval(Of T)
Dim second As TimeInterval(Of T)
Dim returnValue As Boolean

returnValue = (first <> second)
```

```csharp
public static bool operator !=(
    TimeInterval<T> first,
    TimeInterval<T> second
)
```

```c++
public:
static bool operator !=(
    TimeInterval<T> first, 
    TimeInterval<T> second
)
```

```fsharp
static let inline (<>)
        first:TimeInterval<'T> * 
        second:TimeInterval<'T>  : bool
```

```javascript
JScript supports the use of overloaded operators, but not the declaration of new ones.
```

#### Parameters

- first  
  Type: [System.Reactive.TimeInterval](TimeInterval/TimeInterval(T))\<[T](TimeInterval/TimeInterval(T))\>  
  The first argument.

- second  
  Type: [System.Reactive.TimeInterval](TimeInterval/TimeInterval(T))\<[T](TimeInterval/TimeInterval(T))\>  
  The second argument.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if both arguments are not equal; otherwise, false.

## See Also

#### Reference

[TimeInterval\<T\> Structure](TimeInterval/TimeInterval(T))

[System.Reactive Namespace](System.Reactive/System.Reactive)






