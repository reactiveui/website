# AndroidX (Xamarin.Android)

> **Note** AndroidX support is for Xamarin.Android applications. For new Android development, consider using [.NET MAUI](~/docs/getting-started/installation/maui.md) which provides a modern cross-platform approach with full ReactiveUI support.

## Package Installation

Install the following packages for ReactiveUI with AndroidX:

```xml
<!-- In your Xamarin.Android application project -->
<PackageReference Include="ReactiveUI.AndroidX" Version="*" />
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
- MyCoolApp.Core (netstandard2.0/netstandard2.1 library - shared code)
- MyCoolApp.Android (Xamarin.Android application)
- MyCoolApp.UnitTests (test project)
```

### Framework Requirements

Ensure your Android project targets at least Android 5.0 (API Level 21):

```xml
<TargetFramework>net8.0-android</TargetFramework>
<SupportedOSPlatformVersion>21</SupportedOSPlatformVersion>
```

## Getting Started with RxAppBuilder (Recommended)

The modern way to initialize ReactiveUI in Xamarin.Android uses **RxAppBuilder** for dependency injection and platform setup.

### 1. Configure Application Class with RxAppBuilder

Create or update your `Application` class:

```csharp
using Android.App;
using Android.Runtime;
using ReactiveUI;
using Splat;
using System;
using System.Reflection;

namespace MyCoolApp.Android;

[Application]
public class MainApplication : Application
{
    public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
        : base(javaReference, transfer)
    {
    }

    public override void OnCreate()
    {
        base.OnCreate();

        // Initialize ReactiveUI with RxAppBuilder
        var app = RxAppBuilder.CreateReactiveUIBuilder()
            .WithAndroidX()
            .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
            .WithRegistration(locator =>
            {
                // Register your services here
                locator.RegisterLazySingleton<INavigationService>(() => new NavigationService());
                locator.RegisterLazySingleton<IDataService>(() => new DataService());
            })
            .BuildApp();

        // Optional: Store schedulers for later use
        // var mainScheduler = app.MainThreadScheduler;
        // var taskScheduler = app.TaskpoolScheduler;
    }
}
```

Don't forget to register your Application class in `AndroidManifest.xml`:

```xml
<application android:name=".MainApplication">
    <!-- Your application configuration -->
</application>
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

### 3. Create Activities that Implement IViewFor

**MainActivity.cs:**
```csharp
using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using ReactiveUI;
using Splat;
using System.Reactive.Disposables;

namespace MyCoolApp.Android;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : ReactiveAppCompatActivity<MainViewModel>
{
    private EditText? _searchEditText;
    private Button? _searchButton;
    private Button? _clearButton;
    private ProgressBar? _progressBar;
    private ListView? _resultsListView;
    private TextView? _statusTextView;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.activity_main);

        // Initialize views
        _searchEditText = FindViewById<EditText>(Resource.Id.searchEditText);
        _searchButton = FindViewById<Button>(Resource.Id.searchButton);
        _clearButton = FindViewById<Button>(Resource.Id.clearButton);
        _progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
        _resultsListView = FindViewById<ListView>(Resource.Id.resultsListView);
        _statusTextView = FindViewById<TextView>(Resource.Id.statusTextView);

        // Resolve ViewModel from DI container or create directly
        ViewModel = AppLocator.Current.GetService<MainViewModel>() ?? new MainViewModel();

        this.WhenActivated(disposables =>
        {
            // Two-way binding for search text
            this.Bind(ViewModel,
                vm => vm.SearchText,
                v => v._searchEditText!.Text)
                .DisposeWith(disposables);

            // Command bindings
            this.BindCommand(ViewModel,
                vm => vm.SearchCommand,
                v => v._searchButton!)
                .DisposeWith(disposables);

            this.BindCommand(ViewModel,
                vm => vm.ClearCommand,
                v => v._clearButton!)
                .DisposeWith(disposables);

            // One-way bindings
            this.OneWayBind(ViewModel,
                vm => vm.IsBusy,
                v => v._progressBar!.Visibility,
                isBusy => isBusy ? Android.Views.ViewStates.Visible : Android.Views.ViewStates.Gone)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                vm => vm.StatusMessage,
                v => v._statusTextView!.Text)
                .DisposeWith(disposables);

            // Bind search results to ListView
            this.WhenAnyValue(x => x.ViewModel!.SearchResults)
                .Subscribe(results =>
                {
                    if (results != null)
                    {
                        _resultsListView!.Adapter = new ArrayAdapter<SearchResult>(
                            this,
                            Android.Resource.Layout.SimpleListItem1,
                            results);
                    }
                })
                .DisposeWith(disposables);
        });
    }
}
```

**activity_main.xml** (in Resources/layout/):
```xml
<?xml version="1.0" encoding="utf-8"?>
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
    android:orientation="vertical"
    android:layout_width="match_parent"
    android:layout_height="match_parent"
    android:padding="16dp">

    <LinearLayout
        android:orientation="horizontal"
        android:layout_width="match_parent"
        android:layout_height="wrap_content">

        <EditText
            android:id="@+id/searchEditText"
            android:layout_width="0dp"
            android:layout_height="wrap_content"
            android:layout_weight="1"
            android:hint="Search..."
            android:inputType="text" />

        <Button
            android:id="@+id/searchButton"
            android:layout_width="wrap_content"
            android:layout_height="wrap_content"
            android:text="Search" />

        <Button
            android:id="@+id/clearButton"
            android:layout_width="wrap_content"
            android:layout_height="wrap_content"
            android:text="Clear" />
    </LinearLayout>

    <ProgressBar
        android:id="@+id/progressBar"
        style="?android:attr/progressBarStyleHorizontal"
        android:layout_width="match_parent"
        android:layout_height="wrap_content"
        android:indeterminate="true"
        android:visibility="gone" />

    <ListView
        android:id="@+id/resultsListView"
        android:layout_width="match_parent"
        android:layout_height="0dp"
        android:layout_weight="1"
        android:layout_marginTop="16dp" />

    <TextView
        android:id="@+id/statusTextView"
        android:layout_width="match_parent"
        android:layout_height="wrap_content"
        android:layout_marginTop="8dp"
        android:textAppearance="?android:attr/textAppearanceSmall" />
