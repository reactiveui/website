title: Qbservable.Buffer<TSource>(IQbservable<TSource>, TimeSpan, IScheduler)
---
# Qbservable.Buffer\<TSource\> Method (IQbservable\<TSource\>, TimeSpan, IScheduler)

Indicates each element of a queryable observable sequence into consecutive non-overlapping buffers which are produced based on timing information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IQbservable(Of TSource), _
    timeSpan As TimeSpan, _
    scheduler As IScheduler _
) As IQbservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim timeSpan As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of IList(Of TSource))

returnValue = source.Buffer(timeSpan, _
    scheduler)
```

```csharp
public static IQbservable<IList<TSource>> Buffer<TSource>(
    this IQbservable<TSource> source,
    TimeSpan timeSpan,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IList<TSource>^>^ Buffer(
    IQbservable<TSource>^ source, 
    TimeSpan timeSpan, 
    IScheduler^ scheduler
)
```

```fsharp
static member Buffer : 
        source:IQbservable<'TSource> * 
        timeSpan:TimeSpan * 
        scheduler:IScheduler -> IQbservable<IList<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence to produce buffers over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The length of each buffer.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to run buffering timers on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The queryable observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Buffer Overload](Buffer/Qbservable.Buffer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.Buffer\<TSource\> Method (IQbservable\<TSource\>, Int32)

Indicates each element of a queryable observable sequence into consecutive non-overlapping buffers which are produced based on element count information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IQbservable(Of TSource), _
    count As Integer _
) As IQbservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim count As Integer
Dim returnValue As IQbservable(Of IList(Of TSource))

returnValue = source.Buffer(count)
```

```csharp
public static IQbservable<IList<TSource>> Buffer<TSource>(
    this IQbservable<TSource> source,
    int count
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IList<TSource>^>^ Buffer(
    IQbservable<TSource>^ source, 
    int count
)
```

```fsharp
static member Buffer : 
        source:IQbservable<'TSource> * 
        count:int -> IQbservable<IList<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence to produce buffers over.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The length of each buffer.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
A queryable observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Buffer Overload](Buffer/Qbservable.Buffer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.Buffer\<TSource\> Method (IQbservable\<TSource\>, TimeSpan, Int32)

Indicates each element of a queryable observable sequence into a buffer that’s sent out when either it’s full or a given amount of time has elapsed.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IQbservable(Of TSource), _
    timeSpan As TimeSpan, _
    count As Integer _
) As IQbservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim timeSpan As TimeSpan
Dim count As Integer
Dim returnValue As IQbservable(Of IList(Of TSource))

returnValue = source.Buffer(timeSpan, _
    count)
```

```csharp
public static IQbservable<IList<TSource>> Buffer<TSource>(
    this IQbservable<TSource> source,
    TimeSpan timeSpan,
    int count
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IList<TSource>^>^ Buffer(
    IQbservable<TSource>^ source, 
    TimeSpan timeSpan, 
    int count
)
```

```fsharp
static member Buffer : 
        source:IQbservable<'TSource> * 
        timeSpan:TimeSpan * 
        count:int -> IQbservable<IList<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence to produce buffers over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The maximum time length of a window.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum element count of a window.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The queryable observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Buffer Overload](Buffer/Qbservable.Buffer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.Buffer\<TSource\> Method (IQbservable\<TSource\>, Int32, Int32)

Indicates each element of a queryable observable sequence into zero or more buffers which are produced based on element count information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IQbservable(Of TSource), _
    count As Integer, _
    skip As Integer _
) As IQbservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim count As Integer
Dim skip As Integer
Dim returnValue As IQbservable(Of IList(Of TSource))

returnValue = source.Buffer(count, _
    skip)
```

```csharp
public static IQbservable<IList<TSource>> Buffer<TSource>(
    this IQbservable<TSource> source,
    int count,
    int skip
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IList<TSource>^>^ Buffer(
    IQbservable<TSource>^ source, 
    int count, 
    int skip
)
```

```fsharp
static member Buffer : 
        source:IQbservable<'TSource> * 
        count:int * 
        skip:int -> IQbservable<IList<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence to produce buffers over.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The length of each buffer.

- skip  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The number of elements to skip between creation of consecutive buffers.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
A queryable observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Buffer Overload](Buffer/Qbservable.Buffer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.Buffer\<TSource\> Method (IQbservable\<TSource\>, TimeSpan, Int32, IScheduler)

Indicates each element of a queryable observable sequence into a buffer that’s sent out when either it’s full or a given amount of time has elapsed.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IQbservable(Of TSource), _
    timeSpan As TimeSpan, _
    count As Integer, _
    scheduler As IScheduler _
) As IQbservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim timeSpan As TimeSpan
Dim count As Integer
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of IList(Of TSource))

