title: Observable.Replay<TSource>(IObservable<TSource>, Int32, TimeSpan)
---
# Observable.Replay\<TSource\> Method (IObservable\<TSource\>, Int32, TimeSpan)

Returns a connectable observable sequence that shares a single subscription to the underlying sequence replaying bufferSize notifications within window.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource) ( _
    source As IObservable(Of TSource), _
    bufferSize As Integer, _
    window As TimeSpan _
) As IConnectableObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim bufferSize As Integer
Dim window As TimeSpan
Dim returnValue As IConnectableObservable(Of TSource)

returnValue = source.Replay(bufferSize, _
    window)
```

```csharp
public static IConnectableObservable<TSource> Replay<TSource>(
    this IObservable<TSource> source,
    int bufferSize,
    TimeSpan window
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IConnectableObservable<TSource>^ Replay(
    IObservable<TSource>^ source, 
    int bufferSize, 
    TimeSpan window
)
```

```fsharp
static member Replay : 
        source:IObservable<'TSource> * 
        bufferSize:int * 
        window:TimeSpan -> IConnectableObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

- bufferSize  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum element count of the replay buffer.

- window  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The maximum time length of the replay buffer.

#### Return Value

Type: [System.Reactive.Subjects.IConnectableObservable](IConnectableObservable/IConnectableObservable(T))\<TSource\>  
A connectable observable sequence that shares a single subscription to the underlying sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Replay Overload](Replay/Observable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Replay\<TSource\> Method (IObservable\<TSource\>, Int32, IScheduler)

Returns a connectable observable sequence that shares a single subscription to the underlying sequence replaying bufferSize notifications.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource) ( _
    source As IObservable(Of TSource), _
    bufferSize As Integer, _
    scheduler As IScheduler _
) As IConnectableObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim bufferSize As Integer
Dim scheduler As IScheduler
Dim returnValue As IConnectableObservable(Of TSource)

returnValue = source.Replay(bufferSize, _
    scheduler)
```

```csharp
public static IConnectableObservable<TSource> Replay<TSource>(
    this IObservable<TSource> source,
    int bufferSize,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IConnectableObservable<TSource>^ Replay(
    IObservable<TSource>^ source, 
    int bufferSize, 
    IScheduler^ scheduler
)
```

```fsharp
static member Replay : 
        source:IObservable<'TSource> * 
        bufferSize:int * 
        scheduler:IScheduler -> IConnectableObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

- bufferSize  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum element count of the replay buffer.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler where connected observers will be invoked on.

#### Return Value

Type: [System.Reactive.Subjects.IConnectableObservable](IConnectableObservable/IConnectableObservable(T))\<TSource\>  
A connectable observable sequence that shares a single subscription to the underlying sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Replay Overload](Replay/Observable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Replay\<TSource\> Method (IObservable\<TSource\>)

Returns a connectable observable sequence that shares a single subscription to the underlying sequence replaying all notifications.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IConnectableObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IConnectableObservable(Of TSource)

returnValue = source.Replay()
```

```csharp
public static IConnectableObservable<TSource> Replay<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IConnectableObservable<TSource>^ Replay(
    IObservable<TSource>^ source
)
```

```fsharp
static member Replay : 
        source:IObservable<'TSource> -> IConnectableObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

#### Return Value

Type: [System.Reactive.Subjects.IConnectableObservable](IConnectableObservable/IConnectableObservable(T))\<TSource\>  
A connectable observable sequence that shares a single subscription to the underlying sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Replay Overload](Replay/Observable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Replay\<TSource\> Method (IObservable\<TSource\>, TimeSpan)

Returns a connectable observable sequence that shares a single subscription to the underlying sequence replaying all notifications within window.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource) ( _
    source As IObservable(Of TSource), _
    window As TimeSpan _
) As IConnectableObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim window As TimeSpan
Dim returnValue As IConnectableObservable(Of TSource)

returnValue = source.Replay(window)
```

```csharp
public static IConnectableObservable<TSource> Replay<TSource>(
    this IObservable<TSource> source,
    TimeSpan window
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IConnectableObservable<TSource>^ Replay(
    IObservable<TSource>^ source, 
    TimeSpan window
)
```

```fsharp
static member Replay : 
        source:IObservable<'TSource> * 
        window:TimeSpan -> IConnectableObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

- window  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The maximum time length of the replay buffer.

#### Return Value

Type: [System.Reactive.Subjects.IConnectableObservable](IConnectableObservable/IConnectableObservable(T))\<TSource\>  
A connectable observable sequence that shares a single subscription to the underlying sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Replay Overload](Replay/Observable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Replay\<TSource\> Method (IObservable\<TSource\>, Int32)

Returns a connectable observable sequence that shares a single subscription to the underlying sequence replaying bufferSize notifications.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource) ( _
    source As IObservable(Of TSource), _
    bufferSize As Integer _
) As IConnectableObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim bufferSize As Integer
Dim returnValue As IConnectableObservable(Of TSource)

