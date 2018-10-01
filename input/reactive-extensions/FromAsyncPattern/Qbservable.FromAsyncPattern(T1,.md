# Qbservable.FromAsyncPattern\<T1, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of T1, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of T1, IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, IQbservable<TResult>> FromAsyncPattern<T1, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename TResult>
static Func<T1, IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<'T1, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<T1, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<T1, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
The end invoke function.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Action\<IAsyncResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Action(Of IAsyncResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Action(Of IAsyncResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IQbservable(Of Unit))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Action<IAsyncResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<Unit>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Action<IAsyncResult^>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Action<IAsyncResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<T1, T2, T3, T4, T5, T6, T7, T8, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<T1, T2, T3, T4, T5, T6, T7, T8, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Action\<IAsyncResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Action(Of IAsyncResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Action(Of IAsyncResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, IQbservable(Of Unit))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Action<IAsyncResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6>
static Func<T1, T2, T3, T4, T5, T6, IQbservable<Unit>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Action<IAsyncResult^>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Action<IAsyncResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The third type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<T1, T2, T3, T4, T5, T6, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<T1, T2, T3, T4, T5, T6, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of T1, T2, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of T1, T2, IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, IQbservable<TResult>> FromAsyncPattern<T1, T2, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename TResult>
static Func<T1, T2, IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<'T1, 'T2, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The first type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<T1, T2, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<T1, T2, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The first type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The seventh type of function.

- T9  
  The seventh type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The begin invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The first type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402868)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The second type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402862)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Action\<IAsyncResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Action(Of IAsyncResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Action(Of IAsyncResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, IQbservable(Of Unit))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Action<IAsyncResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7>
static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<Unit>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Action<IAsyncResult^>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Action<IAsyncResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The first type of function.

- T3  
  The first type of function.

- T4  
  The first type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The sixth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<T1, T2, T3, T4, T5, T6, T7, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<T1, T2, T3, T4, T5, T6, T7, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of T1, T2, T3, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of T1, T2, T3, IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename TResult>
static Func<T1, T2, T3, IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<'T1, 'T2, 'T3, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The first type of function.

- T3  
  The third type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<T1, T2, T3, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The local Qbservable provider.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The local Qbservable provider.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<T1, T2, T3, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<T1, T2, T3, T4, T5, T6, T7, T8, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<T1, T2, T3, T4, T5, T6, T7, T8, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Action\<IAsyncResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Action(Of IAsyncResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Action(Of IAsyncResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable(Of Unit))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Action<IAsyncResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, IQbservable<Unit>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Action<IAsyncResult^>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Action<IAsyncResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Action\<IAsyncResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Action(Of IAsyncResult)) _
) As Func(Of T1, T2, T3, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Action(Of IAsyncResult))
Dim returnValue As Func(Of T1, T2, T3, IQbservable(Of Unit))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Action<IAsyncResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3>
static Func<T1, T2, T3, IQbservable<Unit>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Action<IAsyncResult^>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Action<IAsyncResult>> -> Func<'T1, 'T2, 'T3, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<T1, T2, T3, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549430)\<T1, T2, T3, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Action\<IAsyncResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Action(Of IAsyncResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Action(Of IAsyncResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable(Of Unit))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Action<IAsyncResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13, typename T14>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, IQbservable<Unit>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Action<IAsyncResult^>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Action<IAsyncResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, 'T14, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The first type of function.

- T3  
  The first type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The fifth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

- T14  
  The fourteenth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402862)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename TResult>
static Func<T1, T2, T3, T4, T5, IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The second type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<T1, T2, T3, T4, T5, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The begin invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<T1, T2, T3, T4, T5, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Action\<IAsyncResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Action(Of IAsyncResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Action(Of IAsyncResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable(Of Unit))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Action<IAsyncResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12, typename T13>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, IQbservable<Unit>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Action<IAsyncResult^>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Action<IAsyncResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, 'T13, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

- T13  
  The thirteenth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402868)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The local Qbservable provider.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The local Qbservable provider.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of T1, T2, T3, T4, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename TResult>
static Func<T1, T2, T3, T4, IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<T1, T2, T3, T4, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<T1, T2, T3, T4, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2\> Method (IQbservableProvider, Expression\<Func\<T1, T2, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Action\<IAsyncResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Action(Of IAsyncResult)) _
) As Func(Of T1, T2, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Action(Of IAsyncResult))
Dim returnValue As Func(Of T1, T2, IQbservable(Of Unit))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, IQbservable<Unit>> FromAsyncPattern<T1, T2>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Action<IAsyncResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2>
static Func<T1, T2, IQbservable<Unit>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Action<IAsyncResult^>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Action<IAsyncResult>> -> Func<'T1, 'T2, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<T1, T2, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The local Qbservable provider.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<T1, T2, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Action\<IAsyncResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Action(Of IAsyncResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Action(Of IAsyncResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable(Of Unit))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Action<IAsyncResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, IQbservable<Unit>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Action<IAsyncResult^>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Action<IAsyncResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The eighth type of function.

- T10  
  The tenth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd383294)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd267613)\<T1, T2, T3, T4, T5, T6, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<T1, T2, T3, T4, T5, T6, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Action\<IAsyncResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Action(Of IAsyncResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Action(Of IAsyncResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable(Of Unit))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Action<IAsyncResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11, typename T12>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, IQbservable<Unit>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Action<IAsyncResult^>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Action<IAsyncResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, 'T12, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

- T12  
  The twelfth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402861)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402863)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, TResult\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Func\<IAsyncResult, TResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, TResult) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Func(Of IAsyncResult, TResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, IQbservable(Of TResult))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Func(Of IAsyncResult, TResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, IQbservable(Of TResult))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<TResult>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, TResult>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Func<IAsyncResult, TResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename TResult>
