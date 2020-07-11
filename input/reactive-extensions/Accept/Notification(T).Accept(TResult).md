title: Notification<T>.Accept<TResult>(Func<T, TResult>, Func<Exception, TResult>, Func<TResult>)
---
# Notification\<T\>.Accept\<TResult\> Method (Func\<T, TResult\>, Func\<Exception, TResult\>, Func\<TResult\>)

Invokes the delegate corresponding to the notification and returns the produced result.

**Namespace:**  [System.Reactive](System.Reactive/System.Reactive)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public MustOverride Function Accept(Of TResult) ( _
    onNext As Func(Of T, TResult), _
    onError As Func(Of Exception, TResult), _
    onCompleted As Func(Of TResult) _
) As TResult
```

```vb
'Usage
Dim instance As Notification
Dim onNext As Func(Of T, TResult)
Dim onError As Func(Of Exception, TResult)
Dim onCompleted As Func(Of TResult)
Dim returnValue As TResult

returnValue = instance.Accept(onNext, _
    onError, onCompleted)
```

```csharp
public abstract TResult Accept<TResult>(
    Func<T, TResult> onNext,
    Func<Exception, TResult> onError,
    Func<TResult> onCompleted
)
```

```c++
public:
generic<typename TResult>
virtual TResult Accept(
    Func<T, TResult>^ onNext, 
    Func<Exception^, TResult>^ onError, 
    Func<TResult>^ onCompleted
) abstract
```

```fsharp
abstract Accept : 
        onNext:Func<'T, 'TResult> * 
        onError:Func<Exception, 'TResult> * 
        onCompleted:Func<'TResult> -> 'TResult 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The result argument type.

#### Parameters

- onNext  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[T](Notification/Notification(T)), TResult\>  
  The delegate to invoke for an OnNext notification.

- onError  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[Exception](https://msdn.microsoft.com/en-us/library/c18k6c59), TResult\>  
  The delegate to invoke for an OnError notification.

- onCompleted  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<TResult\>  
  The delegate to invoke for an OnCompleted notification.

#### Return Value

Type: TResult  
The result produced by the observation..

## See Also

#### Reference

[Notification\<T\> Class](Notification/Notification(T))

[Accept Overload](Accept/Notification(T).Accept)

[System.Reactive Namespace](System.Reactive/System.Reactive)
