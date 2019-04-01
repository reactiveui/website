# CompositeDisposable.GetEnumerator Method

Returns an enumerator that iterates through the CompositeDisposable.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function GetEnumerator As IEnumerator(Of IDisposable)
```

```vb
'Usage
Dim instance As CompositeDisposable
Dim returnValue As IEnumerator(Of IDisposable)

returnValue = instance.GetEnumerator()
```

```csharp
public IEnumerator<IDisposable> GetEnumerator()
```

```c++
public:
virtual IEnumerator<IDisposable^>^ GetEnumerator() sealed
```

```fsharp
abstract GetEnumerator : unit -> IEnumerator<IDisposable> 
override GetEnumerator : unit -> IEnumerator<IDisposable> 
```

```jscript
public final function GetEnumerator() : IEnumerator<IDisposable>
```

#### Return Value

Type: [System.Collections.Generic.IEnumerator](https://msdn.microsoft.com/en-us/library/78dfe2yb)\<[IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\>  
An enumerator to iterate over the disposables.

#### Implements

[IEnumerable\<T\>.GetEnumerator()](https://msdn.microsoft.com/en-us/library/s793z9y2)

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable\CompositeDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)






