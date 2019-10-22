title: HistoricalSchedulerBase.Add()
---
# HistoricalSchedulerBase.Add Method

Adds a relative time to an absolute time value.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Protected Overrides Function Add ( _
    absolute As DateTimeOffset, _
    relative As TimeSpan _
) As DateTimeOffset
```

```vb
'Usage
Dim absolute As DateTimeOffset
Dim relative As TimeSpan
Dim returnValue As DateTimeOffset

returnValue = Me.Add(absolute, relative)
```

```csharp
protected override DateTimeOffset Add(
    DateTimeOffset absolute,
    TimeSpan relative
)
```

```c++
protected:
virtual DateTimeOffset Add(
    DateTimeOffset absolute, 
    TimeSpan relative
) override
```

```fsharp
abstract Add : 
        absolute:DateTimeOffset * 
        relative:TimeSpan -> DateTimeOffset 
override Add : 
        absolute:DateTimeOffset * 
        relative:TimeSpan -> DateTimeOffset 
```

```jscript
protected override function Add(
    absolute : DateTimeOffset, 
    relative : TimeSpan
) : DateTimeOffset
```

#### Parameters

- absolute  
  Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
  The absolute time value.

- relative  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The relative time value to add.

#### Return Value

Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
The resulting absolute time sum value.

## See Also

#### Reference

[HistoricalSchedulerBase Class](HistoricalSchedulerBase\HistoricalSchedulerBase.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)
