title: Subject()s
---
# Subject Methods

Include Protected Members  
Include Inherited Members

The [Subject](Subject\Subject.md) type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Create<TSource, TResult>](https://msdn.microsoft.com/en-us/library/m:system.reactive.subjects.subject.create%60%602(system.iobserver%7b%60%600%7d%2csystem.iobservable%7b%60%601%7d)(v=VS.103))Creates a subject from the specified observer and observable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Synchronize<TSource, TResult>(ISubject<TSource, TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.subjects.subject.synchronize%60%602(system.reactive.subjects.isubject%7b%60%600%2c%60%601%7d)(v=VS.103))Synchronizes the messages on the subject.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Synchronize<TSource, TResult>(ISubject<TSource, TResult>, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.subjects.subject.synchronize%60%602(system.reactive.subjects.isubject%7b%60%600%2c%60%601%7d%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Synchronizes the messages on the subject and notifies observers on the specified scheduler.Top

## See Also

#### Reference

[Subject Class](Subject\Subject.md)

[System.Reactive.Subjects Namespace](System.Reactive.Subjects\System.Reactive.Subjects.md)

# Subject Class

Provides a set of static methods for creating observers.

## Inheritance Hierarchy

[System.Object](https://msdn.microsoft.com/en-us/library/e5kfa45b)  
  System.Reactive.Subjects.Subject

**Namespace:**  [System.Reactive.Subjects](System.Reactive.Subjects\System.Reactive.Subjects.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public NotInheritable Class Subject
```

```vb
'Usage
```

```csharp
public static class Subject
```

```c++
public ref class Subject abstract sealed
```

```fsharp
[<AbstractClassAttribute>]
[<SealedAttribute>]
type Subject =  class end
```

```jscript
public final class Subject
```

The Subject type exposes the following members.

## Methods

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Create<TSource, TResult>](https://msdn.microsoft.com/en-us/library/m:system.reactive.subjects.subject.create%60%602(system.iobserver%7b%60%600%7d%2csystem.iobservable%7b%60%601%7d)(v=VS.103))Creates a subject from the specified observer and observable.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Synchronize<TSource, TResult>(ISubject<TSource, TResult>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.subjects.subject.synchronize%60%602(system.reactive.subjects.isubject%7b%60%600%2c%60%601%7d)(v=VS.103))Synchronizes the messages on the subject.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Synchronize<TSource, TResult>(ISubject<TSource, TResult>, IScheduler)](https://msdn.microsoft.com/en-us/library/m:system.reactive.subjects.subject.synchronize%60%602(system.reactive.subjects.isubject%7b%60%600%2c%60%601%7d%2csystem.reactive.concurrency.ischeduler)(v=VS.103))Synchronizes the messages on the subject and notifies observers on the specified scheduler.Top

## Thread Safety

Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## See Also

#### Reference

[System.Reactive.Subjects Namespace](System.Reactive.Subjects\System.Reactive.Subjects.md)
