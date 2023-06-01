title: Scheduler.ThreadPool Property
---
# Scheduler.ThreadPool Property

Gets the scheduler that schedules work on the ThreadPool.

**Namespace:**  [System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Shared ReadOnly Property ThreadPool As ThreadPoolScheduler
    Get
```

```vb
'Usage
Dim value As ThreadPoolScheduler

value = Scheduler.ThreadPool
```

```csharp
public static ThreadPoolScheduler ThreadPool { get; }
```

```c++
public:
static property ThreadPoolScheduler^ ThreadPool {
    ThreadPoolScheduler^ get ();
}
```

```fsharp
static member ThreadPool : ThreadPoolScheduler
```

```javascript
static function get ThreadPool () : ThreadPoolScheduler
```

#### Property Value

Type: [System.Reactive.Concurrency.ThreadPoolScheduler](ThreadPoolScheduler/ThreadPoolScheduler)  
The thread pool scheduler.

## Remarks

The ThreadPool scheduler schedules actions to be executed on the .NET thread pool. This scheduler is ideal for short running operations.

## Examples

This code example uses the Generate operator to generate a sequence of the integers that are perfect squares less than 1000. The processing associated with the generate operator is scheduled to execute on the .NET thread pool by using the ThreadPool scheduler.

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
          //*** Generate a sequence of integers which are the perfect squares that are less than 100  ***//
          //*********************************************************************************************//
    
          var obs = Observable.Generate(1,                      // Initial state value
                                        x => x * x < 1000,      // The termination condition. Terminate generation when false (the integer squared is not less than 1000)
                                        x => ++x,               // Iteration step function updates the state and returns the new state. In this case state is incremented by 1 
                                        x => x * x,             // Selector function determines the next resulting value in the sequence. The state of type in is squared.
                                        Scheduler.ThreadPool);  // The ThreadPool scheduler runs the generation on a thread pool thread instead of the main thread.
    
          using (IDisposable handle = obs.Subscribe(x => Console.WriteLine(x)))
          {
            Console.WriteLine("Press ENTER to exit...\n");
            Console.ReadLine();
          }
        }
      }
    }

The following output demonstrates running the example code.

    Press ENTER to exit...
    
    1
    4
    9
    16
    25
    36
    49
    64
    81
    100
    121
    144
    169
    196
    225
    256
    289
    324
    361
    400
    441
    484
    529
    576
    625
    676
    729
    784
    841
    900
    961

## See Also

#### Reference

[Scheduler Class](Scheduler/Scheduler)

[System.Reactive.Concurrency Namespace](System.Reactive.Concurrency/System.Reactive.Concurrency)
