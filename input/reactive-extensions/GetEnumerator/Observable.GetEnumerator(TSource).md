title: Observable.GetEnumerator<TSource>()
---
# Observable.GetEnumerator\<TSource\> Method

Returns an enumerator that enumerates all values of the observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function GetEnumerator(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IEnumerator(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IEnumerator(Of TSource)

returnValue = source.GetEnumerator()
```

```csharp
public static IEnumerator<TSource> GetEnumerator<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IEnumerator<TSource>^ GetEnumerator(
    IObservable<TSource>^ source
)
```

```fsharp
static member GetEnumerator : 
        source:IObservable<'TSource> -> IEnumerator<'TSource> 
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
  An observable sequence to get an enumerator for.

#### Return Value

Type: [System.Collections.Generic.IEnumerator](https://msdn.microsoft.com/en-us/library/78dfe2yb)\<TSource\>  
The enumerator that can be used to enumerate over the elements in the observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








