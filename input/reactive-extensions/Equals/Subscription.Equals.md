title: Subscription.Equals(Object)
---
# Subscription.Equals Method (Object)

Checks whether the given object is equal to the current instance.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Overrides Function Equals ( _
    obj As Object _
) As Boolean
```

```vb
'Usage
Dim instance As Subscription
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
  The object to check for equality.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if both objects are equal; otherwise, false.

## See Also

#### Reference

[Subscription Structure](Subscription\Subscription.md)

[Equals Overload](Equals\Subscription.Equals.md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)







# Subscription.Equals Method

Include Protected Members  
Include Inherited Members

Checks whether the given value is equal to the current instance.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Object)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.subscription.equals(system.object)(v=VS.103))Checks whether the given object is equal to the current instance. (Overrides [ValueType.Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.valuetype.equals(system.object)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Subscription)](https://msdn.microsoft.com/en-us/library/m:microsoft.reactive.testing.subscription.equals(microsoft.reactive.testing.subscription)(v=VS.103))Checks whether the given subscription is equal to the current instance.Top

## See Also

#### Reference

[Subscription Structure](Subscription\Subscription.md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)





# Subscription.Equals Method (Subscription)

Checks whether the given subscription is equal to the current instance.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public Function Equals ( _
    other As Subscription _
) As Boolean
```

```vb
'Usage
Dim instance As Subscription
Dim other As Subscription
Dim returnValue As Boolean

returnValue = instance.Equals(other)
```

```csharp
public bool Equals(
    Subscription other
)
```

```c++
public:
virtual bool Equals(
    Subscription other
) sealed
```

```fsharp
abstract Equals : 
        other:Subscription -> bool 
override Equals : 
        other:Subscription -> bool 
```

```jscript
public final function Equals(
    other : Subscription
) : boolean
```

#### Parameters

- other  
  Type: [Microsoft.Reactive.Testing.Subscription](Subscription\Subscription.md)  
  The subscription object to check for equality.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if both objects are equal; otherwise, false.

#### Implements

[IEquatable\<T\>.Equals(T)](https://msdn.microsoft.com/en-us/library/m:system.iequatable%601.equals(%600)(v=VS.103))

## See Also

#### Reference

[Subscription Structure](Subscription\Subscription.md)

[Equals Overload](Equals\Subscription.Equals.md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)







