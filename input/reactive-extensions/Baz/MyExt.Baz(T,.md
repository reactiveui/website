title: MyExt.Baz<T, R>()
---
# MyExt.Baz\<T, R\> Method

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Baz(Of T, R) ( _
    source As IQbservable(Of T), _
    f As Expression(Of Func(Of T, R)) _
) As IQbservable(Of R)
```

```vb
'Usage
Dim source As IQbservable(Of T)
Dim f As Expression(Of Func(Of T, R))
Dim returnValue As IQbservable(Of R)

returnValue = source.Baz(f)
```

```csharp
public static IQbservable<R> Baz<T, R>(
    this IQbservable<T> source,
    Expression<Func<T, R>> f
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T, typename R>
static IQbservable<R>^ Baz(
    IQbservable<T>^ source, 
    Expression<Func<T, R>^>^ f
)
```

```fsharp
static member Baz : 
        source:IQbservable<'T> * 
        f:Expression<Func<'T, 'R>> -> IQbservable<'R> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

- R

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<T\>

- f  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<T, R\>\>

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<R\>

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<T\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[MyExt Class](MyExt\MyExt.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)








