# AsyncSubject\<T\>.OnNext Method

Sends a value to the subject. The last value received before successful termination will be sent to all subscribed observers.

**Namespace:**  [System.Reactive.Subjects](System.Reactive.Subjects\System.Reactive.Subjects.md)  
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
Dim instance As AsyncSubject
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

```jscript
public final function OnNext(
    value : T
)
```

#### Parameters

- value  
  Type: [T](AsyncSubject\AsyncSubject(T).md)  
  The value to store in the subject.

#### Implements

[IObserver\<T\>.OnNext(T)](https://msdn.microsoft.com/en-us/library/m:system.iobserver%601.onnext(%600)(v=VS.103))

## See Also

#### Reference

[AsyncSubject\<T\> Class](AsyncSubject\AsyncSubject(T).md)

[System.Reactive.Subjects Namespace](System.Reactive.Subjects\System.Reactive.Subjects.md)






