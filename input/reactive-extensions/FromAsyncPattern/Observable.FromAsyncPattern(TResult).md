title: Observable.FromAsyncPattern<TResult>(Func<AsyncCallback, Object, IAsyncResult>, Func<IAsyncResult, TResult>)
---
# Observable.FromAsyncPattern\<TResult\> Method (Func\<AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of TResult) ( _
    begin As Func(Of AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<IObservable<TResult>> FromAsyncPattern<TResult>(
    Func<AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename TResult>
static Func<IObservable<TResult>^>^ FromAsyncPattern(
    Func<AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<IObservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<[AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)







