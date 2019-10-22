title: Observable.Dematerialize<TSource>()
---
# Observable.Dematerialize\<TSource\> Method

Dematerializes the explicit notification values of an observable sequence as implicit notifications.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Dematerialize(Of TSource) ( _
    source As IObservable(Of Notification(Of TSource)) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of Notification(Of TSource))
Dim returnValue As IObservable(Of TSource)

returnValue = source.Dematerialize()
```

```csharp
public static IObservable<TSource> Dematerialize<TSource>(
    this IObservable<Notification<TSource>> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Dematerialize(
    IObservable<Notification<TSource>^>^ source
)
```

```fsharp
static member Dematerialize : 
        source:IObservable<Notification<'TSource>> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Notification](Notification\Notification(T).md)\<TSource\>\>  
  An observable sequence containing explicit notification values which have to be turned into implicit notifications.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence exhibiting the behavior corresponding to the source sequence's notification values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Notification](Notification\Notification(T).md)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








