# Qbservable.Aggregate\<TSource, TAccumulate\> Method (IQbservable\<TSource\>, TAccumulate, Expression\<Func\<TAccumulate, TSource, TAccumulate\>\>)

Applies an accumulator function over a queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Aggregate(Of TSource, TAccumulate) ( _
    source As IQbservable(Of TSource), _
    seed As TAccumulate, _
    accumulator As Expression(Of Func(Of TAccumulate, TSource, TAccumulate)) _
) As IQbservable(Of TAccumulate)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim seed As TAccumulate
Dim accumulator As Expression(Of Func(Of TAccumulate, TSource, TAccumulate))
Dim returnValue As IQbservable(Of TAccumulate)

returnValue = source.Aggregate(seed, _
    accumulator)
```

```csharp
public static IQbservable<TAccumulate> Aggregate<TSource, TAccumulate>(
    this IQbservable<TSource> source,
    TAccumulate seed,
    Expression<Func<TAccumulate, TSource, TAccumulate>> accumulator
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TAccumulate>
static IQbservable<TAccumulate>^ Aggregate(
    IQbservable<TSource>^ source, 
    TAccumulate seed, 
    Expression<Func<TAccumulate, TSource, TAccumulate>^>^ accumulator
)
```

```fsharp
static member Aggregate : 
        source:IQbservable<'TSource> * 
        seed:'TAccumulate * 
        accumulator:Expression<Func<'TAccumulate, 'TSource, 'TAccumulate>> -> IQbservable<'TAccumulate> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TAccumulate  
  The type of accumulate.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  An observable sequence to aggregate over.

- seed  
  Type: TAccumulate  
  The initial accumulator value.

- accumulator  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TAccumulate, TSource, TAccumulate\>\>  
  An accumulator function to be invoked on each element.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TAccumulate\>  
A queryable observable sequence containing a single element with the final accumulator value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Aggregate Overload](Aggregate\Qbservable.Aggregate.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








