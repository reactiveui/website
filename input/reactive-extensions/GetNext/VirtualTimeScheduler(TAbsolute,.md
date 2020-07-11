title: VirtualTimeScheduler<TAbsolute, TRelative>.GetNext()
---
# VirtualTimeScheduler\<TAbsolute, TRelative\>.GetNext Method

Gets the next scheduled item to be executed.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Protected Overrides Function GetNext As IScheduledItem(Of TAbsolute)
```

```vb
'Usage
Dim returnValue As IScheduledItem(Of TAbsolute)

returnValue = Me.GetNext()
```

```csharp
protected override IScheduledItem<TAbsolute> GetNext()
```

```c++
protected:
virtual IScheduledItem<TAbsolute>^ GetNext() override
```

```fsharp
abstract GetNext : unit -> IScheduledItem<'TAbsolute> 
override GetNext : unit -> IScheduledItem<'TAbsolute> 
```

```jscript
protected override function GetNext() : IScheduledItem<TAbsolute>
```

#### Return Value

Type: [System.Reactive.Concurrency.IScheduledItem](IScheduledItem/IScheduledItem(TAbsolute))\<[TAbsolute](VirtualTimeScheduler/VirtualTimeScheduler(TAbsolute,)\>  
The next scheduled item.

## See Also

#### Reference

[VirtualTimeScheduler\<TAbsolute, TRelative\> Class](VirtualTimeScheduler/VirtualTimeScheduler(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)





