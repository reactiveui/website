title: Observable.Distinct<TSource>(IObservable<TSource>)
---
# Observable.Distinct\<TSource\> Method (IObservable\<TSource\>)

Returns an observable sequence that contains only distinct elements with a specified source.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Distinct(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = source.Distinct()
```

```csharp
public static IObservable<TSource> Distinct<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Distinct(
    IObservable<TSource>^ source
)
```

```fsharp
static member Distinct : 
        source:IObservable<'TSource> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence to retain distinct elements for.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence only containing the distinct elements from the source sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Distinct Overload](Distinct/Observable.Distinct)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Observable.Distinct\<TSource\> Method (IObservable\<TSource\>, IEqualityComparer\<TSource\>)

Returns an observable sequence that contains only distinct elements according to the comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Distinct(Of TSource) ( _
    source As IObservable(Of TSource), _
    comparer As IEqualityComparer(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim comparer As IEqualityComparer(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = source.Distinct(comparer)
```

```csharp
public static IObservable<TSource> Distinct<TSource>(
    this IObservable<TSource> source,
    IEqualityComparer<TSource> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Distinct(
    IObservable<TSource>^ source, 
    IEqualityComparer<TSource>^ comparer
)
```

```fsharp
static member Distinct : 
        source:IObservable<'TSource> * 
        comparer:IEqualityComparer<'TSource> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence to retain distinct elements for.

- comparer  
  Type: [System.Collections.Generic.IEqualityComparer](https://msdn.microsoft.com/en-us/library/ms132151)\<TSource\>  
  The equality comparer for source elements.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence only containing the distinct elements from the source sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Distinct Overload](Distinct/Observable.Distinct)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








