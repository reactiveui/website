title: Qbservable.Create<TSource>(IQbservableProvider, Expression<Func<IObserver<TSource>, Action>>)
---
# Qbservable.Create\<TSource\> Method (IQbservableProvider, Expression\<Func\<IObserver\<TSource\>, Action\>\>)

Creates a queryable observable sequence from a specified subscribe method implementation with a specified subscribe.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Create(Of TSource) ( _
    provider As IQbservableProvider, _
    subscribe As Expression(Of Func(Of IObserver(Of TSource), Action)) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim subscribe As Expression(Of Func(Of IObserver(Of TSource), Action))
Dim returnValue As IQbservable(Of TSource)

returnValue = provider.Create(subscribe)
```

```csharp
public static IQbservable<TSource> Create<TSource>(
    this IQbservableProvider provider,
    Expression<Func<IObserver<TSource>, Action>> subscribe
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Create(
    IQbservableProvider^ provider, 
    Expression<Func<IObserver<TSource>^, Action^>^>^ subscribe
)
```

```fsharp
static member Create : 
        provider:IQbservableProvider * 
        subscribe:Expression<Func<IObserver<'TSource>, Action>> -> IQbservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- subscribe  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<TSource\>, [Action](https://msdn.microsoft.com/en-us/library/Bb534741)\>\>  
  The implementation of the resulting queryable observable sequence's subscribe method, returning an action delegate that will be wrapped in an IDisposable.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
The queryable observable sequence with the specified implementation for the subscribe method.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Create Overload](Create/Qbservable.Create)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Qbservable.Create\<TSource\> Method (IQbservableProvider, Expression\<Func\<IObserver\<TSource\>, IDisposable\>\>)

Creates a queryable observable sequence from a specified subscribe method implementation with a specified subscribe.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Create(Of TSource) ( _
    provider As IQbservableProvider, _
    subscribe As Expression(Of Func(Of IObserver(Of TSource), IDisposable)) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim subscribe As Expression(Of Func(Of IObserver(Of TSource), IDisposable))
Dim returnValue As IQbservable(Of TSource)

returnValue = provider.Create(subscribe)
```

```csharp
public static IQbservable<TSource> Create<TSource>(
    this IQbservableProvider provider,
    Expression<Func<IObserver<TSource>, IDisposable>> subscribe
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Create(
    IQbservableProvider^ provider, 
    Expression<Func<IObserver<TSource>^, IDisposable^>^>^ subscribe
)
```

```fsharp
static member Create : 
        provider:IQbservableProvider * 
        subscribe:Expression<Func<IObserver<'TSource>, IDisposable>> -> IQbservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- subscribe  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<TSource\>, [IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)\>\>  
  The implementation of the resulting queryable observable sequence's subscribe method.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable/IQbservable(TSource))\<TSource\>  
The queryable observable sequence with the specified implementation for the subscribe method.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[Create Overload](Create/Qbservable.Create)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








