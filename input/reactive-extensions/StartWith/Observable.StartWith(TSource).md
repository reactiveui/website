title: Observable.StartWith<TSource>(IObservable<TSource>, IScheduler, array<TSource[])
---
# Observable.StartWith\<TSource\> Method (IObservable\<TSource\>, IScheduler, array\<TSource\[\])

Prepends a sequence of values to an observable sequence with the specified source, scheduler and values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function StartWith(Of TSource) ( _
    source As IObservable(Of TSource), _
    scheduler As IScheduler, _
    ParamArray values As TSource() _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim scheduler As IScheduler
Dim values As TSource()
Dim returnValue As IObservable(Of TSource)

returnValue = source.StartWith(scheduler, _
    values)
```

```csharp
public static IObservable<TSource> StartWith<TSource>(
    this IObservable<TSource> source,
    IScheduler scheduler,
    params TSource[] values
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ StartWith(
    IObservable<TSource>^ source, 
    IScheduler^ scheduler, 
    ... array<TSource>^ values
)
```

```fsharp
static member StartWith : 
        source:IObservable<'TSource> * 
        scheduler:IScheduler * 
        values:'TSource[] -> IObservable<'TSource> 
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
  The source sequence to prepend values to.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to emit the prepended values on.

- values  
  Type: array\<TSource\[\]  
  The values to prepend to the specified sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The source sequence prepended with the specified values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[StartWith Overload](StartWith/Observable.StartWith)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.StartWith\<TSource\> Method (IObservable\<TSource\>, array\<TSource\[\])

Prepends a sequence of values to an observable sequence with the specified source and values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function StartWith(Of TSource) ( _
    source As IObservable(Of TSource), _
    ParamArray values As TSource() _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim values As TSource()
Dim returnValue As IObservable(Of TSource)

returnValue = source.StartWith(values)
```

```csharp
public static IObservable<TSource> StartWith<TSource>(
    this IObservable<TSource> source,
    params TSource[] values
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ StartWith(
    IObservable<TSource>^ source, 
    ... array<TSource>^ values
)
```

```fsharp
static member StartWith : 
        source:IObservable<'TSource> * 
        values:'TSource[] -> IObservable<'TSource> 
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
  The source sequence to prepend values to.

- values  
  Type: array\<TSource\[\]  
  The values to prepend to the specified sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The source sequence prepended with the specified values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[StartWith Overload](StartWith/Observable.StartWith)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
