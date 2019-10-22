title: Qbservable.Return<TResult>(IQbservableProvider, TResult)
---
# Qbservable.Return\<TResult\> Method (IQbservableProvider, TResult)

Returns a queryable observable sequence that contains a single element with a specified value.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Return(Of TResult) ( _
    provider As IQbservableProvider, _
    value As TResult _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim value As TResult
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.Return(value)
```

```csharp
public static IQbservable<TResult> Return<TResult>(
    this IQbservableProvider provider,
    TResult value
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static IQbservable<TResult>^ Return(
    IQbservableProvider^ provider, 
    TResult value
)
```

```fsharp
static member Return : 
        provider:IQbservableProvider * 
        value:'TResult -> IQbservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- value  
  Type: TResult  
  The single element in the resulting queryable observable sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>  
The queryable observable sequence containing the single specified element.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Return Overload](Return\Qbservable.Return.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Return\<TResult\> Method (IQbservableProvider, TResult, IScheduler)

Returns a queryable observable sequence that contains a single value with a specified value and scheduler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Return(Of TResult) ( _
    provider As IQbservableProvider, _
    value As TResult, _
    scheduler As IScheduler _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim value As TResult
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.Return(value, _
    scheduler)
```

```csharp
public static IQbservable<TResult> Return<TResult>(
    this IQbservableProvider provider,
    TResult value,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static IQbservable<TResult>^ Return(
    IQbservableProvider^ provider, 
    TResult value, 
    IScheduler^ scheduler
)
```

```fsharp
static member Return : 
        provider:IQbservableProvider * 
        value:'TResult * 
        scheduler:IScheduler -> IQbservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- value  
  Type: TResult  
  The single element in the resulting observable sequence.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to send the single element on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>  
The queryable observable sequence containing the single specified element.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Return Overload](Return\Qbservable.Return.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
