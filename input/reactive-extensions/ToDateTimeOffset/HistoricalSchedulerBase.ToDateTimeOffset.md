title: HistoricalSchedulerBase.ToDateTimeOffset()
---
# HistoricalSchedulerBase.ToDateTimeOffset Method

Converts the absolute time value to a DateTimeOffset value.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Protected Overrides Function ToDateTimeOffset ( _
    absolute As DateTimeOffset _
) As DateTimeOffset
```

```vb
'Usage
Dim absolute As DateTimeOffset
Dim returnValue As DateTimeOffset

returnValue = Me.ToDateTimeOffset(absolute)
```

```csharp
protected override DateTimeOffset ToDateTimeOffset(
    DateTimeOffset absolute
)
```

```c++
protected:
virtual DateTimeOffset ToDateTimeOffset(
    DateTimeOffset absolute
) override
```

```fsharp
abstract ToDateTimeOffset : 
        absolute:DateTimeOffset -> DateTimeOffset 
override ToDateTimeOffset : 
        absolute:DateTimeOffset -> DateTimeOffset 
```

```javascript
protected override function ToDateTimeOffset(
    absolute : DateTimeOffset
) : DateTimeOffset
```

#### Parameters

- absolute  
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
  The absolute time value to convert.

#### Return Value

Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
The corresponding DateTimeOffset value.

## See Also

#### Reference

[HistoricalSchedulerBase Class](HistoricalSchedulerBase/HistoricalSchedulerBase)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)
