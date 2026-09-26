# Performance optimization

Most of what makes a ReactiveUI app fast is not specific to ReactiveUI: it is the stream itself. The
[Primitives best practices](../../../primitives/best-practices.md) page covers marking lambdas `static`,
sharing expensive work, filtering to real changes, and the other habits that apply to any stream built on
`ReactiveUI.Primitives`.

Two habits are specific to building a ReactiveUI view model:

- **Marshal to the UI thread only where a value reaches a bound property**, not at every step in a chain. See
  [UI thread and schedulers](ui-thread-and-schedulers.md) and [only one WitnessOn per
  chain](../debugging/threading.md#only-one-witnesson-per-chain).
- **Let `WhenActivated` stop a view model's subscriptions while its view is off screen.** A background timer or
  a polling stream then stops running for a screen nobody is looking at. See [when
  activated](../../handbook/when-activated.md).
