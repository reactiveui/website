# Testing

ReactiveUI includes a few tools to help testing, building on what Rx already includes (see [introtorx](http://www.introtorx.com/content/v1.0.10621.0/16_TestingRx.html)) for details.

The utilities are included in the ReactiveUI.Testing nuget package.

## Custom Scheduler

A common pattern is to replace the schedulers ReactiveUI is using to test execution is occuring without injecting schedulers everywhere. This can be achieved in a couple of ways

```csharp
using(TestUtils.WithScheduler(ImmediateScheduler.Instance))
{
  // your test here
}
```

Or
```csharp
(new TestScheduler()).With(sched => {
  ... // Do something
  sched.AdvanceByMs(2 * 100); // Play with time
  ...
});
```


More details are on the [docs site](https://reactiveui.net/api/reactiveui.testing/testutils/).
