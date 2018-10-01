# MockEnumerable\<T\>.GetEnumerator Method

**Namespace:**  [ReactiveTests](ReactiveTests\ReactiveTests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function GetEnumerator As IEnumerator(Of T)
```

```vb
'Usage
Dim instance As MockEnumerable
Dim returnValue As IEnumerator(Of T)

returnValue = instance.GetEnumerator()
```

```csharp
public IEnumerator<T> GetEnumerator()
```

```c++
public:
virtual IEnumerator<T>^ GetEnumerator() sealed
```

```fsharp
abstract GetEnumerator : unit -> IEnumerator<'T> 
override GetEnumerator : unit -> IEnumerator<'T> 
```

```jscript
public final function GetEnumerator() : IEnumerator<T>
```

#### Return Value

Type: [System.Collections.Generic.IEnumerator](https://msdn.microsoft.com/en-us/library/78dfe2yb)\<[T](MockEnumerable\MockEnumerable(T).md)\>

#### Implements

[IEnumerable\<T\>.GetEnumerator()](https://msdn.microsoft.com/en-us/library/s793z9y2)

## See Also

#### Reference

[MockEnumerable\<T\> Class](MockEnumerable\MockEnumerable(T).md)

[ReactiveTests Namespace](ReactiveTests\ReactiveTests.md)






