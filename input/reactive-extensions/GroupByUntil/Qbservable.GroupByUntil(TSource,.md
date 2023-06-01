title: Qbservable.GroupByUntil<TSource, TKey, TDuration>(IQbservable<TSource>, Expression<Func<TSource, TKey>>, Expression<Func<IGroupedObservable<TKey, TSource>, IObservable<TDuration>>>, IEqualityComparer<TKey>)
---
# Qbservable.GroupByUntil\<TSource, TKey, TDuration\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, TKey\>\>, Expression\<Func\<IGroupedObservable\<TKey, TSource\>, IObservable\<TDuration\>\>\>, IEqualityComparer\<TKey\>)

Groups the elements of a queryable observable sequence according to a specified key selector function and comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupByUntil(Of TSource, TKey, TDuration) ( _
    source As IQbservable(Of TSource), _
    keySelector As Expression(Of Func(Of TSource, TKey)), _
    durationSelector As Expression(Of Func(Of IGroupedObservable(Of TKey, TSource), IObservable(Of TDuration))), _
    comparer As IEqualityComparer(Of TKey) _
) As IQbservable(Of IGroupedObservable(Of TKey, TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim keySelector As Expression(Of Func(Of TSource, TKey))
Dim durationSelector As Expression(Of Func(Of IGroupedObservable(Of TKey, TSource), IObservable(Of TDuration)))
Dim comparer As IEqualityComparer(Of TKey)
Dim returnValue As IQbservable(Of IGroupedObservable(Of TKey, TSource))

returnValue = source.GroupByUntil(keySelector, _
    durationSelector, comparer)
```

```csharp
public static IQbservable<IGroupedObservable<TKey, TSource>> GroupByUntil<TSource, TKey, TDuration>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, TKey>> keySelector,
    Expression<Func<IGroupedObservable<TKey, TSource>, IObservable<TDuration>>> durationSelector,
    IEqualityComparer<TKey> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey, typename TDuration>
static IQbservable<IGroupedObservable<TKey, TSource>^>^ GroupByUntil(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, TKey>^>^ keySelector, 
    Expression<Func<IGroupedObservable<TKey, TSource>^, IObservable<TDuration>^>^>^ durationSelector, 
    IEqualityComparer<TKey>^ comparer
)
```

```fsharp
static member GroupByUntil : 
        source:IQbservable<'TSource> * 
        keySelector:Expression<Func<'TSource, 'TKey>> * 
        durationSelector:Expression<Func<IGroupedObservable<'TKey, 'TSource>, IObservable<'TDuration>>> * 
        comparer:IEqualityComparer<'TKey> -> IQbservable<IGroupedObservable<'TKey, 'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TKey  
  The type key.

- TDuration  
  The type duration.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A queryable observable sequence whose elements to group.

- keySelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>\>  
  A function to extract the key for each element.

- durationSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IGroupedObservable](IGroupedObservable/IGroupedObservable(TKey,)\<TKey, TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TDuration\>\>\>  
  A function to signal the expiration of a group.

- comparer  
  Type: [System.Collections.Generic.IEqualityComparer](https://msdn.microsoft.com/en-us/library/ms132151)\<TKey\>  
  An equality comparer to compare keys with.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IGroupedObservable](IGroupedObservable/IGroupedObservable(TKey,)\<TKey, TSource\>\>  
A sequence of queryable observable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[GroupByUntil Overload](GroupByUntil/Qbservable.GroupByUntil)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.GroupByUntil\<TSource, TKey, TDuration\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, TKey\>\>, Expression\<Func\<IGroupedObservable\<TKey, TSource\>, IObservable\<TDuration\>\>\>)

Groups the elements of a queryable observable sequence according to a specified key selector function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupByUntil(Of TSource, TKey, TDuration) ( _
    source As IQbservable(Of TSource), _
    keySelector As Expression(Of Func(Of TSource, TKey)), _
    durationSelector As Expression(Of Func(Of IGroupedObservable(Of TKey, TSource), IObservable(Of TDuration))) _
) As IQbservable(Of IGroupedObservable(Of TKey, TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim keySelector As Expression(Of Func(Of TSource, TKey))
Dim durationSelector As Expression(Of Func(Of IGroupedObservable(Of TKey, TSource), IObservable(Of TDuration)))
Dim returnValue As IQbservable(Of IGroupedObservable(Of TKey, TSource))

returnValue = source.GroupByUntil(keySelector, _
    durationSelector)
```

```csharp
public static IQbservable<IGroupedObservable<TKey, TSource>> GroupByUntil<TSource, TKey, TDuration>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, TKey>> keySelector,
    Expression<Func<IGroupedObservable<TKey, TSource>, IObservable<TDuration>>> durationSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey, typename TDuration>
static IQbservable<IGroupedObservable<TKey, TSource>^>^ GroupByUntil(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, TKey>^>^ keySelector, 
    Expression<Func<IGroupedObservable<TKey, TSource>^, IObservable<TDuration>^>^>^ durationSelector
)
```

```fsharp
static member GroupByUntil : 
        source:IQbservable<'TSource> * 
        keySelector:Expression<Func<'TSource, 'TKey>> * 
        durationSelector:Expression<Func<IGroupedObservable<'TKey, 'TSource>, IObservable<'TDuration>>> -> IQbservable<IGroupedObservable<'TKey, 'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TKey  
  The type key.

- TDuration  
  The type duration.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A queryable observable sequence whose elements to group.

- keySelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>\>  
  A function to extract the key for each element.

- durationSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IGroupedObservable](IGroupedObservable/IGroupedObservable(TKey,)\<TKey, TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TDuration\>\>\>  
  A function to signal the expiration of a group.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IGroupedObservable](IGroupedObservable/IGroupedObservable(TKey,)\<TKey, TSource\>\>  
A sequence of queryable observable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[GroupByUntil Overload](GroupByUntil/Qbservable.GroupByUntil)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.GroupByUntil\<TSource, TKey, TElement, TDuration\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, TKey\>\>, Expression\<Func\<TSource, TElement\>\>, Expression\<Func\<IGroupedObservable\<TKey, TElement\>, IObservable\<TDuration\>\>\>)

Groups the elements of a queryable observable sequence according to a specified key selector function and selects the resulting elements by using a specified function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupByUntil(Of TSource, TKey, TElement, TDuration) ( _
    source As IQbservable(Of TSource), _
    keySelector As Expression(Of Func(Of TSource, TKey)), _
    elementSelector As Expression(Of Func(Of TSource, TElement)), _
    durationSelector As Expression(Of Func(Of IGroupedObservable(Of TKey, TElement), IObservable(Of TDuration))) _
) As IQbservable(Of IGroupedObservable(Of TKey, TElement))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim keySelector As Expression(Of Func(Of TSource, TKey))
Dim elementSelector As Expression(Of Func(Of TSource, TElement))
Dim durationSelector As Expression(Of Func(Of IGroupedObservable(Of TKey, TElement), IObservable(Of TDuration)))
Dim returnValue As IQbservable(Of IGroupedObservable(Of TKey, TElement))

returnValue = source.GroupByUntil(keySelector, _
    elementSelector, durationSelector)
```

```csharp
public static IQbservable<IGroupedObservable<TKey, TElement>> GroupByUntil<TSource, TKey, TElement, TDuration>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, TKey>> keySelector,
    Expression<Func<TSource, TElement>> elementSelector,
    Expression<Func<IGroupedObservable<TKey, TElement>, IObservable<TDuration>>> durationSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey, typename TElement, typename TDuration>
static IQbservable<IGroupedObservable<TKey, TElement>^>^ GroupByUntil(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, TKey>^>^ keySelector, 
    Expression<Func<TSource, TElement>^>^ elementSelector, 
    Expression<Func<IGroupedObservable<TKey, TElement>^, IObservable<TDuration>^>^>^ durationSelector
)
```

```fsharp
static member GroupByUntil : 
        source:IQbservable<'TSource> * 
        keySelector:Expression<Func<'TSource, 'TKey>> * 
        elementSelector:Expression<Func<'TSource, 'TElement>> * 
        durationSelector:Expression<Func<IGroupedObservable<'TKey, 'TElement>, IObservable<'TDuration>>> -> IQbservable<IGroupedObservable<'TKey, 'TElement>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TKey  
  The type key.

- TElement  
  The type element.

- TDuration  
  The type duration.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A queryable observable sequence whose elements to group.

- keySelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>\>  
  A function to extract the key for each element.

- elementSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TElement\>\>  
  A function to map each source element to an element in an observable group.

- durationSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IGroupedObservable](IGroupedObservable/IGroupedObservable(TKey,)\<TKey, TElement\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TDuration\>\>\>  
  A function to signal the expiration of a group.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IGroupedObservable](IGroupedObservable/IGroupedObservable(TKey,)\<TKey, TElement\>\>  
A sequence of queryable observable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[GroupByUntil Overload](GroupByUntil/Qbservable.GroupByUntil)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.GroupByUntil\<TSource, TKey, TElement, TDuration\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, TKey\>\>, Expression\<Func\<TSource, TElement\>\>, Expression\<Func\<IGroupedObservable\<TKey, TElement\>, IObservable\<TDuration\>\>\>, IEqualityComparer\<TKey\>)

Groups the elements of a queryable observable sequence according to a specified key selector function and comparer and selects the resulting elements by using a specified function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GroupByUntil(Of TSource, TKey, TElement, TDuration) ( _
    source As IQbservable(Of TSource), _
    keySelector As Expression(Of Func(Of TSource, TKey)), _
    elementSelector As Expression(Of Func(Of TSource, TElement)), _
    durationSelector As Expression(Of Func(Of IGroupedObservable(Of TKey, TElement), IObservable(Of TDuration))), _
    comparer As IEqualityComparer(Of TKey) _
) As IQbservable(Of IGroupedObservable(Of TKey, TElement))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim keySelector As Expression(Of Func(Of TSource, TKey))
Dim elementSelector As Expression(Of Func(Of TSource, TElement))
Dim durationSelector As Expression(Of Func(Of IGroupedObservable(Of TKey, TElement), IObservable(Of TDuration)))
Dim comparer As IEqualityComparer(Of TKey)
Dim returnValue As IQbservable(Of IGroupedObservable(Of TKey, TElement))

returnValue = source.GroupByUntil(keySelector, _
    elementSelector, durationSelector, _
    comparer)
```

```csharp
public static IQbservable<IGroupedObservable<TKey, TElement>> GroupByUntil<TSource, TKey, TElement, TDuration>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, TKey>> keySelector,
    Expression<Func<TSource, TElement>> elementSelector,
    Expression<Func<IGroupedObservable<TKey, TElement>, IObservable<TDuration>>> durationSelector,
    IEqualityComparer<TKey> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey, typename TElement, typename TDuration>
static IQbservable<IGroupedObservable<TKey, TElement>^>^ GroupByUntil(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, TKey>^>^ keySelector, 
    Expression<Func<TSource, TElement>^>^ elementSelector, 
    Expression<Func<IGroupedObservable<TKey, TElement>^, IObservable<TDuration>^>^>^ durationSelector, 
    IEqualityComparer<TKey>^ comparer
)
```

```fsharp
static member GroupByUntil : 
        source:IQbservable<'TSource> * 
        keySelector:Expression<Func<'TSource, 'TKey>> * 
        elementSelector:Expression<Func<'TSource, 'TElement>> * 
        durationSelector:Expression<Func<IGroupedObservable<'TKey, 'TElement>, IObservable<'TDuration>>> * 
        comparer:IEqualityComparer<'TKey> -> IQbservable<IGroupedObservable<'TKey, 'TElement>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TKey  
  The type key.

- TElement  
  The type element.

- TDuration  
  The type duration.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A queryable observable sequence whose elements to group.

- keySelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>\>  
  A function to extract the key for each element.

- elementSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TElement\>\>  
  A function to map each source element to an element in an observable group.

- durationSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IGroupedObservable](IGroupedObservable/IGroupedObservable(TKey,)\<TKey, TElement\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TDuration\>\>\>  
  A function to signal the expiration of a group.

- comparer  
  Type: [System.Collections.Generic.IEqualityComparer](https://msdn.microsoft.com/en-us/library/ms132151)\<TKey\>  
  An equality comparer to compare keys with.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IGroupedObservable](IGroupedObservable/IGroupedObservable(TKey,)\<TKey, TElement\>\>  
A sequence of queryable observable groups, each of which corresponds to a unique key value, containing all elements that share that same key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[GroupByUntil Overload](GroupByUntil/Qbservable.GroupByUntil)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








