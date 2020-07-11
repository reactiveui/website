title: Qbservable.Multicast<TSource, TIntermediate, TResult>()
---
# Qbservable.Multicast\<TSource, TIntermediate, TResult\> Method

Returns a queryable observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Multicast(Of TSource, TIntermediate, TResult) ( _
    source As IQbservable(Of TSource), _
    subjectSelector As Expression(Of Func(Of ISubject(Of TSource, TIntermediate))), _
    selector As Expression(Of Func(Of IObservable(Of TIntermediate), IObservable(Of TResult))) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim subjectSelector As Expression(Of Func(Of ISubject(Of TSource, TIntermediate)))
Dim selector As Expression(Of Func(Of IObservable(Of TIntermediate), IObservable(Of TResult)))
Dim returnValue As IQbservable(Of TResult)

returnValue = source.Multicast(subjectSelector, _
    selector)
```

```csharp
public static IQbservable<TResult> Multicast<TSource, TIntermediate, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<ISubject<TSource, TIntermediate>>> subjectSelector,
    Expression<Func<IObservable<TIntermediate>, IObservable<TResult>>> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TIntermediate, typename TResult>
static IQbservable<TResult>^ Multicast(
    IQbservable<TSource>^ source, 
    Expression<Func<ISubject<TSource, TIntermediate>^>^>^ subjectSelector, 
    Expression<Func<IObservable<TIntermediate>^, IObservable<TResult>^>^>^ selector
)
```

```fsharp
static member Multicast : 
        source:IQbservable<'TSource> * 
        subjectSelector:Expression<Func<ISubject<'TSource, 'TIntermediate>>> * 
        selector:Expression<Func<IObservable<'TIntermediate>, IObservable<'TResult>>> -> IQbservable<'TResult> 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  The source sequence which will be multicast in the specified selector function.

- subjectSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[ISubject](ISubject/ISubject(TSource,)\<TSource, TIntermediate\>\>\>  
  The factory function to create an intermediate subject through which the source sequence’s elements will be multicast to the selector function.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TIntermediate\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>\>  
  The selector function which can use the multicasted source sequence subject to the policies enforced by the created subject.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








