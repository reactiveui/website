# ReactiveUI for WPF

**Windows Presentation Foundation (WPF)** is Microsoft's rich desktop application framework for Windows. ReactiveUI provides comprehensive support for WPF with reactive MVVM patterns that make desktop development more productive and maintainable.

## Why ReactiveUI with WPF?

- **Reactive Data Binding**: Powerful binding with WhenAnyValue
- **Dispatcher Integration**: Automatic thread marshaling
- **Command Support**: ReactiveCommand with async/await
- **View Activation**: Lifecycle management for subscriptions
- **Design-Time Support**: ViewModel design data support
- **Testable**: Full unit testing without UI dependencies

## Quick Links

### Getting Started
- [Installation Guide](~/docs/getting-started/installation/windows-presentation-foundation.md)
- [Compelling Example](~/docs/getting-started/compelling-example.md)
- [Guidelines](~/docs/guidelines/index.md)

### Features
- [Data Binding](~/docs/handbook/data-binding/index.md)
- [Commands](~/docs/handbook/commands/index.md)
- [View Models](~/docs/handbook/view-models/index.md)
- [ObservableEvents](~/docs/handbook/observable-events/index.md)

### Samples
- [WPF Samples Repository](https://github.com/reactiveui/ReactiveUI.Samples/tree/main/wpf)
- [GitHub for Visual Studio](https://github.com/github/VisualStudio) (Production App)

## Platform-Specific Features

### ReactiveWindow

```csharp
public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();
        
        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel, vm => vm.Name, v => v.NameTextBox.Text)
                .DisposeWith(disposables);
            
            this.BindCommand(ViewModel, vm => vm.SaveCommand, v => v.SaveButton)
                .DisposeWith(disposables);
            
            this.OneWayBind(ViewModel, vm => vm.IsLoading, v => v.LoadingIndicator.Visibility)
                .DisposeWith(disposables);
        });
    }
}
```

### Dispatcher Scheduler

ReactiveUI automatically uses the Dispatcher:

```csharp
// Automatically marshals to UI thread
Observable.Timer(TimeSpan.FromSeconds(1))
    .ObserveOn(RxSchedulers.MainThreadScheduler)
    .Subscribe(_ => UpdateUI());
```

### Dependency Properties

Bind to WPF dependency properties:

```csharp
this.WhenActivated(disposables =>
{
    this.Bind(ViewModel, vm => vm.Value, v => v.Slider.Value)
        .DisposeWith(disposables);
    
    this.OneWayBind(ViewModel, vm => vm.Text, v => v.TextBlock.Text)
        .DisposeWith(disposables);
});
```

## Common Patterns

### ReactiveUserControl

```csharp
public partial class SearchControl : ReactiveUserControl<SearchViewModel>
{
    public SearchControl()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel, vm => vm.SearchText, v => v.SearchBox.Text)
                .DisposeWith(disposables);
            
            this.BindCommand(ViewModel, vm => vm.SearchCommand, v => v.SearchButton)
                .DisposeWith(disposables);
        });
    }
}
```

### ObservableEvents

Convert WPF events to observables:

```csharp
using ReactiveMarbles.ObservableEvents;

this.WhenActivated(disposables =>
{
    // Mouse events
    this.Events().MouseMove
        .Throttle(TimeSpan.FromMilliseconds(100))
        .Subscribe(e => UpdateMousePosition(e.GetPosition(this)))
        .DisposeWith(disposables);
    
    // Window events
    this.Events().Closing
        .Subscribe(e => 
        {
            if (HasUnsavedChanges)
            {
                e.Cancel = !ConfirmClose();
            }
        })
        .DisposeWith(disposables);
});
```

### Validation

```csharp
public partial class EditViewModel : ReactiveValidationObject
{
    [Reactive]
    private string _email = string.Empty;
    
    [Reactive]
    private string _password = string.Empty;
    
    public EditViewModel()
    {
        this.ValidationRule(
            vm => vm.Email,
            email => email?.Contains("@") == true,
            "Please enter a valid email");
        
        this.ValidationRule(
            vm => vm.Password,
            password => password?.Length >= 6,
            "Password must be at least 6 characters");
        
        SaveCommand = ReactiveCommand.CreateFromTask(
            SaveAsync,
            this.IsValid());
    }
    
    [ReactiveCommand]
    private async Task Save() => await SaveAsync();
}
```

## MVVM Patterns

### View-ViewModel Connection

**XAML:**
```xml
<Window x:Class="MyApp.Views.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:vm="clr-namespace:MyApp.ViewModels"
        Title="My App" Height="450" Width="800">
    
    <Grid>
        <TextBox x:Name="NameTextBox" />
        <Button x:Name="SaveButton" Content="Save" />
    </Grid>
</Window>
```

**Code-Behind:**
```csharp
public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();
    }
}
```

### Design-Time Data

```csharp
public class DesignTimeMainViewModel : MainViewModel
{
    public DesignTimeMainViewModel()
    {
        Name = "Design Time Name";
        Items = new ObservableCollection<string> { "Item 1", "Item 2" };
    }
}
```

**In XAML:**
```xml
<Window xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:vm="clr-namespace:MyApp.ViewModels"
        mc:Ignorable="d"
        d:DataContext="{d:DesignInstance Type=vm:DesignTimeMainViewModel, IsDesignTimeCreatable=True}">
```

## Advanced Features

### Custom Controls

```csharp
public class ReactiveButton : Button, IViewFor<ButtonViewModel>
{
    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(
            nameof(ViewModel),
            typeof(ButtonViewModel),
            typeof(ReactiveButton),
            new PropertyMetadata(null));
    
    public ButtonViewModel ViewModel
    {
        get => (ButtonViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }
    
    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = value as ButtonViewModel;
    }
}
```

### Attached Behaviors

```csharp
public static class TextBoxBehavior
{
    public static readonly DependencyProperty SelectAllOnFocusProperty =
        DependencyProperty.RegisterAttached(
            "SelectAllOnFocus",
            typeof(bool),
            typeof(TextBoxBehavior),
            new PropertyMetadata(false, OnSelectAllOnFocusChanged));
    
    public static bool GetSelectAllOnFocus(DependencyObject obj) =>
        (bool)obj.GetValue(SelectAllOnFocusProperty);
    
    public static void SetSelectAllOnFocus(DependencyObject obj, bool value) =>
        obj.SetValue(SelectAllOnFocusProperty, value);
    
    private static void OnSelectAllOnFocusChanged(
        DependencyObject d, 
        DependencyPropertyChangedEventArgs e)
    {
        if (d is TextBox textBox && (bool)e.NewValue)
        {
            textBox.Events().GotFocus
                .Subscribe(_ => textBox.SelectAll());
        }
    }
}
```

## Collections with DynamicData

```csharp
public partial class ItemListViewModel : ReactiveObject
{
    private readonly SourceCache<Item, int> _itemsCache;
    private readonly ReadOnlyObservableCollection<ItemViewModel> _items;
    
    public ReadOnlyObservableCollection<ItemViewModel> Items => _items;
    
    public ItemListViewModel()
    {
        _itemsCache = new SourceCache<Item, int>(x => x.Id);
        
        _itemsCache.Connect()
            .Transform(item => new ItemViewModel(item))
            .Filter(vm => vm.IsVisible)
            .Sort(SortExpressionComparer<ItemViewModel>.Ascending(x => x.Name))
            .ObserveOn(RxSchedulers.MainThreadScheduler)
            .Bind(out _items)
            .Subscribe();
    }
}
```

## Performance Tips

1. **Use Virtualization**: For large lists, use VirtualizingStackPanel
2. **Freeze Resources**: Freeze brushes and other freezable resources
3. **Throttle Events**: Use Throttle for high-frequency events
4. **Async Commands**: Keep UI responsive with async operations
5. **Proper Disposal**: Always use WhenActivated

## Testing

```csharp
[WpfFact]
public async Task MainWindow_LoadsData()
{
    // Arrange
    var window = new MainWindow();
    window.ViewModel = new MainViewModel();
    
    // Act
    await window.ViewModel.LoadCommand.Execute();
    
    // Assert
    window.ViewModel.Data.Should().NotBeNull();
}
```

## Styling

```xml
<Window.Resources>
    <Style TargetType="Button">
        <Setter Property="Background" Value="{StaticResource PrimaryBrush}" />
        <Setter Property="Foreground" Value="White" />
        <Setter Property="Padding" Value="10,5" />
        <Style.Triggers>
            <Trigger Property="IsMouseOver" Value="True">
                <Setter Property="Background" Value="{StaticResource PrimaryDarkBrush}" />
            </Trigger>
        </Style.Triggers>
    </Style>
</Window.Resources>
```

## Best Practices

1. **Use ReactiveWindow/ReactiveUserControl**: For proper lifetime management
2. **WhenActivated**: Always wrap subscriptions
3. **ObserveOn MainThread**: Before updating UI
4. **DisposeWith**: Automatic cleanup
5. **Design-Time Data**: Better design experience

## Common Issues

### Issue: UI Not Updating

**Solution**: Ensure ObserveOn(RxSchedulers.MainThreadScheduler)

```csharp
backgroundOperation
    .ObserveOn(RxSchedulers.MainThreadScheduler)
    .Subscribe(result => UpdateUI(result));
```

### Issue: Memory Leaks

**Solution**: Always use WhenActivated

```csharp
this.WhenActivated(disposables =>
{
    // All subscriptions here are automatically disposed
});
```

## Community Resources

- [WPF Samples](https://github.com/reactiveui/ReactiveUI.Samples/tree/main/wpf)
- [GitHub for Visual Studio (Real App)](https://github.com/github/VisualStudio)
- [ReactiveUI Slack](https://join.slack.com/t/reactivex/shared_invite/zt-lt48skpz-G5WDYOAuzA80_MByZrLT0g)

## Related Topics

- [Installation](~/docs/getting-started/installation/windows-presentation-foundation.md)
- [Data Binding](~/docs/handbook/data-binding/index.md)
- [ObservableEvents](~/docs/handbook/observable-events/index.md)
- [Commands](~/docs/handbook/commands/index.md)
