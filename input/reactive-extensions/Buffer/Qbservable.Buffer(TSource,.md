# Qbservable.Buffer\<TSource, TBufferClosing\> Method (IQbservable\<TSource\>, Expression\<Func\<IObservable\<TBufferClosing\>\>\>)

Indicates each element of a queryable observable sequence into consecutive non-overlapping buffers.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource, TBufferClosing) ( _
    source As IQbservable(Of TSource), _
    bufferClosingSelector As Expression(Of Func(Of IObservable(Of TBufferClosing))) _
) As IQbservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim bufferClosingSelector As Expression(Of Func(Of IObservable(Of TBufferClosing)))
Dim returnValue As IQbservable(Of IList(Of TSource))

returnValue = source.Buffer(bufferClosingSelector)
```

```csharp
public static IQbservable<IList<TSource>> Buffer<TSource, TBufferClosing>(
    this IQbservable<TSource> source,
    Expression<Func<IObservable<TBufferClosing>>> bufferClosingSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TBufferClosing>
static IQbservable<IList<TSource>^>^ Buffer(
    IQbservable<TSource>^ source, 
    Expression<Func<IObservable<TBufferClosing>^>^>^ bufferClosingSelector
)
```

```fsharp
static member Buffer : 
        source:IQbservable<'TSource> * 
        bufferClosingSelector:Expression<Func<IObservable<'TBufferClosing>>> -> IQbservable<IList<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TBufferClosing  
  The type of queryable observable sequence whose elements denote the closing of each produced buffer.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to produce buffers over.

- bufferClosingSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TBufferClosing\>\>\>  
  A function invoked to define the boundaries of the produced buffers. A new buffer is started when the previous one is closed.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The queryable observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Buffer Overload](Buffer\Qbservable.Buffer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.Buffer\<TSource, TBufferOpening, TBufferClosing\> Method (IQbservable\<TSource\>, IObservable\<TBufferOpening\>, Expression\<Func\<TBufferOpening, IObservable\<TBufferClosing\>\>\>)

Indicates each element of a queryable observable sequence into consecutive non-overlapping buffers.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource, TBufferOpening, TBufferClosing) ( _
    source As IQbservable(Of TSource), _
    bufferOpenings As IObservable(Of TBufferOpening), _
    bufferClosingSelector As Expression(Of Func(Of TBufferOpening, IObservable(Of TBufferClosing))) _
) As IQbservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim bufferOpenings As IObservable(Of TBufferOpening)
Dim bufferClosingSelector As Expression(Of Func(Of TBufferOpening, IObservable(Of TBufferClosing)))
Dim returnValue As IQbservable(Of IList(Of TSource))

returnValue = source.Buffer(bufferOpenings, _
    bufferClosingSelector)
```

```csharp
public static IQbservable<IList<TSource>> Buffer<TSource, TBufferOpening, TBufferClosing>(
    this IQbservable<TSource> source,
    IObservable<TBufferOpening> bufferOpenings,
    Expression<Func<TBufferOpening, IObservable<TBufferClosing>>> bufferClosingSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TBufferOpening, typename TBufferClosing>
static IQbservable<IList<TSource>^>^ Buffer(
    IQbservable<TSource>^ source, 
    IObservable<TBufferOpening>^ bufferOpenings, 
    Expression<Func<TBufferOpening, IObservable<TBufferClosing>^>^>^ bufferClosingSelector
)
```

```fsharp
static member Buffer : 
        source:IQbservable<'TSource> * 
        bufferOpenings:IObservable<'TBufferOpening> * 
        bufferClosingSelector:Expression<Func<'TBufferOpening, IObservable<'TBufferClosing>>> -> IQbservable<IList<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TBufferOpening  
  The type of queryable observable sequence whose elements denote the opening of each produced buffer.

- TBufferClosing  
  The type of queryable observable sequence whose elements denote the closing of each produced buffer.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to produce buffers over.

- bufferOpenings  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TBufferOpening\>  
  The queryable observable sequence whose elements denote the creation of new buffers.

- bufferClosingSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TBufferOpening, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TBufferClosing\>\>\>  
  The function invoked to define the closing of each produced buffer.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The queryable observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Buffer Overload](Buffer\Qbservable.Buffer.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








