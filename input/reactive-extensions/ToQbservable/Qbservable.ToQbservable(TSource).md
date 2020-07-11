title: Qbservable.ToQbservable<TSource>()
---
# Qbservable.ToQbservable\<TSource\> Method

Converts an enumerable sequence to a queryable observable sequence with a specified source.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToQbservable(Of TSource) ( _
    source As IQueryable(Of TSource) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQueryable(Of TSource)
Dim returnValue As IQbservable(Of TSource)

returnValue = source.ToQbservable()
```

```csharp
public static IQbservable<TSource> ToQbservable<TSource>(
    this IQueryable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ ToQbservable(
    IQueryable<TSource>^ source
)
```

```fsharp
static member ToQbservable : 
        source:IQueryable<'TSource> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Linq.IQueryable](https://msdn.microsoft.com/en-us/library/Bb351562)\<TSource\>  
  The enumerable sequence to convert to a queryable observable sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
The queryable observable sequence whose elements are pulled from the given enumerable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQueryable](https://msdn.microsoft.com/en-us/library/Bb351562)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
