# Almost always use `this` as the left hand side of a `WhenAny` call.

## Do
```csharp
public class MyViewModel
{
  public MyViewModel(IDependency dependency)
  {
    Ensure.ArgumentNotNull(dependency, "dependency");

    this.Dependency = dependency;

    this.stuff = this.WhenAny(x => x.Dependency.Stuff, x => x.Value)
      .ToProperty(this, x => x.Stuff);
  }

  public IDependency Dependency { get; private set; }

  readonly ObservableAsPropertyHelper<IStuff> stuff;
  public IStuff Stuff
  {
    get { return this.stuff.Value; }
  }
}
```

## Don't
```csharp
public class MyViewModel(IDependency dependency)
{
  stuff = dependency.WhenAny(x => x.Stuff, x => x.Value)
    .ToProperty(this, x => x.Stuff);
}
```

## Why?
* The lifetime of `dependency` is unknown - if it is a singleton it
 could introduce memory leaks into your application.

## Caveat: still dispose your subscriptions

Using `this` on the left-hand side avoids the singleton-holds-the-view-model leak, but it does **not** remove the need to manage subscription lifetime. If you `Subscribe` / `BindTo` / `InvokeCommand` against an observable rooted in a longer-lived dependency, the dependency's `PropertyChanged` handler still holds a reference to your subscription's closure. Tie those subscriptions to a `CompositeDisposable` and dispose them when the view model goes away — typically via `WhenActivated` and `DisposeWith`:

```csharp
this.WhenActivated(disposables =>
{
    this.WhenAnyValue(x => x.Dependency.Stuff)
        .BindTo(this, x => x.LocalCopy)
        .DisposeWith(disposables);
});
```

`ToProperty(this, ...)` is the one common exception: the generated `ObservableAsPropertyHelper<T>` field's lifetime is bound to `this`, so it dies with the view model.
