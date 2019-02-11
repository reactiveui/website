Taking our classic ViewModel, we are going to decide what is important to save upon application death/resume. We specifically do not save the state of commands because they are recreated by the constructor. It's debatable if you were to keep the Search Results, maybe that's a concern of your Akavache implementation. But DEFINITELY you want to save the SearchQuery, as when that is rehydrated it should restore the viewmodel to the exact state it was in.

```cs
[DataContract]
public class SearchViewModel : ReactiveObject, ISearchViewModel
{
    private readonly ObservableAsPropertyHelper<IEnumerable<SearchResults>> _searchResults;
    private readonly ISearchService searchService;
    private string searchQuery;
    
    public SearchViewModel(ISearchService searchService) 
    { 
        // Reactive commands and OAPHs initialization 
        // logic that is deliberately omitted.     
    }

    [IgnoreDataMember]
    public IEnumerable<SearchResults> SearchResults => _searchResults.Value;
     
    [IgnoreDataMember]
    public ReactiveCommand<Unit, IEnumerable<SearchResults>> Search { get; }
    
    [DataMember]
    public string SearchQuery 
    {
        get => searchQuery;
        set => this.RaiseAndSetIfChanged(ref searchQuery, value);
    }
}
```

If you use Newtonsoft.Json library for serialization, you can replace the `[DataContract]` attribute with `[JsonObject]` attribute, use `[JsonIgnore]` instead of `[IgnoreDataMember]`, and use `[JsonProperty]` instead of `[DataMember]`, although Newtonsoft.Json fully supports [DataContract attributes](https://www.newtonsoft.com/json/help/html/DataContractAndDataMember.htm). Note, that `Newtonsoft.Json` uses opt-out approach by default, in contrast to `DataContractSerializer` which uses opt-in. Opt-out means that all public fields and properties will be serialized, unless you explicitly ignore them by placing `[IgnoreDataMember]` or `[JsonIgnore]` attributes, opt-in means the opposite.

# Suspension

Make sure you `[IgnoreDataMember]` (or `[JsonIgnore]`) the HostScreen or you will get a circular reference. You should apply the pattern described above to every ViewModel which state is going to be serialized and restored. ViewModel serialization is Tricky Business, you have to decide what to serialize and what to recreate. Some stuff you should recalculate/reload when the app wakes up instead of trying to save it out.

### AutoSuspendHelper

The AutoSuspendHelper can help you in persisting your AppState. In this example we create an AppState that generates a random new Guid and persists it. So every App installation has unique key persisted. There are several steps in creating a AppState. You need to have a object for the AppState itself. You need a SuspensionDriver to persist the data. Then you need to wire it all together.

### SuspensionDriver

Here is an implementation that uses [Akavache](https://github.com/reactiveui/Akavache) for its persistense. The SuspensionDriver is platform independent, tested on iOS, Android, WPF, UWP, etc. 

```cs
public class AkavacheSuspensionDriver<TAppState> : ISuspensionDriver where TAppState : class
{
    const string appStateKey = "appState";
  
    public AkavacheSuspensionDriver() => BlobCache.ApplicationName = "Your Application Name";

    public IObservable<Unit> InvalidateState() => BlobCache.UserAccount.InvalidateObject<TAppState>(appStateKey);
  
    public IObservable<object> LoadState() => BlobCache.UserAccount.GetObject<TAppState>(appStateKey);

    public IObservable<Unit> SaveState(object state) => BlobCache.UserAccount.InsertObject(appStateKey, (TAppState)state);
}
```

### AppState

The AppState is an object with `DataContract` or `Newtonsoft.Json` notations, the AppState is platform independent.

```cs
[JsonObject]
public class AppState
{
    public string AuthToken = Guid.NewGuid().ToString();
}
```

# Wiring It Together

### Platform independent

You need to assign a function that creates a new AppState when there is none persisted. And the driver that is used for persistence. This can be done in the PCL for example, or, if it's an application for only one platform, in the application startup code.

```cs
RxApp.SuspensionHost.CreateNewAppState = () => new AppState();
RxApp.SuspensionHost.SetupDefaultSuspendResume(new AkavacheSuspensionDriver<AppState>());
```

### Android

For Android you need to implement the `Android.App.Application.IActivityLifecycleCallbacks` interface. Then add the following to the Application class:

```cs
AutoSuspendHelper suspendHelper;

public MyApplication(IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) { }

public void OnActivityCreated(Activity activity, Bundle savedInstanceState) { }

public void OnActivityDestroyed(Activity activity) { }

public void OnActivityPaused(Activity activity) { }

public void OnActivityResumed(Activity activity) { }

public void OnActivitySaveInstanceState(Activity activity, Bundle outState) { }

public void OnActivityStarted(Activity activity) { }

public void OnActivityStopped(Activity activity) { }

public override void OnCreate()
{
    base.OnCreate();
    suspendHelper = new AutoSuspendHelper(this);
    RxApp.SuspensionHost.CreateNewAppState = () => new AppState();
    RxApp.SuspensionHost.SetupDefaultSuspendResume(new AkavacheSuspensionDriver<AppState>());
}
```

### iOS

For iOS you need to add the following to the AppDelegate:

```cs
readonly AutoSuspendHelper autoSuspendHelper;

public AppDelegate()
{
    autoSuspendHelper = new AutoSuspendHelper(this);
}

public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
{
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
```

### Retrieving the appstate

When you want to store or retrieve data you can get the AppState object with the following code:

```cs
var appState = RxApp.SuspensionHost.GetAppState<AppState>();
```
