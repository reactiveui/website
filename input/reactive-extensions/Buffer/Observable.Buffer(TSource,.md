# Observable.Buffer\<TSource, TBufferOpening, TBufferClosing\> Method (IObservable\<TSource\>, IObservable\<TBufferOpening\>, Func\<TBufferOpening, IObservable\<TBufferClosing\>\>)

Indicates each element of a queryable observable sequence into consecutive non-overlapping buffers.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource, TBufferOpening, TBufferClosing) ( _
    source As IObservable(Of TSource), _
    bufferOpenings As IObservable(Of TBufferOpening), _
    bufferClosingSelector As Func(Of TBufferOpening, IObservable(Of TBufferClosing)) _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim bufferOpenings As IObservable(Of TBufferOpening)
Dim bufferClosingSelector As Func(Of TBufferOpening, IObservable(Of TBufferClosing))
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.Buffer(bufferOpenings, _
    bufferClosingSelector)
```

```csharp
public static IObservable<IList<TSource>> Buffer<TSource, TBufferOpening, TBufferClosing>(
    this IObservable<TSource> source,
    IObservable<TBufferOpening> bufferOpenings,
    Func<TBufferOpening, IObservable<TBufferClosing>> bufferClosingSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TBufferOpening, typename TBufferClosing>
static IObservable<IList<TSource>^>^ Buffer(
    IObservable<TSource>^ source, 
    IObservable<TBufferOpening>^ bufferOpenings, 
    Func<TBufferOpening, IObservable<TBufferClosing>^>^ bufferClosingSelector
)
```

```fsharp
static member Buffer : 
        source:IObservable<'TSource> * 
        bufferOpenings:IObservable<'TBufferOpening> * 
        bufferClosingSelector:Func<'TBufferOpening, IObservable<'TBufferClosing>> -> IObservable<IList<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TBufferOpening  
  The type of observable sequence whose elements denote the opening of each produced buffer.

- TBufferClosing  
  The type of observable sequence whose elements denote the closing of each produced buffer.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to produce buffers over.

- bufferOpenings  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TBufferOpening\>  
  The observable sequence whose elements denote the creation of new buffers.

- bufferClosingSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TBufferOpening, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TBufferClosing\>\>  
  The function invoked to define the closing of each produced buffer.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Buffer Overload](Buffer\Observable.Buffer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Buffer\<TSource, TBufferClosing\> Method (IObservable\<TSource\>, Func\<IObservable\<TBufferClosing\>\>)

Indicates each element of an observable sequence into consecutive non-overlapping buffers.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource, TBufferClosing) ( _
    source As IObservable(Of TSource), _
    bufferClosingSelector As Func(Of IObservable(Of TBufferClosing)) _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim bufferClosingSelector As Func(Of IObservable(Of TBufferClosing))
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.Buffer(bufferClosingSelector)
```

```csharp
public static IObservable<IList<TSource>> Buffer<TSource, TBufferClosing>(
    this IObservable<TSource> source,
    Func<IObservable<TBufferClosing>> bufferClosingSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TBufferClosing>
static IObservable<IList<TSource>^>^ Buffer(
    IObservable<TSource>^ source, 
    Func<IObservable<TBufferClosing>^>^ bufferClosingSelector
)
```

```fsharp
static member Buffer : 
        source:IObservable<'TSource> * 
        bufferClosingSelector:Func<IObservable<'TBufferClosing>> -> IObservable<IList<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TBufferClosing  
  The type of observable sequence whose elements denote the closing of each produced buffer.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to produce buffers over.

- bufferClosingSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TBufferClosing\>\>  
  A function invoked to define the boundaries of the produced buffers. A new buffer is started when the previous one is closed.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Buffer Overload](Buffer\Observable.Buffer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








