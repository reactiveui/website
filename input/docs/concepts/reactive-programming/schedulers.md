Order: 10
Title: Schedulers
---

.https://staltz.com/primer-on-rxjs-schedulers.html


Couple of things that might be helpful, RxUI has some helpers to wrap the schedulers up, and you can just pass ImmediateScheduler.Instance into them - this is neat because it limits the scope and stops you sharing schedulers between tests.

You'll need the Testing package, and then can do, e.g.
```
using(TestUtils.WithScheduler(ImmediateScheduler.Instance)){
 // your test here
}
```
docs are here https://reactiveui.net/api/reactiveui.testing/testutils/949c3f58

(internally, this does what you're doing anyway, but it does tidy up after itself)

The other thing that we use in some tests (e.g. if you're wanting to check whether the observable flips between some values during a few scenarios) is `CreateCollection`, e.g. https://github.com/reactiveui/ReactiveUI/blob/develop/src/ReactiveUI.Tests/ReactiveCommandTest.cs#L35


The advantage of the that way is you get the scheduler to play with, which is nice if you're using a `TestScheduler` because you don't have to worry about tidying up after yourself (e.g. (new TestScheduler()).With(sched=>{});)

Each _thread_ has its own set of test schedulers that the `With` extension manages for you. This means that even if your tests run asynchronously, they will in theory not stomp on each other's schedulers.
