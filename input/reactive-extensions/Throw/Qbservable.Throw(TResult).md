title: Qbservable.Throw<TResult>(IQbservableProvider, Exception)
---
# Qbservable.Throw\<TResult\> Method (IQbservableProvider, Exception)

Returns a queryable observable sequence that terminates with an exception.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Throw(Of TResult) ( _
    provider As IQbservableProvider, _
    exception As Exception _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim exception As Exception
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.Throw(exception)
```

```csharp
public static IQbservable<TResult> Throw<TResult>(
    this IQbservableProvider provider,
    Exception exception
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static IQbservable<TResult>^ Throw(
    IQbservableProvider^ provider, 
    Exception^ exception
)
```

```fsharp
static member Throw : 
        provider:IQbservableProvider * 
        exception:Exception -> IQbservable<'TResult> 
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

- exception  
  Type: [System.Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)  
  The exception object used for the sequence’s termination.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence that terminates exceptionally with the specified exception object.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Throw Overload](Throw/Qbservable.Throw)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Throw\<TResult\> Method (IQbservableProvider, Exception, IScheduler)

Returns a queryable observable sequence that terminates with an exception with the specified scheduler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Throw(Of TResult) ( _
    provider As IQbservableProvider, _
    exception As Exception, _
    scheduler As IScheduler _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim exception As Exception
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.Throw(exception, _
    scheduler)
```

```csharp
public static IQbservable<TResult> Throw<TResult>(
    this IQbservableProvider provider,
    Exception exception,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static IQbservable<TResult>^ Throw(
    IQbservableProvider^ provider, 
    Exception^ exception, 
    IScheduler^ scheduler
)
```

```fsharp
static member Throw : 
        provider:IQbservableProvider * 
        exception:Exception * 
        scheduler:IScheduler -> IQbservable<'TResult> 
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

- exception  
  Type: [System.Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)  
  The exception object used for the sequence’s termination.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to send the exceptional termination call on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence that terminates exceptionally with the specified exception object.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Throw Overload](Throw/Qbservable.Throw)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
