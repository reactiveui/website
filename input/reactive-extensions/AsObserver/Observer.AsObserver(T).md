title: Observer.AsObserver<T>()
---
# Observer.AsObserver\<T\> Method

Hides the identity of an observer.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function AsObserver(Of T) ( _
    observer As IObserver(Of T) _
) As IObserver(Of T)
```

```vb
'Usage
Dim observer As IObserver(Of T)
Dim returnValue As IObserver(Of T)

returnValue = observer.AsObserver()
```

```csharp
public static IObserver<T> AsObserver<T>(
    this IObserver<T> observer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T>
static IObserver<T>^ AsObserver(
    IObserver<T>^ observer
)
```

```fsharp
static member AsObserver : 
        observer:IObserver<'T> -> IObserver<'T> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

#### Parameters

- observer  
  Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<T\>  
  An observer whose identity to hide.

#### Return Value

Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<T\>  
An observer that hides the identity of the specified observer.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<T\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observer Class](Observer/Observer)

[System.Reactive Namespace](System.Reactive/System.Reactive)
