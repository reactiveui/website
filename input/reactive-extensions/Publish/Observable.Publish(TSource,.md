title: Observable.Publish<TSource, TResult>(IObservable<TSource>, Func<IObservable<TSource>, IObservable<TResult>>, TSource)
---
# Observable.Publish\<TSource, TResult\> Method (IObservable\<TSource\>, Func\<IObservable\<TSource\>, IObservable\<TResult\>\>, TSource)

Returns a connectable observable sequence that shares a single subscription to the underlying sequence and starts with initialValue.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Publish(Of TSource, TResult) ( _
    source As IObservable(Of TSource), _
    selector As Func(Of IObservable(Of TSource), IObservable(Of TResult)), _
    initialValue As TSource _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim selector As Func(Of IObservable(Of TSource), IObservable(Of TResult))
Dim initialValue As TSource
Dim returnValue As IObservable(Of TResult)

returnValue = source.Publish(selector, _
    initialValue)
```

```csharp
public static IObservable<TResult> Publish<TSource, TResult>(
    this IObservable<TSource> source,
    Func<IObservable<TSource>, IObservable<TResult>> selector,
    TSource initialValue
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IObservable<TResult>^ Publish(
    IObservable<TSource>^ source, 
    Func<IObservable<TSource>^, IObservable<TResult>^>^ selector, 
    TSource initialValue
)
```

```fsharp
static member Publish : 
        source:IObservable<'TSource> * 
        selector:Func<IObservable<'TSource>, IObservable<'TResult>> * 
        initialValue:'TSource -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TResult  
  The type of Result.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
  The selector function which can use the multicasted source sequence as many times as needed, without causing multiple subscriptions to the source sequence.

- initialValue  
  Type: TSource  
  The initial value received by observers upon subscription.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Publish Overload](Publish/Observable.Publish)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Publish\<TSource, TResult\> Method (IObservable\<TSource\>, Func\<IObservable\<TSource\>, IObservable\<TResult\>\>)

Returns an observable sequence that is the result of invoking the selector on a connectable observable sequence that shares a single subscription to the underlying sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Publish(Of TSource, TResult) ( _
    source As IObservable(Of TSource), _
    selector As Func(Of IObservable(Of TSource), IObservable(Of TResult)) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim selector As Func(Of IObservable(Of TSource), IObservable(Of TResult))
Dim returnValue As IObservable(Of TResult)

returnValue = source.Publish(selector)
```

```csharp
public static IObservable<TResult> Publish<TSource, TResult>(
    this IObservable<TSource> source,
    Func<IObservable<TSource>, IObservable<TResult>> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IObservable<TResult>^ Publish(
    IObservable<TSource>^ source, 
    Func<IObservable<TSource>^, IObservable<TResult>^>^ selector
)
```

```fsharp
static member Publish : 
        source:IObservable<'TSource> * 
        selector:Func<IObservable<'TSource>, IObservable<'TResult>> -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TResult  
  The type of result.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
  The selector function which can use the multicasted source sequence as many times as needed, without causing multiple subscriptions to the source sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Publish Overload](Publish/Observable.Publish)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
