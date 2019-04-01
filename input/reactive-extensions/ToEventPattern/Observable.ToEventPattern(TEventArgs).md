# Observable.ToEventPattern\<TEventArgs\> Method

Exposes an observable sequence as an object with a .NET event.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToEventPattern(Of TEventArgs As EventArgs) ( _
    source As IObservable(Of EventPattern(Of TEventArgs)) _
) As IEventPatternSource(Of TEventArgs)
```

```vb
'Usage
Dim source As IObservable(Of EventPattern(Of TEventArgs))
Dim returnValue As IEventPatternSource(Of TEventArgs)

returnValue = source.ToEventPattern()
```

```csharp
public static IEventPatternSource<TEventArgs> ToEventPattern<TEventArgs>(
    this IObservable<EventPattern<TEventArgs>> source
)
where TEventArgs : EventArgs
```

```c++
[ExtensionAttribute]
public:
generic<typename TEventArgs>
where TEventArgs : EventArgs
static IEventPatternSource<TEventArgs>^ ToEventPattern(
    IObservable<EventPattern<TEventArgs>^>^ source
)
```

```fsharp
static member ToEventPattern : 
        source:IObservable<EventPattern<'TEventArgs>> -> IEventPatternSource<'TEventArgs>  when 'TEventArgs : EventArgs
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TEventArgs  
  The type of event.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[EventPattern](EventPattern\EventPattern(TEventArgs).md)\<TEventArgs\>\>  
  The observable source sequence.

#### Return Value

Type: [System.Reactive.IEventPatternSource](IEventPatternSource\IEventPatternSource(TEventArgs).md)\<TEventArgs\>  
The event source object.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[EventPattern](EventPattern\EventPattern(TEventArgs).md)\<TEventArgs\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
