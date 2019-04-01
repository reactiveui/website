# Observable.Publish\<TSource\> Method (IObservable\<TSource\>)

Returns a connectable observable sequence that shares a single subscription to the underlying sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Publish(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IConnectableObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IConnectableObservable(Of TSource)

returnValue = source.Publish()
```

```csharp
public static IConnectableObservable<TSource> Publish<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IConnectableObservable<TSource>^ Publish(
    IObservable<TSource>^ source
)
```

```fsharp
static member Publish : 
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

Type: [System.Reactive.Subjects.IConnectableObservable](IConnectableObservable\IConnectableObservable(T).md)\<TSource\>  
A connectable observable sequence that shares a single subscription to the underlying sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Publish Overload](Publish\Observable.Publish.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Publish\<TSource\> Method (IObservable\<TSource\>, TSource)

Returns a connectable observable sequence that shares a single subscription to the underlying sequence and starts with initialValue.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Publish(Of TSource) ( _
    source As IObservable(Of TSource), _
    initialValue As TSource _
) As IConnectableObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim initialValue As TSource
Dim returnValue As IConnectableObservable(Of TSource)

returnValue = source.Publish(initialValue)
```

```csharp
public static IConnectableObservable<TSource> Publish<TSource>(
    this IObservable<TSource> source,
    TSource initialValue
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IConnectableObservable<TSource>^ Publish(
    IObservable<TSource>^ source, 
    TSource initialValue
)
```

```fsharp
static member Publish : 
        source:IObservable<'TSource> * 
        initialValue:'TSource -> IConnectableObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

- initialValue  
  Type: TSource  
  The selector function which can use the multicasted source sequence as many times as needed, without causing multiple subscriptions to the source sequence.

#### Return Value

Type: [System.Reactive.Subjects.IConnectableObservable](IConnectableObservable\IConnectableObservable(T).md)\<TSource\>  
The connectable observable sequence that shares a single subscription to the underlying sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Publish Overload](Publish\Observable.Publish.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
