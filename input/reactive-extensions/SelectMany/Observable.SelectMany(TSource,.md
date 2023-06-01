title: Observable.SelectMany<TSource, TResult>(IObservable<TSource>, Func<TSource, IObservable<TResult>>, Func<Exception, IObservable<TResult>>, Func<IObservable<TResult>>)
---
# Observable.SelectMany\<TSource, TResult\> Method (IObservable\<TSource\>, Func\<TSource, IObservable\<TResult\>\>, Func\<Exception, IObservable\<TResult\>\>, Func\<IObservable\<TResult\>\>)

Projects each element of an observable sequence to an observable sequence and flattens the resulting observable sequences into one observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SelectMany(Of TSource, TResult) ( _
    source As IObservable(Of TSource), _
    onNext As Func(Of TSource, IObservable(Of TResult)), _
    onError As Func(Of Exception, IObservable(Of TResult)), _
    onCompleted As Func(Of IObservable(Of TResult)) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim onNext As Func(Of TSource, IObservable(Of TResult))
Dim onError As Func(Of Exception, IObservable(Of TResult))
Dim onCompleted As Func(Of IObservable(Of TResult))
Dim returnValue As IObservable(Of TResult)

returnValue = source.SelectMany(onNext, _
    onError, onCompleted)
```

```csharp
public static IObservable<TResult> SelectMany<TSource, TResult>(
    this IObservable<TSource> source,
    Func<TSource, IObservable<TResult>> onNext,
    Func<Exception, IObservable<TResult>> onError,
    Func<IObservable<TResult>> onCompleted
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IObservable<TResult>^ SelectMany(
    IObservable<TSource>^ source, 
    Func<TSource, IObservable<TResult>^>^ onNext, 
    Func<Exception^, IObservable<TResult>^>^ onError, 
    Func<IObservable<TResult>^>^ onCompleted
)
```

```fsharp
static member SelectMany : 
        source:IObservable<'TSource> * 
        onNext:Func<'TSource, IObservable<'TResult>> * 
        onError:Func<Exception, IObservable<'TResult>> * 
        onCompleted:Func<IObservable<'TResult>> -> IObservable<'TResult> 
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
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence of elements to project.

- onNext  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
  A transform function to apply to each element.

- onError  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[Exception](https://msdn.microsoft.com/en-us/library/c18k6c59), [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
  A transform function to apply when an error occurs in the source sequence.

- onCompleted  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
  A transform function to apply when the end of the source sequence is reached.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence whose elements are the result of invoking the one-to-many transform function corresponding to each notification in the input sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[SelectMany Overload](SelectMany/Observable.SelectMany)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.SelectMany\<TSource, TOther\> Method (IObservable\<TSource\>, IObservable\<TOther\>)

Projects each element of an observable sequence to an observable sequence and flattens the resulting observable sequences into one observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SelectMany(Of TSource, TOther) ( _
    source As IObservable(Of TSource), _
    other As IObservable(Of TOther) _
) As IObservable(Of TOther)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim other As IObservable(Of TOther)
Dim returnValue As IObservable(Of TOther)

returnValue = source.SelectMany(other)
```

```csharp
public static IObservable<TOther> SelectMany<TSource, TOther>(
    this IObservable<TSource> source,
    IObservable<TOther> other
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TOther>
static IObservable<TOther>^ SelectMany(
    IObservable<TSource>^ source, 
    IObservable<TOther>^ other
)
```

```fsharp
static member SelectMany : 
        source:IObservable<'TSource> * 
        other:IObservable<'TOther> -> IObservable<'TOther> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TOther  
  The other type.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence of elements to project.

- other  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TOther\>  
  An observable sequence to project each element from the source sequence onto.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TOther\>  
An observable sequence whose elements are the result of projecting each source element onto the other sequence and merging all the resulting sequences together.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[SelectMany Overload](SelectMany/Observable.SelectMany)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.SelectMany\<TSource, TCollection, TResult\> Method (IObservable\<TSource\>, Func\<TSource, IEnumerable\<TCollection\>\>, Func\<TSource, TCollection, TResult\>)

Projects each element of an observable sequence to an observable sequence and flattens the resulting observable sequences into one observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SelectMany(Of TSource, TCollection, TResult) ( _
    source As IObservable(Of TSource), _
    collectionSelector As Func(Of TSource, IEnumerable(Of TCollection)), _
    resultSelector As Func(Of TSource, TCollection, TResult) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim collectionSelector As Func(Of TSource, IEnumerable(Of TCollection))
Dim resultSelector As Func(Of TSource, TCollection, TResult)
Dim returnValue As IObservable(Of TResult)

returnValue = source.SelectMany(collectionSelector, _
    resultSelector)
```

```csharp
public static IObservable<TResult> SelectMany<TSource, TCollection, TResult>(
    this IObservable<TSource> source,
    Func<TSource, IEnumerable<TCollection>> collectionSelector,
    Func<TSource, TCollection, TResult> resultSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TCollection, typename TResult>
static IObservable<TResult>^ SelectMany(
    IObservable<TSource>^ source, 
    Func<TSource, IEnumerable<TCollection>^>^ collectionSelector, 
    Func<TSource, TCollection, TResult>^ resultSelector
)
```

```fsharp
static member SelectMany : 
        source:IObservable<'TSource> * 
        collectionSelector:Func<'TSource, IEnumerable<'TCollection>> * 
        resultSelector:Func<'TSource, 'TCollection, 'TResult> -> IObservable<'TResult> 
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
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence of elements to project.

- collectionSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TCollection\>\>  
  A transform function to apply to each element.

- resultSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TSource, TCollection, TResult\>  
  A transform function to apply to each element of the intermediate sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence whose elements are the result of invoking the one-to-many transform function collectionSelector on each element of the input sequence and then mapping each of those sequence elements and their corresponding source element to a result element.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[SelectMany Overload](SelectMany/Observable.SelectMany)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.SelectMany\<TSource, TCollection, TResult\> Method (IObservable\<TSource\>, Func\<TSource, IObservable\<TCollection\>\>, Func\<TSource, TCollection, TResult\>)

Projects each element of an observable sequence to an observable sequence and flattens the resulting observable sequences into one observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SelectMany(Of TSource, TCollection, TResult) ( _
    source As IObservable(Of TSource), _
    collectionSelector As Func(Of TSource, IObservable(Of TCollection)), _
    resultSelector As Func(Of TSource, TCollection, TResult) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim collectionSelector As Func(Of TSource, IObservable(Of TCollection))
Dim resultSelector As Func(Of TSource, TCollection, TResult)
Dim returnValue As IObservable(Of TResult)

returnValue = source.SelectMany(collectionSelector, _
    resultSelector)
```

```csharp
public static IObservable<TResult> SelectMany<TSource, TCollection, TResult>(
    this IObservable<TSource> source,
    Func<TSource, IObservable<TCollection>> collectionSelector,
    Func<TSource, TCollection, TResult> resultSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TCollection, typename TResult>
static IObservable<TResult>^ SelectMany(
    IObservable<TSource>^ source, 
    Func<TSource, IObservable<TCollection>^>^ collectionSelector, 
    Func<TSource, TCollection, TResult>^ resultSelector
)
```

```fsharp
static member SelectMany : 
        source:IObservable<'TSource> * 
        collectionSelector:Func<'TSource, IObservable<'TCollection>> * 
        resultSelector:Func<'TSource, 'TCollection, 'TResult> -> IObservable<'TResult> 
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
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence of elements to project.

- collectionSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TCollection\>\>  
  A transform function to apply to each element.

- resultSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TSource, TCollection, TResult\>  
  A transform function to apply to each element of the intermediate sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence whose elements are the result of invoking the one-to-many transform function collectionSelector on each element of the input sequence and then mapping each of those sequence elements and their corresponding source element to a result element.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[SelectMany Overload](SelectMany/Observable.SelectMany)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.SelectMany\<TSource, TResult\> Method (IObservable\<TSource\>, Func\<TSource, IObservable\<TResult\>\>)

Projects each element of an observable sequence to an observable sequence and flattens the resulting observable sequences into one observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SelectMany(Of TSource, TResult) ( _
    source As IObservable(Of TSource), _
    selector As Func(Of TSource, IObservable(Of TResult)) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim selector As Func(Of TSource, IObservable(Of TResult))
Dim returnValue As IObservable(Of TResult)

returnValue = source.SelectMany(selector)
```

```csharp
public static IObservable<TResult> SelectMany<TSource, TResult>(
    this IObservable<TSource> source,
    Func<TSource, IObservable<TResult>> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IObservable<TResult>^ SelectMany(
    IObservable<TSource>^ source, 
    Func<TSource, IObservable<TResult>^>^ selector
)
```

```fsharp
static member SelectMany : 
        source:IObservable<'TSource> * 
        selector:Func<'TSource, IObservable<'TResult>> -> IObservable<'TResult> 
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
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence of elements to project.

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
  A transform function to apply to each element.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[SelectMany Overload](SelectMany/Observable.SelectMany)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.SelectMany\<TSource, TResult\> Method (IObservable\<TSource\>, Func\<TSource, IEnumerable\<TResult\>\>)

Projects each element of an observable sequence to an observable sequence and flattens the resulting observable sequences into one observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SelectMany(Of TSource, TResult) ( _
    source As IObservable(Of TSource), _
    selector As Func(Of TSource, IEnumerable(Of TResult)) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim selector As Func(Of TSource, IEnumerable(Of TResult))
Dim returnValue As IObservable(Of TResult)

returnValue = source.SelectMany(selector)
```

```csharp
public static IObservable<TResult> SelectMany<TSource, TResult>(
    this IObservable<TSource> source,
    Func<TSource, IEnumerable<TResult>> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IObservable<TResult>^ SelectMany(
    IObservable<TSource>^ source, 
    Func<TSource, IEnumerable<TResult>^>^ selector
)
```

```fsharp
static member SelectMany : 
        source:IObservable<'TSource> * 
        selector:Func<'TSource, IEnumerable<'TResult>> -> IObservable<'TResult> 
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
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence of elements to project.

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TResult\>\>  
  A transform function to apply to each element.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[SelectMany Overload](SelectMany/Observable.SelectMany)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
