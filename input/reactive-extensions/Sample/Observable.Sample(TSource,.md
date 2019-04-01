# Observable.Sample\<TSource, TSample\> Method (IObservable\<TSource\>, IObservable\<TSample\>)

Samples the observable sequence at sampling ticks with the specified source and sampler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Sample(Of TSource, TSample) ( _
    source As IObservable(Of TSource), _
    sampler As IObservable(Of TSample) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim sampler As IObservable(Of TSample)
Dim returnValue As IObservable(Of TSource)

returnValue = source.Sample(sampler)
```

```csharp
public static IObservable<TSource> Sample<TSource, TSample>(
    this IObservable<TSource> source,
    IObservable<TSample> sampler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TSample>
static IObservable<TSource>^ Sample(
    IObservable<TSource>^ source, 
    IObservable<TSample>^ sampler
)
```

```fsharp
static member Sample : 
        source:IObservable<'TSource> * 
        sampler:IObservable<'TSample> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TSample  
  The type of sample.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to sample.

- sampler  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSample\>  
  The sampling tick sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The sampled observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Sample Overload](Sample\Observable.Sample.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
