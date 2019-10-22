title: Unit Operators
---
# Unit Operators

Include Protected Members  
Include Inherited Members

The [Unit](Unit\Unit.md) type exposes the following members.

## Operators

NameDescription![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Equality](https://msdn.microsoft.com/en-us/library/m:system.reactive.unit.op_equality(system.reactive.unit%2csystem.reactive.unit)(v=VS.103))Indicates whether first and second arguments are equal.![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Inequality](https://msdn.microsoft.com/en-us/library/m:system.reactive.unit.op_inequality(system.reactive.unit%2csystem.reactive.unit)(v=VS.103))Indicates whether first and second arguments are not equal.Top

## See Also

#### Reference

[Unit Structure](Unit\Unit.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)

# Unit Methods

Include Protected Members  
Include Inherited Members

The [Unit](Unit\Unit.md) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.unit.equals(system.object)(v=VS.103))Indicates whether the current unit is equal to the specified object. (Overrides [ValueType.Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.valuetype.equals(system.object)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Unit)](https://msdn.microsoft.com/en-us/library/m:system.reactive.unit.equals(system.reactive.unit)(v=VS.103))Indicates whether the current unit is equal to the specified unit.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](GetHashCode\Unit.GetHashCode.md)Gets the unit value's hash code. (Overrides [ValueType.GetHashCode()](https://msdn.microsoft.com/en-us/library/y3509fc2).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/wb77sz3h)(Inherited from [ValueType](https://msdn.microsoft.com/en-us/library/aey3s293).)Top

## See Also

#### Reference

[Unit Structure](Unit\Unit.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)

# Unit Properties

Include Protected Members  
Include Inherited Members

The [Unit](Unit\Unit.md) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Default](Default\Unit.Default.md)Gets the single unit value.Top

## See Also

#### Reference

[Unit Structure](Unit\Unit.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)

# Unit Structure

Represents void.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<SerializableAttribute> _
Public Structure Unit _
    Implements IEquatable(Of Unit)
```

```vb
'Usage
Dim instance As Unit
```

```csharp
[SerializableAttribute]
public struct Unit : IEquatable<Unit>
```

```c++
[SerializableAttribute]
public value class Unit : IEquatable<Unit>
```

```fsharp
[<SealedAttribute>]
[<SerializableAttribute>]
type Unit =  
    struct
        interface IEquatable<Unit>
    end
```

```jscript
JScript suports the use of structures, but not the declaration of new ones.
```

The Unit type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Default](Default\Unit.Default.md)Gets the single unit value.Top

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.unit.equals(system.object)(v=VS.103))Indicates whether the current unit is equal to the specified object. (Overrides [ValueType.Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.valuetype.equals(system.object)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Unit)](https://msdn.microsoft.com/en-us/library/m:system.reactive.unit.equals(system.reactive.unit)(v=VS.103))Indicates whether the current unit is equal to the specified unit.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](GetHashCode\Unit.GetHashCode.md)Gets the unit value's hash code. (Overrides [ValueType.GetHashCode()](https://msdn.microsoft.com/en-us/library/y3509fc2).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/wb77sz3h)(Inherited from [ValueType](https://msdn.microsoft.com/en-us/library/aey3s293).)Top

## Operators

NameDescription![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Equality](https://msdn.microsoft.com/en-us/library/m:system.reactive.unit.op_equality(system.reactive.unit%2csystem.reactive.unit)(v=VS.103))Indicates whether first and second arguments are equal.![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Inequality](https://msdn.microsoft.com/en-us/library/m:system.reactive.unit.op_inequality(system.reactive.unit%2csystem.reactive.unit)(v=VS.103))Indicates whether first and second arguments are not equal.Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive Namespace](System.Reactive\System.Reactive.md)
