# ReactiveUI for .NET MAUI

**.NET MAUI** (Multi-platform App UI) is Microsoft's cross-platform framework for creating native mobile and desktop apps with C# and XAML. ReactiveUI provides first-class support for .NET MAUI with reactive MVVM patterns.

## Why ReactiveUI with MAUI?

- **Reactive Data Binding**: Automatic UI updates with WhenAnyValue
- **Reactive Commands**: Async command execution with built-in state
- **View Activation**: Automatic subscription lifecycle management
- **Cross-Platform**: Share view models across iOS, Android, Windows, and macOS
- **Modern Patterns**: Source generators eliminate boilerplate
- **Testable**: Full unit testing support for all reactive logic

## Quick Links

### Getting Started
- [Installation Guide](~/docs/getting-started/installation/maui.md)
- [Compelling Example](~/docs/getting-started/compelling-example.md)
- [Guidelines](~/docs/guidelines/index.md)

### Features
- [Data Binding](~/docs/handbook/data-binding/index.md)
- [Commands](~/docs/handbook/commands/index.md)
- [View Models](~/docs/handbook/view-models/index.md)
- [Navigation (Sextant)](~/docs/handbook/sextant/index.md)
- [Popups](~/docs/handbook/maui-plugins-popup/index.md)

### Samples
- [MAUI Samples Repository](https://github.com/reactiveui/ReactiveUI.Samples/tree/main/maui)
- [Sample Applications](~/docs/resources/samples.md)

## Platform-Specific Features

### Shell Integration

ReactiveUI works seamlessly with MAUI Shell:

```csharp
// Shell navigation with ReactiveUI
public partial class MainViewModel : ReactiveObject
{
    [ReactiveCommand]
    private async Task NavigateToDetails()
    {
        await Shell.Current.GoToAsync("details");
    }
}
```

### Platform APIs

Access platform-specific APIs reactively:

```csharp
// Geolocation
Observable.FromAsync(() => Geolocation.GetLocationAsync())
    .Subscribe(location => CurrentLocation = location);

// Connectivity
Connectivity.ConnectivityChanged
    .ToObservable()
    .Subscribe(e => IsConnected = e.NetworkAccess == NetworkAccess.Internet);
```

### Handlers and Controls

Use ReactiveUI with custom handlers:

```csharp
public partial class ReactiveEntry : Entry, IViewFor<EntryViewModel>
{
    public static readonly BindableProperty ViewModelProperty = 
        BindableProperty.Create(nameof(ViewModel), typeof(EntryViewModel), typeof(ReactiveEntry));
    
    public EntryViewModel ViewModel
    {
        get => (EntryViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }
    
    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = value as EntryViewModel;
    }
}
```

## Common Patterns

### Reactive Page

```csharp
public partial class MainPage : ReactiveContentPage<MainViewModel>
{
    public MainPage()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();
        
        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel, vm => vm.SearchText, v => v.SearchEntry.Text)
                .DisposeWith(disposables);
            
            this.BindCommand(ViewModel, vm => vm.SearchCommand, v => v.SearchButton)
                .DisposeWith(disposables);
        });
    }
}
```

### Reactive ViewModel

```csharp
public partial class MainViewModel : ReactiveObject
{
    [Reactive]
    private string _searchText = string.Empty;
    
    [ObservableAsProperty]
    private List<SearchResult> _searchResults;
    
    public MainViewModel()
    {
        SearchCommand = ReactiveCommand.CreateFromTask(
            async () => await SearchAsync(SearchText),
            this.WhenAnyValue(x => x.SearchText, text => !string.IsNullOrWhiteSpace(text)));
        
        SearchCommand
            .ToPropertyEx(this, x => x.SearchResults);
    }
    
    [ReactiveCommand]
    private async Task<List<SearchResult>> Search(string searchText)
    {
        return await SearchAsync(searchText);
    }
}
```

## Navigation

### MAUI Shell

```csharp
// Register routes
Routing.RegisterRoute("details", typeof(DetailsPage));

// Navigate
await Shell.Current.GoToAsync($"details?id={itemId}");
```

### Sextant (View Model First)

```csharp
// Setup
AppLocator.CurrentMutable.RegisterNavigationView();

// Navigate
var viewStack = AppLocator.Current.GetService<IViewStackService>();
await viewStack.PushPage<DetailsViewModel>();
```

## Styling and Theming

Use ReactiveUI with MAUI styles:

```xml
<ResourceDictionary>
    <Style TargetType="Button" x:Key="PrimaryButton">
        <Setter Property="BackgroundColor" Value="{StaticResource Primary}" />
        <Setter Property="TextColor" Value="White" />
    </Style>
</ResourceDictionary>
```

## Performance Tips

1. **Use ObservableAsPropertyHelper**: Avoid manual property notifications
2. **Throttle User Input**: Use Throttle for search boxes
3. **Async Commands**: Always use async operations for network/disk
4. **Dispose Properly**: Use WhenActivated for automatic cleanup
5. **Platform Caching**: Leverage MAUI's image caching

## Testing

```csharp
[Fact]
public async Task SearchCommand_ReturnsResults()
{
    // Arrange
    var viewModel = new MainViewModel();
    viewModel.SearchText = "test";
    
    // Act
    await viewModel.SearchCommand.Execute();
    
    // Assert
    viewModel.SearchResults.Should().NotBeEmpty();
}
```

## Community Resources

- [ReactiveUI Slack](https://join.slack.com/t/reactivex/shared_invite/zt-lt48skpz-G5WDYOAuzA80_MByZrLT0g)
- [GitHub Discussions](https://github.com/reactiveui/ReactiveUI/discussions)
- [Stack Overflow](https://stackoverflow.com/questions/tagged/reactiveui+maui)

## Migration from Xamarin.Forms

See our comprehensive [Xamarin to MAUI Migration Guide](~/docs/upgrading/xamarin-to-maui.md).

## Related Topics

- [Installation](~/docs/getting-started/installation/maui.md)
- [Data Binding](~/docs/handbook/data-binding/index.md)
- [Sextant Navigation](~/docs/handbook/sextant/index.md)
- [Popups](~/docs/handbook/maui-plugins-popup/index.md)
