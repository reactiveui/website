title: Qbservable.ToAsync<TSource>(IQbservableProvider, Expression<Action<TSource>>)
---
# Qbservable.ToAsync\<TSource\> Method (IQbservableProvider, Expression\<Action\<TSource\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of TSource) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of TSource)) _
) As Func(Of TSource, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of TSource))
Dim returnValue As Func(Of TSource, IQbservable(Of Unit))

returnValue = provider.ToAsync(action)
```

```csharp
public static Func<TSource, IQbservable<Unit>> ToAsync<TSource>(
    this IQbservableProvider provider,
    Expression<Action<TSource>> action
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static Func<TSource, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<TSource>^>^ action
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'TSource>> -> Func<'TSource, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>\>  
  The action used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToAsync\<TSource\> Method (IQbservableProvider, Expression\<Action\<TSource\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of TSource) ( _
    provider As IQbservableProvider, _
    action As Expression(Of Action(Of TSource)), _
    scheduler As IScheduler _
) As Func(Of TSource, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim action As Expression(Of Action(Of TSource))
Dim scheduler As IScheduler
Dim returnValue As Func(Of TSource, IQbservable(Of Unit))

returnValue = provider.ToAsync(action, _
    scheduler)
```

```csharp
public static Func<TSource, IQbservable<Unit>> ToAsync<TSource>(
    this IQbservableProvider provider,
    Expression<Action<TSource>> action,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static Func<TSource, IQbservable<Unit>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Action<TSource>^>^ action, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        action:Expression<Action<'TSource>> * 
        scheduler:IScheduler -> Func<'TSource, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- action  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>\>  
  The action used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToAsync Overload](ToAsync\Qbservable.ToAsync.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
