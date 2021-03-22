title: Qbservable.SelectMany<TSource, TCollection, TResult>(IQbservable<TSource>, Expression<Func<TSource, IObservable<TCollection>>>, Expression<Func<TSource, TCollection, TResult>>)
---
# Qbservable.SelectMany\<TSource, TCollection, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, IObservable\<TCollection\>\>\>, Expression\<Func\<TSource, TCollection, TResult\>\>)

Projects each element of a queryable observable sequence to a queryable observable sequence and flattens the resulting queryable observable sequences into one queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SelectMany(Of TSource, TCollection, TResult) ( _
    source As IQbservable(Of TSource), _
    collectionSelector As Expression(Of Func(Of TSource, IObservable(Of TCollection))), _
    resultSelector As Expression(Of Func(Of TSource, TCollection, TResult)) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim collectionSelector As Expression(Of Func(Of TSource, IObservable(Of TCollection)))
Dim resultSelector As Expression(Of Func(Of TSource, TCollection, TResult))
Dim returnValue As IQbservable(Of TResult)

returnValue = source.SelectMany(collectionSelector, _
    resultSelector)
```

```csharp
public static IQbservable<TResult> SelectMany<TSource, TCollection, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, IObservable<TCollection>>> collectionSelector,
    Expression<Func<TSource, TCollection, TResult>> resultSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TCollection, typename TResult>
static IQbservable<TResult>^ SelectMany(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, IObservable<TCollection>^>^>^ collectionSelector, 
    Expression<Func<TSource, TCollection, TResult>^>^ resultSelector
)
```

```fsharp
static member SelectMany : 
        source:IQbservable<'TSource> * 
        collectionSelector:Expression<Func<'TSource, IObservable<'TCollection>>> * 
        resultSelector:Expression<Func<'TSource, 'TCollection, 'TResult>> -> IQbservable<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TCollection  
  The type of collection.

- TResult  
  The type of result.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A queryable observable sequence of elements to project.

- collectionSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TCollection\>\>\>  
  A transform function to apply to each element.

- resultSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TSource, TCollection, TResult\>\>  
  A transform function to apply to each element of the intermediate sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence whose elements are the result of invoking the one-to-many transform function collectionSelector on each element of the input sequence and then mapping each of those sequence elements and their corresponding source element to a result element.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[SelectMany Overload](SelectMany/Qbservable.SelectMany)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.SelectMany\<TSource, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, IObservable\<TResult\>\>\>, Expression\<Func\<Exception, IObservable\<TResult\>\>\>, Expression\<Func\<IObservable\<TResult\>\>\>)

