# Observable.ToAsync\<T, TResult\> Method (Func\<T, TResult\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T, TResult) ( _
    function As Func(Of T, TResult) _
) As Func(Of T, IObservable(Of TResult))
```

```vb
'Usage
Dim function As Func(Of T, TResult)
Dim returnValue As Func(Of T, IObservable(Of TResult))

returnValue = function.ToAsync()
```

```csharp
public static Func<T, IObservable<TResult>> ToAsync<T, TResult>(
    this Func<T, TResult> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T, typename TResult>
static Func<T, IObservable<TResult>^>^ ToAsync(
    Func<T, TResult>^ function
)
```

```fsharp
static member ToAsync : 
        function:Func<'T, 'TResult> -> Func<'T, IObservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T  
  The type.

- TResult  
  The type of result.

#### Parameters

- function  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<T, TResult\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<T, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<T, TResult\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[ToAsync Overload](ToAsync\Observable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.ToAsync\<T, TResult\> Method (Func\<T, TResult\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T, TResult) ( _
    function As Func(Of T, TResult), _
    scheduler As IScheduler _
) As Func(Of T, IObservable(Of TResult))
```

```vb
'Usage
Dim function As Func(Of T, TResult)
Dim scheduler As IScheduler
Dim returnValue As Func(Of T, IObservable(Of TResult))

returnValue = function.ToAsync(scheduler)
```

```csharp
public static Func<T, IObservable<TResult>> ToAsync<T, TResult>(
    this Func<T, TResult> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T, typename TResult>
static Func<T, IObservable<TResult>^>^ ToAsync(
    Func<T, TResult>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        function:Func<'T, 'TResult> * 
        scheduler:IScheduler -> Func<'T, IObservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T  
  The type.

- TResult  
  The type of result.

#### Parameters

- function  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<T, TResult\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<T, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<T, TResult\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[ToAsync Overload](ToAsync\Observable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
