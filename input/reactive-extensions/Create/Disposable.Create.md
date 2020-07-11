title: Disposable.Create()
---
# Disposable.Create Method

Creates the disposable that invokes the specified action when disposed.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables/System.Reactive.Disposables)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Create ( _
    dispose As Action _
) As IDisposable
```

```vb
'Usage
Dim dispose As Action
Dim returnValue As IDisposable

returnValue = Disposable.Create(dispose)
```

```csharp
public static IDisposable Create(
    Action dispose
)
```

```c++
public:
static IDisposable^ Create(
    Action^ dispose
)
```

```fsharp
static member Create : 
        dispose:Action -> IDisposable 
```

```jscript
public static function Create(
    dispose : Action
) : IDisposable
```

#### Parameters

- dispose  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb534741)  
  The action to run during IDisposable.Dispose.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
The disposable object that runs the given action upon disposal.

## See Also

#### Reference

[Disposable Class](Disposable/Disposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)






