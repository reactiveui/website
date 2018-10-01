# AsyncSubject\<T\>.OnCompleted Method

Notifies all subscribed observers of the end of the sequence, also causing the last received value to be sent out (if any).

**Namespace:**  [System.Reactive.Subjects](System.Reactive.Subjects\System.Reactive.Subjects.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub OnCompleted
```

```vb
'Usage
Dim instance As AsyncSubject

instance.OnCompleted()
```

```csharp
public void OnCompleted()
```

```c++
public:
virtual void OnCompleted() sealed
```

```fsharp
abstract OnCompleted : unit -> unit 
override OnCompleted : unit -> unit 
```

```jscript
public final function OnCompleted()
```

#### Implements

[IObserver\<T\>.OnCompleted()](https://msdn.microsoft.com/en-us/library/Dd782982)

## See Also

#### Reference

[AsyncSubject\<T\> Class](AsyncSubject\AsyncSubject(T).md)

[System.Reactive.Subjects Namespace](System.Reactive.Subjects\System.Reactive.Subjects.md)