title: Qbservable.CombineLatest<TFirst, TSecond, TResult>()
---
# Qbservable.CombineLatest\<TFirst, TSecond, TResult\> Method

Merges two queryable observable sequences into one queryable observable sequence by using the selector function whenever one of the queryable observable sequences produces an element.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function CombineLatest(Of TFirst, TSecond, TResult) ( _
    first As IQbservable(Of TFirst), _
    second As IObservable(Of TSecond), _
    resultSelector As Expression(Of Func(Of TFirst, TSecond, TResult)) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim first As IQbservable(Of TFirst)
Dim second As IObservable(Of TSecond)
Dim resultSelector As Expression(Of Func(Of TFirst, TSecond, TResult))
Dim returnValue As IQbservable(Of TResult)

returnValue = first.CombineLatest(second, _
    resultSelector)
```

```csharp
public static IQbservable<TResult> CombineLatest<TFirst, TSecond, TResult>(
    this IQbservable<TFirst> first,
    IObservable<TSecond> second,
    Expression<Func<TFirst, TSecond, TResult>> resultSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TFirst, typename TSecond, typename TResult>
static IQbservable<TResult>^ CombineLatest(
    IQbservable<TFirst>^ first, 
    IObservable<TSecond>^ second, 
    Expression<Func<TFirst, TSecond, TResult>^>^ resultSelector
)
```

```fsharp
static member CombineLatest : 
        first:IQbservable<'TFirst> * 
        second:IObservable<'TSecond> * 
        resultSelector:Expression<Func<'TFirst, 'TSecond, 'TResult>> -> IQbservable<'TResult> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TFirst  
  The first type.

- TSecond  
  The second type.

- TResult  
  The type of result.

#### Parameters

- first  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TFirst\>  
  The first queryable observable source.

- second  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSecond\>  
  The second queryable observable source.

- resultSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TFirst, TSecond, TResult\>\>  
  The function to invoke whenever either of the sources produces an element.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence containing the result of combining elements of both sources using the specified result selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TFirst\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








