---
NoTitle: true
---
In addition to registering `Interaction` handlers manually, we also provide a set of view extension methods for setting up bindings, both of which mimic the `handler` parameter of the `RegisterHandler` overloads:

```cs
IDisposable BindInteraction<TViewModel, TView, TInput, TOutput>(
    this TView view,
    TViewModel? viewModel,
    Expression<Func<TViewModel, Interaction<TInput, TOutput>>> propertyName,
    Func<InteractionContext<TInput, TOutput>, Task> handler);

IDisposable BindInteraction<TViewModel, TView, TInput, TOutput, TDontCare>(
    this TView view,
    TViewModel? viewModel,
    Expression<Func<TViewModel, Interaction<TInput, TOutput>>> propertyName,
    Func<InteractionContext<TInput, TOutput>, IObservable<TDontCare>> handler);
```

Registering handlers manually is fine for simple scenarios. But if, for example, you expect your `Interaction` or one of its ancestors to change, the complexity starts increasing because of the need to dispose of the old and subscribe to the latest:

```cs
var interactionDisposable = new SerialDisposable();

this
    .WhenAnyValue(x => x.ViewModel.MyInteraction)
    .Where(x => x != null)
    .Do(x => interactionDisposable.Disposable = x.RegisterHandler(context => /*Do Something*/))
    .Finally(() => interactionDisposable?.Dispose())
    .Subscribe();
```

Compare that with the binding approach...

```cs
// In a view
this.BindInteraction(
    this.ViewModel,
    vm => vm.MyInteraction,
    context => /*Do Something*/);
```

and it's clear to see the benefits.

Just like the rest of the `Bind` family, it's easy to "unhook" the binding by disposing of the returned `IDisposable` object. If you're using the `WhenActivated` feature, it's simply:

```cs
this.WhenActivated(
    disposables =>
    {
        this.BindInteraction(
                this.ViewModel,
                vm => vm.MyInteraction,
                context => /*Do Something*/)
            .DisposeWith(disposables);
    });
```
