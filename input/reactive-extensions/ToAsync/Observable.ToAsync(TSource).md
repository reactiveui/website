# Observable.ToAsync\<TSource\> Method (Action\<TSource\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of TSource) ( _
    action As Action(Of TSource), _
    scheduler As IScheduler _
) As Func(Of TSource, IObservable(Of Unit))
```

```vb
'Usage
Dim action As Action(Of TSource)
Dim scheduler As IScheduler
Dim returnValue As Func(Of TSource, IObservable(Of Unit))

returnValue = action.ToAsync(scheduler)
```

```csharp
public static Func<TSource, IObservable<Unit>> ToAsync<TSource>(
    this Action<TSource> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static Func<TSource, IObservable<Unit>^>^ ToAsync(
    Action<TSource>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        action:Action<'TSource> * 
        scheduler:IScheduler -> Func<'TSource, IObservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[ToAsync Overload](ToAsync\Observable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.ToAsync\<TSource\> Method (Action\<TSource\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of TSource) ( _
    action As Action(Of TSource) _
) As Func(Of TSource, IObservable(Of Unit))
```

```vb
'Usage
Dim action As Action(Of TSource)
Dim returnValue As Func(Of TSource, IObservable(Of Unit))

returnValue = action.ToAsync()
```

```csharp
public static Func<TSource, IObservable<Unit>> ToAsync<TSource>(
    this Action<TSource> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static Func<TSource, IObservable<Unit>^>^ ToAsync(
    Action<TSource>^ action
)
```

```fsharp
static member ToAsync : 
        action:Action<'TSource> -> Func<'TSource, IObservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[ToAsync Overload](ToAsync\Observable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
