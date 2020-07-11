title: Qbservable.Scan<TSource>(IQbservable<TSource>, Expression<Func<TSource, TSource, TSource>>)
---
# Qbservable.Scan\<TSource\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, TSource, TSource\>\>)

Applies an accumulator function over a queryable observable sequence and returns each intermediate result with the specified source and accumulator.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Scan(Of TSource) ( _
    source As IQbservable(Of TSource), _
    accumulator As Expression(Of Func(Of TSource, TSource, TSource)) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim accumulator As Expression(Of Func(Of TSource, TSource, TSource))
Dim returnValue As IQbservable(Of TSource)

returnValue = source.Scan(accumulator)
```

```csharp
public static IQbservable<TSource> Scan<TSource>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, TSource, TSource>> accumulator
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Scan(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, TSource, TSource>^>^ accumulator
)
```

```fsharp
static member Scan : 
        source:IQbservable<'TSource> * 
        accumulator:Expression<Func<'TSource, 'TSource, 'TSource>> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A queryable observable sequence to accumulate over.

- accumulator  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TSource, TSource, TSource\>\>  
  An accumulator function to be invoked on each element.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
A queryable observable sequence containing the accumulated values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Scan Overload](Scan/Qbservable.Scan)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
