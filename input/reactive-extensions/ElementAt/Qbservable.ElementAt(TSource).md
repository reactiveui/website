title: Qbservable.ElementAt<TSource>()
---
# Qbservable.ElementAt\<TSource\> Method

Returns the element at a specified index in a sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ElementAt(Of TSource) ( _
    source As IQbservable(Of TSource), _
    index As Integer _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim index As Integer
Dim returnValue As IQbservable(Of TSource)

returnValue = source.ElementAt(index)
```

```csharp
public static IQbservable<TSource> ElementAt<TSource>(
    this IQbservable<TSource> source,
    int index
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ ElementAt(
    IQbservable<TSource>^ source, 
    int index
)
```

```fsharp
static member ElementAt : 
        source:IQbservable<'TSource> * 
        index:int -> IQbservable<'TSource> 
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
  The queryable observable sequence to return the element from.

- index  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The zero-based index of the element to retrieve.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
A queryable observable sequence that produces the element at the specified position in the source sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








