title: Observable.TakeUntil<TSource, TOther>()
---
# Observable.TakeUntil\<TSource, TOther\> Method

Returns the values from the source observable sequence until the other observable sequence produces a value.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function TakeUntil(Of TSource, TOther) ( _
    source As IObservable(Of TSource), _
    other As IObservable(Of TOther) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim other As IObservable(Of TOther)
Dim returnValue As IObservable(Of TSource)

returnValue = source.TakeUntil(other)
```

```csharp
public static IObservable<TSource> TakeUntil<TSource, TOther>(
    this IObservable<TSource> source,
    IObservable<TOther> other
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TOther>
static IObservable<TSource>^ TakeUntil(
    IObservable<TSource>^ source, 
    IObservable<TOther>^ other
)
```

```fsharp
static member TakeUntil : 
        source:IObservable<'TSource> * 
        other:IObservable<'TOther> -> IObservable<'TSource> 
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
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence to propagate elements for.

- other  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TOther\>  
  The observable sequence that terminates propagation of elements of the source sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence containing the elements of the source sequence up to the point the other sequence interrupted further propagation.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
