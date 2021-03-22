title: Observable.FromAsyncPattern<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult>, Func<IAsyncResult, TResult>)
---
# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, TResult\> Method (Func\<T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, TResult) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, TResult>(
    Func<T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, IObservable<TResult>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, IObservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<T1, T2, T3, T4, T5, T6, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<T1, T2, T3, T4, T5, T6, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<TResult>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, IObservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult\>, Action\<IAsyncResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult), _
    end As Action(Of IAsyncResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable(Of Unit))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult)
Dim end As Action(Of IAsyncResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable(Of Unit))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult> begin,
    Action<IAsyncResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<Unit>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Action<IAsyncResult^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, AsyncCallback, Object, IAsyncResult> * 
        end:Action<IAsyncResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, IObservable<Unit>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit/Unit)\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, TResult\> Method (Func\<T1, T2, T3, AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, TResult) ( _
    begin As Func(Of T1, T2, T3, AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of T1, T2, T3, IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of T1, T2, T3, IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, TResult>(
    Func<T1, T2, T3, AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename TResult>
static Func<T1, T2, T3, IObservable<TResult>^>^ FromAsyncPattern(
    Func<T1, T2, T3, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<'T1, 'T2, 'T3, IObservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<T1, T2, T3, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<T1, T2, T3, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult\>, Action\<IAsyncResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult), _
    end As Action(Of IAsyncResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IObservable(Of Unit))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult)
Dim end As Action(Of IAsyncResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IObservable(Of Unit))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult> begin,
    Action<IAsyncResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<Unit>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Action<IAsyncResult^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, AsyncCallback, Object, IAsyncResult> * 
        end:Action<IAsyncResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, IObservable<Unit>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<T1, T2, T3, T4, T5, T6, T7, T8, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<T1, T2, T3, T4, T5, T6, T7, T8, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit/Unit)\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<TResult>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, IObservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5\> Method (Func\<T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult\>, Action\<IAsyncResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5) ( _
    begin As Func(Of T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult), _
    end As Action(Of IAsyncResult) _
) As Func(Of T1, T2, T3, T4, T5, IObservable(Of Unit))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult)
Dim end As Action(Of IAsyncResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, IObservable(Of Unit))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5>(
    Func<T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult> begin,
    Action<IAsyncResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5>
static Func<T1, T2, T3, T4, T5, IObservable<Unit>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Action<IAsyncResult^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, AsyncCallback, Object, IAsyncResult> * 
        end:Action<IAsyncResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, IObservable<Unit>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<T1, T2, T3, T4, T5, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<T1, T2, T3, T4, T5, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit/Unit)\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, TResult\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, TResult) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, IObservable<TResult>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, IObservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<T1, T2, T3, T4, T5, T6, T7, T8, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<T1, T2, T3, T4, T5, T6, T7, T8, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<TResult>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, IObservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6\> Method (Func\<T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult\>, Action\<IAsyncResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult), _
    end As Action(Of IAsyncResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, IObservable(Of Unit))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult)
Dim end As Action(Of IAsyncResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, IObservable(Of Unit))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6>(
    Func<T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult> begin,
    Action<IAsyncResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6>
static Func<T1, T2, T3, T4, T5, T6, IObservable<Unit>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Action<IAsyncResult^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, AsyncCallback, Object, IAsyncResult> * 
        end:Action<IAsyncResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, IObservable<Unit>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<T1, T2, T3, T4, T5, T6, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<T1, T2, T3, T4, T5, T6, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit/Unit)\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult\>, Action\<IAsyncResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult), _
    end As Action(Of IAsyncResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable(Of Unit))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult)
