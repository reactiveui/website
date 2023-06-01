title: Qbservable.Repeat<TResult>(IQbservableProvider, TResult, Int32)
---
# Qbservable.Repeat\<TResult\> Method (IQbservableProvider, TResult, Int32)

Generates a queryable observable sequence that repeats the given element the specified number of times.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Repeat(Of TResult) ( _
    provider As IQbservableProvider, _
    value As TResult, _
    repeatCount As Integer _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim value As TResult
Dim repeatCount As Integer
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.Repeat(value, _
    repeatCount)
```

```csharp
public static IQbservable<TResult> Repeat<TResult>(
    this IQbservableProvider provider,
    TResult value,
    int repeatCount
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static IQbservable<TResult>^ Repeat(
    IQbservableProvider^ provider, 
    TResult value, 
    int repeatCount
)
```

```fsharp
static member Repeat : 
        provider:IQbservableProvider * 
        value:'TResult * 
        repeatCount:int -> IQbservable<'TResult> 
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

- value  
  Type: TResult  
  The element to repeat.

- repeatCount  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The number of times to repeat the element.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence that repeats the given element the specified number of times.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Repeat Overload](Repeat/Qbservable.Repeat)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Repeat\<TResult\> Method (IQbservableProvider, TResult, Int32, IScheduler)

Generates a queryable observable sequence that repeats the given element of the specified number of times.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Repeat(Of TResult) ( _
    provider As IQbservableProvider, _
    value As TResult, _
    repeatCount As Integer, _
    scheduler As IScheduler _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim value As TResult
Dim repeatCount As Integer
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.Repeat(value, _
    repeatCount, scheduler)
```

```csharp
public static IQbservable<TResult> Repeat<TResult>(
    this IQbservableProvider provider,
    TResult value,
    int repeatCount,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static IQbservable<TResult>^ Repeat(
    IQbservableProvider^ provider, 
    TResult value, 
    int repeatCount, 
    IScheduler^ scheduler
)
```

```fsharp
static member Repeat : 
        provider:IQbservableProvider * 
        value:'TResult * 
        repeatCount:int * 
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

- value  
  Type: TResult  
  The element to repeat.

- repeatCount  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The number of times to repeat the element.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to run the producer loop on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence that repeats the given element of the specified number of times.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Repeat Overload](Repeat/Qbservable.Repeat)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Repeat\<TResult\> Method (IQbservableProvider, TResult)

Generates a queryable observable sequence that repeats the given element infinitely.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Repeat(Of TResult) ( _
    provider As IQbservableProvider, _
    value As TResult _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim value As TResult
Dim returnValue As IQbservable(Of TResult)

returnValue = provider.Repeat(value)
```

```csharp
public static IQbservable<TResult> Repeat<TResult>(
    this IQbservableProvider provider,
    TResult value
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static IQbservable<TResult>^ Repeat(
    IQbservableProvider^ provider, 
    TResult value
)
```

```fsharp
static member Repeat : 
        provider:IQbservableProvider * 
        value:'TResult -> IQbservable<'TResult> 
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

- value  
  Type: TResult  
  The element to repeat.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence that repeats the given element infinitely.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Repeat Overload](Repeat/Qbservable.Repeat)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Repeat\<TResult\> Method (IQbservableProvider, TResult, IScheduler)

Generates a queryable observable sequence that repeats the given element infinitely.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Repeat(Of TResult) ( _
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

returnValue = provider.Repeat(value, _
    scheduler)
```

```csharp
public static IQbservable<TResult> Repeat<TResult>(
    this IQbservableProvider provider,
    TResult value,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static IQbservable<TResult>^ Repeat(
    IQbservableProvider^ provider, 
    TResult value, 
    IScheduler^ scheduler
)
```

```fsharp
static member Repeat : 
        provider:IQbservableProvider * 
        value:'TResult * 
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

- value  
  Type: TResult  
  The element to repeat.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  The scheduler to run the producer loop on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
The queryable observable sequence that repeats the given element infinitely.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Repeat Overload](Repeat/Qbservable.Repeat)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
