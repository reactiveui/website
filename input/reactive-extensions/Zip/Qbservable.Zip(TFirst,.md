title: Qbservable.Zip<TFirst, TSecond, TResult>(IQbservable<TFirst>, IEnumerable<TSecond>, Expression<Func<TFirst, TSecond, TResult>>)
---
# Qbservable.Zip\<TFirst, TSecond, TResult\> Method (IQbservable\<TFirst\>, IEnumerable\<TSecond\>, Expression\<Func\<TFirst, TSecond, TResult\>\>)

Merges a queryable observable sequence and an enumerable sequence into one queryable observable sequence by using the selector function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Zip(Of TFirst, TSecond, TResult) ( _
    first As IQbservable(Of TFirst), _
    second As IEnumerable(Of TSecond), _
    resultSelector As Expression(Of Func(Of TFirst, TSecond, TResult)) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim first As IQbservable(Of TFirst)
Dim second As IEnumerable(Of TSecond)
Dim resultSelector As Expression(Of Func(Of TFirst, TSecond, TResult))
Dim returnValue As IQbservable(Of TResult)

returnValue = first.Zip(second, resultSelector)
```

```csharp
public static IQbservable<TResult> Zip<TFirst, TSecond, TResult>(
    this IQbservable<TFirst> first,
    IEnumerable<TSecond> second,
    Expression<Func<TFirst, TSecond, TResult>> resultSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TFirst, typename TSecond, typename TResult>
static IQbservable<TResult>^ Zip(
    IQbservable<TFirst>^ first, 
    IEnumerable<TSecond>^ second, 
    Expression<Func<TFirst, TSecond, TResult>^>^ resultSelector
)
```

```fsharp
static member Zip : 
        first:IQbservable<'TFirst> * 
        second:IEnumerable<'TSecond> * 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TFirst\>  
  The first observable source.

- second  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TSecond\>  
  The second enumerable source.

- resultSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TFirst, TSecond, TResult\>\>  
  The function to invoke for each consecutive pair of elements from the first and second source.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>  
A queryable observable sequence containing the result of pairwise combining the elements of the first and second source using the specified result selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TFirst\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Zip Overload](Zip\Qbservable.Zip.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.Zip\<TFirst, TSecond, TResult\> Method (IQbservable\<TFirst\>, IObservable\<TSecond\>, Expression\<Func\<TFirst, TSecond, TResult\>\>)

Merges two queryable observable sequences into one queryable observable sequence by combining their elements in a pairwise fashion.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Zip(Of TFirst, TSecond, TResult) ( _
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

returnValue = first.Zip(second, resultSelector)
```

```csharp
public static IQbservable<TResult> Zip<TFirst, TSecond, TResult>(
    this IQbservable<TFirst> first,
    IObservable<TSecond> second,
    Expression<Func<TFirst, TSecond, TResult>> resultSelector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TFirst, typename TSecond, typename TResult>
static IQbservable<TResult>^ Zip(
    IQbservable<TFirst>^ first, 
    IObservable<TSecond>^ second, 
    Expression<Func<TFirst, TSecond, TResult>^>^ resultSelector
)
```

```fsharp
static member Zip : 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TFirst\>  
  The first observable source.

- second  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSecond\>  
  The second observable source.

- resultSelector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TFirst, TSecond, TResult\>\>  
  The function to invoke for each consecutive pair of elements from the first and second source.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>  
A queryable observable sequence containing the result of pairwise combining the elements of the first and second source using the specified result selector function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TFirst\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Zip Overload](Zip\Qbservable.Zip.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
