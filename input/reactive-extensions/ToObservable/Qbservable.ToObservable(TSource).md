# Qbservable.ToObservable\<TSource\> Method (IQbservableProvider, IEnumerable\<TSource\>)

Converts an enumerable sequence to a queryable observable sequence with a specified source.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToObservable(Of TSource) ( _
    provider As IQbservableProvider, _
    source As IEnumerable(Of TSource) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim source As IEnumerable(Of TSource)
Dim returnValue As IQbservable(Of TSource)

returnValue = provider.ToObservable(source)
```

```csharp
public static IQbservable<TSource> ToObservable<TSource>(
    this IQbservableProvider provider,
    IEnumerable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ ToObservable(
    IQbservableProvider^ provider, 
    IEnumerable<TSource>^ source
)
```

```fsharp
static member ToObservable : 
        provider:IQbservableProvider * 
        source:IEnumerable<'TSource> -> IQbservable<'TSource> 
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

- source  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TSource\>  
  The enumerable sequence to convert to a queryable observable sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
The queryable observable sequence whose elements are pulled from the given enumerable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToObservable Overload](ToObservable\Qbservable.ToObservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.ToObservable\<TSource\> Method (IQbservableProvider, IEnumerable\<TSource\>, IScheduler)

Converts an enumerable sequence to a queryable observable sequence with a specified source and scheduler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToObservable(Of TSource) ( _
    provider As IQbservableProvider, _
    source As IEnumerable(Of TSource), _
    scheduler As IScheduler _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim source As IEnumerable(Of TSource)
Dim scheduler As IScheduler
Dim returnValue As IQbservable(Of TSource)

returnValue = provider.ToObservable(source, _
    scheduler)
```

```csharp
public static IQbservable<TSource> ToObservable<TSource>(
    this IQbservableProvider provider,
    IEnumerable<TSource> source,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ ToObservable(
    IQbservableProvider^ provider, 
    IEnumerable<TSource>^ source, 
    IScheduler^ scheduler
)
```

```fsharp
static member ToObservable : 
        provider:IQbservableProvider * 
        source:IEnumerable<'TSource> * 
        scheduler:IScheduler -> IQbservable<'TSource> 
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

- source  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TSource\>  
  The enumerable sequence to convert to a queryable observable sequence.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run the enumeration of the input sequence on.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
The queryable observable sequence whose elements are pulled from the given enumerable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[ToObservable Overload](ToObservable\Qbservable.ToObservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
