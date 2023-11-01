
## Dispose your subscriptions

[Lifetime management](http://www.introtorx.com/Content/v1.0.10621.0/03_LifetimeManagement.html)

```csharp
this.WhenActivated(
    disposables =>
    {
        this.WhenAnyValue(...)
            .DisposeWith(disposables);
    });
```

See also [when activated](~/docs/handbook/when-activated.md)


Not _all_ subscriptions need to be disposed. It's like events. If a component exposes an event and also subscribes to it itself, it doesn't need to unsubscribe. That's because the subscription is manifested as the component having a reference to itself. Same is true with Rx. If you're a VM and you e.g. `WhenAnyValue` against your own property, there's no need to clean that up because that is manifested as the VM having a reference to itself.
