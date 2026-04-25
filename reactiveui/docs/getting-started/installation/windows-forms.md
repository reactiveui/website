# Windows Forms

## Package Installation

Install the following packages for ReactiveUI with Windows Forms:

```xml
<!-- In your Windows Forms application project -->
<PackageReference Include="ReactiveUI.WinForms" Version="*" />
<PackageReference Include="ReactiveUI.SourceGenerators" Version="*" PrivateAssets="all" />
<PackageReference Include="ReactiveMarbles.ObservableEvents.SourceGenerator" Version="*" PrivateAssets="all" />

<!-- In your shared .NET Standard library -->
<PackageReference Include="ReactiveUI" Version="*" />
<PackageReference Include="ReactiveUI.SourceGenerators" Version="*" PrivateAssets="all" />

<!-- In your test project -->
<PackageReference Include="ReactiveUI.Testing" Version="*" />
```

### Recommended Project Structure

```
- MyCoolApp (netstandard/net10.0 library - shared code)
- MyCoolApp.WinForms (Windows Forms application)
- MyCoolApp.UnitTests (test project)
```

### Framework Requirements

Ensure you are targeting at least .NET 8.0 with Windows 10.0.17763.0:

```xml
<TargetFramework>net8.0-windows10.0.17763.0</TargetFramework>
<!-- Or for .NET 9/10 -->
<TargetFramework>net10.0-windows10.0.17763.0</TargetFramework>
```

## Getting Started with RxAppBuilder (Recommended)

The modern way to initialize ReactiveUI in Windows Forms uses **RxAppBuilder** for dependency injection and platform setup.

### 1. Configure Program.cs with RxAppBuilder

```csharp
using ReactiveUI;
using Splat;
using System.Reflection;

namespace MyCoolApp.WinForms;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.SystemAware);

        // Initialize ReactiveUI with RxAppBuilder
        var app = RxAppBuilder.CreateReactiveUIBuilder()
            .WithWinForms()
            .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
            .WithRegistration(locator =>
            {
                // Register your services here
                locator.RegisterLazySingleton<IScreen>(() => new MainViewModel());
                locator.RegisterLazySingleton<IDataService>(() => new DataService());
            })
            .BuildApp();

        // Create and show main form
        var mainForm = new MainForm();
        Application.Run(mainForm);
    }
}
```

### 2. Create ViewModels with SourceGenerators

Use ReactiveUI.SourceGenerators for cleaner, compile-time generated reactive properties:

```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Reactive.Linq;

namespace MyCoolApp.ViewModels;

public partial class MainViewModel : ReactiveObject
{
    [Reactive]
    private string _searchText = string.Empty;

    [Reactive]
    private string _statusMessage = string.Empty;

    [ObservableAsProperty]
    private bool _isBusy;

    [ObservableAsProperty]
    private List<SearchResult> _searchResults;

    public MainViewModel()
    {
        // Create reactive commands with automatic CanExecute
        SearchCommand = ReactiveCommand.CreateFromTask(
            async () => await PerformSearchAsync(),
            this.WhenAnyValue(x => x.SearchText, text => !string.IsNullOrWhiteSpace(text)));

        ClearCommand = ReactiveCommand.Create(
            () => SearchText = string.Empty);

        // Wire up IsBusy from command execution
        SearchCommand.IsExecuting
            .ToProperty(this, x => x.IsBusy);

        // Wire up search results
        SearchCommand
            .ToProperty(this, x => x.SearchResults);

        // React to search text changes with debouncing
        this.WhenAnyValue(x => x.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(500))
            .Where(text => !string.IsNullOrWhiteSpace(text))
            .InvokeCommand(SearchCommand);

        // Handle errors
        SearchCommand.ThrownExceptions
            .Subscribe(ex => StatusMessage = $"Error: {ex.Message}");
    }

    [ReactiveCommand]
    private async Task<List<SearchResult>> PerformSearchAsync()
    {
        StatusMessage = "Searching...";
        await Task.Delay(1000); // Simulate search
        StatusMessage = "Search complete";
        return new List<SearchResult>();
    }

    public ReactiveCommand<Unit, List<SearchResult>> SearchCommand { get; }
    public ReactiveCommand<Unit, Unit> ClearCommand { get; }
}
```

