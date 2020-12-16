Taking our classic ViewModel, we are going to decide what is important to save upon application death/resume. We specifically do not save the state of commands because they are recreated by the constructor. It's debatable if you were to keep the Search Results, maybe that's a concern of your Akavache implementation. But DEFINITELY you want to save the SearchQuery, as when that is rehydrated it should restore the viewmodel to the exact state it was in.

```cs
[DataContract]
public class SearchViewModel : ReactiveObject, ISearchViewModel
{
    private readonly ObservableAsPropertyHelper<IEnumerable<SearchResults>> _searchResults;
    private readonly ISearchService searchService;
    private string searchQuery;
    
    public SearchViewModel(ISearchService searchService = null) 
    { 
        this.searchService = searchService ?? Locator.Current.GetService<ISearchService>();

        var canSearch = this
            .WhenAnyValue(x => x.SearchQuery)
            .Select(query => !string.IsNullOrWhiteSpace(query));

        Search = ReactiveCommand.CreateFromTask(
            () => this.searchService.Search(SearchQuery),
            canSearch);
        
        _searchResults = Search.ToProperty(this, x => x.SearchResults);
    }

    public IEnumerable<SearchResults> SearchResults => _searchResults.Value;
     
    public ReactiveCommand<Unit, IEnumerable<SearchResults>> Search { get; }
    
    [DataMember]
    public string SearchQuery 
    {
        get => searchQuery;
        set => this.RaiseAndSetIfChanged(ref searchQuery, value);
    }
}
```

Surely, it's possible to use Newtonsoft.Json suspension driver and its attributes, such as `[JsonProperty]` or `[JsonIgnore]`, but remember, that in this case Newtonsoft.Json uses opt-out approach, in contrast to `DataContractSerializer` which uses opt-in. Opt-out means that all public fields and properties will be serialized, unless you explicitly ignore them by placing `[IgnoreDataMember]` or `[JsonIgnore]` attributes, opt-in means the opposite. Newtonsoft.Json fully supports [DataContract attributes](https://www.newtonsoft.com/json/help/html/DataContractAndDataMember.htm), but when `[DataContract]` attributes are used, it fallbacks to opt-in approach, similarly to `DataContractSerializer`.

Make sure you `[IgnoreDataMember]` or `[JsonIgnore]` the HostScreen if your serializer uses opt-out approach, or you will get a circular reference. You should apply the pattern described above to every ViewModel which state is going to be serialized and restored. ViewModel serialization is Tricky Business, you have to decide what to serialize and what to recreate. Some stuff you should recalculate/reload when the app wakes up instead of trying to save it out.

# Suspension

The `AutoSuspendHelper` class can help you in persisting your `AppState`. In the example below we create an `AppState` that generates a random new Guid and persists it. So every App installation has unique key persisted. There are several steps in creating an `AppState`. You need to have an object for the AppState itself (there are cases when it can be your root view model). You need a `SuspensionDriver` to persist the data. Then you need to wire it all together in the app composition root.

## 1. Create the Suspension Driver

