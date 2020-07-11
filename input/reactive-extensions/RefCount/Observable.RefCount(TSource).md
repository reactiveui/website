title: Observable.RefCount<TSource>()
---
# Observable.RefCount\<TSource\> Method

Returns an observable sequence that stays connected to the source as long as there is at least one subscription to the observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function RefCount(Of TSource) ( _
    source As IConnectableObservable(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IConnectableObservable(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = source.RefCount()
```

```csharp
public static IObservable<TSource> RefCount<TSource>(
    this IConnectableObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ RefCount(
    IConnectableObservable<TSource>^ source
)
```

```fsharp
static member RefCount : 
        source:IConnectableObservable<'TSource> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Subjects.IConnectableObservable](IConnectableObservable/IConnectableObservable(T))\<TSource\>  
  The connectable observable sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence that stays connected to the source.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IConnectableObservable](IConnectableObservable/IConnectableObservable(T))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
