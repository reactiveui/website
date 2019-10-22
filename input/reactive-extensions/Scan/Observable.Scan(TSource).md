title: Observable.Scan<TSource>(IObservable<TSource>, Func<TSource, TSource, TSource>)
---
# Observable.Scan\<TSource\> Method (IObservable\<TSource\>, Func\<TSource, TSource, TSource\>)

Applies an accumulator function over an observable sequence and returns each intermediate result with the specified source and accumulator.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Scan(Of TSource) ( _
    source As IObservable(Of TSource), _
    accumulator As Func(Of TSource, TSource, TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim accumulator As Func(Of TSource, TSource, TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = source.Scan(accumulator)
```

```csharp
public static IObservable<TSource> Scan<TSource>(
    this IObservable<TSource> source,
    Func<TSource, TSource, TSource> accumulator
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Scan(
    IObservable<TSource>^ source, 
    Func<TSource, TSource, TSource>^ accumulator
)
```

```fsharp
static member Scan : 
        source:IObservable<'TSource> * 
        accumulator:Func<'TSource, 'TSource, 'TSource> -> IObservable<'TSource> 
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
  An observable sequence to accumulate over.

- accumulator  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TSource, TSource, TSource\>  
  An accumulator function to be invoked on each element.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence containing the accumulated values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Scan Overload](Scan\Observable.Scan.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
