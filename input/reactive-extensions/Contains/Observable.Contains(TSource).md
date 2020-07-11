title: Observable.Contains<TSource>(IObservable<TSource>, TSource, IEqualityComparer<TSource>)
---
# Observable.Contains\<TSource\> Method (IObservable\<TSource\>, TSource, IEqualityComparer\<TSource\>)

Determines whether an observable sequence contains a specified element by using a specified System.Collections.Generic.IEqualityComparer\&lt;T\&gt;.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Contains(Of TSource) ( _
    source As IObservable(Of TSource), _
    value As TSource, _
    comparer As IEqualityComparer(Of TSource) _
) As IObservable(Of Boolean)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim value As TSource
Dim comparer As IEqualityComparer(Of TSource)
Dim returnValue As IObservable(Of Boolean)

returnValue = source.Contains(value, _
    comparer)
```

```csharp
public static IObservable<bool> Contains<TSource>(
    this IObservable<TSource> source,
    TSource value,
    IEqualityComparer<TSource> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<bool>^ Contains(
    IObservable<TSource>^ source, 
    TSource value, 
    IEqualityComparer<TSource>^ comparer
)
```

```fsharp
static member Contains : 
        source:IObservable<'TSource> * 
        value:'TSource * 
        comparer:IEqualityComparer<'TSource> -> IObservable<bool> 
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
  An observable sequence in which to locate a value.

- value  
  Type: TSource  
  The value to locate a sequence.

- comparer  
  Type: [System.Collections.Generic.IEqualityComparer](https://msdn.microsoft.com/en-us/library/ms132151)\<TSource\>  
  An equality comparer to compare values.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
True if an observable sequence contains a specified element by using a specified System.Collections.Generic.IEqualityComparer\&lt;T\&gt; otherwise, false.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Contains Overload](Contains/Observable.Contains)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Observable.Contains\<TSource\> Method (IObservable\<TSource\>, TSource)

Determines whether an observable sequence contains a specified element by using the default equality comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Contains(Of TSource) ( _
    source As IObservable(Of TSource), _
    value As TSource _
) As IObservable(Of Boolean)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim value As TSource
Dim returnValue As IObservable(Of Boolean)

returnValue = source.Contains(value)
```

```csharp
public static IObservable<bool> Contains<TSource>(
    this IObservable<TSource> source,
    TSource value
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<bool>^ Contains(
    IObservable<TSource>^ source, 
    TSource value
)
```

```fsharp
static member Contains : 
        source:IObservable<'TSource> * 
        value:'TSource -> IObservable<bool> 
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
  An observable sequence in which to locate a value.

- value  
  Type: TSource  
  The value to locate a sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
True if an observable sequence contains a specified element by using the default equality comparer; otherwise, false.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Contains Overload](Contains/Observable.Contains)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








