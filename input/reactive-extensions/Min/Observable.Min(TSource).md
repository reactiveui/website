title: Observable.Min<TSource>(IObservable<TSource>)
---
# Observable.Min\<TSource\> Method (IObservable\<TSource\>)

Returns the minimum element in an observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = source.Min()
```

```csharp
public static IObservable<TSource> Min<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Min(
    IObservable<TSource>^ source
)
```

```fsharp
static member Min : 
        source:IObservable<'TSource> -> IObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence to determine the minimum element of.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The minimum element in an observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Min Overload](Min/Observable.Min)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Observable.Min\<TSource\> Method (IObservable\<TSource\>, IComparer\<TSource\>)

Returns the minimum value in an observable sequence according to the specified comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Min(Of TSource) ( _
    source As IObservable(Of TSource), _
    comparer As IComparer(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim comparer As IComparer(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = source.Min(comparer)
```

```csharp
public static IObservable<TSource> Min<TSource>(
    this IObservable<TSource> source,
    IComparer<TSource> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Min(
    IObservable<TSource>^ source, 
    IComparer<TSource>^ comparer
)
```

```fsharp
static member Min : 
        source:IObservable<'TSource> * 
        comparer:IComparer<'TSource> -> IObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence to determine the minimum element of.

- comparer  
  Type: [System.Collections.Generic.IComparer](https://msdn.microsoft.com/en-us/library/8ehhxeaf)\<TSource\>  
  The comparer used to compare elements.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The minimum value in an observable sequence according to the specified comparer.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Min Overload](Min/Observable.Min)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








