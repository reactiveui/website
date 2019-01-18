# Observable.FromEventPattern Method (Object, String)

Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence, using reflection to find an instance event.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromEventPattern ( _
    target As Object, _
    eventName As String _
) As IObservable(Of EventPattern(Of EventArgs))
```

```vb
'Usage
Dim target As Object
Dim eventName As String
Dim returnValue As IObservable(Of EventPattern(Of EventArgs))

returnValue = Observable.FromEventPattern(target, _
    eventName)
```

```csharp
public static IObservable<EventPattern<EventArgs>> FromEventPattern(
    Object target,
    string eventName
)
```

```c++
public:
static IObservable<EventPattern<EventArgs^>^>^ FromEventPattern(
    Object^ target, 
    String^ eventName
)
```

```fsharp
static member FromEventPattern : 
        target:Object * 
        eventName:string -> IObservable<EventPattern<EventArgs>> 
```

```jscript
public static function FromEventPattern(
    target : Object, 
    eventName : String
) : IObservable<EventPattern<EventArgs>>
```

#### Parameters

- target  
  Type: [System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  The object instance that exposes the event to convert.

- eventName  
  Type: [System.String](https://msdn.microsoft.com/en-us/library/s1wwdcbf)  
  The name of the event to convert.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[EventPattern](EventPattern\EventPattern(TEventArgs).md)\<[EventArgs](https://msdn.microsoft.com/en-us/library/118wxtk3)\>\>  
The observable sequence that contains data representations of invocations of the underlying .NET event.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[FromEventPattern Overload](FromEventPattern\Observable.FromEventPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)







# Observable.FromEventPattern Method (Type, String)

Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence, using reflection to find a static event.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromEventPattern ( _
    type As Type, _
    eventName As String _
) As IObservable(Of EventPattern(Of EventArgs))
```

```vb
'Usage
Dim type As Type
Dim eventName As String
Dim returnValue As IObservable(Of EventPattern(Of EventArgs))

returnValue = Observable.FromEventPattern(type, _
    eventName)
```

```csharp
public static IObservable<EventPattern<EventArgs>> FromEventPattern(
    Type type,
    string eventName
)
```

```c++
public:
static IObservable<EventPattern<EventArgs^>^>^ FromEventPattern(
    Type^ type, 
    String^ eventName
)
```

```fsharp
static member FromEventPattern : 
        type:Type * 
        eventName:string -> IObservable<EventPattern<EventArgs>> 
```

```jscript
public static function FromEventPattern(
    type : Type, 
    eventName : String
) : IObservable<EventPattern<EventArgs>>
```

#### Parameters

- type  
  Type: [System.Type](https://msdn.microsoft.com/en-us/library/42892f65)  
  The type that exposes the static event to convert.

- eventName  
  Type: [System.String](https://msdn.microsoft.com/en-us/library/s1wwdcbf)  
  The name of the event to convert.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[EventPattern](EventPattern\EventPattern(TEventArgs).md)\<[EventArgs](https://msdn.microsoft.com/en-us/library/118wxtk3)\>\>  
The observable sequence that contains data representations of invocations of the underlying .NET event.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[FromEventPattern Overload](FromEventPattern\Observable.FromEventPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)







# Observable.FromEventPattern Method

Include Protected Members  
Include Inherited Members

Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEventPattern<TEventArgs>(Action<EventHandler<TEventArgs>>, Action<EventHandler<TEventArgs>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.fromeventpattern%60%601(system.action%7bsystem.eventhandler%7b%60%600%7d%7d%2csystem.action%7bsystem.eventhandler%7b%60%600%7d%7d)(v=VS.103))Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence with the specified add handler and remove handler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEventPattern(Action<EventHandler>, Action<EventHandler>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.fromeventpattern(system.action%7bsystem.eventhandler%7d%2csystem.action%7bsystem.eventhandler%7d)(v=VS.103))Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence with a specified add handler and remove handler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEventPattern<TDelegate, TEventArgs>(Action<TDelegate>, Action<TDelegate>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.fromeventpattern%60%602(system.action%7b%60%600%7d%2csystem.action%7b%60%600%7d)(v=VS.103))Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence with the specified add handler and remove handler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEventPattern(Object, String)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.fromeventpattern(system.object%2csystem.string)(v=VS.103))Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence, using reflection to find an instance event.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEventPattern<TEventArgs>(Object, String)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.fromeventpattern%60%601(system.object%2csystem.string)(v=VS.103))Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence, using reflection to find an instance event.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEventPattern(Type, String)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.fromeventpattern(system.type%2csystem.string)(v=VS.103))Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence, using reflection to find a static event.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEventPattern<TEventArgs>(Type, String)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.fromeventpattern%60%601(system.type%2csystem.string)(v=VS.103))Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence, using reflection to find a static event.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEventPattern<TDelegate, TEventArgs>(Func<EventHandler<TEventArgs>, TDelegate>, Action<TDelegate>, Action<TDelegate>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.observable.fromeventpattern%60%602(system.func%7bsystem.eventhandler%7b%60%601%7d%2c%60%600%7d%2csystem.action%7b%60%600%7d%2csystem.action%7b%60%600%7d)(v=VS.103))Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence with the specified conversion, add handler and remove handler.Top

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)





# Observable.FromEventPattern Method (Action\<EventHandler\>, Action\<EventHandler\>)

Converts a .NET event, conforming to the standard .NET event pattern, to an observable sequence with a specified add handler and remove handler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromEventPattern ( _
    addHandler As Action(Of EventHandler), _
    removeHandler As Action(Of EventHandler) _
) As IObservable(Of EventPattern(Of EventArgs))
```

```vb
'Usage
Dim addHandler As Action(Of EventHandler)
Dim removeHandler As Action(Of EventHandler)
Dim returnValue As IObservable(Of EventPattern(Of EventArgs))

returnValue = Observable.FromEventPattern(addHandler, _
    removeHandler)
```

```csharp
public static IObservable<EventPattern<EventArgs>> FromEventPattern(
    Action<EventHandler> addHandler,
    Action<EventHandler> removeHandler
)
```

```c++
public:
static IObservable<EventPattern<EventArgs^>^>^ FromEventPattern(
    Action<EventHandler^>^ addHandler, 
    Action<EventHandler^>^ removeHandler
)
```

```fsharp
static member FromEventPattern : 
        addHandler:Action<EventHandler> * 
        removeHandler:Action<EventHandler> -> IObservable<EventPattern<EventArgs>> 
```

```jscript
public static function FromEventPattern(
    addHandler : Action<EventHandler>, 
    removeHandler : Action<EventHandler>
) : IObservable<EventPattern<EventArgs>>
```

#### Parameters

- addHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[EventHandler](https://msdn.microsoft.com/en-us/library/xhb70ccc)\>  
  The action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[EventHandler](https://msdn.microsoft.com/en-us/library/xhb70ccc)\>  
  The action that detaches the given event handler from the underlying .NET event.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[EventPattern](EventPattern\EventPattern(TEventArgs).md)\<[EventArgs](https://msdn.microsoft.com/en-us/library/118wxtk3)\>\>  
The observable sequence that contains data representations of invocations of the underlying .NET event.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[FromEventPattern Overload](FromEventPattern\Observable.FromEventPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)






