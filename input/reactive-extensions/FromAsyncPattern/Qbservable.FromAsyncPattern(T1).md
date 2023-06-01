title: Qbservable.FromAsyncPattern<T1>(IQbservableProvider, Expression<Func<T1, AsyncCallback, Object, IAsyncResult>>, Expression<Action<IAsyncResult>>)
---
# Qbservable.FromAsyncPattern\<T1\> Method (IQbservableProvider, Expression\<Func\<T1, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Action\<IAsyncResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Action(Of IAsyncResult)) _
) As Func(Of T1, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Action(Of IAsyncResult))
Dim returnValue As Func(Of T1, IQbservable(Of Unit))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, IQbservable<Unit>> FromAsyncPattern<T1>(
    this IQbservableProvider provider,
    Expression<Func<T1, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Action<IAsyncResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1>
static Func<T1, IQbservable<Unit>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Action<IAsyncResult^>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Action<IAsyncResult>> -> Func<'T1, IQbservable<Unit>> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider/IQbservableProvider)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<T1, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<T1, [IQbservable](IQbservable/IQbservable(TSource))\<[Unit](Unit/Unit)\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider/IQbservableProvider). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable/Qbservable)

[FromAsyncPattern Overload](FromAsyncPattern/Qbservable.FromAsyncPattern)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








