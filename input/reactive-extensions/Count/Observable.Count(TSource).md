title: Observable.Count<TSource>()
---
# Observable.Count\<TSource\> Method

Returns a [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) that represents the total number of elements in an observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Count(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IObservable(Of Integer)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IObservable(Of Integer)

returnValue = source.Count()
```

```csharp
public static IObservable<int> Count<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<int>^ Count(
    IObservable<TSource>^ source
)
```

```fsharp
static member Count : 
        source:IObservable<'TSource> -> IObservable<int> 
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
  An observable sequence that contains elements to be counted.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
A [Int32](https://msdn.microsoft.com/en-us/library/td2s409d) that represents the total number of elements in an observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








