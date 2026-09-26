---
Order: 18
---
# Design-time support

A designer surface, such as the Visual Studio or Rider XAML preview, loads your views to draw them without running
your app. `WhenActivated` needs an activation fetcher to know when a view is shown and hidden, and a designer never
provides one. Without a check for that case, activation would throw every time a designer opened a view.

`GetIsDesignMode`, an extension method on `IActivatableView`, reports whether a designer surface is loading the
view. It returns `false` unless a platform package overrides it for its own view types. The WPF package, for
example, backs it with `DesignerProperties.GetIsInDesignMode`. `WhenActivated` calls it internally. With no
activation fetcher registered and the view in design mode, it does nothing instead of throwing. A designer preview
then needs none of the services the running app has. See [Design mode](when-activated.md#design-mode) for the check
in place and what it returns for a view outside a designer.

Call `GetIsDesignMode` yourself to skip work a designer cannot run, such as creating a router or navigating it. The
WPF grade-book example's `MainWindow` constructor does this before it builds a real `AppShell`.

For design-time data in XAML markup, bind through `d:DataContext` and a design-time implementation of your view
model's interface, the way you would for any XAML-based framework. ReactiveUI adds nothing of its own on top of
that. [Data Binding](data-binding/index.md) covers the bindings a view sets up once it has a real view model.
