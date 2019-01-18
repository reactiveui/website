# Qbservable.FromEvent Method

Include Protected Members  
Include Inherited Members

Converts a .NET event to a queryable observable sequence.

This member is overloaded. For complete information about this member, including syntax, usage, and examples, click a name in the overload list.

## Overload List

NameDescription![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEvent<TEventArgs>(IQbservableProvider, Expression<Action<Action<TEventArgs>>>, Expression<Action<Action<TEventArgs>>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.fromevent%60%601(system.reactive.linq.iqbservableprovider%2csystem.linq.expressions.expression%7bsystem.action%7bsystem.action%7b%60%600%7d%7d%7d%2csystem.linq.expressions.expression%7bsystem.action%7bsystem.action%7b%60%600%7d%7d%7d)(v=VS.103))Converts a .NET event to a queryable observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEvent(IQbservableProvider, Expression<Action<Action>>, Expression<Action<Action>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.fromevent(system.reactive.linq.iqbservableprovider%2csystem.linq.expressions.expression%7bsystem.action%7bsystem.action%7d%7d%2csystem.linq.expressions.expression%7bsystem.action%7bsystem.action%7d%7d)(v=VS.103))Converts a .NET event to a queryable observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEvent<TDelegate, TEventArgs>(IQbservableProvider, Expression<Action<TDelegate>>, Expression<Action<TDelegate>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.fromevent%60%602(system.reactive.linq.iqbservableprovider%2csystem.linq.expressions.expression%7bsystem.action%7b%60%600%7d%7d%2csystem.linq.expressions.expression%7bsystem.action%7b%60%600%7d%7d)(v=VS.103))Converts a .NET event to a queryable observable sequence.![Public method](https://reactiveui.net/assets/img/Hh303103.pubmethod(en-us,VS.103).gif "Public method")![Static member](https://reactiveui.net/assets/img/Hh244319.static(en-us,VS.103).gif "Static member")[FromEvent<TDelegate, TEventArgs>(IQbservableProvider, Expression<Func<Action<TEventArgs>, TDelegate>>, Expression<Action<TDelegate>>, Expression<Action<TDelegate>>)](https://msdn.microsoft.com/en-us/library/m:system.reactive.linq.qbservable.fromevent%60%602(system.reactive.linq.iqbservableprovider%2csystem.linq.expressions.expression%7bsystem.func%7bsystem.action%7b%60%601%7d%2c%60%600%7d%7d%2csystem.linq.expressions.expression%7bsystem.action%7b%60%600%7d%7d%2csystem.linq.expressions.expression%7bsystem.action%7b%60%600%7d%7d)(v=VS.103))Converts a .NET event to a queryable observable sequence.Top

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)





# Qbservable.FromEvent Method (IQbservableProvider, Expression\<Action\<Action\>\>, Expression\<Action\<Action\>\>)

Converts a .NET event to a queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromEvent ( _
    provider As IQbservableProvider, _
    addHandler As Expression(Of Action(Of Action)), _
    removeHandler As Expression(Of Action(Of Action)) _
) As IQbservable(Of Unit)
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim addHandler As Expression(Of Action(Of Action))
Dim removeHandler As Expression(Of Action(Of Action))
Dim returnValue As IQbservable(Of Unit)

returnValue = provider.FromEvent(addHandler, _
    removeHandler)
```

```csharp
public static IQbservable<Unit> FromEvent(
    this IQbservableProvider provider,
    Expression<Action<Action>> addHandler,
    Expression<Action<Action>> removeHandler
)
```

```c++
[ExtensionAttribute]
public:
static IQbservable<Unit>^ FromEvent(
    IQbservableProvider^ provider, 
    Expression<Action<Action^>^>^ addHandler, 
    Expression<Action<Action^>^>^ removeHandler
)
```

```fsharp
static member FromEvent : 
        provider:IQbservableProvider * 
        addHandler:Expression<Action<Action>> * 
        removeHandler:Expression<Action<Action>> -> IQbservable<Unit> 
```

```jscript
public static function FromEvent(
    provider : IQbservableProvider, 
    addHandler : Expression<Action<Action>>, 
    removeHandler : Expression<Action<Action>>
) : IQbservable<Unit>
```

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- addHandler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Action](https://msdn.microsoft.com/en-us/library/Bb534741)\>\>  
  Action that attaches the given event handler to the underlying .NET event.

- removeHandler  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Action](https://msdn.microsoft.com/en-us/library/Bb534741)\>\>  
  Action that detaches the given event handler from the underlying .NET event.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>  
Observable sequence that contains data representations of invocations of the underlying .NET event.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromEvent Overload](FromEvent\Qbservable.FromEvent.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)







