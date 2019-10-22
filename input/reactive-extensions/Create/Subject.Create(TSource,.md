title: Subject.Create<TSource, TResult>()
---
# Subject.Create\<TSource, TResult\> Method

Creates a subject from the specified observer and observable.

**Namespace:**  [System.Reactive.Subjects](System.Reactive.Subjects\System.Reactive.Subjects.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Create(Of TSource, TResult) ( _
    observer As IObserver(Of TSource), _
    observable As IObservable(Of TResult) _
) As ISubject(Of TSource, TResult)
```

```vb
'Usage
Dim observer As IObserver(Of TSource)
Dim observable As IObservable(Of TResult)
Dim returnValue As ISubject(Of TSource, TResult)

returnValue = Subject.Create(observer, _
    observable)
```

```csharp
public static ISubject<TSource, TResult> Create<TSource, TResult>(
    IObserver<TSource> observer,
    IObservable<TResult> observable
)
```

```c++
public:
generic<typename TSource, typename TResult>
static ISubject<TSource, TResult>^ Create(
    IObserver<TSource>^ observer, 
    IObservable<TResult>^ observable
)
```

```fsharp
static member Create : 
        observer:IObserver<'TSource> * 
        observable:IObservable<'TResult> -> ISubject<'TSource, 'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TResult  
  The type of result.

#### Parameters

- observer  
  Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<TSource\>  
  The observer used to publish messages to the subject.

- observable  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
  The observable used to subscribe to messages sent from the subject.

#### Return Value

Type: [System.Reactive.Subjects.ISubject](ISubject\ISubject(TSource,.md)\<TSource, TResult\>  
Subject implemented using the given observer and observable.

## See Also

#### Reference

[Subject Class](Subject\Subject.md)

[System.Reactive.Subjects Namespace](System.Reactive.Subjects\System.Reactive.Subjects.md)







