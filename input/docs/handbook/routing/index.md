ViewModel-based routing is supported for Xamarin.Forms, WinRT, WP8, UWP, Windows Forms and WPF Desktop applications. Routing is also possible on iOS and Android without Xamarin.Forms, but sometimes it can be difficult to make it work as expected. In case ViewModel-based routing is hard to implement, you can always use View-first routing and customize almost everything.

# About ReactiveUI Routing

ReactiveUI routing consists of an `IScreen` that contains current `RoutingState`, several  `IRoutableViewModel`s, and a platform-specific XAML control called `RoutedViewHost`. `RoutingState` manages the ViewModel Stack and allows ViewModels to navigate to other ViewModels. `IScreen` is the root of a navigation stack; despite the name, its views don't have to occupy the whole screen. `RoutedViewHost` monitors an instance of `RoutingState`, responding to any changes in the navigation stack by creating and embedding the appropriate view.

# A Compelling Example

In Visual Studio, create a new WPF project. Give it a name `ReactiveRouting`. Install the `ReactiveUI.WPF` nuget package into it. Then, create a view model that implements the `IRoutableViewModel` interface, named `FirstViewModel`. The `UrlPathSegment` property is a string token representing the current view model, such as 'login' or 'user'. 'first' in our case, but can be whatever you prefer. The `HostScreen` property typically contains the instance of the host screen used by an app.

**FirstViewModel.cs**

```cs
public class FirstViewModel : ReactiveObject, IRoutableViewModel
{
    public string UrlPathSegment => "first";
    
    public IScreen HostScreen { get; }

    public FirstViewModel(IScreen screen = null)
    {
        HostScreen = screen ?? Locator.Current.GetService<IScreen>();
    }
}
```

Then, create a new `UserControl` — a view for the `FirstViewModel` declared above. Derive it from the `ReactiveUserControl<TViewModel>` class.

**FirstView.xaml**

```xml
<rxui:ReactiveUserControl
    x:Class="ReactiveRouting.FirstView"
    x:TypeArguments="vm:FirstViewModel"
    xmlns:rxui="http://reactiveui.net"
    xmlns:vm="clr-namespace:ReactiveRouting"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    Background="LightSeaGreen"
    mc:Ignorable="d">
    <TextBlock x:Name="PathTextBlock" Foreground="White" Margin="10" />
</rxui:ReactiveUserControl>
```

In this example, we are setting up the bindings in code-behind.

**FirstView.xaml.cs**

```cs
public partial class FirstView : ReactiveUserControl<FirstViewModel>
{
    public FirstView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel, x => x.UrlPathSegment, x => x.PathTextBlock.Text)
                .DisposeWith(disposables);
        });
    }
}
```

Then, create an `IScreen` implementation containing current `RoutingState` that manages the navigation stack.

**MainViewModel.cs**

```cs
public class MainViewModel : ReactiveObject, IScreen
{
    // The Router associated with this Screen.
    // Required by the IScreen interface.
    public RoutingState Router { get; }
        
    // The command that navigates a user to first view model.
    public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

    // The command that navigates a user back.
    public ReactiveCommand<Unit, Unit> GoBack { get; }

    public MainViewModel()
    {
        // Initialize the Router.
        Router = new RoutingState();

        // Router uses Splat.Locator to resolve views for
        // view models, so we need to register our views
        // using Locator.CurrentMutable.Register* methods.
        //
        // Instead of registering views manually, you 
        // can use custom IViewLocator implementation,
        // see "View Location" section for details.
        //
        Locator.CurrentMutable.Register(() => new FirstView(), typeof(IViewFor<FirstViewModel>));

        // Manage the routing state. Use the 'Router.Navigate.Execute'
        // method to navigate to different view models.
        GoNext = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new FirstViewModel()));

        // You can also ask the router to go back.
        GoBack = ReactiveCommand.CreateFromObservable(() => Router.NavigateBack.Execute(Unit.Default));
    }
}
```

