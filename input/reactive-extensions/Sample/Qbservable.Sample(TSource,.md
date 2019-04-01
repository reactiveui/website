# Qbservable.Sample\<TSource, TSample\> Method (IQbservable\<TSource\>, IObservable\<TSample\>)

Samples the queryable observable sequence at sampling ticks with the specified source and sampler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Sample(Of TSource, TSample) ( _
    source As IQbservable(Of TSource), _
    sampler As IObservable(Of TSample) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim sampler As IObservable(Of TSample)
Dim returnValue As IQbservable(Of TSource)

returnValue = source.Sample(sampler)
```

```csharp
public static IQbservable<TSource> Sample<TSource, TSample>(
    this IQbservable<TSource> source,
    IObservable<TSample> sampler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TSample>
static IQbservable<TSource>^ Sample(
    IQbservable<TSource>^ source, 
    IObservable<TSample>^ sampler
)
```

```fsharp
static member Sample : 
        source:IQbservable<'TSource> * 
        sampler:IObservable<'TSample> -> IQbservable<'TSource> 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to sample.

- sampler  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSample\>  
  The sampling tick sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
The sampled queryable observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Sample Overload](Sample\Qbservable.Sample.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
