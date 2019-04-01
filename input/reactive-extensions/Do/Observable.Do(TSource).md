# Observable.Do\<TSource\> Method (IObservable\<TSource\>, Action\<TSource\>, Action\<Exception\>, Action)

Invokes an action for each element in the observable sequence, and invokes an action upon graceful or exceptional termination of the observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Do(Of TSource) ( _
    source As IObservable(Of TSource), _
    onNext As Action(Of TSource), _
    onError As Action(Of Exception), _
    onCompleted As Action _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim onNext As Action(Of TSource)
Dim onError As Action(Of Exception)
Dim onCompleted As Action
Dim returnValue As IObservable(Of TSource)

returnValue = source.Do(onNext, onError, _
    onCompleted)
```

```csharp
public static IObservable<TSource> Do<TSource>(
    this IObservable<TSource> source,
    Action<TSource> onNext,
    Action<Exception> onError,
    Action onCompleted
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Do(
    IObservable<TSource>^ source, 
    Action<TSource>^ onNext, 
    Action<Exception^>^ onError, 
    Action^ onCompleted
)
```

```fsharp
static member Do : 
        source:IObservable<'TSource> * 
        onNext:Action<'TSource> * 
        onError:Action<Exception> * 
        onCompleted:Action -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence.

- onNext  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>  
  The action to invoke for each element in the observable sequence.

- onError  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)\>  
  The action to invoke upon exceptional termination of the observable sequence.

- onCompleted  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb534741)  
  The action to invoke upon graceful termination of the observable sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The source sequence with the side-effecting behavior applied.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Do Overload](Do\Observable.Do.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Do\<TSource\> Method (IObservable\<TSource\>, Action\<TSource\>)

Invokes an action for each element in the observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Do(Of TSource) ( _
    source As IObservable(Of TSource), _
    onNext As Action(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim onNext As Action(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = source.Do(onNext)
```

```csharp
public static IObservable<TSource> Do<TSource>(
    this IObservable<TSource> source,
    Action<TSource> onNext
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Do(
    IObservable<TSource>^ source, 
    Action<TSource>^ onNext
)
```

```fsharp
static member Do : 
        source:IObservable<'TSource> * 
        onNext:Action<'TSource> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence.

- onNext  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>  
  The action to invoke for each element in the observable sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The source sequence with the side-effecting behavior applied.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Do Overload](Do\Observable.Do.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Do\<TSource\> Method (IObservable\<TSource\>, Action\<TSource\>, Action)

Invokes an action for each element in the observable sequence and invokes an action upon graceful termination of the observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Do(Of TSource) ( _
    source As IObservable(Of TSource), _
    onNext As Action(Of TSource), _
    onCompleted As Action _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim onNext As Action(Of TSource)
Dim onCompleted As Action
Dim returnValue As IObservable(Of TSource)

returnValue = source.Do(onNext, onCompleted)
```

```csharp
public static IObservable<TSource> Do<TSource>(
    this IObservable<TSource> source,
    Action<TSource> onNext,
    Action onCompleted
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Do(
    IObservable<TSource>^ source, 
    Action<TSource>^ onNext, 
    Action^ onCompleted
)
```

```fsharp
static member Do : 
        source:IObservable<'TSource> * 
        onNext:Action<'TSource> * 
        onCompleted:Action -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence.

- onNext  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>  
  The action to invoke for each element in the observable sequence.

- onCompleted  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb534741)  
  The action to invoke upon graceful termination of the observable sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The source sequence with the side-effecting behavior applied.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Do Overload](Do\Observable.Do.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Do\<TSource\> Method (IObservable\<TSource\>, Action\<TSource\>, Action\<Exception\>)

Invokes an action for each element in the observable sequence and invokes an action upon exceptional termination of the observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Do(Of TSource) ( _
    source As IObservable(Of TSource), _
    onNext As Action(Of TSource), _
    onError As Action(Of Exception) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim onNext As Action(Of TSource)
Dim onError As Action(Of Exception)
Dim returnValue As IObservable(Of TSource)

returnValue = source.Do(onNext, onError)
```

```csharp
public static IObservable<TSource> Do<TSource>(
    this IObservable<TSource> source,
    Action<TSource> onNext,
    Action<Exception> onError
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Do(
    IObservable<TSource>^ source, 
    Action<TSource>^ onNext, 
    Action<Exception^>^ onError
)
```

```fsharp
static member Do : 
        source:IObservable<'TSource> * 
        onNext:Action<'TSource> * 
        onError:Action<Exception> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence.

- onNext  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>  
  The action to invoke for each element in the observable sequence.

- onError  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)\>  
  The action to invoke upon exceptional termination of the observable sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The source sequence with the side-effecting behavior applied.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Do Overload](Do\Observable.Do.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Do\<TSource\> Method (IObservable\<TSource\>, IObserver\<TSource\>)

Invokes an action for each element in the observable sequence and invokes an action upon exceptional termination of the observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Do(Of TSource) ( _
    source As IObservable(Of TSource), _
    observer As IObserver(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim observer As IObserver(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = source.Do(observer)
```

```csharp
public static IObservable<TSource> Do<TSource>(
    this IObservable<TSource> source,
    IObserver<TSource> observer
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Do(
    IObservable<TSource>^ source, 
    IObserver<TSource>^ observer
)
```

```fsharp
static member Do : 
        source:IObservable<'TSource> * 
        observer:IObserver<'TSource> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence.

- observer  
  Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<TSource\>  
  The observer whose methods to invoke as part of the source sequence's observation.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The source sequence with the side-effecting behavior applied.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Do Overload](Do\Observable.Do.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








