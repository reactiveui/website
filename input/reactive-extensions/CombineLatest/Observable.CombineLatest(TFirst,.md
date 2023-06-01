title: Observable.CombineLatest<TFirst, TSecond, TResult>()
---
# Observable.CombineLatest\<TFirst, TSecond, TResult\> Method

Merges two observable sequences into one observable sequence by using the selector function whenever one of the observable sequences produces an element.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function CombineLatest(Of TFirst, TSecond, TResult) ( _
    first As IObservable(Of TFirst), _
    second As IObservable(Of TSecond), _
    resultSelector As Func(Of TFirst, TSecond, TResult) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim first As IObservable(Of TFirst)
Dim second As IObservable(Of TSecond)
Dim resultSelector As Func(Of TFirst, TSecond, TResult)
Dim returnValue As IObservable(Of TResult)

returnValue = first.CombineLatest(second, _
    resultSelector)
```

```csharp
public static IObservable<TResult> CombineLatest<TFirst, TSecond, TResult>(
    this IObservable<TFirst> first,
    IObservable<TSecond> second,
    Func<TFirst, TSecond, TResult> resultSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TFirst, typename TSecond, typename TResult>
static IObservable<TResult>^ CombineLatest(
    IObservable<TFirst>^ first, 
    IObservable<TSecond>^ second, 
    Func<TFirst, TSecond, TResult>^ resultSelector
)
```

```fsharp
static member CombineLatest : 
        first:IObservable<'TFirst> * 
        second:IObservable<'TSecond> * 
        resultSelector:Func<'TFirst, 'TSecond, 'TResult> -> IObservable<'TResult> 
```

```javascript
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
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TFirst\>  
  The first observable source.

- second  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSecond\>  
  The second observable source.

- resultSelector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TFirst, TSecond, TResult\>  
  The function to invoke whenever either of the sources produces an element.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence containing the result of combining elements of both sources using the specified result selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TFirst\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








