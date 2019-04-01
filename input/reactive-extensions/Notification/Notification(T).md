# Notification\<T\> Class

Represents a notification to an observer.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Notification\<T\>

**Namespace:**  [System.Reactive](System.Reactive\System.Reactive.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<SerializableAttribute> _
Public MustInherit Class Notification(Of T) _
    Implements IEquatable(Of Notification(Of T))
```

```vb
'Usage
Dim instance As Notification(Of T)
```

```csharp
[SerializableAttribute]
public abstract class Notification<T> : IEquatable<Notification<T>>
```

```c++
[SerializableAttribute]
generic<typename T>
public ref class Notification abstract : IEquatable<Notification<T>^>
```

```fsharp
[<AbstractClassAttribute>]
[<SerializableAttribute>]
type Notification<'T> =  
    class
        interface IEquatable<Notification<'T>>
    end
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T  
  The notification argument type.

The Notification\<T\> type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Exception](Exception\Notification(T).Exception.md)Returns the exception of an OnError notification or returns null.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[HasValue](HasValue\Notification(T).HasValue.md)Returns a value that indicates whether the notification has a value.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Kind](Kind\Notification(T).Kind.md)Gets the kind of notification that is represented.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Value](Value\Notification(T).Value.md)Returns the value of an OnNext notification or throws an exception.Top

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Accept(IObserver<T>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.accept(system.iobserver%7b%600%7d)(v=VS.103))Invokes the observer's method corresponding to the notification.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Accept(Action<T>, Action<Exception>, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.accept(system.action%7b%600%7d%2csystem.action%7bsystem.exception%7d%2csystem.action)(v=VS.103))Invokes the delegate corresponding to the notification.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Accept<TResult>(Func<T, TResult>, Func<Exception, TResult>, Func<TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.accept%60%601(system.func%7b%600%2c%60%600%7d%2csystem.func%7bsystem.exception%2c%60%600%7d%2csystem.func%7b%60%600%7d)(v=VS.103))Invokes the delegate corresponding to the notification and returns the produced result.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.equals(system.object)(v=VS.103))Indicates whether this instance and a specified object are equal. (Overrides [Object.Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Notification<T>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.equals(system.reactive.notification%7b%600%7d)(v=VS.103))Indicates whether this instance and other are equal.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToObservable()](ToObservable\Notification(T).ToObservable.md)Returns an observable sequence with a single notification, using the immediate scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToObservable(IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.toobservable(system.reactive.concurrency.ischeduler)(v=VS.103))Returns an observable sequence with a single notification.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## Operators

NameDescription![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Equality](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.op_equality(system.reactive.notification%7b%600%7d%2csystem.reactive.notification%7b%600%7d)(v=VS.103))Indicates whether left and right arguments are equal.![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Inequality](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.op_inequality(system.reactive.notification%7b%600%7d%2csystem.reactive.notification%7b%600%7d)(v=VS.103))Indicates whether left and right arguments are not equal.Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive Namespace](System.Reactive\System.Reactive.md)











# Notification\<T\> Properties

Include Protected Members  
Include Inherited Members

The [Notification\<T\>](Notification\Notification(T).md) type exposes the following members.

## Properties

NameDescription![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Exception](Exception\Notification(T).Exception.md)Returns the exception of an OnError notification or returns null.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[HasValue](HasValue\Notification(T).HasValue.md)Returns a value that indicates whether the notification has a value.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Kind](Kind\Notification(T).Kind.md)Gets the kind of notification that is represented.![Public property](https://reactiveui.net/assets/img/Hh211972.pubproperty(en-us,VS.103).gif "Public property")[Value](Value\Notification(T).Value.md)Returns the value of an OnNext notification or throws an exception.Top

## See Also

#### Reference

[Notification\<T\> Class](Notification\Notification(T).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)





# Notification\<T\> Methods

Include Protected Members  
Include Inherited Members

The [Notification\<T\>](Notification\Notification(T).md) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Accept(IObserver<T>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.accept(system.iobserver%7b%600%7d)(v=VS.103))Invokes the observer's method corresponding to the notification.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Accept(Action<T>, Action<Exception>, Action)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.accept(system.action%7b%600%7d%2csystem.action%7bsystem.exception%7d%2csystem.action)(v=VS.103))Invokes the delegate corresponding to the notification.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Accept<TResult>(Func<T, TResult>, Func<Exception, TResult>, Func<TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.accept%60%601(system.func%7b%600%2c%60%600%7d%2csystem.func%7bsystem.exception%2c%60%600%7d%2csystem.func%7b%60%600%7d)(v=VS.103))Invokes the delegate corresponding to the notification and returns the produced result.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.equals(system.object)(v=VS.103))Indicates whether this instance and a specified object are equal. (Overrides [Object.Equals(Object)](https://msdn.microsoft.com/en-us/library/m:system.object.equals(system.object)(v=VS.103)).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[Equals(Notification<T>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.equals(system.reactive.notification%7b%600%7d)(v=VS.103))Indicates whether this instance and other are equal.![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[Finalize](https://msdn.microsoft.com/en-us/library/4k87zsw7)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetHashCode](https://msdn.microsoft.com/en-us/library/zdee4b3y)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[GetType](https://msdn.microsoft.com/en-us/library/dfwy45w9)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Protected method](https://reactiveui.net/assets/img/Hh303103.protmethod(en-us,VS.103).gif "Protected method")[MemberwiseClone](https://msdn.microsoft.com/en-us/library/57ctke0a)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToObservable()](ToObservable\Notification(T).ToObservable.md)Returns an observable sequence with a single notification, using the immediate scheduler.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToObservable(IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.toobservable(system.reactive.concurrency.ischeduler)(v=VS.103))Returns an observable sequence with a single notification.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")[ToString](https://msdn.microsoft.com/en-us/library/7bxwbwt2)(Inherited from [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b).)Top

## See Also

#### Reference

[Notification\<T\> Class](Notification\Notification(T).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)





# Notification\<T\> Operators

Include Protected Members  
Include Inherited Members

The [Notification\<T\>](Notification\Notification(T).md) type exposes the following members.

## Operators

NameDescription![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Equality](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.op_equality(system.reactive.notification%7b%600%7d%2csystem.reactive.notification%7b%600%7d)(v=VS.103))Indicates whether left and right arguments are equal.![Public operator](https://reactiveui.net/assets/img/Hh229204.puboperator(en-us,VS.103).gif "Public operator")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Inequality](https://msdn.microsoft.com/en-us/library/m:system.reactive.notification%601.op_inequality(system.reactive.notification%7b%600%7d%2csystem.reactive.notification%7b%600%7d)(v=VS.103))Indicates whether left and right arguments are not equal.Top

## See Also

#### Reference

[Notification\<T\> Class](Notification\Notification(T).md)

[System.Reactive Namespace](System.Reactive\System.Reactive.md)




