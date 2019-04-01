# Observable.ElementAt\<TSource\> Method

Returns the element at a specified index in a sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ElementAt(Of TSource) ( _
    source As IObservable(Of TSource), _
    index As Integer _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim index As Integer
Dim returnValue As IObservable(Of TSource)

returnValue = source.ElementAt(index)
```

```csharp
public static IObservable<TSource> ElementAt<TSource>(
    this IObservable<TSource> source,
    int index
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ ElementAt(
    IObservable<TSource>^ source, 
    int index
)
```

```fsharp
static member ElementAt : 
        source:IObservable<'TSource> * 
        index:int -> IObservable<'TSource> 
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
  The observable sequence to return the element from.

- index  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The zero-based index of the element to retrieve.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence that produces the element at the specified position in the source sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








