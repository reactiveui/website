title: Observable.ToEvent<TSource>(IObservable<TSource>)
---
# Observable.ToEvent\<TSource\> Method (IObservable\<TSource\>)

Exposes an observable sequence as an object with a .NET event with a specified source.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToEvent(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IEventSource(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IEventSource(Of TSource)

returnValue = source.ToEvent()
```

```csharp
public static IEventSource<TSource> ToEvent<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IEventSource<TSource>^ ToEvent(
    IObservable<TSource>^ source
)
```

```fsharp
static member ToEvent : 
        source:IObservable<'TSource> -> IEventSource<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The observable source sequence.

#### Return Value

Type: [System.Reactive.IEventSource](IEventSource/IEventSource(T))\<TSource\>  
The event source object.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[ToEvent Overload](ToEvent/Observable.ToEvent)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
