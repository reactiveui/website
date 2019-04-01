# IEventPatternSource\<TEventArgs\> Events

Include Protected Members  
Include Inherited Members

The [IEventPatternSource\<TEventArgs\>](IEventPatternSource\IEventPatternSource(TEventArgs).md) type exposes the following members.

## Events

NameDescription![Public event](https://reactiveui.net/assets/img/Hh315336.pubevent(en-us,VS.103).gif "Public event")[OnNext](OnNext\IEventPatternSource(TEventArgs).OnNext.md)Raises the Next event signaling the next element in the data stream.Top

## See Also

#### Reference

[IEventPatternSource\<TEventArgs\> Interface](IEventPatternSource\IEventPatternSource(TEventArgs).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)





# IEventPatternSource\<TEventArgs\> Interface

Represents a data stream signaling its elements by means of an event.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Interface IEventPatternSource(Of TEventArgs As EventArgs)
```

```vb
'Usage
Dim instance As IEventPatternSource(Of TEventArgs)
```

```csharp
public interface IEventPatternSource<TEventArgs>
where TEventArgs : EventArgs
```

```c++
generic<typename TEventArgs>
where TEventArgs : EventArgs
public interface class IEventPatternSource
```

```fsharp
type IEventPatternSource<'TEventArgs when 'TEventArgs : EventArgs> =  interface end
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TEventArgs  
  The event argument type.

The IEventPatternSource\<TEventArgs\> type exposes the following members.

## Events

NameDescription![Public event](https://reactiveui.net/assets/img/Hh315336.pubevent(en-us,VS.103).gif "Public event")[OnNext](OnNext\IEventPatternSource(TEventArgs).OnNext.md)Raises the Next event signaling the next element in the data stream.Top

## See Also

#### Reference

[System.Reactive Namespace](System.Reactive\System.Reactive.md)






