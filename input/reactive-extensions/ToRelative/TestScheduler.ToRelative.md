title: TestScheduler.ToRelative()
---
# TestScheduler.ToRelative Method

Converts the TimeSpan value to a relative virtual time value.

**Namespace:**  [Microsoft.Reactive.Testing](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)  
**Assembly:**  Microsoft.Reactive.Testing (in Microsoft.Reactive.Testing.dll)

## Syntax

```vb
'Declaration
Protected Overrides Function ToRelative ( _
    timeSpan As TimeSpan _
) As Long
```

```vb
'Usage
Dim timeSpan As TimeSpan
Dim returnValue As Long

returnValue = Me.ToRelative(timeSpan)
```

```csharp
protected override long ToRelative(
    TimeSpan timeSpan
)
```

```c++
protected:
virtual long long ToRelative(
    TimeSpan timeSpan
) override
```

```fsharp
abstract ToRelative : 
        timeSpan:TimeSpan -> int64 
override ToRelative : 
        timeSpan:TimeSpan -> int64 
```

```jscript
protected override function ToRelative(
    timeSpan : TimeSpan
) : long
```

#### Parameters

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The timeSpan value to convert.

#### Return Value

Type: [System.Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)  
The corresponding relative virtual time value.

## See Also

#### Reference

[TestScheduler Class](TestScheduler\TestScheduler.md)

[Microsoft.Reactive.Testing Namespace](Microsoft.Reactive.Testing\Microsoft.Reactive.Testing.md)
