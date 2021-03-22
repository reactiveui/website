title: Observable.TakeWhile<TSource>(IObservable<TSource>, Func<TSource, Boolean>)
---
# Observable.TakeWhile\<TSource\> Method (IObservable\<TSource\>, Func\<TSource, Boolean\>)

Returns values from an observable sequence as long as a specified condition is true, and then skips the remaining values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function TakeWhile(Of TSource) ( _
    source As IObservable(Of TSource), _
    predicate As Func(Of TSource, Boolean) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim predicate As Func(Of TSource, Boolean)
Dim returnValue As IObservable(Of TSource)

returnValue = source.TakeWhile(predicate)
```

```csharp
public static IObservable<TSource> TakeWhile<TSource>(
    this IObservable<TSource> source,
    Func<TSource, bool> predicate
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ TakeWhile(
    IObservable<TSource>^ source, 
    Func<TSource, bool>^ predicate
)
```

```fsharp
static member TakeWhile : 
        source:IObservable<'TSource> * 
        predicate:Func<'TSource, bool> -> IObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  A sequence to return elements from.

- predicate  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
  A function to test each source element for a condition.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence that contains the elements from the input sequence that occur before the element at which the test no longer passes.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[TakeWhile Overload](TakeWhile/Observable.TakeWhile)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Observable.TakeWhile\<TSource\> Method (IObservable\<TSource\>, Func\<TSource, Int32, Boolean\>)

Returns values from an observable sequence as long as a specified condition is true, and then skips the remaining values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function TakeWhile(Of TSource) ( _
    source As IObservable(Of TSource), _
    predicate As Func(Of TSource, Integer, Boolean) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim predicate As Func(Of TSource, Integer, Boolean)
Dim returnValue As IObservable(Of TSource)

returnValue = source.TakeWhile(predicate)
```

```csharp
public static IObservable<TSource> TakeWhile<TSource>(
    this IObservable<TSource> source,
    Func<TSource, int, bool> predicate
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ TakeWhile(
    IObservable<TSource>^ source, 
    Func<TSource, int, bool>^ predicate
)
```

```fsharp
static member TakeWhile : 
        source:IObservable<'TSource> * 
        predicate:Func<'TSource, int, bool> -> IObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  A sequence to return elements from.

- predicate  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TSource, [Int32](https://msdn.microsoft.com/en-us/library/td2s409d), [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
  True to the function to test each element for a condition; the second parameter of the function represents the index of the source element; otherwise, false.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence that contains the elements from the input sequence that occur before the element at which the test no longer passes.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[TakeWhile Overload](TakeWhile/Observable.TakeWhile)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
