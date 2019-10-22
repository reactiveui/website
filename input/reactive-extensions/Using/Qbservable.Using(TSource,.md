title: Qbservable.Using<TSource, TResource>()
---
# Qbservable.Using\<TSource, TResource\> Method

Constructs a queryable observable sequence that depends on a resource object.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Using(Of TSource, TResource) ( _
    provider As IQbservableProvider, _
    resourceFactory As Expression(Of Func(Of TResource)), _
    observableFactory As Expression(Of Func(Of TResource, IObservable(Of TSource))) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim resourceFactory As Expression(Of Func(Of TResource))
Dim observableFactory As Expression(Of Func(Of TResource, IObservable(Of TSource)))
Dim returnValue As IQbservable(Of TSource)

returnValue = provider.Using(resourceFactory, _
    observableFactory)
```

```csharp
public static IQbservable<TSource> Using<TSource, TResource>(
    this IQbservableProvider provider,
    Expression<Func<TResource>> resourceFactory,
    Expression<Func<TResource, IObservable<TSource>>> observableFactory
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResource>
static IQbservable<TSource>^ Using(
    IQbservableProvider^ provider, 
    Expression<Func<TResource>^>^ resourceFactory, 
    Expression<Func<TResource, IObservable<TSource>^>^>^ observableFactory
)
```

```fsharp
static member Using : 
        provider:IQbservableProvider * 
        resourceFactory:Expression<Func<'TResource>> * 
        observableFactory:Expression<Func<'TResource, IObservable<'TSource>>> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TResource  
  The type of resource.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- resourceFactory  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<TResource\>\>  
  The factory functions to obtain a resource object.

- observableFactory  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TResource, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>\>  
  The factory function to obtain a queryable observable sequence that depends on the obtained resource.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
The queryable observable sequence whose lifetime controls the lifetime of the dependent resource object.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
