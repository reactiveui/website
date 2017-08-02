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

You need to assign a function that creates a new AppState when there is none persisted. And the driver that is used for persistence.

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

    App.Initialize ();

    suspendHelper = new AutoSuspendHelper (this);
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
    App.Initialize ();

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

