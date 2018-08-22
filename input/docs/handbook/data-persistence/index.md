# Data Persistence

Taking our classic ViewModel, we are going to decide what is important
to save upon application death/resume. We specifically do not save the
state of commands because they are recreated by the CTOR.

It's debatable if you were to keep the Search Results (maybe that's a
concern of your Akavache implementation)

But DEFINITELY you want to save the SearchQuery, as when that is
rehydrated it should restore the viewmodel to the exact state it was
in..

    [DataContract]
    public class SearchViewModel : ISearchViewModel
    {
        [IgnoreDataMember]
        public ObservableCollection<SearchResults> SearchResults { get; set; }
    
    
        private string searchQuery;
        [DataMember]
        public string SearchQuery {
            get { return searchQuery; }
            set { this.RaiseAndSetIfChanged(ref searchQuery, value); }
     
        }
     
        [IgnoreDataMember]
        public ReactiveCommand<List<SearchResults>> Search { get; set; }
    
        [IgnoreDataMember] 
        public ISearchService SearchService { get; set; }
    }


# Suspension
  
    Make sure you [IgnoreDataMember] the HostScreen or you will get a circular reference.
    
    phil.cleveland [1:57 PM] 
    @paulcbetts: Cool.  Do I do that for every VM?
    
    paulcbetts [2:07 PM] 
    Yeah
    
    phil.cleveland [2:08 PM] 
    Cool.  Cleaning it all up now. Adding serialization and IViewFor to CB
    
    phil.cleveland [2:09 PM]
    Other than the [Attributes] does it all just work Magically?
    
    paulcbetts [2:14 PM] 
    Yep
    
    paulcbetts [2:14 PM]
    Well, kinda
    
    paulcbetts [2:14 PM]
    ViewModel serialization is Tricky Business
    
    paulcbetts [2:14 PM]
    You have to decide what to serialize and what to recreate
    
    paulcbetts [2:14 PM]
    Some stuff you should recalculate / reload when the app wakes up instead of trying to save it out
    
    phil.cleveland [2:16 PM] 
    Ok.  The one I am really thinking about now is a timer.  I have a screen with a count down timer and notifications.  So even if the app goes away I want that timer running.
    
    paulcbetts [2:16 PM] 
    Hm, Android definitely won't let you do that directly
    
    paulcbetts [2:16 PM]
    Your app will eventually get knocked out
    
    paulcbetts [2:17 PM]
    You need what's called a "Service" to do that
    
    phil.cleveland [2:17 PM] 
    hmm.  Well...I guess I could at least store the timestamp for start.....
    
    phil.cleveland [2:17 PM]
    Ok.  I'll look into it
    
    paulcbetts [2:17 PM] 
    ^^ that works
    
    phil.cleveland [2:17 PM] 
    I mean the standard clock that comes with....does more or less what I want
    
    paulcbetts [2:17 PM] 
    Oh wait, maybe you don't - there's a built-in API to wake your own app up at certain times as I recall (edited)
    
    phil.cleveland [2:18 PM] 
    Oh...that would be cool.  If I could set a promise so to speak.  Like ...hey come talk to me in 20 minutes.
    
    kentcb [2:20 PM] 
    @paulcbetts: (regarding a conversation yesterday) I always thought the thing `ObservableForProperty` could do that `WhenAny` can not is specify `beforeChange`. That is, be notified about the old value that is about to be swapped out for the new.
    
    kentcb [2:20 PM]
    I use this quite a bit to proactively clean up disposable stuff
    
    kentcb [2:21 PM]
    e.g.
    ```public SomeDisposableType Property
    {
        // usual get/set here
    }
    
    // elsewhere
    this
        .ObservableForProperty(x => x.Property, beforeChange: true)
        .Where(x => x != null)
        .Subscribe(x => x.Dispose());
    ```
    
    kentcb [2:21 PM]
    is this doable with `WhenAnyValue` somehow?
    
    phil.cleveland [2:22 PM] 
    could you use WhenActivated?
    
    kentcb [2:22 PM] 
    it's not necessarily about activation - more for when some disposable instance is being swapped out for another
    
    paulcbetts [2:23 PM] 
    You should use `SerialDisposable` instead
    
    paulcbetts [2:23 PM]
    It'll change your life :simple_smile:
    
    kentcb [2:24 PM] 
    I see, so just assign the new instance to the `SerialDisposable`
    
    phil.cleveland [2:24 PM] 
    Oh man.  I just learned WhenActivate....now comes along a trump
    
    paulcbetts [2:24 PM] 
    @kentcb: Got it
    
    kentcb [2:24 PM] 
    hmm, very cool - different way of thinking about it
    
    paulcbetts [2:25 PM] 
    `this.WhenAnyValue(x => x.MyDisposableProp).Subscribe(x => latestDisposableProp.Disposable = x);  // Trashes the old one on assign


    paulcbetts [2:26 PM] 
    ```this.WhenAnyValue(x => x.MyDisposableProp)
        .Subscribe(x => latestDisposableProp.Disposable = x != null ? x : Disposable.Empty);

    kentcb [2:27 PM] 
    nice
    
    phil.cleveland [2:27 PM] 
    So would this be something you would do for instance VM1 has a ReactiveObject prop which changes based on state?
    
    kentcb [2:29 PM] 
    is it disposable though?
    
    phil.cleveland [2:30 PM] 
    The VM....I guess in this particular case no
    
    phil.cleveland [2:31 PM]2:31
    I guess I don't impl IDisposable that often TBH.  Wonder if I am missing some leaks
    
    kentcb [2:34 PM] 
    I've gotten into the practice of following *any* call to `Subscribe` or `Connect` or `Bind` et cetera with my own `AddTo` extension:
    ```this.someProperty = this.WhenAnyValue(x => x.SomeProperty)
    .Select(x => x == null)
    .ToProperty(this, x => x.SomeOtherProperty)
    .AddTo(this.disposables);
    ```
    (edited)
    
    kentcb [2:34 PM]
    and `AddTo` is an extension method that adds any `IDisposable` to a `CompositeDisposable`
    
    kentcb [2:34 PM]
    and it's the `CompositeDisposable` that I dispose of
    
    phil.cleveland [2:35 PM] 
    Per a discussion with Paul the other day I think that is only necessary in the code behind
    
    kentcb [2:35 PM] 
    in my `Dispose()`
    
    phil.cleveland [2:35 PM] 
    Yea @kentcb I saw your DisposableReactiveObject in the excercise app.  I think that could be part of RxUI
    
    phil.cleveland [2:36 PM]
    I actually think that looks cleaner than using the WhenActivated too.  Again I might be misunderstanding when each is appropriate
    
    phil.cleveland [2:37 PM]
    this is what my CB WhenAny's look like
    ```this.WhenActivated (d => {
                d (this.WhenAnyValue (x => x.ViewModel.AnticipatedWortLossVolume).Subscribe (x => this.stpWortLoss.Value = x));
            });
    ```
    
    phil.cleveland [2:38 PM]
    the curlys and parens can cause a headache sometimes

    kentcb [2:43 PM] 
    yeah, could be a little neater with an extension method:
    ```this.WhenActivated(
        d => this
                .WhenAnyValue(x => x.ViewModel.AnticipatedWortLossVolume)
                .Subscribe(x => this.stpWortLoss.Value = x)
                .AddTo(d));
    ```
    (just using `AddTo` as example name - might be able to come up with a better one) (edited)
    
    kentcb [2:45 PM]
    am a little surprised that `WhenActivated` doesn't have an overload giving you a `CompositeDisposable`. That would make things a bit easier
    
    rdavisau [2:56 PM] 
    ^ I have used that approach too, @kentcb

    paulcbetts [3:03 PM] 
    So, the rule is, you only have to dispose subscriptions if the thing you're subscribing to outlives your own object, and your Observable never ends (edited)
    
    paulcbetts [3:04 PM]
    If you're an item in a ListView, and you subscribe to something on MainWindow, you need to Dispose it
    
    paulcbetts [3:05 PM]
    Unfortunately, DependencyObjects use static properties everywhere to back everything and they suck
    
    paulcbetts [3:05 PM]
    So, _all_ subscriptions that go through DependencyProperties are effectively scoped to the same as App (i.e. forever)
    
    paulcbetts [3:06 PM]
    So, Rule #2 is, if you WhenAny through a DependencyProperty, you need to Dispose it
    
    phil.cleveland [3:06 PM] 
    So that makes sense why pretty much all the WhenAny's in the code behind require it
    
    paulcbetts [3:06 PM] 
    Nailed it
    
    phil.cleveland [3:09 PM] 
    Anyone know if there is there the equivelent of Break on Any Exception in Xam Studio.  Really hard to debug sometimes.  Just adding a StartsWith  is crashing me, but I am not sure why
    
    
    
    # AutoSuspendHelper

