title: Observable.Aggregate<TSource, TAccumulate>(IObservable<TSource>, TAccumulate, Func<TAccumulate, TSource, TAccumulate>)
---
# Observable.Aggregate\<TSource, TAccumulate\> Method (IObservable\<TSource\>, TAccumulate, Func\<TAccumulate, TSource, TAccumulate\>)

Applies an accumulator function over an observable sequence with the specified seed value.

**Namespace:**  [System.Reactive.Linq](System.Reactive.Linq\System.Reactive.Linq.md)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
<ExtensionAttribute> _
Public Shared Function Aggregate(Of TSource, TAccumulate) ( _
    source As IObservable(Of TSource), _
    seed As TAccumulate, _
    accumulator As Func(Of TAccumulate, TSource, TAccumulate) _
) As IObservable(Of TAccumulate)
```

```vb
'Usage
Dim source As IObservable(Of TSource)
Dim seed As TAccumulate
Dim accumulator As Func(Of TAccumulate, TSource, TAccumulate)
Dim returnValue As IObservable(Of TAccumulate)

returnValue = source.Aggregate(seed, _
    accumulator)
```

```csharp
public static IObservable<TAccumulate> Aggregate<TSource, TAccumulate>(
    this IObservable<TSource> source,
    TAccumulate seed,
    Func<TAccumulate, TSource, TAccumulate> accumulator
)
```

```c++
[ExtensionAttribute]
public:
generic<typename TSource, typename TAccumulate>
static IObservable<TAccumulate>^ Aggregate(
    IObservable<TSource>^ source, 
    TAccumulate seed, 
    Func<TAccumulate, TSource, TAccumulate>^ accumulator
)
```

```fsharp
static member Aggregate : 
        source:IObservable<'TSource> * 
        seed:'TAccumulate * 
        accumulator:Func<'TAccumulate, 'TSource, 'TAccumulate> -> IObservable<'TAccumulate> 
