title: Qbservable.Switch<TSource>()
---
# Qbservable.Switch\<TSource\> Method

Transforms a queryable observable sequence of queryable observable sequences into a queryable observable sequence producing values only from the most recent queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Switch(Of TSource) ( _
    sources As IQbservable(Of IObservable(Of TSource)) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim sources As IQbservable(Of IObservable(Of TSource))
Dim returnValue As IQbservable(Of TSource)

returnValue = sources.Switch()
```

```csharp
public static IQbservable<TSource> Switch<TSource>(
    this IQbservable<IObservable<TSource>> sources
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Switch(
    IQbservable<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Switch : 
        sources:IQbservable<IObservable<'TSource>> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- sources  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
  The queryable observable sequence of inner observable sequences.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
The queryable observable sequence that at any point in time produces the elements of the most recent inner queryable observable sequence that has been received.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
