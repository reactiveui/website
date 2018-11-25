ViewModel-based routing is supported for Xamarin.Forms, WinRT, WP8, UWP, Windows Forms and WPF Desktop applications. Routing is also possible on iOS and Android without Xamarin.Forms, but sometimes it can be difficult to make it work as expected. In case ViewModel-based routing is hard to implement in your particulat case, you can always use View-first routing and customize almost everything.

# About ReactiveUI Routing

ReactiveUI routing consists of an `IScreen` that contains current `RoutingState`, several  `IRoutableViewModel`s, and a platform-specific XAML control called `RoutedViewHost`. `RoutingState` manages the ViewModel Stack and allows ViewModels to navigate to other ViewModels. `IScreen` is the root of a navigation stack; despite the name, its views don't have to occupy the whole screen. `RoutedViewHost` monitors an instance of `RoutingState`, responding to any changes in the navigation stack by creating and embedding the appropriate view.

# Getting Started

First, we need to create a view model that implements the `IRoutableViewModel` interface.

```cs
public sealed class FirstViewModel : IRoutableViewModel
{
    // A string token representing the current view model, 
    // such as 'login' or 'user'. "first" in our case,
    // but can be whatever you like.
    public string UrlPathSegment => "first";
    
    // Instance of the host screen containing routing state 
    // to which this view model belongs.
    public IScreen HostScreen { get; }

    public FirstViewModel(IScreen screen)
    {
        // Inject the host screen via the constructor.
        HostScreen = screen;
    }
}
```

Then, we create an `IScreen` implementation containing current `RoutingState`.

```cs
public sealed class RouterViewModel : IScreen
{
    // The Router associated with this Screen.
    public RoutingState Router { get; }
  
    public RouterViewModel() 
    {
        // Initialize the Router.
        Router = new RoutingState();
        
        // Router uses Splat.Locator to resolve views for
        // view models, so we need to register our views
        // using Locator.CurrentMutable.Register* methods.
        var firstViewModel = new FirstViewModel(this);
        Locator.CurrentMutable.Register<IViewFor<FirstViewModel>>(
            () => new FirstView { DataContext = firstViewModel });
            
        // Manage the routing state. Use the 'Execute'
        // method to navigate to different pages.
        Router.Navigate.Execute(syncViewModel).Subscribe();
    }
}
```

Now we need to place the `RoutedViewHost` XAML control to our view that will contain our routable views and view models. Assuming the view model of this Window is an instance of `RouterViewModel` defined above. We bind the view model `Router` property to `RoutedViewHost.Router` property. 

```xml
<rxui:ReactiveWindow
    xmlns:rxui="http://reactiveui.net" 
    x:Class="ReactiveUI.Example.Views.RouterView"
    x:TypeArguments="vm:RouterViewModel"
    xmlns:vm="clr-namespace:ReactiveUI.Example.ViewModels"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    d:DataContext="{d:DesignInstance vm:RouterViewModel, IsDesignTimeCreatable=False}"
    mc:Ignorable="d" Title="ReactiveUI.Routing" Height="360" Width="620">
    <rxui:RoutedViewHost 
        HorizontalContentAlignment="Stretch"
        VerticalContentAlignment="Stretch"
        Router="{Binding Router}"/>
</rxui:ReactiveWindow>
```

Now, ReactiveUI view model -first routing works! You can use as many nested `IScreen`s and `RoutedViewHost`s as you wish and the routing will work fine. But remember, this works now only for XAML controls pages, for modals and popups it's better to use ReactiveUI `Interaction` feature.

# Assembly Scanning

If you'd like to register all view models and associated views in your application, use the following code:

```
// Splat uses assembly scanning here to register all views and view models.
Locator.CurrentMutable.RegisterViewsForViewModels(
  Assembly.GetCallingAssembly()
);
```


