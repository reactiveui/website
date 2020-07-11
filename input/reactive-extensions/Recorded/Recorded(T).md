title: Recorded<T> Structure
---
# Recorded\<T\> Structure

Records a value with the time it was produced on.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
<SerializableAttribute> _
Public Structure Recorded(Of T) _
    Implements IEquatable(Of Recorded(Of T))
```

```vb
'Usage
Dim instance As Recorded(Of T)
```

```csharp
[SerializableAttribute]
public struct Recorded<T> : IEquatable<Recorded<T>>
```

```c++
[SerializableAttribute]
generic<typename T>
public value class Recorded : IEquatable<Recorded<T>>
```

```fsharp
[<SealedAttribute>]
[<SerializableAttribute>]
type Recorded<'T> =  
    struct
        interface IEquatable<Recorded<'T>>
    end
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T  
  The type.

The Recorded\<T\> type exposes the following members.

## Constructors

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Recorded<T>](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.recorded%601.#ctor(system.int64%2c%600)(v=VS.103))Initializes a new instance of the Recorded<T> class with the specified value at the given virtual time.Top

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Time](Time/Recorded(T).Time)Gets the virtual time the value was produced on.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Value](Value/Recorded(T).Value)Gets the value.Top

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Object)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.recorded%601.equals(system.object)(v=VS.103))Checks whether the given object is equal to the current instance. (Overrides [ValueType.Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.valuetype.equals(system.object)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Recorded<T>)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.recorded%601.equals(microsoft.reactive.testing.recorded%7b%600%7d)(v=VS.103))Checks whether the given recorded object is equal to the current instance.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](GetHashCode/Recorded(T).GetHashCode)Computes a hash code for the current instance. (Overrides [ValueType.GetHashCode()](https://msdn.microsoft.com/en-us/library/y3509fc2).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](ToString/Recorded(T).ToString)Gets a friendly string representation of the current instance. (Overrides [ValueType.ToString()](https://msdn.microsoft.com/en-us/library/wb77sz3h).)Top

## Operators

NameDescription![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Equality](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.recorded%601.op_equality(microsoft.reactive.testing.recorded%7b%600%7d%2cmicrosoft.reactive.testing.recorded%7b%600%7d)(v=VS.103))Checks whether the two given recorded objects are equal.![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Inequality](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.recorded%601.op_inequality(microsoft.reactive.testing.recorded%7b%600%7d%2cmicrosoft.reactive.testing.recorded%7b%600%7d)(v=VS.103))Checks whether the two given recorded objects are not equal.Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

# Recorded\<T\> Methods

Include Protected Members  
Include Inherited Members

The [Recorded\<T\>](Recorded/Recorded(T)) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Object)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.recorded%601.equals(system.object)(v=VS.103))Checks whether the given object is equal to the current instance. (Overrides [ValueType.Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.valuetype.equals(system.object)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Recorded<T>)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.recorded%601.equals(microsoft.reactive.testing.recorded%7b%600%7d)(v=VS.103))Checks whether the given recorded object is equal to the current instance.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](GetHashCode/Recorded(T).GetHashCode)Computes a hash code for the current instance. (Overrides [ValueType.GetHashCode()](https://msdn.microsoft.com/en-us/library/y3509fc2).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](ToString/Recorded(T).ToString)Gets a friendly string representation of the current instance. (Overrides [ValueType.ToString()](https://msdn.microsoft.com/en-us/library/wb77sz3h).)Top

## See Also

#### Reference

[Recorded\<T\> Structure](Recorded/Recorded(T))

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

# Recorded\<T\> Operators

Include Protected Members  
Include Inherited Members

The [Recorded\<T\>](Recorded/Recorded(T)) type exposes the following members.

## Operators

NameDescription![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Equality](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.recorded%601.op_equality(microsoft.reactive.testing.recorded%7b%600%7d%2cmicrosoft.reactive.testing.recorded%7b%600%7d)(v=VS.103))Checks whether the two given recorded objects are equal.![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Inequality](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.recorded%601.op_inequality(microsoft.reactive.testing.recorded%7b%600%7d%2cmicrosoft.reactive.testing.recorded%7b%600%7d)(v=VS.103))Checks whether the two given recorded objects are not equal.Top

## See Also

#### Reference

[Recorded\<T\> Structure](Recorded/Recorded(T))

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

# Recorded\<T\> Properties

Include Protected Members  
Include Inherited Members

The [Recorded\<T\>](Recorded/Recorded(T)) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Time](Time/Recorded(T).Time)Gets the virtual time the value was produced on.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Value](Value/Recorded(T).Value)Gets the value.Top

## See Also

#### Reference

[Recorded\<T\> Structure](Recorded/Recorded(T))

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)

# Recorded\<T\> Constructor

Initializes a new instance of the [Recorded\<T\>](Recorded/Recorded(T)) class with the specified value at the given virtual time.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Sub New ( _
    time As Long, _
    value As T _
)
```

```vb
'Usage
Dim time As Long
Dim value As T

Dim instance As New Recorded(time, value)
```

```csharp
public Recorded(
    long time,
    T value
)
```

```c++
public:
Recorded(
    long long time, 
    T value
)
```

```fsharp
new : 
        time:int64 * 
        value:'T -> Recorded
```

```jscript
public function Recorded(
    time : long, 
    value : T
)
```

#### Parameters

- time  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  Virtual time the value was produced on.

- value  
  Type: [T](Recorded/Recorded(T))  
  Value that was produced.

## See Also

#### Reference

[Recorded\<T\> Structure](Recorded/Recorded(T))

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing/Microsoft.Reactive.Testing)
