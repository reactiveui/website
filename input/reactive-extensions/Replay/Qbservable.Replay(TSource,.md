title: Qbservable.Replay<TSource, TResult>(IQbservable<TSource>, Expression<Func<IObservable<TSource>, IObservable<TResult>>>, Int32, TimeSpan, IScheduler)
---
# Qbservable.Replay\<TSource, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<IObservable\<TSource\>, IObservable\<TResult\>\>\>, Int32, TimeSpan, IScheduler)

Returns a queryable observable sequence that is the result of invoking the selector on a connectable queryable observable sequence that shares a single subscription to the underlying sequence replaying bufferSize notifications within window.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult))), _
    bufferSize As Integer, _
    window As TimeSpan, _
    scheduler As IScheduler _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult)))
Dim bufferSize As Integer
Dim window As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of TResult)

returnValue = source.Replay(selector, _
    bufferSize, window, scheduler)
```

```csharp
public static IQbservable<TResult> Replay<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
    int bufferSize,
    TimeSpan window,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IQbservable<TResult>^ Replay(
    IQbservable<TSource>^ source, 
    Expression<Func<IObservable<TSource>^, IObservable<TResult>^>^>^ selector, 
    int bufferSize, 
    TimeSpan window, 
    IScheduler^ scheduler
)
```

```fsharp
static member Replay : 
        source:IQbservable<'TSource> * 
        selector:Expression<Func<IObservable<'TSource>, IObservable<'TResult>>> * 
        bufferSize:int * 
        window:TimeSpan * 
        scheduler:IScheduler -> IQbservable<'TResult> 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence whose elements will be multicasting through a single shared subscription.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>\>  
  The selector function which can use the multicasted source sequence as many times as needed, without causing multiple subscriptions to the source sequence.

- bufferSize  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum element count of the replay buffer.

- window  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The maximum time length of the replay buffer.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler where connected observers within the selector function will be invoked on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Replay Overload](Replay/Qbservable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Replay\<TSource, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<IObservable\<TSource\>, IObservable\<TResult\>\>\>, Int32, IScheduler)

Returns a queryable observable sequence that is the result of invoking the selector on a connectable queryable observable sequence that shares a single subscription to the underlying sequence replaying bufferSize notifications.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult))), _
    bufferSize As Integer, _
    scheduler As IScheduler _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult)))
Dim bufferSize As Integer
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of TResult)

returnValue = source.Replay(selector, _
    bufferSize, scheduler)
```

```csharp
public static IQbservable<TResult> Replay<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
    int bufferSize,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IQbservable<TResult>^ Replay(
    IQbservable<TSource>^ source, 
    Expression<Func<IObservable<TSource>^, IObservable<TResult>^>^>^ selector, 
    int bufferSize, 
    IScheduler^ scheduler
)
```

```fsharp
static member Replay : 
        source:IQbservable<'TSource> * 
        selector:Expression<Func<IObservable<'TSource>, IObservable<'TResult>>> * 
        bufferSize:int * 
        scheduler:IScheduler -> IQbservable<'TResult> 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence whose elements will be multicasting through a single shared subscription.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>\>  
  The selector function which can use the multicasted source sequence as many times as needed, without causing multiple subscriptions to the source sequence.

- bufferSize  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum element count of the replay buffer.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler where connected observers within the selector function will be invoked on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Replay Overload](Replay/Qbservable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Replay\<TSource, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<IObservable\<TSource\>, IObservable\<TResult\>\>\>)

Returns a queryable observable sequence that is the result of invoking the selector on a connectable observable sequence that shares a single subscription to the underlying sequence and starts with initial value.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult))) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult)))
Dim returnValue As IQbservable(Of TResult)

returnValue = source.Replay(selector)
```

```csharp
public static IQbservable<TResult> Replay<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IQbservable<TResult>^ Replay(
    IQbservable<TSource>^ source, 
    Expression<Func<IObservable<TSource>^, IObservable<TResult>^>^>^ selector
)
```

```fsharp
static member Replay : 
        source:IQbservable<'TSource> * 
        selector:Expression<Func<IObservable<'TSource>, IObservable<'TResult>>> -> IQbservable<'TResult> 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>\>  
  The selector function which can use the multicasted source sequence as many times as needed, without causing multiple subscriptions to the source sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Replay Overload](Replay/Qbservable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Replay\<TSource, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<IObservable\<TSource\>, IObservable\<TResult\>\>\>, IScheduler)

Returns a queryable observable sequence that is the result of invoking the selector on a connectable queryable observable sequence that shares a single subscription to the underlying sequence replaying all notifications.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult))), _
    scheduler As IScheduler _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult)))
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of TResult)

returnValue = source.Replay(selector, _
    scheduler)
```

```csharp
public static IQbservable<TResult> Replay<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IQbservable<TResult>^ Replay(
    IQbservable<TSource>^ source, 
    Expression<Func<IObservable<TSource>^, IObservable<TResult>^>^>^ selector, 
    IScheduler^ scheduler
)
```

```fsharp
static member Replay : 
        source:IQbservable<'TSource> * 
        selector:Expression<Func<IObservable<'TSource>, IObservable<'TResult>>> * 
        scheduler:IScheduler -> IQbservable<'TResult> 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence whose elements will be multicasting through a single shared subscription.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>\>  
  The selector function which can use the multicasted source sequence as many times as needed, without causing multiple subscriptions to the source sequence.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler where connected observers within the selector function will be invoked on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Replay Overload](Replay/Qbservable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Replay\<TSource, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<IObservable\<TSource\>, IObservable\<TResult\>\>\>, TimeSpan)

