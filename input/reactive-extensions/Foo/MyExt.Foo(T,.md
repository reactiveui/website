title: MyExt.Foo<T, R>(IObservable<T>, Func<T, R>)
---
# MyExt.Foo\<T, R\> Method (IObservable\<T\>, Func\<T, R\>)

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests/ReactiveTests.Tests)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Foo(Of T, R) ( _
    source As IObservable(Of T), _
    f As Func(Of T, R) _
) As IObservable(Of R)
```

```vb
'Usage
Dim source As IObservable(Of T)
Dim f As Func(Of T, R)
Dim returnValue As IObservable(Of R)

returnValue = source.Foo(f)
```

```csharp
public static IObservable<R> Foo<T, R>(
    this IObservable<T> source,
    Func<T, R> f
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T, typename R>
static IObservable<R>^ Foo(
    IObservable<T>^ source, 
    Func<T, R>^ f
)
```

```fsharp
static member Foo : 
        source:IObservable<'T> * 
        f:Func<'T, 'R> -> IObservable<'R> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

- R

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<T\>

- f  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<T, R\>

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<R\>

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<T\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[MyExt Class](MyExt/MyExt)

[Foo Overload](Foo/MyExt.Foo)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests/ReactiveTests.Tests)









# MyExt.Foo\<T, R\> Method (IQbservable\<T\>, Expression\<Func\<T, R\>\>)

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests/ReactiveTests.Tests)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Foo(Of T, R) ( _
    source As IQbservable(Of T), _
    f As Expression(Of Func(Of T, R)) _
) As IQbservable(Of R)
```

```vb
'Usage
Dim source As IQbservable(Of T)
Dim f As Expression(Of Func(Of T, R))
Dim returnValue As IQbservable(Of R)

returnValue = source.Foo(f)
```

```csharp
public static IQbservable<R> Foo<T, R>(
    this IQbservable<T> source,
    Expression<Func<T, R>> f
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T, typename R>
static IQbservable<R>^ Foo(
    IQbservable<T>^ source, 
    Expression<Func<T, R>^>^ f
)
```

```fsharp
static member Foo : 
        source:IQbservable<'T> * 
        f:Expression<Func<'T, 'R>> -> IQbservable<'R> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

- R

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<T\>

- f  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<T, R\>\>

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<R\>

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable/IQbservable(TSource))\<T\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[MyExt Class](MyExt/MyExt)

[Foo Overload](Foo/MyExt.Foo)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests/ReactiveTests.Tests)








