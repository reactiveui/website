title: MockEnumerable<T>.IEnumerable.GetEnumerator()
---
# MockEnumerable\<T\>.IEnumerable.GetEnumerator Method

**Namespace:**  [ReactiveTests](ReactiveTests\ReactiveTests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Private Function GetEnumerator As IEnumerator
    Implements IEnumerable.GetEnumerator
```

```vb
'Usage
Dim instance As MockEnumerable
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

#### Implements

[IEnumerable.GetEnumerator()](https://msdn.microsoft.com/en-us/library/5zae5365)

## See Also

#### Reference

[MockEnumerable\<T\> Class](MockEnumerable\MockEnumerable(T).md)

[ReactiveTests Namespace](ReactiveTests\ReactiveTests.md)






