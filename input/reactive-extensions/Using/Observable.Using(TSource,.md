title: Observable.Using<TSource, TResource>()
---
# Observable.Using\<TSource, TResource\> Method

Constructs an observable sequence that depends on a resource object.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Using(Of TSource, TResource As IDisposable) ( _
    resourceFactory As Func(Of TResource), _
    observableFactory As Func(Of TResource, IObservable(Of TSource)) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim resourceFactory As Func(Of TResource)
Dim observableFactory As Func(Of TResource, IObservable(Of TSource))
Dim returnValue As IObservable(Of TSource)

returnValue = Observable.Using(resourceFactory, _
    observableFactory)
```

```csharp
public static IObservable<TSource> Using<TSource, TResource>(
    Func<TResource> resourceFactory,
    Func<TResource, IObservable<TSource>> observableFactory
)
where TResource : IDisposable
```

```c++
public:
generic<typename TSource, typename TResource>
where TResource : IDisposable
static IObservable<TSource>^ Using(
    Func<TResource>^ resourceFactory, 
    Func<TResource, IObservable<TSource>^>^ observableFactory
)
```

```fsharp
static member Using : 
        resourceFactory:Func<'TResource> * 
        observableFactory:Func<'TResource, IObservable<'TSource>> -> IObservable<'TSource>  when 'TResource : IDisposable
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

- resourceFactory  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<TResource\>  
  The factory function to obtain a resource object.

- observableFactory  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TResource, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
  The factory function to obtain an observable sequence that depends on the obtained resource.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence whose lifetime controls the lifetime of the dependent resource object.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
