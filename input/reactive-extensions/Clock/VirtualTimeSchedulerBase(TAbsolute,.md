title: VirtualTimeSchedulerBase<TAbsolute, TRelative>.Clock Property
---
# VirtualTimeSchedulerBase\<TAbsolute, TRelative\>.Clock Property

Gets the scheduler's absolute time clock value.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Property Clock As TAbsolute
    Get
    Protected Set
```

```vb
'Usage
Dim instance As VirtualTimeSchedulerBase
Dim value As TAbsolute

value = instance.Clock

instance.Clock = value
```

```csharp
public TAbsolute Clock { get; protected set; }
```

```c++
public:
property TAbsolute Clock {
    TAbsolute get ();
    protected: void set (TAbsolute value);
}
```

```fsharp
member Clock : 'TAbsolute with get, set
```

```jscript
function get Clock () : TAbsolute
protected function set Clock (value : TAbsolute)
```

#### Property Value

Type: [TAbsolute](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md)  
The scheduler’s absolute time clock value.

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase\VirtualTimeSchedulerBase(TAbsolute,.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)





