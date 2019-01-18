# EventPattern\<TEventArgs\> Methods

Include Protected Members  
Include Inherited Members

The [EventPattern\<TEventArgs\>](EventPattern\EventPattern(TEventArgs).md) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.eventpattern%601.equals(system.object)(v=VS.103))Compares this type with the specified object. (Overrides [Object.Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(EventPattern<TEventArgs>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.eventpattern%601.equals(system.reactive.eventpattern%7b%600%7d)(v=VS.103))Compares this type with the specified object.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](GetHashCode\EventPattern(TEventArgs).GetHashCode.md)(Overrides [Object.GetHashCode()](https://msdn.microsoft.com/en-us/library/zdee4b3y).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## See Also

#### Reference

[EventPattern\<TEventArgs\> Class](EventPattern\EventPattern(TEventArgs).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)





# EventPattern\<TEventArgs\> Operators

Include Protected Members  
Include Inherited Members

The [EventPattern\<TEventArgs\>](EventPattern\EventPattern(TEventArgs).md) type exposes the following members.

## Operators

NameDescription![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Equality](https://msdn.microsoft.com/en-us/library/m:system.reactive.eventpattern%601.op_equality(system.reactive.eventpattern%7b%600%7d%2csystem.reactive.eventpattern%7b%600%7d)(v=VS.103))Compare two objects to see if they are identical.![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Inequality](https://msdn.microsoft.com/en-us/library/m:system.reactive.eventpattern%601.op_inequality(system.reactive.eventpattern%7b%600%7d%2csystem.reactive.eventpattern%7b%600%7d)(v=VS.103))Compare two objects to see if they are identical.Top

## See Also

#### Reference

[EventPattern\<TEventArgs\> Class](EventPattern\EventPattern(TEventArgs).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)





# EventPattern\<TEventArgs\> Properties

Include Protected Members  
Include Inherited Members

The [EventPattern\<TEventArgs\>](EventPattern\EventPattern(TEventArgs).md) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[EventArgs](EventArgs\EventPattern(TEventArgs).EventArgs.md)Represents event arguments for a .NET event.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Sender](Sender\EventPattern(TEventArgs).Sender.md)Represents event sender information for a .NET event.Top

## See Also

#### Reference

[EventPattern\<TEventArgs\> Class](EventPattern\EventPattern(TEventArgs).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)





# EventPattern\<TEventArgs\> Class

Encapsulates sender and event arguments for a .NET event.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.EventPattern\<TEventArgs\>

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Class EventPattern(Of TEventArgs As EventArgs) _
    Implements IEquatable(Of EventPattern(Of TEventArgs))
```

```vb
'Usage
Dim instance As EventPattern(Of TEventArgs)
```

```csharp
public class EventPattern<TEventArgs> : IEquatable<EventPattern<TEventArgs>>
where TEventArgs : EventArgs
```

```c++
generic<typename TEventArgs>
where TEventArgs : EventArgs
public ref class EventPattern : IEquatable<EventPattern<TEventArgs>^>
```

```fsharp
type EventPattern<'TEventArgs when 'TEventArgs : EventArgs> =  
    class
        interface IEquatable<EventPattern<'TEventArgs>>
    end
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TEventArgs

The EventPattern\<TEventArgs\> type exposes the following members.

## Constructors

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[EventPattern<TEventArgs>](https://msdn.microsoft.com/en-us/library/m:system.reactive.eventpattern%601.#ctor(system.object%2c%600)(v=VS.103))Initialize a new instance of the EventPattern<TEventArgs> type.Top

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[EventArgs](EventArgs\EventPattern(TEventArgs).EventArgs.md)Represents event arguments for a .NET event.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Sender](Sender\EventPattern(TEventArgs).Sender.md)Represents event sender information for a .NET event.Top

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.eventpattern%601.equals(system.object)(v=VS.103))Compares this type with the specified object. (Overrides [Object.Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(EventPattern<TEventArgs>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.eventpattern%601.equals(system.reactive.eventpattern%7b%600%7d)(v=VS.103))Compares this type with the specified object.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](GetHashCode\EventPattern(TEventArgs).GetHashCode.md)(Overrides [Object.GetHashCode()](https://msdn.microsoft.com/en-us/library/zdee4b3y).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Operators

NameDescription![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Equality](https://msdn.microsoft.com/en-us/library/m:system.reactive.eventpattern%601.op_equality(system.reactive.eventpattern%7b%600%7d%2csystem.reactive.eventpattern%7b%600%7d)(v=VS.103))Compare two objects to see if they are identical.![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Inequality](https://msdn.microsoft.com/en-us/library/m:system.reactive.eventpattern%601.op_inequality(system.reactive.eventpattern%7b%600%7d%2csystem.reactive.eventpattern%7b%600%7d)(v=VS.103))Compare two objects to see if they are identical.Top

## Remarks

The FromEventPattern operator works with events that take an object sender and some EventArgs, and uses reflection to find add/remove methods. It then converts the given event into an observable sequence with an EventPattern type that captures both the sender and the event arguments.

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive Namespace](System.Reactive\System.Reactive.md)













# EventPattern\<TEventArgs\> Constructor

Initialize a new instance of the [EventPattern\<TEventArgs\>](EventPattern\EventPattern(TEventArgs).md) type.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub New ( _
    sender As Object, _
    e As TEventArgs _
)
```

```vb
'Usage
Dim sender As Object
Dim e As TEventArgs

Dim instance As New EventPattern(sender, _
    e)
```

```csharp
public EventPattern(
    Object sender,
    TEventArgs e
)
```

```c++
public:
EventPattern(
    Object^ sender, 
    TEventArgs e
)
```

```fsharp
new : 
        sender:Object * 
        e:'TEventArgs -> EventPattern
```

```jscript
public function EventPattern(
    sender : Object, 
    e : TEventArgs
)
```

#### Parameters

- sender  
  Type: [System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  The event sender information.

- e  
  Type: [TEventArgs](EventPattern\EventPattern(TEventArgs).md)  
  Event arguments.

## See Also

#### Reference

[EventPattern\<TEventArgs\> Class](EventPattern\EventPattern(TEventArgs).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)