Returns a queryable observable sequence that is the result of invoking the selector on a connectable queryable observable sequence that shares a single subscription to the underlying sequence replaying all notifications within window.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult))), _
    window As TimeSpan _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult)))
Dim window As TimeSpan
Dim returnValue As IQbservable(Of TResult)

returnValue = source.Replay(selector, _
    window)
```

```csharp
public static IQbservable<TResult> Replay<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
    TimeSpan window
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IQbservable<TResult>^ Replay(
    IQbservable<TSource>^ source, 
    Expression<Func<IObservable<TSource>^, IObservable<TResult>^>^>^ selector, 
    TimeSpan window
)
```

```fsharp
static member Replay : 
        source:IQbservable<'TSource> * 
        selector:Expression<Func<IObservable<'TSource>, IObservable<'TResult>>> * 
        window:TimeSpan -> IQbservable<'TResult> 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence whose elements will be multicasting through a single shared subscription.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>\>  
  The selector function which can use the multicasted source sequence as many times as needed, without causing multiple subscriptions to the source sequence.

- window  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The maximum time length of the replay buffer.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Replay Overload](Replay/Qbservable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Replay\<TSource, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<IObservable\<TSource\>, IObservable\<TResult\>\>\>, TimeSpan, IScheduler)

Returns a queryable observable sequence that is the result of invoking the selector on a connectable queryable observable sequence that shares a single subscription to the underlying sequence replaying all notifications within window.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult))), _
    window As TimeSpan, _
    scheduler As IScheduler _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult)))
Dim window As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of TResult)

returnValue = source.Replay(selector, _
    window, scheduler)
```

```csharp
public static IQbservable<TResult> Replay<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
    TimeSpan window,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IQbservable<TResult>^ Replay(
    IQbservable<TSource>^ source, 
    Expression<Func<IObservable<TSource>^, IObservable<TResult>^>^>^ selector, 
    TimeSpan window, 
    IScheduler^ scheduler
)
```

```fsharp
static member Replay : 
        source:IQbservable<'TSource> * 
        selector:Expression<Func<IObservable<'TSource>, IObservable<'TResult>>> * 
        window:TimeSpan * 
        scheduler:IScheduler -> IQbservable<'TResult> 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence whose elements will be multicasting through a single shared subscription.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>\>  
  The selector function which can use the multicasted source sequence as many times as needed, without causing multiple subscriptions to the source sequence.

- window  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The maximum time length of the replay buffer.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler where connected observers within the selector function will be invoked on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Replay Overload](Replay/Qbservable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Replay\<TSource, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<IObservable\<TSource\>, IObservable\<TResult\>\>\>, Int32)

Returns a queryable observable sequence that is the result of invoking the selector on a connectable queryable observable sequence that shares a single subscription to the underlying sequence replaying bufferSize notifications.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult))), _
    bufferSize As Integer _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult)))
Dim bufferSize As Integer
Dim returnValue As IQbservable(Of TResult)

returnValue = source.Replay(selector, _
    bufferSize)
```

```csharp
public static IQbservable<TResult> Replay<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
    int bufferSize
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IQbservable<TResult>^ Replay(
    IQbservable<TSource>^ source, 
    Expression<Func<IObservable<TSource>^, IObservable<TResult>^>^>^ selector, 
    int bufferSize
)
```

```fsharp
static member Replay : 
        source:IQbservable<'TSource> * 
        selector:Expression<Func<IObservable<'TSource>, IObservable<'TResult>>> * 
        bufferSize:int -> IQbservable<'TResult> 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>\>  
  The selector function which can use the multicasted source sequence as many times as needed, without causing multiple subscriptions to the source sequence.

- bufferSize  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum element count of the replay buffer.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Replay Overload](Replay/Qbservable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Replay\<TSource, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<IObservable\<TSource\>, IObservable\<TResult\>\>\>, Int32, TimeSpan)

Returns a queryable observable sequence that is the result of invoking the selector on a connectable queryable observable sequence that shares a single subscription to the underlying sequence replaying bufferSize notifications within window.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult))), _
    bufferSize As Integer, _
    window As TimeSpan _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult)))
Dim bufferSize As Integer
Dim window As TimeSpan
Dim returnValue As IQbservable(Of TResult)

returnValue = source.Replay(selector, _
    bufferSize, window)
```

```csharp
public static IQbservable<TResult> Replay<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector,
    int bufferSize,
    TimeSpan window
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IQbservable<TResult>^ Replay(
    IQbservable<TSource>^ source, 
    Expression<Func<IObservable<TSource>^, IObservable<TResult>^>^>^ selector, 
    int bufferSize, 
    TimeSpan window
)
```

```fsharp
static member Replay : 
        source:IQbservable<'TSource> * 
        selector:Expression<Func<IObservable<'TSource>, IObservable<'TResult>>> * 
        bufferSize:int * 
        window:TimeSpan -> IQbservable<'TResult> 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence whose elements will be multicasting through a single shared subscription.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>\>  
  The selector function which can use the multicasted source sequence as many times as needed, without causing multiple subscriptions to the source sequence.

- bufferSize  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum element count of the replay buffer.

- window  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The maximum time length of the replay buffer.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Replay Overload](Replay/Qbservable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
