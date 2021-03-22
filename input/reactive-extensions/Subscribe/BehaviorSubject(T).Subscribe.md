title: BehaviorSubject<T>.Subscribe()
---
# BehaviorSubject\<T\>.Subscribe Method

Subscribes an observer to the subject.

**Namespace:**  [System.Reactive.Subjects](System.Reactive.Subjects/System.Reactive.Subjects)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Public Function Subscribe ( _
    observer As IObserver(Of T) _
) As IDisposable
```

```vb
'Usage
Dim instance As BehaviorSubject
Dim observer As IObserver(Of T)
Dim returnValue As IDisposable

returnValue = instance.Subscribe(observer)
```

```csharp
public IDisposable Subscribe(
    IObserver<T> observer
)
```

```c++
public:
virtual IDisposable^ Subscribe(
    IObserver<T>^ observer
) sealed
```

```fsharp
abstract Subscribe : 
        observer:IObserver<'T> -> IDisposable 
override Subscribe : 
        observer:IObserver<'T> -> IDisposable 
```

```javascript
public final function Subscribe(
    observer : IObserver<T>
) : IDisposable
```

#### Parameters

- observer  
  Type: [System.IObserver](https://msdn.microsoft.com/en-us/library/Dd783449)\<[T](BehaviorSubject/BehaviorSubject(T))\>  
  Observer to subscribe to the subject.

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
IDisposable object that can be used to unsubscribe the observer from the subject.

#### Implements

[IObservable\<T\>.Subscribe(IObserver\<T\>)](https://msdn.microsoft.com/en-us/library/m:system.iobservable%601.subscribe(system.iobserver%7b%600%7d)(v=VS.103))

## Remarks

A BehaviorSubject buffers the last item it published through its IObservable interface. If no item has been published through its IObservable interface then the initial item provided in the constructor is the currently buffered item. When a subscription is made to the BehaviorSubject's IObservable interface, the sequence published begins with the currently buffered item.

No items are buffered or published from a BehaviorSubject once its IObserver interface receives a completion.

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
