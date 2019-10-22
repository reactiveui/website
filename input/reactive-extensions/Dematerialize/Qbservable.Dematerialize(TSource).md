title: Qbservable.Dematerialize<TSource>()
---
# Qbservable.Dematerialize\<TSource\> Method

Dematerializes the explicit notification values of a queryable observable sequence as implicit notifications.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Dematerialize(Of TSource) ( _
    source As IQbservable(Of Notification(Of TSource)) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of Notification(Of TSource))
Dim returnValue As IQbservable(Of TSource)

returnValue = source.Dematerialize()
```

```csharp
public static IQbservable<TSource> Dematerialize<TSource>(
    this IQbservable<Notification<TSource>> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Dematerialize(
    IQbservable<Notification<TSource>^>^ source
)
```

```fsharp
static member Dematerialize : 
        source:IQbservable<Notification<'TSource>> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Notification](Notification\Notification(T).md)\<TSource\>\>  
  A queryable observable sequence containing explicit notification values which have to be turned into implicit notifications.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
A queryable observable sequence exhibiting the behavior corresponding to the source sequence's notification values.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Notification](Notification\Notification(T).md)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