Projects each element of a queryable observable sequence to a queryable observable sequence and flattens the resulting queryable observable sequences into one queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SelectMany(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    onNext As Expression(Of Func(Of TSource, IObservable(Of TResult))), _
    onError As Expression(Of Func(Of Exception, IObservable(Of TResult))), _
    onCompleted As Expression(Of Func(Of IObservable(Of TResult))) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim onNext As Expression(Of Func(Of TSource, IObservable(Of TResult)))
Dim onError As Expression(Of Func(Of Exception, IObservable(Of TResult)))
Dim onCompleted As Expression(Of Func(Of IObservable(Of TResult)))
Dim returnValue As IQbservable(Of TResult)

returnValue = source.SelectMany(onNext, _
    onError, onCompleted)
```

```csharp
public static IQbservable<TResult> SelectMany<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, IObservable<TResult>>> onNext,
    Expression<Func<Exception, IObservable<TResult>>> onError,
    Expression<Func<IObservable<TResult>>> onCompleted
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IQbservable<TResult>^ SelectMany(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, IObservable<TResult>^>^>^ onNext, 
    Expression<Func<Exception^, IObservable<TResult>^>^>^ onError, 
    Expression<Func<IObservable<TResult>^>^>^ onCompleted
)
```

```fsharp
static member SelectMany : 
        source:IQbservable<'TSource> * 
        onNext:Expression<Func<'TSource, IObservable<'TResult>>> * 
        onError:Expression<Func<Exception, IObservable<'TResult>>> * 
        onCompleted:Expression<Func<IObservable<'TResult>>> -> IQbservable<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TResult  
  The type of result.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A queryable observable sequence of elements to project.

- onNext  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>\>  
  A transform function to apply to each element.

- onError  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[Exception](https://msdn.microsoft.com/en-us/library/c18k6c59), [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>\>  
  A transform function to apply when an error occurs in the source sequence.

- onCompleted  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>\>  
  A transform function to apply when the end of the source sequence is reached.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence whose elements are the result of invoking the one-to-many transform function corresponding to each notification in the input sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[SelectMany Overload](SelectMany/Qbservable.SelectMany)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.SelectMany\<TSource, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, IEnumerable\<TResult\>\>\>)

Projects each element of a queryable observable sequence to a queryable observable sequence and flattens the resulting queryable observable sequences into one queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SelectMany(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    selector As Expression(Of Func(Of TSource, IEnumerable(Of TResult))) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim selector As Expression(Of Func(Of TSource, IEnumerable(Of TResult)))
Dim returnValue As IQbservable(Of TResult)

returnValue = source.SelectMany(selector)
```

```csharp
public static IQbservable<TResult> SelectMany<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, IEnumerable<TResult>>> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IQbservable<TResult>^ SelectMany(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, IEnumerable<TResult>^>^>^ selector
)
```

```fsharp
static member SelectMany : 
        source:IQbservable<'TSource> * 
        selector:Expression<Func<'TSource, IEnumerable<'TResult>>> -> IQbservable<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TResult  
  The type of result.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A queryable observable sequence of elements to project.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TResult\>\>\>  
  A transform function to apply to each element.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[SelectMany Overload](SelectMany/Qbservable.SelectMany)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.SelectMany\<TSource, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, IObservable\<TResult\>\>\>)

Projects each element of a queryable observable sequence to a queryable observable sequence and flattens the resulting queryable observable sequences into one queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SelectMany(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    selector As Expression(Of Func(Of TSource, IObservable(Of TResult))) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim selector As Expression(Of Func(Of TSource, IObservable(Of TResult)))
Dim returnValue As IQbservable(Of TResult)

returnValue = source.SelectMany(selector)
```

```csharp
public static IQbservable<TResult> SelectMany<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, IObservable<TResult>>> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IQbservable<TResult>^ SelectMany(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, IObservable<TResult>^>^>^ selector
)
```

```fsharp
static member SelectMany : 
        source:IQbservable<'TSource> * 
        selector:Expression<Func<'TSource, IObservable<'TResult>>> -> IQbservable<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TResult  
  The type of result.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A queryable observable sequence of elements to project.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>\>  
  A transform function to apply to each element.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[SelectMany Overload](SelectMany/Qbservable.SelectMany)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.SelectMany\<TSource, TCollection, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, IEnumerable\<TCollection\>\>\>, Expression\<Func\<TSource, TCollection, TResult\>\>)

Projects each element of a queryable observable sequence to a queryable observable sequence and flattens the resulting queryable observable sequences into one queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SelectMany(Of TSource, TCollection, TResult) ( _
    source As IQbservable(Of TSource), _
    collectionSelector As Expression(Of Func(Of TSource, IEnumerable(Of TCollection))), _
    resultSelector As Expression(Of Func(Of TSource, TCollection, TResult)) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim collectionSelector As Expression(Of Func(Of TSource, IEnumerable(Of TCollection)))
Dim resultSelector As Expression(Of Func(Of TSource, TCollection, TResult))
Dim returnValue As IQbservable(Of TResult)

returnValue = source.SelectMany(collectionSelector, _
    resultSelector)
```

```csharp
public static IQbservable<TResult> SelectMany<TSource, TCollection, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, IEnumerable<TCollection>>> collectionSelector,
    Expression<Func<TSource, TCollection, TResult>> resultSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TCollection, typename TResult>
static IQbservable<TResult>^ SelectMany(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, IEnumerable<TCollection>^>^>^ collectionSelector, 
    Expression<Func<TSource, TCollection, TResult>^>^ resultSelector
)
```

```fsharp
static member SelectMany : 
        source:IQbservable<'TSource> * 
        collectionSelector:Expression<Func<'TSource, IEnumerable<'TCollection>>> * 
        resultSelector:Expression<Func<'TSource, 'TCollection, 'TResult>> -> IQbservable<'TResult> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TCollection  
  The type of collection.

- TResult  
  The type of result.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A queryable observable sequence of elements to project.

- collectionSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TCollection\>\>\>  
  A transform function to apply to each element.

- resultSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TSource, TCollection, TResult\>\>  
  A transform function to apply to each element of the intermediate sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence whose elements are the result of invoking the one-to-many transform function collectionSelector on each element of the input sequence and then mapping each of those sequence elements and their corresponding source element to a result element.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[SelectMany Overload](SelectMany/Qbservable.SelectMany)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.SelectMany\<TSource, TOther\> Method (IQbservable\<TSource\>, IObservable\<TOther\>)

Projects each element of a queryable observable sequence to a queryable observable sequence and flattens the resulting queryable observable sequences into one queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SelectMany(Of TSource, TOther) ( _
    source As IQbservable(Of TSource), _
    other As IObservable(Of TOther) _
) As IQbservable(Of TOther)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim other As IObservable(Of TOther)
Dim returnValue As IQbservable(Of TOther)

returnValue = source.SelectMany(other)
```

```csharp
public static IQbservable<TOther> SelectMany<TSource, TOther>(
    this IQbservable<TSource> source,
    IObservable<TOther> other
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TOther>
static IQbservable<TOther>^ SelectMany(
    IQbservable<TSource>^ source, 
    IObservable<TOther>^ other
)
```

```fsharp
static member SelectMany : 
        source:IQbservable<'TSource> * 
        other:IObservable<'TOther> -> IQbservable<'TOther> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TOther  
  The type of result.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A queryable observable sequence of elements to project.

- other  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TOther\>  
  A transform function to apply to each element.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TOther\>  
A queryable observable sequence whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[SelectMany Overload](SelectMany/Qbservable.SelectMany)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
