# Migration Guide: Xamarin to .NET MAUI

This comprehensive guide helps you migrate your ReactiveUI Xamarin.Forms application to .NET MAUI.

## Why Migrate to MAUI?

### Benefits of .NET MAUI

? **Modern .NET**: Latest .NET features and performance  
? **Single Project**: One project for all platforms  
? **Hot Reload**: Faster development iteration  
? **Better Performance**: Native AOT and trimming support  
? **Active Development**: Ongoing Microsoft support  
? **Modern Tooling**: Latest Visual Studio features  
? **Unified API**: Consistent across platforms  

### Xamarin Limitations

? **End of Support**: Xamarin support ended May 2024  
? **Old .NET**: Limited to .NET Standard 2.0/2.1  
? **Multiple Projects**: Separate project per platform  
? **Slower Updates**: Bug fixes and features deprecated  
? **Limited Tooling**: Older Visual Studio integration  

## Prerequisites

- Visual Studio 2022 17.8+ or VS Code with .NET MAUI extension
- .NET 8.0 SDK or later
- MAUI workload installed: `dotnet workload install maui`
- ReactiveUI 19.5.31+

## Migration Overview

### Project Structure Changes

#### Xamarin.Forms Structure
```
??? MyApp (Shared .NET Standard)
??? MyApp.Android
??? MyApp.iOS
??? MyApp.UWP (optional)
??? MyApp.Tests
```

#### .NET MAUI Structure
```
??? MyApp (Single Project)
?   ??? Platforms/
?   ?   ??? Android/
?   ?   ??? iOS/
?   ?   ??? MacCatalyst/
?   ?   ??? Windows/
?   ??? Resources/
?   ??? MauiProgram.cs
??? MyApp.Tests
```

## Step-by-Step Migration

### Step 1: Create New MAUI Project

```bash
dotnet new maui -n MyApp
```

Or use Visual Studio:
1. File ? New ? Project
2. Select ".NET MAUI App"
3. Name your project

### Step 2: Update Package References

#### Remove Xamarin Packages

```xml
<!-- Remove these -->
<PackageReference Include="Xamarin.Forms" />
<PackageReference Include="Xamarin.Essentials" />
<PackageReference Include="ReactiveUI.XamForms" />
```

#### Add MAUI Packages

```xml
<!-- Add these -->
<PackageReference Include="Microsoft.Maui.Controls" Version="*" />
<PackageReference Include="ReactiveUI.Maui" Version="*" />
<PackageReference Include="ReactiveUI.SourceGenerators" Version="*" PrivateAssets="all" />
<PackageReference Include="ReactiveMarbles.ObservableEvents.SourceGenerator" Version="*" PrivateAssets="all" />
```

### Step 3: Migrate Application Setup

#### Xamarin.Forms App.xaml.cs

```csharp
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        // Xamarin initialization
        Locator.CurrentMutable.RegisterLazySingleton<IScreen>(() => new MainViewModel());
        Locator.CurrentMutable.Register<IViewFor<MainViewModel>>(() => new MainPage());
        
        MainPage = new NavigationPage(new MainPage());
    }
}
```

#### .NET MAUI MauiProgram.cs

```csharp
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
            });
        
        // Initialize ReactiveUI with RxAppBuilder
        var app = RxAppBuilder.CreateReactiveUIBuilder()
            .WithMaui()
            .WithViewsFromAssembly(typeof(App).Assembly)
            .WithRegistration(locator =>
            {
                locator.RegisterLazySingleton<IScreen>(() => new MainViewModel());
                locator.RegisterLazySingleton<IDataService>(() => new DataService());
            })
            .BuildApp();
        
        return builder.Build();
    }
}
```

#### MAUI App.xaml.cs

```csharp
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        MainPage = new AppShell(); // or NavigationPage
    }
}
```

### Step 4: Migrate Pages/Views

#### Xamarin.Forms ContentPage

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MyApp.MainPage">
    <StackLayout>
        <Label Text="Welcome to Xamarin.Forms!" />
    </StackLayout>
</ContentPage>
```

```csharp
using Xamarin.Forms;
using ReactiveUI;
using ReactiveUI.XamForms;

