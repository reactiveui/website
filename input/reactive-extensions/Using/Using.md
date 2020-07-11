title: Using Rx
---
# Using Rx

This section includes topics that explain how you use Rx to create and subscribe to sequences, bridge existing .NET events and existing asynchronous patterns, as well as using schedulers. It also describes more advanced tasks such as testing and debugging an observable sequence, as well as implementing your own operators.

## In This Section

1. [Exploring The Major Interfaces in Rx](Exploring/Exploring)

2. [Creating and Querying Observable Sequences](Creating/Creating)

3. [Subjects](Subjects/Subjects)

4. [Scheduling and Concurrency](Scheduling/Scheduling)

5. [Testing and Debugging Observable Sequences](Testing/Testing)

6. [Implementing Your Own Operators for IObservable](Implementing/Implementing)

7. [Using Observable Providers](Using/Using)

## Reference

[System.Reactive](System.Reactive/System.Reactive)[System.Reactive.Linq](System.Reactive.Linq/System.Reactive.Linq)[System.Reactive.Concurrency](System.Reactive.Concurrency/System.Reactive.Concurrency)[System.Reactive.Subjects](System.Reactive.Subjects/System.Reactive.Subjects)

## Related Sections

# Using Observable Providers

By implementing the [IQbservable](IQbservable/IQbservable) interface and using the factory extension methods provided by the [Qbservable](Qbservable/Qbservable) type, you can write a custom LINQ provider to query any type of external data, so that these data are treated as sequences that can be subscribed to. For example, the [LINQ to WQL sample](http://go.microsoft.com/fwlink/?linkid=208531) in the [Rx MSDN Developer Center](http://msdn.microsoft.com/en-us/data/gg577610) shows how to build a simple provider for querying WMI Events using WQL. You can use the factory LINQ operators provided by the **Qbservable** type to abstract a sequence of WMI events and query, filter and compose them. Subscribing to this sequence will trigger the translation of the LINQ query expression into the target language, in this case WQL. 

## Using the IQbservable interface to query external data

When we mention that we want to query for data, we are first concerned about what we want to query. This can be a pulled-based IEnumerable collection, or a push-based asynchronous [Observable](Observable/Observable) sequence. We also want to know where (under which context) do we want to execute the query. For **Observable** sequences, that is handled by the [IScheduler](IScheduler/IScheduler) interface and its various Scheduler implementation types. Finally, we want to know how we do the query. We can represent a query (a lambda expression) in verbatim (compiled into .NET intermediate language (IL) code), in which each operator in the query will be evaluated in a linear fashion. This is the case for the factory operator methods of the **Observable** type. Or you can represent your query using expression trees, which can be traversed to get the represented algorithm (e.g., predicting whether an item is greater than a value, etc.), then translate the algorithm into some domain-specific code, such as a T-SQL query statement for querying a SQL database, specific HTTP requests for a particular Web service URI, PowerShell commands, DSQLs for cloud notification services, etc. This is the case for the factory operator methods of the **Qbservable** type. The translated domain-specific code can be executed in a remote target system, or you can use the expression tree representation to do local query optimization.

Just like IObservable/IObserver is a dual of IEnumerable/IEnumerator, **IQbservable** is the dual of [IQueryable](https://msdn.microsoft.com/en-us/library/Bb495796) and provides an expression tree representation of an IObservable query. You can change between **IQbservable** and **IObservable** types by using the AsQbservable and AsObservable methods. Calling **AsQbservable** produces an expression tree made up of a single node that calls the original **IObservable** instance. This relationship is important for understanding why a complete **IQbservable** query has to be defined starting from an **IQbservable** sequence and cannot be obtained simply by calling **AsQbservable** on an existing **IQbservable** query. In the following example, the call to **AsQbservable** produces a complete query tree only when you build the query by applying IQbservable AsQbservable to the data source.

    var source = Observable.Interval(TimeSpan.FromSeconds(1));
    var q = source.AsQbservable();
    Console.WriteLine(q.ToString());
    var sub = q.Subscribe(Console.WriteLine);
    Console.ReadKey();

The **IQbservable** interface is intended for implementation by query providers. It is only supposed to be implemented by providers that also implement IQbservable\<T\>. If the provider does not also implement IQbservable\<T\>, the standard query operators cannot be used on the provider's data source. The **IQbservable** interface inherits the **IObservable** interface so that if it represents a query, the results of that query can be subscribed to. Subscription and publication causes the expression tree associated with an **Qbservable** object to be executed. The definition of "executing an expression tree" is specific to a query provider. For example, it may involve translating the expression tree to an appropriate query language for the underlying data source. The Expression property encapsulates the expression tree that is associated with the **IQbservable** instance, whereas the Provider encapsulates the query provider that is associated with the data source.

The set of methods declared in the **Qbservable** class provides an implementation of the standard query operators for querying data sources that implement **IQbservable**. The standard query operators are general purpose methods that follow the LINQ pattern and enable you to express traversal, filter, and projection operations over data in any .NET-based programming language. The majority of the methods in this class are defined as extension methods that extend the **IQbservable** type. This means they can be called like an instance method on any object that implements **IQbservable**. These methods that extend **IQbservable** do not perform any querying directly. Instead, their functionality is to build an Expression object, which is an expression tree that represents the cumulative query. The methods then pass the new expression tree to the CreateQuery method. The actual query execution on the target data is performed by a class that implements **IQbservable**.

## See Also

#### Reference

[IQbservable](IQbservable/IQbservable)  
[Qbservable](Qbservable/Qbservable)

#### Other Resources

[Rx MSDN Developer Center](http://msdn.microsoft.com/en-us/data/gg577610)

# Using Subjects

The Subject\<T\> type implements both IObservable\<T\> and IObserver\<T\>, in the sense that it is both an observer and an observable. You can use a subject to subscribe all the observers, and then subscribe the subject to a backend data source. In this way, the subject can act as a proxy for a group of subscribers and a source. You can use subjects to implement a custom observable with caching, buffering and time shifting. In addition, you can use subjects to broadcast data to multiple subscribers.

By default, subjects do not perform any synchronization across threads. They do not take a scheduler but rather assume that all serialization and grammatical correctness are handled by the caller of the subject.  A subject simply broadcasts to all subscribed observers in the thread-safe list of subscribers. Doing so has the advantage of reducing overhead and improving performance. If, however, you want to synchronize outgoing calls to observers using a scheduler, you can use the Synchronize method to do so.

## Using Subjects

In the following example, we create a subject, subscribe to that subject and then use the same subject to publish values to the observer. By doing so, we combine the publication and subscription into the same source.

In addition to taking an IObserver\<T\>, the Subscribe method also has an overload that takes an Action\<T\> for onNext, which means that the action will be executed every time an item is published. In our sample, whenever OnNext is invoked, the item will be written to the console.

    Subject<int> subject = new Subject<int>();
    var subscription = subject.Subscribe(
                             x => Console.WriteLine("Value published: {0}", x),
                             () => Console.WriteLine("Sequence Completed."));
    subject.OnNext(1);
    
    subject.OnNext(2);
    
    Console.WriteLine("Press any key to continue");
    Console.ReadKey();
    subject.OnCompleted();
    subscription.Dispose();

The following example illustrates the proxy and broadcast nature of a Subject. We first create a source sequence which produces an integer every 1 second. We then create a Subject, and pass it as an observer to the source so that it will receive all the values pushed out by this source sequence. After that, we create another two subscriptions, this time with the subject as the source. The `subSubject1` and `subSubject2` subscriptions will then receive any value passed down (from the source) by the Subject.

    var source = Observable.Interval(TimeSpan.FromSeconds(1));
    Subject<long> subject = new Subject<long>();
    var subSource = source.Subscribe(subject);
    var subSubject1 = subject.Subscribe(
                             x => Console.WriteLine("Value published to observer #1: {0}", x),
                             () => Console.WriteLine("Sequence Completed."));
    var subSubject2 = subject.Subscribe(
                             x => Console.WriteLine("Value published to observer #2: {0}", x),
                             () => Console.WriteLine("Sequence Completed."));
    Console.WriteLine("Press any key to continue");
    Console.ReadKey();
    subject.OnCompleted();
    subSubject1.Dispose();
    subSubject2.Dispose();

## Different types of Subjects

The Subject\<T\> type in the Rx library is a basic implementation of the ISubject\<T\> interface (you can also implement the ISubject\<T\> interface to create your own subject types). There are other implementations of ISubject\<T\> that offer different functionalities. All of these types store some (or all of) values pushed to them via OnNext, and broadcast it back to its observers. In this way, they convert a Hot Observable into a Cold one. This means that if you Subscribe to any of these more than once (i.e. Subscribe -\> Unsubscribe -\> Subscribe again), you will see at least one of the same value again. For more information on hot and cold observables, see the last section of the [Creating and Subscribing to Simple Observable Sequences](Creating/Creating) topic.

ReplaySubject stores all the values that it has published. Therefore, when you subscribe to it, you automatically receive an entire history of values that it has published, even though your subscription might have come in after certain values have been pushed out. BehaviourSubject is similar to **ReplaySubject**, except that it only stored the last value it published. **BehaviourSubject** also requires a default value of type T upon initialization. This value is sent to observers when no other value has been received by the subject yet. This means that all subscribers will receive a value instantly on Subscribe, unless the Subject has already completed. AsyncSubject is similar to the Replay and Behavior subjects, however it will only store the last value, and only publish it when the sequence is completed. You can use the **AsyncSubject** type for situations when the source observable is hot and might complete before any observer can subscribe to it. In this case, **AsyncSubject** can still provide the last value and publish it to any future subscribers.

# Using Schedulers

A scheduler controls when a subscription starts and when notifications are published. It consists of three components. It is first a data structure. When you schedule for tasks to be completed, they are put into the scheduler for queueing based on priority or other criteria. It also offers an execution context which denotes where the task is executed (e.g., in the thread pool, current thread, or in another app domain). Lastly, it has a clock which provides a notion of time for itself (by accessing the `Now` property of a scheduler). Tasks being scheduled on a particular scheduler will adhere to the time denoted by that clock only.

Schedulers also introduce the notion of virtual time (denoted by the VirtualScheduler type), which does not correlate with real time that is used in our daily life. For example, a sequence that is specified to take 100 years to complete can be scheduled to complete in virtual time in a mere 5 minutes. This will be covered in the [Testing and Debugging Observable Sequences](Testing/Testing) topic.

## Scheduler Types

The various Scheduler types provided by Rx all implement the [IScheduler](https://msdn.microsoft.com/en-us/library/Ff431798) interface. Each of these can be created and returned by using static properties of the Scheduler type. The [ImmediateScheduler](ImmediateScheduler/ImmediateScheduler) (by accessing the static [Immediate](Immediate/Scheduler.Immediate) property) will start the specified action immediately. The [CurrentThreadScheduler](CurrentThreadScheduler/CurrentThreadScheduler) (by accessing the static [CurrentThread](CurrentThread/Scheduler.CurrentThread) property) will schedule actions to be performed on the thread that makes the original call. The action is not executed immediately, but is placed in a queue and only executed after the current action is complete. The [DispatcherScheduler](DispatcherScheduler/DispatcherScheduler) (by accessing the static Dispatcher property) wills schedule actions on the current Dispatcher, which is beneficial to Silverlight developers who use Rx. Specified actions are then delegated to the Dispatcher.BeginInvoke() method in Silverlight. [NewThreadScheduler](NewThreadScheduler/NewThreadScheduler) (by accessing the static [NewThread](NewThread/Scheduler.NewThread) property) schedules actions on a new thread, and is optimal for scheduling long running or blocking actions. [TaskPoolScheduler](TaskPoolScheduler/TaskPoolScheduler) (by accessing the static [TaskPool](TaskPool/Scheduler.TaskPool) property) schedules actions on a specific Task Factory. [ThreadPoolScheduler](ThreadPoolScheduler/ThreadPoolScheduler) (by accessing the static [ThreadPool](ThreadPool/Scheduler.ThreadPool) property) schedules actions on the thread pool. Both pool schedulers are optimized for short-running actions.

## Using Schedulers

You may have already used schedulers in your Rx code without explicitly stating the type of schedulers to be used. This is because all Observable operators that deal with concurrency have multiple overloads. If you do not use the overload which takes a scheduler as an argument, Rx will pick a default scheduler by using the principle of least concurrency. This means that the scheduler which introduces the least amount of concurrency that satisfies the needs of the operator is chosen.  For example, for operators returning an observable with a finite and small number of messages, Rx calls **Immediate**.  For operators returning a potentially large or infinite number of messages, **CurrentThread** is called. For operators which use timers, **ThreadPool** is used.

Because Rx uses the least concurrency scheduler, you can pick a different scheduler if you want to introduce concurrency for performance purpose, or when you have a thread-affinity issue.  An example of the former is that when you do not want to block a particular thread, in this case, you should use **ThreadPool**.  An example of the latter is that when you want a timer to run on the UI, in this case, you should use **Dispatcher**. To specify a particular scheduler, you can use those operator overloads that take a scheduler, e.g., `Timer(TimeSpan.FromSeconds(10), Scheduler.DispatcherScheduler())`.

In the following example, the source observable sequence is producing values at a frantic pace. The default overload of the Timer operator would place OnNext messages on the ThreadPool.

    Observable.Timer(Timespan.FromSeconds(0.01))
              .Subscribe(…);

This will queue up on the observer quickly. We can improve this code by using the ObserveOn operator, which allows you to specify the context that you want to use to send pushed notifications (OnNext) to observers. By default, the ObserveOn operator ensures that OnNext will be called as many times as possible on the current thread. You can use its overloads and redirect the OnNext outputs to a different context. In addition, you can use the SubscribeOn operator to return a proxy observable that delegates actions to a specific scheduler. For example, for a UI-intensive application, you can delegate all background operations to be performed on a scheduler running in the background by using SubscribeOn and passing to it a **ThreadPoolScheduler**. To receive notifications being pushed out and access any UI element, you can pass an instance of the **DispatcherScheduler** to the ObserveOn operator.

The following example will schedule any OnNext notifications on the current Dispatcher, so that any value pushed out is sent on the UI thread. This is especially beneficial to Silverlight developers who use Rx.

    Observable.Timer(Timespan.FromSeconds(0.01))
              .ObserveOn(Scheduler.DispatcherScheduler)
              .Subscribe(…);

Instead of using the ObserveOn operator to change the execution context on which the observable sequence produces messages, we can create concurrency in the right place to begin with. As operators parameterize introduction of concurrency by providing a scheduler argument overload, passing the right scheduler will lead to fewer places where the ObserveOn operator has to be used. For example, we can unblock the observer and subscribe to the UI thread directly by changing the scheduler used by the source, as in the following example. In this code, by using the Timer overload which takes a scheduler, and providing the `Scheduler.Dispatcher` instance, all values pushed out from this observable sequence will originate on the UI thread.

    Observable.Timer(Timespan.FromSeconds(0.01), Scheduler.DispatcherScheduler)
              .Subscribe(…);

You should also note that by using the ObserveOn operator, an action is scheduled for each message that comes through the original observable sequence. This potentially changes timing information as well as puts additional stress on the system. If you have a query that composes various observable sequences running on many different execution contexts, and you are doing filtering in the query, it is best to place ObserveOn later in the query. This is because a query will potentially filter out a lot of messages, and placing the ObserveOn operator earlier in the query would do extra work on messages that would be filtered out anyway. Calling the ObserveOn operator at the end of the query will create the least performance impact.

Another advantage of specifying a scheduler type explicitly is that you can introduce concurrency for performance purpose, as illustrated by the following code.

    seq.GroupBy(...)
            .Select(x=>x.ObserveOn(Scheduler.NewThread))
            .Select(x=>expensive(x))  // perform operations that are expensive on resources
