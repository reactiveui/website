.

# CompositeDisposable

https://msdn.microsoft.com/en-us/library/system.reactive.disposables.compositedisposable(v=vs.103).aspx

# SerialDisposable
* It is one of the most useful disposables
* What's useful about it is that when you set the disposable, the previous one is Disposed
* Any time you have to manage something where only one can be alive at a time
* Or if you set it to Disposable.Empty, zero or one of something
* It's also atomic aka thread safe, and immune to double-disposing

https://msdn.microsoft.com/en-us/library/system.reactive.disposables.serialdisposable(v=vs.103).aspx