public partial class MainPage : ReactiveContentPage<MainViewModel>
{
    public MainPage()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables =>
        {
            // Bindings
        });
    }
}
```

#### .NET MAUI ContentPage

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MyApp.MainPage">
    <VerticalStackLayout>
        <Label Text="Welcome to .NET MAUI!" />
    </VerticalStackLayout>
</ContentPage>
```

```csharp
using ReactiveUI;
using ReactiveUI.Maui;

namespace MyApp;

public partial class MainPage : ReactiveContentPage<MainViewModel>
{
    public MainPage()
    {
        InitializeComponent();
        
        ViewModel = new MainViewModel();
        
        this.WhenActivated(disposables =>
        {
            // Bindings
        });
    }
}
```

### Step 5: Update ViewModels

ViewModels typically require minimal changes, but you should adopt modern patterns:

#### Xamarin Era ViewModel

```csharp
public class MainViewModel : ReactiveObject
{
    private string _text;
    public string Text
    {
        get => _text;
        set => this.RaiseAndSetIfChanged(ref _text, value);
    }
    
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    
    public MainViewModel()
    {
        SaveCommand = ReactiveCommand.CreateFromTask(SaveAsync);
    }
    
    private async Task SaveAsync() { }
}
```

#### Modern MAUI ViewModel

```csharp
using ReactiveUI;
using ReactiveUI.SourceGenerators;

public partial class MainViewModel : ReactiveObject
{
    [Reactive]
    private string _text = string.Empty;
    
    [ReactiveCommand]
    private async Task Save()
    {
        // Save logic
    }
}
```

### Step 6: Migrate Navigation

#### Xamarin Navigation

```csharp
// Push
await Navigation.PushAsync(new DetailsPage());

// Pop
await Navigation.PopAsync();

// Modal
await Navigation.PushModalAsync(new ModalPage());
```

#### MAUI Shell Navigation

```csharp
// Register routes in AppShell.xaml.cs
public AppShell()
{
    InitializeComponent();
    
    Routing.RegisterRoute("details", typeof(DetailsPage));
    Routing.RegisterRoute("modal", typeof(ModalPage));
}

// Navigate
await Shell.Current.GoToAsync("details");
await Shell.Current.GoToAsync("details", new Dictionary<string, object>
{
    ["ItemId"] = itemId
});

// Go back
await Shell.Current.GoToAsync("..");

// Modal
await Shell.Current.GoToAsync("modal", new Dictionary<string, object>
{
    ["Animated"] = true
});
```

### Step 7: Migrate Platform-Specific Code

#### Xamarin DependencyService

```csharp
// Xamarin
[assembly: Dependency(typeof(MyService))]
public class MyService : IMyService
{
    public void DoSomething() { }
}

// Usage
DependencyService.Get<IMyService>().DoSomething();
```

#### MAUI Dependency Injection

```csharp
// Register in MauiProgram.cs
builder.Services.AddSingleton<IMyService, MyService>();

// Inject in constructor
public MainPage(IMyService myService)
{
    _myService = myService;
}
```

#### Platform-Specific Code Location

**Xamarin**:
```
??? MyApp.Android/
?   ??? MainActivity.cs
??? MyApp.iOS/
?   ??? AppDelegate.cs
```

**MAUI**:
```
??? Platforms/
?   ??? Android/
?   ?   ??? MainActivity.cs
?   ??? iOS/
?   ?   ??? AppDelegate.cs
?   ??? MacCatalyst/
?   ??? Windows/
```

### Step 8: Update Resource Files

#### Images

**Xamarin**: Different folders per platform
```
??? Android/Resources/drawable/
??? iOS/Resources/
```

**MAUI**: Single Resources folder
```
??? Resources/
?   ??? Images/
?       ??? myimage.png
```

Usage remains the same:
```xml
<Image Source="myimage.png" />
```

#### Fonts

**Xamarin**: Platform-specific configuration

**MAUI**: Configure in MauiProgram.cs
```csharp
builder.ConfigureFonts(fonts =>
{
    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
});
```

### Step 9: Update Xamarin.Essentials Usage

Good news: Xamarin.Essentials is now built into MAUI!

```csharp
// Both Xamarin and MAUI (no change needed)
var location = await Geolocation.GetLocationAsync();
var connected = Connectivity.NetworkAccess == NetworkAccess.Internet;
await Share.RequestAsync(new ShareTextRequest { Text = "Hello" });
```

