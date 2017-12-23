cTitle: Disposables
---

ReactiveUI provides you with  [WhenActivated](/docs/handbook/when-activated) to help manage lifecyle and the Reactive Extensions provides several different implementations of the IDisposable interface to help you with managing lifetime, scope and resources.

# Disposable

# BooleanDisposable

# CancellationDisposable

# CompositeDisposable
https://msdn.microsoft.com/en-us/library/system.reactive.disposables.compositedisposable(v=vs.103).aspx

# ContextDisposable

# MultipleAssignmentDisposable

# RefCountDisposable

# ScheduledDisposable

# SerialDisposable
* It is one of the most useful disposables
* What's useful about it is that when you set the disposable, the previous one is Disposed
* Any time you have to manage something where only one can be alive at a time
* Or if you set it to Disposable.Empty, zero or one of something
* It's also atomic aka thread safe, and immune to double-disposing

https://msdn.microsoft.com/en-us/library/system.reactive.disposables.serialdisposable(v=vs.103).aspx

# SingleAssignmentDisposable

For a full rundown of each of the implementations see the Disposables


# See Also
* http://www.introtorx.com/content/v1.0.10621.0/20_Disposables.html
