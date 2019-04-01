# Observable.ToEvent Method

Include Protected Members  
Include Inherited Members

Exposes an observable sequence as an object with a .NET event.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToEvent<TSource>(IObservable<TSource>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.toevent%60%601(system.iobservable%7b%60%600%7d)(v=VS.103))Exposes an observable sequence as an object with a .NET event with a specified source.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[ToEvent(IObservable<Unit>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.toevent(system.iobservable%7bsystem.reactive.unit%7d)(v=VS.103))Exposes an observable sequence as an object with a .NET event with a specified source.Top

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.ToEvent Method (IObservable\<Unit\>)

Exposes an observable sequence as an object with a .NET event with a specified source.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToEvent ( _
    source As IObservable(Of Unit) _
) As IEventSource(Of Unit)
```

```vb
'Usage
Dim source As IObservable(Of Unit)
Dim returnValue As IEventSource(Of Unit)

returnValue = source.ToEvent()
```

```csharp
public static IEventSource<Unit> ToEvent(
    this IObservable<Unit> source
)
```

```c++
[ExtensionAttribute]
public:
static IEventSource<Unit>^ ToEvent(
    IObservable<Unit>^ source
)
```

```fsharp
static member ToEvent : 
        source:IObservable<Unit> -> IEventSource<Unit> 
```

```jscript
public static function ToEvent(
    source : IObservable<Unit>
) : IEventSource<Unit>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit\Unit.md)\>  
  The observable source sequence.

#### Return Value

Type: [System.Reactive.IEventSource](IEventSource\IEventSource(T).md)\<[Unit](Unit\Unit.md)\>  
The event source object.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit\Unit.md)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[ToEvent Overload](ToEvent\Observable.ToEvent.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
