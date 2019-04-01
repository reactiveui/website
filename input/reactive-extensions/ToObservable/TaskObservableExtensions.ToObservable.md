# TaskObservableExtensions.ToObservable Method

Include Protected Members  
Include Inherited Members

Returns an observable sequence of the task.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToObservable(Task)](https://msdn.microsoft.com/en-us/library/m:system.reactive.threading.tasks.taskobservableextensions.toobservable(system.threading.tasks.task)(v=VS.103))Returns an observable sequence that signals when the task completes.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToObservable<TResult>(Task<TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.threading.tasks.taskobservableextensions.toobservable%60%601(system.threading.tasks.task%7b%60%600%7d)(v=VS.103))Returns an observable sequence that propagates the result of the task.Top

## See Also

#### Reference

[TaskObservableExtensions Class](TaskObservableExtensions\TaskObservableExtensions.md)

[System.Reactive.Threading.Tasks Namespace](System.Reactive.Threading.Tasks\System.Reactive.Threading.Tasks.md)

# TaskObservableExtensions.ToObservable Method (Task)

Returns an observable sequence that signals when the task completes.

**Namespace:**  [System.Reactive.Threading.Tasks](System.Reactive.Threading.Tasks\System.Reactive.Threading.Tasks.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToObservable ( _
    task As Task _
) As IObservable(Of Unit)
```

```vb
'Usage
Dim task As Task
Dim returnValue As IObservable(Of Unit)

returnValue = task.ToObservable()
```

```csharp
public static IObservable<Unit> ToObservable(
    this Task task
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<Unit>^ ToObservable(
    Task^ task
)
```

```fsharp
static member ToObservable : 
        task:Task -> IObservable<Unit> 
```

```jscript
public static function ToObservable(
    task : Task
) : IObservable<Unit>
```

#### Parameters

- task  
  Type: [System.Threading.Tasks.Task](https://msdn.microsoft.com/en-us/library/Dd235678)  
  The task to convert to an observable sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit\Unit.md)\>  
An observable sequence that produces a unit value when the task completes, or propagates the exception produced by the task.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [Task](https://msdn.microsoft.com/en-us/library/Dd235678). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[TaskObservableExtensions Class](TaskObservableExtensions\TaskObservableExtensions.md)

[ToObservable Overload](ToObservable\TaskObservableExtensions.ToObservable.md)

[System.Reactive.Threading.Tasks Namespace](System.Reactive.Threading.Tasks\System.Reactive.Threading.Tasks.md)
