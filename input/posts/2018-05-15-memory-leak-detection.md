NoTitle: true
IsBlog: true
Title: Memory Leak Detection
Tags: Announcement
Author: Geoffrey Huntley
Published: 2018-05-15
---

Moments ago we merged [a contribution](https://github.com/reactiveui/ReactiveUI/pull/1527) by [Grzegorz Kotfis](https://twitter.com/gkotfis) that adds another layer of protection before the project moves towards [automated continuous delivery](https://reactiveui.net/blog/2018/05/moving-towards-vsts-and-continuous-deployment). The pull-request added a new project called `ReactiveUI.LeakTests` which allows the maintainers to specify tests that can determine memory leaks by checking memory usage for objects of a particular type, or tests that track memory traffic and fail in case the traffic exceeds some threshold. 

In other words, the ReactiveUI unit testing harness now includes a memory profiler. We are using the free [dotMemory Unit by JetBrains](https://www.jetbrains.com/help/dotmemory-unit/Introduction.html). It sounds complicated, but it's not. dotMemory Unit works with almost all of the unit-testing frameworks on the market including our testing framework of choice [xUnit.net](https://xunit.github.io/). 

```csharp
[Test]
public void Observable_Subscription_IsDisposed()
{
    ... // do some work

    dotMemory.Check(memory =>
        Assert.That(memory.GetObjects(where => where.Type.Is<ReactiveObject>()).ObjectsCount, Is.EqualTo(0)));
}
```

Thank-you Grzegorz!

ps. We are currently looking for folks who are interested in performance work to aide with critically accessing the performance of ReactiveUI, the [Reactive Extensions for .NET](https://reactiveui.net/blog/2018/05/system-reactive-has-a-new-home-on-github) and [System.Linq.Expressions](https://github.com/bartdesmet/ExpressionFutures). 

Sound interesting? Jump in [ReactiveUI Slack](https://join.slack.com/t/reactivex/shared_invite/zt-lt48skpz-G5WDYOAuzA80_MByZrLT0g), introduce yourself and ask how you can help out.