</LinearLayout>
```

## Alternative: Traditional Setup (Legacy)

If you prefer not to use RxAppBuilder, you can initialize ReactiveUI manually:

```csharp
public override void OnCreate()
{
    base.OnCreate();
    
    // Register views manually
    AppLocator.CurrentMutable.Register(() => new MainActivity(), typeof(IViewFor<MainViewModel>));
    
    // Register view models
    AppLocator.CurrentMutable.RegisterLazySingleton(() => new MainViewModel(), typeof(MainViewModel));
}
```

## Creating Fragments

For fragments, use `ReactiveFragment<TViewModel>`:

**SearchFragment.cs:**
```csharp
using Android.OS;
using Android.Views;
using Android.Widget;
using ReactiveUI;
using System.Reactive.Disposables;

namespace MyCoolApp.Android.Fragments;

public class SearchFragment : ReactiveFragment<SearchFragmentViewModel>
{
    private EditText? _queryEditText;
    private Button? _searchButton;

    public override View? OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
    {
        var view = inflater.Inflate(Resource.Layout.fragment_search, container, false);

        _queryEditText = view.FindViewById<EditText>(Resource.Id.queryEditText);
        _searchButton = view.FindViewById<Button>(Resource.Id.searchButton);

        ViewModel ??= new SearchFragmentViewModel();

        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel,
                vm => vm.Query,
                v => v._queryEditText!.Text)
                .DisposeWith(disposables);

            this.BindCommand(ViewModel,
                vm => vm.SearchCommand,
                v => v._searchButton!)
                .DisposeWith(disposables);
        });

        return view;
    }
}
```

## RecyclerView Support

For lists with RecyclerView, use `ReactiveRecyclerViewAdapter`:

```csharp
using Android.Views;
using AndroidX.RecyclerView.Widget;
using ReactiveUI;
using System.Reactive.Disposables;

namespace MyCoolApp.Android.Adapters;

public class SearchResultViewHolder : ReactiveRecyclerViewViewHolder<SearchResult>
{
    private TextView? _titleTextView;
    private TextView? _descriptionTextView;

    public SearchResultViewHolder(View itemView) : base(itemView)
    {
        _titleTextView = itemView.FindViewById<TextView>(Resource.Id.titleTextView);
        _descriptionTextView = itemView.FindViewById<TextView>(Resource.Id.descriptionTextView);

        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel,
                vm => vm.Title,
                v => v._titleTextView!.Text)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel,
                vm => vm.Description,
                v => v._descriptionTextView!.Text)
                .DisposeWith(disposables);
        });
    }
}

