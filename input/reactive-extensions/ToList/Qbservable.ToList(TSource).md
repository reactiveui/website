title: Qbservable.ToList<TSource>()
---
# Qbservable.ToList\<TSource\> Method

Creates a list from a queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToList(Of TSource) ( _
    source As IQbservable(Of TSource) _
) As IQbservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim returnValue As IQbservable(Of IList(Of TSource))

returnValue = source.ToList()
```

```csharp
public static IQbservable<IList<TSource>> ToList<TSource>(
    this IQbservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<IList<TSource>^>^ ToList(
    IQbservable<TSource>^ source
)
```

```fsharp
static member ToList : 
        source:IQbservable<'TSource> -> IQbservable<IList<'TSource>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source queryable observable sequence to get a list of elements for.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
A list from a queryable observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
