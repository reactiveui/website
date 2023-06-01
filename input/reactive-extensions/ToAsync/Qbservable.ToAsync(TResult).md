title: Qbservable.ToAsync<TResult>(IQbservableProvider, Expression<Func<TResult>>)
---
# Qbservable.ToAsync\<TResult\> Method (IQbservableProvider, Expression\<Func\<TResult\>\>)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of TResult)) _
) As Func(Of IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of TResult))
Dim returnValue As Func(Of IQbservable(Of TResult))

returnValue = provider.ToAsync(function)
```

```csharp
public static Func<IQbservable<TResult>> ToAsync<TResult>(
    this IQbservableProvider provider,
    Expression<Func<TResult>> function
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static Func<IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<TResult>^>^ function
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'TResult>> -> Func<IQbservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<TResult\>\>  
  The function used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IQbservable](IQbservable/IQbservable(TSource))\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[ToAsync Overload](ToAsync/Qbservable.ToAsync)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.ToAsync\<TResult\> Method (IQbservableProvider, Expression\<Func\<TResult\>\>, IScheduler)

Converts the function into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToAsync(Of TResult) ( _
    provider As IQbservableProvider, _
    function As Expression(Of Func(Of TResult)), _
    scheduler As IScheduler _
) As Func(Of IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim function As Expression(Of Func(Of TResult))
Dim scheduler As IScheduler
Dim returnValue As Func(Of IQbservable(Of TResult))

returnValue = provider.ToAsync(function, _
    scheduler)
```

```csharp
public static Func<IQbservable<TResult>> ToAsync<TResult>(
    this IQbservableProvider provider,
    Expression<Func<TResult>> function,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static Func<IQbservable<TResult>^>^ ToAsync(
    IQbservableProvider^ provider, 
    Expression<Func<TResult>^>^ function, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToAsync : 
        provider:IQbservableProvider * 
        function:Expression<Func<'TResult>> * 
        scheduler:IScheduler -> Func<IQbservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- function  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<TResult\>\>  
  The function used to synchronization.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler used to synchronization.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IQbservable](IQbservable/IQbservable(TSource))\<TResult\>\>  
The function into an asynchronous function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[ToAsync Overload](ToAsync/Qbservable.ToAsync)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
