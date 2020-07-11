title: Qbservable.LongCount<TSource>()
---
# Qbservable.LongCount\<TSource\> Method

Returns a \[System.Int64\] that represents the total number of elements in a queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function LongCount(Of TSource) ( _
    source As IQbservable(Of TSource) _
) As IQbservable(Of Long)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim returnValue As IQbservable(Of Long)

returnValue = source.LongCount()
```

```csharp
public static IQbservable<long> LongCount<TSource>(
    this IQbservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<long long>^ LongCount(
    IQbservable<TSource>^ source
)
```

```fsharp
static member LongCount : 
        source:IQbservable<'TSource> -> IQbservable<int64> 
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

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Int64](https://msdn.microsoft.com/en-us/library/6yy583ek)\>  
A \[System.Int64\] that represents the total number of elements in a queryable observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








