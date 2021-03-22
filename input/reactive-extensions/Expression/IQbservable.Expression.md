title: IQbservable.Expression Property
---
# IQbservable.Expression Property

Gets the expression tree that is associated with the instance of IQbservable.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
ReadOnly Property Expression As Expression
    Get
```

```vb
'Usage
Dim instance As IQbservable
Dim value As Expression

value = instance.Expression
```

```csharp
Expression Expression { get; }
```

```c++
property Expression^ Expression {
    Expression^ get ();
}
```

```fsharp
abstract Expression : Expression
```

```javascript
function get Expression () : Expression
```

#### Property Value

Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb356138)  
The expression tree that is associated with the instance of IQbservable.

## See Also

#### Reference

[IQbservable Interface](IQbservable/IQbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)





