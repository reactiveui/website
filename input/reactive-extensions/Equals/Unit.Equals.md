# Unit.Equals Method

Include Protected Members  
Include Inherited Members

Check equality between the current unit and the other objects.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.unit.equals(system.object)(v=VS.103))Indicates whether the current unit is equal to the specified object. (Overrides [ValueType.Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.valuetype.equals(system.object)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Unit)](https://msdn.microsoft.com/en-us/library/m:system.reactive.unit.equals(system.reactive.unit)(v=VS.103))Indicates whether the current unit is equal to the specified unit.Top

## See Also

#### Reference

[Unit Structure](Unit\Unit.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)





# Unit.Equals Method (Object)

Indicates whether the current unit is equal to the specified object.

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
Dim instance As Unit
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
  The object to check equality with the current unit.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if the current unit is equal to the specified object; otherwise, false.

## See Also

#### Reference

[Unit Structure](Unit\Unit.md)

[Equals Overload](Equals\Unit.Equals.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)







# Unit.Equals Method (Unit)

Indicates whether the current unit is equal to the specified unit.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Equals ( _
    other As Unit _
) As Boolean
```

```vb
'Usage
Dim instance As Unit
Dim other As Unit
Dim returnValue As Boolean

returnValue = instance.Equals(other)
```

```csharp
public bool Equals(
    Unit other
)
```

```c++
public:
virtual bool Equals(
    Unit other
) sealed
```

```fsharp
abstract Equals : 
        other:Unit -> bool 
override Equals : 
        other:Unit -> bool 
```

```jscript
public final function Equals(
    other : Unit
) : boolean
```

#### Parameters

- other  
  Type: [System.Reactive.Unit](Unit\Unit.md)  
  The other unit to check equality with the current unit.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
Always returns true.

#### Implements

[IEquatable\<T\>.Equals(T)](https://msdn.microsoft.com/en-us/library/m:system.iequatable%601.equals(%600)(v=VS.103))

## See Also

#### Reference

[Unit Structure](Unit\Unit.md)

[Equals Overload](Equals\Unit.Equals.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)







