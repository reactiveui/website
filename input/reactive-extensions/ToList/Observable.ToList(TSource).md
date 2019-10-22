title: Observable.ToList<TSource>()
---
# Observable.ToList\<TSource\> Method

Creates a list from an observable sequence.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function ToList(Of TSource) ( _
    source As IObservable(Of TSource) _
) As IObservable(Of IList(Of TSource))
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim returnValue As IObservable(Of IList(Of TSource))

returnValue = source.ToList()
```

```csharp
public static IObservable<IList<TSource>> ToList<TSource>(
    this IObservable<TSource> source
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource>
static IObservable<IList<TSource>^>^ ToList(
    IObservable<TSource>^ source
)
```

```fsharp
static member ToList : 
        source:IObservable<'TSource> -> IObservable<IList<'TSource>> 
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
  The source observable sequence to get a list of elements for.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<[IList](https://msdn.microsoft.com/en-us/library/5y536ey6)\<TSource\>\>  
A list from an observable sequence.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

The ToList operator takes all the items in the sequence and puts them in a list. Then the list is returned as an observable sequence (IObservable\<IList\<TSource\>\>). The return value of this operator differs from the corresponding operator on IEnumerable in order to retain asynchronous behavior.

## Examples

The following example uses the Generate operator to generate a simple sequence of the integers (1-10). Then, the ToList operator is used to convert that sequence to a list. The IList.Add method is used to 9999 to the resulting list before each item in the list is written to the console window.

    using System;
    using System.Reactive.Linq;
    using System.Collections;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //*********************************************//
          //*** Generate a sequence of integers 1-10  ***//
          //*********************************************//
    
          var obs = Observable.Generate(1,            // Initial state value
                                        x => x <= 10, // The termination condition. Terminate generation when false (the integer squared is not less than 1000)
                                        x => ++x,     // Iteration step function updates the state and returns the new state. In this case state is incremented by 1 
                                        x => x);      // Selector function determines the next resulting value in the sequence. The state of type in is squared.
    
    
          //***************************************************************************************************//
          //*** Convert the integer sequence to a list. Use the IList.Add() method to add 9999 to the list  ***//
          //***************************************************************************************************//
    
          var obsList = obs.ToList();
    
          obsList.Subscribe(x => 
          {
            x.Add(9999);
    
            //****************************************//
            //*** Enumerate the items in the list  ***//
            //****************************************//
    
            foreach (int val in x)
            {
              Console.WriteLine(val);
            }
          });
    
          Console.WriteLine("\nPress ENTER to exit...\n");
          Console.ReadLine();
        }
      }
    }

The following output was generated with the example code.

    1
    2
    3
    4
    5
    6
    7
    8
    9
    10
    9999
    
    Press ENTER to exit...

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)
