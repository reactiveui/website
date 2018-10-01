# Qbservable.TakeWhile\<TSource\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, Int32, Boolean\>\>)

Returns values from a queryable observable sequence as long as a specified condition is true, and then skips the remaining values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function TakeWhile(Of TSource) ( _
    source As IQbservable(Of TSource), _
    predicate As Expression(Of Func(Of TSource, Integer, Boolean)) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim predicate As Expression(Of Func(Of TSource, Integer, Boolean))
Dim returnValue As IQbservable(Of TSource)

returnValue = source.TakeWhile(predicate)
```

```csharp
public static IQbservable<TSource> TakeWhile<TSource>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, int, bool>> predicate
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ TakeWhile(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, int, bool>^>^ predicate
)
```

```fsharp
static member TakeWhile : 
        source:IQbservable<'TSource> * 
        predicate:Expression<Func<'TSource, int, bool>> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  A sequence to return elements from.

- predicate  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TSource, [Int32](https://msdn.microsoft.com/en-us/library/td2s409d), [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>\>  
  A function to test each source element for a condition.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
A queryable observable sequence that contains the elements from the input sequence that occur before the element at which the test no longer passes.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[TakeWhile Overload](TakeWhile\Qbservable.TakeWhile.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Qbservable.TakeWhile\<TSource\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, Boolean\>\>)

Returns values from a queryable observable sequence as long as a specified condition is true, and then skips the remaining values.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function TakeWhile(Of TSource) ( _
    source As IQbservable(Of TSource), _
    predicate As Expression(Of Func(Of TSource, Boolean)) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim predicate As Expression(Of Func(Of TSource, Boolean))
Dim returnValue As IQbservable(Of TSource)

returnValue = source.TakeWhile(predicate)
```

```csharp
public static IQbservable<TSource> TakeWhile<TSource>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, bool>> predicate
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ TakeWhile(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, bool>^>^ predicate
)
```

```fsharp
static member TakeWhile : 
        source:IQbservable<'TSource> * 
        predicate:Expression<Func<'TSource, bool>> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  A sequence to return elements from.

- predicate  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>\>  
  A function to test each source element for a condition.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
A queryable observable sequence that contains the elements from the input sequence that occur before the element at which the test no longer passes.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[TakeWhile Overload](TakeWhile\Qbservable.TakeWhile.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
