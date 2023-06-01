title: Qbservable.OnErrorResumeNext<TSource>(IQbservableProvider, IEnumerable<IObservable<TSource>>)
---
# Qbservable.OnErrorResumeNext\<TSource\> Method (IQbservableProvider, IEnumerable\<IObservable\<TSource\>\>)

Continues a queryable observable sequence that is terminated normally or by an exception with the next queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function OnErrorResumeNext(Of TSource) ( _
    provider As IQbservableProvider, _
    sources As IEnumerable(Of IObservable(Of TSource)) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim sources As IEnumerable(Of IObservable(Of TSource))
Dim returnValue As IQbservable(Of TSource)

returnValue = provider.OnErrorResumeNext(sources)
```

```csharp
public static IQbservable<TSource> OnErrorResumeNext<TSource>(
    this IQbservableProvider provider,
    IEnumerable<IObservable<TSource>> sources
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ OnErrorResumeNext(
    IQbservableProvider^ provider, 
    IEnumerable<IObservable<TSource>^>^ sources
)
```

```fsharp
static member OnErrorResumeNext : 
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
  The queryable observable sequences to concatenate.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
A queryable observable sequence that concatenates the source sequences, even if a sequence terminates exceptionally.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[OnErrorResumeNext Overload](OnErrorResumeNext/Qbservable.OnErrorResumeNext)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.OnErrorResumeNext\<TSource\> Method (IQbservableProvider, array\<IObservable\<TSource\>\[\])

Continues a queryable observable sequence that is terminated normally or by an exception with the next queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function OnErrorResumeNext(Of TSource) ( _
    provider As IQbservableProvider, _
    ParamArray sources As IObservable(Of TSource)() _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim sources As IObservable(Of TSource)()
Dim returnValue As IQbservable(Of TSource)

returnValue = provider.OnErrorResumeNext(sources)
```

```csharp
public static IQbservable<TSource> OnErrorResumeNext<TSource>(
    this IQbservableProvider provider,
    params IObservable<TSource>[] sources
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ OnErrorResumeNext(
    IQbservableProvider^ provider, 
    ... array<IObservable<TSource>^>^ sources
)
```

```fsharp
static member OnErrorResumeNext : 
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
  The queryable observable sequences to concatenate.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
A queryable observable sequence that concatenates the source sequences, even if a sequence terminates exceptionally.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[OnErrorResumeNext Overload](OnErrorResumeNext/Qbservable.OnErrorResumeNext)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.OnErrorResumeNext\<TSource\> Method (IQbservable\<TSource\>, IObservable\<TSource\>)

Continues a queryable observable sequence that is terminated normally or by an exception with the next queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function OnErrorResumeNext(Of TSource) ( _
    first As IQbservable(Of TSource), _
    second As IObservable(Of TSource) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim first As IQbservable(Of TSource)
Dim second As IObservable(Of TSource)
Dim returnValue As IQbservable(Of TSource)

returnValue = first.OnErrorResumeNext(second)
```

```csharp
public static IQbservable<TSource> OnErrorResumeNext<TSource>(
    this IQbservable<TSource> first,
    IObservable<TSource> second
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ OnErrorResumeNext(
    IQbservable<TSource>^ first, 
    IObservable<TSource>^ second
)
```

```fsharp
static member OnErrorResumeNext : 
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
  The first queryable observable sequence whose exception (if any) is caught.

- second  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  Second queryable observable sequence used to produce results after the first sequence terminates.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
A queryable observable sequence that concatenates the first and second sequence, even if the first sequence terminates exceptionally.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[OnErrorResumeNext Overload](OnErrorResumeNext/Qbservable.OnErrorResumeNext)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
