title: Disposable.Empty Property
---
# Disposable.Empty Property

Gets the disposable that does nothing when disposed.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables/System.Reactive.Disposables)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared ReadOnly Property Empty As IDisposable
    Get
```

```vb
'Usage
Dim value As IDisposable

value = Disposable.Empty
```

```csharp
public static IDisposable Empty { get; }
```

```c++
public:
static property IDisposable^ Empty {
    IDisposable^ get ();
}
```

```fsharp
static member Empty : IDisposable
```

```javascript
static function get Empty () : IDisposable
```

#### Property Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
The disposable that does nothing when disposed.

## See Also

#### Reference

[Disposable Class](Disposable/Disposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)





