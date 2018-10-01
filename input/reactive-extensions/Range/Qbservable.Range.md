# Qbservable.Range Method (IQbservableProvider, Int32, Int32)

Generates a queryable observable sequence of integral numbers within a specified range.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Range ( _
    provider As IQbservableProvider, _
    start As Integer, _
    count As Integer _
) As IQbservable(Of Integer)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim start As Integer
Dim count As Integer
Dim returnValue As IQbservable(Of Integer)

returnValue = provider.Range(start, _
    count)
```

```csharp
public static IQbservable<int> Range(
    this IQbservableProvider provider,
    int start,
    int count
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<int>^ Range(
    IQbservableProvider^ provider, 
    int start, 
    int count
)
```

```fsharp
static member Range : 
        provider:IQbservableProvider * 
        start:int * 
        count:int -> IQbservable<int> 
```

```jscript
public static function Range(
    provider : IQbservableProvider, 
    start : int, 
    count : int
) : IQbservable<int>
```

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- start  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The value of the first integer in the sequence.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The number of sequential integers to generate.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
A queryable observable sequence that contains a range of sequential integral numbers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Range Overload](Range\Qbservable.Range.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Range Method (IQbservableProvider, Int32, Int32, IScheduler)

Generates a queryable observable sequence of integral numbers within a specified range.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Range ( _
    provider As IQbservableProvider, _
    start As Integer, _
    count As Integer, _
    scheduler As IScheduler _
) As IQbservable(Of Integer)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim start As Integer
Dim count As Integer
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of Integer)

returnValue = provider.Range(start, _
    count, scheduler)
```

```csharp
public static IQbservable<int> Range(
    this IQbservableProvider provider,
    int start,
    int count,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<int>^ Range(
    IQbservableProvider^ provider, 
    int start, 
    int count, 
    IScheduler^ scheduler
)
```

```fsharp
static member Range : 
        provider:IQbservableProvider * 
        start:int * 
        count:int * 
        scheduler:IScheduler -> IQbservable<int> 
```

```jscript
public static function Range(
    provider : IQbservableProvider, 
    start : int, 
    count : int, 
    scheduler : IScheduler
) : IQbservable<int>
```

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- start  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The value of the first integer in the sequence.

- count  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The number of sequential integers to generate.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run the producer loop on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
A queryable observable sequence that contains a range of sequential integral numbers.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Range Overload](Range\Qbservable.Range.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Range Method

Include Protected Members  
Include Inherited Members

Generates a queryable observable sequence of integral numbers within a specified range.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Range(IQbservableProvider, Int32, Int32)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.range(system.reactive.linq.iqbservableprovider%2csystem.int32%2csystem.int32)(v=VS.103))Generates a queryable observable sequence of integral numbers within a specified range.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](images\Hh244319.static(en-us,VS.103).gif "Static member")[Range(IQbservableProvider, Int32, Int32, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.range(system.reactive.linq.iqbservableprovider%2csystem.int32%2csystem.int32%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Generates a queryable observable sequence of integral numbers within a specified range.Top

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
