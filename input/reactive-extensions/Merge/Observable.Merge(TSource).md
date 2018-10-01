# Observable.Merge\<TSource\> Method (IEnumerable\<IObservable\<TSource\>\>, IScheduler)

Merges an enumerable sequence of observable sequences into a single observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Merge(Of TSource) ( _
    sources As IEnumerable(Of IObservable(Of TSource)), _
    scheduler As IScheduler _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IEnumerable(Of IObservable(Of TSource))
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TSource)

returnValue = sources.Merge(scheduler)
```

```csharp
public static IObservable<TSource> Merge<TSource>(
    this IEnumerable<IObservable<TSource>> sources,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Merge(
    IEnumerable<IObservable<TSource>^>^ sources, 
    IScheduler^ scheduler
)
```

```fsharp
static member Merge : 
        sources:IEnumerable<IObservable<'TSource>> * 
        scheduler:IScheduler -> IObservable<'TSource> 
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
  The enumerable sequence of observable sequences.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run the enumeration of the sequence of sources on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence that merges the elements of the observable sequences.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Merge Overload](Merge\Observable.Merge.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Merge\<TSource\> Method (IEnumerable\<IObservable\<TSource\>\>, Int32, IScheduler)

Merges an enumerable sequence of observable sequences into an observable sequence, limiting the number of concurrent subscriptions to inner sequences.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Merge(Of TSource) ( _
    sources As IEnumerable(Of IObservable(Of TSource)), _
    maxConcurrent As Integer, _
    scheduler As IScheduler _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IEnumerable(Of IObservable(Of TSource))
Dim maxConcurrent As Integer
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TSource)

returnValue = sources.Merge(maxConcurrent, _
    scheduler)
```

```csharp
public static IObservable<TSource> Merge<TSource>(
    this IEnumerable<IObservable<TSource>> sources,
    int maxConcurrent,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Merge(
    IEnumerable<IObservable<TSource>^>^ sources, 
    int maxConcurrent, 
    IScheduler^ scheduler
)
```

```fsharp
static member Merge : 
        sources:IEnumerable<IObservable<'TSource>> * 
        maxConcurrent:int * 
        scheduler:IScheduler -> IObservable<'TSource> 
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
  The enumerable sequence of observable sequences.

- maxConcurrent  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum number of observable sequences being subscribed to concurrently.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run the enumeration of the sequence of sources on.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence that merges the elements of the observable sequences.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Merge Overload](Merge\Observable.Merge.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Merge\<TSource\> Method (IEnumerable\<IObservable\<TSource\>\>, Int32)

Merges an enumerable sequence of observable sequences into an observable sequence, limiting the number of concurrent subscriptions to inner sequences.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Merge(Of TSource) ( _
    sources As IEnumerable(Of IObservable(Of TSource)), _
    maxConcurrent As Integer _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IEnumerable(Of IObservable(Of TSource))
Dim maxConcurrent As Integer
Dim returnValue As IObservable(Of TSource)

returnValue = sources.Merge(maxConcurrent)
```

```csharp
public static IObservable<TSource> Merge<TSource>(
    this IEnumerable<IObservable<TSource>> sources,
    int maxConcurrent
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Merge(
    IEnumerable<IObservable<TSource>^>^ sources, 
    int maxConcurrent
)
```

```fsharp
static member Merge : 
        sources:IEnumerable<IObservable<'TSource>> * 
        maxConcurrent:int -> IObservable<'TSource> 
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
  The enumerable sequence of observable sequences.

- maxConcurrent  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum number of observable sequences being subscribed to concurrently.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence that merges the elements of the observable sequences.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Merge Overload](Merge\Observable.Merge.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Merge\<TSource\> Method (IObservable\<IObservable\<TSource\>\>)

Merges an observable sequence of observable sequences into an observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Merge(Of TSource) ( _
    sources As IObservable(Of IObservable(Of TSource)) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IObservable(Of IObservable(Of TSource))
Dim returnValue As IObservable(Of TSource)

returnValue = sources.Merge()
```

```csharp
public static IObservable<TSource> Merge<TSource>(
    this IObservable<IObservable<TSource>> sources
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Merge(
    IObservable<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Merge : 
        sources:IObservable<IObservable<'TSource>> -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- sources  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
  The observable sequence of inner observable sequences.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence that merges the elements of the inner sequences.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Merge Overload](Merge\Observable.Merge.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Merge\<TSource\> Method (IObservable\<TSource\>, IObservable\<TSource\>, IScheduler)

Merges two observable sequences into a single observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Merge(Of TSource) ( _
    first As IObservable(Of TSource), _
    second As IObservable(Of TSource), _
    scheduler As IScheduler _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim first As IObservable(Of TSource)
Dim second As IObservable(Of TSource)
Dim scheduler As IScheduler
Dim returnValue As IObservable(Of TSource)

returnValue = first.Merge(second, _
    scheduler)
```

```csharp
public static IObservable<TSource> Merge<TSource>(
    this IObservable<TSource> first,
    IObservable<TSource> second,
    IScheduler scheduler
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Merge(
    IObservable<TSource>^ first, 
    IObservable<TSource>^ second, 
    IScheduler^ scheduler
)
```

```fsharp
static member Merge : 
        first:IObservable<'TSource> * 
        second:IObservable<'TSource> * 
        scheduler:IScheduler -> IObservable<'TSource> 
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
  The first observable sequence.

- second  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The second observable sequence.

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler used to introduce concurrency for making subscriptions to the given sequences.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence that merges the elements of the given sequences.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Merge Overload](Merge\Observable.Merge.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Merge\<TSource\> Method (IEnumerable\<IObservable\<TSource\>\>)

Merges an enumerable sequence of observable sequences into a single observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Merge(Of TSource) ( _
    sources As IEnumerable(Of IObservable(Of TSource)) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IEnumerable(Of IObservable(Of TSource))
Dim returnValue As IObservable(Of TSource)

returnValue = sources.Merge()
```

```csharp
public static IObservable<TSource> Merge<TSource>(
    this IEnumerable<IObservable<TSource>> sources
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Merge(
    IEnumerable<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Merge : 
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
  The enumerable sequence of observable sequences.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence that merges the elements of the observable sequences.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Merge Overload](Merge\Observable.Merge.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Merge\<TSource\> Method (IObservable\<TSource\>, IObservable\<TSource\>)

Merges an observable sequence of observable sequences into an observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Merge(Of TSource) ( _
    first As IObservable(Of TSource), _
    second As IObservable(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim first As IObservable(Of TSource)
Dim second As IObservable(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = first.Merge(second)
```

```csharp
public static IObservable<TSource> Merge<TSource>(
    this IObservable<TSource> first,
    IObservable<TSource> second
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Merge(
    IObservable<TSource>^ first, 
    IObservable<TSource>^ second
)
```

```fsharp
static member Merge : 
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
  The first observable sequence.

- second  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The second observable sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence that merges the elements of the given sequences.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Merge Overload](Merge\Observable.Merge.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Merge\<TSource\> Method (array\<IObservable\<TSource\>\[\])

Merges all the observable sequences into a single observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Merge(Of TSource) ( _
    ParamArray sources As IObservable(Of TSource)() _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IObservable(Of TSource)()
Dim returnValue As IObservable(Of TSource)

returnValue = Observable.Merge(sources)
```

```csharp
public static IObservable<TSource> Merge<TSource>(
    params IObservable<TSource>[] sources
)
```

```c++
public:
generic<typename TSource>
static IObservable<TSource>^ Merge(
    ... array<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Merge : 
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
  The observable sequences.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence that merges the elements of the observable sequences.

## Remarks

The Merge operator is used to merge multiple observable sequences into a single observable sequence. Various overloads of this operator provide flexibility for specifying the sequences to be merged together. The merged sequence will continue to produce items in the merged sequence until all sequence have run to completion or one of the sequence produces an error.

## Examples

The following example uses the Interval operator to create a sequence of integers starting with 0. A new integer is produced every 500ms. Two filtered sequence are created. One sequence filters the original sequence to produce every third integer. The other sequence filters the original sequence to produce only every 5th integer. The Merge operator is then used to merge these two filtered sequences into a single sequence of integers. A subscription is created for the merged sequence and each item is written to the console window.

    using System;
    using System.Reactive.Linq;
    using System.Reactive.Concurrency;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //*********************************************************************************************//
          //*** Generate a sequence of integers producing a new integer every .5 sec.                 ***//
          //*********************************************************************************************//
    
          var obsInt = Observable.Interval(TimeSpan.FromMilliseconds(500), Scheduler.ThreadPool);
    
    
          //*********************************************************************************************//
          //*** Filter the integer sequence to produce only every 3rd integer.                        ***//
          //*********************************************************************************************//
    
          var obsThrees = obsInt.Where(i => i % 3 == 0);
    
    
          //*********************************************************************************************//
          //*** Filter the integer sequence to produce only every 5th integer.                        ***//
          //*********************************************************************************************//
    
          var obsFives = obsInt.Where(i => i % 5 == 0);
    
    
          //***********************************************************************************************//
          //*** Subscribe to a merged sequence of the two filtered sequences. This merged sequence will ***//
          //*** produce every 3rd and every 5th integer.                                                ***//
          //***********************************************************************************************//
    
          var obs = Observable.Merge(new IObservable<long>[2] {obsThrees, obsFives});
    
          using (IDisposable handle = obs.Subscribe(x => Console.WriteLine(x)))
          {
            Console.WriteLine("Press ENTER to exit...\n");
            Console.ReadLine();
          }
        }
      }
    }

The following output was generated by the example code.

    Press ENTER to exit...
    
    0
    0
    3
    5
    6
    9
    10
    12
    15
    15
    18
    20
    21
    24
    25
    27
    30
    30

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Merge Overload](Merge\Observable.Merge.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)










# Observable.Merge\<TSource\> Method (IScheduler, array\<IObservable\<TSource\>\[\])

Merges all the observable sequences into a single observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Merge(Of TSource) ( _
    scheduler As IScheduler, _
    ParamArray sources As IObservable(Of TSource)() _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim scheduler As IScheduler
Dim sources As IObservable(Of TSource)()
Dim returnValue As IObservable(Of TSource)

returnValue = Observable.Merge(scheduler, _
    sources)
```

```csharp
public static IObservable<TSource> Merge<TSource>(
    IScheduler scheduler,
    params IObservable<TSource>[] sources
)
```

```c++
public:
generic<typename TSource>
static IObservable<TSource>^ Merge(
    IScheduler^ scheduler, 
    ... array<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Merge : 
        scheduler:IScheduler * 
        sources:IObservable<'TSource>[] -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- scheduler  
  Type: [System.Reactive.Concurrency.IScheduler](IScheduler\IScheduler.md)  
  The scheduler to run the enumeration of the sequence of sources on.

- sources  
  Type: array\<[System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\[\]  
  The observable sequences.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
Observable sequence that merges the elements of the observable sequences.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Merge Overload](Merge\Observable.Merge.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








# Observable.Merge\<TSource\> Method (IObservable\<IObservable\<TSource\>\>, Int32)

Merges an enumerable sequence of observable sequences into an observable sequence, limiting the number of concurrent subscriptions to inner sequences.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Merge(Of TSource) ( _
    sources As IObservable(Of IObservable(Of TSource)), _
    maxConcurrent As Integer _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IObservable(Of IObservable(Of TSource))
Dim maxConcurrent As Integer
Dim returnValue As IObservable(Of TSource)

returnValue = sources.Merge(maxConcurrent)
```

```csharp
public static IObservable<TSource> Merge<TSource>(
    this IObservable<IObservable<TSource>> sources,
    int maxConcurrent
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Merge(
    IObservable<IObservable<TSource>^>^ sources, 
    int maxConcurrent
)
```

```fsharp
static member Merge : 
        sources:IObservable<IObservable<'TSource>> * 
        maxConcurrent:int -> IObservable<'TSource> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

#### Parameters

- sources  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
  The observable sequence of inner observable sequences.

- maxConcurrent  
  Type: [System.Int32](https://msdn.microsoft.com/en-us/library/td2s409d)  
  The maximum number of observable sequences being subscribed to concurrently.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
The observable sequence that merges the elements of the inner sequences.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Merge Overload](Merge\Observable.Merge.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)








