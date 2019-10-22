title: TestScheduler.Add()
---
# TestScheduler.Add Method

Adds a relative virtual time to an absolute virtual time value.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Protected Overrides Function Add ( _
    absolute As Long, _
    relative As Long _
) As Long
```

```vb
'Usage
Dim absolute As Long
Dim relative As Long
Dim returnValue As Long

returnValue = Me.Add(absolute, relative)
```

```csharp
protected override long Add(
    long absolute,
    long relative
)
```

```c++
protected:
virtual long long Add(
    long long absolute, 
    long long relative
) override
```

```fsharp
abstract Add : 
        absolute:int64 * 
        relative:int64 -> int64 
override Add : 
        absolute:int64 * 
        relative:int64 -> int64 
```

```jscript
protected override function Add(
    absolute : long, 
    relative : long
) : long
```

#### Parameters

- absolute  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  Absolute virtual time value.

- relative  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  Relative virtual time value to add.

#### Return Value

Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
Resulting absolute virtual time sum value.

## See Also

#### Reference

[TestScheduler Class](TestScheduler\TestScheduler.md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)
