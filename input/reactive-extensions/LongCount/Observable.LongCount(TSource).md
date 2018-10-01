# Observable.LongCount\<TSource\> Method

Returns a [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) that represents the total number of elements in an observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function LongCount(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IObservable(Of Long)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IObservable(Of Long)

returnValue = source.LongCount()
```

```csharp
public static IObservable<long> LongCount<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<long long>^ LongCount(
    IObservable<TSource>^ source
)
```

```fsharp
static member LongCount : 
        source:IObservable<'TSource> -> IObservable<int64> 
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

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
A [Int64](https://msdn.microsoft.com/en-us/library/6yy583ek) that represents the total number of elements in an observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








