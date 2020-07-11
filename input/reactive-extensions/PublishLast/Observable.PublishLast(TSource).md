title: Observable.PublishLast<TSource>(IObservable<TSource>)
---
# Observable.PublishLast\<TSource\> Method (IObservable\<TSource\>)

Returns a connectable observable sequence that shares a single subscription to the underlying sequence containing only the last notification.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function PublishLast(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IConnectableObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IConnectableObservable(Of TSource)

returnValue = source.PublishLast()
```

```csharp
public static IConnectableObservable<TSource> PublishLast<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IConnectableObservable<TSource>^ PublishLast(
    IObservable<TSource>^ source
)
```

```fsharp
static member PublishLast : 
        source:IObservable<'TSource> -> IConnectableObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

#### Return Value

Type: [System.Reactive.Subjects.IConnectableObservable](IConnectableObservable/IConnectableObservable(T))\<TSource\>  
A connectable observable sequence that shares a single subscription to the underlying sequence containing only the last notification.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[PublishLast Overload](PublishLast/Observable.PublishLast)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
