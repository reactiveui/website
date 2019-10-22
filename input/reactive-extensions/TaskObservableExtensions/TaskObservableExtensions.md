title: TaskObservableExtensions Class
---
# TaskObservableExtensions Class

Provides a set of static methods for converting Tasks to IObservables.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Threading.Tasks.TaskObservableExtensions

**Namespace:**  [System.Reactive.Threading.Tasks](System.Reactive.Threading.Tasks\System.Reactive.Threading.Tasks.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public NotInheritable Class TaskObservableExtensions
```

```vb
'Usage
```

```csharp
public static class TaskObservableExtensions
```

```c++
[ExtensionAttribute]
public ref class TaskObservableExtensions abstract sealed
```

```fsharp
[<AbstractClassAttribute>]
[<SealedAttribute>]
type TaskObservableExtensions =  class end
```

```jscript
public final class TaskObservableExtensions
```

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToObservable(Task)](https://msdn.microsoft.com/en-us/library/m:system.reactive.threading.tasks.taskobservableextensions.toobservable(system.threading.tasks.task)(v=VS.103))Returns an observable sequence that signals when the task completes.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToObservable<TResult>(Task<TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.threading.tasks.taskobservableextensions.toobservable%60%601(system.threading.tasks.task%7b%60%600%7d)(v=VS.103))Returns an observable sequence that propagates the result of the task.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToTask<TResult>(IObservable<TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.threading.tasks.taskobservableextensions.totask%60%601(system.iobservable%7b%60%600%7d)(v=VS.103))Returns a task that contains the last value of the observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToTask<TResult>(IObservable<TResult>, Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.threading.tasks.taskobservableextensions.totask%60%601(system.iobservable%7b%60%600%7d%2csystem.object)(v=VS.103))Returns a task that contains the last value of the observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToTask<TResult>(IObservable<TResult>, CancellationToken)](https://msdn.microsoft.com/en-us/library/m:system.reactive.threading.tasks.taskobservableextensions.totask%60%601(system.iobservable%7b%60%600%7d%2csystem.threading.cancellationtoken)(v=VS.103))Returns a task that contains the last value of the observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToTask<TResult>(IObservable<TResult>, CancellationToken, Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.threading.tasks.taskobservableextensions.totask%60%601(system.iobservable%7b%60%600%7d%2csystem.threading.cancellationtoken%2csystem.object)(v=VS.103))Returns a task that contains the last value of the observable sequence.Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Threading.Tasks Namespace](System.Reactive.Threading.Tasks\System.Reactive.Threading.Tasks.md)

# TaskObservableExtensions Methods

Include Protected Members  
Include Inherited Members

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToObservable(Task)](https://msdn.microsoft.com/en-us/library/m:system.reactive.threading.tasks.taskobservableextensions.toobservable(system.threading.tasks.task)(v=VS.103))Returns an observable sequence that signals when the task completes.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToObservable<TResult>(Task<TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.threading.tasks.taskobservableextensions.toobservable%60%601(system.threading.tasks.task%7b%60%600%7d)(v=VS.103))Returns an observable sequence that propagates the result of the task.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToTask<TResult>(IObservable<TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.threading.tasks.taskobservableextensions.totask%60%601(system.iobservable%7b%60%600%7d)(v=VS.103))Returns a task that contains the last value of the observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToTask<TResult>(IObservable<TResult>, Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.threading.tasks.taskobservableextensions.totask%60%601(system.iobservable%7b%60%600%7d%2csystem.object)(v=VS.103))Returns a task that contains the last value of the observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToTask<TResult>(IObservable<TResult>, CancellationToken)](https://msdn.microsoft.com/en-us/library/m:system.reactive.threading.tasks.taskobservableextensions.totask%60%601(system.iobservable%7b%60%600%7d%2csystem.threading.cancellationtoken)(v=VS.103))Returns a task that contains the last value of the observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToTask<TResult>(IObservable<TResult>, CancellationToken, Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.threading.tasks.taskobservableextensions.totask%60%601(system.iobservable%7b%60%600%7d%2csystem.threading.cancellationtoken%2csystem.object)(v=VS.103))Returns a task that contains the last value of the observable sequence.Top

## See Also

#### Reference

[TaskObservableExtensions Class](TaskObservableExtensions\TaskObservableExtensions.md)

[System.Reactive.Threading.Tasks Namespace](System.Reactive.Threading.Tasks\System.Reactive.Threading.Tasks.md)
