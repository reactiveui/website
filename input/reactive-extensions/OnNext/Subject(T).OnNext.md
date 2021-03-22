title: Subject<T>.OnNext()
---
# Subject\<T\>.OnNext Method

Notifies all subscribed observers with the value.

**Namespace:**  [System.Reactive.Subjects](System.Reactive.Subjects/System.Reactive.Subjects)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub OnNext ( _
    value As T _
)
```

```vb
'Usage
Dim instance As Subject
Dim value As T

instance.OnNext(value)
```

```csharp
public void OnNext(
    T value
)
```

```c++
public:
virtual void OnNext(
    T value
) sealed
```

```fsharp
abstract OnNext : 
        value:'T -> unit 
override OnNext : 
        value:'T -> unit 
```

```javascript
public final function OnNext(
    value : T
)
```

#### Parameters

- value  
  Type: [T](Subject/Subject(T))  
  The value to send to all subscribed observers.

#### Implements

[IObserver\<T\>.OnNext(T)](https://msdn.microsoft.com/en-us/library/m:system.iobserver%601.onnext(%600)(v=VS.103))

## See Also

#### Reference

[Subject\<T\> Class](Subject/Subject(T))

[System.Reactive.Subjects Namespace](System.Reactive.Subjects/System.Reactive.Subjects)
