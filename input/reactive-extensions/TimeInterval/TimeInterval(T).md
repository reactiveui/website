title: TimeInterval<T> Structure
---
# TimeInterval\<T\> Structure

Represents a time interval value.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<SerializableAttribute> _
Public Structure TimeInterval(Of T)
```

```vb
'Usage
Dim instance As TimeInterval(Of T)
```

```csharp
[SerializableAttribute]
public struct TimeInterval<T>
```

```c++
[SerializableAttribute]
generic<typename T>
public value class TimeInterval
```

```fsharp
[<SealedAttribute>]
[<SerializableAttribute>]
type TimeInterval<'T> =  struct end
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T  
  The time interval argument type.

The TimeInterval\<T\> type exposes the following members.

## Constructors

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[TimeInterval<T>](https://msdn.microsoft.com/en-us/library/m:system.reactive.timeinterval%601.#ctor(%600%2csystem.timespan)(v=VS.103))Constructs a timestamped value.Top

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Interval](Interval/TimeInterval(T).Interval)Gets the interval.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Value](Value/TimeInterval(T).Value)Gets the value.Top

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.reactive.timeinterval%601.equals(system.object)(v=VS.103))Indicates whether this instance and a specified object are equal. (Overrides [ValueType.Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.valuetype.equals(system.object)(v=VS.103)).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](GetHashCode/TimeInterval(T).GetHashCode)Returns the hash code for this instance. (Overrides [ValueType.GetHashCode()](https://msdn.microsoft.com/en-us/library/y3509fc2).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](ToString/TimeInterval(T).ToString)Returns a string representation of this instance. (Overrides [ValueType.ToString()](https://msdn.microsoft.com/en-us/library/wb77sz3h).)Top

## Operators

NameDescription![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Equality](https://msdn.microsoft.com/en-us/library/m:system.reactive.timeinterval%601.op_equality(system.reactive.timeinterval%7b%600%7d%2csystem.reactive.timeinterval%7b%600%7d)(v=VS.103))Indicates whether first and second arguments are equal.![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Inequality](https://msdn.microsoft.com/en-us/library/m:system.reactive.timeinterval%601.op_inequality(system.reactive.timeinterval%7b%600%7d%2csystem.reactive.timeinterval%7b%600%7d)(v=VS.103))Indicates whether first and second arguments are not equal.Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive Namespace](System.Reactive/System.Reactive)

# TimeInterval\<T\> Properties

Include Protected Members  
Include Inherited Members

The [TimeInterval\<T\>](TimeInterval/TimeInterval(T)) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Interval](Interval/TimeInterval(T).Interval)Gets the interval.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Value](Value/TimeInterval(T).Value)Gets the value.Top

## See Also

#### Reference

[TimeInterval\<T\> Structure](TimeInterval/TimeInterval(T))

[System.Reactive Namespace](System.Reactive/System.Reactive)

# TimeInterval\<T\> Constructor

Constructs a timestamped value.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New ( _
    value As T, _
    interval As TimeSpan _
)
```

```vb
'Usage
Dim value As T
Dim interval As TimeSpan

Dim instance As New TimeInterval(value, interval)
```

```csharp
public TimeInterval(
    T value,
    TimeSpan interval
)
```

```c++
public:
TimeInterval(
    T value, 
    TimeSpan interval
)
```

```fsharp
new : 
        value:'T * 
        interval:TimeSpan -> TimeInterval
```

```javascript
public function TimeInterval(
    value : T, 
    interval : TimeSpan
)
```

#### Parameters

- value  
  Type: [T](TimeInterval/TimeInterval(T))  
  The value.

- interval  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The interval.

## See Also

#### Reference

[TimeInterval\<T\> Structure](TimeInterval/TimeInterval(T))

[System.Reactive Namespace](System.Reactive/System.Reactive)

# TimeInterval\<T\> Methods

Include Protected Members  
Include Inherited Members

The [TimeInterval\<T\>](TimeInterval/TimeInterval(T)) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals](https://msdn.microsoft.com/en-us/library/m:system.reactive.timeinterval%601.equals(system.object)(v=VS.103))Indicates whether this instance and a specified object are equal. (Overrides [ValueType.Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.valuetype.equals(system.object)(v=VS.103)).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](GetHashCode/TimeInterval(T).GetHashCode)Returns the hash code for this instance. (Overrides [ValueType.GetHashCode()](https://msdn.microsoft.com/en-us/library/y3509fc2).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](ToString/TimeInterval(T).ToString)Returns a string representation of this instance. (Overrides [ValueType.ToString()](https://msdn.microsoft.com/en-us/library/wb77sz3h).)Top

## See Also

#### Reference

[TimeInterval\<T\> Structure](TimeInterval/TimeInterval(T))

[System.Reactive Namespace](System.Reactive/System.Reactive)

# TimeInterval\<T\> Operators

Include Protected Members  
Include Inherited Members

The [TimeInterval\<T\>](TimeInterval/TimeInterval(T)) type exposes the following members.

## Operators

NameDescription![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Equality](https://msdn.microsoft.com/en-us/library/m:system.reactive.timeinterval%601.op_equality(system.reactive.timeinterval%7b%600%7d%2csystem.reactive.timeinterval%7b%600%7d)(v=VS.103))Indicates whether first and second arguments are equal.![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Inequality](https://msdn.microsoft.com/en-us/library/m:system.reactive.timeinterval%601.op_inequality(system.reactive.timeinterval%7b%600%7d%2csystem.reactive.timeinterval%7b%600%7d)(v=VS.103))Indicates whether first and second arguments are not equal.Top

## See Also

#### Reference

[TimeInterval\<T\> Structure](TimeInterval/TimeInterval(T))

[System.Reactive Namespace](System.Reactive/System.Reactive)
