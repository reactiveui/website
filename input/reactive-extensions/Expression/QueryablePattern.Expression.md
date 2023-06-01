title: QueryablePattern.Expression Property
---
# QueryablePattern.Expression Property

Gets the expression tree that is associated with the instance of queryable pattern.

**Namespace:**  [System.Reactive.Joins](System.Reactive.Joins/System.Reactive.Joins)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Public Property Expression As Expression
    Get
    Private Set
```

```vb
'Usage
Dim instance As QueryablePattern
Dim value As Expression

value = instance.Expression
```

```csharp
public Expression Expression { get; private set; }
```

```c++
public:
property Expression^ Expression {
    Expression^ get ();
    private: void set (Expression^ value);
}
```

```fsharp
member Expression : Expression with get, private set
```

```javascript
function get Expression () : Expression
private function set Expression (value : Expression)
```

#### Property Value

Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb356138)  
The expression tree that is associated with the instance of queryable pattern.

## See Also

#### Reference

[QueryablePattern Class](QueryablePattern/QueryablePattern)

[System.Reactive.Joins Namespace](System.Reactive.Joins/System.Reactive.Joins)





