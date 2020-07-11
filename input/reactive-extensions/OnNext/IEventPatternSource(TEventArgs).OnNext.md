title: IEventPatternSource<TEventArgs>.OnNext Event
---
# IEventPatternSource\<TEventArgs\>.OnNext Event

Raises the Next event signaling the next element in the data stream.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Event OnNext As EventHandler(Of TEventArgs)
```

```vb
'Usage
Dim instance As IEventPatternSource
Dim handler As EventHandler(Of TEventArgs)

AddHandler instance.OnNext, handler
```

```csharp
event EventHandler<TEventArgs> OnNext
```

```c++
 event EventHandler<TEventArgs>^ OnNext {
    void add (EventHandler<TEventArgs>^ value);
    void remove (EventHandler<TEventArgs>^ value);
}
```

```fsharp
abstract OnNext : IEvent<EventHandler<'TEventArgs>,
    'TEventArgs>
```

```jscript
JScript supports the use of events, but not the declaration of new ones.
```

## See Also

#### Reference

[IEventPatternSource\<TEventArgs\> Interface](IEventPatternSource/IEventPatternSource(TEventArgs))

[System.Reactive Namespace](System.Reactive/System.Reactive)




