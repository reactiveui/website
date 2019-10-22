title: Qbservable.ToQueryable<TSource>()
---
# Qbservable.ToQueryable\<TSource\> Method

Converts an enumerable sequence to a queryable observable sequence with a specified source.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToQueryable(Of TSource) ( _
    source As IQbservable(Of TSource) _
) As IQueryable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim returnValue As IQueryable(Of TSource)

returnValue = source.ToQueryable()
```

```csharp
public static IQueryable<TSource> ToQueryable<TSource>(
    this IQbservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQueryable<TSource>^ ToQueryable(
    IQbservable<TSource>^ source
)
```

```fsharp
static member ToQueryable : 
        source:IQbservable<'TSource> -> IQueryable<'TSource> 
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
  The enumerable sequence to convert to a queryable observable sequence.

#### Return Value

Type: [System.Linq.IQueryable](https://msdn.microsoft.com/en-us/library/Bb351562)\<TSource\>  
The queryable observable sequence whose elements are pulled from the given enumerable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
