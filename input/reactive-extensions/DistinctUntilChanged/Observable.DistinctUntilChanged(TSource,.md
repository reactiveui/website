title: Observable.DistinctUntilChanged<TSource, TKey>(IObservable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>)
---
# Observable.DistinctUntilChanged\<TSource, TKey\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>, IEqualityComparer\<TKey\>)

Returns an observable sequence that contains only distinct contiguous elements according to the keySelector and the comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function DistinctUntilChanged(Of TSource, TKey) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey), _
    comparer As IEqualityComparer(Of TKey) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim comparer As IEqualityComparer(Of TKey)
Dim returnValue As IObservable(Of TSource)

returnValue = source.DistinctUntilChanged(keySelector, _
    comparer)
```

```csharp
public static IObservable<TSource> DistinctUntilChanged<TSource, TKey>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector,
    IEqualityComparer<TKey> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey>
static IObservable<TSource>^ DistinctUntilChanged(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector, 
    IEqualityComparer<TKey>^ comparer
)
```

```fsharp
static member DistinctUntilChanged : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> * 
        comparer:IEqualityComparer<'TKey> -> IObservable<'TSource> 
```

```jscript
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
  An observable sequence to retain distinct contiguous elements for, based on a computed key value.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  A function to compute the comparison key for each element.

- comparer  
  Type: [System.Collections.Generic.IEqualityComparer](https://msdn.microsoft.com/en-us/library/ms132151)\<TKey\>  
  The equality comparer for computed key values.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence only containing the distinct contiguous elements, based on a computed key value, from the source sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[DistinctUntilChanged Overload](DistinctUntilChanged\Observable.DistinctUntilChanged.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.DistinctUntilChanged\<TSource, TKey\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>)

Returns an observable sequence that contains only distinct contiguous elements according to the keySelector.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function DistinctUntilChanged(Of TSource, TKey) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim returnValue As IObservable(Of TSource)

returnValue = source.DistinctUntilChanged(keySelector)
```

```csharp
public static IObservable<TSource> DistinctUntilChanged<TSource, TKey>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey>
static IObservable<TSource>^ DistinctUntilChanged(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector
)
```

```fsharp
static member DistinctUntilChanged : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> -> IObservable<'TSource> 
```

```jscript
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
  An observable sequence to retain distinct contiguous elements for, based on a computed key value.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  A function to compute the comparison key for each element.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence only containing the distinct contiguous elements, based on a computed key value from the source sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[DistinctUntilChanged Overload](DistinctUntilChanged\Observable.DistinctUntilChanged.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








