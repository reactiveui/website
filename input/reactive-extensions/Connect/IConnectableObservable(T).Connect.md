title: IConnectableObservable<T>.Connect()
---
# IConnectableObservable\<T\>.Connect Method

Connects the observable.

**Namespace:**  [System.Reactive.Subjects](System.Reactive.Subjects/System.Reactive.Subjects)  
**Assembly:**  System.Reactive (in System.Reactive.dll)

## Syntax

```vb
'Declaration
Function Connect As IDisposable
```

```vb
'Usage
Dim instance As IConnectableObservable
Dim returnValue As IDisposable

returnValue = instance.Connect()
```

```csharp
IDisposable Connect()
```

```c++
IDisposable^ Connect()
```

```fsharp
abstract Connect : unit -> IDisposable 
```

```jscript
function Connect() : IDisposable
```

#### Return Value

Type: [System.IDisposable](https://msdn.microsoft.com/en-us/library/aax125c9)  
IDisposable object used to disconnect the observable.

## Examples

In the following example, we convert a cold observable sequence source to a hot one using the Publish operator, which returns an [IConnectableObservable\<T\>](IConnectableObservable/IConnectableObservable(T)) instance we name hot. The Publish operator provides a mechanism to share subscriptions by broadcasting a single subscription to multiple subscribers. hot acts as a proxy and subscribes to source, then as it receives values from source, pushes them to its own subscribers. To establish a subscription to the backing source and start receiving values, we use the IConnectableObservable.Connect() method. Since IConnectableObservable inherits IObservable, we can use Subscribe to subscribe to this hot sequence even before it starts running. Notice that in the example, the hot sequence has not been started when subscription1 subscribes to it. Therefore, no value is pushed to the subscriber. After calling Connect, values are then pushed to subscription1. After a delay of 3 seconds, subscription2 subscribes to hot and starts receiving the values immediately from the current position (3 in this case) until the end. The output looks like this:

    Current Time: 6/1/2011 3:38:49 PM
    
    Current Time after 1st subscription: 6/1/2011 3:38:49 PM
    
    Current Time after Connect: 6/1/2011 3:38:52 PM
    
    Observer 1: OnNext: 0
    
    Observer 1: OnNext: 1
    
    Current Time just before 2nd subscription: 6/1/2011 3:38:55 PM 
    
    Observer 1: OnNext: 2
    
    Observer 1: OnNext: 3
    
    Observer 2: OnNext: 3
    
    Observer 1: OnNext: 4
    
    Observer 2: OnNext: 4

```
       
Console.WriteLine("Current Time: " + DateTime.Now);
var source = Observable.Interval(TimeSpan.FromSeconds(1));   //creates a sequence

IConnectableObservable<long> hot = Observable.Publish<long>(source);  // convert the sequence into a hot sequence

IDisposable subscription1 = hot.Subscribe(     // no value is pushed to 1st subscription at this point
                            x => Console.WriteLine("Observer 1: OnNext: {0}", x),
                            ex => Console.WriteLine("Observer 1: OnError: {0}", ex.Message),
                            () => Console.WriteLine("Observer 1: OnCompleted"));
Console.WriteLine("Current Time after 1st subscription: " + DateTime.Now);
Thread.Sleep(3000);  //idle for 3 seconds
hot.Connect();       // hot is connected to source and starts pushing value to subscribers 
Console.WriteLine("Current Time after Connect: " + DateTime.Now);
Thread.Sleep(3000);  //idle for 3 seconds
Console.WriteLine("Current Time just before 2nd subscription: " + DateTime.Now);
IDisposable subscription2 = hot.Subscribe(     // value will immediately be pushed to 2nd subscription
                            x => Console.WriteLine("Observer 2: OnNext: {0}", x),
                            ex => Console.WriteLine("Observer 2: OnError: {0}", ex.Message),
                            () => Console.WriteLine("Observer 2: OnCompleted"));
Console.ReadKey();
```

## See Also

#### Reference

[IConnectableObservable\<T\> Interface](IConnectableObservable/IConnectableObservable(T))

[System.Reactive.Subjects Namespace](System.Reactive.Subjects/System.Reactive.Subjects)






