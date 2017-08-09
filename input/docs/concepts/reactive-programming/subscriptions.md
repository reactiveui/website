Title: Subscriptions
---

# Subscribing


# Unsubscribing


# OnError and OnCompleted


# Lifecycle

The default behavior of the Observable operators is to dispose of the subscription as soon as possible (i.e, when an OnCompleted or OnError messages is published) _but need to consider sequences that never terminate (by OnCompleted or OnError)_

The Subscribe method returns an IDisposable, so that you can unsubscribe to a sequence and dispose of it easily. When you invoke the Dispose method on the observable sequence, the observer will stop listening to the observable for data.

If you call a Subscribe method and ignore the return value, you have lost your only handle to unsubscribe. The subscription will still exist, and you have effectively lost access to this resource, which could result in leaking memory and running unwanted processes.

ReactiveUI provides you with  [WhenActivated](/docs/handbook/when-activated) to help manage lifecyle and the Reactive Extensions provides several different options to help you with managing [lifetime, scope and resources](disposables).

## See Also

* https://msdn.microsoft.com/en-us/library/hh242977(v=vs.103).aspx
* http://www.introtorx.com/content/v1.0.10621.0/03_LifetimeManagement.html

