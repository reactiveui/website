# Qbservable.Window\<TSource\> Method (IQbservable\<TSource\>, Int32)

Projects each element of a queryable observable sequence into consecutive non-overlapping windows which are produced based on element count information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Window(Of TSource) ( _
    source As IQbservable(Of TSource), _
    count As Integer _
) As IQbservable(Of IObservable(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim count As Integer
Dim returnValue As IQbservable(Of IObservable(Of TSource))

returnValue = source.Window(count)
```

```csharp
public static IQbservable<IObservable<TSource>> Window<TSource>(
    this IQbservable<TSource> source,
    int count
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IObservable<TSource>^>^ Window(
    IQbservable<TSource>^ source, 
    int count
)
```

```fsharp
static member Window : 
        source:IQbservable<'TSource> * 
        count:int -> IQbservable<IObservable<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to produce windows over.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The length of each window.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
A queryable observable sequence of windows.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Window Overload](Window\Qbservable.Window.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Window\<TSource\> Method (IQbservable\<TSource\>, TimeSpan, Int32, IScheduler)

Projects each element of a queryable observable sequence into a window that is completed when either it’s full or a given amount of time has elapsed.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Window(Of TSource) ( _
    source As IQbservable(Of TSource), _
    timeSpan As TimeSpan, _
    count As Integer, _
    scheduler As IScheduler _
) As IQbservable(Of IObservable(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim timeSpan As TimeSpan
Dim count As Integer
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of IObservable(Of TSource))

returnValue = source.Window(timeSpan, _
    count, scheduler)
```

```csharp
public static IQbservable<IObservable<TSource>> Window<TSource>(
    this IQbservable<TSource> source,
    TimeSpan timeSpan,
    int count,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IObservable<TSource>^>^ Window(
    IQbservable<TSource>^ source, 
    TimeSpan timeSpan, 
    int count, 
    IScheduler^ scheduler
)
```

```fsharp
static member Window : 
        source:IQbservable<'TSource> * 
        timeSpan:TimeSpan * 
        count:int * 
        scheduler:IScheduler -> IQbservable<IObservable<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to produce windows over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The maximum time length of a window.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum element count of a window.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run windowing timers on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
A queryable observable sequence of windows.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Window Overload](Window\Qbservable.Window.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Window\<TSource\> Method (IQbservable\<TSource\>, TimeSpan, TimeSpan, IScheduler)

Projects each element of a queryable observable sequence into zero or more windows which are produced based on timing information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Window(Of TSource) ( _
    source As IQbservable(Of TSource), _
    timeSpan As TimeSpan, _
    timeShift As TimeSpan, _
    scheduler As IScheduler _
) As IQbservable(Of IObservable(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim timeSpan As TimeSpan
Dim timeShift As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of IObservable(Of TSource))

returnValue = source.Window(timeSpan, _
    timeShift, scheduler)
```

```csharp
public static IQbservable<IObservable<TSource>> Window<TSource>(
    this IQbservable<TSource> source,
    TimeSpan timeSpan,
    TimeSpan timeShift,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IObservable<TSource>^>^ Window(
    IQbservable<TSource>^ source, 
    TimeSpan timeSpan, 
    TimeSpan timeShift, 
    IScheduler^ scheduler
)
```

```fsharp
static member Window : 
        source:IQbservable<'TSource> * 
        timeSpan:TimeSpan * 
        timeShift:TimeSpan * 
        scheduler:IScheduler -> IQbservable<IObservable<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to produce windows over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The length of each window.

- timeShift  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The interval between creation of consecutive windows.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run windowing timers on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
A queryable observable sequence of windows.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Window Overload](Window\Qbservable.Window.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Window\<TSource\> Method (IQbservable\<TSource\>, TimeSpan, TimeSpan)

Projects each element of a queryable observable sequence into zero or more windows which are produced based on timing information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Window(Of TSource) ( _
    source As IQbservable(Of TSource), _
    timeSpan As TimeSpan, _
    timeShift As TimeSpan _
) As IQbservable(Of IObservable(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim timeSpan As TimeSpan
Dim timeShift As TimeSpan
Dim returnValue As IQbservable(Of IObservable(Of TSource))

returnValue = source.Window(timeSpan, _
    timeShift)
```

```csharp
public static IQbservable<IObservable<TSource>> Window<TSource>(
    this IQbservable<TSource> source,
    TimeSpan timeSpan,
    TimeSpan timeShift
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IObservable<TSource>^>^ Window(
    IQbservable<TSource>^ source, 
    TimeSpan timeSpan, 
    TimeSpan timeShift
)
```

```fsharp
static member Window : 
        source:IQbservable<'TSource> * 
        timeSpan:TimeSpan * 
        timeShift:TimeSpan -> IQbservable<IObservable<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to produce windows over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The length of each window.

- timeShift  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The interval between creation of consecutive windows.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
A queryable observable sequence of windows.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Window Overload](Window\Qbservable.Window.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Window\<TSource\> Method (IQbservable\<TSource\>, TimeSpan, IScheduler)

Projects each element of a queryable observable sequence into consecutive non-overlapping windows which are produced based on timing information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Window(Of TSource) ( _
    source As IQbservable(Of TSource), _
    timeSpan As TimeSpan, _
    scheduler As IScheduler _
) As IQbservable(Of IObservable(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim timeSpan As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of IObservable(Of TSource))

returnValue = source.Window(timeSpan, _
    scheduler)
```

```csharp
public static IQbservable<IObservable<TSource>> Window<TSource>(
    this IQbservable<TSource> source,
    TimeSpan timeSpan,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IObservable<TSource>^>^ Window(
    IQbservable<TSource>^ source, 
    TimeSpan timeSpan, 
    IScheduler^ scheduler
)
```

```fsharp
static member Window : 
        source:IQbservable<'TSource> * 
        timeSpan:TimeSpan * 
        scheduler:IScheduler -> IQbservable<IObservable<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to produce windows over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The length of each window.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run windowing timers on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
A queryable observable sequence of windows.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Window Overload](Window\Qbservable.Window.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Window\<TSource\> Method (IQbservable\<TSource\>, TimeSpan)

Projects each element of a queryable observable sequence into consecutive non-overlapping windows which are produced based on timing information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Window(Of TSource) ( _
    source As IQbservable(Of TSource), _
    timeSpan As TimeSpan _
) As IQbservable(Of IObservable(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim timeSpan As TimeSpan
Dim returnValue As IQbservable(Of IObservable(Of TSource))

returnValue = source.Window(timeSpan)
```

```csharp
public static IQbservable<IObservable<TSource>> Window<TSource>(
    this IQbservable<TSource> source,
    TimeSpan timeSpan
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IObservable<TSource>^>^ Window(
    IQbservable<TSource>^ source, 
    TimeSpan timeSpan
)
```

```fsharp
static member Window : 
        source:IQbservable<'TSource> * 
        timeSpan:TimeSpan -> IQbservable<IObservable<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to produce windows over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The length of each window.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
The sequence of windows.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Window Overload](Window\Qbservable.Window.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Window\<TSource\> Method (IQbservable\<TSource\>, Int32, Int32)

Projects each element of a queryable observable sequence into zero or more windows which are produced based on element count information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Window(Of TSource) ( _
    source As IQbservable(Of TSource), _
    count As Integer, _
    skip As Integer _
) As IQbservable(Of IObservable(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim count As Integer
Dim skip As Integer
Dim returnValue As IQbservable(Of IObservable(Of TSource))

returnValue = source.Window(count, _
    skip)
```

```csharp
public static IQbservable<IObservable<TSource>> Window<TSource>(
    this IQbservable<TSource> source,
    int count,
    int skip
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IObservable<TSource>^>^ Window(
    IQbservable<TSource>^ source, 
    int count, 
    int skip
)
```

```fsharp
static member Window : 
        source:IQbservable<'TSource> * 
        count:int * 
        skip:int -> IQbservable<IObservable<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to produce windows over.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The length of each window.

- skip  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The number of elements to skip between creations of consecutive windows.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
A queryable observable sequence of windows.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Window Overload](Window\Qbservable.Window.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Window\<TSource\> Method (IQbservable\<TSource\>, TimeSpan, Int32)

Projects each element of a queryable observable sequence into a window that is completed when either it’s full or a given amount of time has elapsed.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Window(Of TSource) ( _
    source As IQbservable(Of TSource), _
    timeSpan As TimeSpan, _
    count As Integer _
) As IQbservable(Of IObservable(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim timeSpan As TimeSpan
Dim count As Integer
Dim returnValue As IQbservable(Of IObservable(Of TSource))

returnValue = source.Window(timeSpan, _
    count)
```

```csharp
public static IQbservable<IObservable<TSource>> Window<TSource>(
    this IQbservable<TSource> source,
    TimeSpan timeSpan,
    int count
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IObservable<TSource>^>^ Window(
    IQbservable<TSource>^ source, 
    TimeSpan timeSpan, 
    int count
)
```

```fsharp
static member Window : 
        source:IQbservable<'TSource> * 
        timeSpan:TimeSpan * 
        count:int -> IQbservable<IObservable<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to produce windows over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The maximum time length of a window.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum element count of a window.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
A queryable observable sequence of windows.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Window Overload](Window\Qbservable.Window.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