public class SearchResultsAdapter : ReactiveRecyclerViewAdapter<SearchResult, SearchResultViewHolder>
{
    public SearchResultsAdapter(IObservable<IChangeSet<SearchResult>> changeSet)
        : base(changeSet)
    {
    }

    public override SearchResultViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
    {
        var view = LayoutInflater.From(parent.Context)!
            .Inflate(Resource.Layout.item_search_result, parent, false)!;
        return new SearchResultViewHolder(view);
    }
}
```

Usage in Activity:

```csharp
this.WhenActivated(disposables =>
{
    var adapter = new SearchResultsAdapter(
        ViewModel!.SearchResults
            .ToObservableChangeSet());
    
    _recyclerView!.SetAdapter(adapter);
    _recyclerView.SetLayoutManager(new LinearLayoutManager(this));
});
```

## Dependency Injection with RxAppBuilder

RxAppBuilder integrates with Splat for dependency injection:

```csharp
var app = RxAppBuilder.CreateReactiveUIBuilder()
    .WithAndroidX()
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

- **Use ReactiveAppCompatActivity<TViewModel>** or **ReactiveFragment<TViewModel>** base classes
- **Use ReactiveUI.SourceGenerators** for cleaner property and command declarations
- **Use RxAppBuilder** for modern dependency injection and platform setup
- **Always call DisposeWith(disposables)** inside WhenActivated to prevent memory leaks
- **Use ReactiveRecyclerViewAdapter** for efficient list handling with RecyclerView
- **Use ReactiveMarbles.ObservableEvents.SourceGenerator** for converting Android events to observables

## Common Patterns

### Handling Android Lifecycle

```csharp
public class MainActivity : ReactiveAppCompatActivity<MainViewModel>
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        // ViewModel persists across configuration changes
        ViewModel ??= ViewModelStore.GetViewModel<MainViewModel>();
    }

    protected override void OnPause()
    {
        base.OnPause();
        // WhenActivated automatically disposes subscriptions
    }

    protected override void OnResume()
    {
        base.OnResume();
        // WhenActivated automatically reactivates
    }
}
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

### Handling Permissions Reactively

```csharp
using AndroidX.Core.App;
using AndroidX.Core.Content;

public partial class CameraViewModel : ReactiveObject
{
    [ReactiveCommand]
    private async Task RequestCameraPermission()
    {
        var activity = AppLocator.Current.GetService<Activity>();
        
        if (ContextCompat.CheckSelfPermission(activity!, Manifest.Permission.Camera)
            == Permission.Granted)
        {
            // Permission already granted
            await OpenCamera();
        }
        else
        {
            // Request permission
            ActivityCompat.RequestPermissions(
                activity!,
                new[] { Manifest.Permission.Camera },
                RequestCodeCamera);
        }
    }
}
```

### Navigation Between Activities

```csharp
public partial class MainViewModel : ReactiveObject
{
    [ReactiveCommand]
    private void NavigateToDetails(SearchResult item)
    {
        var activity = AppLocator.Current.GetService<Activity>();
        var intent = new Intent(activity, typeof(DetailsActivity));
        intent.PutExtra("ItemId", item.Id);
        activity!.StartActivity(intent);
    }
}
```

## Performance Tips

1. **Use RecyclerView over ListView** for better performance with large lists
2. **Use ViewHolder pattern** with ReactiveRecyclerViewViewHolder
3. **Dispose subscriptions properly** using WhenActivated and DisposeWith
4. **Use Throttle/Debounce** for search and user input to reduce unnecessary operations
5. **Load data on background threads** and observe on MainThreadScheduler

## Migration from Xamarin to MAUI

If you're considering migrating from Xamarin.Android to .NET MAUI:

1. Review the [MAUI Installation Guide](~/docs/getting-started/installation/maui.md)
2. MAUI provides better performance and modern API support
3. Share more code across platforms
4. Access to the latest .NET features

## Additional Resources

- [Xamarin.Android Documentation](https://learn.microsoft.com/xamarin/android/)
- [AndroidX Documentation](https://developer.android.com/jetpack/androidx)
- [ReactiveUI Handbook](~/docs/handbook/index.md)
- [RxAppBuilder Guide](~/docs/handbook/rxappbuilder.md)
- [ReactiveUI.SourceGenerators](https://github.com/reactiveui/ReactiveUI.SourceGenerators)
- [Sample Android Apps](~/docs/resources/samples.md)
- [Migrate to .NET MAUI](https://learn.microsoft.com/dotnet/maui/migration/)