## Platform-Specific Migration

### Android Migration

#### Update MainActivity

**Xamarin.Forms**:
```csharp
[Activity(Label = "MyApp", Theme = "@style/MainTheme", MainLauncher = true)]
public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
        
        LoadApplication(new App());
    }
}
```

**MAUI**:
```csharp
[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true)]
public class MainActivity : MauiAppCompatActivity
{
    // Initialization handled by MAUI
}
```

#### Update AndroidManifest.xml

Permissions and configuration move to `Platforms/Android/AndroidManifest.xml`

### iOS Migration

#### Update AppDelegate

**Xamarin.Forms**:
```csharp
[Register("AppDelegate")]
public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
{
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
        global::Xamarin.Forms.Forms.Init();
        LoadApplication(new App());
        
        return base.FinishedLaunching(app, options);
    }
}
```

**MAUI**:
```csharp
[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
```

#### Update Info.plist

Configuration moves to `Platforms/iOS/Info.plist`

## Common Migration Challenges

### Challenge 1: Custom Renderers

**Xamarin Custom Renderer**:
```csharp
[assembly: ExportRenderer(typeof(MyControl), typeof(MyControlRenderer))]
public class MyControlRenderer : ViewRenderer<MyControl, NativeControl>
{
    // Implementation
}
```

**MAUI Handler**:
```csharp
public partial class MyControlHandler : ViewHandler<MyControl, NativeControl>
{
    public static IPropertyMapper<MyControl, MyControlHandler> PropertyMapper =
        new PropertyMapper<MyControl, MyControlHandler>(ViewMapper)
        {
            [nameof(MyControl.MyProperty)] = MapMyProperty
        };
    
    public MyControlHandler() : base(PropertyMapper)
    {
    }
    
    protected override NativeControl CreatePlatformView()
    {
        return new NativeControl();
    }
    
    public static void MapMyProperty(MyControlHandler handler, MyControl view)
    {
        // Update native control
    }
}
```

### Challenge 2: Effects

**Xamarin Effect**:
```csharp
public class ShadowEffect : PlatformEffect
{
    protected override void OnAttached() { }
    protected override void OnDetached() { }
}
```

**MAUI Platform Behavior**:
```csharp
public class ShadowBehavior : PlatformBehavior<View>
{
    protected override void OnAttachedTo(View bindable) { }
    protected override void OnDetachedFrom(View bindable) { }
}
```

### Challenge 3: MessagingCenter

**Xamarin MessagingCenter**:
```csharp
MessagingCenter.Send(this, "Message", data);
MessagingCenter.Subscribe<T>(this, "Message", callback);
```

**MAUI Alternatives**:

Use ReactiveUI MessageBus (recommended):
```csharp
MessageBus.Current.SendMessage(data);
MessageBus.Current.Listen<DataType>().Subscribe(callback);
```

Or use WeakReferenceMessenger:
```csharp
WeakReferenceMessenger.Default.Send(new MyMessage(data));
WeakReferenceMessenger.Default.Register<MyMessage>(this, (r, m) => callback(m));
```

## ReactiveUI-Specific Changes

### Namespace Changes

```csharp
// Xamarin
using ReactiveUI.XamForms;

// MAUI
using ReactiveUI.Maui;
```

### Base Classes Remain the Same

```csharp
// Both work the same
ReactiveContentPage<TViewModel>
ReactiveContentView<TViewModel>
ReactiveShell<TViewModel>
```

### Routing Support

ReactiveUI routing works seamlessly with MAUI Shell:

```csharp
public class AppViewModel : ReactiveObject, IScreen
{
    public RoutingState Router { get; } = new RoutingState();
    
    public AppViewModel()
    {
        // Navigate using ReactiveUI routing
        Router.Navigate.Execute(new PageViewModel(this))
            .Subscribe();
    }
}
```

## Testing Considerations

### Unit Tests

No changes needed for ViewModels:

```csharp
[Fact]
public async Task ViewModel_LoadsData()
{
    var vm = new MainViewModel();
    await vm.LoadCommand.Execute();
    
    Assert.NotNull(vm.Data);
}
```

### UI Tests

