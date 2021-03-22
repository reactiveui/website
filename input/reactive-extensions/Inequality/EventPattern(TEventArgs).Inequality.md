title: EventPattern<TEventArgs>.Inequality Operator
---
# EventPattern\<TEventArgs\>.Inequality Operator

Compare two objects to see if they are identical.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Operator <> ( _
    first As EventPattern(Of TEventArgs), _
    second As EventPattern(Of TEventArgs) _
) As Boolean
```

```vb
'Usage
Dim first As EventPattern(Of TEventArgs)
Dim second As EventPattern(Of TEventArgs)
Dim returnValue As Boolean

returnValue = (first <> second)
```

```csharp
public static bool operator !=(
    EventPattern<TEventArgs> first,
    EventPattern<TEventArgs> second
)
```

```c++
public:
static bool operator !=(
    EventPattern<TEventArgs>^ first, 
    EventPattern<TEventArgs>^ second
)
```

```fsharp
static let inline (<>)
        first:EventPattern<'TEventArgs> * 
        second:EventPattern<'TEventArgs>  : bool
```

```javascript
JScript supports the use of overloaded operators, but not the declaration of new ones.
```

#### Parameters

- first  
  Type: [System.Reactive.EventPattern](EventPattern/EventPattern(TEventArgs))\<[TEventArgs](EventPattern/EventPattern(TEventArgs))\>  
  The first object to be compared.

- second  
  Type: [System.Reactive.EventPattern](EventPattern/EventPattern(TEventArgs))\<[TEventArgs](EventPattern/EventPattern(TEventArgs))\>  
  The second object to be compared.

#### Return Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
Returns [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50).

## See Also

#### Reference

[EventPattern\<TEventArgs\> Class](EventPattern/EventPattern(TEventArgs))

[System.Reactive Namespace](System.Reactive/System.Reactive)






