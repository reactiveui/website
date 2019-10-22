title: Observable.Select<TSource, TResult>(IObservable<TSource>, Func<TSource, Int32, TResult>)
---
# Observable.Select\<TSource, TResult\> Method (IObservable\<TSource\>, Func\<TSource, Int32, TResult\>)

Projects each element of an observable sequence into a new form by incorporating the element’s index with the specified source and selector.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Select(Of TSource, TResult) ( _
    source As IObservable(Of TSource), _
    selector As Func(Of TSource, Integer, TResult) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim selector As Func(Of TSource, Integer, TResult)
Dim returnValue As IObservable(Of TResult)

returnValue = source.Select(selector)
```

```csharp
public static IObservable<TResult> Select<TSource, TResult>(
    this IObservable<TSource> source,
    Func<TSource, int, TResult> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IObservable<TResult>^ Select(
    IObservable<TSource>^ source, 
    Func<TSource, int, TResult>^ selector
)
```

```fsharp
static member Select : 
        source:IObservable<'TSource> * 
        selector:Func<'TSource, int, 'TResult> -> IObservable<'TResult> 
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
  A sequence of elements to invoke a transform function on.

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TSource, [Int32](https://msdn.microsoft.com/en-us/library/td2s409d), TResult\>  
  A transform function to apply to each source element.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence into a new form.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Select Overload](Select\Observable.Select.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Select\<TSource, TResult\> Method (IObservable\<TSource\>, Func\<TSource, TResult\>)

Projects each element of an observable sequence into a new form with the specified source and selector.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Select(Of TSource, TResult) ( _
    source As IObservable(Of TSource), _
    selector As Func(Of TSource, TResult) _
) As IObservable(Of TResult)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim selector As Func(Of TSource, TResult)
Dim returnValue As IObservable(Of TResult)

returnValue = source.Select(selector)
```

```csharp
public static IObservable<TResult> Select<TSource, TResult>(
    this IObservable<TSource> source,
    Func<TSource, TResult> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IObservable<TResult>^ Select(
    IObservable<TSource>^ source, 
    Func<TSource, TResult>^ selector
)
```

```fsharp
static member Select : 
        source:IObservable<'TSource> * 
        selector:Func<'TSource, 'TResult> -> IObservable<'TResult> 
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
  A sequence of elements to invoke a transform function on.

- selector  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TResult\>  
  A transform function to apply to each source element.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TResult\>  
An observable sequence into a new form.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Select Overload](Select\Observable.Select.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
