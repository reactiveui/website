# Observable.Never\<TResult\> Method

Returns a non-terminating observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Never(Of TResult) As IObservable(Of TResult)
```

```vb
'Usage
Dim returnValue As IObservable(Of TResult)

returnValue = Observable.Never()
```

```csharp
public static IObservable<TResult> Never<TResult>()
```

```c++
public:
generic<typename TResult>
static IObservable<TResult>^ Never()
```

```fsharp
static member Never : unit -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
A non-terminating observable sequence.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)






