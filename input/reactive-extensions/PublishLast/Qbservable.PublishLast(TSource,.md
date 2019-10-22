title: Qbservable.PublishLast<TSource, TResult>()
---
# Qbservable.PublishLast\<TSource, TResult\> Method

Returns a queryable observable sequence that is the result of invoking the selector on a connectable queryable observable sequence that shares a single subscription to the underlying sequence containing only the last notification.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function PublishLast(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult))) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim selector As Expression(Of Func(Of IObservable(Of TSource), IObservable(Of TResult)))
Dim returnValue As IQbservable(Of TResult)

returnValue = source.PublishLast(selector)
```

```csharp
public static IQbservable<TResult> PublishLast<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<IObservable<TSource>, IObservable<TResult>>> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IQbservable<TResult>^ PublishLast(
    IQbservable<TSource>^ source, 
    Expression<Func<IObservable<TSource>^, IObservable<TResult>^>^>^ selector
)
```

```fsharp
static member PublishLast : 
        source:IQbservable<'TSource> * 
        selector:Expression<Func<IObservable<'TSource>, IObservable<'TResult>>> -> IQbservable<'TResult> 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence whose elements will be multicasted through a single shared subscription.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>\>\>  
  The selector function which can use the multicasted source sequence as many times as needed, without causing multiple subscriptions to the source sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>  
A queryable observable sequence that contains the elements of a sequence produced by multicasting the source sequence within a selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
