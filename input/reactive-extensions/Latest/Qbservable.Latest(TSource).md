title: Qbservable.Latest<TSource>()
---
# Qbservable.Latest\<TSource\> Method

Samples the most recent value in a queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Latest(Of TSource) ( _
    source As IQbservable(Of TSource) _
) As IQueryable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim returnValue As IQueryable(Of TSource)

returnValue = source.Latest()
```

```csharp
public static IQueryable<TSource> Latest<TSource>(
    this IQbservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQueryable<TSource>^ Latest(
    IQbservable<TSource>^ source
)
```

```fsharp
static member Latest : 
        source:IQbservable<'TSource> -> IQueryable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source queryable observable sequence.

#### Return Value

Type: [System.Linq.IQueryable](https://msdn.microsoft.com/en-us/library/Bb351562)\<TSource\>  
The enumerable sequence that returns the last sampled element upon each iteration and subsequently blocks until the next element in the queryable observable source sequence becomes available.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








