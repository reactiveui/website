title: TestScheduler.ToDateTimeOffset()
---
# TestScheduler.ToDateTimeOffset Method

Converts the absolute virtual time value to a DateTimeOffset value.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Protected Overrides Function ToDateTimeOffset ( _
    absolute As Long _
) As DateTimeOffset
```

```vb
'Usage
Dim absolute As Long
Dim returnValue As DateTimeOffset

returnValue = Me.ToDateTimeOffset(absolute)
```

```csharp
protected override DateTimeOffset ToDateTimeOffset(
    long absolute
)
```

```c++
protected:
virtual DateTimeOffset ToDateTimeOffset(
    long long absolute
) override
```

```fsharp
abstract ToDateTimeOffset : 
        absolute:int64 -> DateTimeOffset 
override ToDateTimeOffset : 
        absolute:int64 -> DateTimeOffset 
```

```jscript
protected override function ToDateTimeOffset(
    absolute : long
) : DateTimeOffset
```

#### Parameters

- absolute  
  Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
  The absolute virtual time value to convert.

#### Return Value

Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
The corresponding DateTimeOffset value.

## See Also

#### Reference

[TestScheduler Class](TestScheduler\TestScheduler.md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)