```

```jscript
JScript does not support generic types and methods.
```

#### Type Parameters

- TSource  
  The type of source.

- TAccumulate  
  The type of accumulate.

#### Parameters

- source  
  Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>  
  An observable sequence to aggregate over.

- seed  
  Type: TAccumulate  
  The initial accumulator value.

- accumulator  
  Type: [System.Func](https://msdn.microsoft.com/en-us/library/Bb534647)\<TAccumulate, TSource, TAccumulate\>  
  An accumulator function to be invoked on each element.

#### Return Value

Type: [System.IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TAccumulate\>  
An observable sequence containing a single element with the final accumulator value.

#### Usage Note

In Visual Basic and C\#, you can call this method as an instance method on any object of type [IObservable](https://msdn.microsoft.com/en-us/library/Dd990377)\<TSource\>. When you use instance method syntax to call this method, omit the first parameter. For more information, see [](https://msdn.microsoft.com/en-us/library/Bb384936) or [](https://msdn.microsoft.com/en-us/library/Bb383977).

## Remarks

The aggregate operator is used to apply a function across a source sequence to produce an aggregate or accumulated value. The function applied across the sequence is called an accumulator function. It requires two parameters: an accumulator value, and an item from the sequence which is processed with the accumulator value. The initial accumulator value is called the seed value and must be provided to the aggregate operator. The accumulator function returns the new accumulator value each time it is called. The new accumulator value is then used with the next call to the accumulator function to process the item in the sequence. These calls continue until the end of the sequence.

The aggregate operator returns an observable sequence which is of the same type as the seed value which is passed into the operator. To obtain the final aggregate value, you subscribe to the observable sequence returned from the aggregate operator. Once the accumulator function has been applied across the entire sequence, the observer’s OnNext and OnCompleted handlers provided in the subscription are called to provide the final aggregate value. See the example code provided with this operator.

## Examples

This example demonstrates using the aggregate operator to count the vowels in a string of characters generated at runtime with Console.Readkey(). The CountVowels function is the accumulator function and it increments the count of each vowel encountered in the sequence.

    using System;
    using System.Reactive.Linq;
    
    namespace Example
    {
    
      class Program
      {
    
        enum Vowels : int
        {
          A, E, I, O, U
        };
    
    
        static void Main()
        {
    
          //****************************************************************************************//
          //*** Create an observable sequence of char from console input until enter is pressed. ***//
          //****************************************************************************************//
          IObservable<char> xs = Observable.Create<char>(observer =>
          {
            bool bContinue = true;
    
            while (bContinue)
            {
              ConsoleKeyInfo keyInfo = Console.ReadKey(true);
    
              if (keyInfo.Key != ConsoleKey.Enter)
              {
                Console.Write(keyInfo.KeyChar);
                observer.OnNext(keyInfo.KeyChar);
              }
              else
              {
                observer.OnCompleted();
                Console.WriteLine("\n");
                bContinue = false;
              }
            }
    
            return (() => { });
          });
                                                                  
    
          //***************************************************************************************//
          //***                                                                                 ***//
          //*** The "Aggregate" operator causes the accumulator function, "CountVowels", to be  ***//
          //*** called for each character in the sequence.                                      ***//
          //***                                                                                 ***//
          //*** The seed value is the integer array which will hold a count of each of the five ***//
          //*** vowels encountered. It is passed as a parameter to Aggregate.                   ***//
          //*** The seed value will be passed to CountVowels and processed with the first item  ***//
          //*** in the sequence.                                                                ***//
          //***                                                                                 ***//
          //*** The return value from "CountVowels" is the same type as the seed parameter.     ***//
          //*** That return value is subsequently passed into each call to the accumulator with ***//
          //*** its corresponding character from the sequence.                                  ***//
          //                                                                                    ***//
          //*** The event handler, "OnNext", is not called until the accumulator function has   ***//
          //*** been executed across the entire sequence.                                       ***//
          //***                                                                                 ***//
          //***************************************************************************************//
          
          Console.WriteLine("\nEnter a sequence of characters followed by the ENTER key.\n" +
                            "The example code will count the vowels you enter\n");
    
          using (IDisposable handle = xs.Aggregate(new int[5], CountVowels).Subscribe(OnNext))
          {
            Console.WriteLine("\nPress ENTER to exit...");
            Console.ReadLine();
          }
    
        }
    
    
    
        //*********************************************************************************************************//
        //***                                                                                                   ***//
        //*** The Event handler, "OnNext" is called when the event stream that Aggregate is processing          ***//
        //**  completes.                                                                                        ***//
        //***                                                                                                   ***//
        //*** The final accumulator value is passed to the handler. In this example, it is the array containing ***//
        //*** final count of each vowel encountered.                                                            ***//
        //***                                                                                                   ***//
        //*********************************************************************************************************//
        static void OnNext(int[] state)
        {
          Console.WriteLine("Vowel Final Count = A:{0}, E:{1}, I:{2}, O:{3}, U:{4}\n",
                            state[(int)Vowels.A],
                            state[(int)Vowels.E],
                            state[(int)Vowels.I],
                            state[(int)Vowels.O],
                            state[(int)Vowels.U]);
        }
    
    
    
        //*********************************************************************************************************//
        //***                                                                                                   ***//
        //*** CountVowels will be called for each character event in the event stream.                          ***//
        //***                                                                                                   ***//
        //*** The int array, "state", is used as the accumulator. It holds a count for each vowel.              ***//
        //***                                                                                                   ***//
        //*** CountVowels simply looks at the character "ch" to see if it is a vowel and increments that vowel  ***//
        //*** count in the array.                                                                               ***//
        //***                                                                                                   ***//
        //*********************************************************************************************************//
        static int[] CountVowels(int[] state, char ch)
        {
          char lch = char.ToLower(ch);
    
          switch (lch)
          {
            case 'a': state[(int)Vowels.A]++;
              break;
            case 'e': state[(int)Vowels.E]++;
              break;
            case 'i': state[(int)Vowels.I]++;
              break;
            case 'o': state[(int)Vowels.O]++;
              break;
            case 'u': state[(int)Vowels.U]++;
              break;
          };
    
          return state;
        }
      }
    }

Here is example output from the example code.

    Enter a sequence of characters followed by the ENTER key.
    The example code will count the vowels you enter
    
    This is a sequence of char I am generating from Console.ReadKey()
    
    Vowel Final Count = A:5, E:8, I:4, O:4, U:1
    
    
    Press ENTER to exit...

## See Also

#### Reference

[Observable Class](Observable\Observable.md)

[Aggregate Overload](Aggregate\Observable.Aggregate.md)

[System.Reactive.Linq Namespace](System.Reactive.Linq\System.Reactive.Linq.md)










