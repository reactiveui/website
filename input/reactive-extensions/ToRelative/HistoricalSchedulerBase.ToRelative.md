title: HistoricalSchedulerBase.ToRelative()
---
# HistoricalSchedulerBase.ToRelative Method

Converts the TimeSpan value to a relative time value.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Protected Overrides Function ToRelative ( _
    timeSpan As TimeSpan _
) As TimeSpan
```

```vb
'Usage
Dim timeSpan As TimeSpan
Dim returnValue As TimeSpan

returnValue = Me.ToRelative(timeSpan)
```

```csharp
protected override TimeSpan ToRelative(
    TimeSpan timeSpan
)
```

```c++
protected:
virtual TimeSpan ToRelative(
    TimeSpan timeSpan
) override
```

```fsharp
abstract ToRelative : 
        timeSpan:TimeSpan -> TimeSpan 
override ToRelative : 
        timeSpan:TimeSpan -> TimeSpan 
```

```jscript
protected override function ToRelative(
    timeSpan : TimeSpan
) : TimeSpan
```

#### Parameters

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The TimeSpan value to convert.

#### Return Value

Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
The corresponding relative time value.

## See Also

#### Reference

[HistoricalSchedulerBase Class](HistoricalSchedulerBase\HistoricalSchedulerBase.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)
