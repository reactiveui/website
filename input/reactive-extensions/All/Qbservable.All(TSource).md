# Qbservable.All\<TSource\> Method

Determines whether all elements of a queryable observable sequence satisfies a condition.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function All(Of TSource) ( _
    source As IQbservable(Of TSource), _
    predicate As Expression(Of Func(Of TSource, Boolean)) _
) As IQbservable(Of Boolean)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim predicate As Expression(Of Func(Of TSource, Boolean))
Dim returnValue As IQbservable(Of Boolean)

returnValue = source.All(predicate)
```

```csharp
public static IQbservable<bool> All<TSource>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, bool>> predicate
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<bool>^ All(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, bool>^>^ predicate
)
```

```fsharp
static member All : 
        source:IQbservable<'TSource> * 
        predicate:Expression<Func<'TSource, bool>> -> IQbservable<bool> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  An observable sequence whose elements to apply the predicate to.

- predicate  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>\>  
  A function to test each element for a condition.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
True if all elements of a queryable observable sequence satisfies a condition; otherwise, false.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








