title: Observable.MostRecent<TSource>()
---
# Observable.MostRecent\<TSource\> Method

Samples the most recent value in an observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function MostRecent(Of TSource) ( _
    source As IObservable(Of TSource), _
    initialValue As TSource _
) As IEnumerable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim initialValue As TSource
Dim returnValue As IEnumerable(Of TSource)

returnValue = source.MostRecent(initialValue)
```

```csharp
public static IEnumerable<TSource> MostRecent<TSource>(
    this IObservable<TSource> source,
    TSource initialValue
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IEnumerable<TSource>^ MostRecent(
    IObservable<TSource>^ source, 
    TSource initialValue
)
```

```fsharp
static member MostRecent : 
        source:IObservable<'TSource> * 
        initialValue:'TSource -> IEnumerable<'TSource> 
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
  The source observable sequence.

- initialValue  
  Type: TSource  
  The initial value that will be yielded by the enumerable sequence if no element has been sampled yet.

#### Return Value

Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TSource\>  
The enumerable sequence that returns the last sampled element upon each iteration.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








