title: CompositeDisposable.Count Property
---
# CompositeDisposable.Count Property

Gets the number of disposables contained in the CompositeDisposable.

**Namespace:**  [System.Reactive.Disposables](System.Reactive.Disposables/System.Reactive.Disposables)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public ReadOnly Property Count As Integer
    Get
```

```vb
'Usage
Dim instance As CompositeDisposable
Dim value As Integer

value = instance.Count
```

```csharp
public int Count { get; }
```

```c++
public:
virtual property int Count {
    int get () sealed;
}
```

```fsharp
abstract Count : int
override Count : int
```

```jscript
final function get Count () : int
```

#### Property Value

Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
The number of disposables contained in the CompositeDisposable.

#### Implements

[ICollection\<T\>.Count](https://msdn.microsoft.com/en-us/library/5s3kzhec)

## See Also

#### Reference

[CompositeDisposable Class](CompositeDisposable/CompositeDisposable)

[System.Reactive.Disposables Namespace](System.Reactive.Disposables/System.Reactive.Disposables)






