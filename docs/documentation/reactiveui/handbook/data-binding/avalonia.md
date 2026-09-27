---
Order: 3
---
# Data Binding in Avalonia

An Avalonia view binds to its view model with the same `Bind`, `OneWayBind` and `BindCommand` calls as any other
platform. The Avalonia pieces, such as the view base classes and the start-up call, ship in the `ReactiveUI.Avalonia`
package. That package lives in its own repository, [reactiveui/ReactiveUI.Avalonia](https://github.com/reactiveui/ReactiveUI.Avalonia),
so the code on this page is quoted from its
[example app](https://github.com/reactiveui/ReactiveUI.Avalonia/tree/main/src/examples/ReactiveUI.Avalonia.Example).
[Installing for Avalonia](../../getting-started/installation/avalonia.md) lists the packages.

## Bind a view in three steps

1. **Start ReactiveUI with the app.** Call `UseReactiveUI` on Avalonia's `AppBuilder`. It registers the main-thread
   sequencer, activation and binding support. `RegisterReactiveUIViewsFromEntryAssembly` registers each view in your
   app, so view location can find it.

   ```cs
   public static AppBuilder BuildAvaloniaApp() => AppBuilder
       .Configure<App>()
       .UsePlatformDetect()
       .UseReactiveUI(rxui =>
       {
           // Optional: add custom registration here via rxui.WithRegistration(...)
       })
       .RegisterReactiveUIViewsFromEntryAssembly();
   ```

2. **Derive the view from a ReactiveUI base class.** `ReactiveWindow<TViewModel>` and `ReactiveUserControl<TViewModel>`
   implement `IViewFor<TViewModel>`. They store the view model in an Avalonia property, so XAML bindings see it too.
   Avalonia generates a field for each control that has an `x:Name` in the markup, so the view can name its controls
   directly.

3. **Bind inside `WhenActivated`.** [WhenActivated](../when-activated.md) runs its block when the view joins the
   visual tree and disposes everything the block added when the view leaves it. Add each binding to the block's
   `MultipleDisposable`, so no binding outlives the view.

   ```cs
   public sealed partial class CommandLabView : ReactiveUserControl<CommandLabViewModel>
   {
       public CommandLabView()
       {
           InitializeComponent();
           _ = this.WhenActivated(BindView);
       }

       private void BindView(ReactiveUI.Primitives.Disposables.MultipleDisposable disposables)
       {
           disposables.Add(this.Bind(ViewModel, static viewModel => viewModel.WorkItemText, static view => view.WorkItemTextBox.Text));
           disposables.Add(this.OneWayBind(ViewModel, static viewModel => viewModel.LastResult, static view => view.LastResultText.Text));
           disposables.Add(this.BindCommand(ViewModel, static viewModel => viewModel.RunWork, static view => view.RunWorkButton));
       }
   }
   ```

`WorkItemTextBox`, `LastResultText` and `RunWorkButton` are the `x:Name` values in the view's `.axaml` file. The
lambdas are `static` because they capture nothing. See
[Dispose your subscriptions](../../guidelines/framework/dispose-your-subscriptions.md) for why every binding goes
into the activation block.

## Activate the view model too

A view model that implements `IActivatableViewModel` gets its own `WhenActivated` block. That block runs only while
a view that calls `WhenActivated` shows the view model. [WhenActivated](../when-activated.md) covers both halves.

## Where to go next

- [Data Binding](index.md) explains `Bind`, `OneWayBind` and `BindCommand` and their converters.
- The [ReactiveUI.Avalonia README](https://github.com/reactiveui/ReactiveUI.Avalonia#readme) covers start-up with a
  dependency-injection container: Autofac, DryIoc, Microsoft.Extensions.DependencyInjection or Ninject.
