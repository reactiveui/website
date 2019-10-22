title: Observable.MinBy<TSource, TKey>(IObservable<TSource>, Func<TSource, TKey>, IComparer<TKey>)
---
# Observable.MinBy\<TSource, TKey\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>, IComparer\<TKey\>)

Returns the elements in an observable sequence with the minimum key value according to the specified comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function MinBy(Of TSource, TKey) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey), _
    comparer As IComparer(Of TKey) _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim comparer As IComparer(Of TKey)
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.MinBy(keySelector, _
    comparer)
```

```csharp
public static IObservable<IList<TSource>> MinBy<TSource, TKey>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector,
    IComparer<TKey> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey>
static IObservable<IList<TSource>^>^ MinBy(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector, 
    IComparer<TKey>^ comparer
)
```

```fsharp
static member MinBy : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> * 
        comparer:IComparer<'TKey> -> IObservable<IList<'TSource>> 
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
  An observable sequence to get the minimum elements for.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  The key selector function.

- comparer  
  Type: [System.Collections.Generic.IComparer](https://msdn.microsoft.com/en-us/library/8ehhxeaf)\<TKey\>  
  The comparer used to compare key values.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The elements in an observable sequence with the minimum key value according to the specified comparer.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[MinBy Overload](MinBy\Observable.MinBy.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.MinBy\<TSource, TKey\> Method (IObservable\<TSource\>, Func\<TSource, TKey\>)

Returns the elements in an observable sequence with the minimum key value.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function MinBy(Of TSource, TKey) ( _
    source As IObservable(Of TSource), _
    keySelector As Func(Of TSource, TKey) _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim keySelector As Func(Of TSource, TKey)
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.MinBy(keySelector)
```

```csharp
public static IObservable<IList<TSource>> MinBy<TSource, TKey>(
    this IObservable<TSource> source,
    Func<TSource, TKey> keySelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TKey>
static IObservable<IList<TSource>^>^ MinBy(
    IObservable<TSource>^ source, 
    Func<TSource, TKey>^ keySelector
)
```

```fsharp
static member MinBy : 
        source:IObservable<'TSource> * 
        keySelector:Func<'TSource, 'TKey> -> IObservable<IList<'TSource>> 
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
  An observable sequence to get the minimum elements for.

- keySelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TKey\>  
  The key selector function.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The elements in an observable sequence with the minimum key value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[MinBy Overload](MinBy\Observable.MinBy.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








