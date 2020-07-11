title: Subject.Synchronize<TSource, TResult>(ISubject<TSource, TResult>)
---
# Subject.Synchronize\<TSource, TResult\> Method (ISubject\<TSource, TResult\>)

Synchronizes the messages on the subject.

**Namespace:**  [System.Reactive.Subjects](System.Reactive.Subjects/System.Reactive.Subjects)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Synchronize(Of TSource, TResult) ( _
    subject As ISubject(Of TSource, TResult) _
) As ISubject(Of TSource, TResult)
```

```vb
'Usage
Dim subject As ISubject(Of TSource, TResult)
Dim returnValue As ISubject(Of TSource, TResult)

returnValue = Subject.Synchronize(subject)
```

```csharp
public static ISubject<TSource, TResult> Synchronize<TSource, TResult>(
    ISubject<TSource, TResult> subject
)
```

```c++
public:
generic<typename TSource, typename TResult>
static ISubject<TSource, TResult>^ Synchronize(
    ISubject<TSource, TResult>^ subject
)
```

```fsharp
static member Synchronize : 
        subject:ISubject<'TSource, 'TResult> -> ISubject<'TSource, 'TResult> 
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

- subject  
  Type: [System.Reactive.Subjects.ISubject](ISubject/ISubject(TSource,)\<TSource, TResult\>  
  The subject to synchronize.

#### Return Value

Type: [System.Reactive.Subjects.ISubject](ISubject/ISubject(TSource,)\<TSource, TResult\>  
Subject whose messages are synchronized.

## See Also

#### Reference

[Subject Class](Subject/Subject)

[Synchronize Overload](Synchronize/Subject.Synchronize)

[System.Reactive.Subjects Namespace](System.Reactive.Subjects/System.Reactive.Subjects)

# Subject.Synchronize\<TSource, TResult\> Method (ISubject\<TSource, TResult\>, IScheduler)

Synchronizes the messages on the subject and notifies observers on the specified scheduler.

**Namespace:**  [System.Reactive.Subjects](System.Reactive.Subjects/System.Reactive.Subjects)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Synchronize(Of TSource, TResult) ( _
    subject As ISubject(Of TSource, TResult), _
    scheduler As IScheduler _
) As ISubject(Of TSource, TResult)
```

```vb
'Usage
Dim subject As ISubject(Of TSource, TResult)
Dim scheduler As IScheduler
Dim returnValue As ISubject(Of TSource, TResult)

returnValue = Subject.Synchronize(subject, _
    scheduler)
```

```csharp
public static ISubject<TSource, TResult> Synchronize<TSource, TResult>(
    ISubject<TSource, TResult> subject,
    IScheduler scheduler
)
```

```c++
public:
generic<typename TSource, typename TResult>
static ISubject<TSource, TResult>^ Synchronize(
    ISubject<TSource, TResult>^ subject, 
    IScheduler^ scheduler
)
```

```fsharp
static member Synchronize : 
        subject:ISubject<'TSource, 'TResult> * 
        scheduler:IScheduler -> ISubject<'TSource, 'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

- TResult  
  Type of result.

#### Parameters

- subject  
  Type: [System.Reactive.Subjects.ISubject](ISubject/ISubject(TSource,)\<TSource, TResult\>  
  The subject to synchronize.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler/IScheduler)  
  Scheduler to notify observers on.

#### Return Value

Type: [System.Reactive.Subjects.ISubject](ISubject/ISubject(TSource,)\<TSource, TResult\>  
Subject whose messages are synchronized and whose observers are notified on the given scheduler.

## See Also

#### Reference

[Subject Class](Subject/Subject)

[Synchronize Overload](Synchronize/Subject.Synchronize)

[System.Reactive.Subjects Namespace](System.Reactive.Subjects/System.Reactive.Subjects)
