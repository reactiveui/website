# WPF

`ReactiveUI.WPF` connects ReactiveUI to Windows Presentation Foundation. Add it by following
[Installation](../../getting-started/installation/windows-presentation-foundation.md). `ReactiveUI.WPF` also ships
as `ReactiveUI.WPF.Reactive`, built from the same source, for an app that uses System.Reactive.

It gives your windows, pages and user controls a `ViewModel` property WPF can bind to, and a host that shows the
current page of a [router](../../handbook/routing.md). It also adds a dispatcher-backed main-thread sequencer and
the value converters a WPF binding needs for `bool`/`Visibility` conversion.

- [WPF](../../handbook/platforms/wpf.md) walks through building an app with the package, start to finish: a
  university grade book with routed navigation, two-way binding and a validated text box.
- [Windows Presentation Framework guidelines](windows-presentation-framework.md) lists the WPF-specific practices
  worth calling out on their own.

`ReactiveUI.Blend` and `ReactiveUI.Drawing` are two smaller packages built for WPF: `ReactiveUI.Blend` adds Blend
behaviors and triggers that read from a stream, and `ReactiveUI.Drawing` registers a bitmap loader. The handbook's
[WPF](../../handbook/platforms/wpf.md) page covers both.
