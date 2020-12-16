title: Observable.Synchronize<TSource>(IObservable<TSource>, Object)
---
# Observable.Synchronize\<TSource\> Method (IObservable\<TSource\>, Object)

Synchronizes the observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Synchronize(Of TSource) ( _
    source As IObservable(Of TSource), _
    gate As Object _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim gate As Object
Dim returnValue As IObservable(Of TSource)

returnValue = source.Synchronize(gate)
```

```csharp
public static IObservable<TSource> Synchronize<TSource>(
    this IObservable<TSource> source,
    Object gate
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Synchronize(
    IObservable<TSource>^ source, 
    Object^ gate
)
```

```fsharp
static member Synchronize : 
        source:IObservable<'TSource> * 
        gate:Object -> IObservable<'TSource> 
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
  The source sequence.

- gate  
  Type: [System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  The gate object to synchronize each observer call on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The source sequence whose outgoing calls to observers are synchronized on the given gate object.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

This Synchronize method returns an observable sequence of the type TSource which synchronizes outgoing calls to the observer methods (OnNext, OnCompletion, OnError). This is accomplished by acquiring a mutual-exclusion [lock](https://go.microsoft.com/fwlink/?linkid=221631) for the object provided as the **gate** parameter.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Synchronize Overload](Synchronize/Observable.Synchronize)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.Synchronize\<TSource\> Method (IObservable\<TSource\>)

Synchronizes the observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Synchronize(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = source.Synchronize()
```

```csharp
public static IObservable<TSource> Synchronize<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Synchronize(
    IObservable<TSource>^ source
)
```

```fsharp
static member Synchronize : 
        source:IObservable<'TSource> -> IObservable<'TSource> 
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
  The source sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The source sequence whose outgoing calls to observers are synchronized.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

This Synchronize method returns an observable sequence of the type TSource which synchronizes outgoing calls to the observer methods (OnNext, OnCompletion, OnError). This is accomplished by acquiring a mutual-exclusion [lock](https://go.microsoft.com/fwlink/?linkid=221631) for a **gate** object. Another overload of the Synchronize method allows you to provide your own gate object: ([Synchronize\<TSource\>(IObservable\<TSource\>, Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.synchronize%60%601(system.iobservable%7b%60%600%7d%2csystem.object)(v=VS.103)).

This overload of the Synchronize method will create a new gate object for each subscription. Similar to the following:

    return Defer(() =>
    {
      var gate = new object();
      return Synchronize(gate);
    });

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Synchronize Overload](Synchronize/Observable.Synchronize)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
