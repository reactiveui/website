title: EventPattern<TEventArgs>.EventArgs Property
---
# EventPattern\<TEventArgs\>.EventArgs Property

Represents event arguments for a .NET event.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Property EventArgs As TEventArgs
    Get
    Private Set
```

```vb
'Usage
Dim instance As EventPattern
Dim value As TEventArgs

value = instance.EventArgs
```

```csharp
public TEventArgs EventArgs { get; private set; }
```

```c++
public:
property TEventArgs EventArgs {
    TEventArgs get ();
    private: void set (TEventArgs value);
}
```

```fsharp
member EventArgs : 'TEventArgs with get, private set
```

```javascript
function get EventArgs () : TEventArgs
private function set EventArgs (value : TEventArgs)
```

#### Property Value

Type: [TEventArgs](EventPattern/EventPattern(TEventArgs))  
Returns [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50).

## See Also

#### Reference

[EventPattern\<TEventArgs\> Class](EventPattern/EventPattern(TEventArgs))

[System.Reactive Namespace](System.Reactive/System.Reactive)