Dim end As Action(Of IAsyncResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable(Of Unit))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult> begin,
    Action<IAsyncResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IObservable<Unit>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Action<IAsyncResult^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, AsyncCallback, Object, IAsyncResult> * 
        end:Action<IAsyncResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, IObservable<Unit>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit/Unit)\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<TResult>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, IObservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402868)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IObservable<TResult>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, IObservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, TResult\> Method (Func\<T1, T2, AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, TResult) ( _
    begin As Func(Of T1, T2, AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of T1, T2, IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of T1, T2, IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, IObservable<TResult>> FromAsyncPattern<T1, T2, TResult>(
    Func<T1, T2, AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename TResult>
static Func<T1, T2, IObservable<TResult>^>^ FromAsyncPattern(
    Func<T1, T2, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<'T1, 'T2, IObservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<T1, T2, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<T1, T2, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2\> Method (Func\<T1, T2, AsyncCallback, Object, IAsyncResult\>, Action\<IAsyncResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2) ( _
    begin As Func(Of T1, T2, AsyncCallback, Object, IAsyncResult), _
    end As Action(Of IAsyncResult) _
) As Func(Of T1, T2, IObservable(Of Unit))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, AsyncCallback, Object, IAsyncResult)
Dim end As Action(Of IAsyncResult)
Dim returnValue As Func(Of T1, T2, IObservable(Of Unit))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, IObservable<Unit>> FromAsyncPattern<T1, T2>(
    Func<T1, T2, AsyncCallback, Object, IAsyncResult> begin,
    Action<IAsyncResult> end
)
```

```c++
public:
generic<typename T1, typename T2>
static Func<T1, T2, IObservable<Unit>^>^ FromAsyncPattern(
    Func<T1, T2, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Action<IAsyncResult^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, AsyncCallback, Object, IAsyncResult> * 
        end:Action<IAsyncResult> -> Func<'T1, 'T2, IObservable<Unit>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<T1, T2, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<T1, T2, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit/Unit)\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, TResult\> Method (Func\<T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, TResult) ( _
    begin As Func(Of T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of T1, T2, T3, T4, T5, IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, TResult>(
    Func<T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename TResult>
static Func<T1, T2, T3, T4, T5, IObservable<TResult>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, IObservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<T1, T2, T3, T4, T5, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<T1, T2, T3, T4, T5, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, TResult\> Method (Func\<T1, AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, TResult) ( _
    begin As Func(Of T1, AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of T1, IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of T1, AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of T1, IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, IObservable<TResult>> FromAsyncPattern<T1, TResult>(
    Func<T1, AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename T1, typename TResult>
static Func<T1, IObservable<TResult>^>^ FromAsyncPattern(
    Func<T1, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<'T1, IObservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<T1, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<T1, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## Remarks

The FromAsyncPattern operator is used to simplify making asynchronous calls. It wraps an asynchronous call to begin/end invoke with an asynchronous function which handles the asynchronous call for you. The function returns an observable sequence which is of the same type as the result. For example, you could setup an asynchronous call to System.IO.Directory.GetFiles. The result of that method is a string array which contains the list of files requested. So the asynchronous function returned from the FromAsyncPattern operator would return an observable sequence of string\[\]. This is demonstrated in the example code for this topic. There are various overloads of this operator to handle method calls that take a different number of input parameters. To setup a asynchronous call specify the types with the call to the FromAsyncPattern operator. The final type specified is the return value type. For example, System.IO.Directory.GetFiles can take up to three input parameters and returns a string array as the result. The following code snippet shows the order of the types.

    delegate string[] GetFilesDelegate(string searchPath, string searchPattern, SearchOption searchOption);
    
    
    GetFilesDelegate getFiles = Directory.GetFiles;
    
    
    //**************************************************************************************************//
    //***  Observable.FromAsyncPattern<Param 1 Type, Param 2 Type, Param 3 Type, Return value type>  ***//
    //**************************************************************************************************//
    var getFileList = Observable.FromAsyncPattern<string, string, SearchOption, string[]>(getFiles.BeginInvoke, getFiles.EndInvoke);

## Examples

This example demonstrates calling System.IO.Direcotry.GetFile asynchronously to enumerate all files under the C:\\Program Files directory. The example uses the asynchronous function provided by the FromAsyncPattern operator. An action event handler acts as the callback for the asynchronous call to write each filename in the result to the console window.

    using System;
    using System.Reactive.Linq;
    using System.IO;
    
    namespace Example
    {                                       
      delegate string[] GetFilesDelegate(string searchPath, string searchPattern, SearchOption searchOption);
    
      class Program
      {
        static void Main()
        {                                                                                                                    
          //********************************************************************************************************************//
          //*** For this example, Reactive Extensions is used to wrap an asynchronous call that recursively enumerates files ***//
          //*** in a given directory.                                                                                        ***//
          //********************************************************************************************************************//
          string mySearchPath = "C:\\Program Files";                                                                                   
          GetFilesDelegate getFiles = Directory.GetFiles;
    
    
          //*****************************************************************************************************************************//
          //*** Reactive Extensions will wrap the asynchronous call to the delegate returning the asynchronous function, getFileList. ***//
          //*** Calling the asynchronous function returns the observable sequence of the string[].                                    ***//
          //***                                                                                                                       ***//
          //*** There are many overloaded versions of the FromAsyncPattern operator. The types signified here are based on parameters ***//
          //*** in the signature of actual method being called asynchronously. The types are specified in their proper order followed ***//
          //*** by the return type (ex. <Param 1 type, Param 2 type, Param 3 type, return type> ).                                    ***//
          //*****************************************************************************************************************************//
          var getFileList = Observable.FromAsyncPattern<string, string, SearchOption, string[]>(getFiles.BeginInvoke, getFiles.EndInvoke);    
          IObservable<string[]> fileObservable = getFileList(mySearchPath,"*.*",SearchOption.AllDirectories);
    
    
          //*********************************************************************************************************************//
          //*** We subscribe to this sequence with an action event handler defined with the lambda expression. It acts as the ***//
          //*** callback for completion of the asynchronous operation.                                                        ***//
          //*********************************************************************************************************************//
          fileObservable.Subscribe(fileList =>
          {
            foreach (string f in fileList)
            {
              Console.WriteLine(f.ToString());
            }
          });
    
    
          Console.WriteLine("Running async enumeration of the {0} directory.\n\nPress ENTER to cancel...\n",mySearchPath);
          Console.ReadLine();
        }
      }
    }

The following example output is generated by the example code.

    Running async enumeration of the C:\Program Files directory.
    
    Press ENTER to cancel...
    
    C:\Program Files\desktop.ini
    C:\Program Files\ATI\CIM\Bin64\atdcm64a.sys
    C:\Program Files\ATI\CIM\Bin64\ATILog.dll
    C:\Program Files\ATI\CIM\Bin64\ATIManifestDLMExt.dll
    C:\Program Files\ATI\CIM\Bin64\ATISetup.exe
    C:\Program Files\ATI\CIM\Bin64\CompressionDLMExt.dll
    C:\Program Files\ATI\CIM\Bin64\CRCVerDLMExt.dll
    C:\Program Files\ATI\CIM\Bin64\DetectionManager.dll
    C:\Program Files\ATI\CIM\Bin64\difxapi.dll
    C:\Program Files\ATI\CIM\Bin64\DLMCom.dll
    C:\Program Files\ATI\CIM\Bin64\EncryptionDLMExt.dll
    C:\Program Files\ATI\CIM\Bin64\InstallManager.dll
    C:\Program Files\ATI\CIM\Bin64\InstallManagerApp.exe
    C:\Program Files\ATI\CIM\Bin64\InstallManagerApp.exe.manifest
    C:\Program Files\ATI\CIM\Bin64\LanguageMgr.dll
    C:\Program Files\ATI\CIM\Bin64\mfc80u.dll
    C:\Program Files\ATI\CIM\Bin64\Microsoft.VC80.ATL.manifest
    C:\Program Files\ATI\CIM\Bin64\Microsoft.VC80.CRT.manifest
    C:\Program Files\ATI\CIM\Bin64\Microsoft.VC80.MFC.manifest
    C:\Program Files\ATI\CIM\Bin64\Microsoft.VC80.MFCLOC.manifest
    C:\Program Files\ATI\CIM\Bin64\Microsoft.VC80.OpenMP.manifest
    C:\Program Files\ATI\CIM\Bin64\msvcp80.dll
    C:\Program Files\ATI\CIM\Bin64\msvcr80.dll
    C:\Program Files\ATI\CIM\Bin64\PackageManager.dll
    C:\Program Files\ATI\CIM\Bin64\readme.rtf
    C:\Program Files\ATI\CIM\Bin64\SetACL64.exe

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)










# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult\>, Action\<IAsyncResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult), _
    end As Action(Of IAsyncResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable(Of Unit))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult)
Dim end As Action(Of IAsyncResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable(Of Unit))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult> begin,
    Action<IAsyncResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IObservable<Unit>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Action<IAsyncResult^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, AsyncCallback, Object, IAsyncResult> * 
        end:Action<IAsyncResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, IObservable<Unit>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402868)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit/Unit)\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4\> Method (Func\<T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult\>, Action\<IAsyncResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4) ( _
    begin As Func(Of T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult), _
    end As Action(Of IAsyncResult) _
) As Func(Of T1, T2, T3, T4, IObservable(Of Unit))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult)
Dim end As Action(Of IAsyncResult)
Dim returnValue As Func(Of T1, T2, T3, T4, IObservable(Of Unit))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4>(
    Func<T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult> begin,
    Action<IAsyncResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4>
static Func<T1, T2, T3, T4, IObservable<Unit>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Action<IAsyncResult^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, AsyncCallback, Object, IAsyncResult> * 
        end:Action<IAsyncResult> -> Func<'T1, 'T2, 'T3, 'T4, IObservable<Unit>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<T1, T2, T3, T4, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<T1, T2, T3, T4, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit/Unit)\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3\> Method (Func\<T1, T2, T3, AsyncCallback, Object, IAsyncResult\>, Action\<IAsyncResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3) ( _
    begin As Func(Of T1, T2, T3, AsyncCallback, Object, IAsyncResult), _
    end As Action(Of IAsyncResult) _
) As Func(Of T1, T2, T3, IObservable(Of Unit))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, AsyncCallback, Object, IAsyncResult)
Dim end As Action(Of IAsyncResult)
Dim returnValue As Func(Of T1, T2, T3, IObservable(Of Unit))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, IObservable<Unit>> FromAsyncPattern<T1, T2, T3>(
    Func<T1, T2, T3, AsyncCallback, Object, IAsyncResult> begin,
    Action<IAsyncResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3>
static Func<T1, T2, T3, IObservable<Unit>^>^ FromAsyncPattern(
    Func<T1, T2, T3, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Action<IAsyncResult^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, AsyncCallback, Object, IAsyncResult> * 
        end:Action<IAsyncResult> -> Func<'T1, 'T2, 'T3, IObservable<Unit>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<T1, T2, T3, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<T1, T2, T3, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit/Unit)\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, TResult\> Method (Func\<T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, TResult) ( _
    begin As Func(Of T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of T1, T2, T3, T4, IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of T1, T2, T3, T4, IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, TResult>(
    Func<T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename TResult>
static Func<T1, T2, T3, T4, IObservable<TResult>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<'T1, 'T2, 'T3, 'T4, IObservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<T1, T2, T3, T4, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<T1, T2, T3, T4, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult\>, Action\<IAsyncResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult), _
    end As Action(Of IAsyncResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable(Of Unit))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult)
Dim end As Action(Of IAsyncResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable(Of Unit))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult> begin,
    Action<IAsyncResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IObservable<Unit>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Action<IAsyncResult^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, AsyncCallback, Object, IAsyncResult> * 
        end:Action<IAsyncResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, IObservable<Unit>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit/Unit)\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, TResult\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, TResult) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<TResult>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, IObservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<T1, T2, T3, T4, T5, T6, T7, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<T1, T2, T3, T4, T5, T6, T7, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult\>, Action\<IAsyncResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult), _
    end As Action(Of IAsyncResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable(Of Unit))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult)
Dim end As Action(Of IAsyncResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable(Of Unit))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult> begin,
    Action<IAsyncResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IObservable<Unit>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Action<IAsyncResult^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, AsyncCallback, Object, IAsyncResult> * 
        end:Action<IAsyncResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, IObservable<Unit>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit/Unit)\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult\>, Action\<IAsyncResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult), _
    end As Action(Of IAsyncResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable(Of Unit))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult)
