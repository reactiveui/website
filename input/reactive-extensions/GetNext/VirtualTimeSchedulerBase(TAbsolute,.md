title: VirtualTimeSchedulerBase<TAbsolute, TRelative>.GetNext()
---
# VirtualTimeSchedulerBase\<TAbsolute, TRelative\>.GetNext Method

Gets the next scheduled item to be executed.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Protected MustOverride Function GetNext As IScheduledItem(Of TAbsolute)
```

```vb
'Usage
Dim returnValue As IScheduledItem(Of TAbsolute)

returnValue = Me.GetNext()
```

```csharp
protected abstract IScheduledItem<TAbsolute> GetNext()
```

```c++
protected:
virtual IScheduledItem<TAbsolute>^ GetNext() abstract
```

```fsharp
abstract GetNext : unit -> IScheduledItem<'TAbsolute> 
```

```javascript
protected abstract function GetNext() : IScheduledItem<TAbsolute>
```

#### Return Value

Type: [System.Reactive.Concurrency.IScheduledItem](IScheduledItem/IScheduledItem(TAbsolute))\<[TAbsolute](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)\>  
The next scheduled item.

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)





