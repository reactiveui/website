title: CompositeDisposable.IEnumerable.GetEnumerator()
---
# CompositeDisposable.IEnumerable.GetEnumerator Method

Returns an enumerator that iterates through the CompositeDisposable.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables\System.Reactive.Disposables.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Private Function GetEnumerator As IEnumerator
    Implements IEnumerable.GetEnumerator
```

```vb
'Usage
Dim instance As CompositeDisposable
Dim returnValue As IEnumerator

returnValue = CType(instance, IEnumerable).GetEnumerator()
```

```csharp
IEnumerator IEnumerable.GetEnumerator()
```

```c++
private:
virtual IEnumerator^ GetEnumerator() sealed = IEnumerable::GetEnumerator
```

```fsharp
private abstract GetEnumerator : unit -> IEnumerator 
private override GetEnumerator : unit -> IEnumerator 
```

```jscript
JScript supports the use of explicit interface implementations, but not the declarations of new ones.
```

#### Return Value

Type: [System.Collections.IEnumerator](https://msdn.microsoft.com/en-us/library/1t2267t6)  
An enumerator to iterate over the disposables.

#### Implements

[IEnumerable.GetEnumerator()](https://msdn.microsoft.com/en-us/library/5zae5365)

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable\CompositeDisposable.md)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables\System.Reactive.Disposables.md)






