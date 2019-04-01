# Qbservable.ToAsync\<TSource, TResult\> Method (IQbservableProvider, Expression\<Func\<TSource, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of TSource, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of TSource, TResult)) _
) As Func(Of TSource, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of TSource, TResult))
Dim returnValue As Func(Of TSource, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<TSource, IQbservable<TResult>> ToAsync<TSource, TResult>(
    this IQbservableProvider provider,
    Expression<Func<TSource, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static Func<TSource, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<TSource, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'TSource, 'TResult>> -> Func<'TSource, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<TSource, TResult\> Method (IQbservableProvider, Expression\<Func\<TSource, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of TSource, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of TSource, TResult)), _
    scheduler As IScheduler _
) As Func(Of TSource, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of TSource, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of TSource, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<TSource, IQbservable<TResult>> ToAsync<TSource, TResult>(
    this IQbservableProvider provider,
    Expression<Func<TSource, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static Func<TSource, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<TSource, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'TSource, 'TResult>> * 
        scheduler:IScheduler -> Func<'TSource, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
