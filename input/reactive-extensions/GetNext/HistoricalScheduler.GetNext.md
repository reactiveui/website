# HistoricalScheduler.GetNext Method

Gets the next scheduled item to be executed.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Protected Overrides Function GetNext As IScheduledItem(Of DateTimeOffset)
```

```vb
'Usage
Dim returnValue As IScheduledItem(Of DateTimeOffset)

returnValue = Me.GetNext()
```

```csharp
protected override IScheduledItem<DateTimeOffset> GetNext()
```

```c++
protected:
virtual IScheduledItem<DateTimeOffset>^ GetNext() override
```

```fsharp
abstract GetNext : unit -> IScheduledItem<DateTimeOffset> 
override GetNext : unit -> IScheduledItem<DateTimeOffset> 
```

```jscript
protected override function GetNext() : IScheduledItem<DateTimeOffset>
```

#### Return Value

Type: [System.Reactive.Concurrency.IScheduledItem](IScheduledItem\IScheduledItem(TAbsolute).md)\<[DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)\>  
The next scheduled item.

## See Also

#### Reference

[HistoricalScheduler Class](HistoricalScheduler\HistoricalScheduler.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)