Dim end As Action(Of IAsyncResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable(Of Unit))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult> begin,
    Action<IAsyncResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<Unit>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Action<IAsyncResult^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, AsyncCallback, Object, IAsyncResult> * 
        end:Action<IAsyncResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, IObservable<Unit>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402862)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit/Unit)\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult\>, Func\<IAsyncResult, TResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult), _
    end As Func(Of IAsyncResult, TResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable(Of TResult))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult)
Dim end As Func(Of IAsyncResult, TResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable(Of TResult))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult> begin,
    Func<IAsyncResult, TResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IObservable<TResult>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Func<IAsyncResult^, TResult>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, AsyncCallback, Object, IAsyncResult> * 
        end:Func<IAsyncResult, 'TResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, IObservable<'TResult>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

- TResult  
  The type of result.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402862)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7\> Method (Func\<T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult\>, Action\<IAsyncResult\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7) ( _
    begin As Func(Of T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult), _
    end As Action(Of IAsyncResult) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, IObservable(Of Unit))
```

```vb
'Usage
Dim begin As Func(Of T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult)
Dim end As Action(Of IAsyncResult)
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, IObservable(Of Unit))

returnValue = Observable.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7>(
    Func<T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult> begin,
    Action<IAsyncResult> end
)
```

```c++
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7>
static Func<T1, T2, T3, T4, T5, T6, T7, IObservable<Unit>^>^ FromAsyncPattern(
    Func<T1, T2, T3, T4, T5, T6, T7, AsyncCallback^, Object^, IAsyncResult^>^ begin, 
    Action<IAsyncResult^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        begin:Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, AsyncCallback, Object, IAsyncResult> * 
        end:Action<IAsyncResult> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, IObservable<Unit>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

#### Parameters

- begin  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<T1, T2, T3, T4, T5, T6, T7, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The begin invoke function.

- end  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<T1, T2, T3, T4, T5, T6, T7, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Unit](Unit/Unit)\>\>  
A Begin/End invoke function pair.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[FromAsyncPattern Overload](FromAsyncPattern/Observable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)