### 3. Create Forms that Implement IViewFor

**MainForm.cs (Designer):**
```csharp
namespace MyCoolApp.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.ListBox resultsListBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label statusLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.resultsListBox = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            // searchTextBox
            this.searchTextBox.Location = new System.Drawing.Point(12, 12);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(400, 23);
            this.searchTextBox.TabIndex = 0;
            
            // searchButton
            this.searchButton.Location = new System.Drawing.Point(418, 11);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            
            // clearButton
            this.clearButton.Location = new System.Drawing.Point(499, 11);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 2;
            this.clearButton.Text = "Clear";
            
            // progressBar
            this.progressBar.Location = new System.Drawing.Point(12, 41);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(562, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 3;
            this.progressBar.Visible = false;
            
            // resultsListBox
            this.resultsListBox.FormattingEnabled = true;
            this.resultsListBox.ItemHeight = 15;
            this.resultsListBox.Location = new System.Drawing.Point(12, 70);
            this.resultsListBox.Name = "resultsListBox";
            this.resultsListBox.Size = new System.Drawing.Size(562, 304);
            this.resultsListBox.TabIndex = 4;
            
            // statusLabel
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 380);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 15);
            this.statusLabel.TabIndex = 5;
            
            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 404);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.resultsListBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchTextBox);
            this.Name = "MainForm";
            this.Text = "My Cool App";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
```

**MainForm.cs (Code-Behind):**
```csharp
using ReactiveUI;
using ReactiveUI.WinForms;
using Splat;

namespace MyCoolApp.WinForms;

public partial class MainForm : ReactiveForm<MainViewModel>
{
    public MainForm()
    {
        InitializeComponent();
        
        // Resolve ViewModel from DI container or create directly
        ViewModel = AppLocator.Current.GetService<MainViewModel>() ?? new MainViewModel();

        this.WhenActivated(disposables =>
        {
            // Two-way binding for search text
            this.Bind(ViewModel,
                vm => vm.SearchText,
                v => v.searchTextBox.Text)
                .DisposeWith(disposables);

            // Command bindings
            this.BindCommand(ViewModel,
                vm => vm.SearchCommand,
                v => v.searchButton)
                .DisposeWith(disposables);

            this.BindCommand(ViewModel,
                vm => vm.ClearCommand,
                v => v.clearButton)
                .DisposeWith(disposables);

            // One-way bindings
            this.OneWayBind(ViewModel,
                vm => vm.IsBusy,
                v => v.progressBar.Visible)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                vm => vm.SearchResults,
                v => v.resultsListBox.DataSource)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                vm => vm.StatusMessage,
                v => v.statusLabel.Text)
                .DisposeWith(disposables);
        });
    }
}
```

## Alternative: Traditional Setup (Legacy)

If you prefer not to use RxAppBuilder, you can initialize ReactiveUI manually:

```csharp
static void Main()
{
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    
    // Register views manually
    Locator.CurrentMutable.Register(() => new MainForm(), typeof(IViewFor<MainViewModel>));
    
    // Register view models
    Locator.CurrentMutable.RegisterLazySingleton(() => new MainViewModel(), typeof(MainViewModel));
    
    Application.Run(new MainForm());
}
```

## Creating UserControls

For reusable components, use `ReactiveUserControl<TViewModel>`:

**SearchControl.cs (Designer):**
```csharp
namespace MyCoolApp.WinForms.Controls
{
    partial class SearchControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox queryTextBox;
        private System.Windows.Forms.Button searchButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.queryTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            
            // queryTextBox
            this.queryTextBox.Location = new System.Drawing.Point(3, 3);
            this.queryTextBox.Name = "queryTextBox";
            this.queryTextBox.Size = new System.Drawing.Size(200, 23);
            this.queryTextBox.TabIndex = 0;
            
            // searchButton
            this.searchButton.Location = new System.Drawing.Point(209, 2);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            
            // SearchControl
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.queryTextBox);
            this.Name = "SearchControl";
            this.Size = new System.Drawing.Size(287, 28);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
```

