title: TaskObservableExtensions.ToObservable<TResult>(Task<TResult>)
---
# TaskObservableExtensions.ToObservable\<TResult\> Method (Task\<TResult\>)

Returns an observable sequence that propagates the result of the task.

**Namespace:**  [System.Reactive.Threading.Tasks](System.Reactive.Threading.Tasks\System.Reactive.Threading.Tasks.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToObservable(Of TResult) ( _
    task As Task(Of TResult) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim task As Task(Of TResult)
Dim returnValue As IObservable(Of TResult)

returnValue = task.ToObservable()
```

```csharp
public static IObservable<TResult> ToObservable<TResult>(
    this Task<TResult> task
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static IObservable<TResult>^ ToObservable(
    Task<TResult>^ task
)
```

```fsharp
static member ToObservable : 
        task:Task<'TResult> -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- task  
  Type: [System.Threading.Tasks.Task](https://msdn.microsoft.com/en-us/library/Dd321424)\<TResult\>  
  The task to convert to an observable sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence that produces the task's result, or propagates the exception produced by the task.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [Task](https://msdn.microsoft.com/en-us/library/Dd321424)\<TResult\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[TaskObservableExtensions Class](TaskObservableExtensions\TaskObservableExtensions.md)

[ToObservable Overload](ToObservable\TaskObservableExtensions.ToObservable.md)

[System.Reactive.Threading.Tasks Namespace](System.Reactive.Threading.Tasks\System.Reactive.Threading.Tasks.md)
