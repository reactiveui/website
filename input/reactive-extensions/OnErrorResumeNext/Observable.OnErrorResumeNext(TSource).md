# Observable.OnErrorResumeNext\<TSource\> Method (IObservable\<TSource\>, IObservable\<TSource\>)

Continues an observable sequence that is terminated normally or by an exception with the next observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function OnErrorResumeNext(Of TSource) ( _
    first As IObservable(Of TSource), _
    second As IObservable(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim first As IObservable(Of TSource)
Dim second As IObservable(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = first.OnErrorResumeNext(second)
```

```csharp
public static IObservable<TSource> OnErrorResumeNext<TSource>(
    this IObservable<TSource> first,
    IObservable<TSource> second
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ OnErrorResumeNext(
    IObservable<TSource>^ first, 
    IObservable<TSource>^ second
)
```

```fsharp
static member OnErrorResumeNext : 
        first:IObservable<'TSource> * 
        second:IObservable<'TSource> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- first  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The first observable sequence whose exception (if any) is caught.

- second  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  Second observable sequence used to produce results after the first sequence terminates.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence that concatenates the first and second sequence, even if the first sequence terminates exceptionally.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[OnErrorResumeNext Overload](OnErrorResumeNext\Observable.OnErrorResumeNext.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.OnErrorResumeNext\<TSource\> Method (array\<IObservable\<TSource\>\[\])

Continues an observable sequence that is terminated normally or by an exception with the next observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function OnErrorResumeNext(Of TSource) ( _
    ParamArray sources As IObservable(Of TSource)() _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IObservable(Of TSource)()
Dim returnValue As IObservable(Of TSource)

returnValue = Observable.OnErrorResumeNext(sources)
```

```csharp
public static IObservable<TSource> OnErrorResumeNext<TSource>(
    params IObservable<TSource>[] sources
)
```

```c++
public:
generic<typename TSource>
static IObservable<TSource>^ OnErrorResumeNext(
    ... array<IObservable<TSource>^>^ sources
)
```

```fsharp
static member OnErrorResumeNext : 
        sources:IObservable<'TSource>[] -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- sources  
  Type: array\<[System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\[\]  
  The observable sequences to concatenate.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence that concatenates the source sequences, even if a sequence terminates exceptionally.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[OnErrorResumeNext Overload](OnErrorResumeNext\Observable.OnErrorResumeNext.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Observable.OnErrorResumeNext\<TSource\> Method (IEnumerable\<IObservable\<TSource\>\>)

Continues an observable sequence that is terminated normally or by an exception with the next observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function OnErrorResumeNext(Of TSource) ( _
    sources As IEnumerable(Of IObservable(Of TSource)) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IEnumerable(Of IObservable(Of TSource))
Dim returnValue As IObservable(Of TSource)

returnValue = sources.OnErrorResumeNext()
```

```csharp
public static IObservable<TSource> OnErrorResumeNext<TSource>(
    this IEnumerable<IObservable<TSource>> sources
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ OnErrorResumeNext(
    IEnumerable<IObservable<TSource>^>^ sources
)
```

```fsharp
static member OnErrorResumeNext : 
        sources:IEnumerable<IObservable<'TSource>> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- sources  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
  The observable sequences to concatenate.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence that concatenates the source sequences, even if a sequence terminates exceptionally.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[OnErrorResumeNext Overload](OnErrorResumeNext\Observable.OnErrorResumeNext.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