**SearchControl.cs (Code-Behind):**
```csharp
using ReactiveUI;
using ReactiveUI.WinForms;

namespace MyCoolApp.WinForms.Controls;

public partial class SearchControl : ReactiveUserControl<SearchControlViewModel>
{
    public SearchControl()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel,
                vm => vm.Query,
                v => v.queryTextBox.Text)
                .DisposeWith(disposables);
                
            this.BindCommand(ViewModel,
                vm => vm.SearchCommand,
                v => v.searchButton)
                .DisposeWith(disposables);
        });
    }
}
```

## Dependency Injection with RxAppBuilder

RxAppBuilder integrates with Splat for dependency injection:

```csharp
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithWinForms()
    .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
    .WithRegistration(locator =>
    {
        // Register services as singletons
        locator.RegisterLazySingleton<IApiService>(() => new ApiService());
        locator.RegisterLazySingleton<IDataRepository>(() => new DataRepository());
        
        // Register view models (transient)
        locator.Register<MainViewModel>(() => new MainViewModel());
        
        // Register view models as singletons
        locator.RegisterLazySingleton<SettingsViewModel>(() => new SettingsViewModel());
    })
    .BuildApp();
```

Then resolve services in your view models:

```csharp
public MainViewModel()
{
    var apiService = AppLocator.Current.GetService<IApiService>();
    var repository = AppLocator.Current.GetService<IDataRepository>();
}
```

## Key Points

- **Use ReactiveForm<TViewModel>** or **ReactiveUserControl<TViewModel>** base classes
- **Use ReactiveUI.SourceGenerators** for cleaner property and command declarations
- **Use RxAppBuilder** for modern dependency injection and platform setup
- **Always call DisposeWith(disposables)** inside WhenActivated to prevent memory leaks
- **Use ReactiveMarbles.ObservableEvents.SourceGenerator** for converting Windows Forms events to observables

## Common Patterns

### Value Converters in Bindings

```csharp
this.OneWayBind(ViewModel,
    vm => vm.IsEnabled,
    v => v.myButton.Visible,
    isEnabled => isEnabled)
    .DisposeWith(disposables);
```

### Reactive Validation

```csharp
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;

public partial class LoginViewModel : ReactiveValidationObject
{
    [Reactive]
    private string _username = string.Empty;

    [Reactive]
    private string _password = string.Empty;

    public LoginViewModel()
    {
        this.ValidationRule(
            vm => vm.Username,
            username => !string.IsNullOrWhiteSpace(username),
            "Username is required");

        this.ValidationRule(
            vm => vm.Password,
            password => password?.Length >= 6,
            "Password must be at least 6 characters");

        LoginCommand = ReactiveCommand.CreateFromTask(
            async () => await LoginAsync(),
            this.IsValid());
    }
    
    [ReactiveCommand]
    private async Task LoginAsync() { /* ... */ }
}
```

### Loading Data on Activation

```csharp
public MainForm()
{
    InitializeComponent();
    ViewModel = new MainViewModel();
    
    this.WhenActivated(disposables =>
    {
        // Load data when the form is shown
        ViewModel.LoadDataCommand.Execute().Subscribe().DisposeWith(disposables);
        
        // Set up bindings...
    });
}
```

### Handling Form Events Reactively

```csharp
this.WhenActivated(disposables =>
{
    // Convert FormClosing event to observable
    Observable.FromEventPattern<FormClosingEventHandler, FormClosingEventArgs>(
        h => this.FormClosing += h,
        h => this.FormClosing -= h)
        .Subscribe(e =>
        {
            // Handle form closing
            if (ViewModel.HasUnsavedChanges)
            {
                var result = MessageBox.Show("Save changes?", "Confirm", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Cancel)
                {
                    e.EventArgs.Cancel = true;
                }
            }
        })
        .DisposeWith(disposables);
});
```

## Additional Resources

- [Windows Forms Documentation](https://learn.microsoft.com/dotnet/desktop/winforms/)
- [ReactiveUI Handbook](~/docs/handbook/index.md)
- [RxAppBuilder Guide](~/docs/handbook/rxappbuilder.md)
- [ReactiveUI.SourceGenerators](https://github.com/reactiveui/ReactiveUI.SourceGenerators)
- [Sample Windows Forms Apps](~/docs/resources/samples.md)
