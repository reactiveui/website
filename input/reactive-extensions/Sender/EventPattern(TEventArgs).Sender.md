title: EventPattern<TEventArgs>.Sender Property
---
# EventPattern\<TEventArgs\>.Sender Property

Represents event sender information for a .NET event.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Property Sender As Object
    Get
    Private Set
```

```vb
'Usage
Dim instance As EventPattern
Dim value As Object

value = instance.Sender
```

```csharp
public Object Sender { get; private set; }
```

```c++
public:
property Object^ Sender {
    Object^ get ();
    private: void set (Object^ value);
}
```

```fsharp
member Sender : Object with get, private set
```

```javascript
function get Sender () : Object
private function set Sender (value : Object)
```

#### Property Value

Type: [System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
Returns [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).

## See Also

#### Reference

[EventPattern\<TEventArgs\> Class](EventPattern/EventPattern(TEventArgs))

[System.Reactive Namespace](System.Reactive/System.Reactive)
