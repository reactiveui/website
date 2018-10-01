# Observable.Amb\<TSource\> Method (array\<IObservable\<TSource\>\[\])

Propagates the observable sequence that reacts first with a specified sources.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared Function Amb(Of TSource) ( _
    ParamArray sources As IObservable(Of TSource)() _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IObservable(Of TSource)()
Dim returnValue As IObservable(Of TSource)

returnValue = Observable.Amb(sources)
```

```csharp
public static IObservable<TSource> Amb<TSource>(
    params IObservable<TSource>[] sources
)
```

```c++
public:
generic<typename TSource>
static IObservable<TSource>^ Amb(
    ... array<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Amb : 
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
  The observable sources competing to react first.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence that surfaces any of the given sequences, whichever reacted first.

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Amb Overload](Amb\Observable.Amb.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Amb\<TSource\> Method (IEnumerable\<IObservable\<TSource\>\>)

Propagates the observable sequence that reacts first with a specified sources.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Amb(Of TSource) ( _
    sources As IEnumerable(Of IObservable(Of TSource)) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim sources As IEnumerable(Of IObservable(Of TSource))
Dim returnValue As IObservable(Of TSource)

returnValue = sources.Amb()
```

```csharp
public static IObservable<TSource> Amb<TSource>(
    this IEnumerable<IObservable<TSource>> sources
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Amb(
    IEnumerable<IObservable<TSource>^>^ sources
)
```

```fsharp
static member Amb : 
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
  The observable sources competing to react first.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence that surfaces any of the given sequences, whichever reacted first.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<[IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Amb Overload](Amb\Observable.Amb.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)

# Observable.Amb\<TSource\> Method (IObservable\<TSource\>, IObservable\<TSource\>)

Propagates the observable sequence that reacts first with the specified first and second sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Amb(Of TSource) ( _
    first As IObservable(Of TSource), _
    second As IObservable(Of TSource) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim first As IObservable(Of TSource)
Dim second As IObservable(Of TSource)
Dim returnValue As IObservable(Of TSource)

returnValue = first.Amb(second)
```

```csharp
public static IObservable<TSource> Amb<TSource>(
    this IObservable<TSource> first,
    IObservable<TSource> second
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<TSource>^ Amb(
    IObservable<TSource>^ first, 
    IObservable<TSource>^ second
)
```

```fsharp
static member Amb : 
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
The observable sequence which responded first out of the set of given sequences.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

The Amb operator’s name is short for “ambiguous”. The Amb operator takes two or more sequences and returns the first sequence to respond. The Amb operator uses parallel processing to detect which sequence yields the first item.

Amb can be called as an extension method as shown in the code example in this topic or it can be called as static method. Based on the code example given in this topic, the following snippet demonstrates how to call Amb a static method.

```
      int[] sequence1 = { 1, 2, 3 };
      int[] sequence2 = { 4, 5, 6 };
      int[] sequence3 = { 7, 8, 9 };

      //*** The first item in the sequence1 event stream is delayed by 3 seconds. ***//
      IObservable<int> firstSource = sequence1.ToObservable().Delay(TimeSpan.FromSeconds(3));

      //*** The first event in the sequence2 event stream is only delayed by 2 seconds. ***//
      IObservable<int> secondSource = sequence2.ToObservable().Delay(TimeSpan.FromSeconds(2));

      //*** The first event in the sequence3 event stream is only delayed by 1 second.  ***//
      IObservable<int> thirdSource = sequence3.ToObservable().Delay(TimeSpan.FromSeconds(1));


      //*****************************************************************************************//
      //***                                                                                   ***//
      //*** The Amb operator will simply return the observable sequence which responds first. ***//
      //*** The other sequence will be ignored.                                               ***//
      //***                                                                                   ***//
      //*** In this example "thirdSource", which contains sequence3, will respond before      ***//
      //*** "firstSource" and "secondSource". So "thirdSource" will be the observable         ***//
      //*** sequence returned from the Amb operator. It will be subscribed to and written     ***//
      //*** to the console.                                                                   ***//
      //***                                                                                   ***//
      //*****************************************************************************************//


      //*** The static method allows the Amb operator to process more than two sequences ***//
      using (IDisposable handle = Observable.Amb(firstSource, secondSource, thirdSource).Subscribe(value => Console.WriteLine(value)))
      {
        Console.WriteLine("\nPress ENTER to exit...\n");
        Console.ReadLine();
      }
```

Amb can also be called as an extension method for more than two sequences. To use this approach, create a sequence of sequences. The following code snippet demonstrates this.

```
      int[] sequence1 = { 1, 2, 3 };
      int[] sequence2 = { 4, 5, 6 };
      int[] sequence3 = { 7, 8, 9 };

      //*** The first item in the sequence1 event stream is delayed by 3 seconds. ***//
      IObservable<int> firstSource = sequence1.ToObservable().Delay(TimeSpan.FromSeconds(3));

      //*** The first event in the sequence2 event stream is only delayed by 2 seconds. ***//
      IObservable<int> secondSource = sequence2.ToObservable().Delay(TimeSpan.FromSeconds(2));

      //*** The first event in the sequence3 event stream is only delayed by 1 second.  ***//
      IObservable<int> thirdSource = sequence3.ToObservable().Delay(TimeSpan.FromSeconds(1));


      //*****************************************************************************************//
      //***                                                                                   ***//
      //*** The Amb operator will simply return the observable sequence which responds first. ***//
      //*** The other sequence will be ignored.                                               ***//
      //***                                                                                   ***//
      //*** In this example "thirdSource", which contains sequence3, will respond before      ***//
      //*** "firstSource" and "secondSource". So "thirdSource" will be the observable         ***//
      //*** sequence returned from the Amb operator. It will be subscribed to and written     ***//
      //*** to the console.                                                                   ***//
      //***                                                                                   ***//
      //*****************************************************************************************//


      //*** Call the extension method on a sequence of any number of sequences. ***//
      IObservable<int>[] sources = new[] { firstSource, secondSource, thirdSource };
      using(IDisposable handle = sources.Amb().Subscribe(value => Console.WriteLine(value)))
      {
        Console.WriteLine("\nPress ENTER to exit...\n");
        Console.ReadLine();
      }
```

## Examples

The following example demonstrates the Amb operator by applying it with two sequences of integers. The delivery of the integers in the first sequence is delayed by three seconds. The delivery of the integers in the second sequence is only delayed by two seconds. So the second sequence responds first and is the result of the Amb operator as shown.

    using System;
    using System.Reactive.Linq;
    
    namespace Example
    {
    
      class Program
      {
    
        static void Main()
        {
          int[] sequence1 = { 1, 2, 3 };
          int[] sequence2 = { 4, 5, 6 };
    
          //*** The first event in observable sequence1 is delayed by 3 seconds. ***//
          IObservable<int> firstSource = sequence1.ToObservable().Delay(TimeSpan.FromSeconds(3));
    
          //*** The first event in observable sequence2 is only delayed by 2 seconds. ***//
          IObservable<int> secondSource = sequence2.ToObservable().Delay(TimeSpan.FromSeconds(2));
    
    
          //*****************************************************************************************//
          //***                                                                                   ***//
          //*** The Amb operator will simply return the observable sequence which responds first. ***//
          //*** The other sequence will be ignored.                                               ***//
          //***                                                                                   ***//
          //*** In this example "secondSource", which contains sequence2, will respond before     ***//
          //*** "firstSource". So "secondSource" will be the observable sequence returned from    ***//
          //*** the Amb operator.                                                                 ***//
          //***                                                                                   ***//
          //*****************************************************************************************//
    
          using (IDisposable handle = firstSource.Amb(secondSource).Subscribe(value => Console.WriteLine(value)))
          {
            Console.WriteLine("Press Enter to exit...\n");
            Console.ReadLine();
          }
        }
      }
    }

The output from the example code is as follows.

    Press Enter to exit...
    
    4
    5
    6

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Amb Overload](Amb\Observable.Amb.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
