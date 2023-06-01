title: Observable.Switch<TSource>()
---
# Observable.Switch\<TSource\> Method

Transforms an observable sequence of observable sequences into an observable sequence producing values only from the most recent observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Switch(Of TSource) ( _
    sources As IObservable(Of IObservable(Of TSource)) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IObservable(Of IObservable(Of TSource))
Dim returnValue As IObservable(Of TSource)

returnValue = sources.Switch()
```

```csharp
public static IObservable<TSource> Switch<TSource>(
    this IObservable<IObservable<TSource>> sources
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Switch(
    IObservable<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Switch : 
        sources:IObservable<IObservable<'TSource>> -> IObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- sources  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
  The observable sequence of inner observable sequences.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence that at any point in time produces the elements of the most recent inner observable sequence that has been received.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