Now we need to place the `RoutedViewHost` XAML control to our view that will contain our routable views and view models. We bind the view model `MainViewModel.Router` property to `RoutedViewHost.Router` property. The `RoutedViewHost` control is platform-specific, contained in such packages, as `ReactiveUI.WPF`, `ReactiveUI.XamForms`, `ReactiveUI.WinForms`.

**MainWindow.xaml**

```xml
<rxui:ReactiveWindow
    xmlns:rxui="http://reactiveui.net" 
    x:Class="ReactiveRouting.MainWindow"
    x:TypeArguments="vm:MainViewModel"
    xmlns:vm="clr-namespace:ReactiveRouting"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    Title="ReactiveUI.Routing" Height="360" Width="620">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="*" />
            <RowDefinition Height="Auto" />
        </Grid.RowDefinitions>
        <rxui:RoutedViewHost 
            Grid.Row="0" 
            x:Name="RoutedViewHost"
            HorizontalContentAlignment="Stretch"
            VerticalContentAlignment="Stretch" />
        <StackPanel Grid.Row="1" Orientation="Horizontal" Margin="15">
            <Button x:Name="GoNextButton" Content="Go to first" />
            <Button x:Name="GoBackButton" Content="Go back" Margin="5 0 0 0" />
        </StackPanel>
    </Grid>
</rxui:ReactiveWindow>
```

**MainWindow.xaml.cs**

Here is the code-behind for the MainWindow declared above. We use `ReactiveWindow<TViewModel>` here for WPF, for Xamarin.Forms it should typically be `ReactiveMasterDetailPage<TViewModel>`. The main goal on this step is to bind the `MainViewModel.Router` property to the `RoutedViewHost.Router` property, so the `RoutedViewHost` control will display the appropriate view.

```cs
// We use ReactiveWindow here for WPF, but could actually use
// ReactiveUserControl or a custom IViewFor implementation. For
// Xamarin.Forms, use ReactiveMasterDetailPage.
public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();
        this.WhenActivated(disposables =>
        {
            // Bind the view model router to RoutedViewHost.Router property.
            this.OneWayBind(ViewModel, x => x.Router, x => x.RoutedViewHost.Router)
                .DisposeWith(disposables);
            this.BindCommand(ViewModel, x => x.GoNext, x => x.GoNextButton)
                .DisposeWith(disposables);
            this.BindCommand(ViewModel, x => x.GoBack, x => x.GoBackButton)
                .DisposeWith(disposables);
        });
    }
}
```

<img src="./routing.gif" width="60%" />

Now, ReactiveUI view model -first routing should work as expected. You can use as many nested `IScreen`s and `RoutedViewHost`s as you wish and the routing should work fine. But remember, this works only for XAML pages, for modals and popups it's better to use ReactiveUI [Interaction](../interactions) feature. 

> **Note** Routing supports not only WPF. See [Xamarin.Forms routing sample](https://github.com/reactiveui/ReactiveUI/tree/master/samples/xamarin-forms/MasterDetail) and [Windows Forms routing sample](https://github.com/Asesjix/ReactiveUI.Winforms.Samples). If you experience any issues with following this tutorial, head over to [ReactiveUI Slack](https://reactiveui.net/slack) and feel free to ask questions. We are always ready to help out.

# View Location

Instead of registering Views by hand, you can override default `IViewLocator` implementation. While bootstrapping your routing, register your view locator using `Locator.CurrentMutable.RegisterLazySingleton`. See [View Location](../views) for details.

```cs
public class SimpleViewLocator : IViewLocator
{
    public IViewFor ResolveView<T>(T viewModel, string contract = null) where T : class
    {
        if (viewModel is FirstViewModel)
            return new FirstView { ViewModel = viewModel };
        throw new Exception($"Could not find the view for view model {typeof(T).Name}.")
    }
}

// Register the SimpleViewLocator.
Locator.CurrentMutable.RegisterLazySingleton(() => new SimpleViewLocator(), typeof(IViewLocator));
```

# Assembly Scanning

If you'd like to register all view models and associated views in your application, use the following code:

```cs
// Splat uses assembly scanning here to register all views and view models.
Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
```