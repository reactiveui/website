# Sextant - ReactiveUI Navigation Library

**Sextant** is a view model first navigation library for ReactiveUI applications that provides a simple, reactive, and testable approach to navigation across multiple platforms.

## Overview

Sextant focuses on:
- **ViewModel-First Navigation**: Navigate using view models, not views
- **Reactive APIs**: All navigation operations return `IObservable<Unit>`
- **Lifecycle Hooks**: Parameterized navigation with `INavigable` interface
- **Cross-Platform**: Uniform abstractions across MAUI, Avalonia, and legacy Xamarin.Forms
- **Testable**: Full unit testing support for navigation logic

## Packages

Sextant is modular with platform-specific packages:

| Package | Description | NuGet |
|---------|-------------|-------|
| **Sextant** | Core navigation abstractions (required) | [![NuGet](https://img.shields.io/nuget/v/sextant.svg)](https://www.nuget.org/packages/sextant) |
| **Sextant.Maui** | .NET MAUI implementation | [![NuGet](https://img.shields.io/nuget/v/Sextant.Maui.svg)](https://www.nuget.org/packages/Sextant.Maui) |
| **Sextant.Avalonia** | Avalonia implementation | [![NuGet](https://img.shields.io/nuget/v/Sextant.Avalonia.svg)](https://www.nuget.org/packages/Sextant.Avalonia) |
| **Sextant.Plugins.Popup** | Mopups-based modal plugin (MAUI) | [![NuGet](https://img.shields.io/nuget/v/Sextant.Plugins.Popup.svg)](https://www.nuget.org/packages/Sextant.Plugins.Popup) |

### Installation

```bash
# Core (always required)
dotnet add package Sextant

# Platform-specific
dotnet add package Sextant.Maui        # For .NET MAUI
dotnet add package Sextant.Avalonia    # For Avalonia

# Optional: Popup support for MAUI
dotnet add package Sextant.Plugins.Popup
```

## Platform Support

Sextant follows ReactiveUI platform minimums:
- **.NET MAUI**: .NET 8.0+
- **Avalonia**: .NET 8.0+
- **Xamarin.Forms**: Legacy support (use Sextant 3.x or migrate to MAUI)

> **Migration Note**: For Xamarin.Forms projects, consider migrating to .NET MAUI. See our [Xamarin to MAUI Migration Guide](~/docs/upgrading/xamarin-to-maui.md).

## Getting Started

### .NET MAUI Setup

#### 1. Register Navigation Services

In your `App.xaml.cs` or during DI setup:

```csharp
using ReactiveUI;
using ReactiveUI.Maui;
using Sextant;
using Sextant.Maui;
using Splat;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        // Register navigation components
        AppLocator.CurrentMutable
            // Register view locator
            .RegisterConstant(ViewLocator.Current, typeof(IViewLocator))
            // Register Sextant navigation view (MAUI)
            .RegisterNavigationView()
            // Register view model factory
            .RegisterViewModelFactory(() => new DefaultViewModelFactory())
            // Register navigation service
            .RegisterParameterViewStackService()
            // Register views and view models
            .RegisterViewForNavigation<HomeView, HomeViewModel>(
                () => new HomeView(), 
                () => new HomeViewModel())
            .RegisterViewForNavigation<DetailsView, DetailsViewModel>(
                () => new DetailsView(), 
                () => new DetailsViewModel());
        
        // Set MainPage to NavigationView
        MainPage = AppLocator.Current.GetNavigationView();
        
        // Push initial page
        AppLocator.Current
            .GetService<IParameterViewStackService>()
            .PushPage<HomeViewModel>(resetStack: true, animate: false)
            .Subscribe();
    }
}
```

#### 2. Create a View Model

```csharp
using ReactiveUI;
using Sextant;
using System.Reactive;

public class HomeViewModel : ReactiveObject, IViewModel
{
    private readonly IViewStackService _viewStack;
    
    public HomeViewModel(IViewStackService viewStack = null)
    {
        _viewStack = viewStack ?? AppLocator.Current.GetService<IViewStackService>();
        
        OpenDetails = ReactiveCommand.CreateFromObservable(
            () => _viewStack.PushPage<DetailsViewModel>(),
            outputScheduler: RxSchedulers.MainThreadScheduler);
        
        OpenModal = ReactiveCommand.CreateFromObservable(
            () => _viewStack.PushModal<AboutViewModel>(),
            outputScheduler: RxSchedulers.MainThreadScheduler);
    }
    
    public string Id => nameof(HomeViewModel);
    
    public ReactiveCommand<Unit, Unit> OpenDetails { get; }
    public ReactiveCommand<Unit, Unit> OpenModal { get; }
}
```

#### 3. Create a View

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:rxui="clr-namespace:ReactiveUI.Maui;assembly=ReactiveUI.Maui"
    xmlns:vm="clr-namespace:MyApp.ViewModels"
    x:Class="MyApp.Views.HomeView"
    x:TypeArguments="vm:HomeViewModel"
    x:DataType="vm:HomeViewModel">
    
    <VerticalStackLayout Spacing="10" Padding="20">
        <Button x:Name="DetailsButton" Text="Open Details" />
        <Button x:Name="ModalButton" Text="Open Modal" />
    </VerticalStackLayout>
</ContentPage>
```

```csharp
using ReactiveUI;
using ReactiveUI.Maui;

public partial class HomeView : ReactiveContentPage<HomeViewModel>
{
    public HomeView()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            this.BindCommand(ViewModel, vm => vm.OpenDetails, v => v.DetailsButton)
                .DisposeWith(disposables);
            
            this.BindCommand(ViewModel, vm => vm.OpenModal, v => v.ModalButton)
                .DisposeWith(disposables);
        });
    }
}
```

### Avalonia Setup

#### 1. Register Navigation Services

```csharp
using Avalonia;
using Avalonia.Controls;
using ReactiveUI;
using Sextant;
using Sextant.Avalonia;
using Splat;

public class App : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        // Register navigation components
        AppLocator.CurrentMutable
            .RegisterConstant(ViewLocator.Current, typeof(IViewLocator))
            .RegisterNavigationView(() => new Sextant.Avalonia.NavigationView())
            .RegisterViewModelFactory(() => new DefaultViewModelFactory())
            .RegisterViewForNavigation<HomeView, HomeViewModel>(
                () => new HomeView(), 
                () => new HomeViewModel());
        
        // Get navigation service
        var viewStack = AppLocator.Current.GetService<IViewStackService>();
        viewStack.PushPage<HomeViewModel>(resetStack: true).Subscribe();
        
        // Set main window
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new Window 
            { 
                Content = AppLocator.Current.GetNavigationView() 
            };
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}
```

## Navigation Services

Sextant provides two main navigation services:

### IViewStackService

Basic view model first navigation without parameters:

```csharp
public interface IViewStackService
{
    // Push pages
    IObservable<Unit> PushPage<TViewModel>(
        string contract = null, 
        bool resetStack = false, 
        bool animate = true) where TViewModel : IViewModel;
    
    IObservable<Unit> PushPage(
        IViewModel viewModel, 
        string contract = null, 
        bool resetStack = false, 
        bool animate = true);
    
    // Push modals
    IObservable<Unit> PushModal<TViewModel>(
        string contract = null, 
        bool withNavigationPage = true) where TViewModel : IViewModel;
    
    IObservable<Unit> PushModal(
        IViewModel modal, 
        string contract = null, 
        bool withNavigationPage = true);
    
    // Pop operations
    IObservable<Unit> PopPage(bool animate = true);
    IObservable<Unit> PopModal(bool animate = true);
    IObservable<Unit> PopToRootPage(bool animate = true);
    
    // Stack observables
    IObservable<IImmutableList<IViewModel>> PageStack { get; }
    IObservable<IImmutableList<IViewModel>> ModalStack { get; }
    
    // Top view models
    IObservable<IViewModel> TopPage();
    IObservable<IViewModel> TopModal();
}
```

### IParameterViewStackService

Navigation with parameter passing and lifecycle hooks:

```csharp
public interface IParameterViewStackService : IViewStackService
{
    // Push with parameters
    IObservable<Unit> PushPage<TViewModel>(
        INavigationParameter parameter, 
        string contract = null, 
        bool resetStack = false, 
        bool animate = true) where TViewModel : INavigable;
    
    IObservable<Unit> PushPage(
        INavigable viewModel, 
        INavigationParameter parameter, 
        string contract = null, 
        bool resetStack = false, 
        bool animate = true);
    
    IObservable<Unit> PushModal<TViewModel>(
        INavigationParameter parameter, 
        string contract = null, 
        bool withNavigationPage = true) where TViewModel : INavigable;
    
    IObservable<Unit> PushModal(
        INavigable modal, 
        INavigationParameter parameter, 
        string contract = null, 
        bool withNavigationPage = true);
    
    // Pop with parameters
    IObservable<Unit> PopPage(
        INavigationParameter parameter, 
        bool animate = true);
}
```

## Parameter Passing and Lifecycle

### INavigable Interface

Implement `INavigable` to receive navigation parameters and lifecycle notifications:

```csharp
using Sextant;
using System.Reactive;

public class DetailsViewModel : ReactiveObject, INavigable
{
    public string Id => nameof(DetailsViewModel);
    
    public int ItemId { get; private set; }
    public string ItemName { get; private set; }
    
    // Called before navigation begins
    public IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter)
    {
        // Validate or prepare for navigation
        return Observable.Return(Unit.Default);
    }
    
    // Called after navigation completes
    public IObservable<Unit> WhenNavigatedTo(INavigationParameter parameter)
    {
        // Read parameters
        if (parameter.TryGetValue("ItemId", out int itemId))
        {
            ItemId = itemId;
        }
        
        if (parameter.TryGetValue("ItemName", out string itemName))
        {
            ItemName = itemName;
        }
        
        // Load data based on parameters
        return LoadDataAsync();
    }
    
    // Called when navigating away
    public IObservable<Unit> WhenNavigatedFrom(INavigationParameter parameter)
    {
        // Save state or cleanup
        return SaveStateAsync();
    }
    
    private IObservable<Unit> LoadDataAsync() => 
        Observable.Return(Unit.Default);
    
    private IObservable<Unit> SaveStateAsync() => 
        Observable.Return(Unit.Default);
}
```

### Passing Parameters

```csharp
public class HomeViewModel : ReactiveObject, IViewModel
{
    private readonly IParameterViewStackService _viewStack;
    
    public HomeViewModel(IParameterViewStackService viewStack)
    {
        _viewStack = viewStack;
        
        OpenDetails = ReactiveCommand.CreateFromObservable(
            () =>
            {
                var parameters = new NavigationParameter
                {
                    { "ItemId", 123 },
                    { "ItemName", "Sample Item" },
                    { "IsEditMode", true }
                };
                
                return _viewStack.PushPage<DetailsViewModel>(parameters);
            },
            outputScheduler: RxSchedulers.MainThreadScheduler);
    }
    
    public string Id => nameof(HomeViewModel);
    public ReactiveCommand<Unit, Unit> OpenDetails { get; }
}
```

## Advanced Patterns

### Navigation with Result

```csharp
public class SelectItemViewModel : ReactiveObject, INavigable
{
    private readonly Subject<Item> _selectedItemSubject = new();
    private readonly IViewStackService _viewStack;
    
    public IObservable<Item> SelectedItem => _selectedItemSubject.AsObservable();
    
    public ReactiveCommand<Item, Unit> SelectItem { get; }
    
    public SelectItemViewModel(IViewStackService viewStack)
    {
        _viewStack = viewStack;
        
        SelectItem = ReactiveCommand.CreateFromObservable<Item>(item =>
        {
            _selectedItemSubject.OnNext(item);
            _selectedItemSubject.OnCompleted();
            return _viewStack.PopPage();
        });
    }
    
    public string Id => nameof(SelectItemViewModel);
}

// Usage
var selectVm = new SelectItemViewModel(viewStack);
selectVm.SelectedItem
    .Take(1)
    .Subscribe(item => ProcessSelectedItem(item));

await viewStack.PushPage(selectVm);
```

### Navigation Guards

```csharp
public class EditViewModel : ReactiveObject, INavigable
{
    [Reactive]
    public bool HasUnsavedChanges { get; set; }
    
    public string Id => nameof(EditViewModel);
    
    public IObservable<Unit> WhenNavigatedFrom(INavigationParameter parameter)
    {
        if (!HasUnsavedChanges)
        {
            return Observable.Return(Unit.Default);
        }
        
        // Show confirmation dialog
        return Observable.FromAsync(async () =>
        {
            var confirmed = await ShowDiscardConfirmationAsync();
            if (!confirmed)
            {
                throw new NavigationException("Navigation cancelled by user");
            }
        });
    }
    
    private async Task<bool> ShowDiscardConfirmationAsync()
    {
        // Show dialog implementation
        return await Task.FromResult(true);
    }
}
```

### Conditional Navigation

```csharp
public class MainViewModel : ReactiveObject, IViewModel
{
    public ReactiveCommand<Unit, Unit> NavigateCommand { get; }
    
    public MainViewModel(IViewStackService viewStack)
    {
        var canNavigate = this.WhenAnyValue(
            x => x.IsValid, 
            x => x.IsConnected,
            (valid, connected) => valid && connected);
        
        NavigateCommand = ReactiveCommand.CreateFromObservable(
            () => viewStack.PushPage<NextViewModel>(),
            canNavigate,
            RxSchedulers.MainThreadScheduler);
    }
    
    public string Id => nameof(MainViewModel);
    
    [Reactive]
    public bool IsValid { get; set; }
    
    [Reactive]
    public bool IsConnected { get; set; }
}
```

### Modal with Navigation Stack

```csharp
// Present modal with its own navigation stack
await viewStack.PushModal<ModalRootViewModel>(withNavigationPage: true);

// Present modal without navigation stack
await viewStack.PushModal<SimpleModalViewModel>(withNavigationPage: false);
```

### Stack Management

```csharp
// Reset stack to single page
await viewStack.PushPage<HomeViewModel>(resetStack: true);

// Pop to root (clear all except first page)
await viewStack.PopToRootPage();

// Observe stack changes
viewStack.PageStack
    .Subscribe(stack => 
    {
        Console.WriteLine($"Page count: {stack.Count}");
    });
```

## Popup Plugin (MAUI Only)

For MAUI applications, use **Sextant.Plugins.Popup** for Mopups-based popups:

```bash
dotnet add package Sextant.Plugins.Popup
```

### Setup

```csharp
using Sextant.Plugins.Popup;

// In MauiProgram.cs
builder.ConfigureMopups();

// Register popup service
AppLocator.CurrentMutable.RegisterPopupViewStackService();
```

### Usage

```csharp
public class HomeViewModel : ReactiveObject, IViewModel
{
    private readonly IPopupViewStackService _popupService;
    
    public HomeViewModel(IPopupViewStackService popupService)
    {
        _popupService = popupService;
        
        ShowPopup = ReactiveCommand.CreateFromObservable(
            () => _popupService.PushPopup<PopupViewModel>(),
            outputScheduler: RxSchedulers.MainThreadScheduler);
    }
    
    public string Id => nameof(HomeViewModel);
    public ReactiveCommand<Unit, Unit> ShowPopup { get; }
}
```

## Contracts

Contracts allow registering multiple views for the same view model:

```csharp
// Register multiple views
AppLocator.CurrentMutable
    .RegisterViewForNavigation<DetailView, DetailViewModel>(
        () => new DetailView(), 
        () => new DetailViewModel(),
        contract: "Phone")
    .RegisterViewForNavigation<DetailTabletView, DetailViewModel>(
        () => new DetailTabletView(), 
        () => new DetailViewModel(),
        contract: "Tablet");

// Navigate with contract
await viewStack.PushPage<DetailViewModel>(contract: "Tablet");
```

## Testing Navigation

```csharp
using Xunit;
using NSubstitute;
using Sextant;

public class HomeViewModelTests
{
    [Fact]
    public async Task OpenDetails_ShouldPushDetailsViewModel()
    {
        // Arrange
        var viewStack = Substitute.For<IViewStackService>();
        viewStack.PushPage<DetailsViewModel>(null, false, true)
            .Returns(Observable.Return(Unit.Default));
        
        var viewModel = new HomeViewModel(viewStack);
        
        // Act
        await viewModel.OpenDetails.Execute();
        
        // Assert
        await viewStack.Received(1).PushPage<DetailsViewModel>(
            Arg.Any<string>(), 
            Arg.Any<bool>(), 
            Arg.Any<bool>());
    }
    
    [Fact]
    public async Task SelectItem_ShouldPassParameters()
    {
        // Arrange
        var parameterViewStack = Substitute.For<IParameterViewStackService>();
        parameterViewStack
            .PushPage<DetailsViewModel>(
                Arg.Any<INavigationParameter>(), 
                null, 
                false, 
                true)
            .Returns(Observable.Return(Unit.Default));
        
        var viewModel = new HomeViewModel(parameterViewStack);
        viewModel.SelectedItemId = 123;
        
        // Act
        await viewModel.OpenDetails.Execute();
        
        // Assert
        await parameterViewStack.Received(1).PushPage<DetailsViewModel>(
            Arg.Is<INavigationParameter>(p => 
                p.ContainsKey("ItemId") && 
                (int)p["ItemId"] == 123),
            Arg.Any<string>(),
            Arg.Any<bool>(),
            Arg.Any<bool>());
    }
}
```

## Best Practices

1. **Use IParameterViewStackService**: When passing data or using lifecycle hooks
2. **Inject Navigation Services**: Pass via constructor for testability
3. **Observe on Main Thread**: Use `RxSchedulers.MainThreadScheduler` for UI operations
4. **Dispose Subscriptions**: Always use `DisposeWith(disposables)` in `WhenActivated`
5. **Implement INavigable**: For view models that need lifecycle notifications
6. **Use Contracts Sparingly**: Only when truly need multiple views per view model
7. **Test Navigation Logic**: Write unit tests for all navigation scenarios

## Common Patterns

### Master-Detail Flow

```csharp
public class MasterViewModel : ReactiveObject, IViewModel
{
    public ReactiveCommand<Item, Unit> NavigateToDetail { get; }
    
    public MasterViewModel(IParameterViewStackService viewStack)
    {
        NavigateToDetail = ReactiveCommand.CreateFromObservable<Item>(item =>
        {
            var parameters = new NavigationParameter
            {
                { "Item", item }
            };
            return viewStack.PushPage<DetailViewModel>(parameters);
        });
    }
    
    public string Id => nameof(MasterViewModel);
}
```

### Wizard/Multi-Step

```csharp
public class WizardCoordinator
{
    private readonly IViewStackService _viewStack;
    private readonly Type[] _steps;
    private int _currentStep;
    
    public WizardCoordinator(IViewStackService viewStack)
    {
        _viewStack = viewStack;
        _steps = new[]
        {
            typeof(Step1ViewModel),
            typeof(Step2ViewModel),
            typeof(Step3ViewModel)
        };
    }
    
    public async Task StartWizard()
    {
        _currentStep = 0;
        await _viewStack.PushPage((IViewModel)Activator.CreateInstance(_steps[0]), 
            resetStack: true);
    }
    
    public async Task NextStep()
    {
        if (_currentStep < _steps.Length - 1)
        {
            _currentStep++;
            await _viewStack.PushPage(
                (IViewModel)Activator.CreateInstance(_steps[_currentStep]));
        }
    }
    
    public async Task PreviousStep()
    {
        if (_currentStep > 0)
        {
            _currentStep--;
            await _viewStack.PopPage();
        }
    }
}
```

## Troubleshooting

### Navigation Not Working

**Problem**: Navigation command executes but nothing happens

**Solution**: Ensure you subscribe to the navigation observable:

```csharp
// Wrong ?
viewStack.PushPage<DetailsViewModel>();

// Correct ?
viewStack.PushPage<DetailsViewModel>().Subscribe();

// Best ? - In ReactiveCommand
ReactiveCommand.CreateFromObservable(
    () => viewStack.PushPage<DetailsViewModel>());
```

### View Not Found

**Problem**: "View not found for ViewModel" exception

**Solution**: Register view-viewmodel pair:

```csharp
AppLocator.CurrentMutable.RegisterViewForNavigation<MyView, MyViewModel>(
    () => new MyView(),
    () => new MyViewModel());
```

### Parameters Not Received

**Problem**: Parameters are null in `WhenNavigatedTo`

**Solution**: Implement `INavigable` and use `IParameterViewStackService`:

```csharp
// ViewModel must implement INavigable
public class MyViewModel : ReactiveObject, INavigable

// Use IParameterViewStackService
var paramService = AppLocator.Current.GetService<IParameterViewStackService>();
```

## Additional Resources

- [Sextant GitHub Repository](https://github.com/reactiveui/Sextant)
- [MAUI Installation](~/docs/getting-started/installation/maui.md)
- [Avalonia Installation](~/docs/getting-started/installation/avalonia.md)
- [ReactiveUI Routing](~/docs/handbook/routing.md)
- [Sample Applications](~/docs/resources/samples.md)

## Related Topics

- [Routing](~/docs/handbook/routing.md)
- [Commands](~/docs/handbook/commands/index.md)
- [View Location](~/docs/handbook/view-location/index.md)
- [MAUI Plugins Popup](~/docs/handbook/maui-plugins-popup/index.md)