returnValue = source.Buffer(timeSpan, _
    count, scheduler)
```

```csharp
public static IQbservable<IList<TSource>> Buffer<TSource>(
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
static IQbservable<IList<TSource>^>^ Buffer(
    IQbservable<TSource>^ source, 
    TimeSpan timeSpan, 
    int count, 
    IScheduler^ scheduler
)
```

```fsharp
static member Buffer : 
        source:IQbservable<'TSource> * 
        timeSpan:TimeSpan * 
        count:int * 
        scheduler:IScheduler -> IQbservable<IList<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence to produce buffers over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The maximum time length of a buffer.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum element count of a buffer.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to run buffering timers on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The queryable observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Buffer Overload](Buffer/Qbservable.Buffer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.Buffer\<TSource\> Method (IQbservable\<TSource\>, TimeSpan, TimeSpan, IScheduler)

Indicates each element of a queryable observable sequence into zero or more buffers which are produced based on timing information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IQbservable(Of TSource), _
    timeSpan As TimeSpan, _
    timeShift As TimeSpan, _
    scheduler As IScheduler _
) As IQbservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim timeSpan As TimeSpan
Dim timeShift As TimeSpan
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of IList(Of TSource))

returnValue = source.Buffer(timeSpan, _
    timeShift, scheduler)
```

```csharp
public static IQbservable<IList<TSource>> Buffer<TSource>(
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
static IQbservable<IList<TSource>^>^ Buffer(
    IQbservable<TSource>^ source, 
    TimeSpan timeSpan, 
    TimeSpan timeShift, 
    IScheduler^ scheduler
)
```

```fsharp
static member Buffer : 
        source:IQbservable<'TSource> * 
        timeSpan:TimeSpan * 
        timeShift:TimeSpan * 
        scheduler:IScheduler -> IQbservable<IList<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence to produce buffers over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The length of each buffer.

- timeShift  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The interval between creation of consecutive buffers.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to run buffering timers on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The queryable observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Buffer Overload](Buffer/Qbservable.Buffer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.Buffer\<TSource\> Method (IQbservable\<TSource\>, TimeSpan, TimeSpan)

Indicates each element of a queryable observable sequence into zero or more buffers which are produced based on timing information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IQbservable(Of TSource), _
    timeSpan As TimeSpan, _
    timeShift As TimeSpan _
) As IQbservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim timeSpan As TimeSpan
Dim timeShift As TimeSpan
Dim returnValue As IQbservable(Of IList(Of TSource))

returnValue = source.Buffer(timeSpan, _
    timeShift)
```

```csharp
public static IQbservable<IList<TSource>> Buffer<TSource>(
    this IQbservable<TSource> source,
    TimeSpan timeSpan,
    TimeSpan timeShift
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IList<TSource>^>^ Buffer(
    IQbservable<TSource>^ source, 
    TimeSpan timeSpan, 
    TimeSpan timeShift
)
```

```fsharp
static member Buffer : 
        source:IQbservable<'TSource> * 
        timeSpan:TimeSpan * 
        timeShift:TimeSpan -> IQbservable<IList<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence to produce buffers over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The length of each buffer.

- timeShift  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The interval between creation of consecutive buffers.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The queryable observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Buffer Overload](Buffer/Qbservable.Buffer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.Buffer\<TSource\> Method (IQbservable\<TSource\>, TimeSpan)

Indicates each element of a queryable observable sequence into consecutive non-overlapping buffers which are produced based on timing information.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Buffer(Of TSource) ( _
    source As IQbservable(Of TSource), _
    timeSpan As TimeSpan _
) As IQbservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim timeSpan As TimeSpan
Dim returnValue As IQbservable(Of IList(Of TSource))

returnValue = source.Buffer(timeSpan)
```

```csharp
public static IQbservable<IList<TSource>> Buffer<TSource>(
    this IQbservable<TSource> source,
    TimeSpan timeSpan
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IList<TSource>^>^ Buffer(
    IQbservable<TSource>^ source, 
    TimeSpan timeSpan
)
```

```fsharp
static member Buffer : 
        source:IQbservable<'TSource> * 
        timeSpan:TimeSpan -> IQbservable<IList<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence to produce buffers over.

- timeSpan  
  Type: [System.TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)  
  The length of each buffer.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
The queryable observable sequence of buffers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Buffer Overload](Buffer/Qbservable.Buffer)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








