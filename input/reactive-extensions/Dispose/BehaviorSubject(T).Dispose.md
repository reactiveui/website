title: BehaviorSubject<T>.Dispose()
---
# BehaviorSubject\<T\>.Dispose Method

Unsubscribe all observers and release resources.

**Namespace:**  [System.Reactive.Subjects](System.Reactive.Subjects/System.Reactive.Subjects)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Sub Dispose
```

```vb
'Usage
Dim instance As BehaviorSubject

instance.Dispose()
```

```csharp
public void Dispose()
```

```c++
public:
virtual void Dispose() sealed
```

```fsharp
abstract Dispose : unit -> unit 
override Dispose : unit -> unit 
```

```javascript
public final function Dispose()
```

#### Implements

[IDisposable.Dispose()](https://msdn.microsoft.com/en-us/library/es4s3w1d)

## Examples

This example demonstrates the BehaviorSubject. The example uses the Interval operator to publish an integer to a integer sequence every second. The sequence will be completed by the Take operator after 10 integers are published. This is the sequence that the BehaviorSubject subscribes to.

Two subscriptions are created for the BehaviorSubject's IObservable interface to show how it publishes it's data.

- **Subscription \#1** : This subscription will start at the very beginning and will show the initial buffered value from the constructor (-9) in the sequence.

- **Subscription \#2** : This subscription will start after a 5 second sleep. This subscription shows that the sequence starts with the currently buffered item.

    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Reactive.Concurrency;
    using System.Threading;
    
    namespace Example
    {
      class Program
      {
        static void Main()
        {
          //********************************************************************************************************//
          //*** A subject acts similar to a proxy in that it acts as both a subscriber and a publisher           ***//
          //*** It's IObserver interface can be used to subscribe to multiple streams or sequences of data.      ***//
          //*** The data is then published through it's IObservable interface.                                   ***//
          //***                                                                                                  ***//
          //*** A BehaviorSubject buffers the last item it published through its IObservable interface. If no    ***//
          //*** item has been published through its IObservable interface then the initial item provided in the  ***//
          //*** constructor is the current buffered item. When a subscription is made to the BehaviorSubject's   ***//
          //*** IObservable interface, the sequence published begins with the currently buffered item.           ***//
          //***                                                                                                  ***//
          //*** No items are buffered or published from a BehaviorSubject once its IObserver interface receives  ***//
          //*** a completion.                                                                                    ***//
          //***                                                                                                  ***//
          //*** In this example, we use the Interval operator to publish an integer to a integer sequence every  ***//
          //*** second. The sequence will be completed by the Take operator after 10 integers are published.     ***//
          //*** This will be the sequence that the BehaviorSubject subscribes to.                                ***//
          //***                                                                                                  ***//
          //*** We will create 2 subscriptions to the BehaviorSubject's IObservable interface to show how it     ***//
          //*** publishes it's data.                                                                             ***//
          //***                                                                                                  ***//
          //*** Subscription #1 : This subscription will start at the very beginning and will show the initial   ***//
          //***                   buffered value from the constructor (-9) in the sequence.                      ***//
          //***                                                                                                  ***//
          //*** Subscription #2 : This subscription will start after a 5 sec. sleep showing the sequence starts  ***//
          //***                   with the currently buffered item.                                              ***//
          //********************************************************************************************************//
    
          BehaviorSubject<long> myBehaviorSubject = new BehaviorSubject<long>((-9));
          Observable.Interval(TimeSpan.FromSeconds(1), Scheduler.ThreadPool).Take(10).Subscribe(myBehaviorSubject);
    
          
          //********************************************************************************************************//
          //*** Subscription #1 : This subscription will start at the very beginning and will show the initial   ***//
          //***                   buffered value from the constructor (-9) in the sequence.                      ***//
          //********************************************************************************************************//
    
          EventWaitHandle wait1 = new EventWaitHandle(false, EventResetMode.ManualReset);
          myBehaviorSubject.Subscribe(x => Console.WriteLine("Subscription #1 observes : " + x),
                                      () => 
                                      {
                                        Console.WriteLine("Subscription #1 completed.");
                                        wait1.Set();
                                      });
    
    
          //********************************************************************************************************//
          //*** Subscription #2 : This subscription will start after a 5 sec. sleep showing the sequence starts  ***//
          //***                   with the currently buffered item.                                              ***//
          //********************************************************************************************************//
        
          Thread.Sleep(5000);
          EventWaitHandle wait2 = new EventWaitHandle(false, EventResetMode.ManualReset);
          myBehaviorSubject.Subscribe(x => Console.WriteLine("{0,30}Subscription #2 observes : {1}", " ", x), 
                                      () => 
                                      {
                                        Console.WriteLine("{0,30}Subscription #2 completed.", " ");
                                        wait2.Set();
                                      });
    
    
          //**************************************************//
          // *** Wait for completion on both subscriptions ***//
          //**************************************************//
    
          WaitHandle.WaitAll(new WaitHandle[] { wait1, wait2 });
          myBehaviorSubject.Dispose();
    
          Console.WriteLine("\nPress ENTER to exit...");
          Console.ReadLine();
        }
      }
    }

The following output from the example code shows the overlapping subscriptions.

    Subscription #1 observes : -9
    Subscription #1 observes : 0
    Subscription #1 observes : 1
    Subscription #1 observes : 2
    Subscription #1 observes : 3
    Subscription #1 observes : 4
                                  Subscription #2 observes : 4
    Subscription #1 observes : 5
                                  Subscription #2 observes : 5
    Subscription #1 observes : 6
                                  Subscription #2 observes : 6
    Subscription #1 observes : 7
                                  Subscription #2 observes : 7
    Subscription #1 observes : 8
                                  Subscription #2 observes : 8
    Subscription #1 observes : 9
                                  Subscription #2 observes : 9
    Subscription #1 completed.
                                  Subscription #2 completed.
    
    Press ENTER to exit...

## See Also

#### Reference

[BehaviorSubject\<T\> Class](BehaviorSubject/BehaviorSubject(T))

[System.Reactive.Subjects Namespace](System.Reactive.Subjects/System.Reactive.Subjects)






