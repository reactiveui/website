# Notification\<T\>.Accept Method

Include Protected Members  
Include Inherited Members

Invokes the delegate corresponding to the notification.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Accept(IObserver<T>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.accept(system.iobserver%7b%600%7d)(v=VS.103))Invokes the observer's method corresponding to the notification.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Accept(Action<T>, Action<Exception>, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.accept(system.action%7b%600%7d%2csystem.action%7bsystem.exception%7d%2csystem.action)(v=VS.103))Invokes the delegate corresponding to the notification.![Public method](images\Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Accept<TResult>(Func<T, TResult>, Func<Exception, TResult>, Func<TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.accept%60%601(system.func%7b%600%2c%60%600%7d%2csystem.func%7bsystem.exception%2c%60%600%7d%2csystem.func%7b%60%600%7d)(v=VS.103))Invokes the delegate corresponding to the notification and returns the produced result.Top

## See Also

#### Reference

[Notification\<T\> Class](Notification\Notification(T).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)


# Notification\<T\>.Accept Method (IObserver\<T\>)

Invokes the observer's method corresponding to the notification.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public MustOverride Sub Accept ( _
    observer As IObserver(Of T) _
)
```

```vb
'Usage
Dim instance As Notification
Dim observer As IObserver(Of T)

instance.Accept(observer)
```

```csharp
public abstract void Accept(
    IObserver<T> observer
)
```

```c++
public:
virtual void Accept(
    IObserver<T>^ observer
) abstract
```

```fsharp
abstract Accept : 
        observer:IObserver<'T> -> unit 
```

```jscript
public abstract function Accept(
    observer : IObserver<T>
)
```

#### Parameters

- observer  
  Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<[T](Notification\Notification(T).md)\>  
  The observer to invoke the notification on.

## See Also

#### Reference

[Notification\<T\> Class](Notification\Notification(T).md)

[Accept Overload](Accept\Notification(T).Accept.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)

# Notification\<T\>.Accept Method (Action\<T\>, Action\<Exception\>, Action)

Invokes the delegate corresponding to the notification.

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public MustOverride Sub Accept ( _
    onNext As Action(Of T), _
    onError As Action(Of Exception), _
    onCompleted As Action _
)
```

```vb
'Usage
Dim instance As Notification
Dim onNext As Action(Of T)
Dim onError As Action(Of Exception)
Dim onCompleted As Action

instance.Accept(onNext, onError, onCompleted)
```

```csharp
public abstract void Accept(
    Action<T> onNext,
    Action<Exception> onError,
    Action onCompleted
)
```

```c++
public:
virtual void Accept(
    Action<T>^ onNext, 
    Action<Exception^>^ onError, 
    Action^ onCompleted
) abstract
```

```fsharp
abstract Accept : 
        onNext:Action<'T> * 
        onError:Action<Exception> * 
        onCompleted:Action -> unit 
```

```jscript
public abstract function Accept(
    onNext : Action<T>, 
    onError : Action<Exception>, 
    onCompleted : Action
)
```

#### Parameters

- onNext  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[T](Notification\Notification(T).md)\>  
  The delegate to invoke for an OnNext notification.

- onError  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)\>  
  The delegate to invoke for an OnError notification.

- onCompleted  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb534741)  
  The delegate to invoke for an OnCompleted notification.

## See Also

#### Reference

[Notification\<T\> Class](Notification\Notification(T).md)

[Accept Overload](Accept\Notification(T).Accept.md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)





