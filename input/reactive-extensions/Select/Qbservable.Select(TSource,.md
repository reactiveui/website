title: Qbservable.Select<TSource, TResult>(IQbservable<TSource>, Expression<Func<TSource, Int32, TResult>>)
---
# Qbservable.Select\<TSource, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, Int32, TResult\>\>)

Projects each element of a queryable observable sequence into a new form by incorporating the element’s index with the specified source and selector.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Select(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    selector As Expression(Of Func(Of TSource, Integer, TResult)) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim selector As Expression(Of Func(Of TSource, Integer, TResult))
Dim returnValue As IQbservable(Of TResult)

returnValue = source.Select(selector)
```

```csharp
public static IQbservable<TResult> Select<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, int, TResult>> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IQbservable<TResult>^ Select(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, int, TResult>^>^ selector
)
```

```fsharp
static member Select : 
        source:IQbservable<'TSource> * 
        selector:Expression<Func<'TSource, int, 'TResult>> -> IQbservable<'TResult> 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A sequence of elements to invoke a transform function on.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TSource, [Int32](https://msdn.microsoft.com/en-us/library/td2s409d), TResult\>\>  
  A transform function to apply to each source element.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence into a new form.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Select Overload](Select/Qbservable.Select)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)

# Qbservable.Select\<TSource, TResult\> Method (IQbservable\<TSource\>, Expression\<Func\<TSource, TResult\>\>)

Projects each element of a queryable observable sequence into a new form with the specified source and selector.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Select(Of TSource, TResult) ( _
    source As IQbservable(Of TSource), _
    selector As Expression(Of Func(Of TSource, TResult)) _
) As IQbservable(Of TResult)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim selector As Expression(Of Func(Of TSource, TResult))
Dim returnValue As IQbservable(Of TResult)

returnValue = source.Select(selector)
```

```csharp
public static IQbservable<TResult> Select<TSource, TResult>(
    this IQbservable<TSource> source,
    Expression<Func<TSource, TResult>> selector
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TResult>
static IQbservable<TResult>^ Select(
    IQbservable<TSource>^ source, 
    Expression<Func<TSource, TResult>^>^ selector
)
```

```fsharp
static member Select : 
        source:IQbservable<'TSource> * 
        selector:Expression<Func<'TSource, 'TResult>> -> IQbservable<'TResult> 
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
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
  A sequence of elements to invoke a transform function on.

- selector  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, TResult\>\>  
  A transform function to apply to each source element.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TResult\>  
A queryable observable sequence into a new form.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Select Overload](Select/Qbservable.Select)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
