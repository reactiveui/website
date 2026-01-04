# ReactiveUI.Maui.Plugins.Popup

**ReactiveUI.Maui.Plugins.Popup** provides ReactiveUI bindings and helpers for the [Mopups](https://github.com/LuckyDucko/Mopups) library in .NET MAUI. It enables you to build composable, reactive popup views using the Model-View-ViewModel (MVVM) pattern with full ReactiveUI integration.

## Overview

This package wraps Mopups with ReactiveUI-specific features:
- **ReactivePopupPage<TViewModel>**: Strongly-typed popup pages
- **Reactive Navigation Extensions**: Observable-based popup navigation
- **Event Observables**: Hot observables for popup lifecycle events
- **WhenActivated Support**: Proper subscription management
- **IViewFor Integration**: Full ReactiveUI data binding support

## Installation

Install the ReactiveUI-specific package via NuGet:

```bash
dotnet add package ReactiveUI.Maui.Plugins.Popup
```

Or via Package Manager:

```xml
<PackageReference Include="ReactiveUI.Maui.Plugins.Popup" Version="*" />
```

> **Note**: This package has a dependency on `Mopups`, so you don't need to install it separately.

## Setup

Initialize the plugin in your `MauiProgram.cs`:

```csharp
using Microsoft.Maui.Hosting;
using ReactiveUI.Maui.Plugins.Popup;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            // Initialize ReactiveUI Popups
            .ConfigureReactiveUIPopup();
        
        return builder.Build();
    }
}
```

## Quick Start Example

### 1. Create a ViewModel

```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Reactive;

public partial class ConfirmationViewModel : ReactiveObject
{
    [Reactive]
    private string _title = "Confirm Action";
    
    [Reactive]
    private string _message = "Are you sure?";
    
    private readonly TaskCompletionSource<bool> _resultSource;
    
    public ConfirmationViewModel()
    {
        _resultSource = new TaskCompletionSource<bool>();
    }
    
    [ReactiveCommand]
    private void Confirm()
    {
        _resultSource.SetResult(true);
    }
    
    [ReactiveCommand]
    private void Cancel()
    {
        _resultSource.SetResult(false);
    }
    
    public Task<bool> GetResultAsync() => _resultSource.Task;
}
```

### 2. Create a Popup View

```xml
<?xml version="1.0" encoding="utf-8" ?>
<rxui:ReactivePopupPage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:rxui="clr-namespace:ReactiveUI.Maui.Plugins.Popup;assembly=ReactiveUI.Maui.Plugins.Popup"
    xmlns:vm="clr-namespace:MyApp.ViewModels"
    x:Class="MyApp.Views.ConfirmationPopup"
    x:TypeArguments="vm:ConfirmationViewModel"
    x:DataType="vm:ConfirmationViewModel"
    BackgroundColor="#80000000">
    
    <Frame 
        CornerRadius="15"
        BackgroundColor="White"
        Padding="30"
        Margin="40"
        HorizontalOptions="Center"
        VerticalOptions="Center">
        
        <VerticalStackLayout Spacing="20">
            <Label 
                Text="{Binding Title}"
                FontSize="20"
                FontAttributes="Bold"
                HorizontalTextAlignment="Center"/>
            
            <Label 
                Text="{Binding Message}"
                HorizontalTextAlignment="Center"/>
            
            <Grid ColumnDefinitions="*,*" ColumnSpacing="10">
                <Button 
                    x:Name="CancelButton"
                    Text="Cancel"
                    Grid.Column="0"
                    BackgroundColor="LightGray"/>
                
                <Button 
                    x:Name="ConfirmButton"
                    Text="Confirm"
                    Grid.Column="1"
                    BackgroundColor="{StaticResource Primary}"/>
            </Grid>
        </VerticalStackLayout>
    </Frame>
</rxui:ReactivePopupPage>
```

```csharp
using ReactiveUI;
using ReactiveUI.Maui.Plugins.Popup;

namespace MyApp.Views;

public partial class ConfirmationPopup : ReactivePopupPage<ConfirmationViewModel>
{
    public ConfirmationPopup()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            // Bind commands using ReactiveUI
            this.BindCommand(ViewModel,
                vm => vm.ConfirmCommand,
                v => v.ConfirmButton)
                .DisposeWith(disposables);
            
            this.BindCommand(ViewModel,
                vm => vm.CancelCommand,
                v => v.CancelButton)
                .DisposeWith(disposables);
        });
    }
}
```

### 3. Show the Popup

```csharp
// Using Navigation property (returns Cold Observable)
var popup = new ConfirmationPopup 
{ 
    ViewModel = new ConfirmationViewModel() 
};

// Subscribe to execute the navigation
this.Navigation.PushPopup(popup).Subscribe();

// Or await the result
var result = await popup.ViewModel.GetResultAsync();
if (result)
{
    // User confirmed
}
```

## Understanding Hot and Cold Observables

ReactiveUI.Maui.Plugins.Popup provides both Hot and Cold observables:

### Cold Observables (Navigation Methods)

Navigation methods return **Cold Observables** - the operation doesn't execute until subscribed:

```csharp
// The popup is NOT pushed until Subscribe() is called
var pushOperation = this.Navigation.PushPopup(myPopup);

// Subscribe to execute
pushOperation.Subscribe();

// Can also await
await pushOperation;
```

**Key Characteristics**:
- Operation doesn't begin until subscription
- Each subscription triggers a new execution
- Completes after single value emission
- Ideal for command-driven navigation

### Hot Observables (Event Streams)

Event observables are **Hot Observables** - they produce values regardless of subscription:

```csharp
using Mopups.Services;

// Monitor popup lifecycle events
MopupService.Instance.PushedObservable()
    .Subscribe(args => 
    {
        Debug.WriteLine($"Popup pushed: {args}");
    });

// Multiple subscribers receive same events
MopupService.Instance.PoppedObservable()
    .Subscribe(args => 
    {
        Debug.WriteLine($"Popup dismissed: {args}");
    });
```

**Key Characteristics**:
- Produce values regardless of subscribers
- Events fire independently of subscription
- Multiple subscribers receive same notifications
- Ideal for monitoring and analytics

## API Reference

### Navigation Extensions (Cold Observables)

All methods available on both `INavigation` and `IPopupNavigation`:

#### PushPopup

Adds a popup to the top of the stack:

```csharp
// Basic usage
this.Navigation.PushPopup(popup).Subscribe();

// With animation control
this.Navigation.PushPopup(popup, animate: false).Subscribe();

// Await the operation
await this.Navigation.PushPopup(popup);
```

#### PopPopup

Removes the topmost popup (LIFO):

```csharp
this.Navigation.PopPopup(animate: true).Subscribe();
```

#### PopAllPopup

Removes all popups at once:

```csharp
this.Navigation.PopAllPopup(animate: true).Subscribe();
```

#### RemovePopupPage

Removes a specific popup from anywhere in the stack:

```csharp
this.Navigation.RemovePopupPage(specificPopup, animate: true).Subscribe();
```

### Event Observables (Hot Observables)

Available via `MopupService.Instance`:

#### PushingObservable

Fires **before** a popup is added:

```csharp
MopupService.Instance.PushingObservable()
    .Subscribe(args => 
    {
        Console.WriteLine($"About to push: {args}");
        // Validate or log before push
    });
```

#### PushedObservable

Fires **after** a popup is added:

```csharp
MopupService.Instance.PushedObservable()
    .Subscribe(args => 
    {
        Console.WriteLine($"Pushed: {args}");
        // Initialize or track
    });
```

#### PoppingObservable

Fires **before** a popup is removed:

```csharp
MopupService.Instance.PoppingObservable()
    .Subscribe(args => 
    {
        Console.WriteLine($"About to pop: {args}");
        // Cleanup or save state
    });
```

#### PoppedObservable

Fires **after** a popup is removed:

```csharp
MopupService.Instance.PoppedObservable()
    .Subscribe(args => 
    {
        Console.WriteLine($"Popped: {args}");
        // Final cleanup
    });
```

## ReactiveUI Patterns

### Reactive Dialog Service

```csharp
public interface IDialogService
{
    Task<bool> ShowConfirmationAsync(string title, string message);
    Task ShowAlertAsync(string title, string message);
    Task<string?> ShowInputAsync(string title, string placeholder);
}

public class DialogService : IDialogService
{
    public async Task<bool> ShowConfirmationAsync(string title, string message)
    {
        var vm = new ConfirmationViewModel { Title = title, Message = message };
        var popup = new ConfirmationPopup { ViewModel = vm };
        
        await this.Navigation.PushPopup(popup);
        
        return await vm.GetResultAsync();
    }
    
    public async Task ShowAlertAsync(string title, string message)
    {
        var vm = new AlertViewModel { Title = title, Message = message };
        var popup = new AlertPopup { ViewModel = vm };
        
        await this.Navigation.PushPopup(popup);
        await vm.GetResultAsync();
    }
    
    public async Task<string?> ShowInputAsync(string title, string placeholder)
    {
        var vm = new InputViewModel { Title = title, Placeholder = placeholder };
        var popup = new InputPopup { ViewModel = vm };
        
        await this.Navigation.PushPopup(popup);
        
        return await vm.GetResultAsync();
    }
}
```

### Loading Indicator with ReactiveCommand

```csharp
public partial class MainViewModel : ReactiveObject
{
    [Reactive]
    private bool _isLoading;
    
    public MainViewModel(INavigation navigation)
    {
        LoadDataCommand = ReactiveCommand.CreateFromTask(LoadDataAsync);
        
        // Automatically show/hide loading popup
        LoadDataCommand.IsExecuting
            .Subscribe(async isExecuting =>
            {
                if (isExecuting)
                {
                    await navigation.PushPopup(new LoadingPopup());
                }
                else
                {
                    await navigation.PopPopup();
                }
            });
    }
    
    [ReactiveCommand]
    private async Task LoadData()
    {
        await Task.Delay(2000); // Simulate work
    }
}
```

### Popup with Result Pattern

```csharp
public partial class SelectItemViewModel : ReactiveObject
{
    [Reactive]
    private ObservableCollection<Item> _items;
    
    private readonly TaskCompletionSource<Item?> _resultSource;
    
    public SelectItemViewModel()
    {
        _resultSource = new TaskCompletionSource<Item?>();
    }
    
    [ReactiveCommand]
    private void SelectItem(Item item)
    {
        _resultSource.SetResult(item);
    }
    
    [ReactiveCommand]
    private void Cancel()
    {
        _resultSource.SetResult(null);
    }
    
    public Task<Item?> GetResultAsync() => _resultSource.Task;
}

// Usage
var vm = new SelectItemViewModel();
var popup = new SelectItemPopup { ViewModel = vm };

await this.Navigation.PushPopup(popup);
var selectedItem = await vm.GetResultAsync();

if (selectedItem != null)
{
    ProcessSelection(selectedItem);
}
```

## Advanced Patterns

### Popup Stack Monitoring

```csharp
public class PopupMonitorService
{
    private readonly CompositeDisposable _disposables = new();
    
    public void Start()
    {
        var popupService = MopupService.Instance;
        
        // Track all popup lifecycle
        Observable.Merge(
            popupService.PushingObservable().Select(e => $"Pushing: {e}"),
            popupService.PushedObservable().Select(e => $"Pushed: {e}"),
            popupService.PoppingObservable().Select(e => $"Popping: {e}"),
            popupService.PoppedObservable().Select(e => $"Popped: {e}")
        )
        .Subscribe(msg => Debug.WriteLine(msg))
        .DisposeWith(_disposables);
        
        // Track popup count
        Observable.Merge(
            popupService.PushedObservable().Select(_ => 1),
            popupService.PoppedObservable().Select(_ => -1)
        )
        .Scan(0, (count, change) => count + change)
        .Subscribe(count => Debug.WriteLine($"Active popups: {count}"))
        .DisposeWith(_disposables);
    }
    
    public void Stop()
    {
        _disposables.Clear();
    }
}
```

### Conditional Popup Display

```csharp
public partial class MainViewModel : ReactiveObject
{
    [Reactive]
    private bool _hasUnsavedChanges;
    
    public MainViewModel(INavigation navigation)
    {
        NavigateAwayCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            if (HasUnsavedChanges)
            {
                var vm = new ConfirmationViewModel 
                { 
                    Title = "Unsaved Changes",
                    Message = "You have unsaved changes. Continue?"
                };
                var popup = new ConfirmationPopup { ViewModel = vm };
                
                await navigation.PushPopup(popup);
                var confirmed = await vm.GetResultAsync();
                
                if (!confirmed)
                {
                    return; // Cancel navigation
                }
            }
            
            // Proceed with navigation
            await NavigateAway();
        });
    }
    
    [ReactiveCommand]
    private Task NavigateAway() => Task.CompletedTask;
}
```

## Controls

### ReactivePopupPage

Base popup page implementing `IViewFor` for use without specific ViewModel:

```csharp
public partial class GenericPopup : ReactivePopupPage
{
    public GenericPopup()
    {
        InitializeComponent();
    }
}
```

### ReactivePopupPage<TViewModel>

Strongly-typed popup page with compile-time safety:

```csharp
public partial class MyPopup : ReactivePopupPage<MyViewModel>
{
    public MyPopup()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            // Full IntelliSense support for ViewModel
            this.Bind(ViewModel, vm => vm.Property, v => v.Control.Text)
                .DisposeWith(disposables);
        });
    }
}
```

## Best Practices

### 1. Always Use WhenActivated

```csharp
// Good ?
this.WhenActivated(disposables =>
{
    this.BindCommand(ViewModel, vm => vm.Command, v => v.Button)
        .DisposeWith(disposables);
});

// Bad ?
this.BindCommand(ViewModel, vm => vm.Command, v => v.Button);
```

### 2. Subscribe to Navigation Operations

```csharp
// Good ?
this.Navigation.PushPopup(popup).Subscribe();

// Bad ? - Won't execute
this.Navigation.PushPopup(popup); // Cold observable, never subscribed!
```

### 3. Handle Popup Results

```csharp
// Good ?
var result = await viewModel.GetResultAsync();
if (result) { /* handle */ }

// Okay - fire and forget
popup.ViewModel.ConfirmCommand
    .Subscribe(_ => ProcessConfirmation())
    .DisposeWith(disposables);
```

### 4. Dispose Properly

```csharp
// Good ?
this.WhenActivated(disposables =>
{
    Observable.Timer(TimeSpan.FromSeconds(5))
        .Subscribe(_ => ClosePopup())
        .DisposeWith(disposables);
});
```

## Troubleshooting

### Popup Not Showing

**Problem**: Popup doesn't appear when PushPopup is called

**Solution**: Ensure you subscribe to the observable:

```csharp
// Wrong ?
this.Navigation.PushPopup(popup);

// Correct ?
this.Navigation.PushPopup(popup).Subscribe();
```

### ConfigureReactiveUIPopup Not Found

**Problem**: Extension method not available

**Solution**: Ensure you have the correct using statement:

```csharp
using ReactiveUI.Maui.Plugins.Popup;
```

### ViewModel Not Binding

**Problem**: Data binding doesn't work

**Solution**: Ensure you're using ReactivePopupPage<TViewModel>:

```csharp
// Wrong ?
public partial class MyPopup : PopupPage

// Correct ?
public partial class MyPopup : ReactivePopupPage<MyViewModel>
```

## Platform-Specific Notes

### iOS

- Respects safe areas automatically
- Supports both light and dark mode
- Works with UIKit-based MAUI apps

### Android

- Material Design 3 compatible
- Handles back button presses
- Supports edge-to-edge display

### Windows

- WinUI 3 support
- Keyboard navigation
- Adaptive layouts

## Additional Resources

- [ReactiveUI.Maui.Plugins.Popup GitHub](https://github.com/reactiveui/Maui.Plugins.Popup)
- [Mopups Library](https://github.com/LuckyDucko/Mopups)
- [ReactiveUI Documentation](~/docs/index.md)
- [MAUI Installation Guide](~/docs/getting-started/installation/maui.md)
- [ReactiveUI MAUI Samples](~/docs/resources/samples.md)

## Related Topics

- [MAUI Installation](~/docs/getting-started/installation/maui.md)
- [Data Binding](~/docs/handbook/data-binding/index.md)
- [Commands](~/docs/handbook/commands/index.md)
- [View Location](~/docs/handbook/view-location/index.md)
- [WhenActivated](~/docs/handbook/when-activated.md)
