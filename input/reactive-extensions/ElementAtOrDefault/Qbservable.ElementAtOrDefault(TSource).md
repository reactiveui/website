# Qbservable.ElementAtOrDefault\<TSource\> Method

Returns the element at a specified index in a sequence or a default value if the index is out of range.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ElementAtOrDefault(Of TSource) ( _
    source As IQbservable(Of TSource), _
    index As Integer _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim index As Integer
Dim returnValue As IQbservable(Of TSource)

returnValue = source.ElementAtOrDefault(index)
```

```csharp
public static IQbservable<TSource> ElementAtOrDefault<TSource>(
    this IQbservable<TSource> source,
    int index
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ ElementAtOrDefault(
    IQbservable<TSource>^ source, 
    int index
)
```

```fsharp
static member ElementAtOrDefault : 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The queryable observable sequence to return the element from.

- index  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The zero-based index of the element to retrieve.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
A queryable observable sequence that produces the element at the specified position in the source sequence or a default value if the index is outside the bounds of the source sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








