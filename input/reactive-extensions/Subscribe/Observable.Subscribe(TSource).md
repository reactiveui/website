title: Observable.Subscribe<TSource>(IEnumerable<TSource>, IObserver<TSource>)
---
# Observable.Subscribe\<TSource\> Method (IEnumerable\<TSource\>, IObserver\<TSource\>)

Subscribes an observer to an enumerable sequence with the specified source and observer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Subscribe(Of TSource) ( _
    source As IEnumerable(Of TSource), _
    observer As IObserver(Of TSource) _
) As IDisposable
```

```vb
'Usage
Dim source As IEnumerable(Of TSource)
Dim observer As IObserver(Of TSource)
Dim returnValue As IDisposable

returnValue = source.Subscribe(observer)
```

```csharp
public static IDisposable Subscribe<TSource>(
    this IEnumerable<TSource> source,
    IObserver<TSource> observer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IDisposable^ Subscribe(
    IEnumerable<TSource>^ source, 
    IObserver<TSource>^ observer
)
```

```fsharp
static member Subscribe : 
        source:IEnumerable<'TSource> * 
        observer:IObserver<'TSource> -> IDisposable 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TSource\>  
  The enumerable sequence to subscribe to.

- observer  
  Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<TSource\>  
  The observer that will receive notifications from the enumerable sequence.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
The disposable object that can be used to unsubscribe the observer from the enumerable.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Subscribe Overload](Subscribe/Observable.Subscribe)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Subscribe\<TSource\> Method (IEnumerable\<TSource\>, IObserver\<TSource\>, IScheduler)

Subscribes an observer to an enumerable sequence with the specified source and observer.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Subscribe(Of TSource) ( _
    source As IEnumerable(Of TSource), _
    observer As IObserver(Of TSource), _
    scheduler As IScheduler _
) As IDisposable
```

```vb
'Usage
Dim source As IEnumerable(Of TSource)
Dim observer As IObserver(Of TSource)
Dim scheduler As IScheduler
Dim returnValue As IDisposable

returnValue = source.Subscribe(observer, _
    scheduler)
```

```csharp
public static IDisposable Subscribe<TSource>(
    this IEnumerable<TSource> source,
    IObserver<TSource> observer,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IDisposable^ Subscribe(
    IEnumerable<TSource>^ source, 
    IObserver<TSource>^ observer, 
    IScheduler^ scheduler
)
```

```fsharp
static member Subscribe : 
        source:IEnumerable<'TSource> * 
        observer:IObserver<'TSource> * 
        scheduler:IScheduler -> IDisposable 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TSource\>  
  The enumerable sequence to subscribe to.

- observer  
  Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<TSource\>  
  The observer that will receive notifications from the enumerable sequence.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to perform the enumeration on.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
The disposable object that can be used to unsubscribe the observer from the enumerable.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Subscribe Overload](Subscribe/Observable.Subscribe)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
