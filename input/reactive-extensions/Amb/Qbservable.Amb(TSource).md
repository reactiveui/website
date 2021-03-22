title: Qbservable.Amb<TSource>(IQbservableProvider, IEnumerable<IObservable<TSource>>)
---
# Qbservable.Amb\<TSource\> Method (IQbservableProvider, IEnumerable\<IObservable\<TSource\>\>)

Propagates the queryable observable sequence that reacts first with a specified sources.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Amb(Of TSource) ( _
    provider As IQbservableProvider, _
    sources As IEnumerable(Of IObservable(Of TSource)) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim sources As IEnumerable(Of IObservable(Of TSource))
Dim returnValue As IQbservable(Of TSource)

returnValue = provider.Amb(sources)
```

```csharp
public static IQbservable<TSource> Amb<TSource>(
    this IQbservableProvider provider,
    IEnumerable<IObservable<TSource>> sources
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Amb(
    IQbservableProvider^ provider, 
    IEnumerable<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Amb : 
        provider:IQbservableProvider * 
        sources:IEnumerable<IObservable<'TSource>> -> IQbservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- sources  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
  The queryable observable sources competing to react first.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
A queryable observable sequence that surfaces any of the given sequences, whichever reacted first.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Amb Overload](Amb/Qbservable.Amb)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Amb\<TSource\> Method (IQbservableProvider, array\<IObservable\<TSource\>\[\])

Propagates the queryable observable sequence that reacts first with a specified sources.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Amb(Of TSource) ( _
    provider As IQbservableProvider, _
    ParamArray sources As IObservable(Of TSource)() _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim sources As IObservable(Of TSource)()
Dim returnValue As IQbservable(Of TSource)

returnValue = provider.Amb(sources)
```

```csharp
public static IQbservable<TSource> Amb<TSource>(
    this IQbservableProvider provider,
    params IObservable<TSource>[] sources
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Amb(
    IQbservableProvider^ provider, 
    ... array<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Amb : 
        provider:IQbservableProvider * 
        sources:IObservable<'TSource>[] -> IQbservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- sources  
  Type: array\<[System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\[\]  
  The queryable observable sources competing to react first.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
A queryable observable sequence that surfaces any of the given sequences, whichever reacted first.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Amb Overload](Amb/Qbservable.Amb)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Amb\<TSource\> Method (IQbservable\<TSource\>, IObservable\<TSource\>)

Propagates the queryable observable sequence that reacts first with the specified first and second sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Amb(Of TSource) ( _
    first As IQbservable(Of TSource), _
    second As IObservable(Of TSource) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim first As IQbservable(Of TSource)
Dim second As IObservable(Of TSource)
Dim returnValue As IQbservable(Of TSource)

returnValue = first.Amb(second)
```

```csharp
public static IQbservable<TSource> Amb<TSource>(
    this IQbservable<TSource> first,
    IObservable<TSource> second
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Amb(
    IQbservable<TSource>^ first, 
    IObservable<TSource>^ second
)
```

```fsharp
static member Amb : 
        first:IQbservable<'TSource> * 
        second:IObservable<'TSource> -> IQbservable<'TSource> 
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
  The first queryable observable sequence.

- second  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The second queryable observable sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
A queryable observable sequence that surfaces either of the given sequences, whichever reacted first.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Amb Overload](Amb/Qbservable.Amb)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
