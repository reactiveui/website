title: VirtualTimeSchedulerBase<TAbsolute, TRelative>.AdvanceBy()
---
# VirtualTimeSchedulerBase\<TAbsolute, TRelative\>.AdvanceBy Method

Advances the scheduler's clock by the specified relative time, running all work scheduled for that timespan.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub AdvanceBy ( _
    time As TRelative _
)
```

```vb
'Usage
Dim instance As VirtualTimeSchedulerBase
Dim time As TRelative

instance.AdvanceBy(time)
```

```csharp
public void AdvanceBy(
    TRelative time
)
```

```c++
public:
void AdvanceBy(
    TRelative time
)
```

```fsharp
member AdvanceBy : 
        time:'TRelative -> unit 
```

```jscript
public function AdvanceBy(
    time : TRelative
)
```

#### Parameters

- time  
  Type: [TRelative](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)  
  The relative time to advance the scheduler's clock by.

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)





