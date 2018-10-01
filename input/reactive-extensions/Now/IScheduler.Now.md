# IScheduler.Now Property

Gets the scheduler's notion of current time.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
ReadOnly Property Now As DateTimeOffset
    Get
```

```vb
'Usage
Dim instance As IScheduler
Dim value As DateTimeOffset

value = instance.Now
```

```csharp
DateTimeOffset Now { get; }
```

```c++
property DateTimeOffset Now {
    DateTimeOffset get ();
}
```

```fsharp
abstract Now : DateTimeOffset
```

```jscript
function get Now () : DateTimeOffset
```

#### Property Value

Type: [System.DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)  
The current time.

## See Also

#### Reference

[IScheduler Interface](IScheduler\IScheduler.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)





