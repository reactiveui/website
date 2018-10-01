# Qbservable.RefCount\<TSource\> Method

Returns a queryable observable sequence that stays connected to the source as long as there is at least one subscription to the queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function RefCount(Of TSource) ( _
    provider As IQbservableProvider, _
    source As IConnectableObservable(Of TSource) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim source As IConnectableObservable(Of TSource)
Dim returnValue As IQbservable(Of TSource)

returnValue = provider.RefCount(source)
```

```csharp
public static IQbservable<TSource> RefCount<TSource>(
    this IQbservableProvider provider,
    IConnectableObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ RefCount(
    IQbservableProvider^ provider, 
    IConnectableObservable<TSource>^ source
)
```

```fsharp
static member RefCount : 
        provider:IQbservableProvider * 
        source:IConnectableObservable<'TSource> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- source  
  Type: [System.Reactive.Subjects.IConnectableObservable](IConnectableObservable\IConnectableObservable(T).md)\<TSource\>  
  The connectable queryable observable sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
A queryable observable sequence that stays connected to the source.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
