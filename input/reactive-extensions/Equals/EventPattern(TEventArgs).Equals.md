# EventPattern\<TEventArgs\>.Equals Method (Object)

Compares this type with the specified object.

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
Dim instance As EventPattern
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
  Object to be compared to.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
Returns [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50).

## See Also

#### Reference

[EventPattern\<TEventArgs\> Class](EventPattern\EventPattern(TEventArgs).md)

[Equals Overload](Equals\EventPattern(TEventArgs).Equals.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)







# EventPattern\<TEventArgs\>.Equals Method (EventPattern\<TEventArgs\>)

Compares this type with the specified object.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Equals ( _
    other As EventPattern(Of TEventArgs) _
) As Boolean
```

```vb
'Usage
Dim instance As EventPattern
Dim other As EventPattern(Of TEventArgs)
Dim returnValue As Boolean

returnValue = instance.Equals(other)
```

```csharp
public bool Equals(
    EventPattern<TEventArgs> other
)
```

```c++
public:
virtual bool Equals(
    EventPattern<TEventArgs>^ other
) sealed
```

```fsharp
abstract Equals : 
        other:EventPattern<'TEventArgs> -> bool 
override Equals : 
        other:EventPattern<'TEventArgs> -> bool 
```

```jscript
public final function Equals(
    other : EventPattern<TEventArgs>
) : boolean
```

#### Parameters

- other  
  Type: [System.Reactive.EventPattern](EventPattern\EventPattern(TEventArgs).md)\<[TEventArgs](EventPattern\EventPattern(TEventArgs).md)\>  
  The object to be compared to.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
Returns [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50).

#### Implements

[IEquatable\<T\>.Equals(T)](https://msdn.microsoft.com/en-us/library/m:system.iequatable%601.equals(%600)(v=VS.103))

## See Also

#### Reference

[EventPattern\<TEventArgs\> Class](EventPattern\EventPattern(TEventArgs).md)

[Equals Overload](Equals\EventPattern(TEventArgs).Equals.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)








# EventPattern\<TEventArgs\>.Equals Method

Include Protected Members  
Include Inherited Members

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.eventpattern%601.equals(system.object)(v=VS.103))Compares this type with the specified object. (Overrides [Object.Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103)).)![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(EventPattern<TEventArgs>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.eventpattern%601.equals(system.reactive.eventpattern%7b%600%7d)(v=VS.103))Compares this type with the specified object.Top

## See Also

#### Reference

[EventPattern\<TEventArgs\> Class](EventPattern\EventPattern(TEventArgs).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)




