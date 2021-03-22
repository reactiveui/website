title: Qbservable.Materialize<TSource>()
---
# Qbservable.Materialize\<TSource\> Method

Materializes the implicit notifications of a queryable observable sequence as explicit notification values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Materialize(Of TSource) ( _
    source As IQbservable(Of TSource) _
) As IQbservable(Of Notification(Of TSource))
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim returnValue As IQbservable(Of Notification(Of TSource))

returnValue = source.Materialize()
```

```csharp
public static IQbservable<Notification<TSource>> Materialize<TSource>(
    this IQbservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<Notification<TSource>^>^ Materialize(
    IQbservable<TSource>^ source
)
```

```fsharp
static member Materialize : 
        source:IQbservable<'TSource> -> IQbservable<Notification<'TSource>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A queryable observable sequence to get notification values for.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<[Notification](Notification/Notification(T))\<TSource\>\>  
A queryable observable sequence containing the materialized notification values from the source sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








