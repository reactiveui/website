title: Observer.ToNotifier<T>()
---
# Observer.ToNotifier\<T\> Method

Creates a notification callback from an observer.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToNotifier(Of T) ( _
    observer As IObserver(Of T) _
) As Action(Of Notification(Of T))
```

```vb
'Usage
Dim observer As IObserver(Of T)
Dim returnValue As Action(Of Notification(Of T))

returnValue = observer.ToNotifier()
```

```csharp
public static Action<Notification<T>> ToNotifier<T>(
    this IObserver<T> observer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T>
static Action<Notification<T>^>^ ToNotifier(
    IObserver<T>^ observer
)
```

```fsharp
static member ToNotifier : 
        observer:IObserver<'T> -> Action<Notification<'T>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T  
  The observer argument type.

#### Parameters

- observer  
  Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<T\>  
  The observer object.

#### Return Value

Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Notification](Notification/Notification(T))\<T\>\>  
The action that forwards its input notification to the underlying observer.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<T\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observer Class](Observer/Observer)

[System.Reactive Namespace](System.Reactive/System.Reactive)