static Func<T1, T2, T3, T4, T5, T6, T7, IQbservable<TResult>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Func<IAsyncResult^, TResult>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Func<IAsyncResult, 'TResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, IQbservable<'TResult>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- TResult  
  The type of result.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd386894)\<T1, T2, T3, T4, T5, T6, T7, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455), TResult\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<T1, T2, T3, T4, T5, T6, T7, [IQbservable](IQbservable\IQbservable(TSource).md)\<TResult\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Action\<IAsyncResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Action(Of IAsyncResult)) _
) As Func(Of T1, T2, T3, T4, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Action(Of IAsyncResult))
Dim returnValue As Func(Of T1, T2, T3, T4, IQbservable(Of Unit))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Action<IAsyncResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4>
static Func<T1, T2, T3, T4, IQbservable<Unit>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Action<IAsyncResult^>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Action<IAsyncResult>> -> Func<'T1, 'T2, 'T3, 'T4, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  Converts a Begin/End invoke function pair into an asynchronous function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd269654)\<T1, T2, T3, T4, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The local Qbservable provider.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534303)\<T1, T2, T3, T4, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Action\<IAsyncResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Action(Of IAsyncResult)) _
) As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Action(Of IAsyncResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable(Of Unit))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Action<IAsyncResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5, typename T6, typename T7, typename T8, typename T9, typename T10, typename T11>
static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, IQbservable<Unit>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Action<IAsyncResult^>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Action<IAsyncResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9, 'T10, 'T11, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The third type of function.

- T4  
  The fourth type of function.

- T5  
  The fifth type of function.

- T6  
  The sixth type of function.

- T7  
  The seventh type of function.

- T8  
  The eighth type of function.

- T9  
  The ninth type of function.

- T10  
  The tenth type of function.

- T11  
  The eleventh type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd402867)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd402864)\<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.FromAsyncPattern\<T1, T2, T3, T4, T5\> Method (IQbservableProvider, Expression\<Func\<T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult\>\>, Expression\<Action\<IAsyncResult\>\>)

Converts a Begin/End invoke function pair into an asynchronous function.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function FromAsyncPattern(Of T1, T2, T3, T4, T5) ( _
    provider As IQbservableProvider, _
    begin As Expression(Of Func(Of T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult)), _
    end As Expression(Of Action(Of IAsyncResult)) _
) As Func(Of T1, T2, T3, T4, T5, IQbservable(Of Unit))
```

```vb
'Usage
Dim provider As IQbservableProvider
Dim begin As Expression(Of Func(Of T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult))
Dim end As Expression(Of Action(Of IAsyncResult))
Dim returnValue As Func(Of T1, T2, T3, T4, T5, IQbservable(Of Unit))

returnValue = provider.FromAsyncPattern(begin, _
    end)
```

```csharp
public static Func<T1, T2, T3, T4, T5, IQbservable<Unit>> FromAsyncPattern<T1, T2, T3, T4, T5>(
    this IQbservableProvider provider,
    Expression<Func<T1, T2, T3, T4, T5, AsyncCallback, Object, IAsyncResult>> begin,
    Expression<Action<IAsyncResult>> end
)
```

```c++
[ExtensionAttribute]
public:
generic<typename T1, typename T2, typename T3, typename T4, typename T5>
static Func<T1, T2, T3, T4, T5, IQbservable<Unit>^>^ FromAsyncPattern(
    IQbservableProvider^ provider, 
    Expression<Func<T1, T2, T3, T4, T5, AsyncCallback^, Object^, IAsyncResult^>^>^ begin, 
    Expression<Action<IAsyncResult^>^>^ end
)
```

```fsharp
static member FromAsyncPattern : 
        provider:IQbservableProvider * 
        begin:Expression<Func<'T1, 'T2, 'T3, 'T4, 'T5, AsyncCallback, Object, IAsyncResult>> * 
        end:Expression<Action<IAsyncResult>> -> Func<'T1, 'T2, 'T3, 'T4, 'T5, IQbservable<Unit>> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- T1  
  The first type of function.

- T2  
  The second type of function.

- T3  
  The second type of function.

- T4  
  The second type of function.

- T5  
  The second type of function.

#### Parameters

- provider  
  Type: [System.Reactive.Linq.IQbservableProvider](IQbservableProvider\IQbservableProvider.md)  
  The local Qbservable provider.

- begin  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Func](https://msdn.microsoft.com/en-us/library/Dd289456)\<T1, T2, T3, T4, T5, [AsyncCallback](https://msdn.microsoft.com/en-us/library/ckbe7yh5), [Object](https://msdn.microsoft.com/en-us/library/e5kfa45b), [IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The begin invoke function.

- end  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[IAsyncResult](https://msdn.microsoft.com/en-us/library/ft8a6455)\>\>  
  The end invoke function.

#### Return Value

Type: [System.Func](https://msdn.microsoft.com/en-us/library/Dd268303)\<T1, T2, T3, T4, T5, [IQbservable](IQbservable\IQbservable(TSource).md)\<[Unit](Unit\Unit.md)\>\>  
A Begin/End invoke function pair.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservableProvider](IQbservableProvider\IQbservableProvider.md). When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[FromAsyncPattern Overload](FromAsyncPattern\Qbservable.FromAsyncPattern.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








