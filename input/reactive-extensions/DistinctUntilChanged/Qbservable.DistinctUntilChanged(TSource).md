title: Qbservable.DistinctUntilChanged<TSource>(IQbservable<TSource>, IEqualityComparer<TSource>)
---
# Qbservable.DistinctUntilChanged\<TSource\> Method (IQbservable\<TSource\>, IEqualityComparer\<TSource\>)

Returns a queryable observable sequence that contains only distinct contiguous elements according to the comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function DistinctUntilChanged(Of TSource) ( _
    source As IQbservable(Of TSource), _
    comparer As IEqualityComparer(Of TSource) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim comparer As IEqualityComparer(Of TSource)
Dim returnValue As IQbservable(Of TSource)

returnValue = source.DistinctUntilChanged(comparer)
```

```csharp
public static IQbservable<TSource> DistinctUntilChanged<TSource>(
    this IQbservable<TSource> source,
    IEqualityComparer<TSource> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ DistinctUntilChanged(
    IQbservable<TSource>^ source, 
    IEqualityComparer<TSource>^ comparer
)
```

```fsharp
static member DistinctUntilChanged : 
        source:IQbservable<'TSource> * 
        comparer:IEqualityComparer<'TSource> -> IQbservable<'TSource> 
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
  A queryable observable sequence to retain distinct contiguous elements for.

- comparer  
  Type: [System.Collections.Generic.IEqualityComparer](https://msdn.microsoft.com/en-us/library/ms132151)\<TSource\>  
  The equality comparer for computed key values.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
A queryable observable sequence only containing the distinct contiguous elements from the source sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[DistinctUntilChanged Overload](DistinctUntilChanged/Qbservable.DistinctUntilChanged)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.DistinctUntilChanged\<TSource\> Method (IQbservable\<TSource\>)

Returns a queryable observable sequence that contains only distinct contiguous elements with a specified source.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function DistinctUntilChanged(Of TSource) ( _
    source As IQbservable(Of TSource) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim returnValue As IQbservable(Of TSource)

returnValue = source.DistinctUntilChanged()
```

```csharp
public static IQbservable<TSource> DistinctUntilChanged<TSource>(
    this IQbservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ DistinctUntilChanged(
    IQbservable<TSource>^ source
)
```

```fsharp
static member DistinctUntilChanged : 
        source:IQbservable<'TSource> -> IQbservable<'TSource> 
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
  A queryable observable sequence to retain distinct contiguous elements for.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
A queryable observable sequence only containing the distinct contiguous elements from the source sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[DistinctUntilChanged Overload](DistinctUntilChanged/Qbservable.DistinctUntilChanged)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