Update from Xamarin.UITest to .NET MAUI compatible testing:

- Use Appium for cross-platform testing
- Update selectors and automation IDs
- Test on newer OS versions

## Performance Optimization

### MAUI-Specific Optimizations

```xml
<!-- Enable compilation -->
<PropertyGroup>
    <UseInterpreter>false</UseInterpreter>
    <PublishTrimmed>true</PublishTrimmed>
</PropertyGroup>
```

### Startup Performance

```csharp
// MauiProgram.cs
builder.ConfigureMauiHandlers(handlers =>
{
    // Only register handlers you use
    handlers.AddHandler<Entry, EntryHandler>();
});
```

## Migration Checklist

### Pre-Migration
- [ ] Update to latest Xamarin.Forms (if possible)
- [ ] Document custom renderers and effects
- [ ] Review DependencyService usage
- [ ] List platform-specific code
- [ ] Backup current solution

### During Migration
- [ ] Create new MAUI project
- [ ] Copy and update shared code
- [ ] Migrate ViewModels to modern patterns
- [ ] Update XAML namespaces
- [ ] Migrate navigation to Shell
- [ ] Convert custom renderers to handlers
- [ ] Update platform-specific code
- [ ] Migrate resources (images, fonts)
- [ ] Update package references

### Post-Migration
- [ ] Test on all platforms
- [ ] Update CI/CD pipelines
- [ ] Review and optimize performance
- [ ] Update documentation
- [ ] Train team on MAUI differences
- [ ] Remove Xamarin projects

## Troubleshooting

### Build Errors

**Error**: `The name 'Forms' does not exist`

**Solution**: Update namespaces from `Xamarin.Forms` to `Microsoft.Maui.Controls`

### Runtime Errors

**Error**: `Could not load assembly`

**Solution**: Ensure all packages are MAUI-compatible versions

### Layout Issues

**Problem**: Layouts don't look the same

**Solution**: Review layout changes in MAUI:
- `StackLayout` ? `VerticalStackLayout`/`HorizontalStackLayout`
- Update spacing and padding values
- Test on actual devices

## Resources and Tools

### Official Tools

- [.NET Upgrade Assistant](https://dotnet.microsoft.com/platform/upgrade-assistant): Automated migration tool
- [MAUI Migration Analyzer](https://github.com/dotnet/maui/wiki/Migration-Analyzer): Visual Studio analyzer

### Microsoft Documentation

- [Xamarin.Forms to MAUI Migration](https://learn.microsoft.com/dotnet/maui/migration/)
- [MAUI Documentation](https://learn.microsoft.com/dotnet/maui/)
- [Platform Differences](https://learn.microsoft.com/dotnet/maui/migration/forms-projects)

### Community Resources

- [ReactiveUI Samples](https://github.com/reactiveui/ReactiveUI.Samples)
- [MAUI Community Toolkit](https://github.com/CommunityToolkit/Maui)
- [ReactiveUI Slack](https://join.slack.com/t/reactivex/shared_invite/zt-lt48skpz-G5WDYOAuzA80_MByZrLT0g)

## Timeline and Effort

**Small App** (5-10 screens):
- Migration: 1-2 weeks
- Testing: 1 week

**Medium App** (10-30 screens):
- Migration: 3-4 weeks
- Testing: 2 weeks

**Large App** (30+ screens):
- Migration: 6-12 weeks
- Testing: 3-4 weeks

Add time for:
- Complex custom renderers
- Extensive platform-specific code
- Third-party package compatibility

## Getting Help

If you encounter issues during migration:

1. Check [MAUI GitHub Issues](https://github.com/dotnet/maui/issues)
2. Ask on [ReactiveUI Slack](https://join.slack.com/t/reactivex/shared_invite/zt-lt48skpz-G5WDYOAuzA80_MByZrLT0g)
3. Review [Migration Documentation](https://learn.microsoft.com/dotnet/maui/migration/)
4. Post on [Stack Overflow](https://stackoverflow.com/questions/tagged/maui) with tags: `maui`, `reactiveui`

---

**Migration Difficulty**: Moderate to Complex
**Recommended Timeline**: Plan 2-12 weeks depending on app size
**Long-term Benefits**: Modern platform, better performance, ongoing support
