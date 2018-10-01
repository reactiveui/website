# TimeInterval\<T\>.Equals Method

Indicates whether this instance and a specified object are equal.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Overrides Function Equals ( _
    obj As Object _
) As Boolean
```

```vb
'Usage
Dim instance As TimeInterval
Dim obj As Object
Dim returnValue As Boolean

returnValue = instance.Equals(obj)
```

```csharp
public override bool Equals(
    Object obj
)
```

```c++
public:
virtual bool Equals(
    Object^ obj
) override
```

```fsharp
abstract Equals : 
        obj:Object -> bool 
override Equals : 
        obj:Object -> bool 
```

```jscript
public override function Equals(
    obj : Object
) : boolean
```

#### Parameters

- obj  
  Type: [System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  The object to check equality with this instance.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if this instance and the given object are equal; otherwise, false.

## See Also

#### Reference

[TimeInterval\<T\> Structure](TimeInterval\TimeInterval(T).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)






