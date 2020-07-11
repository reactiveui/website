title: Observable.ToEnumerable<TSource>()
---
# Observable.ToEnumerable\<TSource\> Method

Converts an observable sequence to an enumerable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToEnumerable(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IEnumerable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IEnumerable(Of TSource)

returnValue = source.ToEnumerable()
```

```csharp
public static IEnumerable<TSource> ToEnumerable<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IEnumerable<TSource>^ ToEnumerable(
    IObservable<TSource>^ source
)
```

```fsharp
static member ToEnumerable : 
        source:IObservable<'TSource> -> IEnumerable<'TSource> 
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
  An observable sequence to convert to an enumerable sequence.

#### Return Value

Type: [System.Collections.Generic.IEnumerable](https://msdn.microsoft.com/en-us/library/9eekhta0)\<TSource\>  
The enumerable sequence containing the elements in the observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

The ToEnumerator operator returns an enumerator from an observable sequence. The enumerator will yield each item in the sequence as it is produced.

## Examples

The following example creates an observable sequence of the integers. A new integer is produced in the sequence every second by the Interval operator. The observable sequence is converted to an enumerator and each item is written to the console window as it is produced.

    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //******************************************************//
          //*** Create an observable sequence of integers.     ***//
          //******************************************************//
    
          var obs = Observable.Interval(TimeSpan.FromSeconds(1));
      
    
          //*******************************************************//
          //*** Convert the integer sequence to an enumerable.  ***//
          //*******************************************************//
    
          var intEnumerable = obs.ToEnumerable();
    
    
          //*********************************************************************************************//
          //*** Create a task to enumerate the items in the list on a worker thread to allow the main ***//
          //*** thread to process the user's ENTER key press.                                         ***//
          //*********************************************************************************************//
    
          Task.Factory.StartNew(() =>
          {
            foreach (int val in intEnumerable)
            {
              Console.WriteLine(val);
            }
          });
    
    
          //*********************************************************************************************//
          //*** Main thread waiting on the user's ENTER key press.                                    ***//
          //*********************************************************************************************//
    
          Console.WriteLine("\nPress ENTER to exit...\n");
          Console.ReadLine();
        }
      }
    }

The following output was generated with the example code.

```
 
Press ENTER to exit...

0
1
2
3
4
5
6
7
8
9
```

## See Also

#### Reference

[Observable Class](Observable/Observable)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)
