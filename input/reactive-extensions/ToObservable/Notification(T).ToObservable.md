title: Notification<T>.ToObservable()
---
# Notification\<T\>.ToObservable Method

Returns an observable sequence with a single notification, using the immediate scheduler.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function ToObservable As IObservable(Of T)
```

```vb
'Usage
Dim instance As Notification
Dim returnValue As IObservable(Of T)

returnValue = instance.ToObservable()
```

```csharp
public IObservable<T> ToObservable()
```

```c++
public:
IObservable<T>^ ToObservable()
```

```fsharp
member ToObservable : unit -> IObservable<'T> 
```

```jscript
public function ToObservable() : IObservable<T>
```

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[T](Notification\Notification(T).md)\>  
The observable sequence that surfaces the behavior of the notification upon subscription.

## See Also

#### Reference

[Notification\<T\> Class](Notification\Notification(T).md)

[ToObservable Overload](ToObservable\Notification(T).ToObservable.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)

# Notification\<T\>.ToObservable Method (IScheduler)

Returns an observable sequence with a single notification.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function ToObservable ( _
    scheduler As IScheduler _
) As IObservable(Of T)
```

```vb
'Usage
Dim instance As Notification
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of T)

returnValue = instance.ToObservable(scheduler)
```

```csharp
public IObservable<T> ToObservable(
    IScheduler scheduler
)
```

```c++
public:
IObservable<T>^ ToObservable(
    IScheduler^ scheduler
)
```

```fsharp
member ToObservable : 
        scheduler:IScheduler -> IObservable<'T> 
```

```jscript
public function ToObservable(
    scheduler : IScheduler
) : IObservable<T>
```

#### Parameters

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to send out the notification calls on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[T](Notification\Notification(T).md)\>  
The observable sequence that surfaces the behavior of the notification upon subscription.

## See Also

#### Reference

[Notification\<T\> Class](Notification\Notification(T).md)

[ToObservable Overload](ToObservable\Notification(T).ToObservable.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)

# Notification\<T\>.ToObservable Method

Include Protected Members  
Include Inherited Members

Returns an observable sequence with a single notification.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToObservable()](ToObservable\Notification(T).ToObservable.md)Returns an observable sequence with a single notification, using the immediate scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToObservable(IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.toobservable(system.reactive.concurrency.ischeduler)(v=VS.103))Returns an observable sequence with a single notification.Top

## See Also

#### Reference

[Notification\<T\> Class](Notification\Notification(T).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)
