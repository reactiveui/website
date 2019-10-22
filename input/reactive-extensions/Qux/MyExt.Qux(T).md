title: MyExt.Qux<T>(T)
---
# MyExt.Qux\<T\> Method (T)

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Qux(Of T) ( _
    value As T _
) As IObservable(Of T)
```

```vb
'Usage
Dim value As T
Dim returnValue As IObservable(Of T)

returnValue = MyExt.Qux(value)
```

```csharp
public static IObservable<T> Qux<T>(
    T value
)
```

```c++
public:
generic<typename T>
static IObservable<T>^ Qux(
    T value
)
```

```fsharp
static member Qux : 
        value:'T -> IObservable<'T> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

#### Parameters

- value  
  Type: T

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<T\>

## See Also

#### Reference

[MyExt Class](MyExt\MyExt.md)

[Qux Overload](Qux\MyExt.Qux.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)

# MyExt.Qux\<T\> Method (IQbservableProvider, T)

**Namespace:**  [ReactiveTests.Tests](ReactiveTests.Tests\ReactiveTests.Tests.md)  
**Assembly:**  Tests.System.Reactive (in Tests.System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Qux(Of T) ( _
    provider As IQbservableProvider, _
    value As T _
) As IQbservable(Of T)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim value As T
Dim returnValue As IQbservable(Of T)

returnValue = provider.Qux(value)
```

```csharp
public static IQbservable<T> Qux<T>(
    this IQbservableProvider provider,
    T value
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T>
static IQbservable<T>^ Qux(
    IQbservableProvider^ provider, 
    T value
)
```

```fsharp
static member Qux : 
        provider:IQbservableProvider * 
        value:'T -> IQbservable<'T> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)

- value  
  Type: T

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<T\>

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[MyExt Class](MyExt\MyExt.md)

[Qux Overload](Qux\MyExt.Qux.md)

[ReactiveTests.Tests Namespace](ReactiveTests.Tests\ReactiveTests.Tests.md)
