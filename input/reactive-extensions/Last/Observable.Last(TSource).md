title: Observable.Last<TSource>(IObservable<TSource>, Func<TSource, Boolean>)
---
# Observable.Last\<TSource\> Method (IObservable\<TSource\>, Func\<TSource, Boolean\>)

Returns the last element of an observable sequence that matches the predicate.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Last(Of TSource) ( _
    source As IObservable(Of TSource), _
    predicate As Func(Of TSource, Boolean) _
) As TSource
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim predicate As Func(Of TSource, Boolean)
Dim returnValue As TSource

returnValue = source.Last(predicate)
```

```csharp
public static TSource Last<TSource>(
    this IObservable<TSource> source,
    Func<TSource, bool> predicate
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static TSource Last(
    IObservable<TSource>^ source, 
    Func<TSource, bool>^ predicate
)
```

```fsharp
static member Last : 
        source:IObservable<'TSource> * 
        predicate:Func<'TSource, bool> -> 'TSource 
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
  The source observable sequence.

- predicate  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TSource, [Boolean](https://msdn.microsoft.com/en-us/library/a28wyd50)\>  
  A predicate function to evaluate for elements in the sequence.

#### Return Value

Type: TSource  
The last element in the observable sequence for which the predicate holds.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Last Overload](Last/Observable.Last)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Observable.Last\<TSource\> Method (IObservable\<TSource\>)

Returns the last element of an observable sequence with a specified source.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Last(Of TSource) ( _
    source As IObservable(Of TSource) _
) As TSource
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As TSource

returnValue = source.Last()
```

```csharp
public static TSource Last<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static TSource Last(
    IObservable<TSource>^ source
)
```

```fsharp
static member Last : 
        source:IObservable<'TSource> -> 'TSource 
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
  The source observable sequence.

#### Return Value

Type: TSource  
The last element in the observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Last Overload](Last/Observable.Last)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








