# Observable.Multicast\<TSource, TIntermediate, TResult\> Method (IObservable\<TSource\>, Func\<ISubject\<TSource, TIntermediate\>\>, Func\<IObservable\<TIntermediate\>, IObservable\<TResult\>\>)

Returns an observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Multicast(Of TSource, TIntermediate, TResult) ( _
    source As IObservable(Of TSource), _
    subjectSelector As Func(Of ISubject(Of TSource, TIntermediate)), _
    selector As Func(Of IObservable(Of TIntermediate), IObservable(Of TResult)) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim subjectSelector As Func(Of ISubject(Of TSource, TIntermediate))
Dim selector As Func(Of IObservable(Of TIntermediate), IObservable(Of TResult))
Dim returnValue As IObservable(Of TResult)

returnValue = source.Multicast(subjectSelector, _
    selector)
```

```csharp
public static IObservable<TResult> Multicast<TSource, TIntermediate, TResult>(
    this IObservable<TSource> source,
    Func<ISubject<TSource, TIntermediate>> subjectSelector,
    Func<IObservable<TIntermediate>, IObservable<TResult>> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TIntermediate, typename TResult>
static IObservable<TResult>^ Multicast(
    IObservable<TSource>^ source, 
    Func<ISubject<TSource, TIntermediate>^>^ subjectSelector, 
    Func<IObservable<TIntermediate>^, IObservable<TResult>^>^ selector
)
```

```fsharp
static member Multicast : 
        source:IObservable<'TSource> * 
        subjectSelector:Func<ISubject<'TSource, 'TIntermediate>> * 
        selector:Func<IObservable<'TIntermediate>, IObservable<'TResult>> -> IObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TIntermediate  
  The type of intermediate.

- TResult  
  The type of result.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence which will be multicast in the specified selector function.

- subjectSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[ISubject](ISubject\ISubject(TSource,.md)\<TSource, TIntermediate\>\>  
  The factory function to create an intermediate subject through which the source sequence’s elements will be multicast to the selector function.

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TIntermediate\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>  
  The selector function which can use the multicasted source sequence subject to the policies enforced by the created subject.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Multicast Overload](Multicast\Observable.Multicast.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Multicast\<TSource, TResult\> Method (IObservable\<TSource\>, ISubject\<TSource, TResult\>)

Returns a connectable observable sequence that upon connection causes the source sequence to push results into the specified subject.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Multicast(Of TSource, TResult) ( _
    source As IObservable(Of TSource), _
    subject As ISubject(Of TSource, TResult) _
) As IConnectableObservable(Of TResult)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim subject As ISubject(Of TSource, TResult)
Dim returnValue As IConnectableObservable(Of TResult)

returnValue = source.Multicast(subject)
```

```csharp
public static IConnectableObservable<TResult> Multicast<TSource, TResult>(
    this IObservable<TSource> source,
    ISubject<TSource, TResult> subject
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IConnectableObservable<TResult>^ Multicast(
    IObservable<TSource>^ source, 
    ISubject<TSource, TResult>^ subject
)
```

```fsharp
static member Multicast : 
        source:IObservable<'TSource> * 
        subject:ISubject<'TSource, 'TResult> -> IConnectableObservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TResult  
  The type of result.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence whose elements will be pushed into the specified subject.

- subject  
  Type: [System.Reactive.Subjects.ISubject](ISubject\ISubject(TSource,.md)\<TSource, TResult\>  
  The subject to push source elements into.

#### Return Value

Type: [System.Reactive.Subjects.IConnectableObservable](IConnectableObservable\IConnectableObservable(T).md)\<TResult\>  
A connectable observable sequence that upon connection causes the source sequence to push results into the specified subject.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Multicast Overload](Multicast\Observable.Multicast.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








