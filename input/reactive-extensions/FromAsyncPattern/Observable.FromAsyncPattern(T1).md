# Observable.FromAsyncPattern\<T1\> Method (Func\<T1, AsyncCallback, Object, IAsyncResult\>, Action\<IAsyncResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1) ( _
    begin As Func(Of T1, AsyncCallback, Object, IAsyncResult), _
    end As Action(Of IAsyncResult) _
) As Func(Of T1, IObservable(Of Unit))
```

```vb
'Usage
Dim begin As Func(Of T1, AsyncCallback, Object, IAsyncResult)
Dim end As Action(Of IAsyncResult)
Dim returnValue As Func(Of T1, IObservable(Of Unit))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, IObservable<Unit>> FromAsyncPattern<T1>(
    Func<T1, AsyncCallback, Object, IAsyncResult> begin,
    Action<IAsyncResult> end
)
```

```c++
public:
generic<typename T1>
static Func<T1, IObservable<Unit>^>^ FromAsyncPattern(
    Func<T1, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Action<IAsyncResult^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, AsyncCallback, Object, IAsyncResult> * 
        end:Action<IAsyncResult> -> Func<'T1, IObservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<T1, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<T1, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit\Unit.md)\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Observable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)







