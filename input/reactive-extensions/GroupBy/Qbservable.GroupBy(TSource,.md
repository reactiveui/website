# Qbservable.GroupBy\<TSource, TKey, TElement\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, TKey\>\>, Expression\<Func\<TSource, TElement\>\>)

Groups the elements of a queryable observable sequence and selects the resulting elements by using a specified function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupBy(Of TSource, TKey, TElement) ( _
    source As IQbservable(Of TSource), _
    keySelector As Expression(Of Func(Of TSource, TKey)), _
    elementSelector As Expression(Of Func(Of TSource, TElement)) _
) As IQbservable(Of IGroupedObservable(Of TKey, TElement))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim keySelector As Expression(Of Func(Of TSource, TKey))
Dim elementSelector As Expression(Of Func(Of TSource, TElement))
Dim returnValue As IQbservable(Of IGroupedObservable(Of TKey, TElement))

returnValue = source.GroupBy(keySelector, _
    elementSelector)
```

```csharp
public static IQbservable<IGroupedObservable<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, TKey>> keySelector,
    Expression<Func<TSource, TElement>> elementSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey, typename TElement>
static IQbservable<IGroupedObservable<TKey, TElement>^>^ GroupBy(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, TKey>^>^ keySelector, 
    Expression<Func<TSource, TElement>^>^ elementSelector
)
```

```fsharp
static member GroupBy : 
        source:IQbservable<'TSource> * 
        keySelector:Expression<Func<'TSource, 'TKey>> * 
        elementSelector:Expression<Func<'TSource, 'TElement>> -> IQbservable<IGroupedObservable<'TKey, 'TElement>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TKey  
  The type key.

- TElement  
  The type element.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  A queryable observable sequence whose elements to group.

- keySelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>\>  
  A function to extract the key for each element.

- elementSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TElement\>\>  
  A function to map each source element to an element in an observable group.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[IGroupedObservable](IGroupedObservable\IGroupedObservable(TKey,.md)\<TKey, TElement\>\>  
A sequence of queryable observable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[GroupBy Overload](GroupBy\Qbservable.GroupBy.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.GroupBy\<TSource, TKey\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, TKey\>\>)

Groups the elements of a queryable observable sequence according to a specified key selector function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupBy(Of TSource, TKey) ( _
    source As IQbservable(Of TSource), _
    keySelector As Expression(Of Func(Of TSource, TKey)) _
) As IQbservable(Of IGroupedObservable(Of TKey, TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim keySelector As Expression(Of Func(Of TSource, TKey))
Dim returnValue As IQbservable(Of IGroupedObservable(Of TKey, TSource))

returnValue = source.GroupBy(keySelector)
```

```csharp
public static IQbservable<IGroupedObservable<TKey, TSource>> GroupBy<TSource, TKey>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, TKey>> keySelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey>
static IQbservable<IGroupedObservable<TKey, TSource>^>^ GroupBy(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, TKey>^>^ keySelector
)
```

```fsharp
static member GroupBy : 
        source:IQbservable<'TSource> * 
        keySelector:Expression<Func<'TSource, 'TKey>> -> IQbservable<IGroupedObservable<'TKey, 'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TKey  
  The type key.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  A queryable observable sequence whose elements to group.

- keySelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>\>  
  A function to extract the key for each element.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[IGroupedObservable](IGroupedObservable\IGroupedObservable(TKey,.md)\<TKey, TSource\>\>  
A sequence of queryable observable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[GroupBy Overload](GroupBy\Qbservable.GroupBy.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.GroupBy\<TSource, TKey\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, TKey\>\>, IEqualityComparer\<TKey\>)

Groups the elements of a queryable observable sequence according to a specified key selector function and comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupBy(Of TSource, TKey) ( _
    source As IQbservable(Of TSource), _
    keySelector As Expression(Of Func(Of TSource, TKey)), _
    comparer As IEqualityComparer(Of TKey) _
) As IQbservable(Of IGroupedObservable(Of TKey, TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim keySelector As Expression(Of Func(Of TSource, TKey))
Dim comparer As IEqualityComparer(Of TKey)
Dim returnValue As IQbservable(Of IGroupedObservable(Of TKey, TSource))

returnValue = source.GroupBy(keySelector, _
    comparer)
```

```csharp
public static IQbservable<IGroupedObservable<TKey, TSource>> GroupBy<TSource, TKey>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, TKey>> keySelector,
    IEqualityComparer<TKey> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey>
static IQbservable<IGroupedObservable<TKey, TSource>^>^ GroupBy(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, TKey>^>^ keySelector, 
    IEqualityComparer<TKey>^ comparer
)
```

```fsharp
static member GroupBy : 
        source:IQbservable<'TSource> * 
        keySelector:Expression<Func<'TSource, 'TKey>> * 
        comparer:IEqualityComparer<'TKey> -> IQbservable<IGroupedObservable<'TKey, 'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TKey  
  The type key.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  A queryable observable sequence whose elements to group.

- keySelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>\>  
  A function to extract the key for each element.

- comparer  
  Type: [System.Collections.Generic.IEqualityComparer](https://msdn.microsoft.com/en-us/library/ms132151)\<TKey\>  
  An equality comparer to compare keys with.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[IGroupedObservable](IGroupedObservable\IGroupedObservable(TKey,.md)\<TKey, TSource\>\>  
A sequence of queryable observable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[GroupBy Overload](GroupBy\Qbservable.GroupBy.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.GroupBy\<TSource, TKey, TElement\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, TKey\>\>, Expression\<Func\<TSource, TElement\>\>, IEqualityComparer\<TKey\>)

Groups the elements of a queryable observable sequence according to a specified key selector function and comparer and selects the resulting elements by using a specified function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupBy(Of TSource, TKey, TElement) ( _
    source As IQbservable(Of TSource), _
    keySelector As Expression(Of Func(Of TSource, TKey)), _
    elementSelector As Expression(Of Func(Of TSource, TElement)), _
    comparer As IEqualityComparer(Of TKey) _
) As IQbservable(Of IGroupedObservable(Of TKey, TElement))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim keySelector As Expression(Of Func(Of TSource, TKey))
Dim elementSelector As Expression(Of Func(Of TSource, TElement))
Dim comparer As IEqualityComparer(Of TKey)
Dim returnValue As IQbservable(Of IGroupedObservable(Of TKey, TElement))

returnValue = source.GroupBy(keySelector, _
    elementSelector, comparer)
```

```csharp
public static IQbservable<IGroupedObservable<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, TKey>> keySelector,
    Expression<Func<TSource, TElement>> elementSelector,
    IEqualityComparer<TKey> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey, typename TElement>
static IQbservable<IGroupedObservable<TKey, TElement>^>^ GroupBy(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, TKey>^>^ keySelector, 
    Expression<Func<TSource, TElement>^>^ elementSelector, 
    IEqualityComparer<TKey>^ comparer
)
```

```fsharp
static member GroupBy : 
        source:IQbservable<'TSource> * 
        keySelector:Expression<Func<'TSource, 'TKey>> * 
        elementSelector:Expression<Func<'TSource, 'TElement>> * 
        comparer:IEqualityComparer<'TKey> -> IQbservable<IGroupedObservable<'TKey, 'TElement>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TKey  
  The type key.

- TElement  
  The type element.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  A queryable observable sequence whose elements to group.

- keySelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>\>  
  A function to extract the key for each element.

- elementSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TElement\>\>  
  A function to map each source element to an element in an observable group.

- comparer  
  Type: [System.Collections.Generic.IEqualityComparer](https://msdn.microsoft.com/en-us/library/ms132151)\<TKey\>  
  An equality comparer to compare keys with.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[IGroupedObservable](IGroupedObservable\IGroupedObservable(TKey,.md)\<TKey, TElement\>\>  
A sequence of queryable observable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[GroupBy Overload](GroupBy\Qbservable.GroupBy.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








