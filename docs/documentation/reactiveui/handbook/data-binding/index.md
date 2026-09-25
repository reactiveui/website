---
Order: 5
---
# Data Binding in ReactiveUI

A binding connects a property on a view model to a property on a view. When one side changes, the binding writes the new value to the other side. ReactiveUI bindings are written with lambda expressions, so a renamed property breaks the build instead of failing at run time.

[ReactiveUI.Binding](../../../binding/index.md) is the binding engine. A source generator reads each binding at build time and writes the code for it, so bindings need no reflection and work with trimming and Native AOT. The Binding section covers:

* [Observing property changes](../../../binding/observing.md) with `WhenChanged`, `WhenAnyValue` and the other observation methods.
* [Bindings](../../../binding/bindings.md) with `BindOneWay`, `BindTwoWay`, `BindTo`, `BindCommand` and `BindInteraction`.
* [Converters](../../../binding/converters.md) and [custom converters](../../../binding/custom-converters.md) for values whose types differ.
* [Notification mechanisms](../../../binding/mechanisms.md), [views](../../../binding/views.md), [threading](../../../binding/threading.md) and [setup](../../../binding/setup.md).

## Platforms

The platform pages below cover the view base classes and activation for each UI framework. Each one builds on [WhenActivated](../when-activated.md) and the platform base classes.

* [Android](android/index.md)

* [iOS](ios.md)

* [MAUI](../../getting-started/installation/maui.md)

* [Windows Presentation Foundation](windows-presentation-foundation.md)

* [Windows Forms](../../../binding/threading.md)

* [Avalonia UI](avalonia.md)

## Implement IViewFor on the view

A view needs an `IViewFor<TViewModel>` implementation before it can use the view-first binding methods. The way to implement it depends on the platform.

* **iOS:** change the base class to one of the Reactive UIKit classes, such as `ReactiveViewController`, and implement `ViewModel` with `RaiseAndSetIfChanged`. You can also implement `INotifyPropertyChanged` on the view and make sure the `ViewModel` property signals changes.

* **Android:** change the base class to one of the Reactive Activity or Fragment classes, such as `ReactiveActivity<T>`. You can also implement `IViewFor<T>` on the view and make sure the `ViewModel` property signals changes.

* **XAML-based platforms:** implement `IViewFor<T>` by hand and make `ViewModel` a `DependencyProperty`. A `UserControl` can use the `ReactiveUserControl<TViewModel>` base class instead.
