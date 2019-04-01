# Observer.ToObserver\<T\> Method

Creates an observer from a notification callback.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToObserver(Of T) ( _
    handler As Action(Of Notification(Of T)) _
) As IObserver(Of T)
```

```vb
'Usage
Dim handler As Action(Of Notification(Of T))
Dim returnValue As IObserver(Of T)

returnValue = handler.ToObserver()
```

```csharp
public static IObserver<T> ToObserver<T>(
    this Action<Notification<T>> handler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T>
static IObserver<T>^ ToObserver(
    Action<Notification<T>^>^ handler
)
```

```fsharp
static member ToObserver : 
        handler:Action<Notification<'T>> -> IObserver<'T> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T  
  The observer argument type.

#### Parameters

- handler  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Notification](Notification\Notification(T).md)\<T\>\>  
  The action that handles a notification.

#### Return Value

Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<T\>  
The observer object that invokes the specified handler using a notification corresponding to each message it receives.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Notification](Notification\Notification(T).md)\<T\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observer Class](Observer\Observer.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)