returnValue = source.Replay(bufferSize)
```

```csharp
public static IConnectableObservable<TSource> Replay<TSource>(
    this IObservable<TSource> source,
    int bufferSize
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IConnectableObservable<TSource>^ Replay(
    IObservable<TSource>^ source, 
    int bufferSize
)
```

```fsharp
static member Replay : 
        source:IObservable<'TSource> * 
        bufferSize:int -> IConnectableObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

- bufferSize  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum element count of the replay buffer.

#### Return Value

Type: [System.Reactive.Subjects.IConnectableObservable](IConnectableObservable/IConnectableObservable(T))\<TSource\>  
A connectable observable sequence that shares a single subscription to the underlying sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Replay Overload](Replay/Observable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Replay\<TSource\> Method (IObservable\<TSource\>, TimeSpan, IScheduler)

Returns a connectable observable sequence that shares a single subscription to the underlying sequence replaying all notifications within window.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource) ( _
    source As IObservable(Of TSource), _
    window As TimeSpan, _
    scheduler As IScheduler _
) As IConnectableObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim window As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IConnectableObservable(Of TSource)

returnValue = source.Replay(window, _
    scheduler)
```

```csharp
public static IConnectableObservable<TSource> Replay<TSource>(
    this IObservable<TSource> source,
    TimeSpan window,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IConnectableObservable<TSource>^ Replay(
    IObservable<TSource>^ source, 
    TimeSpan window, 
    IScheduler^ scheduler
)
```

```fsharp
static member Replay : 
        source:IObservable<'TSource> * 
        window:TimeSpan * 
        scheduler:IScheduler -> IConnectableObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

- window  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The maximum time length of the replay buffer.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler where connected observers will be invoked on.

#### Return Value

Type: [System.Reactive.Subjects.IConnectableObservable](IConnectableObservable/IConnectableObservable(T))\<TSource\>  
A connectable observable sequence that shares a single subscription to the underlying sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Replay Overload](Replay/Observable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Replay\<TSource\> Method (IObservable\<TSource\>, Int32, TimeSpan, IScheduler)

Returns a connectable observable sequence that shares a single subscription to the underlying sequence replaying bufferSize notifications within window.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource) ( _
    source As IObservable(Of TSource), _
    bufferSize As Integer, _
    window As TimeSpan, _
    scheduler As IScheduler _
) As IConnectableObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim bufferSize As Integer
Dim window As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IConnectableObservable(Of TSource)

returnValue = source.Replay(bufferSize, _
    window, scheduler)
```

```csharp
public static IConnectableObservable<TSource> Replay<TSource>(
    this IObservable<TSource> source,
    int bufferSize,
    TimeSpan window,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IConnectableObservable<TSource>^ Replay(
    IObservable<TSource>^ source, 
    int bufferSize, 
    TimeSpan window, 
    IScheduler^ scheduler
)
```

```fsharp
static member Replay : 
        source:IObservable<'TSource> * 
        bufferSize:int * 
        window:TimeSpan * 
        scheduler:IScheduler -> IConnectableObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

- bufferSize  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum element count of the replay buffer.

- window  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The maximum time length of the replay buffer.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler where connected observers will be invoked on.

#### Return Value

Type: [System.Reactive.Subjects.IConnectableObservable](IConnectableObservable/IConnectableObservable(T))\<TSource\>  
A connectable observable sequence that shares a single subscription to the underlying sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Replay Overload](Replay/Observable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Replay\<TSource\> Method (IObservable\<TSource\>, IScheduler)

Returns a connectable observable sequence that shares a single subscription to the underlying sequence replaying all notifications.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Replay(Of TSource) ( _
    source As IObservable(Of TSource), _
    scheduler As IScheduler _
) As IConnectableObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim scheduler As IScheduler
Dim returnValue As IConnectableObservable(Of TSource)

returnValue = source.Replay(scheduler)
```

```csharp
public static IConnectableObservable<TSource> Replay<TSource>(
    this IObservable<TSource> source,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IConnectableObservable<TSource>^ Replay(
    IObservable<TSource>^ source, 
    IScheduler^ scheduler
)
```

```fsharp
static member Replay : 
        source:IObservable<'TSource> * 
        scheduler:IScheduler -> IConnectableObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler where connected observers will be invoked on.

#### Return Value

Type: [System.Reactive.Subjects.IConnectableObservable](IConnectableObservable/IConnectableObservable(T))\<TSource\>  
A connectable observable sequence that shares a single subscription to the underlying sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Replay Overload](Replay/Observable.Replay)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
