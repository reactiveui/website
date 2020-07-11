title: IQbservableProvider.CreateQuery<TResult>()
---
# IQbservableProvider.CreateQuery\<TResult\> Method

Constructs an IQbservable\>TResult\< object that can evaluate the query represented by a specified expression tree.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
Function CreateQuery(Of TResult) ( _
    expression As Expression _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim instance As IQbservableProvider
Dim expression As Expression
Dim returnValue As IQbservable(Of TResult)

returnValue = instance.CreateQuery(expression)
```

```csharp
IQbservable<TResult> CreateQuery<TResult>(
    Expression expression
)
```

```c++
generic<typename TResult>
IQbservable<TResult>^ CreateQuery(
    Expression^ expression
)
```

```fsharp
abstract CreateQuery : 
        expression:Expression -> IQbservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult

#### Parameters

- expression  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb356138)  
  Expression tree representing the query.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
IQbservable object that can evaluate the given query expression.

## See Also

#### Reference

[IQbservableProvider Interface](IQbservableProvider/IQbservableProvider)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)







