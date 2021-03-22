title: Qbservable.SequenceEqual<TSource>(IQbservable<TSource>, IObservable<TSource>, IEqualityComparer<TSource>)
---
# Qbservable.SequenceEqual\<TSource\> Method (IQbservable\<TSource\>, IObservable\<TSource\>, IEqualityComparer\<TSource\>)

Determines whether two sequences are equal by comparing the elements pairwise using a specified equality comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SequenceEqual(Of TSource) ( _
    first As IQbservable(Of TSource), _
    second As IObservable(Of TSource), _
    comparer As IEqualityComparer(Of TSource) _
) As IQbservable(Of Boolean)
```

```vb
'Usage
Dim first As IQbservable(Of TSource)
Dim second As IObservable(Of TSource)
Dim comparer As IEqualityComparer(Of TSource)
Dim returnValue As IQbservable(Of Boolean)

returnValue = first.SequenceEqual(second, _
    comparer)
```

```csharp
public static IQbservable<bool> SequenceEqual<TSource>(
    this IQbservable<TSource> first,
    IObservable<TSource> second,
    IEqualityComparer<TSource> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<bool>^ SequenceEqual(
    IQbservable<TSource>^ first, 
    IObservable<TSource>^ second, 
    IEqualityComparer<TSource>^ comparer
)
```

```fsharp
static member SequenceEqual : 
        first:IQbservable<'TSource> * 
        second:IObservable<'TSource> * 
        comparer:IEqualityComparer<'TSource> -> IQbservable<bool> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- first  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  First queryable observable sequence to compare.

- second  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  Second queryable observable sequence to compare.

- comparer  
  Type: [System.Collections.Generic.IEqualityComparer](https://msdn.microsoft.com/en-us/library/ms132151)\<TSource\>  
  A comparer used to compare elements of both sequences.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
True if two sequences are equal by comparing the elements pairwise using a specified equality comparer; otherwise, false.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[SequenceEqual Overload](SequenceEqual/Qbservable.SequenceEqual)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.SequenceEqual\<TSource\> Method (IQbservable\<TSource\>, IObservable\<TSource\>)

Determines whether two sequences are equal by comparing the elements pairwise.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SequenceEqual(Of TSource) ( _
    first As IQbservable(Of TSource), _
    second As IObservable(Of TSource) _
) As IQbservable(Of Boolean)
```

```vb
'Usage
Dim first As IQbservable(Of TSource)
Dim second As IObservable(Of TSource)
Dim returnValue As IQbservable(Of Boolean)

returnValue = first.SequenceEqual(second)
```

```csharp
public static IQbservable<bool> SequenceEqual<TSource>(
    this IQbservable<TSource> first,
    IObservable<TSource> second
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<bool>^ SequenceEqual(
    IQbservable<TSource>^ first, 
    IObservable<TSource>^ second
)
```

```fsharp
static member SequenceEqual : 
        first:IQbservable<'TSource> * 
        second:IObservable<'TSource> -> IQbservable<bool> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- first  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  First observable sequence to compare.

- second  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  Second observable sequence to compare.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
True if two sequences are equal by comparing the elements pairwise; otherwise, false.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[SequenceEqual Overload](SequenceEqual/Qbservable.SequenceEqual)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
