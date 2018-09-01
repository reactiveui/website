Order: 10
---
A big part of understanding ReactiveUI is understanding Reactive Programming, as ReactiveUI is a library built on <a href="https://github.com/dotnet/reactive">Reactive Extensions</a>. Reactive Extensions is a bunch of extension methods for programming in a reactive manner. Reactive programming is programming with asynchronous data streams, called <a href="http://reactivex.io/documentation/observable.html">observables</a>. Unlike tasks, <a href="http://reactivex.io/documentation/observable.html">observables</a> represent one or more values over time. It allows for Linq like operations to mutate the observable event stream and allows for flexible marshalling onto desired threads.

When you develop .NET applications, especially large-scale and cross-platform ones, you need to write portable and maintainable code. In XAML-powered platforms, such as Windows Presentation Foundation, Universal Windows Platform, Xamarin Forms and Avalonia UI you can achieve this by implementing the MVVM pattern. MVVM stands for Model-View-ViewModel, where Model represents services, data transfer objects and database entities related to the application domain, View is the UI and ViewModel’s responsibility is to tie these two layers together. ViewModel encapsulates interaction with Model, exposing properties and commands for XAML UI to bind to. 

ReactiveUI allows you to combine the MVVM pattern with Reactive Programming using such features, as [WhenAnyValue](https://reactiveui.net/docs/handbook/when-any/), [ReactiveCommand](https://reactiveui.net/docs/handbook/commands/), [ObservableAsPropertyHelper](https://reactiveui.net/docs/handbook/oaph/), [Binding](https://reactiveui.net/docs/handbook/data-binding/) and [WhenActivated](https://reactiveui.net/docs/handbook/when-activated/).

<img src="./mvvm.png" width="500">

# A Compelling Example

Let's create a simple application demonstrating a number of ReactiveUI functionalities, without getting into too many under-the-hood details. We will create a WPF application, which will allow us to search through NuGet public repositories. The full code of the application is shown at the end of this chapter, and we will show relevant snippets as we go. 

### 1. Create the project
In <a href="https://visualstudio.microsoft.com/">Visual Studio</a>, create a new WPF application (.NET 4.6.1 or above), use `ReactiveDemo` app name. Our view has been already created for us, the `MainWindow`, so we will proceed with creating our ViewModel.

### 2. Add NuGet packages
```
Install-Package ReactiveUI.WPF
```
The complete list containing NuGet packages for all supported platforms <a href="https://reactiveui.net/docs/getting-started/installation/nuget-packages/">can be found here</a>. <a href="https://www.nuget.org/packages/reactiveui/">ReactiveUI</a> main package should normally be installed into you .NET Standard class libraries containing platform-agnostic code (repositories, services, DTOs, view-models), and ReactiveUI.XXX packages are platform-specific, so we use <a href="https://www.nuget.org/packages/ReactiveUI.WPF/">ReactiveUI.WPF</a> in this tutorial as we are developing a tiny WPF application that doesn't need code sharing.
```
Install-Package Nuget.Client
Install-Package NuGet.Protocol.Core.v3
```
We also need a Nuget client library in this tutorial, and we are going to install and use <a href="https://docs.microsoft.com/en-us/nuget/reference/nuget-client-sdk">NuGet Client</a>.

### 3. Create ViewModels
```csharp
// AppViewModel is where we will describe the interaction of our application.
// We can describe the entire application in one class since it's very small now. 
// Most ViewModels will derive off ReactiveObject, while most Model classes will 
// most derive off INotifyPropertyChanged
public class AppViewModel : ReactiveObject
{
    // In ReactiveUI, this is the syntax to declare a read-write property
    // that will notify Observers, as well as WPF, that a property has 
    // changed. If we declared this as a normal property, we couldn't tell 
    // when it has changed!
    private string _searchTerm;
    public string SearchTerm
    {
        get => _searchTerm;
        set => this.RaiseAndSetIfChanged(ref _searchTerm, value);
    }

    // ObservableAsPropertyHelper
    // 
    // Here's the interesting part: In ReactiveUI, we can take IObservables
    // and "pipe" them to a Property - whenever the Observable yields a new
    // value, we will notify ReactiveObject that the property has changed.
    // 
    // To do this, we have a class called ObservableAsPropertyHelper - this
    // class subscribes to an Observable and stores a copy of the latest value.
    // It also runs an action whenever the property changes, usually calling
    // ReactiveObject's RaisePropertyChanged.
    private readonly ObservableAsPropertyHelper<IEnumerable<NugetDetailsViewModel>> _searchResults;
    public IEnumerable<NugetDetailsViewModel> SearchResults => _searchResults.Value;

    // Here, we want to create a property to represent when the application 
    // is performing a search (i.e. when to show the "spinner" control that 
    // lets the user know that the app is busy). We also declare this property
    // to be the result of an Observable (i.e. its value is derived from 
    // some other property)
    private readonly ObservableAsPropertyHelper<bool> _isAvailable;
    public bool IsAvailable => _isAvailable.Value;

    public AppViewModel()
    {
        // These are provided as part of the Nuget.Core nuget package.
        // Our example will allow you to search the main nuget.org library.
        var providers = new List<Lazy<INuGetResourceProvider>>();
        providers.AddRange(Repository.Provider.GetCoreV3()); // Add v3 API support
        var packageSource = new PackageSource("https://api.nuget.org/v3/index.json");
        var sourceRepository = new SourceRepository(packageSource, providers);

        // Creating our UI declaratively
        // 
        // The Properties in this ViewModel are related to each other in different 
        // ways - with other frameworks, it is difficult to describe each relation
        // succinctly; the code to implement "The UI spinner spins while the search 
        // is live" usually ends up spread out over several event handlers.
        //
        // However, with ReactiveUI, we can describe how properties are related in a 
        // very organized clear way. Let's describe the workflow of what the user does 
        // in this application, in the order they do it.

        // We're going to take a Property and turn it into an Observable here - this
        // Observable will yield a value every time the Search term changes, which in
        // the XAML, is connected to the TextBox. 
        //
        // We're going to use the Throttle operator to ignore changes that happen too 
        // quickly, since we don't want to issue a search for each key pressed! We 
        // then pull the Value of the change, then filter out changes that are identical, 
        // as well as strings that are empty.
        //
        // We then do a SelectMany() which converts a async task that returns a 
        // IEnumerable<T> into IObservable<IEnumerable<T>>. If subsequent requests are 
        // made the CancellationToken is called. We then ObservableOn the main thread, 
        // everything up until this point has been running on a separate thread due 
        // to the Throttle() 
        //
        // We then use a ObservableAsPropertyHelper and the ToProperty() method to allow
        // us to have the latest results that we can expose through the property to the View.
        _searchResults = this
            .WhenAnyValue(x => x.SearchTerm)
            .Throttle(TimeSpan.FromMilliseconds(800))
            .Select(term => term?.Trim())
            .DistinctUntilChanged()
            .Where(term => !string.IsNullOrWhiteSpace(term))
            .SelectMany(async (term, token) =>
            {
                var searchResource = await sourceRepository.GetResourceAsync<PackageSearchResource>();
                var searchMetadata = await searchResource.SearchAsync(term, new SearchFilter(false), 0, 10, null, token);
                return searchMetadata.Select(x => new NugetDetailsViewModel(x));
            })
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, x => x.SearchResults);
            
        // Here we listen for all exceptions that our reactive pipeline might throw. In
        // most cases using MessageBox.Show at the ViewModel layer isn't a good practice,
        // especially if you are goind to build a cross-platform app, but for a hello-world
        // app which we are building now it's fine.
        _searchResults.ThrownExceptions.Subscribe(x => MessageBox.Show(x.Message));

        // A helper method we can use for Visibility or Spinners to show if results are available.
        // We get the latest value of the SearchResults and make sure it's not null.
        _isAvailable = this
            .WhenAnyValue(x => x.SearchResults)
            .Select(searchResults => searchResults != null)
            .ToProperty(this, x => x.IsAvailable);
    }
}
```

The goal of the ReactiveUI syntax for read-write properties is to notify Observers that a property has changed. Otherwise we would not be able to know when it was changed. 
  
In cases when we don't need to provide for two-way binding between the View and the ViewModel, we can use one of many ReactiveUI Helpers, to notify Observers of a changing read-only value in the ViewModel. We use the <a href="https://reactiveui.net/docs/handbook/oaph/">ObservableAsPropertyHelper</a> twice, once to turn a generic IEnumerable<T> into an observable read-only collection, and then to change the visibility of an indicator to show that a request is currently executing.

This also works in the opposite direction, when we take the `SearchTerm` property and <a href="https://reactiveui.net/docs/handbook/when-any/">turn it into an observable</a>. This means that we are notified every time a change occurs in the UI. Using Reactive Extensions, we then <a href="http://reactivex.io/documentation/operators/debounce.html">throttle</a> those events, and ensure that the search occurs no sooner than 800ms after the last keystroke. And if at that point the user did not change the last value, or if the search term is blank, we ignore the event completely. We have another ObservableAsPropertyHelper property `IsAvailable` which is generated by determining if our current SearchResults is null.

Let's now create a `NugetDetailsViewModel` that will wrap out NuGet metadata into a more usable class for our View. It includes a [ReactiveCommand](../handbook/commands) for opening the NuGet Repository URL.

```cs
// This class wraps out NuGet model object into a ViewModel and allows
// us to have a ReactiveCommand to open the NuGet package URL.
public class NugetDetailsViewModel : ReactiveObject
{
    private readonly IPackageSearchMetadata metadata;
    private readonly Uri defaultUrl;

    public NugetDetailsViewModel(IPackageSearchMetadata searchMetadata)
    {
        metadata = searchMetadata;
        defaultUrl = new Uri("https://github.com/NuGet/Media/blob/master/Images/MainLogo/128x128/nuget_128.png?raw=true");
        OpenPage = ReactiveCommand.Create(() => { Process.Start(ProjectUrl.ToString()); });
    }
    
    public Uri IconUrl => metadata.IconUrl ?? defaultUrl;
    public string Description => metadata.Description;
    public Uri ProjectUrl => metadata.ProjectUrl;
    public string Title => metadata.Title;

    // ReactiveCommand allows us to execute logic without exposing any of the 
    // implementation details with the View. The generic parameters are the 
    // input into the command and it's output. In our case we don't have any 
    // input or output so we use Unit which in Reactive speak means a void type.
    public ReactiveCommand<Unit, Unit> OpenPage { get; }
}
```

### 4.Create Views

<details><summary>Create Views using ReactiveUI type-safe bindings (recommended)</summary>

First, we need to register our views in the `App.cs` file.

```cs
public partial class App : Application
{
    public App()
    {
        // A helper method that will register all classes that derive off IViewFor 
        // into our dependency injection container. ReactiveUI uses Splat for it's 
        // dependency injection by default, but you can override this if you like.
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
    }
}
```

Then we need to derive the MainWindow from `IViewFor<T>`. We are going to use <a href="https://reactiveui.net/docs/handbook/data-binding/">ReactiveUI Binding</a> to bind our ViewModel to our View. Reactive binding is a cross platform way of consistently binding properties on your ViewModel to controls on your View. The ReactiveUI binding has a few advantages over the XAML based binding. The first advantage is that property name changes will generate a compile error rather than runtime errors. 

```csharp
public partial class MainWindow : IViewFor<AppViewModel>
{
    // Using a DependencyProperty as the backing store for ViewModel.  
    // This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", 
            typeof(AppViewModel), typeof(MainWindow), 
            new PropertyMetadata(null));

    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new AppViewModel();

        // We create our bindings here. These are the code behind bindings which allow 
        // type safety. The bindings will only become active when the Window is being shown.
        // We register our subscription in our disposableRegistration, this will cause 
        // the binding subscription to become inactive when the Window is closed.
        // The disposableRegistration is a CompositeDisposable which is a container of 
        // other Disposables. We use the DisposeWith() extension method which simply adds 
        // the subscription disposable to the CompositeDisposable.
        this.WhenActivated(disposableRegistration =>
        {
            // Notice we don't have to provide a converter, on WPF a global converter is
            // registered which knows how to convert a boolean into visibility.
            this.OneWayBind(ViewModel, 
                viewModel => viewModel.IsAvailable, 
                view => view.searchResultsListBox.Visibility)
                .DisposeWith(disposableRegistration); 
                
            this.OneWayBind(ViewModel, 
                viewModel => viewModel.SearchResults, 
                view => view.searchResultsListBox.ItemsSource)
                .DisposeWith(disposableRegistration); 
                
            this.Bind(ViewModel, 
                viewModel => viewModel.SearchTerm, 
                view => view.searchTextBox.Text)
                .DisposeWith(disposableRegistration);
        });
    }

    // Our main view model instance.
    public AppViewModel ViewModel
    {
        get => (AppViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    // This is required by the interface IViewFor, you always just set it to use the 
    // main ViewModel property. Note on XAML based platforms we have a control called
    // ReactiveUserControl that abstracts this.
    object IViewFor.ViewModel 
    { 
        get => ViewModel; 
        set => ViewModel = (AppViewModel)value; 
    }
}
```

Reactive Binding allows you to modify the ViewModel property by using Binding Converters. These are Binding Converters are able to be registered globally or they can be declared locally. We are going to use Binding Converters in two instances, firstly we have a global converter registered by default on XAML projects that converts a boolean to Visibility, and the second to convert our project URL into a BitmapImage locally one.

Now we declare the XAML for our Main Window.

```xml
<Window x:Class="ReactiveDemo.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        Title="NuGet Browser"
        mc:Ignorable="d" Height="450" Width="800">
    <Grid Margin="12">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        <TextBlock FontSize="16" FontWeight="SemiBold" 
                   VerticalAlignment="Center" Text="Search for: "/>
        <TextBox Grid.Column="1" Margin="6 0 0 0" x:Name="searchTextBox" />
        <ListBox x:Name="searchResultsListBox" Grid.ColumnSpan="3" 
                 Grid.Row="1" Margin="0,6,0,0" HorizontalContentAlignment="Stretch"
                 ScrollViewer.HorizontalScrollBarVisibility="Disabled" />
    </Grid>
</Window>
```

We are now going to create a view for our NugetDetailsViewModel. This will automatically get displayed in the ListBox in the MainWindow. When using Reactive Binding on XAML platforms, if no ItemTemplate has been set, it will look for a IViewFor<T> inside our Dependency Injection and display the Item using that control.

```cs
// The class derives off ReactiveUserControl which contains the ViewModel property.
// In our MainWindow when we register the ListBox with the collection of 
// NugetDetailsViewModels if no ItemTemplate has been declared it will search for 
// a class derived off IViewFor<NugetDetailsViewModel> and show that for the item.
public partial class NugetDetailsView : ReactiveUserControl<NugetDetailsViewModel>
{
    public NugetDetailsView()
    {
        InitializeComponent();
        this.WhenActivated(disposableRegistration =>
        {
            // Our 4th parameter we convert from Url into a BitmapImage. 
            // This is an easy way of doing value conversion using ReactiveUI binding.
            this.OneWayBind(ViewModel, 
                viewModel => viewModel.IconUrl, 
                view => view.iconImage.Source, 
                url => url == null ? null : new BitmapImage(url))
                .DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel, 
                viewModel => viewModel.Title, 
                view => view.titleRun.Text)
                .DisposeWith(disposableRegistration);
                
            this.OneWayBind(ViewModel, 
                viewModel => viewModel.Description, 
                view => view.descriptionRun.Text)
                .DisposeWith(disposableRegistration);
            
            this.BindCommand(ViewModel, 
                viewModel => viewModel.OpenPage, 
                view => view.openButton)
                .DisposeWith(disposableRegistration);
        });
    }
}
```

Notice we convert our URI above into a BitmapImage just for the OneWayBind. ReactiveUI allows us to quickly convert types which is much easier syntax than the XAML Value Converters. Now let's declare our XAML for the NuGet details View:

```xml
<reactiveui:ReactiveUserControl
    x:Class="ReactiveDemo.NugetDetailsView"
    xmlns:reactiveDemo="clr-namespace:ReactiveDemo"
    x:TypeArguments="reactiveDemo:NugetDetailsViewModel"
    xmlns:reactiveui="http://reactiveui.net"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>
        <Image x:Name="iconImage" Margin="6" Width="64" Height="64"
               HorizontalAlignment="Center" VerticalAlignment="Center"/>
        <TextBlock Grid.Column="1" TextWrapping="WrapWithOverflow" 
                   Margin="6" VerticalAlignment="Center">
            <Run FontSize="14" FontWeight="SemiBold" x:Name="titleRun"/>
            <LineBreak />
            <Run FontSize="12" x:Name="descriptionRun"/>
            <LineBreak />
            <Hyperlink x:Name="openButton">Open</Hyperlink>
        </TextBlock>
    </Grid>
</reactiveui:ReactiveUserControl>
```

Now you can search repositories on NuGet using your own app!

<img src="./demo-app.jpg" width="600"/>
<br />

</details>
<details><summary>Create Views using traditional XAML markup bindings</summary>

TODO: Add xaml markup bindigns

</details>

# Discover ReactiveUI

Now you know ReactiveUI, but we have more to offer. If you'd like to discover all features ReactiveUI has, visit our <a href="https://reactiveui.net/docs/handbook/">Handbook</a>! Take a look at a <a href="https://github.com/reactiveui/ReactiveUI/tree/master/integrationtests">truly cross-platform demo app</a> that works on each platform ReactiveUI supports.

Also give a try to <a href="https://github.com/RolandPheasant/DynamicData">DynamicData</a> - reactive collections based on reactive extensions. Use <a href="https://github.com/RolandPheasant/DynamicData">DynamicData</a> for transforming and observing dynamically changing data sets in your ReactiveUI applications. You can also watch <a href="https://reactiveui.net/docs/resources/videos">videos about Reactive Extensions and ReactiveUI</a>, or view sources of <a href="https://reactiveui.net/docs/samples/">open-source applications built with ReactiveUI</a>. We have a <a href="https://reactiveui.net/blog/">blog</a> and a <a href="https://twitter.com/reactivexui">twitter account</a> for you to stay tuned. <a href="https://github.com/reactiveui/ReactiveUI">Star ReactiveUI on Github</a>!


