title: Qbservable.Window<TSource, TWindowClosing>(IQbservable<TSource>, Expression<Func<IObservable<TWindowClosing>>>)
---
# Qbservable.Window\<TSource, TWindowClosing\> Method (IQbservable\<TSource\>, Expression\<Func\<IObservable\<TWindowClosing\>\>\>)

Projects each element of a queryable observable sequence into consecutive non-overlapping windows.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Window(Of TSource, TWindowClosing) ( _
    source As IQbservable(Of TSource), _
    windowClosingSelector As Expression(Of Func(Of IObservable(Of TWindowClosing))) _
) As IQbservable(Of IObservable(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim windowClosingSelector As Expression(Of Func(Of IObservable(Of TWindowClosing)))
Dim returnValue As IQbservable(Of IObservable(Of TSource))

returnValue = source.Window(windowClosingSelector)
```

```csharp
public static IQbservable<IObservable<TSource>> Window<TSource, TWindowClosing>(
    this IQbservable<TSource> source,
    Expression<Func<IObservable<TWindowClosing>>> windowClosingSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TWindowClosing>
static IQbservable<IObservable<TSource>^>^ Window(
    IQbservable<TSource>^ source, 
    Expression<Func<IObservable<TWindowClosing>^>^>^ windowClosingSelector
)
```

```fsharp
static member Window : 
        source:IQbservable<'TSource> * 
        windowClosingSelector:Expression<Func<IObservable<'TWindowClosing>>> -> IQbservable<IObservable<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TWindowClosing  
  The type of window closing.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence to produce windows over.

- windowClosingSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TWindowClosing\>\>\>  
  A function invoked to define the boundaries of the produced windows.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
A queryable observable sequence of windows.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Window Overload](Window/Qbservable.Window)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Window\<TSource, TWindowOpening, TWindowClosing\> Method (IQbservable\<TSource\>, IObservable\<TWindowOpening\>, Expression\<Func\<TWindowOpening, IObservable\<TWindowClosing\>\>\>)

Projects each element of a queryable observable sequence into zero or more windows.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Window(Of TSource, TWindowOpening, TWindowClosing) ( _
    source As IQbservable(Of TSource), _
    windowOpenings As IObservable(Of TWindowOpening), _
    windowClosingSelector As Expression(Of Func(Of TWindowOpening, IObservable(Of TWindowClosing))) _
) As IQbservable(Of IObservable(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim windowOpenings As IObservable(Of TWindowOpening)
Dim windowClosingSelector As Expression(Of Func(Of TWindowOpening, IObservable(Of TWindowClosing)))
Dim returnValue As IQbservable(Of IObservable(Of TSource))

returnValue = source.Window(windowOpenings, _
    windowClosingSelector)
```

```csharp
public static IQbservable<IObservable<TSource>> Window<TSource, TWindowOpening, TWindowClosing>(
    this IQbservable<TSource> source,
    IObservable<TWindowOpening> windowOpenings,
    Expression<Func<TWindowOpening, IObservable<TWindowClosing>>> windowClosingSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TWindowOpening, typename TWindowClosing>
static IQbservable<IObservable<TSource>^>^ Window(
    IQbservable<TSource>^ source, 
    IObservable<TWindowOpening>^ windowOpenings, 
    Expression<Func<TWindowOpening, IObservable<TWindowClosing>^>^>^ windowClosingSelector
)
```

```fsharp
static member Window : 
        source:IQbservable<'TSource> * 
        windowOpenings:IObservable<'TWindowOpening> * 
        windowClosingSelector:Expression<Func<'TWindowOpening, IObservable<'TWindowClosing>>> -> IQbservable<IObservable<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TWindowOpening  
  The type of window opening.

- TWindowClosing  
  The type of window closing.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence to produce windows over.

- windowOpenings  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TWindowOpening\>  
  The observable sequence whose elements denote the creation of new windows.

- windowClosingSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TWindowOpening, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TWindowClosing\>\>\>  
  A function invoked to define the closing of each produced window.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
A queryable observable sequence of windows.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Window Overload](Window/Qbservable.Window)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
