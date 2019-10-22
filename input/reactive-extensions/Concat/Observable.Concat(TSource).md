title: Observable.Concat<TSource>(IObservable<IObservable<TSource>>)
---
# Observable.Concat\<TSource\> Method (IObservable\<IObservable\<TSource\>\>)

Concatenates an observable sequence of observable sequences.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Concat(Of TSource) ( _
    sources As IObservable(Of IObservable(Of TSource)) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IObservable(Of IObservable(Of TSource))
Dim returnValue As IObservable(Of TSource)

returnValue = sources.Concat()
```

```csharp
public static IObservable<TSource> Concat<TSource>(
    this IObservable<IObservable<TSource>> sources
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Concat(
    IObservable<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Concat : 
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
An observable sequence that contains the elements of each observed inner sequence, in sequential order.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Concat Overload](Concat\Observable.Concat.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Concat\<TSource\> Method (IObservable\<TSource\>, IObservable\<TSource\>)

Concatenates two observable sequences.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Concat(Of TSource) ( _
    first As IObservable(Of TSource), _
    second As IObservable(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim first As IObservable(Of TSource)
Dim second As IObservable(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = first.Concat(second)
```

```csharp
public static IObservable<TSource> Concat<TSource>(
    this IObservable<TSource> first,
    IObservable<TSource> second
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Concat(
    IObservable<TSource>^ first, 
    IObservable<TSource>^ second
)
```

```fsharp
static member Concat : 
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
An observable sequence that contains the elements of the first sequence, followed by those of the second the sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

The Concat operator runs the first sequence to completion. Then, the second sequence is run to completion effectively concatenating the second sequence to the end of the first sequence. Other overloads of this operator allow concatenating more than two observable sequences. When calling the Concat operator as an extension method, the **first** parameter is the sequence from which the extension method is being executed. This is demonstrated in the code example in this topic. The sequence passed to the operator as the **second** parameter will be concatenated to the first sequence only when the first sequence runs to completion. So it is important to note that the subscription to the second sequence is deferred until the first sequence runs to its completion. If it does not run to completion because of an exception or being blocked, then a subscription to the second sequence will not be created.

## Examples

This example code demonstrates concatenating two sequences of integers to produce a sequence of the integers 1-6. Each integer is written to the console window.

    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    
    namespace Example
    {
    
      class Program
      {
    
        static void Main()
        {
          int[] sequence1 = {1, 2, 3};
          int[] sequence2 = {4, 5, 6};
          
          //*** Create a new observable sequence of integers by concatenating sequence2 to sequence1 ***//
          IObservable<int> sequences = sequence1.ToObservable().Concat(sequence2.ToObservable());
    
          //*** The event handler for the subscription will just write each integer from the sequence to the console window. ***//
          sequences.Subscribe(i => Console.WriteLine(i));
    
          Console.ReadLine();     
        }
      }
    }

The output from the example code is shown below.

    1
    2
    3
    4
    5
    6

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Concat Overload](Concat\Observable.Concat.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)











# Observable.Concat\<TSource\> Method (IEnumerable\<IObservable\<TSource\>\>)

Concatenates an enumerable sequence of observable sequences.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Concat(Of TSource) ( _
    sources As IEnumerable(Of IObservable(Of TSource)) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IEnumerable(Of IObservable(Of TSource))
Dim returnValue As IObservable(Of TSource)

returnValue = sources.Concat()
```

```csharp
public static IObservable<TSource> Concat<TSource>(
    this IEnumerable<IObservable<TSource>> sources
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Concat(
    IEnumerable<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Concat : 
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
An observable sequence that contains the elements of each given sequence, in sequential order.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Concat Overload](Concat\Observable.Concat.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)









# Observable.Concat\<TSource\> Method (array\<IObservable\<TSource\>\[\])

Concatenates all the observable sequences.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Concat(Of TSource) ( _
    ParamArray sources As IObservable(Of TSource)() _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IObservable(Of TSource)()
Dim returnValue As IObservable(Of TSource)

returnValue = Observable.Concat(sources)
```

```csharp
public static IObservable<TSource> Concat<TSource>(
    params IObservable<TSource>[] sources
)
```

```c++
public:
generic<typename TSource>
static IObservable<TSource>^ Concat(
    ... array<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Concat : 
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
An observable sequence that contains the elements of each given sequence, in sequential order.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Concat Overload](Concat\Observable.Concat.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)







