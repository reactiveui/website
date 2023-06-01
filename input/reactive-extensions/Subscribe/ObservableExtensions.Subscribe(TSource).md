title: ObservableExtensions.Subscribe<TSource>(IObservable<TSource>, Action<TSource>, Action)
---
# ObservableExtensions.Subscribe\<TSource\> Method (IObservable\<TSource\>, Action\<TSource\>, Action)

Subscribes an element handler and a completion handler to an observable sequence.

**Namespace:**  [System](System/System)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Subscribe(Of TSource) ( _
    source As IObservable(Of TSource), _
    onNext As Action(Of TSource), _
    onCompleted As Action _
) As IDisposable
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim onNext As Action(Of TSource)
Dim onCompleted As Action
Dim returnValue As IDisposable

returnValue = source.Subscribe(onNext, _
    onCompleted)
```

```csharp
public static IDisposable Subscribe<TSource>(
    this IObservable<TSource> source,
    Action<TSource> onNext,
    Action onCompleted
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IDisposable^ Subscribe(
    IObservable<TSource>^ source, 
    Action<TSource>^ onNext, 
    Action^ onCompleted
)
```

```fsharp
static member Subscribe : 
        source:IObservable<'TSource> * 
        onNext:Action<'TSource> * 
        onCompleted:Action -> IDisposable 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  Observable sequence to subscribe to.

- onNext  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>  
  Action to invoke for each element in the observable sequence.

- onCompleted  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb534741)  
  Action to invoke upon graceful termination of the observable sequence.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
IDisposable object used to unsubscribe from the observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[ObservableExtensions Class](ObservableExtensions/ObservableExtensions)

[Subscribe Overload](Subscribe/ObservableExtensions.Subscribe)

[System Namespace](System/System)

# ObservableExtensions.Subscribe\<TSource\> Method (IObservable\<TSource\>)

Evaluates the observable sequence with a specified source.

**Namespace:**  [System](System/System)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Subscribe(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IDisposable
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IDisposable

returnValue = source.Subscribe()
```

```csharp
public static IDisposable Subscribe<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IDisposable^ Subscribe(
    IObservable<TSource>^ source
)
```

```fsharp
static member Subscribe : 
        source:IObservable<'TSource> -> IDisposable 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  Observable sequence to subscribe to.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
IDisposable object used to unsubscribe from the observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[ObservableExtensions Class](ObservableExtensions/ObservableExtensions)

[Subscribe Overload](Subscribe/ObservableExtensions.Subscribe)

[System Namespace](System/System)

# ObservableExtensions.Subscribe\<TSource\> Method (IObservable\<TSource\>, Action\<TSource\>, Action\<Exception\>, Action)

Subscribes an element handler, an exception handler, and a completion handler to an observable sequence.

**Namespace:**  [System](System/System)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Subscribe(Of TSource) ( _
    source As IObservable(Of TSource), _
    onNext As Action(Of TSource), _
    onError As Action(Of Exception), _
    onCompleted As Action _
) As IDisposable
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim onNext As Action(Of TSource)
Dim onError As Action(Of Exception)
Dim onCompleted As Action
Dim returnValue As IDisposable

returnValue = source.Subscribe(onNext, _
    onError, onCompleted)
```

```csharp
public static IDisposable Subscribe<TSource>(
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
static IDisposable^ Subscribe(
    IObservable<TSource>^ source, 
    Action<TSource>^ onNext, 
    Action<Exception^>^ onError, 
    Action^ onCompleted
)
```

```fsharp
static member Subscribe : 
        source:IObservable<'TSource> * 
        onNext:Action<'TSource> * 
        onError:Action<Exception> * 
        onCompleted:Action -> IDisposable 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  Observable sequence to subscribe to.

- onNext  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>  
  Action to invoke for each element in the observable sequence.

- onError  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)\>  
  Action to invoke upon exceptional termination of the observable sequence.

- onCompleted  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/Bb534741)  
  Action to invoke upon graceful termination of the observable sequence.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
IDisposable object used to unsubscribe from the observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[ObservableExtensions Class](ObservableExtensions/ObservableExtensions)

[Subscribe Overload](Subscribe/ObservableExtensions.Subscribe)

[System Namespace](System/System)

# ObservableExtensions.Subscribe\<TSource\> Method (IObservable\<TSource\>, Action\<TSource\>)

Subscribes an element handler to an observable sequence.

**Namespace:**  [System](System/System)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Subscribe(Of TSource) ( _
    source As IObservable(Of TSource), _
    onNext As Action(Of TSource) _
) As IDisposable
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim onNext As Action(Of TSource)
Dim returnValue As IDisposable

returnValue = source.Subscribe(onNext)
```

```csharp
public static IDisposable Subscribe<TSource>(
    this IObservable<TSource> source,
    Action<TSource> onNext
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IDisposable^ Subscribe(
    IObservable<TSource>^ source, 
    Action<TSource>^ onNext
)
```

```fsharp
static member Subscribe : 
        source:IObservable<'TSource> * 
        onNext:Action<'TSource> -> IDisposable 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  Observable sequence to subscribe to.

- onNext  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>  
  Action to invoke for each element in the observable sequence.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
IDisposable object used to unsubscribe from the observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[ObservableExtensions Class](ObservableExtensions/ObservableExtensions)

[Subscribe Overload](Subscribe/ObservableExtensions.Subscribe)

[System Namespace](System/System)

# ObservableExtensions.Subscribe\<TSource\> Method (IObservable\<TSource\>, Action\<TSource\>, Action\<Exception\>)

Subscribes an element handler and an exception handler to an observable sequence.

**Namespace:**  [System](System/System)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Subscribe(Of TSource) ( _
    source As IObservable(Of TSource), _
    onNext As Action(Of TSource), _
    onError As Action(Of Exception) _
) As IDisposable
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim onNext As Action(Of TSource)
Dim onError As Action(Of Exception)
Dim returnValue As IDisposable

returnValue = source.Subscribe(onNext, _
    onError)
```

```csharp
public static IDisposable Subscribe<TSource>(
    this IObservable<TSource> source,
    Action<TSource> onNext,
    Action<Exception> onError
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IDisposable^ Subscribe(
    IObservable<TSource>^ source, 
    Action<TSource>^ onNext, 
    Action<Exception^>^ onError
)
```

```fsharp
static member Subscribe : 
        source:IObservable<'TSource> * 
        onNext:Action<'TSource> * 
        onError:Action<Exception> -> IDisposable 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  Observable sequence to subscribe to.

- onNext  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<TSource\>  
  Action to invoke for each element in the observable sequence.

- onError  
  Type: [System.Action](https://msdn.microsoft.com/en-us/library/018hxwa8)\<[Exception](https://msdn.microsoft.com/en-us/library/c18k6c59)\>  
  Action to invoke upon exceptional termination of the observable sequence.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
IDisposable object used to unsubscribe from the observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[ObservableExtensions Class](ObservableExtensions/ObservableExtensions)

[Subscribe Overload](Subscribe/ObservableExtensions.Subscribe)

[System Namespace](System/System)
