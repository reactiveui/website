# IEventSource\<T\>.OnNext Event

Raises the Next event signaling the next element in the data stream.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Event OnNext As Action(Of T)
```

```vb
'Usage
Dim instance As IEventSource
Dim handler As Action(Of T)

AddHandler instance.OnNext, handler
```

```csharp
event Action<T> OnNext
```

```c++
 event Action<T>^ OnNext {
    void add (Action<T>^ value);
    void remove (Action<T>^ value);
}
```

```fsharp
abstract OnNext : IEvent<Action<'T>,
    EventArgs>
```

```jscript
JScript supports the use of events, but not the declaration of new ones.
```

## See Also

#### Reference

[IEventSource\<T\> Interface](IEventSource\IEventSource(T).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)




