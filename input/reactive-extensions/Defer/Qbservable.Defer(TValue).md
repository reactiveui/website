# Qbservable.Defer\<TValue\> Method

Returns a queryable observable sequence that invokes the observable factory whenever a new observer subscribes.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Defer(Of TValue) ( _
    provider As IQbservableProvider, _
    observableFactory As Expression(Of Func(Of IObservable(Of TValue))) _
) As IQbservable(Of TValue)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim observableFactory As Expression(Of Func(Of IObservable(Of TValue)))
Dim returnValue As IQbservable(Of TValue)

returnValue = provider.Defer(observableFactory)
```

```csharp
public static IQbservable<TValue> Defer<TValue>(
    this IQbservableProvider provider,
    Expression<Func<IObservable<TValue>>> observableFactory
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TValue>
static IQbservable<TValue>^ Defer(
    IQbservableProvider^ provider, 
    Expression<Func<IObservable<TValue>^>^>^ observableFactory
)
```

```fsharp
static member Defer : 
        provider:IQbservableProvider * 
        observableFactory:Expression<Func<IObservable<'TValue>>> -> IQbservable<'TValue> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TValue  
  The type of value.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- observableFactory  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TValue\>\>\>  
  The queryable observable factory function to invoke for each observer that subscribes to the resulting sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TValue\>  
The queryable observable sequence whose observers trigger an invocation of the given queryable observable factory function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








