title: Qbservable.Provider Property
---
# Qbservable.Provider Property

Gets the local Qbservable provider.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Shared ReadOnly Property Provider As IQbservableProvider
    Get
```

```vb
'Usage
Dim value As IQbservableProvider

value = Qbservable.Provider
```

```csharp
public static IQbservableProvider Provider { get; }
```

```c++
public:
static property IQbservableProvider^ Provider {
    IQbservableProvider^ get ();
}
```

```fsharp
static member Provider : IQbservableProvider
```

```jscript
static function get Provider () : IQbservableProvider
```

#### Property Value

Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
The provider of a queryable observable sequence.

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
