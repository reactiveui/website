title: Qbservable.Count<TSource>()
---
# Qbservable.Count\<TSource\> Method

Returns a \[System.Int32\] that represents the total number of elements in a queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Count(Of TSource) ( _
    source As IQbservable(Of TSource) _
) As IQbservable(Of Integer)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim returnValue As IQbservable(Of Integer)

returnValue = source.Count()
```

```csharp
public static IQbservable<int> Count<TSource>(
    this IQbservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<int>^ Count(
    IQbservable<TSource>^ source
)
```

```fsharp
static member Count : 
        source:IQbservable<'TSource> -> IQbservable<int> 
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
  A queryable observable sequence that contains elements to be counted.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>  
A \[System.Int32\] that represents the total number of elements in a queryable observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








