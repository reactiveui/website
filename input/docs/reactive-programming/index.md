Order: 12
---

> <img src="http://reactivex.io/assets/Rx_Icon.png" width="25" height="25"> &nbsp; [Reactive programming](http://reactivex.io) is programming with asynchronous data streams.

Event buses or your typical click events are really an asynchronous event stream, on which you can observe and do some side effects. [Reactive programming](http://reactivex.io) is that idea on steroids. You are able to create data streams of anything, not just from click and hover events. Streams are cheap and ubiquitous and anything can be a stream: variables, user inputs, properties, caches, data structures, etc. For example, imagine your Twitter feed would be a data stream in the same fashion that click events are. You can listen to that stream and react accordingly.

# Observer Design Pattern

A stream is a sequence of _ongoing events ordered in time_. It can emit three different things: a value of some type, an error, or a "completed" signal. Consider that the "completed" takes place, for instance, when the current window or view containing that button is closed. We capture these emitted events only _asynchronously_, by defining a function that will execute when a value is emitted, another function when an error is emitted, and another function when 'completed' is emitted.

```cs
public interface IObserver<in T>
{
    void OnNext(T value);
    void OnError(Exception error);
    void OnCompleted();
}
```

 Sometimes these last two can be omitted and you can just focus on defining the function for values. The "listening" to the stream is called subscribing. The functions we are defining are observers. The stream is the subject (or "observable") being observed. This is precisely the [Observer Design Pattern](https://en.wikipedia.org/wiki/Observer_pattern).

```cs
public interface IObservable<out T>
{
    IDisposable Subscribe(IObserver<T> observer);
}
```

The hardest part of the learning journey is _thinking in Reactive_. It's a lot about letting go of old imperative and stateful habits of typical programming, and forcing your brain to work in a different paradigm.

# Operators

On top of that, you are given [an amazing toolbox of functions to combine, create and filter](http://reactivex.io/documentation/operators.html) any of those streams. That's where the "functional" magic kicks in. A stream can be used as an input to another one. Even multiple streams can be used as inputs to another stream. 

```cs
term.Throttle(TimeSpan.FromSeconds(0.8))
    .Where(term => !string.IsNullOrWhiteSpace(term))
    .DistinctUntilChanged()
    .ObserveOn(scheduler)
    .Subscribe(x => { });
```

You can [merge](http://reactivex.io/documentation/operators/merge.html) two streams. You can [filter](http://reactivex.io/documentation/operators/filter.html) a stream to get another one that has only those events you are interested in. You can [map](http://reactivex.io/documentation/operators/map.html) data values from one stream to another new one. Each language-specific implementation of reactive extensions implements a set of operators. Most operators [operate on an Observable and return an Observable](http://reactivex.io/documentation/operators.html). 

# Lifecycle

The default behavior of the Observable operators is to dispose of the subscription as soon as possible (when an `OnCompleted` or `OnError` messages is published) but need to consider sequences [that never terminate by OnCompleted or OnError](http://introtorx.com/Content/v1.0.10621.0/14_HotAndColdObservables.html#HotAndCold). If you call a Subscribe method and ignore the return value, you have lost your only handle to unsubscribe. The subscription will still exist, and you have effectively lost access to this resource, which could result in leaking memory and running unwanted processes. The Subscribe method returns an `IDisposable`, so that you can unsubscribe to a sequence and dispose of it easily. When you invoke the Dispose method on the observable sequence, the observer will stop listening to the observable for data.

> Many people who hear about the Dispose pattern for the first time complain that the GC isn't doing its job. They think it should collect resources, and that this is just like having to manage resources as you did in the unmanaged world. The truth is that the GC was never meant to manage resources. It was designed to manage memory and it is excellent in doing just that. - [Krzysztof Cwalina](http://blogs.msdn.com/b/kcwalina/) from [Joe Duffy's blog](http://www.bluebytesoftware.com/blog/2005/04/08/DGUpdateDisposeFinalizationAndResourceManagement.aspx).

You can use the IDisposable interface for more than the common use of deterministically releasing unmanaged resources. It is a useful tool for managing lifetime or scope of anything. See [Lifetime Management](http://introtorx.com/Content/v1.0.10621.0/03_LifetimeManagement.html) for details.

# Disposables

ReactiveUI provides you with [WhenActivated](/docs/handbook/when-activated) to help manage lifecycle and the Reactive Extensions provides several different implementations of the IDisposable interface to help you with managing lifetime, scope and resources. For example, [CompositeDisposable](https://msdn.microsoft.com/en-us/library/system.reactive.disposables.compositedisposable(v=vs.103).aspx) is used to merge multiple disposables into single one and is used in [WhenActivated](/docs/handbook/when-activated). [SerialDisposable](https://msdn.microsoft.com/en-us/library/system.reactive.disposables.serialdisposable(v=vs.103).aspx) is another most useful disposable - what's useful about it is that when you set the disposable, the previous one is Disposed. It's also atomic aka thread safe, and [immunes to double-disposing](https://twitter.com/anaisbetts/status/1034168666739200000). For a full rundown of each of the implementations see the [Disposables section at Introduction to Reactive Extensions book](http://introtorx.com/Content/v1.0.10621.0/20_Disposables.html#Disposables).

# Schedulers

If you want to introduce multithreading into your cascade of Observable operators, you can do so by instructing those operators, or particular Observables, to operate on particular [Schedulers](http://reactivex.io/documentation/scheduler.html). ReactiveUI provides helper methods to help handle [testing observable streams](/docs/handbook/testing) and [two app-wide schedulers that should be used in-place of other schedulers](/docs/handbook/scheduling/). If you are new to reactive programming, see [scheduling section](http://introtorx.com/Content/v1.0.10621.0/15_SchedulingAndThreading.html) for details.

# See also

- [Introduction to RX](http://introtorx.com/) by Lee Campbell
- [Reactive extensions documentation](http://reactivex.io/)
- [Videos](/docs/reactive-programming/videos)