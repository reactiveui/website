title: VirtualTimeSchedulerBase<TAbsolute, TRelative>.AdvanceTo()
---
# VirtualTimeSchedulerBase\<TAbsolute, TRelative\>.AdvanceTo Method

Advances the scheduler's clock to the specified time, running all work till that point.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub AdvanceTo ( _
    time As TAbsolute _
)
```

```vb
'Usage
Dim instance As VirtualTimeSchedulerBase
Dim time As TAbsolute

instance.AdvanceTo(time)
```

```csharp
public void AdvanceTo(
    TAbsolute time
)
```

```c++
public:
void AdvanceTo(
    TAbsolute time
)
```

```fsharp
member AdvanceTo : 
        time:'TAbsolute -> unit 
```

```javascript
public function AdvanceTo(
    time : TAbsolute
)
```

#### Parameters

- time  
  Type: [TAbsolute](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)  
  The absolute time to advance the scheduler's clock to.

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)





