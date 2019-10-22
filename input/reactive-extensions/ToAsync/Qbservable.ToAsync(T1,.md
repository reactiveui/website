title: Qbservable.ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(IQbservableProvider, Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>>, IScheduler)
---
# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14, typename T15, typename T16, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

- T15  
  The fifteenth type of function.

- T16  
  The sixteenth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402862)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402862)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5)) _
) As Func(Of T1, T2, T3, T4, T5, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, T3, T4, T5, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5>
static Func<T1, T2, T3, T4, T5, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd289012)\<T1, T2, T3, T4, T5\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<T1, T2, T3, T4, T5, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<T1, T2, T3, T4, T5, T6, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<T1, T2, T3, T4, T5, T6, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14, typename T15>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

- T15  
  The fifteenth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd402873)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402868)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14, typename T15, typename T16>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

- T15  
  The fifteenth type of function.

- T16  
  The sixteenth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd402872)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402862)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6)) _
) As Func(Of T1, T2, T3, T4, T5, T6, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6>
static Func<T1, T2, T3, T4, T5, T6, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd269635)\<T1, T2, T3, T4, T5, T6\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<T1, T2, T3, T4, T5, T6, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<T1, T2, T3, T4, T5, T6, T7, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<T1, T2, T3, T4, T5, T6, T7, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd235351)\<T1, T2, T3, T4, T5, T6, T7, T8\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<T1, T2, T3, T4, T5, T6, T7, T8, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd402870)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7>
static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd268304)\<T1, T2, T3, T4, T5, T6, T7\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<T1, T2, T3, T4, T5, T6, T7, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14, typename T15, typename T16>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

- T15  
  The fifteenth type of function.

- T16  
  The sixteenth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd402872)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402862)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7>
static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd268304)\<T1, T2, T3, T4, T5, T6, T7\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<T1, T2, T3, T4, T5, T6, T7, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<T1, T2, T3, T4, T5, T6, T7, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<T1, T2, T3, T4, T5, T6, T7, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd402866)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2\> Method (IQbservableProvider, Expression\<Action\<T1, T2\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2)) _
) As Func(Of T1, T2, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2))
Dim returnValue As Func(Of T1, T2, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, IQbservable<Unit>> ToAsync<T1, T2>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2>
static Func<T1, T2, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2>> -> Func<'T1, 'T2, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Bb549311)\<T1, T2\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<T1, T2, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, T3, T4, T5, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename TResult>
static Func<T1, T2, T3, T4, T5, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<T1, T2, T3, T4, T5, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<T1, T2, T3, T4, T5, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, TResult)) _
) As Func(Of T1, T2, T3, T4, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, T3, T4, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename TResult>
static Func<T1, T2, T3, T4, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<T1, T2, T3, T4, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<T1, T2, T3, T4, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2\> Method (IQbservableProvider, Expression\<Action\<T1, T2\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, IQbservable<Unit>> ToAsync<T1, T2>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2>
static Func<T1, T2, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Bb549311)\<T1, T2\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<T1, T2, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14, typename T15, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

- T15  
  The fifteenth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402868)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402868)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd386922)\<T1, T2, T3, T4, T5, T6, T7, T8, T9\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename TResult>
static Func<T1, T2, T3, T4, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<T1, T2, T3, T4, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<T1, T2, T3, T4, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4)) _
) As Func(Of T1, T2, T3, T4, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4))
Dim returnValue As Func(Of T1, T2, T3, T4, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, T3, T4, IQbservable<Unit>> ToAsync<T1, T2, T3, T4>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4>
static Func<T1, T2, T3, T4, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4>> -> Func<'T1, 'T2, 'T3, 'T4, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Bb548654)\<T1, T2, T3, T4\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<T1, T2, T3, T4, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd235351)\<T1, T2, T3, T4, T5, T6, T7, T8\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<T1, T2, T3, T4, T5, T6, T7, T8, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename TResult>
static Func<T1, T2, T3, T4, T5, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- TResult  
  The type of result

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<T1, T2, T3, T4, T5, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<T1, T2, T3, T4, T5, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3)) _
) As Func(Of T1, T2, T3, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3))
Dim returnValue As Func(Of T1, T2, T3, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, T3, IQbservable<Unit>> ToAsync<T1, T2, T3>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3>
static Func<T1, T2, T3, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3>> -> Func<'T1, 'T2, 'T3, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Bb549392)\<T1, T2, T3\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<T1, T2, T3, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5>
static Func<T1, T2, T3, T4, T5, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd289012)\<T1, T2, T3, T4, T5\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<T1, T2, T3, T4, T5, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<T1, T2, T3, T4, T5, T6, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<T1, T2, T3, T4, T5, T6, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<T1, T2, T3, T4, T5, T6, T7, T8, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<T1, T2, T3, T4, T5, T6, T7, T8, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14, typename T15, typename T16, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'T16, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

- T15  
  The fifteenth type of function.

- T16  
  The sixteenth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402862)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402862)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd402866)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)


# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6>
static Func<T1, T2, T3, T4, T5, T6, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd269635)\<T1, T2, T3, T4, T5, T6\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<T1, T2, T3, T4, T5, T6, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd402871)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, TResult)) _
) As Func(Of T1, T2, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, TResult))
Dim returnValue As Func(Of T1, T2, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, IQbservable<TResult>> ToAsync<T1, T2, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename TResult>
static Func<T1, T2, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'TResult>> -> Func<'T1, 'T2, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<T1, T2, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<T1, T2, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd402870)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, IQbservable<TResult>> ToAsync<T1, T2, T3, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename TResult>
static Func<T1, T2, T3, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<T1, T2, T3, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<T1, T2, T3, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, TResult)) _
) As Func(Of T1, T2, T3, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, TResult))
Dim returnValue As Func(Of T1, T2, T3, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, T3, IQbservable<TResult>> ToAsync<T1, T2, T3, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename TResult>
static Func<T1, T2, T3, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'TResult>> -> Func<'T1, 'T2, 'T3, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<T1, T2, T3, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<T1, T2, T3, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd386922)\<T1, T2, T3, T4, T5, T6, T7, T8, T9\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14, typename T15, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

- T15  
  The fifteenth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402868)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402868)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, IQbservable<TResult>> ToAsync<T1, T2, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename TResult>
static Func<T1, T2, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<T1, T2, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<T1, T2, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd402871)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd402748)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd387291)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd387291)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd402748)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, IQbservable<Unit>> ToAsync<T1, T2, T3, T4>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4>
static Func<T1, T2, T3, T4, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Bb548654)\<T1, T2, T3, T4\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<T1, T2, T3, T4, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, IQbservable<Unit>> ToAsync<T1, T2, T3>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3>
static Func<T1, T2, T3, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Bb549392)\<T1, T2, T3\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<T1, T2, T3, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<T1, T2, T3, T4, T5, T6, T7, T8, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<T1, T2, T3, T4, T5, T6, T7, T8, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult)), _
    scheduler As IScheduler _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<TResult>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'TResult>> * 
        scheduler:IScheduler -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15\> Method (IQbservableProvider, Expression\<Action\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable<Unit>> ToAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
    this IQbservableProvider provider,
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14, typename T15>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, 'T15, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

- T15  
  The fifteenth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Dd402873)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402868)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
