title: Observable.All<TSource>()
---
# Observable.All\<TSource\> Method

Determines whether all elements of an observable sequence satisfies a condition.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function All(Of TSource) ( _
    source As IObservable(Of TSource), _
    predicate As Func(Of TSource, Boolean) _
) As IObservable(Of Boolean)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim predicate As Func(Of TSource, Boolean)
Dim returnValue As IObservable(Of Boolean)

returnValue = source.All(predicate)
```

```csharp
public static IObservable<bool> All<TSource>(
    this IObservable<TSource> source,
    Func<TSource, bool> predicate
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<bool>^ All(
    IObservable<TSource>^ source, 
    Func<TSource, bool>^ predicate
)
```

```fsharp
static member All : 
        source:IObservable<'TSource> * 
        predicate:Func<'TSource, bool> -> IObservable<bool> 
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
  An observable sequence whose elements to apply the predicate to.

- predicate  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
  A function to test each element for a condition.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
True if all elements of an observable sequence satisfies a condition; otherwise, false.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
