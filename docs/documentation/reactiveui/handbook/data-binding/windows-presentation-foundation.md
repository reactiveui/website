---
Order: 5
---
# Data Binding in WPF

[Run the complete page example](https://github.com/reactiveui/ReactiveUI/blob/main/src/examples/Documentation/Pages/platform-wpf/platform-wpf.csproj).

`ReactiveUI.WPF` supplies the base classes a WPF view needs to become an `IViewFor<TViewModel>`: `ReactiveWindow<T>`
for a window and `ReactiveUserControl<T>` for a user control. Each already implements `IViewFor<T>` and exposes
`ViewModel` as a `DependencyProperty`. A view only needs to inherit from one of them instead of writing the
`DependencyProperty` and the interface by hand. This page covers binding; [WPF](../platforms/wpf.md) covers the
rest of the package. `CourseListView`, the example's student list, is one of these views:

```csharp
public partial class CourseListView : ReactiveUserControl<CourseListViewModel>
{
    /// <summary>Initializes a new instance of the <see cref="CourseListView"/> class.</summary>
    public CourseListView()
    {
        InitializeComponent();

        _ = this.WhenActivated(d =>
        {
            _ = this.OneWayBind(ViewModel, vm => vm.Students, v => v.StudentList.ItemsSource)
                .DisposeWith(d);
            _ = this.Bind(ViewModel, vm => vm.SelectedStudent, v => v.StudentList.SelectedItem)
                .DisposeWith(d);
            _ = this.OneWayBind(ViewModel, vm => vm.Summary, v => v.SummaryHost.ViewModel)
                .DisposeWith(d);
            _ = this.BindCommand(
                    ViewModel,
                    vm => vm.OpenStudent,
                    v => v.OpenButton,
                    this.WhenAnyValue(v => v.ViewModel!.SelectedStudent).WhereNotNull())
                .DisposeWith(d);
        });
    }
}
```

Every binding call here — `Bind`, `OneWayBind` and `BindCommand` — is the same call any XAML-based view uses. [Data
Binding](index.md) covers them. [WhenActivated](../when-activated.md) covers why the block runs inside
`WhenActivated`: a live binding keeps the view and the view model alive for each other, so dispose it when the view
is hidden.

## Start ReactiveUI for WPF

An app registers WPF's services once, at startup, with `WithWpf` on the [app builder](../rxappbuilder.md). It adds
dependency-property observation and a scheduler that runs work on WPF's dispatcher thread. The example's `App`
does this before showing its main window:

```csharp
_ = RxAppBuilder.CreateReactiveUIBuilder().WithWpf().BuildApp();
```

`WithWpf` also has an overload on `IAppBuilder`, and calls `WithWpfConverters` and `WithWpfScheduler` for you if you
need only one of the two. See [Register platform modules](../../../binding/setup.md#register-platform-modules) for
the binding library's own version of the same registration.

## A window that hosts a router

`MainWindow`, the example's only window, inherits `ReactiveWindow<AppShell>` and hosts a `RoutedViewHost`, the
control that shows whichever view model a `RoutingState` is currently navigating to. [Routing](../routing.md)
covers the router itself. Skip wiring it up while a designer surface loads the window, using
[`GetIsDesignMode`](../design-time.md).
