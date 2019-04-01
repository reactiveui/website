# Qbservable.Do\<TSource\> Method (IQbservable\<TSource\>, Expression\<Action\<TSource\>\>)

Invokes an action for each element in the queryable observable sequence and invokes an action upon graceful termination of the queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Do(Of TSource) ( _
    source As IQbservable(Of TSource), _
    onNext As Expression(Of Action(Of TSource)) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim onNext As Expression(Of Action(Of TSource))
Dim returnValue As IQbservable(Of TSource)

returnValue = source.Do(onNext)
```

```csharp
public static IQbservable<TSource> Do<TSource>(
    this IQbservable<TSource> source,
    Expression<Action<TSource>> onNext
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Do(
    IQbservable<TSource>^ source, 
    Expression<Action<TSource>^>^ onNext
)
```

```fsharp
static member Do : 
        source:IQbservable<'TSource> * 
        onNext:Expression<Action<'TSource>> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence.

- onNext  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>\>  
  The action to invoke for each element in the queryable observable sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
The source sequence with the side-effecting behavior applied.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Do Overload](Do\Qbservable.Do.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.Do\<TSource\> Method (IQbservable\<TSource\>, Expression\<Action\<TSource\>\>, Expression\<Action\<Exception\>\>)

Invokes an action for each element in the queryable observable sequence and invokes an action upon exceptional termination of the queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Do(Of TSource) ( _
    source As IQbservable(Of TSource), _
    onNext As Expression(Of Action(Of TSource)), _
    onError As Expression(Of Action(Of Exception)) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim onNext As Expression(Of Action(Of TSource))
Dim onError As Expression(Of Action(Of Exception))
Dim returnValue As IQbservable(Of TSource)

returnValue = source.Do(onNext, onError)
```

```csharp
public static IQbservable<TSource> Do<TSource>(
    this IQbservable<TSource> source,
    Expression<Action<TSource>> onNext,
    Expression<Action<Exception>> onError
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Do(
    IQbservable<TSource>^ source, 
    Expression<Action<TSource>^>^ onNext, 
    Expression<Action<Exception^>^>^ onError
)
```

```fsharp
static member Do : 
        source:IQbservable<'TSource> * 
        onNext:Expression<Action<'TSource>> * 
        onError:Expression<Action<Exception>> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence.

- onNext  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>\>  
  The action to invoke for each element in the queryable observable sequence.

- onError  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)\>\>  
  The action to invoke upon exceptional termination of the queryable observable sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
The source sequence with the side-effecting behavior applied.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Do Overload](Do\Qbservable.Do.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.Do\<TSource\> Method (IQbservable\<TSource\>, IObserver\<TSource\>)

Invokes an action for each element in the queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Do(Of TSource) ( _
    source As IQbservable(Of TSource), _
    observer As IObserver(Of TSource) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim observer As IObserver(Of TSource)
Dim returnValue As IQbservable(Of TSource)

returnValue = source.Do(observer)
```

```csharp
public static IQbservable<TSource> Do<TSource>(
    this IQbservable<TSource> source,
    IObserver<TSource> observer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Do(
    IQbservable<TSource>^ source, 
    IObserver<TSource>^ observer
)
```

```fsharp
static member Do : 
        source:IQbservable<'TSource> * 
        observer:IObserver<'TSource> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence.

- observer  
  Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<TSource\>  
  The action to invoke for each element in the queryable observable sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
The source sequence with the side-effecting behavior applied.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Do Overload](Do\Qbservable.Do.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.Do\<TSource\> Method (IQbservable\<TSource\>, Expression\<Action\<TSource\>\>, Expression\<Action\<Exception\>\>, Expression\<Action\>)

Invokes an action for each element in the queryable observable sequence, and invokes an action upon graceful or exceptional termination of the queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Do(Of TSource) ( _
    source As IQbservable(Of TSource), _
    onNext As Expression(Of Action(Of TSource)), _
    onError As Expression(Of Action(Of Exception)), _
    onCompleted As Expression(Of Action) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim onNext As Expression(Of Action(Of TSource))
Dim onError As Expression(Of Action(Of Exception))
Dim onCompleted As Expression(Of Action)
Dim returnValue As IQbservable(Of TSource)

returnValue = source.Do(onNext, onError, _
    onCompleted)
```

```csharp
public static IQbservable<TSource> Do<TSource>(
    this IQbservable<TSource> source,
    Expression<Action<TSource>> onNext,
    Expression<Action<Exception>> onError,
    Expression<Action> onCompleted
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Do(
    IQbservable<TSource>^ source, 
    Expression<Action<TSource>^>^ onNext, 
    Expression<Action<Exception^>^>^ onError, 
    Expression<Action^>^ onCompleted
)
```

```fsharp
static member Do : 
        source:IQbservable<'TSource> * 
        onNext:Expression<Action<'TSource>> * 
        onError:Expression<Action<Exception>> * 
        onCompleted:Expression<Action> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence.

- onNext  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>\>  
  The action to invoke for each element in the queryable observable sequence.

- onError  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)\>\>  
  The action to invoke upon exceptional termination of the queryable observable sequence.

- onCompleted  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Bb534741)\>  
  The action to invoke upon graceful termination of the queryable observable sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
The source sequence with the side-effecting behavior applied.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Do Overload](Do\Qbservable.Do.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Qbservable.Do\<TSource\> Method (IQbservable\<TSource\>, Expression\<Action\<TSource\>\>, Expression\<Action\>)

Invokes an action for each element in the queryable observable sequence, and invokes an action upon graceful or exceptional termination of the queryable observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive.Providers (in System.Reactive.Providers.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Do(Of TSource) ( _
    source As IQbservable(Of TSource), _
    onNext As Expression(Of Action(Of TSource)), _
    onCompleted As Expression(Of Action) _
) As IQbservable(Of TSource)
```

```vb
'Usage
Dim source As IQbservable(Of TSource)
Dim onNext As Expression(Of Action(Of TSource))
Dim onCompleted As Expression(Of Action)
Dim returnValue As IQbservable(Of TSource)

returnValue = source.Do(onNext, onCompleted)
```

```csharp
public static IQbservable<TSource> Do<TSource>(
    this IQbservable<TSource> source,
    Expression<Action<TSource>> onNext,
    Expression<Action> onCompleted
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IQbservable<TSource>^ Do(
    IQbservable<TSource>^ source, 
    Expression<Action<TSource>^>^ onNext, 
    Expression<Action^>^ onCompleted
)
```

```fsharp
static member Do : 
        source:IQbservable<'TSource> * 
        onNext:Expression<Action<'TSource>> * 
        onCompleted:Expression<Action> -> IQbservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
  The source sequence.

- onNext  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>\>  
  The action to invoke for each element in the queryable observable sequence.

- onCompleted  
  Type: [System.Linq.Expressions.Expression](https://msdn.microsoft.com/en-us/library/Bb335710)\<[Action](https://msdn.microsoft.com/en-us/library/Bb534741)\>  
  The action to invoke upon graceful termination of the queryable observable sequence.

#### Return Value

Type: [System.Reactive.Linq.IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>  
The source sequence with the side-effecting behavior applied.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IQbservable](IQbservable\IQbservable(TSource).md)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Qbservable Class](Qbservable\Qbservable.md)

[Do Overload](Do\Qbservable.Do.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








