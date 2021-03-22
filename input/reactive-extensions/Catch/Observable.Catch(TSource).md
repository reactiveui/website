title: Observable.Catch<TSource>(IEnumerable<IObservable<TSource>>)
---
# Observable.Catch\<TSource\> Method (IEnumerable\<IObservable\<TSource\>\>)

Continues an observable sequence that is terminated by an exception with the next observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Catch(Of TSource) ( _
    sources As IEnumerable(Of IObservable(Of TSource)) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IEnumerable(Of IObservable(Of TSource))
Dim returnValue As IObservable(Of TSource)

returnValue = sources.Catch()
```

```csharp
public static IObservable<TSource> Catch<TSource>(
    this IEnumerable<IObservable<TSource>> sources
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Catch(
    IEnumerable<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Catch : 
        sources:IEnumerable<IObservable<'TSource>> -> IObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- sources  
  Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
  The observable sequences to catch exceptions for.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence containing elements from consecutive source sequences until a source sequence terminates successfully.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Catch Overload](Catch/Observable.Catch)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)









# Observable.Catch\<TSource\> Method (array\<IObservable\<TSource\>\[\])

Continues an observable sequence that is terminated by an exception with the next observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Catch(Of TSource) ( _
    ParamArray sources As IObservable(Of TSource)() _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IObservable(Of TSource)()
Dim returnValue As IObservable(Of TSource)

returnValue = Observable.Catch(sources)
```

```csharp
public static IObservable<TSource> Catch<TSource>(
    params IObservable<TSource>[] sources
)
```

```c++
public:
generic<typename TSource>
static IObservable<TSource>^ Catch(
    ... array<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Catch : 
        sources:IObservable<'TSource>[] -> IObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- sources  
  Type: array\<[System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\[\]  
  The observable sequences to catch exceptions for.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence containing elements from consecutive source sequences until a source sequence terminates successfully.

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Catch Overload](Catch/Observable.Catch)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








# Observable.Catch\<TSource\> Method (IObservable\<TSource\>, IObservable\<TSource\>)

Continues an observable sequence that is terminated by an exception with the next observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Catch(Of TSource) ( _
    first As IObservable(Of TSource), _
    second As IObservable(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim first As IObservable(Of TSource)
Dim second As IObservable(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = first.Catch(second)
```

```csharp
public static IObservable<TSource> Catch<TSource>(
    this IObservable<TSource> first,
    IObservable<TSource> second
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Catch(
    IObservable<TSource>^ first, 
    IObservable<TSource>^ second
)
```

```fsharp
static member Catch : 
        first:IObservable<'TSource> * 
        second:IObservable<'TSource> -> IObservable<'TSource> 
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- first  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  First observable sequence whose exception (if any) is caught.

- second  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The second observable sequence used to produce results when an error occurred in the first sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence containing the first sequence's elements, followed by the elements of the second sequence in case an exception occurred.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Catch Overload](Catch/Observable.Catch)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)








