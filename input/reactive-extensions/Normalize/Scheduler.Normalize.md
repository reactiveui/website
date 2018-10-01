# Scheduler.Normalize Method

Ensures that no time spans are negative.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Normalize ( _
    timeSpan As TimeSpan _
) As TimeSpan
```

```vb
'Usage
Dim timeSpan As TimeSpan
Dim returnValue As TimeSpan

returnValue = Scheduler.Normalize(timeSpan)
```

```csharp
public static TimeSpan Normalize(
    TimeSpan timeSpan
)
```

```c++
public:
static TimeSpan Normalize(
    TimeSpan timeSpan
)
```

```fsharp
static member Normalize : 
        timeSpan:TimeSpan -> TimeSpan 
```

```jscript
public static function Normalize(
    timeSpan : TimeSpan
) : TimeSpan
```

#### Parameters

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The time span to normalize.

#### Return Value

Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
The time span if it zero or positive otherwise TimeSpan.Zero.

## See Also

#### Reference

[Scheduler Class](Scheduler\Scheduler.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)






