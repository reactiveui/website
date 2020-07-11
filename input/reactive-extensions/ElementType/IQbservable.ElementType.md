title: IQbservable.ElementType Property
---
# IQbservable.ElementType Property

Gets the type of the element(s) that are returned when the expression tree associated with this instance of IQbservable is executed.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
ReadOnly Property ElementType As Type
    Get
```

```vb
'Usage
Dim instance As IQbservable
Dim value As Type

value = instance.ElementType
```

```csharp
Type ElementType { get; }
```

```c++
property Type^ ElementType {
    Type^ get ();
}
```

```fsharp
abstract ElementType : Type
```

```jscript
function get ElementType () : Type
```

#### Property Value

Type: [System.Type](https://msdn.microsoft.com/en-us/library/42892f65)  
The type of the element(s) that are returned when the expression tree associated with this instance.

## See Also

#### Reference

[IQbservable Interface](IQbservable/IQbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)





