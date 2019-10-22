title: MyExt.Bar(IObservable<Int32>)
---
# MyExt.Bar Method (IObservable\<Int32\>)

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Bar ( _
    source As IObservable(Of Integer) _
) As IObservable(Of String)
```

```vb
'Usage
Dim source As IObservable(Of Integer)
Dim returnValue As IObservable(Of String)

returnValue = source.Bar()
```

```csharp
public static IObservable<string> Bar(
    this IObservable<int> source
)
```

```c++
[ExtensionAttribute]
public:
static IObservable<String^>^ Bar(
    IObservable<int>^ source
)
```

```fsharp
static member Bar : 
        source:IObservable<int> -> IObservable<string> 
```

```jscript
public static function Bar(
    source : IObservable<int>
) : IObservable<String>
```

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[String](https://msdn.microsoft.com/en-us/library/s1wwdcbf)\>

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[MyExt Class](MyExt\MyExt.md)

[Bar Overload](Bar\MyExt.Bar.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)








# MyExt.Bar Method

Include Protected Members  
Include Inherited Members

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Bar(IObservable<Int32>)](https://msdn.microsoft.com/en-us/library/m:reactivetests.tests.myext.bar(system.iobservable%7bsystem.int32%7d)(v=VS.103))![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[Bar(IQbservable<Int32>)](https://msdn.microsoft.com/en-us/library/m:reactivetests.tests.myext.bar(system.reactive.linq.iqbservable%7bsystem.int32%7d)(v=VS.103))Top

## See Also

#### Reference

[MyExt Class](MyExt\MyExt.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)





# MyExt.Bar Method (IQbservable\<Int32\>)

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Bar ( _
    source As IQbservable(Of Integer) _
) As IQbservable(Of String)
```

```vb
'Usage
Dim source As IQbservable(Of Integer)
Dim returnValue As IQbservable(Of String)

returnValue = source.Bar()
```

```csharp
public static IQbservable<string> Bar(
    this IQbservable<int> source
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<String^>^ Bar(
    IQbservable<int>^ source
)
```

```fsharp
static member Bar : 
        source:IQbservable<int> -> IQbservable<string> 
```

```jscript
public static function Bar(
    source : IQbservable<int>
) : IQbservable<String>
```

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[String](https://msdn.microsoft.com/en-us/library/s1wwdcbf)\>

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<[Int32](https://msdn.microsoft.com/en-us/library/td2s409d)\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[MyExt Class](MyExt\MyExt.md)

[Bar Overload](Bar\MyExt.Bar.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)







