title: VirtualTimeSchedulerBase<TAbsolute, TRelative>.IsEnabled Property
---
# VirtualTimeSchedulerBase\<TAbsolute, TRelative\>.IsEnabled Property

Gets whether the scheduler is enabled to run work.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Property IsEnabled As Boolean
    Get
    Private Set
```

```vb
'Usage
Dim instance As VirtualTimeSchedulerBase
Dim value As Boolean

value = instance.IsEnabled
```

```csharp
public bool IsEnabled { get; private set; }
```

```c++
public:
property bool IsEnabled {
    bool get ();
    private: void set (bool value);
}
```

```fsharp
member IsEnabled : bool with get, private set
```

```javascript
function get IsEnabled () : boolean
private function set IsEnabled (value : boolean)
```

#### Property Value

Type: [System.Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)  
true if the scheduler is enabled to run work; otherwise, false.

## See Also

#### Reference

[VirtualTimeSchedulerBase\<TAbsolute, TRelative\> Class](VirtualTimeSchedulerBase/VirtualTimeSchedulerBase(TAbsolute,)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)





