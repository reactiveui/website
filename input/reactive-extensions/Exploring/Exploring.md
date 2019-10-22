title: Exploring The Major Interfaces in Rx
---
# Exploring The Major Interfaces in Rx

This topic describes the major Reactive Extensions (Rx) interfaces used to represent observable sequences and subscribe to them.

The IObservable\<T\>/IObserver\<T\> interfaces are available in the .NET Framework 4.0 base class library, and they come in a package that can be installed in .NET 3.5, Silverlight 3 and 4, as well as Javascript.

## IObservable\<T\>/IObserver\<T\>

Rx exposes asynchronous and event-based data sources as push-based, observable sequences abstracted by the new IObservable\<T\> interface in .NET Framework 4.0. This IObservable\<T\> interface is a dual of the familiar IEnumerable\<T\> interface used for pull-based, enumerable collections. It represents a data source that can be observed, meaning that it can send data to anyone who is interested. It maintains a list of dependent IObserver\<T\> implementations representing such interested listeners, and notifies them automatically of any state changes.

An implementation of the IObservable\<T\> interface can be viewed as a collection of elements of type T. Therefore, an IObservable\<int\> can be viewed as a collection of integers, in which integers will be pushed to the subscribed observers.

As described in [What is Rx](https://msdn.microsoft.com/en-us/library/Hh242962), the other half of the push model is represented by the IObserver\<T\> interface, which represents an observer who registers an interest through a subscription. Items are subsequently handed to the observer from the observable sequence to which it subscribes.

In order to receive notifications from an observable collection, you use the Subscribe method of IObservable to hand it an IObserver\<T\> object. In return for this observer, the Subscribe method returns an IDisposable object that acts as a handle for the subscription. This allows you to clean up the subscription after you are done.  Calling Dispose on this object detaches the observer from the source so that notifications are no longer delivered. As you can infer, in Rx you do not need to explicitly unsubscribe from an event as in the .NET event model.

Observers support three publication events, reflected by the interface’s methods. OnNext can be called zero or more times, when the observable data source has data available. For example, an observable data source used for mouse move events can send out a Point object every time the mouse has moved. The other two methods are used to indicate completion or errors.

The following lists the IObservable\<T\>/IObserver\<T\> interfaces.

    public interface IObservable<out T> 
    { 
       IDisposable Subscribe(IObserver<T> observer); 
    } 
    public interface IObserver<in T> 
    { 
       void OnCompleted();            // Notifies the observer that the source has finished sending messages.
       void OnError(Exception error); // Notifies the observer about any exception or error.
       void OnNext(T value);          // Pushes the next data value from the source to the observer.
    } 

Rx also provides Subscribe extension methods so that you can avoid implementing the IObserver\<T\> interface yourself. For each publication event (OnNext, OnError, OnCompleted) of an observable sequence, you can specify a delegate that will be invoked, as shown in the following example. If you do not specify an action for an event, the default behavior will occur.

    IObservable<int> source = Observable.Range(1, 5); //creates an observable sequence of 5 integers, starting from 1
    IDisposable subscription = source.Subscribe(
                                x => Console.WriteLine("OnNext: {0}", x), //prints out the value being pushed
                                ex => Console.WriteLine("OnError: {0}", ex.Message),
                                () => Console.WriteLine("OnCompleted"));

You can treat the observable sequence (such as a sequence of mouse-over events) as if it were a normal collection. Thus you can write LINQ queries over the collection to do things like filtering, grouping, composing, etc. To make observable sequences more useful, the Rx assemblies provide many factory LINQ operators so that you do not need to implement any of these on your own. This will be covered in the [Querying Observable Sequences using LINQ Operators](Querying\Querying.md) topic.

> [!WARNING]
> You do not need to implement the IObservable&lt;T&gt;/IObserver&lt;T&gt; interfaces yourself.  Rx provides internal implementations of these interfaces for you and exposes them through various extension methods provided by the <A href="hh244252(v=vs.103).md">Observable</A> and Observer types.  See the <A href="hh242972(v=vs.103).md">Creating and Querying Observable Sequences</A> topic for more information.

## See Also

#### Concepts

[Querying Observable Sequences using LINQ Operators](Querying\Querying.md)

#### Other Resources

[Creating and Querying Observable Sequences](Creating\Creating.md)