The AutoSuspendHelper can help you in persisting your AppState. In this example i create a AppState that generates a random new Guid and persists it. So every App installation has unique key persisted.

## Usage

There are several steps in creating a AppState. You need to have a object for the AppState itself. You need a SuspensionDriver to persist the data. Then you need to wire it all together.

### SuspensionDriver

I used a implementation that uses Akavache for its persistense. The SuspensionDriver is platform independent

```
public class AkavacheSuspensionDriver<TAppState> : ISuspensionDriver where TAppState : class
{
	const string appStateKey = "appState";


	public IObservable<Unit> InvalidateState ()
	{
		return BlobCache.UserAccount.InvalidateObject<TAppState> (appStateKey);
	}

	public IObservable<object> LoadState ()
	{
		return BlobCache.UserAccount.GetObject<TAppState> (appStateKey);
	}

	public IObservable<Unit> SaveState (object state)
	{
		return BlobCache.UserAccount.InsertObject (appStateKey, (TAppState)state);
	}
}
```

### AppState

The AppState is a object with Newtonsoft.Json notations The AppState is platform independent

```
[JsonObject]
public class AppState
{
	public string AuthToken = Guid.NewGuid ().ToString ();
}
```

### Wiring it together

#### Platform independent

You need to assign a function that creates a new AppState when there is none persisted. And the driver that is used for persistence. This can be done in the PCL for example. Or if it's a application for one platform in the application startup code.

