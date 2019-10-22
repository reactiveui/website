title: Recorded<T>.Time Property
---
# Recorded\<T\>.Time Property

Gets the virtual time the value was produced on.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Public ReadOnly Property Time As Long
    Get
```

```vb
'Usage
Dim instance As Recorded
Dim value As Long

value = instance.Time
```

```csharp
public long Time { get; }
```

```c++
public:
property long long Time {
    long long get ();
}
```

```fsharp
member Time : int64
```

```jscript
function get Time () : long
```

#### Property Value

Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
The virtual time the value was produced on.

## See Also

#### Reference

[Recorded\<T\> Structure](Recorded\Recorded(T).md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)
