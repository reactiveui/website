title: IQbservable.Provider Property
---
# IQbservable.Provider Property

Gets the query provider that is associated with this data source.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
ReadOnly Property Provider As IQbservableProvider
    Get
```

```vb
'Usage
Dim instance As IQbservable
Dim value As IQbservableProvider

value = instance.Provider
```

```csharp
IQbservableProvider Provider { get; }
```

```c++
property IQbservableProvider^ Provider {
    IQbservableProvider^ get ();
}
```

```fsharp
abstract Provider : IQbservableProvider
```

```javascript
function get Provider () : IQbservableProvider
```

#### Property Value

Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
The query provider that is associated with this data source.

## See Also

#### Reference

[IQbservable Interface](IQbservable/IQbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
