title: Timestamped<T>.Equality Operator
---
# Timestamped\<T\>.Equality Operator

Indicates whether first and second arguments are equal.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Operator = ( _
    first As Timestamped(Of T), _
    second As Timestamped(Of T) _
) As Boolean
```

```vb
'Usage
Dim first As Timestamped(Of T)
Dim second As Timestamped(Of T)
Dim returnValue As Boolean

returnValue = (first = second)
```

```csharp
public static bool operator ==(
    Timestamped<T> first,
    Timestamped<T> second
)
```

```c++
public:
static bool operator ==(
    Timestamped<T> first, 
    Timestamped<T> second
)
```

```fsharp
static let inline (=)
        first:Timestamped<'T> * 
        second:Timestamped<'T>  : bool
```

```jscript
JScript supports the use of overloaded operators, but not the declaration of new ones.
```

#### Parameters

- first  
  Type: [System.Reactive.Timestamped](Timestamped/Timestamped(T))\<[T](Timestamped/Timestamped(T))\>  
  The first argument.

- second  
  Type: [System.Reactive.Timestamped](Timestamped/Timestamped(T))\<[T](Timestamped/Timestamped(T))\>  
  The second argument.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if both arguments are equal; otherwise, false.

## See Also

#### Reference

[Timestamped\<T\> Structure](Timestamped/Timestamped(T))

[System.Reactive Namespace](System.Reactive/System.Reactive)






