title: Observable.Catch<TSource, TException>(IObservable<TSource>, Func<TException, IObservable<TSource>>)
---
# Observable.Catch\<TSource, TException\> Method (IObservable\<TSource\>, Func\<TException, IObservable\<TSource\>\>)

Continues an observable sequence that is terminated by an exception of the specified type with the observable sequence produced by the handler.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Catch(Of TSource, TException As Exception) ( _
    source As IObservable(Of TSource), _
    handler As Func(Of TException, IObservable(Of TSource)) _
) As IObservable(Of TSource)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim handler As Func(Of TException, IObservable(Of TSource))
Dim returnValue As IObservable(Of TSource)

returnValue = source.Catch(handler)
```

```csharp
public static IObservable<TSource> Catch<TSource, TException>(
    this IObservable<TSource> source,
    Func<TException, IObservable<TSource>> handler
)
where TException : Exception
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TException>
where TException : Exception
static IObservable<TSource>^ Catch(
    IObservable<TSource>^ source, 
    Func<TException, IObservable<TSource>^>^ handler
)
```

```fsharp
static member Catch : 
        source:IObservable<'TSource> * 
        handler:Func<'TException, IObservable<'TSource>> -> IObservable<'TSource>  when 'TException : Exception
```

```javascript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TException  
  The type of the exception.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  The source sequence.

- handler  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb549151)\<TException, [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>\>  
  The exception handler function, producing another observable sequence.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
An observable sequence containing the source sequence's elements, followed by the elements produced by the handler's resulting observable sequence in case an exception occurred.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

The catch operator can be used to introduce an additional sequence into the subscription when an exception occurs that is of the same type as the exception specified in the handler function. This is accomplished by the Catch operator executing the exception handler which produces the additional sequence. If the source sequence runs to completion with no exception, then the handler is not executed. If the exception encountered is not of the same type specified in the handler function, then that exception is sent to the observer’s OnError handler. The example code in this topic demonstrates the Catch operator.

## Examples

The following example demonstrates how the catch operator can be used to include an additional sequence of integers with the subscription if an exception is encountered. Notice the exception thrown must be of the same type as the exception in the signature of the exception handler function. If it is not of the same type, then the OnError handler for the observer is executed instead of exception handler.

    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    
    namespace Example
    {
    
      class Program
      {
    
        static void Main()
        {
          //***********************************************************************************************//
          //*** sequence1 is generated from the enumerator returned by the RandomNumSequence function.  ***//
          //*** It will be combined with sequence2 using the Catch() operator only if there is an       ***//
          //*** exception thrown in sequence1 that is caught by the Catch() operator.                   ***//
          //***********************************************************************************************//
          IObservable<int> sequence1 = RandomNumSequence().ToObservable();
    
    
          //**************************************************************************************************************************//
          //*** In this catch operation, the exception handler for Observable::Catch will not be executed. This is because         ***//
          //*** sequence1 throws an InvalidOperationException which isn't of the NullReferenceException type specified in the      ***//
          //*** signature of ExNullRefHandler.                                                                                     ***//
          //***                                                                                                                    ***//
          //*** The InvalidOperationException will be caught by the OnError handler instead.                                       ***//
          //**************************************************************************************************************************//
          Console.WriteLine("==============================================================");
          Console.WriteLine("Calling Catch operator with NullReferenceException handler...");
          Console.WriteLine("==============================================================\n");
          sequence1.Catch((Func<NullReferenceException, IObservable<int>>)ExNullRefHandler)
            .Subscribe(i => Console.WriteLine(i),
                      (ex) => Console.WriteLine("\nException {0} in OnError handler\nException.Message : \"{1}\"\n\n", ex.GetType().ToString(), ex.Message));
    
    
          //**************************************************************************************************************************//
          //*** In this catch operation, the exception handler will be executed. Because InvalidOperationException thrown by       ***//
          //*** sequence1 is of the InvalidOperationException type specified in the signature of ExInvalidOpHandler().             ***//
          //**************************************************************************************************************************//
    
            Console.WriteLine("================================================================");
            Console.WriteLine("Calling Catch operator with InvalidOperationException handler...");
            Console.WriteLine("================================================================\n");
            sequence1.Catch((Func<InvalidOperationException, IObservable<int>>)ExInvalidOpHandler)
            .Subscribe(i => Console.WriteLine(i),
                      (ex) => Console.WriteLine("\nException {0} in OnError handler\nException.Message : \"{1}\"\n\n", ex.GetType().ToString(), ex.Message));
    
    
          Console.WriteLine("\nPress ENTER to exit...\n");
          Console.ReadLine();
        }
    
    
    
    
        //*******************************************************************************************************//
        //***                                                                                                 ***//
        //*** This method will yield a random sequence of 5 integers then throw an InvalidOperationException. ***//
        //***                                                                                                 ***//
        //*******************************************************************************************************//
        static IEnumerable<int> RandomNumSequence()
        {
          Random random = new Random();
    
          //************************************************//
          //*** Yield an a sequence of 5 random integers ***//
          //************************************************//
          for (int i = 0; i < 5; i++)
          {
            yield return random.Next(101);
          }
    
          //*********************************************************//
          //*** Then throw an InvalidOperationException exception ***//
          //*********************************************************//
          throw new InvalidOperationException("Some Exception Happened!");
        }
    
    
    
        //*********************************************************************************************************//
        //***                                                                                                   ***//
        //*** Simple catch handler for NullReferenceExceptions. This handler looks at the exception message and ***//
        //*** returns a sequence of int.                                                                        ***//
        //***                                                                                                   ***//
        //*********************************************************************************************************//
        static IObservable<int> ExNullRefHandler(NullReferenceException ex)
        {
          //***********************************************************************************************//
          //*** Sequence2 will be part of the resulting sequence if an exception is caught in sequence1 ***//
          //***********************************************************************************************//
          int[] sequence2 = { 0 };
    
          if (ex.Message == "Some Exception Happened!")
            sequence2 = new int[] { 5001, 5002, 5003, 5004 };
    
          return sequence2.ToObservable();
        }
    
    
    
        //************************************************************************************************************//
        //***                                                                                                      ***//
        //*** Simple catch handler for InvalidOperationExceptions. This handler looks at the exception message and ***//
        //*** returns a sequence of int.                                                                           ***//
        //***                                                                                                      ***//
        //************************************************************************************************************//
        static IObservable<int> ExInvalidOpHandler(InvalidOperationException ex)
        {
          //***********************************************************************************************//
          //*** Sequence2 will be part of the resulting sequence if an exception is caught in sequence1 ***//
          //***********************************************************************************************//
          int[] sequence2 = { 0 };
    
          if (ex.Message == "Some Exception Happened!")
            sequence2 = new int[] { 1001, 1002, 1003, 1004 };
    
          return sequence2.ToObservable();
        }
      }
    }

The following is example output from the example code.

    ==============================================================
    Calling Catch operator with NullReferenceException handler...
    ==============================================================
    
    68
    20
    17
    6
    24
    
    Exception System.InvalidOperationException in OnError handler
    Exception.Message : "Some Exception Happened!"
    
    
    ================================================================
    Calling Catch operator with InvalidOperationException handler...
    ================================================================
    
    87
    29
    84
    68
    23
    1001
    1002
    1003
    1004
    
    Press ENTER to exit...

## See Also

#### Reference

[Observable Class](Observable/Observable)

[Catch Overload](Catch/Observable.Catch)

[System.Reactive.Linq Namespace](System.Reactive.Linq/System.Reactive.Linq)










