# DispatcherScheduler.Dispatcher Property

Gets the dispatcher associated with the DispatcherScheduler.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency\System.Reactive.Concurrency.md)  
**Assembly:**  System.Reactive.Windows.Threading (in System.Reactive.Windows.Threading.dll)

## Syntax

```vb
'Declaration
Public ReadOnly Property Dispatcher As Dispatcher
    Get
```

```vb
'Usage
Dim instance As DispatcherScheduler
Dim value As Dispatcher

value = instance.Dispatcher
```

```csharp
public Dispatcher Dispatcher { get; }
```

```c++
public:
property Dispatcher^ Dispatcher {
    Dispatcher^ get ();
}
```

```fsharp
member Dispatcher : Dispatcher
```

```jscript
function get Dispatcher () : Dispatcher
```

#### Property Value

Type: [System.Windows.Threading.Dispatcher](https://msdn.microsoft.com/en-us/library/ms615907)  
The dispatcher associated with the DispatcherScheduler.

## See Also

#### Reference

[DispatcherScheduler Class](DispatcherScheduler\DispatcherScheduler.md)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency\System.Reactive.Concurrency.md)





