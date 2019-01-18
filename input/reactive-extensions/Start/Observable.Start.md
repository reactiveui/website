# Observable.Start Method

Include Protected Members  
Include Inherited Members

Invokes the function asynchronously.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Start(Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.start(system.action)(v=VS.103))Invokes the action asynchronously.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Start<TSource>(Func<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.start%60%601(system.func%7b%60%600%7d)(v=VS.103))Invokes the function asynchronously.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Start(Action, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.start(system.action%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Invokes the action asynchronously.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Start<TSource>(Func<TSource>, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.start%60%601(system.func%7b%60%600%7d%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Invokes the function asynchronously.Top

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Start Method (Action)

Invokes the action asynchronously.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Start ( _
    action As Action _
) As IObservable(Of Unit)
```

```vb
'Usage
Dim action As Action
Dim returnValue As IObservable(Of Unit)

returnValue = Observable.Start(action)
```

```csharp
public static IObservable<Unit> Start(
    Action action
)
```

```c++
public:
static IObservable<Unit>^ Start(
    Action^ action
)
```

```fsharp
static member Start : 
        action:Action -> IObservable<Unit> 
```

```jscript
public static function Start(
    action : Action
) : IObservable<Unit>
```

#### Parameters

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb534741)  
  The action used to synchronization.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit\Unit.md)\>  
The action asynchronously.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Start Overload](Start\Observable.Start.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Start Method (Action, IScheduler)

Invokes the action asynchronously.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Start ( _
    action As Action, _
    scheduler As IScheduler _
) As IObservable(Of Unit)
```

```vb
'Usage
Dim action As Action
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of Unit)

returnValue = Observable.Start(action, _
    scheduler)
```

```csharp
public static IObservable<Unit> Start(
    Action action,
    IScheduler scheduler
)
```

```c++
public:
static IObservable<Unit>^ Start(
    Action^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member Start : 
        action:Action * 
        scheduler:IScheduler -> IObservable<Unit> 
```

```jscript
public static function Start(
    action : Action, 
    scheduler : IScheduler
) : IObservable<Unit>
```

#### Parameters

- action  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb534741)  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit\Unit.md)\>  
The action asynchronously.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Start Overload](Start\Observable.Start.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
