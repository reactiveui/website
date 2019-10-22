title: Qbservable.FromAsyncPattern<TResult>(IQbservableProvider, Expression<Func<AsyncCallback, Object, IAsyncResult>>, Expression<Func<IAsyncResult, TResult>>)
---
# Qbservable.FromAsyncPattern\<TResult\> Method (IQbservableProvider, Expression\<Func\<AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<IQbservable<TResult>> FromAsyncPattern<TResult>(
    this IQbservableProvider provider,
    Expression<Func<AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TResult>
static Func<IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<[AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534960)\<[IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








