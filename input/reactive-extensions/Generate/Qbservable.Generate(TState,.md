title: Qbservable.Generate<TState, TResult>(IQbservableProvider, TState, Expression<Func<TState, Boolean>>, Expression<Func<TState, TState>>, Expression<Func<TState, TResult>>, IScheduler)
---
# Qbservable.Generate\<TState, TResult\> Method (IQbservableProvider, TState, Expression\<Func\<TState, Boolean\>\>, Expression\<Func\<TState, TState\>\>, Expression\<Func\<TState, TResult\>\>, IScheduler)

Generates a queryable observable sequence by iterating a state from an initial state until the condition fails.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Generate(Of TState, TResult) ( _
    provider As IQbservableProvider, _
    initialState As TState, _
    condition As Expression(Of Func(Of TState, Boolean)), _
    iterate As Expression(Of Func(Of TState, TState)), _
    resultSelector As Expression(Of Func(Of TState, TResult)), _
    scheduler As IScheduler _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim initialState As TState
Dim condition As Expression(Of Func(Of TState, Boolean))
Dim iterate As Expression(Of Func(Of TState, TState))
Dim resultSelector As Expression(Of Func(Of TState, TResult))
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.Generate(initialState, _
    condition, iterate, resultSelector, _
    scheduler)
```

```csharp
public static IQbservable<TResult> Generate<TState, TResult>(
    this IQbservableProvider provider,
    TState initialState,
    Expression<Func<TState, bool>> condition,
    Expression<Func<TState, TState>> iterate,
    Expression<Func<TState, TResult>> resultSelector,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TState, typename TResult>
static IQbservable<TResult>^ Generate(
    IQbservableProvider^ provider, 
    TState initialState, 
    Expression<Func<TState, bool>^>^ condition, 
    Expression<Func<TState, TState>^>^ iterate, 
    Expression<Func<TState, TResult>^>^ resultSelector, 
    IScheduler^ scheduler
)
```

```fsharp
static member Generate : 
        provider:IQbservableProvider * 
        initialState:'TState * 
        condition:Expression<Func<'TState, bool>> * 
        iterate:Expression<Func<'TState, 'TState>> * 
        resultSelector:Expression<Func<'TState, 'TResult>> * 
        scheduler:IScheduler -> IQbservable<'TResult> 
```


JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The type of state.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- initialState  
  Type: TState  
  The initial state.

- condition  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>\>  
  The condition to terminate generation.

- iterate  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TState\>\>  
  The iteration step function.

- resultSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TResult\>\>  
  The selector function for results produced in the sequence.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler on which to run the generator loop.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
The generated sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Generate Overload](Generate/Qbservable.Generate)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.Generate\<TState, TResult\> Method (IQbservableProvider, TState, Expression\<Func\<TState, Boolean\>\>, Expression\<Func\<TState, TState\>\>, Expression\<Func\<TState, TResult\>\>, Expression\<Func\<TState, TimeSpan\>\>, IScheduler)

Generates a queryable observable sequence by iterating a state from an initial state until the condition fails.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Generate(Of TState, TResult) ( _
    provider As IQbservableProvider, _
    initialState As TState, _
    condition As Expression(Of Func(Of TState, Boolean)), _
    iterate As Expression(Of Func(Of TState, TState)), _
    resultSelector As Expression(Of Func(Of TState, TResult)), _
    timeSelector As Expression(Of Func(Of TState, TimeSpan)), _
    scheduler As IScheduler _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim initialState As TState
Dim condition As Expression(Of Func(Of TState, Boolean))
Dim iterate As Expression(Of Func(Of TState, TState))
Dim resultSelector As Expression(Of Func(Of TState, TResult))
Dim timeSelector As Expression(Of Func(Of TState, TimeSpan))
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.Generate(initialState, _
    condition, iterate, resultSelector, _
    timeSelector, scheduler)
```

```csharp
public static IQbservable<TResult> Generate<TState, TResult>(
    this IQbservableProvider provider,
    TState initialState,
    Expression<Func<TState, bool>> condition,
    Expression<Func<TState, TState>> iterate,
    Expression<Func<TState, TResult>> resultSelector,
    Expression<Func<TState, TimeSpan>> timeSelector,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TState, typename TResult>
static IQbservable<TResult>^ Generate(
    IQbservableProvider^ provider, 
    TState initialState, 
    Expression<Func<TState, bool>^>^ condition, 
    Expression<Func<TState, TState>^>^ iterate, 
    Expression<Func<TState, TResult>^>^ resultSelector, 
    Expression<Func<TState, TimeSpan>^>^ timeSelector, 
    IScheduler^ scheduler
)
```

```fsharp
static member Generate : 
        provider:IQbservableProvider * 
        initialState:'TState * 
        condition:Expression<Func<'TState, bool>> * 
        iterate:Expression<Func<'TState, 'TState>> * 
        resultSelector:Expression<Func<'TState, 'TResult>> * 
        timeSelector:Expression<Func<'TState, TimeSpan>> * 
        scheduler:IScheduler -> IQbservable<'TResult> 
```


JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The type of state.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- initialState  
  Type: TState  
  The initial state.

- condition  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>\>  
  The condition to terminate generation.

- iterate  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TState\>\>  
  The iteration step function.

- resultSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TResult\>\>  
  The selector function for results produced in the sequence.

- timeSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)\>\>  
  The time selector function to control the speed of values being produced each iteration.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler on which to run the generator loop.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
The generated sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Generate Overload](Generate/Qbservable.Generate)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.Generate\<TState, TResult\> Method (IQbservableProvider, TState, Expression\<Func\<TState, Boolean\>\>, Expression\<Func\<TState, TState\>\>, Expression\<Func\<TState, TResult\>\>, Expression\<Func\<TState, DateTimeOffset\>\>, IScheduler)

Generates a queryable observable sequence by iterating a state from an initial state until the condition fails.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Generate(Of TState, TResult) ( _
    provider As IQbservableProvider, _
    initialState As TState, _
    condition As Expression(Of Func(Of TState, Boolean)), _
    iterate As Expression(Of Func(Of TState, TState)), _
    resultSelector As Expression(Of Func(Of TState, TResult)), _
    timeSelector As Expression(Of Func(Of TState, DateTimeOffset)), _
    scheduler As IScheduler _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim initialState As TState
Dim condition As Expression(Of Func(Of TState, Boolean))
Dim iterate As Expression(Of Func(Of TState, TState))
Dim resultSelector As Expression(Of Func(Of TState, TResult))
Dim timeSelector As Expression(Of Func(Of TState, DateTimeOffset))
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.Generate(initialState, _
    condition, iterate, resultSelector, _
    timeSelector, scheduler)
```

```csharp
public static IQbservable<TResult> Generate<TState, TResult>(
    this IQbservableProvider provider,
    TState initialState,
    Expression<Func<TState, bool>> condition,
    Expression<Func<TState, TState>> iterate,
    Expression<Func<TState, TResult>> resultSelector,
    Expression<Func<TState, DateTimeOffset>> timeSelector,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TState, typename TResult>
static IQbservable<TResult>^ Generate(
    IQbservableProvider^ provider, 
    TState initialState, 
    Expression<Func<TState, bool>^>^ condition, 
    Expression<Func<TState, TState>^>^ iterate, 
    Expression<Func<TState, TResult>^>^ resultSelector, 
    Expression<Func<TState, DateTimeOffset>^>^ timeSelector, 
    IScheduler^ scheduler
)
```

```fsharp
static member Generate : 
        provider:IQbservableProvider * 
        initialState:'TState * 
        condition:Expression<Func<'TState, bool>> * 
        iterate:Expression<Func<'TState, 'TState>> * 
        resultSelector:Expression<Func<'TState, 'TResult>> * 
        timeSelector:Expression<Func<'TState, DateTimeOffset>> * 
        scheduler:IScheduler -> IQbservable<'TResult> 
```


JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The type of state.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- initialState  
  Type: TState  
  The initial state.

- condition  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>\>  
  The condition to terminate generation.

- iterate  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TState\>\>  
  The iteration step function.

- resultSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TResult\>\>  
  The selector function for results produced in the sequence.

- timeSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)\>\>  
  The time selector function to control the speed of values being produced each iteration.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler on which to run the generator loop.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
The generated sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Generate Overload](Generate/Qbservable.Generate)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.Generate\<TState, TResult\> Method (IQbservableProvider, TState, Expression\<Func\<TState, Boolean\>\>, Expression\<Func\<TState, TState\>\>, Expression\<Func\<TState, TResult\>\>)

Generates a queryable observable sequence by iterating a state from an initial state until the condition fails.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Generate(Of TState, TResult) ( _
    provider As IQbservableProvider, _
    initialState As TState, _
    condition As Expression(Of Func(Of TState, Boolean)), _
    iterate As Expression(Of Func(Of TState, TState)), _
    resultSelector As Expression(Of Func(Of TState, TResult)) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim initialState As TState
Dim condition As Expression(Of Func(Of TState, Boolean))
Dim iterate As Expression(Of Func(Of TState, TState))
Dim resultSelector As Expression(Of Func(Of TState, TResult))
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.Generate(initialState, _
    condition, iterate, resultSelector)
```

```csharp
public static IQbservable<TResult> Generate<TState, TResult>(
    this IQbservableProvider provider,
    TState initialState,
    Expression<Func<TState, bool>> condition,
    Expression<Func<TState, TState>> iterate,
    Expression<Func<TState, TResult>> resultSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TState, typename TResult>
static IQbservable<TResult>^ Generate(
    IQbservableProvider^ provider, 
    TState initialState, 
    Expression<Func<TState, bool>^>^ condition, 
    Expression<Func<TState, TState>^>^ iterate, 
    Expression<Func<TState, TResult>^>^ resultSelector
)
```

```fsharp
static member Generate : 
        provider:IQbservableProvider * 
        initialState:'TState * 
        condition:Expression<Func<'TState, bool>> * 
        iterate:Expression<Func<'TState, 'TState>> * 
        resultSelector:Expression<Func<'TState, 'TResult>> -> IQbservable<'TResult> 
```


JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The type of state.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- initialState  
  Type: TState  
  The initial state.

- condition  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>\>  
  The condition to terminate generation.

- iterate  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TState\>\>  
  The iteration step function.

- resultSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TResult\>\>  
  The selector function for results produced in the sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
The generated sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Generate Overload](Generate/Qbservable.Generate)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.Generate\<TState, TResult\> Method (IQbservableProvider, TState, Expression\<Func\<TState, Boolean\>\>, Expression\<Func\<TState, TState\>\>, Expression\<Func\<TState, TResult\>\>, Expression\<Func\<TState, TimeSpan\>\>)

Generates a queryable observable sequence by iterating a state from an initial state until the condition fails.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Generate(Of TState, TResult) ( _
    provider As IQbservableProvider, _
    initialState As TState, _
    condition As Expression(Of Func(Of TState, Boolean)), _
    iterate As Expression(Of Func(Of TState, TState)), _
    resultSelector As Expression(Of Func(Of TState, TResult)), _
    timeSelector As Expression(Of Func(Of TState, TimeSpan)) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim initialState As TState
Dim condition As Expression(Of Func(Of TState, Boolean))
Dim iterate As Expression(Of Func(Of TState, TState))
Dim resultSelector As Expression(Of Func(Of TState, TResult))
Dim timeSelector As Expression(Of Func(Of TState, TimeSpan))
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.Generate(initialState, _
    condition, iterate, resultSelector, _
    timeSelector)
```

```csharp
public static IQbservable<TResult> Generate<TState, TResult>(
    this IQbservableProvider provider,
    TState initialState,
    Expression<Func<TState, bool>> condition,
    Expression<Func<TState, TState>> iterate,
    Expression<Func<TState, TResult>> resultSelector,
    Expression<Func<TState, TimeSpan>> timeSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TState, typename TResult>
static IQbservable<TResult>^ Generate(
    IQbservableProvider^ provider, 
    TState initialState, 
    Expression<Func<TState, bool>^>^ condition, 
    Expression<Func<TState, TState>^>^ iterate, 
    Expression<Func<TState, TResult>^>^ resultSelector, 
    Expression<Func<TState, TimeSpan>^>^ timeSelector
)
```

```fsharp
static member Generate : 
        provider:IQbservableProvider * 
        initialState:'TState * 
        condition:Expression<Func<'TState, bool>> * 
        iterate:Expression<Func<'TState, 'TState>> * 
        resultSelector:Expression<Func<'TState, 'TResult>> * 
        timeSelector:Expression<Func<'TState, TimeSpan>> -> IQbservable<'TResult> 
```


JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The type of state.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- initialState  
  Type: TState  
  The initial state.

- condition  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>\>  
  The condition to terminate generation.

- iterate  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TState\>\>  
  The iteration step function.

- resultSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TResult\>\>  
  The selector function for results produced in the sequence.

- timeSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [TimeSpan](https://msdn.microsoft.com/en-us/library/269ew577)\>\>  
  The time selector function to control the speed of values being produced each iteration.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
The generated sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Generate Overload](Generate/Qbservable.Generate)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.Generate\<TState, TResult\> Method (IQbservableProvider, TState, Expression\<Func\<TState, Boolean\>\>, Expression\<Func\<TState, TState\>\>, Expression\<Func\<TState, TResult\>\>, Expression\<Func\<TState, DateTimeOffset\>\>)

Generates a queryable observable sequence by iterating a state from an initial state until the condition fails.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Generate(Of TState, TResult) ( _
    provider As IQbservableProvider, _
    initialState As TState, _
    condition As Expression(Of Func(Of TState, Boolean)), _
    iterate As Expression(Of Func(Of TState, TState)), _
    resultSelector As Expression(Of Func(Of TState, TResult)), _
    timeSelector As Expression(Of Func(Of TState, DateTimeOffset)) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim initialState As TState
Dim condition As Expression(Of Func(Of TState, Boolean))
Dim iterate As Expression(Of Func(Of TState, TState))
Dim resultSelector As Expression(Of Func(Of TState, TResult))
Dim timeSelector As Expression(Of Func(Of TState, DateTimeOffset))
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.Generate(initialState, _
    condition, iterate, resultSelector, _
    timeSelector)
```

```csharp
public static IQbservable<TResult> Generate<TState, TResult>(
    this IQbservableProvider provider,
    TState initialState,
    Expression<Func<TState, bool>> condition,
    Expression<Func<TState, TState>> iterate,
    Expression<Func<TState, TResult>> resultSelector,
    Expression<Func<TState, DateTimeOffset>> timeSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TState, typename TResult>
static IQbservable<TResult>^ Generate(
    IQbservableProvider^ provider, 
    TState initialState, 
    Expression<Func<TState, bool>^>^ condition, 
    Expression<Func<TState, TState>^>^ iterate, 
    Expression<Func<TState, TResult>^>^ resultSelector, 
    Expression<Func<TState, DateTimeOffset>^>^ timeSelector
)
```

```fsharp
static member Generate : 
        provider:IQbservableProvider * 
        initialState:'TState * 
        condition:Expression<Func<'TState, bool>> * 
        iterate:Expression<Func<'TState, 'TState>> * 
        resultSelector:Expression<Func<'TState, 'TResult>> * 
        timeSelector:Expression<Func<'TState, DateTimeOffset>> -> IQbservable<'TResult> 
```


JScript does not support generic types and methods.
```

#### Type Parameters

- TState  
  The type of state.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- initialState  
  Type: TState  
  The initial state.

- condition  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>\>  
  The condition to terminate generation.

- iterate  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TState\>\>  
  The iteration step function.

- resultSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, TResult\>\>  
  The selector function for results produced in the sequence.

- timeSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TState, [DateTimeOffset](https://msdn.microsoft.com/en-us/library/Bb341783)\>\>  
  The time selector function to control the speed of values being produced each iteration.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
The generated sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Generate Overload](Generate/Qbservable.Generate)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








