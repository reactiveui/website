title: Qbservable.Contains<TSource>(IQbservable<TSource>, TSource, IEqualityComparer<TSource>)
---
# Qbservable.Contains\<TSource\> Method (IQbservable\<TSource\>, TSource, IEqualityComparer\<TSource\>)

Determines whether an observable sequence contains a specified element by using a specified source type, source, value and comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Contains(Of TSource) ( _
    source As IQbservable(Of TSource), _
    value As TSource, _
    comparer As IEqualityComparer(Of TSource) _
) As IQbservable(Of Boolean)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim value As TSource
Dim comparer As IEqualityComparer(Of TSource)
Dim returnValue As IQbservable(Of Boolean)

returnValue = source.Contains(value, _
    comparer)
```

```csharp
public static IQbservable<bool> Contains<TSource>(
    this IQbservable<TSource> source,
    TSource value,
    IEqualityComparer<TSource> comparer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<bool>^ Contains(
    IQbservable<TSource>^ source, 
    TSource value, 
    IEqualityComparer<TSource>^ comparer
)
```

```fsharp
static member Contains : 
        source:IQbservable<'TSource> * 
        value:'TSource * 
        comparer:IEqualityComparer<'TSource> -> IQbservable<bool> 
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
  An observable sequence in which to locate a value.

- value  
  Type: TSource  
  The value to locate a sequence.

- comparer  
  Type: [System.Collections.Generic.IEqualityComparer](https://msdn.microsoft.com/en-us/library/ms132151)\<TSource\>  
  An equality comparer to compare values.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
True if an observable sequence contains a specified element by using a specified source type, source, value and comparer; otherwise, false.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Contains Overload](Contains/Qbservable.Contains)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.Contains\<TSource\> Method (IQbservable\<TSource\>, TSource)

Determines whether a queryable observable sequence contains a specified element by using the default equality comparer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Contains(Of TSource) ( _
    source As IQbservable(Of TSource), _
    value As TSource _
) As IQbservable(Of Boolean)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim value As TSource
Dim returnValue As IQbservable(Of Boolean)

returnValue = source.Contains(value)
```

```csharp
public static IQbservable<bool> Contains<TSource>(
    this IQbservable<TSource> source,
    TSource value
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<bool>^ Contains(
    IQbservable<TSource>^ source, 
    TSource value
)
```

```fsharp
static member Contains : 
        source:IQbservable<'TSource> * 
        value:'TSource -> IQbservable<bool> 
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
  An observable sequence in which to locate a value.

- value  
  Type: TSource  
  The value to locate a sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
True if a queryable observable sequence contains a specified element by using the default equality comparer; otherwise, false.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Contains Overload](Contains/Qbservable.Contains)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








