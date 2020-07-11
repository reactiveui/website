title: IGroupedObservable<TKey, TElement>.Key Property
---
# IGroupedObservable\<TKey, TElement\>.Key Property

Gets the common key.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
ReadOnly Property Key As TKey
    Get
```

```vb
'Usage
Dim instance As IGroupedObservable
Dim value As TKey

value = instance.Key
```

```csharp
TKey Key { get; }
```

```c++
property TKey Key {
    TKey get ();
}
```

```fsharp
abstract Key : 'TKey
```

```jscript
function get Key () : TKey
```

#### Property Value

Type: [TKey](IGroupedObservable/IGroupedObservable(TKey,)  
The common key.

## See Also

#### Reference

[IGroupedObservable\<TKey, TElement\> Interface](IGroupedObservable/IGroupedObservable(TKey,)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)





