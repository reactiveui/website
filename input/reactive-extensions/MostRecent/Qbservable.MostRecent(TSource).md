title: Qbservable.MostRecent<TSource>()
---
# Qbservable.MostRecent\<TSource\> Method

Samples the most recent value in a queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function MostRecent(Of TSource) ( _
    source As IQbservable(Of TSource), _
    initialValue As TSource _
) As IQueryable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim initialValue As TSource
Dim returnValue As IQueryable(Of TSource)

returnValue = source.MostRecent(initialValue)
```

```csharp
public static IQueryable<TSource> MostRecent<TSource>(
    this IQbservable<TSource> source,
    TSource initialValue
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQueryable<TSource>^ MostRecent(
    IQbservable<TSource>^ source, 
    TSource initialValue
)
```

```fsharp
static member MostRecent : 
        source:IQbservable<'TSource> * 
        initialValue:'TSource -> IQueryable<'TSource> 
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
  The source queryable observable sequence.

- initialValue  
  Type: TSource  
  The initial value that will be yielded by the enumerable sequence if no element has been sampled yet.

#### Return Value

Type: [System.Linq.IQueryable](https://msdn.microsoft.com/en-us/library/Bb351562)\<TSource\>  
The enumerable sequence that returns the last sampled element upon each iteration.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