```
RxApp.SuspensionHost.CreateNewAppState = () => new AppState ();
RxApp.SuspensionHost.SetupDefaultSuspendResume (new AkavacheSuspensionDriver<AppState> ());
```

#### Android

For Android you need to implement the `Android.App.Application.IActivityLifecycleCallbacks` interface. Then add the following to the Application class

```
AutoSuspendHelper suspendHelper;

public MyApplication (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer)
{
}

public void OnActivityCreated (Activity activity, Bundle savedInstanceState)
{
}

public void OnActivityDestroyed (Activity activity)
{
}

public void OnActivityPaused (Activity activity)
{
}

public void OnActivityResumed (Activity activity)
{
}

public void OnActivitySaveInstanceState (Activity activity, Bundle outState)
{
}

public void OnActivityStarted (Activity activity)
{
}

public void OnActivityStopped (Activity activity)
{
}

public override void OnCreate ()
{
    base.OnCreate ();

    suspendHelper = new AutoSuspendHelper (this);

    RxApp.SuspensionHost.CreateNewAppState = () => new AppState ();
    RxApp.SuspensionHost.SetupDefaultSuspendResume (new AkavacheSuspensionDriver<AppState> ());
}
```

#### iOS

For iOS you need to add the following to the AppDelegate

```
readonly AutoSuspendHelper autoSuspendHelper;

public AppDelegate ()
{
    autoSuspendHelper = new AutoSuspendHelper (this);
}

public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
{
    RxApp.SuspensionHost.CreateNewAppState = () => new AppState ();
    RxApp.SuspensionHost.SetupDefaultSuspendResume (new AkavacheSuspensionDriver<AppState> ());

    autoSuspendHelper.FinishedLaunching (application, launchOptions);

    return true;
}

public override void DidEnterBackground (UIApplication application)
{
    autoSuspendHelper.DidEnterBackground (application);
}

public override void OnActivated (UIApplication application)
{
    autoSuspendHelper.OnActivated (application);
}
```

## Retrieving the appstate
When you want to store or retrieve data you can get the AppState object with the following code
```
var appState = RxApp.SuspensionHost.GetAppState<AppState> ();
```

