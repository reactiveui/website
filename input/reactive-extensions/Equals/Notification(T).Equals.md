# Notification\<T\>.Equals Method (Notification\<T\>)

Indicates whether this instance and other are equal.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public MustOverride Function Equals ( _
    other As Notification(Of T) _
) As Boolean
```

```vb
'Usage
Dim instance As Notification
Dim other As Notification(Of T)
Dim returnValue As Boolean

returnValue = instance.Equals(other)
```

```csharp
public abstract bool Equals(
    Notification<T> other
)
```

```c++
public:
virtual bool Equals(
    Notification<T>^ other
) abstract
```

```fsharp
abstract Equals : 
        other:Notification<'T> -> bool 
```

```jscript
public abstract function Equals(
    other : Notification<T>
) : boolean
```

#### Parameters

- other  
  Type: [System.Reactive.Notification](Notification\Notification(T).md)\<[T](Notification\Notification(T).md)\>  
  The other notification to check equality with this instance.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if this instance and the other notification are equal; otherwise, false.

#### Implements

[IEquatable\<T\>.Equals(T)](https://msdn.microsoft.com/en-us/library/m:system.iequatable%601.equals(%600)(v=VS.103))

## See Also

#### Reference

[Notification\<T\> Class](Notification\Notification(T).md)

[Equals Overload](Equals\Notification(T).Equals.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)








# Notification\<T\>.Equals Method

Include Protected Members  
Include Inherited Members

Indicates whether this instance and other are equal.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.equals(system.object)(v=VS.103))Indicates whether this instance and a specified object are equal. (Overrides [Object.Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Notification<T>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.equals(system.reactive.notification%7b%600%7d)(v=VS.103))Indicates whether this instance and other are equal.Top

## See Also

#### Reference

[Notification\<T\> Class](Notification\Notification(T).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)





# Notification\<T\>.Equals Method (Object)

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
Dim instance As Notification
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
true if this instance and the specified object are equal; otherwise, false.

## See Also

#### Reference

[Notification\<T\> Class](Notification\Notification(T).md)

[Equals Overload](Equals\Notification(T).Equals.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)






