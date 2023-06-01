title: NullErrorObservable<T>.Subscribe()
---
# NullErrorObservable\<T\>.Subscribe Method

**Namespace:**  [ReactiveTests](ReactiveTests/ReactiveTests)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Subscribe ( _
    observer As IObserver(Of T) _
) As IDisposable
```

```vb
'Usage
Dim instance As NullErrorObservable
Dim observer As IObserver(Of T)
Dim returnValue As IDisposable

returnValue = instance.Subscribe(observer)
```

```csharp
public IDisposable Subscribe(
    IObserver<T> observer
)
```

```c++
public:
virtual IDisposable^ Subscribe(
    IObserver<T>^ observer
) sealed
```

```fsharp
abstract Subscribe : 
        observer:IObserver<'T> -> IDisposable 
override Subscribe : 
        observer:IObserver<'T> -> IDisposable 
```

```javascript
public final function Subscribe(
    observer : IObserver<T>
) : IDisposable
```

#### Parameters

- observer  
  Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<[T](NullErrorObservable/NullErrorObservable(T))\>

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)

#### Implements

[IObservable\<T\>.Subscribe(IObserver\<T\>)](https://msdn.microsoft.com/en-us/library/m:system.iobservable%601.subscribe(system.iobserver%7b%600%7d)(v=VS.103))

## See Also

#### Reference

[NullErrorObservable\<T\> Class](NullErrorObservable/NullErrorObservable(T))

[ReactiveTests Namespace](ReactiveTests/ReactiveTests)
