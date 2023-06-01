title: Qbservable.MinBy<TSource, TKey>(IQbservable<TSource>, Expression<Func<TSource, TKey>>, IComparer<TKey>)
---
# Qbservable.MinBy\<TSource, TKey\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, TKey\>\>, IComparer\<TKey\>)

Returns the elements in a queryable observable sequence with the minimum key value according to the specified comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function MinBy(Of TSource, TKey) ( _
    source As IQbservable(Of TSource), _
    keySelector As Expression(Of Func(Of TSource, TKey)), _
    comparer As IComparer(Of TKey) _
) As IQbservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim keySelector As Expression(Of Func(Of TSource, TKey))
Dim comparer As IComparer(Of TKey)
Dim returnValue As IQbservable(Of IList(Of TSource))

returnValue = source.MinBy(keySelector, _
    comparer)
```

```csharp
public static IQbservable<IList<TSource>> MinBy<TSource, TKey>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, TKey>> keySelector,
    IComparer<TKey> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey>
static IQbservable<IList<TSource>^>^ MinBy(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, TKey>^>^ keySelector, 
    IComparer<TKey>^ comparer
)
```

```fsharp
static member MinBy : 
        source:IQbservable<'TSource> * 
        keySelector:Expression<Func<'TSource, 'TKey>> * 
        comparer:IComparer<'TKey> -> IQbservable<IList<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TKey  
  The type of key.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A queryable observable sequence to get the minimum elements for.

- keySelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>\>  
  The key selector function.

- comparer  
  Type: [System.Collections.Generic.IComparer](https://msdn.microsoft.com/en-us/library/8ehhxeaf)\<TKey\>  
  The comparer used to compare key values.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The elements in a queryable observable sequence with the minimum key value according to the specified comparer.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[MinBy Overload](MinBy/Qbservable.MinBy)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.MinBy\<TSource, TKey\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, TKey\>\>)

Returns the elements in a queryable observable sequence with the minimum key value.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function MinBy(Of TSource, TKey) ( _
    source As IQbservable(Of TSource), _
    keySelector As Expression(Of Func(Of TSource, TKey)) _
) As IQbservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim keySelector As Expression(Of Func(Of TSource, TKey))
Dim returnValue As IQbservable(Of IList(Of TSource))

returnValue = source.MinBy(keySelector)
```

```csharp
public static IQbservable<IList<TSource>> MinBy<TSource, TKey>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, TKey>> keySelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey>
static IQbservable<IList<TSource>^>^ MinBy(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, TKey>^>^ keySelector
)
```

```fsharp
static member MinBy : 
        source:IQbservable<'TSource> * 
        keySelector:Expression<Func<'TSource, 'TKey>> -> IQbservable<IList<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TKey  
  The type of key.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A queryable observable sequence to get the minimum elements for.

- keySelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>\>  
  The key selector function.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The elements in a queryable observable sequence with the minimum key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[MinBy Overload](MinBy/Qbservable.MinBy)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








