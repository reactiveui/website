title: Observable.ToLookup<TSource, TKey, TElement>(IObservable<TSource>, Func<TSource, TKey>, Func<TSource, TElement>)
---
# Observable.ToLookup\<TSource, TKey, TElement\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>, Func\<TSource, TElement\>)

Creates a lookup from an observable sequence according to a specified key selector function, and an element selector function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToLookup(Of TSource, TKey, TElement) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey), _
    elementSelector As Func(Of TSource, TElement) _
) As IObservable(Of ILookup(Of TKey, TElement))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim elementSelector As Func(Of TSource, TElement)
Dim returnValue As IObservable(Of ILookup(Of TKey, TElement))

returnValue = source.ToLookup(keySelector, _
    elementSelector)
```

```csharp
public static IObservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector,
    Func<TSource, TElement> elementSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey, typename TElement>
static IObservable<ILookup<TKey, TElement>^>^ ToLookup(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector, 
    Func<TSource, TElement>^ elementSelector
)
```

```fsharp
static member ToLookup : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> * 
        elementSelector:Func<'TSource, 'TElement> -> IObservable<ILookup<'TKey, 'TElement>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TKey  
  The type of key.

- TElement  
  The type of element.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence to create a lookup for.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  A function to extract a key from each element.

- elementSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TElement\>  
  A transform function to produce a result element value from each element.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[ILookup](https://msdn.microsoft.com/en-us/library/Bb534291)\<TKey, TElement\>\>  
A lookup from an observable sequence according to a specified key selector function, and an element selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[ToLookup Overload](ToLookup/Observable.ToLookup)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.ToLookup\<TSource, TKey\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>)

Creates a lookup from an observable sequence according to a specified key selector function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToLookup(Of TSource, TKey) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey) _
) As IObservable(Of ILookup(Of TKey, TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim returnValue As IObservable(Of ILookup(Of TKey, TSource))

returnValue = source.ToLookup(keySelector)
```

```csharp
public static IObservable<ILookup<TKey, TSource>> ToLookup<TSource, TKey>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey>
static IObservable<ILookup<TKey, TSource>^>^ ToLookup(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector
)
```

```fsharp
static member ToLookup : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> -> IObservable<ILookup<'TKey, 'TSource>> 
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
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence to create a lookup for.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  A function to extract a key from each element.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[ILookup](https://msdn.microsoft.com/en-us/library/Bb534291)\<TKey, TSource\>\>  
A lookup from an observable sequence according to a specified key selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[ToLookup Overload](ToLookup/Observable.ToLookup)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.ToLookup\<TSource, TKey\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>, IEqualityComparer\<TKey\>)

Creates a lookup from an observable sequence according to a specified key selector function, and a comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToLookup(Of TSource, TKey) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey), _
    comparer As IEqualityComparer(Of TKey) _
) As IObservable(Of ILookup(Of TKey, TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim comparer As IEqualityComparer(Of TKey)
Dim returnValue As IObservable(Of ILookup(Of TKey, TSource))

returnValue = source.ToLookup(keySelector, _
    comparer)
```

```csharp
public static IObservable<ILookup<TKey, TSource>> ToLookup<TSource, TKey>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector,
    IEqualityComparer<TKey> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey>
static IObservable<ILookup<TKey, TSource>^>^ ToLookup(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector, 
    IEqualityComparer<TKey>^ comparer
)
```

```fsharp
static member ToLookup : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> * 
        comparer:IEqualityComparer<'TKey> -> IObservable<ILookup<'TKey, 'TSource>> 
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
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence to create a lookup for.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  A function to extract a key from each element.

- comparer  
  Type: [System.Collections.Generic.IEqualityComparer](https://msdn.microsoft.com/en-us/library/ms132151)\<TKey\>  
  An equality comparer to compare keys.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[ILookup](https://msdn.microsoft.com/en-us/library/Bb534291)\<TKey, TSource\>\>  
A lookup from an observable sequence according to a specified key selector function, and a comparer.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[ToLookup Overload](ToLookup/Observable.ToLookup)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.ToLookup\<TSource, TKey, TElement\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>, Func\<TSource, TElement\>, IEqualityComparer\<TKey\>)

Creates a lookup from an observable sequence according to a specified key selector function, a comparer, and an element selector function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToLookup(Of TSource, TKey, TElement) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey), _
    elementSelector As Func(Of TSource, TElement), _
    comparer As IEqualityComparer(Of TKey) _
) As IObservable(Of ILookup(Of TKey, TElement))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim elementSelector As Func(Of TSource, TElement)
Dim comparer As IEqualityComparer(Of TKey)
Dim returnValue As IObservable(Of ILookup(Of TKey, TElement))

returnValue = source.ToLookup(keySelector, _
    elementSelector, comparer)
```

```csharp
public static IObservable<ILookup<TKey, TElement>> ToLookup<TSource, TKey, TElement>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector,
    Func<TSource, TElement> elementSelector,
    IEqualityComparer<TKey> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey, typename TElement>
static IObservable<ILookup<TKey, TElement>^>^ ToLookup(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector, 
    Func<TSource, TElement>^ elementSelector, 
    IEqualityComparer<TKey>^ comparer
)
```

```fsharp
static member ToLookup : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> * 
        elementSelector:Func<'TSource, 'TElement> * 
        comparer:IEqualityComparer<'TKey> -> IObservable<ILookup<'TKey, 'TElement>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TKey  
  The type of key.

- TElement  
  The type of element.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence to create a lookup for.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  A function to extract a key from each element.

- elementSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TElement\>  
  A transform function to produce a result element value from each element.

- comparer  
  Type: [System.Collections.Generic.IEqualityComparer](https://msdn.microsoft.com/en-us/library/ms132151)\<TKey\>  
  An equality comparer to compare keys.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[ILookup](https://msdn.microsoft.com/en-us/library/Bb534291)\<TKey, TElement\>\>  
A lookup from an observable sequence according to a specified key selector function, a comparer, and an element selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[ToLookup Overload](ToLookup/Observable.ToLookup)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