Create a class that implements the `ISuspensionDriver` interface. There are several production-ready implementations below, especially the Akavache suspension driver that works on any platform supported by ReactiveUI. We highly recommend using [Akavache](https://github.com/reactiveui/akavache) suspension driver. Xamarin.iOS and Universal Windows Platform will replicate [Akavache](https://github.com/reactiveui/akavache) data to the cloud and synchronize it to all user devices on which the app is installed!

<details><summary>Akavache Platform Independent Suspension Driver</summary>
<p>

Here is an implementation that uses [Akavache](https://github.com/reactiveui/Akavache) for its persistense. The `SuspensionDriver` is platform independent, tested on iOS, Android, WPF, UWP, etc. 

```cs
public class AkavacheSuspensionDriver<TAppState> : ISuspensionDriver where TAppState : class
{
    private const string AppStateKey = "appState";
  
    public AkavacheSuspensionDriver() => BlobCache.ApplicationName = "Your Application Name";

    public IObservable<Unit> InvalidateState() => BlobCache.UserAccount.InvalidateObject<TAppState>(AppStateKey);
  
    public IObservable<object> LoadState() => BlobCache.UserAccount.GetObject<TAppState>(AppStateKey);

    public IObservable<Unit> SaveState(object state) => BlobCache.UserAccount.InsertObject(AppStateKey, (TAppState)state);
}
```

</p>
</details>

<details><summary>Newtonsoft.JSON Suspension Driver</summary>
<p>

Here is an implementation that uses [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) with `TypeNameHandling.All` json serialization setting for its persistense. The type information is included into the serialized JSON file, which means that `RoutingState` navigation stack consisting of `IRoutableViewModel`s could be restored. However, this driver isn't compatible with UWP. If you'd like to persist UWP state to a file, use `StorageFile` APIs instead of `System.IO.File`. You can also use [Xamarin.Essentials SecureStorage APIs](https://docs.microsoft.com/en-us/xamarin/essentials/secure-storage?tabs=android) to read and write serialized JSON objects, `SecureStorage.SetAsync` and  `SecureStorage.GetAsync`. 

```cs
public class NewtonsoftJsonSuspensionDriver : ISuspensionDriver
{
    private readonly string _stateFilePath;
    private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public NewtonsoftJsonSuspensionDriver(string stateFilePath) => _stateFilePath = stateFilePath;

    public IObservable<Unit> InvalidateState()
    {
        if (File.Exists(_stateFilePath)) 
            File.Delete(_stateFilePath);
        return Observable.Return(Unit.Default);
    }

    public IObservable<object> LoadState()
    {
        if (!File.Exists(_stateFilePath))
            return Observable.Throw<object>(new FileNotFoundException(_stateFilePath));
        var lines = File.ReadAllText(_stateFilePath);
        var state = JsonConvert.DeserializeObject<object>(lines, _settings);
        return Observable.Return(state);
    }

    public IObservable<Unit> SaveState(object state)
    {
        var lines = JsonConvert.SerializeObject(state, Formatting.Indented, _settings);
        File.WriteAllText(_stateFilePath, lines);
        return Observable.Return(Unit.Default);
    }
}
```

</p>
</details>

## 2. Define the AppState Object

The `AppState` is an object with `DataContract` notations, the `AppState` is platform independent. You can use your root ViewModel as your `AppState` retrieving services from `Locator.Current.GetService<T>` for [dependency inversion feature](/docs/handbook/dependency-inversion/) to work.

```cs
[DataContract]
public class AppState
{
    [DataMember]
    public string AuthToken = Guid.NewGuid().ToString();
}
```

## 3. Wire It Together

You need to assign a function that creates a new AppState when there is none persisted. And the driver that is used for persistence. This can be done in the PCL for example, or, if it's an application for only one platform, in the application startup code. Remember to call the `SetupDefaultSuspendResume` driver initializer only after initializing the platform-specific `AutoSuspendHelper`, otherwise the things won't get properly initialized. See the platform-specific examples below.

### Android

For Android, add the following to your `MainActivity` class:

```cs
[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
public class MainActivity : Activity
{
    private AutoSuspendHelper autoSuspendHelper;

    public override void OnCreate(Bundle bundle)
    {
        // Initialize the suspension driver after AutoSuspendHelper. 
        this.autoSuspendHelper = new AutoSuspendHelper(this.Application);
        RxApp.SuspensionHost.CreateNewAppState = () => new AppState();
        RxApp.SuspensionHost.SetupDefaultSuspendResume(new AkavacheSuspensionDriver<AppState>());
        base.OnCreate(bundle);
    }
}
```

### iOS

For iOS you need to add the following to the AppDelegate:

```cs
[Register("AppDelegate")]
public class AppDelegate : UIApplicationDelegate
{
    private readonly AutoSuspendHelper autoSuspendHelper;

    public AppDelegate() => autoSuspendHelper = new AutoSuspendHelper(this);

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        // Initialize the suspension driver after AutoSuspendHelper.
        RxApp.SuspensionHost.CreateNewAppState = () => new AppState();
        RxApp.SuspensionHost.SetupDefaultSuspendResume(new AkavacheSuspensionDriver<AppState>());
        autoSuspendHelper.FinishedLaunching(application, launchOptions);
        return true;
    }

    public override void DidEnterBackground(UIApplication application)
    {
        autoSuspendHelper.DidEnterBackground(application);
    }

    public override void OnActivated(UIApplication application)
    {
        autoSuspendHelper.OnActivated(application);
    }
}
```

### UWP

For UWP, add somewhat like the following to your `App.xaml.cs` file:

```cs
sealed partial class App : Application
{
    private readonly AutoSuspendHelper autoSuspendHelper;

    public App()
    {
        // Initialize the suspension driver after AutoSuspendHelper.
        autoSuspendHelper = new AutoSuspendHelper(this);
        RxApp.SuspensionHost.CreateNewAppState = () => new AppState();
        RxApp.SuspensionHost.SetupDefaultSuspendResume(new AkavacheSuspensionDriver<AppState>());
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        // Notice the line below as well.
        this.autoSuspendHelper.OnLaunched(e);
    
        if (!(Window.Current.Content is Frame rootFrame))
        {
            rootFrame = new Frame();
            Window.Current.Content = rootFrame;
        }
        if (e.PrelaunchActivated == false)
        {
            if (rootFrame.Content == null)
                rootFrame.Navigate(typeof(MainView), e.Arguments);
            Window.Current.Activate();
        }
    }
}
```

### WPF

For WPF, add the following to your `App.xaml.cs` file:

```cs
public partial class App : Application
{
    private readonly AutoSuspendHelper autoSuspendHelper;

    public App()
    {
        // Initialize the suspension driver after AutoSuspendHelper.
        this.autoSuspendHelper = new AutoSuspendHelper(this);
        RxApp.SuspensionHost.CreateNewAppState = () => new AppState();
        RxApp.SuspensionHost.SetupDefaultSuspendResume(new AkavacheSuspensionDriver<AppState>());
    }
}
```

### Xamarin.Forms

We also ship a `AutoSuspendHelper` implementation for Xamarin.Forms. If you decide to set up `AutoSuspendHelper` inside your Xamarin.Forms project, then you won't need to set up auto suspension in your Android and iOS projects that are platform-specific parts of your Xamarin.Forms app. Add the following to your Xamarin.Forms `App.xaml.cs` file:

```cs
public partial class App : Application
{
   private readonly AutoSuspendHelper _autoSuspendHelper;

   public App()
   {
     _autoSuspendHelper = new AutoSuspendHelper();
     RxApp.SuspensionHost.CreateNewAppState = () => new MainState();
     RxApp.SuspensionHost.SetupDefaultSuspendResume(new CustomSuspensionDriver());
     _autoSuspendHelper.OnCreate();

     InitializeComponent();
     MainPage = new MainView();
   }

   protected override void OnStart() => _autoSuspendHelper.OnStart();

   protected override void OnResume() => _autoSuspendHelper.OnResume();

   protected override void OnSleep() => _autoSuspendHelper.OnSleep();
}
```

### AvaloniaUI

> **Note** See [Avalonia Data Persistence](https://avaloniaui.net/docs/reactiveui/suspension) for more info. Don't forget to add a call to `UseReactiveUI()` to your `AppBuilder`.

## Retrieving the AppState

The application state will be serialized and persisted using the `ISuspensionDriver` once the application gets closed, suspended or deactivated, depending on the platform. When you want to update data or retrieve data you can get the `AppState` object with the following code:

```cs
var appState = RxApp.SuspensionHost.GetAppState<AppState>();
```

If you use your root view model as the app state object, then most likely you need to call the `GetAppState` method once in your composition root and assign the result to your root `DataContext` or `BindingContext`. When your application gets closed or suspended, the root view model state will be saved to the disc.
