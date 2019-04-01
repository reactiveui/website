# Qbservable.StartWith\<TSource\> Method (IQbservable\<TSource\>, array\<TSource\[\])

Prepends a sequence of values to a queryable observable sequence with the specified source and values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function StartWith(Of TSource) ( _
    source As IQbservable(Of TSource), _
    ParamArray values As TSource() _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim values As TSource()
Dim returnValue As IQbservable(Of TSource)

returnValue = source.StartWith(values)
```

```csharp
public static IQbservable<TSource> StartWith<TSource>(
    this IQbservable<TSource> source,
    params TSource[] values
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ StartWith(
    IQbservable<TSource>^ source, 
    ... array<TSource>^ values
)
```

```fsharp
static member StartWith : 
        source:IQbservable<'TSource> * 
        values:'TSource[] -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to prepend values to.

- values  
  Type: array\<TSource\[\]  
  The values to prepend to the specified sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
The source sequence prepended with the specified values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[StartWith Overload](StartWith\Qbservable.StartWith.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.StartWith\<TSource\> Method (IQbservable\<TSource\>, IScheduler, array\<TSource\[\])

Prepends a sequence of values to a queryable observable sequence with the specified source, scheduler and values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function StartWith(Of TSource) ( _
    source As IQbservable(Of TSource), _
    scheduler As IScheduler, _
    ParamArray values As TSource() _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim scheduler As IScheduler
Dim values As TSource()
Dim returnValue As IQbservable(Of TSource)

returnValue = source.StartWith(scheduler, _
    values)
```

```csharp
public static IQbservable<TSource> StartWith<TSource>(
    this IQbservable<TSource> source,
    IScheduler scheduler,
    params TSource[] values
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ StartWith(
    IQbservable<TSource>^ source, 
    IScheduler^ scheduler, 
    ... array<TSource>^ values
)
```

```fsharp
static member StartWith : 
        source:IQbservable<'TSource> * 
        scheduler:IScheduler * 
        values:'TSource[] -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to prepend values to.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to emit the prepended values on.

- values  
  Type: array\<TSource\[\]  
  The values to prepend to the specified sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
The source sequence prepended with the specified values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[StartWith Overload](StartWith\Qbservable.StartWith.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
