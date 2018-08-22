Title: Subscriptions
---

# Subscribing


# Unsubscribing


# OnError and OnCompleted


# Lifecycle

The default behavior of the Observable operators is to dispose of the subscription as soon as possible (i.e, when an OnCompleted or OnError messages is published) _but need to consider sequences that never terminate (by OnCompleted or OnError)_

If you call a Subscribe method and ignore the return value, you have lost your only handle to unsubscribe. The subscription will still exist, and you have effectively lost access to this resource, which could result in leaking memory and running unwanted processes.

The Subscribe method returns an IDisposable, so that you can unsubscribe to a sequence and dispose of it easily. When you invoke the Dispose method on the observable sequence, the observer will stop listening to the observable for data.

> Many people who hear about the Dispose pattern for the first time complain that the GC isn't doing its job. They think it should collect resources, and that this is just like having to manage resources as you did in the unmanaged world. The truth is that the GC was never meant to manage resources. It was designed to manage memory and it is excellent in doing just that. - [Krzysztof Cwalina](http://blogs.msdn.com/b/kcwalina/) from [Joe Duffy's blog](http://www.bluebytesoftware.com/blog/2005/04/08/DGUpdateDisposeFinalizationAndResourceManagement.aspx).

You can use the IDisposable interface for more than the common use of deterministically releasing unmanaged resources. It is a useful tool for managing lifetime or scope of anything. 

ReactiveUI provides you with  [WhenActivated](/docs/handbook/when-activated) to help manage lifecyle and the Reactive Extensions provides several different options to help you with managing [lifetime, scope and resources](disposables).

## See Also

* https://msdn.microsoft.com/en-us/library/hh242977(v=vs.103).aspx
* http://www.introtorx.com/content/v1.0.10621.0/03_LifetimeManagement.html

