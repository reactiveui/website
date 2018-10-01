# Qbservable.SkipUntil\<TSource, TOther\> Method

Returns the values from the source queryable observable sequence only after the other queryable observable sequence produces a value.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function SkipUntil(Of TSource, TOther) ( _
    source As IQbservable(Of TSource), _
    other As IObservable(Of TOther) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim other As IObservable(Of TOther)
Dim returnValue As IQbservable(Of TSource)

returnValue = source.SkipUntil(other)
```

```csharp
public static IQbservable<TSource> SkipUntil<TSource, TOther>(
    this IQbservable<TSource> source,
    IObservable<TOther> other
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TOther>
static IQbservable<TSource>^ SkipUntil(
    IQbservable<TSource>^ source, 
    IObservable<TOther>^ other
)
```

```fsharp
static member SkipUntil : 
        source:IQbservable<'TSource> * 
        other:IObservable<'TOther> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TOther  
  The other type.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence to propagate elements for.

- other  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TOther\>  
  The queryable observable sequence that triggers propagation of elements of the source sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
A queryable observable sequence containing the elements of the source sequence starting from the point the other sequence triggered propagation.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
