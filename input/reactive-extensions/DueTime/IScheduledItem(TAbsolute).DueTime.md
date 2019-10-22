title: IScheduledItem<TAbsolute>.DueTime Property
---
# IScheduledItem\<TAbsolute\>.DueTime Property

Get the absolute time at which the item executes.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
ReadOnly Property DueTime As TAbsolute
    Get
```

```vb
'Usage
Dim instance As IScheduledItem
Dim value As TAbsolute

value = instance.DueTime
```

```csharp
TAbsolute DueTime { get; }
```

```c++
property TAbsolute DueTime {
    TAbsolute get ();
}
```

```fsharp
abstract DueTime : 'TAbsolute
```

```jscript
function get DueTime () : TAbsolute
```

#### Property Value

Type: [TAbsolute](IScheduledItem\IScheduledItem(TAbsolute).md)  
The absolute time.

## See Also

#### Reference

[IScheduledItem\<TAbsolute\> Interface](IScheduledItem\IScheduledItem(TAbsolute).md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)





